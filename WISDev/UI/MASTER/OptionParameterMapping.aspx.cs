using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS.UI.MASTER
{
    public partial class OptionParameterMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                //Master.PageHeader = "Option Parameter Mapping";
                ViewState["OptParID"] = 0;
                BindGrid();
                //   BindDistricts();
                BindOptionAvailable();
                BindOptionGroup();
                BindOptionDescription();
                BindOptionParameters();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    //  btnShowAdd.Visible = false;
                    //   btnShowSearch.Visible = false;
                    //  pnlSearch.Visible = true;
                    //pnlDistrictDetails.Visible = false;
                    grdOptionMapping.Columns[grdOptionMapping.Columns.Count - 1].Visible = false;
                    grdOptionMapping.Columns[grdOptionMapping.Columns.Count - 2].Visible = false;
                    grdOptionMapping.Columns[grdOptionMapping.Columns.Count - 3].Visible = false;
                }
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",    CreateStartupScript());
            }
        }

        private void BindOptionParameter()
        {
            OptionParameterMappingBLL oOptionParameterBLL = new OptionParameterMappingBLL();
            ddlOptionAvailable.Items.Clear();

            ddlOptionAvailable.DataSource = oOptionParameterBLL.GetOptionAvailable();
            ddlOptionAvailable.DataTextField = "AvailableOptions";
            ddlOptionAvailable.DataValueField = "AvailableOptionsID";
            ddlOptionAvailable.DataBind();

            ddlOptionAvailable.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindOptionAvailable()
        {
            OptionParameterMappingBLL oOptionParameterBLL = new OptionParameterMappingBLL();
            ddlOptionAvailable.Items.Clear();

            ddlOptionAvailable.DataSource = oOptionParameterBLL.GetOptionAvailable();
            ddlOptionAvailable.DataTextField = "AvailableOptions";
            ddlOptionAvailable.DataValueField = "AvailableOptionsID";
            ddlOptionAvailable.DataBind();

            ddlOptionAvailable.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindOptionGroup()
        {
            OptionParameterMappingBLL oOptionParameterBLL = new OptionParameterMappingBLL();
            ddlOptionGroup.Items.Clear();

            ddlOptionGroup.DataSource = oOptionParameterBLL.GetOptionGroup();
            ddlOptionGroup.DataTextField = "OptionGroupName";
            ddlOptionGroup.DataValueField = "OptionGroupID";
            ddlOptionGroup.DataBind();

            ddlOptionGroup.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindOptionDescription()
        {
            OptionParameterMappingBLL oOptionParameterBLL = new OptionParameterMappingBLL();
            ddlDescription.Items.Clear();

            ddlDescription.DataSource = oOptionParameterBLL.GetOptionDescription(Convert.ToInt32(ddlParameter.SelectedValue));
            ddlDescription.DataTextField = "OptionGroupName";
            ddlDescription.DataValueField = "OptionGroupID";
            ddlDescription.DataBind();

            ddlDescription.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindOptionParameters()
        {
            OptionParameterMappingBLL oOptionParameterBLL = new OptionParameterMappingBLL();
            ddlParameter.Items.Clear();
            ddlParameter.DataSource = oOptionParameterBLL.GetOptionParameters(Convert.ToInt32(ddlOptionAvailable.SelectedValue));
            ddlParameter.DataTextField = "Name";
            ddlParameter.DataValueField = "Id";
            ddlParameter.DataBind();
            ddlParameter.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void BindGrid()
        {
            //  int districtid = Convert.ToInt32(ddlDistrict.SelectedValue);
            OptionParameterMappingBLL ooptPrmMap = new OptionParameterMappingBLL();
            grdOptionMapping.DataSource = ooptPrmMap.GetAllOptionParameterMapping();
            grdOptionMapping.DataBind();
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlOptionGroup.ClearSelection();
            ddlOptionAvailable.ClearSelection();
            ddlParameter.ClearSelection();
            ddlDescription.ClearSelection();
            SetUpdateMode(false);
            BindGrid();
        }


        private void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["OptParID"] = "0";
            }

        }
        /// <summary>
        /// Update Database Make data as Obsoluted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string PARISHID = ((Literal)gr.FindControl("litPARISHID")).Text;

                OptionParameterMappingBLL ooptPrmMap = new OptionParameterMappingBLL();
                message = ooptPrmMap.ObsoleteOptionalParameterMapping(Convert.ToInt32(PARISHID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdOptionMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["OptParID"] = e.CommandArgument;
                SetUpdateMode(true);
                GetParishById();

            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                OptionParameterMappingBLL oOptPrmMap = new OptionParameterMappingBLL();
                message = oOptPrmMap.DeleteOptionParameterMapping(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                ClearData();
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To fetch data from database based on ParishID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetParishById()
        {
            OptionParameterMappingBLL oOptPrmMapBLL = new OptionParameterMappingBLL();

            OptionParameterMappingBO oOptPrmMapBO = oOptPrmMapBLL.GetOptionalParameterMappingById(Convert.ToInt32(ViewState["OptParID"]));

            if (oOptPrmMapBO != null)
            {
                //BindOptionAvailable();
                //BindOptionGroup();
                //BindOptionDescription();
                //BindOptionParameters();
                ddlParameter.ClearSelection();
                ddlOptionGroup.ClearSelection();
                ddlOptionAvailable.ClearSelection();
                ddlDescription.ClearSelection();

                ddlOptionGroup.SelectedValue = oOptPrmMapBO.OptionGroupID.ToString();
                ddlOptionAvailable.SelectedValue = oOptPrmMapBO.OptionAvailableID.ToString();
                BindOptionParameters();
                ddlParameter.SelectedValue = oOptPrmMapBO.ParameterID.ToString();
                BindOptionDescription();
                ddlDescription.SelectedValue = oOptPrmMapBO.DescriptionID.ToString();
            }
            SetUpdateMode(true);
        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdOptionMapping.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    Save_OptionParameterMapping();
                }
                if (btnSave.Text == "Update")
                {
                    Update_OptionParameterMapping();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ClearData();
            BindGrid();
        }

        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_OptionParameterMapping()
        {
            OptionParameterMappingBLL oOptPrmMappingBLL = new OptionParameterMappingBLL();
            OptionParameterMappingBO oOptPrmMappingBO = new OptionParameterMappingBO();

            string message = "";

            try
            {
                if (ViewState["OptParID"] != null)
                    oOptPrmMappingBO.OptParID = Convert.ToInt32(ViewState["OptParID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                oOptPrmMappingBO.OptionGroupID = Convert.ToInt32(ddlOptionGroup.SelectedItem.Value);
                oOptPrmMappingBO.OptionAvailableID = Convert.ToInt32(ddlOptionAvailable.SelectedItem.Value);
                oOptPrmMappingBO.DescriptionID = Convert.ToInt32(ddlDescription.SelectedItem.Value);
                oOptPrmMappingBO.ParameterID = Convert.ToInt32(ddlParameter.SelectedItem.Value);
                oOptPrmMappingBO.CreatedBy = Convert.ToInt32(uID);

                oOptPrmMappingBO.UpdatedBy = Convert.ToInt32(uID);

                message = oOptPrmMappingBLL.UpdateOptionParameterMapping(oOptPrmMappingBO);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    //ClearData();
                    //BindGrid();
                    SetUpdateMode(false);
                }

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oOptPrmMappingBO = null;
            }
        }

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_OptionParameterMapping()
        {
            OptionParameterMappingBLL oOptPrmMappingBLL = new OptionParameterMappingBLL();
            OptionParameterMappingBO oOptPrmMappingBO = new OptionParameterMappingBO();

            string message = "";

            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            oOptPrmMappingBO.OptionGroupID = Convert.ToInt32(ddlOptionGroup.SelectedItem.Value);
            oOptPrmMappingBO.OptionAvailableID = Convert.ToInt32(ddlOptionAvailable.SelectedItem.Value);
            oOptPrmMappingBO.DescriptionID = Convert.ToInt32(ddlDescription.SelectedItem.Value);
            oOptPrmMappingBO.ParameterID = Convert.ToInt32(ddlParameter.SelectedItem.Value);
            oOptPrmMappingBO.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = oOptPrmMappingBLL.AddOptionParameterMapping(oOptPrmMappingBO);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                oOptPrmMappingBO = null;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }


        protected void ddlOptionAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOptionParameters();
        }


        protected void ddlParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOptionDescription();
        }

    }
}