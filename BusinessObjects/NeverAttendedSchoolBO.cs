/**
 * 
 * @version          Version Number Page Name
 * @package          Never Attended School
 * @copyright        Copyright ©Ktwo Technology Solutions 2013 - All rights reserved.
 * @author          Akshay Kulkarni
 * @Created Date    17/04/2013   
 * @Updated By       
 * @Updated Date
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class NeverAttendedSchoolBO
    {
        private int _NVR_ATT_SCH_REASONID;

        public int NVR_ATT_SCH_REASONID
        {
            get { return _NVR_ATT_SCH_REASONID; }
            set { _NVR_ATT_SCH_REASONID = value; }
        }
        private string _NVR_ATT_SCH_REASON;

        public string NVR_ATT_SCH_REASON
        {
            get { return _NVR_ATT_SCH_REASON; }
            set { _NVR_ATT_SCH_REASON = value; }
        }
        private string _DESCRIPTION;

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private string _IsDeleted;

        public string IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private int _UpdatedBy;

        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }
    }
}