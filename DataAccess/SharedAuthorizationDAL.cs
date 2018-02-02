using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class SharedAuthorizationDAL
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;

        /// <summary>
        /// To Add Temporary Authorization
        /// </summary>
        /// <param name="pSharedAuth"></param>
        /// <returns></returns>
        public string AddSharedAuthorization(SharedAuthorizationBO pSharedAuth)
        {
            string result = "";

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_TRN_INS_SHARED_WORKFLOW", cnn))
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("WrkFlw_SharedId_", pSharedAuth.WorkFlowSharedId);
                    cmd.Parameters.AddWithValue("ModuleId_", pSharedAuth.ModuleId);
                    cmd.Parameters.AddWithValue("WorkFlowId_", pSharedAuth.WorkFlowId);
                    cmd.Parameters.AddWithValue("ProjectId_", pSharedAuth.ProjectId);
                    cmd.Parameters.AddWithValue("AuthoriserId_", pSharedAuth.AuthoriserId);
                    cmd.Parameters.AddWithValue("Remarks_", pSharedAuth.Remarks);
                    cmd.Parameters.AddWithValue("AssignedTo_", pSharedAuth.AssignedToId);
                    cmd.Parameters.AddWithValue("UserId_", pSharedAuth.CreatedBy);
                    //   cmd.Parameters.AddWithValue("CREATEDBY_", pSharedAuth.Createdby);
                    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_TRN_GET_SHARED_WORKFLOW", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AuthoriserId_", AuthorisedId);
                    cmd.Parameters.AddWithValue("PROJECTID_", ProjectID);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    SharedAuthList = new SharedAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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

            using (cnn = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (cmd = new SqlCommand("USP_TRN_GET_SHARED_WRKFLW_BYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("WRKFLW_SHAREDID_", WorkFlowSharedId);
                   // cmd.Parameters.AddWithValue("projectid_", WorkFlowSharedId);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    // AuthList = new TemporaryAuthorizationList();
                    try
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DEL_SHARED_WORKFLOW", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("WrkFlw_SharedId_", WorkFlowSharedId);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            //try
            //{
            //    myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            //    myCommand = new SqlCommand("USP_MST_OBS_TEMPAUTHBYID", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.AddWithValue("APPROVALTEMPAUTHORISERID_", APPROVALTEMPAUTHORISERID);
            //    myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
            //    /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
