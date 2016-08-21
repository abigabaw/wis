using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ClarifyDAL
    {
        public ClarifyList GetData(int HHID,int UserID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_CLARIFY_REQUESTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("UserID", UserID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClarifyBO ClarifyBO = null;
            ClarifyList ClarifyList = new ClarifyList();

            
            while (dr.Read())
            {
                ClarifyBO = new ClarifyBO();

                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    ClarifyBO.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

                if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                    ClarifyBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME")))
                    ClarifyBO.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("REQUESTER")))
                    ClarifyBO.Requester = dr.GetString(dr.GetOrdinal("REQUESTER"));

                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE")))
                    ClarifyBO.RequestDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYREQUEST")))
                    ClarifyBO.RequestDetails = dr.GetString(dr.GetOrdinal("CLARIFYREQUEST"));

                if (!dr.IsDBNull(dr.GetOrdinal("RESPONDENT")))
                    ClarifyBO.Respondent = dr.GetString(dr.GetOrdinal("RESPONDENT"));

                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDDATE")))
                    ClarifyBO.ResponseDate = Convert.ToString(dr.GetDateTime(dr.GetOrdinal("UPDATEDDATE")));
                    

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYRESPONSE")))
                    ClarifyBO.ResponseDetails = dr.GetString(dr.GetOrdinal("CLARIFYRESPONSE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYSTATUS")))
                    ClarifyBO.Status = dr.GetString(dr.GetOrdinal("CLARIFYSTATUS"));

                ClarifyList.Add(ClarifyBO);

            }
            dr.Close();

            return ClarifyList;
        }

        public string InsertClarify(ClarifyBO ClarifyBO)
        {
            string statusMessage = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_INS_CLARIFY_REQUEST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                 
                dcmd.Parameters.Add("TRACKHDR_", ClarifyBO.TrackHeader);
                dcmd.Parameters.Add("REQUEST_", ClarifyBO.RequestDetails);
                dcmd.Parameters.Add("CREATEDBY_", ClarifyBO.UserID);
                dcmd.Parameters.Add("UPDATEDBY_", ClarifyBO.RespondentID);
                dcmd.Parameters.Add("ERRORMSG_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                

                dcmd.ExecuteNonQuery();

                statusMessage = dcmd.Parameters["ERRORMSG_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }

            return statusMessage;
        }


        public ClarifyList GetMyClarify(int UserID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_RESPONSE_REQUESTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERID_", UserID);
            cmd.Parameters.Add("SP_RECORDSET", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClarifyBO ClarifyBO = null;
            ClarifyList ClarifyList = new ClarifyList();

            
            while (dr.Read())
            {
                ClarifyBO = new ClarifyBO();

                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    ClarifyBO.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

                if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                    ClarifyBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME")))
                    ClarifyBO.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("REQUESTER")))
                    ClarifyBO.Requester = dr.GetString(dr.GetOrdinal("REQUESTER"));

                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE")))
                    ClarifyBO.RequestDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYREQUEST")))
                    ClarifyBO.RequestDetails = dr.GetString(dr.GetOrdinal("CLARIFYREQUEST"));

                if (!dr.IsDBNull(dr.GetOrdinal("RESPONDENT")))
                    ClarifyBO.Respondent = dr.GetString(dr.GetOrdinal("RESPONDENT"));

                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDDATE")))
                    ClarifyBO.ResponseDate = Convert.ToString(dr.GetDateTime(dr.GetOrdinal("UPDATEDDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYRESPONSE")))
                    ClarifyBO.ResponseDetails = dr.GetString(dr.GetOrdinal("CLARIFYRESPONSE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYSTATUS")))
                    ClarifyBO.Status = dr.GetString(dr.GetOrdinal("CLARIFYSTATUS"));

                ClarifyList.Add(ClarifyBO);

            }
            dr.Close();

            return ClarifyList;
        }

        public ClarifyBO SelectClarification(int ClarifyID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_CLARIFY_REQUEST";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ID_", ClarifyID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClarifyBO ClarifyBO = null;
            ClarifyBO = new ClarifyBO();

            while (dr.Read())
            {
                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    ClarifyBO.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

                if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                    ClarifyBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME")))
                    ClarifyBO.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("REQUESTER")))
                    ClarifyBO.Requester = dr.GetString(dr.GetOrdinal("REQUESTER"));

                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE")))
                    ClarifyBO.RequestDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYREQUEST")))
                    ClarifyBO.RequestDetails = dr.GetString(dr.GetOrdinal("CLARIFYREQUEST"));

                if (!dr.IsDBNull(dr.GetOrdinal("RESPONDENT")))
                    ClarifyBO.Respondent = dr.GetString(dr.GetOrdinal("RESPONDENT"));

                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDDATE")))
                    ClarifyBO.ResponseDate = Convert.ToString(dr.GetDateTime(dr.GetOrdinal("UPDATEDDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYRESPONSE")))
                    ClarifyBO.ResponseDetails = dr.GetString(dr.GetOrdinal("CLARIFYRESPONSE"));

                if (!dr.IsDBNull(dr.GetOrdinal("CLARIFYSTATUS")))
                    ClarifyBO.Status = dr.GetString(dr.GetOrdinal("CLARIFYSTATUS"));

               

            }
            dr.Close();

            return ClarifyBO;
        }
    }
}
