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
    public partial class HouseholdSummary : System.Web.UI.UserControl
    {
        /// <summary>
        /// Call GetHouseholdSummary to get papa data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetHouseholdSummary();
            }
            string currentUrl = Request.Url.AbsoluteUri.ToString();
            string[] url = currentUrl.Split('/');
            if (url.Length > 0)
            {
                string Path = "";
                for (int i = 0; i < url.Length - 1; i++)
                {
                    Path += "/" + url[i].ToString();
                }
                if (Path.Length > 0)
                    Path = Path.Remove(0, 1);
                Path = Path + "/PopUpPAPList.aspx";
                if (Path.Contains("Compensation/PopUpPAPList.aspx"))
                {
                    hfCheckPath.Value = "Yes";
                }
                else
                {
                    hfCheckPath.Value = "No";
                }
            }
        }
        /// <summary>
        /// Retun cache name
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private string BuildCacheKey(string keyName)
        {
            return keyName + "_" + Session["USER_ID"].ToString(); 
        }
        /// <summary>
        ///  Get Papa Data
        /// </summary>
        private void GetHouseholdSummary()
        {
            string householdID = String.Empty;
            string papUID = String.Empty;
            string papName = String.Empty;
            string plotReference = String.Empty;
            string papDesignation = String.Empty;
            string lati = String.Empty;
            string longi = String.Empty;

            if (Cache[BuildCacheKey("HOUSEHOLD_ID")] != null && Session["HH_ID_Disp"] != null)
            {
                householdID = Cache[BuildCacheKey("HOUSEHOLD_ID")].ToString();

                if (Session["HH_ID_Disp"].ToString() != Session["HH_ID"].ToString())
                {
                    householdID = Session["HH_ID"].ToString();
                    ClearCache();
                    //CacheHouseholdID(householdID);
                    CachePAPData(householdID);
                }

                if (Cache[BuildCacheKey("HOUSEHOLD_ID")] != null)
                    householdID = Cache[BuildCacheKey("HOUSEHOLD_ID")].ToString();

                if (Cache[BuildCacheKey("UID")] != null)
                    papUID = Cache[BuildCacheKey("UID")].ToString();

                if (Cache[BuildCacheKey("PAPNAME")] != null)
                    papName = Cache[BuildCacheKey("PAPNAME")].ToString();

                if (Cache[BuildCacheKey("PLOTREFERENCE")] != null)
                    plotReference = Cache[BuildCacheKey("PLOTREFERENCE")].ToString();

                if (Cache[BuildCacheKey("PAPDESIGNATION")] != null)
                    papDesignation = Cache[BuildCacheKey("PAPDESIGNATION")].ToString();

                if (Cache[BuildCacheKey("Plotlatitude")] != null)
                    lati = Cache[BuildCacheKey("Plotlatitude")].ToString();

                if (Cache[BuildCacheKey("Plotlongitude")] != null)
                    longi = Cache[BuildCacheKey("Plotlongitude")].ToString();
            }
            else if (Session["HH_ID"] != null)
            {
                householdID = Session["HH_ID"].ToString();

                ClearCache();
                //CacheHouseholdID(householdID);
                CachePAPData(householdID);

                if (Cache[BuildCacheKey("HOUSEHOLD_ID")] != null)
                    householdID = Cache[BuildCacheKey("HOUSEHOLD_ID")].ToString();

                if (Cache[BuildCacheKey("UID")] != null)
                    papUID = Cache[BuildCacheKey("UID")].ToString();

                if (Cache[BuildCacheKey("PAPNAME")] != null)
                    papName = Cache[BuildCacheKey("PAPNAME")].ToString();

                if (Cache[BuildCacheKey("PLOTREFERENCE")] != null)
                    plotReference = Cache[BuildCacheKey("PLOTREFERENCE")].ToString();

                if (Cache[BuildCacheKey("PAPDESIGNATION")] != null)
                    papDesignation = Cache[BuildCacheKey("PAPDESIGNATION")].ToString();

                if (Cache[BuildCacheKey("Plotlatitude")] != null)
                    lati = Cache[BuildCacheKey("Plotlatitude")].ToString();

                if (Cache[BuildCacheKey("Plotlongitude")] != null)
                    longi = Cache[BuildCacheKey("Plotlongitude")].ToString();
            }            

            litSmrHouseholdID.Text = householdID;
            litPAPUID.Text = papUID;
            litSmrPAPName.Text = papName;
            litSmrPlotReference.Text = plotReference;
            hfHHID.Value = Session["HH_ID"].ToString();

            litSmrDesignation.Text = papDesignation;
        }
        /// <summary>
        /// Inser Cache
        /// </summary>
        /// <param name="householdID"></param>
        private void CacheHouseholdID(string householdID)
        {
            Cache.Insert(BuildCacheKey("HOUSEHOLD_ID"), householdID, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
        }
        /// <summary>
        /// Store Pap data into Cache
        /// </summary>
        /// <param name="householdID"></param>
        private void CachePAPData(string householdID)
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
        /// <summary>
        /// Clear Cache
        /// </summary>
        private void ClearCache()
        {
            Cache.Remove(BuildCacheKey("HOUSEHOLD_ID"));
            Cache.Remove(BuildCacheKey("UID"));
            Cache.Remove(BuildCacheKey("PAPNAME"));
            Cache.Remove(BuildCacheKey("PLOTREFERENCE"));
            Cache.Remove(BuildCacheKey("PAPDESIGNATION"));
            Cache.Remove(BuildCacheKey("Plotlatitude"));
            Cache.Remove(BuildCacheKey("Plotlongitude"));
        }
        /// <summary>
        /// link for open PAp list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkPapList_Click(object sender, EventArgs e)
        {
            string currentUrl = Request.Url.AbsoluteUri.ToString();
            string[] url = currentUrl.Split('/');
            if (url.Length > 0)
            {
                string Path = "";
                for (int i = 0; i < url.Length-1; i++)
                {
                    Path += "/" + url[i].ToString();
                }
                if (Path.Length > 0)
                    Path = Path.Remove(0, 1);
                Path = Path + "/PopUpPAPList.aspx";
                if (Path.Contains("Compensation/PopUpPAPList.aspx"))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../../Compensation/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
            }
        }
        /// <summary>
        /// to open pop up pap list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            string currentUrl = Request.Url.AbsoluteUri.ToString();
            string[] url = currentUrl.Split('/');
            if (url.Length > 0)
            {
                string Path = "";
                for (int i = 0; i < url.Length - 1; i++)
                {
                    Path += "/" + url[i].ToString();
                }
                if (Path.Length > 0)
                    Path = Path.Remove(0, 1);
                Path = Path + "/PopUpPAPList.aspx";
                if (Path.Contains("Compensation/PopUpPAPList.aspx"))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../../Compensation/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
            }
        }
        ////
        //private void CacheExample() { 
        //    string cacheKey = "myKey"; string data = ""; 
        //    // Check to see if the data is in the cache already 
        //    if(Cache[cacheKey]==null) { 
        //        // Get the data since it isn't in the cache 
        //        data = System.DateTime.Now.ToString(); 
        //        //create an instance of the callback delegate 
        //        System.Web.Caching.CacheItemRemovedCallback callBack = new System.Web.Caching.CacheItemRemovedCallback(onRemove); 
        //        Label1.Text = "Generated: " + data; Cache.Insert(cacheKey,data,null, System.DateTime.Now.AddSeconds(5), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, callBack); 
        //    } 
        //    else { 
        //        Label1.Text = "Cached: " + Cache[cacheKey].ToString(); 
        //    } 
        //} 
        
        //private void onRemove(string key, object val, CacheItemRemovedReason reason) { 
        //    //create an instance of the callback delegate 
        //    CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove); 
        //    Cache.Insert(key,val.ToString() + "*",null,System.DateTime.Now.AddSeconds(5),Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, callBack); 
        //}
    }
}