using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalElementDAL
    {
        /// <summary>
        /// To Insert MNE Goal Element Details
        /// </summary>
        /// <param name="GoalElementBOObj"></param>
        /// <returns></returns>
        public string InsertMNEGoalElementDetails(MNEGoalElementBOL GoalElementBOObj)
        {
            string result = "";
            SqlConnection Con = new SqlConnection(AppConfiguration.ConnectionString);
            Con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_MNE_INSERT_GOALELEMENT", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);
            try
            {
                cmd.Parameters.AddWithValue(" GOAL_ELEMENTNAME", GoalElementBOObj.GoalElement);
                cmd.Parameters.AddWithValue("CREATEDBY", GoalElementBOObj.CreatedBy);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Update MNE Goal Element Details
        /// </summary>
        /// <param name="GoalElementBOObj"></param>
        /// <returns></returns>
        public string UpdateMNEGoalElementDetails(MNEGoalElementBOL GoalElementBOObj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_MNE_UPDATE_GOALELEMENT ", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.AddWithValue("GOAL_ELEMENTNAME_", GoalElementBOObj.GoalElement);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", GoalElementBOObj.CreatedBy);
                dcmd.Parameters.AddWithValue("GOAL_ELEMENTID_", GoalElementBOObj.GoalElementID);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

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

            return result;
        }

        /// <summary>
        /// To Get All MNE Goal Element Details
        /// </summary>
        /// <returns></returns>
        public MNEGoalElementList GetAllMNEGoalElementDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_MNE_GET_ALLELEMENTS ";


            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalElementBOL MNEGoalElementBOLObj = null;
            MNEGoalElementList MNEGoalElementListObj = new MNEGoalElementList();
            MNEGoalElementBOLObj = new MNEGoalElementBOL();
            while (dr.Read())
            {
                MNEGoalElementBOLObj = new MNEGoalElementBOL();
                MNEGoalElementBOLObj.GoalElementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOAL_ELEMENTID")));
                MNEGoalElementBOLObj.GoalElement = dr.GetString(dr.GetOrdinal("GOAL_ELEMENTNAME"));
                MNEGoalElementBOLObj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                MNEGoalElementListObj.Add(MNEGoalElementBOLObj);

            }
            dr.Close();
            return MNEGoalElementListObj;
        }

        /// <summary>
        /// To Get MNE Goal Element Details by ID
        /// </summary>
        /// <param name="GOALElementID"></param>
        /// <returns></returns>
        public MNEGoalElementBOL GetMNEGoalElementDetailsbyID(int GOALElementID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_MNE_GETGOALELEMENT";


            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GoalElementID_", GOALElementID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalElementBOL MNEGoalElementBOLObj = new MNEGoalElementBOL();

            while (dr.Read())
            {
                MNEGoalElementBOLObj = new MNEGoalElementBOL();

                MNEGoalElementBOLObj.GoalElementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOAL_ELEMENTID")));
                MNEGoalElementBOLObj.GoalElement = dr.GetString(dr.GetOrdinal("GOAL_ELEMENTNAME"));

            }
            dr.Close();
            return MNEGoalElementBOLObj;

        }

        /// <summary>
        /// To Delete Goal Element
        /// </summary>
        /// <param name="GoalElementID"></param>
        /// <returns></returns>
        public string DeleteGoalElement(int GoalElementID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = "";
            try
            {

                string proc = "USP_MST_MNE_DEL_GOALELEMENT";
                cmd = new SqlCommand(proc, cnn);
                int Count = Convert.ToInt32(cmd.CommandType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("C_GoalElementID ", GoalElementID);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();

            }
            catch
            {
                throw;
            }
            finally
            {

                cnn.Close();
                cnn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// To Obsolete Goal Element
        /// </summary>
        /// <param name="goalID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGoalElement(int goalID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_MNE_OBSOLUTEELEMENT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("GoalElementID_", goalID);
                myCommand.Parameters.AddWithValue("Isdeleted_", IsDeleted);
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
        /// To LoadMNEGoalElement
        /// </summary>
        /// <returns></returns>
        public MNEGoalElementList LoadMNEGoalElement()
        {
            string con = AppConfiguration.ConnectionString;
            SqlConnection cnn;
            SqlCommand cmd;
            string proc = string.Empty;
            cnn = new SqlConnection(AppConfiguration.ConnectionString);
            proc = "USP_LOAD_MNEGOALELEMENTS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MNEGoalElementBOL objMNEGoalElementBOL = null;
            MNEGoalElementList MNEGoalEvalElements = new MNEGoalElementList();
            while (dr.Read())
            {
                objMNEGoalElementBOL = new MNEGoalElementBOL();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) objMNEGoalElementBOL.GoalElementID = dr.GetInt32(dr.GetOrdinal("ID"));
                if (!dr.IsDBNull(dr.GetOrdinal("Name"))) objMNEGoalElementBOL.GoalElement = dr.GetString(dr.GetOrdinal("Name"));

                MNEGoalEvalElements.Add(objMNEGoalElementBOL);
            }
            dr.Close();
            return MNEGoalEvalElements;
        }
    }
}
