using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
  public  class OptionParameterBO
  {

      private int parameterID = -1;
      private string parameterName = string.Empty;
      private string availableOptions = string.Empty;

      public string AvailableOptions
      {
          get { return availableOptions; }
          set { availableOptions = value; }
      }

      public int ParameterID
      {
          get
          {
              return parameterID;
          }
          set
          {
              parameterID = value;
          }
      }

      public int DistrictID { get; set; }

      public string ParameterName
      {
          get
          {
              return parameterName;
          }
          set
          {
              parameterName = value;
          }
      }

      public string IsDeleted { get; set; }

      public int CreatedBy { get; set; }

      public int UpdatedBy { get; set; }

      public int AvailableOptionsID { get; set; }
  }

  public class OptionParameterList : List<OptionParameterBO>
  {
      public OptionParameterList() { }
  }

}
