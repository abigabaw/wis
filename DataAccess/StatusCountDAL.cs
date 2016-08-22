using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class StatusCountDAL
    {
        public StatusCountBO GetApprPending(int UserID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_PROJ_SHA_MY_PEND";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ASSIGNTOID_", UserID);
            cmd.Parameters.Add("SP_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            StatusCountBO StatusCountBO = null;
            StatusCountBO = new StatusCountBO();

            while (dr.Read())
            {
                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTS")))
                    StatusCountBO.PendingApprovals = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COUNTS")));

            }
            dr.Close();

            return StatusCountBO;
        }

        public static StatusCountBO GetClarifyPending(int UserID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_CLARIFY_STATUS_PEND";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERID_", UserID);
            cmd.Parameters.Add("SP_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            StatusCountBO StatusCountBO = null;
            StatusCountBO = new StatusCountBO();

            while (dr.Read())
            {
                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTS")))
                    StatusCountBO.PendingClarify = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COUNTS")));
            }
            dr.Close();

            return StatusCountBO;
        }
    }
}
