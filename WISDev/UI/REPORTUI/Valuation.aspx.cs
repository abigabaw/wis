using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace WIS
{
    public partial class Valuation : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation Report";
                else
                    Master.PageHeader = "Valuation Report";
                BindDistricts();
                BindProject();
                if (Request.QueryString["ProjectID"] != null)
                {
                    ddlDistrict.ClearSelection();
                    string Distict = Request.QueryString["Distict"].ToString();
                    string County = Request.QueryString["County"].ToString();
                    string SubCounty = Request.QueryString["SubCounty"].ToString();
                    string Parish = Request.QueryString["Parish"].ToString();
                    string Village = Request.QueryString["Village"].ToString();
                    if (ddlDistrict.Items.FindByValue(Distict) != null)
                        ddlDistrict.Items.FindByValue(Distict).Selected = true;
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        BindCounties(ddlDistrict.SelectedItem.Value);
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByValue(County) != null)
                            ddlCounty.Items.FindByValue(County).Selected = true;
                        if (ddlCounty.SelectedIndex > 0)
                        {
                            BindSubCounties(ddlCounty.SelectedItem.Value);
                            uplSubCounty.Update();
                            ddlSubCounty.ClearSelection();
                            if (ddlSubCounty.Items.FindByValue(SubCounty) != null)
                                ddlSubCounty.Items.FindByValue(SubCounty).Selected = true;
                            if (ddlSubCounty.SelectedIndex > 0)
                            {
                                BindVillages(ddlSubCounty.SelectedItem.Value);
                                ddlVillage.ClearSelection();
                                uplVillage.Update();
                                if (ddlVillage.Items.FindByValue(Village) != null)
                                    ddlVillage.Items.FindByValue(Village).Selected = true;
                                BindParishes(ddlSubCounty.SelectedItem.Value);
                                uplParish.Update();
                                ddlParish.ClearSelection();
                                if (ddlParish.Items.FindByValue(Parish) != null)
                                    ddlParish.Items.FindByValue(Parish).Selected = true;
                            }
                        }
                    }
                    txtPAPName.Text = Session["SelPAPName"].ToString();
                    //pl.Text = Session["SelPlotreference"].ToString();
                }   
            }
        }
        /// <summary>
        /// To search PAP details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx?ProjectID=" + ddlProject.SelectedValue + "&Distict=" + ddlDistrict.SelectedValue + "&County=" + ddlCounty.SelectedValue + "&SubCounty=" + ddlSubCounty.SelectedValue + "&Parish=" + ddlParish.SelectedValue + "&Village=" + ddlVillage.SelectedValue + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindProject()
        {
            ProjectBLL BLLobj = new ProjectBLL();
            ddlProject.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProject.DataTextField = "projectName";
            ddlProject.DataValueField = "projectID";
            ddlProject.DataBind();

            if (Session["PROJECT_ID"] != null)
            {
                if (ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                {
                    ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;
                }
            }
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindParishes(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);
            ddlParish.Items.Clear();

            if (subCounty != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCounty);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishID";
                ddlParish.DataBind();
            }

            ddlParish.Items.Insert(0, firstListItem);
            ddlParish.SelectedIndex = 0;
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindVillages(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);
            ddlVillage.Items.Clear();

            if (subCounty != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }
            ddlVillage.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);
            ddlSubCounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataBind();
            }
            ddlSubCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlCounty.Items.Clear();

            if (districtID != "0")
            {
                ddlCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();
            }
            ddlCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPAPName.Text = "";

            ListItem lstItem = null;

            lstItem = ddlVillage.Items[0];
            ddlVillage.Items.Clear();
            ddlVillage.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlSubCounty.Items[0];
            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlParish.Items[0];
            ddlParish.Items.Clear();
            ddlParish.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlCounty.Items[0];
            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlDistrict.ClearSelection();
            ddlProject.ClearSelection();
        }
        /// <summary>
        /// Calls loadreport method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
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

        //    string RPT_SOURCE = System.Configuration.ConfigurationManager.AppSettings.Get("Rpt_Source");

        //    ReportDocument myRpt = new ReportDocument();

        //    myRpt.Load(RPT_SOURCE + "Valuation.rpt");
        //    myRpt.SetDatabaseLogon(ConnInfo.UserID, ConnInfo.Password, ConnInfo.ServerName, ConnInfo.DatabaseName);

        //    foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myRpt.Database.Tables)
        //    {
        //        TableLogOnInfo logInfo = myTable.LogOnInfo;
        //        logInfo.ConnectionInfo = ConnInfo;
        //        myTable.ApplyLogOnInfo(logInfo);
        //    }

        //    CrystalReportViewer1.ReportSource = myRpt;
            
        //    CrystalReportViewer1.ParameterFieldInfo.Clear();            

        //    ParameterFields ParamFields = CrystalReportViewer1.ParameterFieldInfo;

        //    ParameterField paramDistrict = new ParameterField();
        //    ParameterField paramCounty = new ParameterField();
        //    ParameterField paramSubCounty = new ParameterField();
        //    ParameterField paramParish = new ParameterField();
        //    ParameterField paramVillage = new ParameterField();
        //    ParameterField paramPapName = new ParameterField();
        //    ParameterField paramProjectCode = new ParameterField();
        //    ParameterField paramPrintedby = new ParameterField();

        //    paramDistrict.Name = "district_";
        //    paramCounty.Name = "county_";
        //    paramSubCounty.Name = "subcounty_";
        //    paramParish.Name = "parish_";
        //    paramVillage.Name = "village_";
        //    paramPapName.Name = "papname_";
        //    paramProjectCode.Name = "P_PROJECTCODE";
        //    paramPrintedby.Name = "P_PrintedBy";

        //    ParameterDiscreteValue paramDistrictVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramCountyVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramSubCountyVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramParishVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramVillageVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramPapNameVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramProjectCodeVal = new ParameterDiscreteValue();
        //    ParameterDiscreteValue paramPrintedbyVal = new ParameterDiscreteValue();

        //    if (ddlDistrict.SelectedIndex > 0)
        //        paramDistrictVal.Value = ddlDistrict.SelectedItem.Text;
        //    else
        //        paramDistrictVal.Value = "";

        //    if (ddlCounty.SelectedIndex > 0)
        //        paramCountyVal.Value = ddlCounty.SelectedItem.Text;
        //    else
        //        paramCountyVal.Value = "";

        //    if (ddlSubCounty.SelectedIndex > 0)
        //        paramSubCountyVal.Value = ddlSubCounty.SelectedItem.Text;
        //    else
        //        paramSubCountyVal.Value = "";

        //    if (ddlParish.SelectedIndex > 0)
        //        paramParishVal.Value = ddlParish.SelectedItem.Text;
        //    else
        //        paramParishVal.Value = "";

        //    if (ddlVillage.SelectedIndex > 0)
        //        paramVillageVal.Value = ddlVillage.SelectedItem.Text;
        //    else
        //        paramVillageVal.Value = "";


        //    if (txtPAPName.Text != string.Empty)
        //    {
        //        paramPapNameVal.Value = txtPAPName.Text;
        //    }
        //    else
        //    {
        //        paramPapNameVal.Value = string.Empty;
        //    }

        //    paramProjectCodeVal.Value = Session["PROJECT_CODE"].ToString();
        //    paramPrintedbyVal.Value = Session["userName"].ToString();

        //    paramDistrict.CurrentValues.Add(paramDistrictVal);
        //    paramCounty.CurrentValues.Add(paramCountyVal);
        //    paramSubCounty.CurrentValues.Add(paramSubCountyVal);
        //    paramParish.CurrentValues.Add(paramParishVal);
        //    paramVillage.CurrentValues.Add(paramVillageVal);
        //    paramPapName.CurrentValues.Add(paramPapNameVal);
        //    paramProjectCode.CurrentValues.Add(paramProjectCodeVal);
        //    paramPrintedby.CurrentValues.Add(paramPrintedbyVal);

        //    ParamFields.Add(paramDistrict);
        //    ParamFields.Add(paramCounty);
        //    ParamFields.Add(paramSubCounty);
        //    ParamFields.Add(paramParish);
        //    ParamFields.Add(paramVillage);
        //    ParamFields.Add(paramPapName);
        //    ParamFields.Add(paramProjectCode);
        //    ParamFields.Add(paramPrintedby);

        //    CrystalReportViewer1.RefreshReport();
        //}
    }
}