using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
  public class FenceBLL
    {
      /// <summary>
        /// To Get Fence description
      /// </summary>
      /// <returns></returns>
      public FenceList GetFencedescription( )
        {
            FenceDAL FencedescriptionDAL = new FenceDAL();
            try
            {
                return FencedescriptionDAL.GetFencedescription();
            }
            catch
            {
                throw;
            }
            finally
            {
                FencedescriptionDAL = null;
            }
        }

      /// <summary>
      /// To Edit Fence
      /// </summary>
      /// <param name="FenceBOobj"></param>
      /// <returns></returns>
      public int EditFence(FenceBO FenceBOobj)
      {
          FenceDAL FenceupdateDALobj = new FenceDAL(); //Data pass -to Database Layer

          try
          {
              return FenceupdateDALobj.EditFence(FenceBOobj);
          }
          catch
          {
              throw;
          }
          finally
          {
              FenceupdateDALobj = null;
          }
      }

      /// <summary>
      /// To Insert
      /// </summary>
      /// <param name="FenceBOobj"></param>
      /// <returns></returns>
      public int Insert(FenceBO FenceBOobj)
      {
          FenceDAL fencesaveDALobj = new FenceDAL(); //Data pass -to Database Layer

          try
          {
              return fencesaveDALobj.Insert(FenceBOobj);
          }
          catch
          {
              throw;
          }
          finally
          {
              fencesaveDALobj = null;
          }
      }

      /// <summary>
      /// To Get Fence Data
      /// </summary>
      /// <param name="householdID"></param>
      /// <returns></returns>
      public FenceList GetFencedata(int householdID)
      {
          FenceDAL fencedataDALobj = new FenceDAL();
          return fencedataDALobj.GetFencedata(householdID);
      }

      /// <summary>
      /// To Delete
      /// </summary>
      /// <param name="Pap_fenceid"></param>
      /// <returns></returns>
      public int Delete(int Pap_fenceid)
      {
          FenceDAL fencedelDALobj = new FenceDAL();
          try
          {
              return Convert.ToInt16(fencedelDALobj.Delete(Pap_fenceid));
          }
          catch (Exception ex)
          {

              throw ex;
          }
          finally
          {
              fencedelDALobj = null;

          }
      }

      /// <summary>
      /// To Get fenced Datarow
      /// </summary>
      /// <param name="Pap_fenceid"></param>
      /// <returns></returns>
      public FenceBO Getfencedatarow(int Pap_fenceid)
      {
          FenceDAL fencedatarowDALobj = new FenceDAL();//Data pass -to Database Layer
          return fencedatarowDALobj.Getfencedatarow(Pap_fenceid);
      }

      /// <summary>
      /// To Show PAP GRAVE
      /// </summary>
      /// <param name="householdID"></param>
      /// <param name="PermanentStructureID"></param>
      /// <returns></returns>
      public FenceBO ShowPAPGRAVE(int householdID, int PermanentStructureID)
      {
          return (new FenceDAL()).ShowPAPGRAVE(householdID, PermanentStructureID);
      }
    }
}
