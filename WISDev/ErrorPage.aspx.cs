using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Error occurred"; 
            string PreviousUri = Request["aspxerrorpath"]; 
            
            if (!string.IsNullOrEmpty(PreviousUri)) 
            {
                pnlError.Visible = true; 
                hlinkPreviousPage.NavigateUrl = PreviousUri;
                lnkHomePage.NavigateUrl = ResolveUrl("~/Default.aspx");
            } 
        }
    }
}