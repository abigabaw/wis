using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class DescriptionBLL
    {
        public string AddDescription(DescriptionBO objSubCountyBO)
        {
            return (new DescriptionDAL()).AddDescription(objSubCountyBO);
        }
        public DescriptionList GetParameters(int Pid)
        {
            return (new DescriptionDAL()).GetParameters(Pid);
        }
        public DescriptionList GetOptionAvail()
        {
            return (new DescriptionDAL()).GetOptionAvail();
        }
        public DescriptionList GetAllDescriptionDetails()
        {
            return (new DescriptionDAL()).GetAllDescriptionDetails();
        }
        public DescriptionBO GetAllDescriptionDetailsByID(int id)
        {
            return (new DescriptionDAL()).GetAllDescriptionDetailsByID(id);
        }
        public string UpdateDesription(DescriptionBO objDescription)
        {
            return (new DescriptionDAL()).UpdateDesription(objDescription);
 
        }
        public string DeleteDescription(int id)
        {
            return (new DescriptionDAL()).DeleteDescription(id);
 
        }
        public string ObsoleteDescription(int DesID, string ISDELETED, int UPDATEDBY)
        {
            return (new DescriptionDAL()).ObsoleteDescription(DesID, ISDELETED, UPDATEDBY);
 
        }

    }

}

