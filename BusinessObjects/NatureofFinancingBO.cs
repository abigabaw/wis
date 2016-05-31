using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * 
 * @version		Nature of Financing UI code 
 * @package		 Nature of Financing
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  14-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
namespace WIS_BusinessObjects
{
   public class NatureofFinancingBO
    {
        private int fINANCENATUREID = -1;

        public int FINANCENATUREID
        {
            get { return fINANCENATUREID; }
            set { fINANCENATUREID = value; }
        }
        private string fINANCENATURE = string.Empty;

        public string FINANCENATURE
        {
            get { return fINANCENATURE; }
            set { fINANCENATURE = value; }
        }
        private string iSDELETED = string.Empty;

        public string ISDELETED
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }
        private int cREATEDBY = -1;

        public int CREATEDBY
        {
            get { return cREATEDBY; }
            set { cREATEDBY = value; }
        }
        private DateTime cREATEDDATE;

        public DateTime CREATEDDATE
        {
            get { return cREATEDDATE; }
            set { cREATEDDATE = value; }
        }
        private int uPDATEDBY = -1;

        public int UPDATEDBY
        {
            get { return uPDATEDBY; }
            set { uPDATEDBY = value; }
        }
        private DateTime uPDATEDDATE;

        public DateTime UPDATEDDATE
        {
            get { return uPDATEDDATE; }
            set { uPDATEDDATE = value; }
        }
    }
}
