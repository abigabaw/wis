using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Collections;

namespace WIS
{
    public partial class LivelihoodRestorationView : System.Web.UI.Page
    {
        PAP_LivelihoodList LivelihoodItems = null;
        decimal TotalCashAmount = 0;
        /// <summary>
        /// Call BindGrid method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Livelihood";
                }
                ViewState["HHID"] = Request.QueryString["hhID"];
                GetLivelihoodItems();
                BindGrid();
            }
        }
        /// <summary>
        /// To Calculate total amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLivelihoodItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int itemID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Itemid"));
                Literal txtCash = (Literal)e.Row.FindControl("txtCash");
                Literal txtInKind = (Literal)e.Row.FindControl("txtInKind");

               // txtCash.Attributes.Add("onchange", "CalculateTotalCash(this);");

                foreach (PAP_LivelihoodBO objLivelihood in LivelihoodItems)
                {
                    if (objLivelihood.LivelihoodItemID == itemID)
                    {
                        if (objLivelihood.Cash > 0)
                        {
                            txtCash.Text = objLivelihood.Cash.ToString();
                            TotalCashAmount += objLivelihood.Cash;
                        }

                        txtInKind.Text = objLivelihood.InKind;
                        break;
                    }
                    else if (objLivelihood.Cash < 1)
                    {
                        txtCash.Text = "";
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (e.Row.FindControl("txtTotalCash") is Literal)
                    {
                        Literal txtTotalCash = e.Row.FindControl("txtTotalCash") as Literal;
                        txtTotalCash.Text = TotalCashAmount.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// To Get Livelihood Items 
        /// </summary>
        private void GetLivelihoodItems()
        {
            PAP_LivelihoodBLL objLivelihoodBLL = new PAP_LivelihoodBLL();
            LivelihoodItems = objLivelihoodBLL.GetLivelihoodItemsByID(Convert.ToInt32(ViewState["HHID"]));
        }
        /// <summary>
        /// Bind Data to Grid
        /// </summary>
        private void BindGrid()
        {
           // LivelihoodRestLocationBLL oLivelihoodRestLocationBLL = new LivelihoodRestLocationBLL();
           // LivelihoodRestLocationBO oLivelihoodRestLocationBO = new LivelihoodRestLocationBO();
           // LivelihoodBudgetCategoryList objLivelihoodBudgetCategoryList = new LivelihoodBudgetCategoryList();
           // oLivelihoodRestLocationBO = oLivelihoodRestLocationBLL.GetNewLocation(Convert.ToInt32(ViewState["HHID"]));
           // //  var result = ((IEnumerable)oLivelihoodRestLocationBO).Cast<object>().ToList();

           // // IList<LivelihoodRestLocationBO> objLivelihoodBudgetCategoryList = (IList<LivelihoodRestLocationBO>)oLivelihoodRestLocationBO;
           // List<object> list = new List<object>();
           // //if (oLivelihoodRestLocationBO is IEnumerable)
           // //{
           // //    var enumerator = ((IEnumerable)oLivelihoodRestLocationBO).GetEnumerator();
           // //    while (enumerator.MoveNext())
           // //    {
           // //        list.Add(enumerator.Current);
           // //    }
           // ////}
           // list.Add(oLivelihoodRestLocationBO);
          
           //// List<object> myAnythingList = (oLivelihoodRestLocationBO as IEnumerable<object>).Cast<object>().ToList();
                       
            grdLivelihoodItems.DataSource = (new LivelihoodBLL()).GetLivelihood();
            grdLivelihoodItems.DataBind();
            
        }


    }
}