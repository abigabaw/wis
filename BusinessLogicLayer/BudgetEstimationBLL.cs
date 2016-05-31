using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class BudgetEstimationBLL
    {
        /// <summary>
        /// To get All Category details from database
        /// </summary>
        /// <returns></returns>
        public BudgetEstimationList getAllCategory()
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer

            try
            {
                return BudgetEstimationDAL.getAllCategory();
            }
            catch
            {
                throw;
            }
            finally
            {
                BudgetEstimationDAL = null;
            }
        }
        /// <summary>
        /// To get SubCategory ByCategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public BudgetEstimationList getSubCatByCatID(int CategoryID)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer

            try
            {
                return BudgetEstimationDAL.getSubCatByCatID(CategoryID);
            }
            catch
            {
                throw;
            }
            finally
            {
                BudgetEstimationDAL = null;
            }
        }
        /// <summary>
        /// To Insert Budget Estimation details
        /// </summary>
        /// <param name="objBudgetEstimation"></param>
        /// <returns></returns>
        public string InsertBudgetEstimation(BudgetEstimationBO objBudgetEstimation)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer

            return BudgetEstimationDAL.InsertBudgetEstimation(objBudgetEstimation);
        }
        /// <summary>
        /// To Insert NEW Category details
        /// </summary>
        /// <param name="objNEWCategory"></param>
        /// <returns></returns>
        public string InsertNEWCategory(BudgetEstimationBO objNEWCategory)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer
            return BudgetEstimationDAL.InsertNEWCategory(objNEWCategory);
        }
        /// <summary>
        /// To Insert NEW sub Category details
        /// </summary>
        /// <param name="objNEWCategory"></param>
        /// <returns></returns>
        public string InsertNEWsubCategory(BudgetEstimationBO objNEWsubCategory)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer
            return BudgetEstimationDAL.InsertNEWsubCategory(objNEWsubCategory);
        }
        /// <summary>
        /// To Get Budget Estimation details
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public BudgetEstimationList GetBudgetEstimation(string pID, int categoryID)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer

            try
            {
                return BudgetEstimationDAL.GetBudgetEstimation(pID, categoryID);
            }
            catch
            {
                throw;
            }
            finally
            {
                BudgetEstimationDAL = null;
            }
        }
        /// <summary>
        /// To Get Budget Estimation details by ID
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public BudgetEstimationBO GetBudgetEstimationByID(int BudgetEstimationID)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL();
            return BudgetEstimationDAL.GetBudgetEstimationByID(BudgetEstimationID);
        }
        /// <summary>
        /// To Delete Budget Estimation details
        /// </summary>
        /// <param name="BudgetEstimationID"></param>
        /// <returns></returns>
        public string DeleteBudgetEstimation(int BudgetEstimationID)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL();
            return BudgetEstimationDAL.DeleteBudgetEstimation(BudgetEstimationID);
        }
        /// <summary>
        /// To update Budget Estimation details
        /// </summary>
        /// <param name="objBudgetEstimation"></param>
        /// <returns></returns>
        public string EditBudgetEstimation(BudgetEstimationBO objBudgetEstimation)
        {
            BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer
            return BudgetEstimationDAL.EditBudgetEstimation(objBudgetEstimation);
        }
        /// <summary>
        /// To get Currency From Project details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BudgetEstimationBO getCurrenceFromProject(string projectID)
         {
             BudgetEstimationDAL BudgetEstimationDAL = new BudgetEstimationDAL(); //Data pass -to Database Layer
             return BudgetEstimationDAL.getCurrenceFromProject(projectID);
         }
    }
}