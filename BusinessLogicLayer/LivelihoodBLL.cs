using System;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LivelihoodBLL
    {
        /// <summary>
        /// To Add Livelihood
        /// </summary>
        /// <param name="objLivelihood"></param>
        /// <returns></returns>
        public string AddLivelihood(LivelihoodBO objLivelihood)
        {
            LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL(); //Data pass -to Database Layer

            try
            {
                return objLivelihoodDAL.AddLivelihood(objLivelihood);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objLivelihoodDAL = null;
            }
        }

        /// <summary>
        /// To Get ALL Livelihood
        /// </summary>
        /// <returns></returns>
        public LivelihoodList GetALLLivelihood()
        {
            LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL();
            return objLivelihoodDAL.GetALLLivelihood();
        }

        /// <summary>
        /// To Get Livelihood
        /// </summary>
        /// <returns></returns>
        public LivelihoodList GetLivelihood()
        {
            LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL();
            return objLivelihoodDAL.GetLivelihood();
        }

        /// <summary>
        /// To Update Livelihood
        /// </summary>
        /// <param name="objLivelihood"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public string UpdateLivelihood(LivelihoodBO objLivelihood, int itemid)
        {
            LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL();
            try
            {
                return objLivelihoodDAL.UpdateLivelihood(objLivelihood, itemid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objLivelihood = null;
            }
        }

        /// <summary>
        /// To Delete Livelihood
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public string DeleteLivelihood(int itemid)
        {
                LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL();
            return objLivelihoodDAL.DeleteLivelihood(itemid);

              }

        /// <summary>
        /// To Obsolete Livelihood
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLivelihood(int itemid, string IsDeleted)
        {
            LivelihoodDAL objLivelihoodDAL = new LivelihoodDAL();
            return objLivelihoodDAL.ObsoleteLivelihood(itemid, IsDeleted);
        }
    }
}