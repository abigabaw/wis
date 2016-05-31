using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace WIS
{
    public partial class CDAPImplementation : System.Web.UI.Page
    {
        #region GlobalDeclaration
        ActivityList objActivityList;
        ActivityBLL objActivityBLL;
        PAP_HouseholdBLL objPAP_HouseholdBLL;
        CDAPImplementationBO objCDAPImplementationBO;
        CDAPImplementationBLL objCDAPImplementationBLL;
        CDAPImplementationList objCDAPImplementationList;
        int RowCount;
        #endregion

        #region PageEvents
        /// <summary>
        /// Check User permitions
        /// Set Page Header,set attributes to link buttons. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            caldptxtPeriodFrom.Format = UtilBO.DateFormat;
           caldptxtPeriodTo.Format = UtilBO.DateFormat;
           caldpDateFrom.Format = UtilBO.DateFormat;
            caldpDateTo.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                Master.PageHeader = "Community Development - CDAP Implementation - Plan";

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - CDAP Implementation - Plan";
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/UI/Project/ViewProjects.aspx?mode=redirCDAP"));
                }

                int ProjectID = 0;
                string ProjectCode = string.Empty;

                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                #region Upload Documets
                int userID = Convert.ToInt32(Session["USER_ID"]);

                int HHID = 0;
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }

                if (Session["PROJECT_CODE"] != null)
                {
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }
                string DocumentCode = "CDPADP";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                //if (HHID != 0)
                //{
                //    string DocumentCode = "CDPADP";

                //    string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                //    string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                //    lnkUPloadDoc.Attributes.Add("onclick", param);

                //    lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                //}
                //else
                //{
                //    lnkUPloadDoc.Visible = false;
                //    lnkUPloadDoclist.Visible = false;
                //}


                //End of code
                #endregion
                ViewState["CDAP_PHASEID"] = 0;
                ViewState["CDAP_PHASEACTIVITYID"] = 0;
                BindActivity();
                BindDistrict();
                BindDetailsGrid(ProjectID);

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMMUNITY_DEVELOPMENT) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSavePhase.Visible = false;
                    btnClearPhaseData.Visible = false;
                    lnkbtnshow.Visible = false;
                    grdPhases.Columns[4].Visible = false;
                    grdPhases.Columns[5].Visible = false;
                    lnkUPloadDoc.Visible = false;
                }
            }
        }
        /// <summary>
        /// Show And Hide phActivity placeholder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnshow_click(object sender, EventArgs e)
        {
            if (lnkbtnshow.Text == "Add New Activity")
            {
                phActivity.Visible = true;
                lnkbtnshow.Text = "Hide Activity";
                ClearDetails();
            }
            else
            {
                phActivity.Visible = false;
                lnkbtnshow.Text = "Add New Activity";
            }
        }

        /// <summary>
        /// Save and Update data in to data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavePhase_Click(object sender, EventArgs e)
        {
            objCDAPImplementationBO = new CDAPImplementationBO();
            if (ViewState["CDAP_PHASEID"] != null)
                objCDAPImplementationBO.Cdap_phaseid = Convert.ToInt32(ViewState["CDAP_PHASEID"]);
            else
                objCDAPImplementationBO.Cdap_phaseid = 0;
            objCDAPImplementationBO.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            objCDAPImplementationBO.PeriodFrom = Convert.ToDateTime(dptxtPeriodFrom.Text);
            objCDAPImplementationBO.PeriodTo = Convert.ToDateTime(dptxtPeriodTo.Text);
            objCDAPImplementationBO.Cdap_phaseno = Convert.ToInt32(ddlPhase.SelectedValue);
            objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
            if (txtExpenditure.Text != "")
            {
                objCDAPImplementationBO.EXPENDITURE = Convert.ToDecimal(txtExpenditure.Text);
            }

            objCDAPImplementationBLL = new CDAPImplementationBLL();
            objCDAPImplementationList = new CDAPImplementationList();
            if (((Button)sender).Text.ToUpper() == "SAVE PHASE")
            {
                string sresult = objCDAPImplementationBLL.AddCDAPPhase(objCDAPImplementationBO);
                if (string.IsNullOrEmpty(sresult) || sresult == "" || sresult == "null")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + sresult + "');", true);

            }
            else
            {
                string sresult = objCDAPImplementationBLL.AddCDAPPhase(objCDAPImplementationBO);
                if (string.IsNullOrEmpty(sresult) || sresult == "" || sresult == "null")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data updated successfully');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + sresult + "');", true);
            }

            BindDetailsGrid(Convert.ToInt32(Session["PROJECT_ID"]));
            ClearPhaseDetails();
            SetPhaseUpdateMode(false);
            if (phActivity.Visible == true)
            {
                ClearDetails();
                if (btnClear.Text.ToUpper() == "CANCEL")
                {
                    SetUpdateMode(false);
                }
                phActivity.Visible = false;
                lnkbtnshow.Text = "Add New Activity";
            }
            pnlActivity.Visible = false;
        }
        /// <summary>
        /// Set Update mode to buttons
        /// </summary>
        /// <param name="updateMode"></param>
        protected void SetPhaseUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSavePhase.Text = "Update Phase";
                btnClearPhaseData.Text = "Cancel";
            }
            else
            {
                btnSavePhase.Text = "Save Phase";
                btnClearPhaseData.Text = "Clear Phase";
                ViewState["CDAP_PHASEID"] = 0;
                ViewState["CDAP_PHASEACTIVITYID"] = 0;
            }
        }

        /// <summary>
        /// Clear Phase data
        /// </summary>
        private void ClearPhaseDetails()
        {
            dptxtPeriodFrom.Text = "";
            dptxtPeriodTo.Text = "";
            dpDateFrom.Text = "";
            dpDateTo.Text = "";
            txtExpenditure.Text = string.Empty;
            ddlPhase.ClearSelection();
            ddlPhase.Items[0].Selected = true;
            ViewState["CDAP_PHASEID"] = 0;
            ViewState["CDAP_PHASEACTIVITYID"] = 0;
        }
        /// <summary>
        /// to Change Page Index of Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPhases_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPhases.PageIndex = e.NewPageIndex;
            if (Session["PROJECT_ID"] != null)
            {
                int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                BindDetailsGrid(ProjectID);
            }
        }
        /// <summary>
        /// Set Default date format to Literals inside hte grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPhases_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime projStartDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PeriodFrom"));
                if (projStartDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litPeriodFrom")).Text = projStartDate.ToString(UtilBO.DateFormat);

                DateTime projEndDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PeriodTo"));
                if (projEndDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litPeriodTo")).Text = projEndDate.ToString(UtilBO.DateFormat);
            }
        }
        /// <summary>
        /// Edit,cancel and delete Actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPhases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (phActivity.Visible == true)
            {
                ClearDetails();
                if (btnClear.Text.ToUpper() == "CANCEL")
                {
                    SetUpdateMode(false);
                }
                phActivity.Visible = false;
                lnkbtnshow.Text = "Add New Activity";
            }
            if (e.CommandName == "EditRow")
            {
                ViewState["CDAP_PHASEID"] = e.CommandArgument;
                CDAPImplementationBLL objCDAPImplementationBLL = new CDAPImplementationBLL();
                CDAPImplementationList objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseDetailsByID(Convert.ToInt32(ViewState["CDAP_PHASEID"]));
                if (objCDAPImplementationList.Count > 0)
                {
                    ddlPhase.ClearSelection();
                    ddlPhase.Items.FindByValue(objCDAPImplementationList[0].Cdap_phaseno.ToString()).Selected = true;

                    dptxtPeriodFrom.Text = objCDAPImplementationList[0].PeriodFrom.ToString(UtilBO.DateFormat);
                    dptxtPeriodTo.Text = objCDAPImplementationList[0].PeriodTo.ToString(UtilBO.DateFormat);
                    txtExpenditure.Text = UtilBO.CurrencyFormat(objCDAPImplementationList[0].EXPENDITURE);
                    SetPhaseUpdateMode(true);
                }
                pnlActivity.Visible = false;
            }
            else if (e.CommandName == "DeleteRow")
            {
                CDAPImplementationBLL objCDAPImplementationBLL = new CDAPImplementationBLL();
                objCDAPImplementationBLL.DeletePhasedetailsByID(Convert.ToInt32(e.CommandArgument));
                ClearPhaseDetails();
                int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                BindDetailsGrid(ProjectID);
                SetPhaseUpdateMode(false);
                pnlActivity.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Selected Phase Deleted successfully');", true);
            }
            else if (e.CommandName == "EditPhase")
            {
                lblLegendActivity.Text = "Activity Planning for Phase " + ((LinkButton)e.CommandSource).Text.ToString();
                pnlActivity.Visible = true;
                txtCurrentPhase.Text = e.CommandArgument.ToString();
                FetchData(10, 0);
            }
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearPhase_Click(object sender, EventArgs e)
        {
            ClearPhaseDetails();
            SetPhaseUpdateMode(false);
            if (phActivity.Visible == true)
            {
                ClearDetails();
                if (btnClear.Text.ToUpper() == "CANCEL")
                {
                    SetUpdateMode(false);
                }
                phActivity.Visible = false;
                lnkbtnshow.Text = "Add New Activity";
            }
        }

        /// <summary>
        /// To save and update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objCDAPImplementationBO = new CDAPImplementationBO();
            objCDAPImplementationBLL = new CDAPImplementationBLL();
            objCDAPImplementationList = new CDAPImplementationList();
            string str = string.Empty;
            string str1 = string.Empty;
            int result = 0;
            try
            {
                //objCDAPImplementationBO.Cdap_phaseid = Convert.ToInt32(txtCurrentPhase.Text);
                //objCDAPImplementationBO.Cdap_phaseno = Convert.ToInt32(ddlPhase.SelectedValue);
                //objCDAPImplementationBO.PeriodFrom = Convert.ToDateTime(dptxtPeriodFrom.Text);
                //objCDAPImplementationBO.PeriodTo = Convert.ToDateTime(dptxtPeriodTo.Text);
                //objCDAPImplementationBO.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                //objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                string CDAP_PhaseID = txtCurrentPhase.Text;

                foreach (ListItem item in chkboxVillage.Items)
                {
                    if (item.Selected)
                    {
                        str = str + item.Text + ",";
                    }
                }
                str1 = str.TrimEnd(',');
                if (((Button)sender).Text.ToUpper() == "SAVE")
                {
                    objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseID();

                    objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(ViewState["CDAP_PHASEACTIVITYID"]);
                    objCDAPImplementationBO.Cdap_activityid = Convert.ToInt32(ddlActivity.SelectedValue);
                    objCDAPImplementationBO.Cdap_phaseid = Convert.ToInt32(CDAP_PhaseID);
                    objCDAPImplementationBO.District = ddlDistrict.SelectedItem.ToString();
                    objCDAPImplementationBO.County = ddlCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.SubCounty = ddlSubCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.Village = str1;
                    if (txtActivityDetails.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim();
                    else
                        objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim().Substring(0, 800);
                    if (txtModeImplementation.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim();
                    else
                        objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim().Substring(0, 800);

                    if (txtChallenges.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim();
                    else
                        objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim().Substring(0, 800);
                    if (CommentsTextBox.Text.Trim().Length < 800)
                    {
                        objCDAPImplementationBO.Comments = CommentsTextBox.Text.Trim();
                    }
                    else
                        objCDAPImplementationBO.Comments = CommentsTextBox.Text.Trim().Substring(0, 800);
                    objCDAPImplementationBO.Activitydatefrom = Convert.ToDateTime(dpDateFrom.Text);
                    objCDAPImplementationBO.Activitydateto = Convert.ToDateTime(dpDateTo.Text);
                    objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                    result = objCDAPImplementationBLL.AddCDAPPhaseActivity(objCDAPImplementationBO);
                    objCDAPImplementationList.Clear();
                    objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseActivityID();
                    int papresult = 0;
                    foreach (var lst in objCDAPImplementationList)
                    {
                        foreach (GridViewRow gr in dgPapsInvolved.Rows)
                        {
                            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                            if (chkSelect.Checked)
                            {
                                objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(lst.Cdap_phaseactivityid);
                                objCDAPImplementationBO.HhId = Convert.ToInt32(gr.Cells[2].Text);
                                objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                                papresult = objCDAPImplementationBLL.AddCDAPActivityPAPS(objCDAPImplementationBO);
                            }
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data saved successfully');", true);
                }
                else
                {
                    objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(ViewState["CDAP_PHASEACTIVITYID"]);
                    objCDAPImplementationBO.Cdap_activityid = Convert.ToInt32(ddlActivity.SelectedValue);
                    objCDAPImplementationBO.Cdap_phaseid = Convert.ToDecimal(CDAP_PhaseID);
                    objCDAPImplementationBO.District = ddlDistrict.SelectedItem.ToString();
                    objCDAPImplementationBO.County = ddlCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.SubCounty = ddlSubCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.Village = str1;
                    if (txtActivityDetails.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim();
                    else
                        objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim().Substring(0, 800);
                    if (txtModeImplementation.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim();
                    else
                        objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim().Substring(0, 800);

                    if (txtChallenges.Text.Trim().Length < 800)
                        objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim();
                    else
                        objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim().Substring(0, 800);
                    if (CommentsTextBox.Text.Trim().Length < 500)
                    {
                        objCDAPImplementationBO.Comments = CommentsTextBox.Text.Trim();
                    }
                    else
                        objCDAPImplementationBO.Comments = CommentsTextBox.Text.Trim().Substring(0, 500);
                    objCDAPImplementationBO.Activitydatefrom = Convert.ToDateTime(dpDateFrom.Text);
                    objCDAPImplementationBO.Activitydateto = Convert.ToDateTime(dpDateTo.Text);
                    objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                    result = objCDAPImplementationBLL.AddCDAPPhaseActivity(objCDAPImplementationBO);

                    objCDAPImplementationList.Clear();
                    //objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseActivityID();
                    objCDAPImplementationList.Add(objCDAPImplementationBO);
                    int papresult = 0;
                    foreach (var lst in objCDAPImplementationList)
                    {
                        foreach (GridViewRow gr in dgPapsInvolved.Rows)
                        {
                            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                            if (chkSelect.Checked)
                            {
                                objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(lst.Cdap_phaseactivityid);
                                objCDAPImplementationBO.HhId = Convert.ToInt32(gr.Cells[2].Text);
                                objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                                papresult = objCDAPImplementationBLL.AddCDAPActivityPAPS(objCDAPImplementationBO);
                            }
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('DAta updated successfully');", true);
                }

                ClearDetails();
                int pid = Convert.ToInt32(Session["PROJECT_ID"]);
                FetchData(10, 0);
                SetUpdateMode(false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// To save and update Old
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveOld_Click(object sender, EventArgs e)
        {
            objCDAPImplementationBO = new CDAPImplementationBO();
            objCDAPImplementationBLL = new CDAPImplementationBLL();
            objCDAPImplementationList = new CDAPImplementationList();
            string str = string.Empty;
            string str1 = string.Empty;
            int result = 0;
            try
            {
                objCDAPImplementationBO.Cdap_phaseid = Convert.ToInt32(ViewState["CDAP_PHASEID"]);
                objCDAPImplementationBO.Cdap_phaseno = Convert.ToInt32(ddlPhase.SelectedValue);
                objCDAPImplementationBO.PeriodFrom = Convert.ToDateTime(dptxtPeriodFrom.Text);
                objCDAPImplementationBO.PeriodTo = Convert.ToDateTime(dptxtPeriodTo.Text);
                objCDAPImplementationBO.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                string CDAP_PhaseID = objCDAPImplementationBLL.AddCDAPPhase(objCDAPImplementationBO);

                foreach (ListItem item in chkboxVillage.Items)
                {
                    if (item.Selected)
                    {
                        str = str + item.Text + ",";
                    }
                }
                str1 = str.TrimEnd(',');
                if (((Button)sender).Text.ToUpper() == "SAVE")
                {
                    objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseID();


                    foreach (var phsid in objCDAPImplementationList)
                    {
                        objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(ViewState["CDAP_PHASEACTIVITYID"]);
                        objCDAPImplementationBO.Cdap_activityid = Convert.ToInt32(ddlActivity.SelectedValue);
                        objCDAPImplementationBO.Cdap_phaseid = Convert.ToInt32(phsid.Cdap_phaseid);
                        objCDAPImplementationBO.Cdap_phaseid = Convert.ToDecimal(CDAP_PhaseID);
                        objCDAPImplementationBO.District = ddlDistrict.SelectedItem.ToString();
                        objCDAPImplementationBO.County = ddlCounty.SelectedItem.ToString();
                        objCDAPImplementationBO.SubCounty = ddlSubCounty.SelectedItem.ToString();
                        objCDAPImplementationBO.Village = str1;
                        objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim();
                        objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim();
                        objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim();
                        objCDAPImplementationBO.Activitydatefrom = Convert.ToDateTime(dpDateFrom.Text);
                        objCDAPImplementationBO.Activitydateto = Convert.ToDateTime(dpDateTo.Text);
                        objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                        result = objCDAPImplementationBLL.AddCDAPPhaseActivity(objCDAPImplementationBO);
                    }
                    objCDAPImplementationList.Clear();
                    objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseActivityID();
                    int papresult = 0;
                    foreach (var lst in objCDAPImplementationList)
                    {
                        foreach (GridViewRow gr in dgPapsInvolved.Rows)
                        {
                            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                            if (chkSelect.Checked)
                            {
                                objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(lst.Cdap_phaseactivityid);
                                objCDAPImplementationBO.HhId = Convert.ToInt32(gr.Cells[2].Text);
                                objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                                papresult = objCDAPImplementationBLL.AddCDAPActivityPAPS(objCDAPImplementationBO);
                            }
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data saved successfully');", true);
                }
                else
                {
                    objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(ViewState["CDAP_PHASEACTIVITYID"]);
                    objCDAPImplementationBO.Cdap_activityid = Convert.ToInt32(ddlActivity.SelectedValue);
                    objCDAPImplementationBO.Cdap_phaseid = Convert.ToDecimal(CDAP_PhaseID);
                    objCDAPImplementationBO.District = ddlDistrict.SelectedItem.ToString();
                    objCDAPImplementationBO.County = ddlCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.SubCounty = ddlSubCounty.SelectedItem.ToString();
                    objCDAPImplementationBO.Village = str1;
                    objCDAPImplementationBO.Activitydetails = txtActivityDetails.Text.Trim();
                    objCDAPImplementationBO.Modeofimplementation = txtModeImplementation.Text.Trim();
                    objCDAPImplementationBO.Challenges = txtChallenges.Text.Trim();
                    objCDAPImplementationBO.Activitydatefrom = Convert.ToDateTime(dpDateFrom.Text);
                    objCDAPImplementationBO.Activitydateto = Convert.ToDateTime(dpDateTo.Text);
                    objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                    result = objCDAPImplementationBLL.AddCDAPPhaseActivity(objCDAPImplementationBO);

                    objCDAPImplementationList.Clear();
                    //objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseActivityID();
                    objCDAPImplementationList.Add(objCDAPImplementationBO);
                    int papresult = 0;
                    foreach (var lst in objCDAPImplementationList)
                    {
                        foreach (GridViewRow gr in dgPapsInvolved.Rows)
                        {
                            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                            if (chkSelect.Checked)
                            {
                                objCDAPImplementationBO.Cdap_phaseactivityid = Convert.ToInt32(lst.Cdap_phaseactivityid);
                                objCDAPImplementationBO.HhId = Convert.ToInt32(gr.Cells[2].Text);
                                objCDAPImplementationBO.Updatedby = Convert.ToInt32(Session["USER_ID"]);
                                papresult = objCDAPImplementationBLL.AddCDAPActivityPAPS(objCDAPImplementationBO);
                            }
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Data updated successfully');", true);
                }

                ClearDetails();
                int pid = Convert.ToInt32(Session["PROJECT_ID"]);
                BindDetailsGrid(pid);
                SetUpdateMode(false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// To Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (btnClear.Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }

        }
        /// <summary>
        /// Bind contry,subcontry etc Details Based on District
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            //uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            //uplVillage.Update();
        }

        /// <summary>
        /// Bind subcontry etc Details Based on contry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            //uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            //uplVillage.Update();
        }

        /// <summary>
        /// Bind Villages etc Details Based on subcontry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
        }
        /// <summary>
        /// To Check and uncheck all the check boxes under villages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkAllVillages_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in chkboxVillage.Items)
            {
                item.Selected = chkAllVillages.Checked;
            }
            StringBuilder village = new StringBuilder();
            village = chkboxselection();
            BindPAPGrid(village.ToString());
        }
        /// <summary>
        /// get Paps data for selected Villages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder village = new StringBuilder();
            village = chkboxselection();
            BindPAPGrid(village.ToString());
            //upnPAPsInvolved.Update();
        }

        #endregion

        #region GridEvents
        /// <summary>
        /// for Page index Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgPapsInvolved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            dgPapsInvolved.PageIndex = e.NewPageIndex;
            StringBuilder village = new StringBuilder();
            village = chkboxselection();
            BindPAPGrid(village.ToString());
            PopulateCheckedValues();
        }
        /// <summary>
        /// TO edit and delete reoeter data
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            objCDAPImplementationBLL = new CDAPImplementationBLL();
            objCDAPImplementationList = new CDAPImplementationList();
            objCDAPImplementationBO = new CDAPImplementationBO();
            if (e.CommandName == "EditRow")
            {
                ViewState["CDAP_PHASEACTIVITYID"] = e.CommandArgument;

                objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPAPDetails(Convert.ToInt32(ViewState["CDAP_PHASEACTIVITYID"]));

                if (objCDAPImplementationList.Count > 0)
                {
                    ViewState["CDAP_PHASEID"] = objCDAPImplementationList.First().Cdap_phaseid;
                    BindActivity();
                    ddlActivity.ClearSelection();
                    if (ddlActivity.Items.FindByValue(objCDAPImplementationList.First().Cdap_activityid.ToString()) != null)
                        ddlActivity.Items.FindByValue(objCDAPImplementationList.First().Cdap_activityid.ToString()).Selected = true;
                    BindDistrict();
                    ddlDistrict.ClearSelection();
                    if (ddlDistrict.Items.FindByText(objCDAPImplementationList.First().District.ToString()) != null)
                        ddlDistrict.Items.FindByText(objCDAPImplementationList.First().District.ToString()).Selected = true;
                    BindCounties(ddlDistrict.SelectedItem.Value);
                    ddlCounty.ClearSelection();
                    if (ddlCounty.Items.FindByText(objCDAPImplementationList.First().County.ToString()) != null)
                        ddlCounty.Items.FindByText(objCDAPImplementationList.First().County.ToString()).Selected = true;
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    ddlSubCounty.ClearSelection();
                    if (ddlSubCounty.Items.FindByText(objCDAPImplementationList.First().SubCounty.ToString()) != null)
                        ddlSubCounty.Items.FindByText(objCDAPImplementationList.First().SubCounty.ToString()).Selected = true;
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    chkboxVillage.ClearSelection();
                    string[] strvillage = objCDAPImplementationList.First().Village.Split(',');
                    foreach (string vlg in strvillage)
                    {
                        if (chkboxVillage.Items.FindByText(vlg.ToString()) != null)
                            chkboxVillage.Items.FindByText(vlg.ToString()).Selected = true;
                    }
                    txtActivityDetails.Text = objCDAPImplementationList.First().Activitydetails;
                    txtModeImplementation.Text = objCDAPImplementationList.First().Modeofimplementation;
                    txtChallenges.Text = objCDAPImplementationList.First().Challenges;
                    CommentsTextBox.Text = objCDAPImplementationList.First().Comments;
                    dpDateFrom.Text = objCDAPImplementationList.First().Activitydatefrom.ToString(UtilBO.DateFormat);
                    dpDateTo.Text = objCDAPImplementationList.First().Activitydateto.ToString(UtilBO.DateFormat);

                    StringBuilder hhidno = new StringBuilder();

                    foreach (var item1 in objCDAPImplementationList)
                    {
                        hhidno.Append(item1.HhId + ",");
                    }
                    hhidno = hhidno.Remove(hhidno.Length - 1, 1);
                    ViewState["hhid"] = hhidno;

                    string vlg1 = objCDAPImplementationList.First().Village;
                    objCDAPImplementationList.Clear();
                    objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPVillageID(vlg1);

                    StringBuilder VillageSBID = new System.Text.StringBuilder();

                    foreach (var item in objCDAPImplementationList)
                    {
                        VillageSBID.Append(item.Villageid + ",");
                    }
                    if (VillageSBID.Length > 0)
                    {
                        VillageSBID = VillageSBID.Remove(VillageSBID.Length - 1, 1);
                        BindPAPGrid(VillageSBID.ToString());
                    }

                    string[] t = hhidno.ToString().Split(',');

                    foreach (GridViewRow gr in dgPapsInvolved.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                        foreach (var tt in t)
                        {
                            if (gr.Cells[2].Text == tt.ToString())
                            {
                                chkSelect.Checked = true;
                            }
                        }
                    }
                }
                SetUpdateMode(true);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Populate Checke Boxes based on values
        /// </summary>
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in dgPapsInvolved.Rows)
                {
                    int index = (int)dgPapsInvolved.DataKeys[gvrow.RowIndex].Value;
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkSelect");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Save Selected paps sata into data base
        /// </summary>
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            int index = -1;
            foreach (GridViewRow gvrow in dgPapsInvolved.Rows)
            {
                index = (int)dgPapsInvolved.DataKeys[gvrow.RowIndex].Value;
                bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;

                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }
        /// <summary>
        /// get selected villages
        /// </summary>
        /// <returns></returns>
        public StringBuilder chkboxselection()
        {
            StringBuilder VillageSB = new System.Text.StringBuilder();

            if (chkboxVillage.SelectedIndex >= 0)
            {
                if (chkboxVillage.SelectedItem.Selected == true)
                {
                    foreach (ListItem item in chkboxVillage.Items)
                    {
                        if (item.Selected)
                        {
                            VillageSB.Append(item.Value + ",");
                        }
                    }
                }

                VillageSB = VillageSB.Remove(VillageSB.Length - 1, 1);
            }

            return VillageSB;
        }
        /// <summary>
        /// Bind values to drop down ddlActivity
        /// </summary>
        private void BindActivity()
        {
            objActivityBLL = new ActivityBLL();
            objActivityList = objActivityBLL.GetActivity();
            if (objActivityList.Count > 0)
            {
                ddlActivity.DataSource = objActivityList;
                ddlActivity.DataTextField = "Cdap_activityname";
                ddlActivity.DataValueField = "Cdap_activityid";
                ddlActivity.DataBind();
                ddlActivity.Items.Insert(0, "Select");
            }
        }
        /// <summary>
        /// Bind values to drop down ddlDistrict
        /// </summary>
        private void BindDistrict()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }

        /// <summary>
        /// Bind values to drop down ddlCounty
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
            ddlCounty.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind values to drop down ddlSubCounty
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
            ddlSubCounty.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind values to Check box list chkboxVillage
        /// </summary>
        private void BindVillages(string subCounty)
        {
            //  ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);

            chkboxVillage.Items.Clear();

            if (subCounty != "0")
            {
                chkboxVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                chkboxVillage.DataTextField = "VillageName";
                chkboxVillage.DataValueField = "VillageID";
                chkboxVillage.DataBind();
            }
        }
        /// <summary>
        /// Bind Pap data to grid Based on selected Villages
        /// </summary>
        /// <param name="villages"></param>
        private void BindPAPGrid(string villages)
        {
            objPAP_HouseholdBLL = new PAP_HouseholdBLL();
            Trn_Pap_HouseHoldList objlist = objPAP_HouseholdBLL.GetPAPNameByVillage(villages);
            dgPapsInvolved.DataSource = objlist;
            dgPapsInvolved.DataBind();

            if (objlist.Count > 10)
                pnlPaps.Height = Unit.Pixel(200);
            else
                pnlPaps.Height = Unit.Pixel((objlist.Count + 1) * 20);

            lblNoofPAPS.Text = objlist.Count.ToString();
            //objTrn_Pap_HouseHoldList = objPAP_HouseholdBLL.GetPAPNameByVillage(villages);
            //if (objTrn_Pap_HouseHoldList.Count > 0)
            //{
            //    dgPapsInvolved.DataSource = objTrn_Pap_HouseHoldList;
            //    dgPapsInvolved.DataBind();
            //}
            //else
            //{

            //    dgPapsInvolved.DataSource = objTrn_Pap_HouseHoldList;
            //    dgPapsInvolved.DataBind();
            //}
        }
        /// <summary>
        /// Bind Phases Data to grdPhases
        /// </summary>
        /// <param name="prjctID"></param>
        private void BindDetailsGrid(int prjctID)
        {
            objCDAPImplementationBLL = new CDAPImplementationBLL();
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseDetails(ProjectID);
            grdPhases.DataSource = objCDAPImplementationList;
            grdPhases.DataBind();
        }
        /// <summary>
        /// Set Update mode for buttons
        /// </summary>
        /// <param name="updateMode"></param>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
                phActivity.Visible = true;
                lnkbtnshow.Text = "Hide Activity";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["CDAP_PHASEID"] = 0;
                ViewState["CDAP_PHASEACTIVITYID"] = 0;
            }
        }
        /// <summary>
        /// To Clear data
        /// </summary>
        private void ClearDetails()
        {
            ListItem lstItem = null;
            ddlActivity.SelectedIndex = 0;
            ddlDistrict.ClearSelection();
            ddlDistrict.SelectedIndex = 0;

            lstItem = ddlCounty.Items[0];
            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlSubCounty.Items[0];
            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            chkboxVillage.Items.Clear();

            txtActivityDetails.Text = string.Empty;
            txtModeImplementation.Text = string.Empty;
            txtChallenges.Text = string.Empty;
            CommentsTextBox.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + dpDateFrom.ClientID + "'); ClearDateField('" + dpDateTo.ClientID + "');", true);

            dgPapsInvolved.DataSource = null;
            dgPapsInvolved.DataBind();

            if (dgPapsInvolved.Rows.Count > 10)
                pnlPaps.Height = Unit.Pixel(200);
            else
                pnlPaps.Height = Unit.Pixel(22);
            
            lblNoofPAPS.Text = "0";
        }
        /// <summary>
        /// Bind Data to repeater rptDetails
        /// </summary>
        /// <param name="take"></param>
        /// <param name="pageSize"></param>
        private void FetchData(int take, int pageSize)
        {
            PagedDataSource page = new PagedDataSource();
            page.AllowCustomPaging = true;
            page.AllowPaging = true;
            objCDAPImplementationBLL = new CDAPImplementationBLL();
            int prid = Convert.ToInt32(Session["PROJECT_ID"]);
            objCDAPImplementationList = objCDAPImplementationBLL.GetCDAPPhaseActivityDetails(prid, Convert.ToInt32(txtCurrentPhase.Text));

            page.DataSource = objCDAPImplementationList;
            page.PageSize = 10;
            rptDetails.DataSource = page;
            rptDetails.DataBind();
            if (objCDAPImplementationList.Count == 0)
            {
                phActivity.Visible = true;
                lnkbtnshow.Text = "Hide Activity";
            }
            else
            {
                phActivity.Visible = false;
                lnkbtnshow.Text = "Add New Activity";
            }
            if (!IsPostBack)
            {
                RowCount = objCDAPImplementationList.Count;
            }
        }
        /// <summary>
        /// Get data for selected Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbl_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int currentPage = int.Parse(lnk.Text);
            int take = currentPage * 10;
            int skip = currentPage == 1 ? 0 : take - 10;
            FetchData(take, skip);
        }

        #endregion
        /// <summary>
        /// Set Values to Literals inside repeater rptDetails (like Date format etc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DateTime activityDateFrom = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Activitydatefrom"));
                if (activityDateFrom != DateTime.MinValue)
                    ((Literal)e.Item.FindControl("litActivityDateFrom")).Text = activityDateFrom.ToString(UtilBO.DateFormat);

                DateTime activityDateTo = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Activitydateto"));
                if (activityDateTo != DateTime.MinValue)
                    ((Literal)e.Item.FindControl("litActivityDateTo")).Text = activityDateTo.ToString(UtilBO.DateFormat);


                String papNames = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PapNames"));
                String villages = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Village"));
                if ((papNames != "" || papNames != string.Empty) && (villages != "" || villages != string.Empty))
                    ((Literal)e.Item.FindControl("ltrPapName")).Text = papNames.ToString();
                else
                    ((Literal)e.Item.FindControl("ltrPapName")).Text = "No PAPS available in the selected villages";





                //add
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMMUNITY_DEVELOPMENT) == false)
                {

                    ((ImageButton)e.Item.FindControl("imgEdit")).Visible = false;
                }


            }

            if (e.Item.ItemType == ListItemType.Header)
            {
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMMUNITY_DEVELOPMENT) == false)
                {
                    ((Literal)e.Item.FindControl("ltEdit")).Visible = false;
                }
            }

        }




    }
}