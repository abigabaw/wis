﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PAPLiveliwoodAfter
    {
        private int livelihoodItemID = -1;
        private int houseHoldID = -1;
        private decimal cash = -1;
        private string inKind = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";
        private DateTime cAPTUREDDATE;
        private int id = 0;
        public string ITEMNAME { set; get; }

        public int LID
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime CAPTUREDDATE
        {
            get { return cAPTUREDDATE; }
            set { cAPTUREDDATE = value; }
        }

        public int LivelihoodItemID
        {
            get
            {
                return livelihoodItemID;
            }
            set
            {
                livelihoodItemID = value;
            }
        }

        public int HouseHoldID
        {
            get
            {
                return houseHoldID;
            }
            set
            {
                houseHoldID = value;
            }
        }

        public decimal Cash
        {
            get
            {
                return cash;
            }
            set
            {
                cash = value;
            }
        }

        public string InKind
        {
            get
            {
                return inKind;
            }
            set
            {
                inKind = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }
    }
   
}
