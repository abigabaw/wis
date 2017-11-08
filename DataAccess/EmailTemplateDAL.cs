using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class EmailTemplateDAL
    {
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public EmailTemplateList GetAllOverdueApprovals()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_NOTIF_OVERDUE_APPROVAL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            EmailTemplateBO EmailTemplateBOobj = null;
            EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

            while (dr.Read())
            {
                EmailTemplateBOobj = new EmailTemplateBO();

                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    EmailTemplateBOobj.Description = (dr.GetString(dr.GetOrdinal("DESCRIPTION")));

                if (!dr.IsDBNull(dr.GetOrdinal("WORKFLOWCODE")))
                    EmailTemplateBOobj.Workflowcode = (dr.GetString(dr.GetOrdinal("WORKFLOWCODE")));

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILID")))
                    EmailTemplateBOobj.EmailID = (dr.GetString(dr.GetOrdinal("EMAILID")));

                if (!dr.IsDBNull(dr.GetOrdinal("cellnumber")))
                    EmailTemplateBOobj.Cellnumber = (dr.GetString(dr.GetOrdinal("cellnumber")));

                if (!dr.IsDBNull(dr.GetOrdinal("triggertype")))
                    EmailTemplateBOobj.Triggertype = (dr.GetString(dr.GetOrdinal("triggertype")));

                if (!dr.IsDBNull(dr.GetOrdinal("DISPLAYNAME")))
                    EmailTemplateBOobj.ApproverUserName = (dr.GetString(dr.GetOrdinal("DISPLAYNAME")));

                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE")))
                    EmailTemplateBOobj.ProjectCode = (dr.GetString(dr.GetOrdinal("PROJECTCODE")));

                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME")))
                    EmailTemplateBOobj.ProjectName = (dr.GetString(dr.GetOrdinal("PROJECTNAME")));

                if (!dr.IsDBNull(dr.GetOrdinal("REQUESTDATE")))
                    EmailTemplateBOobj.Requestdate = Convert.ToString(dr.GetValue(dr.GetOrdinal("REQUESTDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("DUEDATE")))
                    EmailTemplateBOobj.Duedate = Convert.ToString(dr.GetValue(dr.GetOrdinal("DUEDATE")));

                EmailTemplateListobj.Add(EmailTemplateBOobj);
            }
            dr.Close();
            return EmailTemplateListobj;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="EMAILTEMPLATECODE"></param>
        /// <returns></returns>
        public EmailTemplateBO GetEmailDetailsForOverDue(string EMAILTEMPLATECODE)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_EMAILDETAILSFORDUE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EMAILTEMPLATECODE_", EMAILTEMPLATECODE);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            EmailTemplateBO EmailTemplateBOobj = null;

            while (dr.Read())
            {
                EmailTemplateBOobj = new EmailTemplateBO();

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILSUBJECT")))
                    EmailTemplateBOobj.EmailSubject = (dr.GetString(dr.GetOrdinal("EMAILSUBJECT")));

                if (!dr.IsDBNull(dr.GetOrdinal("EMAILBODY")))
                    EmailTemplateBOobj.EmailBody = (dr.GetString(dr.GetOrdinal("EMAILBODY")));

            }
            dr.Close();
            return EmailTemplateBOobj;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="SMSTEMPLATECODE"></param>
        /// <returns></returns>
        public EmailTemplateBO GetSMSDetailsForOverDue(string SMSTEMPLATECODE)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_GET_SMSDETAILSFORDUE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("SMSTEMPLATECODE_", SMSTEMPLATECODE);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            EmailTemplateBO EmailTemplateBOobj = null;

            while (dr.Read())
            {
                EmailTemplateBOobj = new EmailTemplateBO();

                if (!dr.IsDBNull(dr.GetOrdinal("smstext")))
                    EmailTemplateBOobj.Smstext = (dr.GetString(dr.GetOrdinal("smstext")));
            }
            dr.Close();
            return EmailTemplateBOobj;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public WIS_ConfigBO GetSMSSenderDataForOverDue()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
            string proc1 = "USP_SEL_SMS_CONFIG";

            cmd = new SqlCommand(proc1, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WIS_ConfigBO WIS_ConfigSMSBO = null;
            // EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

            while (dr1.Read())
            {
                WIS_ConfigSMSBO = new WIS_ConfigBO();

                if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileNumber")))
                    WIS_ConfigSMSBO.MobileNumber = (dr1.GetString(dr1.GetOrdinal("RegMobileNumber")));

                if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobilePassword")))
                    WIS_ConfigSMSBO.MobilePassword = (dr1.GetString(dr1.GetOrdinal("RegMobilePassword")));

                if (!dr1.IsDBNull(dr1.GetOrdinal("RegSiteUrl")))
                    WIS_ConfigSMSBO.SiteUrl = (dr1.GetString(dr1.GetOrdinal("RegSiteUrl")));

                if (!dr1.IsDBNull(dr1.GetOrdinal("RegMobileStatus")))
                    WIS_ConfigSMSBO.MobileStatus = (dr1.GetString(dr1.GetOrdinal("RegMobileStatus")));
            }
            dr1.Close();
            return WIS_ConfigSMSBO;
        }
    }
}
