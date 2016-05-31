using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WIS
{
    public partial class ApprovalMessage : System.Web.UI.UserControl
    {
        /// <summary>
        /// TO get Approval Message belongs to which request
        /// retuns the number
        /// </summary>
        public enum MessageValue
        {
            None,
            RouteApprovalSent,
            RouteApprovalApproved,
            RouteApprovalDeclined,
            ChangeRequestSent,
            ChangeRequestApproved,
            ChangeRequestDeclined,
            DataVerificationSent,
            DataVerificationApproved,
            DataVerificationDeclined,
            NegotiatedAmountSent,
            NegotiatedAmountApproved,
            NegotiatedAmountDeclined,
            PackageReviewSent,
            PackageReviewApproved,
            PackageReviewDeclined,
            PrintRequestSent,
            PrintRequestApproved,
            PrintRequestDeclined,
            FileClosureSent,
            FileClosureApproved,
            FileClosureDeclined,
            PaymentRequestSent,
            PaymentRequestApproved,
            PaymentRequestDeclined,
            PaymentVerificationSent,
            PaymentVerificationApproved,
            PaymentVerificationDeclined,
            GrievanceApprovalSent,
            GrievanceApprovalApproved,
            GrievanceApprovalDeclined,
            CDAPBudgetApprovalSent,
            CDAPBudgetApprovalApproved,
            CDAPBudgetApprovalDeclined
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //public string viewApproverLog { get; set; }

        //public void ViewApproverLog(int projectID, string workflowCode, string pageCode)
        //{
        //    //string param = string.Format("OpenApproverDocumnet({0},'{1}','{2}');", projectID, workflowCode, pageCode);
        //    //lnkApprovalComments.Attributes.Add("onclick", param);

        //    ClientScript.RegisterStartupScript(this.GetType(), "EMAILPOPUP", "AfterNogAmount();", true);
        //    //lnkApprovalComments.Attributes.Add("onclick", string.Format("{0}"));
        //}

        /// <summary>
        /// Set Approval message to label lblApprovalMessage
        /// </summary>
        public MessageValue SetMessage
        {
            set
            {
                switch (value)
                {
                    case MessageValue.None:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.RouteApprovalSent:
                        lblApprovalMessage.Text = "Sent for Route Approval";
                        break;
                    case MessageValue.RouteApprovalApproved:
                        lblApprovalMessage.Text = "Route is Approved";
                        break;
                    case MessageValue.RouteApprovalDeclined:
                        lblApprovalMessage.Text = "Route Approval Request is Declined";
                        break;
                    case MessageValue.ChangeRequestSent:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.ChangeRequestApproved:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.ChangeRequestDeclined:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.DataVerificationSent:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.DataVerificationApproved:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.DataVerificationDeclined:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.NegotiatedAmountSent:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.NegotiatedAmountApproved:
                        lblApprovalMessage.Text = "";
                        break;
                    case MessageValue.NegotiatedAmountDeclined:
                        lblApprovalMessage.Text = "";
                        break;
                }
            }
        }
    }
}