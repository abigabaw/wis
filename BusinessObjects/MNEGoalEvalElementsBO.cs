using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class MNEGoalEvalElementsBO
    {
        private int evaluationid = -1;
        private int evalelementid = -1;
        private int goal_elementid = -1;
        private string goal_elementname = String.Empty;
        private string evalelementdescriptionn = String.Empty;
        private string isdeleted = String.Empty;
        private int createdby = -1;
        private int updatedby = -1;

        public int EvaluationID
        {
            get { return evaluationid; }
            set { evaluationid = value; }
        }

        public int EvalelementID
        {
            get { return evalelementid; }
            set { evalelementid = value; }
        }

        public int Goal_elementID
        {
            get { return goal_elementid; }
            set { goal_elementid = value; }
        }

        public string Goal_elementname
        {
            get { return goal_elementname; }
            set { goal_elementname = value; }
        }

        public string Evalelementdescriptionn
        {
            get { return evalelementdescriptionn; }
            set { evalelementdescriptionn = value; }
        }

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }

        public int Updatedby
        {
            get { return updatedby; }
            set { updatedby = value; }
        }
    }
}
