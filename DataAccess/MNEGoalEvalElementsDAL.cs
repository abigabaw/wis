using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalEvalElementsDAL
    {
        /// <summary>
        /// To Insert MNE Goal Eval Elements
        /// </summary>
        /// <param name="objMNEGoalEvalElementsBO"></param>
        /// <returns></returns>
        public string InsertMNEGoalEvalElements(MNEGoalEvalElementsBO objMNEGoalEvalElementsBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_INS_MNEEVALELEMENTS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("EVALUATIONID_", objMNEGoalEvalElementsBO.EvaluationID);
                dcmd.Parameters.Add("GOAL_ELEMENTID_", objMNEGoalEvalElementsBO.Goal_elementID);
                dcmd.Parameters.Add("EVALELEMENTDESCRIPTIONN_", objMNEGoalEvalElementsBO.Evalelementdescriptionn);
                dcmd.Parameters.Add("ISDELETED_", objMNEGoalEvalElementsBO.Isdeleted);
                dcmd.Parameters.Add("CREATEDBY_", objMNEGoalEvalElementsBO.Createdby);
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
        /// To Get MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public MNEGoalEvalElementsList GetMNEGoalEvalElements(int EvaluationID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = null;

            string proc = "USP_GET_MNEEVALELEMENTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("EVALUATIONID_", EvaluationID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalEvalElementsList MNEGoalEvalElements = new MNEGoalEvalElementsList();

            while (dr.Read())
            {
                objMNEGoalEvalElementsBO = new MNEGoalEvalElementsBO();
                objMNEGoalEvalElementsBO.EvaluationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evaluationid")));
                objMNEGoalEvalElementsBO.EvalelementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evalelementid")));
                objMNEGoalEvalElementsBO.Goal_elementname = dr.GetString(dr.GetOrdinal("goal_elementname"));
                objMNEGoalEvalElementsBO.Evalelementdescriptionn = dr.GetString(dr.GetOrdinal("evalelementdescriptionn"));

                //    objMNEGoalEvaluationBO.ProjectID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("projectid")));

                MNEGoalEvalElements.Add(objMNEGoalEvalElementsBO);
            }

            dr.Close();

            return MNEGoalEvalElements;
        }

        /// <summary>
        /// To Get MNE Goal Eval Elements By ID
        /// </summary>
        /// <param name="EvalelementID"></param>
        /// <returns></returns>
        public MNEGoalEvalElementsBO GetMNEGoalEvalElementsByID(int EvalelementID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_MNEEVALELEMENTSBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("EVALELEMENTID_", EvalelementID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = new MNEGoalEvalElementsBO();

            while (dr.Read())
            {

                objMNEGoalEvalElementsBO.Goal_elementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("goal_elementid")));

                objMNEGoalEvalElementsBO.Evalelementdescriptionn = Convert.ToString(dr.GetValue(dr.GetOrdinal("evalelementdescriptionn")));

                objMNEGoalEvalElementsBO.EvaluationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evaluationid")));

                objMNEGoalEvalElementsBO.EvalelementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("evalelementid")));
            }
            dr.Close();
            return objMNEGoalEvalElementsBO;
        }

        /// <summary>
        /// To Update MNE Goal Eval Elements
        /// </summary>
        /// <param name="objMNEGoalEvalElementsBO"></param>
        /// <returns></returns>
        public string UpdateMNEGoalEvalElements(MNEGoalEvalElementsBO objMNEGoalEvalElementsBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_UPD_MNEEVALELEMENTS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("EVALELEMENTID_", objMNEGoalEvalElementsBO.EvalelementID);
                dcmd.Parameters.Add("EVALUATIONID_", objMNEGoalEvalElementsBO.EvaluationID);
                dcmd.Parameters.Add("GOAL_ELEMENTID_", objMNEGoalEvalElementsBO.Goal_elementID);
                dcmd.Parameters.Add("EVALELEMENTDESCRIPTIONN_", objMNEGoalEvalElementsBO.Evalelementdescriptionn);
                dcmd.Parameters.Add("UPDATEBY_", objMNEGoalEvalElementsBO.Updatedby);
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
        /// To Delete MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvalelementID"></param>
        /// <returns></returns>
        public string DeleteMNEGoalEvalElements(int EvalelementID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_DEL_MNEEVALELEMENTS";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("EVALELEMENTID_", EvalelementID);
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
