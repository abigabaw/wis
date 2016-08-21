using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.IO;
using System.Configuration;

namespace WIS
{
    public partial class EmailPopUpwindow : System.Web.UI.Page
    {
        /// <summary>
        /// Call Screen Loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScreenLoading();
                if (Request.QueryString["ChangeRequestCode"] == "PAYRQ")
                {
                    if (Request.QueryString["BatchNo"] != null)
                    {
                        ViewState["BatchNo"] = Request.QueryString["BatchNo"];
                    }
                }
                UpdateStatus();
                hf1.Value = "1";
            }
        }

        /// <summary>
        /// To Load Scrren
        /// </summary>
        public void ScreenLoading()
        {
            string CR_PreText = "Change Request For ";
            string ResultValue = string.Empty;
            string FormName = string.Empty;
            string PAGECODE = string.Empty;
            string NegotiatedAmount = string.Empty;
            string emailSubject = "";
            string changeRequestCode = Request.QueryString["ChangeRequestCode"];

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Request.QueryString["ProjectID"]), changeRequestCode);

            if (objWorkFlowBO != null)
            {
                if (Request.QueryString["pageCode"] != null)
                {
                    PageCodeTextBox.Text = Request.QueryString["pageCode"].ToString();
                }

                HHIDTextBox.Text = Request.QueryString["HHID"];

                if (changeRequestCode == "CR-HH")
                {
                    switch (PageCodeTextBox.Text)
                    {
                        case "HH":
                            FormName = CR_PreText + "Household Details of HHID " + HHIDTextBox.Text;
                            break;
                        //case "":
                        //    FormName = CR_PreText + ""; break;
                        case "HH-AV":
                            FormName = CR_PreText + "Acreage Valution"; break;
                        case "HH-GOS":
                            FormName = CR_PreText + "Group Ownership"; break;
                        case "HHGMS":
                            FormName = CR_PreText + "Group Member"; break;
                        case "HHTRD":
                            FormName = CR_PreText + "Holder Type Details"; break;
                        case "HH-SA":
                            FormName = CR_PreText + "Services on Affected Plot"; break;
                        case "HH-LU":
                            FormName = CR_PreText + "Affected Land Users"; break;
                        case "HHINS":
                            FormName = CR_PreText + "House Hold Institute"; break;
                        case "HHLHH":
                            FormName = CR_PreText + "House Hold Land Holding"; break;
                        case "HHMCE":
                            FormName = CR_PreText + ""; break;
                        case "HLIOF":
                            FormName = CR_PreText + ""; break;
                        case "HLION":
                            FormName = CR_PreText + "Land Info Resident On"; break;
                        case "HH-MS":
                            FormName = CR_PreText + "Major Shocks"; break;
                        case "HHNEH":
                            FormName = CR_PreText + "Neighbours"; break;
                        case "HH-HD":
                            FormName = CR_PreText + "Health Disability"; break;
                        case "HHHIF":
                            FormName = CR_PreText + "Health Info"; break;
                        case "HH-SE":
                            FormName = CR_PreText + "Socio Economy"; break;
                        case "HHSEL":
                            FormName = CR_PreText + "PAP Livelihood"; break;
                        case "HHSBD":
                            FormName = CR_PreText + "Status Bank"; break;
                        case "HH-SC":
                            FormName = CR_PreText + "Social Concern"; break;
                        case "GRIEV" : 
                            FormName = CR_PreText + "Grievances"; break;

                        case "HV-CO":
                            FormName = CR_PreText + "Valuation Crops"; break;
                        case "HVCUP":
                            FormName = CR_PreText + "Cultural Properties"; break;
                        case "HVDAC":
                            FormName = CR_PreText + "Damaged Crops"; break;
                        case "HVFEN":
                            FormName = CR_PreText + "Fence"; break;
                        case "HFVAL":
                            FormName = CR_PreText + ""; break;
                        case "HV-GR":
                            FormName = CR_PreText + "Grave"; break;
                        //case "HVNPS":
                        //    FormName = CR_PreText + ""; break; //Not Used
                        case "HVOFX":
                            FormName = CR_PreText + "Other Fixtures"; break;
                        case "HVPBU":
                            FormName = CR_PreText + "Permanent Bulding"; break;

                        //case "PKREV":
                        //    FormName = CR_PreText + "Payment"; break;
                        //case "CDAPB":
                        //    FormName = CR_PreText + "Email Popup"; break;
                        //case "CPREV":
                        //    FormName = CR_PreText + "Compensation Package Review"; break;
                        //case "CRFND":
                        //    FormName = CR_PreText + "Payment Request"; break;
                        //case "CREND":
                        //    FormName = CR_PreText + "Payment Verification"; break;

                        //case "RTA":
                        //    FormName = CR_PreText + "Root Approval"; break;
                        //case "DATAV":
                        //    FormName = CR_PreText + "Data Verification"; break;

                        //case "":
                        //    FormName = CR_PreText + ""; break;
                        default:
                            FormName = CR_PreText+"HouseHolD";
                            break;
                    }
                }
                else if (changeRequestCode == "NEG")
                {
                    FormName = "Change Request for Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGC")
                {
                    FormName = "Change Request for Crops Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGL")
                {
                    FormName = "Change Request for Land Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGF")
                {
                    FormName = "Change Request for Fixtures Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGR")
                {
                    FormName = "Change Request for Replacement Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGD")
                {
                    FormName = "Change Request for Damaged crop Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (changeRequestCode == "NEGCU")
                {
                    FormName = "Change Request for Cultural Property Negotiated Amount for HHID " + HHIDTextBox.Text;
                    NegotiatedAmount = Request.QueryString["NegotiatedAmount"].ToString();
                }
                else if (Request.QueryString["ChangeRequestCode"] == "RFPRI")
                {
                    FormName = "Request for Printing the Package Document for HHID " + HHIDTextBox.Text;
                }
                else
                {
                    FormName = objWorkFlowBO.EmailSubject;
                }
                if (Request.QueryString["ChangeRequestCode"] == "PAYRQ")
                {
                    if (Request.QueryString["BatchNo"] != null)
                    {
                        ViewState["BatchNo"] = Request.QueryString["BatchNo"];
                    }
                }
                else if (Request.QueryString["ChangeRequestCode"] == UtilBO.WorkflowGrievances)
                {
                    if (Request.QueryString["GrievanceID"] != null)
                    {
                        ViewState["GrievanceID"] = Request.QueryString["GrievanceID"];
                    }
                    else
                    {
                        ViewState["GrievanceID"] = "0";
                    }
                }

                emailSubject = FormName;
                Master.PageHeader = FormName;
                EmailToTextBox.Text = objWorkFlowBO.EmailID;
                EmailSubjectTextBox.Text = emailSubject;

                WorkFlowApproverIDTextBox.Text = objWorkFlowBO.WorkFlowApproverID.ToString();
                StatusIDTextBox.Text = "3";
                ApproverUserIdTextBox.Text = objWorkFlowBO.ApproverUserID.ToString();
                WorkFlowDefinitionIDTextBox.Text = objWorkFlowBO.WorkFlowDefinitionID.ToString();
                ProjectCodeTextBox.Text = objWorkFlowBO.ProjectCode.ToString();
                ProjectNameTextBox.Text = objWorkFlowBO.ProjectName.ToString();

                //For DIsplaying in TextBox
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Dear Sir / Madam,");
                sb.Append(System.Environment.NewLine);
                sb.Append(objWorkFlowBO.EmailBody);
                sb.Append(System.Environment.NewLine);
                if (Request.QueryString["pageCode"] == "NEG")
                {
                    sb.Append(FormName); sb.Append(" Negotiated Amount : ");
                    sb.Append(NegotiatedAmount);
                }
                else if (Request.QueryString["pageCode"].ToString().Length >= 3 && Request.QueryString["pageCode"].Substring(0, 3) == "NEG")
                {
                    sb.Append(FormName); sb.Append(" Negotiated Amount : ");
                    sb.Append(NegotiatedAmount);
                }
                sb.Append(System.Environment.NewLine);
                sb.Append("Project Code : " + objWorkFlowBO.ProjectCode);
                sb.Append(System.Environment.NewLine);
                sb.Append("Project Name : " + objWorkFlowBO.ProjectName);
                sb.Append(System.Environment.NewLine);
                sb.Append(" Thanks and Regards");
                sb.Append(System.Environment.NewLine);
                sb.Append("WIS - UETCL Team");
                string InputEmail = sb.ToString();

                string mailContent = InputEmail;

                mailContent = mailContent.Replace("\\n", "<br>");
                EmailBodyTextBox.Text = mailContent;
            }
            else
            {
                if (Request.QueryString["ChangeRequestCode"] == "NEG")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "EMAILPOPUP", "AfterNogAmount();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Aftersend", "Aftersend();", true);
                }
            }
        }

        /// <summary>
        /// Send Mail to Approver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SendButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            NotificationBO NotificationObj = new NotificationBO();
            NotificationObj.EmailID = EmailToTextBox.Text.Trim();
            NotificationObj.EmailSubject = EmailSubjectTextBox.Text.Trim();
            NotificationObj.EmailBody = EmailBodyTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(EmailAttachmentFileUpload.FileName))// != string.Empty TempDirectory Path.GetTempPath())
            {
                fileName = Path.Combine(ConfigurationManager.AppSettings["TempDirectory"].ToString(), EmailAttachmentFileUpload.FileName);

                using (FileStream fs = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    byte[] buffer = new byte[Convert.ToInt32(ConfigurationManager.AppSettings["MailFileSize"].ToString())];
                    int bytesRead; while ((bytesRead = EmailAttachmentFileUpload.PostedFile.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                }
                // }
            }
            NotificationObj.AttachedFile = fileName.ToString();
            //NotificationObj.SendChangeRequestEmail(EmailToTextBox.Text, EmailSubjectTextBox.Text, EmailBodyTextBox.Text);
            (new NotificationBLL()).SendChangeRequestEmail(NotificationObj);

            string message = string.Empty;

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Route Approval Notification has been sent";
            if (message != "")
            {
                if (Request.QueryString["ChangeRequestCode"] == "NEG")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "EMAILPOPUP", "AfterNogAmount();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Aftersend", "Aftersend();", true);
                }

                SendButton.Visible = false;
            }
        }

        /// <summary>
        /// Update status
        /// </summary>
        protected void UpdateStatus()
        {
            ProjectRouteBO objApprovalHeaderSave = new ProjectRouteBO();

            objApprovalHeaderSave.WorkFlowApproverID = Convert.ToInt32(WorkFlowApproverIDTextBox.Text.ToString());
            objApprovalHeaderSave.StatusID = (StatusIDTextBox.Text.ToString());
            objApprovalHeaderSave.CreatedBy = Convert.ToInt32(Request.QueryString["userID"]);

            objApprovalHeaderSave.ApproverUserID = Convert.ToInt32(ApproverUserIdTextBox.Text.ToString());
            objApprovalHeaderSave.WorkFlowDefinitionID = Convert.ToInt32(WorkFlowDefinitionIDTextBox.Text.ToString());

            objApprovalHeaderSave.HHID = Convert.ToInt32(HHIDTextBox.Text.ToString());
            objApprovalHeaderSave.PageCode = PageCodeTextBox.Text.ToString();

            objApprovalHeaderSave.EmailSubject = EmailSubjectTextBox.Text.ToString();
            objApprovalHeaderSave.EmailBody = EmailBodyTextBox.Text.ToString();
            if (ViewState["BatchNo"] != null)
            {
                objApprovalHeaderSave.ElementID = Convert.ToInt32(ViewState["BatchNo"]);
            }
            else if (ViewState["GrievanceID"] != null)
            {
                objApprovalHeaderSave.ElementID = Convert.ToInt32(ViewState["GrievanceID"]);
            }

            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            string message = objProjectRouteBLL.AddApprovalTrackheader(objApprovalHeaderSave);
        }
    }
}