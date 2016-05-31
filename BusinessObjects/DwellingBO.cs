/**
 * 
 * @version		 0.1 Dwelling Business Object
 * @package		 Dwelling
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Nikitha.S.B
 * @Created Date 17-April-2013
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
    public class DwellingBO
    {
        private int _DwellingID;

        public int DwellingID
        {
            get { return _DwellingID; }
            set { _DwellingID = value; }
        }
        private string _DwellingType;

        public string DwellingType
        {
            get { return _DwellingType; }
            set { _DwellingType = value; }
        }

        public string IsDeleted { get; set; }

        private int _Createdby;

        public int Createdby
        {
            get { return _Createdby; }
            set { _Createdby = value; }
        }
    }
}