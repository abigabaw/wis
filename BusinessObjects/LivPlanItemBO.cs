using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class LivPlanItemBO
    {
        private int userID = -1;
        private int livPlanItemID;
        private string livPlanItemName = String.Empty;
        private string livPlanItemIsDeleted = String.Empty;
        private string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }


        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public int LivPlanItemID
        {
            get
            {
                return livPlanItemID;
            }
            set
            {
                livPlanItemID = value;
            }
        }

        public string LivPlanItemName
        {
            get
            {
                return livPlanItemName;
            }
            set
            {
                livPlanItemName = value;
            }
        }
        public string LivPlanItemIsDeleted
        {
            get
            {
                return livPlanItemIsDeleted;
            }
            set
            {
                livPlanItemIsDeleted = value;
            }
        }

    }
}
