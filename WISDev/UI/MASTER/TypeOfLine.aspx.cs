using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class TypeOfLine : System.Web.UI.Page
    {
        TypeOfLineBO TypeOfLineBOObj = null;
        TypeOfLineBLL TypeOfLineBLLObj = null;
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
                Master.PageHeader = "Type of Line";
                ViewState["LineTypeId"] = 0;
                BindGrid(-1);
                txtLineType.Attributes.Add("onchange", "setDirtyText();");
                txtRightOfWay.Attributes.Add("onchange", "setDirtyText();");
                txtWayleave.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MST_PROJECT) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    dv_Details.Columns[4].Visible = false;
                    dv_Details.Columns[5].Visible = false;
                    dv_Details.Columns[6].Visible = false;
                    foreach (GridViewRow grRow in dv_Details.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)grRow.FindControl("IsObsolete");
                            chk.Enabled = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(int newPageIndx)
        {
            TypeOfLineBLLObj = new TypeOfLineBLL();
            dv_Details.DataSource = TypeOfLineBLLObj.GetAllLineTypes();
            if (newPageIndx >= 0) dv_Details.PageIndex = newPageIndx;
            dv_Details.DataBind();
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";

            if (Convert.ToInt32(ViewState["LineTypeId"]) > 0)
            {
                string uID = Session["USER_ID"].ToString();
                int LineTypeID = Convert.ToInt32(ViewState["LineTypeId"]);
                TypeOfLineBOObj = new TypeOfLineBO();
                TypeOfLineBOObj.TypeOfLine = txtLineType.Text.ToString();
                TypeOfLineBOObj.Wayleavemeasurement = txtWayleave.Text.ToString();
                TypeOfLineBOObj.Rightofwaymeasurement = txtRightOfWay.Text.ToString();
                TypeOfLineBOObj.Createdby = Convert.ToInt32(uID);
                TypeOfLineBLLObj = new TypeOfLineBLL();

                message = TypeOfLineBLLObj.Update(TypeOfLineBOObj, LineTypeID);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                SetUpdateMode(false);
            }
            else
            {
                string uID = Session["USER_ID"].ToString();
                TypeOfLineBOObj = new TypeOfLineBO();
                TypeOfLineBOObj.Createdby = Convert.ToInt32(uID);
                TypeOfLineBOObj.Rightofwaymeasurement = txtRightOfWay.Text.ToString();
                TypeOfLineBOObj.TypeOfLine = txtLineType.Text.ToString();
                TypeOfLineBOObj.Wayleavemeasurement = txtWayleave.Text.ToString();
                TypeOfLineBLLObj = new TypeOfLineBLL();
                message = TypeOfLineBLLObj.insert(TypeOfLineBOObj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            BindGrid(-1);
            clearfields();
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            txtLineType.Text = string.Empty;
            txtRightOfWay.Text = string.Empty;
            txtWayleave.Text = string.Empty;
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["LineTypeId"] = e.CommandArgument;
                int LineTypeID = Convert.ToInt32(ViewState["LineTypeId"]);
                GetLineTypeDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                TypeOfLineBLLObj = new TypeOfLineBLL();
                TypeOfLineBLLObj.Delete(Convert.ToInt32(e.CommandArgument));

                clearfields();
                SetUpdateMode(false);
                BindGrid(-1);
            }
        }

        /// <summary>
        /// Get the line type details.
        /// </summary>
        private void GetLineTypeDetails()
        {
            TypeOfLineBLLObj = new TypeOfLineBLL();
            int LineTypeID = 0;

            if (ViewState["LineTypeId"] != null)
                LineTypeID = Convert.ToInt32(ViewState["LineTypeId"]);

            TypeOfLineBOObj = new TypeOfLineBO();
            TypeOfLineBOObj = TypeOfLineBLLObj.GetLineTypebyID(LineTypeID);

            txtLineType.Text = TypeOfLineBOObj.TypeOfLine;
            txtRightOfWay.Text = TypeOfLineBOObj.Rightofwaymeasurement;
            txtWayleave.Text = TypeOfLineBOObj.Wayleavemeasurement;
            ConcernIDTextBox.Text = TypeOfLineBOObj.LineTypeId.ToString();
        }


        protected void dv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dv_Details.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(-1);
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
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
                ViewState["LineTypeId"] = "0";
            }
        }

        /// <summary>
        /// Change index of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dv_Details_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            BindGrid(e.NewPageIndex);
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

                string lineTypeID = ((Literal)gr.FindControl("litLineTypeID")).Text;
                TypeOfLineBLL objTypeOfLineBLL = new TypeOfLineBLL();
                message = objTypeOfLineBLL.ObsoleteLineType(Convert.ToInt32(lineTypeID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                BindGrid(-1);

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}