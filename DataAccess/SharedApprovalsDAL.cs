using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;
using System.Text;


namespace WIS_DataAccess
{
    public class SharedApprovalsDAL
    {

        #region for get First grid Data
        /// <summary>
        /// To Get My Task Approval Detail
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public MyTasks_ApprovalList GetMyTaskApprovalDetail(int UserRoleId, int AssigntoId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_PROJ_SHA_MYACTIVITIES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("UserRoleId_", UserRoleId);
            cmd.Parameters.Add("ASSIGNTOID_", AssigntoId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MyTasks_Approval objMyTasks = null;
            MyTasks_ApprovalList MyTasks = new MyTasks_ApprovalList();
            while (dr.Read())
            {
                objMyTasks = new MyTasks_Approval();
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) objMyTasks.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("ModuleName"))) objMyTasks.ModuleName = dr.GetString(dr.GetOrdinal("ModuleName"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL"))) objMyTasks.ApproverLevel = dr.GetInt32(dr.GetOrdinal("APPROVERLEVEL"));
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
        public TrackerHeaderList GetMyTrackHdrDetails(string ProjectId, string ModuleId, string Status, int USERIDIN_, int AssigntoId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_PROJ_TRACKHDRDTL_SHA";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ProjectId.ToString() == "")
            {
                cmd.Parameters.Add("@ProjectIdIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@ProjectIdIN", ProjectId.ToString());
            }

            if (ModuleId.ToString() == "")
            {
                cmd.Parameters.Add("@ModuleIdIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@ModuleIdIN", ModuleId.ToString());
            }

            if (Status.ToString() == "")
            {
                cmd.Parameters.Add("@StatusIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@StatusIN", Status.ToString());
            }
            if (USERIDIN_.ToString() == "")
            {
                cmd.Parameters.Add("@USERIDIN_", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("USERIDIN_", USERIDIN_.ToString());
            }
            cmd.Parameters.Add("ASSIGNTOID_", AssigntoId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

        // to check the Column are Exists or not
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

        /// <summary>
        /// To Get Users
        /// </summary>
        /// <returns></returns>
        public ProjectPersonalList GetApproverUsers(int AssigntoId)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_SHA_APPUSERS";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ASSIGNTOID_", AssigntoId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO ObjPP = null;
            ProjectPersonalList ObjPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                ObjPP = new ProjectPersonalBO();
                ObjPP.UserID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("AUTHORISERID"))));
                ObjPP.Username = dr.GetValue(dr.GetOrdinal("Displayname")).ToString();

                ObjPPList.Add(ObjPP);
            }

            dr.Close();
            return ObjPPList;
        }

        /// <summary>
        /// To UPdate Lock Status
        /// </summary>
        /// <param name="objApprovalCDPABUG"></param>
        /// <returns></returns>
        public void UPdateLockStatus(SharedAuthorizationBO objBo)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_UPD_LOCKSTATUS", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("TRACKERHEADERID_", objBo.TRACKERHEADERID);
            myCommand.Parameters.Add("LOCKEDSTATUS_", objBo.LockStatus);
            myCommand.Parameters.Add("LOCKEDBY_", objBo.UpdateBy);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        /// <summary>
        /// To Get Lock Status
        /// </summary>
        /// <param name="WorkflowdefinationID"></param>
        /// <returns></returns>
        public SharedAuthorizationBO GetLockStatus(int TRACKERHEADERID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LOCKSTATUS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("TRACKERHEADERID_", TRACKERHEADERID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SharedAuthorizationBO objBo = null;

            objBo = new SharedAuthorizationBO();

            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("LOCKEDSTATUS"))) objBo.LockStatus = dr.GetString(dr.GetOrdinal("LOCKEDSTATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("LOCKEDBY"))) objBo.LockedBy = dr.GetString(dr.GetOrdinal("LOCKEDBY"));
            }
            dr.Close();
            return objBo;
        }
    }
}
