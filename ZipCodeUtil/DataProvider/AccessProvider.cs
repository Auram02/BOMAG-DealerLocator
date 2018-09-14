#region FILE HEADER
/// <project>ZipCodeUtil</project>
/// <assembly>SagaraSoftware.ZipCodeUtil.dll</assembly>
/// <filename>AccessProvider.cs</filename>
/// <creator>Jon Sagara</creator>
/// <description>
/// Contains the AccessProvider class, which is used to read data from MS Access. 
/// </description>
/// <copyright>
/// Copyright (c) 2004 Sagara Software.  All rights reserved.
/// </copyright>
/// <disclaimer>
/// This file is provided "as is" with no expressed or implied warranty.  The author accepts no 
///  liability for any damage/loss of business that this product may cause.
/// </disclaimer>
/// <history>
///	<change date="12/29/2004" changedby="Jon Sagara">File created.</changed>
/// </history>
#endregion

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Data.SqlClient;


namespace SagaraSoftware.ZipCodeUtil
{
	/// <summary>
	/// AccessProvider implements the IDataProvider interface, interacting with an MS Access 
	/// database.
	/// </summary>
	public class AccessProvider : IDataProvider
	{
		#region CONSTRUCTORS
		/// <summary>
		/// Default constructor.  Does nothing.
		/// </summary>
		public AccessProvider ()
		{
		}
		#endregion


		#region IDataProvider Members


        /// <summary>
        /// Look up a <see cref="SagaraSoftware.ZipCodeUtil.Location" /> by ZIP Code.  If Latitude
        ///  or Longitude are NULL, they are set to Double.MinValue.
        /// </summary>
        /// <param name="inZipCode">ZIP Code to lookup.</param>
        /// <returns><see cref="SagaraSoftware.ZipCodeUtil.Location" /> of the ZIP Code.</returns>
        public Location DoLookupByZipCode(string inZipCode)
        {
            OdbcConnection objCSV = null;
            OdbcCommand oCmd = null;
            Location loc = null;
            string strConnString = Globals.sConnectionString;
            string sql;

            //if (null == strConnString || string.Empty == strConnString)
            //    throw new ApplicationException ("You must provide a connection string for your MS Access database.");

            //// ##################################################
            //// old
            ////sql.Append ("SELECT * FROM ZIP_CODES WHERE ZIP = ?");

            //// new
            //sql.Append("SELECT * FROM " + Globals.CSVFileName + " WHERE ZIP = ?");
            //// ##################################################


            //objCSV = new OdbcConnection(strConnString);
            //objCSV.Open();

            //oCmd = new OdbcCommand(sql.ToString(), objCSV);
            //oCmd.Parameters.Add (new OdbcParameter ("ZIP", inZipCode));


            //OdbcDataReader oDR = oCmd.ExecuteReader();

            sql = "SELECT * FROM [DL.ZipLookup] WHERE ZIP_CODE = " + inZipCode;
            SqlConnection cn = new SqlConnection(strConnString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int counter = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string loopZip = ds.Tables[0].Rows[i]["ZIP_CODE"].ToString();

                if (loopZip == inZipCode)
                {
                    counter = i;
                    i = ds.Tables[0].Rows.Count + 2;
                }
            }

            loc = new Location();

            try
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //if (oDR.Read ())
                    //{
                    loc.ZipClass = (string)ds.Tables[0].Rows[counter][0].ToString();
                    loc.CityAlias = (string)ds.Tables[0].Rows[counter]["CITY_ALIAS_NAME"].ToString();
                    loc.City = (string)ds.Tables[0].Rows[counter]["CITY"].ToString();
                    loc.State = (string)ds.Tables[0].Rows[counter]["STATE"].ToString();
                    loc.ZipCode = (string)ds.Tables[0].Rows[counter]["ZIP_CODE"].ToString();
                    loc.County = (string)ds.Tables[0].Rows[counter]["COUNTY_NAME"].ToString();
                    loc.Latitude = Convert.ToDouble(ds.Tables[0].Rows[counter]["LATITUDE"].ToString());
                    loc.Longitude = Convert.ToDouble(ds.Tables[0].Rows[counter]["LONGITUDE"].ToString());

                }
                else
                {
                    loc.ZipCode = "0";
                }

                //loc.City = Convert.ToString (oDR["CITY"]);
                //loc.State = Convert.ToString (oDR["STATE"]);
                //loc.ZipCode = inZipCode;
                //loc.County = Convert.ToString (oDR["COUNTY"]);
                //loc.Latitude = (DBNull.Value == oDR["LATITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LATITUDE"]));
                //loc.Longitude = (DBNull.Value == oDR["LONGITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LONGITUDE"]));
                //loc.ZipClass = Convert.ToString (oDR["ZIP_CLASS"]);
                //}
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error getting data from database", e);
            }
            finally
            {
                //if (null != oDR)
                //    oDR.Close ();
                //if (null != objCSV)
                //    objCSV.Close ();
            }

            return loc;
        }

        /// <summary>
        /// Look up a <see cref="SagaraSoftware.ZipCodeUtil.Location" /> by ZIP Code.  If Latitude
        ///  or Longitude are NULL, they are set to Double.MinValue.
        /// </summary>
        /// <param name="inZipCode">ZIP Code to lookup.</param>
        /// <returns><see cref="SagaraSoftware.ZipCodeUtil.Location" /> of the ZIP Code.</returns>
        public Location DoLookupByCityZipCode(string inZipCode, string inCity)
        {
            OdbcConnection objCSV = null;
            OdbcCommand oCmd = null;
            Location loc = null;
            string strConnString = Globals.sConnectionString;
            string sql;

            //if (null == strConnString || string.Empty == strConnString)
            //    throw new ApplicationException ("You must provide a connection string for your MS Access database.");

            //// ##################################################
            //// old
            ////sql.Append ("SELECT * FROM ZIP_CODES WHERE ZIP = ?");

            //// new
            //sql.Append("SELECT * FROM " + Globals.CSVFileName + " WHERE ZIP = ?");
            //// ##################################################


            //objCSV = new OdbcConnection(strConnString);
            //objCSV.Open();

            //oCmd = new OdbcCommand(sql.ToString(), objCSV);
            //oCmd.Parameters.Add (new OdbcParameter ("ZIP", inZipCode));


            //OdbcDataReader oDR = oCmd.ExecuteReader();

            sql = "SELECT * FROM [DL.ZipLookup] WHERE ZIP_CODE = " + inZipCode;
            SqlConnection cn = new SqlConnection(strConnString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int counter = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string loopZip = ds.Tables[0].Rows[i]["ZIP_CODE"].ToString();
                string loopCity = ds.Tables[0].Rows[i]["CITY_ALIAS_NAME"].ToString();
                if ((loopZip == inZipCode) && (loopCity.ToUpper() == inCity.ToUpper()))
                {
                    counter = i;
                    i = ds.Tables[0].Rows.Count + 2;
                }
            }

            loc = new Location();

            try
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //if (oDR.Read ())
                    //{
                    loc.ZipClass = (string)ds.Tables[0].Rows[counter][0].ToString();
                    loc.CityAlias = (string)ds.Tables[0].Rows[counter]["CITY_ALIAS_NAME"].ToString();
                    loc.City = (string)ds.Tables[0].Rows[counter]["CITY"].ToString();
                    loc.State = (string)ds.Tables[0].Rows[counter]["STATE"].ToString();
                    loc.ZipCode = (string)ds.Tables[0].Rows[counter]["ZIP_CODE"].ToString();
                    loc.County = (string)ds.Tables[0].Rows[counter]["COUNTY_NAME"].ToString();
                    loc.Latitude = Convert.ToDouble(ds.Tables[0].Rows[counter]["LATITUDE"].ToString());
                    loc.Longitude = Convert.ToDouble(ds.Tables[0].Rows[counter]["LONGITUDE"].ToString());

                }
                else
                {
                    loc.ZipCode = "0";
                }

                //loc.City = Convert.ToString (oDR["CITY"]);
                //loc.State = Convert.ToString (oDR["STATE"]);
                //loc.ZipCode = inZipCode;
                //loc.County = Convert.ToString (oDR["COUNTY"]);
                //loc.Latitude = (DBNull.Value == oDR["LATITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LATITUDE"]));
                //loc.Longitude = (DBNull.Value == oDR["LONGITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LONGITUDE"]));
                //loc.ZipClass = Convert.ToString (oDR["ZIP_CLASS"]);
                //}
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error getting data from database", e);
            }
            finally
            {
                //if (null != oDR)
                //    oDR.Close ();
                //if (null != objCSV)
                //    objCSV.Close ();
            }

            return loc;
        }



        /// <summary>
        /// NOT DONE: Do Not Use
        /// </summary>
        /// <param name="inZipCode">ZIP Code to lookup.</param>
        /// <returns><see cref="SagaraSoftware.ZipCodeUtil.Location" /> of the ZIP Code.</returns>
        public Location DoLookupByCityStateZipCode(string inZipCode, string inCity, string inState)
        {
            OdbcConnection objCSV = null;
            OdbcCommand oCmd = null;
            Location loc = null;
            string strConnString = Globals.sConnectionString;
            string sql;


            sql = "SELECT * FROM [DL.ZipLookup] WHERE ZIP_CODE = " + inZipCode;
            SqlConnection cn = new SqlConnection(strConnString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);


            loc = new Location();

            try
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //if (oDR.Read ())
                    //{
                    loc.ZipClass = (string)ds.Tables[0].Rows[0][0].ToString();
                    loc.CityAlias = (string)ds.Tables[0].Rows[0]["CITY_ALIAS_NAME"].ToString();
                    loc.City = (string)ds.Tables[0].Rows[0]["CITY"].ToString();
                    loc.State = (string)ds.Tables[0].Rows[0]["STATE"].ToString();
                    loc.ZipCode = (string)ds.Tables[0].Rows[0]["ZIP_CODE"].ToString();
                    loc.County = (string)ds.Tables[0].Rows[0]["COUNTY_NAME"].ToString();
                    loc.Latitude = Convert.ToDouble(ds.Tables[0].Rows[0]["LATITUDE"].ToString());
                    loc.Longitude = Convert.ToDouble(ds.Tables[0].Rows[0]["LONGITUDE"].ToString());

                }
                else
                {
                    loc.ZipCode = "0";
                }

                //loc.City = Convert.ToString (oDR["CITY"]);
                //loc.State = Convert.ToString (oDR["STATE"]);
                //loc.ZipCode = inZipCode;
                //loc.County = Convert.ToString (oDR["COUNTY"]);
                //loc.Latitude = (DBNull.Value == oDR["LATITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LATITUDE"]));
                //loc.Longitude = (DBNull.Value == oDR["LONGITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LONGITUDE"]));
                //loc.ZipClass = Convert.ToString (oDR["ZIP_CLASS"]);
                //}
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error getting data from database", e);
            }
            finally
            {
                //if (null != oDR)
                //    oDR.Close ();
                //if (null != objCSV)
                //    objCSV.Close ();
            }

            return loc;
        }


        /// <summary>
        /// Lookup one or more <see cref="SagaraSoftware.ZipCodeUtil.Location" />s by City/State.
        ///  In the database, some cities are represented by more than one ZIP Code.
        /// </summary>
        /// <param name="inCity">Name of the City.</param>
        /// <param name="inState">Name of the State.</param>
        /// <returns>An array of <see cref="SagaraSoftware.ZipCodeUtil.Location" /> objects whose City/State matches the input City/State.</returns>
        public Location DoLookupByCityState (string inCity, string inState)
        {
        //    OleDbConnection oleConn = null;
        //    OleDbCommand oleCmd = null;
        //    OleDbDataReader oleReader = null;
        //    ArrayList locs = new ArrayList ();
        //    string strConnString = Globals.sConnectionString;
        //    StringBuilder sql = new StringBuilder ();

        //    if (null == strConnString || string.Empty == strConnString)
        //        throw new ApplicationException ("You must provide a connection string for your MS Access database.");

            //    sql.Append ("SELECT * FROM [DL.ZipLookup] WHERE CITY = ? AND STATE = ? ORDER BY ZIP");
            OdbcConnection objCSV = null;
            OdbcCommand oCmd = null;
            Location loc = null;
            string strConnString = Globals.sConnectionString;
            string sql;

            sql = "SELECT * FROM [DL.ZipLookup] WHERE [CITY_ALIAS_NAME] = '" + inCity + "' AND STATE = '" + inState + "' AND CITY != 'DHS' ORDER BY ZIP_CODE";
            SqlConnection cn = new SqlConnection(strConnString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            loc = new Location();

			try
			{

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //if (oDR.Read ())
                    //{
                    loc.ZipClass = (string)ds.Tables[0].Rows[0][0].ToString();
                    loc.CityAlias = (string)ds.Tables[0].Rows[0]["CITY_ALIAS_NAME"].ToString();
                    loc.City = (string)ds.Tables[0].Rows[0]["CITY"].ToString();
                    loc.State = (string)ds.Tables[0].Rows[0]["STATE"].ToString();
                    loc.ZipCode = (string)ds.Tables[0].Rows[0]["ZIP_CODE"].ToString();
                    loc.County = (string)ds.Tables[0].Rows[0]["COUNTY_NAME"].ToString();
                    loc.Latitude = Convert.ToDouble(ds.Tables[0].Rows[0]["LATITUDE"].ToString());
                    loc.Longitude = Convert.ToDouble(ds.Tables[0].Rows[0]["LONGITUDE"].ToString());
                    
                }
                else
                {
                    loc.ZipCode = "0";
                }

                    //loc.City = Convert.ToString (oDR["CITY"]);
                    //loc.State = Convert.ToString (oDR["STATE"]);
                    //loc.ZipCode = inZipCode;
                    //loc.County = Convert.ToString (oDR["COUNTY"]);
                    //loc.Latitude = (DBNull.Value == oDR["LATITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LATITUDE"]));
                    //loc.Longitude = (DBNull.Value == oDR["LONGITUDE"]) ? Double.MinValue : Double.Parse (Convert.ToString (oDR["LONGITUDE"]));
                    //loc.ZipClass = Convert.ToString (oDR["ZIP_CLASS"]);
                //}
			}
			catch (Exception e)
			{
				throw new ApplicationException ("Error getting data from database", e);
			}
			finally
			{
                //if (null != oDR)
                //    oDR.Close ();
                //if (null != objCSV)
                //    objCSV.Close ();
			}

			return loc;
        //    oleConn = new OleDbConnection (strConnString);
        //    oleCmd = new OleDbCommand (sql.ToString (), oleConn);
        //    oleCmd.Parameters.Add (new OleDbParameter ("CITY", inCity));
        //    oleCmd.Parameters.Add (new OleDbParameter ("STATE", inState));
        //    oleConn.Open ();
			
        //    try
        //    {
        //        oleReader = oleCmd.ExecuteReader ();
        //        string jon = oleCmd.CommandText;

        //        while (oleReader.Read ())
        //        {
        //            Location loc = new Location ();

        //            loc.City = Convert.ToString (oleReader["CITY"]);
        //            loc.State = Convert.ToString (oleReader["STATE"]);
        //            loc.ZipCode = Convert.ToString (oleReader["ZIP"]);
        //            loc.County = Convert.ToString (oleReader["COUNTY"]);
        //            loc.Latitude = Double.Parse (Convert.ToString (oleReader["LATITUDE"]));
        //            loc.Longitude = Double.Parse (Convert.ToString (oleReader["LONGITUDE"]));
        //            loc.ZipClass = Convert.ToString (oleReader["ZIP_CLASS"]);

        //            locs.Add (loc);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ApplicationException ("Error getting data from database", e);
        //    }
        //    finally
        //    {
        //        if (null != oleReader)
        //            oleReader.Close ();
        //        if (null != oleConn)
        //            oleConn.Close ();
        //    }

        //    return (Location[]) locs.ToArray (typeof (Location));

            //Location[] myLoc = new Location[1];
            //return myLoc;
        }


        /// <summary>
        /// Finds all <see cref="SagaraSoftware.ZipCodeUtil.LocationInRadius" />es within X miles
        ///  of inRefLoc.
        /// </summary>
        /// <remarks>
        /// To speed the calculation, this method finds all areas within a square area of dimension
        ///  (2*Radius)x(2*Radius).  Any city with a Lat/Lon pair that falls within this square is
        ///  returned.  However, only those cities whose distance is less than or equal to Radius
        ///  miles from inRefLoc are returned.  This has the unfortunate side effect of selecting
        ///  from ~22% more area than is necessary.
        /// </remarks>
        /// <param name="inRefLoc">The central location from which we are trying to find other locations within the specified radius.</param>
        /// <param name="inBounds">A class containing the "box" that encloses inRefLoc.  Used to approximate a circle of Radius R centered around the point inRefLoc.</param>
        /// <returns>0 or more <see cref="SagaraSoftware.ZipCodeUtil.LocationInRadius" />es that are
        ///  within Radius miles of inRefLoc.</returns>
        public LocationInRadius[] GetLocationsWithinRadius (Location inRefLoc, RadiusBox inBounds)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dataReader = null;
            ArrayList locs = new ArrayList();
            string strConnString = Globals.sConnectionString;
            StringBuilder sql = new StringBuilder();

            if (null == strConnString || string.Empty == strConnString)
                throw new ApplicationException("You must provide a connection string for your MS Access database.");

            //sql.Append("SELECT * FROM [DL.ZipLookup] WHERE ");
            //sql.Append("IIf(ISNULL(LATITUDE),999.0,CDbl(LATITUDE)) >= ? AND ");
            //sql.Append("IIf(ISNULL(LATITUDE),999.0,CDbl(LATITUDE)) <= ? AND ");
            //sql.Append("IIf(ISNULL(LONGITUDE),999.0,CDbl(LONGITUDE)) >= ? AND ");
            //sql.Append("IIf(ISNULL(LONGITUDE),999.0,CDbl(LONGITUDE)) <= ? ");
            //sql.Append("ORDER BY CITY, STATE, ZIP_CODE");

            sql.Append("SELECT * FROM [DL.ZipLookup] WHERE ");
            sql.Append("CAST(LATITUDE as FLOAT(20)) >= " + inBounds.BottomLine + " AND ");
            sql.Append("CAST(LATITUDE as FLOAT(20)) <= " + inBounds.TopLine + " AND ");
            sql.Append("CAST(LONGITUDE as FLOAT(20)) >= " + inBounds.LeftLine + " AND ");
            sql.Append("CAST(LONGITUDE as FLOAT(20)) <= " + inBounds.RightLine + " ");
            sql.Append("ORDER BY CITY, STATE, ZIP_CODE");
            //sql.Append("ORDER BY CITY_ALIAS_NAME, STATE, ZIP_CODE");

            conn = new SqlConnection(strConnString);
            cmd = new SqlCommand(sql.ToString(), conn);
            //cmd.Parameters.Add(new SqlParameter("SouthLat", inBounds.BottomLine));
            //cmd.Parameters.Add(new SqlParameter("NorthLat", inBounds.TopLine));
            //cmd.Parameters.Add(new SqlParameter("WestLong", inBounds.LeftLine));
            //cmd.Parameters.Add(new SqlParameter("EastLong", inBounds.RightLine));
            conn.Open();

            LocationInRadius loc = new LocationInRadius();

            try
            {
                dataReader = cmd.ExecuteReader();


                while (dataReader.Read())
                {
                    loc = new LocationInRadius();

                    loc.City = Convert.ToString(dataReader["CITY"]);
                    loc.CityAlias = Convert.ToString(dataReader["CITY_ALIAS_NAME"]);
                    loc.State = Convert.ToString(dataReader["STATE"]);
                    loc.ZipCode = Convert.ToString(dataReader["ZIP_CODE"]);
                    loc.County = Convert.ToString(dataReader["COUNTY_NAME"]);
                    loc.Latitude = Double.Parse(Convert.ToString(dataReader["LATITUDE"]));
                    loc.Longitude = Double.Parse(Convert.ToString(dataReader["LONGITUDE"]));
                    
                    loc.DistanceToCenter = Distance.GetDistance(inRefLoc, loc);

                    if (loc.DistanceToCenter <= inBounds.Radius)
                        locs.Add(loc);
                }
            }
            catch (Exception e)
            {
                //throw new ApplicationException("Error getting data from database", e);

                loc.ZipCode = "0";
            }
            finally
            {
                if (null != dataReader)
                    dataReader.Close();
                if (null != conn)
                    conn.Close();
                locs.Sort(new LocationInRadiusComparer());
            }

            return (LocationInRadius[])locs.ToArray(typeof(LocationInRadius));

        }

        #endregion


        ///// <summary>
        ///// Allows for sorting of an ArrayList of <see cref="SagaraSoftware.ZipCodeUtil.LocationInRadius" /> 
        /////  objects by DistanceToCenter, ascending.
        ///// </summary>
        private class LocationInRadiusComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                LocationInRadius lx = (LocationInRadius)x;
                LocationInRadius ly = (LocationInRadius)y;

                if (lx.DistanceToCenter < ly.DistanceToCenter)
                    return -1;
                else if (lx.DistanceToCenter > ly.DistanceToCenter)
                    return 1;
                else
                    return 0;
            }
        }
	}
}
