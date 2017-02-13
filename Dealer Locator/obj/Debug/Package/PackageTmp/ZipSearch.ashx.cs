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
    public class ZipSearch : IHttpHandler
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
            int zipCode;

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (Int32.TryParse(context.Request.QueryString["term"].ToString(), out zipCode))
            {


                string sql = "SELECT DISTINCT([CITY_ALIAS_NAME]), [STATE] FROM [DL.ZipLookup] WHERE ZIP_CODE = " + zipCode + " GROUP BY [CITY_ALIAS_NAME], [STATE] ORDER BY [CITY_ALIAS_NAME]";
                DataSet ds = new DataSet();

                ds = DA.DataAccess.Read(sql);

                ArrayList values = new ArrayList();



                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    item newitem = new item();
                    newitem.zipcode = zipCode.ToString();
                    newitem.city = dr["CITY_ALIAS_NAME"].ToString();
                    newitem.state = dr["STATE"].ToString();
                    newitem.value = dr["CITY_ALIAS_NAME"].ToString() + ", " + dr["STATE"].ToString();
                    values.Add(newitem);
                }


                context.Response.Write(js.Serialize(values));
            }
            else
            {
                ArrayList values = new ArrayList();
                context.Response.Write(js.Serialize(values));
            }
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