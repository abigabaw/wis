using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.COMPENSATION
{
    public partial class PopUpPAPList : System.Web.UI.Page
    {
        /// <summary>
        /// set page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - PAP List";
                }
                else
                    Master.PageHeader = "PAP List";
            }
            HiddenField HFPouUP = (HiddenField)PAPListUC1.FindControl("HFPouUP");
            HFPouUP.Value = "Yes";
        }

        /// <summary>
        /// set query string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            bool handled = false;

            if (e is GridViewCommandEventArgs)
            {
                GridViewCommandEventArgs ce = (GridViewCommandEventArgs)e;

                //OnItemCommand(ce);
                handled = true;
                if (Request.QueryString["ProjectID"] != null)
                {
                    //string param = string.Format("AfterSelectPAPForReports({0},{1},{2},'{3}','{4}','{5}');", Request.QueryString["ProjectID"].ToString(), Request.QueryString["Distict"].ToString(), Request.QueryString["County"].ToString(), Request.QueryString["SubCounty"].ToString(), Request.QueryString["Parish"].ToString(), Request.QueryString["Village"].ToString());
                    string district = "0";
                    string county = "0";
                    string subCounty = "0";
                    string parish = "0";
                    string village = "0";
                    DropDownList ddlDistrict = (DropDownList)PAPListUC1.FindControl("ddlDistrict");
                    DropDownList ddlCounty = (DropDownList)PAPListUC1.FindControl("ddlCounty");
                    DropDownList ddlSubCounty = (DropDownList)PAPListUC1.FindControl("ddlSubCounty");
                    DropDownList ddlParish = (DropDownList)PAPListUC1.FindControl("ddlParish");
                    DropDownList ddlVillage = (DropDownList)PAPListUC1.FindControl("ddlVillage");
                    if (ddlDistrict.SelectedIndex > 0) district = ddlDistrict.SelectedValue;
                    if (ddlCounty.SelectedIndex > 0) county = ddlCounty.SelectedValue;
                    if (ddlSubCounty.SelectedIndex > 0) subCounty = ddlSubCounty.SelectedValue;
                    if (ddlParish.SelectedIndex > 0) parish = ddlParish.SelectedValue;
                    if (ddlVillage.SelectedIndex > 0) village = ddlVillage.SelectedValue;

                    string param = string.Format("AfterSelectPAPForReports({0},{1},{2},'{3}','{4}','{5}');", Request.QueryString["ProjectID"].ToString(), district, county, subCounty, parish, village);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AfterSelectPAPForReports", param, true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AfterSelectPAP", "AfterSelectPAP();", true);
            }
            return handled;
        }
    }
}