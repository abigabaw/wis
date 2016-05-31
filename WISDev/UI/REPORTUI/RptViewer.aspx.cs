using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class RptViewer : System.Web.UI.Page
    {
        ConnectionInfo ConnInfo = new ConnectionInfo();
        ReportDocument myRpt = null;
        string RPT_SOURCE = "";

        /// <summary>
        /// To unload all controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Unload(object sender, EventArgs e)
        {
            myRpt.Close();
            myRpt.Dispose();
            GC.Collect();

        }
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Session == null)
            {
                string message = "Your Session has expired, This window will close";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                Response.Write("<script>window.close();</" + "script>");
                Response.End();
            }
            else
            {
                ConnInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_ServerName");
                ConnInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_DatabaseName");
                ConnInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_UserID");
                ConnInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Password");
                RPT_SOURCE = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Source");

                int HHID = 0;  //Convert.ToInt32(Request.QueryString["HHID"]);

                if (!Int32.TryParse(Request.QueryString["HHID"], out HHID))
                    HHID = 0;

                string rptCode = Request.QueryString["rptCode"];

                //Edwin Baguma:
                string workflowCode = Request.QueryString["WorkflowCode"];

                switch (workflowCode)
                {
                    case "PKREV":
                        LoadPackageStatus();
                        break;

                    case "PAYRQ":
                        LoadBatchStatusReport();
                        break;

                    case "PAYVR":
                        LoadPaymentStatusReport();
                        break;

                    case "PAYRQCMTS":
                        LoadBatchComments();
                        break;

                    case "BATCHPRINT":
                        LoadBatchPrintout();
                        break;

                    case "Legacy":
                        LoadLegacyReports();
                        break;

                }
                //Edwin: End


                switch (rptCode)
                {
                    case "PIR":
                        PersonalIdentificationReport(HHID);
                        break;

                    case "PAPCC":
                        PAPChildCountReport();
                        break;

                    case "PROCOMP":
                        LoadProCompensationReport();
                        break;

                    case "VAL":
                        LoadValuationReport();
                        break;

                    case "SUR":
                        LoadSurveyReport();
                        break;

                    case "PUC":
                        LoadPublicConsultationReport();
                        break;

                    case "COM":
                        LoadCompensationReport();
                        break;

                    case "LCT":
                        LoadLegalCaseTrackingReport();
                        break;

                    case "STA":
                        LoadStatisticsReport();
                        break;

                    case "DPS":
                        LoadDailyProjectsStatusReport();
                        break;

                    case "MPS":
                        LoadMonthlyProjectExpenditureReport();
                        break;

                    case "LHS":
                        LoadLivelihoodSupportReport();
                        break;

                    case "CUP":
                        LoadCulturalPropertyReport();
                        break;

                    case "DRT":
                    case "DRTGRI":
                        LoadDisputeRsolutionTrackingReport();
                        break;

                    case "CMPDT":
                        LoadComparisionDataReport();
                        break;

                    case "BUDJ":
                        LoadBudjetEstimation();
                        break;

                    case "PR":
                        LoadProjectReport();
                        break;

                    case "AUDT":
                        LoadAuditTrial();
                        break;

                    case "APPR":
                        LoadApprovalsReport();
                        break;

                    case "RIPRF":
                    case "RIPRC":
                        LoadFundProgressRequest(rptCode);
                        break;
                    case "GPC":
                    case "GPCOP":
                        LoadGeneralPapCategory(rptCode);
                        break;

                    case "ACTCOUNT":
                    case "Detail":
                        LoadAcceptanceCount(rptCode);
                        break;

                    case "MNE":
                        LoadMonitoringResultandResolution();
                        break;

                    case "BASTS":
                        LoadBatchStatus();
                        break;

                    case "OPTS":
                        LoadSummarys(rptCode);
                        break;

                    case "OPTST":
                        LoadSummarys(rptCode);
                        break;

                    case "OPTSG":
                        LoadSummarys(rptCode);
                        break;

                    case "OPTSR":
                        LoadSummarys(rptCode);
                        break;

                    case "OPTSL":
                        LoadSummarys(rptCode);
                        break;

                    case "OPTD":
                        LoadOptionDetails();
                        break;

                    case "PKP":
                        LoadPackagePrinting();
                        break;

                    case "CCD":
                        LoadCashCompensationDelivered();
                        break;

                    case "HID":
                        LoadHouseInKindDelivered();
                        break;

                    case "COB":
                        LoadCompensationBeneficiaryReport();
                        break;

                    case "PC":
                        LoadPackagesClosed();
                        break;

                    case "PLAFTR":
                        LoadPAPLAFTER();
                        break;

                    case "CCP":
                        LoadCashCompenPending();
                        break;

                    case "LKP":
                        LoadLandInKindPending();
                        break;


                    case "HIP":
                        LoadHouseInKindPending();
                        break;

                    case "CFD":
                        LoadCompnFinancialDetails();
                        break;

                    case "CFBS":
                        LoadCompnFinancialBatchSummary();
                        break;

                    case "LOSNEW":
                    case "NOPNEW":
                    case "NOCNEW":
                    case "ILDNEW":
                    case "SOPNEW":
                    case "PDPNEW":
                    case "IHPDPNEW":
                    case "ALSNEW":
                    case "LAPNEW":
                        LoadLegacyReportsSummary();
                        break;


                    case "LKD":
                        LoadLandInKindDelivered();
                        break;

                    case "PROJEXP":
                        LoadProjectExpense();
                        break;

                    case "FUNDREQCHNG":
                        LoadFundRequestChange();
                        break;
                    case "QESTSOCIO":
                        SocioEconomicQuestionnaire();

                        if (ViewState["SOCIO_ECONOMIC_SERIAL_NO_INCR"] == null)
                        {
                            UpdateSerialNumber("SOCIO_ECONOMIC_SERIAL_NO");
                            ViewState["SOCIO_ECONOMIC_SERIAL_NO_INCR"] = "Y";
                        }

                        break;
                    case "QESTSUR":
                        SurveyQuestionnaire();

                        if (ViewState["SURVEY_SERIAL_NO_INCR"] == null)
                        {
                            UpdateSerialNumber("SURVEY_SERIAL_NO");
                            ViewState["SURVEY_SERIAL_NO_INCR"] = "Y";
                        }

                        break;
                    case "QESTVALN":
                        ValuationQuestionnaire();

                        if (ViewState["VALUATION_SERIAL_NO_INCR"] == null)
                        {
                            UpdateSerialNumber("VALUATION_SERIAL_NO");
                            ViewState["VALUATION_SERIAL_NO_INCR"] = "Y";
                        }

                        break;

                    case "PCHART":
                    case "LCHART":
                        PAPStatus_Chart(rptCode);
                        break;

                    case "PIECHART":
                        ProjectStatus_PieChart();
                        break;

                }
            }
        }

        private void LoadBatchPrintout()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string BatchNo = Request.QueryString["BatchNo"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "RPT_BATCH_PRINTOUT.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramBatchNo = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramBatchNo.Name = "BATCHNO_";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramBatchNoVal.Value = Convert.ToInt32(BatchNo);
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramBatchNo);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();
        }


        //Edwin Baguma: 9/11/2015 Added to Load Legacy Reports
        private void LoadLegacyReports()
        {
            int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            int reportID = Convert.ToInt32(Request.QueryString["reportID"]);
            string ReportName = "";
            ProjectBLL ReportObj = new ProjectBLL();
            ReportName = ReportObj.GetLegacyReportByID(reportID);

            //Proceed to load the report
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + ReportName);

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramPrintedBy = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramPrintedBy.Name = "P_Printedby";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramPrintedByVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramPrintedByVal.Value = Session["userName"].ToString();
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramPrintedBy.CurrentValues.Add(paramPrintedByVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramPrintedBy);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();

        }

        public static string GetVirtualPath(string PhysicalPath)
        {
            return @"~\" + PhysicalPath.Replace(HostingEnvironment.ApplicationPhysicalPath, string.Empty);
                // PhysicalPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, string.Empty);
        }

        private void LoadBatchComments()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string BatchNo = Request.QueryString["BatchNo"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "RPT_PAYRQ_COMMENTS.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramBatchNo = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramBatchNo.Name = "BATCHNO_";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramBatchNoVal.Value = Convert.ToInt32(BatchNo);
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramBatchNo);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();
        }

        private void LoadPaymentStatusReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string HHID = Request.QueryString["HHID"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "RPT_PAYVR_STATUS.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramHHID.Name = "HHID_";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(HHID);
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();
        }

        private void LoadPAPLAFTER()
        {
            //string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LRA_AFTERRPT) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "LIVELIHOODAFTER.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHID = new ParameterField();
            ParameterField paramHHID_COMN = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();


            paramProjectId.Name = "PROJECTID_";
            paramHHID.Name = "HHID_";
            paramHHID_COMN.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectstartdate.Name = "FROMDATE_";
            paramProjectEnddate.Name = "TODATE_";

            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_COMNVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();


            paramProjectIdVal.Value = Session["PROJECT_ID"].ToString();
            paramHHIDVal.Value = Session["HH_ID"].ToString();
            paramHHID_COMNVal.Value = Session["HH_ID"].ToString();
            paramPrintedbyVal.Value = Session["userName"].ToString();
            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;


            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_COMN.CurrentValues.Add(paramHHID_COMNVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);

            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHID_COMN);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);

            CrystalReportViewer1.RefreshReport();

        }
        /// <summary>
        /// Loads ProCompensationReport
        /// </summary>
        private void LoadProCompensationReport()
        {
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_RPT_PROCOMPENSATION) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "ProCompensation.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramHHID_COMN = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramHHID.Name = "HHID_";
            paramHHID_COMN.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHID_COMNVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramHHID_COMNVal.Value = Session["HH_ID"].ToString();
            paramHHIDVal.Value = Session["HH_ID"].ToString();
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectIdVal.Value = Session["PROJECT_ID"].ToString();

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_COMN.CurrentValues.Add(paramHHID_COMNVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHID_COMN);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Loads ProjectStatus_PieChart
        /// </summary>
        private void ProjectStatus_PieChart()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstatus = Request.QueryString["Projectstatus"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PAP_STATUS_CHART) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "PAPStatusPieChart.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHIdComm = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectStatus = new ParameterField();
            ParameterField paramFromDate = new ParameterField();
            ParameterField paramToDate = new ParameterField();



            paramProjectId.Name = "PROJECTID_";
            paramHHIdComm.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectStatus.Name = "PROJECTSTATUS_";
            paramFromDate.Name = "FROMDATE_";
            paramToDate.Name = "TODATE_";



            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdCommVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectStatusVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramToDateVal = new ParameterDiscreteValue();


            paramProjectIdVal.Value = null;
            paramHHIdCommVal.Value = null;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectStatusVal.Value = Projectstatus.Trim();
            //if (Projectstatus.Trim() != "")
            //    paramProjectStatusVal.Value = Projectstatus;
            //else
            //    paramProjectStatusVal.Value = null;
            if (Projectstartdate.Length > 0)
                paramFromDateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramFromDateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramToDateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramToDateVal.Value = null;

            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHIdComm.CurrentValues.Add(paramHHIdCommVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectStatus.CurrentValues.Add(paramProjectStatusVal);
            paramFromDate.CurrentValues.Add(paramFromDateVal);
            paramToDate.CurrentValues.Add(paramToDateVal);


            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHIdComm);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectStatus);
            ParamFields.Add(paramFromDate);
            ParamFields.Add(paramToDate);

            CrystalReportViewer1.RefreshReport();

        }
        /// <summary>
        /// Loads PAPStatus_Chart
        /// </summary>
        private void PAPStatus_Chart(string rptCode)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PROJECT_STATUS_PIECHART) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            // myRpt.Load(RPT_SOURCE + "ProjectwisePAPStatusChart.rpt");

            if (rptCode == "PCHART")
                myRpt.Load(RPT_SOURCE + "ProjectwisePAPStatusChart.rpt");
            else
                myRpt.Load(RPT_SOURCE + "Expense_LineChart.rpt");


            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;


            ParameterField paramProjectIdIn = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHIdComm = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramFromDate = new ParameterField();
            ParameterField paramToDate = new ParameterField();




            paramProjectIdIn.Name = "PROJECTIDIN";
            paramProjectId.Name = "PROJECTID_";
            paramHHIdComm.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramFromDate.Name = "FROMDATE_";
            paramToDate.Name = "TODATE_";



            ParameterDiscreteValue paramProjectIdInVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdCommVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramToDateVal = new ParameterDiscreteValue();


            paramProjectIdInVal.Value = ProjectID;
            paramProjectIdVal.Value = ProjectID;
            paramHHIdCommVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            if (Projectstartdate.Length > 0)
                paramFromDateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramFromDateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramToDateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramToDateVal.Value = null;


            paramProjectIdIn.CurrentValues.Add(paramProjectIdInVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHIdComm.CurrentValues.Add(paramHHIdCommVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramFromDate.CurrentValues.Add(paramFromDateVal);
            paramToDate.CurrentValues.Add(paramToDateVal);



            ParamFields.Add(paramProjectIdIn);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHIdComm);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramFromDate);
            ParamFields.Add(paramToDate);


            CrystalReportViewer1.RefreshReport();

        }
        /// <summary>
        /// Loads LegacyReportsSummary
        /// </summary>
        private void LoadLegacyReportsSummary()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            //string ProjectNmae = Request.QueryString["ProjectNmae"];
            //string Projectstartdate = Request.QueryString["opsStartDate"];
            //string ProjectEnddate = Request.QueryString["opsEndDate"];
            //string BatchNo = Request.QueryString["BatchNo"];
            //string Hhid = Request.QueryString["Hhid"];
            //string Status = Request.QueryString["Status"];
            string rpttype = Request.QueryString["rptCode"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();
            if (rpttype == "LOSNEW")
            {
                myRpt.Load(RPT_SOURCE + "Land OwnershipStatistics.rpt");
            }
            else if (rpttype == "NOPNEW")
            {
                myRpt.Load(RPT_SOURCE + "Number of packages printed per Options.rpt");
            }
            else if (rpttype == "NOCNEW")
            {
                myRpt.Load(RPT_SOURCE + "Number of closed cases per village.rpt");
            }
            else if (rpttype == "ILDNEW")
            {
                myRpt.Load(RPT_SOURCE + "Compensation Land Delivered.rpt");
            }
            else if (rpttype == "SOPNEW")
            {
                myRpt.Load(RPT_SOURCE + "Compensation Cash Pending.rpt");
            }
            else if (rpttype == "PDPNEW")
            {
                myRpt.Load(RPT_SOURCE + "PDPINKINDLANDSTATUS.rpt");
            }
            else if (rpttype == "IHPDPNEW")
            {
                myRpt.Load(RPT_SOURCE + "PDPINKINDHOUSESTATUS.rpt");
            }
            else if (rpttype == "ALSNEW")
            {
                myRpt.Load(RPT_SOURCE + "AffectedLandsizeperresidencestatus.rpt");
            }
            else if (rpttype == "LAPNEW")
            {
                myRpt.Load(RPT_SOURCE + "LandAcquiredperresidencestatus.rpt");
            }

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            //ParameterField paramProjectstartdate = new ParameterField();
            //ParameterField paramProjectEnddate = new ParameterField();
            //ParameterField paramProjectName = new ParameterField();
            //ParameterField paramBatchNo = new ParameterField();
            //ParameterField paramHhidBatch = new ParameterField();
            //ParameterField paramStatus = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            //paramProjectstartdate.Name = "PROJECTFROMTDATE_";
            //paramProjectEnddate.Name = "PROJECTTODATE_";
            //paramProjectName.Name = "ProjectName_";
            //paramBatchNo.Name = "BATCHNO_";
            //paramHhidBatch.Name = "HHID_";
            //paramStatus.Name = "Status";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramHhidBatchVal = new ParameterDiscreteValue();
            //ParameterDiscreteValue paramStatusVal = new ParameterDiscreteValue();

            //if (Projectstartdate.Length > 0)
            //    paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            //else
            //    paramProjectstartdateVal.Value = null;
            //if (ProjectEnddate.Length > 0)
            //    paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            //else
            //    paramProjectEnddateVal.Value = null;


            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = null;

            //paramProjectNameVal.Value = ProjectNmae;
            //paramStatusVal.Value = Status;

            //if (BatchNo.Trim().Length > 0 && Status.Trim() == "Detail")
            //    paramBatchNoVal.Value = Convert.ToInt32(BatchNo);
            //else
            //    paramBatchNoVal.Value = null;


            //if (Hhid.Trim().Length > 0 && Status.Trim() == "Detail")
            //    paramHhidBatchVal.Value = Convert.ToInt32(Hhid);
            //else
            //    paramHhidBatchVal.Value = null;

            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            //paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            //paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);
            //paramProjectName.CurrentValues.Add(paramProjectNameVal);
            //paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            //paramHhidBatch.CurrentValues.Add(paramHhidBatchVal);
            //paramStatus.CurrentValues.Add(paramStatusVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramPrintedby);
            //ParamFields.Add(paramProjectstartdate);
            //ParamFields.Add(paramProjectEnddate);
            //ParamFields.Add(paramProjectName);
            //ParamFields.Add(paramBatchNo);
            //ParamFields.Add(paramHhidBatch);
            //ParamFields.Add(paramStatus);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Loads FundRequestChange
        /// </summary>
        private void LoadFundRequestChange()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FUNDREQCHNG) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();
            myRpt.Load(RPT_SOURCE + "FundRequestChange.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramDistrict.Name = "district_";
            paramCounty.Name = "county_";
            paramSubCounty.Name = "subcounty_";
            paramParish.Name = "parish_";
            paramVillage.Name = "village_";
            paramPapName.Name = "papname_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramProjectVal.Value = ProjectID;

            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// loads PersonalIdentificationReport
        /// </summary>
        /// <param name="householdID"></param>
        private void PersonalIdentificationReport(int householdID)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Clan = Request.QueryString["Clan"];
            string Tribe = Request.QueryString["Tribe"];
            // string Religion = Request.QueryString["Religion"];
            // string OptionGroup = Request.QueryString["OptionGroup"];
            string Gender = Request.QueryString["Gender"];
            string PAPName = Request.QueryString["PAPName"];
            int Religion = 0;
            if (!Int32.TryParse(Request.QueryString["Religion"], out Religion))
                Religion = 0;

            int OptionGroup = 0;
            if (!Int32.TryParse(Request.QueryString["OptionGroup"], out OptionGroup))
                OptionGroup = 0;

            string optionGroupName = Request.QueryString["optionGroupName"];
            string religionName = Request.QueryString["religionName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Personal_Identification) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "PersonalIdentificationReport.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramClan = new ParameterField();
            ParameterField paramTribe = new ParameterField();
            ParameterField paramReligion = new ParameterField();
            ParameterField paramOptionGroup = new ParameterField();
            //  ParameterField paramHHID = new ParameterField();
            ParameterField paramPAPName = new ParameterField();
            ParameterField paramGender = new ParameterField();

            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHIdComm = new ParameterField();
            ParameterField paramreligionname = new ParameterField();
            ParameterField paramoptiongroupname = new ParameterField();

            paramClan.Name = "CLAN_";
            paramTribe.Name = "TRIBE_";
            paramReligion.Name = "RELIGION_";
            paramOptionGroup.Name = "OPTIONGROUP_";
            // paramHHID.Name = "HHID_";
            paramPAPName.Name = "PAPNAME_";
            paramGender.Name = "GENDER_";

            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";
            paramHHIdComm.Name = "HHID_COMN";
            paramreligionname.Name = "RELIGIONNAME";
            paramoptiongroupname.Name = "OPTIONGROUPNAME";

            ParameterDiscreteValue paramClanVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramTribeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramReligionVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramOptionGroupVal = new ParameterDiscreteValue();
            // ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPAPNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramGenderVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdCommVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramreligionnameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramoptiongroupnameVal = new ParameterDiscreteValue();

            paramClanVal.Value = Clan;
            paramTribeVal.Value = Tribe;
            paramReligionVal.Value = Religion;
            paramOptionGroupVal.Value = OptionGroup;
            //paramHHIDVal.Value = householdID;
            paramPAPNameVal.Value = PAPName;
            paramGenderVal.Value = Gender;
            paramProjectIdVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIdCommVal.Value = householdID;
            paramreligionnameVal.Value = religionName;
            paramoptiongroupnameVal.Value = optionGroupName;



            paramClan.CurrentValues.Add(paramClanVal);
            paramTribe.CurrentValues.Add(paramTribeVal);
            paramReligion.CurrentValues.Add(paramReligionVal);
            paramOptionGroup.CurrentValues.Add(paramOptionGroupVal);
            //  paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPAPName.CurrentValues.Add(paramPAPNameVal);
            paramGender.CurrentValues.Add(paramGenderVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHIdComm.CurrentValues.Add(paramHHIdCommVal);
            paramoptiongroupname.CurrentValues.Add(paramoptiongroupnameVal);
            paramreligionname.CurrentValues.Add(paramreligionnameVal);

            ParamFields.Add(paramClan);
            ParamFields.Add(paramTribe);
            ParamFields.Add(paramReligion);
            ParamFields.Add(paramOptionGroup);
            //ParamFields.Add(paramHHID);
            ParamFields.Add(paramPAPName);
            ParamFields.Add(paramGender);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHIdComm);
            ParamFields.Add(paramoptiongroupname);
            ParamFields.Add(paramreligionname);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// loads PAPChildCountReport
        /// </summary>
        private void PAPChildCountReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PAP_Child_dCount) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "PAPChildrenCount.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            paramProjectVal.Value = ProjectID;

            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load ProjectExpense report
        /// </summary>
        private void LoadProjectExpense()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string ProjectFromdate = Request.QueryString["ProjectFromdate"];
            string ProjectTodate = Request.QueryString["ProjectTodate"];
            string Accountcode = Request.QueryString["accontcode"];

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "ProjectExpenseReport.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramFromDate = new ParameterField();
            ParameterField paramToDate = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramAccountcode = new ParameterField();




            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramFromDate.Name = "FROMDATE_";
            paramToDate.Name = "TODATE_";
            paramProjectId.Name = "PROJECTID_";
            paramAccountcode.Name = "ACCOUNTCODE_";


            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramToDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramAccoundcodeVal = new ParameterDiscreteValue();


            if (ProjectFromdate.Length > 0)
                paramFromDateVal.Value = Convert.ToDateTime(ProjectFromdate).ToString(UtilBO.DateFormatDB);
            else
                paramFromDateVal.Value = null;

            if (ProjectTodate.Length > 0)
                paramToDateVal.Value = Convert.ToDateTime(ProjectTodate).ToString(UtilBO.DateFormatDB);
            else
                paramToDateVal.Value = null;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectIdVal.Value = ProjectID;

            paramAccoundcodeVal.Value = Accountcode;

            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramFromDate.CurrentValues.Add(paramFromDateVal);
            paramToDate.CurrentValues.Add(paramToDateVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramAccountcode.CurrentValues.Add(paramAccoundcodeVal);

            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramFromDate);
            ParamFields.Add(paramToDate);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramAccountcode);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load LandInKindDelivered report
        /// </summary>
        private void LoadLandInKindDelivered()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "LandInkindDelivered.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load CompnFinancialBatchSummary report
        /// </summary>
        private void LoadCompnFinancialBatchSummary()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string ProjectNmae = Request.QueryString["ProjectNmae"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            string BatchNo = Request.QueryString["BatchNo"];
            string Hhid = Request.QueryString["Hhid"];
            string Status = Request.QueryString["Status"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CompFinBatchSummary.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();
            ParameterField paramProjectName = new ParameterField();
            ParameterField paramBatchNo = new ParameterField();
            ParameterField paramHhidBatch = new ParameterField();
            ParameterField paramStatus = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "PROJECTFROMTDATE_";
            paramProjectEnddate.Name = "PROJECTTODATE_";
            paramProjectName.Name = "ProjectName_";
            paramBatchNo.Name = "BATCHNO_";
            paramHhidBatch.Name = "HHID_";
            paramStatus.Name = "Status";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHhidBatchVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramStatusVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;


            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectNameVal.Value = ProjectNmae;
            paramStatusVal.Value = Status;

            if (BatchNo.Trim().Length > 0 && Status.Trim() == "Detail")
                paramBatchNoVal.Value = Convert.ToInt32(BatchNo);
            else
                paramBatchNoVal.Value = null;


            if (Hhid.Trim().Length > 0 && Status.Trim() == "Detail")
                paramHhidBatchVal.Value = Convert.ToInt32(Hhid);
            else
                paramHhidBatchVal.Value = null;

            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);
            paramProjectName.CurrentValues.Add(paramProjectNameVal);
            paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            paramHhidBatch.CurrentValues.Add(paramHhidBatchVal);
            paramStatus.CurrentValues.Add(paramStatusVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);
            ParamFields.Add(paramProjectName);
            ParamFields.Add(paramBatchNo);
            ParamFields.Add(paramHhidBatch);
            ParamFields.Add(paramStatus);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load CompnFinancialDetails report
        /// </summary>
        private void LoadCompnFinancialDetails()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string ProjectNmae = Request.QueryString["ProjectNmae"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CompFinDetails.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();
            ParameterField paramProjectName = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "PROJECTFROMTDATE_";
            paramProjectEnddate.Name = "PROJECTTODATE_";
            paramProjectName.Name = "ProjectName_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectNameVal.Value = ProjectNmae;



            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);
            paramProjectName.CurrentValues.Add(paramProjectNameVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);
            ParamFields.Add(paramProjectName);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load HouseInKindPending report
        /// </summary>
        private void LoadHouseInKindPending()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "HouseInkindPending.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load LandInKindPending report
        /// </summary>
        private void LoadLandInKindPending()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "LandInKindPending.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;
            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load CashCompenPending report
        /// </summary>
        private void LoadCashCompenPending()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CashCompensationPending.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load PackagesClosed report
        /// </summary>
        private void LoadPackagesClosed()
        {
            string ProjectID = Request.QueryString["ProjectID"];

            string Status = Request.QueryString["Status"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();
            if (Status == "OPT")
                myRpt.Load(RPT_SOURCE + "NoPackagesClosed.rpt");
            else
                myRpt.Load(RPT_SOURCE + "PackagesClosedPerAmount.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();


            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";


            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();


            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);



            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);



            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load HouseInKindDelivered report
        /// </summary>
        private void LoadHouseInKindDelivered()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "HouseInKindDelivered.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();
            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load CashCompensationDelivered report
        /// </summary>
        private void LoadCashCompensationDelivered()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CompensationDelivered.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load PackagePrinting report
        /// </summary>
        private void LoadPackagePrinting()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Requeststartdate = Request.QueryString["opsStartDate"];
            string RequestEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "PackagesPrinted.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramRequeststartdate = new ParameterField();
            ParameterField paramRequestEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramRequeststartdate.Name = "REQUESTDATEFROM_";
            paramRequestEnddate.Name = "REQUESTDATETO_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramRequeststartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramRequestEnddateVal = new ParameterDiscreteValue();

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            if (Requeststartdate.Length > 0)
                paramRequeststartdateVal.Value = Convert.ToDateTime(Requeststartdate);
            else
                paramRequeststartdateVal.Value = null;
            if (RequestEnddate.Length > 0)
                paramRequestEnddateVal.Value = Convert.ToDateTime(RequestEnddate);
            else
                paramRequestEnddateVal.Value = null;

            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramRequeststartdate.CurrentValues.Add(paramRequeststartdateVal);
            paramRequestEnddate.CurrentValues.Add(paramRequestEnddateVal);

            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramRequeststartdate);
            ParamFields.Add(paramRequestEnddate);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load OptionDetails report
        /// </summary>

        //Edwin Baguma:
        private void LoadPackageStatus()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string HHID = Request.QueryString["HHID"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "RPT_PKREV_STATUS.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramHHID.Name = "HHID_";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(HHID);
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();
        }
        //Edwin Baguma:


        private void LoadBatchStatusReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            //string WorkflowCode = Request.QueryString["WorkflowCode"];
            string BatchNo = Request.QueryString["BatchNo"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "RPT_PAYRQ_STATUS.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramBatchNo = new ParameterField();
            ParameterField paramProjectID = new ParameterField();

            paramBatchNo.Name = "BATCHNO_";
            paramProjectID.Name = "PROJECTID_";

            ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramWorkflowCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIDVal = new ParameterDiscreteValue();

            paramBatchNoVal.Value = Convert.ToInt32(BatchNo);
            paramProjectIDVal.Value = Convert.ToInt32(ProjectID);

            paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            paramProjectID.CurrentValues.Add(paramProjectIDVal);

            ParamFields.Add(paramBatchNo);
            ParamFields.Add(paramProjectID);

            CrystalReportViewer1.RefreshReport();
        }

        private void LoadOptionDetails()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "OptionDetails.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();

            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);



            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Summarys report
        /// </summary>
        /// <param name="ReportType"></param>
        private void LoadSummarys(string ReportType)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            if (ReportType == "OPTS")
                myRpt.Load(RPT_SOURCE + "OptionsSummaryrpt.rpt");
            else if (ReportType == "OPTST")
                myRpt.Load(RPT_SOURCE + "TribeSummaryrpt.rpt");
            else if (ReportType == "OPTSG")
                myRpt.Load(RPT_SOURCE + "GenderSummaryrpt.rpt");
            else if (ReportType == "OPTSR")
                myRpt.Load(RPT_SOURCE + "ReligionSummaryrpt.rpt");
            else if (ReportType == "OPTSL")
                myRpt.Load(RPT_SOURCE + "LiteracySummaryrpt.rpt");


            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectstartdate.Name = "ProjectFromtDate_";
            paramProjectEnddate.Name = "ProjectToDate_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load BatchStatus report
        /// </summary>
        private void LoadBatchStatus()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string Projectstartdate = Request.QueryString["opsStartDate"];
            string ProjectEnddate = Request.QueryString["opsEndDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_SUMMARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "BatchStatus.rpt");


            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramBatchNo = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramBatchNo.Name = "CMP_BATCHNO_";
            paramProjectstartdate.Name = "BATCHFROMDATE_";
            paramProjectEnddate.Name = "BATCHTODATE_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramBatchNoVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramBatchNoVal = null;

            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramBatchNo.CurrentValues.Add(paramBatchNoVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramBatchNo);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Monitoring Result and Resolution report
        /// </summary>
        private void LoadMonitoringResultandResolution()
        {

            string ProjectID = Request.QueryString["ProjectID"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_M_E) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();
            myRpt.Load(RPT_SOURCE + "MonitoringResults&Evaluations.rpt");
            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();


            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";


            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();


            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);



            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);



            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load AcceptanceCount report
        /// </summary>
        /// <param name="rptCode"></param>
        private void LoadAcceptanceCount(string rptCode)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string FromDate = Request.QueryString["FromDate"];
            string ToDate = Request.QueryString["ToDate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Package_Acceptance) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            if (rptCode == "ACTCOUNT")
                myRpt.Load(RPT_SOURCE + "PKGACCEPTANCECOUNT.rpt");
            else
                myRpt.Load(RPT_SOURCE + "PkgAcceptanceDetail.rpt");

            //myRpt.Load(RPT_SOURCE + "PKGACCEPTANCECOUNT.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramFromdate = new ParameterField();
            ParameterField paramTodate = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramFromdate.Name = "FROMDATE_";
            paramTodate.Name = "TODATE_";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramToDateVal = new ParameterDiscreteValue();

            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            if (FromDate.Length > 0)
                paramFromDateVal.Value = Convert.ToDateTime(FromDate);
            else
                paramFromDateVal.Value = null;
            if (ToDate.Length > 0)
                paramToDateVal.Value = Convert.ToDateTime(ToDate);
            else
                paramToDateVal.Value = null;

            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramFromdate.CurrentValues.Add(paramFromDateVal);
            paramTodate.CurrentValues.Add(paramToDateVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramFromdate);
            ParamFields.Add(paramTodate);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load GeneralPapCategory report
        /// </summary>
        /// <param name="rptCode"></param>
        private void LoadGeneralPapCategory(string rptCode)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PAP_CATEGORY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();
            if (rptCode == "GPC")
                myRpt.Load(RPT_SOURCE + "GeneralPAPCategory.rpt");
            else
                myRpt.Load(RPT_SOURCE + "GeneralPAPOptionCategory.rpt");



            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "district_";
            paramCounty.Name = "county_";
            paramSubCounty.Name = "subcounty_";
            paramParish.Name = "parish_";
            paramVillage.Name = "village_";
            paramPapName.Name = "papname_";

            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramProjectIdVal.Value = ProjectID;


            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load FundProgressRequest report
        /// </summary>
        /// <param name="rptCode"></param>
        private void LoadFundProgressRequest(string rptCode)
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FUND) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FUNDREQCHNG) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            if (rptCode == "RIPRF")
                myRpt.Load(RPT_SOURCE + "FundProgress.rpt");
            else
                myRpt.Load(RPT_SOURCE + "FundCompleted.rpt");


            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramDistrict.Name = "district_";
            paramCounty.Name = "county_";
            paramSubCounty.Name = "subcounty_";
            paramParish.Name = "parish_";
            paramVillage.Name = "village_";
            paramPapName.Name = "papname_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramProjectVal.Value = ProjectID;

            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Approvals Report
        /// </summary>
        private void LoadApprovalsReport()
        {
            string ReportType = Request.QueryString["ReportType"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_APPROVAL) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_APPROVAL_DUE) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            if (ReportType == "APP")
                myRpt.Load(RPT_SOURCE + "Approvals.rpt");
            else if (ReportType == "DUE")
                myRpt.Load(RPT_SOURCE + "ApprovalsDue.rpt");


            string aproverID = Request.QueryString["aproverID"];
            string Projectid = Request.QueryString["Projectid"];
            string Apprstartdate = Request.QueryString["Apprstartdate"];
            string ApprEnddate = Request.QueryString["ApprEnddate"];
            string aproverName = Request.QueryString["AproverName"];
            string ProjectName = Request.QueryString["ProjectName"];

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }
            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramApprId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramApprstartdate = new ParameterField();
            ParameterField paramApprEnddate = new ParameterField();
            ParameterField paramHHID = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramaproverName = new ParameterField();
            ParameterField paramProjectName = new ParameterField();

            paramApprId.Name = "APPROVERID_";
            paramProjectId.Name = "PROJECTID_";
            paramApprstartdate.Name = "REQUESTDATEFROM_";
            paramApprEnddate.Name = "REQUESTDATETO_";
            paramHHID.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramaproverName.Name = "AproverName";
            paramProjectName.Name = "ProjectName";

            ParameterDiscreteValue paramApprIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramApprstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramApprEnddateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramaproverNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();

            if (aproverID.Length > 0)
                paramApprIdVal.Value = aproverID;
            else
                paramApprIdVal.Value = null;
            if (Projectid.Length > 0)
                paramProjectIdVal.Value = Projectid;
            else
                paramProjectIdVal.Value = null;
            if (Apprstartdate.Length > 0)
                paramApprstartdateVal.Value = Convert.ToDateTime(Apprstartdate);
            else
                paramApprstartdateVal.Value = null;
            if (ApprEnddate.Length > 0)
                paramApprEnddateVal.Value = Convert.ToDateTime(ApprEnddate);
            else
                paramApprEnddateVal.Value = null;
            paramaproverNameVal.Value = aproverName;
            paramProjectNameVal.Value = ProjectName;

            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramApprId.CurrentValues.Add(paramApprIdVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramApprstartdate.CurrentValues.Add(paramApprstartdateVal);
            paramApprEnddate.CurrentValues.Add(paramApprEnddateVal);
            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramaproverName.CurrentValues.Add(paramaproverNameVal);
            paramProjectName.CurrentValues.Add(paramProjectNameVal);

            ParamFields.Add(paramApprId);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramApprstartdate);
            ParamFields.Add(paramApprEnddate);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramaproverName);
            ParamFields.Add(paramProjectName);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load AuditTrial report
        /// </summary>
        private void LoadAuditTrial()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string ProjectFromdate = Request.QueryString["ProjectFromdate"];
            string ProjectTodate = Request.QueryString["ProjectTodate"];
            string ActionById = Request.QueryString["ActionByID"];
            string ActionName = Request.QueryString["ActionName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_AUDIT_TRAIL) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "AuditTrail.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramFromDate = new ParameterField();
            ParameterField paramToDate = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramActionBy = new ParameterField();
            ParameterField paramActionName = new ParameterField();



            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramFromDate.Name = "FROMDATE_";
            paramToDate.Name = "TODATE_";
            paramProjectId.Name = "PROJECTID_";
            paramActionBy.Name = "ACTIONBY_";
            paramActionName.Name = "ActionName";


            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramToDateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramActionByVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramActionNameVal = new ParameterDiscreteValue();


            if (ProjectFromdate.Length > 0)
                paramFromDateVal.Value = Convert.ToDateTime(ProjectFromdate).ToString(UtilBO.DateFormatDB);
            else
                paramFromDateVal.Value = null;

            if (ProjectTodate.Length > 0)
                paramToDateVal.Value = Convert.ToDateTime(ProjectTodate).ToString(UtilBO.DateFormatDB);
            else
                paramToDateVal.Value = null;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectIdVal.Value = ProjectID;
            paramActionByVal.Value = ActionById;
            paramActionNameVal.Value = ActionName;


            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramFromDate.CurrentValues.Add(paramFromDateVal);
            paramToDate.CurrentValues.Add(paramToDateVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramActionBy.CurrentValues.Add(paramActionByVal);
            paramActionName.CurrentValues.Add(paramActionNameVal);

            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramFromDate);
            ParamFields.Add(paramToDate);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramActionBy);
            ParamFields.Add(paramActionName);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Project Report
        /// </summary>
        private void LoadProjectReport()
        {
            string ProjectName = Request.QueryString["ProjectName"];
            string Projectstartdate = Request.QueryString["Projectstartdate"];
            string ProjectEnddate = Request.QueryString["ProjectEnddate"];
            string Projectstatus = Request.QueryString["Projectstatus"];
            string Projectcode = Request.QueryString["Projectcode"];
            string consultname = Request.QueryString["consultname"];
            string ProjectID = Request.QueryString["ProjectID"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PROJECT_LIST) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "ProjectReport.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProjectName = new ParameterField();
            ParameterField paramProjectstartdate = new ParameterField();
            ParameterField paramProjectEnddate = new ParameterField();
            ParameterField paramProjectstatus = new ParameterField();
            ParameterField paramHHID = new ParameterField();
            ParameterField paramProjectcode = new ParameterField();
            ParameterField paramconsultname = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramProjectName.Name = "PROJECTNAME_";
            paramProjectstartdate.Name = "PROJECTSTARTDATE_";
            paramProjectEnddate.Name = "PROJECTENDDATE_";
            paramProjectstatus.Name = "PROJECTSTATUS_";
            paramHHID.Name = "HHID_COMN";
            paramProjectcode.Name = "P_PROJECTCODE";
            paramconsultname.Name = "CONSULTANTNAME_";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectstatusVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectcodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramconsultnameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();


            paramProjectNameVal.Value = ProjectName;
            paramProjectstatusVal.Value = Projectstatus;
            paramconsultnameVal.Value = consultname;
            if (Projectstartdate.Length > 0)
                paramProjectstartdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramProjectstartdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramProjectEnddateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramProjectEnddateVal.Value = null;
            //paramProjectcodeVal.Value = Session["PROJECT_CODE"].ToString();
            paramProjectcodeVal.Value = Projectcode;
            paramPrintedbyVal.Value = Session["userName"].ToString();
            //paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramHHIDVal.Value = null;
            paramProjectIdVal.Value = ProjectID;


            paramProjectName.CurrentValues.Add(paramProjectNameVal);
            paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
            paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);
            paramProjectstatus.CurrentValues.Add(paramProjectstatusVal);
            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProjectcode.CurrentValues.Add(paramProjectcodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramconsultname.CurrentValues.Add(paramconsultnameVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramProjectName);
            ParamFields.Add(paramProjectstartdate);
            ParamFields.Add(paramProjectEnddate);
            ParamFields.Add(paramProjectstatus);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProjectcode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramconsultname);
            ParamFields.Add(paramProjectId);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load BudjetEstimation report
        /// </summary>
        private void LoadBudjetEstimation()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BUDGET_EXPENDITURE) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "BudjetEstimation.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();


            paramProject.Name = "PROJECTID_";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            paramProjectVal.Value = ProjectID;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Comparision DataReport
        /// </summary>
        private void LoadComparisionDataReport()
        {

            string ReportType = Request.QueryString["ReportType"];
            string ViewMaster = Request.QueryString["ViewMaster"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CHANGE_REPORTS) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            if (ReportType == "CMPPERM")
                myRpt.Load(RPT_SOURCE + "CMPPermanentStr.rpt");
            else if (ReportType == "CMPNONPER")
                myRpt.Load(RPT_SOURCE + "CMPNonPermanentStr.rpt");
            else if (ReportType == "CMPDMACR")
                myRpt.Load(RPT_SOURCE + "CMPDamagedCrops.rpt");
            else if (ReportType == "CMPCROP")
                myRpt.Load(RPT_SOURCE + "CMPCrops.rpt");
            else if (ReportType == "CMPGRAVE")
                myRpt.Load(RPT_SOURCE + "CMPGrave.rpt");
            else if (ReportType == "CMPFENCE")
                myRpt.Load(RPT_SOURCE + "CMPFence.rpt");
            else if (ReportType == "CMPOTHER")
                myRpt.Load(RPT_SOURCE + "OtherFixtures.rpt");
            else if (ReportType == "CMPCULPRO")
                myRpt.Load(RPT_SOURCE + "CMPCultureProperty.rpt");
            else if (ReportType == "CMPFINVAL")
                myRpt.Load(RPT_SOURCE + "CMPFinalValuation.rpt");
            else if (ReportType == "CMPACVAL")
                myRpt.Load(RPT_SOURCE + "CMPAcreageValuation.rpt");
            else if (ReportType == "CMPNEI")
                myRpt.Load(RPT_SOURCE + "CMPNeighbour.rpt");
            else if (ReportType == "CMPSTA")
                myRpt.Load(RPT_SOURCE + "CMPStakeholder.rpt");
            else if (ReportType == "CMPHOU")
                myRpt.Load(RPT_SOURCE + "CMPHousehold.rpt");
            else if (ReportType == "CMPINST")
                myRpt.Load(RPT_SOURCE + "CMPInstitution.rpt");
            else if (ReportType == "CMPLOAL")
                myRpt.Load(RPT_SOURCE + "CMPLivingonAffectedLand.rpt");
            else if (ReportType == "CMPLOFF")
                myRpt.Load(RPT_SOURCE + "CMPLivingoffAffectedLand.rpt");
            else if (ReportType == "CMPGRP")
                myRpt.Load(RPT_SOURCE + "CMPGroupOwnership.rpt");
            else if (ReportType == "CMPGRPM")
                myRpt.Load(RPT_SOURCE + "CMPGroupMembers.rpt");
            else if (ReportType == "CMPSER")
                myRpt.Load(RPT_SOURCE + "CMPServicesonAffectedplot.rpt");
            else if (ReportType == "CMPHHT")
                myRpt.Load(RPT_SOURCE + "CMPHouseholdRelations.rpt");
            else if (ReportType == "CMPALU")
                myRpt.Load(RPT_SOURCE + "CMPAffectedLandUsers.rpt");
            else if (ReportType == "CMPLHD")
                myRpt.Load(RPT_SOURCE + "CMPLivelihoodDetails.rpt");
            else if (ReportType == "CMPDDT")
                myRpt.Load(RPT_SOURCE + "CMPDisability.rpt");
            else if (ReportType == "CMPHCD")
                myRpt.Load(RPT_SOURCE + "cmp_HealthInfo.rpt");
            else if (ReportType == "CMPSHOCK")
                myRpt.Load(RPT_SOURCE + "MajorShocks.rpt");
            else if (ReportType == "CMPINDW")
                myRpt.Load(RPT_SOURCE + "CMPWelfareIndicators.rpt");
            else if (ReportType == "CMPWELF")
                myRpt.Load(RPT_SOURCE + "CMP_Welfare.rpt");
            else if (ReportType == "CMPLNDINFOPUB")
                myRpt.Load(RPT_SOURCE + "CMPPubLandInfo.rpt");
            else if (ReportType == "CMPLNDINFOPRIV")
                myRpt.Load(RPT_SOURCE + "CMPPrivLandInfo.rpt");
            else if (ReportType == "CMPCON")
                myRpt.Load(RPT_SOURCE + "CMPSocioConcern.rpt");
            else if (ReportType == "CMPOTHLAND")
                myRpt.Load(RPT_SOURCE + "Cmp_OthLandHoldings.rpt");
            else if (ReportType == "CMPMEMCLA")
                myRpt.Load(RPT_SOURCE + "cmp_OthLandMemberClaim.rpt");


            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramHHID_COMN = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramViewMaster = new ParameterField();

            paramHHID.Name = "HHID_";
            paramHHID_COMN.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";
            paramViewMaster.Name = "VIEWMASTERCOPY";

            ParameterDiscreteValue paramHHID_COMNVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramViewMasterVal = new ParameterDiscreteValue();

            paramHHID_COMNVal.Value = Session["HH_ID"].ToString();
            paramHHIDVal.Value = Session["HH_ID"].ToString();
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectIdVal.Value = Session["PROJECT_ID"].ToString();
            paramViewMasterVal.Value = ViewMaster;

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_COMN.CurrentValues.Add(paramHHID_COMNVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramViewMaster.CurrentValues.Add(paramViewMasterVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramHHID_COMN);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramViewMaster);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Survey Report
        /// </summary>
        private void LoadSurveyReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            string PlotReference = Request.QueryString["PlotReference"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_RPT_SURVEY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "Survey.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPlotreference = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramPlotreference.Name = "p_plotreference";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";


            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPlotreferenceVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramPlotreferenceVal.Value = PlotReference;
            paramProjectIdVal.Value = ProjectID;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPlotreference.CurrentValues.Add(paramPlotreferenceVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPlotreference);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Compensation Beneficiary Report
        /// </summary>
        private void LoadCompensationBeneficiaryReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            string PlotReference = Request.QueryString["PlotReference"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMPENSATION_BENEFICIARY) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CompensationBeneficiary.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();
            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPlotreference = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramPlotreference.Name = "p_plotreference";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPlotreferenceVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramPlotreferenceVal.Value = PlotReference;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectIdVal.Value = ProjectID;
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPlotreference.CurrentValues.Add(paramPlotreferenceVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPlotreference);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// Load Dispute Rsolution Tracking Report
        /// </summary>
        private void LoadDisputeRsolutionTrackingReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            string PlotReference = Request.QueryString["PlotReference"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_DISPUTE_RES_TRACKING) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            string rptCode = Request.QueryString["rptCode"];
            if (rptCode == "DRTGRI")
                myRpt.Load(RPT_SOURCE + "Grievance.rpt");
            else
                myRpt.Load(RPT_SOURCE + "DisputeResolutionTracking.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPlotReference = new ParameterField();
            //  ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramPlotReference.Name = "p_plotreference";
            // paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPlotReferenceVal = new ParameterDiscreteValue();
            // ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramPlotReferenceVal.Value = PlotReference;

            //paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIdVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectIdVal.Value = ProjectID;

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPlotReference.CurrentValues.Add(paramPlotReferenceVal);
            //  paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIdVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPlotReference);
            //   ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadValuationReport
        /// </summary>
        private void LoadValuationReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];

            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_RPT_VALUATION) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "Valuation.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();


            paramDistrict.Name = "DISTRICT_";
            paramCounty.Name = "COUNTY_";
            paramSubCounty.Name = "SUBCOUNTY_";
            paramParish.Name = "PARISH_";
            paramVillage.Name = "VILLAGE_";
            paramPapName.Name = "PAPNAME_";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";


            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();


            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramProjectIdVal.Value = ProjectID;
            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);


            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);

            foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }

            CrystalReportViewer1.RefreshReport();


        }
        /// <summary>
        /// LoadCulturalPropertyReport
        /// </summary>
        private void LoadCulturalPropertyReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CULTURAL_PROP_MGMT) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "CulturalProperty.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();
            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";
            paramHHId.Name = "HHID_COMN";


            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramProjectIdVal.Value = ProjectID;
            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHId);

            foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadPublicConsultationReport
        /// </summary>
        private void LoadPublicConsultationReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];

            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            //  string PAPName = Request.QueryString["PAPName"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PUBLIC_CONSULTATION_PROGRESS) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "PublicConsultation.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            // ParameterField paramPapName = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            //  paramPapName.Name = "p_papname";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            // ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            //  paramPapNameVal.Value = PAPName;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            //paramProjectIdVal.Value = "PROJECTID_";
            paramProjectIdVal.Value = ProjectID;

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            // paramPapName.CurrentValues.Add(paramPapNameVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            //ParamFields.Add(paramPapName);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadCompensationReport
        /// </summary>
        private void LoadCompensationReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_RPT_COMPENSATION) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "Compensation.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramProject = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();

            paramProject.Name = "PROJECTID_";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";

            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            paramProjectVal.Value = ProjectID;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramProject.CurrentValues.Add(paramProjectVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);


            ParamFields.Add(paramProject);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadLegalCaseTrackingReport
        /// </summary>
        private void LoadLegalCaseTrackingReport()
        {
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            string PlotReference = Request.QueryString["PlotReference"];
            string ProjectID = Request.QueryString["ProjectID"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LEGAL_CASE_TRACKING) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "Legal Case Tracking.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();


            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPlotreference = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramPlotreference.Name = "p_plotreference";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPlotreferenceVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramPlotreferenceVal.Value = PlotReference;
            paramProjectIdVal.Value = ProjectID;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPlotreference.CurrentValues.Add(paramPlotreferenceVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPlotreference);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadStatisticsReport
        /// </summary>
        private void LoadStatisticsReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            // string PAPName = Request.QueryString["PAPName"];
            string Projectstartdate = Request.QueryString["Projectstartdate"];
            string ProjectEnddate = Request.QueryString["ProjectEnddate"];

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STATISTICS) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }
            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "Statistics.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();


            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            //   ParameterField paramPapName = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramFromdate = new ParameterField();
            ParameterField paramTodate = new ParameterField();

            paramDistrict.Name = "district_";
            paramCounty.Name = "county_";
            paramSubCounty.Name = "subcounty_";
            paramParish.Name = "parish_";
            paramVillage.Name = "village_";
            //   paramPapName.Name = "papname_";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";
            paramFromdate.Name = "FROMDATE_";
            paramTodate.Name = "TODATE_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            //   ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramTodateVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            // paramPapNameVal.Value = PAPName;

            paramProjectIdVal.Value = ProjectID;

            paramHHIdVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            if (Projectstartdate.Length > 0)
                paramFromdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramFromdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramTodateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramTodateVal.Value = null;


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            // paramPapName.CurrentValues.Add(paramPapNameVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIdVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramFromdate.CurrentValues.Add(paramFromdateVal);
            paramTodate.CurrentValues.Add(paramTodateVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            // ParamFields.Add(paramPapName);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramFromdate);
            ParamFields.Add(paramTodate);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadDailyProjectsStatusReport
        /// </summary>
        private void LoadDailyProjectsStatusReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string Projectstartdate = Request.QueryString["Projectstartdate"];
            string ProjectEnddate = Request.QueryString["ProjectEnddate"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_DAILY_PROJ_STATUS) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "DailyProjectStatus.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramFromdate = new ParameterField();
            ParameterField paramTodate = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectCode.Name = "P_PROJECTCODE";
            paramProjectId.Name = "PROJECTID_";
            paramFromdate.Name = "FROMDATE_";
            paramTodate.Name = "TODATE_";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramFromdateVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramTodateVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramProjectIdVal.Value = ProjectID;

            paramProjectCodeVal.Value = "";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIdVal.Value = Convert.ToInt32(Session["HH_ID"]);

            if (Projectstartdate.Length > 0)
                paramFromdateVal.Value = Convert.ToDateTime(Projectstartdate);
            else
                paramFromdateVal.Value = null;
            if (ProjectEnddate.Length > 0)
                paramTodateVal.Value = Convert.ToDateTime(ProjectEnddate);
            else
                paramTodateVal.Value = null;


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHId.CurrentValues.Add(paramHHIdVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramFromdate.CurrentValues.Add(paramFromdateVal);
            paramTodate.CurrentValues.Add(paramTodateVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramFromdate);
            ParamFields.Add(paramTodate);

            foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadMonthlyProjectExpenditureReport
        /// </summary>
        private void LoadMonthlyProjectExpenditureReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string ProjectCode = Request.QueryString["ProjectCode"];
            string ProjectName = Request.QueryString["ProjectName"];
            string Month = Request.QueryString["Month"];
            string Year = Request.QueryString["Year"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MONTHLY_PROJ_EXP) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }


            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "MonthlyProjectExpenditure.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramPrjectCode = new ParameterField();
            ParameterField paramPrjectName = new ParameterField();
            ParameterField paramMonth = new ParameterField();
            ParameterField paramYear = new ParameterField();
            ParameterField paramPrintedBy = new ParameterField();
            ParameterField paramHHId = new ParameterField();
            ParameterField paramProjectId = new ParameterField();


            paramPrjectCode.Name = "P_ProjectCode";
            paramPrjectName.Name = "P_ProjectName";
            paramMonth.Name = "P_Month";
            paramYear.Name = "P_Year";
            paramPrintedBy.Name = "P_PrintedBy";
            paramHHId.Name = "HHID_COMN";
            paramProjectId.Name = "PROJECTID_";


            ParameterDiscreteValue paramPrjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrjectNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramMonthVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramYearVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedByVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            if (ProjectCode.Trim() != "")
                paramPrjectCodeVal.Value = ProjectCode;
            else
                paramPrjectCodeVal.Value = null;

            if (ProjectName.Trim() != "")
                paramPrjectNameVal.Value = ProjectName;
            else
                paramPrjectNameVal.Value = null;

            if (Month.Trim() != "")
                paramMonthVal.Value = Month;
            else
                paramMonthVal.Value = null;

            if (Year.Trim() != "")
                paramYearVal.Value = Year;
            else
                paramYearVal.Value = null;

            //if (ProjectID.Trim() != "")
            //    paramProjectIdVal.Value = ProjectID;
            //else
            paramProjectIdVal.Value = null;
            //paramMonthVal.Value = Month;
            //paramYearVal.Value = Year;

            paramPrintedByVal.Value = Session["userName"].ToString();
            paramHHIdVal.Value = Convert.ToInt32(Session["HH_ID"]);
            //paramProjectIdVal.Value = ProjectID;

            paramPrjectCode.CurrentValues.Add(paramPrjectCodeVal);
            paramPrjectName.CurrentValues.Add(paramPrjectNameVal);
            paramMonth.CurrentValues.Add(paramMonthVal);
            paramYear.CurrentValues.Add(paramYearVal);
            paramPrintedBy.CurrentValues.Add(paramPrintedByVal);
            paramHHId.CurrentValues.Add(paramHHIdVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);


            ParamFields.Add(paramPrjectCode);
            ParamFields.Add(paramPrjectName);
            ParamFields.Add(paramMonth);
            ParamFields.Add(paramYear);
            ParamFields.Add(paramPrintedBy);
            ParamFields.Add(paramHHId);
            ParamFields.Add(paramProjectId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// LoadLivelihoodSupportReport
        /// </summary>
        private void LoadLivelihoodSupportReport()
        {
            string ProjectID = Request.QueryString["ProjectID"];
            string district = Request.QueryString["district"];
            string county = Request.QueryString["county"];
            string subcounty = Request.QueryString["subCounty"];
            string parish = Request.QueryString["parish"];
            string village = Request.QueryString["village"];
            string PAPName = Request.QueryString["PAPName"];
            string PlotReference = Request.QueryString["PlotReference"];
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CDAP_LIV_SUPPORT) == false)
            {
                CrystalReportViewer1.HasExportButton = false;
                CrystalReportViewer1.HasPrintButton = false;
            }

            myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + "LiveliHoodSupportRPT.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }

            CrystalReportViewer1.ReportSource = myRpt;

            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramDistrict = new ParameterField();
            ParameterField paramCounty = new ParameterField();
            ParameterField paramSubCounty = new ParameterField();
            ParameterField paramParish = new ParameterField();
            ParameterField paramVillage = new ParameterField();
            ParameterField paramPapName = new ParameterField();
            ParameterField paramPlotreference = new ParameterField();
            //  ParameterField paramProjectCode = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProjectId = new ParameterField();
            ParameterField paramHHId = new ParameterField();

            paramDistrict.Name = "p_district";
            paramCounty.Name = "p_county";
            paramSubCounty.Name = "p_subcounty";
            paramParish.Name = "p_parish";
            paramVillage.Name = "p_village";
            paramPapName.Name = "p_papname";
            paramPlotreference.Name = "p_plotreference";
            // paramProjectCode.Name = "P_PROJECTCODE";
            paramPrintedby.Name = "P_PrintedBy";
            paramProjectId.Name = "PROJECTID_";
            paramHHId.Name = "HHID_COMN";

            ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPlotreferenceVal = new ParameterDiscreteValue();
            // ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectIdVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            paramDistrictVal.Value = district;
            paramCountyVal.Value = county;
            paramSubCountyVal.Value = subcounty;
            paramParishVal.Value = parish;
            paramVillageVal.Value = village;
            paramPapNameVal.Value = PAPName;
            paramPlotreferenceVal.Value = PlotReference;
            paramProjectIdVal.Value = ProjectID;
            // paramProjectCodeVal.Value = " ";
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);


            paramDistrict.CurrentValues.Add(paramDistrictVal);
            paramCounty.CurrentValues.Add(paramCountyVal);
            paramSubCounty.CurrentValues.Add(paramSubCountyVal);
            paramParish.CurrentValues.Add(paramParishVal);
            paramVillage.CurrentValues.Add(paramVillageVal);
            paramPapName.CurrentValues.Add(paramPapNameVal);
            paramPlotreference.CurrentValues.Add(paramPlotreferenceVal);
            // paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProjectId.CurrentValues.Add(paramProjectIdVal);
            paramHHId.CurrentValues.Add(paramHHIDVal);

            ParamFields.Add(paramDistrict);
            ParamFields.Add(paramCounty);
            ParamFields.Add(paramSubCounty);
            ParamFields.Add(paramParish);
            ParamFields.Add(paramVillage);
            ParamFields.Add(paramPapName);
            ParamFields.Add(paramPlotreference);
            // ParamFields.Add(paramProjectCode);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProjectId);
            ParamFields.Add(paramHHId);


            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// UpdateSerialNumber
        /// </summary>
        /// <param name="configItem"></param>
        private void UpdateSerialNumber(string configItem)
        {
            WIS_BusinessLogic.WIS_ConfigBLL objconfigBLL = new WIS_BusinessLogic.WIS_ConfigBLL();
            WIS_ConfigBO obj = objconfigBLL.GetSerialNumber(configItem);
        }
        /// <summary>
        /// SocioEconomicQuestionnaire
        /// </summary>
        private void SocioEconomicQuestionnaire()
        {
            myRpt = new ReportDocument();
            myRpt.Load(RPT_SOURCE + "SocioQuestionnaire.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }
            CrystalReportViewer1.ReportSource = myRpt;
            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramHHID.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = Convert.ToInt32(Session["PROJECT_ID"]);


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// SurveyQuestionnaire
        /// </summary>
        private void SurveyQuestionnaire()
        {
            myRpt = new ReportDocument();
            myRpt.Load(RPT_SOURCE + "SurveyQuestionnaire.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }
            CrystalReportViewer1.ReportSource = myRpt;
            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramHHID.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = Convert.ToInt32(Session["PROJECT_ID"]);


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }
        /// <summary>
        /// ValuationQuestionnaire
        /// </summary>
        private void ValuationQuestionnaire()
        {
            myRpt = new ReportDocument();
            myRpt.Load(RPT_SOURCE + "ValuationQuestionaries.rpt");

            myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

            foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
            {
                TableLogOnInfo logInfo = myTable.LogOnInfo;
                logInfo.ConnectionInfo = ConnInfo;
                myTable.ApplyLogOnInfo(logInfo);
            }
            CrystalReportViewer1.ReportSource = myRpt;
            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramHHID = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramHHID.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = Convert.ToInt32(Session["PROJECT_ID"]);


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

    }
}