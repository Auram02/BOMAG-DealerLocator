using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Dealer_Locator
{
    /// <summary>
    /// Summary description for ZipSearch
    /// </summary>
    public class CityStateSearch : IHttpHandler
    {

        public struct item
        {
            public string value;
            public string zipcode;
            public string state;
            public string city;
        }

        public void ProcessRequest(HttpContext context)
        {
            string data = context.Request.QueryString["term"];

            string city = data.Substring(0, data.IndexOf(";"));
            string state = data.Substring(data.IndexOf(";") + 1);

            string sql = "SELECT DISTINCT([CITY_ALIAS_NAME]), [STATE], [ZIP_CODE] FROM [DL.ZipLookup] WHERE [CITY_ALIAS_NAME] LIKE '%" + city + "%' AND [STATE] = '" + state + "' ORDER BY [CITY_ALIAS_NAME], [ZIP_CODE]";

            DataSet ds = new DataSet();

            ds = DA.DataAccess.Read(sql);

            ArrayList values = new ArrayList();



            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                item newitem = new item();
                newitem.zipcode = dr["ZIP_CODE"].ToString();
                newitem.city = dr["CITY_ALIAS_NAME"].ToString();
                newitem.state = dr["STATE"].ToString();
                newitem.value = dr["CITY_ALIAS_NAME"].ToString() + ", " + dr["STATE"].ToString() + " (" + dr["ZIP_CODE"].ToString() + ")";
                values.Add(newitem);
            }

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

            context.Response.Write(js.Serialize(values));


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}