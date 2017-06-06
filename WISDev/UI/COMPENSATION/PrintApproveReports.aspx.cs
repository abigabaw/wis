using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.IO;
using System.Collections;
//using Neodynamic.SDK.Web;

namespace WIS
{
    public partial class PrintApproveReports : System.Web.UI.Page
    {
        #region Connection
        ConnectionInfo ConnInfo = new ConnectionInfo();
        ReportDocument myRpt = new ReportDocument();
        string RPT_SOURCE = "";
        #endregion

        /// <summary>
        /// Call Respective methos to Print Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region PageUnload
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (myRpt != null)
            {
                try
                {
                    myRpt.Close();
                    myRpt.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch { }
            }
        }
        #endregion
       
        public void ExportReport()
        {
            MemoryStream oStream = new MemoryStream();
            // // using System.IO

            //this contains the value of the selected export format.
            ////////switch ("Portable Document (PDF)")
            ////////{

            ////////    //case "Rich Text (RTF)":
            ////////    //    //-----------------------------------------------------------

            ////////    //    oStream = myRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            ////////    //    Response.Clear();
            ////////    //    Response.Buffer = true;
            ////////    //    Response.ContentType = "application/rtf";
            ////////    //    break;
            ////////    //------------------------------------------------------------

            ////////    //------------------------------------------------------------
            ////////    case "Portable Document (PDF)":

            ////////        oStream = (System.IO.MemoryStream)myRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            ////////        Response.Clear();
            ////////        Response.Buffer = true;
            ////////        Response.ContentType = "application/pdf";
            ////////        break;
            ////////    //--------------------------------------------------------------

            ////////    //--------------------------------------------------------------
            ////////    //case "MS Word (DOC)":

            ////////    //    oStream = myRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
            ////////    //    Response.Clear();
            ////////    //    Response.Buffer = true;
            ////////    //    Response.ContentType = "application/doc";
            ////////    //    break;
            ////////    //---------------------------------------------------------------

            ////////    //---------------------------------------------------------------
            ////////    //case "MS Excel (XLS)":

            ////////    //    oStream = myRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            ////////    //    Response.Clear();
            ////////    //    Response.Buffer = true;
            ////////    //    Response.ContentType = "application/vnd.ms-excel";
            ////////    //    break;
            ////////    //---------------------------------------------------------------
            ////////}
            //////////export format
            try
            {
                //oStream = (System.IO.MemoryStream)myRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected static Queue reportQueue = new Queue();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    reportQueue.Enqueue(myRpt);
                    if (reportQueue.Count > 5) ((ReportDocument)reportQueue.Dequeue()).Dispose();
                }
                catch
                {
                }
            }

            int USER_ID = 0;
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            string documentCode = Request.QueryString["documentCode"];
            if (Session["USER_ID"] != null)
                USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
            ConnInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_ServerName");
            ConnInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_DatabaseName");
            ConnInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_UserID");
            ConnInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Password");
            RPT_SOURCE = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Source");
            if (!IsPostBack)
            {
                getpreComments(documentCode, HHID, USER_ID);
            }

            ChangeApprovedStatus();
            if (CrystalReportViewer1.Visible)
            {
                GetReport();

            }
        }

        protected void CrystalReportViewer1_DrillDownSubreport(object source, CrystalDecisions.Web.DrillSubreportEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Call Respective methos to Print Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetReport()
        {
            string documentCode_ = Request.QueryString["documentCode"];
            switch (documentCode_)
            {
                case "DEL1":
                case "DEL1A":
                case "DEL2":
                case "DEL3A":
                case "DEL3B":
                case "DEL3C":
                    CompensationPackageChecklist(documentCode_);
                    break;

                //case "PERID":
                case "PERID1":
                case "PERID1A":
                case "PERID2":
                case "PERID3A":
                case "PERID3B":
                case "PERID3C":
                    PersonalIdentification(documentCode_);
                    break;

                //case "RES":
                case "RES1":
                case "RES1A":
                case "RES2":
                case "RES3A":
                case "RES3B":
                case "RES3C":
                    ResidentialStructures(documentCode_);
                    break;

                //case "MNDOC":
                case "MNDOC1":
                case "MNDOC1A":
                case "MNDOC2":
                case "MNDOC3A":
                case "MNDOC3C":
                case "MNDOC3B":
                case "MNDOC3B1":
                case "MNDOC3B2":
                case "MNDOC3B3":
                case "MNDOC3B4":
                    PackagePaymentACK(documentCode_);
                    break;
                // case "DCRP":
                case "DCRP1":
                case "DCRP1A":
                case "DCRP2":
                case "DCRP3A":
                case "DCRP3B":
                case "DCRP3C":
                    DamagedCrop(documentCode_);
                    break;
                //case "OPTDS":
                //case "OPDS1":
                //case "OPDS" :
                case "OPDS1":
                case "OPDS1A":
                case "OPDS2":
                case "OPDS3A":
                case "OPDS3B":
                case "OPDS3C":
                    OptionDisclosureAgreement(documentCode_);
                    break;
                //case "CRP":
                case "CRP1":
                case "CRP1A":
                case "CRP2":
                case "CRP3A":
                case "CRP3B":
                case "CRP3C":
                    pkgCrops(documentCode_);
                    break;
                //case "FIXT":
                case "FIXT1":
                case "FIXT1A":
                case "FIXT2":
                case "FIXT3A":
                case "FIXT3B":
                case "FIXT3C":
                    pkgFixtures(documentCode_);
                    break;
                //case "ATTR":
                case "ATTR1":
                case "ATTR1A":
                case "ATTR2":
                case "ATTR3A":
                case "ATTR3B":
                case "ATTR3C":
                    pkgPowerofAttorney(documentCode_);
                    break;

                //case "BLDG":
                case "BLDG1":
                case "BLDG1A":
                case "BLDG2":
                case "BLDG3A":
                case "BLDG3B":
                case "BLDG3C":
                    UETCLBuildingMaterialOffered(documentCode_);
                    break;
                //case "LND":
                case "LND1":
                case "LND1A":
                case "LND2":
                case "LND3A":
                case "LND3B":
                case "LND3C":
                    Land(documentCode_);
                    break;
                //case "RCASH":
                case "RCASH1":
                case "RCASH1A":
                case "RCASH2":
                case "RCASH3A":
                case "RCASH3B":
                case "RCASH3C":
                    ResidentialStructuresCASHonly(documentCode_);
                    break;
                //case "CNS35":
                case "CNS35_1":
                case "CNS35_1A":
                case "CNS35_2":
                case "CNS35_3A":
                case "CNS35_3B":
                case "CNS35_3C":
                    Consent35(documentCode_);
                    break;
                //case "CNS39":
                case "CNS39_1":
                case "CNS39_1A":
                case "CNS39_2":
                case "CNS39_3A":
                case "CNS39_3B":
                case "CNS39_3C":
                    Consent39(documentCode_);
                    break;
                //case "DEFN":
                case "DEFN1":
                case "DEFN1A":
                case "DEFN2":
                case "DEFN3A":
                case "DEFN3B":
                case "DEFN3C":
                    ListofDefinitions(documentCode_);
                    break;
            }
        }

        #region for Approval Comments
        /// <summary>
        /// Get Comments
        /// </summary>
        /// <param name="documentcode_"></param>
        /// <param name="HHID"></param>
        /// <param name="userID"></param>
        public void getpreComments(string documentcode_, int HHID, int userID)
        {
            int projectID = 0;
            CompensationPackagesBLL COMPACKBLLobj = new CompensationPackagesBLL();
            CompensationPackagesBO cmppkgBo = new CompensationPackagesBO();
            if (Session["PROJECT_ID"] != null)
            {
                projectID = Convert.ToInt32(Session["PROJECT_ID"]);
            }

            cmppkgBo.DocumentCode = documentcode_.ToString();
            cmppkgBo.HHID = HHID;
            cmppkgBo.UserID = userID;
            cmppkgBo.ProjectID = projectID;
            cmppkgBo = COMPACKBLLobj.getpreComments(cmppkgBo);

            // if (cmppkgBo.PKGdocCount == 0)            {
                PnlPrintResion.Visible = false;
                int countresult = 0;
                cmppkgBo.DocumentCode = documentcode_.ToString();
                cmppkgBo.HHID = HHID;
                cmppkgBo.ApprovalComents = "First Time Reports open Document Name is" + documentcode_.ToString();
                cmppkgBo.UserID = userID;
                cmppkgBo.ProjectID = projectID;

                countresult = COMPACKBLLobj.SavereprintComments(cmppkgBo);
                btnClose.Visible = true;
                btnSaveClose.Visible = false;
                GetReport();
                ExportReport();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AfterPrint", "AfterPrint();", true);
            /*} 
            else
            {
                PnlPrintResion.Visible = true;
                btnSaveClose.Visible = true;
                btnClose.Visible = false;
                CrystalReportViewer1.Visible = false;
            }*/
        }

        /// <summary>
        /// Save print reason
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnprintresion_Click(object sender, EventArgs e)
        {
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            string documentCode_ = Request.QueryString["documentCode"];
            int projectID = 0;
            int USER_ID = 0;

            if (Session["PROJECT_ID"] != null)
            {
                projectID = Convert.ToInt32(Session["PROJECT_ID"]);
            }
            if (Session["USER_ID"] != null)
                USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
            CompensationPackagesBLL COMPACKBLLobj = new CompensationPackagesBLL();
            CompensationPackagesBO cmppkgBo = new CompensationPackagesBO();
            cmppkgBo.DocumentCode = documentCode_.ToString();
            cmppkgBo.HHID = HHID;
            cmppkgBo.UserID = USER_ID;
            cmppkgBo.ProjectID = projectID;
            cmppkgBo = COMPACKBLLobj.getpreComments(cmppkgBo);

            if (cmppkgBo.PKGdocCount > 0)
            {
                int countresult = 0;

                cmppkgBo.DocumentCode = documentCode_.ToString();
                cmppkgBo.HHID = HHID;
                string strMax = TxtPrintcomments.Text.ToString().Trim();
                if (strMax.Trim().Length >= 500)
                {
                    strMax = TxtPrintcomments.Text.ToString().Trim().Substring(0, 480);
                }
                cmppkgBo.ApprovalComents = strMax;
                cmppkgBo.UserID = USER_ID;
                cmppkgBo.ProjectID = projectID;

                countresult = COMPACKBLLobj.SavereprintComments(cmppkgBo);

                if (countresult == -1)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                    btnprintresion.Visible = false;
                    btnSaveClose.Visible = true;
                }
            }
            CrystalReportViewer1.Visible = true;
            GetReport();
            ExportReport();
            //printbutton.Visible = true;
            //ExportReport();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AfterPrint", "AfterPrint();", true);
        }

        /// <summary>
        /// Change Approved Status
        /// </summary>
        public void ChangeApprovedStatus()
        {
            int count = 0;
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);

            string documentCode = Request.QueryString["documentCode"];

            CompensationPackagesBLL COMPACKBLLobj = new CompensationPackagesBLL();

            CompensationPackagesBO objCompensationPackagesBO = new CompensationPackagesBO();
            objCompensationPackagesBO.HHID = Convert.ToInt32(HHID);
            objCompensationPackagesBO.DocumentCode = documentCode;

            count = COMPACKBLLobj.UpdateApprovalStatus(objCompensationPackagesBO);
            if (count == -1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CompPackDocument", "AfterstatusChange();", true);
            }

        }
        #endregion

        #region Methods
        // Parameters pass in these methods for different Reports
        // All there methods are user to Load reports

        //Start: Edwin Baguma (26 May, 2017) to properly dispose of report objects
        private void DisposePreviousReport()
        {
            try
            {
                myRpt.Close();
                myRpt.Dispose();
                GC.Collect();
                CrystalReportViewer1.Dispose();
                // GC.WaitForPendingFinalizers();
            }
            catch { }
        }
        //End: Edwin Baguma

        private void Consent39(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "CNS39_3A":
                    rptName = "PKGConsent40Group3A.rpt";
                    break;
                default:
                    rptName = "PKGConsent40Group3B.rpt";
                    break;
            }


            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGConsent39.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProject = new ParameterField();
            ParameterField paramhouseholdid = new ParameterField();


            paramHHID.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProject.Name = "PROJECTID_";
            paramhouseholdid.Name = "HOUSEHOLDID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramhouseholdidVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectVal.Value = ProjectID;
            paramhouseholdidVal.Value = Convert.ToInt32(Session["HH_ID"]); ;

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProject.CurrentValues.Add(paramProjectVal);
            paramhouseholdid.CurrentValues.Add(paramhouseholdidVal);

            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProject);
            ParamFields.Add(paramhouseholdid);

            CrystalReportViewer1.RefreshReport();
        }

        private void ListofDefinitions(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "DEFN3C":
                    rptName = "PKGListofDefinitionsGroup3C.rpt";
                    break;
                case "DEFN2":
                    rptName = "PKGListofDefinitionsGroup2.rpt";
                    break;
                case "DEFN3A":
                    rptName = "PKGListofDefinitionsGroup3A.rpt";
                    break;
                case "DEFN3B":
                    rptName = "PKGListofDefinitionsGroup3B.rpt";
                    break;
                case "DEFN1A":
                    rptName = "PKGListofDefinitionsGroup1A.rpt";
                    break;
                default:
                    rptName = "PKGListofDefinitionsGroup1.rpt";
                    break;
            }

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGDefinitions.rpt"

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
            ParameterField paramHHID = new ParameterField();
            ParameterField paramProject = new ParameterField();
            ParameterField paramHHID_ = new ParameterField();


            paramPrintedby.Name = "P_PrintedBy";
            paramProject.Name = "PROJECTID_";
            paramHHID.Name = "HHID_COMN";
            paramHHID_.Name = "P_HHID";

            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal_ = new ParameterDiscreteValue();


            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectVal.Value = ProjectID;
            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramHHIDVal_.Value = Convert.ToInt32(Session["HH_ID"]);


            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProject.CurrentValues.Add(paramProjectVal);
            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_.CurrentValues.Add(paramHHIDVal_);


            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProject);
            ParamFields.Add(paramHHID);
            if (docCode != "DEFN2" && docCode != "DEFN3A" && docCode != "DEFN3B" && docCode != "DEFN1A")
            {
                //ParamFields.Add(paramHHID_);
            }
            CrystalReportViewer1.RefreshReport();
        }

        private void Consent35(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "CNS35_3A":
                    rptName = "PKGConsent35Group3A.rpt";
                    break;
                default:
                    rptName = "PKGConsent35Group3B.rpt";
                    break;
            }

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGConsent.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProject = new ParameterField();


            paramHHID.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectVal.Value = ProjectID;

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void ResidentialStructuresCASHonly(string docCode)
        {

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            if (docCode == "RCASH3C")
            {
                myRpt.Load(RPT_SOURCE + "PKGResidentialStructuresGroup3C.rpt");
            }
            else
            {
                myRpt.Load(RPT_SOURCE + "PKGResidentialStructureCompensationCash.rpt");
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


            ParameterField paramHHID = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramHHID.Name = "HHID_";
            paramHHID_Comn.Name = "HHID_COMN";
            paramPrintedby.Name = "P_PrintedBy";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramProjectVal.Value = ProjectID;

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void Land(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "LND2":
                    rptName = "PKGLandGroup2.rpt";
                    break;
                case "LND3A":
                    rptName = "PKGLandGroup3A.rpt";
                    break;
                case "LND3B":
                    rptName = "PKGLandGroup3B.rpt";
                    break;
                case "LND3C":
                    rptName = "PKGLandGroup3C.rpt";
                    break;
                //case "DEL1A":
                //    rptName = "PKGCompensationChecklistGroup1A.rpt";
                //    break;
                //default:
                //    rptName = "PKGCompensationChecklistGroup1.rpt";
                //    break;
            }

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/PKGLandCompensationCash.rpt");

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);// "PKGLandCompensationCash.rpt"

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
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();


            paramHHID.Name = "HHID_";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);


            ParamFields.Add(paramHHID);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);


            CrystalReportViewer1.RefreshReport();
        }

        private void UETCLBuildingMaterialOffered(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "BLDG3A":
                    rptName = "PKGListofBuilding MaterialsGroup3A.rpt";
                    break;
                default:
                    rptName = "PKGListofBuilding MaterialsGroup3B.rpt";
                    break;
            }

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();
            //CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/PkgBuildingMaterialOffered.rpt");

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PkgBuildingMaterialOffered.rpt"

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
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();

            paramHHID.Name = "P_HHID";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";
            paramPrintedby.Name = "P_PrintedBy";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;
            paramPrintedbyVal.Value = Session["userName"].ToString();

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);
            ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();
        }

        private void ResidentialStructures(string docCode)
        {

            string rptName = "";

            switch (docCode)
            {
                case "RES2":
                    rptName = "PKGResidentialStructuresGroup2.rpt";
                    break;
                case "RES3A":
                    rptName = "PKGResidentialStructuresGroup3A.rpt";
                    break;
                case "RES3B":
                    rptName = "PKGResidentialStructuresGroup3B.rpt";
                    break;
                case "RES3C":
                    rptName = "PKGResidentialStructuresGroup3C.rpt";
                    break;
                //case "DEL1A":
                //    rptName = "PKGCompensationChecklistGroup1A.rpt";
                //    break;
                default:
                    rptName = "PKGResidentialStructuresGroup1.rpt";
                    break;
            }

            // CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/PKGResidentialStructureCompensation.rpt");

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGResidentialStructureCompensation.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();

            paramHHID.Name = "HHID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void pkgPowerofAttorney(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "ATTR3A":
                    rptName = "PKGPowerofAttorneyGroup3A.rpt";
                    break;
                default:
                    rptName = "PKGPowerofAttorneyGroup3B.rpt";
                    break;
            }

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGPowersOfAttorney.rpt"

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


            paramPrintedby.Name = "P_PrintedBy";


            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();


            paramPrintedbyVal.Value = Session["userName"].ToString();


            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);


            ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();
        }

        private void pkgFixtures(string docCode)
        {

            string rptName = "";

            switch (docCode)
            {
                case "FIXT3C":
                    rptName = "PKGFixturesGroup3C.rpt";
                    break;
                case "FIXT2":
                    rptName = "PKGFixturesGroup2.rpt";
                    break;
                case "FIXT3A":
                    rptName = "PKGFixturesGroup3A.rpt";
                    break;
                case "FIXT3B":
                    rptName = "PKGFixturesGroup3B.rpt";
                    break;
                //case "DEL1A":
                //    rptName = "PKGCompensationChecklistGroup1A.rpt";
                //    break;
                default:
                    rptName = "PKGFixturesGroup1.rpt";
                    break;
            }


            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGCompensationFixture.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();


            paramHHID.Name = "HHID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void pkgCrops(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "CRP3C":
                    rptName = "PKGCropsGroup3C.rpt";
                    break;
                case "CRP2":
                    rptName = "PKGCropsGroup2.rpt";
                    break;
                case "CRP3A":
                    rptName = "PKGCropsGroup3A.rpt";
                    break;
                case "CRP3B":
                    rptName = "PKGCropsGroup3B.rpt";
                    break;
                //case "DEL1A":
                //    rptName = "PKGCompensationChecklistGroup1A.rpt";
                //    break;
                default:
                    rptName = "PKGCropsGroup1.rpt";
                    break;
            }
            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName); //"PKGCropCompensation.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();



            paramHHID.Name = "HHID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();



            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;



            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void CompensationPackageChecklist(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "DEL3C":
                    rptName = "PKGCompensationChecklistGroup3C.rpt";
                    break;
                case "DEL2":
                    rptName = "PKGCompensationChecklistGroup2.rpt";
                    break;
                case "DEL3A":
                    rptName = "PKGCompensationChecklistGroup3A.rpt";
                    break;
                case "DEL3B":
                    rptName = "PKGCompensationChecklistGroup3B.rpt";
                    break;
                case "DEL1A":
                    rptName = "PKGCompensationChecklistGroup1A.rpt";
                    break;
                default:
                    rptName = "PKGCompensationChecklistGroup1.rpt";
                    break;
            }

            //CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/" + rptName);

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);

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
            ParameterField paramprintby = new ParameterField();

            paramHHID.Name = "HHID_";
            paramprintby.Name = "P_PrintedBy";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramprintbyVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramprintbyVal.Value = Session["userName"].ToString();

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramprintby.CurrentValues.Add(paramprintbyVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramprintby);

            CrystalReportViewer1.RefreshReport();
        }

        private void OptionDisclosureAgreement(string docCode)
        {


            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            switch (docCode)
            {
                case "OPDS2":
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup2.rpt");
                    break;
                case "OPDS3A":
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup3A.rpt");
                    break;
                case "OPDS3B":
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup3B.rpt");
                    break;
                case "OPDS3C":
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup3C.rpt");
                    break;
                case "OPDS1A":
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup1A.rpt");
                    break;
                default:
                    myRpt.Load(RPT_SOURCE + "PKGOptionDisclosureGroup1.rpt");
                    break;
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

            ParameterField paramHHID = new ParameterField();
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();


            paramHHID.Name = "HHID_";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);

            CrystalReportViewer1.RefreshReport();
        }

        private void DamagedCrop(string docCode)
        {
            string rptName = "";

            switch (docCode)
            {
                case "DCRP2":
                    rptName = "PKGDamagedCropsGroup2.rpt";
                    break;
                case "DCRP3A":
                    rptName = "PKGDamagedCropsGroup3A.rpt";
                    break;
                case "DCRP3B":
                    rptName = "PKGDamagedCropsGroup3B.rpt";
                    break;
                //case "DEL1A":
                //    rptName = "PKGCompensationChecklistGroup1A.rpt";
                //    break;
                default:
                    rptName = "PKGDamagedCropsGroup1.rpt";
                    break;
            }

            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGDamagedCropCompensation.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();


            paramHHID.Name = "D_HHID";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);



            CrystalReportViewer1.RefreshReport();
        }

        protected void PersonalIdentification(string docCode)
        {

            string rptName = "";

            switch (docCode)
            {
                case "PERID2":
                    rptName = "PKGPersonalIdentificationGroup2.rpt";
                    break;
                case "PERID3A":
                    rptName = "PKGPersonalIdentificationGroup3A.rpt";
                    break;
                case "PERID3B":
                    rptName = "PKGPersonalIdentificationGroup3B.rpt";
                    break;
                case "PERID3C":
                    rptName = "PKGPersonalIdentificationGroup3C.rpt";
                    break;
                case "PERID1A":
                    rptName = "PKGPersonalIdentificationGroup1A.rpt";
                    break;
                default:
                    rptName = "PKGPersonalIdentificationGroup1.rpt";
                    break;
            }

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PKGPersIdentification.rpt"

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
            ParameterField paramPrintedby = new ParameterField();

            paramHHID.Name = "HHID_";
            paramPrintedby.Name = "P_PrintedBy";

            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();

            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);

            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();

        }

        protected void PackagePaymentACK(string docCode)
        {

            string rptName = "";

            switch (docCode)
            {
                case "MNDOC2":
                    rptName = "PKGMainClosingDocumentsGroup2.rpt";
                    break;
                case "MNDOC3A":
                    rptName = "PKGMainClosingDocumentsGroup3A.rpt";
                    break;
                case "MNDOC3C":
                    rptName = "PKGMainClosingDocumentsGroup3C.rpt";
                    break;

                case "MNDOC3B":
                    rptName = "PKGMainClosingDocumentsGroup3B1.rpt";
                    break;
                case "MNDOC3B1":
                    rptName = "PKGMainClosingDocumentsGroup3B2.rpt";
                    break;
                case "MNDOC3B2":
                    rptName = "PKGMainClosingDocumentsGroup3B3.rpt";
                    break;
                case "MNDOC3B3":
                    rptName = "PKGMainClosingDocumentsGroup3B4.rpt";
                    break;
                case "MNDOC3B4":
                    rptName = "PKGMainClosingDocumentsGroup3B5.rpt";
                    break;

                case "MNDOC1A":
                    rptName = "PKGMainClosingDocumentsGroup1A.rpt";
                    break;
                default:
                    rptName = "PKGMainClosingDocumentsGroup1.rpt";
                    break;
            }
            string ProjectID = "0";
            if (Session["PROJECT_ID"] != null)
                ProjectID = Session["PROJECT_ID"].ToString();

            //myRpt = new ReportDocument();

            myRpt.Load(RPT_SOURCE + rptName);//"PkgPaymentACKNEW.rpt"

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
            ParameterField paramPrintedby = new ParameterField();
            ParameterField paramHHID_Comn = new ParameterField();
            ParameterField paramProject = new ParameterField();
            ParameterField paramHHID_ = new ParameterField();


            paramHHID_.Name = "HHID_";
            paramHHID.Name = "p_hhid";
            paramPrintedby.Name = "P_PrintedBy";
            paramHHID_Comn.Name = "HHID_COMN";
            paramProject.Name = "PROJECTID_";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHID_ComnVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramProjectVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramHHIDVal_ = new ParameterDiscreteValue();


            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramPrintedbyVal.Value = Session["userName"].ToString();
            paramHHID_ComnVal.Value = Convert.ToInt32(Session["HH_ID"]);
            paramProjectVal.Value = ProjectID;
            paramHHIDVal_.Value = Convert.ToInt32(Session["HH_ID"]);


            paramHHID.CurrentValues.Add(paramHHIDVal);
            paramPrintedby.CurrentValues.Add(paramPrintedbyVal);
            paramHHID_Comn.CurrentValues.Add(paramHHID_ComnVal);
            paramProject.CurrentValues.Add(paramProjectVal);
            paramHHID_.CurrentValues.Add(paramHHIDVal_);



            if (docCode == "MNDOC3B4" || docCode == "MNDOC3B3" || docCode == "MNDOC3B2" || docCode == "MNDOC3B5" || docCode == "MNDOC3B" || docCode == "MNDOC3B1")
            {
                ParamFields.Add(paramHHID_);
            }
            ParamFields.Add(paramHHID);
            ParamFields.Add(paramPrintedby);
            ParamFields.Add(paramHHID_Comn);
            ParamFields.Add(paramProject);

            //ParameterField paramHHID = new ParameterField();


            //paramHHID.Name = "p_hhid";


            //ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();

            //paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);

            //paramHHID.CurrentValues.Add(paramHHIDVal);

            //ParamFields.Add(paramHHID);

            CrystalReportViewer1.RefreshReport();

        }
        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", "window.close()", true);
            DisposePreviousReport();
        }
    }
}