using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using CrystalDecisions.Shared;
using System.Collections;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class MonthlyProjectExpenditure : System.Web.UI.Page
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
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Monthly Project Expenditure Report";
                else
                    Master.PageHeader = "Monthly Project Expenditure Report";
                //Master.PageHeader = "Monthly Project Expenditure Report";
                ddlProjectCode.Attributes.Add("onchange", "ddlProjectCode_IndexChanged(this);");
                ddlProjectName.Attributes.Add("onchange", "ddlProjectCode_IndexChanged(this);");
                BindProjectCode();
                BindProjectName();
                BindYear();
            }
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindProjectCode()
        {
            //List<MonthlyProjectExpenditureBO> ProjectCodeList = new List<MonthlyProjectExpenditureBO>();
            //MonthlyProjectExpenditureBLL MonthlyProjectExpenditureBLLObj = new MonthlyProjectExpenditureBLL();
            ProjectBLL BLLobj = new ProjectBLL();
            //ProjectCodeList=MonthlyProjectExpenditureBLLObj.LoadProjectCode();
            ddlProjectCode.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProjectCode.DataTextField = "ProjectCode";
            ddlProjectCode.DataValueField = "projectID";
            ddlProjectCode.DataBind();
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindProjectName()
        {
            ProjectBLL BLLobj = new ProjectBLL();
            ddlProjectName.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProjectName.DataTextField = "projectName";
            ddlProjectName.DataValueField = "projectID";
            ddlProjectName.DataBind();
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindYear()
        {
            ArrayList YearList = new ArrayList();
            string CurrentYear =Convert.ToString(System.DateTime.Now.Year);
            int Ival = System.DateTime.Now.Year - 2000;
            for (int i = 0; i <= Ival; i++)
            {
                YearList.Add(CurrentYear);
                ddlYear.Items.Add(CurrentYear);
                CurrentYear = (Convert.ToInt32(CurrentYear) - 1).ToString();
            }           
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ListItem lstItem = null;
            lstItem = ddlYear.Items[0];
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlMonth.Items[0];
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlProjectName.Items[0];
            ddlProjectName.Items.Clear();
            ddlProjectName.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            

            ddlProjectCode.ClearSelection();
        }
        /// <summary>
        /// Calls loadreport method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        /// <summary>
        /// to load report
        /// </summary>
        private void LoadReport()
        {
            ConnectionInfo ConnInfo = new ConnectionInfo();
            ConnInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_ServerName");
            ConnInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_DatabaseName");
            ConnInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_UserID");
            ConnInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Password");

           

            CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/MonthlyProjectExpenditure.rpt");

            foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }
            CrystalReportViewer1.ParameterFieldInfo.Clear();


            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

            ParameterField paramPrjectCode = new ParameterField();
            ParameterField paramPrjectName = new ParameterField();
            ParameterField paramMonth = new ParameterField();
            ParameterField paramYear = new ParameterField();
            ParameterField paramPrintedBy = new ParameterField();


            paramPrjectCode.Name = "P_ProjectCode";
            paramPrjectName.Name = "P_ProjectName";
            paramMonth.Name = "P_Month";
            paramYear.Name = "P_Year";
            paramPrintedBy.Name = "P_PrintedBy";


            ParameterDiscreteValue paramPrjectCodeVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrjectNameVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramMonthVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramYearVal = new ParameterDiscreteValue();
            ParameterDiscreteValue paramPrintedByVal = new ParameterDiscreteValue();

            if (ddlProjectCode.SelectedIndex > 0)
                paramPrjectCodeVal.Value = ddlProjectCode.SelectedItem.Text;
            else
                paramPrjectCodeVal.Value = null;

          //  paramPrjectNameVal.Value = ddlProjectName.SelectedItem.Value;
            if (ddlProjectName.SelectedIndex > 0)
                paramPrjectNameVal.Value = ddlProjectName.SelectedItem.Text;
            else
                paramPrjectNameVal.Value = null;

            if (ddlMonth.SelectedIndex > 0)
                paramMonthVal.Value = ddlMonth.SelectedItem.Text;
            else
                paramMonthVal.Value = null;

            if (ddlYear.SelectedIndex > 0)
                paramYearVal.Value = ddlYear.SelectedItem.Text;
            else
                paramYearVal.Value = null;

            paramPrintedByVal.Value = Session["userName"].ToString();

            paramPrjectCode.CurrentValues.Add(paramPrjectCodeVal);
            paramPrjectName.CurrentValues.Add(paramPrjectNameVal);
            paramMonth.CurrentValues.Add(paramMonthVal);
            paramYear.CurrentValues.Add(paramYearVal);
            paramPrintedBy.CurrentValues.Add(paramPrintedByVal);


            ParamFields.Add(paramPrjectCode);
            ParamFields.Add(paramPrjectName);
            ParamFields.Add(paramMonth);
            ParamFields.Add(paramYear);
            ParamFields.Add(paramPrintedBy);
           

            CrystalReportViewer1.RefreshReport();
        }
       
         

        
    }
   
}