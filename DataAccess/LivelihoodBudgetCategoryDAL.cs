﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class LivelihoodBudgetCategoryDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        string proc = string.Empty;

        #region GetData
        
        /// <summary>
        /// get all data
        /// </summary>
        /// <returns></returns>
        public LivelihoodBudgetCategoryList GetLiveBudgCategory()
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_MST_GET_LIV_BDG_CATEG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodBudgetCategoryBO oLiveBudgCategoryBO = null;
            LivelihoodBudgetCategoryList oLivBudgCategorylst = new LivelihoodBudgetCategoryList();

            while (dr.Read())
            {
                oLiveBudgCategoryBO = new LivelihoodBudgetCategoryBO();
                //objConcern.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));
                //objConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                //objConcern.ConcernIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                oLiveBudgCategoryBO = MapData(dr);

                oLivBudgCategorylst.Add(oLiveBudgCategoryBO);
            }

            dr.Close();

            return oLivBudgCategorylst;
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
        /// MapData
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private LivelihoodBudgetCategoryBO MapData(IDataReader reader)
        {
            LivelihoodBudgetCategoryBO oLiveBudgCategoryBO = new LivelihoodBudgetCategoryBO();

            if (ColumnExists(reader, "Liv_Bud_CategID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_CategID")))
                oLiveBudgCategoryBO.Liv_Bud_CategID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Bud_CategID")));

            if (ColumnExists(reader, "Liv_Bud_CategoryName") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_CategoryName")))
                oLiveBudgCategoryBO.Liv_Bud_CategoryName = reader.GetString(reader.GetOrdinal("Liv_Bud_CategoryName"));

            return oLiveBudgCategoryBO;
        }
        #endregion
/*
        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        public BatchBO AddBatch(BatchBO oBatchBO)
        {
            string returnResult = string.Empty;
            BatchBO ooBatchBO = new BatchBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_CMP_ADDBATCH", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("batchstatus_", oBatchBO.BatchStatus);
                oCmd.Parameters.Add("payt_requestdate_", oBatchBO.Payt_RequestDate);

                oCmd.Parameters.Add("hhid_", oBatchBO.HHID);
                oCmd.Parameters.Add("requeststatus_", oBatchBO.RequestStatus);

                oCmd.Parameters.Add("isdeleted_", oBatchBO.IsDeleted);
                oCmd.Parameters.Add("createdby_", oBatchBO.CreatedBy);

                oCmd.Parameters.Add("getBatchNo_", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                //oCmd.ExecuteNonQuery();
                OracleDataReader oDataReader = oCmd.ExecuteReader();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    ooBatchBO.dbMessage = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    ooBatchBO.dbMessage = string.Empty;

                if (oCmd.Parameters["getBatchNo_"].Value != null)
                    ooBatchBO.CMP_BatchNo = oCmd.Parameters["getBatchNo_"].Value.ToString();
                else
                    ooBatchBO.CMP_BatchNo = string.Empty;

                if (oDataReader != null)
                {
                    while (oDataReader.Read())
                    {
                        // ooBatchBO.dbMessage = oCmd.Parameters["errorMessage_"].Value.ToString();
                        //ooBatchBO.CMP_BatchNo = oCmd.Parameters["getBatchNo_"].Value.ToString();
                        //ooBatchBO.dbMessage = oDataReader["batchcreateddate"].ToString();
                        ooBatchBO.CMP_BatchNo = oDataReader["CMP_BATCHNO"].ToString();
                    }
                }
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
            return ooBatchBO;
        }

        public int DeletePaymentRequest(int PaymentRequestId)
        {
            cnn = new OracleConnection(con);

            proc = "USP_TRN_CMP_DEL_SUBMIT_PAYMENT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("payt_requestid_", PaymentRequestId);
            //cmd.Parameters.Add("Sp_recordset", OracleDbType.Int32).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }

        public string UpdatePaymentRequest(BatchBO oBatchBO)
        {
            string returnResult = string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_TRN_CMP_UPD_SUBMIT_PAYMENT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("payt_requestid_", oBatchBO.Payt_RequestID);
            cmd.Parameters.Add("requeststatus_", oBatchBO.RequestStatus);
            cmd.Parameters.Add("updatedby_", oBatchBO.UpdatedBy);           

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                returnResult = string.Empty;
                throw ex;
            }

            return returnResult;
        }
 * */
        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID

        /*  public Concern GetConcernById(int ConcernID)
          {
              OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
              OracleCommand cmd;

              string proc = "USP_MST_GETSELECTCONCERN";

              cmd = new OracleCommand(proc, cnn);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add("ConcernID_", ConcernID);
              cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

              cmd.Connection.Open();

              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              BO.Concern ConcernObj = null;
              BO.ConcernList Users = new BO.ConcernList();

              ConcernObj = new BO.Concern();
              while (dr.Read())
              {
                  if (ColumnExists(dr, "CONCERN") && !dr.IsDBNull(dr.GetOrdinal("CONCERN")))
                      ConcernObj.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                  if (ColumnExists(dr, "CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("CONCERNID")))
                      ConcernObj.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));

              }
              dr.Close();


              return ConcernObj;
          }*/

     
    }
}