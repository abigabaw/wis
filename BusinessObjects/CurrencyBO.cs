using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CurrencyBO
    {

        private int currencyID = -1;
        private string currencyCode = String.Empty;

        public int CurrencyID
        {
            get
            {
                return currencyID;
            }
            set
            {
                currencyID = value;
            }
        }

        public string CurrencyCode
        {
            get
            {
                return currencyCode;
            }
            set
            {
                currencyCode = value;
            }
        }
    }
}
