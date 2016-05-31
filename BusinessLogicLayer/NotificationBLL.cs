using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class NotificationBLL
    {
        // To format email for Approval Requests
        /// <summary>
        /// To Send Email
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="workFlowCode"></param>
        public void SendEmail(int projectID, string workFlowCode)
        {
            WorkFlowBO objWorkFlow = (new WorkFlowBLL()).getWOrkFlowApprovalID(projectID, workFlowCode);

            if (objWorkFlow != null)
            {
                NotificationBO objNotification = new NotificationBO();
                StringBuilder sb = new StringBuilder();
                string emailSubject = "";
                string emailBody = "";
                string approverName = objWorkFlow.ApproverUserName;

                emailBody = objWorkFlow.EmailBody;

                // Set Email Subject and Body based on Workflow Code
                switch (workFlowCode)
                {
                    case "RTA":
                        emailSubject = string.Format("{0} {1}", objWorkFlow.EmailSubject, objWorkFlow.ProjectName);
                        emailBody = emailBody.Replace("@@PROJECTNAME", objWorkFlow.ProjectName);
                        break;
                    default:
                        emailSubject = objWorkFlow.EmailSubject;
                        break;
                }                

                sb.Append("Dear " + approverName + ",");
                sb.Append("<br/><br/>");
                sb.Append(emailBody);
                sb.Append("<br/><br/>");
                sb.Append("Thanks and Regards,");
                sb.Append("<br/>");
                sb.Append("WIS - UETCL Team");                
               
                objNotification.EmailID = objWorkFlow.EmailID;
                objNotification.EmailSubject = emailSubject;
                objNotification.EmailBody = sb.ToString();
                objNotification.ProjectCode = objWorkFlow.ProjectCode;
                objNotification.ProjectName = objWorkFlow.ProjectName;

                (new NotificationDAL()).SendEmail(objNotification);
            }
        }

        // To format email for Approval Requests that are Approved/Declined
        /// <summary>
        /// To Send Email
        /// </summary>
        /// <param name="objNotification"></param>
        /// <param name="approvalStatus"></param>
        public void SendEmail(NotificationBO objNotification, string approvalStatus)
        {
            string emailBody = "";
            string code = "";
            string requesterName = objNotification.RequesterName;
            #region for Check the workflow code
            if (objNotification.WorkflowCode == "RTA")
            {
                code = "Route Approval";
            }
            else if (objNotification.WorkflowCode == "PAYRQ")
            {
                code = "Payment";
            }
            else if (objNotification.WorkflowCode == "NEG")
            {
                code = "Negotiation Amount";
            }
            else if (objNotification.WorkflowCode == "CRGRA")
            {
                code = "Grievances";
            }
            else if (objNotification.WorkflowCode == "CDAPB")
            {
                code = "CDAPB";
            }
            else 
            {
                code = "Change";
            }
            #endregion

            #region for Check approval staus
            if (approvalStatus == "A")
            {
                emailBody = "Your "+ code +" request has been Approved for the following reason";
            }
            else if (approvalStatus == "D")
            {
                emailBody = "Your " + code + " request has been Declined  for the following reason";
            }
            #endregion

            StringBuilder sb = new StringBuilder();
            sb.Append("Dear " + requesterName + ",");
            sb.Append("<br/><br/>");
            
            sb.Append(emailBody);
            sb.Append("<br/><br/>");
            sb.Append(objNotification.EmailBody);
            sb.Append("<br/><br/>");
            if (approvalStatus == "D")
            {
                sb.Append("Please update the details and resubmit for approval");
            }
            sb.Append("<br/><br/>");
            sb.Append("Thanks and Regards,");
            sb.Append("<br/>");
            sb.Append("WIS - UETCL Team");

            NotificationBO newNotificationBO = new NotificationBO();

            newNotificationBO.EmailID = objNotification.EmailID;
            newNotificationBO.EmailSubject = objNotification.EmailSubject;
            newNotificationBO.AttachedFile = objNotification.AttachedFile;
            newNotificationBO.EmailBody = sb.ToString();
            newNotificationBO.ProjectCode = objNotification.ProjectCode;
            newNotificationBO.ProjectName = objNotification.ProjectName;

            (new NotificationDAL()).SendEmail(newNotificationBO);
           
        }

        /// <summary>
        /// To Send Change Request Email
        /// </summary>
        /// <param name="objNotification"></param>
        public void SendChangeRequestEmail(NotificationBO objNotification)
        {
            (new NotificationDAL()).SendChangeRequestEmail(objNotification);
        }

        /// <summary>
        /// To SEND SMS
        /// </summary>
        /// <param name="SMSNotificationBO"></param>
        /// <returns></returns>
        public string SENDSMS(NotificationBO SMSNotificationBO)
        {
           return (new NotificationDAL()).SendSMSNOTIFICATION(SMSNotificationBO);
        }
    }
}
