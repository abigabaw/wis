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
    public partial class PrintComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string WorkflowCode_ = string.Empty;

            Master.PageHeader = "Print Comments";

            if (!IsPostBack)
            {
                BindRepeater();
            }

        }
        public CompensationPackagesList getApproverComments()
        {

            CompensationPackagesBLL oCompensationPackagesBLL = new CompensationPackagesBLL();
            CompensationPackagesBO oCompensationPackagesBO = new CompensationPackagesBO();
            CompensationPackagesList oCompensationPackagesList = new CompensationPackagesList();

            int projectID = 0;
            int HHID = 0;

            //if (ViewState["PROJECT_ID"] != null)
            //    projectID = Convert.ToInt32(ViewState["PROJECT_ID"]);

            if (Session["HH_ID"] != null)
                HHID = Convert.ToInt32(Session["HH_ID"]);

            oCompensationPackagesBO.HHID = HHID;
            //oCompensationPackagesBO.ProjectID = projectID;

            oCompensationPackagesList = oCompensationPackagesBLL.getprintComments(HHID);

            return oCompensationPackagesList;
        }

        #region Repeater
        /// <summary>
        /// Bind Data to rptrPrintComments
        /// </summary>
        private void BindRepeater()
        {
            rptrPrintComments.DataSource = getApproverComments();
            rptrPrintComments.DataBind();

            if (rptrPrintComments.Items.Count == 0)
            {
                lblMessage.Text = "There is no Conversation";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
            }
        }

        protected void rptrSenderDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
        #endregion

        #region GridView


        protected void rptrPrintComments_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

       
        #endregion

        private int ViewStateProjectId
        {
            get;
            set;
        }
    }
}