using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class Non_perm_structureBLL
    {
        /// <summary>
        /// To Insert into Databse
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
      public int Insert(NonPermanentStructureBO BOobj)
      {
          Non_perm_structureDAL structureDALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer

          try
          {
              return structureDALobj.Insert(BOobj);
          }
          catch
          {
              throw;
          }
          finally
          {
              structureDALobj = null;
          }
      }

        /// <summary>
      /// To Get NPS
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
      public Non_perm_structureList GetNPS(int householdID)
      {
          Non_perm_structureDAL NPSDALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer
          return NPSDALobj.GetNPS(householdID);
      }

        /// <summary>
      /// To Edit NPS
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
      public int EditNPS(NonPermanentStructureBO BOobj)
      {
          Non_perm_structureDAL NPSDALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer

          try
          {
              return NPSDALobj.EditNPS(BOobj);
          }
          catch
          {
              throw;
          }
          finally
          {
              NPSDALobj = null;
          }
      }

        /// <summary>
      /// To Get NPS Id
        /// </summary>
        /// <param name="NonPermanentStructureID"></param>
        /// <returns></returns>
      public NonPermanentStructureBO GetNPSId(int NonPermanentStructureID)
      {
          Non_perm_structureDAL NPSidDALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer
          return NPSidDALobj.GetNPSId(NonPermanentStructureID);
      }

        /// <summary>
        /// To Delete from Database
        /// </summary>
        /// <param name="NonPermanentStructureID"></param>
        /// <returns></returns>
      public int  Delete(string NonPermanentStructureID)
      {
          Non_perm_structureDAL NPSdeleteDALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer
          try
          {
              return Convert.ToInt16(NPSdeleteDALobj.Delete(NonPermanentStructureID));
          }
          catch (Exception ex)
          {

              throw ex;
          }
          finally
          {
              NPSdeleteDALobj = null;
          }
      }

        /// <summary>
      /// To Show PAP NPB Image
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
      public NonPermanentStructureBO ShowPAPNPBImage(int householdID, int PermanentStructureID)
      {
          return (new Non_perm_structureDAL()).ShowPAPNPBImage(householdID, PermanentStructureID);
      }

        /// <summary>
      /// To Update photo
        /// </summary>
        /// <param name="BOobj1"></param>
        /// <returns></returns>
      public int Updatephoto(NonPermanentStructureBO BOobj1)
      {
          Non_perm_structureDAL DALobj = new Non_perm_structureDAL(); //Data pass -to Database Layer

          try
          {
              return DALobj.Updatephoto(BOobj1);
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {
              BOobj1 = null;
          }
      }
    }
}
