using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class WelfareIndicatorBLL
    {
        /// <summary>
        /// To Insert Welfare Indicator
        /// </summary>
        /// <param name="objWelfareIndicatorBO"></param>
        /// <returns></returns>
        public string InsertWelfareIndicator(WelfareIndicatorBO objWelfareIndicatorBO)
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL(); //Data pass -to Database Layer

            try
            {
                return objWelfareIndicatorDAL.InsertWelfareIndicator(objWelfareIndicatorBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objWelfareIndicatorDAL = null;
            }
        }

        /// <summary>
        /// To Get Welfare Indicator
        /// </summary>
        /// <returns></returns>
        public WelfareIndicatorList GetWelfareIndicator()
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL();
            return objWelfareIndicatorDAL.GetWelfareIndicator();
        }

        /// <summary>
        /// To Get Welfare Indicator By Id
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <returns></returns>
        public WelfareIndicatorBO GetWelfareIndicatorById(int Wlf_indicatorID)
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL();
            return objWelfareIndicatorDAL.GetWelfareIndicatorById(Wlf_indicatorID);
        }

        /// <summary>
        /// To Update Welfare Indicator
        /// </summary>
        /// <param name="objWelfareIndicatorBO"></param>
        /// <returns></returns>
        public string UpdateWelfareIndicator(WelfareIndicatorBO objWelfareIndicatorBO)
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL(); //Data pass -to Database Layer

            try
            {
                return objWelfareIndicatorDAL.UpdateWelfareIndicator(objWelfareIndicatorBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objWelfareIndicatorDAL = null;
            }
        }

        /// <summary>
        /// To Delete Welfare Indicator
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <returns></returns>
        public string DeleteWelfareIndicator(int Wlf_indicatorID)
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL();
            return objWelfareIndicatorDAL.DeleteWelfareIndicator(Wlf_indicatorID);
        }

        /// <summary>
        /// To Obsolete Welfare Indicator
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteWelfareIndicator(int Wlf_indicatorID, string IsDeleted)
        {
            WelfareIndicatorDAL objWelfareIndicatorDAL = new WelfareIndicatorDAL();
            return objWelfareIndicatorDAL.ObsoleteWelfareIndicator(Wlf_indicatorID, IsDeleted);
        }

    }
}
