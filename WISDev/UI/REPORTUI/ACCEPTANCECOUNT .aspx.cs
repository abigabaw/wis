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

/**
 * 
 * @version		 
  * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 
 * @Created Date 
 * @Updated By
 * @Updated Date
 */

namespace WIS
{
    public partial class ACCEPTANCECOUNT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Acceptance Count Report";
                BindProject();
                GetProjectDetails();

                string projectName = string.Empty;
                string projectStartDate = string.Empty;
                string projectEndDate = string.Empty;
                string projectStatus = string.Empty;


                ProjectBLL BLLobj = new ProjectBLL();
                ProjectList Projects = new ProjectList();
                Projects = BLLobj.GetProjects(projectName, projectStartDate, projectEndDate, projectStatus);
                string ProjectCode = Session["PROJECT_ID"].ToString();
                if (BLLobj != null)
                {
                    for (int i = 0; i < Projects.Count; i++)
                    {
                        if ((ProjectCode) == (Projects[i].ProjectID.ToString()))
                        {
                            if (ddlProject.Items.FindByValue(Projects[i].ProjectID.ToString()) != null)
                            {
                                ddlProject.Items.FindByValue(Projects[i].ProjectID.ToString()).Selected = true;
                                ddlProject.Enabled = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void GetProjectDetails()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));
            ddlProject.Text = objProject.ProjectCode;
        }

        private void BindProject()
        {
            string projectName = string.Empty;
            string projectStartDate = string.Empty;
            string projectEndDate = string.Empty;
            string projectStatus = string.Empty;

            ProjectBLL BLLobj = new ProjectBLL();
            ProjectList Projects = new ProjectList();
            Projects = BLLobj.GetProjects(projectName, projectStartDate, projectEndDate, projectStatus);
            ddlProject.DataSource = BLLobj.GetProjects(projectName, projectStartDate, projectEndDate, projectStatus);
            ddlProject.DataTextField = "projectName";
            ddlProject.DataValueField = "projectID";
            ddlProject.DataBind();


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlProject.ClearSelection();
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}