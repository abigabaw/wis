using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace WIS_BusinessObjects
{
    public class NotificationBO
    {
        public string EmailID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string WorkflowCode { get; set; }
        public string RequesterName { get; set; }

        public string CellNumber { get; set; }
        public string SmsText { get; set; }
        public string ProviderMobileNo { get; set; }
        public string ProviderPasword { get; set; }
        public string ProviderURL { get; set; }
        public string AttachedFile { get; set; }

        #region unwanted Code / code not used
        /* This is defined in NotificationDAL now.
        public void SendEmail(string EmailID, string Subject, string Body, string ProjectCode, string ProjectName)
        {
            MailMessage mailMessage = null;
            SmtpClient smtp = null;

            try
            {
                mailMessage = new MailMessage();
                smtp = new SmtpClient();                

                mailMessage.To.Add(EmailID);
                mailMessage.From = new MailAddress("info@ktwo.co.in");
                mailMessage.Subject = Subject;
                mailMessage.Body = "Dear Sir <br/><br/> " + Body + "<br/><br/> ProjectCode : " + ProjectCode + "<br/> ProjectName : " + ProjectName + "<br/><br/> Thanks and Regards <br/> WIS - UETCL Team";

                mailMessage.IsBodyHtml = true;

                smtp.Send(mailMessage);
            }
            catch (Exception se)
            {
                // do nothing
            }
            finally
            {
                mailMessage = null;
                smtp = null;
            }
        }
        */

      /*  public string SendSMS(string CellNumber, string SmsText, string ProjectCode, string ProjectName)
        {
            string MobileNo = "9008940779";
            string Pasword = "900894"; //Site2sms
            //string Pasword = "67911";  //fullonsms
            string SMS = SmsText + " ProjectName : "+ ProjectName;
            string SendTo = CellNumber;
            string errmessage = string.Empty;
            ReddyInfoSoft ris = new ReddyInfoSoft();

            string result = ris.sendSMS(MobileNo, Pasword, SMS, SendTo, "Site2sms", "1");

            return result;

        }*/

        /* This is defined in NotificationDAL now.
        public void SendChangeRequestEmail(string EmailID, string Subject, string Body)
        {
            MailMessage mailMessage = null;
            SmtpClient smtp = null;

            mailMessage = new MailMessage();
            smtp = new SmtpClient();
            
            try
            {
                mailMessage.To.Add(EmailID);
                mailMessage.From = new MailAddress("info@ktwo.co.in");
                mailMessage.Subject = Subject;
                mailMessage.Body = Body;

                mailMessage.IsBodyHtml = true;

                smtp.Send(mailMessage);
            }
            catch (Exception se)
            {
                // do nothing
            }
            finally
            {
                mailMessage = null;
                smtp = null;
            }
        }*/
        #endregion
    }
}
