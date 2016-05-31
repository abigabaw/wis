using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class DiseaseBLL
    {
        /// <summary>
        /// To Get ALL Diseases
        /// </summary>
        /// <param name="DiseaseName"></param>
        /// <returns></returns>
        public DiseaseList GetALLDiseases(string DiseaseName)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.GetALLDisease(DiseaseName);
        }

        /// <summary>
        /// To Search Disease
        /// </summary>
        /// <param name="DiseaseName"></param>
        /// <returns></returns>
        public DiseaseList SearchDisease(string DiseaseName)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.SearchDisease(DiseaseName);            
        }

        /// <summary>
        /// To Add Disease
        /// </summary>
        /// <param name="objDisease"></param>
        /// <returns></returns>
        public string AddDisease(DiseaseBO objDisease)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.AddDisease(objDisease);
        }

        /// <summary>
        /// To Delete Disease
        /// </summary>
        /// <param name="DiseaseID"></param>
        /// <returns></returns>
       public string DeleteDisease(int DiseaseID)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.DeleteDisease(DiseaseID);

        }

        /// <summary>
       /// To Update Disease
        /// </summary>
        /// <param name="objDisease"></param>
        /// <returns></returns>
        public string UpdateDisease(DiseaseBO objDisease)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.UpdateDisease(objDisease);
        }

        /// <summary>
        /// To Get Disease By Disease ID
        /// </summary>
        /// <param name="diseaseID"></param>
        /// <returns></returns>
        public DiseaseBO GetDiseaseByDiseaseID(int diseaseID)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.GetDiseaseByDiseaseID(diseaseID);
        }

        /// <summary>
        /// To Obsolete Disease
        /// </summary>
        /// <param name="DiSEASEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteDisease(int DiSEASEID, string IsDeleted)
        {
            DiseaseDAL objDiseaseDAL = new DiseaseDAL();
            return objDiseaseDAL.ObsoleteDisease(DiSEASEID, IsDeleted);
        }
    }
}

/**
 * 
 * @version          :Disease Master
 * @package          :DiseaseBLL
 * @copyright        :Copyright © 2013 - All rights reserved.
 * @author           :Hanamant Singannavar
 * @Created Date     :17-Apr-2013 
 * @Updated By
 * @Updated Date
 * 
 */
