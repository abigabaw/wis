using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public static class UtilBO
    {

        public static string[] sFileExtentions = new string[] { ".TIF", ".DOC", ".CSV", ".PPT", ".DOTX", ".DOCX", ".PDF" };
        public static Boolean CheckExtection(string extention)
        {
            foreach (string fileexe in sFileExtentions)
            {
                if (extention.ToUpper() == fileexe.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        #region for Workflow Code 
        private static string workflowCodeRouteApproval = "RTA";
        private static string workflowChangeRequestApprovalHH = "CR-HH";
        private static string workflowChangeRequestApprovalFL = "CR-FL";
        private static string compensationPrintRequest = "RFPRI";
        private static string workflowNegotatedCodeApproval = "NEG";
        private static string paymentRrequestCode = "PAYRQ";
        private static string grievancesCode = "CRGRA";
        private static string packagePaymentRequestCode = "PKREV";
        private static string cdapBudgetCode = "CDAPB";
        private static string paymentVerificationCode = "PAYVR";
        private static string dataVerificationCode = "DATAV";

        // new Codes For Negotated For Individuals
        private static string workflowNegotatedCodeApprovalCrops = "NEGC";
        private static string workflowNegotatedCodeApprovalLand = "NEGL";
        private static string workflowNegotatedCodeApprovalFixtures = "NEGF";
        private static string workflowNegotatedCodeApprovalRep = "NEGR";
        private static string workflowNegotatedCodeApprovalDamCrops = "NEGD";
        private static string workflowNegotatedCodeApprovalCulPro = "NEGCU";
        #endregion

        private static string datchPaymentStatus = "1";

        private static string dateFormat = "dd-MMM-yyyy";   // Format for displaying the date in UI.
        private static string dateFormatFull = "dd-MMM-yyyy hh:mm tt";   // Format for displaying the date in UI.
        private static string dateFormatDB = "dd-MMM-yyyy";   // Format used to save the date field value in Oracle Database.
        private static string dateFormatDBFull = "dd-MMM-yy hh:mm";   // Format used to save the date field value in Oracle Database.

        public static string PlotReferenceMask { get { return "AAAA/AA999/999"; } }

        public static string DateFormat { get { return dateFormat; } }
        public static string DateFormatFull { get { return dateFormatFull; } }
        public static string DateFormatDB { get { return dateFormatDB; } }
        public static string DateFormatDBFull { get { return dateFormatDBFull; } }
        public static string WorkflowCodeRouteApproval { get { return UtilBO.workflowCodeRouteApproval; } }
        public static string WorkflowChangeRequestApprovalHH { get { return UtilBO.workflowChangeRequestApprovalHH; } }
        public static string WorkflowChangeRequestApprovalFL { get { return UtilBO.workflowChangeRequestApprovalFL; } }
        public static string WorkflowNegotatedCodeApproval { get { return UtilBO.workflowNegotatedCodeApproval; } }
        public static string PaymentRequestCode { get { return UtilBO.paymentRrequestCode; } }
        public static string CompensationPrintRequest { get { return UtilBO.compensationPrintRequest; } }
        public static string GrievancesCode { get { return UtilBO.grievancesCode; } }
        public static string PackagePaymentRequestCode { get { return UtilBO.packagePaymentRequestCode; } }
        public static string CdapBudgetCode { get { return UtilBO.cdapBudgetCode; } }
        public static string PaymentVerificationCode { get { return UtilBO.paymentVerificationCode; } }
        public static string DataVerificationCode { get { return UtilBO.dataVerificationCode; } }
        public static string BatchPaymentStatus { get { return UtilBO.datchPaymentStatus; } }

        //New Codes
        public static string WorkflowNegotatedCodeApprovalCrops { get { return UtilBO.workflowNegotatedCodeApprovalCrops; } }
        public static string WorkflowNegotatedCodeApprovalLand { get { return UtilBO.workflowNegotatedCodeApprovalLand; } }
        public static string WorkflowNegotatedCodeApprovalFixtures { get { return UtilBO.workflowNegotatedCodeApprovalFixtures; } }
        public static string WorkflowNegotatedCodeApprovalRep { get { return UtilBO.workflowNegotatedCodeApprovalRep; } }
        public static string WorkflowNegotatedCodeApprovalDamCrops { get { return UtilBO.workflowNegotatedCodeApprovalDamCrops; } }
        public static string WorkflowNegotatedCodeApprovalCulPro { get { return UtilBO.workflowNegotatedCodeApprovalCulPro; } }

        public static class PrivilegeCode
        {

            #region COMPENSATION
            public static string PRIV_LRA_AFTER = "LRA_AFTER";
            public static string PRIV_PAP_LIST_EXPORT = "PAP_LIST_EXPORT";
            public static string PRIV_PAP_PAYMENT_EXPORT = "PAP_PAYMENT_EXPORT";
            public static string PRIV_TEMP_AUTHORIZATION = "TEMP_AUTHORIZATION";
            public static string PRIV_CONS_PROJECT_EXPENSES = "CONS_PROJECT_EXPENSES";
            public static string PRIV_SOCIO_ECONOMIC = "SOCIO_ECONOMIC";
            public static string PRIV_SURVEY = "SURVEY";
            public static string PRIV_VALUATION = "VALUATION";
            public static string PRIV_GRIEVANCES = "GRIEVANCES";
            public static string PRIV_PACKAGE_DOCUMENTS = "PACKAGE_DOCUMENTS";
            public static string PRIV_BATCH_PAYMENT = "BATCH_PAYMENT";
            public static string PRIV_COMPENSATION_FINANCIALS = "COMPENSATION_FINANCIALS";
            public static string PRIV_PACKAGE_PAYMENT_REQ = "PACKAGE_PAYMENT_REQ";
            public static string PRIV_PACKAGE_CLOSING_INFO = "PACKAGE_CLOSING_INFO";
            public static string PRIV_PAYMENT_PROCESSING_INFO = "PAYMENT_PROCESSING_INFO";
            public static string PRIV_PCDD = "PCDD";
            public static string PRIV_LIVELIHOOD_RESTORATION = "LIVELIHOOD_RESTORATION";
            public static string PRIV_COMMUNITY_DEVELOPMENT = "COMMUNITY_DEVELOPMENT";
            #endregion

            public static string CREATE_PROJECT = "CREATE_PROJECT";
            public static string VIEW_PROJECT = "VIEW_PROJECT";
            public static string COMPARE_PROJECT = "COMPARE_PROJECT";
            public static string MNE_GOAL_EVAL = "MNE_GOAL_EVAL";
            public static string PRIV_IMP_PAP_COORDINATES = "IMP_PAP_COORDINATES";

            public static string PRIV_APPROVALS = "APPROVALS";

            public static string UPLOAD_DOCUMENT = "UPLOAD_DOCUMENT";

            public static string PRIV_SHARED_APPROVALS = "SHARED_APPROVALS";
            public static string PRIV_SHARED_AUTHORIZATION = "SHARED_AUTHORIZATION";

            #region Master
            public static string PRIV_CardType = "CardType";
            public static string PRIV_LivPlanItemMst = "LivPlanItemMst";
            public static string PRIV_FixedCostCentre = "FixedCostCentre";
            public static string PRIV_BANK = "BANK";
            public static string PRIV_EDUCATION = "EDUCATION";
            public static string PRIV_SOCIAL = "SOCIAL";
            public static string PRIV_STRUCTURE = "STRUCTURE";
            public static string PRIV_TENURE = "TENURE";
            public static string PRIV_CROP = "CROP";
            public static string PRIV_POSITION = "POSITION";
            public static string PRIV_FINANCE = "FINANCE";
            public static string PRIV_MODE_OF_PAYMENT = "MODE_OF_PAYMENT";
            public static string PRIV_LAND_RECEIVED_FROM = "LAND_RECEIVED_FROM";
            public static string PRIV_MST_PROJECT = "MST_PROJECT";
            public static string PRIV_USER = "USER";
            public static string PRIV_OPTION_GROUP = "OPTION_GROUP";
            public static string PRIV_WELFARE_INDICATOR = "WELFARE_INDICATOR";
            public static string PRIV_REPRESENTATION = "REPRESENTATION";
            public static string PRIV_HIV_CONTRACTED = "HIV_CONTRACTED";
            public static string PRIV_HEALTH_CENTER = "HEALTH_CENTER";
            public static string PRIV_BUDGET_EST_CATEGORY = "BUDGET_EST_CATEGORY";
            public static string PRIV_CONSULTANT_TYPE = "CONSULTANT_TYPE";
            public static string PRIV_ROLE_PRIVILEGES = "ROLE_PRIVILEGES";
            public static string PRIV_MNE = "M_E";
            public static string PRIV_Location = "Location";
            //new menu Items
            public static string GOUAllowance = "GOUAllowance";
            public static string Grievances_Category = "Grievances_Category";
            public static string Cul_Property_Type = "Cul_Property_Type";

            #endregion

            #region Reports
            public static string PRIV_LRA_AFTERRPT = "LRA_AFTERRPT";
            public static string PRIV_PROJECT_LIST = "PROJECT_LIST";
            public static string PRIV_AUDIT_TRAIL = "AUDIT_TRAIL";
            public static string PRIV_APPROVAL = "APPROVAL";
            public static string PRIV_APPROVAL_DUE = "APPROVAL_DUE";
            public static string PRIV_DAILY_PROJ_STATUS = "DAILY_PROJ_STATUS";
            public static string PRIV_PAP_STATUS_CHART = "PAP_STATUS_CHART";
            public static string PRIV_PROJECT_STATUS_PIECHART = "PROJECT_STATUS_PIECHART";

            public static string PRIV_PUBLIC_CONSULTATION_PROGRESS = "PUBLIC_CONSULTATION_PROGRESS";
            public static string PRIV_RPT_VALUATION = "RPT_VALUATION";
            public static string PRIV_RPT_SURVEY = "RPT_SURVEY";
            public static string PRIV_RPT_COMPENSATION = "RPT_COMPENSATION";
            public static string PRIV_RPT_PROCOMPENSATION = "RPT_PROCOMPENSATION";
            public static string PRIV_STATISTICS = "STATISTICS";
            public static string PRIV_Package_Acceptance = "Package_Acceptance";
            public static string PRIV_FUND = "FUND";
            public static string PRIV_FUNDREQCHNG = "FUNDREQCHNG";
            public static string PRIV_PAP_CATEGORY = "PAP_CATEGORY";
            public static string PRIV_COMPENSATION_BENEFICIARY = "COMPENSATION_BENEFICIARY";
            public static string PRIV_CULTURAL_PROP_MGMT = "CULTURAL_PROP_MGMT";
            public static string PRIV_CHANGE_REPORTS = "CHANGE_REPORTS";

            public static string PRIV_BUDGET_EXPENDITURE = "BUDGET_EXPENDITURE";
            public static string PRIV_MONTHLY_PROJ_EXP = "MONTHLY_PROJ_EXP";

            public static string PRIV_LEGAL_CASE_TRACKING = "LEGAL_CASE_TRACKING";
            public static string PRIV_DISPUTE_RES_TRACKING = "DISPUTE_RES_TRACKING";

            public static string PRIV_CDAP_LIV_SUPPORT = "CDAP_LIV_SUPPORT";

            public static string PRIV_M_E = "M_E";
            public static string PRIV_PAP_Child_dCount = "PAP_Child_dCount";
            public static string PRIV_Personal_Identification = "Personal_Identification";
            public static string PRIV_OPTION_SUMMARY = "OPTION_SUMMARY";

            #endregion

            #region enable hover menu in home
            public static string PRIV_HOME = "HOME";

            #endregion


        }

        public static string CurrencyFormat(decimal numberToFormat)
        {
            string formattedVal = String.Format("{0:C0}", numberToFormat);
            formattedVal = formattedVal.Replace("$", "");
            formattedVal = formattedVal.Replace("Rs.", "");

            return formattedVal;
        }
    }
}
