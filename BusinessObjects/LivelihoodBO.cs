using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LivelihoodBO
    {
       private int ITEMID;
       private string Itemname = string.Empty;
       private string isdeleted;
       private int createdby;
       private DateTime createddate;

       public DateTime Createddate
       {
           get { return createddate; }
           set { createddate = value; }
       }

       public int Createdby
       {
           get { return createdby; }
           set { createdby = value; }
       }


       public string Isdeleted
       {
           get { return isdeleted; }
           set { isdeleted = value; }
       }
       public string ITEMNAME
       {
           get { return Itemname; }
           set { Itemname = value; }
       }

       public int Itemid
       {
           get { return ITEMID; }
           set { ITEMID = value; }
       }
     
    }
}
