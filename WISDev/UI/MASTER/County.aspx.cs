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
    public partial class County : System.Web.UI.Page
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
                Master.PageHeader = "County";
                ViewState["COUNTYID"] = 0;
                BindGrid();
                GetDistrictName();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                txtCounty.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    pnlSearch.Visible = true;
                    pnlCountyDetails.Visible = false;               
                    grdCounty.Columns[grdCounty.Columns.Count - 1].Visible = false;
                    grdCounty.Columns[grdCounty.Columns.Count - 2].Visible = false;
                    grdCounty.Columns[grdCounty.Columns.Count - 3].Visible = false;                    
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
                stb.Append(btnCountySearch.ClientID);
            else
                stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// to get district names
        /// </summary>
        /// <returns></returns>
        private void GetDistrictName()
        {
            DistrictBLL BLLobj = new DistrictBLL();

            ddlDistrictName.DataSource = BLLobj.GetDistrict();
            ddlDistrictName.DataTextField = "DISTRICTNAME";
            ddlDistrictName.DataValueField = "DISTRICTID";
            ddlDistrictName.DataBind();

            //DDLSearchDistrictName.DataSource = BLLobj.GetDistrict();
            //DDLSearchDistrictName.DataTextField = "DISTRICTNAME";
            //DDLSearchDistrictName.DataValueField = "DISTRICTID";
            //DDLSearchDistrictName.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            int districtID =Convert.ToInt32( ddlDistrictName.SelectedValue );
            //int districtID = Convert.ToInt32(ViewState["DISTRICTID"]);
            CountyBLL CountyBLLobj = new CountyBLL();
            grdCounty.DataSource = CountyBLLobj.GetAllCounties(districtID);
            grdCounty.DataBind();

        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <returns></returns>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCounty.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to get county names based on ID
        /// </summary>
        /// <returns></returns>
        private void GetCountyById()
        {
            CountyBLL CountyBLLobj = new CountyBLL();

            CountyBO CountyBOobj = CountyBLLobj.GetCountyById(Convert.ToInt32(ViewState["COUNTYID"]));

            if (CountyBOobj != null)
                txtCounty.Text = CountyBOobj.CountyName;
            ddlDistrictName.ClearSelection();
            if (ddlDistrictName.Items.FindByText(CountyBOobj.DistrictName.ToString()) != null)
                ddlDistrictName.Items.FindByText(CountyBOobj.DistrictName.ToString()).Selected = true;
            CountyBOobj = null;
            CountyBLLobj = null;
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

                string COUNTYID = ((Literal)gr.FindControl("litCOUNTYID")).Text;

                CountyBLL CountyBLLobj = new CountyBLL();
                message = CountyBLLobj.ObsoleteCounty(Convert.ToInt32(COUNTYID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        /// to save data to database
        /// </summary>
        /// <returns></returns>
        private void SaveCounty()
        {
            CountyBLL CountyBLLobj = new CountyBLL();
            CountyBO CountyBOobj = new CountyBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            CountyBOobj.DistrictID =Convert.ToInt32( ddlDistrictName.SelectedItem.Value);
            CountyBOobj.CountyName = txtCounty.Text.Trim();
            CountyBOobj.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = CountyBLLobj.AddCounty(CountyBOobj);

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
                CountyBLLobj = null;
            }
        }
        /// <summary>
        /// to update data to database
        /// </summary>
        /// <returns></returns>
        private void UpdateCounty()
        {
            CountyBLL CountyBLLobj = new CountyBLL();
            CountyBO CountyBOobj = new CountyBO();
            string message = "";

            try
            {
                if (ViewState["COUNTYID"] != null)
                    CountyBOobj.CountyID = Convert.ToInt32(ViewState["COUNTYID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();
                
                CountyBOobj.DistrictID = Convert.ToInt32(ddlDistrictName.SelectedItem.Value);
                CountyBOobj.CountyName = txtCounty.Text.Trim();
                CountyBOobj.UpdatedBy = Convert.ToInt32(uID);

                message = CountyBLLobj.UpdateCounty(CountyBOobj);

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
                CountyBLLobj = null;
            }
        }
        /// <summary>
        /// calls save method
        /// </summary>
        /// <returns></returns>
        protected void btnSave_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveCounty();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateCounty();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// calls clear method
        /// </summary>
        /// <returns></returns>
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
            BindGrid();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlDistrictName.ClearSelection();
            txtCounty.Text = string.Empty;
            
            SetUpdateMode(false);
          
           

        }
        /// <summary>
        /// to change text of thebutton based on condition
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
                ViewState["COUNTYID"] = "0";
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCounty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["COUNTYID"] = e.CommandArgument;
                GetCountyById();
                SetUpdateMode(true);
                pnlCountyDetails.Visible = true;
                pnlSearch.Visible = false;
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                CountyBLL CountyBLLobj = new CountyBLL();
                message = CountyBLLobj.DeleteCounty(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                ClearData();
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// to change values in dropdownlist based on index 
        /// </summary>
        protected void ddlDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// Show Add county Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            pnlCountyDetails.Visible = true;
            pnlSearch.Visible = false;
           // DDLSearchDistrictName.ClearSelection();
            ClearData();
            BindGrid();
        }
       /// <summary>
       /// Show search county Panel and hide Add panel
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            pnlCountyDetails.Visible = false;
            pnlSearch.Visible = true;
            txtSearchCounty.Text = string.Empty;
           // DDLSearchDistrictName.ClearSelection();
            ClearData();
            GetDistrictName();
            BindGrid();
        }
        /// <summary>
        /// Show UserSearch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUserSearch_Click(object sender, EventArgs e)
        {
            string County = "0";
            //int districtID = Convert.ToInt32(DDLSearchDistrictName.SelectedValue);
            if (txtSearchCounty.Text != String.Empty)
            {
                County = txtSearchCounty.Text.ToString();
            }
            searchBind(County);
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        public void searchBind(string County)
        {
            CountyBLL CountyBLLobj = new CountyBLL();
            grdCounty.DataSource = CountyBLLobj.GetCounties(County);
            grdCounty.DataBind();
           // DDLSearchDistrictName.ClearSelection();
            //txtSearchCounty.Text = string.Empty;
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
           // DDLSearchDistrictName.ClearSelection();
            txtSearchCounty.Text = string.Empty;
            BindGrid();
        }
        /// <summary>
        /// to change values in dropdownlist based on index 
        /// </summary>
        protected void DDLSearchDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }
    }
}