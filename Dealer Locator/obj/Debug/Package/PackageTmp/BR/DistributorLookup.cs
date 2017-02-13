using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;


using SagaraSoftware.ZipCodeUtil;


namespace Dealer_Locator.BR
{
    public class DistributorLookup
    {

        public DistributorLookup()
        {
            ProductID = 0;
        }

        public DistributorLookup(int productID, string cityName, string stateName, string zipCode = "")
        {
            ProductID = productID;
            CityName = cityName;
            StateName = stateName;

            UserLocation = ZipCodeUtil.LookupByCityState(CityName, StateName);

            if (UserLocation.State == null)
                UserLocation = ZipCodeUtil.LookupByZipCode(zipCode);
            
        }



        public int ProductID;
        public string CityName;
        public string StateName;
        public Location UserLocation;


        public DA.ContractTDS.DistributorRow GetClosestDistributor(out double shortestDistance, out Location shortestLocation, int productCategoryId, int isManufacturerRep = -1)
        {

            try
            {
                DA.ContractTDS.DistributorDataTable dt = new Dealer_Locator.DA.ContractTDS.DistributorDataTable();

                try
                {

                    // Get all of the distributors that have a contract in the county the user selected
                    // The user's county is stored in the object "UserLocation"
                    dt = GetDistributorsInCounty(UserLocation);
                    bool splitCountyExists = false;

 
                    if (CheckValidDistributors(dt,isManufacturerRep) == false)
                    {
                        dt = GetDistributorsInState(UserLocation);

                        if (CheckValidDistributors(dt, isManufacturerRep) == false)
                        {
                            dt = GetDistributorsInCountry(UserLocation);
                        }
                    }
                    else
                    {
                        // distributor found in county.  Check if the distributor is part of a county split...
                        //foreach (DA.ContractTDS.DistributorRow distRow in dt.Rows)

                        // this might be wrong.  I need to make sure I am getting the right row...
                        //DA.ContractTDS.DistributorRow distRow = ((DA.ContractTDS.DistributorRow)dt.Rows[0]);


                        string ZipCode2 = UserLocation.ZipCode;

                        string StateName2 = UserLocation.State;
                        int StateID2 = DDA.DataAccess.Location_da.GetStateID2(StateName2);

                        string CityName2 = UserLocation.City;

                        string CountyName = UserLocation.County;


                        string sql;
                        //DataSet ds = new DataSet();
                        //sql = "SELECT COUNTY_NAME FROM [DL.ZipLookup] WHERE ZIP_CODE = '" + ZipCode2 + "' AND CITY = '" + CityName2 + "'";

                        //ds = DA.DataAccess.Read(sql);

                        try
                        {
                            //if (ds.Tables[0].Rows.Count > 0)
                            if (1 == 1)
                            {


                                int userCountyID = DDA.DataAccess.Location_da.GetCountyID(CountyName, StateID2);

                                // use "UserLocation" to look up coordinates (lat/long)
                                // use SplitCounty to determine if there is a split
                                // and get what type of split it is.  then compare lat/long and figure out which split qualifies as correct.
                                // have to trust that the first "split" brought back will define correctly if it is north/south or east/west split.

                                DA.SplitCountyTableAdapters.SplitCountyTableAdapter scta = new Dealer_Locator.DA.SplitCountyTableAdapters.SplitCountyTableAdapter();
                                DA.SplitCounty.SplitCountyDataTable scdt = new Dealer_Locator.DA.SplitCounty.SplitCountyDataTable();

                                scdt = scta.GetFakeCountyIDs_byCountyID(userCountyID);  // will get all split counties and their data

                                if (scdt.Rows.Count > 0)
                                {
                                    // split exists

                                    DA.SplitCounty.SplitCountyRow drTemp = ((DA.SplitCounty.SplitCountyRow)scdt.Rows[0]);

                                    string splitType = "";

                                    if (drTemp.NorthSouth != "None")
                                        splitType = "NorthSouth";
                                    else
                                        splitType = "EastWest";


                                    // now we have the type of split.  Time to determine which distributor is the winner
                                    double userLatitude = 0.0;
                                    double userLongitude = 0.0;


                                    DataSet ds2 = new DataSet();

                                    string sql2 = "SELECT LATITUDE, LONGITUDE FROM [DL.ZipLookup] WHERE City = '" + CityName2 + "' AND STATE = '" + StateName2 + "'";

                                    ds2 = DA.DataAccess.Read(sql2);


                                    userLatitude = Convert.ToDouble(ds2.Tables[0].Rows[0]["LATITUDE"].ToString());
                                    userLongitude = Convert.ToDouble(ds2.Tables[0].Rows[0]["LONGITUDE"].ToString());

                                    int winningFakeCountyID = -1;


                                    DA.SplitCounty.SplitCountyRow dr1 = ((DA.SplitCounty.SplitCountyRow)scdt.Rows[0]);
                                    DA.SplitCounty.SplitCountyRow dr2 = ((DA.SplitCounty.SplitCountyRow)scdt.Rows[1]);

                                    // check the coordinates and determine if they are above/below or left/right depending on the split
                                    // we need to have latitude/longitude of user to do this.
                                    if (splitType == "NorthSouth")
                                    {
                                        if (dr1.NorthSouth == "North")
                                            if (userLatitude >= dr1.latitude)
                                                winningFakeCountyID = dr1.fk_fakeCountyID;
                                            else
                                                winningFakeCountyID = dr2.fk_fakeCountyID;
                                        else
                                            if (userLatitude >= dr2.latitude)
                                                winningFakeCountyID = dr2.fk_fakeCountyID;
                                            else
                                                winningFakeCountyID = dr1.fk_fakeCountyID;



                                    }
                                    else
                                    {

                                        if (userLongitude >= dr1.longitude)
                                            winningFakeCountyID = dr1.fk_fakeCountyID;
                                        else
                                            winningFakeCountyID = dr2.fk_fakeCountyID;

                                    }

                                    // now that we have a fake county id, get the distributor that has a contract in that fake county for that product line...
                                    DA.ContractTDS.DistributorDataTable cdt = new Dealer_Locator.DA.ContractTDS.DistributorDataTable();
                                    DA.ContractTDSTableAdapters.DistributorTableAdapter dta = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();

                                    cdt = dta.GetDataByContract_CategoryIDCountyID(productCategoryId, winningFakeCountyID);


                                    double tempDistance2 = -1.00;
                                    shortestDistance = -1.00;
                                    Location tempLoc2 = new Location();
                                    shortestLocation = null;

                                    tempLoc2 = ZipCodeUtil.LookupByCityZipCode(ZipCode2, CityName2);

                                    tempDistance2 = UserLocation.DistanceFrom(tempLoc2);

                                    shortestDistance = tempDistance2;
                                    shortestLocation = tempLoc2;


                                    //return ((DA.ContractTDS.DistributorRow )cdt.Rows[0]);

                                    splitCountyExists = true;
                                }
                                else
                                {
                                    // split doesn't exist
                                    splitCountyExists = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // no split exists or an error occurred...carry on.
                            splitCountyExists = false;
                        }


                    }

                }
                catch
                {

                }

                DA.ContractTDS.DistributorDataTable tempDT = ((DA.ContractTDS.DistributorDataTable)dt.Copy());


                if (isManufacturerRep > -1)
                {
                    dt.Clear();
                    
                    foreach (DA.ContractTDS.DistributorRow distRow in tempDT.Rows)
                    {
                        if (isManufacturerRep == distRow.ManufacturerRep)
                        {
                            dt.ImportRow(distRow);
                        }
                    }
                }


                // Now should have all distributors with a contract in the given county
                // From here, determine closest distributor to UserZipCode.
                double tempDistance = -1.00;
                shortestDistance = -1.00;
                Location tempLoc = new Location();
                shortestLocation = null;

                DA.ContractTDS.DistributorRow drShortestDistance = null;

                string StateAbbreviation = "", CityName = "", ZipCode = "";
                int countloop = 0;
                // Determine the Distributor that is the closest to the user's zip-code
                foreach (DA.ContractTDS.DistributorRow distRow in dt.Rows)
                {

                    try
                    {



                        countloop = countloop + 1;
                        // Assign the state abbreviation
                        StateAbbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(distRow.fk_StateID);
                        CityName = distRow.CityName;
                        ZipCode = distRow.fk_ZipID;


                        tempLoc = ZipCodeUtil.LookupByCityZipCode(ZipCode, CityName);

                        tempDistance = UserLocation.DistanceFrom(tempLoc);

                        // If the temporary distance is less than the current shortest distance, or the shortest distance is still set to -1
                        if (tempDistance < shortestDistance || shortestDistance < 0)
                        {
                            shortestDistance = tempDistance;
                            shortestLocation = tempLoc;
                            drShortestDistance = distRow;
                        }

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("[" + countloop + "] CityName: " + CityName + " - " + ex.Message);
                    }
                    finally
                    {

                        // reset tempDistance for next round
                        tempDistance = -1.00;
                    }

                }

                // Check if the returned county is part of the "SPLIT" county



                return drShortestDistance;


            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;
                shortestDistance = -1.00;
                shortestLocation = null;
                return null;
            }
        }

        private bool CheckValidDistributors(DA.ContractTDS.DistributorDataTable dtDistributors, int isManufacturerRep)
        {
            bool containsValidDistributor = false;

            if (dtDistributors.Rows.Count > 0)
            {
                //if (isManufacturerRep == 1)
                //{
                //    foreach (DA.ContractTDS.DistributorRow dr in dtDistributors.Rows)
                //    {
                //        if (dr.ManufacturerRep == 1)
                //        {
                //            containsValidDistributor = true;
                //        }
                //    }
                //}
                //else
                //{
                //    containsValidDistributor = true;
                //}

                foreach (DA.ContractTDS.DistributorRow dr in dtDistributors.Rows)
                {
                    if (dr.ManufacturerRep == isManufacturerRep)
                    {
                        containsValidDistributor = true;
                    }
                }
                
            } else {
                containsValidDistributor = false;
            }

            return containsValidDistributor;
        }

        #region private DA.ContractTDS.DistributorDataTable GetDistributorsInCountry()

        /// <summary>
        /// Get all distributors that have a contract in the county the user selected
        /// </summary>
        /// <returns>DataTable that holds the distributors</returns>
        private DA.ContractTDS.DistributorDataTable GetDistributorsInCountry(Location UserLocation)
        {
            try
            {
                DataSet ds = new DataSet();

                ArrayList zipCodes = new ArrayList();

                Location[] locs = new Location[0];

                // Given a Zip Code, get which County it is in.  (handle through UserLocation.County)
                string userCounty;
                userCounty = UserLocation.County;

                string userState;
                userState = UserLocation.State;

                int userStateID;
                userStateID = DDA.DataAccess.Location_da.GetStateID2(userState);

                int userCategoryID;
                userCategoryID = ProductID;

                DA.ContractTDSTableAdapters.DistributorTableAdapter da = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();
                DA.ContractTDS.DistributorDataTable dt = da.GetDataByContract_CategoryID(userCategoryID);

                return dt;
            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;

                return null;
            }

        }

        #endregion

        #region private DA.ContractTDS.DistributorDataTable GetDistributorsInCounty()

        /// <summary>
        /// Get all distributors that have a contract in the county the user selected
        /// </summary>
        /// <returns>DataTable that holds the distributors</returns>
        private DA.ContractTDS.DistributorDataTable GetDistributorsInCounty(Location UserLocation)
        {
            try
            {
                DataSet ds = new DataSet();

                ArrayList zipCodes = new ArrayList();

                Location[] locs = new Location[0];


                // Given a Zip Code, get which County it is in.  (handle through UserLocation.County)
                string userCounty;
                userCounty = UserLocation.County;

                string userState;
                userState = UserLocation.State;

                int userStateID;
                userStateID = DDA.DataAccess.Location_da.GetStateID2(userState);


                // Given County, Get all contract in County which contain a certain product line (Category)
                // 1: Get CountyID from table [County]
                int userCountyID;
                userCountyID = DDA.DataAccess.Location_da.GetCountyID(userCounty, userStateID);

                string newUserCountyID = userCountyID.ToString();

                int userCategoryID;
                userCategoryID = ProductID;

                // should have a CountyID and a CategoryID by this point
                // 2: GetDistributors that have Contracts that are 1) In the User's County   2) In the User's selected Category
                ds = DDA.DataAccess.Contract_da.GetContractList(userCategoryID, userCountyID);

                DA.ContractTDSTableAdapters.DistributorTableAdapter da = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();

                // Add in the "split" county lookups here.  I will append the additional counties to the userCountyID as a comma seperated list
                DA.SplitCountyTableAdapters.SplitCountyTableAdapter scta = new Dealer_Locator.DA.SplitCountyTableAdapters.SplitCountyTableAdapter();
                DA.SplitCounty.SplitCountyDataTable scdt = new Dealer_Locator.DA.SplitCounty.SplitCountyDataTable();

                scdt = scta.GetFakeCountyIDs_byCountyID(userCountyID);


                DA.ContractTDS.DistributorDataTable dt = new Dealer_Locator.DA.ContractTDS.DistributorDataTable();
                DA.ContractTDS.DistributorDataTable dtTemp2 = da.GetDataByContract_CategoryIDCountyID(userCategoryID, Convert.ToInt32(newUserCountyID));

                try
                {

                    for (int i = 0; i < dtTemp2.Rows.Count; i++)
                        dt.ImportRow(dtTemp2.Rows[i]);
                }
                catch (Exception ex)
                {

                }

                foreach (DA.SplitCounty.SplitCountyRow dr in scdt.Rows)
                {

                    newUserCountyID = dr.fk_fakeCountyID.ToString();
                    DA.ContractTDS.DistributorDataTable dtTemp = da.GetDataByContract_CategoryIDCountyID(userCategoryID, Convert.ToInt32(newUserCountyID));


                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {

                        dt.ImportRow(dtTemp[i]);

                    }
                }





                //(ContractCounty.fk_countyID IN (@CountyID))


                return dt;
            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;

                return null;
            }

        }



        #endregion

        #region private DA.ContractTDS.DistributorDataTable GetDistributorsInState()

        /// <summary>
        /// Get all distributors that have a contract in the county the user selected
        /// </summary>
        /// <returns>DataTable that holds the distributors</returns>
        private DA.ContractTDS.DistributorDataTable GetDistributorsInState(Location UserLocation)
        {
            try
            {
                DataSet ds = new DataSet();

                ArrayList zipCodes = new ArrayList();

                Location[] locs = new Location[0];


                // Given a Zip Code, get which County it is in.  (handle through UserLocation.County)
                string userCounty;
                userCounty = UserLocation.County;

                string userState;
                userState = UserLocation.State;

                int userStateID;
                userStateID = DDA.DataAccess.Location_da.GetStateID2(userState);

                int userCategoryID;
                userCategoryID = ProductID;

                DA.ContractTDSTableAdapters.DistributorTableAdapter da = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();
                DA.ContractTDS.DistributorDataTable dt = da.GetDataByContract_CategoryIDStateID(userCategoryID, userStateID);

                return dt;
            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;

                return null;
            }

        }

        #endregion

    }
}
