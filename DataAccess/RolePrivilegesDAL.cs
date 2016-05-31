using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class RolePrivilegesDAL
    {

        //public Role Privileges List Get Role Privileges()
        public DataTable GetRolePrivileges()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            
            string proc = "USP_MST_GET_ROLEPRIVILAGE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            OracleDataAdapter oadp = new OracleDataAdapter(cmd);
            DataSet dsRolePriv = new DataSet();

            oadp.Fill(dsRolePriv);

            //cmd.Connection.Open();
            //OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_ROLE_PRIV", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
          
                try
                {
                      dcmd.Parameters.Add("USERID_", RolePrivilegesList.UserID);
                        dcmd.Parameters.Add("MENUID_", RolePrivilegesList.MenuID);
                        dcmd.Parameters.Add("VIEWPRIVILEGE_", RolePrivilegesList.CanView);
                        dcmd.Parameters.Add("UPDATEPRIVILEGE_", RolePrivilegesList.CanUpdate);
                        dcmd.Parameters.Add("CREATEDBY_", RolePrivilegesList.UpdatedBy);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_ROLEBYUSERID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERID_", UserID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RolePrivilegesBO RolePrivilegesObj = null;
            RolePrivilegesList RolePrivilegesList = new RolePrivilegesList();
            
            while (dr.Read())
            {
                RolePrivilegesObj = new RolePrivilegesBO();

                if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) RolePrivilegesObj.UserID = dr.GetInt32(dr.GetOrdinal("USERID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MENUID"))) RolePrivilegesObj.MenuID = dr.GetInt32(dr.GetOrdinal("MENUID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MENULEVEL"))) RolePrivilegesObj.MenuLevel = dr.GetInt32(dr.GetOrdinal("MENULEVEL"));
                if (!dr.IsDBNull(dr.GetOrdinal("MENUNAME"))) RolePrivilegesObj.MenuName = dr.GetString(dr.GetOrdinal("MENUNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("CANVIEW"))) RolePrivilegesObj.CanView = dr.GetString(dr.GetOrdinal("CANVIEW"));
                if (!dr.IsDBNull(dr.GetOrdinal("CANUPDATE"))) RolePrivilegesObj.CanUpdate = dr.GetString(dr.GetOrdinal("CANUPDATE"));
                if (!dr.IsDBNull(dr.GetOrdinal("CHILDMENUCOUNT"))) RolePrivilegesObj.ChildMenuCount = dr.GetInt32(dr.GetOrdinal("CHILDMENUCOUNT"));
                
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_UPD_ROLE_PRIV";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERID_", DeletedID);
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}