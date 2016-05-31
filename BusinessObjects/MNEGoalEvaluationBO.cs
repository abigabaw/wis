using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class MNEGoalEvaluationBO
    {
        private int evaluationid = -1;
        private int projectid = -1;
        private int goalid = -1;
        private string goalname = String.Empty;
        private string goaldescription = String.Empty;
        private string goalnarrative = String.Empty;
        private string isdeleted = String.Empty;
        private int createdby = -1;
        private int updatedby = -1;


        public string Goalname
        {
            get { return goalname; }
            set { goalname = value; }
        }

        public int EvaluationID
        {
            get { return evaluationid; }
            set { evaluationid = value; }
        }

        public int ProjectID
        {
            get { return projectid; }
            set { projectid = value; }
        }

        public int GoalID
        {
            get { return goalid; }
            set { goalid = value; }
        }

        public string GoalDescription
        {
            get { return goaldescription; }
            set { goaldescription = value; }
        }
        public string GoalNarrative
        {
            get { return goalnarrative; }
            set { goalnarrative = value; }
        }
        public string IsDeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

        public int CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }

        public int UpdatedBy
        {
            get { return updatedby; }
            set { updatedby = value; }
        }
    }
}
