using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ITEMDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region GetItem
        /// <summary>
        /// To Get Item
        /// </summary>
        /// <returns></returns>
        public ItemList GetItem()
        {
            proc = "USP_GET_MST_CDAP_CATEG";
            cnn = new OracleConnection(con);
            ITEMBO objITEMBO = null;
            ItemList lstItemList = new ItemList();
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objITEMBO = new ITEMBO();
                    objITEMBO.ItemcatId = dr.GetInt32(dr.GetOrdinal("ID"));
                    objITEMBO.ItemName = dr.GetString(dr.GetOrdinal("Name"));
                    lstItemList.Add(objITEMBO);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstItemList;
        }
        #endregion

        #region GetSubItem
        /// <summary>
        /// To Get Sub Item
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public ItemList GetSubItem(int CatID)
        {
            proc = "USP_GET_MST_CDAP_SUBCATEG";
            cnn = new OracleConnection(con);
            ITEMBO objITEMBO = null;
            ItemList lstItemList = new ItemList();
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CDAP_CATEGORYID_", CatID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objITEMBO = new ITEMBO();
                    objITEMBO.ItemsubcatId = dr.GetInt32(dr.GetOrdinal("ID"));
                    objITEMBO.ItemsubcatName = dr.GetString(dr.GetOrdinal("Name"));
                    lstItemList.Add(objITEMBO);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstItemList;
        }
        #endregion

        /// <summary>
        /// To Add CDAP Budget Master
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string AddCDAPBudgetMaster(CDAPBudgetMasterBO objCDAPBudgetMasterBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_INS_CDAP_CATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("CDAP_CATEGORYNAME_", objCDAPBudgetMasterBO.CategoryName);
            cmd.Parameters.Add("CREATEDBY_", objCDAPBudgetMasterBO.CreatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Update CDAP Budget Master
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string UpdateCDAPBudgetMaster(CDAPBudgetMasterBO objCDAPBudgetMasterBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_UPD_CDAP_CATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_CATEGORYID_", objCDAPBudgetMasterBO.CategoryID);
            cmd.Parameters.Add("CDAP_CATEGORYNAME_", objCDAPBudgetMasterBO.CategoryName);
            cmd.Parameters.Add("UPDATEDBY_", objCDAPBudgetMasterBO.UpdatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Delete CDAP Budget Master
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string DeleteCDAPBudgetMaster(int categoryID)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_DEL_CDAP_CATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_CATEGORYID_", categoryID);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Obsolete CDAP Budget Master
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteCDAPBudgetMaster(int categoryID, string isDeleted, int updatedBy)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_OBS_CDAP_CATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_CATEGORYID_", categoryID);
            cmd.Parameters.Add("UPDATEDBY_", updatedBy);
            cmd.Parameters.Add("ISDELETED_", isDeleted);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Get CDAP Budget Item By ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public CDAPBudgetMasterBO GetCDAPBudgetItemByID(int categoryID)
        {
            proc = "USP_GET_MST_CDAP_CATEGBYID";

            cnn = new OracleConnection(con);
            CDAPBudgetMasterBO objBudgetBO = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CDAP_CATEGORYID_", categoryID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objBudgetBO = new CDAPBudgetMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYID"))) objBudgetBO.CategoryID = dr.GetInt32(dr.GetOrdinal("CDAP_CATEGORYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYNAME"))) objBudgetBO.CategoryName = dr.GetString(dr.GetOrdinal("CDAP_CATEGORYNAME"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBudgetBO;
        }

        /// <summary>
        /// To Get All CDAP Budget Items
        /// </summary>
        /// <returns></returns>
        public List<CDAPBudgetMasterBO> GetAllCDAPBudgetItems()
        {
            proc = "USP_GET_ALL_MST_CDAP_CATEG";
            cnn = new OracleConnection(con);
            CDAPBudgetMasterBO objBudgetBO = null;
            List<CDAPBudgetMasterBO> CDAPBudgetMasterList = new List<CDAPBudgetMasterBO>();
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objBudgetBO = new CDAPBudgetMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYID"))) objBudgetBO.CategoryID = dr.GetInt32(dr.GetOrdinal("CDAP_CATEGORYID")); 
                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYNAME"))) objBudgetBO.CategoryName = dr.GetString(dr.GetOrdinal("CDAP_CATEGORYNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objBudgetBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                    CDAPBudgetMasterList.Add(objBudgetBO);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CDAPBudgetMasterList;
        }

        ////

        /// <summary>
        /// To Add CDAP Budget Sub Item
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string AddCDAPBudgetSubItem(CDAPBudgetDescrMasterBO objCDAPBudgetMasterBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_INS_CDAP_SUBCATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_CATEGORYID_", objCDAPBudgetMasterBO.CategoryID);
            cmd.Parameters.Add("CDAP_SUBCATEGORYNAME_", objCDAPBudgetMasterBO.SubCategoryName);
            cmd.Parameters.Add("CREATEDBY_", objCDAPBudgetMasterBO.CreatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Update CDAP Budget Sub Item
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string UpdateCDAPBudgetSubItem(CDAPBudgetDescrMasterBO objCDAPBudgetMasterBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_UPD_CDAP_SUBCATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_SUBCATEGORYID_", objCDAPBudgetMasterBO.SubCategoryID);
            cmd.Parameters.Add("CDAP_CATEGORYID_", objCDAPBudgetMasterBO.CategoryID);
            cmd.Parameters.Add("CDAP_SUBCATEGORYNAME_", objCDAPBudgetMasterBO.SubCategoryName);
            cmd.Parameters.Add("UPDATEDBY_", objCDAPBudgetMasterBO.UpdatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Delete CDAP Budget Sub Item
        /// </summary>
        /// <param name="subcategoryID"></param>
        /// <returns></returns>
        public string DeleteCDAPBudgetSubItem(int subcategoryID)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_DEL_CDAP_SUBCATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_SUBCATEGORYID_", subcategoryID);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Obsolete CDAP Budget Sub Item
        /// </summary>
        /// <param name="subcategoryID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteCDAPBudgetSubItem(int subcategoryID, string isDeleted, int updatedBy)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";

            proc = "USP_MST_OBS_CDAP_SUBCATEG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("CDAP_SUBCATEGORYID_", subcategoryID);
            cmd.Parameters.Add("UPDATEDBY_", updatedBy);
            cmd.Parameters.Add("ISDELETED_", isDeleted);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return returnResult;
        }

        /// <summary>
        /// To Get CDAP Budget Sub Item By ID
        /// </summary>
        /// <param name="subcategoryID"></param>
        /// <returns></returns>
        public CDAPBudgetDescrMasterBO GetCDAPBudgetSubItemByID(int subcategoryID)
        {
            proc = "USP_GET_MST_CDAP_SUBCATEGBYID";

            cnn = new OracleConnection(con);
            CDAPBudgetDescrMasterBO objBudgetBO = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CDAP_SUBCATEGORYID_", subcategoryID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objBudgetBO = new CDAPBudgetDescrMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYID"))) objBudgetBO.CategoryID = dr.GetInt32(dr.GetOrdinal("CDAP_SUBCATEGORYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"))) objBudgetBO.SubCategoryName = dr.GetString(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBudgetBO;
        }

        /// <summary>
        /// To Get All CDAP Budget Sub Items
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<CDAPBudgetDescrMasterBO> GetAllCDAPBudgetSubItems(int categoryID)
        {
            proc = "USP_GET_ALL_MST_CDAP_SUBCATEG";
            cnn = new OracleConnection(con);
            CDAPBudgetDescrMasterBO objBudgetBO = null;
            List<CDAPBudgetDescrMasterBO> CDAPBudgetDescrList = new List<CDAPBudgetDescrMasterBO>();
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CDAP_CATEGORYID_", categoryID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objBudgetBO = new CDAPBudgetDescrMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYID"))) objBudgetBO.SubCategoryID = dr.GetInt32(dr.GetOrdinal("CDAP_SUBCATEGORYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"))) objBudgetBO.SubCategoryName = dr.GetString(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objBudgetBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                    CDAPBudgetDescrList.Add(objBudgetBO);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CDAPBudgetDescrList;
        }
    }
}
