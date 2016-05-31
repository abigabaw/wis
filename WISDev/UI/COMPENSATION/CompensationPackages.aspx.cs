 using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CompensationPackages : System.Web.UI.Page
    {
        #region Global Declaration & Page Loading
        string documentCode = string.Empty;

        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Request.QueryString["HHID"] != null)
                    Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                else
                    Session["HH_ID"] = null;
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            Page.Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Compensation Packages";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                checkApprovalExitOrNot();
                LoadComponestion();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PACKAGE_DOCUMENTS) == false)
                {
                    lnkValuationPCI.Visible = false;
                    foreach (RepeaterItem item1 in rptPKGDoccatName.Items)
                    {
                        Repeater rptDOCitem = (Repeater)item1.FindControl("rptDOCitem");
                        thPrint.Visible = false;
                        thApprover.Visible = false;  
                        foreach (RepeaterItem item in rptDOCitem.Items)
                        {
                            System.Web.UI.HtmlControls.HtmlTableCell tdPrintButton = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdPrintButton");
                            System.Web.UI.HtmlControls.HtmlTableCell tdApproverButton = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdApproverButton");
                            tdPrintButton.Visible = false;
                            tdApproverButton.Visible = false;
                        }
                    }
                }
            }

            if (Mode == "Readonly")
            {
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkValuationPCI.Visible = false;
                
                foreach (RepeaterItem item1 in rptPKGDoccatName.Items)
                {
                    Repeater rptDOCitem = (Repeater)item1.FindControl("rptDOCitem");
                    thPrint.Visible = false;
                    thApprover.Visible = false;
                    thStatus.Visible = true;
                    foreach (RepeaterItem item in rptDOCitem.Items)
                    {
                        System.Web.UI.HtmlControls.HtmlTableCell tdPrintButton = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdPrintButton");
                        System.Web.UI.HtmlControls.HtmlTableCell tdApproverButton = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdApproverButton");
                        System.Web.UI.HtmlControls.HtmlTableCell tdStatusButton = (System.Web.UI.HtmlControls.HtmlTableCell)item.FindControl("tdStatusButton");
                        tdPrintButton.Visible = false;
                        tdApproverButton.Visible = false;
                        tdStatusButton.Visible = true;
                    }
                }
            }
        }
        #endregion Global Declaration & Page Loading

        #region Load Default Data
        /// <summary>
        /// TO Bind Pkgs Data TO rptPKGDoccatName
        /// </summary>
        public void LoadComponestion()
        {
            CompensationPackagesBLL COMPACKBLLobj = new CompensationPackagesBLL();
            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            try
            {
                int HHID = 0;
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                }
                else
                {
                    Response.Redirect("~/UI/Compensation/PAPList.aspx");
                }

                COMPACKList = COMPACKBLLobj.GetComponestionbyId(HHID);

                rptPKGDoccatName.DataSource = COMPACKList;
                rptPKGDoccatName.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                COMPACKBLLobj = null;
            }
        }
        /// <summary>
        /// Set Attributes to Link buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptDOCitem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string Mode = string.Empty;
                string PageCode = string.Empty;
                documentCode = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PKGDocumentCode"));

                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPkgItem = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("lnkViewPkgItem");
                if (Request.QueryString["Mode"] != null)
                {
                    Mode = Request.QueryString["Mode"].ToString();
                }
                if (Request.QueryString["PageCode"] != null)
                {
                    PageCode = Request.QueryString["PageCode"].ToString();
                }
                if (Mode == "Readonly")
                {
                    int ApprovalLevel = 0;
                    ApprovalLevel = Convert.ToInt32(Request.QueryString["ApprovalLevel"].ToString());
                    lnkViewPkgItem.Attributes.Add("onclick", string.Format("OpenApprovalReport('{0}',{1},'{2}',{3},'{4}');", documentCode, Convert.ToInt32(Session["HH_ID"].ToString()), "Approval", ApprovalLevel, PageCode));
                }
                else
                {
                    lnkViewPkgItem.Attributes.Add("onclick", string.Format("OpenReport('{0}',{1});", documentCode, Convert.ToInt32(Session["HH_ID"].ToString())));
                }
                System.Web.UI.HtmlControls.HtmlAnchor LnkprintApproved = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("LnkprintApproved");

                LnkprintApproved.Attributes.Add("onclick", string.Format("OpenPrintReport('{0}',{1});", documentCode, Convert.ToInt32(Session["HH_ID"].ToString())));

                LnkprintApproved.Visible = false;

                #region for Approval Task

                System.Web.UI.HtmlControls.HtmlAnchor lnkApprovalTAsk = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("lnkApprovalTAsk");
                PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
                PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
                objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                int householdID = Convert.ToInt32(Session["HH_ID"]);
                objHouseHold.HhId = householdID;
                objHouseHold.PageCode = "CPREV";
                objHouseHold.Workflowcode = UtilBO.PackagePaymentRequestCode;

                objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

                //int countResult = totalcountapproval();

                if (objHouseHold != null)
                {
                    if (objHouseHold.ApproverStatus == 3)//(objHouseHold.ApproverStatus == 3)
                    {//PENDING
                        lnkApprovalTAsk.Visible = false;
                        LnkprintApproved.Visible = false;
                    }
                    if (objHouseHold.ApproverStatus == 2)// (objHouseHold.ApproverStatus == 2)
                    {
                        //DECLINED
                        lnkApprovalTAsk.Visible = false;
                        LnkprintApproved.Visible = false;
                    }
                    if (objHouseHold.ApproverStatus == 1)// (objHouseHold.ApproverStatus == 1)
                    {
                        //APPROVED
                        lnkApprovalTAsk.Visible = false;
                        LnkprintApproved.Visible = true;
                    }
                    //if (countResult == 0 || countResult == 3)
                    //{
                    //    try
                    //    {
                    //        WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                    //        WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                    //        string ChangeRequestCode = UtilBO.CompensationPrintRequest;

                    //        objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

                    //        int userID = Convert.ToInt32(Session["USER_ID"]);
                    //        int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    //        int HHID = Convert.ToInt32(Session["HH_ID"]);
                    //        string pageCode = documentCode;

                    //        if (objWorkFlowBO != null)
                    //        {
                    //            string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                    //            lnkApprovalTAsk.Attributes.Add("onclick", paramChangeRequest);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw ex;
                    //    }
                    //}
                }
                else
                {
                    //try
                    //{
                    //    WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                    //    WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                    //    string ChangeRequestCode = UtilBO.CompensationPrintRequest;

                    //    objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

                    //    int userID = Convert.ToInt32(Session["USER_ID"]);
                    //    int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    //    int HHID = Convert.ToInt32(Session["HH_ID"]);
                    //    string pageCode = documentCode;

                    //    if (objWorkFlowBO != null)
                    //    {
                    //        string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                    //        lnkApprovalTAsk.Attributes.Add("onclick", paramChangeRequest);
                    //        lnkApprovalTAsk.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        lnkApprovalTAsk.Visible = false;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
                }

                #endregion

                #region For PageLevel Approver: Package Review
                if (ViewState["ApproveExists"] != null)
                {
                    System.Web.UI.HtmlControls.HtmlTableCell tdPrintButton = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdPrintButton");
                    System.Web.UI.HtmlControls.HtmlTableCell tdApproverButton = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdApproverButton");

                    if (ViewState["ApproveExists"] != null && ViewState["ApproveExists"].ToString() == "Yes")
                    {
                        PAP_HouseholdBLL oHouseHoldBLL = new PAP_HouseholdBLL();
                        PAP_HouseholdBO oHouseHold = new PAP_HouseholdBO();

                        oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                        int HHID = Convert.ToInt32(Session["HH_ID"]);
                        oHouseHold.HhId = householdID;
                        oHouseHold.PageCode = "CPREV";
                        oHouseHold.Workflowcode = UtilBO.PackagePaymentRequestCode;

                        oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);

                        if (oHouseHold != null)
                        {

                            if (oHouseHold.ApproverStatus == 3)
                            {
                                //PENDING
                                thPrint.Visible = false;
                                thApprover.Visible = false;
                                tdPrintButton.Visible = false;
                                tdApproverButton.Visible = false;
                            }
                            else if (oHouseHold.ApproverStatus == 2)
                            {
                                //DECLINED
                                thPrint.Visible = true;
                                
                                thApprover.Visible = false;
                                tdPrintButton.Visible = true;
                                tdApproverButton.Visible = false;
                            }
                            else if (oHouseHold.ApproverStatus == 1)
                            {
                                //APPROVED
                                thPrint.Visible = true;
                                thApprover.Visible = false;
                                tdPrintButton.Visible = true;
                                tdApproverButton.Visible = false;
                            }
                        }
                        else
                        {
                            thPrint.Visible = true;
                            thApprover.Visible = false;
                            tdPrintButton.Visible = true;
                            tdApproverButton.Visible = false;
                        }
                    }
                }
                #endregion For PageLevel Approver: Package Review
            }
        }
        /// <summary>
        /// Set data to Child Repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptPKGDoccatName_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hdnCategoryID = e.Item.FindControl("hdnCATpkgdoccatID") as HiddenField;
            Repeater rpChild = e.Item.FindControl("rptDOCitem") as Repeater;//Child Repeater

            CompensationPackagesBLL COMPACKBLLobj = new CompensationPackagesBLL();
            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            COMPACKList = COMPACKBLLobj.getComponestion(Convert.ToInt32(Session["HH_ID"]), Convert.ToInt32(hdnCategoryID.Value), Convert.ToInt32(Session["USER_ID"]));

            rpChild.DataSource = COMPACKList;
            rpChild.DataBind();
        }
        #endregion Load Default Data

        #region Change Request Approval

        /// <summary>
        /// Check Approvar Exist or not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            lblCompensationPackageStatus.Text = string.Empty;
            lblCompensationPackageStatus.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.PackagePaymentRequestCode;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "CPREV";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                lnkValuationPCI.Attributes.Add("onclick", paramChangeRequest);

                lnkValuationPCI.Visible = true;
                ViewState["ApproveExists"] = "Yes";
            }
            else
            {
                lnkValuationPCI.Visible = false;
                ViewState["ApproveExists"] = "No";
                lblCompensationPackageStatus.Visible = true;
                lblCompensationPackageStatus.ForeColor = System.Drawing.Color.Red;
                lblCompensationPackageStatus.Text = "Package Review Approver Not Defined";
            }
            #endregion
            getAppoverReqStatusPakClos();

        }
        /// <summary>
        /// Che Approval Status
        /// </summary>
        public void getAppoverReqStatusPakClos()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();

            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "CPREV";
            objHouseHold.Workflowcode = UtilBO.PackagePaymentRequestCode;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    //PENDING
                    lnkValuationPCI.Visible = false;
                    lblCompensationPackageStatus.Visible = true;
                    lblCompensationPackageStatus.ForeColor = System.Drawing.Color.Red;
                    lblCompensationPackageStatus.Text = "Sent for Package Review";
                }
                else if (objHouseHold.ApproverStatus == 2)
                {
                    //DECLINED
                    lnkValuationPCI.Visible = true;
                    lblCompensationPackageStatus.Visible = true;
                    lblCompensationPackageStatus.ForeColor = System.Drawing.Color.Red;
                    lblCompensationPackageStatus.Text = "Package Reviewed and Declined";
                }
                else if (objHouseHold.ApproverStatus == 1)
                {
                    //APPTOVED
                    lnkValuationPCI.Visible = false;
                    lblCompensationPackageStatus.Visible = false;
                    lblCompensationPackageStatus.Visible = true;
                    //need to add print button
                    lblCompensationPackageStatus.ForeColor = System.Drawing.Color.Green;
                    lblCompensationPackageStatus.Text = "Package Reviewed and Approved";
                }
            }
            //else
            //{
            //    //lblCompensationPackageStatus.Visible = true;
            //    //lblCompensationPackageStatus.Text = "Pending Package Review";
            //}
        }
        #endregion Change Request Approval

        public void getApprovalChangerequestStatus(string pageCode)
        {

        }
        /// <summary>
        /// To Update Document as read or unread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsRead_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                Label lblDocID = (Label)chk.Parent.FindControl("lblDocID");
                CompensationPackagesBLL objBLL = new CompensationPackagesBLL();
                objBLL.UpdateDocReadStatus(Convert.ToInt32(lblDocID.Text.Trim()), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]), Convert.ToInt32(Session["HH_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully.";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Che Approval Status
        /// </summary>
        /// <returns></returns>
        public int totalcountapproval()
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;

            string ChangeRequestCode = UtilBO.CompensationPrintRequest;

            objProjectRoute.WorkFlowApprover = ChangeRequestCode;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"].ToString());

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    int householdID = Convert.ToInt32(Session["HH_ID"].ToString());
                    objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = documentCode;
                    objProjectRoute.WorkflowCode = UtilBO.CompensationPrintRequest;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    if (objPrintApprovalWF != null)
                    {
                        if (objWorkFlowList[i].CountApproval == objPrintApprovalWF.LEVEL)
                        {
                            if (objPrintApprovalWF.ApprovalstatusID == 1)
                            {
                                finalcount = 1;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 2)
                            {
                                finalcount = 2;
                                approvalcount = 0;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 3)
                            {
                                finalcount = 3;
                                approvalcount = 0;
                                break;
                            }
                        }
                        else
                        {
                            approvalcount = 0;
                        }
                    }
                    if (objWorkFlowList.Count == approvalcount)
                    {
                        finalcount = 1;
                    }
                    else
                    {
                        finalcount = 0;
                    }
                }
            }

            return finalcount;
        }
    }
}