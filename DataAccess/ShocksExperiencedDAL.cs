using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ShocksExperiencedDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
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
            cnn = new SqlConnection(con);
            ShocksExperiencedBO objShocksExperienced = null;

            ShocksExperiencedList lstShocksExperiencedList = new ShocksExperiencedList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objShocksExperienced = new ShocksExperiencedBO();

                    if (ColumnExists(dr, "SHOCKID") && !dr.IsDBNull(dr.GetOrdinal("SHOCKID")))
                        objShocksExperienced.ShocksExperiencedID = (int)dr.GetDecimal(dr.GetOrdinal("SHOCKID"));
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
            cnn = new SqlConnection(con);
            ShocksExperiencedBO objShocksExperienced = null;
            
            ShocksExperiencedList lstShocksExperiencedList = new ShocksExperiencedList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);

            proc = "USP_MST_GET_SHOCKSEXP_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("shockid_", ShockID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            cnn = new SqlConnection(con);

            proc = "USP_MST_INS_SHOCKSEXPERIENCED";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("shockexperienced_", oShocksExperienced.ShocksExperience);

            cmd.Parameters.AddWithValue("isdeleted_", oShocksExperienced.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oShocksExperienced.CreatedBy);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPD_SHOCKSEXPERIENCED";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("shockid_", oShocksExperienced.ShocksExperiencedID);
            cmd.Parameters.AddWithValue("shockexperienced_", oShocksExperienced.ShocksExperience);
           
            cmd.Parameters.AddWithValue("updatedby_", oShocksExperienced.CreatedBy);
    
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_SHOCKSEXPERIENCED", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("shockid_", ShockID);
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
        #endregion

        /// <summary>
        /// To Obsolete shock experienced id
        /// </summary>
        /// <param name="ShockID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoleteshockexperiencedid(int ShockID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_SHOCKEXP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("SHOCKID_", ShockID);
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