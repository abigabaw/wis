using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS.UI.MASTER
{
    public partial class District : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "District";
                ViewState["DISTRICTID"] = 0;
                BindGrid();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                txtDistrict.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    pnlSearch.Visible = true;
                    pnlDistrictDetails.Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 1].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 2].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 3].Visible = false;
                }
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnSearch.ClientID);
            else
                stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            
                DistrictBLL DistrictBLLobj = new DistrictBLL();
                grdDistrict.DataSource = DistrictBLLobj.GetAllDistricts();
                grdDistrict.DataBind();
           
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <returns></returns>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdDistrict.PageIndex = e.NewPageIndex;
            SearchBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["District_ID"] = e.CommandArgument;
                DistrictBLL objDistrictBLL = new DistrictBLL();
                DistrictBO objDistrict = objDistrictBLL.GetDistrictById(Convert.ToInt32(ViewState["District_ID"]));

                txtDistrict.Text = objDistrict.DistrictName;
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                DistrictBLL DistrictBLLobj = new DistrictBLL();
                message = DistrictBLLobj.DeleteDistrict(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                BindGrid();
                ClearData();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        //protected void ChangePage(object sender, GridViewPageEventArgs e)
        //{
        //    grdDistrict.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        /// <summary>
        /// to get details to textbox 
        /// </summary>
        /// <returns></returns>
        private void GetDistrictById()
        {
            DistrictBLL DistrictBLLobj = new DistrictBLL();

            DistrictBO DistrictBOobj = DistrictBLLobj.GetDistrictById(Convert.ToInt32(ViewState["DISTRICTID"]));

            if (DistrictBOobj != null)
                txtDistrict.Text = DistrictBOobj.DistrictName;

            DistrictBOobj = null;
            DistrictBLLobj = null;
        }
        /// <summary>
        /// to link to other page on click of link  in grid
        /// </summary>
        /// <returns></returns>
        protected void grdBudgetCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkSubCategory = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkSubCategory");
                int DISTRICTID = (int)DataBinder.Eval(e.Row.DataItem, "DISTRICTID");

                lnkSubCategory.Attributes.Add("onclick", "OpenSubCategories(" + DISTRICTID + ");");
            }
        }
        /// <summary>
        /// Update Database Make data as Obsoluted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string DISTRICTID = ((Literal)gr.FindControl("litDISTRICTID")).Text;
                
                DistrictBLL DistrictBLLobj = new DistrictBLL();
                message = DistrictBLLobj.ObsoleteDistrict(Convert.ToInt32(DISTRICTID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBudgetItem()
        {
            DistrictBLL DistrictBLLobj = new DistrictBLL();
            DistrictBO DistrictBOobj = new DistrictBO();

            string message = "";

            
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            DistrictBOobj.DistrictName = txtDistrict.Text.Trim();
            DistrictBOobj.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = DistrictBLLobj.AddDistrict(DistrictBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                BindGrid();
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                DistrictBLLobj = null;
            }
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBudgetItem()
        {
            DistrictBLL DistrictBLLobj = new DistrictBLL();
            DistrictBO DistrictBOobj = new DistrictBO();
            string message = "";

            try
            {
                if (ViewState["District_ID"] != null)
                    DistrictBOobj.DistrictID = Convert.ToInt32(ViewState["District_ID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                DistrictBOobj.DistrictName = txtDistrict.Text.Trim();
                DistrictBOobj.UpdatedBy = Convert.ToInt32(uID);

                message = DistrictBLLobj.UpdateDistrict(DistrictBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                ClearData();
                SetUpdateMode(false);
                BindGrid();

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DistrictBLLobj = null;
            }
        }
        /// <summary>
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveBudgetItem();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateBudgetItem();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearData();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtDistrict.Text = string.Empty;
            SetUpdateMode(false);
            BindGrid();
        }
        /// <summary>
        /// to change text of the button based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["DISTRICTID"] = "0";
            }
        }
        /// <summary>
        /// Show Add county Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindGrid();
            btnSave.Text = "Save";
            ClearSearchData();

           

        }
        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
            ClearData();
        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlDistrictDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlDistrictDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        /// <summary>
        /// To display details on click of button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string districtName = txtSearchDistrictName.Text.Trim();
            DistrictBLL objDistrictBLL = new DistrictBLL();
            grdDistrict.DataSource = objDistrictBLL.SearchDistrict(districtName);
            grdDistrict.DataBind();

            

        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void SearchBind()
        {
            string districtName = txtSearchDistrictName.Text.Trim();
            DistrictBLL objDistrictBLL = new DistrictBLL();
            grdDistrict.DataSource = objDistrictBLL.SearchDistrict(districtName);
            grdDistrict.DataBind();
 
        }
        /// <summary>
        /// Ccalls clearsearch method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchData();
            string districtName = txtSearchDistrictName.Text.Trim();
            DistrictBLL objDistrictBLL = new DistrictBLL();
            grdDistrict.DataSource = objDistrictBLL.SearchDistrict(districtName);
            grdDistrict.DataBind();

        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearSearchData()
        {
            txtDistrict.Text = "";
            ViewState["District_ID"] = 0;
            txtSearchDistrictName.Text = "";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }
      

      

    }
}