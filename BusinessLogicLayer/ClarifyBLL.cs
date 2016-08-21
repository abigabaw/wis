using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ClarifyBLL
    {
        public ClarifyList GetClarifications(int HHID, int UserID)
        {
            ClarifyDAL ClarifyDAL = new ClarifyDAL();
            return ClarifyDAL.GetData(HHID,UserID);
        }

        public string InsertClarify(ClarifyBO ClarifyBO)
        {
            ClarifyDAL ClarifyDAL = new ClarifyDAL(); //Data pass -to Database Layer
            return ClarifyDAL.InsertClarify(ClarifyBO);
        }

        public ClarifyList GetMyClarifications(int UserID)
        {
            ClarifyDAL ClarifyDAL = new ClarifyDAL();
            return ClarifyDAL.GetMyClarify(UserID);
        }

        public ClarifyBO SelectClarification(int ClarifyID)
        {
            ClarifyDAL ClarifyDAL = new ClarifyDAL(); 
            return ClarifyDAL.SelectClarification(ClarifyID);
        }
    }
}
