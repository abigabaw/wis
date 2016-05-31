using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class VaccinationBLL
    {
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="Vaccinationobj"></param>
        /// <returns></returns>
        public string Insert(VaccinationBO Vaccinationobj)
        {
            VaccinationDAL VaccinationDALobj = new VaccinationDAL(); //Data pass -to Database Layer

            try
            {
                return VaccinationDALobj.Insert(Vaccinationobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                VaccinationDALobj = null;
            }
        }

        /// <summary>
        /// To Edit Vaccination
        /// </summary>
        /// <param name="Vaccinationobj"></param>
        /// <returns></returns>
        public string EditVaccination(VaccinationBO Vaccinationobj)
       {
           VaccinationDAL VaccinationDALobj = new VaccinationDAL(); //Data pass -to Database Layer

           try
           {
               return VaccinationDALobj.EditVaccination(Vaccinationobj);
           }
           catch
           {
               throw;
           }
           finally
           {
               VaccinationDALobj = null;
           }
       }

        /// <summary>
        /// To Get ALL Vaccination
        /// </summary>
        /// <returns></returns>
        public VaccinationList GetALLVaccination()
        {
            VaccinationDAL VaccinationDALobj = new VaccinationDAL();
            return VaccinationDALobj.GetALLVaccination();
        }

        /// <summary>
        /// To Get Vaccination
        /// </summary>
        /// <returns></returns>
      public VaccinationList GetVaccination()
       {
           VaccinationDAL VaccinationDALobj = new VaccinationDAL();
           return VaccinationDALobj.GetVaccination();
       }

        /// <summary>
      /// To Get Vaccination By Id
        /// </summary>
        /// <param name="VaccinationID"></param>
        /// <returns></returns>
      public VaccinationBO GetVaccinationById(int VaccinationID)
      {
          VaccinationDAL VaccinationDALobj = new VaccinationDAL();
          return VaccinationDALobj.GetVaccinationById(VaccinationID);
      }

        /// <summary>
      /// To Delete Vaccination
        /// </summary>
        /// <param name="vaccinationID"></param>
        /// <returns></returns>
      public string DeleteVaccination(int vaccinationID)
      {
          VaccinationDAL VaccinationDALobj = new VaccinationDAL();
          return VaccinationDALobj.DeleteVaccination(vaccinationID);
      }

        /// <summary>
      /// To Obsolete vaccination
        /// </summary>
        /// <param name="VaccinationID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
      public string Obsoletevaccination(int VaccinationID, string IsDeleted)
      {
          VaccinationDAL VaccinationDALobj = new VaccinationDAL();
          return VaccinationDALobj.Obsoletevaccination(VaccinationID , IsDeleted);
      }

    }
}