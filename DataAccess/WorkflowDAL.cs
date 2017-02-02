using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class WorkflowDAL
    {
        #region for page load
        /// <summary>
        /// To Get All Module from database
        /// </summary>
        /// <returns></returns>
        public WorkFlowList getAllModule()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SELECTMODULE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                objWorkFlow = new WorkFlowBO();
                objWorkFlow.ModuleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODULEID")));
                objWorkFlow.ModuleName = dr.GetString(dr.GetOrdinal("MODULENAME"));

                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }

        /// <summary>
        /// To Get Work Flow By Module Id
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public WorkFlowList GetWorkFlowByModuleId(int ModuleID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTWORKFLOWBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ModuleID_", ModuleID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();


            while (dr.Read())
            {
                WorkFlowObj = new WorkFlowBO();
                if (ColumnExists(dr, "DESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    WorkFlowObj.WorkDesc = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (ColumnExists(dr, "WORKFLOWID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWID")))
                    WorkFlowObj.WorkflowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWID")));
                if (ColumnExists(dr, "MODULEID") && !dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                    WorkFlowObj.ModuleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODULEID")));

                WorkFlowList.Add(WorkFlowObj);

            }
            dr.Close();
            return WorkFlowList;
        }

        // To check that the Column Exists or not
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
        /// To Get All Projects Person User
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList getAllProjectPersonUser(string projectID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GETALLROLE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("projectID_", projectID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                objWorkFlow = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("DISPLAYNAME"))) objWorkFlow.UserName = dr.GetString(dr.GetOrdinal("DISPLAYNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) objWorkFlow.UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID")));
                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }

        /// <summary>
        /// To Get All Employee Name
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList getAllEmpName(string projectID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GETPRJPERSONALUSER";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("projectID_", projectID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                objWorkFlow = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROLEID"))) objWorkFlow.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) objWorkFlow.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));

                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }

        /// <summary>
        /// To Get Approval Role User ID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public WorkFlowList getApprovalRoleUserID(int projectID, int roleID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GETUSERBYROLEID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("projectID_", projectID);
            cmd.Parameters.Add("projectID_", roleID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                objWorkFlow = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objWorkFlow.ApproverUserName = dr.GetString(dr.GetOrdinal("USERNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) objWorkFlow.ApproverUserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID")));
                //if (!dr.IsDBNull(dr.GetOrdinal("ROLEID"))) objWorkFlow.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));
                //if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) objWorkFlow.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));

                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }
        #endregion

        #region for Workflow defination
        //Data Inserting for WorkFlow Defination
        #region to Insert data into TRN_Workflow_defination Table
        public int InsertWorkFlow(WorkFlowBO objWorkFlow)
        {
            int Result = 0;

            int Count = 0;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWDEF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

          
            while (dr.Read())
            {
                WorkFlowObj = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) WorkFlowObj.WorkFlowDefID = dr.GetInt32(dr.GetOrdinal("WORKFLOWDEFINITIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODULEID"))) WorkFlowObj.ModuleID = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID"))) WorkFlowObj.WorkflowID = dr.GetInt32(dr.GetOrdinal("WORKFLOWITEMID"));

                WorkFlowList.Add(WorkFlowObj);

            }
            
            dr.Close();
            for (int i = 0; i < WorkFlowList.Count; i++)
            {
                if (WorkFlowList[i].WorkflowID == objWorkFlow.WorkflowID)
                {
                    Count = Count + 1;
                }
            }
            if (Count == 0)
            {
                //OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                cnn.Open();
                OracleCommand dcmd = new OracleCommand("USP_TRN_WORKFLOW_DEFINITION", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);
                dcmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
                dcmd.Parameters.Add("ModuleID_", objWorkFlow.ModuleID);
                dcmd.Parameters.Add("WorkflowID_", objWorkFlow.WorkflowID);
                dcmd.Parameters.Add("HighaultorityID_", objWorkFlow.HigherAuthorityID);
                if (objWorkFlow.Trigger == "0")
                {
                    dcmd.Parameters.Add("Trigger_", DBNull.Value);
                }
                else
                {
                    dcmd.Parameters.Add("Trigger_", objWorkFlow.Trigger);
                }
                dcmd.Parameters.Add("AfterDays_", objWorkFlow.AfterDays);
                dcmd.Parameters.Add("CreatedBY", objWorkFlow.UserID);
                Result = dcmd.ExecuteNonQuery();
                cnn.Close();
            }
            else
            {
                Result = 1;
            }
            return Result;
        }
        #endregion

        #region For GridBind / Edit / Delete  / GetElementByID(Pramary Key)
        /// <summary>
        /// To Get Work Flow Definition
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList GetWorkFlowDefinition(string projectID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWDEF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", projectID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {

                objWorkFlow = new WorkFlowBO();

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) objWorkFlow.WorkFlowDefID = dr.GetInt32(dr.GetOrdinal("WORKFLOWDEFINITIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) objWorkFlow.ProjectID = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODULEID"))) objWorkFlow.ModuleID = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODULENAME"))) objWorkFlow.ModuleName = dr.GetString(dr.GetOrdinal("MODULENAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID"))) objWorkFlow.WorkflowID = dr.GetInt32(dr.GetOrdinal("WORKFLOWITEMID"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objWorkFlow.WorkflowName = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                if (!dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITY"))) objWorkFlow.HigherAuthorityID = dr.GetInt32(dr.GetOrdinal("HIGHERAUTHORITY"));

                if (!dr.IsDBNull(dr.GetOrdinal("UserName"))) objWorkFlow.UserName = dr.GetString(dr.GetOrdinal("UserName"));

                if (!dr.IsDBNull(dr.GetOrdinal("TRIGGERTYPE"))) objWorkFlow.Trigger = dr.GetString(dr.GetOrdinal("TRIGGERTYPE"));

                if (!dr.IsDBNull(dr.GetOrdinal("TRIGGERPERIOD"))) objWorkFlow.AfterDays = dr.GetInt32(dr.GetOrdinal("TRIGGERPERIOD"));

                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }

        /// <summary>
        /// To Get Work Flow Definition by ID
        /// </summary>
        /// <param name="WorkFlowDefID"></param>
        /// <returns></returns>
        public WorkFlowBO GetWorkFlowDefByID(int WorkFlowDefID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_WORKFLOWDEFBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("WorkFlowDefID_", WorkFlowDefID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowBOObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            WorkFlowBOObj = new WorkFlowBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    WorkFlowBOObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (ColumnExists(dr, "MODULEID") && !dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                    WorkFlowBOObj.ModuleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODULEID")));

                if (ColumnExists(dr, "WORKFLOWITEMID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID")))
                    WorkFlowBOObj.WorkflowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWITEMID")));

                if (ColumnExists(dr, "HIGHERAUTHORITY") && !dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITY")))
                    WorkFlowBOObj.HigherAuthorityID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HIGHERAUTHORITY")));

                if (ColumnExists(dr, "TRIGGERTYPE") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERTYPE")))
                    WorkFlowBOObj.Trigger = Convert.ToString(dr.GetValue(dr.GetOrdinal("TRIGGERTYPE")));

                if (ColumnExists(dr, "TRIGGERPERIOD") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERPERIOD")))
                    WorkFlowBOObj.AfterDays = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIGGERPERIOD")));

            }
            dr.Close();
            return WorkFlowBOObj;
        }

        /// <summary>
        /// To Delete Work Flow Definition
        /// </summary>
        /// <param name="WorkFlowDefID"></param>
        /// <returns></returns>
        public string DeleteWorkFlowDef(int WorkFlowDefID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DEL_WORKFLOWDEF";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("WorkFlowDefID_", WorkFlowDefID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return message;
        }

        /// <summary>
        /// To Edit Work Flow Definition
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int EditWorkFlowDef(WorkFlowBO objWorkFlow)
        {
            int Result = 0;

            int Count = 0;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWDEF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                WorkFlowObj = new WorkFlowBO();

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) WorkFlowObj.WorkFlowDefID = dr.GetInt32(dr.GetOrdinal("WORKFLOWDEFINITIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODULEID"))) WorkFlowObj.ModuleID = dr.GetInt32(dr.GetOrdinal("MODULEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID"))) WorkFlowObj.WorkflowID = dr.GetInt32(dr.GetOrdinal("WORKFLOWITEMID"));

                WorkFlowList.Add(WorkFlowObj);

            }
            dr.Close();
            for (int i = 0; i < WorkFlowList.Count; i++)
            {
                if (WorkFlowList[i].WorkflowID == objWorkFlow.WorkflowID && WorkFlowList[i].WorkFlowDefID != objWorkFlow.WorkFlowDefID)
                {
                    Count = Count + 1;
                }
            }
            if (Count == 0)
            {

                cnn.Open();
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPDATE_WORKFLOW_DEF", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("WorkFlowDefID_", objWorkFlow.WorkFlowDefID);
                    dcmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
                    dcmd.Parameters.Add("ModuleID_", objWorkFlow.ModuleID);
                    dcmd.Parameters.Add("WorkflowID_", objWorkFlow.WorkflowID);
                    dcmd.Parameters.Add("HighaultorityID_", objWorkFlow.HigherAuthorityID);
                    
                    if (objWorkFlow.Trigger == "0")
                    {
                        dcmd.Parameters.Add("Trigger_", DBNull.Value);
                    }
                    else
                    {
                        dcmd.Parameters.Add("Trigger_", objWorkFlow.Trigger);
                    }

                    dcmd.Parameters.Add("AfterDays_", objWorkFlow.AfterDays);
                    dcmd.Parameters.Add("UserID_", objWorkFlow.UserID);
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
            }
            else
            {
                Result = 1;
            }
            return Result;
        }
        #endregion
        #endregion 

        #region for unused Code
        //public int SaveInsertWorkFlow(WorkFlowBO objWorkFlowApp)
        //{
        //    int Result = 0;

        //    OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
        //    OracleCommand cmd;

        //    string proc = "USP_MST_GET_EXIT_PRJID";

        //    cmd = new OracleCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("ProjectID_", objWorkFlowApp.ProjectID);
        //    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //    cmd.Connection.Open();

        //    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    WorkFlowBO WorkFlowObj = null;
        //    WorkFlowList WorkFlowList = new WorkFlowList();

        //    WorkFlowObj = new WorkFlowBO();
        //    while (dr.Read())
        //    {
        //        if (ColumnExists(dr, "PROJECTID") && !dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
        //            WorkFlowObj.ProjectID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));
        //        if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
        //            WorkFlowObj.WorkflowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

        //    }
        //    dr.Close();
        //    if (WorkFlowObj.ProjectID == objWorkFlowApp.ProjectID)
        //    {
        //        cnn.Open();
        //        OracleCommand dcmd = new OracleCommand("USP_TRN_INS_WRKFLW_APPROVAL", cnn);
        //        dcmd.CommandType = CommandType.StoredProcedure;
        //        int count = Convert.ToInt32(dcmd.CommandType);

        //        dcmd.Parameters.Add("WORKFLOWDEFINITIONID_", WorkFlowObj.WorkflowID);
        //        dcmd.Parameters.Add("ApprovalID_", objWorkFlowApp.ApprovalID);
        //        dcmd.Parameters.Add("LEVEL_", objWorkFlowApp.LEVEL);
        //        dcmd.Parameters.Add("CreatedBY", objWorkFlowApp.UserID);

        //        Result = dcmd.ExecuteNonQuery();
        //        cnn.Close();
        //    }
        //    return Result;
        //}
        #endregion

        #region for Approval 
        /// <summary>
        /// To Get Approver
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkflowDefID"></param>
        /// <returns></returns>
        public WorkFlowList GetApprover(string projectID, string WorkflowDefID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_SELECTWORKFLOW";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", projectID);
            cmd.Parameters.Add("WorkflowDefID_", WorkflowDefID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO objWorkFlow = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                objWorkFlow = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROLEID"))) objWorkFlow.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) objWorkFlow.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("USERID"))) objWorkFlow.ApproverUserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID")));
                if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objWorkFlow.ApproverUserName = dr.GetString(dr.GetOrdinal("USERNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL"))) objWorkFlow.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) objWorkFlow.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID"))) objWorkFlow.WorkApprovalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWAPPROVERID")));

                WorkFlowList.Add(objWorkFlow);
            }

            dr.Close();

            return WorkFlowList;
        }

        /// <summary>
        /// To Get Work Flow By Id
        /// </summary>
        /// <param name="WORKAPPROVALID"></param>
        /// <returns></returns>
        public WorkFlowBO GetWorkFlowById(int WORKAPPROVALID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("WORKFLOWDEFID_", WORKAPPROVALID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            WorkFlowObj = new WorkFlowBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "ROLENAME") && !dr.IsDBNull(dr.GetOrdinal("ROLENAME")))
                    WorkFlowObj.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));
                if (ColumnExists(dr, "ROLEID") && !dr.IsDBNull(dr.GetOrdinal("ROLEID")))
                    WorkFlowObj.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));

                if (ColumnExists(dr, "USERID") && !dr.IsDBNull(dr.GetOrdinal("USERID")))
                    WorkFlowObj.UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID")));
                if (ColumnExists(dr, "USERNAME") && !dr.IsDBNull(dr.GetOrdinal("USERNAME")))
                    WorkFlowObj.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));

                if (ColumnExists(dr, "APPROVERLEVEL") && !dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL")))
                    WorkFlowObj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    WorkFlowObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));
                if (ColumnExists(dr, "WORKFLOWAPPROVERID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID")))
                    WorkFlowObj.WorkApprovalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWAPPROVERID")));
            }
            dr.Close();
            return WorkFlowObj;
        }

        /// <summary>
        /// To Delete Approver
        /// </summary>
        /// <param name="WORKFLOWDEFID"></param>
        /// <returns></returns>
        public int DeleteApprover(int WORKFLOWDEFID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            int result = 0;

            string proc = "USP_TRN_DELT_WORK_APP";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("WORKFLOWDEFID_", WORKFLOWDEFID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null && cmd.Parameters["errorMessage_"].Value.ToString() != "null"
                 && cmd.Parameters["errorMessage_"].Value.ToString().Trim() != "")
            {
                result = 0;
                }
            else{
                result = -1;
                }
            return result;
        }
        // To Insert approaver By role and Level
        public int InsertAPPROVALADD(WorkFlowBO objWorkFlow)
        {

            int Result = 0;
            int Count = 0;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_SELECTWORKFLOW";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
            cmd.Parameters.Add("WorkflowDefID_", objWorkFlow.WorkFlowDefID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

           
            while (dr.Read())
            {
                WorkFlowObj = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL"))) WorkFlowObj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) WorkFlowObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLEID"))) WorkFlowObj.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) WorkFlowObj.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));

                WorkFlowList.Add(WorkFlowObj);
            }
            dr.Close();
            for (int i = 0; i < WorkFlowList.Count; i++)
            {
                if (WorkFlowList[i].WorkFlowDefID == objWorkFlow.WorkFlowDefID)
                {
                    if (WorkFlowList[i].LEVEL == objWorkFlow.LEVEL || WorkFlowList[i].RoleID == objWorkFlow.ApprovalID)
                    {
                        Count = Count + 1;
                    }

                }
            }
            if (Count == 0)
            {
                    cnn.Open();
                    OracleCommand dcmd = new OracleCommand("USP_TRN_INS_WRKFLW_APPROVAL", cnn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    int count = Convert.ToInt32(dcmd.CommandType);

                    try
                    {
                        dcmd.Parameters.Add("WORKFLOWDEFINITIONID_", objWorkFlow.WorkFlowDefID);
                        dcmd.Parameters.Add("ApproverUserID_", objWorkFlow.ApproverUserID);
                        dcmd.Parameters.Add("ApprovalID_", objWorkFlow.ApprovalID);
                        dcmd.Parameters.Add("LEVEL_", objWorkFlow.LEVEL);
                        dcmd.Parameters.Add("CreatedBY", objWorkFlow.UserID);
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
                }
                else
                {
                    Result = 1;
                }
            
            return Result;
        }

        /// <summary>
        /// To Edit Approver
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int EDITAPPROVER(WorkFlowBO objWorkFlow)
        {
            int Result = 0;
            int Count = 0;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_SELECTWORKFLOW";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
            cmd.Parameters.Add("WorkflowDefID_", objWorkFlow.WorkFlowDefID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();
                       
            while (dr.Read())
            {
                WorkFlowObj = new WorkFlowBO();
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL"))) WorkFlowObj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID"))) WorkFlowObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLEID"))) WorkFlowObj.RoleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROLEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROLENAME"))) WorkFlowObj.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));

                WorkFlowList.Add(WorkFlowObj);
            }
            dr.Close();

            for (int i = 0; i < WorkFlowList.Count; i++)
            {
                if (WorkFlowList[i].WorkFlowDefID == objWorkFlow.WorkFlowDefID)
                {
                    if (WorkFlowList[i].RoleID == WorkFlowObj.RoleID && WorkFlowList[i].LEVEL == objWorkFlow.LEVEL)
                    {
                        Count = 0;
                    }
                    else
                    {
                        Count = Count + 1;
                    }

                }
            }
            if (Count == 0)
            {
                cnn.Open();
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPDATE_APPROVER", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {

                    dcmd.Parameters.Add("OWORKAPPROVALID", WorkFlowObj.WorkApprovalID);
                    dcmd.Parameters.Add("OROLEID", WorkFlowObj.RoleID);
                    dcmd.Parameters.Add("OLEVEL", WorkFlowObj.LEVEL);
                    dcmd.Parameters.Add("OISDELETE", WorkFlowObj.WorkIsDeleted);
                    dcmd.Parameters.Add("OCREATEDBY", WorkFlowObj.UserID);
                    dcmd.Parameters.Add("OCREATEDDATE", WorkFlowObj.WCreatedDate);
                    //dcmd.Parameters.Add("WorkFlowDefID_", WorkFlowObj.WUpdatedBy);
                    //dcmd.Parameters.Add("WorkFlowDefID_", WorkFlowObj.WUpdatedDate);

                    dcmd.Parameters.Add("WorkApprovalID_", objWorkFlow.WorkApprovalID);
                    dcmd.Parameters.Add("WorkFlowDefID_", objWorkFlow.WorkFlowDefID);
                    dcmd.Parameters.Add("RoleID_", objWorkFlow.ApprovalID);
                    dcmd.Parameters.Add("LEVEL_", objWorkFlow.LEVEL);
                    dcmd.Parameters.Add("UpdatedBY_", objWorkFlow.UserID);
                    dcmd.Parameters.Add("ApproverUserID_", objWorkFlow.ApproverUserID);
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
            }
            else
            {
                Result = 1;
            }
            return Result;
        }

        #endregion

        #region donot delete
        /// <summary>
        /// To Get All Saved WORK FLOW Data
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public WorkFlowBO getALLSaveWORKFLOWData(string pID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_ALL_WORKFLOW";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", pID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            WorkFlowObj = new WorkFlowBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "ROLENAME") && !dr.IsDBNull(dr.GetOrdinal("ROLENAME")))
                    WorkFlowObj.RoleName = dr.GetString(dr.GetOrdinal("ROLENAME"));


                if (ColumnExists(dr, "PROJECTID") && !dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                    WorkFlowObj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    WorkFlowObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (ColumnExists(dr, "MODULEID") && !dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                    WorkFlowObj.ModuleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODULEID")));

                if (ColumnExists(dr, "MODULENAME") && !dr.IsDBNull(dr.GetOrdinal("MODULENAME")))
                    WorkFlowObj.ModuleName = dr.GetString(dr.GetOrdinal("MODULENAME"));

                if (ColumnExists(dr, "WORKFLOWITEMID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID")))
                    WorkFlowObj.WorkflowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWITEMID")));

                if (ColumnExists(dr, "DESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    WorkFlowObj.WorkDesc = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                if (ColumnExists(dr, "HIGHERAUTHORITY") && !dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITY")))
                    WorkFlowObj.HigherAuthorityID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HIGHERAUTHORITY")));

                if (ColumnExists(dr, "TRIGGERTYPE") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERTYPE")))
                    WorkFlowObj.Trigger = dr.GetString(dr.GetOrdinal("TRIGGERTYPE"));

                if (ColumnExists(dr, "TRIGGERPERIOD") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERPERIOD")))
                    WorkFlowObj.AfterDays = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIGGERPERIOD")));

            }
            dr.Close();
            return WorkFlowObj;
        }

        /// <summary>
        /// To Update Work flow
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int UpdateWorkflow(WorkFlowBO objWorkFlow)
        {
            int Result = 0;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_ALL_WORKFLOW";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", objWorkFlow.ProjectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO WorkFlowObj = null;
            WorkFlowList WorkFlowList = new WorkFlowList();

            WorkFlowObj = new WorkFlowBO();
            while (dr.Read())
            {
                string strCreatedDate = string.Empty;

                if (ColumnExists(dr, "PROJECTID") && !dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                    WorkFlowObj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

                if (ColumnExists(dr, "WORKFLOWDEFINITIONID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    WorkFlowObj.WorkFlowDefID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (ColumnExists(dr, "MODULEID") && !dr.IsDBNull(dr.GetOrdinal("MODULEID")))
                    WorkFlowObj.ModuleID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODULEID")));


                if (ColumnExists(dr, "WORKFLOWITEMID") && !dr.IsDBNull(dr.GetOrdinal("WORKFLOWITEMID")))
                    WorkFlowObj.WorkflowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWITEMID")));

                if (ColumnExists(dr, "HIGHERAUTHORITY") && !dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITY")))
                    WorkFlowObj.HigherAuthorityID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HIGHERAUTHORITY")));

                if (ColumnExists(dr, "TRIGGERTYPE") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERTYPE")))
                    WorkFlowObj.Trigger = dr.GetString(dr.GetOrdinal("TRIGGERTYPE"));

                if (ColumnExists(dr, "TRIGGERPERIOD") && !dr.IsDBNull(dr.GetOrdinal("TRIGGERPERIOD")))
                    WorkFlowObj.AfterDays = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIGGERPERIOD")));

                if (ColumnExists(dr, "CREATEDBY") && !dr.IsDBNull(dr.GetOrdinal("CREATEDBY")))
                    WorkFlowObj.UserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CREATEDBY")));

                if (ColumnExists(dr, "CREATEDDATE") && !dr.IsDBNull(dr.GetOrdinal("CREATEDDATE")))
                    strCreatedDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE")).ToString("dd-MM-yy");
                WorkFlowObj.WCreatedDate = strCreatedDate;

            }
            dr.Close();

            if (WorkFlowObj.WorkFlowDefID == objWorkFlow.WorkFlowDefID)
            {
                //OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                cnn.Open();
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPDATE_WORKFLOWDEF", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {

                    dcmd.Parameters.Add("OWORKAPPROVALID", WorkFlowObj.WorkFlowDefID);
                    dcmd.Parameters.Add("OMODULEID", WorkFlowObj.ModuleID);
                    dcmd.Parameters.Add("OWORKFLOW", WorkFlowObj.WorkflowID);
                    dcmd.Parameters.Add("OHiGHULTORITY", WorkFlowObj.HigherAuthorityID);
                    dcmd.Parameters.Add("OTRIGGER", WorkFlowObj.Trigger);
                    dcmd.Parameters.Add("OAFTERDAYS", WorkFlowObj.AfterDays);
                    dcmd.Parameters.Add("OCREATEDBY", WorkFlowObj.UserID);
                    dcmd.Parameters.Add("OCREATEDDATE", WorkFlowObj.WCreatedDate);

                    dcmd.Parameters.Add("WorkFlowDefID_", objWorkFlow.WorkFlowDefID);
                    dcmd.Parameters.Add("PROJECTID_", objWorkFlow.ProjectID);
                    dcmd.Parameters.Add("MODULEID_", objWorkFlow.ModuleID);
                    dcmd.Parameters.Add("WORKFLOWID_", objWorkFlow.WorkflowID);
                    dcmd.Parameters.Add("HIGHAULTID_", objWorkFlow.HigherAuthorityID);
                    dcmd.Parameters.Add("TRIGGER_", objWorkFlow.Trigger);
                    dcmd.Parameters.Add("AFTERDAYS_", objWorkFlow.AfterDays);
                    dcmd.Parameters.Add("UpdatedBY_", objWorkFlow.UserID);
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
            }
            return Result;
        }
        #endregion

        #region for Email Sending
        /// <summary>
        /// To get WOrk Flow Approval ID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        public WorkFlowBO getWOrkFlowApprovalID(int projectID, string workflowCode)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_WORKFLOWAPPREID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectId_", projectID);
            cmd.Parameters.Add("WorkFlowApprover_", workflowCode);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO ProjectRouteBOobj = null;

            while (dr.Read())
            {
                ProjectRouteBOobj = new WorkFlowBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CELLNUMBER")))
                    ProjectRouteBOobj.CellNumber = (dr.GetString(dr.GetOrdinal("CELLNUMBER")));

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILID")))
                    ProjectRouteBOobj.EmailID = (dr.GetString(dr.GetOrdinal("EMAILID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWDEFINITIONID")))
                    ProjectRouteBOobj.WorkFlowDefinitionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWDEFINITIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWAPPROVERID")))
                    ProjectRouteBOobj.WorkFlowApproverID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WORKFLOWAPPROVERID")));

                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERUSERID")))
                    ProjectRouteBOobj.ApproverUserID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERUSERID")));

                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERUSERNAME")))
                    ProjectRouteBOobj.ApproverUserName = (dr.GetString(dr.GetOrdinal("APPROVERUSERNAME")));

                //Edwin: 02FEB2017 for Notifying Higher Authority
                if (!dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITYNAME")))
                    ProjectRouteBOobj.HigherAuthorityName = (dr.GetString(dr.GetOrdinal("HIGHERAUTHORITYNAME")));

                if (!dr.IsDBNull(dr.GetOrdinal("HIGHERAUTHORITYEMAILID")))
                    ProjectRouteBOobj.HigherAuthorityEmailID = (dr.GetString(dr.GetOrdinal("HIGHERAUTHORITYEMAILID")));
                //End:

                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE")))
                    ProjectRouteBOobj.ProjectCode = (dr.GetString(dr.GetOrdinal("PROJECTCODE")));

                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME")))
                    ProjectRouteBOobj.ProjectName = (dr.GetString(dr.GetOrdinal("PROJECTNAME")));

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT")))
                    ProjectRouteBOobj.EmailSubject = (dr.GetString(dr.GetOrdinal("EMAILSUBJECT")));

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILBODY")))
                    ProjectRouteBOobj.EmailBody = (dr.GetString(dr.GetOrdinal("EMAILBODY")));

                if (!dr.IsDBNull(dr.GetOrdinal("SMSTEXT")))
                    ProjectRouteBOobj.SmsText = (dr.GetString(dr.GetOrdinal("SMSTEXT")));
            }
            dr.Close();
            return ProjectRouteBOobj;
        }
        #endregion

        #region for check number of approval and the Status of approval  for that workflow(used in packageDocumnet and Final valution)
        /// <summary>
        /// To Get Total Count Approval
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowList getTotalcountapproval(WorkFlowBO objProjectRoute)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_TOTALAPPREID";

            //string APPROVERLEVEL = "1";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectId_", objProjectRoute.Project_Id);
            cmd.Parameters.Add("WorkFlowApprover_", objProjectRoute.WorkFlowApprover);
            //cmd.Parameters.Add("APPROVERLEVEL_", APPROVERLEVEL);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO ProjectRouteBOobj = null;
            // ProjectRouteList ProjectRouteList = new ProjectRouteList();
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                ProjectRouteBOobj = new WorkFlowBO();

                if (ColumnExists(dr, "APPROVERLEVEL") && !dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL")))
                    ProjectRouteBOobj.CountApproval = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                WorkFlowList.Add(ProjectRouteBOobj);
            }
            dr.Close();
            return WorkFlowList;
        }

        /// <summary>
        /// Approval Status Check
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowBO ApprovalStatuscheck(WorkFlowBO objProjectRoute)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_TOTALAPPRESTATUS";

            //string APPROVERLEVEL = "1";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectId_", objProjectRoute.Project_Id);
            cmd.Parameters.Add("WorkFlowCode_", objProjectRoute.WorkflowCode);
            cmd.Parameters.Add("APPROVERLEVEL_", objProjectRoute.LEVEL);
            cmd.Parameters.Add("HHID_", objProjectRoute.HHID);
            cmd.Parameters.Add("PageCode_", objProjectRoute.PageCode);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO ProjectRouteBOobj = null;
            // ProjectRouteList ProjectRouteList = new ProjectRouteList();
            //WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                ProjectRouteBOobj = new WorkFlowBO();

                if (ColumnExists(dr, "STATUSID") && !dr.IsDBNull(dr.GetOrdinal("STATUSID")))
                    ProjectRouteBOobj.ApprovalstatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STATUSID")));
                if (ColumnExists(dr, "APPROVERLEVEL") && !dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL")))
                    ProjectRouteBOobj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                
            }
            dr.Close();
            return ProjectRouteBOobj;
        }

        /// <summary>
        /// Approval Status Checklist
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowList ApprovalStatuschecklist(WorkFlowBO objProjectRoute)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_TOTALAPPRESTATUS";

            //string APPROVERLEVEL = "1";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectId_", objProjectRoute.Project_Id);
            cmd.Parameters.Add("WorkFlowCode_", objProjectRoute.WorkflowCode);
            cmd.Parameters.Add("APPROVERLEVEL_", objProjectRoute.LEVEL);
            cmd.Parameters.Add("HHID_", objProjectRoute.HHID);
            cmd.Parameters.Add("PageCode_", objProjectRoute.PageCode);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WorkFlowBO ProjectRouteBOobj = null;
            // ProjectRouteList ProjectRouteList = new ProjectRouteList();
            WorkFlowList WorkFlowList = new WorkFlowList();

            while (dr.Read())
            {
                ProjectRouteBOobj = new WorkFlowBO();

                if (ColumnExists(dr, "STATUSID") && !dr.IsDBNull(dr.GetOrdinal("STATUSID")))
                    ProjectRouteBOobj.ApprovalstatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STATUSID")));
                if (ColumnExists(dr, "APPROVERLEVEL") && !dr.IsDBNull(dr.GetOrdinal("APPROVERLEVEL")))
                    ProjectRouteBOobj.LEVEL = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVERLEVEL")));
                if (ColumnExists(dr, "ELEMENTID") && !dr.IsDBNull(dr.GetOrdinal("ELEMENTID")))
                    ProjectRouteBOobj.ELEMENTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ELEMENTID")));
                WorkFlowList.Add(ProjectRouteBOobj);
            }
            dr.Close();
            return WorkFlowList;
        }
        #endregion
    }
}
