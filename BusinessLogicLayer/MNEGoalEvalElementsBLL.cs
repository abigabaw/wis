using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class MNEGoalEvalElementsBLL
    {
        /// <summary>
        /// To Insert MNE Goal Eval Elements
        /// </summary>
        /// <param name="objMNEGoalEvalElementsBO"></param>
        /// <returns></returns>
        public string InsertMNEGoalEvalElements(MNEGoalEvalElementsBO objMNEGoalEvalElementsBO)
        {
            MNEGoalEvalElementsDAL objMNEGoalEvalElementsDAL = new MNEGoalEvalElementsDAL(); //Data pass -to Database Layer

            try
            {
                return objMNEGoalEvalElementsDAL.InsertMNEGoalEvalElements(objMNEGoalEvalElementsBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objMNEGoalEvalElementsDAL = null;
            }
        }

        /// <summary>
        /// To Get MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvaluationID"></param>
        /// <returns></returns>
        public MNEGoalEvalElementsList GetMNEGoalEvalElements(int EvaluationID)
        {
            MNEGoalEvalElementsDAL objMNEGoalEvalElementsDAL = new MNEGoalEvalElementsDAL();
            return objMNEGoalEvalElementsDAL.GetMNEGoalEvalElements(EvaluationID);
        }

        /// <summary>
        /// To Get MNE Goal Eval Elements By ID
        /// </summary>
        /// <param name="EvalelementID"></param>
        /// <returns></returns>
        public MNEGoalEvalElementsBO GetMNEGoalEvalElementsByID(int EvalelementID)
        {
            MNEGoalEvalElementsDAL objMNEGoalEvalElementsDAL = new MNEGoalEvalElementsDAL();
            return objMNEGoalEvalElementsDAL.GetMNEGoalEvalElementsByID(EvalelementID);
        }

        /// <summary>
        /// To Update MNE Goal Eval Elements
        /// </summary>
        /// <param name="objMNEGoalEvalElementsBO"></param>
        /// <returns></returns>
        public string UpdateMNEGoalEvalElements(MNEGoalEvalElementsBO objMNEGoalEvalElementsBO)
        {
            MNEGoalEvalElementsDAL objMNEGoalEvalElementsDAL = new MNEGoalEvalElementsDAL(); //Data pass -to Database Layer

            try
            {
                return objMNEGoalEvalElementsDAL.UpdateMNEGoalEvalElements(objMNEGoalEvalElementsBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objMNEGoalEvalElementsDAL = null;
            }
        }

        /// <summary>
        /// To Delete MNE Goal Eval Elements
        /// </summary>
        /// <param name="EvalelementID"></param>
        /// <returns></returns>
        public string DeleteMNEGoalEvalElements(int EvalelementID)
        {
            MNEGoalEvalElementsDAL objMNEGoalEvalElementsDAL = new MNEGoalEvalElementsDAL();
            return objMNEGoalEvalElementsDAL.DeleteMNEGoalEvalElements(EvalelementID);
        }
    }
}
