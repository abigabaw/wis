using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class RoleBO
    {
        private int roleID = -1;
        private string roleName = String.Empty;
        private string roledescription = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string isDeleted = string.Empty;

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }


        public int RoleID
        {
            get
            {
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }

        public string RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public string RoleDescription
        {
            get
            {
                return roledescription;
            }
            set
            {
                roledescription = value;
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

        
    }
}