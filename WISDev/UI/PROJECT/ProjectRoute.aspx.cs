using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class ProjectRoute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.RouteInfo;

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Route Info";

                txtRouteDetails1.Attributes.Add("maxlength", txtRouteDetails1.MaxLength.ToString());
                txtRouteDetails2.Attributes.Add("maxlength", txtRouteDetails2.MaxLength.ToString());
                txtRouteDetails3.Attributes.Add("maxlength", txtRouteDetails3.MaxLength.ToString());

                ViewState["ROUTEID"] = 0;
                lnkbtnone1.Visible = false;
                lnkbtnTwo.Visible = false;
                lnkbtnthree.Visible = false;
                ApproverButton.Visible = false;

                GetProjectRouteDetails(Convert.ToInt32(Session["PROJECT_ID"]));

                txtFinalRoute.Enabled = false;
                txtapprovedby.Enabled = false;
                txtapprovedDate.Enabled = false;
                txtComments.Enabled = false;
                //StatusLabel.Visible = false;
                ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.None;
                lnkApprovalComments.Visible = false;
               // ApprovalMessage1.Visible = false;

                //findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));
                if (Session["FROZEN"] != null && Session["FROZEN"].ToString() == "Y")
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                   // ApproverButton.Visible = true;
                    //StatusLabel.Visible = false;
                   // ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.None;
                    //lnkApprovalComments.Visible = false;
                   // ApprovalMessage1.Visible = false;
                }
                getFinalRouteApprovalDetial(Convert.ToInt32(Session["PROJECT_ID"])); 
                findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    ApproverButton.Visible = false;
                }
               // GetApproavlComments();
            }
        }

        #region for approval comment
        //rightnow not in use
        public void GetApproavlComments()
        {
           //.ApprovalMessage.viewApproverLog(ProjectID, WorkFlowCode, pageCode);
            //ApprovalMessage1.Visible = true;
            lnkApprovalComments.Visible = true;
             int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
             string WorkFlowCode = "RTA";
             string pageCode = "RTA";
            int TrackHdrId = 0;
            string param = string.Format("OpenApproverDocumnet({0},'{1}','{2}',{3});", ProjectID, WorkFlowCode, pageCode, TrackHdrId);
            lnkApprovalComments.Attributes.Add("onclick", param);
           // ApprovalMessage1.ViewApproverLog(ProjectID, WorkFlowCode, pageCode);
        }
        #endregion
        /// <summary>
        /// To fetch details and assign to textbox
        /// </summary>
        /// <param name="ProjectID"></param>
        public void GetProjectRouteDetails(int ProjectID)
        {
           // int RouteCordinate = 0;
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteList objProjectRoute = objProjectRouteBLL.GetProjectRoutebyId(ProjectID);

            if (objProjectRoute.Count > 0)
            {
                pnlRouteSelCriteria.Visible = true;
                //ApproverButton.Visible = true;
                
                for (int i = 0; i < objProjectRoute.Count; i++)
                {
                    if (i == 0)
                    {
                        txtRoute1.Text = objProjectRoute[i].Route_Name;
                        txtRouteDetails1.Text = objProjectRoute[i].Route_Details;
                        ViewState["ROUTE_ID1"] = objProjectRoute[i].Route_ID;
                        lnkbtnone1.Visible = true;
                        RouteCoordinatesOne();
                    }
                    else if (i == 1)
                    {
                        txtRoute2.Text = objProjectRoute[i].Route_Name;
                        txtRouteDetails2.Text = objProjectRoute[i].Route_Details;
                        ViewState["ROUTE_ID2"] = objProjectRoute[i].Route_ID;
                        lnkbtnTwo.Visible = true;
                        RouteCoordinatesTwo();
                    }
                    else if (i == 2)
                    {
                        txtRoute3.Text = objProjectRoute[i].Route_Name;
                        txtRouteDetails3.Text = objProjectRoute[i].Route_Details;
                        ViewState["ROUTE_ID3"] = objProjectRoute[i].Route_ID;
                        lnkbtnthree.Visible = true;
                        RouteCoordinatesThree();
                    }
                }
                ProjectRouteList lstProjectRouteList = objProjectRouteBLL.GetProjectRoutebyId(ProjectID);


                if (lstProjectRouteList.Count > 0)
                {
                    //For Enabling/Disabling Approver Button
                    if (lstProjectRouteList[0].TotalRouteScore > 0)
                        ApproverButton.Visible = true;
                    else
                        ApproverButton.Visible = false;
                }

                //Enabling or Disabling Route Selection Panel based on Route Cordinate Value
                if(ViewState["ROUTE_ID1"]!=null)
                {
                    if (RouteCordinates(ViewState["ROUTE_ID1"].ToString()) > 0)
                    {
                        //Enabling Route Selection Link Button
                        pnlRouteSelCriteria.Visible = true;
                    }
                }
                else if (ViewState["ROUTE_ID2"] != null) 
                {
                    if (RouteCordinates(ViewState["ROUTE_ID2"].ToString()) > 0) 
                    {
                        pnlRouteSelCriteria.Visible = true;
                    }
                }
                else if (ViewState["ROUTE_ID3"] != null)
                {
                    if (RouteCordinates(ViewState["ROUTE_ID3"].ToString()) > 0)
                    {
                        //Enabling Route Selection Link Button
                        pnlRouteSelCriteria.Visible = true;
                    }
                }
                else
                {
                    //Disbling Route Selection Link Button
                    pnlRouteSelCriteria.Visible = false;
                }
            }
            else
            {
                pnlRouteSelCriteria.Visible = false;
                ApproverButton.Visible = false;
            }
        }
        /// <summary>
        /// to fetch the count of route co ordinates
        /// </summary>
        /// <param name="RouteId"></param>
        /// <returns></returns>
        private int RouteCordinates(string RouteId)
        {
            RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
            RouteCoordinatesList lstRouteCoordinates = objRouteCoordinatesBLL.GetRouteCoordinates(RouteId.ToString());
            return lstRouteCoordinates.Count;
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txtRoute1.Text != "")
            {
                ProjectRouteBO objProjectRoute = new ProjectRouteBO();
                objProjectRoute.Route_ID = Convert.ToInt32(ViewState["ROUTE_ID1"]);
                objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);
                objProjectRoute.Route_Name = txtRoute1.Text;
                objProjectRoute.Route_Details = txtRouteDetails1.Text.Trim();
                if (objProjectRoute.Route_Details.Length > 1000)
                    objProjectRoute.Route_Details = objProjectRoute.Route_Details.Substring(0, 999);
                objProjectRoute.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
                ViewState["ROUTE_ID1"] = objProjectRouteBLL.AddProjectRoutes(objProjectRoute);
                if (Convert.ToInt32(ViewState["ROUTE_ID1"]) != 0)
                {
                    lnkbtnone1.Visible = true;
                    RouteCoordinatesOne();
                }
            }

            if (txtRoute2.Text != "")
            {
                ProjectRouteBO objProjectRoute = new ProjectRouteBO();
                objProjectRoute.Route_ID = Convert.ToInt32(ViewState["ROUTE_ID2"]);
                objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);
                objProjectRoute.Route_Name = txtRoute2.Text;
                objProjectRoute.Route_Details = txtRouteDetails2.Text.Trim();
                if (objProjectRoute.Route_Details.Length > 1000)
                    objProjectRoute.Route_Details = objProjectRoute.Route_Details.Substring(0, 999);
                objProjectRoute.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
                //objProjectRouteBLL.AddProjectRoutes(objProjectRoute);
                ViewState["ROUTE_ID2"] = objProjectRouteBLL.AddProjectRoutes(objProjectRoute);
                if (Convert.ToInt32(ViewState["ROUTE_ID2"]) != 0)
                {
                    lnkbtnTwo.Visible = true;
                    RouteCoordinatesTwo();
                }
            }

            if (txtRoute3.Text != "")
            {
                ProjectRouteBO objProjectRoute = new ProjectRouteBO();
                objProjectRoute.Route_ID = Convert.ToInt32(ViewState["ROUTE_ID3"]);
                objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);
                objProjectRoute.Route_Name = txtRoute3.Text;
                objProjectRoute.Route_Details = txtRouteDetails3.Text.Trim();
                if (objProjectRoute.Route_Details.Length > 1000)
                    objProjectRoute.Route_Details = objProjectRoute.Route_Details.Substring(0, 999);
                objProjectRoute.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
               // objProjectRouteBLL.AddProjectRoutes(objProjectRoute);
                ViewState["ROUTE_ID3"] = objProjectRouteBLL.AddProjectRoutes(objProjectRoute);
                if (Convert.ToInt32(ViewState["ROUTE_ID3"]) != 0)
                {
                    lnkbtnthree.Visible = true;
                    RouteCoordinatesThree();
                }
            }
            GetProjectRouteDetails(Convert.ToInt32(Session["PROJECT_ID"]));
            findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
            // ClearDetails();            
        }
        /// <summary>
        /// To clear input fields and load default data
        /// </summary>
        protected void ClearDetails()
        {
            //Disabled Elements
            txtapprovedby.Text = "";
            txtapprovedDate.Text = "";
            txtFinalRoute.Text = "";
            txtComments.Text = "";

            //Enabled Elements
            txtRoute1.Text = "";
            txtRoute2.Text = "";
            txtRoute3.Text = "";
            txtRouteDetails1.Text = "";
            txtRouteDetails2.Text = "";
            txtRouteDetails3.Text = "";

            ViewState["ROUTEID"] = 0;
            btn_Save.Text = "Save";
        }
        /// <summary>
        /// link for RouteCoordinatesOne
        /// </summary>
        public void RouteCoordinatesOne()
        {
            string RCOne = string.Format("openpopupOne({0},'{1}');", ViewState["ROUTE_ID1"], txtRoute1.Text);

            lnkbtnone1.Attributes.Add("onclick", RCOne);
        }
        /// <summary>
        /// link for RouteCoordinatesTwo
        /// </summary>
        public void RouteCoordinatesTwo()
        {
            string RCTwo = string.Format("openpopupTwo({0},'{1}');", ViewState["ROUTE_ID2"], txtRoute2.Text);

            lnkbtnTwo.Attributes.Add("onclick", RCTwo);
        }
        /// <summary>
        /// link for RouteCoordinatesThree
        /// </summary>
        public void RouteCoordinatesThree()
        {
            string RCThree = string.Format("openpopupThree({0},'{1}');", ViewState["ROUTE_ID3"], txtRoute3.Text);

            lnkbtnthree.Attributes.Add("onclick", RCThree);
        }
        /// <summary>
        /// Calls ClearDetails method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        /// <summary>
        /// To send route approval notification 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApproverButton_Click(object sender, EventArgs e)
        {
            string ResultValue = string.Empty;
            string message = string.Empty;
            string WorkFlowCode = UtilBO.WorkflowRouteApproval;
            string emailSubject = string.Empty;
            string emailBody = string.Empty;

            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteList objProjectRouteList = new ProjectRouteList();

            objProjectRoute.WorkFlowApprover = UtilBO.WorkflowRouteApproval;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);

            objProjectRoute = objProjectRouteBLL.getWOrkFlowApprovalID(objProjectRoute);

            if ((objProjectRoute) != null)
            {
                (new NotificationBLL()).SendEmail(Convert.ToInt32(Session["PROJECT_ID"]), UtilBO.WorkflowRouteApproval);
                #region for sending SMS
                WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                WIS_ConfigBLL WIS_ConfigBLL = new WIS_ConfigBLL();
                WIS_ConfigBO = WIS_ConfigBLL.GetConfigSMSsending();
                if (WIS_ConfigBO != null)
                {
                    if (WIS_ConfigBO.MobileStatus == "Y")
                    {
                        string Result = string.Empty;
                        string SendsmsTest = objProjectRoute.SmsText + objProjectRoute.ProjectCode + objProjectRoute.ProjectName;
                        NotificationBO SMSNotificationBO = new NotificationBO();
                        NotificationBLL SMSNotificationBLL = new NotificationBLL();
                        SMSNotificationBO.ProviderMobileNo = WIS_ConfigBO.MobileNumber;
                        SMSNotificationBO.ProviderPasword = WIS_ConfigBO.MobilePassword;
                        SMSNotificationBO.ProviderURL = WIS_ConfigBO.SiteUrl;

                        SMSNotificationBO.CellNumber = objProjectRoute.CellNumber;
                        SMSNotificationBO.SmsText = SendsmsTest;

                        Result = SMSNotificationBLL.SENDSMS(SMSNotificationBO);
                    }
                }
                #endregion
                
                //NotificationObj.SendEmail(objProjectRoute.EmailID, objProjectRoute.EmailSubject, objProjectRoute.EmailBody, objProjectRoute.ProjectCode, objProjectRoute.ProjectName);
                //ResultValue = NotificationObj.SendSMS(objProjectRoute.CellNumber, objProjectRoute.SmsText, objProjectRoute.ProjectCode, objProjectRoute.ProjectName);               

                ProjectRouteBO objApprovalHeaderSave = new ProjectRouteBO();
                objApprovalHeaderSave.WorkFlowApproverID = objProjectRoute.WorkFlowApproverID;
                objApprovalHeaderSave.StatusID = objProjectRoute.StatusID;
                objApprovalHeaderSave.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                objApprovalHeaderSave.PageCode = "RTA";
                objApprovalHeaderSave.ApproverUserID = objProjectRoute.ApproverUserID;
                objApprovalHeaderSave.WorkFlowDefinitionID = objProjectRoute.WorkFlowDefinitionID;

                #region email formate
                switch (WorkFlowCode)
                {
                    case "RTA":
                        emailSubject = string.Format("{0} {1}", objProjectRoute.EmailSubject, objProjectRoute.ProjectName);
                        //emailBody = emailBody.Replace("@@PROJECTNAME", objProjectRoute.EmailBody);
                        emailBody = objProjectRoute.EmailBody.Replace("@@PROJECTNAME", objProjectRoute.ProjectName);
                        break;
                    default:
                        emailSubject = objProjectRoute.EmailSubject;
                        break;
                }
                string approverName = objProjectRoute.ApproverUserName;
                StringBuilder sb = new StringBuilder();
                sb.Append("Dear " + approverName + ",");
                sb.Append("<br/><br/>");
                sb.Append(emailBody);
                sb.Append("<br/><br/>");
                sb.Append("Thanks and Regards,");
                sb.Append("<br/>");
                sb.Append("WIS - UETCL Team");
                #endregion 

                objApprovalHeaderSave.EmailSubject = emailSubject;
                objApprovalHeaderSave.EmailBody = sb.ToString();

                message = objProjectRouteBLL.AddApprovalTrackheader(objApprovalHeaderSave);
           
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Route Approval Notification has been sent";
                if (message != "")
                {

                    int ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                    findRouteStatusafterSave(ProjectId);
                    getFinalRouteApprovalDetial(ProjectId);
                    GetApproavlComments();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ApproverButton.Visible = false;
                }

            }
            else
            {
                message = "Route Approver is not defined";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            }

            findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));
        }
        /// <summary>
        /// To find route status after save
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
                txtFinalRoute.Enabled = false;
                txtComments.Enabled = false;
                txtapprovedby.Enabled = false;
                txtapprovedDate.Enabled = false;
                GetApproavlComments();

                if (objProjectRoute.ApprovedstatusID == 3)
                {
                    pnlApprovel.Visible = false;
                    //StatusLabel.Visible = true;
                    ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.RouteApprovalSent;
                    //StatusLabel.Text = "Route Approval is Pending";
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    ApproverButton.Visible = false;
                }
            }
        }
        /// <summary>
        /// To get final route approval detail
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

                        txtFinalRoute.Enabled = false;
                        txtComments.Enabled = false;
                        txtapprovedby.Enabled = false;
                        txtapprovedDate.Enabled = false;
                        GetApproavlComments();

                        if (ProjectRouteList[i].ApprovedstatusID == 1)
                        {
                            pnlApprovel.Visible = true;
                            txtFinalRoute.Text = ProjectRouteList[i].Route_Name.ToString();
                            txtComments.Text = ProjectRouteList[i].ApproverComment.ToString();
                            txtapprovedby.Text = ProjectRouteList[i].ApproverUserName.ToString();
                            txtapprovedDate.Text = ProjectRouteList[i].Approveddate.ToString();

                            btn_Save.Visible = false;
                            btn_Clear.Visible = false;
                            ApproverButton.Visible = false;
                           // lnkApprovalComments.Visible = false;

                            //StatusLabel.Visible = false;
                            //StatusLabel.Text = "";
                            ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.None;
                        }                        
                    }
                    else
                    {
                        if (ProjectRouteList[i].ApprovedstatusID == 3)
                        {
                            pnlApprovel.Visible = false;
                            //StatusLabel.Visible = true;
                            //StatusLabel.Text = "Route Approval is Pending";
                            ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.RouteApprovalSent;
                            btn_Save.Visible = false;
                            btn_Clear.Visible = false;
                            ApproverButton.Visible = false;
                            ///lnkApprovalComments.Visible = true;
                        }
                        if (ProjectRouteList[i].ApprovedstatusID == 2)
                        {
                            pnlApprovel.Visible = false;

                            //StatusLabel.Visible = true;

                            //StatusLabel.Text = "Route Declined";
                            ApprovalMessage1.SetMessage = ApprovalMessage.MessageValue.RouteApprovalDeclined;

                            btn_Save.Visible = true;
                            btn_Clear.Visible = true;
                            ApproverButton.Visible = false;
                            //lnkApprovalComments.Visible = true;
                        }                        
                    }
                }
            }
            else
            {
                pnlApprovel.Visible = false;
            }
            
        }

    }
}