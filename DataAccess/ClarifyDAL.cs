using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ClarifyDAL
    {
        public ClarifyList GetData(int HHID,int UserID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_CLARIFY_REQUESTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            cmd.Parameters.AddWithValue("UserID", UserID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_INS_CLARIFY_REQUEST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", ClarifyBO.HHID); 
                dcmd.Parameters.AddWithValue("TRACKHDR_", ClarifyBO.TrackHeader);
                dcmd.Parameters.AddWithValue("REQUEST_", ClarifyBO.RequestDetails);
                dcmd.Parameters.AddWithValue("CREATEDBY_", ClarifyBO.UserID);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", ClarifyBO.RespondentID);
                dcmd.Parameters.AddWithValue("ERRORMSG_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_RESPONSE_REQUESTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("USERID_", UserID);
            // cmd.Parameters.AddWithValue("SP_RECORDSET", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_CLARIFY_REQUEST";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID_", ClarifyID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

        public string InsertResponse(ClarifyBO ClarifyBO)
        {
            string statusMessage = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_UPD_CLARIFY_REQUEST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {

                dcmd.Parameters.AddWithValue("ID_", ClarifyBO.ID);
                dcmd.Parameters.AddWithValue("RESPONSE_", ClarifyBO.ResponseDetails);
                dcmd.Parameters.AddWithValue("STATUS_", ClarifyBO.Status);
                dcmd.Parameters.AddWithValue("ERRORMSG_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;



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

        public int CheckPendClarify(int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            int CountPend = 0;

            string proc = "USP_GET_CLARIFY_PENDING";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // cmd.Parameters.AddWithValue("SP_RECORDSET", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClarifyBO ClarifyBO = null;
            ClarifyBO = new ClarifyBO();

            while (dr.Read())
            {
                // ID,CLARIFYREQUEST,CLARIFYRESPONSE,CLARIFYSTATUS,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,ISDELETED,TRACKERHEADERID,HHID,
                // PAPNAME,REQ.USERNAME AS REQUESTER,RES.USERNAME AS RESPONDENT
                if (!dr.IsDBNull(dr.GetOrdinal("EXISTING_REQUESTS")))
                    CountPend = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EXISTING_REQUESTS")));

            }
            dr.Close();

            return CountPend;
        }
    }
}
