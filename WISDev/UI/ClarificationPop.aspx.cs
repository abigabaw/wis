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

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                Master.PageHeader = "Clarification and Responses";
                // btnShowUpload.Attributes.Add("onclick", "SetVisible(0);");
                //  btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");

                /* CheckSystemAccount();
                CheckPendingTasks();
                CheckClarifications(); */

                grdClarifications.Visible = true;
                
                
                

                string Mode = Request.QueryString["Mode"];

                if (Mode == "Clarify"){
                    ResponseFields.Visible = false;
                    RequesterDetails.Visible = false;
                    BindPersonnel();
                    GetRelevantDetails();
                    BindClarifications(false, false);
                }
                else
                {
                    ResponseFields.Visible = true;
                    RequesterDetails.Visible = false;
                    RespondentSelect.Visible = false;
                    ClarificationDetail.Visible = false;
                    PapDetails.Visible = false;
                    ProjectDetails.Visible = false;
                    MyClarifications(false, false);
                    SaveButton.Text = "Send Response";
                    //this.MasterPageFile = "~/Site.Master"; 
                }
                

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

                


            }
        }

        private void CheckClarifications()
        {
            throw new NotImplementedException();
        }

        private void CheckPendingTasks()
        {
            throw new NotImplementedException();
        }

        private void CheckSystemAccount()
        {
            throw new NotImplementedException();
        }

        private void BindPersonnel()//These are Project Users
        {
            ListItem firstListItem = new ListItem(ddlProjectPersonnel.Items[0].Text, ddlProjectPersonnel.Items[0].Value);
            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();

            int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            
                //Edwin: 30MAY2016 - 
            ProjectPersonalList ProjectPersonnels = objProjPersonalLogic.GetProjectPersonnel(ProjectID);
                ddlProjectPersonnel.ClearSelection();
                ddlProjectPersonnel.Items.Clear();
                if (ProjectPersonnels != null)
                {
                    ddlProjectPersonnel.DataSource = ProjectPersonnels;
                    ddlProjectPersonnel.DataTextField = "Username";
                    ddlProjectPersonnel.DataValueField = "UserID";
                    ddlProjectPersonnel.DataBind();
                }
                ddlProjectPersonnel.Items.Insert(0, firstListItem);
            
        }

        private void GetRelevantDetails()
        {
            int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);

            ProjectBLL ProjectBLL = new ProjectBLL();
            PAP_HouseholdBLL PAP_HouseholdBLL = new PAP_HouseholdBLL();

            ProjectBO ProjectBO = new ProjectBO();
            PAP_HouseholdBO PAP_HouseholdBO = new PAP_HouseholdBO();

            ProjectBO = ProjectBLL.GetProjectByProjectID(ProjectID);
            PAP_HouseholdBO = PAP_HouseholdBLL.GetHouseHoldData(HHID);

            txtHHID.Text = PAP_HouseholdBO.HhId.ToString();
            txtPapName.Text = PAP_HouseholdBO.PapName.ToString();
            txtProjectName.Text = ProjectBO.ProjectName.ToString();

        }

        private void BindClarifications(bool addRow, bool deleteRow)
        {
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            ClarifyBLL ClarifyBLL = new ClarifyBLL();
            grdClarifications.DataSource = ClarifyBLL.GetClarifications(HHID,UserID);
            grdClarifications.DataBind();
        }

        private void MyClarifications(bool addRow, bool deleteRow)
        {

            //int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            ClarifyBLL ClarifyBLL = new ClarifyBLL();
            grdClarifications.DataSource = ClarifyBLL.GetMyClarifications(UserID);
            grdClarifications.DataBind();
        }

        protected void grdClarifications_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string Mode = Request.QueryString["Mode"];
            // if (Mode.Equals("Clarify", StringComparison.Ordinal))
            if (Mode == "Clarify")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    grdClarifications.Columns[10].Visible = false;
                }

            }

        }

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

        
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Mode"] == "Clarify")
            {
                if (txtClarifyDetails.Text == "" || ddlProjectPersonnel.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Empty Clarification, Check Details or Select Respondent');</script>", false);
                }
                else
                {

                    string statusMessage = string.Empty;

                    string UserID = Request.QueryString["UserID"];
                    string ReqID = Request.QueryString["ReqID"];

                    int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                    ClarifyBO ClarifyBO = new ClarifyBO();


                    ClarifyBO.TrackHeader = Convert.ToInt32(ReqID);
                    ClarifyBO.RequestDetails = txtClarifyDetails.Text.ToString();
                    ClarifyBO.UserID = Convert.ToInt32(UserID);
                    ClarifyBO.RespondentID = Convert.ToInt32(ddlProjectPersonnel.SelectedValue);

                    ClarifyBLL ClarifyBLL = new ClarifyBLL();
                    statusMessage = ClarifyBLL.InsertClarify(ClarifyBO);

                    if (statusMessage == "Failed")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Pending Clarifications Already Exist');</script>", false);
                        /* Alert that displays custom variable
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert(\"You have selected " + ClarifyID + "\");</script>", false); */

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Your Clarification has been Sent');</script>", false);
                        ddlProjectPersonnel.ClearSelection();
                        txtClarifyDetails.Text = string.Empty;
                        BindClarifications(false, false);
                    }
                }
            }
            else
            {
                if (txtResponseDetails.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Empty Response');</script>", false);
                }
                else
                {
                    string statusMessage = string.Empty;
                    int ReqID = Convert.ToInt32(ViewState["ID"]);
                    // int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                    ClarifyBO ClarifyBO = new ClarifyBO();


                    ClarifyBO.ID = ReqID;
                    ClarifyBO.ResponseDetails = txtResponseDetails.Text.ToString();
                    ClarifyBO.Status = "Resolved";

                    ClarifyBLL ClarifyBLL = new ClarifyBLL();
                    statusMessage = ClarifyBLL.InsertReponse(ClarifyBO);

                    if (statusMessage == "Failed")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Response Not Sent');</script>", false);
                        /* Alert that displays custom variable
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert(\"You have selected " + ClarifyID + "\");</script>", false); */

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('Your Response has been Sent');</script>", false);
                        // ddlProjectPersonnel.ClearSelection();
                        txtResponseDetails.Text = string.Empty;
                        txtResponseDetails.Enabled = false;
                        PapDetails.Visible = false;
                        RequesterDetails.Visible = false;
                        ClarificationDetail.Visible = false;
                        MyClarifications(false, false);
                    }
                }
            }

            
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"];

            if (Mode == "Clarify")
            {
                ddlProjectPersonnel.ClearSelection();
                txtClarifyDetails.Text = string.Empty;
            }
            else
            {
                PapDetails.Visible = false;
                RequesterDetails.Visible = false;
                ClarificationDetail.Visible = false;
            }
        }

        protected void grdClarifications_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            /* if (e.CommandName == "RespondToClarify")
            {
                ViewState["ID"] = e.CommandArgument;
                string ClarifyID = ViewState["ID"].ToString
            } */

            ViewState["ID"] = e.CommandArgument;
            int ClarifyID = Convert.ToInt32( ViewState["ID"]);

            ClarifyBLL ClarifyBLL = new ClarifyBLL();
            ClarifyBO ClarifyBO = new ClarifyBO();
            ClarifyBO = ClarifyBLL.SelectClarification(ClarifyID);

            if (ClarifyBO.Status == "Pending")
            {
                PapDetails.Visible = true;
                RequesterDetails.Visible = true;
                txtClarifyDetails.Enabled = false;
                txtResponseDetails.Enabled = true;
                ClarificationDetail.Visible = true;
                txtHHID.Text = ClarifyBO.HHID.ToString();
                txtPapName.Text = ClarifyBO.PapName;
                txtRequester.Text = ClarifyBO.Requester;
                txtClarifyDetails.Text = ClarifyBO.RequestDetails;
            }else{
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alert", "<script>alert('You have already responded to this query');</script>", false);
            }

            

            
        }


    }
}