using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using System.Text;

namespace WIS
{
    public partial class GrievanceClosure : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,call  getGrievance() to get grievance
        /// Set attributes to link buttons lnkUPloadDoc,lnkUPloadDoclist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Grievance Closure";
                txtClosureComments.Attributes.Add("maxlength", txtClosureComments.MaxLength.ToString());
                string mode = Request.QueryString["mode"];

                ViewState["GrievanceID"] = Request.QueryString["id"];
                getGrievance();

                // ----
                int userID = Convert.ToInt32(Session["USER_ID"]);
                int projectID = 0;
                string projectCode = "";

                if (mode == "readonly")
                {
                    lnkUPloadDoc.Visible = false;
                    btnClose.Visible = false;
                }

                if (Session["PROJECT_ID"] != null)
                {
                    projectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    projectCode = Session["PROJECT_CODE"].ToString();
                }

                int HHID = 0;
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }

                if (Session["PROJECT_CODE"] == null)
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                if (Session["HH_ID"] == null)
                {
                    Response.Redirect("~/UI/Compensation/PAPList.aspx");
                }
                // ----

                string DocumentCode = "GRIVC";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", projectID, HHID, userID, projectCode, DocumentCode);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", projectID, HHID, userID, projectCode, DocumentCode);

                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);
            }

        }
        /// <summary>
        /// get Grievance data from database
        /// </summary>
        public void getGrievance()
        {
            GrievancesBLL objGrievancesBLL = new GrievancesBLL();
            GrievancesBO GrievancesBOobj = new GrievancesBO();

            //GrievancesBO GrievancesBOobj = (new GrievancesBLL()).getGrievance(Convert.ToInt32(ViewState["GrievanceID"]));
            //if (GrievancesBOobj != null)
            //    lblCategoryAssign.Text = GrievancesBOobj.GrievCategory;
            //lblCreatedDateAssign.Text = GrievancesBOobj.CreatedDate.ToString();
            //descriptionTextBox.Text = GrievancesBOobj.Description;

            int GrievanceID = Convert.ToInt32(ViewState["GrievanceID"]);
            GrievancesBOobj = objGrievancesBLL.GetGrievanceClosure(GrievanceID);
            if (GrievancesBOobj != null)
            {
                lblCategoryAssign.Text = GrievancesBOobj.GrievCategory;
                lblCreatedDateAssign.Text = GrievancesBOobj.CreatedDate.ToString(UtilBO.DateFormatFull);
                txtClosureComments.Text = GrievancesBOobj.ClosureComments;
            }
        }
        /// <summary>
        /// update Grievance status to close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string updateStatus = "C";
            GrievancesBO objGrievance = new GrievancesBO();
            objGrievance.Hhid = Convert.ToInt32(Session["HH_ID"]);
            objGrievance.GrievanceID = Convert.ToInt32(ViewState["GrievanceID"]);
            if (txtClosureComments.Text.Length > 499)
            {
                objGrievance.ClosureComments = txtClosureComments.Text.Trim().Substring(0, 499);
            }
            else
                objGrievance.ClosureComments = txtClosureComments.Text.Trim();
            objGrievance.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
            objGrievance.ResolutionStatus = updateStatus;
            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
            objMytaskApprovalBLL.UPdateGrievance(objGrievance);

            ClientScript.RegisterStartupScript(this.GetType(), "GrivanceClosure", "GrivanceClosure();", true);
        }
    }
}