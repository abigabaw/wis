using WIS_BusinessObjects;
using WIS_DataAccess;
using System;

namespace WIS_BusinessLogic
{
    public class NegoBLL
    
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Negoobj"></param>
        /// <returns></returns>
        public int Insert(Nego Negoobj)
        {
            NegoDAL DALobj = new NegoDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(Negoobj);
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
        /// To Get NGO DATA
        /// </summary>
        /// <param name="culturalPropertyID"></param>
        /// <returns></returns>
        public NegoList GetNGODATA(int culturalPropertyID)
        {
            NegoDAL DALobj = new NegoDAL();
            return DALobj.GetNGODATA(culturalPropertyID);
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="CULTURALNEGOID"></param>
        /// <returns></returns>
        public Nego GetData(int CULTURALNEGOID)
        {
            NegoDAL DALobj = new NegoDAL();
            return DALobj.GetData(CULTURALNEGOID);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="Negoobj"></param>
        /// <returns></returns>
        public int Update(Nego Negoobj)
        {
            NegoDAL DALobj = new NegoDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(Negoobj);
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
    }
}
