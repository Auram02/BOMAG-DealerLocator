using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

using NUnit.Framework;

namespace Dealer_Locator.Tests
{
    [TestFixture]
    public class MapReportTest
    {

        [SetUp]
        public void Init()
        {

        }
        
        [Test, Category("Tests")]
        public void TestMapReport()
        {
            List<string> stateList = new List<string>();
            stateList.Add("Illinois");
            stateList.Add("Missouri");

            BR.MapReport mapReport = new BR.MapReport();

            System.Data.DataTable dt = mapReport.GenerateGroupData(7, stateList);
            System.Data.DataTable dtBogus = new System.Data.DataTable();
            string jsonData = mapReport.BuildReportData(dt, dtBogus, "Light Tandems", stateList);


        }

        
        [Test, Category("Tests")]
        public void TestOverviewMapReport()
        {
            List<string> stateList = new List<string>();
            
            
            System.Data.DataSet ds = Dealer_Locator.admin.Reports.ReportGeneration.Reports.GenerateOverview(7, ref stateList);

            Dealer_Locator.BR.MapReport mr = new BR.MapReport();
            string json = mr.BuildReportData(ds.Tables[0], ds.Tables[1], "Test", stateList);
            Assert.Greater(ds.Tables[0].Rows.Count, 0);

            bool isGroupNeg1 = false;

            List<int> negOneGroupList = new List<int>();
            List<int> countyIDList = new List<int>(mr.Counties.Keys.ToList());

            foreach (int countyID in countyIDList)
            {
                Dealer_Locator.BR.MapReport.CountyItem ci = mr.Counties[countyID];
                if (ci.GroupID == -1)
                {
                    negOneGroupList.Add(countyID);
                    isGroupNeg1 = true;
                }
            }

            foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["GroupID"].ToString() == "-1")
                    isGroupNeg1 = true;
            }

            if (isGroupNeg1 == true)
            {
                foreach (int countyID in negOneGroupList)
                {
                    Dealer_Locator.BR.MapReport.CountyItem ci = mr.Counties[countyID];
                    string output = ci.Name + " (" + ci.GroupID + "): OverlapID: " + ci.OverlapID + " - IsOverlap: " + ci.Overlap;
                    Console.WriteLine(countyID);
                }

                

                Assert.IsFalse(isGroupNeg1);
            }
            Assert.Greater(stateList.Count, 0);
            Assert.Greater(json.Length, 0);

            

        }
        


        [Test, Category("Tests")]
        public void TestMapReportService()
        {
            List<string> stateList = new List<string>();
            stateList.Add("Illinois");
            stateList.Add("Missouri");

            BR.MapReport mapReport = new BR.MapReport();

            System.Data.DataTable dt = mapReport.GenerateGroupData(7, stateList);
            System.Data.DataTable dtBogus = new System.Data.DataTable();

            System.Data.DataSet ds = new System.Data.DataSet();

            Dealer_Locator.admin.WebServices.MapReportService mrsvc = new admin.WebServices.MapReportService();
            mrsvc.UploadReportData(ds, "Light Tandems", stateList);

        }
        [Test, Category("Tests")]
        public void TestMapReportColors()
        {
            Bitmap Bmp = new Bitmap(500, 500);

            using (Graphics gfx = Graphics.FromImage(Bmp))
            {
                BR.StyleGenerator cg = new BR.StyleGenerator();

                List<string> colors = new List<string>();
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        BR.StyleGenerator.StyleDefinition mapColor = cg.GetStyle();
                        colors.Add(mapColor.ToString());

                        using (SolidBrush brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(mapColor.FillColor)))
                        {
                            gfx.FillRectangle(brush, 10 * i, 10 * j, 10, 10);

                        }
                    }
                }
                Bmp.Save("ColorTest.jpg");
            }           
        }
    }
}