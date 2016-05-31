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
    public partial class PublicConsultation : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Public Consultation Report";
                else
                    Master.PageHeader = "Public Consultation Report";

                BindDistricts();
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
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindParishes(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);
            ddlParish.Items.Clear();

            if (subCounty != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCounty);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishID";
                ddlParish.DataBind();
            }

            ddlParish.Items.Insert(0, firstListItem);
            ddlParish.SelectedIndex = 0;
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindVillages(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);
            ddlVillage.Items.Clear();

            if (subCounty != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }
            ddlVillage.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);
            ddlSubCounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataBind();
            }
            ddlSubCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlCounty.Items.Clear();

            if (districtID != "0")
            {
                ddlCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();
            }
            ddlCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //txtPAPName.Text = "";

            ListItem lstItem = null;

            lstItem = ddlVillage.Items[0];
            ddlVillage.Items.Clear();
            ddlVillage.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlSubCounty.Items[0];
            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlParish.Items[0];
            ddlParish.Items.Clear();
            ddlParish.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlCounty.Items[0];
            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlDistrict.ClearSelection();
            ddlProject.ClearSelection();
        }
    }
}