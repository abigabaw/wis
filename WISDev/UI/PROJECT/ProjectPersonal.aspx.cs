using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ProjectPersonal : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.Personnel;

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Project Personnel";

                BindGrid(false, false);
                BindProjectUsers();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btn_Save.Visible = false;
                    BtnAdd.Visible = false;
                    btnReset.Visible = false;
                    Btn_remove.Visible = false;
                }
            }
        }
        /// <summary>
        /// To bind values to project listbox from database
        /// </summary>
        private void BindProjectUsers()
        {
            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();

            ProjectPersonalList ProjectPersonnels = objProjPersonalLogic.GetProjectPersonnel(Convert.ToInt32(Session["PROJECT_ID"]));

            foreach (ProjectPersonalBO objProjPers in ProjectPersonnels)
            {
                if (LstUsers.Items.FindByValue(objProjPers.UserID.ToString()) != null)
                    LstUsers.Items.Remove(LstUsers.Items.FindByValue(objProjPers.UserID.ToString()));
            }

            LstProject.DataSource = ProjectPersonnels;
            LstProject.DataTextField = "UserName";
            LstProject.DataValueField = "UserID";
            LstProject.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            ProjectPersonalBO objPP = new ProjectPersonalBO();
            ProjectPersonalBLL objPPBLL = new ProjectPersonalBLL();
            UserBLL objUserBLL = new UserBLL();

            UserBO oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;

            UserList UserList = objUserBLL.GetUsers(oBOUser);

            ListItem lstItem = null;
            LstUsers.Items.Clear();
            foreach (UserBO objUser in UserList)
            {
                lstItem = new ListItem();
                lstItem.Value = objUser.UserID.ToString();
                lstItem.Text = objUser.UserName;
                LstUsers.Items.Add(lstItem);
            }
        }

        /// <summary>
        /// calls MoveListBoxItems method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveListBoxItems(LstUsers, LstProject);

        }
        /// <summary>
        /// Adds values from lstFrom to lstTo and removes items from lstFrom
        /// </summary>
        /// <param name="lstFrom"></param>
        /// <param name="lstTo"></param>
        private void MoveListBoxItems(ListBox lstFrom, ListBox lstTo)
        {
            for (int i = lstFrom.Items.Count - 1; i >= 0; i--)
            {
                if (lstFrom.Items[i].Selected)
                {
                    lstTo.Items.Add(new ListItem(lstFrom.SelectedItem.Text, lstFrom.SelectedItem.Value));
                    lstFrom.Items.Remove(lstFrom.SelectedItem);
                }
            }
        }

        //private void MoveListBoxItems1(ListBox from, ListBox to)
        //{
        //    for (int i = 0; i < LstProject.Items.Count; i++)
        //    {
        //        if (LstProject.Items[i].Selected)
        //        {
        //            LstUsers.Items.Add(LstProject.SelectedItem);
        //            LstProject.Items.Remove(LstProject.SelectedItem);
        //        }
        //    }
        //    from.SelectedIndex = -1;
        //    to.SelectedIndex = -1;
        //}
        /// <summary>
        /// calls MoveListBoxItems method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(LstUsers, LstProject);
        }
        /// <summary>
        /// calls MoveListBoxItems method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_remove_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            if (LstProject.SelectedItem != null)
            {
                ProjectPersonalBLL objPPBLL = new ProjectPersonalBLL();
                message = objPPBLL.CheckUser(Convert.ToInt32(LstProject.SelectedItem.Value), Convert.ToInt32(Session["PROJECT_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    MoveListBoxItems(LstProject, LstUsers);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
            }
        }
        /// <summary>
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveProjectPersonnel();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveProjectPersonnel()
        {
            int ProjID = int.Parse(Session["PROJECT_ID"].ToString());
            int userID = Convert.ToInt32(Session["USER_ID"]);
            ProjectPersonalList PPList = new ProjectPersonalList();
            ProjectPersonalBO objPP = null;

            foreach (ListItem lstItem in LstProject.Items)
            {
                objPP = new ProjectPersonalBO();
                objPP.UserID = Convert.ToInt32(lstItem.Value);
                objPP.ProjID = ProjID;
                objPP.CreatedBy = userID;
                PPList.Add(objPP);
            }

            ProjectPersonalBLL objPPBLL = new ProjectPersonalBLL();
            objPPBLL.AddPersonal(ProjID, PPList);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
        }

        protected void LstProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MoveListBoxItems1(LstProject, LstUsers);
        }
        /// <summary>
        /// resets the values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            LstProject.Items.Clear();
            SaveProjectPersonnel();
            BindGrid(false, false);            
        }
    }
}
