using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;

namespace WIS
{
    public partial class PackagePaymentAcknowledgement : System.Web.UI.Page
    {
        /// <summary>
        /// Set Connection Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            }

            ConnectionInfo ConnInfo = new ConnectionInfo();
            ConnInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_ServerName");
            ConnInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_DatabaseName");
            ConnInfo.UserID = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_UserID");
            ConnInfo.Password = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Password");



            CrystalReportViewer1.ReportSource = ResolveUrl("~/REPORTS/PkgPaymentACK.rpt");
            
           

            foreach (TableLogOnInfo cnInfo in CrystalReportViewer1.LogOnInfo)
            {
                cnInfo.ConnectionInfo = ConnInfo;
            }
            CrystalReportViewer1.ParameterFieldInfo.Clear();

            ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;
            ParameterField paramHHID = new ParameterField();
           // ParameterField paramPrintedby = new ParameterField();



            paramHHID.Name = "p_hhid";
            //paramPrintedby.Name = "P_PrintedBy";


            ParameterDiscreteValue paramHHIDVal = new ParameterDiscreteValue();
           // ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();

            paramHHIDVal.Value = Convert.ToInt32(Session["HH_ID"]);
           // paramPrintedbyVal.Value = Session["userName"].ToString();

            paramHHID.CurrentValues.Add(paramHHIDVal);
           // paramPrintedby.CurrentValues.Add(paramPrintedbyVal);


            ParamFields.Add(paramHHID);
           //  ParamFields.Add(paramPrintedby);

            CrystalReportViewer1.RefreshReport();

        }
    }
}