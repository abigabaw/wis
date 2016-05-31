using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
  public  class DescriptionBO
    {
     
        string optionAvailablename;

        public string OptionAvailablename
        {
            get { return optionAvailablename; }
            set { optionAvailablename = value; }
        }
        int optionID;

        public int OptionID
        {
            get { return optionID; }
            set { optionID = value; }
        }
        string parameterName;

        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }
        int descriptionID;

        public int DescriptionID
        {
            get { return descriptionID; }
            set { descriptionID = value; }
        }
        int parameterID;

        public int ParameterID
        {
            get { return parameterID; }
            set { parameterID = value; }
        }
        int optionAvailID;

        public int OptionAvailID
        {
            get { return optionAvailID; }
            set { optionAvailID = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }
        int createdBy;

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        int updatedBy;

        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
    }
}
