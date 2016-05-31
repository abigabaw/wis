using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using WIS;

namespace WISDev
{
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Set page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
                GetRecentPAPS();
                Master.PageHeader = "Welcome to Wayleaves Information System";
                if (CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false
                    && CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkRecentPAPS.Enabled = false;
                    foreach (RepeaterItem item in rptRecentPAPS.Items)
                    {
                        LinkButton lnk = (LinkButton)item.FindControl("lnkPAP");
                        lnk.Enabled = false;
                    }
                }
                if (CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.UPLOAD_DOCUMENT) == false
                    && CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.UPLOAD_DOCUMENT) == false)
                {
                    hypUploadDoc.Enabled = false;
                }
                if (CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.PRIV_APPROVALS) == false
                    && CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_APPROVALS) == false)
                {
                    hypApprovals.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Bind Drop down data
        /// </summary>
        private void BindProjects()
        {
            //drpProject.DataSource = (new DashboardBLL()).GetProjects(Convert.ToInt32(Session["USER_ID"]));
            drpProject.DataSource = (new ProjectBLL()).GetProjects("", "", "", "", Convert.ToInt32(Session["USER_ID"]));
            drpProject.DataTextField = "ProjectName";
            drpProject.DataValueField = "ProjectID";
            drpProject.DataBind();

            if (drpProject.Items.Count > 0)
            {
                DSH_PAPStatusList PAPStatusList = (new DashboardBLL()).GetRecentProject(Convert.ToInt32(Session["USER_ID"]));

                if (PAPStatusList.Count > 0)
                {
                    DSH_PAPStatusBO objPAPStatusBO = PAPStatusList[0];

                    drpProject.ClearSelection();
                    if (drpProject.Items.FindByValue(objPAPStatusBO.ProjectId.ToString()) != null)
                        drpProject.Items.FindByValue(objPAPStatusBO.ProjectId.ToString()).Selected = true;

                    objPAPStatusBO = null;
                }

                GetPAPStatus();
                GetPAPStatusPie();
                GetPAPStatusSpline();
            }
            else
                phProjects.Visible = false;
        }


        /// <summary>
        /// Bind Chart data for Spiline
        /// </summary>
        private void GetPAPStatusSpline()
        {
            DashboardBLL objPAPStatusLogic = new DashboardBLL();
            DSH_PAPStatusList PAPStatusList = objPAPStatusLogic.GetProjectwisePAPBudgetForSpline(Convert.ToInt32(drpProject.SelectedValue));
            ProjectStatusSplineChart.Series["Series1"].Points.AddXY(0, 0);
            ProjectStatusSplineChart.Series["Series2"].Points.AddXY(0, 0);
            for (int i = 0; i < PAPStatusList.Count; i++)
            {
                ProjectStatusSplineChart.Series["Series1"].Points.AddXY(i + 1, PAPStatusList[i].est_value);
                ProjectStatusSplineChart.Series["Series2"].Points.AddXY(i + 1, PAPStatusList[i].expenseamount);            
            }
            //ProjectStatusSplineChart.Series["Series1"].Points.AddXY(1, 5);
            //ProjectStatusSplineChart.Series["Series1"].Points.AddXY(2, 15);
            //ProjectStatusSplineChart.Series["Series1"].Points.AddXY(3, 10);
            //ProjectStatusSplineChart.Series["Series2"].Points.AddXY(0, 0);
            //ProjectStatusSplineChart.Series["Series2"].Points.AddXY(1, 10);
            //ProjectStatusSplineChart.Series["Series2"].Points.AddXY(2, 8);
            //ProjectStatusSplineChart.Series["Series2"].Points.AddXY(3, 20);
            for (int i = 0; i < PAPStatusList.Count + 1; i++)
            {
                CustomLabel lbl = new CustomLabel();
                if (i == 0)
                    lbl.Text = "";
                else
                    lbl.Text = PAPStatusList[i - 1].BudDate.Substring(0,3);
                //else if (i == 1)
                //    lbl.Text = "Feb";
                //else if (i == 2)
                //    lbl.Text = "Mar";
                //else if (i == 3)
                //    lbl.Text = "Apr";

                lbl.FromPosition = i - 0.5;
                lbl.ToPosition = i + 0.5;

                ProjectStatusSplineChart.ChartAreas[0].AxisX.CustomLabels.Add(lbl);
                ProjectStatusSplineChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;

            }

            ProjectStatusSplineChart.ChartAreas["ChartArea1"].AxisX.IsStartedFromZero = true;
            ProjectStatusSplineChart.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            ProjectStatusSplineChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ProjectStatusSplineChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        /// <summary>
        /// Bind Chart data for Pie
        /// </summary>
        private void GetPAPStatusPie()
        {
            DashboardBLL objPAPStatusLogic = new DashboardBLL();
            //ProjectStatusPieChart.Legends.Add(new Legend("ProjectStatus"));
            ProjectStatusPieChart.Series[0].XValueMember = "ProjectStatus";
            ProjectStatusPieChart.Series[0].YValueMembers = "StatuCount";
            ProjectStatusPieChart.Series[0].Label = "#PERCENT{P0}";
            ProjectStatusPieChart.Series[0].Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            ProjectStatusPieChart.Series[0].Legend = "ProjectStatus";
            DSH_PAPStatusList PAPStatusList = objPAPStatusLogic.GetProjectwisePAPStatusForPie();
            ProjectStatusPieChart.DataSource = PAPStatusList;
            ProjectStatusPieChart.DataBind();
            for (int i = 0; i < PAPStatusList.Count; i++)
            {
                DataPoint pt = ProjectStatusPieChart.Series[0].Points[i];
                if (PAPStatusList[i].ProjectStatus.ToString().ToUpper() == "IN PROGRESS")
                {
                    pt.LegendText = "In Progress";
                    pt.Color = Color.Orange;
                }
                else if (PAPStatusList[i].ProjectStatus.ToString().ToUpper() == "COMPLETED")
                {
                    pt.LegendText = "Completed";
                    pt.Color = Color.Green;
                }
                else if (PAPStatusList[i].ProjectStatus.ToString().ToLower() == "new")
                {
                    pt.LegendText = "New";
                    pt.Color = Color.Blue;
                }
                else
                {
                    pt.LegendText = PAPStatusList[i].ProjectStatus.ToString();
                }
            }
        }

        /// <summary>
        /// Bind Reacent paps data
        /// </summary>
        private void GetRecentPAPS()
        {
            DashboardBLL objRecentPAPSLogic = new DashboardBLL();
            DSH_RecentPAPSList RecentPAPS = objRecentPAPSLogic.GetRecentPAPSByUser(Convert.ToInt32(Session["USER_ID"]));

            rptRecentPAPS.DataSource = RecentPAPS;
            rptRecentPAPS.DataBind();
        }

        /// <summary>
        /// Bind Chart data for Bar
        /// </summary>
        private void GetPAPStatus()
        {
            DashboardBLL objPAPStatusLogic = new DashboardBLL();

            PAPStatusChart.Series["Series1"].XValueMember = "ProjectName";
            PAPStatusChart.Series["Series1"].YValueMembers = "PAPCount";
            PAPStatusChart.Series["Series2"].XValueMember = "ProjectName";
            PAPStatusChart.Series["Series2"].YValueMembers = "PAPPaidCount";
            PAPStatusChart.Series["Series3"].XValueMember = "ProjectName";
            PAPStatusChart.Series["Series3"].YValueMembers = "PAPPendingPayCount";
            //PAPStatusChart.Series["Series1"]["PixelPointWidth"] = "20";
            //PAPStatusChart.Series["Series2"]["PixelPointWidth"] = "20";
            //PAPStatusChart.Series["Series3"]["PixelPointWidth"] = "20";
            //PAPStatusChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            PAPStatusChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
            PAPStatusChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

            PAPStatusChart.DataSource = objPAPStatusLogic.GetProjectwisePAPStatus(Convert.ToInt32(drpProject.SelectedValue));
            PAPStatusChart.DataBind();
        }

        /// <summary>
        /// set Url to Links
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptRecentPAPS_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string args = e.CommandArgument.ToString();
                string[] argsArray = args.Split(',');

                Session["PROJECT_ID"] = argsArray[0];
                Session["HH_ID"] = argsArray[1];
                Session["PROJECT_CODE"] = argsArray[2];
                CheckProjectFrozen();

                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Household.aspx"));
            }
        }

        /// <summary>
        /// check project is frozen or not
        /// </summary>
        public void CheckProjectFrozen()
        {
            ProjectBLL ObjProjectBLL = new ProjectBLL();
            ProjectBO ObjProjectBO = new ProjectBO();

            ObjProjectBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);

            ObjProjectBO = ObjProjectBLL.getFrozen(ObjProjectBO);

            Session["FROZEN"] = null;
            if ((ObjProjectBO) != null)
            {
                Session["FROZEN"] = ObjProjectBO.Frozen;
            }
        }

        /// <summary>
        /// Call GetPAPStatus,GetPAPStatusPie,GetPAPStatusSpline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPAPStatus();
            GetPAPStatusPie();
            GetPAPStatusSpline();
        }
    }
}
