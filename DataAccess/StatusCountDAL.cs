using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class StatusCountDAL
    {
        public StatusCountBO GetApprPending(int UserID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_PROJ_SHA_MY_PEND";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ASSIGNTOID_", UserID);
           // // cmd.Parameters.AddWithValue("SP_RECORDSET", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_CLARIFY_STATUS_PEND";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("USERID_", UserID);
          //  // cmd.Parameters.AddWithValue("SP_RECORDSET", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
