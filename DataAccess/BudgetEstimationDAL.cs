using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class BudgetEstimationDAL
    {
        /// <summary>
        /// To get All Category details from database
        /// </summary>
        /// <returns></returns>
        public BudgetEstimationList getAllCategory()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTALLCATEGORY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BudgetEstimationBO objBudgetEstimation = null;
            BudgetEstimationList BudgetEstimationList = new BudgetEstimationList();

            while (dr.Read())
            {
                objBudgetEstimation = new BudgetEstimationBO();
                objBudgetEstimation.CategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));
                objBudgetEstimation.CategoryName = dr.GetString(dr.GetOrdinal("BGT_CATEGORYNAME"));

                BudgetEstimationList.Add(objBudgetEstimation);
            }

            dr.Close();

            return BudgetEstimationList;
        }
        /// <summary>
        /// To get SubCategory ByCategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public BudgetEstimationList getSubCatByCatID(int CategoryID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_SUBCATBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CategoryID_", CategoryID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BudgetEstimationBO BudgetEstimationObj = null;
            BudgetEstimationList BudgetEstimationList = new BudgetEstimationList();

           
            while (dr.Read())
            {
                BudgetEstimationObj = new BudgetEstimationBO();

                if (ColumnExists(dr, "BGT_SUBCATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYNAME")))
                    BudgetEstimationObj.SubCategoryName = dr.GetString(dr.GetOrdinal("BGT_SUBCATEGORYNAME"));
                if (ColumnExists(dr, "BGT_SUBCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYID")))
                    BudgetEstimationObj.SubCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_SUBCATEGORYID")));
                if (ColumnExists(dr, "BGT_CATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYID")))
                    BudgetEstimationObj.CategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));

                BudgetEstimationList.Add(BudgetEstimationObj);

            }
            dr.Close();
            return BudgetEstimationList;
        }

   
        /// <summary>
        /// to check the Column are Exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// To Insert Budget Estimation details
        /// </summary>
        /// <param name="objBudgetEstimation"></param>
        /// <returns></returns>
        public string InsertBudgetEstimation(BudgetEstimationBO objBudgetEstimation)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_PROJ_BGT_EST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            string returnResult = string.Empty;
            try
            {
                dcmd.Parameters.Add("CategoryID_", objBudgetEstimation.CategoryID);
                dcmd.Parameters.Add("SubCategoryID_", objBudgetEstimation.SubCategoryID);
                dcmd.Parameters.Add("ValueAmount_", objBudgetEstimation.ValueAmount);
                dcmd.Parameters.Add("ValueAmountper_", objBudgetEstimation.ValueAmountper);
                dcmd.Parameters.Add("ProjectID_", objBudgetEstimation.ProjectID);
                dcmd.Parameters.Add("UserID_", objBudgetEstimation.UserID);
                dcmd.Parameters.Add("CurrencyID_", objBudgetEstimation.CurrencyID);
                dcmd.Parameters.Add("AccountNo_", objBudgetEstimation.AccountNo);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }
        /// <summary>
        /// To Insert NEW Category details
        /// </summary>
        /// <param name="objNEWCategory"></param>
        /// <returns></returns>
        public string InsertNEWCategory(BudgetEstimationBO objNEWCategory)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_BGT_EST_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CategoryName_", objNEWCategory.CategoryName);
                dcmd.Parameters.Add("UserID_", objNEWCategory.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }
        /// <summary>
        /// To Insert NEW sub Category details
        /// </summary>
        /// <param name="objNEWCategory"></param>
        /// <returns></returns>
        public string InsertNEWsubCategory(BudgetEstimationBO objNEWsubCategory)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_BGT_EST_SUBCATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("SUBCategoryName_", objNEWsubCategory.SubCategoryName);
                dcmd.Parameters.Add("CategoryID_", objNEWsubCategory.CategoryID);
                dcmd.Parameters.Add("UserID_", objNEWsubCategory.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }
        /// <summary>
        /// To Get Budget Estimation details
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public BudgetEstimationList GetBudgetEstimation(string pID, int categoryID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_PRJ_BGT_EST";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PROJECTID_", pID);
            cmd.Parameters.Add("CATEGORYID_", categoryID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BudgetEstimationBO BudgetEstimationObj = null;
            BudgetEstimationList BudgetEstimationList = new BudgetEstimationList();

           // BudgetEstimationObj = new BudgetEstimation();
            while (dr.Read())
            {
                BudgetEstimationObj = new BudgetEstimationBO();
                if (ColumnExists(dr, "BGT_ESTIMATIONID") && !dr.IsDBNull(dr.GetOrdinal("BGT_ESTIMATIONID")))
                    BudgetEstimationObj.BudgetEstimationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_ESTIMATIONID")));

                if (ColumnExists(dr, "BGT_CATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYNAME")))
                    BudgetEstimationObj.CategoryName = dr.GetString(dr.GetOrdinal("BGT_CATEGORYNAME"));
                if (ColumnExists(dr, "BGT_CATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYID")))
                    BudgetEstimationObj.CategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));

                if (ColumnExists(dr, "BGT_SUBCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYID")))
                    BudgetEstimationObj.SubCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_SUBCATEGORYID")));
                if (ColumnExists(dr, "BGT_SUBCATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYNAME")))
                    BudgetEstimationObj.SubCategoryName = dr.GetString(dr.GetOrdinal("BGT_SUBCATEGORYNAME"));

                if (ColumnExists(dr, "EST_VALUE") && !dr.IsDBNull(dr.GetOrdinal("EST_VALUE")))
                    BudgetEstimationObj.ValueAmount = Convert.ToString(dr.GetValue(dr.GetOrdinal("EST_VALUE")));

                if (ColumnExists(dr, "EST_PERCENTAGE") && !dr.IsDBNull(dr.GetOrdinal("EST_PERCENTAGE")))
                    BudgetEstimationObj.ValueAmountper = Convert.ToString(dr.GetValue(dr.GetOrdinal("EST_PERCENTAGE")));

                BudgetEstimationList.Add(BudgetEstimationObj);
            }
            dr.Close();
            return BudgetEstimationList;
        }
        /// <summary>
        /// To Get Budget Estimation details by ID
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public BudgetEstimationBO GetBudgetEstimationByID(int BudgetEstimationID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_BGT_EST_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("BudgetEstimationID_", BudgetEstimationID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BudgetEstimationBO BudgetEstimationBOObj = null;
            BudgetEstimationList BudgetEstimationList = new BudgetEstimationList();

            BudgetEstimationBOObj = new BudgetEstimationBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "BGT_ESTIMATIONID") && !dr.IsDBNull(dr.GetOrdinal("BGT_ESTIMATIONID")))
                    BudgetEstimationBOObj.BudgetEstimationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_ESTIMATIONID")));

                if (ColumnExists(dr, "BGT_SUBCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYID")))
                    BudgetEstimationBOObj.SubCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_SUBCATEGORYID")));

                if (ColumnExists(dr, "BGT_CATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYID")))
                    BudgetEstimationBOObj.CategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));

                if (ColumnExists(dr, "EST_VALUE") && !dr.IsDBNull(dr.GetOrdinal("EST_VALUE")))
                    BudgetEstimationBOObj.ValueAmount = Convert.ToString(dr.GetValue(dr.GetOrdinal("EST_VALUE")));

                if (ColumnExists(dr, "EST_PERCENTAGE") && !dr.IsDBNull(dr.GetOrdinal("EST_PERCENTAGE")))
                    BudgetEstimationBOObj.ValueAmountper = Convert.ToString(dr.GetValue(dr.GetOrdinal("EST_PERCENTAGE")));

                if (!dr.IsDBNull(dr.GetOrdinal("accountcode"))) BudgetEstimationBOObj.AccountNo = Convert.ToString(dr.GetValue(dr.GetOrdinal("accountcode")));
            }
            dr.Close();
            return BudgetEstimationBOObj;
        }
        /// <summary>
        /// To Delete Budget Estimation details
        /// </summary>
        /// <param name="BudgetEstimationID"></param>
        /// <returns></returns>
        public string DeleteBudgetEstimation(int BudgetEstimationID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEBGTEST";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("BudgetEstimationID_", BudgetEstimationID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return message;
        }
        /// <summary>
        /// To update Budget Estimation details
        /// </summary>
        /// <param name="objBudgetEstimation"></param>
        /// <returns></returns>
        public string EditBudgetEstimation(BudgetEstimationBO objBudgetEstimation)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPDATE_PROJ_BGT_EST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            string returnResult = string.Empty;
            try
            {
                dcmd.Parameters.Add("BudgetEstimationID_", objBudgetEstimation.BudgetEstimationID);
                dcmd.Parameters.Add("CategoryID_", objBudgetEstimation.CategoryID);
                dcmd.Parameters.Add("SubCategoryID_", objBudgetEstimation.SubCategoryID);
                dcmd.Parameters.Add("ValueAmount_", objBudgetEstimation.ValueAmount);
                dcmd.Parameters.Add("ValueAmountper_", objBudgetEstimation.ValueAmountper);
                dcmd.Parameters.Add("ProjectID_", objBudgetEstimation.ProjectID);
                dcmd.Parameters.Add("UserID_", objBudgetEstimation.UserID);
                dcmd.Parameters.Add("CurrencyID_", objBudgetEstimation.CurrencyID);
                dcmd.Parameters.Add("AccountNo_", objBudgetEstimation.AccountNo);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }

            return returnResult;
        }
        /// <summary>
        /// To get Currency From Project details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BudgetEstimationBO getCurrenceFromProject(string projectID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTCURRBUDEST";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("projectID_", projectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BudgetEstimationBO BudgetEstimationBOObj = null;
            BudgetEstimationList BudgetEstimationList = new BudgetEstimationList();

            BudgetEstimationBOObj = new BudgetEstimationBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CURRENCYID") && !dr.IsDBNull(dr.GetOrdinal("CURRENCYID")))
                    BudgetEstimationBOObj.CurrencyID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CURRENCYID")));

                if (ColumnExists(dr, "CURRENCYCODE") && !dr.IsDBNull(dr.GetOrdinal("CURRENCYCODE")))
                    BudgetEstimationBOObj.CurrencyCode = dr.GetString(dr.GetOrdinal(("CURRENCYCODE")));

                if (ColumnExists(dr, "PROJECTID") && !dr.IsDBNull(dr.GetOrdinal("PROJECTID")))
                    BudgetEstimationBOObj.ProjectID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

            }
            dr.Close();
            return BudgetEstimationBOObj;
        }
    }
}
