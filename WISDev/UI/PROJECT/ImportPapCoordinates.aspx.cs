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
    public partial class ImportPapCoordinates : System.Web.UI.Page
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
                Master.PageHeader = "Import Pap Coordinates";
                if (Session["PROJECT_CODE"] != null)
                {
                    //Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Other Fixtures";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                BindGrid();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_IMP_PAP_COORDINATES) == false)
                {
                    pnlFileUpload.Visible = false;
                }
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

            }

            else if (e.CommandName == "DeleteRow")
            {

            }
        }
        /// <summary>
        /// To make values in textbox in grid as read only
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
        /// To upload file and import to grid
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
        /// method to enable save and cancel
        /// </summary>
        /// <param name="Status"></param>
        private void EnableSaveCancel(bool Status)
        {
            btnLoadCordinate.Visible = Status;
            btnGridDataCancel.Visible = Status;
        }
        /// <summary>
        /// method to import data from excel to grid
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
                dtCoordinates.Columns.Add("ROW_LATITUDE", typeof(string));
                dtCoordinates.Columns.Add("ROW_LONGITUDE", typeof(string));
                dtCoordinates.Columns.Add("WL_LATITUDE", typeof(string));
                dtCoordinates.Columns.Add("WL_LONGITUDE", typeof(string));
                if (!dtCoordinates.Columns.Contains("HHID"))
                    dtCoordinates.Columns.Add("HHID", typeof(string));
                GrdPAPCoordinates.DataSource = dtCoordinates;
                GrdPAPCoordinates.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CalcLatLongForGrid", "CalcLatLongForGrid();", true);
                pnlFileUpload.Visible = false;
                DataTable dt = (DataTable)GrdPAPCoordinates.DataSource;
                ViewState["EXCEL_DATA"] = dtCoordinates;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid Excel File.Please Upload a valid Excel File.');", true);
            }
        }
        /// <summary>
        /// Loads pap co ordinates
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
                if (GrdPAPCoordinates.Rows[i].Cells[1].Text.Trim().Length > 0)
                    objBo.HHID = Convert.ToInt32(GrdPAPCoordinates.Rows[i].Cells[1].Text.Trim());
                else
                    objBo.HHID = 0;
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
                if (GrdPAPCoordinates.Rows[i].Cells[6].Text != "&nbsp;")
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

            string Count = objUploadPAPCoordinatesBLL.SaveExcelDataForAllPaps(list1, Session["USER_ID"].ToString());
            if (!string.IsNullOrEmpty(Count))
            {
                message = Count + " - Coordinates(s) were added successfully";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            EnableSaveCancel(false);
            BindGrid();
            pnlFileUpload.Visible = true;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            UploadPAPCoordinatesBLL objUploadPAPCoordinatesBLL = new UploadPAPCoordinatesBLL();
            GrdPAPCoordinates.DataSource = objUploadPAPCoordinatesBLL.GetAllPapCoordinatesData(0, Convert.ToInt32(Session["PROJECT_ID"]));
            GrdPAPCoordinates.DataBind();
        }
        /// <summary>
        /// to enable pnlFileUpload and hide savecancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGridDataCancel_Click(object sender, EventArgs e)
        {
            pnlFileUpload.Visible = true;
            BindGrid();
            EnableSaveCancel(false);
        }
    }
}