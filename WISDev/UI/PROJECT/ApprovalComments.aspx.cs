using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;

namespace WIS.UI.PROJECT
{
    public partial class ApprovalComments : System.Web.UI.Page
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
                Master.PageHeader = "Route Approval Details";
                BindGrid(false, false);
            }

        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {

            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int CheckProjectID = 0;
            if (Request.QueryString["ProjectID"] != null)
            {
                CheckProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            }

            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
           
            grdApprovalComments.DataSource = objProjectRouteBLL.GetApprovedComments(ProjectID);
            grdApprovalComments.DataBind();
        }
        //Grid Page Change
      

    }
}