using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.COMPENSATION.SOCIOECONOMIC
{
    public partial class PlotDependents : System.Web.UI.Page
    {
        /// <summary>
        /// to Load the Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] == null)
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                if (Session["HH_ID"] == null)
                {
                    Response.Redirect("~/UI/Compensation/PAPList.aspx");
                }
                Master.PageHeader = "View Stakeholders";
                BindGrid(true, true);
            }
        }
        /// <summary>
        /// to Set the PageIndex for the GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdPlotDependent.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, true);
        }
        /// <summary>
        /// To bind the GridView
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            int HHID_ = 0;

            if ((Request.QueryString["HHID"]) != null)
            {
                HHID_ = Convert.ToInt32(Request.QueryString["HHID"]);
            }
            Trn_Pap_HouseHoldList PAPHouseHoldList  = new Trn_Pap_HouseHoldList(); 
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();

            PAPHouseHoldList = objHouseHoldBLL.GetPlotDependents(HHID_);

            if (PAPHouseHoldList.Count > 0)
                grdPlotDependent.DataSource = PAPHouseHoldList;
                grdPlotDependent.DataBind();
            
        }

    }
}