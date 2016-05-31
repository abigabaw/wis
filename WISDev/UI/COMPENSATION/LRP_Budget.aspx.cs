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
    public partial class LRP_Budget : System.Web.UI.Page
    {
        #region Declaration & Load
        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (!IsPostBack)
            {
                // dvDistrict.InnerHtml = "Testing Datataatat";
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Livelihood Restoration - Budget";
                }
                else
                    Master.PageHeader = " Livelihood Restoration - Budget";

                ClearFields();
                txtTotal.Attributes.Add("ReadOnly", "ReadOnly");
                LoadRestoreItems();
                LoadCategory();
                BindDropDownDistrict();
                LoadNewLocation();

                BindGrid();
                txtNoOfBeneficial.Attributes.Add("onchange", "setDirtyText();");
                txtItemQuantity.Attributes.Add("onchange", "setDirtyText();");
                txtCostPerUnit.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LIVELIHOOD_RESTORATION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;

                    grdRestBudget.Columns[11].Visible = false;
                    grdRestBudget.Columns[12].Visible = false;

                }             
            
            }
        }
        #endregion
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

        #region ViewStates/Sessions/Properties

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

        private int ViewStateLivRestBudgetID
        {
            get
            {
                if (ViewState["LIV_RES_BUDGID"] != null)
                    return Convert.ToInt32(ViewState["LIV_RES_BUDGID"]);
                else
                    return 0;
            }
            set
            {
                ViewState["LIV_RES_BUDGID"] = value;
            }
        }
        #endregion

        #region Clear Buttons
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        /// <summary>
        /// To Clear Data in Fields
        /// </summary>
        private void ClearFields()
        {
            // ClearNewLocation();
            ddlCategory.SelectedIndex = 0;
            ddlItem.SelectedIndex = 0;
            ddlItem.Items.Clear();
            for (int i = 0; i < chklst.Items.Count; i++)
                chklst.Items[i].Selected = false;

            //chklst.Items.Clear();
            txtItemDescription.Text = string.Empty;
            txtCostPerUnit.Text = string.Empty;
            txtItemQuantity.Text = string.Empty;
            txtNoOfBeneficial.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtComments.Text = string.Empty;

            ddlCounty.Items.Clear();
            ddlSubCounty.Items.Clear();
            ddlParish.Items.Clear();
            lstbVillages.Items.Clear();

            chkAllVillages.Checked = false;

            ddlItem.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlSubCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlParish.Items.Insert(0, (new ListItem("--Select--", "0")));

            ddlDistrict.SelectedIndex = 0;
            ddlCounty.SelectedIndex = 0;
            ddlSubCounty.SelectedIndex = 0;
            ddlParish.SelectedIndex = 0;

            btnSave.Text = "Save";
            btnClear.Text = "Clear";
            ViewStateLivRestBudgetID = 0;
        }


        /// <summary>
        /// Save data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddLiveRestBudget();
        }

        #endregion

        #region Load Methods
        private void LoadNewLocation()
        {
            //NewLocationBLL oNewLocationBLL = new NewLocationBLL();
            //NewLocationBO oNewLocationBO = new NewLocationBO();
            //oNewLocationBO = oNewLocationBLL.GetNewLocation(SessionHHID);

            //if (oNewLocationBO != null)
            //{
            //    //if (oNewLocationBO.NewPlotNo != null)
            //    //    txtNewPlotNumber.Text = oNewLocationBO.NewPlotNo;

            //    if (oNewLocationBO.DistanceFromOldPlot != null)
            //        txtDistanceFromOldLocation.Text = oNewLocationBO.DistanceFromOldPlot;

            //    if (oNewLocationBO.District != null)
            //    {
            //        ddlDistrict.Items.Insert(0, (new ListItem("--Select--", "0")));
            //        ddlDistrict.SelectedItem.Text = oNewLocationBO.District;
            //        dvDistrict.InnerText = oNewLocationBO.District;
            //    }
            //    else
            //    {
            //        ddlDistrict.SelectedIndex = 0;
            //        dvDistrict.InnerHtml = "&nbsp;";
            //    }

            //    if (oNewLocationBO.County != null)
            //    {
            //        // BindCounties();
            //        ddlCounty.Items.Clear();
            //        ddlCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            //        ddlCounty.SelectedItem.Text = oNewLocationBO.County;
            //        dvCounty.InnerText = oNewLocationBO.County;
            //    }
            //    else
            //    {
            //        ddlCounty.SelectedIndex = 0;
            //        dvCounty.InnerHtml = "&nbsp;";
            //    }

            //    if (oNewLocationBO.SubCounty != null)
            //    {
            //        //BindSubCounties();
            //        ddlSubCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            //        ddlSubCounty.SelectedItem.Text = oNewLocationBO.SubCounty;
            //        dvSubCounty.InnerText = oNewLocationBO.SubCounty;
            //    }
            //    else
            //    {
            //        ddlSubCounty.SelectedIndex = 0;
            //        dvSubCounty.InnerHtml = "&nbsp;";
            //    }

            //    if (oNewLocationBO.Village != null)
            //    {
            //        //BindVillages();
            //        ddlVillage.Items.Clear();
            //        ddlVillage.Items.Insert(0, (new ListItem("--Select--", "0")));
            //        ddlVillage.SelectedItem.Text = oNewLocationBO.Village;
            //        dvVillage.InnerText = oNewLocationBO.Village;
            //    }
            //    else
            //    {
            //        ddlVillage.SelectedIndex = 0;
            //        dvVillage.InnerHtml = "&nbsp;";
            //    }

            //    if (oNewLocationBO.Parish != null)
            //    {
            //        //BindParishes();
            //        ddlParish.Items.Clear();
            //        ddlParish.Items.Insert(0, (new ListItem("--Select--", "0")));
            //        ddlParish.SelectedItem.Text = oNewLocationBO.Parish;
            //        dvParish.InnerText = oNewLocationBO.Parish;
            //    }
            //    else
            //    {
            //        ddlParish.SelectedIndex = 0;
            //        dvParish.InnerHtml = "&nbsp;";
            //    }
            //}
        }
        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlDistrict.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindCounties()
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);

            ddlCounty.Items.Clear();

            if (ddlDistrict.SelectedValue != "0")
            {
                MasterBLL objMasterBLL = new MasterBLL();
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataSource = objMasterBLL.LoadCountyData(ddlDistrict.SelectedValue);
                ddlCounty.DataBind();
            }
            ddlCounty.Items.Insert(0, firstListItem);
            ddlCounty.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Drop Downs
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
            ddlSubCounty.Items.Insert(0, firstListItem);
            ddlSubCounty.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindVillages()
        {
            //ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);

            lstbVillages.Items.Clear();

            if (ddlSubCounty.SelectedValue != "0")
            {
                MasterBLL objMasterBLL = new MasterBLL();
                List<VillageBO> lstVillages = new List<VillageBO>();
             
                lstVillages = objMasterBLL.LoadVillageData(ddlSubCounty.SelectedValue);
                lstbVillages.DataSource = lstVillages;
                lstbVillages.DataTextField = "VillageName";
                lstbVillages.DataValueField = "VillageID";
                lstbVillages.DataBind();
            }
        }

        /// <summary>
        /// Bind Data to Drop Downs
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
        /// Bind Data to Drop Downs
        /// </summary>
        private void LoadRestoreItems()
        {
            LivelihoodBudgetItemsList lstLiveBudgItems = new LivelihoodBudgetItemsList();
            LivelihoodBudgetItemsBLL oLiveBudgItemsBLL = new LivelihoodBudgetItemsBLL();
            LivelihoodBudgetItemsBO oLiveBudgItemsBO = new LivelihoodBudgetItemsBO();
            if (ddlCategory.SelectedIndex > 0)
            {
                oLiveBudgItemsBO.Liv_Bud_CategID = Convert.ToInt32(ddlCategory.SelectedValue.ToString());

                if (ddlItem.SelectedIndex > 0)
                {
                    oLiveBudgItemsBO.Liv_Bud_ItemID = Convert.ToInt32(ddlItem.SelectedValue.ToString());
                }
                else
                    oLiveBudgItemsBO.Liv_Bud_ItemID = 0;
                ddlItem.Items.Clear();
                lstLiveBudgItems = oLiveBudgItemsBLL.GetLivBudgetItems(oLiveBudgItemsBO);
                ddlItem.DataSource = lstLiveBudgItems;
                ddlItem.DataTextField = "Liv_Bud_ItemName";
                ddlItem.DataValueField = "Liv_Bud_ItemID";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, (new ListItem("--Select--", "0")));
                ddlItem.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void LoadCategory()
        {
            LivelihoodBudgetCategoryBLL oLiveBudgCategoryBLL = new LivelihoodBudgetCategoryBLL();
            LivelihoodBudgetCategoryList oLiveBudgCategoryList = new LivelihoodBudgetCategoryList();
            oLiveBudgCategoryList = oLiveBudgCategoryBLL.GetLivBdgCategory();
            ddlCategory.DataSource = oLiveBudgCategoryList;
            ddlCategory.DataTextField = "Liv_Bud_CategoryName";
            ddlCategory.DataValueField = "Liv_Bud_CategID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlCategory.SelectedIndex = 0;
        }

        #endregion

        #region GridView
        /// <summary>
        /// Bind Data to Grid
        /// </summary>
        private void BindGrid()
        {
            // LivelihoodRestBudgetBO oLiveRestBudgetBO = new LivelihoodRestBudgetBO();
            LivelihoodRestBudgetList lstLiveRestBudget = new LivelihoodRestBudgetList();
            LivelihoodRestBudgetBLL oLivRestPlanBLL = new LivelihoodRestBudgetBLL();
            lstLiveRestBudget = oLivRestPlanBLL.GetLiveRestBudget(Convert.ToInt32(Session["PROJECT_ID"]));
            if (lstLiveRestBudget.Count > 0)
            {
                grdRestBudget.DataSource = lstLiveRestBudget;
                grdRestBudget.DataBind();
            }
            else
            {
                grdRestBudget.DataSource = null;
                grdRestBudget.DataBind();
            }
        }

        protected void grdRestBudget_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdRestBudget.EditIndex = e.NewEditIndex;
            //BindGrid();
        }

        protected void grdRestBudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //grdRestBudget.EditIndex = -1;
            //BindGrid();
        }

        protected void grdRestBudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdRestBudget.EditIndex = -1;
            //BindGrid();
        }
        /// <summary>
        /// To edit and Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRestBudget_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewStateLivRestBudgetID = Convert.ToInt32(e.CommandArgument);
                getLiveRestBudgetById();
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewStateLivRestBudgetID = Convert.ToInt32(e.CommandArgument);
                DeleteLivRestBudget();
                BindGrid();
                ClearFields();
            }
        }
        /// <summary>
        /// To Change Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRestBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRestBudget.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdRestBudget_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decimal Planned = 0;
                //decimal Recieved = 0;
                //decimal Balance = 0;

                //Label lblPlanned = e.Row.FindControl("lblPlanned") as Label;
                //Label lblRecieved = e.Row.FindControl("lblRecieved") as Label;
                //Label lblBalance = e.Row.FindControl("lblBalance") as Label;

                //if (e.Row.FindControl("lblPlanned") is Label)
                //{
                //    if (!string.IsNullOrEmpty(lblPlanned.Text))
                //        Planned = Convert.ToDecimal(lblPlanned.Text);
                //}
                //if (e.Row.FindControl("lblRecieved") is Label)
                //{
                //    if (!string.IsNullOrEmpty(lblRecieved.Text))
                //        Recieved = Convert.ToDecimal(lblRecieved.Text);
                //}
                //Balance = Planned - Recieved;
                //if (e.Row.FindControl("lblBalance") is Label)
                //{
                //    lblBalance.Text = Balance.ToString();
                //}
            }
        }

        protected void grdRestBudget_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //DropDownList ddlUnits = (DropDownList)e.Row.FindControl("ddlUnits");
                //UnitBLL oUnitBLL = new UnitBLL();
                //UnitBO oUnitBO = new UnitBO();

                //oUnitBO.UnitName = string.Empty;
                //oUnitBO.UnitID = 0;

                //ddlUnits.DataSource = oUnitBLL.GetUnit();

                //ddlUnits.DataTextField = "UnitName";
                //ddlUnits.DataValueField = "UnitID";
                //ddlUnits.DataBind();


            }
        }
        #endregion

        #region Save/Update/Delete/Retrieve Section
        /// <summary>
        /// To Save And update Data
        /// </summary>
        private void AddLiveRestBudget()
        {
            LivelihoodRestBudgetBO oLiveRestBudgetBO = new LivelihoodRestBudgetBO();
            LivelihoodRestBudgetBLL oLiveRestBudgetBLL = new LivelihoodRestBudgetBLL();

            oLiveRestBudgetBO.IsDeleted = "False";
            oLiveRestBudgetBO.CreatedBy = SessionUserId;

            oLiveRestBudgetBO.Liv_Bud_ItemID = Convert.ToInt32(ddlItem.SelectedValue.ToString());
            oLiveRestBudgetBO.Liv_Bud_CategID = Convert.ToInt32(ddlCategory.SelectedValue.ToString());

            //foreach(
            if (chklst.Items[0].Selected == true)
                oLiveRestBudgetBO.ImplCost = "Yes";
            else
                oLiveRestBudgetBO.ImplCost = "No";

            if (chklst.Items[1].Selected == true)
                oLiveRestBudgetBO.OperCost = "Yes";
            else
                oLiveRestBudgetBO.OperCost = "No";

            if (chklst.Items[2].Selected == true)
                oLiveRestBudgetBO.ExternalMonitory = "Yes";
            else
                oLiveRestBudgetBO.ExternalMonitory = "No";

            oLiveRestBudgetBO.NoOfBeneficial = Convert.ToDecimal(txtNoOfBeneficial.Text);
            oLiveRestBudgetBO.ItemQuantity = Convert.ToDecimal(txtItemQuantity.Text);
            oLiveRestBudgetBO.CostPerUnit = Convert.ToDecimal(txtCostPerUnit.Text);
            oLiveRestBudgetBO.TotalAmount = Convert.ToDecimal(txtTotal.Text);

            oLiveRestBudgetBO.Comments = txtComments.Text;

            oLiveRestBudgetBO.District = ddlDistrict.SelectedItem.Text;
            oLiveRestBudgetBO.County = ddlCounty.SelectedItem.Text;
            oLiveRestBudgetBO.SubCounty = ddlSubCounty.SelectedItem.Text;
            oLiveRestBudgetBO.Parish = ddlParish.SelectedItem.Text;
            oLiveRestBudgetBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            string AlertMessage = string.Empty;
            string retMessage = string.Empty;

            if (ViewStateLivRestBudgetID == 0)
            {
                //ADD SECTION
                string[] message = oLiveRestBudgetBLL.AddLiveRestBudget(oLiveRestBudgetBO);

                if (string.IsNullOrEmpty(message[0]) || message[0] == "" || message[0] == "null")
                {
                    // message[0] = "Insertion Successfull";
                    if (!string.IsNullOrEmpty(message[1]))
                    {
                        LiveRestBudVillagesBLL oLiveRestBudVillagesBLL = new LiveRestBudVillagesBLL();
                        LiveRestBudVillagesBO oLiveRestBudVillagesBO = new LiveRestBudVillagesBO();
                        oLiveRestBudVillagesBO.IsDeleted = "False";
                        oLiveRestBudVillagesBO.CreatedBy = SessionUserId;
                        oLiveRestBudVillagesBO.Liv_Res_BudgId = Convert.ToInt32(message[1]);

                        for (int i = 0; i < lstbVillages.Items.Count; i++)
                        {
                            if (lstbVillages.Items[i].Selected)
                            {
                                oLiveRestBudVillagesBO.Village = lstbVillages.Items[i].Text;
                                retMessage = oLiveRestBudVillagesBLL.AddLiveRestBudVillages(oLiveRestBudVillagesBO);
                            }
                        }
                        if (string.IsNullOrEmpty(retMessage) || retMessage == "" || retMessage == "null")
                        {
                            retMessage = "Data saved successfully";
                            message[0] = "Data saved successfully";
                        }
                    }
                }
            }
            else
            {
                //UPDATE SECTION
                if (ViewStateLivRestBudgetID > 0)
                {
                    oLiveRestBudgetBO.Liv_Res_BudgID = ViewStateLivRestBudgetID;
                    oLiveRestBudgetBO.UpdatedBy = SessionUserId;
                    oLiveRestBudgetBO.CreatedBy = SessionUserId;
                    retMessage = oLiveRestBudgetBLL.UpdateLiveRestBudget(oLiveRestBudgetBO);

                    if (string.IsNullOrEmpty(retMessage) || retMessage == "" || retMessage == "null")
                    {
                        LiveRestBudVillagesBLL oLiveRestBudVillagesBLL = new LiveRestBudVillagesBLL();
                        LiveRestBudVillagesBO oLiveRestBudVillagesBO = new LiveRestBudVillagesBO();
                        oLiveRestBudVillagesBO.IsDeleted = "False";
                        oLiveRestBudVillagesBO.CreatedBy = SessionUserId;
                        oLiveRestBudVillagesBO.Liv_Res_BudgId = ViewStateLivRestBudgetID;
                        //DELETE & INSERT CONCEPT
                        oLiveRestBudVillagesBLL.DeleteLiveRestBudVillages(ViewStateLivRestBudgetID); //DELETE
                        for (int i = 0; i < lstbVillages.Items.Count; i++)
                        {
                            if (lstbVillages.Items[i].Selected)
                            {
                                oLiveRestBudVillagesBO.Village = lstbVillages.Items[i].Text;
                                retMessage = oLiveRestBudVillagesBLL.UpdateLiveRestBudVillages(oLiveRestBudVillagesBO); //INSERT
                            }
                        }
                        retMessage = "Data updated successfully";
                    }
                }
            }
            ClearFields();
            BindGrid();
            AlertMessage = "alert('" + retMessage + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To Delete
        /// </summary>
        private void DeleteLivRestBudget()
        {
            LivelihoodRestBudgetBO oLiveRestBudgetBO = new LivelihoodRestBudgetBO();
            LivelihoodRestBudgetBLL oLiveRestBudgetBLL = new LivelihoodRestBudgetBLL();
            if (ViewStateLivRestBudgetID > 0)
                oLiveRestBudgetBLL.DeleteLiveRestBudget(ViewStateLivRestBudgetID);
        }
        /// <summary>
        /// Get Data by ID
        /// </summary>
        private void getLiveRestBudgetById()
        {
            LivelihoodRestBudgetBO oLiveRestBudgetBO = new LivelihoodRestBudgetBO();
            LivelihoodRestBudgetBLL oLiveRestBudgetBLL = new LivelihoodRestBudgetBLL();

            if (ViewStateLivRestBudgetID > 0)
            {
                oLiveRestBudgetBO = oLiveRestBudgetBLL.GetLiveRestBudgetByID(ViewStateLivRestBudgetID);
                ddlCategory.SelectedValue = oLiveRestBudgetBO.Liv_Bud_CategID.ToString();
                LoadRestoreItems();

                ddlItem.ClearSelection();
                if (ddlItem.Items.FindByValue(oLiveRestBudgetBO.Liv_Bud_ItemID.ToString()) != null)
                    ddlItem.Items.FindByValue(oLiveRestBudgetBO.Liv_Bud_ItemID.ToString()).Selected = true;
                // ddlItem.SelectedValue = oLiveRestBudgetBO.Liv_Bud_ItemID.ToString();

                //Item Change Event
                #region
                LivelihoodBudgetItemsList lstLiveBudgItems = new LivelihoodBudgetItemsList();
                LivelihoodBudgetItemsBLL oLiveBudgItemsBLL = new LivelihoodBudgetItemsBLL();
                LivelihoodBudgetItemsBO oLiveBudgItemsBO = new LivelihoodBudgetItemsBO();
                oLiveBudgItemsBO.Liv_Bud_CategID = Convert.ToInt32(ddlCategory.SelectedValue.ToString());

                if (ddlItem.SelectedIndex > 0)
                {
                    oLiveBudgItemsBO.Liv_Bud_ItemID = Convert.ToInt32(ddlItem.SelectedValue.ToString());

                    lstLiveBudgItems = oLiveBudgItemsBLL.GetLivBudgetItems(oLiveBudgItemsBO);

                    if (lstLiveBudgItems.Count > 0)
                    {
                        if (lstLiveBudgItems[0].Liv_Bud_ItemDesc != null)
                            txtItemDescription.Text = lstLiveBudgItems[0].Liv_Bud_ItemDesc.ToString();
                        else
                            txtItemDescription.Text = string.Empty;
                    }
                }
                #endregion

                #region CheckBox Section
                if (oLiveRestBudgetBO.ImplCost == "Yes")
                    chklst.Items[0].Selected = true;
                else
                    chklst.Items[0].Selected = false;

                if (oLiveRestBudgetBO.OperCost == "Yes")
                    chklst.Items[1].Selected = true;
                else
                    chklst.Items[1].Selected = false;

                if (oLiveRestBudgetBO.ExternalMonitory == "Yes")
                    chklst.Items[2].Selected = true;
                else
                    chklst.Items[2].Selected = false;
                #endregion

                txtNoOfBeneficial.Text = oLiveRestBudgetBO.NoOfBeneficial.ToString();
                txtItemQuantity.Text = oLiveRestBudgetBO.ItemQuantity.ToString();
                txtCostPerUnit.Text = UtilBO.CurrencyFormat(oLiveRestBudgetBO.CostPerUnit);
                txtTotal.Text = UtilBO.CurrencyFormat(oLiveRestBudgetBO.TotalAmount);
                txtComments.Text = oLiveRestBudgetBO.Comments;

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(oLiveRestBudgetBO.District) != null)
                    ddlDistrict.Items.FindByText(oLiveRestBudgetBO.District).Selected = true;

                BindCounties();
                ddlCounty.ClearSelection();
                if (ddlCounty.Items.FindByText(oLiveRestBudgetBO.County) != null)
                    ddlCounty.Items.FindByText(oLiveRestBudgetBO.County).Selected = true;
                //ddlCounty.SelectedItem.Text = oLiveRestBudgetBO.County;

                BindSubCounties();
                ddlSubCounty.ClearSelection();
                if (ddlSubCounty.Items.FindByText(oLiveRestBudgetBO.SubCounty) != null)
                    ddlSubCounty.Items.FindByText(oLiveRestBudgetBO.SubCounty).Selected = true;
                //                ddlSubCounty.SelectedItem.Text = oLiveRestBudgetBO.SubCounty;

                BindParish();
                ddlParish.ClearSelection();
                if (ddlParish.Items.FindByText(oLiveRestBudgetBO.Parish) != null)
                    ddlParish.Items.FindByText(oLiveRestBudgetBO.Parish).Selected = true;
                //ddlParish.SelectedItem.Text = oLiveRestBudgetBO.Parish;

                BindVillages();
                lstbVillages.ClearSelection();
                LiveRestBudVillagesBLL oLiveRestBudVillagesBLL = new LiveRestBudVillagesBLL();
                LiveRestBudVillagesList oLiveRestBudVillagesList = new LiveRestBudVillagesList();
                if (ViewStateLivRestBudgetID > 0)
                {
                    oLiveRestBudVillagesList = oLiveRestBudVillagesBLL.GetLiveRestBudVillagesById(ViewStateLivRestBudgetID);
                    foreach (LiveRestBudVillagesBO objLiveRestBudVillagesBO in oLiveRestBudVillagesList)
                    {
                        if (lstbVillages.Items.FindByText(objLiveRestBudVillagesBO.Village) != null)
                            lstbVillages.Items.FindByText(objLiveRestBudVillagesBO.Village).Selected = true;
                        // lstbVillages.Items.Add(objLiveRestBudVillagesBO.Village);
                    }
                }

                // LoadVillages();
            }
        }
        /// <summary>
        /// Load Villages
        /// </summary>
        private void LoadVillages()
        {
            LiveRestBudVillagesBLL oLiveRestBudVillagesBLL = new LiveRestBudVillagesBLL();
            LiveRestBudVillagesList oLiveRestBudVillagesList = new LiveRestBudVillagesList();
            if (ViewStateLivRestBudgetID > 0)
            {
                oLiveRestBudVillagesList = oLiveRestBudVillagesBLL.GetLiveRestBudVillagesById(ViewStateLivRestBudgetID);
                foreach (LiveRestBudVillagesBO oLiveRestBudVillagesBO in oLiveRestBudVillagesList)
                {
                    lstbVillages.Items.Add(oLiveRestBudVillagesBO.Village);
                }
            }
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
          //  BindCounties();
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

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRestoreItems();
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LivelihoodBudgetItemsList lstLiveBudgItems = new LivelihoodBudgetItemsList();
            LivelihoodBudgetItemsBLL oLiveBudgItemsBLL = new LivelihoodBudgetItemsBLL();
            LivelihoodBudgetItemsBO oLiveBudgItemsBO = new LivelihoodBudgetItemsBO();
            oLiveBudgItemsBO.Liv_Bud_CategID = Convert.ToInt32(ddlCategory.SelectedValue.ToString());

            if (ddlItem.SelectedIndex > 0)
            {
                oLiveBudgItemsBO.Liv_Bud_ItemID = Convert.ToInt32(ddlItem.SelectedValue.ToString());

                lstLiveBudgItems = oLiveBudgItemsBLL.GetLivBudgetItems(oLiveBudgItemsBO);

                if (lstLiveBudgItems.Count > 0)
                {
                    if (lstLiveBudgItems[0].Liv_Bud_ItemDesc != null)
                        txtItemDescription.Text = lstLiveBudgItems[0].Liv_Bud_ItemDesc.ToString();
                    else
                        txtItemDescription.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Select or unselect villages data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkAllVillages_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in lstbVillages.Items)
            {
                item.Selected = chkAllVillages.Checked;
            }
        }
        #endregion

    }
}