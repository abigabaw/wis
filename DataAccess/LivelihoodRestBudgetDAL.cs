using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class LivelihoodRestBudgetDAL
    {
        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        #region GetData
      
        /// <summary>
        /// get all data
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public LivelihoodRestBudgetList GetLiveRestBudget(int projectID)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_LIV_RES_BUDG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("projectid_", projectID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_LIV_RES_BUDG_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_res_budgid_", LivRestBudgID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_INS_LIV_RES_BUDG", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("Liv_Bud_CategID_", oLivelihoodRestBudgetBO.Liv_Bud_CategID);
                oCmd.Parameters.Add("Liv_Bud_ItemID_", oLivelihoodRestBudgetBO.Liv_Bud_ItemID);

                oCmd.Parameters.Add("ImplCost_", oLivelihoodRestBudgetBO.ImplCost);
                oCmd.Parameters.Add("OperCost_", oLivelihoodRestBudgetBO.OperCost);
                oCmd.Parameters.Add("ExternalMonitory_", oLivelihoodRestBudgetBO.ExternalMonitory);

                oCmd.Parameters.Add("NoOfBeneficial_", oLivelihoodRestBudgetBO.NoOfBeneficial);
                oCmd.Parameters.Add("ItemQuantity_", oLivelihoodRestBudgetBO.ItemQuantity);
                oCmd.Parameters.Add("CostPerUnit_", oLivelihoodRestBudgetBO.CostPerUnit);
                oCmd.Parameters.Add("TotalAmount_", oLivelihoodRestBudgetBO.TotalAmount);
                oCmd.Parameters.Add("Comments_", oLivelihoodRestBudgetBO.Comments);

                oCmd.Parameters.Add("District_", oLivelihoodRestBudgetBO.District);
                oCmd.Parameters.Add("County_", oLivelihoodRestBudgetBO.County);
                oCmd.Parameters.Add("SubCounty_", oLivelihoodRestBudgetBO.SubCounty);
                oCmd.Parameters.Add("Parish_", oLivelihoodRestBudgetBO.Parish);
                oCmd.Parameters.Add("projectid_", oLivelihoodRestBudgetBO.ProjectID);
                oCmd.Parameters.Add("isdeleted_", oLivelihoodRestBudgetBO.IsDeleted);
                oCmd.Parameters.Add("createdby_", oLivelihoodRestBudgetBO.CreatedBy);

                oCmd.Parameters.Add("liv_res_budgIDD_", OracleDbType.Long, 50).Direction = ParameterDirection.Output;
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                //oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();
                //OracleDataReader oDataReader = oCmd.ExecuteReader();

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
            cnn = new OracleConnection(con);

            proc = "USP_TRN_DEL_LIV_RES_BUDG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_res_budgid_", LiveResBudgId);
            //cmd.Parameters.Add("Sp_recordset", OracleDbType.Int32).Direction = ParameterDirection.Output;

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
            cnn = new OracleConnection(con);

            proc = "USP_TRN_UPD_LIV_RES_BUDG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("Liv_Res_BudgID_", oLivelihoodRestBudgetBO.Liv_Res_BudgID);
            cmd.Parameters.Add("Liv_Bud_CategID_", oLivelihoodRestBudgetBO.Liv_Bud_CategID);
            cmd.Parameters.Add("Liv_Bud_ItemID_", oLivelihoodRestBudgetBO.Liv_Bud_ItemID);

            cmd.Parameters.Add("ImplCost_", oLivelihoodRestBudgetBO.ImplCost);
            cmd.Parameters.Add("OperCost_", oLivelihoodRestBudgetBO.OperCost);
            cmd.Parameters.Add("ExternalMonitory_", oLivelihoodRestBudgetBO.ExternalMonitory);

            cmd.Parameters.Add("NoOfBeneficial_", oLivelihoodRestBudgetBO.NoOfBeneficial);
            cmd.Parameters.Add("ItemQuantity_", oLivelihoodRestBudgetBO.ItemQuantity);
            cmd.Parameters.Add("CostPerUnit_", oLivelihoodRestBudgetBO.CostPerUnit);
            cmd.Parameters.Add("TotalAmount_", oLivelihoodRestBudgetBO.TotalAmount);
            cmd.Parameters.Add("Comments_", oLivelihoodRestBudgetBO.Comments);

            cmd.Parameters.Add("District_", oLivelihoodRestBudgetBO.District);
            cmd.Parameters.Add("County_", oLivelihoodRestBudgetBO.County);
            cmd.Parameters.Add("SubCounty_", oLivelihoodRestBudgetBO.SubCounty);
            cmd.Parameters.Add("Parish_", oLivelihoodRestBudgetBO.Parish);

            cmd.Parameters.Add("IsDeleted_", oLivelihoodRestBudgetBO.IsDeleted);
            cmd.Parameters.Add("UpdatedBy_", oLivelihoodRestBudgetBO.UpdatedBy);

            cmd.Parameters.Add("liv_res_budgIDD_", OracleDbType.Long,50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           // cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            //cmd.ExecuteNonQuery();
            ////OracleDataReader oDataReader = cmd.ExecuteReader();

            //if (cmd.Parameters["liv_res_budgIDD_"].Value != null)
            //    resultArray[0] = cmd.Parameters["liv_res_budgIDD_"].Value.ToString();
            //else
            //    resultArray[0] = string.Empty;

            //if (cmd.Parameters["errorMessage_"].Value != null)
            //    resultArray[1] = cmd.Parameters["errorMessage_"].Value.ToString();
            //else
            //    resultArray[1] = string.Empty;

            //cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
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