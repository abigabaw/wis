using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class Welfare : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.Welfare;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.GeneralWelfareIndicatorsfromGovernmentsurvey;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.WelfareDetails;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["PROJECT_ID"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Welfare";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                chkDoYouFish.Attributes.Add("onclick", string.Format("EnableFishing(this,'{0}','{1}');", txtWhereDoYouFish.ClientID, txtHowOftenFish.ClientID));
                chkDoYouHunt.Attributes.Add("onclick", string.Format("EnableHunting(this,'{0}');", txtWhereHunt.ClientID));

                txtWhereDoYouFish.Enabled = false;
                txtHowOftenFish.Enabled = false;
                txtWhereHunt.Enabled = false;
                BindWelfareMaster();
                GetPAPWelfares();
                GetPAPVoluntrayWelfares();
                ProjectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }                
                
              
            }
        }
        /// <summary>
        /// Set default buttons
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

        #region Frozen / Approval / Decline / Pending
        /// <summary>
        /// to check project Frozen or not
        /// </summary>
        public void ProjectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    checkApprovalExitOrNot();
                }
            }
        }
        /// <summary>
        /// to apporval exist or not for change request
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusWelfare.Text = "";
            StatusWelfare.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-WE");
                lnkWelfare.Attributes.Add("onclick", paramChangeRequest);
                lnkWelfare.Visible = true;
            }
            else
            {
                lnkWelfare.Visible = false;
            }
            #endregion
            getApprrequtStatusWelfare();
        }
        /// <summary>
        /// to Check request status
        /// </summary>
        public void ChangeRequestStatusWelfare()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-WE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to Check request status
        /// </summary>
        public void getApprrequtStatusWelfare()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-WE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkWelfare.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusWelfare.Visible = true;
                    StatusWelfare.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkWelfare.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusWelfare.Visible = false;
                    StatusWelfare.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkWelfare.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusWelfare.Visible = false;
                }
            }
        }
        #endregion
        /// <summary>
        /// To Get Welfare Voluntary data for seleted pap
        /// </summary>
        protected void GetPAPVoluntrayWelfares()
        {
            WelfareVoluntaryBO objVoluntary = (new SocioEconomyBLL()).GetWelfareVoluntary(Convert.ToInt32(Session["HH_ID"]));

            if (objVoluntary != null)
            {
                txtWhereGetDrinkingWater.Text = objVoluntary.WhereGetDrinkingWater;
                txtWaterSourceDistance.Text = objVoluntary.WaterSourceDistance;

                if (objVoluntary.DoYouFish == "YES")
                {
                    chkDoYouFish.Checked = true;
                    txtWhereDoYouFish.Enabled = true;
                    txtHowOftenFish.Enabled = true;
                }
                else
                {
                    txtWhereDoYouFish.Enabled = false;
                    txtHowOftenFish.Enabled = false;
                }


                txtWhereDoYouFish.Text = objVoluntary.WhereDoYouFish;
                txtHowOftenFish.Text = objVoluntary.HowOftenFish;

                if (objVoluntary.DoYouHunt == "YES")
                {
                    chkDoYouHunt.Checked = true;
                    txtWhereHunt.Enabled = true;
                }
                else
                    txtWhereHunt.Enabled = false;

                txtWhereHunt.Text = objVoluntary.WhereHunt;

                if (objVoluntary.Firewood == "YES")
                    chkFirewood.Checked = true;

                if (objVoluntary.Charcoal == "YES")
                    chkCharcoal.Checked = true;

                if (objVoluntary.Paraffin == "YES")
                    chkParaffin.Checked = true;

                if (objVoluntary.Electricity == "YES")
                    chkElectricity.Checked = true;
                else
                    objVoluntary.Electricity = "NO";

                if (objVoluntary.Gas == "YES")
                    chkGas.Checked = true;
                else
                    objVoluntary.Gas = "NO";

                if (objVoluntary.Solar == "YES")
                    chkSolar.Checked = true;
                else
                    objVoluntary.Solar = "NO";

                if (objVoluntary.Biogas == "YES")
                    chkBiogas.Checked = true;
                else
                    objVoluntary.Biogas = "NO";

                if (objVoluntary.OtherFuel == "YES")
                    chkOtherFuel.Checked = true;

                txtComments.Text = objVoluntary.Comments;
            }
        }
        /// <summary>
        /// Get PAP Welfare details
        /// </summary>
        protected void GetPAPWelfares()
        {
            GeneralWelfareList Welfares = (new SocioEconomyBLL()).GetGeneralWelfares(Convert.ToInt32(Session["HH_ID"]));

            CheckBox chkWelfareType = null;
            TextBox txtWelfareType = null;
            Label lblWelfareName = null;
            int welfareMasterID = 0;
            string[] arr = new string[lstWelfares.Items.Count+1];
            int i = 0;
            foreach (DataListItem lstItem in lstWelfares.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    welfareMasterID = Convert.ToInt32(((Literal)lstItem.FindControl("litWelfareID")).Text);
                    chkWelfareType = (CheckBox)lstItem.FindControl("chkWelfareType");
                    txtWelfareType = (TextBox)lstItem.FindControl("txtWelfareType");
                    lblWelfareName = (Label)lstItem.FindControl("lblWelfareName");

                    foreach (GeneralWelfareBO objWelfare in Welfares)
                    {
                        if (objWelfare.WelfareIndicatorID == welfareMasterID)
                        {
                            if (txtWelfareType.Visible)
                            {
                                txtWelfareType.Text = objWelfare.FieldValue;
                                txtWelfareType.Enabled = false;
                                if (arr.Length > 0 && i > 0)
                                {
                                    string param = string.Format("EnableOtherTransEquipment('{0}','{1}');", arr[i - 1].ToString(), txtWelfareType.ClientID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "enable", param, true);
                                }
                            }
                            else if (chkWelfareType.Visible && objWelfare.FieldValue == "TRUE")
                            {
                                chkWelfareType.Checked = true;
                                if (chkWelfareType.Checked)
                                {
                                    arr[i] = chkWelfareType.ClientID;
                                    i++;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// bind data to list lstWelfares
        /// </summary>
        private void BindWelfareMaster()
        {
            lstWelfares.DataSource = (new SocioEconomyBLL()).GetGeneralWelfareMasters();
            lstWelfares.DataBind();

            // Find the check box that is associated with the text box and bind them.
            foreach (DataListItem lstWelfareItem in lstWelfares.Items)
            {
                if (lstWelfareItem.ItemType == ListItemType.Item || lstWelfareItem.ItemType == ListItemType.AlternatingItem)
                {
                    Literal litAssociatedWith = (Literal)lstWelfareItem.FindControl("litAssociatedWith");

                    if (litAssociatedWith.Text.Trim() != "" && litAssociatedWith.Text.Trim() != "0")
                    {
                        Literal litWelfareID = (Literal)lstWelfareItem.FindControl("litWelfareID");
                        TextBox txtWelfareType = (TextBox)lstWelfareItem.FindControl("txtWelfareType");

                        foreach (DataListItem lstWelfareItemChk in lstWelfares.Items)
                        {
                            if (lstWelfareItemChk.ItemType == ListItemType.Item || lstWelfareItemChk.ItemType == ListItemType.AlternatingItem)
                            {
                                Literal litWelfareIDCHK = (Literal)lstWelfareItemChk.FindControl("litWelfareID");

                                if (litWelfareIDCHK.Text == litAssociatedWith.Text)
                                {
                                    CheckBox chkWelfareType = (CheckBox)lstWelfareItemChk.FindControl("chkWelfareType");
                                    chkWelfareType.Attributes.Add("onclick", string.Format("EnableOtherTransEquipment(this,'{0}');", txtWelfareType.ClientID));
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// to set status of Check box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstWelfares_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string fieldType = DataBinder.Eval(e.Item.DataItem, "FIELDTYPE").ToString();

                if (fieldType.ToUpper() == "TEXTBOX")
                {
                    CheckBox chkWelfareType = (CheckBox)e.Item.FindControl("chkWelfareType");
                    chkWelfareType.Visible = false;
                    ((Label)e.Item.FindControl("lblCHKWelfareTypeMsg")).Visible = false;

                    TextBox txtWelfareType = (TextBox)e.Item.FindControl("txtWelfareType");
                    txtWelfareType.Visible = true;
                    txtWelfareType.Enabled = false;

                    Label lblWelfareName = (Label)e.Item.FindControl("lblWelfareName");
                    lblWelfareName.Width = Unit.Pixel(600);
                }
                //else
                //{
                //    CheckBox chkWelfareType = (CheckBox)e.Item.FindControl("chkWelfareType");
                //    Label lblWelfareName = (Label)e.Item.FindControl("lblWelfareName");
                //}
            }
        }
        /// <summary>
        /// To sava and updata data into data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GeneralWelfareList GeneralWelfares = new GeneralWelfareList();
            GeneralWelfareBO objWelfare = null;
            CheckBox chkWelfareType = null;
            TextBox txtWelfareType = null;
            string[] arr = new string[lstWelfares.Items.Count + 1];
            int i = 0;
            foreach (DataListItem lstItem in lstWelfares.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    chkWelfareType = (CheckBox)lstItem.FindControl("chkWelfareType");
                    txtWelfareType = (TextBox)lstItem.FindControl("txtWelfareType");

                    objWelfare = new GeneralWelfareBO();
                    objWelfare.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                    objWelfare.WelfareIndicatorID = Convert.ToInt32(((Literal)lstItem.FindControl("litWelfareID")).Text);

                    if (txtWelfareType.Visible)
                    {
                        objWelfare.FieldValue = txtWelfareType.Text;
                        txtWelfareType.Enabled = false;
                        if (arr.Length > 0 && i > 0)
                        {
                            string param = string.Format("EnableOtherTransEquipment('{0}','{1}');", arr[i - 1].ToString(), txtWelfareType.ClientID);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "enable", param, true);
                        }
                    }
                    else if (chkWelfareType.Visible)
                    {
                        if (chkWelfareType.Checked)
                            objWelfare.FieldValue = "TRUE";
                        else
                            objWelfare.FieldValue = "FALSE";

                        if (chkWelfareType.Checked)
                        {
                            arr[i] = chkWelfareType.ClientID;
                            i++;
                        }
                    }
                    else
                    {
                        objWelfare.FieldValue = "";
                    }

                    objWelfare.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                    GeneralWelfares.Add(objWelfare);
                }
            }

            (new SocioEconomyBLL()).UpdatePAPWelfare(GeneralWelfares);

            GeneralWelfares = null;

            WelfareVoluntaryBO objVoluntary = new WelfareVoluntaryBO();

            objVoluntary.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
            objVoluntary.WhereGetDrinkingWater = txtWhereGetDrinkingWater.Text.Trim();
            objVoluntary.WaterSourceDistance = txtWaterSourceDistance.Text.Trim();

            if (chkDoYouFish.Checked)
                objVoluntary.DoYouFish = "YES";
            else
                objVoluntary.DoYouFish = "NO";
            if (chkDoYouFish.Checked)
            {
                txtWhereDoYouFish.Enabled = true;
                txtHowOftenFish.Enabled = true;
            }
            objVoluntary.WhereDoYouFish = txtWhereDoYouFish.Text.Trim();
            objVoluntary.HowOftenFish = txtHowOftenFish.Text.Trim();

            if (chkDoYouHunt.Checked)
                objVoluntary.DoYouHunt = "YES";
            else
                objVoluntary.DoYouHunt = "NO";
            if (chkDoYouHunt.Checked)
            {
                txtWhereHunt.Enabled = true;
            }
            objVoluntary.WhereHunt = txtWhereHunt.Text.Trim();

            if (chkFirewood.Checked)
                objVoluntary.Firewood = "YES";
            else
                objVoluntary.Firewood = "NO";

            if (chkCharcoal.Checked)
                objVoluntary.Charcoal = "YES";
            else
                objVoluntary.Charcoal = "NO";

            if (chkParaffin.Checked)
                objVoluntary.Paraffin = "YES";
            else
                objVoluntary.Paraffin = "NO";

            if (chkElectricity.Checked)
                objVoluntary.Electricity = "YES";
            else
                objVoluntary.Electricity = "NO";

            if (chkGas.Checked)
                objVoluntary.Gas = "YES";
            else
                objVoluntary.Gas = "NO";

            if (chkSolar.Checked)
                objVoluntary.Solar = "YES";
            else
                objVoluntary.Solar = "NO";

            if (chkBiogas.Checked)
                objVoluntary.Biogas = "YES";
            else
                objVoluntary.Biogas = "NO";

            if (chkOtherFuel.Checked)
                objVoluntary.OtherFuel = "YES";
            else
                objVoluntary.OtherFuel = "NO";

            objVoluntary.Comments = txtComments.Text.Trim();
            if (objVoluntary.Comments.Length > 1000)
                objVoluntary.Comments = objVoluntary.Comments.Substring(0, 999);

            objVoluntary.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

            (new SocioEconomyBLL()).UpdatePAPWelfareVoluntary(objVoluntary);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
            ChangeRequestStatusWelfare();
            ProjectFrozen();
           
        }
        /// <summary>
        /// to clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        /// <summary>
        /// to clear data
        /// </summary>
        private void ClearFields()
        {
            CheckBox chkWelfareType = null;
            TextBox txtWelfareType = null;
            foreach (DataListItem lstItem in lstWelfares.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    chkWelfareType = (CheckBox)lstItem.FindControl("chkWelfareType");
                    txtWelfareType = (TextBox)lstItem.FindControl("txtWelfareType");

                    if (txtWelfareType.Visible)
                    {
                        txtWelfareType.Text = string.Empty;
                    }
                    else if (chkWelfareType.Visible)
                    {
                        chkWelfareType.Checked = false;
                    }
                }
            }

            txtWhereGetDrinkingWater.Text = string.Empty;
            txtWaterSourceDistance.Text = string.Empty;

            chkDoYouFish.Checked = false;
            txtWhereDoYouFish.Text = string.Empty;
            txtHowOftenFish.Text = string.Empty;

            chkDoYouHunt.Checked = false;
            txtWhereHunt.Text = string.Empty;
            chkFirewood.Checked = false;

            chkFirewood.Checked = false;
            chkCharcoal.Checked = false;
            chkBiogas.Checked = false;
            chkGas.Checked = false;
            chkParaffin.Checked = false;
            chkOtherFuel.Checked = false;
            chkElectricity.Checked = false;
            chkSolar.Checked = false;

            txtComments.Text = string.Empty;
        }
    }
}