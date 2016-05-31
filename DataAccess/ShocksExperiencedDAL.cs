using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ShocksExperiencedDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get ALL Shocks Experienced
        /// </summary>
        /// <returns></returns>
        public ShocksExperiencedList GetALLShocksExperienced()//(ShocksExperienced oShocksExperienced)
        {
            proc = "USP_MST_GETALL_SHOCKSEXP";
            cnn = new OracleConnection(con);
            ShocksExperiencedBO objShocksExperienced = null;

            ShocksExperiencedList lstShocksExperiencedList = new ShocksExperiencedList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objShocksExperienced = new ShocksExperiencedBO();

                    if (ColumnExists(dr, "SHOCKID") && !dr.IsDBNull(dr.GetOrdinal("SHOCKID")))
                        objShocksExperienced.ShocksExperiencedID = dr.GetInt32(dr.GetOrdinal("SHOCKID"));
                    if (ColumnExists(dr, "SHOCKEXPERIENCED") && !dr.IsDBNull(dr.GetOrdinal("SHOCKEXPERIENCED")))
                        objShocksExperienced.ShocksExperience = dr.GetString(dr.GetOrdinal("SHOCKEXPERIENCED"));
                    if (ColumnExists(dr, "isdeleted") && !dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                        objShocksExperienced.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                    lstShocksExperiencedList.Add(objShocksExperienced);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstShocksExperiencedList;
        }

        /// <summary>
        /// To Get Shocks Experienced
        /// </summary>
        /// <returns></returns>
        public ShocksExperiencedList GetShocksExperienced()//(ShocksExperienced oShocksExperienced)
        {
            proc = "USP_MST_GET_SHOCKSEXPERIENCED";
            cnn = new OracleConnection(con);
            ShocksExperiencedBO objShocksExperienced = null;
            
            ShocksExperiencedList lstShocksExperiencedList = new ShocksExperiencedList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objShocksExperienced = new ShocksExperiencedBO();

                    if (ColumnExists(dr, "SHOCKID") && !dr.IsDBNull(dr.GetOrdinal("SHOCKID")))
                        objShocksExperienced.ShocksExperiencedID = dr.GetInt32(dr.GetOrdinal("SHOCKID"));
                    if (ColumnExists(dr, "SHOCKEXPERIENCED") && !dr.IsDBNull(dr.GetOrdinal("SHOCKEXPERIENCED")))
                        objShocksExperienced.ShocksExperience = dr.GetString(dr.GetOrdinal("SHOCKEXPERIENCED"));
                   
                    lstShocksExperiencedList.Add(objShocksExperienced);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstShocksExperiencedList;
        }

        /// <summary>
        /// To Get Shocks Experienced By Id
        /// </summary>
        /// <param name="ShockID"></param>
        /// <returns></returns>
        public ShocksExperiencedBO GetShocksExperiencedById(int ShockID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_SHOCKSEXP_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("shockid_", ShockID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ShocksExperiencedBO objShocksExperienced = null;

            while (dr.Read())
            {
                objShocksExperienced = new ShocksExperiencedBO();

                if (ColumnExists(dr, "SHOCKID") && !dr.IsDBNull(dr.GetOrdinal("SHOCKID")))
                    objShocksExperienced.ShocksExperiencedID = dr.GetInt32(dr.GetOrdinal("SHOCKID"));
                if (ColumnExists(dr, "SHOCKEXPERIENCED") && !dr.IsDBNull(dr.GetOrdinal("SHOCKEXPERIENCED")))
                    objShocksExperienced.ShocksExperience = dr.GetString(dr.GetOrdinal("SHOCKEXPERIENCED"));
            }
            dr.Close();

            return objShocksExperienced;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Save, Update & Delete Record
        /// <summary>
        /// To Save Shocks Experienced
        /// </summary>
        /// <param name="oShocksExperienced"></param>
        /// <returns></returns>
        public string SaveShocksExperienced(ShocksExperiencedBO oShocksExperienced)
        {
            string returnResult=string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_SHOCKSEXPERIENCED";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("shockexperienced_", oShocksExperienced.ShocksExperience);

            cmd.Parameters.Add("isdeleted_", oShocksExperienced.IsDeleted);
            cmd.Parameters.Add("createdby_", oShocksExperienced.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;    

            return returnResult;
        }

        /// <summary>
        /// To Update Shocks Experienced
        /// </summary>
        /// <param name="oShocksExperienced"></param>
        /// <returns></returns>
        public string UpdateShocksExperienced(ShocksExperiencedBO oShocksExperienced)
        {
            string returnResult = string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_SHOCKSEXPERIENCED";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("shockid_", oShocksExperienced.ShocksExperiencedID);
            cmd.Parameters.Add("shockexperienced_", oShocksExperienced.ShocksExperience);
           
            cmd.Parameters.Add("updatedby_", oShocksExperienced.CreatedBy);
    
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;    

            return returnResult;
        }

        /// <summary>
        /// To Delete Shocks Experienced
        /// </summary>
        /// <param name="ShockID"></param>
        /// <returns></returns>
        public string DeleteShocksExperienced(int ShockID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_SHOCKSEXPERIENCED", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("shockid_", ShockID);
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
        #endregion

        /// <summary>
        /// To Obsolete shock experienced id
        /// </summary>
        /// <param name="ShockID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoleteshockexperiencedid(int ShockID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_SHOCKEXP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("SHOCKID_", ShockID);
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