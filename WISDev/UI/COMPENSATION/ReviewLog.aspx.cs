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
    public partial class ReviewLog : System.Web.UI.Page
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

            Master.PageHeader = " Approver Review Comments";

            if (!IsPostBack)
            {
                ViewState["PROJECT_ID"] = Request.QueryString["ProjectID"];
                ViewState["HHID"] = Request.QueryString["HHID"]; 
                BindRepeater();
            }
        }

        public CompensationPackagesList getApproverComments()
        {

            CompensationPackagesBLL oCompensationPackagesBLL = new CompensationPackagesBLL();
            CompensationPackagesBO oCompensationPackagesBO = new CompensationPackagesBO();
            CompensationPackagesList oCompensationPackagesList = new CompensationPackagesList();
            
            int projectID = 0;
            int HHID = 0;

            if (ViewState["PROJECT_ID"] != null)
                projectID = Convert.ToInt32(ViewState["PROJECT_ID"]);

            if (ViewState["HHID"] != null)
                HHID = Convert.ToInt32(ViewState["HHID"]);

            oCompensationPackagesBO.HHID = HHID;
            oCompensationPackagesBO.ProjectID = projectID;

            oCompensationPackagesList = oCompensationPackagesBLL.getApproverReviewComments(oCompensationPackagesBO);

            return oCompensationPackagesList;
        }

        #region Repeater
        /// <summary>
        /// Bind Data to rptrSenderDetails
        /// </summary>
        private void BindRepeater()
        {     
            rptrSenderDetails.DataSource = getApproverComments();
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