using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
   public class LivBudgCategoryBLL
    {
       /// <summary>
        /// To Insert Budget Category
       /// </summary>
       /// <param name="LivBudgCategoryBOobj"></param>
       /// <returns></returns>
       public string InsertBudCategory(LivBudgCategoryBO LivBudgCategoryBOobj)
       {
           return (new LivBudgCategoryDAL()).InsertBudCategory(LivBudgCategoryBOobj);
       }

       /// <summary>
       /// To Update Living Budget Category
       /// </summary>
       /// <param name="LivBudgCategoryBOobj"></param>
       /// <returns></returns>
       public string UpdateLivBudgCategory(LivBudgCategoryBO LivBudgCategoryBOobj)
       {
           return (new LivBudgCategoryDAL()).UpdateLivBudgCategory(LivBudgCategoryBOobj);
       }

       /// <summary>
       /// To Get Living Budget Category By ID
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <returns></returns>
       public LivBudgCategoryBO GetLivBudCategoryByID(int livBudgCatgId)
       {
           return (new LivBudgCategoryDAL()).GetLivBudCategoryByID(livBudgCatgId);
       }

       /// <summary>
       /// To Delete Living Budget Category
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <returns></returns>
       public string DeleteLivBudCategory(int livBudgCatgId)
       {
           return (new LivBudgCategoryDAL()).DeleteCDAPBudgetMaster(livBudgCatgId);
       }

       /// <summary>
       /// TO Obsolete Living Budget Category
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteLivBudCategory(int livBudgCatgId, string isDeleted, int updatedBy)
       {
           return (new LivBudgCategoryDAL()).ObsoleteCDAPBudgetMaster(livBudgCatgId, isDeleted, updatedBy);
       }

       /// <summary>
       /// To Get All Living Budget Category
       /// </summary>
       /// <returns></returns>
       public List<LivBudgCategoryBO> GetAllLivBudCategory()
       {
           return (new LivBudgCategoryDAL()).GetAllLivBudCategory();
       }

        /// <summary>
       /// To Insert Budget Sub Item
        /// </summary>
        /// <param name="LivBudgItemBOobj"></param>
        /// <returns></returns>
       public string InsertBudgetSubItem(LivBudgItemBO LivBudgItemBOobj)
       {
           return (new LivBudgCategoryDAL()).InsertBudgetSubItem(LivBudgItemBOobj);
       }

       /// <summary>
       /// To Update Budget Sub Item
       /// </summary>
       /// <param name="LivBudgItemBOobj"></param>
       /// <returns></returns>
       public string UpdateBudgetSubItem(LivBudgItemBO LivBudgItemBOobj)
       {
           return (new LivBudgCategoryDAL()).UpdateBudgetSubItem(LivBudgItemBOobj);
       }

       /// <summary>
       /// To Delete Budget SubItem
       /// </summary>
       /// <param name="ItemId"></param>
       /// <returns></returns>
       public string DeleteBudgetSubItem(int ItemId)
       {
           return (new LivBudgCategoryDAL()).DeleteBudgetSubItem(ItemId);
       }

       /// <summary>
       /// To Obsolete Budget SubItem
       /// </summary>
       /// <param name="categoryID"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteBudgetSubItem(int categoryID, string isDeleted, int updatedBy)
       {
           return (new LivBudgCategoryDAL()).ObsoleteBudgetSubItem(categoryID, isDeleted, updatedBy);
       }

       /// <summary>
       /// To Get Budget SubItem By ID
       /// </summary>
       /// <param name="categoryID"></param>
       /// <returns></returns>
       public LivBudgItemBO GetBudgetSubItemByID(int categoryID)
       {
           return (new LivBudgCategoryDAL()).GetBudgetSubItemByID(categoryID);
       }

       /// <summary>
       /// To Get All Budget Sub Items
       /// </summary>
       /// <param name="categoryID"></param>
       /// <returns></returns>
       public List<LivBudgItemBO> GetAllBudgetSubItems(int categoryID)
       {
           return (new LivBudgCategoryDAL()).GetAllBudgetSubItems(categoryID);
       }

    }
}
