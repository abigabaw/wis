using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  WIS_BusinessObjects
{
    public class GraveFinishBO
    {
        private int graveFinishID = -1;
        private string graveFinish = String.Empty;
        private int createdBy = -1;
        public int updatedBy = -1;
        public string isDeleted = "False";

        public int GraveFinishID
        {
            get
            {
                return graveFinishID;
            }
            set
            {
                graveFinishID = value;
            }
        }

        public string GraveFinishType
        {
            get
            {
                return graveFinish;
            }
            set
            {
                graveFinish = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }
        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }
        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }
    }
}