using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class StatusCountBLL
    {
        
        public StatusCountBO GetApprPending(int UserID)
        {
            StatusCountDAL StatusCountDAL = new StatusCountDAL();
            return StatusCountDAL.GetApprPending(UserID);
        }

        public StatusCountBO GetClarifyPending(int UserID)
        {
            StatusCountDAL StatusCountDAL = new StatusCountDAL();
            return StatusCountDAL.GetClarifyPending(UserID);
        }
    }
}
