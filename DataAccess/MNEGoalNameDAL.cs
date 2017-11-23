using System;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalNameDAL
    {
        /// <summary>
        /// To Insert MNE Goal Details
        /// </summary>
        /// <param name="GoalNameBOObj"></param>
        /// <returns></returns>
        public string InsertMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            string result = "";
            SqlConnection Con = new SqlConnection(AppConfiguration.ConnectionString);
            Con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_MNE_INSERT_GOAL", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);
            try
            {
                cmd.Parameters.AddWithValue("GOALNAME_", GoalNameBOObj.GoalName);
                cmd.Parameters.AddWithValue("CREATEDBY_", GoalNameBOObj.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
        /// To Update MNE Goal Details
        /// </summary>
        /// <param name="GoalNameBOObj"></param>
        /// <returns></returns>
        public string UpdateMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_MNE_UPDATE_GOAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.AddWithValue("GOALNAME_", GoalNameBOObj.GoalName);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", GoalNameBOObj.CreatedBy);
                dcmd.Parameters.AddWithValue("GOALID_", GoalNameBOObj.GoalID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
        /// To Get All MNE Goal Name Details
        /// </summary>
        /// <returns></returns>
        public MNEGoalList GetAllMNEGoalNameDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_MNE_GET_ALLGOALNAMES";


            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = null;
            MNEGoalList MNEGoalListObj = new MNEGoalList();
            MNEGoalBOObj = new MNEGoalBO();
            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));
                MNEGoalBOObj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                MNEGoalListObj.Add(MNEGoalBOObj);

            }
            dr.Close();
            return MNEGoalListObj;
        }

        /// <summary>
        /// To Get Active MNE Goal Names
        /// </summary>
        /// <returns></returns>
        public MNEGoalList GetActiveMNEGoalNames()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_MNE_GET_GOALNAMES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = null;
            MNEGoalList MNEGoalListObj = new MNEGoalList();
            MNEGoalBOObj = new MNEGoalBO();
            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));
                MNEGoalListObj.Add(MNEGoalBOObj);

            }
            dr.Close();
            return MNEGoalListObj;
        }

        /// <summary>
        /// To Get MNE Goal Name Details by ID
        /// </summary>
        /// <param name="GOALID"></param>
        /// <returns></returns>
        public MNEGoalBO GetMNEGoalNameDetailsbyID(int GOALID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_MNE_GET_GOALBYID";


            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GoalID_", GOALID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = new MNEGoalBO();

            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));

            }
            dr.Close();
            return MNEGoalBOObj;

        }

        /// <summary>
        /// To Delete Goal Name
        /// </summary>
        /// <param name="GoalID"></param>
        /// <returns></returns>
        public string DeleteGoalName(int GoalID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = "";
            try
            {

                string proc = "USP_MST_MNE_DEL_GOALNAME";
                cmd = new SqlCommand(proc, cnn);
                int Count = Convert.ToInt32(cmd.CommandType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("c_GoalID ", GoalID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
        /// To Obsolete Goal Name
        /// </summary>
        /// <param name="goalID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGoalName(int goalID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_MNE_OBSOLETEGOALNAME", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("goalID_", goalID);
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
