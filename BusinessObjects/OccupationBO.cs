using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * 
 * @version		 0.1 Concern BUsinessObject
 * @package		 Concern
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
 * @Created Date 17-April-203
 * @Updated By
 * @Updated Date
 * 
 */
namespace WIS_BusinessObjects
{
    public class OccupationBO
    {
        private int userID = -1;
        private int OccupationID;
        private string OccupationName = String.Empty;
        private string OccupationIsDeleted = String.Empty;
        private string isDeleted;

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
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

        public int OCCUPATIONID
        {
            get
            {
                return OccupationID;
            }
            set
            {
                OccupationID = value;
            }
        }

        public string OCCUPATIONNAME
        {
            get
            {
                return OccupationName;
            }
            set
            {
                OccupationName = value;
            }
        }
        public string OCCUPATIONIsDeleted
        {
            get
            {
                return OccupationIsDeleted;
            }
            set
            {
                OccupationIsDeleted = value;
            }
        }

    }
}