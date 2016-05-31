using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_INS_TRN_MNE_EVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("PROJECTID_", objMNEGoalEvaluationBO.ProjectID);
                dcmd.Parameters.Add("GOALID_", objMNEGoalEvaluationBO.GoalID);
                dcmd.Parameters.Add("GOALDESCRIPTION_", objMNEGoalEvaluationBO.GoalDescription);
                dcmd.Parameters.Add("GOALNARRATIVE_", objMNEGoalEvaluationBO.GoalNarrative);
                dcmd.Parameters.Add("ISDELETED_", objMNEGoalEvaluationBO.IsDeleted);
                dcmd.Parameters.Add("CREATEDBY_", objMNEGoalEvaluationBO.CreatedBy);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            MNEGoalEvaluationBO objMNEGoalEvaluationBO = null;

            string proc = "USP_GET_TRN_MNE_EVAL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectID_", projectID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_TRN_MNE_EVALBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("EVALUATIONID_", EvaluationID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_UPD_TRN_MNE_EVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("EVALUATIONID_", objMNEGoalEvaluationBO.EvaluationID);
                dcmd.Parameters.Add("PROJECTID_", objMNEGoalEvaluationBO.ProjectID);
                dcmd.Parameters.Add("GOALID_", objMNEGoalEvaluationBO.GoalID);
                dcmd.Parameters.Add("GOALDESCRIPTION_", objMNEGoalEvaluationBO.GoalDescription);
                dcmd.Parameters.Add("GOALNARRATIVE_", objMNEGoalEvaluationBO.GoalNarrative);
                dcmd.Parameters.Add("UPDATEBY_", objMNEGoalEvaluationBO.UpdatedBy);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_DEL_TRN_MNE_EVAL";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("EVALUATIONID_", EvaluationID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
