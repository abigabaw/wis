using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LiteracyStatusDAL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="LiteracyStatusBoobj"></param>
        /// <returns></returns>
        public string Insert(LiteracyStatusBO LiteracyStatusBoobj)
        {
            string returnResult = string.Empty;
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTLITERACYSTATUS", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("LTR_STATUS", LiteracyStatusBoobj.LTR_STATUS);
                dcmd.Parameters.AddWithValue("DESCRIPTION", LiteracyStatusBoobj.DESCRIPTION);
                dcmd.Parameters.AddWithValue("createdby", LiteracyStatusBoobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();

                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
        /// To Get All Literacy Status
        /// </summary>
        /// <returns></returns>
        public LiteracyStatusList GetAllLiteracyStatus()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLLITERACYSTATUS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LiteracyStatusBO LiteracyStatusBOobj = null;
            LiteracyStatusList LiteracyStatusListobj = new LiteracyStatusList();

            while (dr.Read())
            {
                LiteracyStatusBOobj = new LiteracyStatusBO();
                if (!dr.IsDBNull(dr.GetOrdinal("LTR_STATUSID")))
                    LiteracyStatusBOobj.LTR_STATUSID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LTR_STATUSID")));
                if (!dr.IsDBNull(dr.GetOrdinal("LTR_STATUS")))
                    LiteracyStatusBOobj.LTR_STATUS = dr.GetString(dr.GetOrdinal("LTR_STATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    LiteracyStatusBOobj.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    LiteracyStatusBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                LiteracyStatusListobj.Add(LiteracyStatusBOobj);
            }

            dr.Close();

            return LiteracyStatusListobj;
        }

        /// <summary>
        /// To Get Literacy Status
        /// </summary>
        /// <returns></returns>
        public LiteracyStatusList GetLiteracyStatus()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETLITERACYSTATUS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LiteracyStatusBO LiteracyStatusBOobj = null;
            LiteracyStatusList LiteracyStatusListobj = new LiteracyStatusList();

            while (dr.Read())
            {
                LiteracyStatusBOobj = new LiteracyStatusBO();
                if (!dr.IsDBNull(dr.GetOrdinal("LTR_STATUSID")))
                    LiteracyStatusBOobj.LTR_STATUSID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LTR_STATUSID")));
                if (!dr.IsDBNull(dr.GetOrdinal("LTR_STATUS")))
                    LiteracyStatusBOobj.LTR_STATUS = dr.GetString(dr.GetOrdinal("LTR_STATUS"));

                LiteracyStatusListobj.Add(LiteracyStatusBOobj);
            }

            dr.Close();

            return LiteracyStatusListobj;
        }

        //public int Delete(int literacyStatusID)
        //{
        //    SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
        //    conn.Open();
        //    SqlCommand dCmd = new SqlCommand("USP_MST_DELETELITERACYSTATUS", conn);
        //    dCmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        dCmd.Parameters.AddWithValue("L_LTR_STATUSID", literacyStatusID);
        //        return dCmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        dCmd.Dispose();
        //        conn.Close();
        //        conn.Dispose();

        //    }
        //}

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="literacyStatusID"></param>
        /// <returns></returns>
        public string Delete(int literacyStatusID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETELITERACYSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("L_LTR_STATUSID", literacyStatusID);
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

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="LitStatusBoobj"></param>
        /// <param name="litStatusID"></param>
        /// <returns></returns>
        public string Update(LiteracyStatusBO LitStatusBoobj, int litStatusID)
        {
            string returnResult = string.Empty;
            SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_UPDATELITERACYSTATUS", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.AddWithValue("L_LTR_STATUSID", litStatusID);
                dCmd.Parameters.AddWithValue("L_LTR_STATUS", LitStatusBoobj.LTR_STATUS);
                dCmd.Parameters.AddWithValue("L_DESCRIPTION", LitStatusBoobj.DESCRIPTION);
                dCmd.Parameters.AddWithValue("L_CREATEDBY", LitStatusBoobj.CREATEDBY);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;


                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
                //return dCmd.ExecuteNonQuery();
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

        //newly added
        /// <summary>
        /// To Obsolete Literacy Status
        /// </summary>
        /// <param name="literacyStatusID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLiteracyStatus(int literacyStatusID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETELITERACYSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("L_LTR_STATUSID", literacyStatusID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
    }
}