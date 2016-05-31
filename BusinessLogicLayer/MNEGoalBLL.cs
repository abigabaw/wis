using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public class MNEGoalBLL
    {
      /// <summary>
        /// To Insert MNE Goal Details
      /// </summary>
      /// <param name="GoalNameBOObj"></param>
      /// <returns></returns>
        public string InsertMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.InsertMNEGoalDetails(GoalNameBOObj);
        }

      /// <summary>
        /// To Update MNE Goal Details
      /// </summary>
      /// <param name="GoalNameBOObj"></param>
      /// <returns></returns>
        public string UpdateMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.UpdateMNEGoalDetails(GoalNameBOObj);
        }

      /// <summary>
        /// To Get All Goal Name Details
      /// </summary>
      /// <returns></returns>
        public MNEGoalList GetAllGoalNameDetails()
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.GetAllMNEGoalNameDetails();
        }

      /// <summary>
        /// To Get Active MNE Goal Names
      /// </summary>
      /// <returns></returns>
        public MNEGoalList GetActiveMNEGoalNames()
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.GetActiveMNEGoalNames();
        }

      /// <summary>
        /// To Get MNE Goal Name Details by ID
      /// </summary>
      /// <param name="GOALID"></param>
      /// <returns></returns>
        public MNEGoalBO GetMNEGoalNameDetailsbyID(int GOALID)
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.GetMNEGoalNameDetailsbyID(GOALID);
 
        }

      /// <summary>
        /// To Obsolete Goal Name
      /// </summary>
      /// <param name="goalID"></param>
      /// <param name="IsDeleted"></param>
      /// <returns></returns>
        public string ObsoleteGoalName(int goalID, string IsDeleted)
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.ObsoleteGoalName(goalID, IsDeleted);
        }

      /// <summary>
        /// To Delete Goal Name
      /// </summary>
      /// <param name="GoalID"></param>
      /// <returns></returns>
        public string DeleteGoalName(int GoalID)
        {
            MNEGoalNameDAL MNEGoalNameDALObj = new MNEGoalNameDAL();
            return MNEGoalNameDALObj.DeleteGoalName(GoalID);
        }

    }
}
