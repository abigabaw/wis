using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ViewMasterCopy : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ReportType = CheckIndex((int)menuIndex);
                lnkMasterCopy.Attributes.Add("OnClick", "OpenReport('" + ReportType + "')");
            }
        }

        private string CheckIndex(int Value)
        {
            string rpttype = "";
            switch (Value.ToString())
            {
                case "0":
                    rpttype = "CMPHOU";
                    break;

                case "1":
                    rpttype = "CMPINST";
                    break;

                case "2":
                    rpttype = "CMPGRP";
                    break;

                case "3":
                    rpttype = "CMPGRPM";
                    break;

                case "4":
                    rpttype = "CMPSER";
                    break;

                case "5":
                    rpttype = "CMPHHT";
                    break;

                case "6":
                    rpttype = "CMPALU";
                    break;

                case "7":
                    rpttype = "CMPSTA";
                    break;

                case "8":
                    rpttype = "CMPLHD";
                    break;

                case "9":
                    rpttype = "CMPDDT";
                    break;

                case "10":
                    rpttype = "CMPHCD";
                    break;

                case "11":
                    rpttype = "CMPNEI";
                    break;

                case "12":
                    rpttype = "CMPSHOCK";
                    break;

                case "13":
                    rpttype = "CMPINDW";
                    break;

                case "14":
                    rpttype = "CMPWELF";
                    break;

                case "15":
                    rpttype = "CMPCON";
                    break;

                case "16":
                    rpttype = "CMPOTHLAND";
                    break;

                case "17":
                    rpttype = "CMPMEMCLA";
                    break;

                case "18":
                    rpttype = "CMPLOAL";
                    break;

                case "19":
                    rpttype = "CMPLOFF";
                    break;

                case "20":
                    rpttype = "CMPACVAL";
                    break;

                case "21":
                    rpttype = "CMPLNDINFOPUB";
                    break;

                case "22":
                    rpttype = "CMPPERM";
                    break;

                case "23":
                    rpttype = "CMPNONPER";
                    break;

                case "24":
                    rpttype = "CMPDMACR";
                    break;

                case "25":
                    rpttype = "CMPCROP";
                    break;

                case "26":
                    rpttype = "CMPGRAVE";
                    break;

                case "27":
                    rpttype = "CMPFENCE";
                    break;

                case "28":
                    rpttype = "CMPCULPRO";
                    break;

                case "29":
                    rpttype = "CMPFINVAL";
                    break;

                case "30":
                    rpttype = "CMPLNDINFOPRIV";
                    break;

                case "31":
                    rpttype = "CMPOTHER";
                    break;
            }
            return rpttype;
        }

        private MenuValue menuIndex;

        public MenuValue HighlightMenu
        {
            set
            {
                menuIndex = value;
            }
        }

        public enum MenuValue
        {
            HouseholdDetails,
            Institution,
            GroupOwnerShip,
            GroupMembersDetails,
            ServicesonAffectedPlot,
            HouseholderDetails,
            AffectedLandusersontheAffectedPlotofLand,
            StakeholderDetails,
            LivelihoodDetails,
            DisabilityDetails,
            HealthCareDetails,
            NeighbourDetails,
            ShockDetails,
            GeneralWelfareIndicatorsfromGovernmentsurvey,
            WelfareDetails,
            ConcernDetails,
            OtherLandHoldings,
            MemberClaimsandEasements,
            LivingonAffectedLand,
            LivingoffAffectedLand,
            AffectedAcreageValuation,
            LandInfo,
            PermanentBuilding,
            NonPermanentStructureDetails,
            DamagedCropDetails,
            CropDetails,
            Grave,
            Fence,
            CultureProperty,
            FinalValuation,
            LandInfoPriv,
            OtherFixtures
        }

        protected void lnkMasterCopy_Click(object sender, EventArgs e)
        {
        }
    }
}