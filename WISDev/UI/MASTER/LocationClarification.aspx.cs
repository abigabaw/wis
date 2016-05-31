using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

/**
 * 
 * @version		  Location UI code 
 * @package		  
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Mahalakshmi
 * @Created Date  07-10-2013
 * @Updated By
 * @Updated Date
 *  
 */

namespace WIS.UI.MASTER
{
    public partial class LocationClarification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Location";

            }

        }
    }
}