using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;


namespace WIS_DataAccess
{
    public class GOUAllowanceDAL
    {
        string returnResult = string.Empty;
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="GOUAllowanceBOobj"></param>
        /// <returns></returns>
        public string Insert(GOUAllowanceBO GOUAllowanceBOobj)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_GOUALLOWANC", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("p_GOUALLOWANCECATEGORY", GOUAllowanceBOobj.GOUAllowanceCategory);
                dcmd.Parameters.Add("p_GOUALLOWANCEVALUE", GOUAllowanceBOobj.GOUAllowanceValue);
                dcmd.Parameters.Add("p_CREATEDBY", GOUAllowanceBOobj.Createdby);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                //return dcmd.ExecuteNonQuery();

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                dcmd.Dispose();
                con.Close();
                con.Dispose();

            }
            return returnResult;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public GOUAllowanceList GetGOUAllowance()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_GOUALLOWANC";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GOUAllowanceBO objUser = null;
            GOUAllowanceList ReasonList = new GOUAllowanceList();

            while (dr.Read())
            {
                objUser = new GOUAllowanceBO();
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORYID")))
                    objUser.GOUALLOWANCECATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOUALLOWANCECATEGORYID")));
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORY")))
                    objUser.GOUAllowanceCategory = dr.GetString(dr.GetOrdinal("GOUALLOWANCECATEGORY"));
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCEVALUE")))
                    objUser.GOUAllowanceValue = Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("GOUALLOWANCEVALUE")));

                ReasonList.Add(objUser);
            }

            dr.Close();

            return ReasonList;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public GOUAllowanceList GetAllSchoolGOUAllowance()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_GOUALLOWANC";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GOUAllowanceBO objUser = null;
            GOUAllowanceList ReasonList = new GOUAllowanceList();

            while (dr.Read())
            {
                objUser = new GOUAllowanceBO();
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORYID")))
                    objUser.GOUALLOWANCECATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOUALLOWANCECATEGORYID")));
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORY")))
                    objUser.GOUAllowanceCategory = dr.GetString(dr.GetOrdinal("GOUALLOWANCECATEGORY"));
                if (!dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCEVALUE")))
                    objUser.GOUAllowanceValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("GOUALLOWANCEVALUE")));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    objUser.Isdeleted = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ISDELETED")));

                ReasonList.Add(objUser);
            }

            dr.Close();

            return ReasonList;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="GouAllowanceID"></param>
        /// <returns></returns>
        public GOUAllowanceBO GetGOUAllowancebyID(int GouAllowanceID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_GOUALLOWANCEBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_GOUALLOWANCECATEGORYID", GouAllowanceID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GOUAllowanceBO GOUAllowanceBOobj = null;
            GOUAllowanceList Users = new GOUAllowanceList();

            GOUAllowanceBOobj = new GOUAllowanceBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "GOUALLOWANCECATEGORY") && !dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORY")))
                    GOUAllowanceBOobj.GOUAllowanceCategory = dr.GetString(dr.GetOrdinal("GOUALLOWANCECATEGORY"));
                if (ColumnExists(dr, "GOUALLOWANCEVALUE") && !dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCEVALUE")))
                    GOUAllowanceBOobj.GOUAllowanceValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("GOUALLOWANCEVALUE")));
                if (ColumnExists(dr, "GOUALLOWANCECATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("GOUALLOWANCECATEGORYID")))
                    GOUAllowanceBOobj.GOUALLOWANCECATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOUALLOWANCECATEGORYID")));

            }
            dr.Close();


            return GOUAllowanceBOobj;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="GOUAllowanceBOobj"></param>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Update(GOUAllowanceBO GOUAllowanceBOobj, int reasonid)
        {
            string returnResult = string.Empty;
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPD_GOUALLOWANC", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_GOUALLOWANCECATEGORYID", reasonid);
                dCmd.Parameters.Add("p_GOUALLOWANCECATEGORY", GOUAllowanceBOobj.GOUAllowanceCategory);
                dCmd.Parameters.Add("p_GOUALLOWANCEVALUE", GOUAllowanceBOobj.GOUAllowanceValue);
                dCmd.Parameters.Add("p_CREATEDBY", GOUAllowanceBOobj.Createdby);
                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                //return dCmd.ExecuteNonQuery();

                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="reasonid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsolete(int reasonid, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_GOUALLOWANC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_SCH_DRP_REASONID", reasonid);
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
        /// <summary>
        /// to check whether column exists
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// to delete data
        /// </summary>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Delete(int reasonid)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_GOUALLOWANC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_GOUALLOWANCECATEGORYID", reasonid);
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
    }
}
