using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using System.Data;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class UploadPAPCoordinates : System.Web.UI.Page
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
                string HHID = Request.QueryString["HHID"].ToString();
                string PapName = Request.QueryString["PapName"].ToString();

                ViewState["PAPCOORID"] = "0";
                Master.PageHeader = "Coordinates for " + PapName;
                BindGrid();
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPAPCoordinates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PAPCOORID"] = e.CommandArgument;
                int ID = Convert.ToInt32(ViewState["PAPCOORID"]);
                GetPapCoorDetailsByID(ID);
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                UploadPAPCoordinatesBLL UploadPAPCoordinatesBLLobj = new UploadPAPCoordinatesBLL();
                UploadPAPCoordinatesBLLobj.DeletePapCoordinates(Convert.ToInt32(e.CommandArgument));
               
                string message = "Data Deleted successfully";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                ClearDetails();
                SetUpdateMode(false);
                BindGrid();
            }
        }

        private void GetPapCoorDetailsByID(int ID)
        {
            UploadPAPCoordinatesBLL ObjBLL = new UploadPAPCoordinatesBLL();
            UploadPAPCoordinatesBO OBJbo = new UploadPAPCoordinatesBO();
            OBJbo = ObjBLL.GetPapCoordinatesDataByID(ID);
            txtROWX.Text = OBJbo.ROW_X;
            txtROWY.Text = OBJbo.ROW_Y;
            txtROWLatitude.Text = OBJbo.ROW_LATITUDE;
            txtROWLongitude.Text = OBJbo.ROW_LONGITUDE; 
            txtWLX.Text = OBJbo.WL_X;
            txtWLY.Text = OBJbo.WL_Y;
            txtWLLatitude.Text = OBJbo.WL_LATITUDE;
            txtWLLongitude.Text = OBJbo.WL_LONGITUDE;
        }
        /// <summary>
        /// To makes textboxes in grid readonly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPAPCoordinates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtLat = (TextBox)e.Row.FindControl("txtLATITUDE");
                TextBox txtLng = (TextBox)e.Row.FindControl("txtLONGITUDE");
                TextBox txtLat1 = (TextBox)e.Row.FindControl("txtWLLATITUDE");
                TextBox txtLng2 = (TextBox)e.Row.FindControl("txtWLLONGITUDE");

                txtLat.Attributes.Add("readonly", "readonly");
                txtLng.Attributes.Add("readonly", "readonly");
                txtLat1.Attributes.Add("readonly", "readonly");
                txtLng2.Attributes.Add("readonly", "readonly");
            }
        }
        /// <summary>
        /// To upload files
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
                    if (GrdPAPCoordinates.Rows.Count > 0)
                    {
                        EnableSaveCancel(true);
                        GrdPAPCoordinates.Visible = true;
                    }
                    else
                    {
                        GrdPAPCoordinates.Visible = false;
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
        /// To enable save and cancel
        /// </summary>
        /// <param name="Status"></param>
        private void EnableSaveCancel(bool Status)
        {
            btnLoadCordinate.Visible = Status;
            btnGridDataCancel.Visible = Status;
        }

        //To Clear the Roots
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        protected void ClearDetails()
        {
            txtROWX.Text = "";
            txtROWY.Text = "";
            txtROWLatitude.Text = "";
            txtROWLongitude.Text = "";
            txtWLX.Text = "";
            txtWLY.Text = "";
            txtWLLatitude.Text = "";
            txtWLLongitude.Text = "";

            ViewState["PAPCOORID"] = 0;
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            pnlRouteCoordinatesDetails.Visible = true;
            btn_ImportExcel.Visible = true;
            // lnkMap.Visible = true;
            pnlFileUpload.Visible = false;
            EnableSaveCancel(false);
            BindGrid();
        }

        protected void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            //btn_ImportExcel.Attributes.Add("onclick", "UploadCoordinates();");
            pnlRouteCoordinatesDetails.Visible = false;
            pnlFileUpload.Visible = true;
            btn_ImportExcel.Visible = false;
            // lnkMap.Visible = false;
            //GrdPAPCoordinates.Visible = false;
            EnableSaveCancel(false);
        }

        /// <summary>
        /// To save the roots manually
        /// </summary>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";
            UploadPAPCoordinatesBO objBo;
            objBo = new UploadPAPCoordinatesBO();
            objBo.HHID = Convert.ToInt32(Request.QueryString["HHID"].ToString());
            if (txtROWX.Text.Trim() != "")
                objBo.ROW_X = txtROWX.Text;
            if (txtROWY.Text.Trim() != "")
                objBo.ROW_Y = txtROWY.Text;
            if (txtROWLatitude.Text.Trim() != "")
                objBo.ROW_LATITUDE = txtROWLatitude.Text;
            if (txtROWLongitude.Text.Trim() != "")
                objBo.ROW_LONGITUDE = txtROWLongitude.Text;
            if (txtWLX.Text.Trim() != "")
                objBo.WL_X = txtWLX.Text;
            if (txtWLY.Text.Trim() != "")
                objBo.WL_Y = txtWLY.Text;
            if (txtWLLatitude.Text.Trim() != "")
                objBo.WL_LATITUDE = txtWLLatitude.Text;
            if (txtWLLongitude.Text.Trim() != "")
                objBo.WL_LONGITUDE = txtWLLongitude.Text;

            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            if (Convert.ToInt32(ViewState["PAPCOORID"]) == 0)
            {
                objBo.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                objUploadPAPCoordinatesBLL.SavePAPCoordinates(objBo);

                message = "Data saved successfully";
            }
            else
            {
                objBo.Id = Convert.ToInt32(ViewState["PAPCOORID"]);
                objBo.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                objUploadPAPCoordinatesBLL.UpdatePAPCoordinates(objBo);

                message = "Data updated successfully";
            }
            ClearDetails();
            BindGrid();

            SetUpdateMode(false);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
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
                ViewState["PAPCOORID"] = "0";
            }
        }
        /// <summary>
        /// To import data to grid
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="fileExtension"></param>
        private void Import_To_Grid(string pFileName, string fileExtension)
        {
            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            DataTable dtCoordinates = objUploadPAPCoordinatesBLL.ExcelDataImportintoGrid(pFileName, fileExtension, 0, Convert.ToInt32(Session["USER_ID"]));
            if (dtCoordinates.Columns.Contains("PAP NUMBER") &&
                dtCoordinates.Columns.Contains("ROW_X") && dtCoordinates.Columns.Contains("ROW_Y") &&
                dtCoordinates.Columns.Contains("WL_X") && dtCoordinates.Columns.Contains("WL_Y"))
            {
                dtCoordinates.Columns.Add("ID", typeof(int));
                dtCoordinates.Columns.Add("ROW_LATITUDE", typeof(string));
                dtCoordinates.Columns.Add("ROW_LONGITUDE", typeof(string));
                dtCoordinates.Columns.Add("WL_LATITUDE", typeof(string));
                dtCoordinates.Columns.Add("WL_LONGITUDE", typeof(string));
                if (!dtCoordinates.Columns.Contains("HHID"))
                    dtCoordinates.Columns.Add("HHID", typeof(string));
                GrdPAPCoordinates.DataSource = dtCoordinates;
                GrdPAPCoordinates.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CalcLatLongForGrid", "CalcLatLongForGrid()", true);
                pnlFileUpload.Visible = false;
                DataTable dt = (DataTable)GrdPAPCoordinates.DataSource;
                ViewState["EXCEL_DATA"] = dtCoordinates;
                GrdPAPCoordinates.Columns[GrdPAPCoordinates.Columns.Count - 1].Visible = false;
                GrdPAPCoordinates.Columns[GrdPAPCoordinates.Columns.Count - 2].Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid Excel File.Please Upload a valid Excel File.');", true);
            }
        }
        /// <summary>
        /// To load co ordinates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadCordinate_Click(object sender, EventArgs e)
        {

            UploadPAPCoordinatesList list1 = new UploadPAPCoordinatesList();
            UploadPAPCoordinatesBO objBo;
            for (int i = 0; i < GrdPAPCoordinates.Rows.Count; i++)
            {
                objBo = new UploadPAPCoordinatesBO();
                objBo.HHID = Convert.ToInt32(Request.QueryString["HHID"].ToString());
                if (GrdPAPCoordinates.Rows[i].Cells[2].Text != "&nbsp;")
                    objBo.ROW_X = GrdPAPCoordinates.Rows[i].Cells[2].Text;
                if (GrdPAPCoordinates.Rows[i].Cells[3].Text != "&nbsp;")
                    objBo.ROW_Y = GrdPAPCoordinates.Rows[i].Cells[3].Text;
                TextBox txtlat = (TextBox)GrdPAPCoordinates.Rows[i].FindControl("txtLATITUDE");
                TextBox txtlon = (TextBox)GrdPAPCoordinates.Rows[i].FindControl("txtLONGITUDE");
                if (txtlat.Text != "&nbsp;")
                    objBo.ROW_LATITUDE = txtlat.Text;
                if (txtlon.Text != "&nbsp;")
                    objBo.ROW_LONGITUDE = txtlon.Text;
                if(GrdPAPCoordinates.Rows[i].Cells[6].Text !="&nbsp;")
                    objBo.WL_X = GrdPAPCoordinates.Rows[i].Cells[6].Text;

                if (GrdPAPCoordinates.Rows[i].Cells[7].Text != "&nbsp;")
                    objBo.WL_Y = GrdPAPCoordinates.Rows[i].Cells[7].Text;
                TextBox txtlat1 = (TextBox)GrdPAPCoordinates.Rows[i].FindControl("txtWLLATITUDE");
                TextBox txtlon1 = (TextBox)GrdPAPCoordinates.Rows[i].FindControl("txtWLLONGITUDE");
                if (txtlat1.Text != "&nbsp;")
                    objBo.WL_LATITUDE = txtlat1.Text;
                if (txtlon1.Text != "&nbsp;")
                    objBo.WL_LONGITUDE = txtlon1.Text;
                list1.Add(objBo);
            }
            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            string message = "";
            string uID = Session["USER_ID"].ToString();

            string Count = objUploadPAPCoordinatesBLL.SaveExcelData(list1, Session["USER_ID"].ToString());
            if (!string.IsNullOrEmpty(Count))
            {
                message = Count + " - Coordinates(s) were added successfully";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            pnlRouteCoordinatesDetails.Visible = true;
            pnlFileUpload.Visible = false;
            btn_ImportExcel.Visible = true;
            BindGrid();
            EnableSaveCancel(false);
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            GrdPAPCoordinates.DataSource = objUploadPAPCoordinatesBLL.GetAllPapCoordinatesData(Convert.ToInt32(Request.QueryString["HHID"].ToString()), Convert.ToInt32(Session["PROJECT_ID"]));
            GrdPAPCoordinates.DataBind();
            GrdPAPCoordinates.Columns[GrdPAPCoordinates.Columns.Count - 1].Visible = true;
            GrdPAPCoordinates.Columns[GrdPAPCoordinates.Columns.Count - 2].Visible = true;
        }
        /// <summary>
        /// To disable save and cancel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGridDataCancel_Click(object sender, EventArgs e)
        {
            pnlRouteCoordinatesDetails.Visible = true;
            pnlFileUpload.Visible = false;
            btn_ImportExcel.Visible = true;
            BindGrid();
            EnableSaveCancel(false);
        }
    }
}