using System;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Web.UI;

namespace WIS
{
    public partial class LiveliHoodAfter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                opsStartDate.Attributes.Add("readonly","readonly");
                opsEndDate.Attributes.Add("readonly", "readonly");
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Livelihood Restoration Assessment Report";
                else
                    Master.PageHeader = "Livelihood Restoration Assessment Report";
                txtHHID.Text = Session["HH_ID"].ToString();
                //if (Request.QueryString["ProjectID"] != null)
                //{
                //    txtPAPName.Text = Session["SelPAPName"].ToString();
                //}
            }
            calopsStartDate.Format = UtilBO.DateFormat;
            CalopsEndDate.Format = UtilBO.DateFormat;

        }
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
            opsStartDate.Text = "";
            opsEndDate.Text = "";
            txtHHID.Text = "";
        }
    }
}