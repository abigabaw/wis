using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class OptionGroupBLL
    {
        /// <summary>
        /// To Insert Option Groups
        /// </summary>
        /// <param name="objOptionGroupBO"></param>
        /// <returns></returns>
        public string InsertOptionGroups(OptionGroupBO objOptionGroupBO)
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL(); //Data pass -to Database Layer

            try
            {
                return objOptionGroupDAL.InsertOptionGroups(objOptionGroupBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objOptionGroupDAL = null;
            }
        }

        /// <summary>
        /// To Get Option Group
        /// </summary>
        /// <returns></returns>
        public OptionGroupList GetOptionGroup()
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL();
            return objOptionGroupDAL.GetOptionGroup();
        }

        /// <summary>
        /// To Get Option Group By Id
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <returns></returns>
        public OptionGroupBO GetOptionGroupById(int OptionGroupID)
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL();
            return objOptionGroupDAL.GetOptionGroupById(OptionGroupID);
        }

        /// <summary>
        /// To Update Option Groups
        /// </summary>
        /// <param name="objOptionGroupBO"></param>
        /// <returns></returns>
        public string UpdateOptionGroups(OptionGroupBO objOptionGroupBO)
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL(); //Data pass -to Database Layer

            try
            {
                return objOptionGroupDAL.UpdateOptionGroups(objOptionGroupBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                objOptionGroupDAL = null;
            }
        }

        /// <summary>
        /// To Delete Option Group
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <returns></returns>
        public string DeleteOptionGroup(int OptionGroupID)
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL();
            return objOptionGroupDAL.DeleteOptionGroup(OptionGroupID);
        }

        /// <summary>
        /// To Obsolete Option Group
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteOptionGroup(int OptionGroupID, string IsDeleted)
        {
            OptionGroupDAL objOptionGroupDAL = new OptionGroupDAL();
            return objOptionGroupDAL.ObsoleteOptionGroup(OptionGroupID, IsDeleted);
        }
    }
}
