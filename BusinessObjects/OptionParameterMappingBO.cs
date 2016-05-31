using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public  class OptionParameterMappingBO
   {
       public Int32 OptParID { get; set; }

       public Int32 DescriptionID { get; set; }

       public Int32 ParameterID { get; set; }

       public Int32 OptionAvailableID { get; set; } //OPTIONAVAILABLEID

       public Int32 OptionGroupID { get; set; } //OPTIONGROUPID

       public String ParameterName { get; set; }

       public String Description { get; set; }

       public String OptionGroup { get; set; }

       public String OptionAvailable { get; set; }

       public Int32 ID { get; set; }

       public String Name { get; set; }

       #region Common Fields
       public String IsDeleted { get; set; }

       public String CreatedDate { get; set; }

       public Int32 CreatedBy { get; set; }

       public String UpdatedDate { get; set; }

       public Int32 UpdatedBy { get; set; }
       #endregion
   }

   public class OptionParameterMappingList : List<OptionParameterMappingBO>
   {
       public OptionParameterMappingList() { }
   }
}
