using System;
using System.Data;
using WIS_BusinessObjects;
using System.Data.SqlClient;

namespace WIS_DataAccess
{
    public class UserDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get All Users
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public UserList GetAllUsers(UserBO oUser)
        {
            proc = "USP_MST_GETALLUSER";
            cnn = new SqlConnection(con);
            UserBO objUser = null;
            //Role objRole = null; //Need to used assgining the Role Name

            UserList Users = new UserList();
            string strUser = string.Empty;

            if (!string.IsNullOrEmpty(oUser.UserName))
            {
                strUser = "%" + oUser.UserName + "%";
            }

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("roleid_", oUser.RoleID);
            cmd.Parameters.AddWithValue("username_", strUser);
           // // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUser = new UserBO();
                    //objRole = new Role();
                    if (ColumnExists(dr, "UserID") && !dr.IsDBNull(dr.GetOrdinal("UserID")))
                        objUser.UserID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("UserID")));
                    if (ColumnExists(dr, "UserName") && !dr.IsDBNull(dr.GetOrdinal("UserName")))
                        objUser.UserName = dr.GetString(dr.GetOrdinal("UserName"));
                    if (ColumnExists(dr, "EmailID") && !dr.IsDBNull(dr.GetOrdinal("EmailID")))
                        objUser.EmailID = dr.GetString(dr.GetOrdinal("EmailID"));
                    if (ColumnExists(dr, "RoleID") && !dr.IsDBNull(dr.GetOrdinal("RoleID")))
                        objUser.RoleID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("RoleID")));
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
            cnn = new SqlConnection(con);
            UserBO objUser = null;
            //Role objRole = null; //Need to used assgining the Role Name

            UserList Users = new UserList();
            string strUser = string.Empty;
            
            if (!string.IsNullOrEmpty(oUser.UserName))
            {
               
                strUser = "%" + oUser.UserName +"%";
            }
           
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("roleid_", oUser.RoleID);
            cmd.Parameters.AddWithValue("username_", strUser);
          //  // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);

            proc = "USP_MST_INSERTUSER";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("userid_", oUser.UserID);
            cmd.Parameters.AddWithValue("username_", oUser.UserName);
            cmd.Parameters.AddWithValue("pwd_", oUser.Pwd);
            cmd.Parameters.AddWithValue("emailid_", oUser.EmailID);
            cmd.Parameters.AddWithValue("displayname_", oUser.DisplayName);
            cmd.Parameters.AddWithValue("roleid_", oUser.RoleID);
            cmd.Parameters.AddWithValue("cellnumber_", oUser.CellNumber);
            cmd.Parameters.AddWithValue("isdeleted_", oUser.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oUser.CreatedBy);

            cmd.Parameters.AddWithValue("createddate_", oUser.CreatedDate);
            oUser.ErrorMessage = null;


            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPDATEUSER";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("userid_", oUser.UserID);
            cmd.Parameters.AddWithValue("username_", oUser.UserName);
            cmd.Parameters.AddWithValue("pwd_", oUser.Pwd);
            cmd.Parameters.AddWithValue("emailid_", oUser.EmailID);
            cmd.Parameters.AddWithValue("displayname_", oUser.DisplayName);
            cmd.Parameters.AddWithValue("roleid_", oUser.RoleID);
            cmd.Parameters.AddWithValue("cellnumber_", oUser.CellNumber);
            cmd.Parameters.AddWithValue("isdeleted_", oUser.IsDeleted);
            cmd.Parameters.AddWithValue("updatedby_", oUser.CreatedBy);
            cmd.Parameters.AddWithValue("updateddate_", oUser.CreatedDate);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            cnn = new SqlConnection(con);

            proc = "USP_MST_GETUSERBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userID_", UserID);
           // // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            UserBO objUser = null;
            //Role objRole = null; //For Assgining the RoleName
            UserList Users = new UserList();


            while (dr.Read())
            {
                objUser = new UserBO();
                // objRole = new Role();

                if (ColumnExists(dr, "UserID") && !dr.IsDBNull(dr.GetOrdinal("UserID")))
                    objUser.UserID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("UserID")));
                if (ColumnExists(dr, "UserName") && !dr.IsDBNull(dr.GetOrdinal("UserName")))
                    objUser.UserName = dr.GetString(dr.GetOrdinal("UserName"));
                if (ColumnExists(dr, "EmailID") && !dr.IsDBNull(dr.GetOrdinal("EmailID")))
                    objUser.EmailID = dr.GetString(dr.GetOrdinal("EmailID"));
                if (ColumnExists(dr, "RoleID") && !dr.IsDBNull(dr.GetOrdinal("RoleID")))
                    objUser.RoleID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("RoleID")));
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
                cnn = new SqlConnection(con);
                proc = "USP_MST_DELETEUSER";
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userid_", UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
           // try
           // {
                cnn = new SqlConnection(con);

                proc = "USP_MST_OBSOLETEUSER";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userid_", UserID);
                cmd.Parameters.AddWithValue("@isdeleted_", IsDeleted);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                cnn.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            /*}
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }  */
            cmd.Dispose();
            cnn.Close();
            cnn.Dispose();
            return result;

        }
    }
}