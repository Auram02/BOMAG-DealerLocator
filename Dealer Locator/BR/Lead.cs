using System;
using System.Data;
using System.Configuration;
using System.Collections;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

using SagaraSoftware.ZipCodeUtil;

using System.Collections.Generic;
using System.Xml;

using System.Text.RegularExpressions;

namespace Dealer_Locator.BR
{
    [Serializable]
    public class Lead : System.Web.UI.Page
    {

        /// <summary>
        /// Lead id after submitted to database.
        /// </summary>
        int _leadID = -1;
        public bool InKioskMode = false;

        DA.LeadsTDS.DL_LeadDataTable leadDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
        DA.LeadsTDS.DL_LeadEmailDataTable leadEmailDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();
        DA.LeadsTDS.DL_LeadEmailTypeDataTable leadEmailTypeDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailTypeDataTable();
        DA.LeadsTDS.DL_LeadProductDataTable leadProductDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadProductDataTable();
        DA.LeadsTDS.DL_LeadValuesDataTable leadValueDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadValuesDataTable();

        DA.LeadsTDSTableAdapters.DL_LeadTableAdapter leadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
        DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leadEmailTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();
        DA.LeadsTDSTableAdapters.DL_LeadValuesTableAdapter leadValueTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadValuesTableAdapter();
        DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter leadProductTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter();

        Hashtable _excludeFieldList = new Hashtable();


        public List<LeadValue> _leadValues = new List<LeadValue>();
        ArrayList _leadEmails = new ArrayList();

        public List<LeadProduct> _leadProducts = new List<LeadProduct>();
        LeadStruct _lead = new LeadStruct();

        public static string PendingLeadErrorMessage = "";

        public static bool _AllowEmailsToSend = true;

        private string _additionalEmailRecipient = "nwilson@nwilson.org";
        public bool isRentalCompany = false;

        #region Lead Specific

        public Lead()
        {

        }

        public Lead(bool _InKioskMode)
        {
            InKioskMode = _InKioskMode;

        }

        public Lead(System.Data.SqlClient.SqlConnection conn, int leadID)
        {


            leadTA.Connection = conn;
            leadValueTA.Connection = conn;
            leadEmailTA.Connection = conn;
            leadProductTA.Connection = conn;

            LoadLeadByID(leadID);

        }



        public Lead(int leadID)
        {
            LoadLeadByID(leadID);
        }

        public int LeadID
        {
            get { return _leadID; }
        }

        public static bool RemoveLead(int leadID)
        {
            bool IsSuccessful = false;



            try
            {
                DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();
                DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter lpta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter();
                DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
                DA.LeadsTDSTableAdapters.DL_LeadValuesTableAdapter lvta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadValuesTableAdapter();

                if (leta.Connection.ConnectionString == "")
                {
                    System.Data.SqlClient.SqlConnection conn = DA.DataAccess.GetDatabaseConnection();

                    leta.Connection = conn;
                    lpta.Connection = conn;
                    lta.Connection = conn;
                    lvta.Connection = conn;

                }

                leta.DeleteLeadData(leadID);
                lpta.DeleteLeadData(leadID);
                lta.DeleteLeadData(leadID);
                lvta.DeleteLeadData(leadID);

                IsSuccessful = true;
            }
            catch (Exception ex)
            {

                IsSuccessful = false;
            }

            return IsSuccessful;


        }

        public static DA.LeadsTDS.DL_LeadDataTable GetLeadsByLastName(string LastName)
        {
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();

            ldt = lta.GetDataByLastName(LastName);

            return ldt;

        }


        /// <summary>
        /// Given a LeadID, load the data into this class object
        /// </summary>
        /// <param name="leadID">The leadID to load</param>
        private void LoadLeadByID(int leadID)
        {
            _leadID = leadID;

            //leadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            try
            {

                leadDT = leadTA.GetDataByLeadID(leadID);
            }
            catch (Exception ex)
            {
                leadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
                leadDT = leadTA.GetDataByLeadID(leadID);
            }

            DA.LeadsTDS.DL_LeadRow leadRow = ((DA.LeadsTDS.DL_LeadRow)leadDT.Rows[0]);

            _lead.errorOccurred = leadRow.errorOccurred;
            _lead.errorValue = leadRow.errorValue;
            _lead.pk_leadID = leadRow.pk_leadID;
            _lead.sendDate = leadRow.sendDate;
            _lead.submitDate = leadRow.submitDate;
            _lead.submitted = leadRow.submitted;




            // get lead data here
            leadValueDT = leadValueTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadValuesRow dr in leadValueDT.Rows)
            {
                LeadValue tempVal = new LeadValue();


                tempVal.elementName = dr.elementName;
                tempVal.elementValue = dr.elementValue;

                tempVal.fk_leadID = dr.fk_leadID;
                tempVal.pk_valueID = dr.pk_valueiD;

                _leadValues.Add(tempVal);

            }


            leadEmailDT = leadEmailTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadEmailRow dr in leadEmailDT.Rows)
            {
                LeadEmail tempEmail = new LeadEmail();
                tempEmail.emailBCC = dr.emailBCC;
                tempEmail.emailBody = dr.emailBody;
                tempEmail.emailCC = dr.emailCC;
                tempEmail.emailFrom = dr.emailFrom;
                tempEmail.emailSubject = dr.emailSubject;
                tempEmail.emailTo = dr.emailTo;
                tempEmail.fk_emailType = dr.fk_emailType;
                tempEmail.fk_leadID = dr.fk_leadID;
                tempEmail.pk_leadEmailID = dr.pk_leadEmailID;
                tempEmail.emailSent = dr.emailSent;

                DateTime sentDate = DateTime.Now;

                if (!dr.IssentDateNull())
                    sentDate = dr.sentDate;

                tempEmail.sentDate = GetSentDate(sentDate);

                _leadEmails.Add(tempEmail);
            }


            leadProductDT = leadProductTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadProductRow dr in leadProductDT.Rows)
            {
                LeadProduct tempProd = new LeadProduct();

                tempProd.distributorID = dr.distributorID;
                tempProd.fk_leadID = dr.fk_leadID;
                tempProd.fk_MainCatID = dr.fk_MainCatID;
                tempProd.fk_ModelID = dr.fk_ModelID;
                tempProd.fk_SubCatID = dr.fk_SubCatID;
                tempProd.IsElectronic = dr.IsElectronic;
                tempProd.IsMail = dr.IsMail;
                tempProd.pk_leadProductID = dr.pk_leadProductID;
                tempProd.territoryManagerID = dr.territoryManagerID;
                tempProd.manufacturerDistributorID = dr.manufacturerDistributorID;

                _leadProducts.Add(tempProd);
            }

            // all data should now be loaded
        }


        /// <summary>
        /// Creates the XML Data Set given a list of leads
        /// </summary>
        /// <param name="LeadIDList">List of lead id's to load and save to an XML file</param>
        /// <returns></returns>
        public static XmlDocument CreateNewLeadXmlDataSet(List<int> LeadIDList)
        {
            BR.Lead standardLead = new Dealer_Locator.BR.Lead(true);

            XmlDocument xDoc = new XmlDocument();

            XmlNode rootNode = xDoc.CreateNode(XmlNodeType.Element, "UnsentLeadsExport", null);

            xDoc.AppendChild(rootNode);


            XmlNode root = xDoc.FirstChild;



            foreach (int leadID in LeadIDList)
            {
                try
                {
                    BR.Lead tempLead = new Dealer_Locator.BR.Lead(leadID);

                    XmlNode tempXmlLeadNode = tempLead.LoadLeadToXML(leadID);


                    xDoc.DocumentElement.AppendChild(xDoc.ImportNode((XmlNode)tempXmlLeadNode, true));
                    //xDoc.InsertAfter(xDoc.ImportNode((XmlNode)tempXmlLeadNode, true), root);


                    // switch the tempLead to "submitted"
                    bool HasBeenUpdated = standardLead.UpdateLeadStatus(true, leadID);



                }
                catch (Exception ex)
                {

                }

            }

            return xDoc;

        }


        /// <summary>
        /// Reads a lead from XML into the lead structs
        /// </summary>
        /// <param name="LeadNode">The node to start from</param>
        public void ReadLeadFromXML(XmlNode LeadNode)
        {

            XmlNode LeadDataNode = LeadNode.SelectSingleNode("Data");
            this._lead.errorOccurred = Convert.ToBoolean(LeadDataNode.SelectSingleNode("errorOccurred").InnerText);
            this._lead.errorValue = LeadDataNode.SelectSingleNode("errorValue").InnerText;
            this._lead.pk_leadID = Convert.ToInt32(LeadDataNode.SelectSingleNode("pk_leadID").InnerText);
            this._lead.sendDate = Convert.ToDateTime(LeadDataNode.SelectSingleNode("sendDate").InnerText);
            this._lead.submitDate = Convert.ToDateTime(LeadDataNode.SelectSingleNode("submitDate").InnerText);
            this._lead.submitted = Convert.ToBoolean(LeadDataNode.SelectSingleNode("submitted").InnerText);

            XmlNodeList LeadValuesList = LeadNode.SelectNodes("LeadValue");

            foreach (XmlNode LeadValueNode in LeadValuesList)
            {

                LeadValue tempValue = new LeadValue();
                tempValue.elementName = LeadValueNode.SelectSingleNode("elementName").InnerText;
                tempValue.elementValue = LeadValueNode.SelectSingleNode("elementValue").InnerText;
                tempValue.fk_leadID = Convert.ToInt32(LeadValueNode.SelectSingleNode("fk_leadID").InnerText);
                tempValue.pk_valueID = Convert.ToInt32(LeadValueNode.SelectSingleNode("pk_valueID").InnerText);

                _leadValues.Add(tempValue);
            }

            XmlNodeList LeadEmailsList = LeadNode.SelectNodes("LeadEmail");

            foreach (XmlNode LeadEmailNode in LeadEmailsList)
            {

                LeadEmail tempEmail = new LeadEmail();
                tempEmail.emailBCC = LeadEmailNode.SelectSingleNode("emailBCC").InnerText;
                tempEmail.emailBody = LeadEmailNode.SelectSingleNode("emailBody").InnerText;
                tempEmail.emailCC = LeadEmailNode.SelectSingleNode("emailCC").InnerText;
                tempEmail.emailFrom = LeadEmailNode.SelectSingleNode("emailFrom").InnerText;
                tempEmail.emailSubject = LeadEmailNode.SelectSingleNode("emailSubject").InnerText;
                tempEmail.emailTo = LeadEmailNode.SelectSingleNode("emailTo").InnerText;
                tempEmail.fk_emailType = Convert.ToInt32(LeadEmailNode.SelectSingleNode("fk_emailType").InnerText);
                tempEmail.fk_leadID = Convert.ToInt32(LeadEmailNode.SelectSingleNode("fk_leadID").InnerText);
                tempEmail.pk_leadEmailID = Convert.ToInt32(LeadEmailNode.SelectSingleNode("pk_leadEmailID").InnerText);
                tempEmail.emailSent = false;

                _leadEmails.Add(tempEmail);
            }


            XmlNodeList LeadProductsList = LeadNode.SelectNodes("LeadProduct");

            foreach (XmlNode LeadProductNode in LeadProductsList)
            {
                LeadProduct tempProduct = new LeadProduct();

                tempProduct.distributorID = Convert.ToInt32(LeadProductNode.SelectSingleNode("distributorID").InnerText);
                tempProduct.fk_leadID = Convert.ToInt32(LeadProductNode.SelectSingleNode("fk_leadID").InnerText);
                tempProduct.fk_MainCatID = Convert.ToInt32(LeadProductNode.SelectSingleNode("fk_MainCatID").InnerText);
                tempProduct.fk_ModelID = Convert.ToInt32(LeadProductNode.SelectSingleNode("fk_ModelID").InnerText);
                tempProduct.fk_SubCatID = Convert.ToInt32(LeadProductNode.SelectSingleNode("fk_SubCatID").InnerText);
                tempProduct.IsElectronic = Convert.ToBoolean(LeadProductNode.SelectSingleNode("IsElectronic").InnerText);
                tempProduct.IsMail = Convert.ToBoolean(LeadProductNode.SelectSingleNode("IsMail").InnerText);
                tempProduct.pk_leadProductID = Convert.ToInt32(LeadProductNode.SelectSingleNode("pk_leadProductID").InnerText);
                tempProduct.territoryManagerID = Convert.ToInt32(LeadProductNode.SelectSingleNode("territoryManagerID").InnerText);
                tempProduct.manufacturerDistributorID = Convert.ToInt32(LeadProductNode.SelectSingleNode("manufacturerDistributorID").InnerText);

                _leadProducts.Add(tempProduct);
            }

        }

        /// <summary>
        /// Converts a lead into an XML form
        /// </summary>
        /// <param name="leadID">The lead to take to XML</param>
        /// <returns></returns>
        public XmlNode LoadLeadToXML(int leadID)
        {
            XmlDocument xDoc = new XmlDocument();

            XmlNode LeadXmlNode = xDoc.CreateNode(XmlNodeType.Element, "Lead", null);


            _leadID = leadID;

            //leadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            try
            {


                leadDT = leadTA.GetDataByLeadID(leadID);
            }
            catch (Exception ex)
            {
                leadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
                leadDT = leadTA.GetDataByLeadID(leadID);
            }

            DA.LeadsTDS.DL_LeadRow leadRow = ((DA.LeadsTDS.DL_LeadRow)leadDT.Rows[0]);

            XmlNode LeadData = xDoc.CreateNode(XmlNodeType.Element, "Data", null);

            XmlNode errorOccurred = xDoc.CreateNode(XmlNodeType.Element, "errorOccurred", null);
            errorOccurred.InnerText = leadRow.errorOccurred.ToString(); ;

            XmlNode errorValue = xDoc.CreateNode(XmlNodeType.Element, "errorValue", null);
            errorValue.InnerText = leadRow.errorValue;

            XmlNode pk_leadID = xDoc.CreateNode(XmlNodeType.Element, "pk_leadID", null);
            pk_leadID.InnerText = leadRow.pk_leadID.ToString(); ;

            XmlNode sendDate = xDoc.CreateNode(XmlNodeType.Element, "sendDate", null);
            sendDate.InnerText = leadRow.sendDate.ToString(); ;

            XmlNode submitDate = xDoc.CreateNode(XmlNodeType.Element, "submitDate", null);
            submitDate.InnerText = leadRow.submitDate.ToString(); ;

            XmlNode submitted = xDoc.CreateNode(XmlNodeType.Element, "submitted", null);
            submitted.InnerText = leadRow.submitted.ToString(); ;


            LeadData.AppendChild(errorOccurred);
            LeadData.AppendChild(errorValue);
            LeadData.AppendChild(pk_leadID);
            LeadData.AppendChild(sendDate);
            LeadData.AppendChild(submitDate);
            LeadData.AppendChild(submitted);

            // append data node
            LeadXmlNode.AppendChild(LeadData);


            // get lead data here
            leadValueDT = leadValueTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadValuesRow dr in leadValueDT.Rows)
            {
                XmlNode LeadValue = xDoc.CreateNode(XmlNodeType.Element, "LeadValue", null);


                XmlNode elementName = xDoc.CreateNode(XmlNodeType.Element, "elementName", null);
                elementName.InnerText = dr.elementName;


                XmlNode elementValue = xDoc.CreateNode(XmlNodeType.Element, "elementValue", null);
                elementValue.InnerText = dr.elementValue;


                XmlNode fk_leadID = xDoc.CreateNode(XmlNodeType.Element, "fk_leadID", null);
                fk_leadID.InnerText = dr.fk_leadID.ToString(); ;


                XmlNode pk_valueiD = xDoc.CreateNode(XmlNodeType.Element, "pk_valueID", null);
                pk_valueiD.InnerText = dr.pk_valueiD.ToString(); ;


                LeadValue.AppendChild(elementName);
                LeadValue.AppendChild(elementValue);
                LeadValue.AppendChild(fk_leadID);
                LeadValue.AppendChild(pk_valueiD);


                LeadXmlNode.AppendChild(LeadValue);

            }


            leadEmailDT = leadEmailTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadEmailRow dr in leadEmailDT.Rows)
            {

                XmlNode LeadEmailNode = xDoc.CreateNode(XmlNodeType.Element, "LeadEmail", null);


                XmlNode emailBCC = xDoc.CreateNode(XmlNodeType.Element, "emailBCC", null);
                emailBCC.InnerText = dr.emailBCC;

                XmlNode emailBody = xDoc.CreateNode(XmlNodeType.Element, "emailBody", null);
                emailBody.InnerText = dr.emailBody;

                XmlNode emailCC = xDoc.CreateNode(XmlNodeType.Element, "emailCC", null);
                emailCC.InnerText = dr.emailCC;

                XmlNode emailFrom = xDoc.CreateNode(XmlNodeType.Element, "emailFrom", null);
                emailFrom.InnerText = dr.emailFrom;

                XmlNode emailSubject = xDoc.CreateNode(XmlNodeType.Element, "emailSubject", null);
                emailSubject.InnerText = dr.emailSubject;

                XmlNode emailTo = xDoc.CreateNode(XmlNodeType.Element, "emailTo", null);
                emailTo.InnerText = dr.emailTo;

                XmlNode fk_emailType = xDoc.CreateNode(XmlNodeType.Element, "fk_emailType", null);
                fk_emailType.InnerText = dr.fk_emailType.ToString(); ;

                XmlNode fk_leadID = xDoc.CreateNode(XmlNodeType.Element, "fk_leadID", null);
                fk_leadID.InnerText = dr.fk_leadID.ToString(); ;

                XmlNode pk_leadEmailID = xDoc.CreateNode(XmlNodeType.Element, "pk_leadEmailID", null);
                pk_leadEmailID.InnerText = dr.pk_leadEmailID.ToString(); ;

                LeadEmailNode.AppendChild(emailBCC);
                LeadEmailNode.AppendChild(emailBody);
                LeadEmailNode.AppendChild(emailCC);
                LeadEmailNode.AppendChild(emailFrom);
                LeadEmailNode.AppendChild(emailSubject);
                LeadEmailNode.AppendChild(emailTo);
                LeadEmailNode.AppendChild(fk_emailType);
                LeadEmailNode.AppendChild(fk_leadID);
                LeadEmailNode.AppendChild(pk_leadEmailID);

                LeadXmlNode.AppendChild(LeadEmailNode);
            }


            leadProductDT = leadProductTA.GetDataByLeadID(leadID);
            foreach (DA.LeadsTDS.DL_LeadProductRow dr in leadProductDT.Rows)
            {

                XmlNode LeadProductNode = xDoc.CreateNode(XmlNodeType.Element, "LeadProduct", null);


                XmlNode distributorID = xDoc.CreateNode(XmlNodeType.Element, "distributorID", null);
                distributorID.InnerText = dr.distributorID.ToString(); ;

                XmlNode fk_leadID = xDoc.CreateNode(XmlNodeType.Element, "fk_leadID", null);
                fk_leadID.InnerText = dr.fk_leadID.ToString(); ;

                XmlNode fk_MainCatID = xDoc.CreateNode(XmlNodeType.Element, "fk_MainCatID", null);
                fk_MainCatID.InnerText = dr.fk_MainCatID.ToString(); ;

                XmlNode fk_ModelID = xDoc.CreateNode(XmlNodeType.Element, "fk_ModelID", null);
                fk_ModelID.InnerText = dr.fk_ModelID.ToString(); ;

                XmlNode fk_SubCatID = xDoc.CreateNode(XmlNodeType.Element, "fk_SubCatID", null);
                fk_SubCatID.InnerText = dr.fk_SubCatID.ToString(); ;

                XmlNode IsElectronic = xDoc.CreateNode(XmlNodeType.Element, "IsElectronic", null);
                IsElectronic.InnerText = dr.IsElectronic.ToString(); ;

                XmlNode IsMail = xDoc.CreateNode(XmlNodeType.Element, "IsMail", null);
                IsMail.InnerText = dr.IsMail.ToString(); ;

                XmlNode pk_leadProductID = xDoc.CreateNode(XmlNodeType.Element, "pk_leadProductID", null);
                pk_leadProductID.InnerText = dr.pk_leadProductID.ToString(); ;

                XmlNode territoryManagerID = xDoc.CreateNode(XmlNodeType.Element, "territoryManagerID", null);
                territoryManagerID.InnerText = dr.territoryManagerID.ToString(); ;

                XmlNode manufacturerdistributorID = xDoc.CreateNode(XmlNodeType.Element, "manufacturerdistributorID", null);
                manufacturerdistributorID.InnerText = dr.manufacturerDistributorID.ToString(); ;

                LeadProductNode.AppendChild(distributorID);
                LeadProductNode.AppendChild(fk_leadID);
                LeadProductNode.AppendChild(fk_MainCatID);
                LeadProductNode.AppendChild(fk_ModelID);
                LeadProductNode.AppendChild(fk_SubCatID);
                LeadProductNode.AppendChild(IsElectronic);
                LeadProductNode.AppendChild(IsMail);
                LeadProductNode.AppendChild(pk_leadProductID);
                LeadProductNode.AppendChild(territoryManagerID);
                LeadProductNode.AppendChild(manufacturerdistributorID);


                LeadXmlNode.AppendChild(LeadProductNode);

            }




            return LeadXmlNode;

        }

        public void CommitLeadToDatabase()
        {

            DoCommit("");

        }



        public void CommitLeadToDatabase(string DestinationDatabaseConnectionString)
        {

            DoCommit(DestinationDatabaseConnectionString);

        }

        private void DoCommit(string DestinationDatabaseConnectionString)
        {

            if (DestinationDatabaseConnectionString != "")
            {
                leadTA.Connection.ConnectionString = DestinationDatabaseConnectionString;
                leadValueTA.Connection.ConnectionString = DestinationDatabaseConnectionString;
                leadEmailTA.Connection.ConnectionString = DestinationDatabaseConnectionString;
                leadProductTA.Connection.ConnectionString = DestinationDatabaseConnectionString;

                _leadID = DA.DataAccess.GetNextID("[DL.Lead]", "pk_leadID", DestinationDatabaseConnectionString);

            }
            else
            {
                _leadID = DA.DataAccess.GetNextID("[DL.Lead]", "pk_leadID");

            }

            try
            {

                bool submitted = _lead.submitted;

                // if we are in kiosk mode, do not submit the lead
                if (InKioskMode == true)
                    submitted = false;

                leadTA.InsertQuery(_leadID, _lead.submitDate, submitted, _lead.errorOccurred, _lead.errorValue, _lead.sendDate);


                foreach (LeadValue dr in _leadValues)
                {
                    int nextID = -1;

                    if (DestinationDatabaseConnectionString != "")
                        nextID = DA.DataAccess.GetNextID("[DL.LeadValues]", "pk_valueID", DestinationDatabaseConnectionString);
                    else
                        nextID = DA.DataAccess.GetNextID("[DL.LeadValues]", "pk_valueID");

                    leadValueTA.Insert(nextID, _leadID, dr.elementName, dr.elementValue);
                }


                foreach (LeadEmail dr in _leadEmails)
                {
                    int nextID = -1;


                    if (DestinationDatabaseConnectionString != "")
                        nextID = DA.DataAccess.GetNextID("[DL.LeadEmail]", "pk_leadEmailID", DestinationDatabaseConnectionString);
                    else
                        nextID = DA.DataAccess.GetNextID("[DL.LeadEmail]", "pk_leadEmailID");

                    DateTime sentDate = GetSentDate(dr.sentDate);
                    bool emailSent = dr.emailSent;

                    // 2 == Distribution Vendor
                    if (dr.fk_emailType == 2)
                        emailSent = true;

                    leadEmailTA.InsertQuery(nextID, _leadID, dr.fk_emailType, dr.emailBody, dr.emailTo, dr.emailFrom, dr.emailCC, dr.emailBCC, dr.emailSubject, emailSent, sentDate);
                }



                foreach (LeadProduct dr in _leadProducts)
                {
                    int nextID = -1;

                    if (DestinationDatabaseConnectionString != "")
                        nextID = DA.DataAccess.GetNextID("[DL.LeadProduct]", "pk_leadProductID", DestinationDatabaseConnectionString);
                    else
                        nextID = DA.DataAccess.GetNextID("[DL.LeadProduct]", "pk_leadProductID");

                    leadProductTA.InsertQuery(nextID, _leadID, dr.fk_MainCatID, dr.fk_SubCatID, dr.fk_ModelID, dr.IsMail, dr.IsElectronic, dr.distributorID, dr.territoryManagerID, dr.manufacturerDistributorID);
                }

            }
            catch (Exception ex)
            {
                // remove any lead data that has been submitted 
                leadTA.DeleteLeadData(_leadID);
                leadProductTA.DeleteLeadData(_leadID);
                leadEmailTA.DeleteLeadData(_leadID);
                leadValueTA.DeleteLeadData(_leadID);

            }
        }

        public struct LeadStruct
        {
            public int pk_leadID;
            public DateTime submitDate;
            public bool submitted;
            public bool errorOccurred;
            public string errorValue;
            public DateTime sendDate;
        }


        public struct LeadValue
        {

            public int pk_valueID;
            public int fk_leadID;
            public string elementName;
            public string elementValue;

        }


        public struct LeadEmail
        {
            public string emailBody;
            public int fk_emailType;
            public int fk_leadID;
            public int pk_leadEmailID;
            public string emailTo;
            public string emailFrom;
            public string emailCC;
            public string emailBCC;
            public string emailSubject;
            public bool emailSent;
            public DateTime sentDate;
        }

        public struct LeadProduct
        {
            public int pk_leadProductID;
            public int fk_leadID;
            public int fk_MainCatID;
            public int fk_SubCatID;
            public int fk_ModelID;
            public bool IsMail;
            public bool IsElectronic;
            public bool AllowTerritoryOverlap;
            public int distributorID;
            public int manufacturerDistributorID;
            public int territoryManagerID;
        }

        public void AddLead(DateTime submitDate, bool submitted, bool errorOccurred, string errorValue, DateTime sendDate)
        {
            _lead.pk_leadID = _leadID;
            _lead.submitDate = submitDate;
            _lead.submitted = submitted;
            _lead.errorOccurred = errorOccurred;
            _lead.errorValue = errorValue;
            _lead.sendDate = sendDate;
        }


        public void AddLeadValue(string elementName, string elementValue)
        {

            LeadValue dr = new LeadValue();

            dr.elementName = elementName;
            dr.elementValue = elementValue;
            dr.fk_leadID = -1;
            dr.pk_valueID = -1;

            _leadValues.Add(dr);

        }


        public void AddLeadEmail(string emailType, string emailBody, string emailTo, string emailFrom, string emailCC, string emailBCC, string emailSubject, DateTime sentDate, bool vendorEmailSent)
        {

            DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter emailTypeTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();

            LeadEmail dr = new LeadEmail();

            string sql = "SELECT MAX( pk_leadEmailType ) FROM [DL.LeadEmailType] WHERE EmailType = '" + emailType + "'";
            DataSet ds = new DataSet();

            ds = DA.DataAccess.Read(sql);

            dr.emailBody = emailBody;
            dr.fk_emailType = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            dr.fk_leadID = -1;
            dr.pk_leadEmailID = -1;
            dr.emailTo = emailTo;
            dr.emailFrom = emailFrom;
            dr.emailCC = emailCC;
            dr.emailBCC = emailBCC;
            dr.emailSubject = emailSubject;

            dr.sentDate = GetSentDate(sentDate);

            if (dr.fk_emailType == 2)
                dr.emailSent = vendorEmailSent;

            bool addEmail = true;

            int index = 0;
            foreach (LeadEmail le in _leadEmails)
            {
                if (le.emailBody == dr.emailBody)
                {
                    addEmail = false;
                    break;
                }

                index++;

            }

            if (addEmail == true)
                _leadEmails.Add(dr);
        }




        public void AddLeadProduct(int mainCatID, int subCatID, int modelID, bool IsElectronic, bool IsMail, int distributorID, int territoryManagerID, bool allowTerritoryOverlap, int manufacturerDistributorID)
        {


            LeadProduct dr = new LeadProduct();

            dr.fk_leadID = -1;
            dr.fk_MainCatID = mainCatID;
            dr.fk_ModelID = modelID;
            dr.fk_SubCatID = subCatID;
            dr.pk_leadProductID = -1;
            dr.IsElectronic = IsElectronic;
            dr.IsMail = IsMail;
            dr.distributorID = distributorID;
            dr.territoryManagerID = territoryManagerID;
            dr.AllowTerritoryOverlap = allowTerritoryOverlap;
            dr.manufacturerDistributorID = manufacturerDistributorID;


            _leadProducts.Add(dr);

        }

        /// <summary>
        /// Sends a fax with the parameters given
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="ccAddress"></param>
        /// <param name="smtpHost"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        /// <param name="Attachments"></param>
        /// <returns></returns>
        public string SendFax(string fromAddress, string toAddress, string ccAddress, string smtpHost, string body, string subject, string password, string type, List<Attachment> Attachments)
        {
            string error = "";

            try
            {
                //create the mail message
                MailMessage mail = new MailMessage();

                if (Attachments != null)
                    foreach (Attachment fileAttachment in Attachments)
                        mail.Attachments.Add(fileAttachment);


                //set the addresses
                //saleslead@findbomag.com
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);

                if (ccAddress != "")
                {
                    mail.CC.Add(ccAddress);
                }


                //set the content
                mail.Subject = subject;
                mail.Body = body;

                //send the message
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.findbomag.com";
                smtp.Host = smtpHost;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, password);


                smtp.Send(mail);
                //lblMessage.Text = "Mail Sent";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }




            return error;
        }

        public string SendFax(string fromAddress, string toAddress, string ccAddress, string smtpHost, string body, string subject, string password, string type)
        {
            return SendFax(fromAddress, toAddress, ccAddress, smtpHost, body, subject, password, type, null);
        }


        #endregion


        private int PendingLeadRebuild(int pendingLeadID)
        {
            ArrayList fields = new ArrayList();

            foreach (LeadValue lv in _leadValues)
            {
                BR.Lead.FormField tempField = new BR.Lead.FormField();
                tempField.ID = lv.elementName;
                tempField.value = lv.elementValue;
                tempField.IsValid = true;

                fields.Add(tempField);
            }



            ModelList modelList = new ModelList();

            ArrayList productArray = new ArrayList();

            foreach (LeadProduct lp in _leadProducts)
            {
                BR.Lead.ProductList tempProduct = new BR.Lead.ProductList();

                ModelList.Model currentModel = modelList.GetModel(lp.fk_ModelID);

                tempProduct.productId = lp.fk_MainCatID.ToString();
                tempProduct.modelID = currentModel.ModelID.ToString();
                tempProduct.subCatID = lp.fk_SubCatID.ToString();


                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter scta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();


                string modelName = currentModel.ModelName;
                string productName = ((string)mcta.GetMainCategoryNameByID(lp.fk_MainCatID));
                string subCatName = ((string)scta.GetSubCatNameByID(lp.fk_SubCatID));


                tempProduct.model = modelName;
                tempProduct.product = productName;
                tempProduct.subCat = subCatName;


                tempProduct.IsMail = lp.IsMail;
                tempProduct.IsElectronic = lp.IsElectronic;

                tempProduct.modelDisplayText = "";
                tempProduct.modelURL = currentModel.ModelURL;


                productArray.Add(tempProduct);


            }

            // Clear the values we're rebuilding hopefully
            bool distributionVendorEmailSent = false;

            foreach (LeadEmail dr in _leadEmails)
            {
                if (dr.fk_emailType == 2)
                    distributionVendorEmailSent = dr.emailSent;
            }

            _leadEmails.Clear();
            _leadValues.Clear();
            _leadProducts.Clear();

            System.Text.StringBuilder errorBuilder = new System.Text.StringBuilder();

            System.Web.UI.Page myPage = new Page();

            SalesLeadsMaster(ref errorBuilder, productArray, ref fields, false, ref myPage, true);



            // Purge all the old emails from the database and save the new ones
            leadEmailTA.DeleteLeadData(pendingLeadID);

            Console.WriteLine(_leadID);

            foreach (LeadEmail dr in _leadEmails)
            {
                int nextID = -1;


                nextID = DA.DataAccess.GetNextID("[DL.LeadEmail]", "pk_leadEmailID");

                DateTime sentDate = new DateTime();
                sentDate = GetSentDate(dr.sentDate);

                bool emailSent = dr.emailSent;
                
                if (dr.fk_emailType == 2)
                    emailSent = distributionVendorEmailSent;

                leadEmailTA.InsertQuery(nextID, pendingLeadID, dr.fk_emailType, dr.emailBody, dr.emailTo, dr.emailFrom, dr.emailCC, dr.emailBCC, dr.emailSubject, emailSent, sentDate);
            }

            return _leadID;

        }

        /// <summary>
        /// The date to examine
        /// </summary>
        /// <param name="dr">The </param>
        /// <param name="sentDate"></param>
        private DateTime GetSentDate(DateTime sentDate)
        {
            try
            {
                if (sentDate.Year > 1900)
                    return sentDate;
                else
                    sentDate = DateTime.Now;
            }
            catch
            {
                sentDate = DateTime.Now;
            }

            return sentDate;
        }

        #region WebService Calls

        public static void SendPendingLeads()
        {
            // Do not send any emails
            _AllowEmailsToSend = false;

            DateTime today = new DateTime();
            today = DateTime.Now;

            Lead pendingLead = new Lead(false);

            DA.LeadsTDS.DL_LeadDataTable tempLeadDT = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();

            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter tempLeadTA = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            tempLeadDT = tempLeadTA.GetUnsentLeadsByDate(today);



            DA.LeadsTDS.DL_LeadEmailDataTable ledt_1 = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();
            DA.LeadsTDS.DL_LeadEmailDataTable ledt_temp = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta_1 = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();

            int counter = 0;

            Page myPage = new Page();

            bool masterErrorOccurred = false;
            int masterNumberErrorOccurred = 0;

            List<string> sentEmailBodies = new List<string>();

            foreach (DA.LeadsTDS.DL_LeadRow dr in tempLeadDT.Rows)
            {

                pendingLead = new Lead(dr.pk_leadID);
                ArrayList tempEmails = new ArrayList();

                foreach (LeadEmail le in pendingLead._leadEmails)
                {
                    LeadEmail tempEmail = new LeadEmail();
                    tempEmail.emailSent = le.emailSent;
                    tempEmail.sentDate = le.sentDate;
                    tempEmail.pk_leadEmailID = le.pk_leadEmailID;
                    tempEmail.fk_emailType = le.fk_emailType;
                    tempEmail.fk_leadID = le.fk_leadID;

                    tempEmails.Add(tempEmail);
                }

                _AllowEmailsToSend = false;

                int _leadIdStaticPasser = pendingLead.PendingLeadRebuild(dr.pk_leadID);



                // Allow emails to send from here on out
                _AllowEmailsToSend = true;

                ledt_1 = leta_1.GetDataByLeadID(dr.pk_leadID);

                foreach (DA.LeadsTDS.DL_LeadEmailRow drEmail in ledt_1.Rows)
                {
                    bool IsEmailDuplicate = false;

                    foreach (string leBody in sentEmailBodies)
                    {
                        if (leBody == drEmail.emailBody)
                            IsEmailDuplicate = true;
                    }

                    bool hasBeenSentBefore = false;
                    DateTime sentDate = DateTime.Now;

                    foreach (LeadEmail le in tempEmails)
                    {
                        if (drEmail.pk_leadEmailID == le.pk_leadEmailID)
                        {
                            hasBeenSentBefore = le.emailSent;
                            sentDate = le.sentDate;
                        }
                    }

                    if (IsEmailDuplicate == false && hasBeenSentBefore == false)
                    {
                        DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter letta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();


                        string type;

                        type = letta.GetEmailTypeByID(drEmail.fk_emailType).ToString();

                        string errorMessage = pendingLead.SendEmail(drEmail.emailFrom, drEmail.emailTo, drEmail.emailCC, drEmail.emailBody, drEmail.emailSubject, "findbomagco", type, ref  myPage, true);


                        bool errorOccurred = false;

                        if (errorMessage != "")
                        {
                            PendingLeadErrorMessage += errorMessage + "<BR><BR>";

                            errorOccurred = true;
                            masterErrorOccurred = true;
                            masterNumberErrorOccurred += 1;
                        }


                        sentEmailBodies.Add(drEmail.emailBody);

                        leta_1.UpdateLeadEmailSentStatus(true, DateTime.Now, drEmail.pk_leadEmailID);
                        tempLeadTA.UpdateSentStatus(true, errorOccurred, errorMessage, dr.pk_leadID);

                        counter += 1;
                    }
                }


                Lead.RemoveLead(_leadIdStaticPasser);

            }


            // send email to Tim and I if an email was sent
            if (counter > 0)
            {
                string body = counter + " email(s) attempted to send on " + today.ToString() + ".";

                body = body + Environment.NewLine + Environment.NewLine + masterNumberErrorOccurred + " error(s) occurred.";

                //pendingLead.SendEmail("saleslead@findbomag.com", "tim.tiemann@bomag.com", "nwilson@nwilson.org", "smtp.findbomag.com", body, "Lead Emails Sent on " + today.ToString(), "findbomagco", "User Email", ref myPage);
                pendingLead.SendEmail("saleslead@findbomag.com", "nwilson@nwilson.org", "nwilson@nwilson.org", body, "Lead Emails Sent on " + today.ToString(), "findbomagco", "User Email", ref myPage);
            }

        }

        #endregion


        #region Sales Lead Form Stuff


        #region Variables

        string leadFirstName, leadLastName, leadUserName, leadUserEmail, leadCompany, leadAddress1, leadAddress2, leadZip;


        bool PlanToBuy = true, IsUnitedStates = true, InDevMode = false;

        string CountryRegion = "";

        const string tabCharacter = "    ";

        public string leadCity, leadState, zipCode;
        public string leadSource = string.Empty;
        public string leadComments = string.Empty;
        public string leadPhone = string.Empty;

        int MonthsToAddToSendDate = 0;

        #endregion


        #region GetMonthsToAddToSubmit(...)

        public int GetMonthsToAddToSubmit(string value)
        {
            int MonthsToAddToSendDate = 0;

            value = value.ToUpper();

            switch (value)
            {
                case "IMMEDIATELY":
                    MonthsToAddToSendDate = 0;
                    break;

                case "1 TO 3 MONTHS":
                    MonthsToAddToSendDate = 0;
                    break;

                case "4 TO 6 MONTHS":
                    MonthsToAddToSendDate = 3;
                    break;

                case "1 YEAR":
                    MonthsToAddToSendDate = 10;
                    break;

                case "DO NOT PLAN TO BUY":
                    MonthsToAddToSendDate = 0;
                    break;

                default:
                    MonthsToAddToSendDate = 0;
                    break;
            }

            return MonthsToAddToSendDate;
        }

        #endregion



        public struct ProductList
        {
            public string productId;
            public string product;
            public string subCat;
            public string subCatID;
            public string model;
            public string modelID;
            public string modelDisplayText;
            public string modelURL;
            public bool AllowTerritoryOverlap;
            public bool IsElectronic;
            public bool IsMail;
            public DA.ContractTDS.DistributorRow DistributorInfo;
            public DA.ContractTDS.DistributorRow ManufacturerDistributorInfo;

        }

        public struct DistributorCategoryRep
        {
            public string MainCategory;
            public bool AllowTerritoryOverlap;
            public string RepEmail;
            public string RepName;
            public string RepID;
            public ArrayList models;
            public DA.ContractTDS.DistributorRow DistributorInfo;
            public DA.ContractTDS.DistributorRow ManufacturerDistributorInfo;
        }


        #region CreateMainCategories()

        /// <summary>
        /// Creates an arraylist with the main categories that the products are in, as well as calculates the closest distributor for them
        /// </summary>
        /// <param name="productArray">List of product models we have</param>
        /// <param name="mainCatsArrayList">Arraylist of current main categories.</param>
        /// <param name="IsUnitedStates">Is this a US location?  If not, we won't do lookup</param>
        /// <param name="leadCity">City the location is in</param>
        /// <param name="leadState">State it is in</param>
        /// <returns>Returns list of all current unique main categories</returns>
        public ArrayList CreateMainCategories(ArrayList productArray, ArrayList mainCatsArrayList, bool IsUnitedStates, string leadCity, string leadState)
        {

            DA.ContractTDS.DistributorRow drShortestDistanceDistributor = null;
            DA.ContractTDS.DistributorRow drShortestDistanceManufacturerRepDistributor = null;

            for (int i = 0; i < productArray.Count; i++)
            {

                ProductList tempProduct = (ProductList)(productArray[i]);

                // Clean up the data model by adding some attributes first
                bool inArray = false;

                foreach (DistributorCategoryRep tempStruct in mainCatsArrayList)
                {
                    if (tempStruct.MainCategory == tempProduct.product)
                        inArray = true;

                }

                // main category is not in the arraylist yet.  add it
                if (inArray == false)
                {
                    DistributorCategoryRep distributorCategoryRep = new DistributorCategoryRep();
                    distributorCategoryRep.MainCategory = tempProduct.product;
                    distributorCategoryRep.models = new ArrayList();  // initialize array.  values will be added later
                    distributorCategoryRep.AllowTerritoryOverlap = false;

                    // if user is in the United States, do a lookup for the closest distributor
                    if (IsUnitedStates)
                    {
                        int manufacturerRepParameter = -1;
                        double shortestDistance;
                        Location shortestLocation;

                        // Get closest distributor
                        Dealer_Locator.BR.DistributorLookup distLookup = new Dealer_Locator.BR.DistributorLookup(Convert.ToInt32(tempProduct.productId), leadCity, leadState, leadZip);

                        if (tempProduct.AllowTerritoryOverlap == true)
                        {
                            manufacturerRepParameter = 0;
                            distributorCategoryRep.AllowTerritoryOverlap = true;
                        }

                        drShortestDistanceDistributor = distLookup.GetClosestDistributor(out shortestDistance, out shortestLocation, Convert.ToInt32(tempProduct.productId), manufacturerRepParameter);

                        if (drShortestDistanceDistributor != null)
                        {
                            // assign that distributor to the main category
                            distributorCategoryRep.DistributorInfo = drShortestDistanceDistributor;

                        }

                        if (tempProduct.AllowTerritoryOverlap == true)
                        {
                            drShortestDistanceManufacturerRepDistributor = distLookup.GetClosestDistributor(out shortestDistance, out shortestLocation, Convert.ToInt32(tempProduct.productId), 1);

                            if (drShortestDistanceManufacturerRepDistributor != null)
                            {
                                // assign that distributor to the main category
                                distributorCategoryRep.ManufacturerDistributorInfo = drShortestDistanceManufacturerRepDistributor;
                            }
                        }

                    }

                    // add the new main category
                    mainCatsArrayList.Add(distributorCategoryRep);

                }

                if (IsUnitedStates)
                {
                    // should always work correctly
                    tempProduct.DistributorInfo = drShortestDistanceDistributor;
                    tempProduct.ManufacturerDistributorInfo = drShortestDistanceManufacturerRepDistributor;
                }

                foreach (DistributorCategoryRep tempStruct in mainCatsArrayList)
                {
                    if (tempStruct.MainCategory == tempProduct.product)
                    {
                        // add models to the structure's array
                        tempStruct.models.Add(tempProduct);
                    }
                }

                productArray[i] = tempProduct;

            }

            return mainCatsArrayList;

        }

        #endregion

        #region private string SendEmail()


        public string SendEmail(string fromAddress, string toAddress, string ccAddress, string body, string subject, string password, string type, ref System.Web.UI.Page myPage, List<Attachment> fileAttachments, bool vendorEmailSent = false)
        {
            string error = "";

            string smtpHost = "smtp.findbomag.com";

            this.AddLeadEmail(type, body, toAddress, fromAddress, ccAddress, "", subject, DateTime.Now, vendorEmailSent);

            // InDevMode = false;

            if (InDevMode == true && type != "Distributor Fax")
            {
                // type = type of email being sent
                string totalBody = "";


                try
                {
                    totalBody = myPage.Session[type].ToString();
                }
                catch
                {
                }

                totalBody = totalBody + "<table width=\"100%\" style=\"border: 1px solid gray; padding: 4px;\"><tr><td>" + Environment.NewLine;
                totalBody = totalBody + "To: " + toAddress + Environment.NewLine + "From: " + fromAddress + Environment.NewLine;

                if (ccAddress != "")
                    totalBody = totalBody + "CC: " + ccAddress + Environment.NewLine + Environment.NewLine;

                totalBody = totalBody + "Subject: " + subject + Environment.NewLine + Environment.NewLine;
                totalBody = totalBody + body;
                totalBody = totalBody + Environment.NewLine + "</td></tr></table>";

                try
                {
                    myPage.Session.Remove(type);
                }
                catch
                {
                }



                myPage.Session.Add(type, totalBody);

                return "";
            }

            if ((MonthsToAddToSendDate == 0 || (type == "Distribution Vendor"
                                                    || type == "User Email"))
                   && InKioskMode == false)
            {
                if (type == "Distribution Vendor" && vendorEmailSent)
                {
                    // Do not send...do logging here later for some reason?
                }
                else
                {
                    try
                    {
                        //create the mail message
                        MailMessage mail = new MailMessage();

                        if (fileAttachments != null)
                            foreach (Attachment file in fileAttachments)
                                mail.Attachments.Add(file);

                        //set the addresses
                        //saleslead@findbomag.com
                        mail.From = new MailAddress(fromAddress);
                        mail.To.Add(toAddress);

                        if (ccAddress != "")
                        {
                            if (ccAddress.IndexOf(";") > 0)
                            {
                                string[] ccAddressList = ccAddress.Split(';');

                                foreach (string addressTemp in ccAddressList)
                                {
                                    mail.CC.Add(addressTemp.Trim());
                                }
                            }
                            else
                            {
                                mail.CC.Add(ccAddress);
                            }
                        }


                        //set the content
                        mail.Subject = subject;
                        mail.Body = body;

                        //send the message
                        SmtpClient smtp = new SmtpClient();
                        //smtp.Host = "smtp.findbomag.com";
                        smtp.Host = smtpHost;
                        smtp.Port = 587;  // Added 11/23/2014
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(fromAddress, password);
                        smtp.Timeout = 60000;
                        smtp.EnableSsl = false;

                        if (_AllowEmailsToSend == true)
                        {

                            smtp.Send(mail);
                            //lblMessage.Text = "Mail Sent";
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }


                }
            }
            return error;
        }

        public string SendEmail(string fromAddress, string toAddress, string ccAddress, string body, string subject, string password, string type, ref System.Web.UI.Page myPage, bool vendorEmailSent = false)
        {
            return SendEmail(fromAddress, toAddress, ccAddress, body, subject, password, type, ref myPage, null, vendorEmailSent);
        }

        #endregion


        public struct FormField
        {
            public string ID;
            public string value;
            public bool IsValid;
        }

        private bool HideFieldCheck(string fieldId)
        {
            bool hideField = false;

            foreach (DictionaryEntry htEntry in _excludeFieldList)
            {
                if (fieldId.Contains(htEntry.Key.ToString()))
                {
                    hideField = true;
                    break;
                }
            }

            return hideField;
        }

        public string GenerateFormValuesOutput(ArrayList fields, bool HidePlanToBuy)
        {
            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();



            string errorMessage = "";


            //sb3.Append("Sales Lead Form Name: " + slFormName + Environment.NewLine + Environment.NewLine);
            //sb3.Append("The following form was submitted at: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString() + Environment.NewLine + Environment.NewLine);

            ArrayList arrBadFields = new ArrayList();

            bool stateAdded = false;
            string tempzipcode = "";
            // validate our fields

            _excludeFieldList = new Hashtable();

            _excludeFieldList.Add("__EVENTTARGET", null);
            _excludeFieldList.Add("__EVENTARGUMENT", null);
            _excludeFieldList.Add("__LASTFOCUS", null);
            _excludeFieldList.Add("__VIEWSTATE", null);
            _excludeFieldList.Add("BUTTON1", null);
            _excludeFieldList.Add("BUTTON4", null);
            _excludeFieldList.Add("TXTITEMTOREMOVE", null);
            _excludeFieldList.Add("CBOPRODUCTTYPE", null);
            _excludeFieldList.Add("CBOSUBCATEGORY", null);
            _excludeFieldList.Add("CBOMODELS", null);
            _excludeFieldList.Add("CHKELECTRONIC", null);
            _excludeFieldList.Add("CHKBYMAIL", null);
            _excludeFieldList.Add("LBLHIDDENCITY2", null);
            _excludeFieldList.Add("CODENUMBERTEXTBOX", null);
            _excludeFieldList.Add("__SCROLLPOSITIONX", null);
            _excludeFieldList.Add("SCROLLPOSITIONY", null);
            _excludeFieldList.Add("__EVENTVALIDATION", null);
            _excludeFieldList.Add("CBLMODELS", null);
            _excludeFieldList.Add("BTNSENDSALESLEADFORM", null);
            _excludeFieldList.Add("THEHIDDENTABINDEX", null);
            _excludeFieldList.Add("PRODUCTSEARCHTEXTBOX", null);
            _excludeFieldList.Add("MENUIDHIDDENFIELD", null);
            _excludeFieldList.Add("PHYSICALDELIVERY", null);
            _excludeFieldList.Add("ELECTRONICDELIVERY", null);
            _excludeFieldList.Add("CHK_", null);
            _excludeFieldList.Add("BTNSALESLEADFORMHIDDEN", null);
            _excludeFieldList.Add("KIOSKMODEHIDDENFIELD", null);
            _excludeFieldList.Add("RADIOS", null);




            //if (!excludeFieldList.ContainsKey(tempFieldId.ToUpper()))


            foreach (FormField tempField in fields)
            {
                string tempFieldId = tempField.ID;
                tempFieldId = tempFieldId.Replace("ctl00$ContentPlaceHolder1$", "");

                string tempFieldValue = tempField.value;
                tempFieldValue = tempFieldValue.Replace(",", ", ");
                tempFieldValue = tempFieldValue.Replace(",  ", ", ");


                //if ((tempFieldId.Contains("__EVENTTARGET") == false)
                //    && (tempFieldId.Contains("__EVENTARGUMENT") == false)
                //    && (tempFieldId.Contains("__LASTFOCUS") == false)
                //    && (tempFieldId.Contains("__VIEWSTATE") == false)
                //    && (tempFieldId.Contains("Button1") == false)
                //    && (tempFieldId.Contains("Button4") == false)
                //    && (tempFieldId.Contains("txtItemToRemove") == false)
                //    && (tempFieldId.Contains("cboProductType") == false)
                //    && (tempFieldId.Contains("cboSubCategory") == false)
                //    && (tempFieldId.Contains("cboModels") == false)
                //    && (tempFieldId.Contains("chkElectronic") == false)
                //    && (tempFieldId.Contains("chkByMail") == false)
                //    && (tempFieldId.Contains("lblHiddenCity2") == false)
                //    && (tempFieldId.Contains("CodeNumberTextBox") == false)
                //    && (tempFieldId.Contains("__SCROLLPOSITIONX") == false)
                //    && (tempFieldId.Contains("MODELS") == false)
                //    && (tempFieldId.Contains("__SCROLLPOSITIONY") == false)
                //    && (tempFieldId.Contains("__EVENTVALIDATION") == false)
                //    && (tempFieldId.ToUpper().Contains("CBLMODELS") == false)
                //    && (tempFieldId.ToUpper().Contains("BTNSENDSALESLEADFORM") == false)
                //    && (tempFieldId.ToUpper().Contains("THEHIDDENTABINDEX") == false)
                //    && (tempFieldId.ToUpper().Contains("PRODUCTSEARCHTEXTBOX") == false)
                //    && (tempFieldId.ToUpper().Contains("MENUIDHIDDENFIELD") == false)
                //    && (tempFieldId.ToUpper().Contains("PHYSICALDELIVERY") == false)
                //    && (tempFieldId.ToUpper().Contains("ELECTRONICDELIVERY") == false)

                //    )
                int fakeInt = -1;

                if (HideFieldCheck(tempFieldId.ToUpper()) == false && Int32.TryParse(tempFieldId, out fakeInt) == false)
                {
                    if (tempFieldId.ToUpper().Contains("FIRSTNAME"))
                        leadFirstName = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("LASTNAME"))
                        leadLastName = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("EMAIL") && !tempFieldId.ToUpper().Contains("EMAILDELIVERY"))
                        leadUserEmail = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("STATE"))
                        leadState = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("ADDRESS1"))
                        leadAddress1 = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("ADDRESS2"))
                        leadAddress2 = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("STATE"))
                        leadState = tempFieldValue;


                    if (tempFieldId.ToUpper().Contains("COMPANY_AGENCY"))
                        leadCompany = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("SOURCE_CSS") || tempFieldId.ToUpper().Contains("SOURCE"))
                        leadSource = tempFieldValue;

                    if (tempFieldId.ToUpper().Contains("PHONE"))
                        leadPhone = tempFieldValue;

                    //                    string leadFirstName-OK, leadLastName-OK, leadUserName-OK, leadUserEmail-OK, leadCompany, leadAddress1=OK, leadAddress2-OK, leadZip-OK;



                    tempFieldId = tempFieldId.Replace("txt", "");
                    tempFieldId = tempFieldId.Replace("cbo", "");
                    tempFieldId = tempFieldId.Replace("Zip2", "Zip Code");


                    if (tempFieldId.ToUpper().Contains("COUNTRYREGION"))
                    {
                        CountryRegion = tempFieldValue;
                    }



                    if (tempFieldId.ToUpper().Contains("PLANTOBUY"))
                    {
                        if (tempFieldValue.ToString() == "Do Not Plan To Buy")
                            PlanToBuy = false;
                        else
                            PlanToBuy = true;


                        tempFieldId = "Plan To Buy";

                        MonthsToAddToSendDate = this.GetMonthsToAddToSubmit(tempFieldValue.ToUpper());

                        if (HidePlanToBuy == false)
                            sb3.AppendLine("");
                    }

                    if (tempFieldId.ToUpper().Contains("COMMENTS"))
                    {
                        sb3.AppendLine("");
                        leadComments = tempFieldValue;  // for vendor email
                    }

                    // #######################################
                    // # ADD LEADValue to the LEAD class object
                    // # Only add it once
                    // #######################################
                    if (HidePlanToBuy == false)
                        this.AddLeadValue(tempFieldId.ToString(), tempFieldValue);



                    if (CountryRegion == "United States" || CountryRegion == "" || CountryRegion == "USA")
                    {
                        IsUnitedStates = true;

                        if (IsRestricted("United States", tempFieldId) == false)
                        {
                            if (tempFieldId.ToUpper().Contains("ZIP") == false)
                            {
                                if (tempFieldId.ToUpper().Contains("PLAN TO BUY"))
                                {
                                    if (HidePlanToBuy == false)
                                        sb3.Append(tempFieldId + ": " + tempFieldValue + Environment.NewLine);
                                }
                                else
                                {
                                    // if its the source css field then do not display it in the email
                                    if (tempFieldId.ToUpper().Contains("SOURCE_CSS") == false)
                                        sb3.Append(tempFieldId + ": " + tempFieldValue + Environment.NewLine);
                                }


                                if (tempFieldId.ToUpper().Contains("CITY"))
                                {

                                    leadCity = tempFieldValue;

                                    tempFieldId = "City";
                                }


                                if (tempFieldId.ToUpper().Contains("STATE"))
                                {
                                    if (tempzipcode.Length > 0)
                                    sb3.Append("Zip" + ": " + tempzipcode + Environment.NewLine);

                                    tempzipcode = "";
                                    stateAdded = true;
                                }
                            }

                            if (tempFieldId.ToUpper().Contains("ZIP"))
                            {

                                leadZip = tempFieldValue;

                                tempzipcode = tempFieldValue;

                                if (stateAdded)
                                    sb3.Append("Zip" + ": " + tempzipcode + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        // other country
                        IsUnitedStates = false;

                        if (IsRestricted("Other", tempFieldId) == false)
                        {
                            sb3.Append(tempFieldId + ": " + tempFieldValue + Environment.NewLine);
                        }
                    }

                }

                leadUserName = leadFirstName + " " + leadLastName;

            }

            return sb3.ToString();
        }

        string errorMessage;

        public void ClearMasterValues()
        {
            leadFirstName = "";
            leadLastName = "";
            leadCompany = "";
            leadAddress1 = "";
            leadAddress2 = "";
            leadCity = "";
            leadState = "";
            leadZip = "";
            leadUserName = "";
            leadUserEmail = "";

        }


        private bool IsRestricted(string type, string fieldID)
        {
            ArrayList restricted = new ArrayList();
            bool IsIDRestricted = false;

            if (type == "United States")
            {
                restricted.Add("OTHERCOUNTRY");
                restricted.Add("OTHERPOSTALCODE");
                restricted.Add("OTHERSTATEPROVINCE");
                restricted.Add("OTHERCITY");
            }
            else
            {
                // other

                restricted.Add("TXTSTATE");
                restricted.Add("TXTCITYNAME");
                restricted.Add("TXTZIP2");

            }

            foreach (string resItem in restricted)
            {
                if (fieldID.ToUpper().Contains(resItem.ToUpper()))
                    IsIDRestricted = true;
            }

            return IsIDRestricted;
        }



        public void SalesLeadsMaster(ref System.Text.StringBuilder errorBuilder, ArrayList productArray, ref ArrayList fields, bool InDevMode2, ref System.Web.UI.Page myPage, bool vendorEmailSent = false )
        {
            InDevMode = InDevMode2;

            string OUTPUT_FormValues = this.GenerateFormValuesOutput(fields, false);


            DA.LeadBlackList leadBlackList = new DA.LeadBlackList();
            DataTable dt = leadBlackList.GetBlackList();

            leadPhone = Regex.Replace(leadPhone, @"[^\d]", "");

            foreach (DataRow dr in dt.Rows)
            {
                string rowLastName = dr["lastName"].ToString().ToUpper();
                string rowCity = dr["city"].ToString().ToUpper();
                string rowState = dr["state"].ToString().ToUpper();
                string rowZip = dr["zip"].ToString().ToUpper();
                string rowPhone = dr["phone"].ToString().ToUpper();


                if (leadLastName.ToUpper() == rowLastName
                        && leadCity.ToUpper() == rowCity
                        && leadState.ToUpper() == rowState
                        && leadZip.ToUpper() == rowZip
                        && leadPhone == rowPhone)
                {
                    // set up email + send
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.AppendLine("Dear " + leadFirstName + ",");
                    sb.AppendLine("");
                    sb.AppendLine("A request has been made to send BOMAG product literature to your attention. At this time we are unable to process this request. Please contact Tim Tiemann (tim.tiemann@bomag.com) at 309-852-6216 for further information.");


                    SendEmail("saleslead@findbomag.com", leadUserEmail, "", sb.ToString(), "Lead Submission Message", "findbomagco", "User Email", ref myPage);


                    // Update the black list record
                    leadBlackList.UpdateItem(Convert.ToInt32(dr["leadBlackListId"].ToString()), "", 1);

                    // On return from SalesLeadMaster, page will automatically redirect to thank you url.
                    return;

                }

            }


            string TM_Fax_FormValues = this.GenerateFormValuesOutput(fields, true);

            string vendorBody = "Distribution Vendor," + Environment.NewLine + Environment.NewLine + OUTPUT_FormValues;


            errorMessage = "";
            errorBuilder = new System.Text.StringBuilder();


            #region Create Main Categories ArrayList and Find Closest Distributor

            ArrayList MainCatsArrayList = new ArrayList();


            if (productArray != null)
            {
                MainCatsArrayList = this.CreateMainCategories(productArray, MainCatsArrayList, IsUnitedStates, leadCity, leadState);
            }
            else
            {
                productArray = new ArrayList();
                MainCatsArrayList = new ArrayList();
            }


            #endregion

            if (productArray.Count > 0)
            {
                if (IsUnitedStates == false)
                {
                    ArrayList faxProducts = new ArrayList();
                    // Loop the main categories and build our emails from them.
                    foreach (BR.Lead.DistributorCategoryRep tempCategoryList in MainCatsArrayList)
                    {

                        foreach (BR.Lead.ProductList tempProduct in tempCategoryList.models)
                        {

                            int manufacturerDistId = -1;

                            if (tempProduct.ManufacturerDistributorInfo != null && tempProduct.ManufacturerDistributorInfo.pk_DistributorID != null)
                            {
                                manufacturerDistId = tempProduct.ManufacturerDistributorInfo.pk_DistributorID;
                            }

                            this.AddLeadProduct(Convert.ToInt32(tempProduct.productId), Convert.ToInt32(tempProduct.subCatID), Convert.ToInt32(tempProduct.modelID),
                                               tempProduct.IsElectronic, tempProduct.IsMail, -1, -1, tempProduct.AllowTerritoryOverlap, manufacturerDistId);


                            faxProducts.Add(tempProduct);
                        }
                    }

                    BR.Lead.DistributorCategoryRep tempCategory = new BR.Lead.DistributorCategoryRep();

                    tempCategory.RepName = "Temp";
                    tempCategory.RepEmail = "Temp";


                    DA.DL.DL_RegionDataTable rdt = new Dealer_Locator.DA.DL.DL_RegionDataTable();
                    DA.DLTableAdapters.DL_RegionTableAdapter rta = new Dealer_Locator.DA.DLTableAdapters.DL_RegionTableAdapter();

                    rdt = rta.GetDataByRegionName(CountryRegion);

                    string worldwideDealerName = rdt[0].RegionContactName;
                    string worldwideDealerEmail = rdt[0].RegionEmail;

                    // send fax
                    string faxEmailMessage = GenerateFax(TM_Fax_FormValues, faxProducts, tempCategory, worldwideDealerName, false, false);
                    errorMessage = SendEmail("saleslead@findbomag.com", worldwideDealerEmail, "", faxEmailMessage, "Sales Lead from FindBomag.com", "findbomagco", "Distributor Fax", ref myPage);

                    if (errorMessage != "")
                        errorBuilder.Append(errorMessage + Environment.NewLine);

                    errorMessage = "";


                }
                else
                {
                    // #########################################
                    // ##  Criteria  ##
                    // #########################################
                    //* Territory Manager (TM) - receives email based on the email address within 
                    //the DDA uploaded database

                    //* If Customer (Who filled out the lead) chooses to receive spec sheet "Electronic"
                    // - he receives an email with a link to the PDF file associated with the products chosen on the lead form

                    //* If Customer (Who filled out the lead) chooses to receive spec sheet "By Mail"
                    // - The "Distribution Vendor" receives an email with Customer shipping information and the Distributor information located by the zip locator.

                    //* Distributor - receives the fax of the lead based on fax number within the DDA uploaded database

                    /*
                     -If Rental
                        Light equipment: All "light" leads will be sent to the Manufacturer Rep ONLY.  The Heavy Dist will NOT receive any sort of communication for light equipment, even if they have a contract.  
                        Heavy equipment: All leads will be sent to the Heavy Rep for that region.  An additional email will be sent to the person you set up through the new admin area (default: Bert Dejong)

                     -If Not Rental
                        Light: Leads go to the Manu Rep and the Heavy Distributor.  Does this mean that all TM emails, all faxes, etc. are sent out twice, once to the Manu Rep recipients and once to the Heavy Distributor recipients?  I believe leads would have to be sent twice because both the Manu reps and Heavy reps would have different TMs and fax numbers.
                        Heavy: All leads will be sent to the Heavy Dist for that region.  (system currently does this)

                     */


                    ArrayList faxProducts;
                    ArrayList terrManProducts;
                    ArrayList userProducts = new ArrayList();
                    ArrayList vendorProducts = new ArrayList();
                    errorMessage = "";
                    errorBuilder = new System.Text.StringBuilder();


                    // Loop the main categories and build our emails from them.
                    foreach (BR.Lead.DistributorCategoryRep tempCategory in MainCatsArrayList)
                    {

                        faxProducts = new ArrayList();
                        terrManProducts = new ArrayList();

                        DA.ContractTDS.DistributorRow leadDistributorInfo = tempCategory.DistributorInfo;
                        DA.ContractTDS.DistributorRow manuDistributorInfo = tempCategory.ManufacturerDistributorInfo;

                        if (isRentalCompany)
                        {

                            if (tempCategory.AllowTerritoryOverlap)
                            {
                                // If YES territory overlap, only "ManufacturerRep" distributor gets lead
                                ProcessDistributor(ref errorBuilder, ref myPage, TM_Fax_FormValues, ref faxProducts, ref terrManProducts, ref userProducts, ref vendorProducts, tempCategory, manuDistributorInfo, true,false,true);
                            }
                            else
                            {
                                // If NO territory overlap, only "heavy" distributor gets lead and "Rental Administrator" gets cc'd on the territory rep email
                                ProcessDistributor(ref errorBuilder, ref myPage, TM_Fax_FormValues, ref faxProducts, ref terrManProducts, ref userProducts, ref vendorProducts, tempCategory, leadDistributorInfo, true, true);
                            }
                        }
                        else
                        {
                            if (tempCategory.AllowTerritoryOverlap)
                            {
                                // If YES territory overlap, both "heavy" distributor and "ManufacturerRep" distributor get lead
                                if (leadDistributorInfo != null)
                                {
                                    ProcessDistributor(ref errorBuilder, ref myPage, TM_Fax_FormValues, ref faxProducts, ref terrManProducts, ref userProducts, ref vendorProducts, tempCategory, leadDistributorInfo, true);
                                }

                                ProcessDistributor(ref errorBuilder, ref myPage, TM_Fax_FormValues, ref faxProducts, ref terrManProducts, ref userProducts, ref vendorProducts, tempCategory, manuDistributorInfo, false, false, true);
                            }
                            else
                            {
                                // If NO territory overlap, only "heavy" distributor gets lead
                                ProcessDistributor(ref errorBuilder, ref myPage, TM_Fax_FormValues, ref faxProducts, ref terrManProducts, ref userProducts, ref vendorProducts, tempCategory, leadDistributorInfo, true);
                            }
                        }


                    }

                    // send user email

                    string ccEmail = "";

                    if (PlanToBuy == false)
                    {


                    }

                    if (leadUserEmail != "" && PlanToBuy == true && userProducts.Count > 0)
                    {
                        string userEmailMessage = GenerateUserEmail(OUTPUT_FormValues, userProducts, leadUserName);
                        errorMessage = SendEmail("saleslead@findbomag.com", leadUserEmail, ccEmail, userEmailMessage, "Product Information from FindBomag.com", "findbomagco", "User Email", ref myPage);

                        if (errorMessage != "")
                            errorBuilder.Append(errorMessage + Environment.NewLine);

                        errorMessage = "";

                    }
                    else if (PlanToBuy == false)
                    {
                        bool userSubmittedEmail = true;

                        bool VendorEmailSent = false;

                        // Get CC Email
                        DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();
                        DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();

                        etdt = etta.GetDataByEmailType("DoNotPlanToBuy");

                        if (etdt != null && etdt.Rows.Count > 0)
                            ccEmail = etdt[0].emailAddress;

                        // by request of Tim on 07/09/07.
                        // changed by request of Tim via email on 05/15/08 - "FW: Product Information from FindBomag.com"
                        // if leaduseremail is empty, "DoNotPlanToBuy" email becomes the "to" address
                        if (leadUserEmail.Length == 0)
                        {
                            leadUserEmail = ccEmail;
                            userSubmittedEmail = false;
                        }

                        DA.VendorTDS.DL_VendorDataTable vdt = new Dealer_Locator.DA.VendorTDS.DL_VendorDataTable();
                        DA.VendorTDSTableAdapters.DL_VendorTableAdapter vta = new Dealer_Locator.DA.VendorTDSTableAdapters.DL_VendorTableAdapter();

                        vdt = vta.GetData();

                        string venderEmail = vdt[0].email;
                        vendorBody = GenerateVendorEmail(OUTPUT_FormValues, vendorProducts);

                        bool productIsElectronic = false;
                        bool productIsMail = false;

                        foreach (ProductList tempProdList in vendorProducts)
                        {
                            if (tempProdList.IsElectronic == true)
                                productIsElectronic = true;

                            if (tempProdList.IsMail == true)
                                productIsMail = true;
                        }


                        foreach (ProductList tempProdList in userProducts)
                        {
                            if (tempProdList.IsElectronic == true)
                                productIsElectronic = true;

                            if (tempProdList.IsMail == true)
                                productIsMail = true;
                        }

                        // If DNPTB
                        // ELECTRONIC
                        if (productIsElectronic == true)
                        {
                            string userEmailMessage = GenerateUserEmail(OUTPUT_FormValues, userProducts, leadUserName);
                            errorMessage = SendEmail("saleslead@findbomag.com", leadUserEmail, ccEmail, userEmailMessage, "Product Information from FindBomag.com", "findbomagco", "User Email", ref myPage);

                            if (errorMessage != "")
                                errorBuilder.Append(errorMessage + Environment.NewLine);

                            errorMessage = "";

                            // USER DOESN'T SUBMIT EMAIL
                            if (userSubmittedEmail == false)
                            {
                                // Vendor Email
                                VendorEmailSent = true;

                                // == Distribution Vendor Email ==
                                //errorMessage = SendEmail("saleslead@findbomag.com", venderEmail, "", vendorBody, "Sales Lead from FindBomag.com to Distribution Vendor", "findbomagco", "Distribution Vendor", ref myPage);
                                errorMessage = SendEmail("saleslead@findbomag.com", venderEmail, "nwilson@nwilson.org; tim.tiemann@hotmail.com", vendorBody, "Sales Lead from FindBomag.com to Distribution Vendor", "findbomagco", "Distribution Vendor", ref myPage);
                                
                                errorBuilder.Append("productIsElectronic == true");

                                if (errorMessage != "")
                                    errorBuilder.Append(errorMessage + Environment.NewLine);

                                errorMessage = "";
                            }

                        }

                        if (productIsMail == true)
                        {
                            // Product is mail, and do not plan to buy is true.  NO user email sent, but DONOTPLANTOBUY email is sent
                            etdt = etta.GetDataByEmailType("DoNotPlanToBuy");

                            if (etdt != null && etdt.Rows.Count > 0)
                                leadUserEmail = etdt[0].emailAddress;

                            string DoNotPlanToBuyEmailMessage = GenerateDoNotPlanToBuy(OUTPUT_FormValues);
                            errorMessage = SendEmail("saleslead@findbomag.com", leadUserEmail, "", DoNotPlanToBuyEmailMessage, "BOMAG -Do Not Plan To Buy", "findbomagco", "User Email", ref myPage);

                            if (errorMessage != "")
                                errorBuilder.Append(errorMessage + Environment.NewLine);

                            errorMessage = "";


                            if (VendorEmailSent == false)
                            {

                                // == Distribution Vendor Email ==
                                errorMessage = SendEmail("saleslead@findbomag.com", venderEmail, "nwilson@nwilson.org; tim.tiemann@hotmail.com", vendorBody, "Sales Lead from FindBomag.com to Distribution Vendor", "findbomagco", "Distribution Vendor", ref myPage);

                                errorBuilder.Append("productIsMail == true");

                                if (errorMessage != "")
                                    errorBuilder.Append(errorMessage + Environment.NewLine);

                                errorMessage = "";
                            }
                        }

                    }



                    if (leadUserEmail != "" || PlanToBuy == false)
                    {
                    }
                    else
                    {
                        errorMessage = "There was no user email address submitted for this lead.  The CC Lookup for DoNotPlanToBuy may have failed as well";

                        if (errorMessage != "")
                            errorBuilder.Append(errorMessage + Environment.NewLine);

                        errorMessage = "";

                    }

                    ccEmail = "";
                    //}


                    if (PlanToBuy == true)
                    {

                        // send distribution vendor email
                        DA.VendorTDS.DL_VendorDataTable vdt = new Dealer_Locator.DA.VendorTDS.DL_VendorDataTable();
                        DA.VendorTDSTableAdapters.DL_VendorTableAdapter vta = new Dealer_Locator.DA.VendorTDSTableAdapters.DL_VendorTableAdapter();

                        vdt = vta.GetData();

                        string venderEmail = vdt[0].email;

                        // determine which products will get lumped intto which distributor categories
                        int processed = 0;

                        ArrayList groupHolder = new ArrayList();
                        ArrayList processedCategories = new ArrayList();
                        string currentID = "";

                        while (processed < vendorProducts.Count)
                        {
                            ArrayList tempGroup = new ArrayList();
                            foreach (BR.Lead.ProductList tempProduct in vendorProducts)
                            {
                                if (currentID == "")
                                {
                                    if (processedCategories.Count > 0)
                                    {


                                        bool IsFound = false;
                                        foreach (string tempID in processedCategories)
                                        {
                                            if (tempID == tempProduct.DistributorInfo.pk_DistributorID.ToString())
                                                IsFound = true;
                                        }

                                        if (IsFound == false)
                                        {
                                            currentID = tempProduct.DistributorInfo.pk_DistributorID.ToString();  // set to subcat

                                            processedCategories.Add(currentID);
                                        }


                                    }
                                    else
                                    {
                                        currentID = tempProduct.DistributorInfo.pk_DistributorID.ToString();  // set to main cat id
                                        processedCategories.Add(currentID);
                                    }
                                }


                                if (tempProduct.DistributorInfo.pk_DistributorID.ToString() == currentID)
                                {
                                    tempGroup.Add(tempProduct);
                                    processed += 1;
                                }

                            }

                            groupHolder.Add(tempGroup);
                            currentID = "";  // reset
                        }

                        foreach (ArrayList tempArray in groupHolder)
                        {
                            vendorBody = GenerateVendorEmail(OUTPUT_FormValues, tempArray);

                            //    // ===============================
                            //    // == Distribution Vendor Email ==
                            //    // ===============================
                            errorMessage = SendEmail("saleslead@findbomag.com", venderEmail, "nwilson@nwilson.org; tim.tiemann@hotmail.com", vendorBody, "Sales Lead from FindBomag.com to Distribution Vendor", "findbomagco", "Distribution Vendor", ref myPage, vendorEmailSent);

                            //errorBuilder.Append("ArrayList tempArray in groupHolder");

                            if (errorMessage != "")
                                errorBuilder.Append(errorMessage + Environment.NewLine);

                            errorMessage = "";

                        }
                    }

                }
            }
            else
            {

                // Do not plan to buy is true, and no products selected

                DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();
                DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();

                etdt = etta.GetDataByEmailType("DoNotPlanToBuy");

                if (etdt != null && etdt.Rows.Count > 0)
                    leadUserEmail = etdt[0].emailAddress;

                string DoNotPlanToBuyEmailMessage = GenerateDoNotPlanToBuy(OUTPUT_FormValues);
                errorMessage = SendEmail("saleslead@findbomag.com", leadUserEmail, "", DoNotPlanToBuyEmailMessage, "BOMAG -Do Not Plan To Buy", "findbomagco", "User Email", ref myPage);

                if (errorMessage != "")
                    errorBuilder.Append(errorMessage + Environment.NewLine);

                errorMessage = "";

            }


            #region ThanksUrl
            SendSalesLead(errorBuilder, InDevMode);


            #endregion

        }

        private void ProcessDistributor(ref System.Text.StringBuilder errorBuilder, ref System.Web.UI.Page myPage, string TM_Fax_FormValues, ref ArrayList faxProducts, ref ArrayList terrManProducts,
                                                ref ArrayList userProducts, ref ArrayList vendorProducts, BR.Lead.DistributorCategoryRep tempCategory, DA.ContractTDS.DistributorRow leadDistributorInfo,
                                                    bool updateArrays, bool sendRentalAdministratorEmail = false, bool sendToManufacturerRep = false)
        {

            // get the territorymanager for this distributor
            int distId = leadDistributorInfo.pk_DistributorID;

            string distName = string.Empty;
            if (leadDistributorInfo.DistName != null)
                distName = leadDistributorInfo.DistName;



            int repID = DDA.DataAccess.Representative_da.GetActiveRepForDistributor(distId, "Territory");


            DataSet ds = new DataSet();
            string repName = "";
            string repEmail = "";
            string repFullEmail = "";
            string ccEmail2 = "";

            try
            {
                //  RepID, RepName, Address, fk_CityID AS [CityName], RepresentativeType.Description, 
                //  State.FullName, Country.CountryName, Phone, Fax, Email, fk_ZipID, MobilePhone 
                ds = DDA.DataAccess.Representative_da.GetRepresentativeInformation_NoCountryData(repID);

                repName = ds.Tables[0].Rows[0]["RepName"].ToString();
                repEmail = ds.Tables[0].Rows[0]["Email"].ToString();

            }
            catch
            {
                repName = "Rep Email Error";

                DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();
                DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();

                etdt = etta.GetDataByEmailType("DoNotPlanToBuy");

                if (etdt != null && etdt.Rows.Count > 0)
                    ccEmail2 = etdt[0].emailAddress;


                repEmail = "tim.tiemann@bomag.com";

            }

            //repFullEmail = "\"" + repName + "\" " + repEmail;
            repFullEmail = repEmail;

            if (InDevMode)
            {
            }


            foreach (BR.Lead.ProductList tempProduct in tempCategory.models)
            {

                // #################################
                // # Add product to sales lead
                // #################################

                int manufacturerDistId = -1;

                if (tempProduct.ManufacturerDistributorInfo != null && tempProduct.ManufacturerDistributorInfo.pk_DistributorID != null)
                {
                    manufacturerDistId = tempProduct.ManufacturerDistributorInfo.pk_DistributorID;
                }

                if (updateArrays)
                {
                    this.AddLeadProduct(Convert.ToInt32(tempProduct.productId), Convert.ToInt32(tempProduct.subCatID), Convert.ToInt32(tempProduct.modelID),
                                                        tempProduct.IsElectronic, tempProduct.IsMail, tempProduct.DistributorInfo.pk_DistributorID, repID, tempProduct.AllowTerritoryOverlap, manufacturerDistId);

                    // add each to a fax for the category
                    faxProducts.Add(tempProduct);
                    terrManProducts.Add(tempProduct);
                }

                // if "ELECTRONIC" then add to user email
                // Also, if "PlanToBuy" == false, then add all products for the user email
                if (tempProduct.IsElectronic == true || PlanToBuy == false)
                {
                    if (updateArrays)
                    {
                        userProducts.Add(tempProduct);
                    }
                }
                else
                {

                    if (InDevMode)
                    {
                        PendingLeadErrorMessage += "Model: " + tempProduct.model + "<BR>";
                        PendingLeadErrorMessage += "Product Electronic?: " + tempProduct.IsElectronic + "<BR>";
                        PendingLeadErrorMessage += "Product Mail?: " + tempProduct.IsMail + "<BR>";
                        PendingLeadErrorMessage += "Model ID: " + tempProduct.modelID + "<BR>";
                        PendingLeadErrorMessage += "PlanToBuy: " + PlanToBuy + "<BR>"; ;
                        PendingLeadErrorMessage += "-----------------------------------------------<BR>";
                    }

                }

                // if "BY MAIL" then add to distribution vendor email
                if (tempProduct.IsMail == true && updateArrays)
                    vendorProducts.Add(tempProduct);


            }


            if (PlanToBuy == true)
            {
                if (sendRentalAdministratorEmail)
                {
                    if (ccEmail2.Length > 0)
                    {
                        ccEmail2 += ";";
                    }
                    DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();
                    DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();

                    etdt = etta.GetDataByEmailType("RentalAdministrator");

                    ccEmail2 += etdt[0].emailAddress;
                }


                // for each model, send an email to the territory manager
                string repEmailMessage = GenerateRepEmail(TM_Fax_FormValues, terrManProducts, ds, leadDistributorInfo, distName);
                errorMessage += SendEmail("saleslead@findbomag.com", repFullEmail, ccEmail2, repEmailMessage, "Sales Lead from FindBomag.com", "findbomagco", "Territory Manager", ref myPage);

                if (errorMessage != "")
                    errorBuilder.Append(errorMessage + Environment.NewLine);

                errorMessage = "";
                
                // send fax
                string faxEmailMessage = GenerateFax(TM_Fax_FormValues, faxProducts, tempCategory, "", true, sendToManufacturerRep);
                // real code
                // our fax number: 8006086479@mhsfax.com
                // remember to remove the parantheses and ( )
                //leadDistributorInfo.Fax.ToString() + "@mhsfax.com"
                string faxNumber = "";

                if (InDevMode == true)
                {
                    //faxNumber = "8006086479@mhsfax.com";
                    faxNumber = "8006086479@metrofax.com";
                }
                else
                {
                    //faxNumber = leadDistributorInfo.Fax.ToString() + "@mhsfax.com";
                    faxNumber = leadDistributorInfo.Fax.ToString() + "@metrofax.com";
                }

                faxNumber = faxNumber.Replace("(", "");
                faxNumber = faxNumber.Replace(")", "");
                faxNumber = faxNumber.Replace(" ", "");
                faxNumber = faxNumber.Replace("-", "");

                // remove leading 1
                if (faxNumber.Substring(0, 1) == "1")
                    faxNumber = faxNumber.Remove(0, 1);

                errorMessage = SendEmail("fax@findbomag.com", faxNumber, "", faxEmailMessage, "Sales Lead from FindBomag.com", "findbomagco", "Distributor Fax", ref myPage);

                if (errorMessage != "")
                    errorBuilder.Append(errorMessage + Environment.NewLine);

                errorMessage = "";

            }
        }

        private void SendSalesLead(System.Text.StringBuilder errorBuilder, bool InDevMode)
        {
            bool ErrorOccurred = false;
            if (errorBuilder.ToString() != "")
            {
                ErrorOccurred = true;
            }

            DateTime sendDate = DateTime.Now.AddMonths(MonthsToAddToSendDate);

            bool submitted = false;

            if (MonthsToAddToSendDate == 0)
                submitted = true;

            this.AddLead(DateTime.Now, submitted, ErrorOccurred, errorBuilder.ToString(), sendDate);

            if (InDevMode == false)
            {
                this.CommitLeadToDatabase();
            }

        }

        public bool UpdateLeadValues()
        {
            bool errorOccurred = false;

            foreach (LeadValue leadValue in this._leadValues)
            {
                try
                {
                    leadValueTA.UpdateLeadValue(leadValue.elementValue, leadValue.pk_valueID, leadValue.elementName);
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                }
            }

            return errorOccurred;
        }

        public bool UpdateLeadStatus(bool HasBeenSubmitted)
        {
            return UpdateLeadStatus(HasBeenSubmitted, this.LeadID);

        }


        public bool UpdateLeadStatus(bool HasBeenSubmitted, int LeadIDToUpdate)
        {
            bool UpdateSuccessful = false;
            try
            {
                leadTA.UpdateSubmittedStatus(HasBeenSubmitted, LeadIDToUpdate);
                UpdateSuccessful = true;
            }
            catch (Exception ex)
            {
                UpdateSuccessful = false;
            }

            return UpdateSuccessful;
        }

        #region UserEmails


        private string IndentText(int NumberOfIndents)
        {

            string retVal = "";

            for (int i = 0; i < NumberOfIndents; i++)
            {
                retVal = retVal + tabCharacter;
            }

            return retVal;
        }



        private string FormatProductPDFLinks(ArrayList products, bool IncludeDistributor, bool IsUserEmail, bool DisablePDFLinks)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string currentMainCategory = "";
            string currentSubCategory = "";


            if (IsUserEmail == false)
            {
                #region IsUserEmail = false
                foreach (BR.Lead.ProductList tempProduct in products)
                {

                    if (tempProduct.productId != currentMainCategory)
                    {
                        currentMainCategory = tempProduct.productId;

                        if (sb.ToString() != "")
                        {
                            if (IncludeDistributor)
                            {
                                sb.AppendLine("");

                                // if user email, put this before the distributor info
                                if (IsUserEmail)
                                    sb.AppendLine("BOMAG AMERICAS DISTRIBUTOR:");

                                if (InDevMode == true)
                                    sb.AppendLine(tempProduct.DistributorInfo.DistName + "<BR>");

                                string DistributorInfo = FormatDistributorInformation(tempProduct.DistributorInfo);

                                if (DistributorInfo.Contains("ERROR:") == true)
                                    errorMessage = errorMessage + DistributorInfo;

                                sb.Append(DistributorInfo);
                            }

                            sb.AppendLine(Environment.NewLine + tempProduct.product);
                        }
                        else
                        {
                            sb.AppendLine(Environment.NewLine + tempProduct.product);
                        }

                    }

                    if (currentSubCategory == tempProduct.subCat)
                    {
                        sb.Append(IndentText(2) + "- " + tempProduct.model);

                        if (DisablePDFLinks == false)
                        {
                            sb.Append(" - ");

                            //sb.AppendLine("<a href=\"" + tempProduct.modelURL + "\">" + tempProduct.modelURL + "</a>");

                            if (tempProduct.modelURL != "")
                                sb.AppendLine(tempProduct.modelURL);
                            else
                                sb.AppendLine("No PDF Link");
                        }

                    }
                    else
                    {

                        currentSubCategory = tempProduct.subCat;

                        if (currentSubCategory != "")
                            sb.AppendLine(IndentText(1) + "- " + tempProduct.subCat);
                        else
                            sb.AppendLine("");  // blank line

                        sb.Append(IndentText(2) + "- " + tempProduct.model);

                        if (DisablePDFLinks == false)
                        {
                            sb.Append(" - ");

                            if (tempProduct.modelURL != "")
                                sb.AppendLine(tempProduct.modelURL);
                            else
                                sb.AppendLine("No PDF Link");
                        }
                    }


                }

                #endregion

            }
            else
            {

                #region IsUserEmail = true
                if (InDevMode == true)
                    sb.AppendLine("Products Count: " + products.Count + "<BR>");

                for (int i = 0; i < products.Count; i++)
                {

                    try
                    {

                        // may have been causing double "BOMAG AMERICAS DISTRIBUTOR:" to show up
                        //// if user email, put this before the distributor info
                        //if (IsUserEmail)
                        //    sb.AppendLine("BOMAG AMERICAS DISTRIBUTOR:");

                        if (InDevMode == true)
                            sb.AppendLine("1) IN TRY<BR>");

                        //foreach (BR.Lead.ProductList tempProduct in products)
                        //{
                        BR.Lead.ProductList tempProduct = ((BR.Lead.ProductList)products[i]);

                        if (tempProduct.productId != currentMainCategory)
                        {
                            if (InDevMode == true)
                                sb.AppendLine("2) tempProduct.productId != currentMainCategory<BR>");

                            currentMainCategory = tempProduct.productId;

                            if (sb.ToString() != "" || i == 0)
                            {

                                if (InDevMode == true)
                                    sb.AppendLine("3) sb is not empty or i == 0" + " i:" + i + "  SB: " + sb.ToString() + "<BR>");

                                if (IncludeDistributor)
                                {

                                    if (InDevMode == true)
                                        sb.AppendLine("IncludeDistributor: " + IncludeDistributor + "<BR>");

                                    if (i > 0)
                                    {
                                        sb.AppendLine("");
                                        sb.AppendLine("-------------------------------------------------------------");
                                        sb.AppendLine("");

                                    }
                                    // if user email, put this before the distributor info
                                    if (IsUserEmail)
                                        sb.AppendLine("BOMAG AMERICAS DISTRIBUTOR:");

                                    string DistributorInfo = FormatDistributorInformation(tempProduct.DistributorInfo);

                                    if (DistributorInfo.Contains("ERROR:") == true)
                                        errorMessage = errorMessage + DistributorInfo;

                                    sb.Append(DistributorInfo);

                                }

                                sb.AppendLine(Environment.NewLine + tempProduct.product);
                            }
                            else
                            {
                                sb.AppendLine(tempProduct.product);
                            }

                        }

                        if (currentSubCategory == tempProduct.subCat)
                        {
                            sb.Append(IndentText(2) + "- " + tempProduct.model + " - ");

                            if (tempProduct.modelURL != "")
                                sb.AppendLine("<a href=\"" + tempProduct.modelURL + "\">" + tempProduct.modelURL + "</a>");
                            else
                                sb.AppendLine("No PDF Link");


                        }
                        else
                        {

                            currentSubCategory = tempProduct.subCat;

                            if (currentSubCategory != "")
                                sb.AppendLine(IndentText(1) + "- " + tempProduct.subCat);
                            else
                                sb.AppendLine("");  // blank line

                            sb.Append(IndentText(2) + "- " + tempProduct.model + " - ");

                            if (tempProduct.modelURL != "")
                                sb.AppendLine("<a href=\"" + tempProduct.modelURL + "\">" + tempProduct.modelURL + "</a>");
                            else
                                sb.AppendLine("No PDF Link");

                        }
                    }
                    catch (Exception ex)
                    {
                        if (InDevMode == true)
                            sb.AppendLine("ERROR: " + ex.Message);
                    }
                }

                #endregion

            }

            //// append the last distributor's info if needed
            //if (IncludeDistributor)
            //{
            //    sb.AppendLine("");

            //    // if user email, put this before the distributor info
            //    if (IsUserEmail)
            //        sb.AppendLine("BOMAG AMERICAS DISTRIBUTOR:");

            //    sb.Append(FormatDistributorInformation(((BR.Lead.ProductList)(products[products.Count - 1])).DistributorInfo));
            //}


            return sb.ToString();

        }



        #region Territory Rep Email

        //        To: tm@bomag.com
        //From: saleslead@findbomag.com
        //Subject: Sales Lead from FindBomag.com

        //Attention: Sales Manager - Anderson Equipment <----------------------------------(Neil add distributor name)

        //The following lead was submitted at: 3/23/2007 - 9:01 AM

        //Name: Tim Tiemann
        //Company: TEST
        //Address: 2000 Kentville Road
        //City: Kewanee
        //State: IL
        //Zip: 61443
        //Country: USA
        //Phone: 309-852-6216
        //Fax: 309-852-6216
        //Email: tim.tiemann@bomag.com

        //source_css: Better Roads <----------------------------------(Neil do not need this)

        //PlanToBuy: 1   <---------------------------------- Neil this should be "Immediately" or what ever is selected

        //Comments: TESTING SYSTEM

        //The user requested information on the following product model(s)

        //BOMAG HEAVY COMPACTION
        // - Static Rollers
        //   BW9ASW - [PDF File]  <------ Neil this is the link

        //Please contact the distributor below to verify the follow up with this lead.

        //Anderson Equipment
        //2313 What Ever Drive
        //Kewanee, IL 61443

        //Phone: 309-852-7895
        //Fax: 309-852-8594

        private string GenerateRepEmail(string formValues, ArrayList products, DataSet rep, DA.ContractTDS.DistributorRow distRow, string repDistributorName)
        {
            //string returnVal = "";

            //returnVal = rep.Tables[0].Rows[0]["RepName"].ToString() + ", " + Environment.NewLine + Environment.NewLine;
            //returnVal += "The following form information was submitted on " + DateTime.Now.ToLongDateString() + Environment.NewLine + Environment.NewLine;
            //returnVal += formValues + Environment.NewLine + Environment.NewLine + "Product Submitted: " + Environment.NewLine;


            //foreach (string tempProduct in products)
            //    returnVal += tempProduct + Environment.NewLine;

            //return returnVal;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //sb.AppendLine("Attention: " + repDistributorName);
            //sb.AppendLine("");
            //sb.AppendLine("The following lead was submitted at: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
            //sb.AppendLine("");
            sb.AppendLine("Source: " + leadSource);
            sb.AppendLine("");
            sb.AppendLine(formValues);

            sb.AppendLine("The user requested information on the following product model(s)");
            sb.AppendLine("");

            //foreach (string tempProduct in products)
            //{
            //    sb.AppendLine(tempProduct);
            //    sb.AppendLine("");
            //}


            sb.Append(FormatProductPDFLinks(products, false, false, false));

            sb.AppendLine("");
            sb.AppendLine("Please contact the distributor below to verify the follow up of this lead.");
            sb.AppendLine("");
            sb.AppendLine(FormatDistributorInformation(distRow));


            return sb.ToString();

        }


        #endregion


        #region private string GenerateDoNotPlanToBuy()   // Distributor Fax


        //To: (815) 485-5773@findbomag.com
        //From: saleslead@findbomag.com
        //Subject: Sales Lead from FindBomag.com

        //Attention: Anderson Equipment <--------------------(Neil add distributor name)

        //The following lead was submitted at: 3/23/2007 - 9:01 AM

        //Name: Tim Tiemann
        //Company: TEST
        //Address: 2000 Kentville Road
        //City: Kewanee
        //State: IL
        //Zip: 61443
        //Country: USA
        //Phone: 309-852-6216
        //Fax: 309-852-6216
        //Email: tim.tiemann@bomag.com

        //source_css: Better Roads <------------------------------(Neil do not need this)

        //PlanToBuy: 1   <----------------------------(Neil this should be "Immediately")

        //Comments: TESTING SYSTEM

        //The user requested information on the following product model(s)

        //BOMAG HEAVY COMPACTION
        // - Static Rollers
        //   - BW9ASW - http://www.bomag.com/pdf/bw9asw.pdf

        private string GenerateDoNotPlanToBuy(string formValues)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("=================================");
            sb.AppendLine("    BOMAG AMERICAS SALES LEAD");
            sb.AppendLine("     --DO NOT PLAN TO BUY--");
            sb.AppendLine("=================================");
            sb.AppendLine("");

            string finalName = "";

            sb.AppendLine("");
            sb.AppendLine("The following lead was submitted on: " + DateTime.Now.ToShortDateString()); // + " - " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("");
            sb.AppendLine(formValues);

            sb.AppendLine("The user did not request information on any product models.");


            return sb.ToString();

            //string returnVal = "";

            //returnVal = DistributorForCategory.DistributorInfo.DistName + ", " + Environment.NewLine + Environment.NewLine;
            //returnVal += "The following form information was submitted on " + DateTime.Now.ToLongDateString() + Environment.NewLine + Environment.NewLine;
            //returnVal += formValues + Environment.NewLine + Environment.NewLine + "Products Submitted: " + Environment.NewLine;

            //foreach (string tempProduct in products)
            //    returnVal += tempProduct + Environment.NewLine;

            //return returnVal;
        }



        #endregion



        #region private string GenerateFax()   // Distributor Fax


        //To: (815) 485-5773@findbomag.com
        //From: saleslead@findbomag.com
        //Subject: Sales Lead from FindBomag.com

        //Attention: Anderson Equipment <--------------------(Neil add distributor name)

        //The following lead was submitted at: 3/23/2007 - 9:01 AM

        //Name: Tim Tiemann
        //Company: TEST
        //Address: 2000 Kentville Road
        //City: Kewanee
        //State: IL
        //Zip: 61443
        //Country: USA
        //Phone: 309-852-6216
        //Fax: 309-852-6216
        //Email: tim.tiemann@bomag.com

        //source_css: Better Roads <------------------------------(Neil do not need this)

        //PlanToBuy: 1   <----------------------------(Neil this should be "Immediately")

        //Comments: TESTING SYSTEM

        //The user requested information on the following product model(s)

        //BOMAG HEAVY COMPACTION
        // - Static Rollers
        //   - BW9ASW - http://www.bomag.com/pdf/bw9asw.pdf

        private string GenerateFax(string formValues, ArrayList products, BR.Lead.DistributorCategoryRep DistributorForCategory, string AlternateName, bool DisablePDFLinks, bool sendToManufacturerRep = false)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("=================================");
            sb.AppendLine("    BOMAG AMERICAS SALES LEAD");
            sb.AppendLine("=================================");
            sb.AppendLine("");

            string finalName = "";

            if (AlternateName == "" && DistributorForCategory.DistributorInfo != null)
            {
                finalName = DistributorForCategory.DistributorInfo.DistName;
            }
            else if (DistributorForCategory.ManufacturerDistributorInfo != null)
            {
                finalName = DistributorForCategory.ManufacturerDistributorInfo.DistName;
            }
            else
            {
                finalName = AlternateName;
            }

            if (DistributorForCategory.ManufacturerDistributorInfo != null && sendToManufacturerRep == true)
            {
                finalName = DistributorForCategory.ManufacturerDistributorInfo.DistName;
            }

            sb.AppendLine("Attention: " + finalName);
            sb.AppendLine("");
            sb.AppendLine("The following lead was submitted on: " + DateTime.Now.ToShortDateString()); // + " - " + DateTime.Now.ToShortTimeString());
            sb.AppendLine("Source: " + leadSource);
            sb.AppendLine("");
            sb.AppendLine(formValues);
            sb.AppendLine("The user requested information on the following product model(s)");
            sb.AppendLine("");

            //foreach (string tempProduct in products)
            //{
            //    sb.AppendLine(tempProduct);
            //    sb.AppendLine();
            //}
            sb.Append(FormatProductPDFLinks(products, false, false, DisablePDFLinks));
            sb.AppendLine();

            return sb.ToString();

            //string returnVal = "";

            //returnVal = DistributorForCategory.DistributorInfo.DistName + ", " + Environment.NewLine + Environment.NewLine;
            //returnVal += "The following form information was submitted on " + DateTime.Now.ToLongDateString() + Environment.NewLine + Environment.NewLine;
            //returnVal += formValues + Environment.NewLine + Environment.NewLine + "Products Submitted: " + Environment.NewLine;

            //foreach (string tempProduct in products)
            //    returnVal += tempProduct + Environment.NewLine;

            //return returnVal;
        }



        #endregion

        #region private string GenerateUserEmail()

        //To: tim.tiemann@bomag.com
        //From: saleslead@findbomag.com
        //Subject: Product Information from FindBomag.com

        //Dear Tim Tiemann,

        //On Tuesday, March 27, 2007, you requested information on the following BOMAG Americas, Inc. product model(s). 

        //BOMAG HEAVY COMPACTION
        // - Static Rollers
        //   - BW9ASW - [PDF File]  <------ Neil this is the link

        //If you need further assistance please contact the distributor below.

        //BOMAG AMERICAS DISTRIBUTOR:
        //Anderson Equipment
        //2313 What Ever Drive
        //Kewanee, IL 61443

        //Phone: 309-852-7895
        //Fax: 309-852-8594
        private string GenerateUserEmail(string formValues, ArrayList products, string UserName)
        {


            #region ReOrganize SubCategories

            // determine which products will get lumped into which subcategories...rearrange subcategories so they are grouped...
            // the next step after this then reorganizes the subcategories into their appropriate main...
            int processed = 0;

            ArrayList groupHolder = new ArrayList();
            ArrayList processedCategories = new ArrayList();
            string currentID = "";

            while (processed < products.Count)
            {
                ArrayList tempGroup = new ArrayList();
                foreach (BR.Lead.ProductList tempProduct in products)
                {
                    if (currentID == "")
                    {
                        if (processedCategories.Count > 0)
                        {
                            bool IsFound = false;
                            foreach (string tempID in processedCategories)
                            {
                                if (tempID == (tempProduct.subCat + tempProduct.productId))
                                    IsFound = true;
                            }

                            if (IsFound == false)
                            {
                                currentID = (tempProduct.subCat + tempProduct.productId);  // set to subcat

                                processedCategories.Add(currentID);
                            }
                            //for (int j = 0; j < processedCategories.Count; j++)
                            //{
                            //    if (processedCategories[j].ToString() != tempProduct.subCat)
                            //    {
                            //        currentID = tempProduct.subCat;  // set to subcat
                            //        j = processedCategories.Count + 2; // kick out of the loop
                            //        processedCategories.Add(currentID);
                            //    }
                            //}
                        }
                        else
                        {
                            currentID = (tempProduct.subCat + tempProduct.productId);  // set to main cat id
                            processedCategories.Add(currentID);
                        }
                    }


                    if ((tempProduct.subCat + tempProduct.productId) == currentID)
                    {
                        tempGroup.Add(tempProduct);
                        processed += 1;
                    }

                }

                groupHolder.Add(tempGroup);
                currentID = "";  // reset
            }

            ArrayList finalArray = new ArrayList();

            // combine arrays together so one large product array, with grouped subcats
            foreach (ArrayList tempArray in groupHolder)
            {
                foreach (BR.Lead.ProductList product in tempArray)
                {
                    finalArray.Add(product);
                }
            }


            #endregion





            System.Text.StringBuilder returnVal = new System.Text.StringBuilder();

            returnVal.AppendLine("Dear " + UserName + ",");
            returnVal.AppendLine("");
            returnVal.AppendLine("On " + DateTime.Now.DayOfWeek + ", " + DateTime.Now.ToShortDateString() + ", you requested information on the following BOMAG Americas, Inc. product model(s).");

            //foreach (string tempProduct in products)
            //{
            //    returnVal.AppendLine(tempProduct);

            //    returnVal.AppendLine();
            //}

            returnVal.AppendLine("If you need further assistance please contact the distributor below.");
            returnVal.AppendLine();

            try
            {
                string pdfLinks = FormatProductPDFLinks(finalArray, true, true, false);
                returnVal.Append(pdfLinks);

                if (InDevMode)
                    returnVal.Append("RETURN VALUE: '" + pdfLinks + "'<BR>" + PendingLeadErrorMessage);
            }
            catch (Exception ex)
            {

                if (InDevMode)
                {
                    foreach (BR.Lead.ProductList product in finalArray)
                    {
                        returnVal.AppendLine(product.productId + "<BR>");
                    }

                    returnVal.AppendLine("ERROR: " + ex.Message + "<BR>");

                }

            }
            returnVal.AppendLine();


            // distributor information
            //returnVal.AppendLine(FormatDistributorInformation());


            //returnVal = UserName + ", " + Environment.NewLine + Environment.NewLine;
            //returnVal += "The following form information was submitted on " + DateTime.Now.ToLongDateString() + Environment.NewLine + Environment.NewLine;
            //returnVal += formValues + Environment.NewLine + Environment.NewLine + "Products Submitted: " + Environment.NewLine;



            return returnVal.ToString();
        }

        #endregion

        #region private string GenerateVendorEmail()

        private string GenerateVendorEmail(string formValues, ArrayList products)
        {
            string returnVal = "";

            string vendorBody;

            DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();
            DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();

            etdt = etta.GetDataByEmailType("vendorEmail");

            vendorBody = etdt[0].emailText;

            //vendorBody = vendorBody.Replace("\n", "<BR>");

            // create a list of the products
            string categoryNames = "";

            int counter = 1;

            ArrayList usedProducts = new ArrayList();

            if (products == null)
                products = new ArrayList();

            if (products.Count == 1)
            {
                categoryNames = ((BR.Lead.ProductList)products[0]).product;

            }
            else if (products.Count > 1)
            {
                foreach (BR.Lead.ProductList tempProducts in products)
                {
                    bool HasBeenUsed = false;

                    foreach (BR.Lead.ProductList tempUsedProduct in usedProducts)
                        if (tempUsedProduct.productId == tempProducts.productId)
                            HasBeenUsed = true;

                    if (HasBeenUsed == false)
                    {
                        usedProducts.Add(tempProducts);
                    }
                }


                foreach (BR.Lead.ProductList tempUsedProduct in usedProducts)
                {
                    if (categoryNames != "" && counter != usedProducts.Count)
                        categoryNames += ", ";

                    if (counter == usedProducts.Count && counter > 1)
                        categoryNames += " and ";

                    categoryNames += tempUsedProduct.product;

                    counter += 1;
                }

            }


            // Variable replacement for vendorbody
            string leadFullAddress = leadAddress1;
            if (leadAddress2 != "")
                leadFullAddress += Environment.NewLine + leadAddress2;

            vendorBody = vendorBody.Replace("{LEAD FIRST NAME}", leadFirstName);

            vendorBody = vendorBody.Replace("{LEAD LAST NAME}", leadLastName);
            vendorBody = vendorBody.Replace("{LEAD COMPANY}", leadCompany);
            vendorBody = vendorBody.Replace("(LEAD ADDRESS}", leadFullAddress);
            vendorBody = vendorBody.Replace("{LEAD CITY}", leadCity);
            vendorBody = vendorBody.Replace("{LEAD STATE}", leadState);
            vendorBody = vendorBody.Replace("{LEAD ZIP}", leadZip);

            vendorBody = vendorBody.Replace("{PRODUCT CATEGORY}", categoryNames);

            vendorBody = vendorBody.Replace("{SOURCE}", leadSource);
            vendorBody = vendorBody.Replace("{COMMENTS}", leadComments);



            if (products.Count > 0)
            {
                string stateAbbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(((BR.Lead.ProductList)(products[0])).DistributorInfo.fk_StateID);

                vendorBody = vendorBody.Replace("{DISTRIBUTOR COMPANY}", ((BR.Lead.ProductList)(products[0])).DistributorInfo.DistName);
                vendorBody = vendorBody.Replace("{DISTRIBUTOR ADDRESS}", ((BR.Lead.ProductList)(products[0])).DistributorInfo.ShippingAddress);
                vendorBody = vendorBody.Replace("{DISTRIBUTOR CITY}", ((BR.Lead.ProductList)(products[0])).DistributorInfo.CityName);
                vendorBody = vendorBody.Replace("{DISTRIBUTOR STATE} ", stateAbbreviation);
                vendorBody = vendorBody.Replace("{DISTRIBUTOR ZIP}", ((BR.Lead.ProductList)(products[0])).DistributorInfo.fk_ZipID);
                vendorBody = vendorBody.Replace("{DISTRIBUTOR PHONE}", ((BR.Lead.ProductList)(products[0])).DistributorInfo.Phone);

                returnVal += Environment.NewLine + Environment.NewLine + "Products Submitted: " + Environment.NewLine;

                //foreach (string tempProduct in products)
                //    returnVal += tempProduct + Environment.NewLine + Environment.NewLine;
                returnVal += (FormatProductPDFLinks(products, false, false, false));
            }


            returnVal += vendorBody;

            returnVal += Environment.NewLine;

            return returnVal;
        }

        #endregion

        #region private string CreateProductModelPDFLink()

        private string CreateProductModelPDFLink(BR.Lead.ProductList tempProduct)
        {

            string returnVal = "";

            if (tempProduct.product != "")
                returnVal = returnVal + tempProduct.product + Environment.NewLine;

            if (tempProduct.subCat != "")
                returnVal = returnVal + tabCharacter + "- " + tempProduct.subCat + Environment.NewLine;

            returnVal = returnVal + tabCharacter + tabCharacter + "- " + tempProduct.model;

            if (tempProduct.modelURL != "")

                returnVal = returnVal + " - <a href=\"" + tempProduct.modelURL + "\">" + tempProduct.modelURL + "</a>";
            else
                returnVal = returnVal + " - No PDF Available";

            return returnVal;
        }

        #region private string FormatDistributorInformation()

        //Anderson Equipment
        //2313 What Ever Drive
        //Kewanee, IL 61443

        //Phone: 309-852-7895
        //Fax: 309-852-8594

        private string FormatDistributorInformation(DA.ContractTDS.DistributorRow distRow)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {

                string abbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(distRow.fk_StateID);

                sb.AppendLine(distRow.DistName);
                sb.AppendLine(distRow.ShippingAddress);
                sb.AppendLine(distRow.CityName + ", " + abbreviation + " " + distRow.fk_ZipID);
                sb.AppendLine("");

                if (distRow.Phone != "")
                {
                    string phone = distRow.Phone;
                    string phone2 = phone.Substring(phone.IndexOf("Ext"));
                    phone2 = phone2.Replace("Ext: ", "");
                    if (phone2 == "")
                    {
                        phone = phone.Substring(0, phone.IndexOf("Ext") - 1);
                    }

                    sb.AppendLine("Phone: " + phone);

                }

                if (distRow.Fax != "")
                    sb.AppendLine("Fax: " + distRow.Fax);

            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }

            return sb.ToString();
        }

        #endregion



        //private string CreateProductModelPDFLink(BR.Lead.ProductList tempProduct, string productModels)
        //{

        //    // ===============================
        //    // == User Model PDF Link Email ==
        //    // ===============================
        //    if (productModels != "")
        //        productModels = productModels + Environment.NewLine;

        //    if (tempProduct.product != "")
        //        productModels = productModels + "[" + tempProduct.product + "] ";

        //    if (tempProduct.subCat != "")
        //        productModels = productModels + "[" + tempProduct.subCat + "] ";

        //        productModels = productModels + tempProduct.model;

        //    if (tempProduct.modelURL != "")

        //        productModels = productModels + " - <a href=\"" + tempProduct.modelURL + "\">Model PDF Link</a>";
        //    else
        //        productModels = productModels + " - No PDF Available";

        //    return productModels;
        //}

        #endregion

        #endregion




        #endregion


    }
}
