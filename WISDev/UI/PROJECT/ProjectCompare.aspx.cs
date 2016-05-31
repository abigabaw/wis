using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ProjectCompare : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Compare Projects";
                Getprojectnames();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.COMPARE_PROJECT) == false)
                {
                    btnCompare.Visible = false;
                    btnReset.Visible = false;
                }
             }
        }
        /// <summary>
        /// To fetch details and assign to listitem
        /// </summary>
        private void Getprojectnames()
        {
            string PROJECTID = string.Empty;
            
            DataSet Ds = new DataSet();
            //Compare_projectList list = new Compare_projectList();            

            Compare_ProjectBLL Compare_ProjectBLLobj = new Compare_ProjectBLL();
            //list = Compare_ProjectBLLobj.Getprojectname(PROJECTID);
            ProjectList list = (new ProjectBLL()).GetProjects("", "", "", "", Convert.ToInt32(Session["USER_ID"]));
         
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    LstProjects.Items.Add(new ListItem(list[i].ProjectName.ToString(), list[i].ProjectID.ToString()));
                }
                
                // Sort the list contents
                List<ListItem> t = new List<ListItem>();
                Comparison<ListItem> compare = new Comparison<ListItem>(CompareListItems);

                foreach (ListItem lbItem in LstProjects.Items)
                    t.Add(lbItem);

                t.Sort(compare);
                LstProjects.Items.Clear();
                LstProjects.Items.AddRange(t.ToArray());
            }
            catch (Exception ee)
            {
                msgsaveLabel.Text = ee.Message.ToString();
            }
            finally
            {
                Compare_ProjectBLLobj = null;                
            }
        }
        /// <summary>
        /// method to compare listitems
        /// </summary>
        /// <param name="li1"></param>
        /// <param name="li2"></param>
        /// <returns></returns>
        private int CompareListItems(ListItem li1, ListItem li2)
        {
            return String.Compare(li1.Text, li2.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LstProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MoveListBoxItems(LstProjectcomp, LstProjects);
        }
        /// <summary>
        /// method to move listitems from one listbox to another
        /// </summary>
        /// <param name="LstProjects"></param>
        /// <param name="LstProjectcomp"></param>
        private void MoveListBoxItems(ListBox LstProjects, ListBox LstProjectcomp)
        {
            for (int i = LstProjects.Items.Count - 1; i >= 0; i--)
            {
                if (LstProjects.Items[i].Selected)
                {
                    
                    LstProjectcomp.Items.Add(new ListItem(LstProjects.SelectedItem.Text, LstProjects.SelectedItem.Value));
                    
                    LstProjects.Items.Remove(LstProjects.SelectedItem);
                    
                }
               
             
            }
        }
        /// <summary>
        /// calls MoveListBoxItems method and to anotherlistbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(LstProjects, LstProjectcomp);

            // Sort the list contents
            List<ListItem> t = new List<ListItem>();
            Comparison<ListItem> compare = new Comparison<ListItem>(CompareListItems);

            foreach (ListItem lbItem in LstProjectcomp.Items)
                t.Add(lbItem);

            t.Sort(compare);
            LstProjectcomp.Items.Clear();
            LstProjectcomp.Items.AddRange(t.ToArray());
        }
        /// <summary>
        /// calls MoveListBoxItems method and removes from  anotherlistbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_remove_Click(object sender, EventArgs e)
        {
            MoveListBoxItems(LstProjectcomp, LstProjects);

            // Sort the list contents
            List<ListItem> t = new List<ListItem>();
            Comparison<ListItem> compare = new Comparison<ListItem>(CompareListItems);

            foreach (ListItem lbItem in LstProjects.Items)
                t.Add(lbItem);

            t.Sort(compare);
            LstProjects.Items.Clear();
            LstProjects.Items.AddRange(t.ToArray());
        }
        /// <summary>
        /// To compare items in both the listboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompare_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder projectsSB = new System.Text.StringBuilder();

            Compare_ProjectBLL Compare_ProjectBLLobj = new Compare_ProjectBLL();
            Compare_projectBO Compare_projectBOObj = new Compare_projectBO();
            foreach (ListItem lstItem in LstProjectcomp.Items)
            {                
                projectsSB.Append(lstItem.Value + ",");
            }

            if (projectsSB.Length > 0)
            {
                projectsSB = projectsSB.Remove(projectsSB.Length - 1, 1);
                Compare_projectBOObj.CompairID = projectsSB.ToString();
            }

            grdcompareprjt.DataSource = Compare_ProjectBLLobj.Getdata(Compare_projectBOObj);
            grdcompareprjt.DataBind();
            //tblProjectHeader.Visible = true;
        }
        /// <summary>
        /// Used to load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            LstProjectcomp.Items.Clear();
            LstProjects.Items.Clear();
            Getprojectnames();
            grdcompareprjt.DataSource = null;
            grdcompareprjt.DataBind();
            //tblProjectHeader.Visible = false;
        }
        /// <summary>
        /// calls MoveListBoxItems method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LstProjectcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveListBoxItems(LstProjectcomp,LstProjects);
        }

      
    }
}