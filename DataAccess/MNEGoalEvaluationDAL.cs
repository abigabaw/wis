using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalEvaluationDAL
    {
        /// <summary>
        /// To Insert MNE Goal Eval
        /// </summary>
        /// <param name="objMNEGoalEvaluationBO"></param>
        /// <returns></returns>
        public string InsertMNEGoalEval(MNEGoalEvaluationBO objMNEGoalEvaluationBO)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_INS_TRN_MNE_EVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("PROJECTID_", objMNEGoalEvaluationBO.ProjectID);
                dcmd.Parameters.AddWithValue("GOALID_", objMNEGoalEvaluationBO.GoalID);
                dcmd.Parameters.AddWithValue("GOALDESCRIPTION_", objMNEGoalEvaluationBO.GoalDescription);
                dcmd.Parameters.AddWithValue("GOALNARRATIVE_", objMNEGoalEvaluationBO.GoalNarrative);
                dcmd.Parameters.AddWithValue("ISDELETED_", objMNEGoalEvaluationBO.IsDeleted);
                dcmd.Parameters.AddWithValue("CREATEDBY_", objMNEGoalEvaluationBO.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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

        /// <summary>
        /// To Get MNE Goal Evaluation
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public MNEGoalEvaluationList GetMNEGoalEvaluation(int projectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            MNEGoalEvaluationBO objMNEGoalEvaluationBO = null;

            string proc = "USP_GET_TRN_MNE_EVAL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectID_", projectID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalEvaluationList MNEGoalEvaluation = new MNEGoalEvaluationList();

            while (dr.Read())
            {
                objMNEGoalEvaluationBO = new MNEGoalEvaluationBO();
                objMNEGoalEvaluationBO.EvaluationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evaluationid")));
                objMNEGoalEvaluationBO.Goalname = dr.GetString(dr.GetOrdinal("goalname"));
                objMNEGoalEvaluationBO.GoalDescription = dr.GetString(dr.GetOrdinal("goaldescription"));
                //objMNEGoalEvaluationBO.GoalNarrative = dr.GetString(dr.GetOrdinal("goalnarrative"));
                //objMNEGoalEvaluationBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                //    objMNEGoalEvaluationBO.ProjectID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("projectid")));

                MNEGoalEvaluation.Add(objMNEGoalEvaluationBO);
            }

            dr.Close();

            return MNEGoalEvaluation;
        }

        /// <summary>
        /// To Get MNE Goal Evaluation By ID
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public MNEGoalEvaluationBO GetMNEGoalEvaluationByID(int EvaluationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_TRN_MNE_EVALBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EVALUATIONID_", EvaluationID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MNEGoalEvaluationBO objMNEGoalEvaluationBO = null;
            MNEGoalEvaluationList MNEGoalEvaluation = new MNEGoalEvaluationList();

            objMNEGoalEvaluationBO = new MNEGoalEvaluationBO();
            while (dr.Read())
            {

                objMNEGoalEvaluationBO.EvaluationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evaluationid")));

                objMNEGoalEvaluationBO.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("goalid")));

                objMNEGoalEvaluationBO.GoalDescription = Convert.ToString(dr.GetValue(dr.GetOrdinal("goaldescription")));

                objMNEGoalEvaluationBO.GoalNarrative = Convert.ToString(dr.GetValue(dr.GetOrdinal("goalnarrative")));

                objMNEGoalEvaluationBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                objMNEGoalEvaluationBO.ProjectID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("projectid")));
            }
            dr.Close();
            return objMNEGoalEvaluationBO;
        }

        /// <summary>
        /// To Update MNE Goal Evaluation
        /// </summary>
        /// <param name="objMNEGoalEvaluationBO"></param>
        /// <returns></returns>
        public string UpdateMNEGoalEvaluation(MNEGoalEvaluationBO objMNEGoalEvaluationBO)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_UPD_TRN_MNE_EVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("EVALUATIONID_", objMNEGoalEvaluationBO.EvaluationID);
                dcmd.Parameters.AddWithValue("PROJECTID_", objMNEGoalEvaluationBO.ProjectID);
                dcmd.Parameters.AddWithValue("GOALID_", objMNEGoalEvaluationBO.GoalID);
                dcmd.Parameters.AddWithValue("GOALDESCRIPTION_", objMNEGoalEvaluationBO.GoalDescription);
                dcmd.Parameters.AddWithValue("GOALNARRATIVE_", objMNEGoalEvaluationBO.GoalNarrative);
                dcmd.Parameters.AddWithValue("UPDATEBY_", objMNEGoalEvaluationBO.UpdatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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

        /// <summary>
        /// To Delete MNE Goal Evaluation
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public string DeleteMNEGoalEvaluation(int EvaluationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_DEL_TRN_MNE_EVAL";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EVALUATIONID_", EvaluationID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                cnn.Close();
                cnn.Dispose();

            }

            return result;
        }
    }
}
