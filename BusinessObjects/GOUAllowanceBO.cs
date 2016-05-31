using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * 
 * @version		 0.1 GOU Allowance Business Object
 * @package		GOU Allowance
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Nikitha.S.B
 * @Created Date 08-oct-2013
 * @Updated By
 * @Updated Date
 * 
 */

namespace WIS_BusinessObjects
{
   public  class GOUAllowanceBO
    {
        private string gOUAllowanceCategory;

        public string GOUAllowanceCategory
        {
            get { return gOUAllowanceCategory; }
            set { gOUAllowanceCategory = value; }
        }


        private decimal gOUAllowanceValue;

        public decimal GOUAllowanceValue
        {
            get { return gOUAllowanceValue; }
            set { gOUAllowanceValue = value; }
        }
        private int createdby;

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }
        private DateTime createddate;

        public DateTime Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }
        private bool isdeleted;

        public bool Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }
        private int gOUALLOWANCECATEGORYID;

        public int GOUALLOWANCECATEGORYID
        {
            get { return gOUALLOWANCECATEGORYID; }
            set { gOUALLOWANCECATEGORYID = value; }
        }


    }
}
