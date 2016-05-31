using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
   public class LocationClassificationBLL
    {
       /// <summary>
        /// To INSERT location
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string INSERTlocation(LocationClassificationBO BOobj)
       {
           LocationClassificationDAL DALobj = new LocationClassificationDAL(); //Data pass -to Database Layer
           try
           {
               return DALobj.INSERTlocation(BOobj);
           }
           catch
           {
               
               throw;
           }
           finally
           {
               DALobj = null;
           }
       }

       /// <summary>
       /// To UPDATE location
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string UPDATElocation(LocationClassificationBO BOobj)
       {
           LocationClassificationDAL DALobj = new LocationClassificationDAL();

           try
           {
               return DALobj.UPDATElocation(BOobj);

           }
           catch
           {

               throw;
           }
           finally
           {
               DALobj = null;
           }
       }

       /// <summary>
       /// To Get All LOCATION
       /// </summary>
       /// <returns></returns>
       public LocationClassificationList GetallLOCATION()
       {
           return (new LocationClassificationDAL()).GetallLOCATION();
       }

       /// <summary>
       /// To Get LOCATION Classification
       /// </summary>
       /// <returns></returns>
       public LocationClassificationList GetLOCATIONClassification()
       {
           return (new LocationClassificationDAL()).GetLOCATIONClassification();
       }

       /// <summary>
       /// To Get Location Classification ID
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <returns></returns>
       public LocationClassificationBO GetLOCTNCLASFCTNID(int LOCTNCLASFCTNID)
       {
           LocationClassificationDAL DALobj = new LocationClassificationDAL();
           return DALobj.GetLOCTNCLASFCTNID(LOCTNCLASFCTNID);
       }

       /// <summary>
       /// To Delete Location
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <returns></returns>
       public string DeleteLocation(int LOCTNCLASFCTNID)
       {
           LocationClassificationDAL DALobj = new LocationClassificationDAL();
           return DALobj.DeleteLocation(LOCTNCLASFCTNID);
       }

       /// <summary>
       /// To Obsolete Location
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <param name="IsDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteLocation(int LOCTNCLASFCTNID, string IsDeleted, int updatedBy)
       {
           return (new LocationClassificationDAL()).ObsoleteLocation(LOCTNCLASFCTNID, IsDeleted, updatedBy);
       }
    }
}
