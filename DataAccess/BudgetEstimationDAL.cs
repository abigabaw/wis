using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTALLCATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_SUBCATBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CategoryID_", CategoryID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PROJ_BGT_EST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            string returnResult = string.Empty;
            try
            {
                dcmd.Parameters.AddWithValue("CategoryID_", objBudgetEstimation.CategoryID);
                dcmd.Parameters.AddWithValue("SubCategoryID_", objBudgetEstimation.SubCategoryID);
                dcmd.Parameters.AddWithValue("ValueAmount_", objBudgetEstimation.ValueAmount);
                dcmd.Parameters.AddWithValue("ValueAmountper_", objBudgetEstimation.ValueAmountper);
                dcmd.Parameters.AddWithValue("ProjectID_", objBudgetEstimation.ProjectID);
                dcmd.Parameters.AddWithValue("UserID_", objBudgetEstimation.UserID);
                dcmd.Parameters.AddWithValue("CurrencyID_", objBudgetEstimation.CurrencyID);
                dcmd.Parameters.AddWithValue("AccountNo_", objBudgetEstimation.AccountNo);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_BGT_EST_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CategoryName_", objNEWCategory.CategoryName);
                dcmd.Parameters.AddWithValue("UserID_", objNEWCategory.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_BGT_EST_SUBCATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("SUBCategoryName_", objNEWsubCategory.SubCategoryName);
                dcmd.Parameters.AddWithValue("CategoryID_", objNEWsubCategory.CategoryID);
                dcmd.Parameters.AddWithValue("UserID_", objNEWsubCategory.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_PRJ_BGT_EST";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PROJECTID_", pID);
            cmd.Parameters.AddWithValue("CATEGORYID_", categoryID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_BGT_EST_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("BudgetEstimationID_", BudgetEstimationID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEBGTEST";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BudgetEstimationID_", BudgetEstimationID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPDATE_PROJ_BGT_EST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            string returnResult = string.Empty;
            try
            {
                dcmd.Parameters.AddWithValue("BudgetEstimationID_", objBudgetEstimation.BudgetEstimationID);
                dcmd.Parameters.AddWithValue("CategoryID_", objBudgetEstimation.CategoryID);
                dcmd.Parameters.AddWithValue("SubCategoryID_", objBudgetEstimation.SubCategoryID);
                dcmd.Parameters.AddWithValue("ValueAmount_", objBudgetEstimation.ValueAmount);
                dcmd.Parameters.AddWithValue("ValueAmountper_", objBudgetEstimation.ValueAmountper);
                dcmd.Parameters.AddWithValue("ProjectID_", objBudgetEstimation.ProjectID);
                dcmd.Parameters.AddWithValue("UserID_", objBudgetEstimation.UserID);
                dcmd.Parameters.AddWithValue("CurrencyID_", objBudgetEstimation.CurrencyID);
                dcmd.Parameters.AddWithValue("AccountNo_", objBudgetEstimation.AccountNo);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCURRBUDEST";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("projectID_", projectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
