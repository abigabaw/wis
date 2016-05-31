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
    public partial class RouteMap : System.Web.UI.Page
    {
        string projectID = "";
        int routeID = 0;
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
                if (Request.QueryString["projID"] != null)
                    projectID = Request.QueryString["projID"];

                if (Request.QueryString["routeID"] != null)
                    routeID = Convert.ToInt32(Request.QueryString["routeID"]);

                if (projectID != "")
                {
                    GetProjectRoutes();
                }
                else
                {
                    GenerateMap(routeID);
                }
            }
        }
        /// <summary>
        /// To fetch ProjectRoutes and bind to list
        /// </summary>
        private void GetProjectRoutes()
        {
            ProjectRouteBLL ProjectRouteLogic = new ProjectRouteBLL();

            ProjectRouteList ProjectRoutes = ProjectRouteLogic.GetProjectRoutebyId(Convert.ToInt32(projectID));

            lstRoutes.DataSource = ProjectRoutes;
            lstRoutes.DataBind();
            pnlRoutes.Visible = true;
        }
        /// <summary>
        /// To generate map
        /// </summary>
        /// <param name="routeID"></param>
        private void GenerateMap(int routeID)
        {
            RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
            RouteCoordinatesList RouteCoordinates = objRouteCoordinatesBLL.GetRouteCoordinates(routeID.ToString());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach(RouteCoordinatesBO objRouteCoord in RouteCoordinates)
            {
                sb.Append(string.Format("'{0},{1},{2}'", objRouteCoord.Latitude, objRouteCoord.Longitude, objRouteCoord.Routename));
                sb.Append(";");
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            //Response.Write(sb.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMap", string.Format("initialize(\"{0}\")", sb.ToString()), true);
            
            /*
            if (RouteCoordinates.Count > 0)
            {
                hdnStartingCoordinate.Value = string.Format("{0},{1}", RouteCoordinates[0].Latitude, RouteCoordinates[0].Longitude);
                hdnEndingCoordinate.Value = string.Format("{0},{1}", RouteCoordinates[RouteCoordinates.Count - 1].Latitude, RouteCoordinates[RouteCoordinates.Count - 1].Longitude); ;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMap", "calcRoute();", true);
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Visible=true;
            }*/
        }
        /// <summary>
        /// map is generated based on condition
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void lstRoutes_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ShowRoute")
            {
                GenerateMap(Convert.ToInt32(e.CommandArgument));
            }
        }
    }
}