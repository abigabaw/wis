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
    public partial class ConversationLog : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Call Bind Repeater method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string WorkflowCode_ = string.Empty;

            if (Request.QueryString["WorkFlowCode"] == "RTA")
            {
                WorkflowCode_ = "Route Approval";
            }
            if (Session["PROJECT_CODE"] != null)
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " Conversation Log for "+ " "+ WorkflowCode_ ;

       
            if (!IsPostBack)
            {
       
                //ViewState["BATCH_PROJECT_ID"] = Convert.ToInt32(Session["PROJECT_ID"]);
                ViewState["PROJECT_ID"] = Request.QueryString["ProjectID"]; //Convert.ToInt32(Session["PROJECT_ID"]);
                ViewState["WorkFlowCode"] = Request.QueryString["WorkFlowCode"]; //Convert.ToInt32(Session["WorkFlowCode"]);
                ViewState["PageCode"] = Request.QueryString["pageCode"]; //Convert.ToInt32(Session["PageCode"]); 
                ViewState["TrackHdrId"] = Request.QueryString["TrackHdrId"];
                if (Request.QueryString["BatchNo"] != null)
                {
                    ViewState["BatchNo"] = Request.QueryString["BatchNo"];
                }
                else
                    ViewState["BatchNo"] = 0;
                BindRepeater();
       
            }         
            
        }

        #region Repeater
        /// <summary>
        /// Bind Data to rptrSenderDetails
        /// </summary>
        private void BindRepeater()
        {
            ConversationLogBLL oConversationLogBLL = new ConversationLogBLL();
            ConversationLogList objConversationLogList = new ConversationLogList();
            if (ViewState["PROJECT_ID"] != null && ViewState["WorkFlowCode"] != null && ViewState["PageCode"] != null && ViewState["TrackHdrId"] != null && ViewState["BatchNo"] != null)
            {
                objConversationLogList = oConversationLogBLL.GetApproverDetails(Convert.ToInt32(ViewState["PROJECT_ID"]), ViewState["WorkFlowCode"].ToString(), ViewState["PageCode"].ToString(), ViewState["TrackHdrId"].ToString(), Convert.ToInt32(ViewState["BatchNo"].ToString()));
            }
            rptrSenderDetails.DataSource = objConversationLogList;
            rptrSenderDetails.DataBind();

            if (rptrSenderDetails.Items.Count == 0)
            {
                lblMessage.Text = "There is no Conversation";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
            }
            
        }

        protected void rptrSenderDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "GetPAPS")
            //{
            //    ViewState["CMP_BATCHNO"] = e.CommandArgument.ToString();
            //   // BindPAPS();
            //}
        }
        #endregion

        #region GridView


        protected void rptrSenderDetails_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.FindControl("rptrApproverDetails") is Repeater)
            //{
            //    Repeater rptrApproverDetails = e.Item.FindControl("rptrApproverDetails") as Repeater;
            //    ConversationLogBLL oConversationLogBLL = new ConversationLogBLL();
            //    ConversationLogList objConversationLogList = oConversationLogBLL.GetApproverDetails(Convert.ToInt32(Session["PROJECT_ID"]), "PAYVR");

            //    rptrApproverDetails.DataSource = objConversationLogList;
            //    rptrApproverDetails.DataBind();
            //}
        }

        //protected void grdApproverDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "DeleteRow")
        //    {
        //        ViewState["PaymentRequestId"] = e.CommandArgument;

        //        ConversationLogBLL oConversationLogBLL = new ConversationLogBLL();
        //        int PaymentRequestId = 0;
        //        int Result = 0;
        //        if (ViewState["PaymentRequestId"] != null)
        //            PaymentRequestId = Convert.ToInt32(ViewState["PaymentRequestId"].ToString());

        //       // Result = oConversationLogBLL.DeletePaymentRequest(PaymentRequestId);
        //       // BindPAPS();
        //    }
        //}
        #endregion

        private int ViewStateProjectId
        {
            get;
            set;
        }

    }
}