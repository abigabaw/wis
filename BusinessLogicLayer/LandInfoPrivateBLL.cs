using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class LandInfoPrivateBLL
    {
        /// <summary>
        /// To Get Land Info Priv
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PrivateLandInfoBO GetLandInfoPriv(int householdID)
        {           
            LandInfoPrivateDAL objLandInfoResDAL = new LandInfoPrivateDAL();
            return objLandInfoResDAL.GetLandInfoPriv(householdID);
        }

        /// <summary>
        /// To Get Land Info Priv Child
        /// </summary>
        /// <param name="PRIVATELANDID"></param>
        /// <returns></returns>
        public PAPRelationList GetLandInfoPrivChild(int PRIVATELANDID)
        {
            LandInfoPrivateDAL objLandInfoResDAL = new LandInfoPrivateDAL();
            return objLandInfoResDAL.GetLandInfoPrivChild(PRIVATELANDID);
        }

        /// <summary>
        /// To Get Land Info Priv Spose
        /// </summary>
        /// <param name="PRIVATELANDID"></param>
        /// <returns></returns>
        public PAPRelationList GetLandInfoPrivSpose(int PRIVATELANDID)
        {
            LandInfoPrivateDAL objLandInfoResDAL = new LandInfoPrivateDAL();
            return objLandInfoResDAL.GetLandInfoPrivSpose(PRIVATELANDID);
        }

        /// <summary>
        /// To Add Land Info Private
        /// </summary>
        /// <param name="objPubLF"></param>
        public void AddLandInfoPrivate(PrivateLandInfoBO objPubLF)        
        {
            LandInfoPrivateDAL objLandInfoResDAL = new LandInfoPrivateDAL();
            objLandInfoResDAL.AddLandInfoPriv(objPubLF);
        }
        
        /// <summary>
        /// To Update Land Info Private
        /// </summary>
        /// <param name="objPubLF"></param>
        public void UpdateLandInfoPrivate(PrivateLandInfoBO objPubLF)
        {
            LandInfoPrivateDAL objLandInfoResDAL = new LandInfoPrivateDAL();
            objLandInfoResDAL.UpdateLandInfoPriv(objPubLF);
        }

        /// <summary>
        /// To Insert Update Relations Spose
        /// </summary>
        /// <param name="RelationsSpose"></param>
        public void InsertUpdateRelationsSpose(PAPRelationList RelationsSpose)
        {
            (new LandInfoPrivateDAL()).InsertUpdateRelationsSpose(RelationsSpose);
        }

        /// <summary>
        /// To Insert Update Relations Child
        /// </summary>
        /// <param name="RelationsChild"></param>
        public void InsertUpdateRelationsChild(PAPRelationList RelationsChild)
        {
            (new LandInfoPrivateDAL()).InsertUpdateRelationsChild(RelationsChild);
        }
    }
}