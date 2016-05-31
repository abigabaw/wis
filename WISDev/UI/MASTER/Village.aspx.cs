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
    public partial class Village : System.Web.UI.Page
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
                Master.PageHeader = "Village";
                ViewState["VILLAGEID"] = 0;
                BindGrid();
                BindDistricts();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                txtVillage.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    pnlSearch.Visible = true;
                    pnlAddVillage.Visible = false;
                    grdvillage.Columns[grdvillage.Columns.Count - 1].Visible = false;
                    grdvillage.Columns[grdvillage.Columns.Count - 2].Visible = false;
                    grdvillage.Columns[grdvillage.Columns.Count - 3].Visible = false;
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
            int SUBCOUNTYID = Convert.ToInt32(ddlSubcounty.SelectedValue);
            VillageBLL VillageBLLobj = new VillageBLL();
            grdvillage.DataSource = VillageBLLobj.GetAllVillage(SUBCOUNTYID);
            grdvillage.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindSearchVillage()
        {
            string value = txtVillagename.Text;
            VillageBLL VillageBLLobj = new VillageBLL();
            grdvillage.DataSource = VillageBLLobj.SearchVillage(value);
            grdvillage.DataBind();


        }
        /// <summary>
        /// To bind data to district dropdownlist from database
        /// </summary>
 
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To change values in the dropdownlist based on index
        /// </summary>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
                            
        }
        /// <summary>
        /// To bind data to SubCounties dropdownlist from database based on countyID
        /// </summary>
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubcounty.Items[0].Text, ddlSubcounty.Items[0].Value);
            ddlSubcounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubcounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubcounty.DataTextField = "SubCountyName";
                ddlSubcounty.DataValueField = "SubCountyID";
                ddlSubcounty.DataBind();
            }
            ddlSubcounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To bind data to Counties dropdownlist from database based on districtID
        /// </summary>
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
        /// To fetch values in dropdownlist on change of index 
        /// </summary>
        protected void ddlSubcounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            uplSubCounty.Update();
            BindGrid();

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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBudgetItem()
        {
            VillageBLL VillageBLLobj = new VillageBLL();
            VillageBO objVillageBO = new VillageBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            objVillageBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            objVillageBO.CountyID = Convert.ToInt32(ddlCounty.SelectedItem.Value);
            objVillageBO.SubCountyID = Convert.ToInt32(ddlSubcounty.SelectedItem.Value);
            objVillageBO.VillageName = txtVillage.Text.Trim();
            objVillageBO.CreatedBy = Convert.ToInt32(uID);
            
            try
            {
                message = VillageBLLobj.AddVillage(objVillageBO);

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
                VillageBLLobj = null;
            }
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBudgetItem()
        {
            VillageBLL VillageBLLobj = new VillageBLL();
            VillageBO objVillageBO = new VillageBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();
            objVillageBO.VillageID = Convert.ToInt32(ViewState["VILLAGEID"].ToString());
            objVillageBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            objVillageBO.CountyID = Convert.ToInt32(ddlCounty.SelectedItem.Value);
            objVillageBO.SubCountyID = Convert.ToInt32(ddlSubcounty.SelectedItem.Value);
            objVillageBO.VillageName = txtVillage.Text.Trim();
            objVillageBO.CreatedBy = Convert.ToInt32(uID);
            try
            {
                message = VillageBLLobj.UpdateVillage(objVillageBO);

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
                VillageBLLobj = null;
            }
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
                    ViewState["VILLAGEID"] = "0";
                }
            }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();
            ddlSubcounty.ClearSelection();
            txtVillage.Text = string.Empty;
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdvillage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["VILLAGEID"] = e.CommandArgument;
                SetUpdateMode(true);
                GetVillageById();
                pnlAddVillage.Visible = true;
                pnlSearch.Visible = false;
               
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                VillageBLL VillageBLLobj = new VillageBLL();
                message = VillageBLLobj.DeleteVillage(Convert.ToInt32(e.CommandArgument));
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
        ///To fetch village details based on ID and assign to textbox
        /// </summary>
        private void GetVillageById()
        {
            VillageBLL VillageBLLobj = new VillageBLL();


            VillageBO objVillageBO = VillageBLLobj.GetVillageById(Convert.ToInt32(ViewState["VILLAGEID"]));

            if (objVillageBO != null)
                txtVillage.Text = objVillageBO.VillageName;
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();

            if (ddlDistrict.Items.FindByText(objVillageBO.DistrictName.ToString()) != null)
                ddlDistrict.Items.FindByText(objVillageBO.DistrictName.ToString()).Selected = true;

            BindCounties(ddlDistrict.SelectedItem.Value);
            
            if (ddlCounty.Items.FindByText(objVillageBO.CountyName.ToString()) != null)
                ddlCounty.Items.FindByText(objVillageBO.CountyName.ToString()).Selected = true;
          //string countyID = objVillageBO.CountyID.ToString();
            BindSubCounties(ddlCounty.SelectedItem.Value);

            if (ddlSubcounty.Items.FindByText(objVillageBO.SubCountyName.ToString()) != null)
                ddlSubcounty.Items.FindByText(objVillageBO.SubCountyName.ToString()).Selected = true;

            
            objVillageBO = null;
            VillageBLLobj = null;
        }
        /// <summary>
        ///To change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdvillage.PageIndex = e.NewPageIndex;
                BindGrid();
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

                string VILLAGEID = ((Literal)gr.FindControl("litVILLAGEID")).Text;

                VillageBLL VillageBLLobj = new VillageBLL();
                message = VillageBLLobj.ObsoleteVillage(Convert.ToInt32(VILLAGEID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        /// to fetch values in dropdownlist based on index 
        /// </summary>
        protected void ddlCounty_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
        }

        /// <summary>
        /// calls seach village method
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string val = "";
            if (txtVillagename.Text.Trim() != string.Empty)
            {
                val = txtVillagename.Text;
            }
            VillageBLL villageBLL = new VillageBLL();
            searchVillage(val);
        }

        /// <summary>
        /// binds village data
        /// </summary>
        private void searchVillage(string val)
        {
            VillageBLL objVillageBLL = new VillageBLL();
            grdvillage.DataSource = objVillageBLL.SearchVillage(val);
            grdvillage.DataBind();           
 
        }

        /// <summary>
        /// clear search details
        /// </summary>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtVillagename.Text = string.Empty;
            BindGrid();
            
        }
        /// <summary>
        /// Show Add village Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            pnlAddVillage.Visible = true;
            pnlSearch.Visible = false;          
            ClearData();

        }
        /// <summary>
        /// Show search village Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);

        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlSearch.Visible = true;
            pnlAddVillage.Visible = false;
            if (showAdd) pnlAddVillage.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        protected void txtVillagename_TextChanged(object sender, EventArgs e)
        {

        }

        }


       
    }

