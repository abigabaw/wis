using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class BatchPayment : System.Web.UI.Page
    {
        /// <summary>
        /// Check User Permitions and Call Bind grid Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            caldpBatchFromDate.Format = UtilBO.DateFormat;
            CaldpBatchToDate.Format = UtilBO.DateFormat;

            if (Session["PROJECT_CODE"] != null)
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Batch Payment";

            //ViewStateProjectId = Convert.ToInt32(Request.QueryString["projID"]);
            //ViewStateProjectId = Convert.ToInt32(Session["PROJECT_ID"]); 

            if (!IsPostBack)
            {
                //ViewState["BATCH_PROJECT_ID"] = Request.QueryString["projID"];
                ViewState["BATCH_PROJECT_ID"] = Convert.ToInt32(Session["PROJECT_ID"]);
                //if (ViewState["BATCH_PROJECT_ID"] != null)
                //    ViewStateProjectId = Convert.ToInt32(ViewState["BATCH_PROJECT_ID"]);
                dpBatchFromDate.Attributes.Add("readonly", "readonly");
                dpBatchToDate.Attributes.Add("readonly", "readonly");
                BindRepeater();
                //  checkForApprover();PRIV_BATCH_PAYMENT
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BATCH_PAYMENT) == false)
                {
                    btnSubmitForPayment.Visible = false;
                    grdPaymentRequestBatch.Columns[grdPaymentRequestBatch.Columns.Count - 1].Visible = false;
                }
            }
        }

        #region Repeater
        /// <summary>
        /// Bind Data to repeater
        /// </summary>
        private void BindRepeater()
        {
            string FromData = string.Empty;
            string ToDate = string.Empty;
            string BatchNO = string.Empty;

            if(dpBatchFromDate.Text.ToString()!=string.Empty)
                FromData = dpBatchFromDate.Text.ToString();
            if (dpBatchToDate.Text.ToString() != string.Empty)
                ToDate = dpBatchToDate.Text.ToString();
            if (txtBachNo.Text.ToString() != string.Empty)
                BatchNO = txtBachNo.Text.ToString();

            BatchBLL oBatchBLL = new BatchBLL();
            BatchList objBatchList = oBatchBLL.GetPaymentBatches(Convert.ToInt32(ViewState["BATCH_PROJECT_ID"]), BatchNO, ToDate, FromData);

            rptrBatchPayment.DataSource = objBatchList;
            rptrBatchPayment.DataBind();

            if (rptrBatchPayment.Items.Count == 0)
            {
                lblMessage.Text = "No batch Exist.";
                lblMessage.Visible = true;
            }
            //else if (objBatchList.Count > 0)
            //{
            //    lblMessage.Text = "Click on Batch No";
            //    lblMessage.Visible = true;
            //}
            else
            {
                lblMessage.Visible = false;
            }
            clear();
        }

        /// <summary>
        /// For Child Repeater bind data based on Selected Batch
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptrBatchPayment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "GetPAPS")
            {
                ViewState["CMP_BATCHNO"] = e.CommandArgument.ToString();
                if (e.Item.FindControl("hdnBatchNo") is HiddenField)
                {
                    HiddenField hdnBatchNo = e.Item.FindControl("hdnBatchNo") as HiddenField;
                    HiddenField hdnBatchDate = e.Item.FindControl("hdnBatchDate") as HiddenField;

                   //HyperLink Status = new HyperLink();
                   //string uStatus = "~/UI/REPORTUI/RptViewer.aspx?rptCode=OPTS&ProjectID=46";
                   lgndBatch.InnerText = "Batch: " + hdnBatchNo.Value + " | Created: " + hdnBatchDate.Value;
                }
                //ViewState["display"];
                BindPAPS(0);
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BATCH_PAYMENT) == false)
                {
                    btnSubmitForPayment.Visible = false;
                    grdPaymentRequestBatch.Columns[grdPaymentRequestBatch.Columns.Count - 1].Visible = false;
                }
            }
           
        }
        #endregion

        #region GridView
        /// <summary>
        /// Check Batch Status and Give permitions to delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentRequestBatch_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            int ProjectId = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int count = 0; 

                Label lblReqStatus = e.Row.FindControl("lblRequestStatus") as Label;
                CheckBox chkboxSelect = e.Row.FindControl("chkSelect") as CheckBox;
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                
                if(Session["PROJECT_ID"]!=null)
                  ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);

                //int ApprovalTotalCount = totalcountapproval(ProjectId);
                //if (ApprovalTotalCount == 1 || ApprovalTotalCount == 2)
                //{
                   
                    string getCharacter = string.Empty;
                    if (lblReqStatus.Text.Length > 0)
                    {
                        getCharacter = lblReqStatus.Text.Substring(0, 1).ToLower();

                        if (lblReqStatus.Text.Length > 0)
                        {
                            string strReqStatus = lblReqStatus.Text;
                            if (BatchBLL.RequestStatus_Submitted == strReqStatus)
                            {
                                chkboxSelect.Visible = false;
                                imgDelete.Visible = false;
                                btnSubmitForPayment.Visible = false;
                                BatchStatusLinks.Visible = true;
                                
                            }
                            else if (BatchBLL.RequestStatus_Pending == strReqStatus)
                            {
                                chkboxSelect.Visible = true;
                                //count = Convert.ToInt32(hdnPendingRequestCount.Value);
                               // hdnPendingRequestCount.Value = (++count).ToString();
                                btnSubmitForPayment.Visible = true;
                                BatchStatusLinks.Visible = false;
                            }
                            else if (BatchBLL.RequestStatus_Declined == strReqStatus)
                            {
                                chkboxSelect.Visible = true;
                                //count = Convert.ToInt32(hdnPendingRequestCount.Value);
                                //hdnPendingRequestCount.Value = (++count).ToString();
                                imgDelete.Visible = false;
                                btnSubmitForPayment.Visible = false;
                                BatchStatusLinks.Visible = true;
                            }
                            else if (BatchBLL.RequestStatus_Approved == strReqStatus)
                            {
                                chkboxSelect.Visible = false;
                                imgDelete.Visible = false;
                                btnSubmitForPayment.Visible = false;
                                BatchStatusLinks.Visible = true;
                            }
                        }
                    }
                //}
                //if (ApprovalTotalCount == 3)
                //{
                //    lblReqStatus.Text = "Submitted";
                //    chkboxSelect.Visible = false;
                //    count = Convert.ToInt32(hdnPendingRequestCount.Value);
                //    //hdnPendingRequestCount.Value = (++count).ToString();
                //    //EnableSubmitButton(false);
                //    btnSubmitForPayment.Visible = false;
                //    imgDelete.Visible = false;
                //}
                //else if (ApprovalTotalCount == 0)
                //{
                //    lblReqStatus.Text = lblReqStatus.Text.ToString();
                //   // hdnPendingRequestCount.Value = (++count).ToString();
                //    imgDelete.Visible = true;
                //    btnSubmitForPayment.Visible = true;
                //}
            }
        }
        
        /// <summary>
        /// To delete Data form the Batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentRequestBatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                ViewState["PaymentRequestId"] = e.CommandArgument;

                BatchBLL oBatchBLL = new BatchBLL();
                int PaymentRequestId = 0;
                int Result = 0;
                if (ViewState["PaymentRequestId"] != null)
                    PaymentRequestId = Convert.ToInt32(ViewState["PaymentRequestId"].ToString());

                Result = oBatchBLL.DeletePaymentRequest(PaymentRequestId);
                string message = "Data deleted successfully";
                string AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                BindPAPS(1);
                BindRepeater();
            }
        }
        #endregion
        /// <summary>
        /// Send request to Batch Payment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitForPayment_Click(object sender, EventArgs e)
        {
            BatchBLL oBatchBLL = new BatchBLL();
            BatchBO oBatchBO;

            int HHID = 0;
            int ProjectID = 0;
            string AlertMessage = string.Empty;
            string message = string.Empty;
            int BatchNo = 0;
            int userID = Convert.ToInt32(Session["USER_ID"]);
            if (ViewState["CMP_BATCHNO"] != null)
                BatchNo = Convert.ToInt32(ViewState["CMP_BATCHNO"].ToString());
            foreach (GridViewRow grvRow in grdPaymentRequestBatch.Rows)
            {
                CheckBox chkBoxSelected = grvRow.FindControl("chkSelect") as CheckBox;

                //if (chkBoxSelected.Checked)
                //{
                    //oBatchBO.Payt_RequestID = Convert.ToInt32((grvRow.FindControl("litPaymentReqID") as Literal).ToString());
                    int payemntRequestId = Convert.ToInt32((grvRow.FindControl("lblPaymentRequestId") as Label).Text);

                    oBatchBO = new BatchBO();
                    oBatchBO.Payt_RequestID = payemntRequestId;
                    oBatchBO.RequestStatus = BatchBLL.RequestStatus_Submitted;
                    oBatchBO.UpdatedBy = userID;
                    if (grvRow.FindControl("lblHHID") is Label)
                        HHID = Convert.ToInt32((grvRow.FindControl("lblHHID") as Label).Text);
                    oBatchBO.StausLevel = Convert.ToInt32(UtilBO.BatchPaymentStatus);
                    message = oBatchBLL.UpdatePaymentSubmit(oBatchBO);
                //}
            }

            if (!string.IsNullOrEmpty(message))
            {
                AlertMessage = "alert('" + message + "');";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                //after Closing Email popup in datagrid status is change to submited.
                BindPAPS(1);

                //SENDING MAIL
                //int userID = Convert.ToInt32(Session["USER_ID"]);
                ProjectID = Convert.ToInt32(ViewState["BATCH_PROJECT_ID"]);

                if (Session["HH_ID"] != null)
                    HHID = Convert.ToInt32(Session["HH_ID"]);

                //Taking HHID From this LOOP 
                //foreach (GridViewRow grvRow in grdPaymentRequestBatch.Rows)
                //{
                //    CheckBox chkBoxSelected = grvRow.FindControl("chkSelect") as CheckBox;

                //    if (chkBoxSelected.Checked)
                //    {
                //        if(grvRow.FindControl("lblHHID") is Label)
                //            HHID = Convert.ToInt32((grvRow.FindControl("lblHHID") as Label).Text);
                //    }
                //}


                string pageCode = UtilBO.WorkflowPaymentRequest;

                string BatchRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}',{5})", UtilBO.WorkflowPaymentRequest, ProjectID, userID, HHID, pageCode, BatchNo);

                ClientScript.RegisterStartupScript(this.GetType(), "BATCHPAYMENT", BatchRequest, true);

            }
        }
        /// <summary>
        /// Property ViewStateProjectId
        /// </summary>
        private int ViewStateProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// check For Approver Exists or Not
        /// </summary>
        /// <returns></returns>
        public bool checkForApprover()
        {
            string NegotiatedAmount = string.Empty;
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();
            bool approverExists = false;

            string ChangeRequestCode = UtilBO.WorkflowPaymentRequest;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            if (objWorkFlowBO != null)
            {
                approverExists = true;
                //EnableSubmitButton(true);
                //if (hdnPendingRequestCount.Value != null)
                //{
                //    int CountPendingRecords = Convert.ToInt32(hdnPendingRequestCount.Value);
                //    if (CountPendingRecords == 0)
                //        EnableSubmitButton(false);
                //    else
                //        EnableSubmitButton(true);
                //}
            }
            else
            {
                EnableSubmitButton(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approver is not defined')", true);
            }

            return approverExists;
        }
        /// <summary>
        /// set Visible to Submit Button
        /// </summary>
        /// <param name="YesNo"></param>
        private void EnableSubmitButton(bool YesNo)
        {
            btnSubmitForPayment.Visible = YesNo;
        }
        /// <summary>
        /// Bind Paps data to grid grdPaymentRequestBatch for Selected Batch
        /// </summary>
        /// <param name="Status"></param>
        private void BindPAPS(int Status)
        {
            BatchBLL oBatchBLL = new BatchBLL();
            //Old Method
            //BatchList objBatchList = oBatchBLL.GetPaymentRequestBatch(Convert.ToInt32(ViewState["BATCH_PROJECT_ID"]), Convert.ToInt32(ViewState["CMP_BATCHNO"]));

            //New Method 
            BatchList objBatchList = oBatchBLL.GetPaymentPendingBatch(Convert.ToInt32(ViewState["BATCH_PROJECT_ID"]), Convert.ToInt32(ViewState["CMP_BATCHNO"]));

            hdnPendingRequestCount.Value = "0";
            if (objBatchList.Count == 0 && Status==1)
            {
                lblMessage.Text = "No Batch Found.";
                lblMessage.Visible = true;
            }
            //else if (objBatchList.Count > 0 && Status == 0)
            //{
            //    lblMessage.Text = "Click on Batch";
            //    lblMessage.Visible = true;
            //}
            //else
            //{
            //    lblMessage.Text = string.Empty;
            //    lblMessage.Visible = false;
            //}
            grdPaymentRequestBatch.DataSource = objBatchList;
            grdPaymentRequestBatch.DataBind();

            bool approverDefined = checkForApprover();

            if (objBatchList.Count > 0 && approverDefined)
            {
                pnlBatchPAPS.Visible = true;
              //  lblMessage.Visible = true;
            }
            else
            {
                pnlBatchPAPS.Visible = false;
                //lblMessage.Text = "";
                //  lblMessage.Visible = true;
            }

            
        }
        /// <summary>
        /// To change Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentRequestBatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPaymentRequestBatch.PageIndex = e.NewPageIndex;
            BindPAPS(0);
        }
        /// <summary>
        /// Use to Search Batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchBatch_Click(object sender, EventArgs e)
        {   
            BindRepeater();
        }
        /// <summary>
        /// Use to Clear Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            txtBachNo.Text = string.Empty;
            dpBatchFromDate.Text = string.Empty;
            dpBatchToDate.Text = string.Empty;
            BindRepeater();
        }
        /// <summary>
        /// Use to Clear Search
        /// </summary>
        public void clear()
        {
            txtBachNo.Text = string.Empty;
            dpBatchFromDate.Text = string.Empty;
            dpBatchToDate.Text = string.Empty;
        }
        /// <summary>
        /// Check total count approval
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public int totalcountapproval(int ProjectId)
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;
            WorkFlowList objWorkFlowList_ = null;

            string ChangeRequestCode = UtilBO.WorkflowPaymentRequest;

            objProjectRoute.WorkFlowApprover = ChangeRequestCode;
            objProjectRoute.Project_Id = ProjectId;

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    if (Session["HH_ID"] != null)
                    {
                        objProjectRoute.HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                    }
                    else
                    {
                        objProjectRoute.HHID = 0;
                    }
                    //objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = UtilBO.WorkflowPaymentRequest; // objHouseHold.PageCode = "DATAV";
                    objProjectRoute.WorkflowCode = UtilBO.WorkflowPaymentRequest;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    //addtional list
                    objWorkFlowList_ = objProjectRouteBLL.ApprovalStatuschecklist(objProjectRoute);

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
                            //i + 1; addtionl Code
                            if (objWorkFlowList[i].CountApproval == objWorkFlowList_[i].LEVEL)
                            {
                                if (objWorkFlowList_[i].ApprovalstatusID == 3)
                                {
                                    finalcount = 3;
                                    break;
                                }
                                else
                                {
                                    approvalcount = 0;
                                }
                            }
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
        /// <summary>
        /// Set Values to Literal inside repeater rptrBatchPayment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptrBatchPayment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int TotalapprovalCount_ = 0;
            int TotalDeclineCount_ = 0;
            int TotalPendingCount_ = 0;
            int ProjectId = 0;
            int totalcount = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrtotalApproval = (Literal)e.Item.FindControl("ltrtotalApproval");
                Literal ltrtotalDeclined = (Literal)e.Item.FindControl("ltrtotalDeclined");

                 if(Session["PROJECT_ID"]!=null)
                  ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                 TotalapprovalCount_ = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TOTALApproved"));
                 TotalDeclineCount_ = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TOTALDeclined"));

                 ltrtotalApproval.Text = TotalapprovalCount_.ToString();
                 ltrtotalDeclined.Text = TotalDeclineCount_.ToString();
                 //totalcount = totalcountapproval(ProjectId);
                 //if (totalcount == 1 || totalcount == 2)
                 //{
                 //    TotalapprovalCount_ = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TOTALApproved"));
                 //    TotalDeclineCount_ = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TOTALDeclined"));

                 //    ltrtotalApproval.Text = TotalapprovalCount_.ToString();
                 //    ltrtotalDeclined.Text = TotalDeclineCount_.ToString();
                 //}
                 //else
                 //{
                 //    ltrtotalApproval.Text = "0";
                 //    ltrtotalDeclined.Text = "0";
                 //}

                 TotalPendingCount_ = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TOTALPending"));
            }
            
        }
    }
}