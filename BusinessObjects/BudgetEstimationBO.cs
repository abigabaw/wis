using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class BudgetEstimationBO
    {
        private int userID = -1;
        private int projectID;
        private int categoryID; //MODULEID
        private string categoryName = String.Empty; //MODULENAME
        private int subcategoryID; //MODULEID
        private string subcategoryName = String.Empty; //MODULENAME
        private string valueAmount;
        private string valueAmountper;
        private int budgetEstimationID;
        private string currencyCode = String.Empty;
        private int currencyID;
        private string accountNo = String.Empty;

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public int CurrencyID
        {
            get { return currencyID; }
            set { currencyID = value; }
        }
        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }
        public int BudgetEstimationID
        {
            get { return budgetEstimationID; }
            set { budgetEstimationID = value; }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public int ProjectID{
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
    }
        public int CategoryID
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }
        public int SubCategoryID
        {
            get
            {
                return subcategoryID;
            }
            set
            {
                subcategoryID = value;
            }
        }

        public string SubCategoryName
        {
            get
            {
                return subcategoryName;
            }
            set
            {
                subcategoryName = value;
            }
        }
         public string ValueAmount
        {
            get
            {
                return valueAmount;
            }
            set
            {
                valueAmount = value;
            }
        }
         public string ValueAmountper
         {
             get
             {
                 return valueAmountper;
             }
             set
             {
                 valueAmountper = value;
             }
         }
    }
}