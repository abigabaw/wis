using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class RouteIdentification : System.Web.UI.Page
    {

        protected RouteCriteriaScoreList lstRouteCriteriaScore;
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Route Selection Criteria";

                ClearDetails();
                LoadRoutes();
                findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));

                if (Session["FROZEN"] != null)
                {
                    if (Session["FROZEN"].ToString() == "Y")
                    {
                        btnSave.Visible = false;
                        btnClear.Visible = false;
                    }
                }

                getFinalRouteApprovalDetial(Convert.ToInt32(Session["PROJECT_ID"]));

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
            }
        }
        /// <summary>
        /// To get FinalRoute Approval Detial
        /// </summary>
        /// <param name="ProjectId"></param>
        public void getFinalRouteApprovalDetial(int ProjectId)
        {

            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteList ProjectRouteList = new ProjectRouteList();

            objProjectRoute.Project_Id = ProjectId;

            ProjectRouteList = objProjectRouteBLL.getFinalRouteApprovalDetial(objProjectRoute);

            //ProjectRouteList = objProjectRouteBLL.getFinalRouteApprovalDetial(objProjectRoute);

            if ((ProjectRouteList.Count) > 0)
            {
                for (int i = 0; i < ProjectRouteList.Count; i++)
                {
                    if (ProjectRouteList[i].IsFinal == "TRUE")
                    {
                        if (ProjectRouteList[i].ApprovedstatusID == 1)
                        {
                            btnSave.Visible = false;
                            btnClear.Visible = false;
                        }
                    }
                    else
                    {
                        if (ProjectRouteList[i].ApprovedstatusID == 3)
                        {
                            btnSave.Visible = false;
                            btnClear.Visible = false;
                        }
                        if (ProjectRouteList[i].ApprovedstatusID == 2)
                        {
                            btnSave.Visible = true;
                            btnClear.Visible = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// To find Route Status after Save
        /// </summary>
        /// <param name="ProjectId"></param>
        public void findRouteStatusafterSave(int ProjectId)
        {
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            objProjectRoute.Project_Id = ProjectId;

            objProjectRoute = objProjectRouteBLL.findRouteStatusafterSave(objProjectRoute);
            if ((objProjectRoute) != null)
            {
                if (objProjectRoute.ApprovedstatusID == 3)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
            }
        }
        /// <summary>
        /// To enable route
        /// </summary>
        /// <param name="YesNo"></param>
        private void RouteEnabled(bool YesNo)
        {
            if (YesNo)
            {
                pnlFactorsData.Enabled = true;
                pnlSave.Visible = true;
            }
            else
            {
                pnlFactorsData.Enabled = false;
                pnlSave.Visible = false;
            }
        }
        /// <summary>
        /// To load routes
        /// </summary>
        public void LoadRoutes()
        {
            ProjectRouteBLL oProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteList lstProjectRouteList = new ProjectRouteList();

            try
            {
                int ProjectId = 0;
                if (Request.QueryString.Count == 0)
                {
                    if (Session["PROJECT_ID"] != null)
                    {
                        ProjectId = Convert.ToInt32(Session["PROJECT_ID"].ToString());
                        RouteEnabled(true);
                    }
                }
                else
                {
                    ProjectId = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());
                    RouteEnabled(false);
                }

                lstProjectRouteList = oProjectRouteBLL.GetProjectRoutebyId(ProjectId);

                rptRoute.DataSource = lstProjectRouteList;
                rptRoute.DataBind();

                FindSum();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            for (int i = 0; i < rptRoute.Items.Count; i++)
            {
                Panel p = (Panel)rptRoute.Items[i].FindControl("pnlspace");
                if (p != null)
                {
                    if (i == 0)
                        p.Visible = true;
                    else
                        p.Visible = false;
                }
            }
        }

        #region Clear Button & Methods
        /// <summary>
        /// calls cleardetails method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        /// <summary>
        /// Clears input fields and loads default data
        /// </summary>
        protected void ClearDetails()
        {
            for (int k = 0; k < rptRoute.Controls.Count; k++)
            {
                Repeater rptFactorsX = rptRoute.Controls[k].FindControl("rptFactors") as Repeater;

                //setting the Route Score Textbox=0
                //if (rptFactorsX.Controls[k].FindControl("txtRouteScore") is TextBox)
                //{
                //    (rptFactorsX.Controls[k].FindControl("txtRouteScore") as TextBox).Text = "0";
                //}

                for (int i = 0; i < rptFactorsX.Controls.Count; i++)
                {
                    //setting the Factor Score Textbox=0
                    //if (rptFactorsX.Controls[i].FindControl("txtFactorScore") is TextBox)
                    //{
                    //    (rptFactorsX.Controls[i].FindControl("txtFactorScore") as TextBox).Text = "0";
                    //}

                    if (rptFactorsX.Controls[i].FindControl("rptCriteria") is Repeater)
                    {
                        Repeater rptCriteriaX = rptFactorsX.Controls[i].FindControl("rptCriteria") as Repeater;

                        for (int j = 0; j < rptCriteriaX.Controls.Count; j++)
                        {
                            if (rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") is DropDownList)
                            {
                                //setting the DropDownList Index=0
                                DropDownList ddlCriteriaScoreX = rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") as DropDownList;
                                ddlCriteriaScoreX.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            FindSum();
        }

        #endregion
        /// <summary>
        ///Finding Sum of Criteria = Factors
        ///Finding Sum Factors = Route 
        /// </summary>
        private void FindSum()
        {
            //Finding Sum of Criteria = Factors
            //Finding Sum Factors = Route
            //Repeat Thorugh Routes
            for (int k = 0; k < rptRoute.Controls.Count; k++)
            {
                int SumFactors = 0;
                Repeater rptFactors = rptRoute.Controls[k].FindControl("rptFactors") as Repeater;

                //Repeat Through Factors
                for (int i = 0; i < rptFactors.Controls.Count; i++)
                {
                    if (rptFactors.Controls[i].FindControl("rptCriteria") is Repeater)
                    {
                        Repeater rptCriteriaX = rptFactors.Controls[i].FindControl("rptCriteria") as Repeater;
                        int SumCriteria = 0;
                        //Finding Sum of Criteria
                        for (int j = 0; j < rptCriteriaX.Controls.Count; j++)
                        {
                            if (rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") is DropDownList)
                            {
                                DropDownList ddlCriteriaScoreX = rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") as DropDownList;
                                SumCriteria += Convert.ToInt32(ddlCriteriaScoreX.SelectedValue.ToString());
                            }
                        }

                        if (rptFactors.Controls[i].FindControl("txtFactorScore") is TextBox)
                        {
                            (rptFactors.Controls[i].FindControl("txtFactorScore") as TextBox).Text = SumCriteria.ToString();
                        }
                    }
                    if (rptFactors.Controls[i].FindControl("txtFactorScore") is TextBox)
                    {
                        SumFactors += Convert.ToInt32((rptFactors.Controls[i].FindControl("txtFactorScore") as TextBox).Text);
                    }
                }
                if (rptRoute.Controls[k].FindControl("txtRouteScore") is TextBox)
                {
                    (rptRoute.Controls[k].FindControl("txtRouteScore") as TextBox).Text = SumFactors.ToString();
                }
            }

        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RouteScoreBO oRouteScore = new RouteScoreBO();
            ProjectBLL oProjectBLL = new ProjectBLL();
            int TotalScore = 0;

            if (Session["USER_ID"] != null)
                oRouteScore.UserId = Convert.ToInt32(Session["USER_ID"]);

            oRouteScore.IsDeleted = "False";

            //Repeat Through Routes
            for (int k = 0; k < rptRoute.Controls.Count; k++)
            {
                Repeater rptFactors = rptRoute.Controls[k].FindControl("rptFactors") as Repeater;

                //RootID Assignment
                if (rptRoute.Controls[k].FindControl("hdnRouteId") is HiddenField)
                    oRouteScore.RouteId = Convert.ToInt32((rptRoute.Controls[k].FindControl("hdnRouteId") as HiddenField).Value);

                //Repeat Through Factors
                for (int i = 0; i < rptFactors.Controls.Count; i++)
                {
                    if (rptFactors.Controls[i].FindControl("rptCriteria") is Repeater)
                    {
                        Repeater rptCriteriaX = rptFactors.Controls[i].FindControl("rptCriteria") as Repeater;
                        int SumCriteria = 0;
                        for (int j = 0; j < rptCriteriaX.Controls.Count; j++)
                        {
                            if (rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") is DropDownList)
                            {
                                DropDownList ddlCriteriaScoreX = rptCriteriaX.Controls[j].FindControl("ddlCriteriaScore") as DropDownList;
                                //ScoreId Assignment
                                oRouteScore.ScoreId = Convert.ToInt32(ddlCriteriaScoreX.SelectedValue.ToString());
                                SumCriteria += Convert.ToInt32(ddlCriteriaScoreX.SelectedValue.ToString()); //Finding Sum of Criteria
                            }

                            if (rptCriteriaX.Controls[j].FindControl("hdnCriteriaId") is HiddenField)
                            {
                                HiddenField hdnCriteriaIdX = rptCriteriaX.Controls[j].FindControl("hdnCriteriaId") as HiddenField;
                                //CriteriaId Assignment
                                oRouteScore.CriteriaId = Convert.ToInt32(hdnCriteriaIdX.Value.ToString());
                            }
                            if (oRouteScore.CriteriaId > 0)
                                oProjectBLL.SaveRouteScore(oRouteScore); //Storing Data
                        }
                    }
                }
                if (rptRoute.Controls[k].FindControl("txtRouteScore") is TextBox)
                {
                    int Scores = 0;
                    int RouteId = 0;
                    if (rptRoute.Controls[k].FindControl("txtRouteScore") is TextBox)
                    {
                        TextBox txtRouteScore = (rptRoute.Controls[k].FindControl("txtRouteScore") as TextBox);
                        if (!string.IsNullOrEmpty(txtRouteScore.Text))
                        {
                            Scores = Convert.ToInt32(txtRouteScore.Text);
                        }
                    }
                    if (rptRoute.Controls[k].FindControl("hdnRouteId") is HiddenField)
                    {

                        HiddenField hdnRouteId = (rptRoute.Controls[k].FindControl("hdnRouteId") as HiddenField);
                        if (!string.IsNullOrEmpty(hdnRouteId.Value))
                            RouteId = Convert.ToInt32(hdnRouteId.Value);
                    }

                    TotalScore = Convert.ToInt32(Scores);
                    RouteBO oRouteBO = new RouteBO();
                    oRouteBO.TotalRouteScore = TotalScore;
                    oRouteBO.RouteID = RouteId;
                    oProjectBLL.SaveToalRouteScore(oRouteBO);
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data saved successfully');", true);
            ClientScript.RegisterStartupScript(this.GetType(), "RouteTotalScore", "AfterSave(1);", true);
            //ClearDetails();
            //LoadRoutes();
        }
        
      
        /// <summary>
        /// Load & Bind data for Scoring
        /// </summary>
        /// <returns></returns>
        private RouteCriteriaScoreList LoadCriteriaScore()
        {
            ProjectBLL oProjectBLL = new ProjectBLL();
            lstRouteCriteriaScore = new RouteCriteriaScoreList();

            lstRouteCriteriaScore = oProjectBLL.GetRouteCriteriaScore();

            return lstRouteCriteriaScore;
        }
        /// <summary>
        /// TO Set Data to Child Repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptRout_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // LoadFactors();
            TextBox txtRouteScore = (TextBox)e.Item.FindControl("txtRouteScore");
            txtRouteScore.Attributes.Add("readonly","readonly");
            HiddenField hdnRouteId = e.Item.FindControl("hdnRouteId") as HiddenField;
            Repeater rpChild = e.Item.FindControl("rptFactors") as Repeater;//Child Repeater

            ViewState["ROUTE_ID"] = hdnRouteId.Value;

            ProjectBLL oProjectBLL = new ProjectBLL();
            RouteSelectionFactorsList oRouteSelectionFactorsList = new RouteSelectionFactorsList();

            oRouteSelectionFactorsList = oProjectBLL.getRouteSelectionFactors();

            rpChild.DataSource = oRouteSelectionFactorsList;//oProjectBLL.GetRouteSelectionCriteria_ByFactorId(Convert.ToInt32(hdnRouteId.Value));//.GetRouteSelectionCriteria_ByFactorId(hdnSec);
            rpChild.DataBind();
        }
        /// <summary>
        /// TO Set Data to Child Repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptFactors_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ProjectBLL oProjectBLL = new ProjectBLL();

            HiddenField hdnFactorId = e.Item.FindControl("hdnFactorId") as HiddenField;
            Repeater rpChild = e.Item.FindControl("rptCriteria") as Repeater;//Child Repeater

            TextBox txtFactorScore = (TextBox) e.Item.FindControl("txtFactorScore");
            txtFactorScore.Attributes.Add("readonly", "readonly");

            rpChild.DataSource = oProjectBLL.GetRouteSelectionCriteria_ByFactorId(Convert.ToInt32(hdnFactorId.Value));//.GetRouteSelectionCriteria_ByFactorId(hdnSec);
            rpChild.DataBind();
        }
        /// <summary>
        /// To set Data to Dropdowns Inside repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptCriteria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList rpt_ddlCriteriaSCore = e.Item.FindControl("ddlCriteriaScore") as DropDownList;
            HiddenField hdnCriteriaId = (HiddenField)e.Item.FindControl("hdnCriteriaId");

            try
            {
                if (lstRouteCriteriaScore == null)
                {
                    lstRouteCriteriaScore = LoadCriteriaScore();
                }

                rpt_ddlCriteriaSCore.DataSource = lstRouteCriteriaScore;

                rpt_ddlCriteriaSCore.DataTextField = "ScoreDescription";
                rpt_ddlCriteriaSCore.DataValueField = "scoreid";
                rpt_ddlCriteriaSCore.DataBind();

                rpt_ddlCriteriaSCore.SelectedIndex = 0;

                RouteScoreBO oRouteScoreBO = (new ProjectBLL()).GetRouteScore(Convert.ToInt32(ViewState["ROUTE_ID"]), Convert.ToInt32(hdnCriteriaId.Value));

                if (oRouteScoreBO != null)
                {
                    if (rpt_ddlCriteriaSCore.Items.FindByValue(oRouteScoreBO.ScoreId.ToString()) != null)
                    {
                        rpt_ddlCriteriaSCore.ClearSelection();
                        rpt_ddlCriteriaSCore.Items.FindByValue(oRouteScoreBO.ScoreId.ToString()).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Call FindSum() method to do Calculations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCriteriaScore_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindSum();
        }
        /// <summary>
        /// To close the curent window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string mode = "";
            if (Request.QueryString["Mode"] != null)
                mode = Request.QueryString["Mode"].ToString();
            if (mode == "Score")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "RouteTotalScore", "AfterClick(1);", true);
            }
            else
            {
                if (pnlSave.Visible)
                    ClientScript.RegisterStartupScript(this.GetType(), "RouteTotalScore", "AfterTotalScore(1);", true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "RouteTotalScore", "AfterTotalScore(1);", true);
            }
        }
    }
}
