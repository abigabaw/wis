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
    public partial class RolePrivileges : System.Web.UI.Page
    {
        RolePrivilegesBO objPriv = null;
        DataTable subMenuTable = null;
        RolePrivilegesList RolePrivilegesLst = null;
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                string userName = Session["userName"].ToString();
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Role Privileges";
                getuserName();
                GenerateMenuItems();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_ROLE_PRIVILEGES) == false)
                {
                    SaveButton.Visible = false;
                    btnSaveTop.Visible = false;
                    ClearButton.Visible = false;
                    btnClearTop.Visible = false;
                }
            }
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void GenerateMenuItems()
        {
            RolePrivilegesLst = new RolePrivilegesList();
            RolePrivilegesBLL objRolePrivBLL = new RolePrivilegesBLL();

            DataTable mainMenuTable = objRolePrivBLL.GetRolePrivileges();
            subMenuTable = mainMenuTable.Copy();

            DataView MainMenuView = mainMenuTable.AsDataView();
            DataView SubMenuView = subMenuTable.AsDataView();

            MainMenuView.RowFilter = "PARENTMENUID=0";

            for (int i = 0; i < MainMenuView.Count; i++)
            {
                objPriv = new RolePrivilegesBO();
                objPriv.MenuID = Convert.ToInt32(MainMenuView[i]["CHILDMENUID"]);
                objPriv.MenuDescription = MainMenuView[i]["MENUDESCRIPTION"].ToString();
                objPriv.ChildMenuCount = Convert.ToInt32(MainMenuView[i]["CHILDMENUCOUNT"]);
                objPriv.MenuLevel = Convert.ToInt32(MainMenuView[i]["CHILDLEVEL"]);
                objPriv.ParentMenuID = Convert.ToInt32(MainMenuView[i]["PARENTMENUID"]);

                RolePrivilegesLst.Add(objPriv);

                GenerateChildMenu(MainMenuView[i]["CHILDMENUID"].ToString());
            }

            mainMenuTable = null;

            rptRolePrivileges.DataSource = RolePrivilegesLst;
            rptRolePrivileges.DataBind();
        }
        /// <summary>
        /// To generate child menu based on parentid from database
        /// </summary>
    
        private void GenerateChildMenu(string parentMenuID)
        {
            DataView menuView = new DataView(subMenuTable);

            menuView.RowFilter = "PARENTMENUID = " + parentMenuID;

            for (int i = 0; i < menuView.Count; i++)
            {
                objPriv = new RolePrivilegesBO();
                objPriv.MenuID = Convert.ToInt32(menuView[i]["CHILDMENUID"]);

                //sb.Clear();
                //paddingCount = Convert.ToInt32(menuView[i]["CHILDLEVEL"]);
                //for (int j = 1; j < paddingCount; j++)
                //{
                //    if (paddingCount == 2)
                //        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;------");
                //    else if (j < (paddingCount - 1))
                //        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //    else
                //        sb.Append("------");
                //}

                objPriv.ParentMenuID = Convert.ToInt32(menuView[i]["PARENTMENUID"]);
                objPriv.MenuDescription = menuView[i]["MENUDESCRIPTION"].ToString();
                objPriv.ChildMenuCount = Convert.ToInt32(menuView[i]["CHILDMENUCOUNT"]);
                objPriv.MenuLevel = Convert.ToInt32(menuView[i]["CHILDLEVEL"]);
                RolePrivilegesLst.Add(objPriv);

                GenerateChildMenu(menuView[i]["CHILDMENUID"].ToString());
            }
            menuView = null;
        }
        /// <summary>
        /// to set values to dropdownlist from database
        /// </summary>
     
        public void getuserName()
        {
            RolePrivilegesBO objRolePrivileges = new RolePrivilegesBO();
            RolePrivilegesBLL RolePrivilegesBLLobj = new RolePrivilegesBLL();

            UserBLL objUserBLL = new UserBLL();
            UserList objUserList = new UserList();
            UserBO oBOUser = null;
            oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;
            objUserList = objUserBLL.GetUsers(oBOUser);

            UserIDDropDownList.DataSource = objUserBLL.GetUsers(oBOUser);
            UserIDDropDownList.DataTextField = "UserName";
            UserIDDropDownList.DataValueField = "UserID";
            UserIDDropDownList.DataBind();
            UserIDDropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            UserIDDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            int userID = Convert.ToInt32(UserIDDropDownList.SelectedItem.Value.ToString());

            int count = 0;
            string noneValue = String.Empty;
            CheckBox chkView = null;
            CheckBox chkUpdate = null;

            RolePrivilegesBLL RolePrivilegesBLLOBJ = new RolePrivilegesBLL();
            RolePrivilegesBO objRolePrivileges = null;
            RolePrivilegesList RolePrivilegesList = new RolePrivilegesList();

            DeleteRolePrivileges(userID);

            try
            {
                foreach (RepeaterItem rptItem in rptRolePrivileges.Items)
                {
                    if (rptItem.ItemType == ListItemType.Item || rptItem.ItemType == ListItemType.AlternatingItem)
                    {
                        int menuID = Convert.ToInt32(((Literal)rptItem.FindControl("litMenuID")).Text);
                        chkView = (CheckBox)rptItem.FindControl("chkView");
                        chkUpdate = (CheckBox)rptItem.FindControl("chkUpdate");

                        if (menuID > 0)
                        {
                            objRolePrivileges = new RolePrivilegesBO();

                            objRolePrivileges.MenuID = menuID;

                            if (chkView != null && chkView.Checked)
                            {
                                objRolePrivileges.CanView = "Y";
                            }

                            if (chkUpdate != null && chkUpdate.Checked)
                            {
                                objRolePrivileges.CanUpdate = "Y";
                            }

                            objRolePrivileges.UserID = Convert.ToInt32(UserIDDropDownList.SelectedItem.Value);
                            objRolePrivileges.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                            count = RolePrivilegesBLLOBJ.InsertRolePrivilages(objRolePrivileges);
                        }
                    }
                }

                if (count > 0)
                {
                    message = "Data saved successfully";

                    System.Collections.IDictionaryEnumerator cacheDict = Cache.GetEnumerator();

                    while (cacheDict.MoveNext())
                    {
                        if (cacheDict.Key.ToString().IndexOf("PRIV_") >= 0)
                            Cache.Remove(cacheDict.Key.ToString());
                    };

                    GenerateMenuItems();
                    getRolePrivByUserID();
                }
                else
                {
                    message = "Data could not be saved";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                RolePrivilegesBLLOBJ = null;
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ROLEPRIVDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getRolePrivByUserID();
        }
        /// <summary>
        /// Tofetch role privileges based on userID
        /// </summary
        public void getRolePrivByUserID()
        {
            int userID = Convert.ToInt32(UserIDDropDownList.SelectedItem.Value.ToString());

            if (userID > 0)
            {
                CheckBox chkView = null;
                CheckBox chkUpdate = null;

                RolePrivilegesBO objRolePrivileges = new RolePrivilegesBO();
                RolePrivilegesBLL RolePrivilegesBLLobj = new RolePrivilegesBLL();
                RolePrivilegesList RolePrivilegesList = new RolePrivilegesList();
                RolePrivilegesList = RolePrivilegesBLLobj.GetROLEPRIId(userID);
                Literal litMenuID = null;

                // Clear the controls.
                ClearGridSelections();

                // Populate the controls
                foreach (RolePrivilegesBO oRolePrivilegesList in RolePrivilegesList)
                {
                    foreach (RepeaterItem rptRow in rptRolePrivileges.Items)
                    {
                        litMenuID = (Literal)rptRow.FindControl("litMenuID");
                        chkView = (CheckBox)rptRow.FindControl("chkView");
                        chkUpdate = (CheckBox)rptRow.FindControl("chkUpdate");

                        if (oRolePrivilegesList.MenuID.ToString() == litMenuID.Text)
                        {
                            if (oRolePrivilegesList.CanView == "Y")
                                chkView.Checked = true;

                            if (oRolePrivilegesList.CanUpdate == "Y")
                                chkUpdate.Checked = true;

                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// To delete roleprivileges
        /// </summary
        private void DeleteRolePrivileges(int DeletedID)
        {
            RolePrivilegesBLL RolePrivilegesBLLobj = new RolePrivilegesBLL();
            RolePrivilegesBLLobj.DeleteRolePrivileges(DeletedID);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            UserIDDropDownList.ClearSelection();
            ClearGridSelections();
        }
        /// <summary>
        /// To uncheck view and update checkbox
        /// </summary>
        private void ClearGridSelections()
        {
            foreach (RepeaterItem rptRow in rptRolePrivileges.Items)
            {
                ((CheckBox)rptRow.FindControl("chkView")).Checked = false;
                ((CheckBox)rptRow.FindControl("chkUpdate")).Checked = false;
            }
        }
        /// <summary>
        /// To bind data to menu from database
        /// </summary>
        protected void rptRolePrivileges_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int menuLevel = (int)DataBinder.Eval(e.Item.DataItem, "MenuLevel");
                int childMenuCount = (int)DataBinder.Eval(e.Item.DataItem, "ChildMenuCount");
                Literal litSpacer = (Literal)e.Item.FindControl("litSpacer");
                Label lblMenuDescription = (Label)e.Item.FindControl("lblMenuDescription");

                if (menuLevel > 1)
                {
                    for (int i = 1; i <= menuLevel; i++)
                    {
                        litSpacer.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                else
                {
                    lblMenuDescription.Font.Bold = true;
                }

                if (childMenuCount > 0)
                {
                    ((CheckBox)e.Item.FindControl("chkView")).Visible = false;
                    ((CheckBox)e.Item.FindControl("chkUpdate")).Visible = false;
                    ((Literal)e.Item.FindControl("litMenuID")).Text = "0";
                }
            }
        }
    }
}
