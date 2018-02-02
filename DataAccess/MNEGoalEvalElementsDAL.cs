using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_INS_MNEEVALELEMENTS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("EVALUATIONID_", objMNEGoalEvalElementsBO.EvaluationID);
                dcmd.Parameters.AddWithValue("GOAL_ELEMENTID_", objMNEGoalEvalElementsBO.Goal_elementID);
                dcmd.Parameters.AddWithValue("EVALELEMENTDESCRIPTIONN_", objMNEGoalEvalElementsBO.Evalelementdescriptionn);
                dcmd.Parameters.AddWithValue("ISDELETED_", objMNEGoalEvalElementsBO.Isdeleted);
                dcmd.Parameters.AddWithValue("CREATEDBY_", objMNEGoalEvalElementsBO.Createdby);
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
        /// To Get MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public MNEGoalEvalElementsList GetMNEGoalEvalElements(int EvaluationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = null;

            string proc = "USP_GET_MNEEVALELEMENTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EVALUATIONID_", EvaluationID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_MNEEVALELEMENTSBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EVALELEMENTID_", EvalelementID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_UPD_MNEEVALELEMENTS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("EVALELEMENTID_", objMNEGoalEvalElementsBO.EvalelementID);
                dcmd.Parameters.AddWithValue("EVALUATIONID_", objMNEGoalEvalElementsBO.EvaluationID);
                dcmd.Parameters.AddWithValue("GOAL_ELEMENTID_", objMNEGoalEvalElementsBO.Goal_elementID);
                dcmd.Parameters.AddWithValue("EVALELEMENTDESCRIPTIONN_", objMNEGoalEvalElementsBO.Evalelementdescriptionn);
                dcmd.Parameters.AddWithValue("UPDATEBY_", objMNEGoalEvalElementsBO.Updatedby);
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
        /// To Delete MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvalelementID"></param>
        /// <returns></returns>
        public string DeleteMNEGoalEvalElements(int EvalelementID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_DEL_MNEEVALELEMENTS";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EVALELEMENTID_", EvalelementID);
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
