using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class RequestClarification : System.Web.UI.Page
    {
        public string contentType = string.Empty;

        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        UploadDocumentBO objUploadDocument;
        UploadDocumentBLL objUploadDocumentBLL;
        #endregion

        #region page load
        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Response.Cache.SetNoStore();            

            if (!IsPostBack)
            {
                Master.PageHeader = "Clarification and Responses";
               // btnShowUpload.Attributes.Add("onclick", "SetVisible(0);");
              //  btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                int DocserviceID = 0;
                int ProjectID = 0;
                int userID = 0;

                if (Session["Project_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
                }

                if (Session["USER_ID"] != null)
                {
                    userID = Convert.ToInt32(Request.QueryString["userID"]);
                }

                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                
                // string ProjectCode = Request.QueryString["ProjectCode"].ToString();
                // string DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
                ViewState["PAPDOCUMENTID"] = 0;
                if (Request.QueryString["DOCSERVICEID"] != null)
                {
                    DocserviceID = Convert.ToInt32(Request.QueryString["DOCSERVICEID"]);
                }
                else
                {
                    DocserviceID = 0;
                }

                if (userID == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    
                }
                if (ProjectID == 0)
                {
                    ProjectID = 0;
                    //upProjectIDTextBox.Text = "0";
                   // ProjectCodeTextBox.Text = ProjectCode;
                   // upProjectIDTextBox.Visible = false;
                }
                else
                {
                    //upHHIDTextBox.Text = HHID.ToString();
                    PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
                    PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHousaeHoldData(HHID);
                    if (objHousehold != null)
                    {
                     //   upHHIDTextBoxDisp.Text = objHousehold.HhId.ToString();
                    }
                    //ProjectCodeTextBox.Text = ProjectCode;
                }
                if (HHID == 0)
                {
                    HHID = 0;

                    //upProjectIDTextBox.Text = ProjectID.ToString();
                    // ProjectCodeTextBox.Text = ProjectCode;
                    //DocTypeDropDownList.Visible = false;
                    //upProjectIDTextBox.Visible = false;
                    //upHHIDTextBox.Visible = false;
                    //upHHIDTextBoxDisp.Visible = false;
                }
                else
                {
                    //upProjectIDTextBox.Text = ProjectID.ToString();
                    //ProjectCodeTextBox.Text = ProjectCode;
                    //upProjectIDTextBox.Visible = false;
                }
                if (DocserviceID > 0)
                {
                    //txtDocserviceID.Text = DocserviceID.ToString();
                }
                // BindGrid(false, false);
                //screenIntilation();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PACKAGE_CLOSING_INFO) == false)
                {
                   // SaveButton.Visible = false;
                   // ClearButton.Visible = false;
                }
            }
            //this.contentPanel1.Attributes["src"] = null;
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",CreateCStartupScript());
            }
            // Response.Write(ProjectID);
        }
        #endregion

        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateCStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            /* if (hfVisible.Value.Trim() == "1")
                stb.Append(btnShowSearch.ClientID);
            else
                stb.Append(btnShowUpload.ClientID);
            stb.Append("\";\n"); */

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }


    }
}