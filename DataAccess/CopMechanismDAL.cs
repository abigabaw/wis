using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CopMechanismDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// Get ALL CopMechanism
        /// </summary>
        /// <returns></returns>
        public CopMechanismList GetALLCopMechanism()//(CopMechanism oCopMechanism)
        {
            proc = "USP_MST_GETALLCOP_MECHANISM";
            cnn = new SqlConnection(con);
            CopMechanismBO objCopMechanism = null;

            CopMechanismList lstCopMechanismList = new CopMechanismList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCopMechanism = new CopMechanismBO();

                    if (ColumnExists(dr, "cop_mechanismid") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanismid")))
                        objCopMechanism.CopMechanismID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cop_mechanismid")));
                    if (ColumnExists(dr, "cop_mechanism") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanism")))
                        objCopMechanism.CopMechanismName = dr.GetString(dr.GetOrdinal("cop_mechanism"));
                    if (ColumnExists(dr, "isdeleted") && !dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                        objCopMechanism.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                    lstCopMechanismList.Add(objCopMechanism);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstCopMechanismList;
        }

        /// <summary>
        /// Get Cop Mechanism
        /// </summary>
        /// <returns></returns>
        public CopMechanismList GetCopMechanism()//(CopMechanism oCopMechanism)
        {
            proc = "USP_MST_GET_COPMECHANISM";
            cnn = new SqlConnection(con);
            CopMechanismBO objCopMechanism = null;
            
            CopMechanismList lstCopMechanismList = new CopMechanismList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCopMechanism = new CopMechanismBO();
                  
                    if (ColumnExists(dr, "cop_mechanismid") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanismid")))
                        objCopMechanism.CopMechanismID = Convert.ToInt32( dr.GetValue(dr.GetOrdinal("cop_mechanismid")));
                    if (ColumnExists(dr, "cop_mechanism") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanism")))
                        objCopMechanism.CopMechanismName = dr.GetString(dr.GetOrdinal("cop_mechanism"));
                   
                   
                    lstCopMechanismList.Add(objCopMechanism);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstCopMechanismList;
        }
        /// <summary>
        /// To check whether column exists
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
        /// <summary>
        /// To insert details to databse
        /// </summary>
        /// <param name="oCopMechanism"></param>
        /// <returns></returns>
        public string SaveCopMechanism(CopMechanismBO oCopMechanism)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_MST_INSERT_COPMECHANISM";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
          
            cmd.Parameters.AddWithValue("cop_mechanism_", oCopMechanism.CopMechanismName);

            cmd.Parameters.AddWithValue("isdeleted_", oCopMechanism.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oCopMechanism.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// To update details into database
        /// </summary>
        /// <param name="oCopMechanism"></param>
        /// <returns></returns>
        public string UpdateCopMechanism(CopMechanismBO oCopMechanism)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPDATE_COPMECHANISM";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("cop_mechanismid_", oCopMechanism.CopMechanismID);
            cmd.Parameters.AddWithValue("cop_mechanism_", oCopMechanism.CopMechanismName);
           
            cmd.Parameters.AddWithValue("updatedby_", oCopMechanism.CreatedBy);

            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// Get CopMechanism By Id
        /// </summary>
        /// <param name="CopMechanismID"></param>
        /// <returns></returns>
        public CopMechanismBO GetCopMechanismById(int CopMechanismID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_MST_GET_COPMECHANISM_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cop_mechanismid_", CopMechanismID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CopMechanismBO objCopMechanism = null;
                    
           
            while (dr.Read())
            {
                objCopMechanism = new CopMechanismBO();
             
                if (ColumnExists(dr, "cop_mechanismid") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanismid")))
                    objCopMechanism.CopMechanismID =Convert.ToInt32( dr.GetValue(dr.GetOrdinal("cop_mechanismid")));
                if (ColumnExists(dr, "cop_mechanism") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanism")))
                    objCopMechanism.CopMechanismName = dr.GetString(dr.GetOrdinal("cop_mechanism"));
            }
            dr.Close();

            return objCopMechanism;
        }

        /// <summary>
        /// To make obsolete
        /// </summary>
        /// <param name="CopMechanismID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCopMechanism(int CopMechanismID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_COPMECH", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("COP_MECHANISMID_", CopMechanismID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
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

        /// <summary>
        /// To delete details from database
        /// </summary>
        /// <param name="CopMechanismID"></param>
        /// <returns></returns>
        public string DeleteCopMechanism(int CopMechanismID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETE_COPMECHANISM", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("cop_mechanismid_", CopMechanismID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected item is already in use. Cannot delete";
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
    }
}