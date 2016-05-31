using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class GOUAllowanceMaster : System.Web.UI.Page
    {
        GOUAllowanceBO GOUAllowanceBOobj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "GOU Allowance";
                ViewState["GOUALLOWANCECATEGORYID"] = 0;
                //BindGrid();
            }
        }
    }
}