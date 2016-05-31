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
    public partial class Parish : System.Web.UI.Page
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
                Master.PageHeader = "Parish";
                ViewState["PARISHID"] = 0;
                BindGrid();
                BindDistricts();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                txtParish.Attributes.Add("onchange", "setDirtyText();");



                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    pnlSearch.Visible = true;
                    pnlDistrictDetails.Visible = false;
                    grdParish.Columns[grdParish.Columns.Count - 1].Visible = false;
                    grdParish.Columns[grdParish.Columns.Count - 2].Visible = false;
                    grdParish.Columns[grdParish.Columns.Count - 3].Visible = false;
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
            int subcountyid = Convert.ToInt32(ddlsubcounty.SelectedValue);
            int countyid = Convert.ToInt32(ddlCounty.SelectedValue);
            int districtid = Convert.ToInt32(ddlDistrict.SelectedValue);
            ParishBLL ParishBLLobj = new ParishBLL();
            grdParish.DataSource = ParishBLLobj.GetAllParish(subcountyid, countyid, districtid);
            grdParish.DataBind();
        }
        /// <summary>
        /// To set values to  district dropdownlist from database
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            //BindGrid();
        }
        /// <summary>
        /// To set values to county dropdownlist from database
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
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
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            //BindGrid();
        }

        //protected void ddlsubcounty_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindVillages(ddlSubCounty.SelectedItem.Value);
        //    BindParishes(ddlSubCounty.SelectedItem.Value);
        //}
        /// <summary>
        /// To set values to subcounty dropdownlist from database
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlsubcounty.Items[0].Text, ddlsubcounty.Items[0].Value);

            ddlsubcounty.Items.Clear();

            if (countyID != "0")
            {
                ddlsubcounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlsubcounty.DataTextField = "SubCountyName";
                ddlsubcounty.DataValueField = "SubCountyID";
                ddlsubcounty.DataBind();
            }

            ddlsubcounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// Calls saveparish method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveParish();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateParish();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateParish()
        {
            ParishBLL ParishBLLobj = new ParishBLL();
            ParishBO ParishBOobj = new ParishBO();

            string message = "";

            try
            {
                if (ViewState["PARISHID"] != null)
                    ParishBOobj.ParishId = Convert.ToInt32(ViewState["PARISHID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                ParishBOobj.SubcountyID = Convert.ToInt32(ddlsubcounty.SelectedItem.Value);
                ParishBOobj.ParishName = txtParish.Text.Trim();
                ParishBOobj.UpdatedBy = Convert.ToInt32(uID);

                message = ParishBLLobj.UpdateParish(ParishBOobj);

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
                ParishBLLobj = null;
            }
           
        }

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
                ViewState["PARISHID"] = "0";
            }
            
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveParish()
        {
            ParishBLL ParishBLLobj = new ParishBLL();
            ParishBO ParishBOobj = new ParishBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            ParishBOobj.SubcountyID = Convert.ToInt32(ddlsubcounty.SelectedItem.Value);
            ParishBOobj.ParishName = txtParish.Text.Trim();
            ParishBOobj.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = ParishBLLobj.AddParish(ParishBOobj);

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
                ParishBLLobj = null;
            }
            
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlsubcounty.ClearSelection();
            txtParish.Text = string.Empty;
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();
            SetUpdateMode(false);
            BindGrid();
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

                string PARISHID = ((Literal)gr.FindControl("litPARISHID")).Text;

                ParishBLL ParishBLLobj = new ParishBLL();
                message = ParishBLLobj.ObsoleteParish(Convert.ToInt32(PARISHID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdParish_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["PARISHID"] = e.CommandArgument;
                SetUpdateMode(true);
                GetParishById();                
                pnlDistrictDetails.Visible = true;
                pnlSearch.Visible = false;
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                ParishBLL ParishBLLobj = new ParishBLL();
                message = ParishBLLobj.DeleteParish(Convert.ToInt32(e.CommandArgument));
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
        /// To fetch data from database based on ParishID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetParishById()
        {
            ParishBLL ParishBLLobj = new ParishBLL();

            ParishBO ParishBOobj = ParishBLLobj.GetParishById(Convert.ToInt32(ViewState["PARISHID"]));

            if (ParishBOobj != null)

              ddlDistrict.ClearSelection();
            if (ddlDistrict.Items.FindByText(ParishBOobj.DistrictName.ToString()) != null)
                ddlDistrict.Items.FindByText(ParishBOobj.DistrictName.ToString()).Selected = true;

             ddlCounty.ClearSelection();
             string districtID = ParishBOobj.DistrictID.ToString();
             BindCounties(districtID);
            if (ddlCounty.Items.FindByText(ParishBOobj.countyName.ToString()) != null)
                ddlCounty.Items.FindByText(ParishBOobj.countyName.ToString()).Selected = true;
            
             ddlsubcounty.ClearSelection();
             string countyID = ParishBOobj.CountyID.ToString();
             BindSubCounties(countyID);
            if (ddlsubcounty.Items.FindByText(ParishBOobj.subcountyName.ToString()) != null)
                ddlsubcounty.Items.FindByText(ParishBOobj.subcountyName.ToString()).Selected = true;

                txtParish.Text = ParishBOobj.ParishName;
     
            ParishBOobj = null;
            ParishBLLobj = null;
        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdParish.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        protected void ddlsubcounty_SelectedIndexChanged(object sender, EventArgs e)
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
            pnlSearch.Visible = false;
            pnlDistrictDetails.Visible = true;
            TxtSearchParish.Text = String.Empty;
            ClearData();
           
        }
        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            pnlDistrictDetails.Visible = false;
            TxtSearchParish.Text = String.Empty;
            ClearData();
        }
        /// <summary>
        /// To clear search data and bind grid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            TxtSearchParish.Text = String.Empty;
            BindGrid();

        }
        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string SearchParish = "0";

            if (TxtSearchParish.Text != string.Empty)
            {
                SearchParish = TxtSearchParish.Text.ToString();
            }

            ParishBLL ParishBLLobj = new ParishBLL();
            grdParish.DataSource = ParishBLLobj.SearchParish(SearchParish);
            grdParish.DataBind();
        }

    }
}