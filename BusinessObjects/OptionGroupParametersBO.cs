using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
  public  class OptionGroupParametersBO
    {
        string optiongrpname;

        public string Optiongrpname
        {
            get { return optiongrpname; }
            set { optiongrpname = value; }
        }
        string optiongrpstatusname;

        public string Optiongrpstatusname
        {
            get { return optiongrpstatusname; }
            set { optiongrpstatusname = value; }
        }
        int optionGrpID;
        int paramID;

        public int ParamID
        {
            get { return paramID; }
            set { paramID = value; }
        }
        public int OptionGrpID
        {
            get { return optionGrpID; }
            set { optionGrpID = value; }
        }
        int optionstatusID;

        public int OptionstatusID
        {
            get { return optionstatusID; }
            set { optionstatusID = value; }
        }
        string isResident;

        public string IsResident
        {
            get { return isResident; }
            set { isResident = value; }
        }
        string landCompensation;

        public string LandCompensation
        {
            get { return landCompensation; }
            set { landCompensation = value; }
        }
        string houseCompensation;

        public string HouseCompensation
        {
            get { return houseCompensation; }
            set { houseCompensation = value; }
        }
        string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }
        int createdby;

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }
        DateTime dreatedDate;

        public DateTime DreatedDate
        {
            get { return dreatedDate; }
            set { dreatedDate = value; }
        }
        int updatedBy;

        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        DateTime updatedDate;

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
    }
}
