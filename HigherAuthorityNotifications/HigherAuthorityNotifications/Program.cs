using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using SMSClassLibrary;

namespace HigherAuthorityNotifications
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            EmailTemplateBLL EmailTemplateBLLobj = new EmailTemplateBLL();
            EmailTemplateList EmailTemplateListObj = EmailTemplateBLLobj.GetAllOverdueApprovals();
            int a = 0;
            if (EmailTemplateBLLobj != null)
            {
                for (int i = 0; i < EmailTemplateListObj.Count; i++)
                {
                    try
                    {

                        string fileLoc = ConfigurationManager.AppSettings["ERROS_PATH"].ToString();
                        FileStream fs = null;
                        if (!File.Exists(fileLoc))
                        {
                            using (fs = File.Create(fileLoc))
                            {

                            }
                        }
                        MailMessage mailMessage = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        if (EmailTemplateListObj[i].Triggertype.ToUpper() == "SMS" || EmailTemplateListObj[i].Triggertype.ToUpper() == "BOTH")
                        {
                            EmailTemplateBO EmailTemplateBOobj = EmailTemplateBLLobj.GetSMSDetailsForOverDue(EmailTemplateListObj[i].Workflowcode);
                            if (EmailTemplateBOobj != null)
                            {
                                WIS_ConfigBO WIS_ConfigBO = EmailTemplateBLLobj.GetSMSSenderDataForOverDue();
                                ReddyInfoSoft ris = new ReddyInfoSoft();
                                ris.sendSMS(WIS_ConfigBO.MobileNumber, WIS_ConfigBO.MobilePassword, EmailTemplateBOobj.Smstext, EmailTemplateListObj[i].Cellnumber, WIS_ConfigBO.SiteUrl, "1");
                            }
                            else
                            {
                                // for get errors
                                using (StreamWriter sw = new StreamWriter(fileLoc, true))
                                {
                                    if (a == 0)
                                    {
                                        sw.WriteLine("[" + DateTime.Now.ToString() + "]" + Environment.NewLine);
                                        a++;
                                    }
                                    sw.WriteLine(i + 1 + ") " + EmailTemplateListObj[i].Workflowcode.ToString() + " Does not contain SMS Text." + Environment.NewLine);
                                    //sw.WriteLine(Environment.NewLine + "-------------------------------------------------------" + Environment.NewLine);
                                }
                                //end
                            }
                        }
                        if (EmailTemplateListObj[i].Triggertype.ToUpper() != "SMS")
                        {
                            string mailBody = "";

                            mailMessage.To.Add(EmailTemplateListObj[i].EmailID);
                            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MAIL_FROM"].ToString());
                            EmailTemplateBO EmailTemplateBOobj = EmailTemplateBLLobj.GetEmailDetailsForOverDue(EmailTemplateListObj[i].Workflowcode);
                            if (EmailTemplateBOobj != null)
                            {
                                mailMessage.Subject = EmailTemplateBOobj.EmailSubject;
                                mailMessage.Body = "Dear Sir, <br/><br/> " + EmailTemplateBOobj.EmailBody + " <br/><br/> ProjectCode : " + EmailTemplateListObj[i].ProjectCode
                                    + "<br/> ProjectName : " + EmailTemplateListObj[i].ProjectName + "<br/>" +
                                    "<br/> Requested Date : " + EmailTemplateListObj[i].Requestdate + "<br/>" +
                                    "<br/> Due Date : " + EmailTemplateListObj[i].Duedate + "<br/><br/>" +
                                    "Thanks and Regards, <br/> WIS - UETCL Team";
                                mailMessage.IsBodyHtml = true;

                                smtp.Send(mailMessage);
                            }
                            else
                            {
                                // for get errors                                
                                using (StreamWriter sw = new StreamWriter(fileLoc, true))
                                {
                                    if (a == 0)
                                    {
                                        sw.WriteLine("[" + DateTime.Now.ToString() + "]" + Environment.NewLine);
                                        a++;
                                    }
                                    sw.WriteLine(i + 1 + ") " + EmailTemplateListObj[i].Workflowcode.ToString() + " Does not contain Subject and Body." + Environment.NewLine);
                                    //sw.WriteLine(Environment.NewLine + "-------------------------------------------------------" + Environment.NewLine);
                                }
                                //end
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // for get errors
                        string fileLoc = ConfigurationManager.AppSettings["ERROS_PATH"].ToString();
                        FileStream fs = null;
                        if (!File.Exists(fileLoc))
                        {
                            using (fs = File.Create(fileLoc))
                            {

                            }
                        }
                        using (StreamWriter sw = new StreamWriter(fileLoc, true))
                        {
                            if (a == 0)
                            {
                                sw.WriteLine("[" + DateTime.Now.ToString() + "]" + Environment.NewLine);
                                a++;
                            }
                            sw.WriteLine(i + 1 + ") " + EmailTemplateListObj[i].Workflowcode.ToString() + "-" + ex.ToString());
                            //sw.WriteLine(Environment.NewLine + "-------------------------------------------------------" + Environment.NewLine);
                        }
                        //end
                    }
                }
            }
        }
    }
}
