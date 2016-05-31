using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ProjectUnfreeze : System.Web.UI.Page
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
                // Master.PageHeader = "Unfreeze Projects";

                //if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false &&
                //        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == true)
                //{
                //    for (int i = grdProjects.Columns.Count - 1; i >= 0; i--)
                //    {
                //        if (i > grdProjects.Columns.Count - 4)
                //            grdProjects.Columns[i].Visible = false;
                //    }
                //}
                //else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false &&
                //        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false)
                //{
                //    Response.Redirect(ResolveUrl("~/Default.aspx"));
                //}

                int UserID = Convert.ToInt32(Session["USER_ID"]);
                int ProjectID = 0;
                int HHID = 0;
                string ProjectCode = string.Empty;

                //Using Session Parameters
                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                if (Session["HH_ID"] != null)
                    HHID = Convert.ToInt32(Session["HH_ID"]);

                if (Session["PROJECT_CODE"] != null)
                    ProjectCode = Session["PROJECT_CODE"].ToString();

                //Using Query String Parameters
                if (Request.QueryString["ProjectID"] != null)
                    ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());

                if (Request.QueryString["UserID"] != null)
                    UserID = Convert.ToInt32(Request.QueryString["UserID"].ToString());

                if (Request.QueryString["ProjectCode"] != null)
                    ProjectCode = Request.QueryString["ProjectCode"].ToString();

                string DocumentCode = "PR_UNFRZ";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, UserID, ProjectCode, DocumentCode);

                lnkUPloadDoc.Attributes.Add("onclick", param);
            }
            // Unfreeze();
        }
        /// <summary>
        /// To fetch project frozen details
        /// </summary>
        public void getFrozen()
        {
            ProjectBLL ObjProjectBLL = new ProjectBLL();
            ProjectBO ObjProjectBO = new ProjectBO();

            ObjProjectBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);


            ObjProjectBO = ObjProjectBLL.getFrozen(ObjProjectBO);

            if ((ObjProjectBO) != null)
            {
                Session["FROZEN"] = ObjProjectBO.Frozen;
            }

        }
        /// <summary>
        /// to fetch project unfrozen details and assign to textbox 
        /// </summary>
        private void Unfreeze()
        {
            //Response.q

            ProjectBLL oProjectBLL = new ProjectBLL();
            ProjectBO oProjectBO = new ProjectBO();

            int countParameters = Request.QueryString.Count;
            int ProjectId = 0, UserId = 0;
            if (countParameters > 0)
            {
                if (Request.QueryString["ProjectID"] != null)
                {
                    ProjectId = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());
                    oProjectBO.ProjectID = ProjectId;
                }
                if (Request.QueryString["UserID"] != null)
                {
                    UserId = Convert.ToInt32(Request.QueryString["UserID"].ToString());
                    oProjectBO.UnfreezeBy = UserId;
                }
                oProjectBO.UnfreezeDate = Convert.ToDateTime(DateTime.Now.ToString(UtilBO.DateFormatDB));

                if (txtUnfreezeComments.Text.Trim().Length > 500)
                    oProjectBO.UnfreezeComments = txtUnfreezeComments.Text.Trim().Substring(0, 500);
                else
                    oProjectBO.UnfreezeComments = txtUnfreezeComments.Text.Trim();

                string message = oProjectBLL.UnfreezeProject(oProjectBO);

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append(@"<script type=" + "text/javascript" + "language=" + "javascript" + ">");
                //sb.Append(@"alert('Data updated successfully'); ");
                //sb.Append(@"Aftersend();");
                //sb.Append(@"</script>");

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "alert('Data updated successfully'); ";
                }
                else
                {
                    //message = "alert('Data updated successfully'); Aftersend();";
                    message = "alert('" + message + "');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", message, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Aftersend", "Aftersend();", true);
            }
        }
        /// <summary>
        /// calls unfreeze method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnfreeze_Click(object sender, EventArgs e)
        {
            Unfreeze();
        }

    }
}