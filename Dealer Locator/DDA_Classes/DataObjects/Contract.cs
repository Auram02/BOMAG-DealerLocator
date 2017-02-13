using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;


namespace Dealer_Locator.DataObjects
{
    class Contract
    {

        #region Private Variables

        private int _contractID;
        private String _contractNumber;
        private string _contractDate;
        private string _modifyDate;
        private string _stateName;
        private int _stateID;
        private string _contractMode;

        private ArrayList _countyID;
        private ArrayList _categoryID;
        private ArrayList _BranchID;
        private ArrayList _deleteCounties;
        private ArrayList _deleteBranches;

        public ArrayList _SalesLocation;
        public ArrayList _Category;
        public ArrayList _State;
        public ArrayList _Counties;

        #endregion

        #region Constructors

        
        public Contract()
        {
            _countyID = new ArrayList();
            _categoryID = new ArrayList();
            _BranchID = new ArrayList();
            _deleteCounties = new ArrayList();
            _deleteBranches = new ArrayList();

            _SalesLocation = new ArrayList();
            _Category = new ArrayList();
            _Counties = new ArrayList();
            _State = new ArrayList();
        }

        public Contract(int p_ContractID)
        {
            _countyID = new ArrayList();
            _categoryID = new ArrayList();
            _BranchID = new ArrayList();
            _deleteCounties = new ArrayList();
            _deleteBranches = new ArrayList();

            _SalesLocation = new ArrayList();
            _Category = new ArrayList();
            _Counties = new ArrayList();
            _State = new ArrayList();
        }

        public void InitializeArrays()
        {
            _countyID = new ArrayList();
            _categoryID = new ArrayList();
            _BranchID = new ArrayList();
            _deleteCounties = new ArrayList();
            _deleteBranches = new ArrayList();

            _SalesLocation = new ArrayList();
            _Category = new ArrayList();
            _Counties = new ArrayList();
            _State = new ArrayList();
        }

#endregion

        #region Accessors

        public String ContractNumber
        {
            get { return _contractNumber; }
            set { _contractNumber = value; }
        }

        public string ContractMode
        {
            get { return _contractMode; }
            set { _contractMode = value; }
        }

        public int ContractID
        {
            get { return _contractID; }
            set { _contractID = value; }
        }

        public string ContractDate
        {
            get { return _contractDate; }
            set { _contractDate = value; }
        }

        public string ModifyDate
        {
            get { return _modifyDate; }
            set { _modifyDate = value; }
        }

        public string StateName
        {
            get { return _stateName; }
            set
            {
                _stateName = value;
                _stateID = Dealer_Locator.DDA.DataAccess.Location_da.GetStateID(value);
            }

        }

        public int StateID
        {
            get { return _stateID; }

        }

        public ArrayList DeleteCounties
        {
            get { return _deleteCounties; }
        }

        public ArrayList DeleteBranches
        {
            get { return _deleteBranches; }
        }

        public ArrayList Categories
        {
            get { return _categoryID; }
        }

        public ArrayList Counties
        {
            get { return _countyID; }
        }

        public ArrayList Branches
        {
            get { return _BranchID; }
        }

        #endregion

        #region Public Methods

        public void ClearContractData()
        {
            this._contractID = 0;
            this._contractNumber = "";
            this._contractDate = Convert.ToString(System.DateTime.Now.ToShortDateString());
            this._modifyDate = Convert.ToString(System.DateTime.Now.ToShortDateString());
            this._stateID = 0;
            this._stateName = "";
            this._countyID.Clear();
            this._categoryID.Clear();
            this._deleteCounties.Clear();
            this._deleteBranches.Clear();
            this._BranchID.Clear();
        
        }

        private void ClearIt()
        {
            this._SalesLocation.Clear();
            this._Category.Clear();
            this._Counties.Clear();
            this._State.Clear();
        }

        public void ClearReportData()
        {
            this.ClearIt();

        }

        public bool CheckIfCountyExists(int p_id)
        {
            int i;
            bool result;
            result = false;

            for (i = 0; i < this._countyID.Count; i++)
            {
                if (p_id == Convert.ToInt32(this._countyID[i].ToString()))
                {
                    result = true;
                    break;
                }
            }

            if (result == false)
            {
                p_id = p_id;
            }

            return result;
        }

        public string CreateNewContractNumber(int p_distID)
        {
            string newCNum;

            string distName;
            string date;
            int randomNum;

            DataSet ds = new DataSet();
            ds = Dealer_Locator.DDA.DataAccess.Distributor_da.GetDistributorInformation(p_distID);
            distName = ds.Tables[0].Rows[0]["DistName"].ToString();

            distName = distName.Substring(0, 3);  // substring of dist name
            distName = distName.ToUpper();

            date = this._contractDate.ToString();

            Random random = new Random();
            randomNum = random.Next(100, 999);

            newCNum = distName + date.Replace("/","") + "-" + randomNum;

            this._contractNumber = newCNum;
            
            return newCNum;

            
        }

        public void AddCategory(string p_CategoryName)
        {
            int p_CategoryID;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_CategoryName);

            p_CategoryID = Dealer_Locator.DDA.DataAccess.Category_da.GetCategoryID(p_CategoryName);
            
            this._categoryID.Add(p_CategoryID);
        }


        public void AddDeleteCounty(string p_CountyName, int p_stateID)
        {
            int p_cID;
            p_cID = Dealer_Locator.DDA.DataAccess.Location_da.GetCountyID(p_CountyName, p_stateID);

            this._deleteCounties.Add(p_cID);
        }

        public void AddDeleteBranch(int p_branchID)
        {
            this._deleteBranches.Add(p_branchID);
        }

        public void RemoveCategory(string p_CategoryName)
        {
            int i;

            int p_CategoryID;
            p_CategoryID = Dealer_Locator.DDA.DataAccess.Category_da.GetCategoryID(p_CategoryName);


            for (i = 0; i < this._categoryID.Count; i++)
            {
                if (p_CategoryID == Convert.ToInt32(this._categoryID[i]))
                {
                    this._categoryID.RemoveAt(i);
                }
            }
        }

        public void ClearCategories()
        {
            this._categoryID.Clear();
        }

        public void ClearCounties()
        {
            this._countyID.Clear();
        }

        public void ClearDeleteCounties()
        {
            this._deleteCounties.Clear();
        }

        public void ClearDeleteBranches()
        {
            this._deleteBranches.Clear();
        }

        /// <summary>
        /// Adds a single county
        /// </summary>
        /// <param name="p_CountyID"></param>
        public void AddCounty(int p_CountyID)
        {
            this._countyID.Add(p_CountyID);
        }

        /// <summary>
        /// Removes a single county
        /// </summary>
        /// <param name="p_CountyID"></param>
        public void RemoveCounty(int p_CountyID)
        {
            int i;

            for (i = 0; i < this._countyID.Count; i++)
            {
                if(p_CountyID == Convert.ToInt32(this._countyID[i]))
                    this._countyID.RemoveAt(i);
            }
        }

        public void ClearCounties(int p_ContractID)
        {
            // clear the counties

        }

        public void AddBranch(int p_ID)
        {
            this._BranchID.Add(p_ID);
        }


        /// <summary>
        /// Removes a single branch
        /// </summary>
        /// <param name="p_id"></param>
        public void RemoveBranch(int p_id)
        {
            int i;

            for (i = 0; i < this._BranchID.Count; i++)
            {
                if (p_id == Convert.ToInt32(this._BranchID[i]))
                    this._BranchID.RemoveAt(i);
            }
        }

        public void ClearBranches()
        {
            this._BranchID.Clear();
        }

        #endregion

    }
}
