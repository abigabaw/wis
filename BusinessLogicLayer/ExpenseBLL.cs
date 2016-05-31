using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Data;

namespace WIS_BusinessLogic
{
  public  class ExpenseBLL
    {
      /// <summary>
        /// To Excel Data Import into Grid
      /// </summary>
      /// <param name="FilePath"></param>
      /// <param name="Extension"></param>
      /// <param name="projectID"></param>
      /// <param name="createdBy"></param>
      /// <returns></returns>
      public DataTable ExcelDataImportintoGrid( string FilePath,string Extension,int projectID, int createdBy)
      {
          ExpenseDAL objExpensePopup = new ExpenseDAL();
          return objExpensePopup.ExcelDataImportintoGrid(FilePath, Extension, projectID, createdBy);
      }

      /// <summary>
      /// To Get Expense Data For ACC
      /// </summary>
      /// <param name="ProjectID"></param>
      /// <returns></returns>
      public object GetExpenseDataForACC(int ProjectID)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.GetExpenseDataForACC(ProjectID);
      }

      /// <summary>
      /// To Get All Expense Data
      /// </summary>
      /// <param name="ProjectID"></param>
      /// <returns></returns>
      public object GetAllExpenseData(int ProjectID)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.GetAllExpenseData(ProjectID);
      }

      /// <summary>
      /// To Save Data
      /// </summary>
      /// <param name="dtExpen"></param>
      /// <param name="ProjectID"></param>
      /// <param name="uID"></param>
      /// <returns></returns>
      public DataTable savedata(DataTable dtExpen, int ProjectID, string uID)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.savedata(dtExpen, ProjectID, uID);
      }
      //New Changes

      /// <summary>
      /// To Add Expense
      /// </summary>
      /// <param name="objExpense"></param>
      /// <returns></returns>
      public string AddExpense(ExpenseBO objExpense)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.AddExpense(objExpense);
      }
      /// <summary>
      /// To Update Expense
      /// </summary>
      /// <param name="objExpense"></param>
      /// <returns></returns>
      public string UpdateExpense(ExpenseBO objExpense)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.UpdateBank(objExpense);
      }
      /// <summary>
      /// To Delete Expense
      /// </summary>
      /// <param name="EXPENSEID"></param>
      /// <returns></returns>
      public string DeleteExpense(int EXPENSEID)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.DeleteExpense(EXPENSEID);
      }
      public ExpenseBO GetExpenseByID(int EXPENSEID)
      {
          ExpenseDAL ExpenseDALobj = new ExpenseDAL();
          return ExpenseDALobj.GetExpenseByID(EXPENSEID);
      }

    }
}
