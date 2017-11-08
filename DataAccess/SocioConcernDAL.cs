using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConcernDAL
    {
        /// <summary>
        /// To Insert Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string InsertSocioConcern(SocioConcernBO objSocioConcern)
        {
            string result = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_INS_TRN_CONCERN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("ConcernID_", objSocioConcern.ConcernID);
                dcmd.Parameters.AddWithValue("OtherConcern_", objSocioConcern.OtherConcern);
                dcmd.Parameters.AddWithValue("CREATEDBY", objSocioConcern.UserID);
                dcmd.Parameters.AddWithValue("HHID_", objSocioConcern.HHID);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        /// To get Socio Concern
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernList getSocioConcern(int HHID)
        {
            // used in Master page
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GETSOCIALCONCERNS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SocioConcernBO objSocioConcern = null;
            SocioConcernList SocioConcernList = new SocioConcernList();

            while (dr.Read())
            {
                objSocioConcern = new SocioConcernBO();

                if (!dr.IsDBNull(dr.GetOrdinal("PAP_CONCERNID"))) objSocioConcern.PapConcernID = dr.GetInt32(dr.GetOrdinal("PAP_CONCERNID"));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objSocioConcern.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONCERNID"))) objSocioConcern.ConcernID = dr.GetInt32(dr.GetOrdinal("CONCERNID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONCERN"))) objSocioConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                if (!dr.IsDBNull(dr.GetOrdinal("OTHERCONCERN"))) objSocioConcern.OtherConcern = dr.GetString(dr.GetOrdinal("OTHERCONCERN"));

                SocioConcernList.Add(objSocioConcern);
            }

            dr.Close();

            return SocioConcernList;
        }

        /// <summary>
        /// To Get Socio Concern By Id
        /// </summary>
        /// <param name="PapConcernID"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernBO GetSocioConcernById(int PapConcernID, int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_SOC_CONCERN_ID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PapConcernID_", PapConcernID);
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SocioConcernBO SocioConcernObj = null;
            SocioConcernList SocioConcernList = new SocioConcernList();

            SocioConcernObj = new SocioConcernBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "OTHERCONCERN") && !dr.IsDBNull(dr.GetOrdinal("OTHERCONCERN")))
                    SocioConcernObj.OtherConcern = dr.GetString(dr.GetOrdinal("OTHERCONCERN"));
                if (ColumnExists(dr, "PAP_CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("PAP_CONCERNID")))
                    SocioConcernObj.PapConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_CONCERNID")));
                if (ColumnExists(dr, "CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("CONCERNID")))
                    SocioConcernObj.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));

            }
            dr.Close();
            return SocioConcernObj;
        }

        // To check the Column are Exists or not
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
        /// To Edit Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string EditSocioConcern(SocioConcernBO objSocioConcern)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPDATE_SOCIAL_CONCER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("PapConcernID_", objSocioConcern.PapConcernID);
                dcmd.Parameters.AddWithValue("HHID_", objSocioConcern.HHID);
                dcmd.Parameters.AddWithValue("ConcernID_", objSocioConcern.ConcernID);
                dcmd.Parameters.AddWithValue("OtherConcern_", objSocioConcern.OtherConcern);
                dcmd.Parameters.AddWithValue("UpdatedBY", objSocioConcern.UserID);
               
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
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

            return returnResult;
        }

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteSocialConcern(int PapConcernID, int HHID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DELETESOCIALCONCERN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("PapConcernID_", PapConcernID);
                myCommand.Parameters.AddWithValue("HHID_", HHID);
                //myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }
    }
}