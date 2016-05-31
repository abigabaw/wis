using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data.Sql;

namespace WIS_DataAccess
{
    public class UserDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get All Users
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public UserList GetAllUsers(UserBO oUser)
        {
            proc = "USP_MST_GETALLUSER";
            cnn = new OracleConnection(con);
            UserBO objUser = null;
            //Role objRole = null; //Need to used assgining the Role Name

            UserList Users = new UserList();
            string strUser = string.Empty;

            if (!string.IsNullOrEmpty(oUser.UserName))
            {
                strUser = "%" + oUser.UserName + "%";
            }

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("roleid_", oUser.RoleID);
            cmd.Parameters.Add("username_", strUser);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUser = new UserBO();
                    //objRole = new Role();
                    if (ColumnExists(dr, "UserID") && !dr.IsDBNull(dr.GetOrdinal("UserID")))
                        objUser.UserID = dr.GetInt32(dr.GetOrdinal("UserID"));
                    if (ColumnExists(dr, "UserName") && !dr.IsDBNull(dr.GetOrdinal("UserName")))
                        objUser.UserName = dr.GetString(dr.GetOrdinal("UserName"));
                    if (ColumnExists(dr, "EmailID") && !dr.IsDBNull(dr.GetOrdinal("EmailID")))
                        objUser.EmailID = dr.GetString(dr.GetOrdinal("EmailID"));
                    if (ColumnExists(dr, "RoleID") && !dr.IsDBNull(dr.GetOrdinal("RoleID")))
                        objUser.RoleID = dr.GetInt32(dr.GetOrdinal("RoleID"));
                    //Role Name need to be Assigned
                    if (ColumnExists(dr, "rolename") && !dr.IsDBNull(dr.GetOrdinal("rolename")))
                    {
                        objUser.RoleName = dr.GetString(dr.GetOrdinal("rolename"));
                    }

                    if (ColumnExists(dr, "CellNumber") && !dr.IsDBNull(dr.GetOrdinal("CellNumber")))
                        objUser.CellNumber = dr.GetString(dr.GetOrdinal("CellNumber"));
                    if (ColumnExists(dr, "DisplayName") && !dr.IsDBNull(dr.GetOrdinal("DisplayName")))
                        objUser.DisplayName = dr.GetString(dr.GetOrdinal("DisplayName"));

                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objUser.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    Users.Add(objUser);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Users;
        }
        
        /// <summary>
        /// To Get Users
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public UserList GetUsers(UserBO oUser)
        {
            proc = "USP_MST_GETUSERS";
            cnn = new OracleConnection(con);
            UserBO objUser = null;
            //Role objRole = null; //Need to used assgining the Role Name

            UserList Users = new UserList();
            string strUser = string.Empty;
            
            if (!string.IsNullOrEmpty(oUser.UserName))
            {
               
                strUser = "%" + oUser.UserName +"%";
            }
           
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("roleid_", oUser.RoleID);
            cmd.Parameters.Add("username_", strUser);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUser = new UserBO();
                    //objRole = new Role();
                    if (ColumnExists(dr, "UserID") && !dr.IsDBNull(dr.GetOrdinal("UserID")))
                        objUser.UserID = dr.GetInt32(dr.GetOrdinal("UserID"));
                    if (ColumnExists(dr, "UserName") && !dr.IsDBNull(dr.GetOrdinal("UserName")))
                        objUser.UserName = dr.GetString(dr.GetOrdinal("UserName"));
                    if (ColumnExists(dr, "EmailID") && !dr.IsDBNull(dr.GetOrdinal("EmailID")))
                        objUser.EmailID = dr.GetString(dr.GetOrdinal("EmailID"));
                    if (ColumnExists(dr, "RoleID") && !dr.IsDBNull(dr.GetOrdinal("RoleID")))
                        objUser.RoleID = dr.GetInt32(dr.GetOrdinal("RoleID"));
                    //Role Name need to be Assigned
                    if (ColumnExists(dr, "rolename") && !dr.IsDBNull(dr.GetOrdinal("rolename")))
                    {
                        objUser.RoleName = dr.GetString(dr.GetOrdinal("rolename"));
                    }

                    if (ColumnExists(dr, "CellNumber") && !dr.IsDBNull(dr.GetOrdinal("CellNumber")))
                        objUser.CellNumber = dr.GetString(dr.GetOrdinal("CellNumber"));
                    if (ColumnExists(dr, "DisplayName") && !dr.IsDBNull(dr.GetOrdinal("DisplayName")))
                        objUser.DisplayName = dr.GetString(dr.GetOrdinal("DisplayName"));

                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objUser.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    Users.Add(objUser);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Users;
        }

        /// <summary>
        /// To Check Weather Column Exixts or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            string colName = columnName,rdColumnName=string.Empty;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                rdColumnName=reader.GetName(i).ToLower();
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Save User
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public string SaveUser(UserBO oUser)
        {
            string returnResult = string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INSERTUSER";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("userid_", oUser.UserID);
            cmd.Parameters.Add("username_", oUser.UserName);
            cmd.Parameters.Add("pwd_", oUser.Pwd);
            cmd.Parameters.Add("emailid_", oUser.EmailID);
            cmd.Parameters.Add("displayname_", oUser.DisplayName);
            cmd.Parameters.Add("roleid_", oUser.RoleID);
            cmd.Parameters.Add("cellnumber_", oUser.CellNumber);
            cmd.Parameters.Add("isdeleted_", oUser.IsDeleted);
            cmd.Parameters.Add("createdby_", oUser.CreatedBy);

            cmd.Parameters.Add("createddate_", oUser.CreatedDate);
            oUser.ErrorMessage = null;


            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update User
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public string UpdateUser(UserBO oUser)
        {
            string returnResult = string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPDATEUSER";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("userid_", oUser.UserID);
            cmd.Parameters.Add("username_", oUser.UserName);
            cmd.Parameters.Add("pwd_", oUser.Pwd);
            cmd.Parameters.Add("emailid_", oUser.EmailID);
            cmd.Parameters.Add("displayname_", oUser.DisplayName);
            cmd.Parameters.Add("roleid_", oUser.RoleID);
            cmd.Parameters.Add("cellnumber_", oUser.CellNumber);
            cmd.Parameters.Add("isdeleted_", oUser.IsDeleted);
            cmd.Parameters.Add("updatedby_", oUser.CreatedBy);
            cmd.Parameters.Add("updateddate_", oUser.CreatedDate);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                returnResult = string.Empty;
                throw ex;
            }

            return returnResult;
        }

        /// <summary>
        /// To Get User By Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserBO GetUserById(int UserID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GETUSERBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("userID_", UserID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            UserBO objUser = null;
            //Role objRole = null; //For Assgining the RoleName
            UserList Users = new UserList();


            while (dr.Read())
            {
                objUser = new UserBO();
                // objRole = new Role();

                if (ColumnExists(dr, "UserID") && !dr.IsDBNull(dr.GetOrdinal("UserID")))
                    objUser.UserID = dr.GetInt32(dr.GetOrdinal("UserID"));
                if (ColumnExists(dr, "UserName") && !dr.IsDBNull(dr.GetOrdinal("UserName")))
                    objUser.UserName = dr.GetString(dr.GetOrdinal("UserName"));
                if (ColumnExists(dr, "EmailID") && !dr.IsDBNull(dr.GetOrdinal("EmailID")))
                    objUser.EmailID = dr.GetString(dr.GetOrdinal("EmailID"));
                if (ColumnExists(dr, "RoleID") && !dr.IsDBNull(dr.GetOrdinal("RoleID")))
                    objUser.RoleID = dr.GetInt32(dr.GetOrdinal("RoleID"));
                if (ColumnExists(dr, "rolename") && !dr.IsDBNull(dr.GetOrdinal("rolename")))
                {
                    // string RoleName = string.Empty;
                    //objRole.RoleName = dr.GetString(dr.GetOrdinal("rolename"));
                    objUser.RoleName = dr.GetString(dr.GetOrdinal("rolename"));
                }
                if (ColumnExists(dr, "CellNumber") && !dr.IsDBNull(dr.GetOrdinal("CellNumber")))
                    objUser.CellNumber = dr.GetString(dr.GetOrdinal("CellNumber"));
                if (ColumnExists(dr, "DisplayName") && !dr.IsDBNull(dr.GetOrdinal("DisplayName")))
                    objUser.DisplayName = dr.GetString(dr.GetOrdinal("DisplayName"));

                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objUser.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objUser;
        }

        /// <summary>
        /// To Delete User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string DeleteUser(int UserID)
        {
            string result = string.Empty;
            try
            {
                cnn = new OracleConnection(con);
                proc = "USP_MST_DELETEUSER";
                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("userid_", UserID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cnn.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected User is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
                //throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Obsolete User
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteUser(int UserID,string IsDeleted)
        {
            string result = string.Empty;
            try
            {
                cnn = new OracleConnection(con);

                proc = "USP_MST_OBSOLETEUSER";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("userid_", UserID);
                cmd.Parameters.Add("@isdeleted_", IsDeleted);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cnn.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }            

            return result;

        }
    }
}