using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class GraveFinishBLL
    {
        /// <summary>
        /// To Get All Grave Finish
        /// </summary>
        /// <returns></returns>
        public GraveFinishList GetAllGraveFinish()
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.GetAllGraveFinish();
        }

        /// <summary>
        /// To Get Grave Finish
        /// </summary>
        /// <returns></returns>
        public GraveFinishList  GetGraveFinish()
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.GetGraveFinish();
        }

        /// <summary>
        /// To Add Grave
        /// </summary>
        /// <param name="objGrave"></param>
        /// <returns></returns>
        public string AddGrave(GraveFinishBO objGrave)
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.AddGrave(objGrave);           
        }

        /// <summary>
        /// To Delete Grave
        /// </summary>
        /// <param name="graveID"></param>
        /// <returns></returns>
        public string DeleteGrave(int graveID)
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.DeleteGrave(graveID);
        }

        /// <summary>
        /// To Update Grave
        /// </summary>
        /// <param name="objGF"></param>
        /// <returns></returns>
        public string UpdateGrave(GraveFinishBO objGF)
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.UpdateGrave(objGF);
        }

        /// <summary>
        /// To Get Grave By ID
        /// </summary>
        /// <param name="graveID"></param>
        /// <returns></returns>
        public GraveFinishBO GetGraveByID(int graveID)
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.GetGraveByID(graveID);
        }

        /// <summary>
        /// To Obsolete Grave Finish
        /// </summary>
        /// <param name="GraveFinishID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGraveFinish(int GraveFinishID, string IsDeleted)
        {
            GraveFinishDAL objGraveDAL = new GraveFinishDAL();
            return objGraveDAL.ObsoleteGraveFinish(GraveFinishID, IsDeleted);
        }
    }
}