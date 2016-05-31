using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class TemporaryAuthorizationDAL
    {
        OracleConnection cnn = null;
        OracleCommand cmd = null;

        /// <summary>
        /// To Add Temporary Authorization
        /// </summary>
        /// <param name="objAuth"></param>
        /// <returns></returns>
        public string AddTemporaryAuthorization(TemporaryAuthorizationBO objAuth)
        {
            string result = "";

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_INS_APPROVALTEMPAUTHORISER", cnn))
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("APPROVALTEMPAUTHORISERID_", objAuth.Approvaltempauthoriserid);
                    cmd.Parameters.Add("AUTHORISERID_", objAuth.Authoriserid);
                    cmd.Parameters.Add("ASSIGNTOID_", objAuth.Assigntoid);
                    cmd.Parameters.Add("FROMDATE_", objAuth.Fromdate);
                    cmd.Parameters.Add("TODATE_", objAuth.Todate);
                    cmd.Parameters.Add("REMARKS_", objAuth.Remarks);
                    cmd.Parameters.Add("PROJECTID_", objAuth.ProjectID);
                    cmd.Parameters.Add("CREATEDBY_", objAuth.Createdby);
                    cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    
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

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_TRN_GET_TEMPAUTHBYUSERID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CREATEDBY_", userID);
                    cmd.Parameters.Add("PROJECTID_", ProjectID);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_TRN_GET_TEMPAUTHBYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_DEL_TEMPAUTHBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_TEMPAUTHBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
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
    }
}
