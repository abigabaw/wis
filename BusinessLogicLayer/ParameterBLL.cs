using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class ParameterBLL
    {
        /// <summary>
        /// To Get Parameter
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public ParameterList GetOptionAvailable()
        {
            ParameterDAL objparameterDAL = new ParameterDAL();
            return objparameterDAL.GetOptionAvailable();
        }

        /// <summary>
        /// To Get All Counties
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public ParameterList GetAllParameters(int AvailableOptionID)
        {
            ParameterDAL ParameterDALobj = new ParameterDAL();
            return ParameterDALobj.GetAllParameters(AvailableOptionID);
        }

        /// <summary>
        /// To Add Parameter
        /// </summary>
        /// <param name="ParameterBOobj"></param>
        /// <returns></returns>
        public string AddParameter(ParameterBO ParameterBOobj)
        {
            return (new ParameterDAL()).AddParameter(ParameterBOobj);
        }

        /// <summary>
        /// To Update Parameter
        /// </summary>
        /// <param name="ParameterBOobj"></param>
        /// <returns></returns>
        public string UpdateParameter(ParameterBO ParameterBOobj)
        {
            return (new ParameterDAL()).UpdateParameter(ParameterBOobj);
        }

        /// <summary>
        /// To Delete Parameter
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public string DeleteParameter(int parameterID)
        {
            return (new ParameterDAL()).DeleteParameter(parameterID);
        }

        /// <summary>
        /// To Obsolete Parameter
        /// </summary>
        /// <param name="parameterID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteParameter(int parameterID, string isDeleted, int updatedBy)
        {
            return (new ParameterDAL()).ObsoleteParameter(parameterID, isDeleted, updatedBy);
        }

        /// <summary>
        /// To Get Parameter By Id
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public ParameterBO GetParameterById(int parameterID)
        {
            return (new ParameterDAL()).GetParameterById(parameterID);
        }

    }
}
