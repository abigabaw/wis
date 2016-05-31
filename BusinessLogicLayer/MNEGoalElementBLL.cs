using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
  public  class MNEGoalElementBLL
    {
      /// <summary>
        /// To Insert MNE Goal Element Details
      /// </summary>
      /// <param name="GoalElementBOObj"></param>
      /// <returns></returns>
        public string InsertMNEGoalElementDetails(MNEGoalElementBOL GoalElementBOObj)
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();

            return MNEGoalElementDAL.InsertMNEGoalElementDetails(GoalElementBOObj);
        
        }

      /// <summary>
        /// To Update Goal Element
      /// </summary>
      /// <param name="MNEGoalElementBOLObj"></param>
      /// <returns></returns>
        public string UpdateGoalElement(MNEGoalElementBOL MNEGoalElementBOLObj)
        {
            MNEGoalElementDAL MNEGoalElementDALObj = new MNEGoalElementDAL();
            return MNEGoalElementDALObj.UpdateMNEGoalElementDetails(MNEGoalElementBOLObj);
        }

      /// <summary>
        /// To Get MNE Goal Element Details by ID
      /// </summary>
      /// <param name="GOALElementID"></param>
      /// <returns></returns>
        public MNEGoalElementBOL GetMNEGoalElementDetailsbyID(int GOALElementID)
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();
            return MNEGoalElementDAL.GetMNEGoalElementDetailsbyID(GOALElementID);

        }

      /// <summary>
        /// To Get All MNE Goal Element Details
      /// </summary>
      /// <returns></returns>
        public MNEGoalElementList GetAllMNEGoalElementDetails()
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();
            return MNEGoalElementDAL.GetAllMNEGoalElementDetails();
 
        }

      /// <summary>
        /// To Delete Goal Element
      /// </summary>
      /// <param name="GoalElementID"></param>
      /// <returns></returns>
        public string DeleteGoalElement(int GoalElementID)
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();
            return MNEGoalElementDAL.DeleteGoalElement(GoalElementID);
 
        }

      /// <summary>
        /// To Obsolete Goal Element
      /// </summary>
      /// <param name="goalID"></param>
      /// <param name="IsDeleted"></param>
      /// <returns></returns>
        public string ObsoleteGoalElement(int goalID, string IsDeleted)
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();
            return MNEGoalElementDAL.ObsoleteGoalElement(goalID, IsDeleted);
 
        }

      /// <summary>
        /// To Load MNE Goal Element
      /// </summary>
      /// <returns></returns>
        public MNEGoalElementList LoadMNEGoalElement()
        {
            MNEGoalElementDAL MNEGoalElementDAL = new MNEGoalElementDAL();
            return MNEGoalElementDAL.LoadMNEGoalElement();
        }
    }
}
