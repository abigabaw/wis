using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_USERSPROJECT";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO ObjPP = null;
            ProjectPersonalList ObjPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                ObjPP = new ProjectPersonalBO();
                ObjPP.UserID = (int)dr.GetDecimal(dr.GetOrdinal("USERID"));
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_CHECKUSER", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("UserId_", UserID);
                myCommand.Parameters.AddWithValue("PROJECTID_", ProjectId);
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

        /// <summary>
        /// To Get Project Personnel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectPersonalList GetProjectPersonnel(int projectID)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PROJECT_PERSONAL";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = (int)dr.GetDecimal(dr.GetOrdinal("USERID"));
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);

            SqlCommand myCommand; 
            myCommand = new SqlCommand("USP_TRN_DEL_PROJECT_PERSONAL", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;

            con.Open();

            myCommand.Parameters.AddWithValue("PROJECTID_", projectID);
            myCommand.ExecuteNonQuery();

            if (objPPList.Count > 0)
            {
                myCommand.Parameters.Clear();
                myCommand.CommandText = "USP_TRN_INS_PROJECT_PERSONAL";

                myCommand.Parameters.AddWithValue("@PROJECTID", "");
                myCommand.Parameters.AddWithValue("@USERID", "");
                myCommand.Parameters.AddWithValue("@CREATEDBY", "");
            
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PROJ_OTH_PERSONAL";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            cmd.Parameters.AddWithValue("UserId_", UserId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = (int)dr.GetDecimal(dr.GetOrdinal("USERID"));
                objPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                objPPList.Add(objPP);
            }

            dr.Close();
            return objPPList;
        }

        //Edwin 30MAY2016 - Added to retrieve all users in the system for disclosure information update
        public ProjectPersonalList GetAllPersonnel(int projectID)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PROJ_ALL_PERSONAL";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectPersonalBO objPP = null;
            ProjectPersonalList objPPList = new ProjectPersonalList();

            while (dr.Read())
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = (int)dr.GetDecimal(dr.GetOrdinal("USERID"));
                objPP.Username = dr.GetValue(dr.GetOrdinal("USERNAME")).ToString();
                objPPList.Add(objPP);
            }

            dr.Close();
            return objPPList;
        }
    }   
}
    
