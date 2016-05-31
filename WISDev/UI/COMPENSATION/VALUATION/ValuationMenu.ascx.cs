using System;

namespace WIS
{
    public partial class ValuationMenu : System.Web.UI.UserControl
    {
        
        public enum MenuValue
        {
            PermanentBuildings,
            //NonPermanentBuildings,
            DamagedCrops,
            Crops,
            OtherFixtures,
            CultureProperties,
            AcreageValuation,
            FinalValuation
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

        private MenuValue menuIndex;
        
       
        /// <summary>
        ///Dropdown menu with data
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