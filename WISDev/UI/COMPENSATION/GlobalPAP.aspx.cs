using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS.UI.COMPENSATION
{
    public partial class GlobalPAP : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Call SetGridSource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            mskPlotReference.Mask = UtilBO.PlotReferenceMask;
            if (!IsPostBack)
            {
                Master.PageHeader = "Global PAP Search";

                BindDistricts();
                if (Request.QueryString["ProjectID"] != null)
                {
                    string Distict = Request.QueryString["Distict"].ToString();
                    string County = Request.QueryString["County"].ToString();
                    string SubCounty = Request.QueryString["SubCounty"].ToString();
                    string Parish = Request.QueryString["Parish"].ToString();
                    string Village = Request.QueryString["Village"].ToString();
                    Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"].ToString());
                    ddlDistrict.ClearSelection();
                    if (ddlDistrict.Items.FindByValue(Distict) != null)
                        ddlDistrict.Items.FindByValue(Distict).Selected = true;
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        BindCounties(ddlDistrict.SelectedItem.Value);
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByValue(County) != null)
                            ddlCounty.Items.FindByValue(County).Selected = true;
                        if (ddlCounty.SelectedIndex > 0)
                        {
                            BindSubCounties(ddlCounty.SelectedItem.Value);
                            uplSubCounty.Update();
                            ddlSubCounty.ClearSelection();
                            if (ddlSubCounty.Items.FindByValue(SubCounty) != null)
                                ddlSubCounty.Items.FindByValue(SubCounty).Selected = true;
                            if (ddlSubCounty.SelectedIndex > 0)
                            {
                                BindVillages(ddlSubCounty.SelectedItem.Value);
                                uplVillage.Update();
                                ddlVillage.ClearSelection();
                                if (ddlVillage.Items.FindByValue(Village) != null)
                                    ddlVillage.Items.FindByValue(Village).Selected = true;
                                BindParishes(ddlSubCounty.SelectedItem.Value);
                                uplParish.Update();
                                ddlParish.ClearSelection();
                                if (ddlParish.Items.FindByValue(Parish) != null)
                                    ddlParish.Items.FindByValue(Parish).Selected = true;
                            }
                        }
                    }
                }
                SetGridSource(false);
                grdPAPs.DataBind();
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSearch.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Bind Data to Drop Down
        /// </summary>
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To search Pap data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetGridSource(false);
            grdPAPs.DataBind();
        }
        /// <summary>
        /// Set data to Grid
        /// </summary>
        /// <param name="showRecentRecords"></param>
        protected void SetGridSource(bool showRecentRecords)
        {
            string district = String.Empty;
            string county = String.Empty;
            string subCounty = String.Empty;
            string parish = String.Empty;
            string village = String.Empty;
            int HHid = 0;
            string PapUid = String.Empty;
            if (txtHHID.Text.Trim() != "")
            {
                HHid = Convert.ToInt32(txtHHID.Text.Trim());
            }
            if (txtPAPUID.Text.Trim() != "")
            {
                PapUid = txtPAPUID.Text.Trim();
            }


            if (ddlDistrict.SelectedIndex > 0) district = ddlDistrict.SelectedItem.Text;
            if (ddlCounty.SelectedIndex > 0) county = ddlCounty.SelectedItem.Text;
            if (ddlSubCounty.SelectedIndex > 0) subCounty = ddlSubCounty.SelectedItem.Text;
            if (ddlParish.SelectedIndex > 0) parish = ddlParish.SelectedItem.Text;
            if (ddlVillage.SelectedIndex > 0) village = ddlVillage.SelectedItem.Text;

            PAP_HouseholdBLL objPAPLogic = new PAP_HouseholdBLL();
            grdPAPs.DataSource = objPAPLogic.GlobalSearchPAP(
                HHid,
                PapUid,
                txtPAPName.Text.Trim(),
                txtPlotReference.Text.Trim(),
                district,
                county,
                subCounty,
                parish,
                village,
                Convert.ToInt32(Session["USER_ID"].ToString()));
        }
        /// <summary>
        /// Call Respective method to fill data
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
        /// Bind Data to Drop Down
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
        /// Call Respective method to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCounty.SelectedIndex > 0)
            {
                BindSubCounties(ddlCounty.SelectedItem.Value);
            }
        }

        /// <summary>
        /// Bind Data to Drop Down
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
        /// Call Respective method to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCounty.SelectedIndex > 0)
            {
                BindVillages(ddlSubCounty.SelectedItem.Value);
                BindParishes(ddlSubCounty.SelectedItem.Value);
            }
        }

        /// <summary>
        /// Bind Data to Drop Down
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
        /// Bind Data to Drop Down
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
        /// To Change PAge Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPAPs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPAPs.PageIndex = e.NewPageIndex;
            SetGridSource(false);
            grdPAPs.DataBind();
        }
        /// <summary>
        ///  To Check Project Frozen
        /// </summary>
        public void getFrozen()
        {
            ProjectBLL ObjProjectBLL = new ProjectBLL();
            ProjectBO ObjProjectBO = new ProjectBO();

            ObjProjectBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);


            ObjProjectBO = ObjProjectBLL.getFrozen(ObjProjectBO);

            if ((ObjProjectBO) != null)
            {
                Session["FROZEN"] = ObjProjectBO.Frozen;
            }

        }
        /// <summary>
        /// TO Select Pap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void grdPAPs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetPAP")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["HH_ID"] = e.CommandArgument;

                Label lblProjectCode = (Label)gvr.FindControl("lblProjectCode");
                Label lblProjectedId = (Label)gvr.FindControl("lblProjectedId");
                Label lblviewstatus = (Label)gvr.FindControl("lblviewstatus");
                if (lblviewstatus.Text == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alertmes", "alert('Cannot Access. You are not included in " + lblProjectCode.Text + " Project.');", true);
                    return;
                }              

                Session["PROJECT_ID"] = lblProjectedId.Text.Trim();
                Session["FROZEN"] = null;
                getFrozen();
                Session["PROJECT_CODE"] = lblProjectedId.Text;

                PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
                PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHouseHoldData(Convert.ToInt32(e.CommandArgument));

                if (objHousehold != null)
                {
                    Session["HH_IDForDisplay"] = objHousehold.HhId;
                }
                if (Request.QueryString["ProjectID"] != null)
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    LinkButton lnkpap = (LinkButton)row.FindControl("lnkPAPName");
                    Label lblPlotreference = (Label)row.FindControl("lblPlotreference");
                    Session["SelPAPName"] = lnkpap.Text;
                    Session["SelPlotreference"] = lblPlotreference.Text;
                }
                Label l1 = (Label)gvr.FindControl("lblPaptype");
                string Paptype = l1.Text;
                if (Paptype.ToUpper() == "INS")
                {
                    Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Institution.aspx"));
                }
                else if (Paptype.ToUpper() == "GRP")
                {
                    Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Groupownership.aspx"));
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Household.aspx"));
                }
                
            }
            else if (e.CommandName == "ViewMap")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Session["HH_ID"] = e.CommandArgument;

                Label lblProjectCode = (Label)gvr.FindControl("lblProjectCode");
                Label lblProjectedId = (Label)gvr.FindControl("lblProjectedId");

                Session["PROJECT_ID"] = lblProjectedId.Text.Trim();
                Session["PROJECT_CODE"] = lblProjectedId.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewMap", "ViewMap(" + e.CommandArgument.ToString() + ")", true);
            }
        }
        /// <summary>
        /// to clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtHHID.Text = "";
            txtPAPUID.Text = "";
            txtPAPName.Text = "";
            txtPlotReference.Text = "";

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

            SetGridSource(false);
            grdPAPs.DataBind();
            upnPAP.Update();
        }
    }
}