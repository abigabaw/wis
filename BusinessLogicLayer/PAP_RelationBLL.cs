using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_RelationBLL
    {
        /// <summary>
        /// To Add Relation
        /// </summary>
        /// <param name="objRelation"></param>
        /// <returns></returns>
        public string AddRelation(PAP_RelationBO objRelation)
        {
            return (new PAP_RelationDAL()).AddRelation(objRelation);
        }

        /// <summary>
        /// To Update Relation
        /// </summary>
        /// <param name="objRelation"></param>
        /// <returns></returns>
        public string UpdateRelation(PAP_RelationBO objRelation)
        {
            return  (new PAP_RelationDAL()).UpdateRelation(objRelation);
        }

        /// <summary>
        /// To Delete Relation
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        public string DeleteRelation(int relationID)
        {
            return (new PAP_RelationDAL()).DeleteRelation(relationID);
        }

        /// <summary>
        /// To Get Relations
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="holderTypeID"></param>
        /// <returns></returns>
        public PAPRelationList GetRelations(int householdID, int holderTypeID)
        {
            return (new PAP_RelationDAL()).GetRelations(householdID, holderTypeID);
        }

        /// <summary>
        /// To Get Relation By ID
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        public PAP_RelationBO GetRelationByID(int relationID)
        {
            return (new PAP_RelationDAL()).GetRelationByID(relationID);
        }

        /// <summary>
        /// To Get Holder Types
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="holderTypeID"></param>
        /// <returns></returns>
        public HolderTypeList GetHolderTypes(int householdID, int holderTypeID)
        {
            return (new PAP_RelationDAL()).GetHolderTypes(householdID, holderTypeID);
        }

        /// <summary>
        /// To Add Affected Land User
        /// </summary>
        /// <param name="objAffLandUser"></param>
        /// <returns></returns>
        public string AddAffectedLandUser(AffectedLandUserBO objAffLandUser)
        {
            return (new PAP_RelationDAL()).AddAffectedLandUser(objAffLandUser);
        }

        /// <summary>
        /// To Update Affected Land User
        /// </summary>
        /// <param name="objAffLandUser"></param>
        /// <returns></returns>
        public string UpdateAffectedLandUser(AffectedLandUserBO objAffLandUser)
        {
            return (new PAP_RelationDAL()).UpdateAffectedLandUser(objAffLandUser);
        }

        /// <summary>
        /// To Delete Affected Land User
        /// </summary>
        /// <param name="landUserID"></param>
        /// <param name="updatedBy"></param>
        public void DeleteAffectedLandUser(int landUserID, int updatedBy)
        {
            (new PAP_RelationDAL()).DeleteAffectedLandUser(landUserID, updatedBy);
        }

        /// <summary>
        /// To Get Affected Land Users
        /// </summary>
        /// <param name="landUserID"></param>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_AffectedLandUserList GetAffectedLandUsers(int landUserID, int householdID)
        {
            return (new PAP_RelationDAL()).GetAffectedLandUsers(landUserID, householdID);
        }

        /// <summary>
        /// To Get PAP Service Masters
        /// </summary>
        /// <returns></returns>
        public PAPServiceMasterList GetPAPServiceMasters()
        {
            return (new PAP_RelationDAL()).GetPAPServiceMasters();
        }

        /// <summary>
        /// To Add PAP Service
        /// </summary>
        /// <param name="PAPServices"></param>
        public void AddPAPService(PAPServicesList PAPServices)
        {
            (new PAP_RelationDAL()).AddPAPService(PAPServices);
        }

        /// <summary>
        /// To Get PAP Services
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAPServicesList GetPAPServices(int householdID)
        {
            return (new PAP_RelationDAL()).GetPAPServices(householdID);
        }
    }
}