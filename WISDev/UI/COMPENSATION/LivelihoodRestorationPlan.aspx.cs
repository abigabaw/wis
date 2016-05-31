using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class LivelihoodRestorationPlan : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            caldpcDate.Format = UtilBO.DateFormat;
            caldpcSettlementDate.Format = UtilBO.DateFormat;
            caldpcRecDate.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Livelihood Restoration - Plan";
                }
                else
                    Master.PageHeader = " Livelihood Restoration - Plan";

                if (Session["PROJECT_CODE"] == null)
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                //  string paramUploadDoc = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}','{5}');", Convert.ToInt32(Session["PROJECT_ID"]), Session["HH_ID"], Session["USER_ID"], Session["PROJECT_CODE"].ToString(), "NEW_LOC", 0);
                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", Convert.ToInt32(Session["PROJECT_ID"]), Session["HH_ID"], Session["USER_ID"], Session["PROJECT_CODE"].ToString(), "NEW_LOC");
                // lnkUploadDoc.Attributes.Add("onclick", paramUploadDoc);
                lnkUploadDoc.Attributes.Add("onclick", paramView);

                ClearNewLocation();
                getProjectDtaes();
                BindDropDownDistrict();
                LoadPresentLocation();
                LoadNewLocation();
                BindGrid();
                getCompensationStatus();
                dpcSettlementDate.Attributes.Add("readonly","readonly");
                dpcSettlementDate.Attributes.Add("onkeydown", "doCheck();");
                dpcDate.Attributes.Add("readonly", "readonly");
                dpcDate.Attributes.Add("onkeydown", "doCheck();");
                if (ViewStateLiveRestPlanId > 0)
                    grdItemsReceived.Visible = true;

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LIVELIHOOD_RESTORATION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;

                    grdRestorationPlan.Columns[8].Visible = false;
                    if(grdRestorationPlan.FooterRow != null)
                        grdRestorationPlan.FooterRow.Visible = false;
                }
              
            }
        }
        private void getProjectDtaes()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));

            if (objProject.ProjectStartDate != DateTime.MinValue)
                hfProjStartDate.Value = Convert.ToString(objProject.ProjectStartDate.ToString(UtilBO.DateFormat));

            if (objProject.ProjectEndDate != DateTime.MinValue)
                hfProjEndDate.Value = Convert.ToString(objProject.ProjectEndDate.ToString(UtilBO.DateFormat));

        }

        #region ViewStates/Sessions/Properties
        private int ViewStateNewLocationID
        {
            get
            {
                if (ViewState["NewLocationId"] != null)
                    return Convert.ToInt32(ViewState["NewLocationId"].ToString());
                else
                    return 0;
            }
            set { ViewState["NewLocationId"] = value; }
        }

        private int ViewStateLiveRestPlanId
        {
            get
            {
                if (ViewState["LiveRestPlanId"] != null)
                    return Convert.ToInt32(ViewState["LiveRestPlanId"]);
                else
                    return 0;
            }
            set
            {
                ViewState["LiveRestPlanId"] = value;
            }
        }

        private int ViewStateReceivedId
        {
            get
            {
                if (ViewState["LIV_REST_RECDID"] != null)
                    return Convert.ToInt32(ViewState["LIV_REST_RECDID"]);
                else
                    return 0;
            }
            set
            {
                ViewState["LIV_REST_RECDID"] = value;
            }
        }

        private int SessionHHID
        {
            get
            {
                if (Session["HH_ID"] != null)
                    return Convert.ToInt32(Session["HH_ID"]);
                else
                    return 0;
            }
            set
            {
                Session["HH_ID"] = value;
            }
        }

        private int SessionUserId
        {
            get
            {
                if (Session["USER_ID"] != null)
                    return Convert.ToInt32(Session["USER_ID"]);
                else
                    return 0;
            }
            //set
            //{
            //    Session["USER_ID"] = value;
            //}
        }

        private DataTable ViewStateGridTable
        {
            get
            {
                if (ViewState["GridData"] != null)
                    return ViewState["GridData"] as DataTable;
                else
                    return null;
            }
            set
            {
                ViewState["GridData"] = value;
            }
        }

        private decimal ViewStateTotalPlanned
        {
            get
            {
                if (ViewState["TotalPlanned"] != null)
                    return Convert.ToDecimal(ViewState["TotalPlanned"]);
                else
                    return 0;
            }
            set { ViewState["TotalPlanned"] = value; }
        }
        #endregion

        #region Utility Method
        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// st intial row for Grid
        /// </summary>
        /// <returns></returns>
        private DataTable SetInitialRow()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Liv_Rest_PlanID", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
            dt.Columns.Add(new DataColumn("Planned", typeof(string)));
            dt.Columns.Add(new DataColumn("Received", typeof(string)));
            dt.Columns.Add(new DataColumn("Balance", typeof(string)));
            dt.Columns.Add(new DataColumn("PlannedDate", typeof(string)));
            dr = dt.NewRow();
            dr["Liv_Rest_PlanID"] = 0;
            dr["ItemName"] = "";
            dr["UnitName"] = "";
            dr["Planned"] = 0;
            dr["Received"] = 0;
            dr["Balance"] = 0;
            dr["PlannedDate"] = DateTime.MinValue;

            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdRestorationPlan.DataSource = ds;
                grdRestorationPlan.DataBind();
                //grdItemsReceived.Rows[0].Visible = false;
                ViewState["setItemRow"] = "yes";

                //grdRestorationPlan.Columns[grdRestorationPlan.Columns.Count - 1].Visible = false;
                //grdRestorationPlan.Columns[grdRestorationPlan.Columns.Count - 2].Visible = false;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                grdRestorationPlan.DataSource = ds;
                grdRestorationPlan.DataBind();
                int columncount = grdRestorationPlan.Rows[0].Cells.Count;
                grdRestorationPlan.Rows[0].Cells.Clear();
                grdRestorationPlan.Rows[0].Cells.Add(new TableCell());
                //grdRestorationPlan.Rows[grdRestorationPlan.Columns.Count - 4].Cells.Add( DateTime.Now.ToString());
                grdItemsReceived.Rows[0].Visible = false;
                //grdRestorationPlan.Rows[0].Cells[0].ColumnSpan = columncount;
                //grdRestorationPlan.Rows[0].Cells[0].Text = "<b><center>No Records Found</center></b>";
                //gvData.Rows[0].Cells[0].Text=
            }
            //else
            //{
            //    grdRestorationPlan.DataSource = dt;
            //    grdRestorationPlan.DataBind();
            //}

            //Store the DataTable in ViewState
            //ViewStateGridTable = dt;
            return dt;
        }
        /// <summary>
        /// set Blank Item Details Row
        /// </summary>
        /// <returns></returns>
        private DataTable setBlankItemDetailsRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("LIV_REST_RECDID", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
            dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
            dt.Columns.Add(new DataColumn("Planned", typeof(string)));
            dt.Columns.Add(new DataColumn("Received", typeof(string)));
            dt.Columns.Add(new DataColumn("Balance", typeof(string)));
            dt.Columns.Add(new DataColumn("PlannedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceivedDate", typeof(DateTime)));
            dr = dt.NewRow();
            dr["LIV_REST_RECDID"] = 0;
            dr["ItemName"] = "";
            dr["Planned"] = "";
            dr["Received"] = -1;
            dr["Balance"] = 0;
            dr["PlannedDate"] = DateTime.MinValue;
            dr["ReceivedDate"] = DateTime.MinValue;
            dt.Rows.Add(dr);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdItemsReceived.DataSource = dt;
                grdItemsReceived.DataBind();
                ViewState["setItemDetailRow"] = "yes";

                //grdItemsReceived.Columns[grdItemsReceived.Columns.Count - 1].Visible = false;
                //grdItemsReceived.Columns[grdItemsReceived.Columns.Count - 2].Visible = false;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                grdItemsReceived.DataSource = ds;
                grdItemsReceived.DataBind();
                int columncount = grdItemsReceived.Rows[0].Cells.Count;
                grdItemsReceived.Rows[0].Cells.Clear();
                grdItemsReceived.Rows[0].Cells.Add(new TableCell());
                //  grdItemsReceived.Rows[0].Visible = false;

                //grdRestorationPlan.Rows[0].Cells[0].ColumnSpan = columncount;
                //grdRestorationPlan.Rows[0].Cells[0].Text = "<b><center>No Records Found</center></b>";
                //gvData.Rows[0].Cells[0].Text=
            }
            return dt;
        }
        #endregion

        #region Buttons
        /// <summary>
        /// To Save Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavePlanned_Click(object sender, EventArgs e)
        {
            AddLiveRestPlan();
        }
        /// <summary>
        /// To Cancel Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelLiveRestPlan();
        }
        /// <summary>
        /// To Save Item Detais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddItemDetail_Click(object sender, EventArgs e)
        {
            AddItemDetails();
        }
        /// <summary>
        /// To Cancel Item Detais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelItemDetail_Click(object sender, EventArgs e)
        {
            //start
            decimal BalanceAmount = 0, TotalPland = 0;
            if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                BalanceAmount = Convert.ToDecimal(hdnReceivedAmount.Value.ToString());
            BalanceAmount = BalanceAmount + Convert.ToDecimal(hdnEdit.Value);
            hdnReceivedAmount.Value = BalanceAmount.ToString();
            hdnEdit.Value = 0.ToString();
            if (!string.IsNullOrEmpty(hdnTotalPlannedAmt.Value))
                TotalPland = Convert.ToDecimal(hdnTotalPlannedAmt.Value);
            DisplyBAl((TotalPland - BalanceAmount).ToString());
            //end
            CancelItemDetails();
            Button btnCancelItemDetail;
            if (grdItemsReceived.FooterRow.FindControl("btnCancelItemDetail") is Button)
            {
                btnCancelItemDetail = grdItemsReceived.FooterRow.FindControl("btnCancelItemDetail") as Button;
                btnCancelItemDetail.Visible = false;
            }
        }

        #endregion

        #region Clear Buttons
        /// <summary>
        /// TO Clear Location details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearNewLocation();
        }
        /// <summary>
        /// TO Clear Location details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearNewLocation()
        {
            txtDistanceFromOldLocation.Text = string.Empty;
            dpcSettlementDate.Text = "";
            ddlDistrict.SelectedIndex = 0;

            ddlCounty.Items.Clear();
            ddlCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlCounty.SelectedIndex = 0;

            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlSubCounty.SelectedIndex = 0;

            ddlVillage.Items.Clear();
            ddlVillage.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlVillage.SelectedIndex = 0;

            ddlParish.Items.Clear();
            ddlParish.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlParish.SelectedIndex = 0;
        }

        private void ClearFooterRow()
        {
            
        }
        /// <summary>
        /// TO Clear Item details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearItemDetailsFooterRow()
        {
            txtRecReceived.Text = string.Empty;
            txtRecBalance.Text = string.Empty;
            dpcRecDate.Text = "";

            btnAddItemDetail.Text = "Add";
            btnAddItemDetail.Visible = true;
            btnCancelItemDetail.Text = "Cancel";
            btnCancelItemDetail.Visible = false;

            ViewStateReceivedId = 0;
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// to get Compensation Status
        /// </summary>
        private void getCompensationStatus()
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO oPaymentBO;//=new PaymentBO();
            oPaymentBO = oPaymentBLL.getPapValuation(SessionHHID);
            if (oPaymentBO != null)
            {
                if (!string.IsNullOrEmpty(oPaymentBO.PaymentStatus))
                {
                    if (oPaymentBO.PaymentStatus.ToLower() == "CP".ToLower())
                    {
                        dvCompensationStatus.Text = "Completely Paid";
                    }
                    else if (oPaymentBO.PaymentStatus.ToLower() == "PP".ToLower())
                    {
                        dvCompensationStatus.Text = "Partially Paid";
                    }
                    else if (oPaymentBO.PaymentStatus.ToLower() == "NP".ToLower())
                    {
                        dvCompensationStatus.Text = "Not Paid";
                    }
                }
                else
                    dvCompensationStatus.Text = "";
            }
        }
        /// <summary>
        /// to Load Present Location
        /// </summary>
        private void LoadPresentLocation()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            if (Session["HH_ID"] != null)
            {
                int HHID = Convert.ToInt32(Session["HH_ID"]);
                PAP_HouseholdBO oHouseHold = objHouseHoldBLL.GetHousaeHoldData(HHID);
                if (oHouseHold != null)
                {
                    dvDistrict.Text = oHouseHold.District;
                    dvCounty.Text = oHouseHold.County;
                    dvSubCounty.Text = oHouseHold.SubCounty;
                    dvParish.Text = oHouseHold.Parish;
                    dvVillage.Text = oHouseHold.Village;
                }
            }
        }
        /// <summary>
        /// to Load New Location
        /// </summary>
        private void LoadNewLocation()
        {
            LivelihoodRestLocationBLL oLivelihoodRestLocationBLL = new LivelihoodRestLocationBLL();
            LivelihoodRestLocationBO oLivelihoodRestLocationBO = new LivelihoodRestLocationBO();
            oLivelihoodRestLocationBO = oLivelihoodRestLocationBLL.GetNewLocation(SessionHHID);
            if (oLivelihoodRestLocationBO != null)
            {
                if (oLivelihoodRestLocationBO.Liv_Rest_LocationID > 0)
                {
                    ViewStateNewLocationID = oLivelihoodRestLocationBO.Liv_Rest_LocationID;
                    pnlRestorationPlan.Visible = true;
                    LoadUnits();
                    LoadItemsList();

                    if (oLivelihoodRestLocationBO.DistFrmOldLoc != null)
                        txtDistanceFromOldLocation.Text = oLivelihoodRestLocationBO.DistFrmOldLoc;

                    if (oLivelihoodRestLocationBO.DateOfSettlement != DateTime.MinValue)
                        dpcSettlementDate.Text = (oLivelihoodRestLocationBO.DateOfSettlement.ToString(UtilBO.DateFormat));
                    else
                        dpcSettlementDate.Text = "";

                    if (oLivelihoodRestLocationBO.NewDistrict != null)
                    {
                        ddlDistrict.ClearSelection();
                        if (ddlDistrict.Items.FindByText(oLivelihoodRestLocationBO.NewDistrict) != null)
                            ddlDistrict.Items.FindByText(oLivelihoodRestLocationBO.NewDistrict).Selected = true;
                    }
                    else
                    {
                        ddlDistrict.SelectedIndex = 0;
                    }

                    if (oLivelihoodRestLocationBO.NewCounty != null)
                    {
                        BindCounties();
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(oLivelihoodRestLocationBO.NewCounty) != null)
                            ddlCounty.Items.FindByText(oLivelihoodRestLocationBO.NewCounty).Selected = true;
                    }
                    else
                    {
                        ddlCounty.SelectedIndex = 0;
                    }

                    if (oLivelihoodRestLocationBO.NewSubCounty != null)
                    {
                        BindSubCounties();
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(oLivelihoodRestLocationBO.NewSubCounty) != null)
                            ddlSubCounty.Items.FindByText(oLivelihoodRestLocationBO.NewSubCounty).Selected = true;
                    }
                    else
                    {
                        ddlSubCounty.SelectedIndex = 0;
                    }

                    if (oLivelihoodRestLocationBO.NewVillage != null)
                    {
                        BindVillages();
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(oLivelihoodRestLocationBO.NewVillage) != null)
                            ddlVillage.Items.FindByText(oLivelihoodRestLocationBO.NewVillage).Selected = true;
                    }
                    else
                    {
                        ddlVillage.SelectedIndex = 0;
                    }

                    if (oLivelihoodRestLocationBO.NewParish != null)
                    {
                        BindParish();

                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(oLivelihoodRestLocationBO.NewParish) != null)
                            ddlParish.Items.FindByText(oLivelihoodRestLocationBO.NewParish).Selected = true;
                    }
                    else
                    {
                        ddlParish.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlDistrict.ClearSelection();
                    ddlParish.ClearSelection();
                    ddlVillage.ClearSelection();
                    ddlSubCounty.ClearSelection();
                    pnlRestorationPlan.Visible = false;
                }
            }
            else
                pnlRestorationPlan.Visible = false;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindDropDownDistrict()
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlDistrict.Items.Clear();

            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();

            ddlDistrict.Items.Insert(0, firstListItem);
            //ddlDistrict.Items.Insert(0, (new ListItem("--Select--", "0")));
            //ddlDistrict.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindCounties()
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlCounty.Items.Clear();
            MasterBLL objMasterBLL = new MasterBLL();
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataSource = objMasterBLL.LoadCountyData(ddlDistrict.SelectedValue);
                ddlCounty.DataBind();
            }
            ddlCounty.Items.Insert(0, firstListItem);
            ddlCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindSubCounties()
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);

            ddlSubCounty.Items.Clear();
            if (ddlCounty.SelectedValue != "0")
            {
                MasterBLL objMasterBLL = new MasterBLL();
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataSource = objMasterBLL.LoadSubCountyData(ddlCounty.SelectedValue);
                ddlSubCounty.DataBind();
            }
            ddlSubCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlSubCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindVillages()
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);

            ddlVillage.Items.Clear();

            if (ddlSubCounty.SelectedValue != "0")
            {
                MasterBLL objMasterBLL = new MasterBLL();
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataSource = objMasterBLL.LoadVillageData(ddlSubCounty.SelectedValue);
                ddlVillage.DataBind();
            }
            ddlVillage.Items.Insert(0, firstListItem);
            ddlVillage.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindParish()
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);

            ddlParish.Items.Clear();

            if (ddlSubCounty.SelectedValue != "0")
            {
                MasterBLL objMasterBLL = new MasterBLL();
                ddlParish.DataTextField = "parishname";
                ddlParish.DataValueField = "parishid";
                ddlParish.DataSource = objMasterBLL.LoadParishData(ddlSubCounty.SelectedValue);
                ddlParish.DataBind();
            }
            ddlParish.Items.Insert(0, firstListItem);
            ddlParish.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void LoadUnits()
        {
            UnitBLL oUnitBLL = new UnitBLL();
            UnitBO oUnitBO = new UnitBO();

            oUnitBO.UnitName = string.Empty;
            oUnitBO.UnitID = 0;
            ddlUnits.Items.Clear();
            ddlUnits.DataSource = oUnitBLL.GetUnit();

            ddlUnits.DataTextField = "UnitName";
            ddlUnits.DataValueField = "UnitID";
            ddlUnits.DataBind();

            ListItem firstListItem = new ListItem("--Select--", "0");
            ddlUnits.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void LoadItemsList()
        {
            LivelihoodRestoreItemsList lstLiveRestItems = new LivelihoodRestoreItemsList();
            LivelihoodRestoreItemsBLL oLiveRestItemsBLL = new LivelihoodRestoreItemsBLL();
            LivelihoodRestoreItemsBO oLiveRestItemsBO = new LivelihoodRestoreItemsBO();

            ddlRestoreItems.Items.Clear();
            lstLiveRestItems = oLiveRestItemsBLL.GetLiveRestItems();
            ddlRestoreItems.DataSource = lstLiveRestItems;
            ddlRestoreItems.DataTextField = "Liv_Rest_ItemName";
            ddlRestoreItems.DataValueField = "Liv_Rest_ItemID";
            ddlRestoreItems.DataBind();
            ListItem firstListItem = new ListItem("--Select--", "0");
            ddlRestoreItems.Items.Insert(0, firstListItem);
        }
        #endregion

        #region GridView Item Planned
        /// <summary>
        /// Bind data to Grid
        /// </summary>
        private void BindGrid()
        {
            LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
            LivelihoodRestorationList lstLiveRestPlan = new LivelihoodRestorationList();
            LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();
            if (ViewStateNewLocationID > 0)
            {
                lstLiveRestPlan = oLivRestPlanBLL.GetLiveRestPlan(ViewStateNewLocationID);
                grdRestorationPlan.DataSource = lstLiveRestPlan;
                grdRestorationPlan.DataBind();
            }
        }
        /// <summary>
        /// To cancel Plan
        /// </summary>
        private void CancelLiveRestPlan()
        {
            ClearFooterRow();
            BindGrid();
        }
        /// <summary>
        /// Get Plan details By ID
        /// </summary>
        private void getLiveRestPlanById()
        {
            decimal Planned = 0, Recieved = 0, Balance = 0;
            LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
            LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();

            oLiveRestPlanBO = oLivRestPlanBLL.GetLiveRestPlanById(ViewStateLiveRestPlanId);

            if (oLiveRestPlanBO != null)
            {
                ddlRestoreItems.SelectedValue = oLiveRestPlanBO.Liv_Rest_ItemID.ToString();
                ddlRestoreItems.Enabled = false;
                ddlUnits.SelectedValue = oLiveRestPlanBO.UnitID.ToString();
                ddlUnits.Enabled = false;

                if (oLiveRestPlanBO.Planned > 0)
                    txtCostPerUnit.Text = UtilBO.CurrencyFormat(oLiveRestPlanBO.UnitPrice);

                txtPlanned.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(oLiveRestPlanBO.Planned));
                if (!string.IsNullOrEmpty(txtPlanned.Text))
                    Planned = Convert.ToDecimal(txtPlanned.Text);

                txtRecieved.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(oLiveRestPlanBO.Received));
                if (!string.IsNullOrEmpty(txtRecieved.Text))
                    Recieved = Convert.ToDecimal(txtRecieved.Text);
                txtRecieved.ReadOnly = true;

                Balance = Planned - Recieved;

                txtBalance.Text = UtilBO.CurrencyFormat(Balance);
                string strDate = oLiveRestPlanBO.PlannedDate.ToString(UtilBO.DateFormat);
                if (strDate == "01-Jan-0001")
                {
                    dpcDate.Text = string.Empty;
 
                }             
                else
                {
                    dpcDate.Text = strDate;

                }

                    //oLiveRestPlanBO.PlannedDate;
                
                SetUpdateMode(true);
            }
        }
        /// <summary>
        /// Edit, Update,and Delete Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRestorationPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            pnlReceived.Visible = false;
            string message = string.Empty;

            if (e.CommandName == "Update")
            {
                ViewStateLiveRestPlanId = Convert.ToInt32(e.CommandArgument);
                AddLiveRestPlan();
            }
            else if (e.CommandName == "Cancel")
            {
                CancelLiveRestPlan();
            }
            else if (e.CommandName == "EditRow")
            {
                if (e.CommandArgument != null && e.CommandArgument.ToString() != "")
                {
                    ViewStateLiveRestPlanId = Convert.ToInt32(e.CommandArgument);
                    Button btnAdd, btnCancel;
                    //if (grdRestorationPlan.FooterRow.FindControl("btnAdd") is Button)
                    //{
                    //    btnAdd = grdRestorationPlan.FooterRow.FindControl("btnAdd") as Button;
                    //    btnAdd.Text = "Update";
                    //}
                    //if (grdRestorationPlan.FooterRow.FindControl("btnCancel") is Button)
                    //{
                    //    btnCancel = grdRestorationPlan.FooterRow.FindControl("btnCancel") as Button;
                    //    btnCancel.Text = "Cancel";
                    //    btnCancel.Visible = true;
                    //}
                    if (grdRestorationPlan.FooterRow.FindControl("ddlRestoreItems") is DropDownList)
                    {
                        DropDownList ddlRestoreItems = grdRestorationPlan.FooterRow.FindControl("ddlRestoreItems") as DropDownList;
                        ddlRestoreItems.Enabled = false;
                    }
                    if (grdRestorationPlan.FooterRow.FindControl("ddlUnits") is DropDownList)
                    {
                        DropDownList ddlUnits = grdRestorationPlan.FooterRow.FindControl("ddlRestoreItems") as DropDownList;
                        ddlUnits.Enabled = false;
                    }
                    BindGrid();
                    getLiveRestPlanById();
                }
            }
            else if (e.CommandName == "ClickItemName")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                Label lblRestItemName = (Label)row.FindControl("lblRestItemName");
                Label lblLiv_Rest_PlanID = (Label)row.FindControl("lblLiv_Rest_PlanID");
                Label lblPlanned = (Label)row.FindControl("lblPlanned");

                Label lblRecievedd = (Label)row.FindControl("lblRecieved");
                Label lblBalanceAmount = (Label)row.FindControl("lblBalance");

                Label lblDate = (Label)row.FindControl("lblDate");
                hfPlanedDate.Value = lblDate.Text;
                pnlReceived.Visible = true;
                if (e.CommandArgument != null && e.CommandArgument.ToString() != "")
                {
                    lblTReceived.Text = lblRestItemName.Text + " - " + "Received";
                    ViewStateLiveRestPlanId = Convert.ToInt32(lblLiv_Rest_PlanID.Text);
                    //ViewState["Liv_Rest_PlanID"] = lblLiv_Rest_PlanID.Text;

                    hdnReceivedAmount.Value = lblRecievedd.Text;
                    hdnBalanceAmount.Value = lblBalanceAmount.Text;
                    if (!string.IsNullOrEmpty(lblPlanned.Text))
                    {
                        ViewStateTotalPlanned = Convert.ToDecimal(lblPlanned.Text);
                        hdnTotalPlanned.Value = ViewStateTotalPlanned.ToString();
                        hdnTotalPlannedAmt.Value = ViewStateTotalPlanned.ToString();
                    }
                    DisplyBAl(lblBalanceAmount.Text);
                    BindItemDetails();
                }
                ClearItemDetailsFooterRow();
            }
            else if (e.CommandName == "DeleteRow")
            {
                LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();

                message = oLivRestPlanBLL.DeleteLivRestPlan(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                ClearPlannedDetails();
                SetUpdateMode(false);
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To set Balance amount
        /// </summary>
        /// <param name="sBal"></param>
        protected void DisplyBAl(string sBal)
        {
            lblBalanceAmountBtm.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(sBal));
            hdnBalanceAmount.Value = sBal;
        }
        //protected void grdRestorationPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //grdRestorationPlan.PageIndex = e.NewPageIndex;
        //    // BindGrid(true, false);
        //}
        /// <summary>
        /// Format Date Fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRestorationPlan_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal Planned = 0;
                decimal Recieved = 0;
                decimal Balance = 0;

                Label lblPlanned = e.Row.FindControl("lblPlanned") as Label;
                Label lblRecieved = e.Row.FindControl("lblRecieved") as Label;
                Label lblBalance = e.Row.FindControl("lblBalance") as Label;
                DateTime PlannedDate = Convert.ToDateTime(null);
                LinkButton lnkRestItemName = e.Row.FindControl("lnkRestItemName") as LinkButton;

                if (lnkRestItemName.Text.Trim().Length == 0)
                    e.Row.Visible = false;

                DateTime plannedDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PlannedDate"));
                if (plannedDate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblDate")).Text = plannedDate.ToString(UtilBO.DateFormat);

                if (e.Row.FindControl("lblPlanned") is Label)
                {
                    if (!string.IsNullOrEmpty(lblPlanned.Text))
                        Planned = Convert.ToDecimal(lblPlanned.Text);
                }
                lblPlanned.Text = UtilBO.CurrencyFormat(Planned);
                //Calculating Total_Unit_Price
                decimal CostPerUnit = 0, TotalUnitPrice = 0;
                if (e.Row.FindControl("lblCostPerUnit") is Label)
                {
                    Label lblCostPerUnit = e.Row.FindControl("lblCostPerUnit") as Label;
                    Label lblTotalAmount_Grid = e.Row.FindControl("lblTotalAmount_Grid") as Label;
                    if (!string.IsNullOrEmpty(lblCostPerUnit.Text))
                    {
                        CostPerUnit = Convert.ToDecimal(lblCostPerUnit.Text);
                        TotalUnitPrice = Planned * CostPerUnit;
                        lblTotalAmount_Grid.Text = UtilBO.CurrencyFormat(TotalUnitPrice);
                        lblCostPerUnit.Text = UtilBO.CurrencyFormat(CostPerUnit);
                    }
                    else
                        lblTotalAmount_Grid.Text = string.Empty;
                }

                if (e.Row.FindControl("lblRecieved") is Label)
                {
                    if (!string.IsNullOrEmpty(lblRecieved.Text))
                        Recieved = Convert.ToDecimal(lblRecieved.Text);
                }
                Balance = Planned - Recieved;
                if (e.Row.FindControl("lblBalance") is Label)
                {
                    lblBalance.Text = UtilBO.CurrencyFormat(Balance);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (e.Row.FindControl("dpcDate") is DatePickerControl.DatePicker)
                {
                    DatePickerControl.DatePicker dpcDate;

                    dpcDate = e.Row.FindControl("dpcDate") as DatePickerControl.DatePicker;
                    dpcDate.DateFormat = UtilBO.DateFormat;
                    string strDate = dpcDate.CalendarDate.ToString(UtilBO.DateFormat);
                    // oLiveRestPlanBO.PlannedDate = Convert.ToDateTime(strDate);//dpcDate.CalendarDate;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRestorationPlan_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //DropDownList ddlUnits = (DropDownList)e.Row.FindControl("ddlUnits");             
                //DropDownList ddlRestoreItems = (DropDownList)e.Row.FindControl("ddlRestoreItems");
            }
        }
        #endregion GridView Item Planned

        #region Item Received Grid
        /// <summary>
        /// Bind Item Details to grid
        /// </summary>
        private void BindItemDetails()
        {
            LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
            LivelihoodRestorationList lstLiveRestPlan = new LivelihoodRestorationList();
            LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();
            if (ViewStateNewLocationID > 0)
            {
                if (ViewStateLiveRestPlanId != 0)//(ViewState["Liv_Rest_PlanID"] != null)
                    lstLiveRestPlan = oLivRestPlanBLL.GetLivelihoodRestReceivedByPlanId(ViewStateLiveRestPlanId);// (Convert.ToInt32(ViewState["Liv_Rest_PlanID"].ToString()));
            }
            if (lstLiveRestPlan.Count > 0)
            {
                ClearItemDetailsFooterRow();

                hdnTotalPlanned.Value = ViewStateTotalPlanned.ToString();
                grdItemsReceived.Visible = true;
                //hdnReceivedAmount.Value=0.ToString();               
            }
            else if (lstLiveRestPlan.Count == 0)
            {
                hdnReceivedAmount.Value = "0";
                grdItemsReceived.Visible = false;
                //DataTable dt = setBlankItemDetailsRow();
            }
            grdItemsReceived.DataSource = lstLiveRestPlan;
            grdItemsReceived.DataBind();
        }
        /// <summary>
        /// Cancel Item Details
        /// </summary>
        private void CancelItemDetails()
        {
            ViewStateReceivedId = 0;
            BindItemDetails();
        }
        /// <summary>
        /// Edit,Update and Delete data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdItemsReceived_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "Update")
            {
                ViewStateLiveRestPlanId = Convert.ToInt32(e.CommandArgument);
                AddItemDetails();
            }
            else if (e.CommandName == "Cancel")
            {
                CancelItemDetails();
            }
            else if (e.CommandName == "EditRow")
            {
                if (e.CommandArgument != null && e.CommandArgument.ToString() != "")
                {
                    // Button btnAddItemDetail, btnCancelItemDetail;

                    ViewStateReceivedId = Convert.ToInt32(e.CommandArgument);

                    btnAddItemDetail.Text = "Update";
                    btnCancelItemDetail.Text = "Cancel";
                    btnCancelItemDetail.Visible = true;
                    //start
                    if (!string.IsNullOrEmpty(hdnEdit.Value) && Convert.ToDecimal(hdnEdit.Value) > 0)
                    {
                        decimal ReceivedAmount = 0;
                        decimal editval = 0, BalanceAmount = 0;
                        if (!string.IsNullOrEmpty(hdnEdit.Value))
                            editval = Convert.ToDecimal(hdnEdit.Value.ToString());
                        if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                            ReceivedAmount = Convert.ToDecimal(hdnReceivedAmount.Value.ToString());
                        ReceivedAmount = ReceivedAmount + editval;
                        hdnReceivedAmount.Value = ReceivedAmount.ToString();

                        BalanceAmount = Convert.ToDecimal(hdnTotalPlannedAmt.Value) - Convert.ToDecimal(hdnReceivedAmount.Value);
                        hdnBalanceAmount.Value = BalanceAmount.ToString();
                    }
                    //end
                    //BindItemDetails();
                    getReceivedPlanById();
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();
                LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();

                oLiveRestPlanBO = oLivRestPlanBLL.GetItemReceivedByPlanId(Convert.ToInt32(e.CommandArgument));

                if (oLiveRestPlanBO != null)
                {
                    //start
                    decimal BalanceAmount = 0, ReceivedAmount = 0;
                    if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                        ReceivedAmount = Convert.ToDecimal(hdnReceivedAmount.Value.ToString());
                    ReceivedAmount = ReceivedAmount - Convert.ToDecimal(oLiveRestPlanBO.Received.ToString());

                    hdnReceivedAmount.Value = ReceivedAmount.ToString();
                    BalanceAmount = Convert.ToDecimal(hdnTotalPlannedAmt.Value) - ReceivedAmount;
                    hdnBalanceAmount.Value = BalanceAmount.ToString();
                    //end
                }
                //end
                message = oLivRestPlanBLL.DeleteItemReceived(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                BindGrid();
                BindItemDetails();

                btnAddItemDetail.Text = "Add";
                btnCancelItemDetail.Text = "Cancel";
                btnCancelItemDetail.Visible = false;
                ClearItemDetailsFooterRow();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            checkBalanceAmount();
        }
        /// <summary>
        /// Format data like date and amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdItemsReceived_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal TotalPlanned = 0;
                decimal ReceivedItems = 0;
                decimal TotalItemBalance = 0;

                Label lblPlanned = e.Row.FindControl("lblPlanned") as Label;
                Label lblRecieved = e.Row.FindControl("lblRecieved") as Label;
                Label lblBalance = e.Row.FindControl("lblBalance") as Label;
                DateTime ReceivedDate = Convert.ToDateTime(null);

                Label lblDate = e.Row.FindControl("lblDate") as Label;

                if (lblRecieved.Text == "-1")
                    e.Row.Visible = false;

                DateTime dtReceivedDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ReceivedDate"));
                if (dtReceivedDate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblDate")).Text = dtReceivedDate.ToString(UtilBO.DateFormat);

                //------Start Balance Calculation------
                if (!string.IsNullOrEmpty(hdnTotalPlanned.Value))
                {
                    TotalPlanned = Convert.ToDecimal(hdnTotalPlanned.Value);
                }
                if (e.Row.FindControl("lblRecieved") is Label)
                {
                    if (!string.IsNullOrEmpty(lblRecieved.Text))
                        ReceivedItems = Convert.ToDecimal(lblRecieved.Text);
                }
                lblRecieved.Text = UtilBO.CurrencyFormat(ReceivedItems);
                // TotalPlanned = Convert.ToDecimal(hdnTotalPlanned.Value);
                //ReceivedItems = Convert.ToDecimal(lblRecieved.Text);
                TotalItemBalance = TotalPlanned - ReceivedItems;
                lblBalance.Text = UtilBO.CurrencyFormat(TotalItemBalance);
                hdnTotalPlanned.Value = TotalItemBalance.ToString();
                //------End Balance Calculation------
                checkBalanceAmount();
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (e.Row.FindControl("dpcDate") is DatePickerControl.DatePicker)
                {
                    DatePickerControl.DatePicker ReceivedDate;

                    ReceivedDate = e.Row.FindControl("dpcDate") as DatePickerControl.DatePicker;
                    ReceivedDate.DateFormat = UtilBO.DateFormat;
                    string strDate = ReceivedDate.CalendarDateString;
                    // oLiveRestPlanBO.PlannedDate = Convert.ToDateTime(strDate);//dpcDate.CalendarDate;
                }
            }
        }

        protected void grdItemsReceived_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        /// <summary>
        /// To Check Balance amount
        /// </summary>
        private void checkBalanceAmount()
        {
            //If Balance Amount is Zero then Hide the to enter the Add Button
            string BalanceAmount = hdnBalanceAmount.Value;
            if (Convert.ToDecimal(BalanceAmount) > 0)
                tabReceived.Visible = true;
            else
                tabReceived.Visible = false;
        }
        /// <summary>
        /// Get Recived plan details By Id
        /// </summary>
        private void getReceivedPlanById()
        {
            decimal Recieved = 0, Balance = 0;

            LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
            LivelihoodRestorationPlanBLL oLivRestPlanBLL = new LivelihoodRestorationPlanBLL();

            oLiveRestPlanBO = oLivRestPlanBLL.GetItemReceivedByPlanId(ViewStateReceivedId);

            if (oLiveRestPlanBO != null)
            {
                txtRecReceived.Text = oLiveRestPlanBO.Received.ToString();
                if (!string.IsNullOrEmpty(txtRecReceived.Text))
                    Recieved = Convert.ToDecimal(txtRecReceived.Text);

                txtRecBalance.Text = UtilBO.CurrencyFormat(Balance);
                //start
                decimal BalanceAmount = 0, TotalPland = 0;
                hdnEdit.Value = txtRecReceived.Text;
                if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                    BalanceAmount = Convert.ToDecimal(hdnReceivedAmount.Value.ToString());
                BalanceAmount = BalanceAmount - Convert.ToDecimal(txtRecReceived.Text);
                hdnReceivedAmount.Value = BalanceAmount.ToString();

                if (!string.IsNullOrEmpty(hdnTotalPlannedAmt.Value))
                    TotalPland = Convert.ToDecimal(hdnTotalPlannedAmt.Value);
                DisplyBAl((TotalPland - BalanceAmount).ToString());
                //end
                //BalanceAmount= BalanceAmount-
                dpcRecDate.Text = (oLiveRestPlanBO.ReceivedDate.ToString(UtilBO.DateFormat));


                Balance = Convert.ToDecimal(hdnTotalPlanned.Value);//Planned - Recieved;

                btnAddItemDetail.Text = "Update";
                btnCancelItemDetail.Text = "Cancel";
                btnCancelItemDetail.Visible = true;


            }
        }
        #endregion Item Received Grid

        #region Save/Update/Delete/Retrieve Section
        /// <summary>
        /// Call Save_NewLocation to Save data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save_NewLocation();
        }
        /// <summary>
        /// save New Location details
        /// </summary>
        private void Save_NewLocation()
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            LivelihoodRestLocationBO oLivelihoodRestLocationBO = new LivelihoodRestLocationBO();
            oLivelihoodRestLocationBO.HHID = SessionHHID;

            if (dpcSettlementDate.Text != null)
                oLivelihoodRestLocationBO.DateOfSettlement = Convert.ToDateTime(dpcSettlementDate.Text.ToString());
            else
                oLivelihoodRestLocationBO.DateOfSettlement = DateTime.MinValue;

            oLivelihoodRestLocationBO.DistFrmOldLoc = txtDistanceFromOldLocation.Text;
            if (ddlDistrict.Items.Count > 0)
                oLivelihoodRestLocationBO.NewDistrict = ddlDistrict.SelectedItem.ToString();
            if (ddlCounty.Items.Count > 0)
                oLivelihoodRestLocationBO.NewCounty = ddlCounty.SelectedItem.ToString();
            if (ddlSubCounty.Items.Count > 0)
                oLivelihoodRestLocationBO.NewSubCounty = ddlSubCounty.SelectedItem.ToString();
            if (ddlParish.Items.Count > 0)
                oLivelihoodRestLocationBO.NewParish = ddlParish.SelectedItem.ToString();
            if (ddlVillage.Items.Count > 0)
                oLivelihoodRestLocationBO.NewVillage = ddlVillage.SelectedItem.ToString();
            oLivelihoodRestLocationBO.CreatedBy = SessionUserId;
            oLivelihoodRestLocationBO.IsDeleted = "False";

            LivelihoodRestLocationBLL oLivelihoodRestLocationBLL = new LivelihoodRestLocationBLL();
            message = oLivelihoodRestLocationBLL.AddNewLocation(oLivelihoodRestLocationBO);
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
                LoadNewLocation();
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Save Plan details
        /// </summary>
        private void AddLiveRestPlan()
        {
            if (ViewStateNewLocationID > 0)
            {
                LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
                LivelihoodRestorationPlanBLL oLiveRestPlnaBLL = new LivelihoodRestorationPlanBLL();

                oLiveRestPlanBO.IsDeleted = "False";
                oLiveRestPlanBO.CreatedBy = SessionUserId;

                //if (btnSavePlanned.Text.ToUpper() == "SAVE")
                //{
                //    ViewStateLiveRestPlanId = 0;
                //}
                //else
                //{
                //    SetUpdateMode(false);
                //}
                
                oLiveRestPlanBO.Liv_Rest_PlanID = ViewStateLiveRestPlanId;
                oLiveRestPlanBO.Liv_Rest_LocationID = ViewStateNewLocationID;
                oLiveRestPlanBO.Liv_Rest_ItemID = Convert.ToInt32(ddlRestoreItems.SelectedValue.ToString());
                oLiveRestPlanBO.UnitID = Convert.ToInt32(ddlUnits.SelectedValue.ToString());
                oLiveRestPlanBO.Planned = Convert.ToDecimal(txtPlanned.Text);

                if (!string.IsNullOrEmpty(txtCostPerUnit.Text))
                    oLiveRestPlanBO.UnitPrice = Convert.ToDecimal(txtCostPerUnit.Text);
                else
                    oLiveRestPlanBO.UnitPrice = 0;

                if (!string.IsNullOrEmpty(txtRecieved.Text))
                    oLiveRestPlanBO.Received = Convert.ToDecimal(txtRecieved.Text);
                else
                    oLiveRestPlanBO.Received = 0;

                string strDate = dpcDate.Text;
                if (dpcDate.Text.ToString().Trim() != "" && dpcDate.Text.ToString().Trim() != "1/1/0001")
                    oLiveRestPlanBO.PlannedDate = Convert.ToDateTime(strDate);
                
                //btnAdd.Text = "Add";
                //btnCancel.Text = "Cancel";
                //btnCancel.Visible = false;

                string AlertMessage = string.Empty;
                string message = oLiveRestPlnaBLL.AddLivelihoodRestorationPlan(oLiveRestPlanBO);

                BindGrid();
                ClearFooterRow();
                SetUpdateMode(false);
                ClearPlannedDetails();

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";                    
                }
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Add New Location')", true);
        }
        /// <summary>
        /// Save Item Details
        /// </summary>
        private void AddItemDetails()
        {
            if (ViewStateNewLocationID > 0)
            {
                LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();
                LivelihoodRestorationPlanBLL oLiveRestPlnaBLL = new LivelihoodRestorationPlanBLL();

                decimal NewReceived = 0;
                decimal TotalPland = 0;
                decimal TotalReceivd = 0;
                //decimal BalanceAmount = 0;
                if (!string.IsNullOrEmpty(txtRecReceived.Text))
                    NewReceived = Convert.ToDecimal(txtRecReceived.Text);
                if (!string.IsNullOrEmpty(hdnTotalPlannedAmt.Value))
                    TotalPland = Convert.ToDecimal(hdnTotalPlannedAmt.Value);
                if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                    TotalReceivd = Convert.ToDecimal(hdnReceivedAmount.Value);
                //if (!string.IsNullOrEmpty(hdnBalanceAmount.Value))
                //    BalanceAmount = Convert.ToDecimal(hdnBalanceAmount.Value.ToString());
                //BalanceAmount= BalanceAmount-
                //if (NewReceived + 14 <= TotalPland)
                //{

                //start
                decimal BalanceAmount = 0;
                if (!string.IsNullOrEmpty(hdnReceivedAmount.Value))
                    BalanceAmount = Convert.ToDecimal(hdnReceivedAmount.Value.ToString());
                BalanceAmount = BalanceAmount + Convert.ToDecimal(txtRecReceived.Text);
                hdnReceivedAmount.Value = BalanceAmount.ToString();
                hdnEdit.Value = 0.ToString();
                DisplyBAl((TotalPland - BalanceAmount).ToString());
                //end

                oLiveRestPlanBO.IsDeleted = "False";
                oLiveRestPlanBO.CreatedBy = SessionUserId;
                oLiveRestPlanBO.Liv_Rest_PlanID = ViewStateLiveRestPlanId;
                oLiveRestPlanBO.Liv_Rest_LocationID = ViewStateNewLocationID;

                if (!string.IsNullOrEmpty(txtRecReceived.Text))
                    oLiveRestPlanBO.Received = Convert.ToDecimal(txtRecReceived.Text);
                else
                    oLiveRestPlanBO.Received = 0;

                string strDate = string.Empty;
                if (dpcRecDate.Text.Trim() != "")
                {
                    strDate = dpcRecDate.Text;
                    oLiveRestPlanBO.ReceivedDate = Convert.ToDateTime(strDate);//dpcDate.CalendarDate;
                }

                btnAddItemDetail.Text = "Add";
                btnCancelItemDetail.Text = "Cancel";
                btnCancelItemDetail.Visible = false;

                oLiveRestPlanBO.Liv_Rest_RecdID = ViewStateReceivedId;
                oLiveRestPlanBO.Liv_Rest_PlanID = ViewStateLiveRestPlanId;
                oLiveRestPlanBO.IsDeleted = "False";
                oLiveRestPlanBO.CreatedBy = SessionUserId;

                string message = oLiveRestPlnaBLL.AddLiveReceivedPlan(oLiveRestPlanBO);
                string AlertMessage = string.Empty;
                BindItemDetails();
                BindGrid();
                ClearItemDetailsFooterRow();

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                //}
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Add New Location')", true);

            checkBalanceAmount();
        }
        #endregion

        #region DropDowns Selected Index Changed
        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties();
            BindSubCounties();
            uplSubCounty.Update();
            BindVillages();
            uplVillage.Update();
            BindParish();
            uplParish.Update();
        }
        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages();
            BindParish();

        }
        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties();
        }

        protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlParish_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        /// <summary>
        /// Set Update Mode to buttons
        /// </summary>
        /// <param name="updateMode"></param>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSavePlanned.Text = "Update";
                btnClearPlanned.Text = "Cancel";
            }
            else
            {
                btnSavePlanned.Text = "Save";
                btnClearPlanned.Text = "Clear";
                ViewStateLiveRestPlanId = 0;
            }
        }
        /// <summary>
        /// to clear planed Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearPlanned_Click(object sender, EventArgs e)
        {
            ClearPlannedDetails();

            ViewStateLiveRestPlanId = 0;

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// to clear planed Details
        /// </summary>
        protected void ClearPlannedDetails()
        {
            ddlRestoreItems.ClearSelection();
            ddlUnits.ClearSelection();
            ddlRestoreItems.Enabled = true;
            ddlUnits.Enabled = true;

            txtPlanned.Text = string.Empty;
            txtRecieved.Text = string.Empty;
            txtBalance.Text = string.Empty;
            txtCostPerUnit.Text = string.Empty;
            lblTotalAmount_Plan.Text = string.Empty;
            dpcDate.Text = "";
        }
    }
}

