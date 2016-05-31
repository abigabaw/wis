using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
  public  class OptionGroupParametersBLL
    {
        public string SaveOptionGrp(OptionGroupParametersBO objgrpparam)
        {
            return (new OptionGroupParametersDAL()).SaveOptionGroup(objgrpparam);
        }
        public OptionGroupList GetOptionGroup()
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL();
            return objOptionGroupDAL.GetOptionGroup();
        }
        public OptionGrpParamList getdatatogrid()
        {
            return (new OptionGroupParametersDAL()).getdatatogrid();
        }
        public OptionGroupParametersBO GetOptionalDetailsByID(int paramID)
        {
            return (new OptionGroupParametersDAL()).GetOptionalDetailsByID(paramID);
 
        }
        public string UpdateOptionGroup(OptionGroupParametersBO objOptionGroupParametersBO)
        {
            return (new OptionGroupParametersDAL()).UpdateOptionGroup(objOptionGroupParametersBO);
 
        }
        public string DeleteOptionGrp(int paramID)
        {
            return (new OptionGroupParametersDAL()).DeleteOptionGrp(paramID);
 
        }
    } 
}
