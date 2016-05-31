/**
 * 
 * @version      :    Version Number Page Name
 * @package      :    FloorType/Structure
 * @copyright    :    Copyright © 2013 - All rights reserved.
 * @author       :    Iranna Shirol
 * @Created Date :    19 Apr 2013 
 * @Updated By   :
 * @Updated Date : 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class FloorTypeBO
    {
        public int FloorTypeID
        {
            get;
            set;
        }

        public string FloorTypeName
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public string IsDeleted
        {
            get;
            set;
        }
    }
}