using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * 
 * @version		  Public Consultation and Disclosure BO.
 * @package		  Public Consultation and Disclosure. 
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  13-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
namespace WIS_BusinessObjects
{
    public class PublicConsultationBO
    {
        private int cONSULTATIONID;
        private int hHID;
        private string pURPOSEOFMEETING;
        private string iSSUES;
        private string rEMEDIES;
        private string iSDELETED;
        private int cREATEDBY;
        private int uPDATEDBY;
        private DateTime uPDATEDDATE;
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int CONSULTATIONID
        {
            get { return cONSULTATIONID; }
            set { cONSULTATIONID = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }

        public int ProjectID { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Parish { get; set; }
        public string Village { get; set; }
        public string NameOfPerson { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string StakeholdingCategory { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string PurposeOfMeeting
        {
            get { return pURPOSEOFMEETING; }
            set { pURPOSEOFMEETING = value; }
        }
        public string Issues
        {
            get { return iSSUES; }
            set { iSSUES = value; }
        }
        public string Remedies
        {
            get { return rEMEDIES; }
            set { rEMEDIES = value; }
        }
        public int OfficerIncharge { get; set; }
        public string OfficerInchargeName { get; set; }
        public string IsDeleted
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }
        public int CreatedBy
        {
            get { return cREATEDBY; }
            set { cREATEDBY = value; }
        }
        public int UpdatedBy
        {
            get { return uPDATEDBY; }
            set { uPDATEDBY = value; }
        }
        public DateTime UpdatedDate
        {
            get { return uPDATEDDATE; }
            set { uPDATEDDATE = value; }
        }
    }
}
