using System;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.DirectoryServices;
using System.Security.Cryptography;

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

                        if (objLogin.USERNAME != "NONE")
                        {
                            // if (inputUsername.ToLower() == objLogin.USERNAME.ToLower())
                            
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
                            // else { lblMsgSave.Text = "Wrong Credentials"; }
                        }
                        else
                        {
                            lblMsgSave.Text = "Access Denied / Wrong Credentials. Contact WIS Admin";
                        }
                    }

                    // else { lblMsgSave.Text = "Wrong Credentials. Contact the Administrator"; } 

                }
                catch (Exception ee)
                {
                    lblMsgSave.Text = "Error Occurred. Contact WIS Admin";
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
            //Edwin: Old Logic ... if (domainName == "UETCL") { return true; }
            
            //Edwin: 22/01/2018 - Global Password Things
            LoginBLL objLoginBLL = null;
            objLoginBLL = new LoginBLL();
            LoginBO objLogin = objLoginBLL.DBAuth(usrID, pwd);

            if (GetMD5(pwd) == objLogin.PASSWORD)
            {
                Boolean found = false;
                return found = true;
            }
            //End: Edwin
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

        //Edwin: 22/01/2018 - Encrypt User Provide Pass for Comparison
        private string GetMD5(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);


            // step 2, convert byte array to hex string

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                
                sb.Append(hash[i].ToString("x2"));

            }

            return sb.ToString();

        } //End: Edwin
    }
}