using System;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Web.UI;
using System.IO;

namespace WIS
{
    public partial class OptionSummary : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Summary Reports";
                else
                    Master.PageHeader = "Summary Reports";

                BindProject();

                //Edwin Baguma:
                BindLegacyReports();
                

           
                

                ddlReportType.Attributes.Add("onchange", "ddlReportType_IndexChanged(this);");
               
                /* Edwin Baguma: 
                chkShow.Attributes.Add("onclick", "ShowHide(this);");
                chkHide.Attributes.Add("onclick", "SetDefault(this);");
                 */
                
                if (Request.QueryString["ProjectID"] != null && Request.QueryString["Distict"] != null)
                {
                    string Distict = Request.QueryString["Distict"].ToString();
                    string County = Request.QueryString["County"].ToString();
                    string SubCounty = Request.QueryString["SubCounty"].ToString();
                    string Parish = Request.QueryString["Parish"].ToString();
                    string Village = Request.QueryString["Village"].ToString();
                    ddlReportType.ClearSelection();
                    ddlReportType.Items.FindByValue("CFBS").Selected = true;
                    
                    /* Edwin Baguma: Reemove unwanted panels
                    chkHide.Checked = false;
                    chkShow.Checked = true;
                    spBatch.Style.Add("display", "");
                    SpBachDetails.Style.Add("display", "");
                    txtHHID.Text = Session["HH_ID"].ToString();
                     */ 
                }
            }

            /* Edwin Baguma: Remove date fields
            calopsStartDate.Format = UtilBO.DateFormat;
            CalopsEndDate.Format = UtilBO.DateFormat;
             */ 
        }
        /// <summary>
        /// To search PAP details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        /*
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            spBatch.Style.Add("display", "");
            SpBachDetails.Style.Add("display", "");
            Session["PROJECT_ID"] = Convert.ToInt32(ddlProject.SelectedItem.Value);
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx?ProjectID=" + ddlProject.SelectedValue + "&Distict=0&County=0&SubCounty=0&Parish=0&Village=0', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
         */
 

        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindProject()
        {
            ProjectBLL BLLobj = new ProjectBLL();
            ddlProject.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProject.DataTextField = "projectName";
            ddlProject.DataValueField = "projectID";
            ddlProject.DataBind();

            if (Session["PROJECT_ID"] != null)
            {
                if (ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                {
                    ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;
                }
            }
        }


        // Edwin Baguma: Start
        private void BindLegacyReports()
        {
            ProjectBLL LegacyRpt = new ProjectBLL();
            ddlReportType.DataSource = LegacyRpt.GetLegacyReports();
            ddlReportType.DataTextField = "reportName";
            ddlReportType.DataValueField = "reportID";
            ddlReportType.DataBind();

            /*
            if (Session["PROJECT_ID"] != null)
            {
                if (ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                {
                    ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;
                }
            }*/
        
        }
        // Edwin Baguma: End


        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlProject.ClearSelection();
            ddlReportType.ClearSelection();
            
            
            /* Edwin Baguma:
             opsStartDate.Text = "";
            opsEndDate.Text = "";
            chkShow.Checked = false;
            chkHide.Checked = true;
            txtBatchNo.Text = "";
            txtHHID.Text = "";
            spBatch.Style.Add("display", "none");
            SpBachDetails.Style.Add("display", "none");
             */
        }     
    }
}