using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using WIS.UI;

namespace WIS
{
    public partial class RouteCoordinates : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string RouteName;
            if (!IsPostBack)
            {
                RouteName = Request.QueryString["Route"].ToString();

                Master.PageHeader = "Coordinates for Route " + RouteName;
                ViewState["ROUTE_ID"] = Convert.ToInt32(Request.QueryString["RouteId"].ToString());
                lnkMap.Attributes["href"] = string.Format("RouteMap.aspx?routeID={0}", ViewState["ROUTE_ID"].ToString());

                ViewState["ROUTE_COORDINATEID"] = 0;
                BindGrid();

                findRouteStatusafterSave(Convert.ToInt32(Session["PROJECT_ID"]));
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btn_ImportExcel.Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                }
                getFinalRouteApprovalDetial(Convert.ToInt32(Session["PROJECT_ID"]));
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btn_ImportExcel.Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                }
            }
        }
        /// <summary>
        /// To fetch FinalRouteApprovalDetial 
        /// </summary>
        /// <param name="ProjectId"></param>
        public void getFinalRouteApprovalDetial(int ProjectId)
        {

            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteList ProjectRouteList = new ProjectRouteList();

            objProjectRoute.Project_Id = ProjectId;

            ProjectRouteList = objProjectRouteBLL.getFinalRouteApprovalDetial(objProjectRoute);

            //ProjectRouteList = objProjectRouteBLL.getFinalRouteApprovalDetial(objProjectRoute);

            if ((ProjectRouteList.Count) > 0)
            {
                for (int i = 0; i < ProjectRouteList.Count; i++)
                {
                    if (ProjectRouteList[i].IsFinal == "TRUE")
                    {
                        if (ProjectRouteList[i].ApprovedstatusID == 1)
                        {
                            btn_Save.Visible = false;
                            btn_Clear.Visible = false;
                            btn_ImportExcel.Visible = false;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                        }
                    }
                    else
                    {
                        if (ProjectRouteList[i].ApprovedstatusID == 3)
                        {
                            btn_Save.Visible = false;
                            btn_Clear.Visible = false;
                            btn_ImportExcel.Visible = false;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                        }
                        if (ProjectRouteList[i].ApprovedstatusID == 2)
                        {
                            btn_Save.Visible = true;
                            btn_Clear.Visible = true;
                            btn_ImportExcel.Visible = true;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = true;
                            GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// to find Route Status after Save
        /// </summary>
        /// <param name="ProjectId"></param>
        public void findRouteStatusafterSave(int ProjectId)
        {
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            objProjectRoute.Project_Id = ProjectId;

            objProjectRoute = objProjectRouteBLL.findRouteStatusafterSave(objProjectRoute);
            if ((objProjectRoute) != null)
            {
                if (objProjectRoute.ApprovedstatusID == 3)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btn_ImportExcel.Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                    GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                }
            }
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            if (pnlFileUpload.Visible)
            {

                GrdRouteCoordinates.DataSource = (DataTable)ViewState["EXCEL_DATA"];
                GrdRouteCoordinates.DataBind();
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                lnkMap.Visible = false;
            }
            else
            {
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = true;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = true;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 3].Visible = false;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 4].Visible = false;
                RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
                GrdRouteCoordinates.DataSource = objRouteCoordinatesBLL.GetRouteCoordinates(ViewState["ROUTE_ID"].ToString());
                GrdRouteCoordinates.DataBind();
                GrdRouteCoordinates.Visible = true;
                if (GrdRouteCoordinates.Rows.Count > 1)
                {
                    lnkMap.Visible = true;
                }
                else
                {
                    lnkMap.Visible = false;
                }
            }
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";

            if (!IsDuplicateRoute(btn_Save.Text))
            {

                RouteCoordinatesBO objRouteCoordinates = new RouteCoordinatesBO();
                objRouteCoordinates.Route_CoordinateID = Convert.ToInt32(ViewState["ROUTE_COORDINATEID"]);
                objRouteCoordinates.X_axis = txtX.Text.Trim();
                objRouteCoordinates.Y_axis = txtY.Text.Trim();
                objRouteCoordinates.Z_axis = txtZ.Text.Trim();
                objRouteCoordinates.Latitude = txtLatitude.Text;
                objRouteCoordinates.Longitude = txtLongitude.Text;
                objRouteCoordinates.Route_ID = Convert.ToInt32(ViewState["ROUTE_ID"]);
                RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
                if (objRouteCoordinates.Route_CoordinateID == 0)
                {
                    objRouteCoordinates.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    objRouteCoordinatesBLL.AddRouteCoordinates(objRouteCoordinates);

                    message = "Data saved successfully";
                }
                else
                {
                    objRouteCoordinates.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                    objRouteCoordinatesBLL.UpdateRouteCoordinates(objRouteCoordinates);

                    message = "Data updated successfully";
                }
                ClearDetails();
                BindGrid();

                SetUpdateMode(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            else
            {
                message = "Route Coordinates already exist in the system";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
        }
        /// <summary>
        /// To check if DuplicateRoute is present
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private bool IsDuplicateRoute( string Text)
        {
            bool Dupl = false;
            int idup = 0;
            try
            {
                if (GrdRouteCoordinates.Rows.Count > 0)
                {
                    foreach (GridViewRow dr in GrdRouteCoordinates.Rows)
                    {
                        if (dr.Cells[1].Text.Trim() == txtX.Text.Trim() && dr.Cells[2].Text.Trim() == txtY.Text.Trim() && dr.Cells[3].Text.Trim() == txtZ.Text.Trim())
                        {
                            idup++;
                            //Dupl = true;
                            //break;
                        }
                    }
                }
                if (Text.ToUpper() == "SAVE" && idup > 0 )
                {
                    Dupl = true;
                }
                else if (idup > 1)
                    Dupl = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Dupl;
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        protected void ClearDetails()
        {
            txtX.Text = "";
            txtY.Text = "";
            txtZ.Text = "";
            ViewState["ROUTE_COORDINATEID"] = 0;
            btn_Save.Text = "Save";
        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            //pnlRouteCoordinatesDetails.Visible = false;
            //if (showAdd) pnlRouteCoordinatesDetails.Visible = true;
        }
        /// <summary>
        /// calls clear details method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (btn_Clear.Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// data gets imported to excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            //btn_ImportExcel.Attributes.Add("onclick", "UploadCoordinates();");
            pnlRouteCoordinatesDetails.Visible = false;
            pnlFileUpload.Visible = true;
            btn_ImportExcel.Visible = false;
            lnkMap.Visible = false;
            GrdRouteCoordinates.Visible = false;
            EnableSaveCancel(false);
        }
        /// <summary>
        /// To cancel upload of file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelUpload_Click(object sender, EventArgs e)
        {
            pnlRouteCoordinatesDetails.Visible = true;
            btn_ImportExcel.Visible = true;
            lnkMap.Visible = true;
            pnlFileUpload.Visible = false;
            EnableSaveCancel(false);
            BindGrid();
        }
        /// <summary>
        /// To Enable Save and Cancel
        /// </summary>
        /// <param name="Status"></param>
        private void EnableSaveCancel(bool Status)
        {
            btnLoadCordinate.Visible = Status;
            btnGridDataCancel.Visible = Status;
        }
        /// <summary>
        /// To load co ordiante data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadCordinate_Click(object sender, EventArgs e)
        {

            RouteCoordinatesList list1 = new RouteCoordinatesList();
            RouteCoordinatesBO objBo;
            for (int i = 0; i < GrdRouteCoordinates.Rows.Count; i++)
            {
                objBo = new RouteCoordinatesBO();
                if (GrdRouteCoordinates.Rows[i].Cells[1].Text.Trim() != "&nbsp;")
                    objBo.X_axis = GrdRouteCoordinates.Rows[i].Cells[1].Text;
                if (GrdRouteCoordinates.Rows[i].Cells[2].Text.Trim() != "&nbsp;")
                    objBo.Y_axis = GrdRouteCoordinates.Rows[i].Cells[2].Text;
                if (GrdRouteCoordinates.Rows[i].Cells[3].Text.Trim() != "&nbsp;")
                    objBo.Z_axis = GrdRouteCoordinates.Rows[i].Cells[3].Text;
                //TextBox txtlat = (TextBox)GrdRouteCoordinates.Rows[i].Cells[0].FindControl("txtLATITUDE");
                //TextBox txtlon = (TextBox)GrdRouteCoordinates.Rows[i].Cells[0].FindControl("txtLONGITUDE");
                TextBox txtlat = (TextBox)GrdRouteCoordinates.Rows[i].FindControl("txtLATITUDE");
                TextBox txtlon = (TextBox)GrdRouteCoordinates.Rows[i].FindControl("txtLONGITUDE");
                objBo.Latitude = txtlat.Text;
                objBo.Longitude = txtlon.Text;
                list1.Add(objBo);
            }
            RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
            string message = "";
            string uID = Session["USER_ID"].ToString();

            string Count = objRouteCoordinatesBLL.SaveExcelData(list1, Convert.ToInt32(ViewState["ROUTE_ID"]), Session["USER_ID"].ToString());
            if (!string.IsNullOrEmpty(Count))
            {
                if (Count == "0")
                    message = "Coordinates already exist in the system.";
                else
                    message = Count + " - Coordinates(s) were added successfully";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            pnlRouteCoordinatesDetails.Visible = true;
            pnlFileUpload.Visible = false;
            EnableSaveCancel(false);
            BindGrid();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGridDataCancel_Click(object sender, EventArgs e)
        {
            pnlRouteCoordinatesDetails.Visible = true;
            btn_ImportExcel.Visible = true;
            lnkMap.Visible = true;
            pnlFileUpload.Visible = false;
            EnableSaveCancel(false);
            BindGrid();
        }
        /// <summary>
        /// To upload details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
                string Extension = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName);
                if (Extension.ToUpper().Contains(".xlsx".ToUpper()) ||
                    Extension.ToUpper().Contains(".xls".ToUpper()))
                {
                    string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = FolderPath + FileName;
                    FileUpload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension);
                    if (GrdRouteCoordinates.Rows.Count > 0)
                    {
                        EnableSaveCancel(true);
                        GrdRouteCoordinates.Visible = true;
                    }
                    else
                    {
                        GrdRouteCoordinates.Visible = false;
                        EnableSaveCancel(false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid File.Please Upload an Excel File');", true);
                }
            }
        }
        /// <summary>
        /// To Import To Grid
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="fileExtension"></param>
        private void Import_To_Grid(string pFileName, string fileExtension)
        {
            RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
            DataTable dtCoordinates = objRouteCoordinatesBLL.ExcelDataImportintoGrid(pFileName, fileExtension, Convert.ToInt32(ViewState["ROUTE_ID"]), Convert.ToInt32(Session["USER_ID"]));
            dtCoordinates.Columns.Add("LATITUDE", typeof(string));
            dtCoordinates.Columns.Add("LONGITUDE", typeof(string));
            dtCoordinates.Columns.Add("ROUTE_COORDINATEID", typeof(string));
            if (dtCoordinates.Columns.Contains("X_axis") && dtCoordinates.Columns.Contains("Y_axis") && dtCoordinates.Columns.Contains("Z_axis"))
            {
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 1].Visible = false;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 2].Visible = false;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 3].Visible = true;
                GrdRouteCoordinates.Columns[GrdRouteCoordinates.Columns.Count - 4].Visible = true;
                GrdRouteCoordinates.DataSource = dtCoordinates;
                GrdRouteCoordinates.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CalcLatLongForGrid", "CalcLatLongForGrid()", true);
                pnlRouteCoordinatesDetails.Visible = false;
                pnlFileUpload.Visible = true;
                DataTable dt = (DataTable)GrdRouteCoordinates.DataSource;
                ViewState["EXCEL_DATA"] = dtCoordinates;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid Excel File.Please Upload valid Excel File');", true);
            }
        }
        # region
        // conversion

        //end
        #endregion 
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdRouteCoordinates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdRouteCoordinates.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdRouteCoordinates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["ROUTE_COORDINATEID"] = e.CommandArgument;
                RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
                RouteCoordinatesBO objRouteCoordinates = new RouteCoordinatesBO();
                objRouteCoordinates = objRouteCoordinatesBLL.GetRouteCoordinatesByID(Convert.ToInt32(ViewState["ROUTE_COORDINATEID"]));
               
                txtX.Text = objRouteCoordinates.X_axis;
                txtY.Text = objRouteCoordinates.Y_axis;
                txtZ.Text = objRouteCoordinates.Z_axis;

                ClientScript.RegisterStartupScript(this.GetType(), "CalcLatLong", "CalcLatLong();", true);
                //updtpnl1.Update();
                //updRoute.Update();
                SetUpdateMode(true);

                
            }

            else if (e.CommandName == "DeleteRow")
            {
                int count = 0;
                RouteCoordinatesBLL objRouteCoordinatesBLL = new RouteCoordinatesBLL();
                count = objRouteCoordinatesBLL.DeleteRouteCoordinates(Convert.ToInt32(e.CommandArgument));
                ClearDetails();
                SetUpdateMode(false);
                if (count == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
                }
                BindGrid();
            }
        }
        /// <summary>
        /// to change text of the button based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["ROUTE_COORDINATEID"] = "0";
            }
        }
        /// <summary>
        /// To make textboxes in the grid to read only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdRouteCoordinates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtLat = (TextBox)e.Row.FindControl("txtLATITUDE");
                TextBox txtLng = (TextBox)e.Row.FindControl("txtLONGITUDE");

                txtLat.Attributes.Add("readonly", "readonly");
                txtLng.Attributes.Add("readonly", "readonly");
            }
        }






    }
}