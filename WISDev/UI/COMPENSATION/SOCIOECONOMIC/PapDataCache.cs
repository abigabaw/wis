using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.IO;

namespace WIS
{
    public class PapDataCache: Page
    {
        public void ClearCache()
        {
            Cache.Remove(BuildCacheKey("HOUSEHOLD_ID"));
            Cache.Remove(BuildCacheKey("UID"));
            Cache.Remove(BuildCacheKey("PAPNAME"));
            Cache.Remove(BuildCacheKey("PLOTREFERENCE"));
            Cache.Remove(BuildCacheKey("PAPDESIGNATION"));
            Cache.Remove(BuildCacheKey("Plotlatitude"));
            Cache.Remove(BuildCacheKey("Plotlongitude"));
        }

        public string BuildCacheKey(string keyName)
        {
            return keyName + "_" + Session["USER_ID"].ToString();
        }

        private void CacheHouseholdID(string householdID)
        {
            Cache.Insert(BuildCacheKey("HOUSEHOLD_ID"), householdID, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
        }

        public void CachePAPData(string householdID)
        {
            string papUID = String.Empty;
            string papName = String.Empty;
            string plotReference = String.Empty;
            string papDesignation = "";
            string lati = "";
            string longi = "";

            Session["HH_ID_Disp"] = householdID;
            PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHouseHoldData(Convert.ToInt32(householdID));

            if (objHousehold != null)
            {
                CacheHouseholdID(objHousehold.HhId.ToString());
                if (objHousehold.Paptype.ToUpper() == "INS")
                    papName = objHousehold.InstitutionName;
                else
                    papName = objHousehold.PapName;

                Cache.Insert(BuildCacheKey("PAPNAME"), papName, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));

                papUID = objHousehold.Pap_UId;
                Cache.Insert(BuildCacheKey("UID"), papUID, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));

                plotReference = objHousehold.PlotReference;
                Cache.Insert(BuildCacheKey("PLOTREFERENCE"), plotReference, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));

                if (objHousehold.Designation != null)
                {
                    papDesignation = objHousehold.Designation;
                    Cache.Insert(BuildCacheKey("PAPDESIGNATION"), papDesignation, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
                }
                if (objHousehold.Plotlatitude != null)
                {
                    lati = objHousehold.Plotlatitude;
                    Cache.Insert(BuildCacheKey("Plotlatitude"), lati, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
                }
                if (objHousehold.Plotlongitude != null)
                {
                    longi = objHousehold.Plotlongitude;
                    Cache.Insert(BuildCacheKey("Plotlongitude"), longi, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
                }

                //if (objHousehold.PapstatusId > 0)
                //{
                //    PstatusBLL objPstatusBLL = new PstatusBLL();
                //    PstatusBO objPstatus = objPstatusBLL.GetPstatusById(objHousehold.PapstatusId);

                //    if (objPstatus != null)
                //        papDesignation = objPstatus.PAPDESIGNATION1;                 
                //}

                //Cache.Insert(BuildCacheKey("PAPDESIGNATION"), papDesignation, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
            }

            objHousehold = null;
            objHouseholdLogic = null;
        }
    }
}