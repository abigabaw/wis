using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;


namespace WIS
{
    public partial class ScheduleMeeting : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void Page_Load(object sender, EventArgs e)
        {
            calAppntDate.Format = UtilBO.DateFormat;
            calNegotDate.Format = UtilBO.DateFormat;
            calMeetingDatePicker.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] == null)
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                if (Session["HH_ID"] == null)
                {
                    Response.Redirect("~/UI/Compensation/PAPList.aspx");
                }
                ViewState["CULTURALMEETID"] = 0;
                ViewState["CULTURALNEGOID"] = 0;
                ViewState["CULTURALPROPID"] = Request.QueryString["CULTURALPROPID"];

                getMEETINGPURPOSE();

                BindGrid();
                BindGrid1();
                AppntDate.Attributes.Add("readonly","readonly");
                NegotDate.Attributes.Add("readonly", "readonly");
                MeetingDatePicker.Attributes.Add("readonly","readonly");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    ScheduledInformBtnSave.Visible = false;
                    ScheduledInformBtnClear.Visible = false;
                    MeetingButton1.Visible = false;
                    MeetingButton2.Visible = false;
                    grdScheduled.Columns[5].Visible = false;
                }
            }
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid1()
        {
            MeetingBLL MeetingBLLobj = new MeetingBLL();
            grdMeeting.DataSource = MeetingBLLobj.GetCulturePropertyMeetings(Convert.ToInt32(ViewState["CULTURALPROPID"]));
            grdMeeting.DataBind();
        }

       
        /// <summary>
        ///To get the meeting purpose details
        /// </summary>
        private void getMEETINGPURPOSE()
        {
            MeetingBLL BLLobj = new MeetingBLL();

            ddlMeetingPurpose.DataSource = BLLobj.getMEETINGPURPOSE();
            ddlMeetingPurpose.DataTextField = "MEETINGPURPOSE";
            ddlMeetingPurpose.DataValueField = "MEETINGPURPOSEID";
            ddlMeetingPurpose.DataBind();
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            NegoBLL NegoBLLobj = new NegoBLL();
            grdScheduled.DataSource = NegoBLLobj.GetNGODATA(Convert.ToInt32(ViewState["CULTURALPROPID"]));
            grdScheduled.DataBind();
        }

        
        /// <summary>
        ///To save the scheduled information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ScheduledInformBtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (CULTURALNEGOIDtxtbx.Text == string.Empty)
            {
                NegoBLL BLLobj = new NegoBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    int CULTURALPROPID = Convert.ToInt32(ViewState["CULTURALPROPID"]);

                    Nego Negoobj = new Nego();

                    Negoobj.HHID = Convert.ToInt32(hhid);

                    Negoobj.CULTURALPROPID = CULTURALPROPID;
                    Negoobj.NEGO_APPOINTMENTDATE = Convert.ToDateTime(AppntDate.Text);

                    Negoobj.NEGO_VENUE = txtbxVenueforNegotiation.Text;
                    Negoobj.NEGO_DATE = Convert.ToDateTime(NegotDate.Text);

                    if (txtbxProblems.Text != string.Empty)
                    {
                        Negoobj.NEGO_PROBLEMDESC = txtbxProblems.Text;
                    }
                    else
                    {
                        Negoobj.NEGO_PROBLEMDESC = "";
                    }

                    Negoobj.ISDELETED = "False";
                    Negoobj.CREATEDBY = Convert.ToInt32(uID);


                    NegoBLL NegoBLLobj = new NegoBLL();
                    count = NegoBLLobj.Insert(Negoobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                    ClearScheduledData();
                    BindGrid();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    BLLobj = null;
                }
            }
            else if (CULTURALNEGOIDtxtbx.Text != string.Empty)
            {

                NegoBLL BLLobj = new NegoBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    int CULTURALPROPID = Convert.ToInt32(ViewState["CULTURALPROPID"]);

                    Nego Negoobj = new Nego();

                    Negoobj.HHID = Convert.ToInt32(hhid);

                    Negoobj.CULTURALNEGOID = Convert.ToInt32(CULTURALNEGOIDtxtbx.Text);
                    Negoobj.CULTURALPROPID = CULTURALPROPID;
                    Negoobj.NEGO_APPOINTMENTDATE = Convert.ToDateTime(AppntDate.Text);

                    Negoobj.NEGO_VENUE = txtbxVenueforNegotiation.Text;
                    Negoobj.NEGO_DATE = Convert.ToDateTime(NegotDate.Text);

                    Negoobj.NEGO_PROBLEMDESC = txtbxProblems.Text;


                    Negoobj.ISDELETED = "False";
                    Negoobj.CREATEDBY = Convert.ToInt32(uID);


                    NegoBLL NegoBLLobj = new NegoBLL();
                    count = NegoBLLobj.Update(Negoobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);
                    ClearScheduledData();
                    BindGrid();
                    SetUpdateMode(false);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {

                    BLLobj = null;
                }
            }

        }

       /// <summary>
       ///To save the meeting details
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void MeetingBtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;

            if (CULTURALMEETIDtxtbx.Text == string.Empty)
            {
                MeetingBLL BLLobj = new MeetingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    int CULTURALPROPID = Convert.ToInt32(ViewState["CULTURALPROPID"]);

                    Meeting Meetingobj = new Meeting();

                    Meetingobj.HHID = Convert.ToInt32(hhid);

                    Meetingobj.CULTURALPROPID = CULTURALPROPID;
                    Meetingobj.MEETINGDATE = Convert.ToDateTime(MeetingDatePicker.Text.ToString());
                    if (txtbxLocation.Text != string.Empty)
                    {
                        Meetingobj.MEETINGLOCATION = txtbxLocation.Text;
                    }
                    else
                    {
                        Meetingobj.MEETINGLOCATION = "";
                    }
                    Meetingobj.MEETINGPURPOSEID = Convert.ToInt32(ddlMeetingPurpose.SelectedValue);
                    if (txtbxWitnessNGO.Text != string.Empty)
                    {
                        Meetingobj.WITNESSNGO = txtbxWitnessNGO.Text;
                    }
                    else
                    {
                        Meetingobj.WITNESSNGO = "";
                    }
                    Meetingobj.OPINIONLEADER = txtbxOpinionLeader.Text;
                    Meetingobj.MINISTRYOFGLSD = txtbxMinistryofGLSD.Text;
                    Meetingobj.AESREP = txtbxAESRep.Text;
                    if (ChkMOUSigned.Checked == true)
                    {
                        Meetingobj.MOUSIGNED = "Yes";
                    }
                    else
                    {
                        Meetingobj.MOUSIGNED = "No";
                    }

                    if (txtbxComments.Text != string.Empty)
                    {
                        Meetingobj.MEETINGCOMMENTS = txtbxComments.Text;
                    }
                    else
                    {
                        Meetingobj.MEETINGCOMMENTS = "";
                    }


                    Meetingobj.ISDELETED = "False";
                    Meetingobj.CREATEDBY = Convert.ToInt32(uID);


                    MeetingBLL MeetingBLLobj = new MeetingBLL();
                    count = MeetingBLLobj.Insert(Meetingobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);
                    ClearMeetingData();
                    BindGrid1();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    BLLobj = null;
                }
            }
            else if (CULTURALMEETIDtxtbx.Text != string.Empty)
            {
                MeetingBLL BLLobj = new MeetingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    int CULTURALPROPID = Convert.ToInt32(ViewState["CULTURALPROPID"]);

                    Meeting Meetingobj = new Meeting();

                    Meetingobj.HHID = Convert.ToInt32(hhid);

                    Meetingobj.CULTURALMEETID = Convert.ToInt32(CULTURALMEETIDtxtbx.Text);
                    Meetingobj.CULTURALPROPID = CULTURALPROPID;
                    Meetingobj.MEETINGDATE = Convert.ToDateTime(MeetingDatePicker.Text.ToString());

                    Meetingobj.MEETINGLOCATION = txtbxLocation.Text;
                    Meetingobj.MEETINGPURPOSEID = Convert.ToInt32(ddlMeetingPurpose.SelectedValue);

                    Meetingobj.WITNESSNGO = txtbxWitnessNGO.Text;
                    Meetingobj.OPINIONLEADER = txtbxOpinionLeader.Text;
                    Meetingobj.MINISTRYOFGLSD = txtbxMinistryofGLSD.Text;
                    Meetingobj.AESREP = txtbxAESRep.Text;
                    if (ChkMOUSigned.Checked == true)
                    {
                        Meetingobj.MOUSIGNED = "Yes";
                    }
                    else
                    {
                        Meetingobj.MOUSIGNED = "No";
                    }
                    Meetingobj.MEETINGCOMMENTS = txtbxComments.Text;
                    Meetingobj.ISDELETED = "False";
                    Meetingobj.CREATEDBY = Convert.ToInt32(uID);

                    MeetingBLL MeetingBLLobj = new MeetingBLL();
                    count = MeetingBLLobj.UpdateMeeting(Meetingobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Data updated successfully');", true);
                    ClearMeetingData();
                    BindGrid1();
                    SetUpdateMode1(false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    BLLobj = null;
                }
            }
        }

        /// <summary>
        /// To clear the meeting information related fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MeetingInformBtnClear_Click(object sender, EventArgs e)
        {
            ClearMeetingData();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode1(false);
            }
        }

       /// <summary>
        /// To clear all fields
       /// </summary>
        private void ClearMeetingData()
        {
            MeetingDatePicker.Text = "";
            txtbxLocation.Text = string.Empty;
            ddlMeetingPurpose.ClearSelection();
            txtbxWitnessNGO.Text = string.Empty;
            txtbxOpinionLeader.Text = string.Empty;
            txtbxMinistryofGLSD.Text = string.Empty;
            txtbxAESRep.Text = string.Empty;
            ChkMOUSigned.Text = string.Empty;
            txtbxComments.Text = string.Empty;
            ChkMOUSigned.Checked = false;
            CULTURALMEETIDtxtbx.Text = string.Empty;
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdScheduled_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CULTURALNEGOID"] = e.CommandArgument;
                GetData();
                SetUpdateMode(true);
            }
        }

        /// <summary>
        /// Set link attributes to link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                ScheduledInformBtnSave.Text = "Update";
                ScheduledInformBtnClear.Text = "Cancel";
            }
            else
            {
                ScheduledInformBtnSave.Text = "Save";
                ScheduledInformBtnClear.Text = "Clear";
                ViewState["CULTURALNEGOID"] = "0";
            }
        }

       /// <summary>
        /// To get the data
       /// </summary>
        private void GetData()
        {

            NegoBLL BLLobj = new NegoBLL();
            int CULTURALNEGOID = 0;

            if (ViewState["CULTURALNEGOID"] != null)
                CULTURALNEGOID = Convert.ToInt32(ViewState["CULTURALNEGOID"]);

            Nego BOobj = new Nego();

            BOobj = BLLobj.GetData(CULTURALNEGOID);

            CULTURALNEGOIDtxtbx.Text = BOobj.CULTURALNEGOID.ToString();
            AppntDate.Text = Convert.ToString(BOobj.NEGO_APPOINTMENTDATE.ToString(UtilBO.DateFormat));
            txtbxVenueforNegotiation.Text = BOobj.NEGO_VENUE;
            NegotDate.Text = Convert.ToString(BOobj.NEGO_DATE.ToString(UtilBO.DateFormat));
            txtbxProblems.Text = BOobj.NEGO_PROBLEMDESC;
        }


        protected void grdScheduled_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMeeting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CULTURALMEETID"] = e.CommandArgument;
                GetMeetingData();
                SetUpdateMode1(true);
            }

        }

        /// <summary>
        /// Set link attributes to link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetUpdateMode1(bool updateMode)
        {
            if (updateMode)
            {
                MeetingButton1.Text = "Update";
                MeetingButton2.Text = "Cancel";
            }
            else
            {
                MeetingButton1.Text = "Save";
                MeetingButton2.Text = "Clear";
                ViewState["CULTURALMEETID"] = "0";
            }
        }

       /// <summary>
        /// To get details of meeting
       /// </summary>
        private void GetMeetingData()
        {
            MeetingBLL BLLobj = new MeetingBLL();
            int CULTURALMEETID = 0;

            if (ViewState["CULTURALMEETID"] != null)
                CULTURALMEETID = Convert.ToInt32(ViewState["CULTURALMEETID"]);

            Meeting BOobj = new Meeting();

            BOobj = BLLobj.GetMeetingData(CULTURALMEETID);

            CULTURALMEETIDtxtbx.Text = BOobj.CULTURALMEETID.ToString();
            MeetingDatePicker.Text = Convert.ToString(BOobj.MEETINGDATE.ToString(UtilBO.DateFormat));
            txtbxLocation.Text = BOobj.MEETINGLOCATION;

            ddlMeetingPurpose.ClearSelection();
            if (ddlMeetingPurpose.Items.FindByValue(BOobj.MEETINGPURPOSEID.ToString()) != null)
                ddlMeetingPurpose.Items.FindByValue(BOobj.MEETINGPURPOSEID.ToString()).Selected = true;

            txtbxWitnessNGO.Text = BOobj.WITNESSNGO;
            txtbxOpinionLeader.Text = BOobj.OPINIONLEADER;
            txtbxMinistryofGLSD.Text = BOobj.MINISTRYOFGLSD;
            txtbxAESRep.Text = BOobj.AESREP;

            if (BOobj.MOUSIGNED == "Yes")
            {
                ChkMOUSigned.Checked = true;
            }
            else if (BOobj.MOUSIGNED == "No")
            {
                ChkMOUSigned.Checked = false;
            }

            txtbxComments.Text = BOobj.MEETINGCOMMENTS;

        }


        protected void grdMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       /// <summary>
        /// To clear the scheduled information
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void ScheduledInformBtnClear_Click(object sender, EventArgs e)
        {
            ClearScheduledData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// To clear the fields
        /// </summary>
        private void ClearScheduledData()
        {
            txtbxVenueforNegotiation.Text = string.Empty;
            AppntDate.Text = "";
            NegotDate.Text = "";
            txtbxProblems.Text = string.Empty;
            CULTURALNEGOIDtxtbx.Text = string.Empty;
        }

        /// <summary>
        /// To change page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ScheduleChangePage(object sender, GridViewPageEventArgs e)
        {
            grdScheduled.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// To cahnge page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void MeetingChangePage(object sender, GridViewPageEventArgs e)
        {
            grdMeeting.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid1();
        }

        /// <summary>
        /// Set link attributes to Branch link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdScheduled_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime appointmentDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "NEGO_APPOINTMENTDATE"));
                if (appointmentDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litAppointmentDate")).Text = appointmentDate.ToString(UtilBO.DateFormat);

                DateTime negotiationDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "NEGO_DATE"));
                if (negotiationDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litNegotiationDate")).Text = negotiationDate.ToString(UtilBO.DateFormat);
            }
        }

        /// <summary>
        /// Set link attributes to Branch link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdMeeting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime meetingDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "MEETINGDATE"));
                if (meetingDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litMeetingDate")).Text = meetingDate.ToString(UtilBO.DateFormat);
            }
        }
    }
}