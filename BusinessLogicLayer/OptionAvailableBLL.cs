using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class OptionAvailableBLL
    {
        public OptionAvailableList GetAllOptionAvail()
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL();
            return Objoptionavail.GetAllOptionAvail();
        }

        // serach the data from the Database Mst_Concern
        public OptionAvailableList GetConcern()
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL();
            return Objoptionavail.GetAllOptionAvail();
        }
        //Insert the data into Database
        public string Insert(OptionAvailableBO objoptionbo)
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL(); ; //Data pass -to Database Layer

            try
            {
                return Objoptionavail.Insert(objoptionbo);
            }
            catch
            {
                throw;
            }
            finally
            {
                Objoptionavail = null;
            }
        }
        //Search the Singal Data by passing ID
        public OptionAvailableBO GetAllOptionById(int optionID)
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL();
            return Objoptionavail.GetAllOptionById(optionID);
        }
        //Edit the data
        public string editoptionavail(OptionAvailableBO objoptionbo)
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL(); //Data pass -to Database Layer

            try
            {
                return Objoptionavail.editoptionavail(objoptionbo);
            }
            catch
            {
                throw;
            }
            finally
            {
                Objoptionavail = null;
            }
        }
        //Delete the data
        public string Deleteoptionavail(int optionID)
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL();
            return Objoptionavail.Deleteoptionavail(optionID);
        }

        public string Obsoleteoptionavail(int optionID, string Isdeleted)
        {
            OptionAvailableDAL Objoptionavail = new OptionAvailableDAL();
            return Objoptionavail.Obsoleteoptionavail(optionID, Isdeleted);
        }
    }
}

