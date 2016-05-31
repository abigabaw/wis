using System;

namespace WIS
{
    public partial class CompSocioEconomyMenu : System.Web.UI.UserControl
    {
        public enum MenuValue
        {
            HouseholdDetails,
            HouseholdRelations,
            PAPInfo,
            Livelihood,
            Health,
            Neighbours,
            Welfare,
            MajorShocks,
            Concern,
            OtherLandHoldings,
            OnAffectedLand,
            OffAffectedLand,
            AcreageValuation
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private MenuValue menuIndex;
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