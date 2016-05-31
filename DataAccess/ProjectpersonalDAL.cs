using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{    
    public class ProjectpersonalDAL
    {  

        /// <summary>
        /// To Get Users
        /// </summary>
        /// <returns></returns>
        public ProjectPersonalList GetUsers()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_USERSPROJECT";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO ObjPP = null;
            ProjectPersonalList ObjPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                ObjPP = new ProjectPersonalBO();
                ObjPP.UserID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("USERID"))));
                ObjPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                
                ObjPPList.Add(ObjPP);
            }

            dr.Close();
            return ObjPPList;
        }

        /// <summary>
        /// To Check User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CheckUser(int UserID, int ProjectId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_CHECKUSER", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("UserId_", UserID);
                myCommand.Parameters.Add("PROJECTID_", ProjectId);
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
        /// To Get Project Personnel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectPersonalList GetProjectPersonnel(int projectID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PROJECT_PERSONAL";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = dr.GetInt32(dr.GetOrdinal("USERID"));
                objPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                objPPList.Add(objPP);
            }

            dr.Close();
            return objPPList;
        }

        /// <summary>
        /// To Add Personal
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="objPPList"></param>
        public void AddPersonal(int projectID, ProjectPersonalList objPPList)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);

            OracleCommand myCommand; 
            myCommand = new OracleCommand("USP_TRN_DEL_PROJECT_PERSONAL", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;

            con.Open();

            myCommand.Parameters.Add("PROJECTID_", projectID);
            myCommand.ExecuteNonQuery();

            if (objPPList.Count > 0)
            {
                myCommand.Parameters.Clear();
                myCommand.CommandText = "USP_TRN_INS_PROJECT_PERSONAL";

                myCommand.Parameters.Add("@PROJECTID", "");
                myCommand.Parameters.Add("@USERID", "");
                myCommand.Parameters.Add("@CREATEDBY", "");
            
                foreach (ProjectPersonalBO objPP in objPPList)
                {
                    myCommand.Parameters["@PROJECTID"].Value = objPP.ProjID;
                    myCommand.Parameters["@USERID"].Value = objPP.UserID;
                    myCommand.Parameters["@CREATEDBY"].Value = objPP.CreatedBy;
                    myCommand.ExecuteNonQuery();
                }
                con.Close();   
            }
        }


        /// <summary>
        /// To Get Project Personnel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectPersonalList GetProjectOtherPersonnel(int projectID,int UserId)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PROJ_OTH_PERSONAL";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("UserId_", UserId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = dr.GetInt32(dr.GetOrdinal("USERID"));
                objPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                objPPList.Add(objPP);
            }

            dr.Close();
            return objPPList;
        }

        //Edwin 30MAY2016 - Added to retrieve all users in the system for disclosure information update
        public ProjectPersonalList GetAllPersonnel(int projectID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PROJ_ALL_PERSONAL";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = dr.GetInt32(dr.GetOrdinal("USERID"));
                objPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                objPPList.Add(objPP);
            }

            dr.Close();
            return objPPList;
        }
    }   
}
    
