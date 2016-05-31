using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConversationLogDAL
    {
        #region for Declination
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region GetData
        #region not used
        /// <summary>
        /// To Get Sender Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="PageCode"></param>
        /// <param name="TrackHdrId"></param>
        /// <returns></returns>
        public ConversationLogList GetSenderDetails(int projectID, string WorkFlowCode, string PageCode, string TrackHdrId)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_WORKFLO_SENDER_DETAILS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("WorkFlowCode_", WorkFlowCode);
            cmd.Parameters.Add("ProjectID_", projectID);
            cmd.Parameters.Add("PageCode_", PageCode);
            cmd.Parameters.Add("TrackHdrId_", TrackHdrId);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConversationLogBO oConversationLogBO = null;
            ConversationLogList lstConversationLog = new ConversationLogList();

            while (dr.Read())
            {
                oConversationLogBO = new ConversationLogBO();

                oConversationLogBO = MapData(dr);

                lstConversationLog.Add(oConversationLogBO);
            }

            dr.Close();

            return lstConversationLog;
        }
        #endregion

        /// <summary>
        /// To Get Approver Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="PageCode"></param>
        /// <param name="TrackHdrId"></param>
        /// <returns></returns>
        public ConversationLogList GetApproverDetails(int projectID, string WorkFlowCode, string PageCode, string TrackHdrId, int BatchNo)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_WORK_APPROVED_DETAILS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("WorkFlowCode_", WorkFlowCode);
            cmd.Parameters.Add("ProjectID_", projectID);
            if (PageCode == "")
            {
                cmd.Parameters.Add("PageCode_", "RTA");
            }else{
            cmd.Parameters.Add("PageCode_", PageCode);
            }
            cmd.Parameters.Add("TrackHdrId_", TrackHdrId);
            cmd.Parameters.Add("BatchNo_", BatchNo);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConversationLogBO oConversationLogBO = null;
            ConversationLogList lstConversationLog = new ConversationLogList();

            while (dr.Read())
            {
                oConversationLogBO = new ConversationLogBO();

                oConversationLogBO = MapDataFIn(dr);

                lstConversationLog.Add(oConversationLogBO);
            }

            dr.Close();

            return lstConversationLog;
        }
        #region for Main Data Fech ConversationLog
        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ConversationLogBO MapDataFIn(IDataReader reader)
        {
            ConversationLogBO oConversationLogBO = new ConversationLogBO();

            if (ColumnExists(reader, "SenderId") && !reader.IsDBNull(reader.GetOrdinal("SenderId")))
                oConversationLogBO.RequesterId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("SenderId")));

            if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                oConversationLogBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            if (ColumnExists(reader, "SenderName1") && !reader.IsDBNull(reader.GetOrdinal("SenderName1")))
                oConversationLogBO.RequesterName = reader.GetString(reader.GetOrdinal("SenderName1"));

            if (ColumnExists(reader, "SentDate1") && !reader.IsDBNull(reader.GetOrdinal("SentDate1")))
                oConversationLogBO.RequestDateTime = reader.GetDateTime(reader.GetOrdinal("SentDate1")).ToString(UtilBO.DateFormatFull);

            if (ColumnExists(reader, "TRACKERHEADERID") && !reader.IsDBNull(reader.GetOrdinal("TRACKERHEADERID")))
                oConversationLogBO.TrackerHeaderId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TRACKERHEADERID")));

            if (ColumnExists(reader, "TRACKERDETAILID") && !reader.IsDBNull(reader.GetOrdinal("TRACKERDETAILID")))
                oConversationLogBO.TrackerDetailId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TRACKERDETAILID")));

            if (ColumnExists(reader, "PROJECTID") && !reader.IsDBNull(reader.GetOrdinal("PROJECTID")))
                oConversationLogBO.ProjectId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PROJECTID")));

            if (ColumnExists(reader, "Comments") && !reader.IsDBNull(reader.GetOrdinal("Comments")))
                oConversationLogBO.ApproverComments = reader.GetString(reader.GetOrdinal("Comments"));

            if (ColumnExists(reader, "MailSubject") && !reader.IsDBNull(reader.GetOrdinal("MailSubject")))
                oConversationLogBO.eMailSubject = reader.GetString(reader.GetOrdinal("MailSubject"));

            if (ColumnExists(reader, "MailBody") && !reader.IsDBNull(reader.GetOrdinal("MailBody")))
                oConversationLogBO.eMailBody = reader.GetString(reader.GetOrdinal("MailBody"));

            if (ColumnExists(reader, "PAGECODE") && !reader.IsDBNull(reader.GetOrdinal("PAGECODE")))
                oConversationLogBO.PageCode = reader.GetString(reader.GetOrdinal("PAGECODE"));

            if (ColumnExists(reader, "WorkFlowCode") && !reader.IsDBNull(reader.GetOrdinal("WorkFlowCode")))
                oConversationLogBO.WorkFlowCode = reader.GetString(reader.GetOrdinal("WorkFlowCode"));

            if (ColumnExists(reader, "DESCRIPTION") && !reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")))
                oConversationLogBO.WorkFlowDescription = reader.GetString(reader.GetOrdinal("DESCRIPTION"));

            if (ColumnExists(reader, "STATUS") && !reader.IsDBNull(reader.GetOrdinal("STATUS")))
                oConversationLogBO.Status = reader.GetString(reader.GetOrdinal("STATUS"));

            if (ColumnExists(reader, "SenderName") && !reader.IsDBNull(reader.GetOrdinal("SenderName")))
                oConversationLogBO.ApproverName = reader.GetString(reader.GetOrdinal("SenderName"));

            if (ColumnExists(reader, "SentDate") && !reader.IsDBNull(reader.GetOrdinal("SentDate")))
                oConversationLogBO.ApprovalDateTime = reader.GetDateTime(reader.GetOrdinal("SentDate")).ToString(UtilBO.DateFormatFull);

            return oConversationLogBO;
        }

        private ConversationLogBO MapData(IDataReader reader)
        {
            ConversationLogBO oConversationLogBO = new ConversationLogBO();

            if (ColumnExists(reader, "SenderId") && !reader.IsDBNull(reader.GetOrdinal("SenderId")))
                oConversationLogBO.RequesterId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("SenderId")));

            if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                oConversationLogBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            if (ColumnExists(reader, "SenderName") && !reader.IsDBNull(reader.GetOrdinal("SenderName")))
                oConversationLogBO.RequesterName = reader.GetString(reader.GetOrdinal("SenderName"));

            if (ColumnExists(reader, "SentDate") && !reader.IsDBNull(reader.GetOrdinal("SentDate")))
                oConversationLogBO.RequestDateTime = reader.GetDateTime(reader.GetOrdinal("SentDate")).ToString(UtilBO.DateFormatFull);

            if (ColumnExists(reader, "TRACKERHEADERID") && !reader.IsDBNull(reader.GetOrdinal("TRACKERHEADERID")))
                oConversationLogBO.TrackerHeaderId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TRACKERHEADERID")));

            if (ColumnExists(reader, "TRACKERDETAILID") && !reader.IsDBNull(reader.GetOrdinal("TRACKERDETAILID")))
                oConversationLogBO.TrackerDetailId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TRACKERDETAILID")));

            if (ColumnExists(reader, "PROJECTID") && !reader.IsDBNull(reader.GetOrdinal("PROJECTID")))
                oConversationLogBO.ProjectId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PROJECTID")));

            if (ColumnExists(reader, "Comments") && !reader.IsDBNull(reader.GetOrdinal("Comments")))
                oConversationLogBO.ApproverComments = reader.GetString(reader.GetOrdinal("Comments"));

            if (ColumnExists(reader, "MailSubject") && !reader.IsDBNull(reader.GetOrdinal("MailSubject")))
                oConversationLogBO.eMailSubject = reader.GetString(reader.GetOrdinal("MailSubject"));

            if (ColumnExists(reader, "MailBody") && !reader.IsDBNull(reader.GetOrdinal("MailBody")))
                oConversationLogBO.eMailBody = reader.GetString(reader.GetOrdinal("MailBody"));

            if (ColumnExists(reader, "PAGECODE") && !reader.IsDBNull(reader.GetOrdinal("PAGECODE")))
                oConversationLogBO.PageCode = reader.GetString(reader.GetOrdinal("PAGECODE"));

            if (ColumnExists(reader, "WorkFlowCode") && !reader.IsDBNull(reader.GetOrdinal("WorkFlowCode")))
                oConversationLogBO.WorkFlowCode = reader.GetString(reader.GetOrdinal("WorkFlowCode"));

            if (ColumnExists(reader, "DESCRIPTION") && !reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")))
                oConversationLogBO.WorkFlowDescription = reader.GetString(reader.GetOrdinal("DESCRIPTION"));

            if (ColumnExists(reader, "STATUS") && !reader.IsDBNull(reader.GetOrdinal("STATUS")))
                oConversationLogBO.Status = reader.GetString(reader.GetOrdinal("STATUS"));

            if (ColumnExists(reader, "ApproverName") && !reader.IsDBNull(reader.GetOrdinal("ApproverName")))
                oConversationLogBO.ApproverName = reader.GetString(reader.GetOrdinal("ApproverName"));

            if (ColumnExists(reader, "ApprovalDateTime") && !reader.IsDBNull(reader.GetOrdinal("ApprovalDateTime")))
                oConversationLogBO.ApprovalDateTime = reader.GetDateTime(reader.GetOrdinal("ApprovalDateTime")).ToString(UtilBO.DateFormatFull);

            return oConversationLogBO;
        }
        #endregion
        // to check the Column are Exists or not
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
        /// To Get Approver Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="PageCode"></param>
        /// <param name="TrackHdrId"></param>
        /// <returns></returns>
        public ConversationLogList GetBatchComments(int BatchNo, int HHID)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_BATCHCOMMENTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("BatchNo_", BatchNo);
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConversationLogBO oConversationLogBO = null;
            ConversationLogList lstConversationLog = new ConversationLogList();

            while (dr.Read())
            {
                oConversationLogBO = new ConversationLogBO();

                if (!dr.IsDBNull(dr.GetOrdinal("papname")))
                    oConversationLogBO.PAPName = dr.GetString(dr.GetOrdinal("papname"));

                if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                    oConversationLogBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (!dr.IsDBNull(dr.GetOrdinal("ACTIONTAKENBY")))
                    oConversationLogBO.RequesterName = dr.GetString(dr.GetOrdinal("ACTIONTAKENBY"));

                if (!dr.IsDBNull(dr.GetOrdinal("ACTIONTAKENDATE")))
                    oConversationLogBO.RequestDateTime = dr.GetDateTime(dr.GetOrdinal("ACTIONTAKENDATE")).ToString(UtilBO.DateFormatFull);

                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS")))
                    oConversationLogBO.eMailBody = dr.GetString(dr.GetOrdinal("COMMENTS"));

                if (!dr.IsDBNull(dr.GetOrdinal("STATUS")))
                    oConversationLogBO.Status = dr.GetString(dr.GetOrdinal("STATUS"));


                lstConversationLog.Add(oConversationLogBO);
            }

            dr.Close();

            return lstConversationLog;
        }
     }
}