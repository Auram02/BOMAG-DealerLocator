using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.DA
{
    public class CSV
    {
        System.IO.Stream _strm;
        DataTable m_dtCSV = new DataTable();
        int m_iColumnCount = 0;

        public CSV(string FilePath)
        {
            System.IO.StreamReader tempStrm = new System.IO.StreamReader(FilePath);
            _strm = tempStrm.BaseStream;
        }

        public CSV(System.IO.Stream strm)
        {
            _strm = strm;
        }

        public DataTable PopulateDataTableFromUploadedFile()
        {
            System.IO.StreamReader srdr = new System.IO.StreamReader(_strm);
            String strLine = String.Empty;
            Int32 iLineCount = 0;
            do
            {
                strLine = srdr.ReadLine();
                if (strLine == null)
                {
                    break;
                }
                if (0 == iLineCount++)
                {
                    m_dtCSV = this.CreateDataTableForCSVData(strLine);
                }
                this.AddDataRowToTable(strLine, m_dtCSV);
            } while (true);

            return m_dtCSV;
        }

        private DataTable CreateDataTableForCSVData(String strLine)
        {
            DataTable dt = new DataTable("CSVTable");
            String[] strVals = strLine.Split(new char[] { ',' });
            m_iColumnCount = strVals.Length;
            int idx = 0;
            foreach (String strVal in strVals)
            {
                String strColumnName = String.Format("Column-{0}", idx++);
                dt.Columns.Add(strColumnName, Type.GetType("System.String"));
            }
            return dt;
        }

        private DataRow AddDataRowToTable(String strCSVLine, DataTable dt)
        {
            String[] strVals = strCSVLine.Split(new char[] { ',' });
            Int32 iTotalNumberOfValues = strVals.Length;
            // If number of values in this line are more than the columns
            // currently in table, then we need to add more columns to table.
            if (iTotalNumberOfValues > m_iColumnCount)
            {
                Int32 iDiff = iTotalNumberOfValues - m_iColumnCount;
                for (Int32 i = 0; i < iDiff; i++)
                {
                    String strColumnName = String.Format("Column-{0}", (m_iColumnCount + i));
                    dt.Columns.Add(strColumnName, Type.GetType("System.String"));
                }
                m_iColumnCount = iTotalNumberOfValues;
            }
            int idx = 0;
            DataRow drow = dt.NewRow();
            foreach (String strVal in strVals)
            {
                String strColumnName = String.Format("Column-{0}", idx++);
                drow[strColumnName] = strVal.Trim();
            }
            dt.Rows.Add(drow);
            return drow;
        }                     
    }
}
