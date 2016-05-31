using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class BudgetEstimation : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.Budget;

            CurrenceID.Visible = false;

            if (Session["userName"] != null)
            {
                //Retrieving UserName from Session
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
                string projectID = Session["PROJECT_ID"].ToString();
            }

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Budget Estimation";

                ViewState["BudgetEstimationID"] = 0;
                HiddenFieldTotalValue.Value = "0";
                BindEstCategories();

                screenintiallization();
                getToatlBudgetValue();
                ValuePerTextBox.Attributes.Add("readonly", "readonly");
                ValueTextBox.Attributes.Add("onchange","setDirty();");
                ValuePerTextBox.Attributes.Add("onchange", "setDirty();");
                CurrenceID.Attributes.Add("onchange", "setDirty();");
                SaveButton.Attributes.Add("onclick", "isDirty = 0;");
                ClearButton.Attributes.Add("onclick", "isDirty = 0;");


               // LoadCurrencyEstimate();
                //SaveButton.Attributes.Add("onclick", "return CalculateBGTPRECTFinal();");
                // ValueTextBox.Attributes.Add("onchange", "CalculateBGTPRECT();");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    foreach (RepeaterItem item in rptBudgetEstCategory.Items)
                    {
                        GridView grdBudgetEstimation = item.FindControl("grdBudgetEstimation") as GridView;
                        if (grdBudgetEstimation != null)
                        {
                            grdBudgetEstimation.Columns[grdBudgetEstimation.Columns.Count - 1].Visible = false;
                            grdBudgetEstimation.Columns[grdBudgetEstimation.Columns.Count - 2].Visible = false;
                        }
                    }
                    plsedit1.Visible = false;
                    plsedit.Visible = false;
                    btnAddNewCategory.Visible = false;
                    btnAddNewSubCategory.Visible = false;
                }
            }
        }
        /// <summary>
        /// To bind data
        /// </summary>
        private void BindEstCategories()
        {
            rptBudgetEstCategory.DataSource = (new BudgetEstimationBLL()).getAllCategory();
            rptBudgetEstCategory.DataBind();
        }

        //private void LoadCurrencyEstimate()
        //{
        //    MasterBLL objMasterBLL = new MasterBLL();
        //    CurrencyList objCurrencyList = new CurrencyList();
        //    objCurrencyList = objMasterBLL.LoadCurrency();
        //    ddlCurrencyBudget.Items.Clear();
        //    ddlCurrencyBudget.DataTextField = "CurrencyCode";
        //    ddlCurrencyBudget.DataValueField = "CurrencyID";
        //    ddlCurrencyBudget.DataSource = objCurrencyList;
        //    ddlCurrencyBudget.DataBind();
        //    ddlCurrencyBudget.Items.Insert(0, "--Select--");
        //    ddlCurrencyBudget.SelectedIndex = 0;
        //}
        /// <summary>
        /// method to load category and subcategory dropdownlist
        /// </summary>
        private void screenintiallization()
        {
            BindCategories(false);
            BindNewCategories(false);
            getBudgetEstimationfromProject();
            getBudgetCurrenceFromProject();
        }
        /// <summary>
        /// Method to fetch currency from database and assign to textbox
        /// </summary>
        private void getBudgetCurrenceFromProject()
        {
            BudgetEstimationBLL BudgetEstimationBLLObj = new BudgetEstimationBLL();
            BudgetEstimationBO BudgetEstimationBOObj = new BudgetEstimationBO();
            string projectID = Session["PROJECT_ID"].ToString();
            BudgetEstimationBOObj = BudgetEstimationBLLObj.getCurrenceFromProject(projectID);
            MillionUSH.Text = BudgetEstimationBOObj.CurrencyCode;
            lblTotalBudgEstCurr.Text = BudgetEstimationBOObj.CurrencyCode;
            CurrenceID.Text = BudgetEstimationBOObj.CurrencyID.ToString();
            CurrenceID.Visible = false;
        }
        /// <summary>
        /// Method to fetch currency format from database
        /// </summary>
        public void getBudgetEstimationfromProject()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));
            TBSTextBox.Text = UtilBO.CurrencyFormat(objProject.TotalEstBudget); 
            BudgetEstimationIDTextBox.Text = "0";
        }
        /// <summary>
        /// To bind category dropdownlist
        /// </summary>
        /// <param name="lastItemSelected"></param>
        protected void BindCategories(bool lastItemSelected)
        {
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();

            CategoryDropDownList.DataSource = BudgetEstimationBLLobj.getAllCategory();
            CategoryDropDownList.DataTextField = "CategoryName";
            CategoryDropDownList.DataValueField = "CategoryID";
            CategoryDropDownList.DataBind();
            CategoryDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));

            if (lastItemSelected)
                CategoryDropDownList.Items[CategoryDropDownList.Items.Count - 1].Selected = true;

            else
                CategoryDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        /// to bind new category dropdownlist
        /// </summary>
        /// <param name="lastItemSelected"></param>
        protected void BindNewCategories(bool lastItemSelected)
        {
            // used when adding a new Sub Category.
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();

            AddNewCategoryDropDownList.DataSource = BudgetEstimationBLLobj.getAllCategory();
            AddNewCategoryDropDownList.DataTextField = "CategoryName";
            AddNewCategoryDropDownList.DataValueField = "CategoryID";
            AddNewCategoryDropDownList.DataBind();

            if (lastItemSelected)
                CategoryDropDownList.Items[CategoryDropDownList.Items.Count - 1].Selected = true;
            else
                CategoryDropDownList.SelectedIndex = 0;
        }
        // calls getSubCatByCatID method
        protected void CategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSubCatByCatID();
        }
        /// <summary>
        /// method to fetch ID based on selected item in the CategoryDropDownList
        /// </summary>
        private void getSubCatByCatID()
        {
            int CategoryID = Convert.ToInt32(CategoryDropDownList.SelectedItem.Value.ToString());

            BindSubCategories(CategoryID, false);
        }
        /// <summary>
        /// method to bind data to subcategory dropdownlist
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="lastItemSelected"></param>
        protected void BindSubCategories(int categoryID, bool lastItemSelected)
        {
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();

            ListItem lstItem = new ListItem(SubCategoryDropDownList.Items[0].Text, SubCategoryDropDownList.Items[0].Value);
            SubCategoryDropDownList.Items.Clear();

            SubCategoryDropDownList.DataSource = BudgetEstimationBLLobj.getSubCatByCatID(categoryID);
            SubCategoryDropDownList.DataTextField = "SubCategoryName";
            SubCategoryDropDownList.DataValueField = "SubCategoryID";
            SubCategoryDropDownList.DataBind();
            SubCategoryDropDownList.Items.Insert(0, lstItem);

            if (lastItemSelected)
                SubCategoryDropDownList.Items[SubCategoryDropDownList.Items.Count - 1].Selected = true;
            else
                SubCategoryDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ViewState["BudgetEstimationID"] = 0;
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
            if (BudgetEstimationIDTextBox.Text != "0")
            {
                string uID = Session["USER_ID"].ToString();
                string pID = Session["PROJECT_ID"].ToString();

                BudgetEstimationBO objBudgetEstimation = new BudgetEstimationBO();
                objBudgetEstimation.BudgetEstimationID = Convert.ToInt32(BudgetEstimationIDTextBox.Text);
                objBudgetEstimation.CategoryID = Convert.ToInt32(CategoryDropDownList.SelectedItem.Value.ToString().Trim());
                objBudgetEstimation.SubCategoryID = Convert.ToInt32(SubCategoryDropDownList.SelectedItem.Value.ToString());
                objBudgetEstimation.ValueAmount = ValueTextBox.Text.ToString().Replace(",", "").Trim();
                objBudgetEstimation.ValueAmountper = ValuePerTextBox.Text.ToString().Trim();
                objBudgetEstimation.UserID = Convert.ToInt32(uID);
                objBudgetEstimation.ProjectID = Convert.ToInt32(pID);
                objBudgetEstimation.CurrencyID = Convert.ToInt32(CurrenceID.Text.ToString());
                objBudgetEstimation.AccountNo = AcountNumberTextBox.Text.ToString();

                //BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
                string message = BudgetEstimationBLLobj.EditBudgetEstimation(objBudgetEstimation);
                if (message == string.Empty || message == "null")
                {
                    BudgetEstimationIDTextBox.Text = "0";
                    BindEstCategories();
                    getToatlBudgetValue();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data Updated successfully');", true);

                    ClearDetails();
                    SetUpdateMode(false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
            }
            else
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string pID = Session["PROJECT_ID"].ToString();

                    BudgetEstimationBO objBudgetEstimation = new BudgetEstimationBO();
                    objBudgetEstimation.CategoryID = Convert.ToInt32(CategoryDropDownList.SelectedItem.Value.ToString().Trim());
                    objBudgetEstimation.SubCategoryID = Convert.ToInt32(SubCategoryDropDownList.SelectedItem.Value.ToString());
                    objBudgetEstimation.ValueAmount = ValueTextBox.Text.ToString().Replace(",", "").Trim();
                    objBudgetEstimation.ValueAmountper = ValuePerTextBox.Text;
                    objBudgetEstimation.UserID = Convert.ToInt32(uID);
                    objBudgetEstimation.ProjectID = Convert.ToInt32(pID);
                    objBudgetEstimation.CurrencyID = Convert.ToInt32(CurrenceID.Text.ToString());
                    objBudgetEstimation.AccountNo = AcountNumberTextBox.Text.ToString();
                    //if (ddlCurrencyBudget.SelectedIndex > 0)
                    //{
                    //    objBudgetEstimation.BudgetCurrency = Convert.ToInt32(ddlCurrencyBudget.SelectedValue);
                    //}

                    //BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
                    string message = BudgetEstimationBLLobj.InsertBudgetEstimation(objBudgetEstimation);

                    if (message == string.Empty || message == "null")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                        BindEstCategories();
                        getToatlBudgetValue();

                        ClearDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    BudgetEstimationBLLobj = null;
                }
            }
        }
        /// <summary>
        /// To save category details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Category_SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
            try
            {
                string uID = Session["USER_ID"].ToString();

                BudgetEstimationBO objNEWCategory = new BudgetEstimationBO();
                objNEWCategory.CategoryName = AddCategoryTextBox.Text.ToString().Trim();
                objNEWCategory.UserID = Convert.ToInt32(uID);

                //BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
                message = BudgetEstimationBLLobj.InsertNEWCategory(objNEWCategory);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    BindCategories(true);
                    BindNewCategories(true);
                    BindEstCategories();
                   // SubCategoryDropDownList.Items.Clear();
                    ListItem lstItem = SubCategoryDropDownList.Items[0];
                    SubCategoryDropDownList.Items.Clear();
                    SubCategoryDropDownList.Items.Insert(0, lstItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                BudgetEstimationBLLobj = null;
                AddCategoryTextBox.Text = "";
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To save subcategory details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Sub_Category_SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();
            try
            {
                string uID = Session["USER_ID"].ToString();

                BudgetEstimationBO objNEWsubCategory = new BudgetEstimationBO();
                objNEWsubCategory.SubCategoryName = AddSubCategoryTextBox.Text.ToString().Trim();
                objNEWsubCategory.CategoryID = Convert.ToInt32(AddNewCategoryDropDownList.SelectedItem.Value.ToString());
                objNEWsubCategory.UserID = Convert.ToInt32(uID);

                message = BudgetEstimationBLLobj.InsertNEWsubCategory(objNEWsubCategory);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    BindSubCategories(Convert.ToInt32(CategoryDropDownList.SelectedItem.Value), true);
                    BindEstCategories();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AddNewCategoryDropDownList.SelectedIndex = 0;
                AddSubCategoryTextBox.Text = "";
                BudgetEstimationBLLobj = null;
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To calculate and display amount in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBudgetEstimation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litAmountDisplay = (Literal)e.Row.FindControl("litAmountDisplay");
                string amountValue = DataBinder.Eval(e.Row.DataItem, "ValueAmount").ToString();
                decimal amount;

                if (decimal.TryParse(amountValue, out amount))
                {
                    litAmountDisplay.Text = UtilBO.CurrencyFormat(amount);
                }
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBudgetEstimation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["BudgetEstimationID"] = e.CommandArgument;
                BudgetEstimationBO objBudgetEstimation = new BudgetEstimationBO();
                BudgetEstimationBLL objBudgetEstimationBLL = new BudgetEstimationBLL();
                objBudgetEstimation = objBudgetEstimationBLL.GetBudgetEstimationByID(Convert.ToInt32(ViewState["BudgetEstimationID"]));
                if (objBudgetEstimation != null)
                {
                    ValueTextBox.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(objBudgetEstimation.ValueAmount));
                    HiddenFieldTotalValue.Value = (float.Parse(HiddenFieldTotalValue.Value) - float.Parse(objBudgetEstimation.ValueAmount)).ToString();
                    ValuePerTextBox.Text = objBudgetEstimation.ValueAmountper;
                    BudgetEstimationIDTextBox.Text = objBudgetEstimation.BudgetEstimationID.ToString();
                    CategoryDropDownList.ClearSelection();
                    if (CategoryDropDownList.Items.FindByValue(objBudgetEstimation.CategoryID.ToString()) != null)
                        CategoryDropDownList.Items.FindByValue(objBudgetEstimation.CategoryID.ToString()).Selected = true;
                    getSubCatByCatID();
                    SubCategoryDropDownList.ClearSelection();
                    if (SubCategoryDropDownList.Items.FindByValue(objBudgetEstimation.SubCategoryID.ToString()) != null)
                        SubCategoryDropDownList.Items.FindByValue(objBudgetEstimation.SubCategoryID.ToString()).Selected = true;
                    AcountNumberTextBox.Text = objBudgetEstimation.AccountNo;

                }
                SetUpdateMode(true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                string budgetEstimationID = e.CommandArgument.ToString();
                BudgetEstimationBLL objBudgetEstimationBLL = new BudgetEstimationBLL();
                message = objBudgetEstimationBLL.DeleteBudgetEstimation(Convert.ToInt32(budgetEstimationID));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearDetails();
                SetUpdateMode(false);
                BindEstCategories();
                getToatlBudgetValue();
            }           
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                SaveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                SaveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["BudgetEstimationID"] = "0";
            }
        }
        /// <summary>
        /// Method to calculate total budget value
        /// </summary>
        public void getToatlBudgetValue()
        {
            Label TotalX = null;
            decimal subTotal = 0;
            decimal total = 0;

            foreach (RepeaterItem rptItem in rptBudgetEstCategory.Items)
            {
                GridView grdSubCat = (GridView)rptItem.FindControl("grdBudgetEstimation");
                subTotal = 0;

                foreach (GridViewRow grdRow in grdSubCat.Rows)
                {
                    TotalX = ((Label)grdSubCat.FooterRow.FindControl("TotalAmount_cal"));
                    decimal litValueAmount = Convert.ToDecimal(((Literal)grdRow.FindControl("litValueAmount")).Text);
                    subTotal += litValueAmount;
                    total = total + litValueAmount;
                    TotalX.Text = UtilBO.CurrencyFormat(subTotal);
                }
            }

            HiddenFieldTotalValue.Value = total.ToString();
            lblGrandTotal.Text = UtilBO.CurrencyFormat(total);
        }
        /// <summary>
        /// calls ClearDetails method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearDetails();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            CategoryDropDownList.ClearSelection();
            getSubCatByCatID();
            ValueTextBox.Text = "";
            ValuePerTextBox.Text = "";
            BudgetEstimationIDTextBox.Text = "0";
            AcountNumberTextBox.Text = "";
        }
        /// <summary>
        /// To bind data to hidden field in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptBudgetEstCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnEstCategoryID = (HiddenField)e.Item.FindControl("hdnEstCategoryID");
                GridView grdSubCat = (GridView)e.Item.FindControl("grdBudgetEstimation");

                BudgetEstimationBLL BudgetEstimationBLLobj = new BudgetEstimationBLL();

                string pID = Session["PROJECT_ID"].ToString();
                grdSubCat.DataSource = BudgetEstimationBLLobj.GetBudgetEstimation(pID, Convert.ToInt32(hdnEstCategoryID.Value));
                grdSubCat.DataBind();

                if (grdSubCat.Rows.Count == 0) e.Item.Visible = false;
            }
        }
    }
}