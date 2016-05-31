using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class RelationshipBLL
    {
        /// <summary>
        /// To Get ALL Relationship
        /// </summary>
        /// <returns></returns>
        public RELATIONSHIPLIST GetALLRelationship()
        {
            RelationshipDAL objRDAL = new RelationshipDAL();
            return objRDAL.GetALLRelationship();
        }

        /// <summary>
        /// To Obsolete Relationship
        /// </summary>
        /// <param name="RELATIONSHIPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRelationship(int RELATIONSHIPID, string IsDeleted)
        {
            RelationshipDAL objRDAL = new RelationshipDAL();
            return objRDAL.ObsoleteRelationship(RELATIONSHIPID, IsDeleted);
        }

        /// <summary>
        /// To Get Relationship
        /// </summary>
        /// <returns></returns>
        public RELATIONSHIPLIST GetRelationship()
        {
            RelationshipDAL objRDAL = new RelationshipDAL();
            return objRDAL.GetRelationship();
        }

        /// <summary>
        /// To Add Relation
        /// </summary>
        /// <param name="objRel"></param>
        /// <returns></returns>
        public string AddRelation(RelationshipBO objRel)
        {
            RelationshipDAL objRelDAL = new RelationshipDAL();
            return objRelDAL.AddRelation(objRel);
        }

      /// <summary>
        /// To Update Relation
      /// </summary>
      /// <param name="objRel"></param>
      /// <returns></returns>
        public string UpdateRelationship(RelationshipBO objRel)
        {
            RelationshipDAL objRelDAL = new RelationshipDAL();
            return objRelDAL.UpdateRelation(objRel);
        }

        /// <summary>
        /// To Get Relationship By ID
        /// </summary>
        /// <param name="relationshipID"></param>
        /// <returns></returns>
        public RelationshipBO GetRelationshipByID(int relationshipID)
        {
            RelationshipDAL objRelDAL = new RelationshipDAL();
            return objRelDAL.GetRelationshipByID(relationshipID);
        }

        /// <summary>
        /// To Delete Relation
        /// </summary>
        /// <param name="RELATIONSHIPID"></param>
        /// <returns></returns>
        public string DeleteRelation(int RELATIONSHIPID)
        {
            RelationshipDAL objRelDAL = new RelationshipDAL();

            return objRelDAL.DeleteRelation(RELATIONSHIPID);
          

           
        }
    }
}