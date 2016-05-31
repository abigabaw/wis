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
    public partial class OptionGroupParameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadddlOptionGrp();
                loadStatusddl();
                loadGrid();
            }

        }

        private void loadGrid()
        {
            OptionGroupParametersBLL objgrpParam = new OptionGroupParametersBLL();
            grdOptionGrp.DataSource = objgrpParam.getdatatogrid();
            grdOptionGrp.DataBind();
        }

        private void loadStatusddl()
        {
            PstatusBLL objMasterBLL = new PstatusBLL();
            OccupationStatus.DataTextField = "PAPDESIGNATION1";
            OccupationStatus.DataValueField = "PAPDESIGNATIONID1";
            OccupationStatus.DataSource = objMasterBLL.GetPstatus();
            OccupationStatus.DataBind();
        }

        private void LoadddlOptionGrp()
        {
            OptionGroupParametersBLL optGrpOBJ = new OptionGroupParametersBLL();

            ListItem firstListItem = new ListItem(ddloptionGroup.Items[0].Text, ddloptionGroup.Items[0].Value);
            ddloptionGroup.Items.Clear();
            MasterBLL objMasterBLL = new MasterBLL();
            ddloptionGroup.DataTextField = "OptionGroupName";
            ddloptionGroup.DataValueField = "OptionGroupID";
            ddloptionGroup.DataSource = objMasterBLL.LoadOptionGroupData();
            ddloptionGroup.DataBind();

            ddloptionGroup.Items.Insert(0, firstListItem);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Save")
            {
                string AlertMessage = string.Empty;
                OptionGroupParametersBO objgrpParamBO = new OptionGroupParametersBO();
                objgrpParamBO.OptionGrpID = Convert.ToInt32(ddloptionGroup.SelectedValue);
                objgrpParamBO.OptionstatusID = Convert.ToInt32(OccupationStatus.SelectedValue);
                if (RdbtnYES.Checked == true)
                {
                    objgrpParamBO.IsResident = "Yes";
                }
                else
                {
                    objgrpParamBO.IsResident = "No";
                }
                if (RdbtnCash.Checked == true)
                {
                    objgrpParamBO.LandCompensation = "Cash";

                }
                else if (Rdbtninkind.Checked == true)
                {
                    objgrpParamBO.LandCompensation = "In Kind";

                }
                else if(RdbtnBoth.Checked==true)
                {
                    objgrpParamBO.LandCompensation = "Both";

                }
                if (RdbtnHcash.Checked == true)
                {
                    objgrpParamBO.HouseCompensation = "Cash";

                }
                else if (RdbtnHbtninkind.Checked == true)
                {
                    objgrpParamBO.HouseCompensation = "In Kind";

                }
                else if(RdbtnHBoth.Checked==true)
                {
                    objgrpParamBO.HouseCompensation = "Both";

                }
                objgrpParamBO.Createdby = Convert.ToInt32(Session["USER_ID"]);
                OptionGroupParametersBLL objgrpParam = new OptionGroupParametersBLL();
                string res = objgrpParam.SaveOptionGrp(objgrpParamBO);
                if (string.IsNullOrEmpty(res) || res == "" || res == "null")
                {
                    res = "Data saved successfully";
                    clearDetails();
                    AlertMessage = "alert('" + res + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    loadGrid();
                }
                AlertMessage = "alert('" + res + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                clearDetails();
            }
            else if (btnAdd.Text == "Update")
            {
                string AlertMessage = string.Empty;
                OptionGroupParametersBO objgrpParamBO = new OptionGroupParametersBO();
                objgrpParamBO.ParamID = Convert.ToInt32(ViewState["ParamID"]);
                objgrpParamBO.OptionGrpID = Convert.ToInt32(ddloptionGroup.SelectedValue);
                objgrpParamBO.OptionstatusID = Convert.ToInt32(OccupationStatus.SelectedValue);
                if (RdbtnYES.Checked == true)
                {
                    objgrpParamBO.IsResident = "Yes";
                }
                else
                {
                    objgrpParamBO.IsResident = "No";
                }
                if (RdbtnCash.Checked == true)
                {
                    objgrpParamBO.LandCompensation = "Cash";

                }
                else if (Rdbtninkind.Checked == true)
                {
                    objgrpParamBO.LandCompensation = "In Kind";

                }
                else
                {
                    objgrpParamBO.LandCompensation = "Both";

                }
                if (RdbtnHcash.Checked == true)
                {
                    objgrpParamBO.HouseCompensation = "Cash";

                }
                else if (RdbtnHbtninkind.Checked == true)
                {
                    objgrpParamBO.HouseCompensation = "In Kind";

                }
                else
                {
                    objgrpParamBO.HouseCompensation = "Both";

                }
                objgrpParamBO.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                OptionGroupParametersBLL objgrpParam = new OptionGroupParametersBLL();
                string res = objgrpParam.UpdateOptionGroup(objgrpParamBO);
                if (string.IsNullOrEmpty(res) || res == "" || res == "null")
                {
                    res = "Data Updated successfully";
                    clearDetails();
                    AlertMessage = "alert('" + res + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                }
                else
                {
                    AlertMessage = "alert('" + res + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    clearDetails();
                }
                btnAdd.Text = "Save";
                btnCncl.Text = "Clear";
                loadGrid();
            }


        }

        protected void btnCncl_Click(object sender, EventArgs e)
        {
            clearDetails();

        }

        private void clearDetails()
        {
            ddloptionGroup.ClearSelection();
            OccupationStatus.ClearSelection();
            RdbtnBoth.Checked = false;
            RdbtnCash.Checked = false;
            RdbtnHBoth.Checked = false;
            RdbtnHbtninkind.Checked = false;
            RdbtnHcash.Checked = false;
            Rdbtninkind.Checked = false;
            RdbtnNO.Checked = false;
            RdbtnYES.Checked = false;
            btnAdd.Text = "Save";
            btnCncl.Text = "Clear";
        }

        protected void grdOptionGrp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["ParamID"] = Convert.ToInt32(e.CommandArgument);
                int paramid =Convert.ToInt32(ViewState["ParamID"]);
                OptionGroupParametersBLL objgrpParam = new OptionGroupParametersBLL();
                OptionGroupParametersBO objgrpDetails = objgrpParam.GetOptionalDetailsByID(paramid);
                ddloptionGroup.SelectedValue =Convert.ToString(objgrpDetails.OptionGrpID);
                OccupationStatus.SelectedValue = Convert.ToString(objgrpDetails.OptionstatusID);
                if (objgrpDetails.IsResident == "Yes")
                {
                    RdbtnYES.Checked = true;
                }
                else
                {
                    RdbtnNO.Checked = true;
                }
                if (objgrpDetails.LandCompensation == "Cash")
                {
                    RdbtnCash.Checked = true;

                }
                else if (objgrpDetails.LandCompensation == "In kind")
                {
                    Rdbtninkind.Checked = true;
 
                }
                else if (objgrpDetails.LandCompensation == "Both")
                {
                    RdbtnBoth.Checked = true;
                }
                if (objgrpDetails.HouseCompensation == "Cash")
                {
                    RdbtnHcash.Checked = true;
                }
                else if (objgrpDetails.HouseCompensation == "In kind")
                {
                    Rdbtninkind.Checked = true;
 
                }
                else if (objgrpDetails.HouseCompensation == "Both")
                {
                    RdbtnHBoth.Checked = true;
 
                }
                btnAdd.Text = "Update";
                btnCncl.Text = "Cancel";
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["ParamID"] = Convert.ToInt32(e.CommandArgument);
                int paramid = Convert.ToInt32(ViewState["ParamID"]);
                OptionGroupParametersBLL objgrpParam = new OptionGroupParametersBLL();
                message = objgrpParam.DeleteOptionGrp(paramid);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                clearDetails();
                loadGrid();
 
            }

        }
        protected void cvRadioButtonGroup_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool itemSelected = false;
            // get all radio buttons on the page 
            foreach (Control c in pnlOptionGrp.Controls)
            {
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    if (rb.GroupName == "Rd3" && rb.Checked == true)
                    {
                        itemSelected = true;
                    }
                }
            }
            args.IsValid = itemSelected;
        }
        
       
    }
}