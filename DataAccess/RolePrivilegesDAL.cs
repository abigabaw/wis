using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;
using System.IO;

namespace WIS_DataAccess
{
    public class RolePrivilegesDAL
    {
        public static void Log(string logMessage, TextWriter w)
        {
          //  w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1} {2} :", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString(), logMessage);
           // w.WriteLine("  :");
         //   w.WriteLine("  :{0}", logMessage);
         //   w.WriteLine("-------------------------------");
        }

        //public Role Privileges List Get Role Privileges()
        public DataTable GetRolePrivileges()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            
            string proc = "USP_MST_GET_ROLEPRIVILAGE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
         //   //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            SqlDataAdapter oadp = new SqlDataAdapter(cmd);
            DataSet dsRolePriv = new DataSet();

            oadp.Fill(dsRolePriv);

            //cmd.Connection.Open();
            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //RolePrivilegesBO objRolePrivileges = null;
            //RolePrivilegesList RolePrivilegesList = new RolePrivilegesList();

            //while (dr.Read())
            //{
            //    objRolePrivileges = new RolePrivilegesBO();
            //    objRolePrivileges.MenuID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ChildMenuID")));
            //    //objRolePrivileges.ModuleName = dr.GetString(dr.GetOrdinal("childmenu"));
            //    objRolePrivileges.MenuDescription = dr.GetString(dr.GetOrdinal("MenuDescription"));
            //    if (!dr.IsDBNull(dr.GetOrdinal("CHILDMENUCOUNT"))) objRolePrivileges.ChildMenuCount = dr.GetInt32(dr.GetOrdinal("CHILDMENUCOUNT"));

            //    RolePrivilegesList.Add(objRolePrivileges);
            //}

            //dr.Close();

            cmd.Connection.Close();
            return dsRolePriv.Tables[0];
        }

        /// <summary>
        /// To Insert Role Privilages
        /// </summary>
        /// <param name="RolePrivilegesList"></param>
        /// <returns></returns>
        public int InsertRolePrivilages(RolePrivilegesBO RolePrivilegesList)
        {
            
            int Result = 0;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_ROLE_PRIV", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
          
                try
                {
                      dcmd.Parameters.AddWithValue("USERID_", RolePrivilegesList.UserID);
                        dcmd.Parameters.AddWithValue("MENUID_", RolePrivilegesList.MenuID);
                        dcmd.Parameters.AddWithValue("VIEWPRIVILEGE_", RolePrivilegesList.CanView);
                        dcmd.Parameters.AddWithValue("UPDATEPRIVILEGE_", RolePrivilegesList.CanUpdate);
                        dcmd.Parameters.AddWithValue("CREATEDBY_", RolePrivilegesList.UpdatedBy);

                        Result = dcmd.ExecuteNonQuery();
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
            return Result;

            }      
        

        // to check the Column are Exists or not
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
        /// To Get ROLE PRI Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public RolePrivilegesList GetROLEPRIId(int UserID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ROLEBYUSERID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("USERID_", UserID);
           // // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RolePrivilegesBO RolePrivilegesObj = null;
            RolePrivilegesList RolePrivilegesList = new RolePrivilegesList();
            
            while (dr.Read())
            {
                RolePrivilegesObj = new RolePrivilegesBO();

                using (StreamWriter w = File.AppendText("c:/WIS/log.txt"))
                {
                   // Log("Test1", w);
                    Log(dr.GetDataTypeName(dr.GetOrdinal("CHILDMENUCOUNT")), w);
                }
                if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) RolePrivilegesObj.UserID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("USERID")));
                if (!dr.IsDBNull(dr.GetOrdinal("MENUID"))) RolePrivilegesObj.MenuID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MENUID")));
                if (!dr.IsDBNull(dr.GetOrdinal("MENULEVEL"))) RolePrivilegesObj.MenuLevel = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MENULEVEL")));
                if (!dr.IsDBNull(dr.GetOrdinal("MENUNAME"))) RolePrivilegesObj.MenuName = dr.GetString(dr.GetOrdinal("MENUNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("CANVIEW"))) RolePrivilegesObj.CanView = dr.GetString(dr.GetOrdinal("CANVIEW"));
                if (!dr.IsDBNull(dr.GetOrdinal("CANUPDATE"))) RolePrivilegesObj.CanUpdate = dr.GetString(dr.GetOrdinal("CANUPDATE"));
                if (!dr.IsDBNull(dr.GetOrdinal("CHILDMENUCOUNT"))) RolePrivilegesObj.ChildMenuCount = Convert.ToInt32(dr.GetInt64(dr.GetOrdinal("CHILDMENUCOUNT")));
                
                if (!dr.IsDBNull(dr.GetOrdinal("PROJDEPENDENT")))
                    RolePrivilegesObj.ProjectDependent = dr.GetString(dr.GetOrdinal("PROJDEPENDENT"));
                else
                    RolePrivilegesObj.ProjectDependent = "N";

                if (!dr.IsDBNull(dr.GetOrdinal("PAPDEPENDENT")))
                    RolePrivilegesObj.PAPDependent = dr.GetString(dr.GetOrdinal("PAPDEPENDENT"));
                else
                    RolePrivilegesObj.PAPDependent = "N";

                RolePrivilegesList.Add(RolePrivilegesObj);
            }

            dr.Close();
            return RolePrivilegesList;
        }

        /// <summary>
        /// To Delete Role Privileges
        /// </summary>
        /// <param name="DeletedID"></param>
        /// <returns></returns>
        public int DeleteRolePrivileges(int DeletedID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_UPD_ROLE_PRIV";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("USERID_", DeletedID);
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}