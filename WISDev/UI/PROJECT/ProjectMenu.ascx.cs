using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ProjectMenu : System.Web.UI.UserControl
    { 
        public enum MenuValue
        {
            ProjectDetails,
            RouteInfo,
            GeographicalInfo,
            FinancierInfo,
            Budget,
            Consultant,
            Personnel,
            PAP,
            Workflow,
            MAXCAP_DIS
        }
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string mode;
        /// <summary>
        /// To set mode
        /// </summary>
        public string Mode
        {
            set
            {
                mode = value.ToUpper();

                if (mode == "NEW")
                {
                    foreach (MenuItem itm in NavigationSubMenu.Items)
                    {
                        itm.Enabled = false;
                    }

                    NavigationSubMenu.Items[0].Enabled = true;
                }
            }
        }

        private MenuValue menuIndex;
        /// <summary>
        /// To set menu value
        /// </summary>
        public MenuValue HighlightMenu
        {
            set
            {
                menuIndex = value;

                if (NavigationSubMenu.Items[(int)menuIndex] != null)
                    NavigationSubMenu.Items[(int)menuIndex].Selected = true;
            }
        }
    }
}