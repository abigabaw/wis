using System;
using System.Data;
using WIS_BusinessObjects;
using System.Data.SqlClient;

namespace WIS_DataAccess
{
    public class RoleDAL
    {
        /// <summary>
        /// To Get Role
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public RoleList  GetRole(string RoleName)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GETROLES";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RoleName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@RoleNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RoleNameIN", RoleName.ToString());
            }             
           // // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoleBO objRole = null;            
            RoleList Roles = new RoleList();
            while (dr.Read())
            {
                objRole = new RoleBO();
                objRole.RoleID  = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("RoleId")));
                objRole.RoleName  = dr.GetString(dr.GetOrdinal("RoleName"));
                objRole.RoleDescription = dr.GetString(dr.GetOrdinal("RoleDescription"));
                objRole.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));                
                Roles.Add(objRole);
            }
            dr.Close();
            return Roles;
        }

        /// <summary>
        /// To Get All Role
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public RoleList GetAllRole(string RoleName)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ALL_ROLES";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RoleName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@RoleNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RoleNameIN", RoleName.ToString());
            }
          //  // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoleBO objRole = null;
            RoleList Roles = new RoleList();
            while (dr.Read())
            {
                objRole = new RoleBO();
                objRole.RoleID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("RoleId")));
                objRole.RoleName = dr.GetString(dr.GetOrdinal("RoleName"));
                objRole.RoleDescription = dr.GetString(dr.GetOrdinal("RoleDescription"));
                objRole.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                Roles.Add(objRole);
            }
            dr.Close();
            return Roles;
        }

        /// <summary>
        /// To Add Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public string AddRole(RoleBO objRole)
        {            
            string result = string.Empty;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_INSERTROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;                       
                myCommand.Parameters.AddWithValue("@ROLENAMEIN",  objRole.RoleName);
                if (string.IsNullOrEmpty(objRole.RoleDescription) == true)
                {
                    myCommand.Parameters.AddWithValue("@RoleDescriptionIN",  " ");                                          
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@RoleDescriptionIN", objRole.RoleDescription);
                }
                myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objRole.CreatedBy);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;

                myConnection.Close();
            }
            return result;          
        }

        /// <summary>
        /// To Delete Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string DeleteRole(int roleId)
        {
            SqlConnection myConnection=null;
            SqlCommand myCommand=null;
            
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RoleId_", (float)roleId);
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
                    result = "Selected Role is already in use. Connot delete";
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
        /// To Obsolete Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRole(int roleId,string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
          //  try
           // {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RoleId_", roleId);
                myCommand.Parameters.AddWithValue("@isdeleted_", IsDeleted);
               // /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
               // if (myCommand.Parameters["errorMessage_"].Value != null)
               //     result = myCommand.Parameters["errorMessage_"].Value.ToString();
          /*  }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {*/
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
           // }

            return result;
        }

        /// <summary>
        /// To Update Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public string UpdateRole(RoleBO objRole)
        {
            string result = string.Empty;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_UPDATEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;                               
               myCommand.Parameters.AddWithValue("@ROLEIDIN", objRole.RoleID);                
                myCommand.Parameters.AddWithValue("@ROLENAMEIN",objRole.RoleName);
                if (string.IsNullOrEmpty(objRole.RoleDescription) == true)
                {
                    myCommand.Parameters.AddWithValue("@RoleDescription", " ");
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@RoleDescription", objRole.RoleDescription);
                }                                      
                myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objRole.UpdatedBy);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myConnection.Open();
                
                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;

                myConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// To Get Role By Role ID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public RoleBO GetRoleByRoleID(int roleID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETROLEBYROLEID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleIdIN", roleID);
          //  // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoleBO obRole = null;                        
            while (dr.Read())
            {
                obRole = new RoleBO();
                obRole.RoleID = dr.GetInt16(dr.GetOrdinal("RoleId"));
                obRole.RoleName = dr.GetString(dr.GetOrdinal("RoleName"));
                obRole.RoleDescription = dr.GetString(dr.GetOrdinal("RoleDescription"));                
            }
            dr.Close();
            return obRole;
        }  

      }
}

