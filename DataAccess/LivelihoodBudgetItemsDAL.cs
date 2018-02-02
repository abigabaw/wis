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
    public class LivelihoodBudgetItemsDAL
    {
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        string proc = string.Empty;

        #region GetData
     
        /// <summary>
        /// get all data
        /// </summary>
        /// <param name="prmLiveBudgItemsBO"></param>
        /// <returns></returns>
        public LivelihoodBudgetItemsList GetLivBudgetItems_ById(LivelihoodBudgetItemsBO prmLiveBudgItemsBO)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_MST_GET_BDG_ITEM_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("liv_bud_categid_", prmLiveBudgItemsBO.Liv_Bud_CategID);
            cmd.Parameters.AddWithValue("liv_bud_itemid_", prmLiveBudgItemsBO.Liv_Bud_ItemID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodBudgetItemsBO oLivBudgItemsBO = null;
            LivelihoodBudgetItemsList lstLivBudgItem = new LivelihoodBudgetItemsList();

            while (dr.Read())
            {
                oLivBudgItemsBO = new LivelihoodBudgetItemsBO();
                //objConcern.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));
                //objConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                //objConcern.ConcernIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                oLivBudgItemsBO = MapData(dr);

                lstLivBudgItem.Add(oLivBudgItemsBO);
            }

            dr.Close();

            return lstLivBudgItem;
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
        private LivelihoodBudgetItemsBO MapData(IDataReader reader)
        {
            LivelihoodBudgetItemsBO oLiveBudgItemsBO = new LivelihoodBudgetItemsBO();

            if (ColumnExists(reader, "Liv_Bud_ItemID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_ItemID")))
                oLiveBudgItemsBO.Liv_Bud_ItemID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Bud_ItemID")));

            if (ColumnExists(reader, "Liv_Bud_CategID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_CategID")))
                oLiveBudgItemsBO.Liv_Bud_CategID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Bud_CategID")));

            if (ColumnExists(reader, "Liv_Bud_ItemName") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_ItemName")))
                oLiveBudgItemsBO.Liv_Bud_ItemName = reader.GetString(reader.GetOrdinal("Liv_Bud_ItemName"));

            if (ColumnExists(reader, "Liv_Bud_ItemDesc") && !reader.IsDBNull(reader.GetOrdinal("Liv_Bud_ItemDesc")))
                oLiveBudgItemsBO.Liv_Bud_ItemDesc = reader.GetString(reader.GetOrdinal("Liv_Bud_ItemDesc"));

            //if (ColumnExists(reader, "papname") && !reader.IsDBNull(reader.GetOrdinal("papname")))
            //    oLiveBudgItemsBO.PAPName = reader.GetString(reader.GetOrdinal("papname"));

            return oLiveBudgItemsBO;
        }
        #endregion

        ////save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        //public BatchBO AddBatch(BatchBO oBatchBO)
        //{
        //    string returnResult = string.Empty;
        //    BatchBO ooBatchBO = new BatchBO();//For Storing & Returning Result as Object

        //    SqlConnection OCon = new SqlConnection(con);
        //    OCon.Open();
        //    SqlCommand oCmd = new SqlCommand("USP_TRN_CMP_ADDBATCH", OCon);
        //    oCmd.CommandType = CommandType.StoredProcedure;
        //    int count = Convert.ToInt32(oCmd.CommandType);

        //    try
        //    {
        //        oCmd.Parameters.AddWithValue("batchstatus_", oBatchBO.BatchStatus);
        //        oCmd.Parameters.AddWithValue("payt_requestdate_", oBatchBO.Payt_RequestDate);

        //        oCmd.Parameters.AddWithValue("hhid_", oBatchBO.HHID);
        //        oCmd.Parameters.AddWithValue("requeststatus_", oBatchBO.RequestStatus);

        //        oCmd.Parameters.AddWithValue("isdeleted_", oBatchBO.IsDeleted);
        //        oCmd.Parameters.AddWithValue("createdby_", oBatchBO.CreatedBy);

        //        oCmd.Parameters.AddWithValue("getBatchNo_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
        //        /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = oCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
        //        // Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

        //        //oCmd.ExecuteNonQuery();
        //        SqlDataReader oDataReader = oCmd.ExecuteReader();

        //        if (oCmd.Parameters["errorMessage_"].Value != null)
        //            ooBatchBO.dbMessage = oCmd.Parameters["errorMessage_"].Value.ToString();
        //        else
        //            ooBatchBO.dbMessage = string.Empty;

        //        if (oCmd.Parameters["getBatchNo_"].Value != null)
        //            ooBatchBO.CMP_BatchNo = oCmd.Parameters["getBatchNo_"].Value.ToString();
        //        else
        //            ooBatchBO.CMP_BatchNo = string.Empty;

        //        if (oDataReader != null)
        //        {
        //            while (oDataReader.Read())
        //            {
        //                // ooBatchBO.dbMessage = oCmd.Parameters["errorMessage_"].Value.ToString();
        //                //ooBatchBO.CMP_BatchNo = oCmd.Parameters["getBatchNo_"].Value.ToString();
        //                //ooBatchBO.dbMessage = oDataReader["batchcreateddate"].ToString();
        //                ooBatchBO.CMP_BatchNo = oDataReader["CMP_BATCHNO"].ToString();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        oCmd.Dispose();
        //        OCon.Close();
        //        OCon.Dispose();
        //    }
        //    return ooBatchBO;
        //}

        //public int DeletePaymentRequest(int PaymentRequestId)
        //{
        //    cnn = new SqlConnection(con);

        //    proc = "USP_TRN_CMP_DEL_SUBMIT_PAYMENT";

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("payt_requestid_", PaymentRequestId);
        //    //cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.Int).Direction = ParameterDirection.Output;

        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();

        //    return result;
        //}

        //public string UpdatePaymentRequest(BatchBO oBatchBO)
        //{
        //    string returnResult = string.Empty;
        //    cnn = new SqlConnection(con);

        //    proc = "USP_TRN_CMP_UPD_SUBMIT_PAYMENT";

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection.Open();
        //    cmd.Parameters.AddWithValue("payt_requestid_", oBatchBO.Payt_RequestID);
        //    cmd.Parameters.AddWithValue("requeststatus_", oBatchBO.RequestStatus);
        //    cmd.Parameters.AddWithValue("updatedby_", oBatchBO.UpdatedBy);           

        //    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();

        //        if (cmd.Parameters["errorMessage_"].Value != null)
        //            returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
        //        else
        //            returnResult = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        returnResult = string.Empty;
        //        throw ex;
        //    }

        //    return returnResult;
        //}
        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID

        /*  public Concern GetConcernById(int ConcernID)
          {
              SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
              SqlCommand cmd;

              string proc = "USP_MST_GETSELECTCONCERN";

              cmd = new SqlCommand(proc, cnn);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("ConcernID_", ConcernID);
              // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

              cmd.Connection.Open();

              SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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