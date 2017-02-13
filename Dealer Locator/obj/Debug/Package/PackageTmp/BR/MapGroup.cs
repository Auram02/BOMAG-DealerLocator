using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dealer_Locator.BR
{
    public class MapGroup
    {

        public struct County
        {
            public int CountyID;
            public string CountyName;
            public string StateName;
            public string StateAbbreviation;
            public string Color;
        }

        private List<string> _name;
        private List<County> _counties;
        private int _groupID;
        private string _color;

        public List<string> Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }

        public List<County> Counties
        {
            get { return _counties; }
            set { _counties = value; }
        }

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public MapGroup(string name, int groupID =-1, string color = "")
        {
            if (!_name.Contains(name))
                _name.Add(name);

            _counties = new List<County>();
            _groupID = groupID;
            _color = color;
        }

    }
}