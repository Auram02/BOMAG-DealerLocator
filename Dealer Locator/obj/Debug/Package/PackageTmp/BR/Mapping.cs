using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Collections;

namespace Dealer_Locator.BR
{

    public class Mapping
    {

        public struct FieldMap
        {
            public string FormField;
            public int CardPosition;
        }


        #region Constructor

        public Mapping()
        {
            DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

            int ActiveMapdID = Convert.ToInt32( mta.GetActiveMapID());

            LoadMapping(ActiveMapdID);

        }

        public Mapping(int pk_MapID)
        {


            LoadMapping(pk_MapID);

        }

        private void LoadMapping(int pk_MapID)
        {

            try
            {
                Dealer_Locator.DA.MapTDS.DL_MapDataTable mdt = new Dealer_Locator.DA.MapTDS.DL_MapDataTable();
                DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();


                MapFields = GetFieldMappings(pk_MapID);

                mdt = mta.GetDataByMapID(pk_MapID);

                DA.MapTDS.DL_MapRow mr = ((DA.MapTDS.DL_MapRow)mdt.Rows[0]);

                MapID = mr.pk_mapID;
                MapName = mr.mapName;
                Active = mr.active;

                ReadMethodName = GetReadMethodName(mr.fk_mapReadMethodID);
            }
            catch
            {

            }
        }

        #endregion

        #region Public Methods

        public static int AddNewMap(string MappingName)
        {
            int NewMapID = -1;

            try
            {
                int MapReadMethod = -1;

                DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter mrmta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter();
                MapReadMethod = Convert.ToInt32(mrmta.GetMinimumPK());


                NewMapID = DA.DataAccess.GetNextID("[DL.Map]", "pk_mapID");

                DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

                mta.InsertNewMap(NewMapID, MappingName, false, MapReadMethod);


            }
            catch (Exception)
            {

                NewMapID = -1;
            }

            return NewMapID;

        }

        public static bool DeleteMap(int MapID)
        {
            bool IsSuccessful = false;

            try
            {
                DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

                mta.DeleteMap(MapID);

                IsSuccessful = true;
            }
            catch
            {
                IsSuccessful = false;
            }


            return IsSuccessful;
        }

        public List<Mapping> GetAllMaps()
        {
            List<Mapping> returnList = new List<Mapping>();

            Dealer_Locator.DA.MapTDS.DL_MapDataTable mdt = new Dealer_Locator.DA.MapTDS.DL_MapDataTable();
            DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

            mdt = mta.GetData();

            foreach (DA.MapTDS.DL_MapRow mr in mdt.Rows)
            {
                Mapping tempMap = new Mapping(mr.pk_mapID);

                returnList.Add(tempMap);

            }

            return returnList;
        }

        public string GetReadMethodName(int ReadMethodID)
        {
            string returnName = "";

            DA.MapTDS.DL_MapReadMethodDataTable mrmdt = new Dealer_Locator.DA.MapTDS.DL_MapReadMethodDataTable();
            DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter mrmta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter();

            mrmdt = mrmta.GetDataByMethodID(ReadMethodID);

            //DA.MapTDS.DL_MapReadMethodRow mrmr = ((DA.MapTDS.DL_MapReadMethodRow)mrmdt.Rows[0]);

            //returnName = mrmr.MethodName;

            returnName = mrmdt[0].MethodName;

            return returnName;
        }


        public void RefreshFieldMappings()
        {
            MapFields.Clear();
            MapFields = GetFieldMappings(MapID);
        }

        public List<FieldMap> GetFieldMappings(int MapID)
        {
            List<FieldMap> returnList = new List<FieldMap>();

            DA.MapTDS.DL_MapFieldDataTable mfdt = new Dealer_Locator.DA.MapTDS.DL_MapFieldDataTable();
            DA.MapTDSTableAdapters.DL_MapFieldTableAdapter mfta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapFieldTableAdapter();

            mfdt = mfta.GetDataByMapID(MapID);

            foreach (DA.MapTDS.DL_MapFieldRow mfr in mfdt.Rows)
            {
                FieldMap temp = new FieldMap();

                temp.CardPosition = mfr.CardPosition;
                temp.FormField = mfr.FormField;

                returnList.Add(temp);
            }

            return returnList;

        }

        public int AddMapping(string FieldName, int CardPosition)
        {
            int NewFieldMapID = -1;

            DA.MapTDS.DL_MapFieldDataTable mfdt = new Dealer_Locator.DA.MapTDS.DL_MapFieldDataTable();
            DA.MapTDSTableAdapters.DL_MapFieldTableAdapter mfta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapFieldTableAdapter();

            try
            {
                NewFieldMapID = DA.DataAccess.GetNextID("[DL.MapField]", "pk_mapFieldID");

                mfta.InsertQuery(NewFieldMapID, MapID, FieldName, CardPosition);

                mfdt = mfta.GetDataByMapID(MapID);



            }
            catch (Exception ex)
            {
                NewFieldMapID = -1;
            }

            return NewFieldMapID;

        }

        public bool RemoveMapping(int FieldMapID)
        {
            bool IsMapRemoveSuccessful = false;

            try
            {

                DA.MapTDSTableAdapters.DL_MapFieldTableAdapter mfta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapFieldTableAdapter();

                mfta.DeleteQuery(FieldMapID);

                IsMapRemoveSuccessful = true;
            }
            catch (Exception ex)
            {
                IsMapRemoveSuccessful = false;
            }

            return IsMapRemoveSuccessful;

        }

        public static bool SetActive(int MappingID)
        {
            bool IsSuccessful = false;



            try
            {
                DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

                mta.SetAllInactive();
                mta.SetActive(MappingID);

                IsSuccessful = true;


            }
            catch
            {
                IsSuccessful = false;
            }

            return IsSuccessful;
        }

        public bool SetActive()
        {
            return SetActive(this.MapID);

        }

        public List<string> ProcessMap(string ReadMethod, string CardText)
        {
            List<string> returnList = new List<string>();

            switch (ReadMethod)
            {
                case "Experient":

                    returnList = WorldOfAsphalt_v1(CardText);

                    break;

                case "SmartReg":

                    returnList = SmartReg_v1(CardText);

                    break;

                case "Waste Expo":

                    returnList = WasteExpo_v1(CardText);

                    break;


                case "Convention Data Services":

                    returnList = WasteExpo_v1( CardText );

                    break;


                case "Epic":

                    returnList = Epic_v1( CardText );

                    break;

                case "CompuLead":

                    returnList = CompuLead_v1( CardText );

                    break;

                case "CompuLead_ARA":

                    returnList = CompuLead_ARA_v1( CardText );

                    break;

                case "ConExpo 2011":

                    returnList = ConExpo2011_v1(CardText);

                    break;

                default:

                    break;

            }

            return returnList;

        }

        private List<string> WorldOfAsphalt_v1( string CardText )
        {
            string[] tempList = CardText.Split( '^' );

            List<string> returnList = new List<string>();

            for ( int i = 0; i < tempList.Length; i++ )
            {
                string item = tempList[ i ];
                item = item.Replace( "@", "" );
                item = item.Replace( "?", "" );

                // Asterisk (*) has replaced the @ sign
                item = item.Replace( "*", "@" );

                item = item.Replace( "&", "" );


                returnList.Add( item );
            }

            return returnList;
        }


        private List<string> SmartReg_v1(string CardText)
        {
            CardText = CardText.Replace(@"\", "^");

            string[] tempList = CardText.Split('^');

            List<string> returnList = new List<string>();

            for (int i = 0; i < tempList.Length; i++)
            {
                string item = tempList[i];
                item = item.Replace("@", "");
                item = item.Replace("?", "");

                // Asterisk (*) has replaced the @ sign
                item = item.Replace("*", "@");

                returnList.Add(item);
            }

            return returnList;
        }

        private List<string> WasteExpo_v1( string CardText )
        {
            string[] tempList = CardText.Split( '$' );

            List<string> returnList = new List<string>();

            for ( int i = 0; i < tempList.Length; i++ )
            {
                string item = tempList[ i ];

                returnList.Add( item );
            }

            return returnList;
        }

        private List<string> Epic_v1( string CardText )
        {
            string[] tempList = CardText.Split( '\t' );

            List<string> returnList = new List<string>();

            for ( int i = 0; i < tempList.Length; i++ )
            {
                string item = tempList[ i ];

                returnList.Add( item );
            }

            return returnList;
        }

        private List<string> CompuLead_v1( string CardText )
        {
            CardText = CardText.Replace( "", "" );
            string[] tempList = CardText.Split( '' );

            List<string> returnList = new List<string>();

            for ( int i = 0; i < tempList.Length; i++ )
            {
                string item = tempList[ i ];

                returnList.Add( item );
            }

            return returnList;
        }

        private List<string> CompuLead_ARA_v1( string CardText )
        {
            
            string[] tempList = CardText.Split( '|' );

            List<string> returnList = new List<string>();

            for ( int i = 0; i < tempList.Length; i++ )
            {
                string item = tempList[ i ];

                returnList.Add( item );
            }

            return returnList;
        }

        private List<string> ConExpo2011_v1(string CardText)
        {
            string[] tempList = CardText.Split('\t');

            List<string> returnList = new List<string>();

            for (int i = 0; i < tempList.Length; i++)
            {
                string item = tempList[i];

                returnList.Add(item);
            }

            return returnList;
        }
        

        public System.Collections.Specialized.NameValueCollection MapData(string CardText)
        {
            System.Collections.Specialized.NameValueCollection coll = new System.Collections.Specialized.NameValueCollection();

            coll.Add("", "");
            
            // get card data
            List<string> cardData = ProcessMap(this.ReadMethodName, CardText);

            for (int i = 0; i < cardData.Count; i++)
            {

                foreach (FieldMap fieldMapping in MapFields)
                {
                    if (fieldMapping.CardPosition == i)
                    {
                        string valueToAdd = cardData[i].ToString();

                        if (fieldMapping.FormField.ToUpper() == "TXTZIP2")
                        {
                            if (valueToAdd.Length > 5)
                            {
                                valueToAdd = valueToAdd.Substring(0, 5);
                            }
                        }   
                        

                        coll.Add(fieldMapping.FormField, valueToAdd);
                    }
                }

            }


            return coll;

        }

        #endregion


        #region private member variables

        private int _mapID = -1;
        private string _mapName = "";
        private bool _active = false;

        private string _ReadMethodName = "";

        public List<FieldMap> MapFields = new List<FieldMap>();


        #endregion


        #region public properties


        public int MapID
        {
            get { return _mapID; }
            set { _mapID = value; }
        }

        public string MapName
        {
            get { return _mapName; }
            set { _mapName = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public string ReadMethodName
        {
            get { return _ReadMethodName; }
            set { _ReadMethodName = value; }
        }

        #endregion







    }
}
