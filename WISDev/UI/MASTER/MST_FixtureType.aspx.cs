using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS.UI.MASTER
{
    public partial class MST_FixtureType : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FixtureTypeBO ObjFixtureBO = new FixtureTypeBO();

            ObjFixtureBO.FixtureID = Convert.ToInt32(ViewState["FixtureType_ID"]);
            ObjFixtureBO.FixtureType = txtFixtureType.Text.Trim();

            FixtureTypeBLL ObjFixtureBLL = new FixtureTypeBLL();
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ObjFixtureBO.FixtureID == 0)
            {
                ObjFixtureBO.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = ObjFixtureBLL.AddFixtureType(ObjFixtureBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                }
            }
            else
            {
                ObjFixtureBO.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = ObjFixtureBLL.UpdateFixtureType(ObjFixtureBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            BindFixtureType(true, false);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtFixtureType.Text = "";
            ViewState["FixtureType_ID"] = 0;
            txtSearchFixtureType.Text = "";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindFixtureType(bool addRow, bool deleteRow)
        {
            FixtureTypeBLL ObjFixtureBLL = new FixtureTypeBLL();
            grdBanks.DataSource = ObjFixtureBLL.GetAllFixtureType("");
            grdBanks.DataBind();
        }

    }
}