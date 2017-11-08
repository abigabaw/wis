using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class TemporaryAuthorizationDAL
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;

        /// <summary>
        /// To Add Temporary Authorization
        /// </summary>
        /// <param name="objAuth"></param>
        /// <returns></returns>
        public string AddTemporaryAuthorization(TemporaryAuthorizationBO objAuth)
        {
            string result = "";

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_INS_APPROVALTEMPAUTHORISER", cnn))
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("APPROVALTEMPAUTHORISERID_", objAuth.Approvaltempauthoriserid);
                    cmd.Parameters.AddWithValue("AUTHORISERID_", objAuth.Authoriserid);
                    cmd.Parameters.AddWithValue("ASSIGNTOID_", objAuth.Assigntoid);
                    cmd.Parameters.AddWithValue("FROMDATE_", objAuth.Fromdate);
                    cmd.Parameters.AddWithValue("TODATE_", objAuth.Todate);
                    cmd.Parameters.AddWithValue("REMARKS_", objAuth.Remarks);
                    cmd.Parameters.AddWithValue("PROJECTID_", objAuth.ProjectID);
                    cmd.Parameters.AddWithValue("CREATEDBY_", objAuth.Createdby);
                    cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                    
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["errorMessage_"].Value != null)
                        result = cmd.Parameters["errorMessage_"].Value.ToString();
                    else
                        result = string.Empty;
                }
            }

            return result;
        }

        /// <summary>
        /// To Get Temp Authorizations By User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public TemporaryAuthorizationList GetTempAuthorizationsByUser(int userID, int ProjectID)
        {
            TemporaryAuthorizationBO objAuth = null;
            TemporaryAuthorizationList AuthList = null;

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_TRN_GET_TEMPAUTHBYUSERID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CREATEDBY_", userID);
                    cmd.Parameters.AddWithValue("PROJECTID_", ProjectID);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                objAuth = new TemporaryAuthorizationBO();

                                if (!dr.IsDBNull(dr.GetOrdinal("APPROVALTEMPAUTHORISERID"))) objAuth.Approvaltempauthoriserid = dr.GetInt32(dr.GetOrdinal("APPROVALTEMPAUTHORISERID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AuthoriserName"))) objAuth.AuthoriserName = dr.GetString(dr.GetOrdinal("AuthoriserName"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AssignedTo"))) objAuth.AssignedTo = dr.GetString(dr.GetOrdinal("AssignedTo"));
                                if (!dr.IsDBNull(dr.GetOrdinal("fromdate"))) objAuth.Fromdate = dr.GetDateTime(dr.GetOrdinal("fromdate"));
                                if (!dr.IsDBNull(dr.GetOrdinal("todate"))) objAuth.Todate = dr.GetDateTime(dr.GetOrdinal("todate"));
                                if (!dr.IsDBNull(dr.GetOrdinal("remarks"))) objAuth.Remarks = dr.GetString(dr.GetOrdinal("remarks"));
                                if (!dr.IsDBNull(dr.GetOrdinal("isdeleted"))) objAuth.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                                AuthList.Add(objAuth);
                            }

                            dr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            objAuth = null;
            return AuthList;
        }

        /// <summary>
        /// To Get Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <returns></returns>
        public TemporaryAuthorizationBO GetTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID)
        {
            TemporaryAuthorizationBO objAuth = null;
            TemporaryAuthorizationList AuthList = null;

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_TRN_GET_TEMPAUTHBYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                objAuth = new TemporaryAuthorizationBO();

                                if (!dr.IsDBNull(dr.GetOrdinal("APPROVALTEMPAUTHORISERID"))) objAuth.Approvaltempauthoriserid = dr.GetInt32(dr.GetOrdinal("APPROVALTEMPAUTHORISERID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AUTHORISERID"))) objAuth.AuthoriserName = dr.GetValue(dr.GetOrdinal("AUTHORISERID")).ToString();
                                if (!dr.IsDBNull(dr.GetOrdinal("ASSIGNTOID"))) objAuth.AssignedTo = dr.GetValue(dr.GetOrdinal("ASSIGNTOID")).ToString();
                                if (!dr.IsDBNull(dr.GetOrdinal("fromdate"))) objAuth.Fromdate = dr.GetDateTime(dr.GetOrdinal("fromdate"));
                                if (!dr.IsDBNull(dr.GetOrdinal("todate"))) objAuth.Todate = dr.GetDateTime(dr.GetOrdinal("todate"));
                                if (!dr.IsDBNull(dr.GetOrdinal("remarks"))) objAuth.Remarks = dr.GetString(dr.GetOrdinal("remarks"));

                                //AuthList.Add(objAuth);
                            }

                            dr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return objAuth;
        }

        /// <summary>
        /// To Delete Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        public void DeleteTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DEL_TEMPAUTHBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }
        }

        /// <summary>
        /// To Obsolete Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_TEMPAUTHBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
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
    }
}
