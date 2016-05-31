using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class RepresentationBLL
    {
        /// <summary>
        /// To Insert Representation
        /// </summary>
        /// <param name="objRepresentationBO"></param>
        /// <returns></returns>
        public string InsertRepresentation(RepresentationBO objRepresentationBO)
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL(); //Data pass -to Database Layer

            try
            {
                return objRepresentationDAL.InsertRepresentation(objRepresentationBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objRepresentationBO = null;
            }
        }

        /// <summary>
        /// To Get Representation
        /// </summary>
        /// <returns></returns>
        public RepresentationList GetRepresentation()
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL();
            return objRepresentationDAL.GetRepresentation();
        }

        /// <summary>
        /// To Get Representation By Id
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <returns></returns>
        public RepresentationBO GetRepresentationById(int RepresentationID)
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL();
            return objRepresentationDAL.GetRepresentationById(RepresentationID);
        }

        /// <summary>
        /// To Update Representation
        /// </summary>
        /// <param name="objRepresentationBO"></param>
        /// <returns></returns>
        public string UpdateRepresentation(RepresentationBO objRepresentationBO)
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL(); //Data pass -to Database Layer

            try
            {
                return objRepresentationDAL.UpdateRepresentation(objRepresentationBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objRepresentationDAL = null;
            }
        }

        /// <summary>
        /// To Delete Representation
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <returns></returns>
        public string DeleteRepresentation(int RepresentationID)
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL();
            return objRepresentationDAL.DeleteRepresentation(RepresentationID);
        }

        /// <summary>
        /// To Obsolete Representation
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRepresentation(int RepresentationID, string IsDeleted)
        {
            RepresentationDAL objRepresentationDAL = new RepresentationDAL();
            return objRepresentationDAL.ObsoleteRepresentation(RepresentationID, IsDeleted);
        }
    }
}
