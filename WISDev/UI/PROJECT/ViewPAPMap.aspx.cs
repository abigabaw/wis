using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.PROJECT
{
    public partial class ViewPAPMap : System.Web.UI.Page
    {
        string projectID = "";
        int routeID = 0;
        /// <summary>
        /// Call GenerateMap() method to Generate Map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_ID"] != null)
                    projectID = Session["PROJECT_ID"].ToString();


                GenerateMap();
            }

        }
        /// <summary>
        /// Set the Route coordinates to Map
        /// </summary>
        private void GenerateMap()
        {
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteList ProjectRouteList = new ProjectRouteList();

            objProjectRoute.Project_Id = Convert.ToInt32(projectID); ;
            bool checck = true;
            ProjectRouteList = objProjectRouteBLL.getFinalRouteApprovalDetial(objProjectRoute);
            for (int i = 0; i < ProjectRouteList.Count; i++)
            {
                if (ProjectRouteList[i].IsFinal == "TRUE")
                {
                    checck = false;
                    RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
                    RouteCoordinatesList RouteCoordinates = objRouteCoordinatesBLL.GetRouteCoordinates(ProjectRouteList[i].Route_ID.ToString());
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    foreach (RouteCoordinatesBO objRouteCoord in RouteCoordinates)
                    {
                        sb.Append(string.Format("'{0},{1},{2}'", objRouteCoord.Latitude, objRouteCoord.Longitude, objRouteCoord.Routename));
                        sb.Append(";");
                    }

                    if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                    //Response.Write(sb.ToString());

                    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                    sb1.Append(string.Format("'{0},{1},{2}'", "0.469104", "33.164163", "E"));
                    sb1.Append(";");
                    sb1.Append(string.Format("'{0},{1},{2}'", "0.469145", "33.164285", "B"));
                    sb1.Append(";");
                    sb1.Append(string.Format("'{0},{1},{2}'", "0.468865", "33.164583", "C"));
                    sb1.Append(";");
                    sb1.Append(string.Format("'{0},{1},{2}'", "0.468782", "33.164507", "D"));
                    sb1.Append(";");
                    sb1.Append(string.Format("'{0},{1},{2}'", "0.469104", "33.164163", "E"));
                    sb1.Append(";");

                    if (sb1.Length > 0) sb1.Remove(sb1.Length - 1, 1); 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMap", string.Format("initialize(\"{0}\")", sb.ToString()), true);
            
                    //string paramView = string.Format("initialize('{0}','{1}');", sb.ToString(), sb1.ToString());
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMap", paramView, true);
                }
            }
            lblMessage.Visible = checck;            
        }
        /// <summary>
        /// Get the Pap Details For Tooltip
        /// </summary>
        /// <returns></returns>
        public string GetPapData()
        {
            string sPapdata = "";
            int HHID = 0;
            if (Request.QueryString["HHID"] != null)
                HHID = Convert.ToInt32(Request.QueryString["HHID"].ToString());
            if (HHID > 0)
            {
                PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
                PAP_HouseholdBO objHousehold = objHouseHoldBLL.GetHouseHoldData(HHID);
                if (objHousehold != null)
                {
                    ProjectBLL objProjectBLL = new ProjectBLL();
                    ProjectBO objProject = objProjectBLL.GetProjectByProjectID(objHousehold.ProjectedId);

                    sPapdata += "<b>PROJECT CODE:</b> " + objProject.ProjectCode;
                    sPapdata += "<br /><b>PAP UID:</b> " + objHousehold.Pap_UId;
                    sPapdata += "<br /><b>HHID:</b> " + objHousehold.HhId;
                    if (objHousehold.PapName == null)
                        sPapdata += "<br /><b>PAP Name:</b> " + objHousehold.Surname + objHousehold.Firstname;
                    else
                        sPapdata += "<br /><b>PAP Name:</b> " + objHousehold.PapName;

                    sPapdata += "<br /><b>Plot Reference:</b> " + objHousehold.PlotReference;
                    sPapdata += "<br /><b>Designation:</b> " + objHousehold.Designation;
                    sPapdata += "<br /><b>District:</b> " + objHousehold.District;
                    sPapdata += "<br /><b>County:</b> " + objHousehold.County;
                    sPapdata += "<br /><b>SubCounty:</b> " + objHousehold.SubCounty;
                    sPapdata += "<br /><b>Village:</b> " + objHousehold.Village;
                    sPapdata += "<br /><b>Parish:</b> " + objHousehold.Parish;
                    sPapdata += "<br /><b>Rightofway (Acres):</b> " + objHousehold.Rightofway;
                    sPapdata += "<br /><b>Wayleaves (Acres):</b> " + objHousehold.Wayleaves;
                }
            }
            return sPapdata;
        }

        /// <summary>
        /// Set the PAp coordinates to Map
        /// </summary>
        public string GetPap()
        {
            int HHID = 0;
            if (Request.QueryString["HHID"] != null)
                HHID = Convert.ToInt32(Request.QueryString["HHID"].ToString());
            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            UploadPAPCoordinatesList objUploadPAPCoordinatesList = objUploadPAPCoordinatesBLL.GetAllPapCoordinatesData(HHID, Convert.ToInt32(Session["PROJECT_ID"]));

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int a = 0;
            string s1 = "";
            int ROWCount = 0;
            for (int i = 0; i < objUploadPAPCoordinatesList.Count; i++)
            {
                UploadPAPCoordinatesBO objUploadPAPCoordinatesBO = objUploadPAPCoordinatesList[i];
                if (objUploadPAPCoordinatesBO.ROW_LATITUDE != null && objUploadPAPCoordinatesBO.ROW_LATITUDE.Trim() != ""
                    && objUploadPAPCoordinatesBO.ROW_LONGITUDE != null && objUploadPAPCoordinatesBO.ROW_LONGITUDE.Trim() != "")
                {
                    ROWCount++;
                    if (a == 0)
                    {
                        s1 = string.Format("'{0},{1},{2}'", objUploadPAPCoordinatesBO.ROW_LATITUDE, objUploadPAPCoordinatesBO.ROW_LONGITUDE, objUploadPAPCoordinatesBO.Papname);
                        a++;
                    }
                    sb.Append(string.Format("'{0},{1},{2}'", objUploadPAPCoordinatesBO.ROW_LATITUDE, objUploadPAPCoordinatesBO.ROW_LONGITUDE, objUploadPAPCoordinatesBO.Papname));
                    sb.Append(";");
                }
                if (objUploadPAPCoordinatesList.Count == i + 1 && ROWCount > 0)
                {
                    sb.Append(s1);
                    sb.Append(";");
                }
            }
            if (ROWCount == 0)
            {
                for (int i = 0; i < objUploadPAPCoordinatesList.Count; i++)
                {
                    UploadPAPCoordinatesBO objUploadPAPCoordinatesBO = objUploadPAPCoordinatesList[i];
                    if (objUploadPAPCoordinatesBO.WL_LATITUDE != null && objUploadPAPCoordinatesBO.WL_LATITUDE.Trim() != ""
                        && objUploadPAPCoordinatesBO.WL_LONGITUDE != null && objUploadPAPCoordinatesBO.WL_LONGITUDE.Trim() != "")
                    {
                        ROWCount++;
                        if (a == 0)
                        {
                            s1 = string.Format("'{0},{1},{2}'", objUploadPAPCoordinatesBO.WL_LATITUDE, objUploadPAPCoordinatesBO.WL_LONGITUDE, objUploadPAPCoordinatesBO.Papname);
                            a++;
                        }
                        sb.Append(string.Format("'{0},{1},{2}'", objUploadPAPCoordinatesBO.ROW_LATITUDE, objUploadPAPCoordinatesBO.ROW_LONGITUDE, objUploadPAPCoordinatesBO.Papname));
                        sb.Append(";");
                    }
                    if (objUploadPAPCoordinatesList.Count == i + 1)
                    {
                        sb.Append(s1);
                        sb.Append(";");
                    }
                }
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);

            //System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            //sb1.Append(string.Format("'{0},{1},{2}'", "0.438583", "33.171651", "E"));
            //sb1.Append(";");
            //sb1.Append(string.Format("'{0},{1},{2}'", "0.43588", "33.172681", "B"));
            //sb1.Append(";");
            //sb1.Append(string.Format("'{0},{1},{2}'", "0.437553", "33.177788", "C"));
            //sb1.Append(";");
            //sb1.Append(string.Format("'{0},{1},{2}'", "0.441888", "33.176286", "D"));
            //sb1.Append(";");
            //sb1.Append(string.Format("'{0},{1},{2}'", "0.438583", "33.171651", "E"));
            //sb1.Append(";");

            //if (sb1.Length > 0) sb1.Remove(sb1.Length - 1, 1);

            return sb.ToString();
        }


        #region WebService
        [System.Web.Services.WebMethod]
        public static string getPapCoor(int id)
        {

            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            sb1.Append(string.Format("'{0},{1},{2}'", "0.469104", "33.164163", "E"));
            sb1.Append(";");
            sb1.Append(string.Format("'{0},{1},{2}'", "0.469145", "33.164285", "B"));
            sb1.Append(";");
            sb1.Append(string.Format("'{0},{1},{2}'", "0.468865", "33.164583", "C"));
            sb1.Append(";");
            sb1.Append(string.Format("'{0},{1},{2}'", "0.468782", "33.164507", "D"));
            sb1.Append(";");
            sb1.Append(string.Format("'{0},{1},{2}'", "0.469104", "33.164163", "E"));
            sb1.Append(";");

            if (sb1.Length > 0) sb1.Remove(sb1.Length - 1, 1);

            return sb1.ToString();
        }
        #endregion WebService
    }
}