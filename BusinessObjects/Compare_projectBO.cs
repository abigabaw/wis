using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
   public class Compare_projectBO
    {
        private int projectID = -1;
        private string projectName = string.Empty;
        private decimal totalestBudget = 0;
        private string compairID = string.Empty;
        private int option1 = -1;
        private int option2 = -1;
        private int option3 = -1;
        private int option4 = -1;
        private int option5 = -1;

        public int Option5
        {
            get { return option5; }
            set { option5 = value; }
        }

        public int Option4
        {
            get { return option4; }
            set { option4 = value; }
        }

        public int Option3
        {
            get { return option3; }
            set { option3 = value; }
        }

        public int Option2
        {
            get { return option2; }
            set { option2 = value; }
        }

        public int Option1
        {
            get { return option1; }
            set { option1 = value; }
        }

        public string CompairID
        {
            get { return compairID; }
            set { compairID = value; }
        }

        public decimal TotalestBudget
        {
            get { return totalestBudget; }
            set { totalestBudget = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
    }
}
