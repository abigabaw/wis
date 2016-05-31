using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ProCompensation : System.Web.UI.Page
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Propotionated Compensation Report";
                else
                    Master.PageHeader = "Propotionated Compensation Report";
                txtHHID.Text = Session["HH_ID"].ToString();
                if (Request.QueryString["ProjectID"] != null)
                {
                    txtPAPName.Text = Session["SelPAPName"].ToString();
                }
            }
        }
        /// <summary>
        /// To search PAP details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx?ProjectID=" + Session["PROJECT_ID"].ToString() + "&Distict=" + 0 + "&County=" + 0 + "&SubCounty=" + 0 + "&Parish=" + 0 + "&Village=" + 0 + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPAPName.Text = "";
            txtHHID.Text = "";
        }
    }
}