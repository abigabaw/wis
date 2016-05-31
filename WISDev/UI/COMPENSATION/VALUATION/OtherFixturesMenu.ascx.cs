using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class OtherFixturesMenu : System.Web.UI.UserControl
    {
        public enum MenuValue
        {
            Grave,
            Fence,
            OtherFixture
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
        /// <summary>
        /// To display menu
        /// </summary>
        public MenuValue HighlightMenu
        {
            set
            {
                if (NavigationFixturesSubMenu.Items[(int)value] != null)
                    NavigationFixturesSubMenu.Items[(int)value].Selected = true;
            }
        }
    }
}