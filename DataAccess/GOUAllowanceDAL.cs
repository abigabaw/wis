using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_GOUALLOWANC", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("p_GOUALLOWANCECATEGORY", GOUAllowanceBOobj.GOUAllowanceCategory);
                dcmd.Parameters.AddWithValue("p_GOUALLOWANCEVALUE", GOUAllowanceBOobj.GOUAllowanceValue);
                dcmd.Parameters.AddWithValue("p_CREATEDBY", GOUAllowanceBOobj.Createdby);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_GOUALLOWANC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_GOUALLOWANC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_GOUALLOWANCEBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_GOUALLOWANCECATEGORYID", GouAllowanceID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_UPD_GOUALLOWANC", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.AddWithValue("p_GOUALLOWANCECATEGORYID", reasonid);
                dCmd.Parameters.AddWithValue("p_GOUALLOWANCECATEGORY", GOUAllowanceBOobj.GOUAllowanceCategory);
                dCmd.Parameters.AddWithValue("p_GOUALLOWANCEVALUE", GOUAllowanceBOobj.GOUAllowanceValue);
                dCmd.Parameters.AddWithValue("p_CREATEDBY", GOUAllowanceBOobj.Createdby);
                dCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_GOUALLOWANC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("p_SCH_DRP_REASONID", reasonid);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_GOUALLOWANC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("p_GOUALLOWANCECATEGORYID", reasonid);
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
