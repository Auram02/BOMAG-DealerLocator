using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Collections;


namespace Dealer_Locator.BR
{
    public class ExcelReport
    {

        private string XmlFileName;
        private ArrayList AllRows;
        /// <summary>
        /// Instantiate a price report.
        /// </summary>
        public ExcelReport(string myXmlFile, ArrayList myData)
        {

            this.XmlFileName = myXmlFile;
            this.AllRows = myData;

            //allPriceObjects = new ArrayList(5);

            //allPriceObjects.Add(new SamplePriceData
            //    ("Standard", 10, 5, 25, "1.0", System.DateTime.Now, System.DateTime.Now));

            //allPriceObjects.Add(new SamplePriceData
            //    ("Laptop", (decimal)7.5, 5, 50, "1.0", System.DateTime.Now, System.DateTime.Now));

            //allPriceObjects.Add(new SamplePriceData
            //    ("Premium", (decimal)12.5, 10, 999, "1.0", System.DateTime.Now, System.DateTime.Now));

        } // constructor

        /// <summary>
        /// Generates the actual Excel report and saves to disk.
        /// </summary>
        public string GenerateSelf()
        {
            string returnMessage = "";

            if (this.AllRows.Count == 0)
            {
                returnMessage = "There were no leads that met your criteria";

            }
            else
            {

                try
                {
                    FileInfo fi = new FileInfo(XmlFileName);

                    StreamWriter s = fi.CreateText();

                    string crlf = "\r\n";

                    #region Header

                    s.WriteLine(
                        "<?xml version=\"1.0\"?>" + crlf +
                        "<?mso-application progid=\"Excel.Sheet\"?>" + crlf +
                        "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"" + crlf +
                        "xmlns:o=\"urn:schemas-microsoft-com:office:office\"" + crlf +
                        "xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" + crlf +
                        "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"" + crlf +
                        "xmlns:html=\"http://www.w3.org/TR/REC-html40\">" + crlf +
                        "<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">" +
                        "<Author>FindBomag.com Dealer Locator</Author>" + crlf +
                        "<LastAuthor>FindBomag.com Dealer Locator</LastAuthor>" + crlf +
                        //"<Created>2005-11-07T02:19:12Z</Created>
                        "<Company>Bomag.com</Company>" + crlf +
                        "<Title><![CDATA[Leads by Main Category, Subcategory and Model as of " + System.DateTime.Now.ToString() + "]]></Title>" + crlf +
                        "<Subject>Lead Report</Subject>" + crlf +
                        //"<Description>Some comments</Description>" + crlf +
                        "<Version>11.6408</Version>" + crlf +
                        "</DocumentProperties>");

                    s.WriteLine(
                        "<OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">" + crlf +
                        "<DownloadComponents/>" + crlf +
                        //"<LocationOfComponents HRef=\"file:///D:\\\"/>" + crlf +
                        "</OfficeDocumentSettings>" + crlf +
                        "<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">" + crlf +
                        //<WindowHeight>11765</WindowHeight>
                        //<WindowWidth>15446</WindowWidth>
                        //<WindowTopX>217</WindowTopX>
                        //<WindowTopY>95</WindowTopY>
                        //<ProtectStructure>False</ProtectStructure>
                        //<ProtectWindows>False</ProtectWindows>
                        "</ExcelWorkbook>");

                    s.WriteLine(
                        "<Styles>" + crlf +
                        "  <Style ss:ID=\"Default\" ss:Name=\"Normal\">" + crlf +
                        "    <Alignment ss:Vertical=\"Bottom\"/>" + crlf +
                        "    <Borders/>" + crlf +
                        "    <Font/>" + crlf +
                        "    <Interior/>" + crlf +
                        "    <NumberFormat/>" + crlf +
                        "    <Protection/>" + crlf +
                        "  </Style>" + crlf +
                        "  <Style ss:ID=\"BoldAndUnderline\">" + crlf +
                        "    <Font x:Family=\"Swiss\" ss:Bold=\"1\" ss:Underline=\"Single\"/>" + crlf +
                        "  </Style>" + crlf +
                        "  <Style ss:ID=\"s26\">" + crlf +
                        "    <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>" + crlf +
                        "    <NumberFormat ss:Format=\"&quot;$&quot;#,##0.00\"/>" + crlf +
                        "  </Style>" + crlf +
                        "  <Style ss:ID=\"Date\">" + crlf +
                        "    <NumberFormat ss:Format=\"[$-409]m/d/yy\\ h:mm\\ AM/PM;@\"/>" + crlf +
                        "  </Style>" + crlf +
                        "</Styles>" + crlf);

                    s.WriteLine(
                        "<Worksheet ss:Name=\"Lead Report\">" + crlf +
                        "<Table " +
                        //ss:ExpandedColumnCount=\"3\" " +
                        //"ss:ExpandedRowCount=\"1\" " +
                        "x:FullColumns=\"1\"" + crlf +
                        "x:FullRows=\"1\" ss:DefaultColumnWidth=\"48.905660377358487\"" + crlf +
                        "ss:DefaultRowHeight=\"12.90566037735849\">" + crlf);

                    int counter = 0;

                    ArrayList tempArr = ((ArrayList)this.AllRows[0]);

                    foreach (string DataItem in tempArr)
                    {
                        counter += 1;
                        s.WriteLine("<Column ss:Index=\"" + counter + "\" ss:Width=\"70\"/>");     // Title

                    }

                    // Column headers. 
                    //s.WriteLine("<Row ss:StyleID=\"BoldAndUnderline\">");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Title</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Rate</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Min Charge</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Max Charge</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Data Version</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Created</Data></Cell>");
                    //s.WriteLine("<Cell><Data ss:Type=\"String\">Modified</Data></Cell>");
                    //s.WriteLine("</Row>");

                    #endregion




                    // Loop
                    foreach (ArrayList RowData in this.AllRows)
                    {            // header
                        //ss:StyleID=\"BoldAndUnderline\"
                        s.WriteLine("<Row >");


                        foreach (string DataItem in RowData)
                        {
                            s.WriteLine("<Cell><Data ss:Type=\"String\">");
                            s.WriteLine(DataItem);
                            s.WriteLine("</Data></Cell>");
                        }
                        #region Deprecated


                        //s.WriteLine("<Cell><Data ss:Type=\"Number\">" + aPrice.myRate + "</Data></Cell>");

                        //s.WriteLine("<Cell><Data ss:Type=\"Number\">" + aPrice.myMinCharge + "</Data></Cell>");

                        //s.WriteLine("<Cell><Data ss:Type=\"Number\">" + aPrice.myMaxCharge + "</Data></Cell>");

                        //// Note that I've noticed that the carriage return here is necessary or my version of
                        //// Excel balks at processing it.
                        //s.WriteLine("<Cell><Data ss:Type=\"String\">");
                        //s.WriteLine(aPrice.myDataVersion);
                        //s.WriteLine("</Data></Cell>");

                        //System.DateTime ds = new System.DateTime();

                        //ds = aPrice.myDateCreated;

                        //s.Write("<Cell><Data ss:Type=\"DateTime\">");
                        //s.Write(ds.Year.ToString("0000") + "-" + ds.Month.ToString("00") + "-" + ds.Day.ToString("00") +
                        //    "T" + ds.Hour.ToString("00") + ":" + ds.Minute.ToString("00") + ":" + ds.Second.ToString("00") + "." + ds.Millisecond.ToString("000"));
                        //s.WriteLine("</Data></Cell>");

                        //ds = aPrice.myDateModified;

                        //s.Write("<Cell><Data ss:Type=\"DateTime\">");
                        //s.Write(ds.Year.ToString("0000") + "-" + ds.Month.ToString("00") + "-" + ds.Day.ToString("00") +
                        //    "T" + ds.Hour.ToString("00") + ":" + ds.Minute.ToString("00") + ":" + ds.Second.ToString("00") + "." + ds.Millisecond.ToString("000"));
                        //s.WriteLine("</Data></Cell>");

                        #endregion

                        s.WriteLine("</Row>" + crlf);
                    }


                    #region Footer

                    s.WriteLine("</Table>" + crlf);

                    s.WriteLine(
                        "<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">" + crlf +
                        "<Selected/>" + crlf +
                        "<Panes>" + crlf +
                        "<Pane>" + crlf +
                        "<Number>3</Number>" + crlf +
                        "<ActiveRow>1</ActiveRow>" + crlf +
                        "</Pane>" + crlf +
                        "</Panes>" + crlf +
                        "<ProtectObjects>False</ProtectObjects>" + crlf +
                        "<ProtectScenarios>False</ProtectScenarios>" + crlf +
                        "</WorksheetOptions>" + crlf +
                        "</Worksheet>");

                    s.WriteLine("</Workbook>" + crlf);

                    #endregion

                    s.Close();
                }
                catch (Exception ex)
                {
                    returnMessage = "An error has occurred: " + ex.Message + Environment.NewLine + Environment.NewLine + "Please note the Categories and Models you were working with and contact the developer.";
                }
            }
            return returnMessage;

        } // GenerateReport
    }
}
