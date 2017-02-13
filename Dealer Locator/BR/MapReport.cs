using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Net;
using System.Xml;
using System.IO;

using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Linq;

namespace Dealer_Locator.BR
{
    public class MapReport
    {
        DA.MapReport _mapDA;

        Dictionary<int, CountyItem> _counties = new Dictionary<int, CountyItem>();
        Dictionary<int, GroupItem> _groups = new Dictionary<int, GroupItem>();
        Dictionary<int, OverlapItem> _overlaps = new Dictionary<int, OverlapItem>();
        List<GroupItem> _subGroups = new List<GroupItem>();
        private string _legendTitle = "";
        private StyleGenerator _solidColorGenerator = new StyleGenerator();
        private StyleGenerator _overlapColorGenerator = new StyleGenerator();

        private List<CountyJSON> _countiesJSON = new List<CountyJSON>();
        private List<string> _states = new List<string>();


        /// <summary>
        /// List of each county and the group it belongs to
        /// </summary>
        public Dictionary<int, CountyItem> Counties
        {
            get { return _counties; }
        }

        /// <summary>
        /// List of individual group items
        /// </summary>
        public Dictionary<int, GroupItem> Groups
        {
            get { return _groups; }
        }

        public List<GroupItem> SubGroups
        {
            get { return _subGroups; }
        }

        /// <summary>
        /// Overlap list.  Each overlap has a list of groupid's it contains
        /// </summary>
        public Dictionary<int, OverlapItem> Overlaps
        {
            get { return _overlaps; }
            set { _overlaps = value; }
        }

        public MapReport()
        {
            _mapDA = new DA.MapReport();
        }

        #region Supporting Classes/Structures

        public struct ReportItem
        {
            public List<CountyJSON> Counties;
            public Dictionary<string, GroupItem> Groups;
            public List<string> States;
            public List<GroupItem> SubGroups;
            public string LegendTitle;
        }

        public struct CountyItem
        {
            
            private int _groupID;
            private bool _overlap;
            private int? _overlapID;
            private StyleGenerator.StyleDefinition _style;
            private string _abbreviation;
            private string _stateName;
            private string _name;

            private bool _isSplit;
            private string _splitType;
            private string _splitLine;
            private string _splitCountyName;

            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            public bool Overlap
            {
                get { return _overlap; }
                set { _overlap = value; }
            }

            public int? OverlapID
            {
                get { return _overlapID; }
                set { _overlapID = value; }
            }

            public StyleGenerator.StyleDefinition Style
            {
                get { return _style; }
                set { _style = value; }
            }

            public string Abbreviation
            {
                get { return _abbreviation; }
                set { _abbreviation = value; }
            }

            public string StateName
            {
                get { return _stateName; }
                set { _stateName = value; }
            }
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public bool IsSplit
            {
                get { return _isSplit; }
                set { _isSplit = value; }
            }

            public string SplitType
            {
                get { return _splitType; }
                set { _splitType = value; }
            }

            public string SplitLine
            {
                get { return _splitLine; }
                set { _splitLine = value; }
            }

            public string SplitCountyName
            {
                get { return _splitCountyName; }
                set { _splitCountyName = value; }
            }

        }

        public struct CountyJSON
        {
            public int CountyID;
            public string Abbreviation;
            public string StateName;
            public string Name;
            public StyleGenerator.StyleDefinition Style;
            public int GroupID;
            public bool IsSplit;
            public string SplitType;
            public string SplitLine;
            public string SplitCountyName;
        }

        public class OverlapItem
        {
            private int _overlapID; 
            private List<int> _groupIDList;
            private int _groupID = -1;

            public int OverlapID
            {
                get { return _overlapID; }
                set { _overlapID = value; }
            }

            public List<int> GroupIDList
            {
                get
                {
                    if (_groupIDList == null)
                        _groupIDList = new List<int>();
                    return _groupIDList;
                }
                set
                {
                    _groupIDList = value;
                }
            }

            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }
        }

        public struct GroupItem
        {
            private int _groupID;
            private string _name;
            private StyleGenerator.StyleDefinition _style;
            private bool _isOverlap;
            private string _address;
            private string _city;
            private string _state;
            private string _stateAbbreviation;
            private string _contractState;
            private int _zip;
            private double _lat;
            private double _lng;

            public int GroupID
            {
                get { return _groupID; }
                set { _groupID = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public StyleGenerator.StyleDefinition Style
            {
                get { return _style; }
                set { _style = value; }
            }

            public bool IsOverlap
            {
                get { return _isOverlap; }
                set { _isOverlap = value; }
            }

            public string Address
            {
                get { return _address; }
                set { _address = value; }
            }

            public string City
            {
                get { return _city; }
                set { _city = value; }
            }

            public string State
            {
                get { return _state; }
                set { _state = value; }
            }

            public string StateAbbreviation
            {
                get { return _stateAbbreviation; }
                set { _stateAbbreviation = value; }
            }

            public int Zip
            {
                get { return _zip; }
                set { _zip = value; }
            }

            public string ContractState
            {
                get { return _contractState; }
                set { _contractState = value; }
            }

            public double Lat
            {
                get { return _lat; }
                set { _lat = value; }
            }

            public double Lng
            {
                get { return _lng; }
                set { _lng = value; }
            }
        }
        
        #endregion


        public DataTable GenerateGroupData(int categoryID, List<string> stateList)
        {

            string stateListString = "";

            foreach (string state in stateList)
            {
                if (stateListString.Length > 0)
                    stateListString += ",";

                stateListString += "'" + state + "'";
            }

            foreach (string state in stateList)
            {
                _states.Add(state);
            }

            DataTable dtSource = _mapDA.GetContracts(categoryID, stateListString);

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("GroupID");
            dt.Columns.Add("CountyID");
            dt.Columns.Add("Color");
            dt.Columns.Add("Abbreviation");
            dt.Columns.Add("CountyName");
            dt.Columns.Add("StateName");
            dt.Columns.Add("GroupStateName");
            dt.Columns.Add("GroupStateAbbreviation");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("Zip");

            List<string> states = new List<string>();

            foreach (DataRow dr in dtSource.Rows)
            {

                string groupName = dr["DistName"].ToString();
                int groupID = Convert.ToInt32(dr["pk_DistributorID"].ToString());
                int countyID = Convert.ToInt32(dr["CountyID"].ToString());
                string color = "";
                string abbreviation = dr["Abbreviation"].ToString();
                string countyName = dr["CountyName"].ToString();
                string stateName = dr["FullName"].ToString();
                string groupStateAbbreviation = dr["GroupStateAbbreviation"].ToString();
                string groupStateName = dr["GroupStateName"].ToString();
                string city = dr["CityName"].ToString();
                string address = dr["ShippingAddress"].ToString();
                string zip = dr["fk_ZipID"].ToString();

                DataRow newRow = dt.NewRow();
                newRow["Name"] = groupName;
                newRow["GroupID"] = groupID;
                newRow["CountyID"] = countyID;
                newRow["Abbreviation"] = abbreviation;
                newRow["CountyName"] = countyName;
                newRow["StateName"] = stateName;
                newRow["GroupStateName"] = groupStateName;
                newRow["GroupStateAbbreviation"] = groupStateAbbreviation;
                newRow["Color"] = color;
                newRow["Address"] = address;
                newRow["City"] = city;
                newRow["Zip"] = zip;
                dt.Rows.Add(newRow);
            }

            return dt;

        }

        public string BuildReportData(DataTable dtGroups, DataTable dtSubGroups, string legendTitle, List<string> stateList)
        {
            string result = "";
            int currentOverlapID = 0;

            _legendTitle = legendTitle;
            _states = stateList;

            Random rnd = new Random();

            foreach (DataRow dr in dtGroups.Rows)
            {

                string groupName = dr["Name"].ToString();
                int groupID = Convert.ToInt32(dr["GroupID"].ToString());
                int countyID = Convert.ToInt32(dr["CountyID"].ToString());
                string color = dr["Color"].ToString();
                string countyName = dr["CountyName"].ToString();
                string abbreviation = dr["Abbreviation"].ToString();
                string stateName = dr["StateName"].ToString();
                string groupStateName = dr["GroupStateName"].ToString();
                string groupStateAbbreviation = dr["GroupStateAbbreviation"].ToString();
                string city = dr["City"].ToString();
                string address = dr["Address"].ToString();

                bool isSplit = Convert.ToBoolean(dr["IsSplit"].ToString());
                string splitType = dr["SplitType"].ToString();
                string splitLine = dr["SplitLine"].ToString();
                string splitCountyName = dr["SplitCountyName"].ToString();

                string zipString = dr["Zip"].ToString();

                if (zipString.IndexOf("-") > -1)
                    zipString = zipString.Substring(0, dr["Zip"].ToString().IndexOf("-"));

                int zip = Convert.ToInt32(zipString);

                StyleGenerator.StyleDefinition styleDef = new StyleGenerator.StyleDefinition();

                AddGroup(groupName, groupID, address, city, groupStateName, groupStateAbbreviation, zip, styleDef);  // Try to add the group to the master group list (if exists)

                if (Counties.ContainsKey(countyID))
                {
                    CountyItem ci = Counties[countyID];

                    //  If not currently 
                    if (ci.Overlap == false)
                    {
                        ci.Overlap = true;

                        OverlapItem oi = new OverlapItem();
                        oi.GroupIDList.Add(ci.GroupID);
                        
                        if (oi.GroupIDList.Contains(groupID) == false)
                            oi.GroupIDList.Add(groupID);

                        currentOverlapID += 1;

                        oi.OverlapID = currentOverlapID;

                        Overlaps.Add(currentOverlapID, oi);

                        ci.GroupID = -1;    // overlapGroupID;
                        ci.OverlapID = currentOverlapID;

                        Counties[countyID] = ci;

                    }
                    else
                    {
                        int overlapID = Convert.ToInt32(ci.OverlapID);

                        OverlapItem oi = Overlaps[overlapID];

                        if (oi.GroupIDList.Contains(groupID) == false)
                            oi.GroupIDList.Add(groupID);

                    }

                }
                else
                {
                    // Create new county item
                    CountyItem ci = new CountyItem();
                    ci.GroupID = groupID;
                    ci.Name = countyName;
                    ci.Abbreviation = abbreviation;
                    ci.Overlap = false;
                    ci.StateName = stateName;

                    ci.IsSplit = isSplit;
                    ci.SplitType = splitType;
                    ci.SplitLine = splitLine;
                    ci.SplitCountyName = splitCountyName;
                    
                    Counties.Add(countyID, ci);

                }

            }


            foreach (DataRow dr in dtSubGroups.Rows)
            {

                string groupName = dr["Name"].ToString();
                int groupID = Convert.ToInt32(dr["GroupID"].ToString());
                string abbreviation = dr["Abbreviation"].ToString();
                string stateName = dr["StateName"].ToString();
                string city = dr["City"].ToString();
                string address = dr["Address"].ToString();
                string zipString = dr["Zip"].ToString();
                string contractState = dr["ContractState"].ToString();
                int distributorID = Convert.ToInt32(dr["pk_DistributorID"].ToString());

                GroupItem gi = new GroupItem();
                gi.GroupID = groupID;

                gi.Name = groupName;
                gi.Address = address;
                gi.City = city;
                gi.State = stateName;
                gi.StateAbbreviation = abbreviation;
                gi.Zip = Convert.ToInt32(zipString);
                gi.ContractState = contractState;

                gi.Lat = 0;
                gi.Lng = 0;


                if (dtSubGroups.Columns.Contains("Lat") && dtSubGroups.Columns.Contains("Lng") && dtSubGroups.Columns.Contains("GeoLastUpdated"))
                {
                    if (dr["GeoLastUpdated"] == null || dr["GeoLastUpdated"].ToString().Length == 0 || Convert.ToDateTime(dr["GeoLastUpdated"].ToString()) < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0, 0)))
                    {
                        // do geocode
                        gi = LookupGeoCode(gi, distributorID);
                    }
                    else
                    {
                        gi.Lat = Convert.ToDouble(dr["Lat"].ToString());
                        gi.Lng = Convert.ToDouble(dr["Lng"].ToString());
                    }
                }
                else
                {
                    gi = LookupGeoCode(gi, distributorID);
                }

                _subGroups.Add(gi);

            }


            DedupeOverlaps();
            FinishGroups();
            FinishCounties();
            result = GenerateJSON();    

            return result;

        }

        public static GroupItem Locate(GroupItem gi, string query)
        {
            WebRequest request = WebRequest
               .Create("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address="
                  + HttpUtility.UrlEncode(query));

            using (WebResponse response = request.GetResponse())
            {

                
                using (Stream stream = response.GetResponseStream())
                {
                    XDocument document = XDocument.Load(new StreamReader(stream));
                    
                    XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                    XElement latitudeElement = document.Descendants("lat").FirstOrDefault();

                    if (longitudeElement != null && latitudeElement != null)
                    {
                        gi.Lng = Double.Parse(longitudeElement.Value, System.Globalization.CultureInfo.InvariantCulture);
                        gi.Lat = Double.Parse(latitudeElement.Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }

            return gi;
        }

        private GroupItem LookupGeoCode(GroupItem gi, int distributorID)
        {
            gi.Lat = -1;
            gi.Lng = -1;

            string query = gi.Address + "," + gi.City + "," + gi.StateAbbreviation + " " + gi.Zip;
            
            gi = Locate(gi, query);

            if (gi.Lng == -1 && gi.Lat == -1)
            {
                query = gi.City + "," + gi.State + " " + gi.Zip;
                Locate(gi, query);
            }

            admin.Reports.ReportGeneration.DA.Report_da.UpdateDistributorGeo(distributorID, gi.Lat, gi.Lng);

            return gi;
        }

        private string GenerateJSON()
        {
            string json = "";

            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var groupDict = _groups.ToDictionary(item => item.Key.ToString(), item => item.Value);

            ReportItem ri = new ReportItem();
            ri.Counties = _countiesJSON;
            ri.Groups = groupDict;
            ri.States = _states;
            ri.LegendTitle = _legendTitle;
            ri.SubGroups = _subGroups;
            
            json = jss.Serialize(ri);

            return json;
        }

        private void FinishCounties()
        {
            List<int> countyIDList = new List<int>(_counties.Keys.ToList());
            int debugCountyID = -1;
            try
            {
                foreach (int countyID in countyIDList)
                {
                    debugCountyID = countyID;
                    CountyItem ci = _counties[countyID];
                    //ci.Style = _groups[ci.GroupID].Style;
                    _counties[countyID] = ci;


                    CountyJSON tempJSON = new CountyJSON();
                    tempJSON.Abbreviation = ci.Abbreviation;

                    tempJSON.Style = ci.Style;
                    tempJSON.CountyID = countyID;
                    tempJSON.GroupID = ci.GroupID;
                    tempJSON.Name = ci.Name;
                    tempJSON.StateName = ci.StateName;

                    tempJSON.IsSplit = ci.IsSplit;
                    tempJSON.SplitType = ci.SplitType;
                    tempJSON.SplitLine = ci.SplitLine;
                    tempJSON.SplitCountyName = ci.SplitCountyName;

                    _countiesJSON.Add(tempJSON);

                }
            }
            catch (Exception ex)
            {
                if (1 == 1)
                {

                }
            }
        }


        public void FinishGroups()
        {
            List<int> groupIdList = new List<int>(_groups.Keys);
            Dictionary<int, GroupItem> tempGroups = new Dictionary<int, GroupItem>();

            List<KeyValuePair<string, int>> groupIdDict = new List<KeyValuePair<string, int>>();

            foreach (int groupId in groupIdList)
            {
                GroupItem gi = _groups[groupId];
                KeyValuePair<string, int> kvp = new KeyValuePair<string, int>(gi.Name, groupId);
                groupIdDict.Add(kvp);
            }

            foreach (KeyValuePair<string, int> item in groupIdDict)
            {
                string groupName = item.Key;
                int dictGroupID = item.Value;
                GroupItem gi = _groups[dictGroupID];

                tempGroups.Add(dictGroupID, gi);
            }

            _groups = tempGroups;

            groupIdList = new List<int>(_groups.Keys);

            foreach (int groupId in groupIdList)
            {
                GroupItem gi = _groups[groupId];

                if (gi.IsOverlap)
                {
                    // get overlap color style
                    gi.Style = _overlapColorGenerator.GetStyleOverlap();
                }
                else
                {
                    gi.Style = _solidColorGenerator.GetStyle();
                }

                _groups[groupId] = gi;

            }
        }

        /// <summary>
        /// Determines whether two non-sorted lists are equal
        /// </summary>
        /// <typeparam name="T">DataType</typeparam>
        /// <param name="list1">First List</param>
        /// <param name="list2">Second List</param>
        /// <returns>True/False: Lists contain same members or not</returns>
        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// Build master list of groups and overlaps
        /// </summary>
        public void DedupeOverlaps()
        {
            Dictionary<int, OverlapItem> tempOverlaps = new Dictionary<int, OverlapItem>();

            foreach (OverlapItem oi in Overlaps.Values)
            {
                int currentOverlapID = oi.OverlapID;
                bool duplicateFound = false;

                foreach (OverlapItem oiTemp in tempOverlaps.Values)
                {
                    bool areEqual = oi.GroupIDList.Except(oiTemp.GroupIDList).ToList().Count == 0;
                    
                    if (areEqual)
                        areEqual = oiTemp.GroupIDList.Except(oi.GroupIDList).ToList().Count == 0;

                    if (areEqual)
                    {
                        duplicateFound = true;
                        break;
                    }

                }

                if (duplicateFound == false)
                    tempOverlaps.Add(currentOverlapID, oi);

            }

            // Loop each county that exists and get it's overlap id (if exists)
            // Get the groupIDList that corresponds to that overlap id

            // Loop through each overlap in the new list, and compare to the list from above.  If a match is found, update the item above with the new list's overlap id

            var keys = new List<int>(Counties.Keys);
            foreach (int key in keys)
            {
                CountyItem ci = Counties[key];

                if (ci.Overlap)
                {
                    List<int> groupIDList = Overlaps[Convert.ToInt32(ci.OverlapID)].GroupIDList;

                    bool isFound = false;

                    foreach (OverlapItem tempOI in tempOverlaps.Values)
                    {

                        if (ScrambledEquals(groupIDList, tempOI.GroupIDList))
                        {
                            ci.OverlapID = tempOI.OverlapID;
                            Counties[key] = ci;
                            isFound = true;

                            break;
                        }
                    }

                    //if (isFound == false)
                    //{
                    //    OverlapItem oi = new OverlapItem();
                    //    oi.GroupID = ci.GroupID;
                    //    oi.GroupIDList = groupIDList;
                    //    oi.OverlapID = Convert.ToInt32(ci.OverlapID);

                    //    Overlaps.Add(oi.OverlapID, oi);

                    //}
                }

                if (ci.GroupID == -1)
                {

                }

            }


            Overlaps = tempOverlaps;

            List<int> countyKeys = new List<int>(Counties.Keys);
            List<int> overlapKeys = new List<int>(Overlaps.Keys);

            int currentOverlapGroupID = 1000000;

            foreach (int overlapID in overlapKeys)
            {

                OverlapItem oi = Overlaps[overlapID]; 

                foreach (int countyID in countyKeys)
                {
                    CountyItem ci = Counties[countyID];

                    if (ci.Overlap && ci.OverlapID == oi.OverlapID)
                    {
                        int groupID = 0;

                        if (oi.GroupID == -1)
                        {
                            currentOverlapGroupID += 1;
                            groupID = currentOverlapGroupID;
                        }
                        else
                        {
                            groupID = oi.GroupID;
                        }

                        ci.GroupID = groupID;
                        oi.GroupID = groupID;

                        List<string> groupNames = new List<string>();

                        foreach (int tempGroupID in oi.GroupIDList)
                        {
                            groupNames.Add(Groups[tempGroupID].Name);
                        }

                        groupNames.Sort();
                        string groupName = "";

                        foreach (string groupNameItem in groupNames)
                        {
                            if (groupName.Length > 0)
                                groupName += ", ";

                            groupName += groupNameItem;
                        }

                        StyleGenerator.StyleDefinition styleDef = new StyleGenerator.StyleDefinition();

                        // Create the overlap group
                        AddGroup(groupName, groupID, "", "", "", "", -1, styleDef, true, false);

                        Counties[countyID] = ci;
                        Overlaps[overlapID] = oi;

                    }

                }
            }

        }

        /// <summary>
        /// Adds group to group list
        /// </summary>
        /// <param name="groupName">Name for Group</param>
        /// <param name="groupID">Group ID</param>
        /// <param name="color">Color to assign to group</param>
        private void AddGroup(string groupName, int groupID, string address, string city, string state, string stateAbbreviation, int zip, StyleGenerator.StyleDefinition style, bool isOverlap = false, bool addStyle = false)
        {
            if (Groups.ContainsKey(groupID) == false)
            {
                GroupItem gi = new GroupItem();

                if (addStyle)
                    gi.Style = style;

                gi.GroupID = groupID;

                gi.Name = groupName;
                gi.IsOverlap = isOverlap;
                gi.Address = address;
                gi.City = city;
                gi.State = state;
                gi.StateAbbreviation = stateAbbreviation;
                gi.Zip = zip;

                Groups.Add(groupID, gi);
            }
            else
            {
                if (addStyle)
                {
                    GroupItem gi = Groups[groupID];
                    gi.Style = style;
                }
            }
        }

    }
}