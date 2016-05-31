using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data.Sql;

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GETROLES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RoleName.ToString() == "")
            {
                cmd.Parameters.Add("@RoleNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@RoleNameIN", RoleName.ToString());
            }             
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoleBO objRole = null;            
            RoleList Roles = new RoleList();
            while (dr.Read())
            {
                objRole = new RoleBO();
                objRole.RoleID  = dr.GetInt16(dr.GetOrdinal("RoleId"));
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ALL_ROLES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RoleName.ToString() == "")
            {
                cmd.Parameters.Add("@RoleNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@RoleNameIN", RoleName.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoleBO objRole = null;
            RoleList Roles = new RoleList();
            while (dr.Read())
            {
                objRole = new RoleBO();
                objRole.RoleID = dr.GetInt16(dr.GetOrdinal("RoleId"));
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
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_INSERTROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;                       
                myCommand.Parameters.Add("@ROLENAMEIN",  objRole.RoleName);
                if (string.IsNullOrEmpty(objRole.RoleDescription) == true)
                {
                    myCommand.Parameters.Add("@RoleDescription",  " ");                                          
                }
                else
                {
                    myCommand.Parameters.Add("@RoleDescription", objRole.RoleDescription);
                }
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objRole.CreatedBy);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection=null;
            OracleCommand myCommand=null;
            
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RoleId_", roleId);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RoleId_", roleId);
                myCommand.Parameters.Add("@isdeleted_", IsDeleted);
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

        /// <summary>
        /// To Update Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public string UpdateRole(RoleBO objRole)
        {
            string result = string.Empty;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_UPDATEROLE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;                               
               myCommand.Parameters.Add("@ROLEIDIN", objRole.RoleID);                
                myCommand.Parameters.Add("@ROLENAMEIN",objRole.RoleName);
                if (string.IsNullOrEmpty(objRole.RoleDescription) == true)
                {
                    myCommand.Parameters.Add("@RoleDescription", " ");
                }
                else
                {
                    myCommand.Parameters.Add("@RoleDescription", objRole.RoleDescription);
                }                                      
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objRole.UpdatedBy);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETROLEBYROLEID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleIdIN", roleID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

