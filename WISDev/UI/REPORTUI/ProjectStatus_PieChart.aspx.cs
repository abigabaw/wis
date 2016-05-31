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
    public partial class ProjectStatus_PieChart : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            calopsStartDate.Format = UtilBO.DateFormat;
            CalopsEndDate.Format = UtilBO.DateFormat;
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Project Status PieChart Report";
                else
                    Master.PageHeader = "Project Status PieChart Report";
            }
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlProjectStatus.ClearSelection();
            opsStartDate.Text = "";
            opsEndDate.Text = "";
        }

        
    }
}