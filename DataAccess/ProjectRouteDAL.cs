using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ProjectRouteDAL
    {
        /// <summary>
        /// To Add Project Routes
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public int AddProjectRoutes(ProjectRouteBO objProjectRoute)
        {
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_PROJECTROUTE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("ROUTEID_", objProjectRoute.Route_ID);
            myCommand.Parameters.AddWithValue("PROJECTID_", objProjectRoute.Project_Id);
            myCommand.Parameters.AddWithValue("ROUTENAME_", objProjectRoute.Route_Name);

            if (objProjectRoute.Route_Details.Length > 1000)
                myCommand.Parameters.AddWithValue("ROUTEDETAIL_", objProjectRoute.Route_Details.Substring(0,1000));
            else
                myCommand.Parameters.AddWithValue("ROUTEDETAIL_", objProjectRoute.Route_Details.Trim());

            myCommand.Parameters.AddWithValue("CREATEDBY_", objProjectRoute.CreatedBy);
            SqlParameter param = new SqlParameter("NEW_ROUTE_ID_", SqlDbType.Int);
            myCommand.Parameters.Add(param);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
            return Convert.ToInt32(myCommand.Parameters["NEW_ROUTE_ID_"].Value.ToString());
        }

        /// <summary>
        /// To Get Project Route by Id
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ProjectRouteList GetProjectRoutebyId(int ProjectId)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GETPROJECTROUTEBYID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectIdIN", ProjectId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO objProjectRoute = null;
            ProjectRouteList ProjectRoute = new ProjectRouteList();
            while (dr.Read())
            {
                objProjectRoute = new ProjectRouteBO();
                if (ColumnExists(dr, "RouteID") && !dr.IsDBNull(dr.GetOrdinal("RouteID")))
                    objProjectRoute.Route_ID =Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RouteID")));
                if (ColumnExists(dr, "routename") && !dr.IsDBNull(dr.GetOrdinal("routename")))
                    objProjectRoute.Route_Name = dr.GetString(dr.GetOrdinal("routename"));
                if (ColumnExists(dr, "routedetails") && !dr.IsDBNull(dr.GetOrdinal("routedetails")))
                    objProjectRoute.Route_Details = dr.GetString(dr.GetOrdinal("routedetails"));
                if (ColumnExists(dr, "totalroutescore") && !dr.IsDBNull(dr.GetOrdinal("totalroutescore")))
                    objProjectRoute.TotalRouteScore = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("totalroutescore")));

                ProjectRoute.Add(objProjectRoute);
            }
            dr.Close();
            return ProjectRoute;
        }

        /// <summary>
        /// To get WOrk Flow Approval ID
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteBO getWOrkFlowApprovalID(ProjectRouteBO objProjectRoute)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWAPPREID";

            //string APPROVERLEVEL = "1";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectId_", objProjectRoute.Project_Id);
            cmd.Parameters.AddWithValue("WorkFlowApprover_", objProjectRoute.WorkFlowApprover);
            //cmd.Parameters.AddWithValue("APPROVERLEVEL_", APPROVERLEVEL);
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
            return ProjectRouteBOobj;
        }

        // to check the Column are Exists or not
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

        /// <summary>
        /// To Add Approval Track header
        /// </summary>
        /// <param name="objApprovalHeaderSave"></param>
        /// <returns></returns>
        public string AddApprovalTrackheader(ProjectRouteBO objApprovalHeaderSave)
        {
            string result = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PRJ_ROUTEAPPROVER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;

            try
            {

                dcmd.Parameters.AddWithValue("WorkFlowApproverID_", objApprovalHeaderSave.WorkFlowApproverID);
                dcmd.Parameters.AddWithValue("StatusID_", objApprovalHeaderSave.StatusID);
                dcmd.Parameters.AddWithValue("CreatedBy_", objApprovalHeaderSave.CreatedBy);

                dcmd.Parameters.AddWithValue("ApproverUserID_", objApprovalHeaderSave.ApproverUserID);
                dcmd.Parameters.AddWithValue("WorkFlowDefinitionID_", objApprovalHeaderSave.WorkFlowDefinitionID);
                if (objApprovalHeaderSave.HHID != 0)
                {
                    dcmd.Parameters.AddWithValue("HHID_", objApprovalHeaderSave.HHID);
                }
                else
                {
                    dcmd.Parameters.AddWithValue("HHID_", "0");
                }
                if (objApprovalHeaderSave.PageCode != "0")
                {
                    dcmd.Parameters.AddWithValue("PageCode_", objApprovalHeaderSave.PageCode);
                }
                else
                {
                    dcmd.Parameters.AddWithValue("PageCode_", "0");
                }
                if (objApprovalHeaderSave.EmailSubject != "0")
                {
                    dcmd.Parameters.AddWithValue("EmailSubject_", objApprovalHeaderSave.EmailSubject);
                }
                else
                {
                    dcmd.Parameters.AddWithValue("EmailSubject_", "0");
                }
                if (objApprovalHeaderSave.EmailBody != "0")
                {
                    dcmd.Parameters.AddWithValue("EmailBody_", objApprovalHeaderSave.EmailBody);
                }
                else
                {
                    dcmd.Parameters.AddWithValue("EmailBody_", "0");
                }
                if (objApprovalHeaderSave.ElementID != 0)
                {
                    dcmd.Parameters.AddWithValue("ElementID_", objApprovalHeaderSave.ElementID);
                }
                else
                {
                    dcmd.Parameters.AddWithValue("ElementID_", "0");
                }
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return result;
        }

        /// <summary>
        /// To get Final Route Approval Detial
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteList getFinalRouteApprovalDetial(ProjectRouteBO objProjectRoute)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GETAPPROVEDROUTE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectIdIN_", objProjectRoute.Project_Id);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO objProjectApprovedRoute = null;
            ProjectRouteList ProjectRoute = new ProjectRouteList();
            while (dr.Read())
            {
                objProjectApprovedRoute = new ProjectRouteBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTENAME"))) objProjectApprovedRoute.Route_Name = dr.GetString(dr.GetOrdinal("ROUTENAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEID"))) objProjectApprovedRoute.Route_ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROUTEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objProjectApprovedRoute.ApproverUserName = dr.GetString(dr.GetOrdinal("USERNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVEDDATE"))) objProjectApprovedRoute.Approveddate = dr.GetDateTime(dr.GetOrdinal("APPROVEDDATE")).ToString(UtilBO.DateFormat); ;
                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) objProjectApprovedRoute.ApproverComment = dr.GetString(dr.GetOrdinal("COMMENTS"));
                if (!dr.IsDBNull(dr.GetOrdinal("STATUSID"))) objProjectApprovedRoute.ApprovedstatusID = dr.GetInt32(dr.GetOrdinal("STATUSID"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISFINAL"))) objProjectApprovedRoute.IsFinal = dr.GetString(dr.GetOrdinal("ISFINAL"));
                ProjectRoute.Add(objProjectApprovedRoute);
            }
            dr.Close();
            return ProjectRoute;
        }

        //After Save Approval Data Check the status of approval
        /// <summary>
        /// To find Route Status after Save
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteBO findRouteStatusafterSave(ProjectRouteBO objProjectRoute)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_APPAFTERSAVEROUTE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectedId_", objProjectRoute.Project_Id);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO objProjectApprovedRoute = null;
            ProjectRouteList ProjectRoute = new ProjectRouteList();
            while (dr.Read())
            {
                objProjectApprovedRoute = new ProjectRouteBO();
                if (!dr.IsDBNull(dr.GetOrdinal("STATUSID"))) objProjectApprovedRoute.ApprovedstatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STATUSID")));
            }
            dr.Close();
            return objProjectApprovedRoute;
        }

        /// <summary>
        /// To Get Approved Comments
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public ProjectRouteList GetApprovedComments(int ProjectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_RTAAPPROVALCOMMENTS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectIdIN_", ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectRouteBO objProjectBO = null;
            ProjectRouteList ProjectList = new ProjectRouteList();
            while (dr.Read())
            {
                objProjectBO = new ProjectRouteBO();
                if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objProjectBO.AppName = dr.GetString(dr.GetOrdinal("USERNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("STATUS"))) objProjectBO.Status = dr.GetString(dr.GetOrdinal("STATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("ACTIONTAKENDATE"))) objProjectBO.ActioDate = dr.GetDateTime(dr.GetOrdinal("ACTIONTAKENDATE")).ToString(UtilBO.DateFormat);
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) objProjectBO.Comments = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) objProjectBO.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));
                ProjectList.Add(objProjectBO);
            }
            dr.Close();
            return ProjectList;
        }
    }
}