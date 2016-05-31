using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using CrystalDecisions.Shared;
using WIS_BusinessObjects;
using System.Configuration;

namespace WIS
{
    public partial class ACCEPTANCECOUNT : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Acceptance Count Report";
                else
                    Master.PageHeader = "Acceptance Count Report";

                RadioButtonSummary.Checked = true;
                BindProject();
                DPFromDate.Attributes.Add("readonly","readonly");
                DPToDate.Attributes.Add("readonly", "readonly");
                if (Session["PROJECT_ID"] != null)
                {
                    if (ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                    {
                        ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;
                    }
                }
            }

            calDPFromDate.Format = UtilBO.DateFormat;
            CalDPToDate.Format = UtilBO.DateFormat;
        }
        /// <summary>
        /// To bind values to Project dropdownlist
        /// </summary>
        private void BindProject()
        {
            ProjectBLL BLLobj = new ProjectBLL();
            ddlProject.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProject.DataTextField = "projectName";
            ddlProject.DataValueField = "projectID";
            ddlProject.DataBind();
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlProject.ClearSelection();
            DPFromDate.Text = "";
            DPToDate.Text = "";
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}