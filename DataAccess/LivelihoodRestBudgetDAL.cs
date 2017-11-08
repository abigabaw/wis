using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class LivelihoodRestBudgetDAL
    {
        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        #region GetData
      
        /// <summary>
        /// get all data
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public LivelihoodRestBudgetList GetLiveRestBudget(int projectID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_LIV_RES_BUDG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("projectid_", projectID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodRestBudgetBO objLivelihoodRestBudgetBO = null;
            LivelihoodRestBudgetList oLivelihoodRestBudgetList = new LivelihoodRestBudgetList();

            while (dr.Read())
            {
                objLivelihoodRestBudgetBO = new LivelihoodRestBudgetBO();

                objLivelihoodRestBudgetBO = MapData(dr);

                oLivelihoodRestBudgetList.Add(objLivelihoodRestBudgetBO);
            }

            dr.Close();

            return oLivelihoodRestBudgetList;
        }
        /// <summary>
        /// get all data by ID
        /// </summary>
        /// <param name="LivRestBudgID"></param>
        /// <returns></returns>
        public LivelihoodRestBudgetBO GetLiveRestBudgetById(int LivRestBudgID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_LIV_RES_BUDG_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("liv_res_budgid_", LivRestBudgID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            LivelihoodRestBudgetBO objLivelihoodRestBudgetBO = null;
            LivelihoodRestBudgetList oLivelihoodRestBudgetList = new LivelihoodRestBudgetList();

            objLivelihoodRestBudgetBO = new LivelihoodRestBudgetBO();

            while (dr.Read())
            {
                objLivelihoodRestBudgetBO = MapData(dr);
            }
            dr.Close();

            return objLivelihoodRestBudgetBO;
        }

       
        /// <summary>
        /// to check the Column are Exists or no
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
        /// MapData
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private LivelihoodRestBudgetBO MapData(IDataReader reader)
        {
            LivelihoodRestBudgetBO oLivelihoodRestBudgetBO = new LivelihoodRestBudgetBO();

            if (ColumnExists(reader, "Liv_Res_BudgID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Res_BudgID")))
                oLivelihoodRestBudgetBO.Liv_Res_BudgID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Res_BudgID")));

            if (ColumnExists(reader, "Liv_Bud_CategID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_CategID")))
                oLivelihoodRestBudgetBO.Liv_Bud_CategID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Bud_CategID")));

            if (ColumnExists(reader, "Liv_Bud_ItemID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_ItemID")))
                oLivelihoodRestBudgetBO.Liv_Bud_ItemID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Bud_ItemID")));

            if (ColumnExists(reader, "Liv_Bud_CategoryName") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_CategoryName")))
                oLivelihoodRestBudgetBO.Liv_Bud_CategoryName = reader.GetString(reader.GetOrdinal("Liv_Bud_CategoryName"));

            if (ColumnExists(reader, "Liv_Bud_ItemName") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_ItemName")))
                oLivelihoodRestBudgetBO.Liv_Bud_ItemName = reader.GetString(reader.GetOrdinal("Liv_Bud_ItemName"));


            if (ColumnExists(reader, "NoOfBeneficial") && !reader.IsDBNull(reader.GetOrdinal("NoOfBeneficial")))
                oLivelihoodRestBudgetBO.NoOfBeneficial = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("NoOfBeneficial")));

            if (ColumnExists(reader, "Comments") && !reader.IsDBNull(reader.GetOrdinal("Comments")))
                oLivelihoodRestBudgetBO.Comments = reader.GetString(reader.GetOrdinal("Comments"));

            if (ColumnExists(reader, "ItemQuantity") && !reader.IsDBNull(reader.GetOrdinal("ItemQuantity")))
                oLivelihoodRestBudgetBO.ItemQuantity = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ItemQuantity")));

            if (ColumnExists(reader, "TotalAmount") && !reader.IsDBNull(reader.GetOrdinal("TotalAmount")))
                oLivelihoodRestBudgetBO.TotalAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("TotalAmount")));

            if (ColumnExists(reader, "CostPerUnit") && !reader.IsDBNull(reader.GetOrdinal("CostPerUnit")))
                oLivelihoodRestBudgetBO.CostPerUnit = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CostPerUnit")));


            if (ColumnExists(reader, "ImplCost") && !reader.IsDBNull(reader.GetOrdinal("ImplCost")))
                oLivelihoodRestBudgetBO.ImplCost = reader.GetString(reader.GetOrdinal("ImplCost"));

            if (ColumnExists(reader, "OperCost") && !reader.IsDBNull(reader.GetOrdinal("OperCost")))
                oLivelihoodRestBudgetBO.OperCost = reader.GetString(reader.GetOrdinal("OperCost"));

            if (ColumnExists(reader, "ExternalMonitory") && !reader.IsDBNull(reader.GetOrdinal("ExternalMonitory")))
                oLivelihoodRestBudgetBO.ExternalMonitory = reader.GetString(reader.GetOrdinal("ExternalMonitory"));


            if (ColumnExists(reader, "County") && !reader.IsDBNull(reader.GetOrdinal("County")))
                oLivelihoodRestBudgetBO.County = reader.GetString(reader.GetOrdinal("County"));

            if (ColumnExists(reader, "SubCounty") && !reader.IsDBNull(reader.GetOrdinal("SubCounty")))
                oLivelihoodRestBudgetBO.SubCounty = reader.GetString(reader.GetOrdinal("SubCounty"));

            if (ColumnExists(reader, "District") && !reader.IsDBNull(reader.GetOrdinal("District")))
                oLivelihoodRestBudgetBO.District = reader.GetString(reader.GetOrdinal("District"));

            if (ColumnExists(reader, "Parish") && !reader.IsDBNull(reader.GetOrdinal("Parish")))
                oLivelihoodRestBudgetBO.Parish = reader.GetString(reader.GetOrdinal("Parish"));

            return oLivelihoodRestBudgetBO;
        }
        #endregion

       
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="oLivelihoodRestBudgetBO"></param>
        /// <returns></returns>
        public string[] AddLiveRestBudget(LivelihoodRestBudgetBO oLivelihoodRestBudgetBO)
        {
            //string returnResult = string.Empty;
            string[] resultArray = new string[2];
            LivelihoodRestBudgetBO ooLivelihoodRestBudgetBO = new LivelihoodRestBudgetBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_INS_LIV_RES_BUDG", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("Liv_Bud_CategID_", oLivelihoodRestBudgetBO.Liv_Bud_CategID);
                oCmd.Parameters.AddWithValue("Liv_Bud_ItemID_", oLivelihoodRestBudgetBO.Liv_Bud_ItemID);

                oCmd.Parameters.AddWithValue("ImplCost_", oLivelihoodRestBudgetBO.ImplCost);
                oCmd.Parameters.AddWithValue("OperCost_", oLivelihoodRestBudgetBO.OperCost);
                oCmd.Parameters.AddWithValue("ExternalMonitory_", oLivelihoodRestBudgetBO.ExternalMonitory);

                oCmd.Parameters.AddWithValue("NoOfBeneficial_", oLivelihoodRestBudgetBO.NoOfBeneficial);
                oCmd.Parameters.AddWithValue("ItemQuantity_", oLivelihoodRestBudgetBO.ItemQuantity);
                oCmd.Parameters.AddWithValue("CostPerUnit_", oLivelihoodRestBudgetBO.CostPerUnit);
                oCmd.Parameters.AddWithValue("TotalAmount_", oLivelihoodRestBudgetBO.TotalAmount);
                oCmd.Parameters.AddWithValue("Comments_", oLivelihoodRestBudgetBO.Comments);

                oCmd.Parameters.AddWithValue("District_", oLivelihoodRestBudgetBO.District);
                oCmd.Parameters.AddWithValue("County_", oLivelihoodRestBudgetBO.County);
                oCmd.Parameters.AddWithValue("SubCounty_", oLivelihoodRestBudgetBO.SubCounty);
                oCmd.Parameters.AddWithValue("Parish_", oLivelihoodRestBudgetBO.Parish);
                oCmd.Parameters.AddWithValue("projectid_", oLivelihoodRestBudgetBO.ProjectID);
                oCmd.Parameters.AddWithValue("isdeleted_", oLivelihoodRestBudgetBO.IsDeleted);
                oCmd.Parameters.AddWithValue("createdby_", oLivelihoodRestBudgetBO.CreatedBy);

                oCmd.Parameters.AddWithValue("liv_res_budgIDD_", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                oCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();
                //SqlDataReader oDataReader = oCmd.ExecuteReader();

                if (oCmd.Parameters["liv_res_budgIDD_"].Value != null)
                    resultArray[0] = oCmd.Parameters["liv_res_budgIDD_"].Value.ToString();
                else
                    resultArray[0] = string.Empty;

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    resultArray[1] = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    resultArray[1] = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return resultArray;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="LiveResBudgId"></param>
        /// <returns></returns>
        public int DeleteLiveRestBudget(int LiveResBudgId)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_LIV_RES_BUDG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("liv_res_budgid_", LiveResBudgId);
            //cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="oLivelihoodRestBudgetBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestBudget(LivelihoodRestBudgetBO oLivelihoodRestBudgetBO)
        {
            //string returnResult = string.Empty;
            string[] resultArray = new string[2];
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_LIV_RES_BUDG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("Liv_Res_BudgID_", oLivelihoodRestBudgetBO.Liv_Res_BudgID);
            cmd.Parameters.AddWithValue("Liv_Bud_CategID_", oLivelihoodRestBudgetBO.Liv_Bud_CategID);
            cmd.Parameters.AddWithValue("Liv_Bud_ItemID_", oLivelihoodRestBudgetBO.Liv_Bud_ItemID);

            cmd.Parameters.AddWithValue("ImplCost_", oLivelihoodRestBudgetBO.ImplCost);
            cmd.Parameters.AddWithValue("OperCost_", oLivelihoodRestBudgetBO.OperCost);
            cmd.Parameters.AddWithValue("ExternalMonitory_", oLivelihoodRestBudgetBO.ExternalMonitory);

            cmd.Parameters.AddWithValue("NoOfBeneficial_", oLivelihoodRestBudgetBO.NoOfBeneficial);
            cmd.Parameters.AddWithValue("ItemQuantity_", oLivelihoodRestBudgetBO.ItemQuantity);
            cmd.Parameters.AddWithValue("CostPerUnit_", oLivelihoodRestBudgetBO.CostPerUnit);
            cmd.Parameters.AddWithValue("TotalAmount_", oLivelihoodRestBudgetBO.TotalAmount);
            cmd.Parameters.AddWithValue("Comments_", oLivelihoodRestBudgetBO.Comments);

            cmd.Parameters.AddWithValue("District_", oLivelihoodRestBudgetBO.District);
            cmd.Parameters.AddWithValue("County_", oLivelihoodRestBudgetBO.County);
            cmd.Parameters.AddWithValue("SubCounty_", oLivelihoodRestBudgetBO.SubCounty);
            cmd.Parameters.AddWithValue("Parish_", oLivelihoodRestBudgetBO.Parish);

            cmd.Parameters.AddWithValue("IsDeleted_", oLivelihoodRestBudgetBO.IsDeleted);
            cmd.Parameters.AddWithValue("UpdatedBy_", oLivelihoodRestBudgetBO.UpdatedBy);

            cmd.Parameters.AddWithValue("liv_res_budgIDD_", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
           // //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            //cmd.ExecuteNonQuery();
            ////SqlDataReader oDataReader = cmd.ExecuteReader();

            //if (cmd.Parameters["liv_res_budgIDD_"].Value != null)
            //    resultArray[0] = cmd.Parameters["liv_res_budgIDD_"].Value.ToString();
            //else
            //    resultArray[0] = string.Empty;

            //if (cmd.Parameters["errorMessage_"].Value != null)
            //    resultArray[1] = cmd.Parameters["errorMessage_"].Value.ToString();
            //else
            //    resultArray[1] = string.Empty;

            //cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["liv_res_budgIDD_"].Value != null)
                    resultArray[0] = cmd.Parameters["liv_res_budgIDD_"].Value.ToString();
                else
                    resultArray[0] = string.Empty;

                if (cmd.Parameters["errorMessage_"].Value != null)
                    resultArray[1] = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    resultArray[1] = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultArray[1];
        }
     

       
    }
}