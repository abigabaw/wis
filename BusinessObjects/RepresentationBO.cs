using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class RepresentationBO
    {

        private int representationid = -1;
        private string representationname = String.Empty;
        private string isDeleted = string.Empty;
        private int userID = -1;

        public int RepresentationID
        {
            get
            {
                return representationid;
            }
            set
            {
                representationid = value;
            }
        }

        public string RepresentationName
        {
            get
            {
                return representationname;
            }
            set
            {
                representationname = value;
            }
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

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
            }
        }
    }
}
