using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WIS_BusinessObjects;
using System.Text;

namespace WIS_DataAccess
{
    public class BatchDAL
    {
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        #region GetData
        /// <summary>
        /// To Get Batches from database
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BatchList GetBatches(int projectID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_CMP_GET_BATCH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
         
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;
            BatchList Batches = new BatchList();

            while (dr.Read())
            {
                objBatchBO = new BatchBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CMP_BATCHNO"))) objBatchBO.CMP_BatchNo = dr.GetString(dr.GetOrdinal("CMP_BATCHNO"));
                if (!dr.IsDBNull(dr.GetOrdinal("BATCHCREATEDDATE"))) objBatchBO.BatchCreatedDate = dr.GetDateTime(dr.GetOrdinal("BATCHCREATEDDATE")).ToString(UtilBO.DateFormat);
                if (!dr.IsDBNull(dr.GetOrdinal("BATCHSTATUS"))) objBatchBO.BatchStatus = dr.GetString(dr.GetOrdinal("BATCHSTATUS"));

                Batches.Add(objBatchBO);
            }

            dr.Close();

            return Batches;
        }
        /// <summary>
        /// To Get Payment Batches from database
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNO"></param>
        /// <param name="ToDate"></param>
        /// <param name="FromData"></param>
        /// <returns></returns>
        public BatchList GetPaymentBatches(int projectID, string BatchNO, string ToDate, string FromData)
        {            
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAYMENTBATCHES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            if (BatchNO.Trim().Length > 0)
                cmd.Parameters.AddWithValue("CMP_BATCHNO_", BatchNO);
            else
                cmd.Parameters.AddWithValue("CMP_BATCHNO_", DBNull.Value);
            if (FromData.Trim().Length > 0)
                cmd.Parameters.AddWithValue("BATCHFROMDATE_", FromData);
            else
                cmd.Parameters.AddWithValue("BATCHFROMDATE_", DBNull.Value);
            if (ToDate.Trim().Length > 0)
                cmd.Parameters.AddWithValue("BATCHTODATE_", ToDate);
            else
                cmd.Parameters.AddWithValue("BATCHTODATE_", DBNull.Value);

            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;
            BatchList Batches = new BatchList();

            while (dr.Read())
            {
                objBatchBO = new BatchBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CMP_BATCHNO"))) 
                    objBatchBO.CMP_BatchNo = dr.GetString(dr.GetOrdinal("CMP_BATCHNO"));
                if (!dr.IsDBNull(dr.GetOrdinal("BATCHCREATEDDATE")))
                    objBatchBO.BatchCreatedDate = dr.GetDateTime(dr.GetOrdinal("BATCHCREATEDDATE")).ToString(UtilBO.DateFormatFull);
                if (!dr.IsDBNull(dr.GetOrdinal("BATCHSTATUS"))) 
                    objBatchBO.BatchStatus = dr.GetString(dr.GetOrdinal("BATCHSTATUS"));
                if (ColumnExists(dr, "HHID") && !dr.IsDBNull(dr.GetOrdinal("HHID")))
                    objBatchBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                if (!dr.IsDBNull(dr.GetOrdinal("Total"))) objBatchBO.TOTALCount = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Total")));
                if (!dr.IsDBNull(dr.GetOrdinal("Approved"))) objBatchBO.TOTALApproved = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Approved")));
                if (!dr.IsDBNull(dr.GetOrdinal("Declined"))) objBatchBO.TOTALDeclined = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Declined")));
                if (!dr.IsDBNull(dr.GetOrdinal("Pending"))) objBatchBO.TOTALPending = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pending")));
                Batches.Add(objBatchBO);
            }

            dr.Close();

            return Batches;
        }
        /// <summary>
        /// To Get Submited Payment details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BatchList GetSubmitedPayment(int projectID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_CMP_GET_SUBMIT_PAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;
            BatchList oBatchList = new BatchList();

            while (dr.Read())
            {
                objBatchBO = new BatchBO();
                objBatchBO = MapData(dr);

                oBatchList.Add(objBatchBO);
            }

            dr.Close();

            return oBatchList;
        }
        /// <summary>
        /// To Get Payment Request Batch details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestBatch(int projectID, int BatchNo)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAPS_PROJECT_ID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            cmd.Parameters.AddWithValue("cmp_batchno_", BatchNo);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;
            BatchList oBatchList = new BatchList();

            while (dr.Read())
            {
                objBatchBO = new BatchBO();
                objBatchBO = MapData(dr);

                oBatchList.Add(objBatchBO);
            }

            dr.Close();

            return oBatchList;
        }
        /// <summary>
        /// To Get Payment Pending Batch details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public BatchList GetPaymentPendingBatch(int projectID, int BatchNo)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PENDING_BATCH_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            cmd.Parameters.AddWithValue("cmp_batchno_", BatchNo);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;
            BatchList oBatchList = new BatchList();

            while (dr.Read())
            {
                objBatchBO = new BatchBO();
                objBatchBO = MapData(dr);

                oBatchList.Add(objBatchBO);
            }

            dr.Close();

            return oBatchList;
        }
        /// <summary>
        /// To GetPaymentRequestByHHID details
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public BatchBO GetPaymentRequestByHHID(int householdID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAYTREQUESTSTATUS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO objBatchBO = null;

            while (dr.Read())
            {
                objBatchBO = new BatchBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CMP_BATCHNO"))) objBatchBO.CMP_BatchNo = dr.GetString(dr.GetOrdinal("CMP_BATCHNO"));
                if (!dr.IsDBNull(dr.GetOrdinal("requeststatus"))) objBatchBO.RequestStatus = dr.GetString(dr.GetOrdinal("requeststatus"));
                if (!dr.IsDBNull(dr.GetOrdinal("payt_requestdate"))) objBatchBO.Payt_RequestDate = dr.GetDateTime(dr.GetOrdinal("payt_requestdate")).ToString();
            }

            dr.Close();

            return objBatchBO;
        }
        /// <summary>
        /// To GetPaymentRequestByHHID details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestByHHID(int ProjectId, int HHID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAPS_BY_HHID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", HHID);
            cmd.Parameters.AddWithValue("ProjectId_", ProjectId);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO oBatchBO = null;
            BatchList oBatchList = new BatchList();
            while (dr.Read())
            {
                oBatchBO = new BatchBO();
                oBatchBO = MapData(dr);               
                oBatchList.Add(oBatchBO);
            }

            dr.Close();

            return oBatchList;
        }
        /// <summary>
        /// To GetPaymentRequestByHHID details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="HHID"></param>
        /// <param name="ElementId"></param>
        /// <param name="Status"></param>
        /// <param name="ApprovalLevel"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestByHHID(int ProjectId, int HHID, int ElementId, string Status, int ApprovalLevel)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_BATCH_HHID_TRAKID";// USP_TRN_GET_BATCH_HHID_TRAKID

             cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", HHID);
            cmd.Parameters.AddWithValue("ProjectId_", ProjectId);
            cmd.Parameters.AddWithValue("ElementId_", ElementId);
            cmd.Parameters.AddWithValue("Status_", Status);
            cmd.Parameters.AddWithValue("ApprovalLevel_", ApprovalLevel);
             
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO oBatchBO = null;
            BatchList oBatchList = new BatchList();
            while (dr.Read())
            {
                //oBatchBO = new BatchBO();
                //oBatchBO = MapData(dr);
                //oBatchList.Add(oBatchBO);
                 oBatchBO = new BatchBO();

                 if (ColumnExists(dr, "payt_requestid") && !dr.IsDBNull(dr.GetOrdinal("payt_requestid")))
                     oBatchBO.Payt_RequestID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("payt_requestid")));

                 if (ColumnExists(dr, "HHID") && !dr.IsDBNull(dr.GetOrdinal("HHID")))
                     oBatchBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                 if (ColumnExists(dr, "HHID_DISP") && !dr.IsDBNull(dr.GetOrdinal("HHID_DISP")))
                     oBatchBO.HHID_DISP = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID_DISP")));

                 if (ColumnExists(dr, "requeststatus") && !dr.IsDBNull(dr.GetOrdinal("requeststatus")))
                     oBatchBO.RequestStatus = dr.GetString(dr.GetOrdinal("requeststatus"));

                 if (ColumnExists(dr, "RequestStatusShow") && !dr.IsDBNull(dr.GetOrdinal("RequestStatusShow")))
                     oBatchBO.RequestStatusShow = dr.GetString(dr.GetOrdinal("RequestStatusShow"));

                 if (ColumnExists(dr, "amt_requested") && !dr.IsDBNull(dr.GetOrdinal("amt_requested")))
                     oBatchBO.Amt_Requested = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("amt_requested")));

                 if (ColumnExists(dr, "TOTAL_AMOUNT") && !dr.IsDBNull(dr.GetOrdinal("TOTAL_AMOUNT")))
                     oBatchBO.TotalAmount = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TOTAL_AMOUNT")));

                 if (ColumnExists(dr, "PAYT_DESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("PAYT_DESCRIPTION")))
                     oBatchBO.Payt_Description = dr.GetString(dr.GetOrdinal("PAYT_DESCRIPTION"));

                 if (ColumnExists(dr, "payt_requestdate") && !dr.IsDBNull(dr.GetOrdinal("payt_requestdate")))
                {
                    oBatchBO.Payt_RequestDate = dr.GetDateTime(dr.GetOrdinal("payt_requestdate")).ToString();
                }

                 if (ColumnExists(dr, "papname") && !dr.IsDBNull(dr.GetOrdinal("papname")))
                     oBatchBO.PAPName = dr.GetString(dr.GetOrdinal("papname"));

                 if (ColumnExists(dr, "cmp_batchno") && !dr.IsDBNull(dr.GetOrdinal("cmp_batchno")))
                     oBatchBO.CMP_BatchNo = dr.GetString(dr.GetOrdinal("cmp_batchno"));

                 if (ColumnExists(dr, "comments") && !dr.IsDBNull(dr.GetOrdinal("comments")))
                     oBatchBO.Comments = dr.GetString(dr.GetOrdinal("comments"));

                 if (ColumnExists(dr, "statuslevel") && !dr.IsDBNull(dr.GetOrdinal("statuslevel")))
                     oBatchBO.StausLevel =  Convert.ToInt32(dr.GetValue(dr.GetOrdinal("statuslevel")));

                 oBatchList.Add(oBatchBO);
            }

            dr.Close();

            return oBatchList;
        }
        /// <summary>
        /// To GetPaymentAmountApproved
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public BatchBO GetPaymentAmountApproved(BatchBO oBatchBO)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GETPAYMENTAMTAPPROVED";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", oBatchBO.HHID);
            cmd.Parameters.AddWithValue("amt_requested_", oBatchBO.Amt_Requested);
            //cmd.Parameters.AddWithValue("HHID_", oBatchBO.HHID);

            cmd.Parameters.AddWithValue("AmountMessage_", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            BatchBO ooBatchBO = null;

            while (dr.Read())
            {
                ooBatchBO = new BatchBO();

                if (ColumnExists(dr, "Amt_Requested") && !dr.IsDBNull(dr.GetOrdinal("Amt_Requested")))
                    ooBatchBO.Amt_Requested = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Amt_Requested")));
                //if (ColumnExists(dr, "payt_requestid") && !dr.IsDBNull(dr.GetOrdinal("requeststatus"))) 
                //    ooBatchBO.RequestStatus = dr.GetString(dr.GetOrdinal("requeststatus"));
                //if (ColumnExists(dr, "payt_requestid") && !dr.IsDBNull(dr.GetOrdinal("payt_requestdate"))) 
                //    ooBatchBO.Payt_RequestDate = dr.GetDateTime(dr.GetOrdinal("payt_requestdate")).ToString();
            }

            dr.Close();

            return ooBatchBO;
        }

        //public CompensationFinancialList getCompPackagePaymentRequest(BatchBO oBatchBO)
        //{
        //    SqlConnection cnn = new SqlConnection(con);
        //    SqlCommand cmd;

        //    string proc = "USP_TRN_GET_CMP_PAK_PAY_REQ";

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("HHID_", oBatchBO.HHID);
        //    //cmd.Parameters.AddWithValue("ProjectId_", oBatchBO.p);
        //    //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

        //    cmd.Connection.Open();
        //    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    CompensationFinancialBO oCompFinancialBO = null;
        //   // BatchBO ooBatchBO = null;
        //    CompensationFinancialList lstCompensationFinancial = new CompensationFinancialList();
        //    while (reader.Read())
        //    {
        //        //ooBatchBO = new BatchBO();
        //        oCompFinancialBO = new CompensationFinancialBO();

        //        #region Batch Section
        //        if (ColumnExists(reader, "payt_requestid") && !reader.IsDBNull(reader.GetOrdinal("payt_requestid")))
        //            oCompFinancialBO.oBatchBO.Payt_RequestID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("payt_requestid")));

        //        if (ColumnExists(reader, "Payt_RequestDate") && !reader.IsDBNull(reader.GetOrdinal("Payt_RequestDate")))
        //            oCompFinancialBO.oBatchBO.Payt_RequestDate = Convert.ToDateTime(reader.GetOrdinal("Payt_RequestDate")).ToString(UtilBO.DateFormatFull);

        //        if (ColumnExists(reader, "RequestStatus") && !reader.IsDBNull(reader.GetOrdinal("RequestStatus")))
        //            oCompFinancialBO.oBatchBO.RequestStatus =  reader.GetString(reader.GetOrdinal("RequestStatus"));

        //        if (ColumnExists(reader, "Payt_Description") && !reader.IsDBNull(reader.GetOrdinal("Payt_Description")))
        //            oCompFinancialBO.oBatchBO.Payt_Description = reader.GetString(reader.GetOrdinal("Payt_Description"));

        //        if (ColumnExists(reader, "Amt_Requested") && !reader.IsDBNull(reader.GetOrdinal("Amt_Requested")))
        //            oCompFinancialBO.oBatchBO.Amt_Requested = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Amt_Requested")));
        //        #endregion

        //        #region Compensation Financial
        //        if (ColumnExists(reader, "Cmp_FinancialID") && !reader.IsDBNull(reader.GetOrdinal("Cmp_FinancialID")))
        //            oCompFinancialBO.Cmp_FinancialID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Cmp_FinancialID")));

        //        if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
        //            oCompFinancialBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));


        //        #region Land Section
        //        if (ColumnExists(reader, "LandValuation") && !reader.IsDBNull(reader.GetOrdinal("LandValuation")))
        //            oCompFinancialBO.LandValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandValuation")));

        //        if (ColumnExists(reader, "LandDA") && !reader.IsDBNull(reader.GetOrdinal("LandDA")))
        //            oCompFinancialBO.LandDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDA")));

        //        if (ColumnExists(reader, "LandInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("LandInKindCompensation")))
        //            oCompFinancialBO.LandInKindCompensation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandInKindCompensation")));

        //        if (ColumnExists(reader, "LandDiffPayment") && !reader.IsDBNull(reader.GetOrdinal("LandDiffPayment")))
        //            oCompFinancialBO.LandDiffPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDiffPayment")));

        //        if (ColumnExists(reader, "LandValComments") && !reader.IsDBNull(reader.GetOrdinal("LandValComments")))
        //            oCompFinancialBO.LandValComments = reader.GetString(reader.GetOrdinal("LandValComments"));

        //        if (ColumnExists(reader, "LANDTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("LANDTOTALVALUATION")))
        //            oCompFinancialBO.LandTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LANDTOTALVALUATION")));
        //        #endregion

        //        #region Residential Structure Section
        //        if (ColumnExists(reader, "ResDepreciatedValue") && !reader.IsDBNull(reader.GetOrdinal("ResDepreciatedValue")))
        //            oCompFinancialBO.ResDepreciatedValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDepreciatedValue")));

        //        if (ColumnExists(reader, "ResReplacementValue") && !reader.IsDBNull(reader.GetOrdinal("ResReplacementValue")))
        //            oCompFinancialBO.ResReplacementValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResReplacementValue")));

        //        if (ColumnExists(reader, "ResDA") && !reader.IsDBNull(reader.GetOrdinal("ResDA")))
        //            oCompFinancialBO.ResDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDA")));

        //        if (ColumnExists(reader, "ResMovingAllowance") && !reader.IsDBNull(reader.GetOrdinal("ResMovingAllowance")))
        //            oCompFinancialBO.ResMovingAllowance = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResMovingAllowance")));

        //        if (ColumnExists(reader, "ResLabourCost") && !reader.IsDBNull(reader.GetOrdinal("ResLabourCost")))
        //            oCompFinancialBO.ResLabourCost = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResLabourCost")));

        //        if (ColumnExists(reader, "ResPayment") && !reader.IsDBNull(reader.GetOrdinal("ResPayment")))
        //            oCompFinancialBO.ResPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResPayment")));

        //        if (ColumnExists(reader, "ResInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("ResInKindCompensation")))
        //        {
        //            //  string testing = reader.GetString(reader.GetOrdinal("ResInKindCompensation"));
        //            oCompFinancialBO.ResInKindCompensation = reader.GetString(reader.GetOrdinal("ResInKindCompensation"));
        //        }

        //        if (ColumnExists(reader, "ResComments") && !reader.IsDBNull(reader.GetOrdinal("ResComments")))
        //            oCompFinancialBO.ResComments = reader.GetString(reader.GetOrdinal("ResComments"));

        //        if (ColumnExists(reader, "RESTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("RESTOTALVALUATION")))
        //            oCompFinancialBO.ResTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("RESTOTALVALUATION")));
        //        #endregion

        //        #region Fixture Section
        //        if (ColumnExists(reader, "FixtureValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureValuation")))
        //            oCompFinancialBO.FixtureValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureValuation")));

        //        if (ColumnExists(reader, "FixtureDA") && !reader.IsDBNull(reader.GetOrdinal("FixtureDA")))
        //            oCompFinancialBO.FixtureDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureDA")));

        //        if (ColumnExists(reader, "FixtureTotalValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureTotalValuation")))
        //            oCompFinancialBO.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureTotalValuation")));

        //        if (ColumnExists(reader, "FixtureComments") && !reader.IsDBNull(reader.GetOrdinal("FixtureComments")))
        //            oCompFinancialBO.FixtureComments = reader.GetString(reader.GetOrdinal("FixtureComments"));

        //        if (ColumnExists(reader, "FIXTURETOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("FIXTURETOTALVALUATION")))
        //            oCompFinancialBO.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FIXTURETOTALVALUATION")));
        //        #endregion

        //        #region Crops Section
        //        if (ColumnExists(reader, "CropValuation") && !reader.IsDBNull(reader.GetOrdinal("CropValuation")))
        //            oCompFinancialBO.CropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValuation")));

        //        if (ColumnExists(reader, "CropMaxCapCase") && !reader.IsDBNull(reader.GetOrdinal("CropMaxCapCase")))
        //            oCompFinancialBO.CropMaxCapCase = reader.GetString(reader.GetOrdinal("CropMaxCapCase"));

        //        if (ColumnExists(reader, "CropValAftMaxCap") && !reader.IsDBNull(reader.GetOrdinal("CropValAftMaxCap")))
        //            oCompFinancialBO.CropValAftMaxCap = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValAftMaxCap")));

        //        if (ColumnExists(reader, "CropDA") && !reader.IsDBNull(reader.GetOrdinal("CropDA")))
        //            oCompFinancialBO.CropDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropDA")));

        //        if (ColumnExists(reader, "CropComments") && !reader.IsDBNull(reader.GetOrdinal("CropComments")))
        //            oCompFinancialBO.CropComments = reader.GetString(reader.GetOrdinal("CropComments"));

        //        if (ColumnExists(reader, "CROPTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("CROPTOTALVALUATION")))
        //            oCompFinancialBO.CropTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CROPTOTALVALUATION")));
        //        #endregion

        //        #region Summery Section
        //        if (ColumnExists(reader, "CulturePropValuation") && !reader.IsDBNull(reader.GetOrdinal("CulturePropValuation")))
        //            oCompFinancialBO.CulturePropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CulturePropValuation")));

        //        if (ColumnExists(reader, "DamagedCropValuation") && !reader.IsDBNull(reader.GetOrdinal("DamagedCropValuation")))
        //            oCompFinancialBO.DamagedCropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DamagedCropValuation")));

        //        if (ColumnExists(reader, "TotalValuation"))
        //        {
        //            if (!reader.IsDBNull(reader.GetOrdinal("TotalValuation")))
        //                oCompFinancialBO.TotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("TotalValuation")));
        //        }

        //        if (ColumnExists(reader, "NegotiatedAmount") && !reader.IsDBNull(reader.GetOrdinal("NegotiatedAmount")))
        //            oCompFinancialBO.NegotiatedAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("NegotiatedAmount")));
        //        #endregion

        //        #region Common Section
        //        /* if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
        //        oCompFinancial.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

        //    if (ColumnExists(reader, "CreatedDate") && !reader.IsDBNull(reader.GetOrdinal("CreatedDate")))
        //        oCompFinancial.CreatedDate = reader.GetString(reader.GetOrdinal("CreatedDate"));

        //    if (ColumnExists(reader, "CreatedBy") && !reader.IsDBNull(reader.GetOrdinal("CreatedBy")))
        //        oCompFinancial.CreatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("CreatedBy")));

        //    if (ColumnExists(reader, "UpdatedDate") && !reader.IsDBNull(reader.GetOrdinal("UpdatedDate")))
        //        oCompFinancial.UpdatedDate = reader.GetString(reader.GetOrdinal("UpdatedDate"));

        //    if (ColumnExists(reader, "UpdatedBy") && !reader.IsDBNull(reader.GetOrdinal("UpdatedBy")))
        //        oCompFinancial.UpdatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("UpdatedBy")));*/
        //        #endregion
        //        #endregion  Compensation Financial
        //        lstCompensationFinancial.Add(oCompFinancialBO);
        //    }

        //    reader.Close();

        //    return lstCompensationFinancial;
        //}
        /// <summary>
        /// To MAPData
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private BatchBO MapData(IDataReader reader)
        {
            BatchBO oBatchBO = new BatchBO();

            if (ColumnExists(reader, "payt_requestid") && !reader.IsDBNull(reader.GetOrdinal("payt_requestid")))
                oBatchBO.Payt_RequestID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("payt_requestid")));

            if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                oBatchBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            if (ColumnExists(reader, "HHID_DISP") && !reader.IsDBNull(reader.GetOrdinal("HHID_DISP")))
                oBatchBO.HHID_DISP = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID_DISP")));

            if (ColumnExists(reader, "requeststatus") && !reader.IsDBNull(reader.GetOrdinal("requeststatus")))
                oBatchBO.RequestStatus = reader.GetString(reader.GetOrdinal("requeststatus"));

            if (ColumnExists(reader, "amt_requested") && !reader.IsDBNull(reader.GetOrdinal("amt_requested")))
                oBatchBO.Amt_Requested = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("amt_requested")));

            if (ColumnExists(reader, "TOTAL_AMOUNT") && !reader.IsDBNull(reader.GetOrdinal("TOTAL_AMOUNT")))
                oBatchBO.TotalAmount =  Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("TOTAL_AMOUNT")));

            if (ColumnExists(reader, "PAYT_DESCRIPTION") && !reader.IsDBNull(reader.GetOrdinal("PAYT_DESCRIPTION")))
                oBatchBO.Payt_Description = reader.GetString(reader.GetOrdinal("PAYT_DESCRIPTION"));

            if (ColumnExists(reader, "payt_requestdate") && !reader.IsDBNull(reader.GetOrdinal("payt_requestdate")))
            {
                oBatchBO.Payt_RequestDate = reader.GetDateTime(reader.GetOrdinal("payt_requestdate")).ToString();
            }

            if (ColumnExists(reader, "papname") && !reader.IsDBNull(reader.GetOrdinal("papname")))
                oBatchBO.PAPName = reader.GetString(reader.GetOrdinal("papname"));

            if (ColumnExists(reader, "PLOTREFERENCE") && !reader.IsDBNull(reader.GetOrdinal("PLOTREFERENCE")))
                oBatchBO.PlotRef = reader.GetString(reader.GetOrdinal("PLOTREFERENCE"));

            if (ColumnExists(reader, "DESIGNATION") && !reader.IsDBNull(reader.GetOrdinal("DESIGNATION")))
                oBatchBO.Designation = reader.GetString(reader.GetOrdinal("DESIGNATION"));

            if (ColumnExists(reader, "cmp_batchno") && !reader.IsDBNull(reader.GetOrdinal("cmp_batchno")))
                oBatchBO.CMP_BatchNo = reader.GetString(reader.GetOrdinal("cmp_batchno"));

            if (ColumnExists(reader, "comments") && !reader.IsDBNull(reader.GetOrdinal("comments")))
                oBatchBO.Comments = reader.GetString(reader.GetOrdinal("comments"));

            if (ColumnExists(reader, "LANDINKINDCOMPENSATION") && !reader.IsDBNull(reader.GetOrdinal("LANDINKINDCOMPENSATION")))
                oBatchBO.InKindValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LANDINKINDCOMPENSATION")));

            return oBatchBO;
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
        #endregion
        /// <summary>
        /// To add batch
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public BatchBO AddBatch(BatchBO oBatchBO)
        {
            string returnResult = string.Empty;
            BatchBO ooBatchBO = new BatchBO();

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_CMP_ADDBATCH", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("CMP_BATCHNO_", oBatchBO.CMP_BatchNo);
                oCmd.Parameters.AddWithValue("batchstatus_", oBatchBO.BatchStatus);
                oCmd.Parameters.AddWithValue("payt_requestdate_", Convert.ToDateTime(oBatchBO.Payt_RequestDate).ToString(UtilBO.DateFormatDB));

                oCmd.Parameters.AddWithValue("hhid_", oBatchBO.HHID);
                oCmd.Parameters.AddWithValue("requeststatus_", oBatchBO.RequestStatus);

                oCmd.Parameters.AddWithValue("Payt_Description_", oBatchBO.Payt_Description);
                oCmd.Parameters.AddWithValue("Amt_Requested_", oBatchBO.Amt_Requested);
                oCmd.Parameters.AddWithValue("Comments_", oBatchBO.Comments);
                oCmd.Parameters.AddWithValue("TotalAmount_", oBatchBO.TotalAmount);

                oCmd.Parameters.AddWithValue("isdeleted_", oBatchBO.IsDeleted);
                oCmd.Parameters.AddWithValue("createdby_", oBatchBO.CreatedBy);

                oCmd.Parameters.AddWithValue("getBatchNo_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = oCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
               // // Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                SqlDataReader oDataReader = oCmd.ExecuteReader();

                if (oCmd.Parameters["errorMessage_"].Value == null)
                    ooBatchBO.dbMessage = string.Empty;
                else
                    ooBatchBO.dbMessage = oCmd.Parameters["errorMessage_"].Value.ToString();

                if (oCmd.Parameters["getBatchNo_"].Value == null)
                     ooBatchBO.CMP_BatchNo = string.Empty;
                else
                    ooBatchBO.CMP_BatchNo = oCmd.Parameters["getBatchNo_"].Value.ToString();

                if (oDataReader != null)
                {
                    while (oDataReader.Read())
                    {
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
        /// <summary>
        /// To delete payment request
        /// </summary>
        /// <param name="PaymentRequestId"></param>
        /// <returns></returns>
        public int DeletePaymentRequest(int PaymentRequestId)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_CMP_DEL_SUBMIT_PAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("payt_requestid_", PaymentRequestId);

            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }
        /// <summary>
        /// To update payment request
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public string UpdatePaymentRequest(BatchBO oBatchBO)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_CMP_UPD_SUBMIT_PAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("payt_requestid_", oBatchBO.Payt_RequestID);
            if (oBatchBO.CMP_BatchNo != null)
            {
                cmd.Parameters.AddWithValue("CMP_BatchNo_", oBatchBO.CMP_BatchNo);
            }
            else
            {
                cmd.Parameters.AddWithValue("CMP_BatchNo_", "0");
            }
            if (oBatchBO.HHID != 0)
            {
                cmd.Parameters.AddWithValue("HHID_", oBatchBO.HHID);
            }
            else
            {
                cmd.Parameters.AddWithValue("HHID_","0");
            }
            cmd.Parameters.AddWithValue("statuslevel_", oBatchBO.StausLevel);
            cmd.Parameters.AddWithValue("requeststatus_", oBatchBO.RequestStatus);
            cmd.Parameters.AddWithValue("updatedby_", oBatchBO.UpdatedBy);           

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
        /// <summary>
        /// To DeclineBatchHHID
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <param name="PaymentRequestId"></param>
        /// <param name="HHID"></param>
        public void DeclineBatchHHID(int BatchNo, int PaymentRequestId, int HHID)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_DECLINE_BATCH_HHID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
           // cmd.Parameters.AddWithValue("BatchNo_", BatchNo);
            cmd.Parameters.AddWithValue("PaymentRequestId_", PaymentRequestId);
            cmd.Parameters.AddWithValue("HHID_", HHID);

           // /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                //if (cmd.Parameters["errorMessage_"].Value != null)
                //    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                //else
                //    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
               // returnResult = string.Empty;
                throw ex;
            }

           // return returnResult;
        }
        /// <summary>
        /// To close batch
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="UserId"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public string CloseBatch(int HHID,int UserId,int BatchNo, WorkFlowBO objWorkFlow)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_CMP_CLOSE_PAYREQ_BATCH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            //cmd.Parameters.AddWithValue("HHID_", HHID);
            cmd.Parameters.AddWithValue("CMP_BATCHNO_", BatchNo);
            cmd.Parameters.AddWithValue("UPDATEDBY_", UserId);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                    returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                returnResult = string.Empty;
                throw ex;
            }

            #region Notify Higher Authority of Batch Completion:
            
            if (objWorkFlow != null)
            {
                NotificationBO objNotification = new NotificationBO();
                StringBuilder sb = new StringBuilder();

                if (objWorkFlow.HigherAuthorityEmailID != null)
                {

                    string HigherAuthorityName = objWorkFlow.HigherAuthorityName;
                    string ProjectName = objWorkFlow.ProjectName;
                    string ProjectCode = objWorkFlow.ProjectCode;

                    sb.Append("Dear " + HigherAuthorityName + ",");
                    sb.Append("<br/><br/>");
                    sb.Append("Approval completed on " + "Batch: <a href='wisapp.uetcl.com' style='text-decoration:none'><b>" + BatchNo + "</b></a>");
                    sb.Append("<br/>");
                    sb.Append("You can now prepare the MEMO for payment processing.");
                    sb.Append("<br/>");
                    sb.Append("Follow the link to WIS for Batch Details report, and approver Comments");
                    sb.Append("<br/><br/>");
                    sb.Append("<br/><br/>");
                    sb.Append("<br/><br/>");
                    sb.Append("<br/><br/>");
                    sb.Append("<br/><br/>");
                    sb.Append("<br/><br/>");
                    sb.Append("___________________________________");
                    sb.Append("<br/><br/>");
                    sb.Append("Wayleaves Information System (WIS)");

                    objNotification.EmailID = objWorkFlow.HigherAuthorityEmailID;
                    objNotification.EmailSubject = "Batch: " + BatchNo + " (" + ProjectCode + " Project) - Ready for payment";
                    objNotification.EmailBody = sb.ToString();
                    

                    (new NotificationDAL()).SendEmail(objNotification);
                }
            }

            #endregion

            return returnResult;
            
        }
        /// <summary>
        /// To Add Comments
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public void AddBatchComments(BatchBO oBatchBO)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_BATCHINDCOMMENTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            //cmd.Parameters.AddWithValue("payt_requestid_", oBatchBO.Payt_RequestID);
            if (oBatchBO.CMP_BatchNo != null)
            {
                cmd.Parameters.AddWithValue("CMP_BatchNo_", oBatchBO.CMP_BatchNo);
            }
            else
            {
                cmd.Parameters.AddWithValue("CMP_BatchNo_", "0");
            }
            if (oBatchBO.HHID != 0)
            {
                cmd.Parameters.AddWithValue("HHID_", oBatchBO.HHID);
            }
            else
            {
                cmd.Parameters.AddWithValue("HHID_", "0");
            }
            cmd.Parameters.AddWithValue("statuslevel_", oBatchBO.StausLevel);
            cmd.Parameters.AddWithValue("requeststatus_", oBatchBO.RequestStatus);
            cmd.Parameters.AddWithValue("updatedby_", oBatchBO.UpdatedBy);
            cmd.Parameters.AddWithValue("Comments_", oBatchBO.Comments);
            cmd.ExecuteNonQuery();
        }
    }
}