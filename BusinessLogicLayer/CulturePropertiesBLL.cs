using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Data;
using System;

namespace WIS_BusinessLogic
{
    public class CulturePropertiesBLL
    {
        /// <summary>
        /// To Get Cultural Property Type
        /// </summary>
        /// <returns></returns>
        public CulturePropertiesList GetCulturalPropertyType()
        {
            CulturePropertiesDAL DALobj = new CulturePropertiesDAL();

            try
            {
                return DALobj.GetCulturalPropertyType();
            }
            catch (Exception erromsg)
            {
                throw (erromsg);
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="CulturPropertiesobj"></param>
        /// <returns></returns>
        public int Insert(CulturPropertiesBO CulturPropertiesobj)
        {
            CulturePropertiesDAL DALobj = new CulturePropertiesDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(CulturPropertiesobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Get Culture Property
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CulturePropertiesList GetCultureProp(int householdID)
        {
            CulturePropertiesDAL DALobj = new CulturePropertiesDAL();
            return DALobj.GetCultureProp(householdID);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="CulturPropertiesobj"></param>
        /// <returns></returns>
        public int Update(CulturPropertiesBO CulturPropertiesobj)
        {
            CulturePropertiesDAL DALobj = new CulturePropertiesDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(CulturPropertiesobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="culTURALPROPID"></param>
        /// <returns></returns>
        public CulturPropertiesBO GetData(int culTURALPROPID)
        {
            CulturePropertiesDAL DALobj = new CulturePropertiesDAL();
            return DALobj.GetData(culTURALPROPID);
        }

        /// <summary>
        /// To Show PAPCP Image
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public CulturPropertiesBO ShowPAPCPImage(int householdID, int PermanentStructureID)
        {
            return (new CulturePropertiesDAL()).ShowPAPCPImage(householdID, PermanentStructureID);
        }
    }
}
