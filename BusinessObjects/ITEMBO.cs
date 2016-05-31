using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ITEMBO
    {
        private int itemID = -1;
        private string itemName = String.Empty;
        private int itemcatId = -1;
        private string itemsubcatName = String.Empty;
        private int itemsubcatId = -1;


        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        public int ItemcatId
        {
            get
            {
                return itemcatId;
            }
            set
            {
                itemcatId = value;
            }
        }

        public string ItemsubcatName
        {
            get
            {
                return itemsubcatName;
            }
            set
            {
                itemsubcatName = value;
            }
        }

        public int ItemsubcatId
        {
            get
            {
                return itemsubcatId;
            }
            set
            {
                itemsubcatId = value;
            }
        }
    }
}