using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class PositionBO
    {
        private int userID = -1;
        private int positionID;
        private string positionName = String.Empty;
        private string positionIsDeleted = String.Empty;

        public int PositionID
        {
            get { return positionID; }
            set { positionID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string PositionName
        {
            get { return positionName; }
            set { positionName = value; }
        }

        public string PositionIsDeleted
        {
            get { return positionIsDeleted; }
            set { positionIsDeleted = value; }
        }
        
    }
}
