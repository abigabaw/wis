using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
   public class OptionParameterMappingBLL
    {
       public OptionParameterMappingList GetAllOptionParameterMapping()
        {
            //  OptionParameterMappingDAL s=new  OptionParameterMappingDAL();

            return (new OptionParameterMappingDAL()).GetAllOptionParameterMapping();
        }

        public String AddOptionParameterMapping(OptionParameterMappingBO pOptParmMappingBO)
        {
            return (new OptionParameterMappingDAL()).AddOptionParameterMapping(pOptParmMappingBO);
        }

        public String UpdateOptionParameterMapping(OptionParameterMappingBO pOptParmMappingBO)
        {
            return (new OptionParameterMappingDAL()).UpdateOptionParameterMapping(pOptParmMappingBO);
        }

        public String DeleteOptionParameterMapping(int pOptParameterId)
        {
            return (new OptionParameterMappingDAL()).DeleteOptionParameterMapping(pOptParameterId);
        }

        public String ObsoleteOptionalParameterMapping(Int32 pOptParameterId, String isDeleted, Int32 updatedBy)
        {
            return (new OptionParameterMappingDAL()).ObsoleteOptionalParameterMapping(pOptParameterId, isDeleted, updatedBy);
        }

        public OptionParameterMappingBO GetOptionalParameterMappingById(Int32 pOptParameterId)
        {
            return (new OptionParameterMappingDAL()).GetOptionalParameterMappingById(pOptParameterId);
        }

        public OptionParameterList GetOptionAvailable()
        {
            return (new OptionParameterMappingDAL()).GetOptionAvailable();
        }

        public OptionGroupList GetOptionGroup()
        {
            return (new OptionParameterMappingDAL()).GetOptionGroup();
        }

        public OptionGroupList GetOptionDescription(int Pid)
        {
            return (new OptionParameterMappingDAL()).GetOptionDescription(Pid);
        }

        public OptionParameterMappingList GetOptionParameters(int Pid)
        {
            return (new OptionParameterMappingDAL()).GetOptionParameters(Pid);
        }
    }

}
