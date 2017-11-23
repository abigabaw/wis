using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class SocialSupportDAL
    {
        string ConStr = AppConfiguration.ConnectionString;
        /// <summary>
        /// To Get ALL School Details
        /// </summary>
        /// <returns></returns>
        public object GetALLSchoolDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_SUPPORT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            SocialSupportBO SocialSupportBOObj = null;
            SocialSupportList SocialSupportListObj = new SocialSupportList();
            SocialSupportBOObj = new SocialSupportBO();

            while (dr.Read())
            {
                SocialSupportBOObj = new SocialSupportBO();
                SocialSupportBOObj.SUPPORTEDBYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUPPORTID")));
                SocialSupportBOObj.SupportedBy = dr.GetString(dr.GetOrdinal("SUPPORTEDBY"));
                SocialSupportBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                SocialSupportListObj.Add(SocialSupportBOObj);
            }
            dr.Close();
            return SocialSupportListObj;
        }

        /// <summary>
        /// To Get School Details
        /// </summary>
        /// <returns></returns>
        public object GetSchoolDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_SUPPORT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            SocialSupportBO SocialSupportBOObj = null;
            SocialSupportList SocialSupportListObj = new SocialSupportList();
            SocialSupportBOObj = new SocialSupportBO();

            while (dr.Read())
            {
                SocialSupportBOObj = new SocialSupportBO();
                SocialSupportBOObj.SUPPORTEDBYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUPPORTID")));
                SocialSupportBOObj.SupportedBy = dr.GetString(dr.GetOrdinal("SUPPORTEDBY"));
                //SocialSupportBOObj.IsDeletedBy = dr.GetString(dr.GetOrdinal("ISDELETED"));
                SocialSupportListObj.Add(SocialSupportBOObj);
            }            
            dr.Close();
            return SocialSupportListObj;
        }

        /// <summary>
        /// To Insert Support Details
        /// </summary>
        /// <param name="SocialSupportBOObj"></param>
        /// <returns></returns>
        public string InsertSupportDetails(SocialSupportBO SocialSupportBOObj)
        {
            string returnResult = string.Empty;

            SqlConnection Con = new SqlConnection(ConStr);
            Con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_INSERT_SUPPORT", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.AddWithValue("S_SUPPORTEDBY", SocialSupportBOObj.SupportedBy);
                cmd.Parameters.AddWithValue("S_CREATEDBY", SocialSupportBOObj.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                //return cmd.ExecuteNonQuery();

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;   
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// To Edit Support Details
        /// </summary>
        /// <param name="SocialSupportBOObj"></param>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public string EditSupportDetails(SocialSupportBO SocialSupportBOObj, int SUPPORTEDBYID)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATE_SUPPORTEDBY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("B_SUPPORTID", SUPPORTEDBYID);
                dcmd.Parameters.AddWithValue("B_UPDATEDBY", SocialSupportBOObj.SupportedBy);
                dcmd.Parameters.AddWithValue("B_UPDATEDBY", SocialSupportBOObj.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                //return dcmd.ExecuteNonQuery();

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

        /// <summary>
        /// To Get Support By Id
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public SocialSupportBO GetSupportById(int SUPPORTEDBYID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_SUPPORT ";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("B_SUPPORTID", SUPPORTEDBYID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            SocialSupportBO SocialSupportBOObj = null;
            SocialSupportList SocialSupportListObj = new SocialSupportList();
            SocialSupportBOObj = new SocialSupportBO();

            while (dr.Read())
            {
                if (ColumnExists(dr, "SUPPORTID") && !dr.IsDBNull(dr.GetOrdinal("SUPPORTID")))
                    SocialSupportBOObj.SUPPORTEDBYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUPPORTID")));

                if (ColumnExists(dr, "SUPPORTEDBY") && !dr.IsDBNull(dr.GetOrdinal("SUPPORTEDBY")))
                    SocialSupportBOObj.SupportedBy = dr.GetString(dr.GetOrdinal("SUPPORTEDBY"));
            }
            dr.Close();

            return SocialSupportBOObj;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
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

        /// <summary>
        /// To Delete Support Row
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public string DeleteSupportRow(int SUPPORTEDBYID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETE_SUPPORT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("P_SUPPORTID", SUPPORTEDBYID);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected item is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result; 
        }

        /// <summary>
        /// To Obsolete Social Support
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteSocialSupport(int SUPPORTEDBYID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_SUPPORT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("SUPPORTID_", SUPPORTEDBYID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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