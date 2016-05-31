
/**
 * 
 * @version		 0.1 Fence DescriptionBO
 * @package		Fence Description
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Nikitha.S.B
 * @Created Date 19-April-2013
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
    public class FenceDescriptionBO
    {
        private int _FenceID;

        public int FenceID
        {
            get { return _FenceID; }
            set { _FenceID = value; }
        }
        private string _FenceDescription;

        public string FenceDescription
        {
            get { return _FenceDescription; }
            set { _FenceDescription = value; }
        }
        private int _Createdby;

        public int Createdby
        {
            get { return _Createdby; }
            set { _Createdby = value; }
        }
        public string IsDeleted { get; set; }
    }
}