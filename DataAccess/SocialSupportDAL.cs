using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_SUPPORT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_SUPPORT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            OracleConnection Con = new OracleConnection(ConStr);
            Con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_INSERT_SUPPORT", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.Add("S_SUPPORTEDBY", SocialSupportBOObj.SupportedBy);
                cmd.Parameters.Add("S_CREATEDBY", SocialSupportBOObj.CreatedBy);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATE_SUPPORTEDBY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("B_SUPPORTID", SUPPORTEDBYID);
                dcmd.Parameters.Add("B_UPDATEDBY", SocialSupportBOObj.SupportedBy);
                dcmd.Parameters.Add("B_UPDATEDBY", SocialSupportBOObj.CreatedBy);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_SUPPORT ";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("B_SUPPORTID", SUPPORTEDBYID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETE_SUPPORT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("P_SUPPORTID", SUPPORTEDBYID);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_SUPPORT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("SUPPORTID_", SUPPORTEDBYID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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