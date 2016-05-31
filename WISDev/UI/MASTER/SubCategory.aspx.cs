using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class SubCategory: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["BGT_CATEGORYID"] = Request.QueryString["id"];
                ViewState["BGT_SUBCATEGORYID"] = 0;
                BindGrid(false, false); //Calling the Grid Data
               
                //if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                //{
                //    btnSave.Visible = false;
                //    btnClear.Visible = false;
                //    grdSubcategory.Columns[3].Visible = false;
                //    grdSubcategory.Columns[4].Visible = false;
                //    grdSubcategory.Columns[5].Visible = false;
                //}
            }
        }

        private void BindGrid(bool addRow, bool deleteRow)
        {
            SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();
            grdSubcategory.DataSource = SubCategoryBLLobj.GetALLSubCategory();
            grdSubcategory.DataBind();
        }

        protected void grdSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["BGT_SUBCATEGORYID"] = e.CommandArgument;
                GetSubCategoryDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();
                message = SubCategoryBLLobj.DeleteSubCategory(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(true);
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }

        private void GetSubCategoryDetails()
        {

            SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();
            int SUBCATEGORYID = 0;

            if (ViewState["BGT_SUBCATEGORYID"] != null)
                SUBCATEGORYID = Convert.ToInt32(ViewState["BGT_SUBCATEGORYID"]);

            SubCategoryBO SubCategoryBOobj = new SubCategoryBO();
            SubCategoryBOobj = SubCategoryBLLobj.GetSubCategoryById(SUBCATEGORYID);

            SubcategoryTextBox.Text = SubCategoryBOobj.BGT_SUBCATEGORYNAME;
        }



        private void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["BGT_SUBCATEGORYID"] = "0";
            }
        }

        private void ClearAll()
        {
            SubcategoryTextBox.Text = string.Empty;
            ViewState["BGT_SUBCATEGORYID"] = 0;
        }

        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {

                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                string BGT_SUBCATEGORYID = ((Literal)gr.FindControl("litCATEGORYID")).Text;
                int BGT_SUBCATEGORYID_ = Convert.ToInt32(BGT_SUBCATEGORYID);
                SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();
                message = SubCategoryBLLobj.ObsoleteSubCategory(BGT_SUBCATEGORYID_, Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid(false, true);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdSubcategory.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ViewState["BGT_SUBCATEGORYID"].ToString() == "0")
            {
                SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    SubCategoryBO SubCategoryBOobj = new SubCategoryBO();
                    SubCategoryBOobj.BGT_SUBCATEGORYNAME = SubcategoryTextBox.Text.ToString().Trim();
                    SubCategoryBOobj.CREATEDBY = Convert.ToInt32(uID);

                    SubCategoryBLL BLLobj = new SubCategoryBLL();
                    message = BLLobj.Insert(SubCategoryBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    SubCategoryBLLobj = null;
                }

            }
            else
            {
                SubCategoryBLL SubCategoryBLLobj = new SubCategoryBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    SubCategoryBO SubCategoryBOobj = new SubCategoryBO();
                    SubCategoryBOobj.BGT_SUBCATEGORYNAME = SubcategoryTextBox.Text.ToString().Trim();
                    SubCategoryBOobj.BGT_SUBCATEGORYID = Convert.ToInt32(ViewState["BGT_SUBCATEGORYID"]);
                    SubCategoryBOobj.CREATEDBY = Convert.ToInt32(uID);

                    SubCategoryBLL BLLobj = new SubCategoryBLL();
                    message = BLLobj.Edit(SubCategoryBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    SubCategoryBLLobj = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

       

    }
}