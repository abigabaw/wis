using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using WIS_BusinessObjects;
using System.Net;
using SMSClassLibrary;
using System.IO;


namespace WIS_DataAccess
{
    public class NotificationDAL
    {
        /// <summary>
        /// To Send Email
        /// </summary>
        /// <param name="objNotification"></param>
        public void SendEmail(NotificationBO objNotification)
        {
            MailMessage mailMessage = null;
            SmtpClient smtp = null;

            try
            {
                mailMessage = new MailMessage();
                smtp = new SmtpClient();

                mailMessage.To.Add(objNotification.EmailID);
                mailMessage.From = new MailAddress(AppConfiguration.FromMailAddress);
                mailMessage.Subject = objNotification.EmailSubject;
                mailMessage.Body = objNotification.EmailBody;
                mailMessage.IsBodyHtml = true;

                smtp.Send(mailMessage);
            }
            catch (Exception se)
            {
                //do nothing
            }
            finally
            {
                mailMessage = null;
                smtp = null;
            }
        }

        /// <summary>
        /// To Send Change Request Email
        /// </summary>
        /// <param name="objNotification"></param>
        public void SendChangeRequestEmail(NotificationBO objNotification)
        {
            MailMessage mailMessage = null;
            SmtpClient smtp = null;

            try
            {
                mailMessage = new MailMessage();
                smtp = new SmtpClient();
            
                mailMessage.To.Add(objNotification.EmailID);
                mailMessage.From = new MailAddress(AppConfiguration.FromMailAddress);
                mailMessage.Subject = objNotification.EmailSubject;
                try
                {
                    if (objNotification.AttachedFile.Trim() != string.Empty && File.Exists(objNotification.AttachedFile))
                    {
                        Attachment attachment = new Attachment(objNotification.AttachedFile);
                        mailMessage.Attachments.Add(attachment);
                    }
                }
                catch { }
                mailMessage.Body = objNotification.EmailBody;

                mailMessage.IsBodyHtml = true;

                smtp.Send(mailMessage);
            }
            catch (Exception se)
            {
                //throw se;
                //do nothing
            }
            finally
            {
                mailMessage = null;
                smtp = null;
            }
        }

        /// <summary>
        /// To Send SMS NOTIFICATION
        /// </summary>
        /// <param name="SMSNotificationBO"></param>
        /// <returns></returns>
        public string SendSMSNOTIFICATION(NotificationBO SMSNotificationBO)
        {   
            string errmessage = string.Empty;
            string result;
            ReddyInfoSoft ris = new ReddyInfoSoft();
            result = ris.sendSMS(SMSNotificationBO.ProviderMobileNo, SMSNotificationBO.ProviderPasword, SMSNotificationBO.SmsText, SMSNotificationBO.CellNumber, SMSNotificationBO.ProviderURL, "1");
            
            return result;
        }
    }
}
