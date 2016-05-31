using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.IO;
using System.Drawing;

namespace WIS
{
    public partial class PaymentExport : System.Web.UI.Page
    {
        string sPapName = string.Empty;
        string sPlotRef = string.Empty;
        string sDistict = string.Empty;
        string sCounty = string.Empty;
        string sSubcounty = string.Empty;
        string sParish = string.Empty;
        string sVillage = string.Empty;

        #region PageEvents
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            mskPlotReference.Mask = UtilBO.PlotReferenceMask;
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - PAP List";
                }
                else
                    Master.PageHeader = "PAP List";

                BindDistricts();
                BindGrid(true);
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PAP_PAYMENT_EXPORT) == false)
                {
                    btn_ExportExcel.Visible = false;
                }
            }
        }
        /// <summary>
        /// used to search pap details and bind to grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(false);
            upnPAP.Update();
        }
        /// <summary>
        /// used to clear details and load default data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
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

            BindGrid(false);
            upnPAP.Update();
        }
        /// <summary>
        /// To bind counties,subcounties village and parish on change of index of district dropdownlist
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
        /// To bind subcouties villages and parish on change of index of county dropdownlist
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
        /// To bind village and parish on change of index of subcounty dropdownlist
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
        /// To export grid data to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PapList.xls"));

            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            // grdPAPs.AllowPaging = false;
            BindGrid(false);
            // grdPAPs.DataBind();
            //Change the Header Row back to white color

            //Applying stlye to gridview header cells            
            grdPAPs.Style.Add("background-color", "#000000");
            grdPAPs.Attributes.Add("border", "1");
            grdPAPs.Style.Add("border-color", "#000000");
            grdPAPs.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < grdPAPs.HeaderRow.Cells.Count; i++)
            {
                grdPAPs.HeaderRow.Cells[i].Font.Bold = true;
            }
            int j = 1;
            //This loop is used to apply stlye to cells based on particular row
            foreach (GridViewRow gvrow in grdPAPs.Rows)
            {
                gvrow.BackColor = Color.White;
                if (j <= grdPAPs.Rows.Count)
                {
                    //if (j % 2 != 0)
                    //{
                    for (int k = 0; k < gvrow.Cells.Count; k++)
                    {
                        gvrow.Cells[k].Style.Add("border-color", "#000000");
                        gvrow.Cells[k].Style.Add("background-color", "#ffffff");

                    }
                    //}
                }
                j++;
                //grdPAPs.RenderControl(htw);
            }

            grdPAPs.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        #endregion


        protected void grdPAPs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (sPapName.ToUpper() == e.Row.Cells[1].Text.ToUpper() && sPlotRef.ToUpper() == e.Row.Cells[2].Text.ToUpper()
                    && sDistict.ToUpper() == e.Row.Cells[3].Text.ToUpper() && sCounty.ToUpper() == e.Row.Cells[4].Text.ToUpper()
                    && sSubcounty.ToUpper() == e.Row.Cells[5].Text.ToUpper() && sParish.ToUpper() == e.Row.Cells[6].Text.ToUpper()
                    && sVillage.ToUpper() == e.Row.Cells[7].Text.ToUpper())
                {
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";
                    e.Row.Cells[7].Text = "";
                }
                else
                {
                    sPapName = e.Row.Cells[1].Text;
                    sPlotRef = e.Row.Cells[2].Text;
                    sDistict = e.Row.Cells[3].Text;
                    sCounty = e.Row.Cells[4].Text;
                    sSubcounty = e.Row.Cells[5].Text;
                    sParish = e.Row.Cells[6].Text;
                    sVillage = e.Row.Cells[7].Text;
                }
            }
        }

        #region Methods

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        /// <summary>
        /// To bind values to county dropdownlist based on districtID from data base
        /// </summary>
        /// <param name="districtID"></param>
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
        /// To bind district values to district dropdownlist
        /// </summary>
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// To bind values to subcounty dropdownlist based on countyid from database
        /// </summary>
        /// <param name="countyID"></param>
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
        /// To bind values to values to village dropdownlist based on subcounty value from database
        /// </summary>
        /// <param name="subCounty"></param>
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
        /// To bind values to values to village dropdownlist based on subcounty value from database
        /// </summary>
        /// <param name="subCounty"></param>
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        protected void BindGrid(bool showRecentRecords)
        {
            string district = String.Empty;
            string county = String.Empty;
            string subCounty = String.Empty;
            string parish = String.Empty;
            string village = String.Empty;

            if (ddlDistrict.SelectedIndex > 0) district = ddlDistrict.SelectedItem.Text;
            if (ddlCounty.SelectedIndex > 0) county = ddlCounty.SelectedItem.Text;
            if (ddlSubCounty.SelectedIndex > 0) subCounty = ddlSubCounty.SelectedItem.Text;
            if (ddlParish.SelectedIndex > 0) parish = ddlParish.SelectedItem.Text;
            if (ddlVillage.SelectedIndex > 0) village = ddlVillage.SelectedItem.Text;

            PaymentBLL objPAPLogic = new PaymentBLL();
            grdPAPs.DataSource = objPAPLogic.getCompensationPaymentExport(
                Convert.ToInt32(Session["PROJECT_ID"]),
                txtPAPName.Text.Trim(),
                txtPlotReference.Text.Trim(),
                district,
                county,
                subCounty,
                parish,
                village);
            grdPAPs.DataBind();

            if (grdPAPs.Rows.Count > 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showExport", "ShowExport(1);", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showExport", "ShowExport(0);", true);
        }
        #endregion
    }
}