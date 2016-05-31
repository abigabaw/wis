using System;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class PAPList : System.Web.UI.Page
    {
        /// <summary>
        /// Set PAge Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - PAP List";
                }
                else
                    Master.PageHeader = "PAP List";
                //BindDistricts();
                //SetGridSource(true);
                //grdPAPs.DataBind();
            }
        }

    }
}