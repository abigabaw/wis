using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.WebControls;
using System.Text;

namespace WIS
{
    public partial class LandInfo : System.Web.UI.Page
    {
        #region GlobalDeclaration
        TenureLandList objTenureLandList;
        MasterBLL objMasterBLL;
        #endregion

        #region PageEvents

        /// <summary>
        /// Set Page header
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Request.QueryString["HHID"] != null)
                    Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                else
                    Session["HH_ID"] = null;
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.LandInfo;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.LandInfoPriv;
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["PROJECT_ID"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Survey - Land Information";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                BindLandTenure();
                BindReceivedFromWhom();
                GetLandInfo();

                txtwhenbeginfarm.Attributes.Add("onblur", "CheckYear();");
                txtyearmoved.Attributes.Add("onblur", "CheckYearForAll(this);");
                txtyear.Attributes.Add("onblur", "CheckYearForAll(this);");            
                chkLivedSinceBirth.Attributes.Add("onclick", "chkLivedSinceBirth_onclick(this);");
                ChkMortagelies.Attributes.Add("onclick", "chkMortgage_onclick(this);");
                ddlReceivedFromWhom.Attributes.Add("onchange", "EnableDisableOtherOccupantStatus(this);");
                txtlandlord.Attributes.Add("onchange", "setDirtyText();");
                txtwhenbeginfarm.Attributes.Add("onchange", "setDirtyText();");
                txtagrrement.Attributes.Add("onchange", "setDirtyText();");
                txtwhoclaims.Attributes.Add("onchange", "setDirtyText();");
                txtwheredidfarm.Attributes.Add("onchange", "setDirtyText();");
                txtprodutive.Attributes.Add("onchange", "setDirtyText();");
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SURVEY) == false)
                {
                    lnkILOP.Visible = false;
                    lnkITML.Visible = false;
                    btn_SavePub.Visible = false;
                    btn_ClearPublic.Visible = false;
                    btn_SavePrivate.Visible = false;
                    btn_Clear.Visible = false;
                }              
                
               
                
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (Mode == "Readonly")
            {
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkILOP.Visible = false;
                lnkITML.Visible = false;
                btn_SavePub.Visible = false;
                btn_ClearPublic.Visible = false;
                btn_SavePrivate.Visible = false;
                btn_Clear.Visible = false;
            }
        }

        /// <summary>
        /// Set default button
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (pnlMailo.Visible == true)
                stb.Append(btn_SavePrivate.ClientID);
            else if (pnlPublic.Visible == true)
                stb.Append(btn_SavePub.ClientID);
            else
                stb.Append(btn_SavePub.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        ///  Get Land Info Details for Selected PAP
        /// </summary>
        private void GetLandInfo()
        {
            LandInfoBLL objLandInfoBLL = new LandInfoBLL();
            PublicLandInfoBO objLandInfo = objLandInfoBLL.GetLandInfo(Convert.ToInt32(Session["HH_ID"]));

            if (objLandInfo != null)
            {
                ddlLandTenure.ClearSelection();
                if (ddlLandTenure.Items.FindByValue(objLandInfo.LND_TENUREID.ToString()) != null)
                    ddlLandTenure.Items.FindByValue(objLandInfo.LND_TENUREID.ToString()).Selected = true;

                ddlReceivedFromWhom.ClearSelection();
                if (ddlReceivedFromWhom.Items.FindByValue(objLandInfo.LandRecivedfromid.ToString()) != null)
                    ddlReceivedFromWhom.Items.FindByValue(objLandInfo.LandRecivedfromid.ToString()).Selected = true;
                if (ddlReceivedFromWhom.SelectedItem.Text == "Other" || ddlReceivedFromWhom.SelectedItem.Text == "Individual")
                {
                    txtfromwhom.Enabled = true;
                }
                else
                    txtfromwhom.Enabled = false;
                txtyear.Text = objLandInfo.YEAROFACQUISITION;
                txtfromwhom.Text = objLandInfo.FROMWHOM;
                txtcoments.Text = objLandInfo.COMMENTS;
                txtland.Text = objLandInfo.WHOCLAIMSLAND;

                if (objLandInfo.HASTITLEDETAILS.ToUpper() == "YES")
                {
                    Chkhasitdetails.Checked = true;
                }

                if (objLandInfo.LIVEDSINCEBIRTH.ToUpper() == "YES")
                {
                    chkLivedSinceBirth.Checked = true;
                }

                if (objLandInfo.ISMORTGAGED.ToUpper() == "YES")
                {
                    ChkMortagelies.Checked = true;
                }

                txtyearmoved.Text = objLandInfo.MOVEDYEAR;
                txtwherebefore.Text = objLandInfo.WHERELIVEDBEFORE;
                txtmortagedetails.Text = objLandInfo.MORTGAGEDETAILS;

                pnlPublic.Visible = true;
                //ddlLandTenure.Enabled = false;
            }
            else
            {
                LandInfoPrivateBLL objLandInfoPrivBLL = new LandInfoPrivateBLL();
                PrivateLandInfoBO objPrivLandInfo = objLandInfoPrivBLL.GetLandInfoPriv(Convert.ToInt32(Session["HH_ID"]));

                if (objPrivLandInfo != null)
                {
                    ddlLandTenure.ClearSelection();
                    if (ddlLandTenure.Items.FindByValue(objPrivLandInfo.Lnd_TENUREIDPriv.ToString()) != null)
                        ddlLandTenure.Items.FindByValue(objPrivLandInfo.Lnd_TENUREIDPriv.ToString()).Selected = true;

                    txtlandlord.Text = objPrivLandInfo.LANDLORDNAME;
                    txtwhoclaims.Text = objPrivLandInfo.CLAIMANTNAME;
                    txtwhenbeginfarm.Text = objPrivLandInfo.WHENFARMINGBEGAN;
                    txtwheredidfarm.Text = objPrivLandInfo.WHEREFARMEDBEFORE;

                    if (objPrivLandInfo.DOSPOUSESFARM.ToUpper() == "YES")
                    {
                        Chkspouse.Checked = true;
                        PAP_RelationBLL objPAPRBLL = new PAP_RelationBLL();
                        PAPRelationList objPAPRelationList = new PAPRelationList();
                        objPAPRelationList = objPAPRBLL.GetRelations(Convert.ToInt32(Session["HH_ID"]), 1);
                        if (objPAPRelationList.Count > 0)
                        {
                            Chkspouselist.DataSource = objPAPRelationList;
                            Chkspouselist.DataTextField = "HOLDERNAME";
                            Chkspouselist.DataValueField = "RelationID";
                            Chkspouselist.DataBind();
                        }
                        else
                        {
                            chkmsg1.Text = "Spouses Not Available";
                        }
                        if (Chkspouselist.Items.Count > 0)
                        {
                            PAPRelationList RelationsSpose = objLandInfoPrivBLL.GetLandInfoPrivSpose(Convert.ToInt32(objPrivLandInfo.PRIVATELANDID));
                            if (RelationsSpose != null)
                            {
                                for (int i = 0; i < RelationsSpose.Count; i++)
                                {
                                    Chkspouselist.Items.FindByValue(RelationsSpose[i].RelationID.ToString()).Selected = true;
                                }
                            }
                        }
                    }

                    if (objPrivLandInfo.DOCHILDRENFARM.ToUpper() == "YES")
                    {
                        ChkChildren.Checked = true;
                        BindChild(Convert.ToInt32(Session["HH_ID"]));


                        if (Chkchildrenlist.Items.Count > 0)
                        {
                            PAPRelationList RelationsSpose = objLandInfoPrivBLL.GetLandInfoPrivChild(Convert.ToInt32(objPrivLandInfo.PRIVATELANDID));
                            if (RelationsSpose != null)
                            {
                                for (int i = 0; i < RelationsSpose.Count; i++)
                                {
                                    Chkchildrenlist.Items.FindByValue(RelationsSpose[i].RelationID.ToString()).Selected = true;
                                }
                            }
                        }
                    }

                    txtagrrement.Text = objPrivLandInfo.AGREEMENTTYPE;
                    txtprodutive.Text = objPrivLandInfo.PRODASSETOPPORTUNITIES;

                    pnlMailo.Visible = true;
                    //ddlLandTenure.Enabled = false;
                }
            }
        }
        /// <summary>
        /// To Sava and Update Public land Data info in to data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SavePublic(object sender, EventArgs e)
        {
            PublicLandInfoBO objPublicLF = new PublicLandInfoBO();

            objPublicLF.HID = Convert.ToInt32(Session["HH_ID"]);
            objPublicLF.LND_TENUREID = Convert.ToInt32(ddlLandTenure.SelectedItem.Value);
            objPublicLF.LandRecivedfromid = Convert.ToInt32(ddlReceivedFromWhom.SelectedItem.Value);
            objPublicLF.YEAROFACQUISITION = txtyear.Text.Trim();
            objPublicLF.FROMWHOM = txtfromwhom.Text.Trim();
            string strMax = txtcoments.Text.ToString().Trim();
            if (strMax.Trim().Length >= 999)
            {
                strMax = txtcoments.Text.ToString().Trim().Substring(0, 999);
            }
            objPublicLF.COMMENTS = strMax;

            objPublicLF.WHOCLAIMSLAND = txtland.Text.Trim();

            if (Chkhasitdetails.Checked == true)
            {
                objPublicLF.HASTITLEDETAILS = "Yes";
            }
            else if (Chkhasitdetails.Checked == false)
            {
                objPublicLF.HASTITLEDETAILS = "No";
            }


            if (chkLivedSinceBirth.Checked == true)
            {
                objPublicLF.LIVEDSINCEBIRTH = "Yes";
            }
            else
            {
                objPublicLF.LIVEDSINCEBIRTH = "No";
            }


            if (ChkMortagelies.Checked == true)
            {
                objPublicLF.ISMORTGAGED = "Yes";
            }
            else if (ChkMortagelies.Checked == false)
            {
                objPublicLF.ISMORTGAGED = "No";
            }

            objPublicLF.MOVEDYEAR = txtyearmoved.Text.Trim();
            objPublicLF.WHERELIVEDBEFORE = txtwherebefore.Text.Trim();
            string strgMax = txtmortagedetails.Text.Trim();
            if (strgMax.Trim().Length >= 200)
            {
                strgMax = txtmortagedetails.Text.Trim().Substring(0, 200);
            }
            objPublicLF.MORTGAGEDETAILS = strgMax;
            objPublicLF.Userid = Convert.ToInt32(Session["USER_ID"]);
            LandInfoBLL objLFBLL = new LandInfoBLL();

            //objPublicLF. = txtwherebefore.Text.Trim();     

            //if (objPublicLF.HID != 0)
            //{
            //    objLFBLL.AddLandInfo(objPublicLF);
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Public Land Info added successfully');", true);
            //}
            //else
            //{
            ChangeRequestStatusILOP();
            objLFBLL.UpdateLandInfo(objPublicLF);
            projectFrozen();
                        
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Data saved successfully');", true);

            //ClearDetailspublic();
            //}
        }
        /// <summary>
        /// To Sava and Update Private land Data info in to data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PrivateLandInfoBO objPrivateLF = new PrivateLandInfoBO();

            objPrivateLF.HIDPriv = Convert.ToInt32(Session["HH_ID"]);
            objPrivateLF.Lnd_TENUREIDPriv = Convert.ToInt32(ddlLandTenure.SelectedItem.Value);
            objPrivateLF.LANDLORDNAME = txtlandlord.Text;
            objPrivateLF.CLAIMANTNAME = txtwhoclaims.Text.Trim();
            objPrivateLF.WHENFARMINGBEGAN = txtwhenbeginfarm.Text.Trim();
            objPrivateLF.WHEREFARMEDBEFORE = txtwheredidfarm.Text.Trim();
            //objPrivateLF.DOSPOUSESFARM = Chkspouse.Text;

            if (Chkspouse.Checked == true)
            {
                objPrivateLF.DOSPOUSESFARM = "Yes";

                //BO.PAP_Relation objPAPR = new BO.PAP_Relation();
                //BO.PAP_RelationBLL objPAPRBLL = new BO.PAP_RelationBLL();
                //Chkspouselist.DataSource = objPAPRBLL.GetRelations(2,3);

            }
            else if (Chkspouse.Checked == false)
            {
                objPrivateLF.DOSPOUSESFARM = "No";

            }

            if (ChkChildren.Checked == true)
            {
                objPrivateLF.DOCHILDRENFARM = "Yes";

            }
            else if (ChkChildren.Checked == false)
            {
                objPrivateLF.DOCHILDRENFARM = "No";

            }
            string sagr = txtagrrement.Text.Trim();
            string sPro = txtprodutive.Text.Trim();
            if (sagr.Length > 999)
                sagr = sagr.Substring(0, 999);
            if (sPro.Length > 999)
                sPro = sPro.Substring(0, 999);
            objPrivateLF.AGREEMENTTYPE = sagr;
            objPrivateLF.PRODASSETOPPORTUNITIES = sPro;
            objPrivateLF.Useridpriv = Convert.ToInt32(Session["USER_ID"]);
            LandInfoPrivateBLL objLFPrivateBLL = new LandInfoPrivateBLL();
            //if (objPrivateLF.HIDPriv != 0)
            //{
            //    objLFPrivateBLL.AddLandInfoPrivate(objPrivateLF);
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Private Land Info added successfully');", true);
            //}
            //else
            //{
            objLFPrivateBLL.UpdateLandInfoPrivate(objPrivateLF);

            PAPRelationList RelationsSpose = new PAPRelationList();
            if (Chkspouse.Checked == true)
            {
                PAP_RelationBO objRelation = null;
                for (int i = 0; i < Chkspouselist.Items.Count; i++)
                {
                    if (Chkspouselist.Items[i].Selected)
                    {
                        objRelation = new PAP_RelationBO();
                        objRelation.RelationID = Convert.ToInt32(Chkspouselist.Items[i].Value);
                        objRelation.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                        objRelation.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                        RelationsSpose.Add(objRelation);
                    }
                }
            }
            objLFPrivateBLL.InsertUpdateRelationsSpose(RelationsSpose);
            PAPRelationList RelationsChild = new PAPRelationList();
            if (ChkChildren.Checked == true)
            {
                PAP_RelationBO objRelation = null;
                for (int i = 0; i < Chkchildrenlist.Items.Count; i++)
                {
                    if (Chkchildrenlist.Items[i].Selected)
                    {
                        objRelation = new PAP_RelationBO();
                        objRelation.RelationID = Convert.ToInt32(Chkchildrenlist.Items[i].Value);
                        objRelation.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                        objRelation.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                        RelationsChild.Add(objRelation);
                    }
                }
            }
            objLFPrivateBLL.InsertUpdateRelationsChild(RelationsChild);
            ChangeRequestStatusITML();
            projectFrozen();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Data updated successfully');", true);
            btn_SavePrivate.Text = "Save";
            //ClearDetailspriv();

            //}
        }
        /// <summary>
        /// Clear public land info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ClearPublic_Click(object sender, EventArgs e)
        {
            ClearDetailspublic();

        }
        /// <summary>
        /// set visible to public and private land info panels based on selected data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlLandTenure_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ddlLandTenure.SelectedItem.Text;
            string str1 = str.Contains("Mailo").ToString();
            if (ddlLandTenure.SelectedIndex > 0)
            {
                if (str1 == "True")
                {
                    pnlMailo.Visible = true;
                    pnlPublic.Visible = false;
                    //upnlMailo.Update();
                    //btn_SavePub.Visible = false;
                    //btn_ClearPublic.Visible = false;
                    //btn_SavePrivate.Visible = true;
                    //btn_Clear.Visible = true;
                }
                else
                {
                    pnlMailo.Visible = false;
                    pnlPublic.Visible = true;
                    //upnlPublic.Update();
                    //btn_SavePub.Visible = true;
                    //btn_ClearPublic.Visible = true;
                    //btn_SavePrivate.Visible = false;
                    //btn_Clear.Visible = false;
                }
            }
            else
            {
                pnlMailo.Visible = false;
                pnlPublic.Visible = false;
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// Clear private land info Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetailspriv();

        }

        /// <summary>
        /// to see Spouse details for selected Pap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Chkspouse_CheckedChanged(object sender, EventArgs e)
        {
            //for getting spouse name we r passing id
            int HHH_ID = Convert.ToInt32(Session["HH_ID"]);

            if (Chkspouse.Checked == true)
            {
                //PAP_Relation objPAPR = new PAP_Relation();
                PAP_RelationBLL objPAPRBLL = new PAP_RelationBLL();
                PAPRelationList objPAPRelationList = new PAPRelationList();
                objPAPRelationList = objPAPRBLL.GetRelations(HHH_ID, 1);
                if (objPAPRelationList.Count > 0)
                {
                    Chkspouselist.DataSource = objPAPRelationList;
                    Chkspouselist.DataTextField = "HOLDERNAME";
                    Chkspouselist.DataValueField = "RelationID";
                    Chkspouselist.DataBind();
                }
                else
                {
                    chkmsg1.Text = "Spouses Not Available";
                }

            }
            else
            {
                Chkspouselist.Items.Clear();
                chkmsg1.Text = "";
            }

        }

        /// <summary>
        /// to see Children details for selected Pap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChkChildren_CheckedChanged(object sender, EventArgs e)
        {

            //for getting children name we r passing id
            int HHHH_ID = Convert.ToInt32(Session["HH_ID"]);

            if (ChkChildren.Checked == true)
            {
                //BO.PAP_Relation objPAPR = new BO.PAP_Relation();                
                BindChild(HHHH_ID);
            }
            else
            {
                Chkchildrenlist.Items.Clear();
                chkmsg.Text = "";
            }

        }
        /// <summary>
        /// Bind Child Data to Chkchildrenlist
        /// </summary>
        /// <param name="HHHH_ID"></param>
        protected void BindChild(int HHHH_ID)
        {
            PAP_RelationBLL objPAPRBLL = new PAP_RelationBLL();
            PAPRelationList objPAPRelationList = new PAPRelationList();
            PAPRelationList objPAPRelationList2 = new PAPRelationList();
            objPAPRelationList = objPAPRBLL.GetRelations(HHHH_ID, 2);
            objPAPRelationList2 = objPAPRBLL.GetRelations(HHHH_ID, 3);
            PAP_RelationBO objRelation = null;
            for (int i = 0; i < objPAPRelationList2.Count; i++)
            {
                objRelation = new PAP_RelationBO();

                objRelation.RelationID = objPAPRelationList2[i].RelationID;
                objRelation.HouseholdID = objPAPRelationList2[i].HouseholdID;
                objRelation.HolderName = objPAPRelationList2[i].HolderName;
                objRelation.ResideOnAffectedLand = objPAPRelationList2[i].ResideOnAffectedLand;
                objRelation.DateOfBirth = objPAPRelationList2[i].DateOfBirth;
                objRelation.LiteracyStatus = objPAPRelationList2[i].LiteracyStatus;
                objRelation.IsDeleted = objPAPRelationList2[i].IsDeleted;

                objPAPRelationList.Add(objRelation);
            }
            if (objPAPRelationList.Count > 0)
            {
                Chkchildrenlist.DataSource = objPAPRelationList;
                Chkchildrenlist.DataTextField = "HOLDERNAME";
                Chkchildrenlist.DataValueField = "RelationID";
                Chkchildrenlist.DataBind();
            }
            else
            {
                chkmsg.Text = "Children Not Available";
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Bind data to ddlReceivedFromWhom drop down
        /// </summary>
        private void BindReceivedFromWhom()
        {
            LandReceivedMSTBLL objLandRecdBLL = new LandReceivedMSTBLL();

            ddlReceivedFromWhom.DataSource = objLandRecdBLL.GetLandReceived();        //GetAllLandReceived();
            ddlReceivedFromWhom.DataTextField = "LandReceived";
            ddlReceivedFromWhom.DataValueField = "LandReceivedID";
            ddlReceivedFromWhom.DataBind();
        }

        /// <summary>
        /// Bind data to ddlLandTenure drop down
        /// </summary>
        private void BindLandTenure()
        {
            objMasterBLL = new MasterBLL();
            objTenureLandList = objMasterBLL.LoadTenureLand();
            if (objTenureLandList.Count > 0)
            {
                ddlLandTenure.DataSource = objTenureLandList;
                ddlLandTenure.DataTextField = "Lnd_Tenure";
                ddlLandTenure.DataValueField = "Lnd_TenureId";
                ddlLandTenure.DataBind();
            }
            ddlLandTenure.Items.Insert(0, "--Select--");
        }

        /// <summary>
        /// Clear Public Details
        /// </summary>
        protected void ClearDetailspublic()
        {
            txtfromwhom.Text = "";
            ddlReceivedFromWhom.ClearSelection();
            ddlReceivedFromWhom.Items[0].Selected = true;
            txtland.Text = "";
            txtyearmoved.Text = "";
            txtyear.Text = "";
            txtcoments.Text = "";
            txtwherebefore.Text = "";
            txtmortagedetails.Text = "";
            Chkhasitdetails.Checked = false;
            chkLivedSinceBirth.Checked = false;
            ChkMortagelies.Checked = false;
        }

        /// <summary>
        /// Clear Private Details
        /// </summary>
        protected void ClearDetailspriv()
        {
            txtlandlord.Text = "";
            txtwhenbeginfarm.Text = "";
            txtwhoclaims.Text = "";
            txtwheredidfarm.Text = "";
            Chkspouse.Checked = false;
            ChkChildren.Checked = false;
            txtagrrement.Text = "";
            txtprodutive.Text = "";
            Chkchildrenlist.Items.Clear();
            Chkspouselist.Items.Clear();
            chkmsg1.Text = "";
            chkmsg.Text = "";
        }

        #endregion

        #region Frozen / Approval / Decline / Pending
        /// <summary>
        /// Check Project Frozen or not
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btn_SavePub.Visible = false;
                    btn_ClearPublic.Visible = false;

                    btn_SavePrivate.Visible = false;
                    btn_Clear.Visible = false;

                    checkApprovalExitOrNot();
                    getApprrequtStatusILOP();
                    getApprrequtStatusITML();
                }
                else
                {
                    lnkILOP.Visible = false;
                    lnkITML.Visible = false;
                }
            }
        }
        /// <summary>
        /// Check Approver exit or not for change request
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusILOP.Text = "";
            StatusILOP.Visible = false;

            StatuslnkITML.Text = "";
            StatuslnkITML.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "IT-OP");
                lnkILOP.Attributes.Add("onclick", paramChangeRequest);
                string paramChangeRequest1 = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "IT-ML");
                lnkITML.Attributes.Add("onclick", paramChangeRequest1);
                lnkILOP.Visible = true;
                lnkITML.Visible = true;
            }
            else
            {
                lnkILOP.Visible = false;
                lnkITML.Visible = false;
            }
            #endregion
            getApprrequtStatusILOP();
            getApprrequtStatusITML();

        }
        /// <summary>
        /// to Check Change Request Status
        /// </summary>
        public void ChangeRequestStatusILOP()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "IT-OP";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        /// <summary>
        /// to Check Change Request Status
        /// </summary>
        public void getApprrequtStatusILOP()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "IT-OP";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkILOP.Visible = false;
                    btn_SavePub.Visible = false;
                    btn_ClearPublic.Visible = false;
                    StatusILOP.Visible = true;
                    StatusILOP.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkILOP.Visible = true;
                    btn_SavePub.Visible = false;
                    btn_ClearPublic.Visible = false;
                    StatusILOP.Visible = false;
                    StatusILOP.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkILOP.Visible = false;
                    btn_SavePub.Visible = true;
                    btn_ClearPublic.Visible = true;
                    StatusILOP.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }

        /// <summary>
        /// to Check Change Request Status
        /// </summary>
        public void ChangeRequestStatusITML()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "IT-ML";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        /// <summary>
        /// to Check Change Request Status
        /// </summary>
        public void getApprrequtStatusITML()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "IT-ML";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkITML.Visible = false;
                    btn_SavePrivate.Visible = false;
                    btn_Clear.Visible = false;
                    StatuslnkITML.Visible = true;
                    StatuslnkITML.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkITML.Visible = true;
                    btn_SavePrivate.Visible = false;
                    btn_Clear.Visible = false;
                    StatuslnkITML.Visible = false;
                    StatuslnkITML.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkITML.Visible = false;
                    btn_SavePrivate.Visible = true;
                    btn_Clear.Visible = true;
                    StatuslnkITML.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
    }
}