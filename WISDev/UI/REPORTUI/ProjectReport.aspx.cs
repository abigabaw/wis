using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using CrystalDecisions.Shared;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class ProjectReport : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = " Project Report";
                Bindconsultantname();
            }
            caldpProjStartDate.Format = UtilBO.DateFormat;
            CaldpProjEndDate.Format = UtilBO.DateFormat;

        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void Bindconsultantname()
        {
            ConsultantBO objCon = new ConsultantBO();
            ConsultantBLL objConBLL = new ConsultantBLL();
            ddlconsultantname.DataSource = objConBLL.GetConsultant(0);
            ddlconsultantname.DataTextField = "ConsultName";
            ddlconsultantname.DataValueField = "ConsultID";
            ddlconsultantname.DataBind();
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            projectcodeTextBox.Text = string.Empty;
            txtProjectName.Text = string.Empty;
            ddlconsultantname.ClearSelection();
            //consultantnameTextBox.Text = string.Empty;
            ddlProjectStatus.ClearSelection();
            dpProjStartDate.Text = "";
            dpProjEndDate.Text = "";
           
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            //LoadReport();
        }

        //private void LoadReport()
        //{
        //    ConnectionInfo ConnInfo = new ConnectionInfo();
        //    ConnInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_ServerName");
        //    ConnInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_DatabaseName");
        //    ConnInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_UserID");
        //    ConnInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Password");

        //    CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/ProjectReport.rpt");
        //    foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
        //    {
        //        cnInfo.ConnectionInfo = ConnInfo;
        //    }
        //    CrystalReportViewer1.ParameterFieldInfo.Clear();

        //    ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

        //    ParameterField paramProjectName = new ParameterField();
        //    ParameterField paramProjectstartdate = new ParameterField();
        //    ParameterField paramProjectEnddate = new ParameterField();
        //    ParameterField paramProjectstatus = new ParameterField();
        //    ParameterField paramHHID = new ParameterField();
        //    ParameterField paramProjectcode = new ParameterField();
        //    ParameterField paramconsultname = new ParameterField();
        //    ParameterField paramPrintedby = new ParameterField();

        //    paramProjectName.Name = "PROJECTNAME_";
        //    paramProjectstartdate.Name = "PROJECTSTARTDATE_";
        //    paramProjectEnddate.Name = "PROJECTENDDATE_";
        //    paramProjectstatus.Name = "PROJECTSTATUS_";
        //    paramHHID.Name = "HHID_COMN";
        //    paramProjectcode.Name = "P_PROJECTCODE";
        //    paramconsultname.Name = "CONSULTANTNAME_";
        //    paramPrintedby.Name = "P_PrintedBy";

        //    ParameterDiscreteValue paramProjectNameVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramProjectstartdateVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramProjectEnddateVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramProjectstatusVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramProjectcodeVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramconsultnameVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();


        //    if (txtProjectName.Text != string.Empty)
        //    {
        //        paramProjectNameVal.Value = txtProjectName.Text;
        //    }
        //    else
        //    {
        //        paramProjectNameVal.Value = string.Empty;
        //    }


        //    if (ddlProjectStatus.SelectedIndex > 0)
        //        paramProjectstatusVal.Value = ddlProjectStatus.SelectedItem.Text;
        //    else
        //        paramProjectstatusVal.Value = "";


        //    paramProjectcodeVal.Value = Session["PROJECT_CODE"].ToString();
        //    paramPrintedbyVal.Value = Session["userName"].ToString();
        //    paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
        //    if (consultantnameTextBox.Text != string.Empty)
        //    {
        //        paramconsultnameVal.Value = consultantnameTextBox.Text;
        //    }
        //    else
        //    {
        //        paramconsultnameVal.Value = string.Empty;
        //    }
        //    DateTime FromDate = Convert.ToDateTime(dpProjStartDate.CalendarDate);
        //    DateTime ToDate = Convert.ToDateTime(dpProjEndDate.CalendarDate);


        //    paramProjectName.CurrentValues.Add(paramProjectNameVal);
        //    paramProjectstartdate.CurrentValues.Add(paramProjectstartdateVal);
        //    paramProjectEnddate.CurrentValues.Add(paramProjectEnddateVal);
        //    paramProjectstatus.CurrentValues.Add(paramProjectstatusVal);
        //    paramHHID.CurrentValues.Add(paramHHIDVal);
        //    paramProjectcode.CurrentValues.Add(paramProjectcodeVal);
        //    paramconsultname.CurrentValues.Add(paramconsultnameVal);
        //    paramPrintedby.CurrentValues.Add(paramPrintedbyVal);

        //    ParamFields.Add(paramProjectName);
        //    ParamFields.Add(paramProjectstartdate);
        //    ParamFields.Add(paramProjectEnddate);
        //    ParamFields.Add(paramProjectstatus);
        //    ParamFields.Add(paramHHID);
        //    ParamFields.Add(paramProjectcode);
        //    ParamFields.Add(paramconsultname);
        //    ParamFields.Add(paramPrintedby);
        //}
    }
}