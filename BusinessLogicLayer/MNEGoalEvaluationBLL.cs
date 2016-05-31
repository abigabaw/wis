using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class MNEGoalEvaluationBLL
    {
        /// <summary>
        /// To Insert MNE Goal Eval
        /// </summary>
        /// <param name="objMNEGoalEvaluationBO"></param>
        /// <returns></returns>
        public string InsertMNEGoalEval(MNEGoalEvaluationBO objMNEGoalEvaluationBO)
        {
            MNEGoalEvaluationDAL objMNEGoalEvaluationDAL = new MNEGoalEvaluationDAL(); //Data pass -to Database Layer

            try
            {
                return objMNEGoalEvaluationDAL.InsertMNEGoalEval(objMNEGoalEvaluationBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objMNEGoalEvaluationDAL = null;
            }
        }

        /// <summary>
        /// To Get MNE Goal Evaluation
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public MNEGoalEvaluationList GetMNEGoalEvaluation(int projectID)
        {
            MNEGoalEvaluationDAL objMNEGoalEvaluationDAL = new MNEGoalEvaluationDAL();
            return objMNEGoalEvaluationDAL.GetMNEGoalEvaluation(projectID);
        }

        /// <summary>
        /// To Get MNE Goal Evaluation By ID
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public MNEGoalEvaluationBO GetMNEGoalEvaluationByID(int EvaluationID)
        {
            MNEGoalEvaluationDAL objMNEGoalEvaluationDAL = new MNEGoalEvaluationDAL();
            return objMNEGoalEvaluationDAL.GetMNEGoalEvaluationByID(EvaluationID);
        }

        /// <summary>
        /// To Update MNE Goal Evaluation
        /// </summary>
        /// <param name="objMNEGoalEvaluationBO"></param>
        /// <returns></returns>
        public string UpdateMNEGoalEvaluation(MNEGoalEvaluationBO objMNEGoalEvaluationBO)
        {
            MNEGoalEvaluationDAL objMNEGoalEvaluationDAL = new MNEGoalEvaluationDAL(); //Data pass -to Database Layer

            try
            {
                return objMNEGoalEvaluationDAL.UpdateMNEGoalEvaluation(objMNEGoalEvaluationBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objMNEGoalEvaluationDAL = null;
            }
        }

        /// <summary>
        /// To Delete MNE Goal Evaluation
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public string DeleteMNEGoalEvaluation(int EvaluationID)
        {
            MNEGoalEvaluationDAL objMNEGoalEvaluationDAL = new MNEGoalEvaluationDAL();
            return objMNEGoalEvaluationDAL.DeleteMNEGoalEvaluation(EvaluationID);
        }
    }
}
