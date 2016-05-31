using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class SubCounty : System.Web.UI.Page
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
                Master.PageHeader = "Sub County";
                ViewState["SUBCOUNTYID"] = 0;
                BindGrid();
                BindDistricts();
                BindSearchDistricts();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                txtSubcounty.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    pnlSearch.Visible = true;
                    pnlSubCountyDetails.Visible = false;
                    grdSubcounty.Columns[grdSubcounty.Columns.Count - 1].Visible = false;
                    grdSubcounty.Columns[grdSubcounty.Columns.Count - 2].Visible = false;
                    grdSubcounty.Columns[grdSubcounty.Columns.Count - 3].Visible = false;
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
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
           
            int COUNTYID = Convert.ToInt32(ddlCounty.SelectedValue);
            SubCountyBLL SubCountyBLLobj = new SubCountyBLL();
            grdSubcounty.DataSource = SubCountyBLLobj.GetAllSubCounties(COUNTYID);
            grdSubcounty.DataBind();
        }

        /// <summary>
        /// to bind district details to district  dropdownlist
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }


        private void BindSearchDistricts()
        {
          //  ddlDistrictSearch.DataSource = (new MasterBLL()).LoadDistrictData();
            //ddlDistrictSearch.DataTextField = "DistrictName";
          //  ddlDistrictSearch.DataValueField = "DistrictID";
           // ddlDistrictSearch.DataBind();
        }
        /// <summary>
        ///To select values in dropdownlist based on iondex
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
           
        }

        /// <summary>
        /// To bind details to counties dropdownlist based on districtID
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlCounty.Items.Clear();

            if (districtID != "0")
            {
                ddlCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();
            }
            ddlCounty.Items.Insert(0, firstListItem);
        }
        //private void BindSearchCounties(string districtID)
        //{
        //    ListItem firstListItem = new ListItem(ddlCountySearch.Items[0].Text, ddlCountySearch.Items[0].Value);
        //    ddlCountySearch.Items.Clear();

        //    if (districtID != "0")
        //    {
        //        ddlCountySearch.DataSource = (new MasterBLL()).LoadCountyData(districtID);
        //        ddlCountySearch.DataTextField = "CountyName";
        //        ddlCountySearch.DataValueField = "CountyID";
        //        ddlCountySearch.DataBind();
        //    }
        //    ddlCountySearch.Items.Insert(0, firstListItem);
        //}

        /// <summary>
        /// Save and Update Data into Database
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
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBudgetItem()
        {
            SubCountyBLL SubCountyBLLobj = new SubCountyBLL();
            SubCountyBO objSubCountyBO = new SubCountyBO();

            string message = "";

            try
            {
                if (ViewState["SUBCOUNTYID"] != null)
                    objSubCountyBO.SubCountyID = Convert.ToInt32(ViewState["SUBCOUNTYID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                objSubCountyBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
                objSubCountyBO.CountyID = Convert.ToInt32(ddlCounty.SelectedItem.Value);
                objSubCountyBO.SubCountyName = txtSubcounty.Text.Trim();
                objSubCountyBO.CreatedBy = Convert.ToInt32(uID);

                message = SubCountyBLLobj.UpdateSubCounty(objSubCountyBO);

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
                SubCountyBLLobj = null;
            }
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBudgetItem()
        {
            SubCountyBLL SubCountyBLLobj = new SubCountyBLL();
            SubCountyBO objSubCountyBO = new SubCountyBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

           // objSubCountyBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            objSubCountyBO.CountyID = Convert.ToInt32(ddlCounty.SelectedItem.Value);
            if (txtSubcounty.Text != "")
            {
                objSubCountyBO.SubCountyName = txtSubcounty.Text.Trim();
            }
            objSubCountyBO.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = SubCountyBLLobj.AddSubCounty(objSubCountyBO);

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
                SubCountyBLLobj = null;
            }
            
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();
            txtSubcounty.Text = string.Empty;
           SetUpdateMode(false);
        }

        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["SUBCOUNTYID"] = "0";
            }
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdSubcounty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["SUBCOUNTYID"] = e.CommandArgument;
                GetSubCountyById();
                SetUpdateMode(true);
                pnlSubCountyDetails.Visible = true;
                pnlSearch.Visible = false;
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                SubCountyBLL SubCountyBLLobj = new SubCountyBLL();
                message = SubCountyBLLobj.DeleteSubCounty(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                BindGrid();
                ClearData();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// To fetch suncounty details based on ID from database
        /// </summary>
   
        private void GetSubCountyById()
        {
            SubCountyBLL SubCountyBLLobj = new SubCountyBLL();

            SubCountyBO SubCountyBOobj = SubCountyBLLobj.GetSubCountyById(Convert.ToInt32(ViewState["SUBCOUNTYID"]));

            if (SubCountyBOobj != null)
              txtSubcounty.Text = SubCountyBOobj.SubCountyName;
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();

            if (ddlDistrict.Items.FindByText(SubCountyBOobj.DistrictName.ToString()) != null)
                ddlDistrict.Items.FindByText(SubCountyBOobj.DistrictName.ToString()).Selected = true;

            BindCounties(ddlDistrict.SelectedItem.Value);
            if (ddlCounty.Items.FindByText(SubCountyBOobj.CountyName.ToString()) != null)
                ddlCounty.Items.FindByText(SubCountyBOobj.CountyName.ToString()).Selected = true;

            SubCountyBOobj = null;
            SubCountyBLLobj = null;
        }
        /// <summary>
        /// To change page in the grid
        /// </summary>

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdSubcounty.PageIndex = e.NewPageIndex;
            //int Dist = Convert.ToInt32(ddlDistrictSearch.SelectedValue);
            //int cnty = Convert.ToInt32(ddlCountySearch.SelectedValue);
            string sbcounty = txtSearchSubcounty.Text;
            searchBind(sbcounty);
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

                string SUBCOUNTYID = ((Literal)gr.FindControl("litSUBCOUNTYID")).Text;

                SubCountyBLL SubCountyBLLobj = new SubCountyBLL();
                message = SubCountyBLLobj.ObsoleteSubCounty(Convert.ToInt32(SUBCOUNTYID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearSearchData()
        {
            ddlDistrict.SelectedIndex = 0;
            ddlCounty.SelectedIndex = 0;
            ViewState["SUBCOUNTYID"] = 0;
            txtSubcounty.Text = "";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
            BindGrid();
        }

        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
            ClearSearchData();

        }

        /// <summary>
        /// Show and Hide the Search or Add banks panels baced on Selection
        /// </summary>
        /// <param name="showAdd"></param>
        /// <param name="showSearch"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlSubCountyDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlSubCountyDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

 
        protected void ddlDistrictSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BindSearchCounties(ddlDistrictSearch.SelectedItem.Value);
        }


        protected void ddlCountySearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Subcounty = "0";
            //int DistrictSearch = Convert.ToInt32(ddlDistrictSearch.SelectedItem.Value);
            //int CountySearch = Convert.ToInt32(ddlCountySearch.SelectedItem.Value);
            if (txtSearchSubcounty.Text != string.Empty)
            {
                Subcounty = txtSearchSubcounty.Text.ToString();
                searchBind(Subcounty);
            }
            else
            {
                BindGrid();
            }

        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void searchBind(string subCounty)
        {
            SubCountyBLL subCountyBLLobj = new SubCountyBLL();
            grdSubcounty.DataSource = subCountyBLLobj.GetSubCounties(subCounty);
            grdSubcounty.DataBind();
            //ddlDistrictSearch.ClearSelection();
            txtSearchSubcounty.Text = string.Empty;
           
        }

        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
           // ddlDistrictSearch.SelectedIndex = 0;
           // ddlCountySearch.SelectedIndex = 0;
            txtSearchSubcounty.Text = string.Empty;
            ClearSearchData();
        }


        protected void txtSearchSubcounty_TextChanged(object sender, EventArgs e)
        {

        }

    

       
    }
}