using System;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.DirectoryServices;


namespace WIS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get Username if Stay Signed In was checked by user.
                System.Web.HttpCookie usernameCookie = Request.Cookies.Get("WIS_USER");
                if (usernameCookie != null)
                {
                    UsernameTextBox.Text = usernameCookie.Value;
                    chkStaySignedIn.Checked = true;
                }
                if (string.IsNullOrEmpty(UsernameTextBox.Text.Trim()))
                {
                    UsernameTextBox.Focus();
                }
                else
                {
                    PasswordTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Check user Credintials are valid or not
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Logincheck();
        }
        /// <summary>
        /// Check user Credintials are valid or not
        /// </summary>
        private void Logincheck()
        {
            string LDAPDomainName = System.Configuration.ConfigurationManager.AppSettings.Get("LDAPDomainName");

            if (UsernameTextBox.Text == string.Empty)
            {
                lblMsgSave.Text = "Username mismatch";
            }
            else if (PasswordTextBox.Text == string.Empty)
            {
                lblMsgSave.Text = "Password mismatch";
            }
            else
            {
                LoginBLL objLoginBLL = null;

                try
                {
                    if (IsLDAPAuthenticated(LDAPDomainName, UsernameTextBox.Text.Trim(), PasswordTextBox.Text.Trim()))
                    {
                        string inputUsername = "";
                        string inputPassword = "";

                        inputUsername = UsernameTextBox.Text.Trim();
                        inputPassword = PasswordTextBox.Text.Trim();

                        objLoginBLL = new LoginBLL();

                        LoginBO objLogin = objLoginBLL.Authentication(inputUsername, inputPassword);

                        if (objLogin != null)
                        {
                            if (inputUsername.ToLower() == objLogin.USERNAME.ToLower())
                            {
                                if (chkStaySignedIn.Checked)
                                {
                                    // Remember the username so that, it is populated every time the login page is opened, till the check box is unchecked
                                    // before sign-in.
                                    System.Web.HttpCookie usernameCookie = new System.Web.HttpCookie("WIS_USER");
                                    usernameCookie.Value = objLogin.USERNAME;
                                    usernameCookie.Expires = DateTime.MaxValue;
                                    Response.Cookies.Add(usernameCookie);
                                }
                                else
                                {
                                    // Clear the stored Username, as user opted out of Stay Sign In.
                                    System.Web.HttpCookie usernameCookie = new System.Web.HttpCookie("WIS_USER");
                                    if (usernameCookie != null)
                                    {
                                        usernameCookie.Expires = DateTime.Now.AddYears(-1);
                                        Response.Cookies.Add(usernameCookie);
                                    }
                                }

                                Session["userName"] = objLogin.DisplayName;
                                Session["USER_ID"] = objLogin.UserID;

                                Response.Redirect("Default.aspx");
                            }
                            else
                            {
                                lblMsgSave.Text = "Please check your username and password";
                            }
                        }
                        else
                        {
                            lblMsgSave.Text = "Please check your username and password";
                        }
                    }
                    else
                    {
                        lblMsgSave.Text = "Please check your username and password";
                    }
                }
                catch (Exception ee)
                {
                    lblMsgSave.Text = "Unable to connect to the application. Please contact the administrator.";
                }
                finally
                {
                    objLoginBLL = null;
                }
            }
        }

        /// <summary>
        /// Check user Credintials are valid or not
        /// </summary>
        private bool IsLDAPAuthenticated(string domainName, string usrID, string pwd)
        {
            if (domainName == "UETCL")
            {
                return true;
            }
            else
            {
                // return WIS_Utility.Utility.IsValidUser(domainName, usrID, pwd);
                string LDAPPath = "LDAP://" + domainName + "/OU=All Users,DC=uetcl,DC=com";
                Boolean found = false;
                DirectoryEntry searchRoot = new DirectoryEntry(LDAPPath, usrID, pwd);
                DirectorySearcher DirectorySearcherObject = new DirectorySearcher(searchRoot);
                SearchResult SearchResultObject = DirectorySearcherObject.FindOne();

                try
                {
                    if (SearchResultObject != null)
                    {
                        string Email = SearchResultObject.Properties["mail"].ToString();
                        return found = true;
                    }
                    else
                    {
                        return found;
                    }
                }
                catch (Exception)
                {
                    return found;
                }
            }

            
        }
    }
}