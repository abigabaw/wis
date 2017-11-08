using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;
using System.Text;

namespace WIS_DataAccess
{
    public class MyTasks_ApprovalDAL
    {
        #region for get First grid Data
        /// <summary>
        /// To Get My Task Approval Detail
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public MyTasks_ApprovalList GetMyTaskApprovalDetail(int UserRoleId)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_SEL_MYACTIVITIES";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UserRoleId_", UserRoleId);
            //cmd.Parameters.AddWithValue("UserRoleId_",);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MyTasks_Approval objMyTasks = null;
            MyTasks_ApprovalList MyTasks = new MyTasks_ApprovalList();
            while (dr.Read())
            {
                objMyTasks = new MyTasks_Approval();
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) objMyTasks.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("ModuleName"))) objMyTasks.ModuleName = dr.GetString(dr.GetOrdinal("ModuleName"));
                if (!dr.IsDBNull(dr.GetOrdinal("ApprovedCount"))) objMyTasks.ApprovedCount = dr.GetInt32(dr.GetOrdinal("ApprovedCount"));
                if (!dr.IsDBNull(dr.GetOrdinal("DeclinedCount"))) objMyTasks.DeclinedCount = dr.GetInt32(dr.GetOrdinal("DeclinedCount"));
                if (!dr.IsDBNull(dr.GetOrdinal("PendingCount"))) objMyTasks.PendingCount = dr.GetInt32(dr.GetOrdinal("PendingCount"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) objMyTasks.ProjectID = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODULEID"))) objMyTasks.ModuleID = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                MyTasks.Add(objMyTasks);
            }
            dr.Close();
            return MyTasks;
        }
        #endregion

        #region for 2nd grid Data
        /// <summary>
        /// To Get My Track Hdr Details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        /// <param name="USERIDIN_"></param>
        /// <returns></returns>
        public TrackerHeaderList GetMyTrackHdrDetails(string ProjectId, string ModuleId, string Status, int USERIDIN_)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_TRACKHDRDTL";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ProjectId.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@ProjectIdIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIdIN", ProjectId.ToString());
            }

            if (ModuleId.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@ModuleIdIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ModuleIdIN", ModuleId.ToString());
            }

            if (Status.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@StatusIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StatusIN", Status.ToString());
            }
            if (USERIDIN_.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@USERIDIN_", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("USERIDIN_", USERIDIN_.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TrackerHeaderBO objMyTasks = null;
            TrackerHeaderList MyTasks = new TrackerHeaderList();
            while (dr.Read())
            {
                objMyTasks = new TrackerHeaderBO();
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE"))) objMyTasks.WorkflowCode = dr.GetString(dr.GetOrdinal("WORKFLOWCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objMyTasks.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRACKERHEADERID"))) objMyTasks.TrackHdrId = dr.GetInt32(dr.GetOrdinal("TRACKERHEADERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID"))) objMyTasks.WorkflowapproverID = dr.GetInt32(dr.GetOrdinal("WORKFLOWAPPROVERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) objMyTasks.WorkflowdefinationID = dr.GetInt32(dr.GetOrdinal("WORKFLOWDEFINITIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL"))) objMyTasks.ApproverLevel = dr.GetInt32(dr.GetOrdinal("APPROVERLEVEL"));
                if (!dr.IsDBNull(dr.GetOrdinal("ELEMENTID"))) objMyTasks.ElementID = dr.GetInt32(dr.GetOrdinal("ELEMENTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("createddate"))) objMyTasks.UpdatedDate = dr.GetDateTime(dr.GetOrdinal("createddate")).ToString(UtilBO.DateFormatFull);
                if (!dr.IsDBNull(dr.GetOrdinal("actiontakendate"))) objMyTasks.ActionTakenDate = dr.GetDateTime(dr.GetOrdinal("actiontakendate")).ToString(UtilBO.DateFormatFull);
                if (!dr.IsDBNull(dr.GetOrdinal("workflowid"))) objMyTasks.WorkFlowId = dr.GetInt32(dr.GetOrdinal("workflowid"));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objMyTasks.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRACKERDETAILID"))) objMyTasks.TrackerDetailID = dr.GetInt32(dr.GetOrdinal("TRACKERDETAILID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAGECODE")))
                    objMyTasks.PageCode = dr.GetString(dr.GetOrdinal("PAGECODE"));
                else
                    objMyTasks.PageCode = "";
                if (ColumnExists(dr, "WCODE") && !dr.IsDBNull(dr.GetOrdinal("WCODE")))
                {
                    objMyTasks.WCode = dr.GetString(dr.GetOrdinal("WCODE"));
                }
                MyTasks.Add(objMyTasks);
            }
            dr.Close();
            return MyTasks;
        }
        #endregion

        #region to find colum data exixts or not
        /// <summary>
        /// To check the Column are Exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        
        #region 3 grid data only if workflow code is 'RTA'
        /// <summary>
        /// To Get Final Project Details
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="myActiveHHID"></param>
        /// <returns></returns>
        public ApprovalscoredtlList GetFinalProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int myActiveHHID)//(string WorkflowCode)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_SEL_SCOREDTL";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("WorkFlowCode_", WorkFlowCode);
            cmd.Parameters.AddWithValue("WORKFLOWITEMID_", WorkFlowId);
            cmd.Parameters.AddWithValue("projectid_", ProjectId);//"RTA");
            cmd.Parameters.AddWithValue("WORKFLOWAPPID_", "0"); //used only when user send other than RTA
            cmd.Parameters.AddWithValue("HHID_", myActiveHHID); //used only when user send other than RTA
            cmd.Parameters.AddWithValue("TrackerHdrID_", "0");
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO objApprovalscore = null;
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();
            while (dr.Read())
            {
                objApprovalscore = new ApprovalscoredtlBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEID"))) objApprovalscore.RouteID = dr.GetInt32(dr.GetOrdinal("ROUTEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) objApprovalscore.ProjectID = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTENAME"))) objApprovalscore.RouteName = dr.GetString(dr.GetOrdinal("ROUTENAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEDETAILS"))) objApprovalscore.RouteDetails = dr.GetString(dr.GetOrdinal("ROUTEDETAILS"));
                if (!dr.IsDBNull(dr.GetOrdinal("TOTALROUTESCORE"))) objApprovalscore.TotalRouteScore = dr.GetInt32(dr.GetOrdinal("TOTALROUTESCORE"));
                ApprovalScoreList.Add(objApprovalscore);
            }

            dr.Close();
            return ApprovalScoreList;

        }
        #endregion

        public void DeclineStatus(WorkflowApprovalBO objWorkflow)
        {

            int status = 0;

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_TASKAPPROVAL_DEC", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("WorkflowApprovarIDIN", Convert.ToInt32(objWorkflow.WorkflowapprovalId));
            myCommand.Parameters.AddWithValue("StatusIDIN", objWorkflow.Status);
            myCommand.Parameters.AddWithValue("AuthorisedIdIN", Convert.ToInt32(objWorkflow.AuthoriserID));
            myCommand.Parameters.AddWithValue("WorkFlowDefinationIdIN", Convert.ToInt32(objWorkflow.WorkFlowDefinationId));
            myCommand.Parameters.AddWithValue("AutionTakenByIN", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("CreatedBy", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("Approvercomments_", objWorkflow.Approvercomments);
            myCommand.Parameters.AddWithValue("TrackerHdrID_", objWorkflow.TrackerHdrID); //TrackerHdrID

            myConnection.Open();
            status = myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public void ApproveStatus(WorkflowApprovalBO objWorkflow)
        {
            //Edwin: 14/04/2016 this approves main request when last pap is declined after level 1
            int status = 0;

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_TASKAPPROVAL_APP", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("WorkflowApprovarIDIN", Convert.ToInt32(objWorkflow.WorkflowapprovalId));
            myCommand.Parameters.AddWithValue("StatusIDIN", objWorkflow.Status);
            myCommand.Parameters.AddWithValue("AuthorisedIdIN", Convert.ToInt32(objWorkflow.AuthoriserID));
            myCommand.Parameters.AddWithValue("WorkFlowDefinationIdIN", Convert.ToInt32(objWorkflow.WorkFlowDefinationId));
            myCommand.Parameters.AddWithValue("AutionTakenByIN", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("CreatedBy", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("Approvercomments_", objWorkflow.Approvercomments);
            myCommand.Parameters.AddWithValue("TrackerHdrID_", objWorkflow.TrackerHdrID); //TrackerHdrID

            myConnection.Open();
            status = myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public int CreateNextRequestOrExit(WorkflowApprovalBO objWorkflow)
        {
            int result = 0;

            string proc = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            // ProjectRouteBO ProjectRouteBOobj = new ProjectRouteBO();

            // Parameters to check for next approver
            int ApprovalLevel = Convert.ToInt32(objWorkflow.ApprovalLevel);
            string WorkFlowCode = objWorkflow.WorkFlowCode.ToString();
            int ProjectID = Convert.ToInt32(objWorkflow.ProjectID);
            int HHID = Convert.ToInt32(objWorkflow.HHID);
            int ElementID = Convert.ToInt32(objWorkflow.ElementID);
            int TrackerHdrID_ = Convert.ToInt32(objWorkflow.TrackerHdrID);
            string pageCode_ = objWorkflow.PageCode.ToString(); ;

           

            #region Check For Next Approver

            int Level = Convert.ToInt32(ApprovalLevel);
            if (Level > 0)
            {
                Level = Level + 1;
            }

            proc = "USP_TRN_GET_SEC_APP_EMAIL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ApprovalLevel_", Convert.ToInt32(Level));
            cmd.Parameters.AddWithValue("WorkFloeCode_", WorkFlowCode);
            cmd.Parameters.AddWithValue("ProjectID_", ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO ProjectRouteBOobj = null;
            ProjectRouteList ProjectRouteList = new ProjectRouteList();


            while (dr.Read())
            {
                ProjectRouteBOobj = new ProjectRouteBO();

                if (ColumnExists(dr, "CELLNUMBER") && !dr.IsDBNull(dr.GetOrdinal("CELLNUMBER")))
                    ProjectRouteBOobj.CellNumber = (dr.GetString(dr.GetOrdinal("CELLNUMBER")));

                if (ColumnExists(dr, "EMAILID") && !dr.IsDBNull(dr.GetOrdinal("EMAILID")))
                    ProjectRouteBOobj.EmailID = (dr.GetString(dr.GetOrdinal("EMAILID")));

                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    ProjectRouteBOobj.WorkFlowDefinitionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (ColumnExists(dr, "WORKFLOWAPPROVERID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID")))
                    ProjectRouteBOobj.WorkFlowApproverID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWAPPROVERID")));

                if (ColumnExists(dr, "APPROVERUSERID") && !dr.IsDBNull(dr.GetOrdinal("APPROVERUSERID")))
                    ProjectRouteBOobj.ApproverUserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERUSERID")));

                if (ColumnExists(dr, "APPROVERUSERNAME") && !dr.IsDBNull(dr.GetOrdinal("APPROVERUSERNAME")))
                    ProjectRouteBOobj.ApproverUserName = (dr.GetString(dr.GetOrdinal("APPROVERUSERNAME")));

                if (ColumnExists(dr, "PROJECTCODE") && !dr.IsDBNull(dr.GetOrdinal("PROJECTCODE")))
                    ProjectRouteBOobj.ProjectCode = (dr.GetString(dr.GetOrdinal("PROJECTCODE")));

                if (ColumnExists(dr, "PROJECTNAME") && !dr.IsDBNull(dr.GetOrdinal("PROJECTNAME")))
                    ProjectRouteBOobj.ProjectName = (dr.GetString(dr.GetOrdinal("PROJECTNAME")));

                if (ColumnExists(dr, "EMAILSUBJECT") && !dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT")))
                    ProjectRouteBOobj.EmailSubject = (dr.GetString(dr.GetOrdinal("EMAILSUBJECT")));

                if (ColumnExists(dr, "EMAILBODY") && !dr.IsDBNull(dr.GetOrdinal("EMAILBODY")))
                    ProjectRouteBOobj.EmailBody = (dr.GetString(dr.GetOrdinal("EMAILBODY")));

                if (ColumnExists(dr, "SMSTEXT") && !dr.IsDBNull(dr.GetOrdinal("SMSTEXT")))
                    ProjectRouteBOobj.SmsText = (dr.GetString(dr.GetOrdinal("SMSTEXT")));

            }
            dr.Close();

            #endregion

            #region Get More Info Next Approver

            proc = "USP_TRN_SELSENDERDETAILFAPP";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TrackerHdrID_", TrackerHdrID_);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr_track = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO SecPrjRouteBOobj = null;

            while (dr_track.Read())
            {
                SecPrjRouteBOobj = new ProjectRouteBO();

                if (!dr_track.IsDBNull(dr_track.GetOrdinal("CREATEDBY"))) SecPrjRouteBOobj.createdBy = Convert.ToInt32(dr_track.GetValue(dr_track.GetOrdinal("CREATEDBY")));
                if (!dr_track.IsDBNull(dr_track.GetOrdinal("PAGECODE"))) SecPrjRouteBOobj.PageCode = (dr_track.GetString(dr_track.GetOrdinal("PAGECODE")));
            }
            dr_track.Close();
            
            #endregion
            
            if (ProjectRouteBOobj != null)
            {
                #region Sending Email Notification

                NotificationBO NotificationObj = new NotificationBO();

                StringBuilder sb = new StringBuilder();
                string emailSubject = "";
                string emailBody = "";
                string approverName = ProjectRouteBOobj.ApproverUserName;

                //emailBody = ProjectRouteBOobj.EmailBody;

                // Set Email Subject and Body based on Workflow Code
                switch (WorkFlowCode)
                {
                    case "RTA":
                        emailSubject = string.Format("{0} {1}", ProjectRouteBOobj.EmailSubject, ProjectRouteBOobj.ProjectName);
                        emailBody = ProjectRouteBOobj.EmailBody.Replace("@@PROJECTNAME", ProjectRouteBOobj.ProjectName);
                        break;
                    default:
                        emailSubject = ProjectRouteBOobj.EmailSubject;
                        emailBody = ProjectRouteBOobj.EmailBody.Replace("@@PROJECTNAME", ProjectRouteBOobj.ProjectName);
                        break;
                }

                sb.Append("Dear " + approverName + ",");
                //sb.Append("<br/><br/>");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(emailBody);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Thanks and Regards,");
                sb.Append(Environment.NewLine);
                sb.Append("WIS - UETCL Team");


                NotificationObj.EmailID = ProjectRouteBOobj.EmailID;
                NotificationObj.EmailSubject = emailSubject;
                NotificationObj.EmailBody = sb.ToString();
                NotificationObj.ProjectCode = ProjectRouteBOobj.ProjectCode;
                NotificationObj.ProjectName = ProjectRouteBOobj.ProjectName;

                (new NotificationDAL()).SendEmail(NotificationObj);
                #endregion

                //NotificationObj.SendEmail(ProjectRouteBOobj.EmailID, ProjectRouteBOobj.EmailSubject, ProjectRouteBOobj.EmailBody, ProjectRouteBOobj.ProjectCode, ProjectRouteBOobj.ProjectName);

                #region for sending SMS

                #region Fetch SMS Data

                WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                string proc1 = "USP_SEL_SMS_CONFIG";

                cmd = new SqlCommand(proc1, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                cmd.Connection.Open();

                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                WIS_ConfigBO WIS_ConfigSMSBO = null;
                // EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

                while (dr1.Read())
                {
                    WIS_ConfigSMSBO = new WIS_ConfigBO();

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileNumber")))
                        WIS_ConfigSMSBO.MobileNumber = (dr1.GetString(dr1.GetOrdinal("RegMobileNumber")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobilePassword")))
                        WIS_ConfigSMSBO.MobilePassword = (dr1.GetString(dr1.GetOrdinal("RegMobilePassword")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegSiteUrl")))
                        WIS_ConfigSMSBO.SiteUrl = (dr1.GetString(dr1.GetOrdinal("RegSiteUrl")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileStatus")))
                        WIS_ConfigSMSBO.MobileStatus = (dr1.GetString(dr1.GetOrdinal("RegMobileStatus")));
                }
                dr1.Close();

                #endregion

                #region for send Sms if only SMS Status = 'Y'

                if (WIS_ConfigSMSBO.MobileStatus == "Y")
                {
                    string Result = string.Empty;
                    string SendsmsTest = ProjectRouteBOobj.SmsText + ProjectRouteBOobj.ProjectCode + ProjectRouteBOobj.ProjectName;
                    NotificationBO SMSNotificationBO = new NotificationBO();

                    SMSNotificationBO.ProviderMobileNo = WIS_ConfigSMSBO.MobileNumber;
                    SMSNotificationBO.ProviderPasword = WIS_ConfigSMSBO.MobilePassword;
                    SMSNotificationBO.ProviderURL = WIS_ConfigSMSBO.SiteUrl;

                    SMSNotificationBO.CellNumber = ProjectRouteBOobj.CellNumber;
                    SMSNotificationBO.SmsText = SendsmsTest;

                    Result = (new NotificationDAL()).SendSMSNOTIFICATION(SMSNotificationBO);
                }

                #endregion

                #endregion

                #region Create Next Approval 2nd And Beyond

                cnn.Open();
                SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PRJ_ROUTEAPPROVER", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;

                string StatusID = "3";
                int CreatedBy = 0;
                int SECHHID = HHID;

                if (SecPrjRouteBOobj.createdBy != null)
                {
                    CreatedBy = SecPrjRouteBOobj.createdBy;
                }

                dcmd.Parameters.AddWithValue("WorkFlowApproverID_", ProjectRouteBOobj.WorkFlowApproverID);
                dcmd.Parameters.AddWithValue("StatusID_", StatusID);
                dcmd.Parameters.AddWithValue("CreatedBy_", CreatedBy);

                dcmd.Parameters.AddWithValue("ApproverUserID_", ProjectRouteBOobj.ApproverUserID);
                dcmd.Parameters.AddWithValue("WorkFlowDefinitionID_", ProjectRouteBOobj.WorkFlowDefinitionID);

                if (SECHHID != 0) { dcmd.Parameters.AddWithValue("HHID_", SECHHID); }
                else { dcmd.Parameters.AddWithValue("HHID_", "0"); }
                if (pageCode_ != null) { dcmd.Parameters.AddWithValue("PageCode_", pageCode_); }
                else { dcmd.Parameters.AddWithValue("PageCode_", DBNull.Value); }
                if (ProjectRouteBOobj.EmailSubject != "0") { dcmd.Parameters.AddWithValue("EmailSubject_", ProjectRouteBOobj.EmailSubject); }
                else { dcmd.Parameters.AddWithValue("EmailSubject_", "0"); }
                if (ProjectRouteBOobj.EmailBody != "0") { dcmd.Parameters.AddWithValue("EmailBody_", sb.ToString()); }
                else { dcmd.Parameters.AddWithValue("EmailBody_", "0"); }
                if (ElementID != 0) { dcmd.Parameters.AddWithValue("ElementID_", ElementID); }
                else { dcmd.Parameters.AddWithValue("ElementID_", "0"); }

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                {
                    result = 1;
                }
                return result;

                #endregion
            }
            else
            {
                result = 0;
                return result;
            }
        }

        public int AddWorkflowApproval(WorkflowApprovalBO objWorkflow, int AppDataCount)
        {
            int update_status = 0;
            int result = 0;
            int checkResult;

            string pageCode_ = string.Empty;
            if (objWorkflow.PageCode != "")
            {
                pageCode_ = objWorkflow.PageCode.ToString();
            }
            else
            {
                pageCode_ = "0";
            }

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_TASKAPPROVAL", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("WorkflowApprovarIDIN", Convert.ToInt32(objWorkflow.WorkflowapprovalId));
            myCommand.Parameters.AddWithValue("StatusIDIN", objWorkflow.Status);
            myCommand.Parameters.AddWithValue("AuthorisedIdIN", Convert.ToInt32(objWorkflow.AuthoriserID));
            myCommand.Parameters.AddWithValue("WorkFlowDefinationIdIN", Convert.ToInt32(objWorkflow.WorkFlowDefinationId));
            myCommand.Parameters.AddWithValue("AutionTakenByIN", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("CreatedBy", Convert.ToInt32(objWorkflow.Auctiontakenby));
            myCommand.Parameters.AddWithValue("Approvercomments_", objWorkflow.Approvercomments);
            myCommand.Parameters.AddWithValue("TrackerHdrID_", objWorkflow.TrackerHdrID); //TrackerHdrID

            myConnection.Open();
            result = myCommand.ExecuteNonQuery();
            myConnection.Close();



            if (objWorkflow.Status == "DECLINED")
            {
                if (pageCode_ == "PAYRQ")
                {
                    #region for Second to n- approval exit or not
                    // 3 - Parameter Value need to get 2nd to n- level approval -  1. ApprovalLevel, 2.WorkFlowCode 3.ProjectID
                    int ApprovalLevel = Convert.ToInt32(objWorkflow.ApprovalLevel);
                    string WorkFloeCode = objWorkflow.WorkFlowCode.ToString();
                    int ProjectID = Convert.ToInt32(objWorkflow.ProjectID);
                    int HHID = Convert.ToInt32(objWorkflow.HHID);

                    int ElementID = Convert.ToInt32(objWorkflow.ElementID);
                    int TrackerHdrID_ = Convert.ToInt32(objWorkflow.TrackerHdrID);

                    if (AppDataCount > 0)
                    {
                        checkResult = checkforanotherLevelExitorNot(ApprovalLevel, WorkFloeCode, ProjectID, HHID, pageCode_, ElementID, TrackerHdrID_);
                        return checkResult;
                    }


                    return result;
                    #endregion
                }
                else
                {
                    return result;
                }
            }
            else
            {
                #region for Second to n- approval exit or not
                // 3 - Parameter Value need to get 2nd to n- level approval -  1. ApprovalLevel, 2.WorkFlowCode 3.ProjectID
                int ApprovalLevel = Convert.ToInt32(objWorkflow.ApprovalLevel);
                string WorkFloeCode = objWorkflow.WorkFlowCode.ToString();
                int ProjectID = Convert.ToInt32(objWorkflow.ProjectID);
                int HHID = Convert.ToInt32(objWorkflow.HHID);

                int ElementID = Convert.ToInt32(objWorkflow.ElementID);
                int TrackerHdrID_ = Convert.ToInt32(objWorkflow.TrackerHdrID);

                checkResult = checkforanotherLevelExitorNot(ApprovalLevel, WorkFloeCode, ProjectID, HHID, pageCode_, ElementID, TrackerHdrID_);
                return checkResult;

                #endregion
            }
        }

        #region 2nd to n- level Approval Data Save / Sending EMAIL / SMS

        private int checkforanotherLevelExitorNot(int ApprovalLevel, string WorkFloeCode, int ProjectID, int HHID, string pageCode_, int ElementID, int TrackerHdrID_)
        {
            int result = 0;
            string proc = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            #region for checking Second approval
            int Level = Convert.ToInt32(ApprovalLevel);
            if (Level > 0)
            {
                Level = Level + 1;
            }

            proc = "USP_TRN_GET_SEC_APP_EMAIL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ApprovalLevel_", Convert.ToInt32(Level));
            cmd.Parameters.AddWithValue("WorkFloeCode_", WorkFloeCode);
            cmd.Parameters.AddWithValue("ProjectID_", ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO ProjectRouteBOobj = null;
            ProjectRouteList ProjectRouteList = new ProjectRouteList();


            while (dr.Read())
            {
                ProjectRouteBOobj = new ProjectRouteBO();

                if (ColumnExists(dr, "CELLNUMBER") && !dr.IsDBNull(dr.GetOrdinal("CELLNUMBER")))
                    ProjectRouteBOobj.CellNumber = (dr.GetString(dr.GetOrdinal("CELLNUMBER")));

                if (ColumnExists(dr, "EMAILID") && !dr.IsDBNull(dr.GetOrdinal("EMAILID")))
                    ProjectRouteBOobj.EmailID = (dr.GetString(dr.GetOrdinal("EMAILID")));

                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    ProjectRouteBOobj.WorkFlowDefinitionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (ColumnExists(dr, "WORKFLOWAPPROVERID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID")))
                    ProjectRouteBOobj.WorkFlowApproverID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWAPPROVERID")));

                if (ColumnExists(dr, "APPROVERUSERID") && !dr.IsDBNull(dr.GetOrdinal("APPROVERUSERID")))
                    ProjectRouteBOobj.ApproverUserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERUSERID")));

                if (ColumnExists(dr, "APPROVERUSERNAME") && !dr.IsDBNull(dr.GetOrdinal("APPROVERUSERNAME")))
                    ProjectRouteBOobj.ApproverUserName = (dr.GetString(dr.GetOrdinal("APPROVERUSERNAME")));

                if (ColumnExists(dr, "PROJECTCODE") && !dr.IsDBNull(dr.GetOrdinal("PROJECTCODE")))
                    ProjectRouteBOobj.ProjectCode = (dr.GetString(dr.GetOrdinal("PROJECTCODE")));

                if (ColumnExists(dr, "PROJECTNAME") && !dr.IsDBNull(dr.GetOrdinal("PROJECTNAME")))
                    ProjectRouteBOobj.ProjectName = (dr.GetString(dr.GetOrdinal("PROJECTNAME")));

                if (ColumnExists(dr, "EMAILSUBJECT") && !dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT")))
                    ProjectRouteBOobj.EmailSubject = (dr.GetString(dr.GetOrdinal("EMAILSUBJECT")));

                if (ColumnExists(dr, "EMAILBODY") && !dr.IsDBNull(dr.GetOrdinal("EMAILBODY")))
                    ProjectRouteBOobj.EmailBody = (dr.GetString(dr.GetOrdinal("EMAILBODY")));

                if (ColumnExists(dr, "SMSTEXT") && !dr.IsDBNull(dr.GetOrdinal("SMSTEXT")))
                    ProjectRouteBOobj.SmsText = (dr.GetString(dr.GetOrdinal("SMSTEXT")));

            }
            dr.Close();

            #endregion

            #region for get user id and other information
            proc = "USP_TRN_SELSENDERDETAILFAPP";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TrackerHdrID_", TrackerHdrID_);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr_track = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO SecPrjRouteBOobj = null;

            while (dr_track.Read())
            {
                SecPrjRouteBOobj = new ProjectRouteBO();

                if (!dr_track.IsDBNull(dr_track.GetOrdinal("CREATEDBY"))) SecPrjRouteBOobj.createdBy = Convert.ToInt32(dr_track.GetValue(dr_track.GetOrdinal("CREATEDBY")));
                if (!dr_track.IsDBNull(dr_track.GetOrdinal("PAGECODE"))) SecPrjRouteBOobj.PageCode = (dr_track.GetString(dr_track.GetOrdinal("PAGECODE")));
            }
            dr_track.Close();
            #endregion

            #region for Save Data for N- Approval Request
            if (ProjectRouteBOobj != null)
            {
                #region for Sending Email Notification
                NotificationBO NotificationObj = new NotificationBO();

                StringBuilder sb = new StringBuilder();
                string emailSubject = "";
                string emailBody = "";
                string approverName = ProjectRouteBOobj.ApproverUserName;

                //emailBody = ProjectRouteBOobj.EmailBody;

                // Set Email Subject and Body based on Workflow Code
                switch (WorkFloeCode)
                {
                    case "RTA":
                        emailSubject = string.Format("{0} {1}", ProjectRouteBOobj.EmailSubject, ProjectRouteBOobj.ProjectName);
                        emailBody = ProjectRouteBOobj.EmailBody.Replace("@@PROJECTNAME", ProjectRouteBOobj.ProjectName);
                        break;
                    default:
                        emailSubject = ProjectRouteBOobj.EmailSubject;
                        emailBody = ProjectRouteBOobj.EmailBody.Replace("@@PROJECTNAME", ProjectRouteBOobj.ProjectName);
                        break;
                }

                sb.Append("Dear " + approverName + ",");
                //sb.Append("<br/><br/>");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(emailBody);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Thanks and Regards,");
                sb.Append(Environment.NewLine);
                sb.Append("WIS - UETCL Team");


                NotificationObj.EmailID = ProjectRouteBOobj.EmailID;
                NotificationObj.EmailSubject = emailSubject;
                NotificationObj.EmailBody = sb.ToString();
                NotificationObj.ProjectCode = ProjectRouteBOobj.ProjectCode;
                NotificationObj.ProjectName = ProjectRouteBOobj.ProjectName;

                (new NotificationDAL()).SendEmail(NotificationObj);
                #endregion

                //NotificationObj.SendEmail(ProjectRouteBOobj.EmailID, ProjectRouteBOobj.EmailSubject, ProjectRouteBOobj.EmailBody, ProjectRouteBOobj.ProjectCode, ProjectRouteBOobj.ProjectName);

                #region for sending SMS

                #region to featch SMS Data

                WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                string proc1 = "USP_SEL_SMS_CONFIG";

                cmd = new SqlCommand(proc1, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                cmd.Connection.Open();

                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                WIS_ConfigBO WIS_ConfigSMSBO = null;
                // EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

                while (dr1.Read())
                {
                    WIS_ConfigSMSBO = new WIS_ConfigBO();

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileNumber")))
                        WIS_ConfigSMSBO.MobileNumber = (dr1.GetString(dr1.GetOrdinal("RegMobileNumber")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobilePassword")))
                        WIS_ConfigSMSBO.MobilePassword = (dr1.GetString(dr1.GetOrdinal("RegMobilePassword")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegSiteUrl")))
                        WIS_ConfigSMSBO.SiteUrl = (dr1.GetString(dr1.GetOrdinal("RegSiteUrl")));

                    if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileStatus")))
                        WIS_ConfigSMSBO.MobileStatus = (dr1.GetString(dr1.GetOrdinal("RegMobileStatus")));
                }
                dr1.Close();

                #endregion

                #region for send Sms if only SMS Status = 'Y'

                if (WIS_ConfigSMSBO.MobileStatus == "Y")
                {
                    string Result = string.Empty;
                    string SendsmsTest = ProjectRouteBOobj.SmsText + ProjectRouteBOobj.ProjectCode + ProjectRouteBOobj.ProjectName;
                    NotificationBO SMSNotificationBO = new NotificationBO();

                    SMSNotificationBO.ProviderMobileNo = WIS_ConfigSMSBO.MobileNumber;
                    SMSNotificationBO.ProviderPasword = WIS_ConfigSMSBO.MobilePassword;
                    SMSNotificationBO.ProviderURL = WIS_ConfigSMSBO.SiteUrl;

                    SMSNotificationBO.CellNumber = ProjectRouteBOobj.CellNumber;
                    SMSNotificationBO.SmsText = SendsmsTest;

                    Result = (new NotificationDAL()).SendSMSNOTIFICATION(SMSNotificationBO);
                }

                #endregion

                #endregion

                #region to insert Data into 2nd to n - level Data

                cnn.Open();
                SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PRJ_ROUTEAPPROVER", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;

                string StatusID = "3";
                int CreatedBy = 0;
                int SECHHID = HHID;

                if (SecPrjRouteBOobj.createdBy != null)
                {
                    CreatedBy = SecPrjRouteBOobj.createdBy;
                }

                dcmd.Parameters.AddWithValue("WorkFlowApproverID_", ProjectRouteBOobj.WorkFlowApproverID);
                dcmd.Parameters.AddWithValue("StatusID_", StatusID);
                dcmd.Parameters.AddWithValue("CreatedBy_", CreatedBy);

                dcmd.Parameters.AddWithValue("ApproverUserID_", ProjectRouteBOobj.ApproverUserID);
                dcmd.Parameters.AddWithValue("WorkFlowDefinitionID_", ProjectRouteBOobj.WorkFlowDefinitionID);

                if (SECHHID != 0) { dcmd.Parameters.AddWithValue("HHID_", SECHHID); }
                else { dcmd.Parameters.AddWithValue("HHID_", "0"); }
                if (pageCode_ != null) { dcmd.Parameters.AddWithValue("PageCode_", pageCode_); }
                else { dcmd.Parameters.AddWithValue("PageCode_", DBNull.Value); }
                if (ProjectRouteBOobj.EmailSubject != "0") { dcmd.Parameters.AddWithValue("EmailSubject_", ProjectRouteBOobj.EmailSubject); }
                else { dcmd.Parameters.AddWithValue("EmailSubject_", "0"); }
                if (ProjectRouteBOobj.EmailBody != "0") { dcmd.Parameters.AddWithValue("EmailBody_", sb.ToString()); }
                else { dcmd.Parameters.AddWithValue("EmailBody_", "0"); }
                if (ElementID != 0) { dcmd.Parameters.AddWithValue("ElementID_", ElementID); }
                else { dcmd.Parameters.AddWithValue("ElementID_", "0"); }

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                {
                    result = 1;
                }
                return result;
            }
            else
            {
                result = 0;
                return result;
            }
                #endregion

            #endregion
        }

        #endregion

        #region for UPdate Final Route if workflow approval level is lastone
        
        public int UPdateFinalRoute(ApprovalscoredtlBO objRouteDetail)
        {
            int result = 0;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_UPD_PROJECTROUTE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@ROUTEIDIN", objRouteDetail.RouteID);
                myCommand.Parameters.AddWithValue("@CommentsIN", objRouteDetail.RouteDetails);
                myCommand.Parameters.AddWithValue("@USERIDIN", objRouteDetail.UpdatedBy);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }
        #endregion

        public ApprovalscoredtlBO GetEmailID(int WorkflowdefinationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_APPROVEREMAIID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("WorkflowdefinationID_", WorkflowdefinationID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO ObjUserEmailID = null;
            ApprovalscoredtlList ApprovalEmailList = new ApprovalscoredtlList();

            ObjUserEmailID = new ApprovalscoredtlBO();

            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("EMAILID"))) ObjUserEmailID.EmailID = dr.GetString(dr.GetOrdinal("EMAILID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CELLNUMBER"))) ObjUserEmailID.CellNumber = dr.GetString(dr.GetOrdinal("CELLNUMBER"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) ObjUserEmailID.ProjectCode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) ObjUserEmailID.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISPLAYNAME"))) ObjUserEmailID.RequesterName = dr.GetString(dr.GetOrdinal("DISPLAYNAME"));

            }
            dr.Close();
            return ObjUserEmailID;
        }

        //used for CH-RR / NEG
        #region get Change Request detial(All the page) and neg Amount value(Final Valaution page only)
        
        public ApprovalscoredtlBO GetCRProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, int TrackerDetailID_)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_SEL_SCOREDTL";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("WorkFlowCode_", WorkFlowCode);
            cmd.Parameters.AddWithValue("WORKFLOWITEMID_", WorkFlowId);
            cmd.Parameters.AddWithValue("projectid_", ProjectId);
            cmd.Parameters.AddWithValue("WORKFLOWAPPID_", WORKFLOWAPPID);
            cmd.Parameters.AddWithValue("HHID_", myActiveHHID);
            cmd.Parameters.AddWithValue("TrackerDetailID_", TrackerDetailID_);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO objApprovalscore = null;
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();

            while (dr.Read())
            {
                objApprovalscore = new ApprovalscoredtlBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objApprovalscore.ProjectCode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) objApprovalscore.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT"))) objApprovalscore.EmailSubject = dr.GetString(dr.GetOrdinal("EMAILSUBJECT"));
                if (!dr.IsDBNull(dr.GetOrdinal("EMAILBODY"))) objApprovalscore.EmailBody = dr.GetString(dr.GetOrdinal("EMAILBODY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE"))) objApprovalscore.WorkFlowCode = dr.GetString(dr.GetOrdinal("WORKFLOWCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRACKERHEADERID"))) objApprovalscore.TrackHeaderID = dr.GetInt32(dr.GetOrdinal("TRACKERHEADERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRACKERHEADERID"))) objApprovalscore.TrackHeaderID = dr.GetInt32(dr.GetOrdinal("TRACKERHEADERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objApprovalscore.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                //objApprovalscore.Add(objApprovalscore);
            }
            dr.Close();
            return objApprovalscore;
        }

        #endregion

        #region for GetBack approval information(comment over Approval or reject)

        public ApprovalscoredtlList GetApprovedFinalProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int myActiveHHID)//(string WorkflowCode)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_TRACKAPPDETL";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("projectid_", ProjectId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO objApprovalscore = null;
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();
            while (dr.Read())
            {
                objApprovalscore = new ApprovalscoredtlBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEID"))) objApprovalscore.RouteID = dr.GetInt32(dr.GetOrdinal("ROUTEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) objApprovalscore.ProjectID = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTENAME"))) objApprovalscore.RouteName = dr.GetString(dr.GetOrdinal("ROUTENAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEDETAILS"))) objApprovalscore.RouteDetails = dr.GetString(dr.GetOrdinal("ROUTEDETAILS"));
                if (!dr.IsDBNull(dr.GetOrdinal("TOTALROUTESCORE"))) objApprovalscore.TotalRouteScore = dr.GetInt32(dr.GetOrdinal("TOTALROUTESCORE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISFINAL"))) objApprovalscore.IsFinal = dr.GetString(dr.GetOrdinal("ISFINAL"));
                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) objApprovalscore.ApproverComments = dr.GetString(dr.GetOrdinal("COMMENTS"));
                ApprovalScoreList.Add(objApprovalscore);
            }

            dr.Close();
            return ApprovalScoreList;
        }

        #endregion

        public ApprovalscoredtlBO GetApprovalCRProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, string pageCode, int Status_id)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_PROJ_SEL_APPRDATA";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("WorkFlowCode_", WorkFlowCode);
            cmd.Parameters.AddWithValue("WORKFLOWITEMID_", WorkFlowId);
            cmd.Parameters.AddWithValue("projectid_", ProjectId);
            cmd.Parameters.AddWithValue("WORKFLOWAPPID_", WORKFLOWAPPID);
            cmd.Parameters.AddWithValue("HHID_", myActiveHHID);
            cmd.Parameters.AddWithValue("PAGECODE_", pageCode);
            cmd.Parameters.AddWithValue("Status_id_", Status_id);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO objApprovalscore = null;
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();

            while (dr.Read())
            {
                objApprovalscore = new ApprovalscoredtlBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objApprovalscore.ProjectCode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) objApprovalscore.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT"))) objApprovalscore.EmailSubject = dr.GetString(dr.GetOrdinal("EMAILSUBJECT"));
                if (!dr.IsDBNull(dr.GetOrdinal("EMAILBODY"))) objApprovalscore.EmailBody = dr.GetString(dr.GetOrdinal("EMAILBODY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE"))) objApprovalscore.WorkFlowCode = dr.GetString(dr.GetOrdinal("WORKFLOWCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRACKERHEADERID"))) objApprovalscore.TrackHeaderID = dr.GetInt32(dr.GetOrdinal("TRACKERHEADERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) objApprovalscore.ApproverComments = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                //objApprovalscore.Add(objApprovalscore);APPROVERCOMMENTS
            }
            dr.Close();
            return objApprovalscore;
        }

        public int UPdateFinalValue(ApprovalscoredtlBO objFinalValue)
        {
            int result = 0;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_UPD_FINALVALUATIONAPPR", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TRACKERDETAILID_", objFinalValue.TrackerHdrID);

                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        public int UPdateFinalValueForIndNeg(int TrackerHdrID, string ChangeRequest)
        {
            int result = 0;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_UPD_FINVALAPRINDNEG", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TRACKERDETAILID_", TrackerHdrID);
                myCommand.Parameters.AddWithValue("PAGECODE_", ChangeRequest);

                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        public int UPdateGrievance(GrievancesBO objGrievance)
        {
            int result = 0;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_UPD_GRIEVANCESTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("HHID_", objGrievance.Hhid);
                myCommand.Parameters.AddWithValue("GRIEVANCEID_", objGrievance.GrievanceID);
                myCommand.Parameters.AddWithValue("CLOSURECOMMENTS_", objGrievance.ClosureComments);
                myCommand.Parameters.AddWithValue("UpdatedBy_", objGrievance.UpdatedBy);
                myCommand.Parameters.AddWithValue("Status_", objGrievance.ResolutionStatus);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        public int UPdateCDAPBUGStatus(ApprovalscoredtlBO objApprovalCDPABUG)
        {
            int result = 0;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_UPD_APPCDAPBUGSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CDAPBudgetID_", objApprovalCDPABUG.CDAPBudgetID);
                myCommand.Parameters.AddWithValue("UpdatedBy_", objApprovalCDPABUG.UpdatedBy);
                myCommand.Parameters.AddWithValue("Status_", objApprovalCDPABUG.Status);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        public ApprovalscoredtlBO GetApprovalComments(int TrackHeaderID, int StatusID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            //string proc = "USP_TRN_PROJ_SEL_APPRDATA";
            string proc = "USP_TRN_PROJ_SEL_APPROVERCOMM";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TrackHeaderID_", TrackHeaderID);
            cmd.Parameters.AddWithValue("StatusID_", StatusID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ApprovalscoredtlBO objApprovalscore = null;
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();

            while (dr.Read())
            {
                objApprovalscore = new ApprovalscoredtlBO();
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) objApprovalscore.ApproverComments = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                //objApprovalscore.Add(objApprovalscore);APPROVERCOMMENTS
            }
            dr.Close();
            return objApprovalscore;
        }

        public TrackerDetailBO GetApprovalTrackerDetailsByID(int trackerDetailID)
        {
            SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            TrackerDetailBO trackerDetailBO = null;

            string proc = "USP_TRN_GET_APPROVALDETAILBYID";

            cmd = new SqlCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TRACKERDETAILID_", trackerDetailID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                trackerDetailBO = new TrackerDetailBO();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) trackerDetailBO.ApproverComments = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return trackerDetailBO;
        }

    }
            

}