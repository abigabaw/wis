using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class PublicConsultationandDisclosure : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Set Page Header,set attributes to link buttons. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }

            calConsultationDate.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                // p1Grid.Visible = false;

                //p1Grid.ScrollBars = ScrollBars.None;
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Public Consultation and Disclosure";
                }

                ViewState["CONSULTATIONID"] = 0;

                BindDropDownDistrict();
                BindOfficerInCharge();
                BindGrid(false, false);
                getProjectDtaes();
                txtConsultationDate.Attributes.Add("readonly", "readonly");
                txtConsultationDate.Attributes.Add("onkeydown", "doCheck();");
                txtbxNameofthePersonGroup.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PCDD) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPublicConsultation.Columns[grdPublicConsultation.Columns.Count - 1].Visible = false;
                }

            }
        }
        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        /// <summary>
        /// Bind data to grid
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {

            int ProjectId = 0;

            if (Session["PROJECT_ID"] != null)
                ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);

            PublicConsultationBLL oBLL = new PublicConsultationBLL();
            PublicConsultationList PublicConsul = oBLL.GetPublucConsultation(ProjectId);
            if (PublicConsul.Count > 0)
                p1Grid.Style.Add("display", "");
            else
                p1Grid.Style.Add("display", "none");
            grdPublicConsultation.DataSource = oBLL.GetPublucConsultation(ProjectId);
            grdPublicConsultation.DataBind();
            p1Grid.Visible = true;
            p1Grid.ScrollBars = ScrollBars.Horizontal;
        }

        /// <summary>
        /// Save and Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;

            if (ViewState["CONSULTATIONID"].ToString() == "0")
            {
                PublicConsultationBLL oBLL = new PublicConsultationBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();

                    PublicConsultationBO oBO = new PublicConsultationBO();

                    oBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    oBO.District = ddlDistrict.SelectedItem.Text;
                    oBO.County = ddlCounty.SelectedItem.Text;
                    oBO.SubCounty = ddlSubCounty.SelectedItem.Text;
                    oBO.Parish = ddlParish.SelectedItem.Text;
                    oBO.Village = ddlVillage.SelectedItem.Text;
                    oBO.NameOfPerson = txtbxNameofthePersonGroup.Text.Trim();
                    string strMax = txtbxAddress.Text.ToString().Trim();
                    if (strMax.Trim().Length >= 500)
                    {
                        strMax = txtbxAddress.Text.ToString().Trim().Substring(0, 500);
                    }
                    oBO.Address = strMax;
                    oBO.Telephone = txtbxTelephoneContact.Text.Trim();
                    oBO.StakeholdingCategory = txtbxCategoryofStakeholding.Text.Trim();
                    oBO.ConsultationDate = Convert.ToDateTime(txtConsultationDate.Text);
                    oBO.OfficerIncharge = Convert.ToInt32(ddlOfficerInCharge.SelectedItem.Value);

                    oBO.PurposeOfMeeting = txtbxPurposeofMeeting.Text;
                    string strngMax = txtbxIssuesArising.Text.ToString().Trim();
                    if (strngMax.Trim().Length >= 2000)
                    {
                        strngMax = txtbxIssuesArising.Text.ToString().Trim().Substring(0, 2000);
                    }
                    oBO.Issues = strngMax;
                    string strngMaxstr = txtbxProposedRem.Text.ToString().Trim();
                    if (strngMax.Trim().Length >= 2000)
                    {
                        strngMaxstr = txtbxProposedRem.Text.ToString().Trim().Substring(0, 2000);
                    }

                    oBO.Remedies = strngMaxstr;
                    oBO.CreatedBy = Convert.ToInt32(uID);

                    String Message = oBLL.Insert(oBO);

                    BindGrid(true, true);

                    if (String.IsNullOrEmpty(Message) || Message == "null")
                    {
                        ClearData();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data Saved successfully');", true);
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NotAdded", "alert('" + Message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    oBLL = null;
                }
            }
            else
            {
                PublicConsultationBLL oBLL = new PublicConsultationBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();

                    PublicConsultationBO oBO = new PublicConsultationBO();

                    oBO.CONSULTATIONID = Convert.ToInt32(ViewState["CONSULTATIONID"]);
                    oBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    oBO.District = ddlDistrict.SelectedItem.Text;
                    oBO.County = ddlCounty.SelectedItem.Text;
                    oBO.SubCounty = ddlSubCounty.SelectedItem.Text;
                    oBO.Parish = ddlParish.SelectedItem.Text;
                    oBO.Village = ddlVillage.SelectedItem.Text;
                    oBO.NameOfPerson = txtbxNameofthePersonGroup.Text.Trim();
                    oBO.Address = txtbxAddress.Text.Trim();
                    oBO.Telephone = txtbxTelephoneContact.Text.Trim();
                    oBO.StakeholdingCategory = txtbxCategoryofStakeholding.Text.Trim();
                    oBO.ConsultationDate = Convert.ToDateTime(txtConsultationDate.Text);
                    oBO.OfficerIncharge = Convert.ToInt32(ddlOfficerInCharge.SelectedItem.Value);
                    oBO.PurposeOfMeeting = txtbxPurposeofMeeting.Text;
                    oBO.Issues = txtbxIssuesArising.Text;
                    oBO.Remedies = txtbxProposedRem.Text;

                    oBO.UpdatedBy = Convert.ToInt32(uID);

                    //PublicConsultationBLL oBLL1 = new PublicConsultationBLL();
                    String Message = oBLL.Update(oBO);
                   // ClearData();
                    BindGrid(true, true);
                    SetUpdateMode(false);
                    if (String.IsNullOrEmpty(Message) || Message == "null")
                    {
                        ClearData();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data Updated successfully');", true);
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NotAdded", "alert('" + Message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    oBLL = null;
                }
            }
        }

        /// <summary>
        /// To clear data in fields
        /// </summary>
        private void ClearData()
        {
            txtbxPurposeofMeeting.Text = string.Empty;
            txtbxIssuesArising.Text = string.Empty;
            txtbxProposedRem.Text = string.Empty;
            txtbxNameofthePersonGroup.Text = string.Empty;
            txtConsultationDate.Text = string.Empty;
            txtbxAddress.Text = string.Empty;
            txtbxTelephoneContact.Text = string.Empty;
            txtbxCategoryofStakeholding.Text = string.Empty;

            ddlCounty.ClearSelection();
            ddlDistrict.ClearSelection();
            ddlSubCounty.ClearSelection();
            ddlParish.ClearSelection();
            ddlVillage.ClearSelection();
            ddlOfficerInCharge.ClearSelection();
            ViewState["CONSULTATIONID"] = "0";
        }

        /// <summary>
        /// To clear data in fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// TO Edit Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PublicConsultation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CONSULTATIONID"] = e.CommandArgument;
                GetData();
                SetUpdateMode(true);
            }
        }

        /// <summary>
        /// Set Update Mode for buttons
        /// </summary>
        /// <param name="updateMode"></param>
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
                //ViewState["CONSULTATIONID"] = "0";
            }
        }

        /// <summary>
        /// Get Data
        /// </summary>
        private void GetData()
        {
            PublicConsultationBLL oBLL = new PublicConsultationBLL();
            int CONSULTATIONID = 0;

            if (ViewState["CONSULTATIONID"] != null)
                CONSULTATIONID = Convert.ToInt32(ViewState["CONSULTATIONID"]);

            PublicConsultationBO oBO = new PublicConsultationBO();

            oBO = oBLL.GetData(CONSULTATIONID);

            if (oBO != null)
            {
                txtbxPurposeofMeeting.Text = oBO.PurposeOfMeeting;
                txtbxIssuesArising.Text = oBO.Issues;
                txtbxProposedRem.Text = oBO.Remedies;
                txtbxCategoryofStakeholding.Text = oBO.StakeholdingCategory;
                txtbxAddress.Text = oBO.Address;
                txtbxNameofthePersonGroup.Text = oBO.NameOfPerson;
                txtbxTelephoneContact.Text = oBO.Telephone;
                txtConsultationDate.Text = oBO.ConsultationDate.ToString(UtilBO.DateFormat);

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(oBO.District) != null)
                    ddlDistrict.Items.FindByText(oBO.District).Selected = true;

                if (ddlDistrict.SelectedIndex > 0)
                {
                    BindCounties(ddlDistrict.SelectedItem.Value);

                    if (Convert.ToString(oBO.County) != "")
                    {
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(oBO.County) != null)
                            ddlCounty.Items.FindByText(oBO.County).Selected = true;
                    }
                }

                if (ddlCounty.SelectedIndex > 0)
                {
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    if (Convert.ToString(oBO.SubCounty) != "")
                    {
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(oBO.SubCounty) != null)
                            ddlSubCounty.Items.FindByText(oBO.SubCounty).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    if (Convert.ToString(oBO.Village) != "")
                    {
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(oBO.Village) != null)
                            ddlVillage.Items.FindByText(oBO.Village).Selected = true;
                    }

                    BindParish(ddlSubCounty.SelectedItem.Value);
                    if (oBO.Parish != null || Convert.ToString(oBO.Parish) != "")
                    {
                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(Convert.ToString(oBO.Parish).ToUpper()) != null)
                            ddlParish.Items.FindByText(Convert.ToString(oBO.Parish).ToUpper()).Selected = true;
                    }
                }

                ddlOfficerInCharge.ClearSelection();
                if (ddlOfficerInCharge.Items.FindByValue(oBO.OfficerIncharge.ToString()) != null)
                    ddlOfficerInCharge.Items.FindByValue(oBO.OfficerIncharge.ToString()).Selected = true;
            }
        }

        /// <summary>
        /// Format data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPublicConsultation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime consultationDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CONSULTATIONDATE"));
                if (consultationDate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lnkConsultationDate")).Text = consultationDate.ToString(UtilBO.DateFormat);
            }
        }

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {

        }


        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindOfficerInCharge()
        {
            UserBLL objUserBLL = new UserBLL();
            UserList objUserList = new UserList();
            UserBO oBOUser = null;
            oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;
            objUserList = objUserBLL.GetUsers(oBOUser);

            ddlOfficerInCharge.DataSource = objUserList;
            ddlOfficerInCharge.DataTextField = "UserName";
            ddlOfficerInCharge.DataValueField = "UserID";
            ddlOfficerInCharge.DataBind();
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
        }

        /// <summary>
        /// Bind Data to Drop Downs
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
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);
            ddlSubCounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataBind();
            }
            ddlSubCounty.Items.Insert(0, firstListItem);
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindVillages(string subCountyID)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);
            ddlVillage.Items.Clear();

            if (subCountyID != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCountyID);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }
            ddlVillage.Items.Insert(0, firstListItem);
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindParish(string subCountyID)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);
            ddlParish.Items.Clear();

            if (subCountyID != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCountyID);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishId";
                ddlParish.DataBind();
            }
            ddlParish.Items.Insert(0, firstListItem);
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            uplCounty.Update();
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        /// <summary>
        /// getProjectDtaes
        /// </summary>
        private void getProjectDtaes()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));

            if (objProject.ProjectStartDate != DateTime.MinValue)
                hfProjStartDate.Value = Convert.ToString(objProject.ProjectStartDate.ToString(UtilBO.DateFormat));

            if (objProject.ProjectEndDate != DateTime.MinValue)
                hfProjEndDate.Value = Convert.ToString(objProject.ProjectEndDate.ToString(UtilBO.DateFormat));

        }
    }
}