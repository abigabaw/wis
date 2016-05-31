using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CopMechanismBLL
    {
        CopMechanismDAL objCopMechanismDAL;
        /// <summary>
        /// To Get ALL Cop Mechanism
        /// </summary>
        /// <returns></returns>
        public CopMechanismList GetALLCopMechanism()//(CopMechanism oCopMechanism)
        {
            objCopMechanismDAL = new CopMechanismDAL();
            return objCopMechanismDAL.GetALLCopMechanism();// (oCopMechanism);
        }

        /// <summary>
        /// To Get Cop Mechanism
        /// </summary>
        /// <returns></returns>
        public CopMechanismList GetCopMechanism()//(CopMechanism oCopMechanism)
        {
            objCopMechanismDAL = new CopMechanismDAL();
            return objCopMechanismDAL.GetCopMechanism();// (oCopMechanism);
        }

        /// <summary>
        /// To Add Cop Mechanism
        /// </summary>
        /// <param name="oCopMechanism"></param>
        /// <returns></returns>
        public string AddCopMechanism(CopMechanismBO oCopMechanism)
        {
            objCopMechanismDAL = new CopMechanismDAL();

            return objCopMechanismDAL.SaveCopMechanism(oCopMechanism);
        }

        /// <summary>
        /// To Update Cop Mechanism
        /// </summary>
        /// <param name="oCopMechanism"></param>
        /// <returns></returns>
        public string UpdateCopMechanism(CopMechanismBO oCopMechanism)
        {
            objCopMechanismDAL = new CopMechanismDAL();

            return objCopMechanismDAL.UpdateCopMechanism(oCopMechanism);
        }

        /// <summary>
        /// To Get Cop Mechanism By Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public CopMechanismBO GetCopMechanismById(int UserId)
        {
            objCopMechanismDAL = new CopMechanismDAL();
            return objCopMechanismDAL.GetCopMechanismById(UserId);
        }

        /// <summary>
        /// To Delete Cop Mechanism
        /// </summary>
        /// <param name="CopMechanismID"></param>
        /// <returns></returns>
        public string DeleteCopMechanism(int CopMechanismID)
        {
            objCopMechanismDAL = new CopMechanismDAL();
            return objCopMechanismDAL.DeleteCopMechanism(CopMechanismID);
        }

        /// <summary>
        /// To Obsolete Cop Mechanism
        /// </summary>
        /// <param name="CopMechanismID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCopMechanism(int CopMechanismID, string IsDeleted)
        {
            CopMechanismDAL objCopMechanismDAL = new CopMechanismDAL();
            return objCopMechanismDAL.ObsoleteCopMechanism(CopMechanismID, IsDeleted);
        }
    }
}