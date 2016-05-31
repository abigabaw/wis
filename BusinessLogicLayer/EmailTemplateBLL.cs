using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class EmailTemplateBLL
    {
        /// <summary>
        /// TO Get All Over due Approvals
        /// </summary>
        /// <returns></returns>
        public EmailTemplateList GetAllOverdueApprovals()
        {
            EmailTemplateDAL EmailTemplateDALObj = new EmailTemplateDAL();
            return EmailTemplateDALObj.GetAllOverdueApprovals();
        }

        /// <summary>
        /// To Get Email Details For Over Due
        /// </summary>
        /// <param name="EMAILTEMPLATECODE"></param>
        /// <returns></returns>
        public EmailTemplateBO GetEmailDetailsForOverDue(string EMAILTEMPLATECODE)
        {
            EmailTemplateDAL EmailTemplateDALObj = new EmailTemplateDAL();
            return EmailTemplateDALObj.GetEmailDetailsForOverDue("H" + EMAILTEMPLATECODE);
        }

        /// <summary>
        /// To Get SMS Details For Over Due
        /// </summary>
        /// <param name="SMSTEMPLATECODE"></param>
        /// <returns></returns>
        public EmailTemplateBO GetSMSDetailsForOverDue(string SMSTEMPLATECODE)
        {
            EmailTemplateDAL EmailTemplateDALObj = new EmailTemplateDAL();
            return EmailTemplateDALObj.GetSMSDetailsForOverDue("H" + SMSTEMPLATECODE);
        }

        /// <summary>
        /// To Get SMS Sender Data For Over Due
        /// </summary>
        /// <returns></returns>
        public WIS_ConfigBO GetSMSSenderDataForOverDue()
        {
            EmailTemplateDAL EmailTemplateDALObj = new EmailTemplateDAL();
            return EmailTemplateDALObj.GetSMSSenderDataForOverDue();
        }
    }
}
