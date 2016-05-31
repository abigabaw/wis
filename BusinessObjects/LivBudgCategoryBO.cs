using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class LivBudgCategoryBO
    {
       public int lIV_BUD_CATEGID { get; set; }
       public string LIV_BUD_CATEGORYNAME { get; set; }
       public string ISDELETED { get; set; }
       public int CREATEDBY { get; set; }
       public DateTime CREATEDDATE { get; set; }
       public int UPDATEDBY { get; set; }
       public DateTime UPDATEDDATE { get; set; }
    }
}
