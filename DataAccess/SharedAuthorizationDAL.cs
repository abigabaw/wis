using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class SharedAuthorizationDAL
    {
        OracleConnection cnn = null;
        OracleCommand cmd = null;

        /// <summary>
        /// To Add Temporary Authorization
        /// </summary>
        /// <param name="pSharedAuth"></param>
        /// <returns></returns>
        public string AddSharedAuthorization(SharedAuthorizationBO pSharedAuth)
        {
            string result = "";

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_TRN_INS_SHARED_WORKFLOW", cnn))
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("WrkFlw_SharedId_", pSharedAuth.WorkFlowSharedId);
                    cmd.Parameters.Add("ModuleId_", pSharedAuth.ModuleId);
                    cmd.Parameters.Add("WorkFlowId_", pSharedAuth.WorkFlowId);
                    cmd.Parameters.Add("ProjectId_", pSharedAuth.ProjectId);
                    cmd.Parameters.Add("AuthoriserId_", pSharedAuth.AuthoriserId);
                    cmd.Parameters.Add("Remarks_", pSharedAuth.Remarks);
                    cmd.Parameters.Add("AssignedTo_", pSharedAuth.AssignedToId);
                    cmd.Parameters.Add("UserId_", pSharedAuth.CreatedBy);
                    //   cmd.Parameters.Add("CREATEDBY_", pSharedAuth.Createdby);
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
        public SharedAuthorizationList GetSharedAuthorizationsByUser(int AuthorisedId, int ProjectID)
        {
            SharedAuthorizationBO pSharedAuth = null;
            SharedAuthorizationList SharedAuthList = null;

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_TRN_GET_SHARED_WORKFLOW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("AuthoriserId_", AuthorisedId);
                    cmd.Parameters.Add("PROJECTID_", ProjectID);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    SharedAuthList = new SharedAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                pSharedAuth = new SharedAuthorizationBO();
                                if (!dr.IsDBNull(dr.GetOrdinal("WRKFLW_SHAREDID")))
                                    pSharedAuth.WorkFlowSharedId = dr.GetInt32(dr.GetOrdinal("WRKFLW_SHAREDID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("modulename")))
                                    pSharedAuth.ModuleName = dr.GetString(dr.GetOrdinal("modulename"));
                                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE")))
                                    pSharedAuth.WorkFlow = dr.GetString(dr.GetOrdinal("WORKFLOWCODE"));
                                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                                    pSharedAuth.ProjectId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AssignedTo")))
                                    pSharedAuth.AssignedTo = dr.GetString(dr.GetOrdinal("AssignedTo"));
                                if (!dr.IsDBNull(dr.GetOrdinal("Authoriser")))
                                    pSharedAuth.AuthoriserName = dr.GetString(dr.GetOrdinal("Authoriser"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AssignedTo")))
                                    pSharedAuth.AssignedTo = dr.GetString(dr.GetOrdinal("AssignedTo"));
                                if (!dr.IsDBNull(dr.GetOrdinal("REMARKS")))
                                    pSharedAuth.Remarks = dr.GetString(dr.GetOrdinal("REMARKS"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("isdeleted"))) 
                                //    pSharedAuth.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                                SharedAuthList.Add(pSharedAuth);
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

            pSharedAuth = null;
            return SharedAuthList;
        }

        /// <summary>
        /// To Get Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <returns></returns>
        public SharedAuthorizationBO GetSharedAuthorizationsByID(int WorkFlowSharedId)//,int ProjectId)
        {
            SharedAuthorizationBO pSharedAuth = null;
            //   TemporaryAuthorizationList AuthList = null;

            using (cnn = new OracleConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new OracleCommand("USP_TRN_GET_SHARED_WRKFLW_BYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("WRKFLW_SHAREDID_", WorkFlowSharedId);
                   // cmd.Parameters.Add("projectid_", WorkFlowSharedId);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    // AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                pSharedAuth = new SharedAuthorizationBO();

                                //if (!dr.IsDBNull(dr.GetOrdinal("WRKFLW_SHAREDID")))
                                //    pSharedAuth.WorkFlowSharedId = dr.GetInt32(dr.GetOrdinal("WRKFLW_SHAREDID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                                //    pSharedAuth.ModuleId = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWID")))
                                //    pSharedAuth.WorkFlowId = dr.GetInt32(dr.GetOrdinal("WORKFLOWID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                                //    pSharedAuth.ProjectId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("AssignedTo")))
                                //    pSharedAuth.AssignedTo = dr.GetString(dr.GetOrdinal("AssignedTo"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("AUTHORISERID")))
                                //    pSharedAuth.AuthoriserId = dr.GetInt32(dr.GetOrdinal("AUTHORISERID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("ASSIGNTOID")))
                                //    pSharedAuth.AssignedToId = dr.GetInt32(dr.GetOrdinal("ASSIGNTOID"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("REMARKS")))
                                //    pSharedAuth.Remarks = dr.GetString(dr.GetOrdinal("REMARKS"));

                                if (!dr.IsDBNull(dr.GetOrdinal("WRKFLW_SHAREDID")))
                                    pSharedAuth.WorkFlowSharedId = dr.GetInt32(dr.GetOrdinal("WRKFLW_SHAREDID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("modulename")))
                                    pSharedAuth.ModuleName = dr.GetString(dr.GetOrdinal("modulename"));
                                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE")))
                                    pSharedAuth.WorkFlow = dr.GetString(dr.GetOrdinal("WORKFLOWCODE"));
                                if (!dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                                    pSharedAuth.ModuleId = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWID")))
                                    pSharedAuth.WorkFlowId = dr.GetInt32(dr.GetOrdinal("WORKFLOWID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                                    pSharedAuth.ProjectId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("assigntoid")))
                                    pSharedAuth.AssignedToId = dr.GetInt32(dr.GetOrdinal("assigntoid"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AssignedTo")))
                                    pSharedAuth.AssignedTo = dr.GetString(dr.GetOrdinal("AssignedTo"));
                                if (!dr.IsDBNull(dr.GetOrdinal("AUTHORISERID")))
                                    pSharedAuth.AuthoriserId = dr.GetInt32(dr.GetOrdinal("AUTHORISERID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("Authoriser")))
                                    pSharedAuth.AuthoriserName = dr.GetString(dr.GetOrdinal("Authoriser"));
                                //if (!dr.IsDBNull(dr.GetOrdinal("ASSIGNTOID")))
                                //    pSharedAuth.AssignedToId = dr.GetInt32(dr.GetOrdinal("ASSIGNTOID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("REMARKS")))
                                    pSharedAuth.Remarks = dr.GetString(dr.GetOrdinal("REMARKS"));

                                //AuthList.Add(pSharedAuth);
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
            return pSharedAuth;
        }

        /// <summary>
        /// To Delete Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        public void DeleteSharedAuthorizationsByID(int WorkFlowSharedId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_DEL_SHARED_WORKFLOW", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("WrkFlw_SharedId_", WorkFlowSharedId);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            //try
            //{
            //    myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            //    myCommand = new OracleCommand("USP_MST_OBS_TEMPAUTHBYID", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
            //    myCommand.Parameters.Add("isdeleted_", IsDeleted);
            //    myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    if (myCommand.Parameters["errorMessage_"].Value != null)
            //        result = myCommand.Parameters["errorMessage_"].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}

            return result;
        }
    }
}
