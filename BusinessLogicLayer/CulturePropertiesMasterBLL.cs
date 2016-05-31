using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;
using System.Data;

namespace WIS_BusinessLogic
{
    public class CulturePropertiesMasterBLL
    {
        CulturePropertiesMasterDAL CulturePropertiesMasterDALObj = null;
        /// <summary>
        /// To Insert Into Culture Properties
        /// </summary>
        /// <param name="CulturePropertiesMasterBObj"></param>
        /// <returns></returns>
        public string InsertIntoCultureProp(CulturePropertiesMasterBO CulturePropertiesMasterBObj)
        {
            CulturePropertiesMasterDALObj =new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.InsertIntoCultureProp(CulturePropertiesMasterBObj);
        }

        /// <summary>
        /// To Get All Culture Property Type
        /// </summary>
        /// <returns></returns>
        public CulturePropTypeList GetAllCulturePropertyType()
        {
            CulturePropertiesMasterDALObj = new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.GetAllCulturePropertyType();
        }

        /// <summary>
        /// To Get Culture Properties By ID
        /// </summary>
        /// <param name="CulturePropID"></param>
        /// <returns></returns>
        public CulturePropertiesMasterBO GetCulturePropByID(int CulturePropID)
        {
            CulturePropertiesMasterDALObj = new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.GetCulturePropByID(CulturePropID);
        }

        /// <summary>
        /// To Delete Culture Properties By ID
        /// </summary>
        /// <param name="CulturePropID"></param>
        /// <returns></returns>
         public string DeleteCulturePropByID(int CulturePropID)
        {
            CulturePropertiesMasterDALObj = new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.DeleteCulturePropByID(CulturePropID);
        }

        /// <summary>
         /// To EDIT Culture Properties By ID
        /// </summary>
        /// <param name="CulturePropertiesMasterBObj"></param>
        /// <returns></returns>
         public string EDITCulturePropByID(CulturePropertiesMasterBO CulturePropertiesMasterBObj)
        {
            CulturePropertiesMasterDALObj = new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.EDITCulturePropByID(CulturePropertiesMasterBObj);
        }

        /// <summary>
         /// To Obsolete Culture Properties Type
        /// </summary>
        /// <param name="CulturePropTypeID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
         public string ObsoleteCulturePropType(int CulturePropTypeID, string Isdeleted)
        {
            CulturePropertiesMasterDALObj = new CulturePropertiesMasterDAL();
            return CulturePropertiesMasterDALObj.ObsoleteCulturePropType(CulturePropTypeID, Isdeleted);
        }
    }
}
