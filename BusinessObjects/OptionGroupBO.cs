using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class OptionGroupBO
    {
        int optionGroup;

        public int OptionGroup
        {
            get { return optionGroup; }
            set { optionGroup = value; }
        }
        int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        string optiongrpName;

        public string OptiongrpName
        {
            get { return optiongrpName; }
            set { optiongrpName = value; }
        }
        string statusname;

        public string Statusname
        {
            get { return statusname; }
            set { statusname = value; }
        }
        private int optionGroupID = -1;
        private string optionGroupName = String.Empty;
        private string isDeleted = string.Empty;
        private int userID = -1;

        public int OptionGroupID
        {
            get
            {
                return optionGroupID;
            }
            set
            {
                optionGroupID = value;
            }
        }

        public string OptionGroupName
        {
            get
            {
                return optionGroupName;
            }
            set
            {
                optionGroupName = value;
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
