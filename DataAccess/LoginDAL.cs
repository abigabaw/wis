using System;
using System.Data;
using WIS_BusinessObjects;
using System.Data.SqlClient;

namespace WIS_DataAccess
{
    public class LoginDAL
    {
        /// <summary>
        /// To Authenticate username &  password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginBO Authentication(string username, string password)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_AUTHENTICATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("UserName_", username);
            cmd.Parameters.AddWithValue("Pwd_", password);
           // cmd.Parameters.AddWithValue("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LoginBO Loginobj = null;

            try
            {

                while (dr.Read())
                {
                    Loginobj = new LoginBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) Loginobj.UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) Loginobj.USERNAME = dr.GetString(dr.GetOrdinal("USERNAME")) ;
                    if (!dr.IsDBNull(dr.GetOrdinal("PWD"))) Loginobj.PASSWORD = dr.GetString(dr.GetOrdinal("PWD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISPLAYNAME"))) Loginobj.DisplayName = dr.GetString(dr.GetOrdinal("DISPLAYNAME"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Loginobj;
        }
    }
}