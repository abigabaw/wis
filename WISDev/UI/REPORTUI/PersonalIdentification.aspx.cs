using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using CrystalDecisions.Shared;


namespace WIS
{
    public partial class PersonalIdentification : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Personal Identification Report";
                else
                Master.PageHeader = "Personal Identification Report";

                BindTribe();
                BindReligion();
                BindOptiongroup();
                BindProject();
            }

        }
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
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindReligion()
        {
            ddlReligion.DataSource = (new MasterBLL()).LoadReligionData();
            ddlReligion.DataTextField = "ReligionName";
            ddlReligion.DataValueField = "ReligionID";
            ddlReligion.DataBind();
         }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindOptiongroup()
        {
            ddloptiongroup.DataSource = (new OptionGroupBLL()).GetOptionGroup();
            ddloptiongroup.DataTextField = "OptionGroupName";
            ddloptiongroup.DataValueField = "optiongroupid";
            ddloptiongroup.DataBind();
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindTribe()
        {
            ddlTribe.DataSource = (new TribeBLL()).FetchTribeList();
            ddlTribe.DataTextField = "TRIBENAME";
            ddlTribe.DataValueField = "TRIBEID";
            ddlTribe.DataBind();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClan(ddlTribe.SelectedItem.Value);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindClan(string tribeID)
        {
            ListItem firstListItem = new ListItem(ddlClan.Items[0].Text, ddlClan.Items[0].Value);
            ddlClan.Items.Clear();

            if (tribeID != "0")
            {
                ddlClan.DataSource = (new ClansBLL()).FetchClansList((Convert.ToInt32(tribeID)));
                ddlClan.DataTextField = "CLANNAME";
                ddlClan.DataValueField = "CLANID";
                ddlClan.DataBind();
            }
            ddlClan.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ListItem lstItem = null;

            lstItem = ddlTribe.Items[0];
            ddlTribe.Items.Clear();
            ddlTribe.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlClan.Items[0];
            ddlClan.Items.Clear();
            ddlClan.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlReligion.ClearSelection();
            ddloptiongroup.ClearSelection();
            ddlgender.ClearSelection();
            ddlProject.ClearSelection();
           
           // txthhid.Text = string.Empty;
            txtname.Text = string.Empty;

        }
    }
}