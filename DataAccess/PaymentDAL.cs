using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
//using System.Configuration;
using System.Data.SqlClient;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class PaymentDAL
    {
        #region Declareation
        // string ccon= WIS_DataAccess.AppConfiguration.ConnectionString;
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;

        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Mode Of Payment
        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To Get Mode Of Payment
        /// </summary>
        /// <param name="TypeOfPayment"></param>
        /// <returns></returns>
        public PaymentList GetModeOfPayment(string TypeOfPayment)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_MST_GET_MODEOFPAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TypeOfPayment_", TypeOfPayment);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PaymentBO objPaymentBO = null;
            PaymentList oPaymentList = new PaymentList();

            while (dr.Read())
            {
                objPaymentBO = new PaymentBO();

                objPaymentBO = MapData(dr);

                oPaymentList.Add(objPaymentBO);
            }

            dr.Close();

            return oPaymentList;
        }

        // to check the Column are Exists or not
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
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private PaymentBO MapData(IDataReader reader)
        {
            PaymentBO oPaymentBO = new PaymentBO();

            if (ColumnExists(reader, "modeofpaymentid") && !reader.IsDBNull(reader.GetOrdinal("modeofpaymentid")))
                oPaymentBO.ModeOfPaymentId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("modeofpaymentid")));

            if (ColumnExists(reader, "modeofpayment") && !reader.IsDBNull(reader.GetOrdinal("modeofpayment")))
                oPaymentBO.ModeOfPayment = reader.GetString(reader.GetOrdinal("modeofpayment"));

            if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
                oPaymentBO.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

            return oPaymentBO;
        }
        #endregion

        #region Compensation Payment
        CompensationPayementList lstCompensationPayement;

        //public CompensationFinancialList GetCompensationFinancialList(int HHID)
        //{
        //    proc = "USP_TRN_GET_CMP_FINANCIALS";
        //    cnn = new SqlConnection(con);
        //    CompensationFinancialBO oCompensationFinancial = null;

        //    CompensationFinancialList lstCompensationFinancial = new CompensationFinancialList();

        //    CompensationFinancialBO OCompensationFinancial = new CompensationFinancialBO();

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    //cmd.Parameters.AddWithValue("CompensationFinancialName_", CompensationFinancialName);
        //    //cmd.Parameters.AddWithValue("city_", city);
        //    cmd.Parameters.AddWithValue("hhid_", HHID);

        //    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

        //    try
        //    {
        //        cmd.Connection.Open();
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (dr.Read())
        //        {

        //            oCompensationFinancial = new CompensationFinancialBO();
        //            oCompensationFinancial = MapData_CompPayment(dr);
        //            //if (ColumnExists(dr, "Cmp_FinancialID") && !dr.IsDBNull(dr.GetOrdinal("Cmp_FinancialID")))
        //            //    objUnit.Cmp_FinancialID = dr.GetInt32(dr.GetOrdinal("Cmp_FinancialID"));

        //            lstCompensationFinancial.Add(oCompensationFinancial);
        //        }

        //        dr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return lstCompensationFinancial;
        //}

        //private CompensationFinancialBO MapData(IDataReader reader)
        //{
        //    CompensationFinancialBO oCompFinancial = new CompensationFinancialBO();

        //    if (ColumnExists(reader, "Cmp_FinancialID") && !reader.IsDBNull(reader.GetOrdinal("Cmp_FinancialID")))
        //        oCompFinancial.Cmp_FinancialID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Cmp_FinancialID")));

        //    if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
        //        oCompFinancial.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

        //    #region Land Section
        //    if (ColumnExists(reader, "LandValuation") && !reader.IsDBNull(reader.GetOrdinal("LandValuation")))
        //        oCompFinancial.LandValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandValuation")));

        //    if (ColumnExists(reader, "LandDA") && !reader.IsDBNull(reader.GetOrdinal("LandDA")))
        //        oCompFinancial.LandDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDA")));

        //    if (ColumnExists(reader, "LandInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("LandInKindCompensation")))
        //        oCompFinancial.LandInKindCompensation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandInKindCompensation")));

        //    if (ColumnExists(reader, "LandDiffPayment") && !reader.IsDBNull(reader.GetOrdinal("LandDiffPayment")))
        //        oCompFinancial.LandDiffPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDiffPayment")));

        //    if (ColumnExists(reader, "LandValComments") && !reader.IsDBNull(reader.GetOrdinal("LandValComments")))
        //        oCompFinancial.LandValComments = reader.GetString(reader.GetOrdinal("LandValComments"));

        //    if (ColumnExists(reader, "LANDTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("LANDTOTALVALUATION")))
        //        oCompFinancial.LandTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LANDTOTALVALUATION")));
        //    #endregion

        //    #region Residential Structure Section
        //    if (ColumnExists(reader, "ResDepreciatedValue") && !reader.IsDBNull(reader.GetOrdinal("ResDepreciatedValue")))
        //        oCompFinancial.ResDepreciatedValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDepreciatedValue")));

        //    if (ColumnExists(reader, "ResReplacementValue") && !reader.IsDBNull(reader.GetOrdinal("ResReplacementValue")))
        //        oCompFinancial.ResReplacementValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResReplacementValue")));

        //    if (ColumnExists(reader, "ResDA") && !reader.IsDBNull(reader.GetOrdinal("ResDA")))
        //        oCompFinancial.ResDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDA")));

        //    if (ColumnExists(reader, "ResMovingAllowance") && !reader.IsDBNull(reader.GetOrdinal("ResMovingAllowance")))
        //        oCompFinancial.ResMovingAllowance = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResMovingAllowance")));

        //    if (ColumnExists(reader, "ResLabourCost") && !reader.IsDBNull(reader.GetOrdinal("ResLabourCost")))
        //        oCompFinancial.ResLabourCost = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResLabourCost")));

        //    if (ColumnExists(reader, "ResPayment") && !reader.IsDBNull(reader.GetOrdinal("ResPayment")))
        //        oCompFinancial.ResPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResPayment")));

        //    if (ColumnExists(reader, "ResInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("ResInKindCompensation")))
        //        oCompFinancial.ResInKindCompensation = reader.GetString(reader.GetOrdinal("ResInKindCompensation"));

        //    if (ColumnExists(reader, "ResComments") && !reader.IsDBNull(reader.GetOrdinal("ResComments")))
        //        oCompFinancial.ResComments = reader.GetString(reader.GetOrdinal("ResComments"));

        //    if (ColumnExists(reader, "RESTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("RESTOTALVALUATION")))
        //        oCompFinancial.ResTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("RESTOTALVALUATION")));
        //    #endregion

        //    #region Fixture Section
        //    if (ColumnExists(reader, "FixtureValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureValuation")))
        //        oCompFinancial.FixtureValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureValuation")));

        //    if (ColumnExists(reader, "FixtureDA") && !reader.IsDBNull(reader.GetOrdinal("FixtureDA")))
        //        oCompFinancial.FixtureDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureDA")));

        //    if (ColumnExists(reader, "FixtureTotalValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureTotalValuation")))
        //        oCompFinancial.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureTotalValuation")));

        //    if (ColumnExists(reader, "FixtureComments") && !reader.IsDBNull(reader.GetOrdinal("FixtureComments")))
        //        oCompFinancial.FixtureComments = reader.GetString(reader.GetOrdinal("FixtureComments"));

        //    if (ColumnExists(reader, "FIXTURETOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("FIXTURETOTALVALUATION")))
        //        oCompFinancial.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FIXTURETOTALVALUATION")));
        //    #endregion

        //    #region Crops Section
        //    if (ColumnExists(reader, "CropValuation") && !reader.IsDBNull(reader.GetOrdinal("CropValuation")))
        //        oCompFinancial.CropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValuation")));

        //    if (ColumnExists(reader, "CropMaxCapCase") && !reader.IsDBNull(reader.GetOrdinal("CropMaxCapCase")))
        //        oCompFinancial.CropMaxCapCase = reader.GetString(reader.GetOrdinal("CropMaxCapCase"));

        //    if (ColumnExists(reader, "CropValAftMaxCap") && !reader.IsDBNull(reader.GetOrdinal("CropValAftMaxCap")))
        //        oCompFinancial.CropValAftMaxCap = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValAftMaxCap")));

        //    if (ColumnExists(reader, "CropDA") && !reader.IsDBNull(reader.GetOrdinal("CropDA")))
        //        oCompFinancial.CropDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropDA")));

        //    if (ColumnExists(reader, "CropComments") && !reader.IsDBNull(reader.GetOrdinal("CropComments")))
        //        oCompFinancial.CropComments = reader.GetString(reader.GetOrdinal("CropComments"));

        //    if (ColumnExists(reader, "CROPTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("CROPTOTALVALUATION")))
        //        oCompFinancial.CropTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CROPTOTALVALUATION")));
        //    #endregion

        //    #region Summery Section
        //    if (ColumnExists(reader, "CulturePropValuation") && !reader.IsDBNull(reader.GetOrdinal("CulturePropValuation")))
        //        oCompFinancial.CulturePropValuation = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("CulturePropValuation")));

        //    if (ColumnExists(reader, "DamagedCropValuation") && !reader.IsDBNull(reader.GetOrdinal("DamagedCropValuation")))
        //        oCompFinancial.DamagedCropValuation = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("DamagedCropValuation")));

        //    if (ColumnExists(reader, "NegotiatedAmount") && !reader.IsDBNull(reader.GetOrdinal("NegotiatedAmount")))
        //        oCompFinancial.NegotiatedAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("NegotiatedAmount")));
        //    #endregion

        //    #region Common Section
        //    /* if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
        //        oCompFinancial.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

        //    if (ColumnExists(reader, "CreatedDate") && !reader.IsDBNull(reader.GetOrdinal("CreatedDate")))
        //        oCompFinancial.CreatedDate = reader.GetString(reader.GetOrdinal("CreatedDate"));

        //    if (ColumnExists(reader, "CreatedBy") && !reader.IsDBNull(reader.GetOrdinal("CreatedBy")))
        //        oCompFinancial.CreatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("CreatedBy")));

        //    if (ColumnExists(reader, "UpdatedDate") && !reader.IsDBNull(reader.GetOrdinal("UpdatedDate")))
        //        oCompFinancial.UpdatedDate = reader.GetString(reader.GetOrdinal("UpdatedDate"));

        //    if (ColumnExists(reader, "UpdatedBy") && !reader.IsDBNull(reader.GetOrdinal("UpdatedBy")))
        //        oCompFinancial.UpdatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("UpdatedBy")));*/
        //    #endregion

        //    return oCompFinancial;
        //}

        /// <summary>
        /// To Get Compensation Payment
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationPayementList getCompensationPayment(int HHID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_CMP_PAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("hhid_", HHID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PaymentBO.CompensationPayementBO objCompensationPayementBO = null;
            lstCompensationPayement = new CompensationPayementList();

            while (dr.Read())
            {
                objCompensationPayementBO = new PaymentBO.CompensationPayementBO();

                objCompensationPayementBO = MapData_CompPayment(dr);

                lstCompensationPayement.Add(objCompensationPayementBO);
            }

            dr.Close();

            return lstCompensationPayement;
        }

        /// <summary>
        /// TO Get Compensation Payment By Id
        /// </summary>
        /// <param name="CompensationPaymentID"></param>
        /// <returns></returns>
        public PaymentBO.CompensationPayementBO getCompensationPaymentById(int CompensationPaymentID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_CMP_PAYMENTBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cmp_paymentid_", CompensationPaymentID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PaymentBO.CompensationPayementBO objCompensationPayementBO = null;
            lstCompensationPayement = new CompensationPayementList();

            objCompensationPayementBO = new PaymentBO.CompensationPayementBO();

            while (dr.Read())
            {

                objCompensationPayementBO = MapData_CompPayment(dr);

                //lstCompensationPayement.Add(objCompensationPayementBO);
            }

            dr.Close();

            return objCompensationPayementBO;
        }

        /// <summary>
        /// To Add Compensation Payment
        /// </summary>
        /// <param name="oCompPayementBO"></param>
        /// <returns></returns>
        public string[] AddCompensationPayment(PaymentBO.CompensationPayementBO oCompPayementBO)
        {
            string[] returnResult = new string[2];
            PaymentBO ooPaymentBO = new PaymentBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_INS_CMP_PAYMENT", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("hhid_", oCompPayementBO.HHID);
                oCmd.Parameters.AddWithValue("compensationtype_", oCompPayementBO.CompensationType);
                oCmd.Parameters.AddWithValue("modeofpaymentid_", oCompPayementBO.ModeOfPaymentId);
                oCmd.Parameters.AddWithValue("compensationamount_", oCompPayementBO.CompensationAmount);
                oCmd.Parameters.AddWithValue("deliveredtostakeholder_", oCompPayementBO.DeliveredToStakeHolder);

                if (!string.IsNullOrEmpty(oCompPayementBO.DeliveredDate))
                    oCmd.Parameters.AddWithValue("delivereddate_", oCompPayementBO.DeliveredDate);
                else
                    oCmd.Parameters.AddWithValue("delivereddate_", DBNull.Value);
                //oCmd.Parameters.AddWithValue("delivereddate_", oCompPayementBO.DeliveredDate);

                oCmd.Parameters.AddWithValue("BANKID_", oCompPayementBO.BankID);
                oCmd.Parameters.AddWithValue("BRANCHID_", oCompPayementBO.BranchID);
                oCmd.Parameters.AddWithValue("BANKREFERENCE_", oCompPayementBO.BankReference);
                //oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", oCompPayementBO.FixedCostCentreID);

                if (oCompPayementBO.FixedCostCentreID > 0)
                    oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", oCompPayementBO.FixedCostCentreID);
                else
                    oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", DBNull.Value);

                oCmd.Parameters.AddWithValue("isdeleted_", oCompPayementBO.IsDeleted);
                oCmd.Parameters.AddWithValue("createdby_", oCompPayementBO.CreatedBy);

                oCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                oCmd.Parameters.AddWithValue("cmp_paymentidout_", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult[0] = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult[0] = string.Empty;

                if (oCmd.Parameters["cmp_paymentidout_"].Value != null)
                    returnResult[1] = oCmd.Parameters["cmp_paymentidout_"].Value.ToString();
                else
                    returnResult[1] = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// To Update Composition Payment
        /// </summary>
        /// <param name="oCompPayementBO"></param>
        /// <returns></returns>
        public string UpdateCompositionPayment(PaymentBO.CompensationPayementBO oCompPayementBO)
        {
            string returnResult = string.Empty;
            PaymentBO ooPaymentBO = new PaymentBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_UPD_CMP_PAYMENT", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("cmp_paymentid_", oCompPayementBO.CompPaymentId);
                oCmd.Parameters.AddWithValue("hhid_", oCompPayementBO.HHID);
                oCmd.Parameters.AddWithValue("compensationtype_", oCompPayementBO.CompensationType);
                oCmd.Parameters.AddWithValue("modeofpaymentid_", oCompPayementBO.ModeOfPaymentId);
                oCmd.Parameters.AddWithValue("compensationamount_", oCompPayementBO.CompensationAmount);

                oCmd.Parameters.AddWithValue("BANKID_", oCompPayementBO.BankID);
                oCmd.Parameters.AddWithValue("BRANCHID_", oCompPayementBO.BranchID);
                oCmd.Parameters.AddWithValue("BANKREFERENCE_", oCompPayementBO.BankReference);
                //oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", oCompPayementBO.FixedCostCentreID);

                if (oCompPayementBO.FixedCostCentreID > 0)
                    oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", oCompPayementBO.FixedCostCentreID);
                else
                    oCmd.Parameters.AddWithValue("FIXEDCOSTCENTREID_", DBNull.Value);

                oCmd.Parameters.AddWithValue("deliveredtostakeholder_", oCompPayementBO.DeliveredToStakeHolder);
                if (!string.IsNullOrEmpty(oCompPayementBO.DeliveredDate))// (oCompPayementBO.DeliveredDate != "" || oCompPayementBO.DeliveredDate != null)
                    oCmd.Parameters.AddWithValue("delivereddate_", oCompPayementBO.DeliveredDate);
                else
                    oCmd.Parameters.AddWithValue("delivereddate_", DBNull.Value);

                oCmd.Parameters.AddWithValue("isdeleted_", oCompPayementBO.IsDeleted);
                oCmd.Parameters.AddWithValue("updatedby_", oCompPayementBO.UpdatedBy);

                oCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }

        #region Compensation Financial
        public string UpdateCompensationFinancial(CompensationFinancialBO oCompensationFinancialBO)
        {
            string returnResult = string.Empty;

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_UPD_CMP_PAYMENTSUMMERY", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("hhid_", oCompensationFinancialBO.HHID);
                oCmd.Parameters.AddWithValue("facilitationallowance_", oCompensationFinancialBO.FacilitationAllowance);

                if (oCompensationFinancialBO.LandInKindCompensation == 0 || oCompensationFinancialBO.LandInKindCompensation == -1)
                    oCmd.Parameters.AddWithValue("resinkindcompensation_", DBNull.Value);
                else
                    oCmd.Parameters.AddWithValue("resinkindcompensation_", oCompensationFinancialBO.LandInKindCompensation);

                if (oCompensationFinancialBO.ResInKindCompensation == "0" || oCompensationFinancialBO.ResInKindCompensation == "-1")
                    oCmd.Parameters.AddWithValue("resinkindcompensation_", DBNull.Value);
                else
                    oCmd.Parameters.AddWithValue("resinkindcompensation_", oCompensationFinancialBO.ResInKindCompensation);
                oCmd.Parameters.AddWithValue("updatedby_", oCompensationFinancialBO.UpdatedBy);

                oCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery(); //Executing Query

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
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
            return returnResult;
        }

        public CompensationFinancialBO getCompnesationFinancial(int HHID)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_CMP_PAYMENTSUMMERY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("hhid_", HHID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CompensationFinancialBO oCompensationFinancialBO = null;
            //CompensationFinancialList lstCompensationFinancial = new CompensationFinancialList();

            while (dr.Read())
            {
                oCompensationFinancialBO = new CompensationFinancialBO();

                if (ColumnExists(dr, "cmp_financialid") && !dr.IsDBNull(dr.GetOrdinal("cmp_financialid")))
                    oCompensationFinancialBO.Cmp_FinancialID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cmp_financialid")));

                if (ColumnExists(dr, "HHID") && !dr.IsDBNull(dr.GetOrdinal("HHID")))
                    oCompensationFinancialBO.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (ColumnExists(dr, "FacilitationAllowance") && !dr.IsDBNull(dr.GetOrdinal("FacilitationAllowance")))
                    oCompensationFinancialBO.FacilitationAllowance = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FacilitationAllowance")));

                if (ColumnExists(dr, "FACILITATIONALLOWANCEPAID") && !dr.IsDBNull(dr.GetOrdinal("FACILITATIONALLOWANCEPAID")))
                    oCompensationFinancialBO.FacilitationAllowancePaid = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FACILITATIONALLOWANCEPAID")));

                if (ColumnExists(dr, "TotalValuation") && !dr.IsDBNull(dr.GetOrdinal("TotalValuation")))
                    oCompensationFinancialBO.TotalValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TotalValuation")));

                if (ColumnExists(dr, "ResInKindCompensation") && !dr.IsDBNull(dr.GetOrdinal("ResInKindCompensation")))
                    oCompensationFinancialBO.ResInKindCompensation = dr.GetString(dr.GetOrdinal("ResInKindCompensation"));

                if (ColumnExists(dr, "LandInKindCompensation") && !dr.IsDBNull(dr.GetOrdinal("LandInKindCompensation")))
                    oCompensationFinancialBO.LandInKindCompensation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("LandInKindCompensation")));
            }

            dr.Close();

            return oCompensationFinancialBO;

        }
        #endregion

        /// <summary>
        /// To Delete Composition Payment
        /// </summary>
        /// <param name="ComPaymentId"></param>
        /// <returns></returns>
        public int DeleteCompositionPayment(int ComPaymentId)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_CMP_PAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cmp_paymentid_", ComPaymentId);
            //cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            int result = cmd.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// To get Compensation Payment Export
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public PaymentExportList getCompensationPaymentExport(int projectID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_CMP_PAYMENTALL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            cmd.Parameters.AddWithValue("PAPNAME_", papName);
            cmd.Parameters.AddWithValue("PLOTREFERENCE_", plotReference);
            cmd.Parameters.AddWithValue("DISTRICT_", district);
            cmd.Parameters.AddWithValue("COUNTY_", county);
            cmd.Parameters.AddWithValue("SUBCOUNTY_", subCounty);
            cmd.Parameters.AddWithValue("PARISH_", parish);
            cmd.Parameters.AddWithValue("VILLAGE_", village);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PaymentExportBO objPaymentExportBO = null;
            PaymentExportList lstPaymentExportList = new PaymentExportList();

            while (dr.Read())
            {
                objPaymentExportBO = new PaymentExportBO();


                if (ColumnExists(dr, "cmp_paymentid") && !dr.IsDBNull(dr.GetOrdinal("cmp_paymentid")))
                    objPaymentExportBO.CompPaymentId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cmp_paymentid")));

                if (ColumnExists(dr, "compensationtype") && !dr.IsDBNull(dr.GetOrdinal("compensationtype")))
                    objPaymentExportBO.CompensationType = dr.GetString(dr.GetOrdinal("compensationtype"));

                if (ColumnExists(dr, "modeofpaymentid") && !dr.IsDBNull(dr.GetOrdinal("modeofpaymentid")))
                    objPaymentExportBO.ModeOfPaymentId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("modeofpaymentid")));

                if (ColumnExists(dr, "modeofpayment") && !dr.IsDBNull(dr.GetOrdinal("modeofpayment")))
                    objPaymentExportBO.ModeOfPayment = dr.GetString(dr.GetOrdinal("modeofpayment"));

                if (ColumnExists(dr, "compensationamount") && !dr.IsDBNull(dr.GetOrdinal("compensationamount")))
                    objPaymentExportBO.CompensationAmount = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("compensationamount")));

                if (ColumnExists(dr, "deliveredtostakeholder") && !dr.IsDBNull(dr.GetOrdinal("deliveredtostakeholder")))
                    objPaymentExportBO.DeliveredToStakeHolder = dr.GetString(dr.GetOrdinal("deliveredtostakeholder"));

                if (ColumnExists(dr, "delivereddate") && !dr.IsDBNull(dr.GetOrdinal("delivereddate")))
                    objPaymentExportBO.DeliveredDate = (dr.GetDateTime(dr.GetOrdinal("delivereddate"))).ToString(UtilBO.DateFormat);
                if (ColumnExists(dr, "delivereddate") && !dr.IsDBNull(dr.GetOrdinal("delivereddate")))
                {
                    objPaymentExportBO.CreatedDate = dr.GetDateTime(dr.GetOrdinal("delivereddate")).Year.ToString() + "0" + dr.GetDateTime(dr.GetOrdinal("delivereddate")).Month.ToString("00");
                }

                if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objPaymentExportBO.BankID = dr.GetInt32(dr.GetOrdinal("BANKID"));
                if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objPaymentExportBO.BranchID = dr.GetInt32(dr.GetOrdinal("BRANCHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("BANKREFERENCE"))) objPaymentExportBO.BankReference = dr.GetString(dr.GetOrdinal("BANKREFERENCE"));

                if (ColumnExists(dr, "bankname") && !dr.IsDBNull(dr.GetOrdinal("bankname")))
                    objPaymentExportBO.BankName = dr.GetString(dr.GetOrdinal("bankname"));

                if (ColumnExists(dr, "branchname") && !dr.IsDBNull(dr.GetOrdinal("branchname")))
                    objPaymentExportBO.BranchName = dr.GetString(dr.GetOrdinal("branchname"));

                if (ColumnExists(dr, "FUNDREQSTATUS") && !dr.IsDBNull(dr.GetOrdinal("FUNDREQSTATUS")))
                    objPaymentExportBO.FundReqStatus = dr.GetValue(dr.GetOrdinal("FUNDREQSTATUS")).ToString();
                //new
                if (ColumnExists(dr, "BankCode") && !dr.IsDBNull(dr.GetOrdinal("BankCode")))
                    objPaymentExportBO.BankCode = dr.GetValue(dr.GetOrdinal("BankCode")).ToString();

                if (ColumnExists(dr, "FIXEDCOSTCENTRE") && !dr.IsDBNull(dr.GetOrdinal("FIXEDCOSTCENTRE")))
                    objPaymentExportBO.FixedCostCentre = dr.GetValue(dr.GetOrdinal("FIXEDCOSTCENTRE")).ToString();

                if (ColumnExists(dr, "FIXEDCOSTCENTREID") && !dr.IsDBNull(dr.GetOrdinal("FIXEDCOSTCENTREID")))
                    objPaymentExportBO.FixedCostCentreID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FIXEDCOSTCENTREID")).ToString());

                if (ColumnExists(dr, "TReference") && !dr.IsDBNull(dr.GetOrdinal("TReference")))
                    objPaymentExportBO.TReference = dr.GetValue(dr.GetOrdinal("TReference")).ToString();

                if (ColumnExists(dr, "papname") && !dr.IsDBNull(dr.GetOrdinal("papname")))
                    objPaymentExportBO.Papname = dr.GetValue(dr.GetOrdinal("papname")).ToString();

                if (ColumnExists(dr, "PROJECTNAME") && !dr.IsDBNull(dr.GetOrdinal("PROJECTNAME")))
                    objPaymentExportBO.PROJECTNAME = dr.GetValue(dr.GetOrdinal("PROJECTNAME")).ToString();

                if (ColumnExists(dr, "SEGMENTNAME") && !dr.IsDBNull(dr.GetOrdinal("SEGMENTNAME")))
                    objPaymentExportBO.SEGMENTNAME = dr.GetValue(dr.GetOrdinal("SEGMENTNAME")).ToString();
                lstPaymentExportList.Add(objPaymentExportBO);
            }

            dr.Close();

            return lstPaymentExportList;
        }

        /// <summary>
        /// To Map Data Compensation Payment
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private PaymentBO.CompensationPayementBO MapData_CompPayment(IDataReader reader)
        {
            PaymentBO.CompensationPayementBO oCompPaymentBO = new PaymentBO.CompensationPayementBO();

            if (ColumnExists(reader, "cmp_paymentid") && !reader.IsDBNull(reader.GetOrdinal("cmp_paymentid")))
                oCompPaymentBO.CompPaymentId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("cmp_paymentid")));

            if (ColumnExists(reader, "compensationtype") && !reader.IsDBNull(reader.GetOrdinal("compensationtype")))
                oCompPaymentBO.CompensationType = reader.GetString(reader.GetOrdinal("compensationtype"));

            if (ColumnExists(reader, "modeofpaymentid") && !reader.IsDBNull(reader.GetOrdinal("modeofpaymentid")))
                oCompPaymentBO.ModeOfPaymentId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("modeofpaymentid")));

            if (ColumnExists(reader, "modeofpayment") && !reader.IsDBNull(reader.GetOrdinal("modeofpayment")))
                oCompPaymentBO.ModeOfPayment = reader.GetString(reader.GetOrdinal("modeofpayment"));

            if (ColumnExists(reader, "compensationamount") && !reader.IsDBNull(reader.GetOrdinal("compensationamount")))
                oCompPaymentBO.CompensationAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("compensationamount")));

            if (ColumnExists(reader, "deliveredtostakeholder") && !reader.IsDBNull(reader.GetOrdinal("deliveredtostakeholder")))
                oCompPaymentBO.DeliveredToStakeHolder = reader.GetString(reader.GetOrdinal("deliveredtostakeholder"));

            if (ColumnExists(reader, "delivereddate") && !reader.IsDBNull(reader.GetOrdinal("delivereddate")))
                oCompPaymentBO.DeliveredDate = (reader.GetDateTime(reader.GetOrdinal("delivereddate"))).ToString(UtilBO.DateFormat);

            if (!reader.IsDBNull(reader.GetOrdinal("BANKID"))) oCompPaymentBO.BankID = reader.GetInt32(reader.GetOrdinal("BANKID"));
            if (!reader.IsDBNull(reader.GetOrdinal("BRANCHID"))) oCompPaymentBO.BranchID = reader.GetInt32(reader.GetOrdinal("BRANCHID"));
            if (!reader.IsDBNull(reader.GetOrdinal("BANKREFERENCE"))) oCompPaymentBO.BankReference = reader.GetString(reader.GetOrdinal("BANKREFERENCE"));

            if (ColumnExists(reader, "bankname") && !reader.IsDBNull(reader.GetOrdinal("bankname")))
                oCompPaymentBO.BankName = reader.GetString(reader.GetOrdinal("bankname"));

            if (ColumnExists(reader, "branchname") && !reader.IsDBNull(reader.GetOrdinal("branchname")))
                oCompPaymentBO.BranchName = reader.GetString(reader.GetOrdinal("branchname"));


            if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
                oCompPaymentBO.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

            if (ColumnExists(reader, "FUNDREQSTATUS") && !reader.IsDBNull(reader.GetOrdinal("FUNDREQSTATUS")))
                oCompPaymentBO.FundReqStatus = reader.GetValue(reader.GetOrdinal("FUNDREQSTATUS")).ToString();
            //new
            if (ColumnExists(reader, "BankCode") && !reader.IsDBNull(reader.GetOrdinal("BankCode")))
                oCompPaymentBO.BankCode = reader.GetValue(reader.GetOrdinal("BankCode")).ToString();

            if (ColumnExists(reader, "FIXEDCOSTCENTRE") && !reader.IsDBNull(reader.GetOrdinal("FIXEDCOSTCENTRE")))
                oCompPaymentBO.FixedCostCentre = reader.GetValue(reader.GetOrdinal("FIXEDCOSTCENTRE")).ToString();

            if (ColumnExists(reader, "FIXEDCOSTCENTREID") && !reader.IsDBNull(reader.GetOrdinal("FIXEDCOSTCENTREID")))
                oCompPaymentBO.FixedCostCentreID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("FIXEDCOSTCENTREID")).ToString());

            if (ColumnExists(reader, "CMP_BATCHNO") && !reader.IsDBNull(reader.GetOrdinal("CMP_BATCHNO")))
                oCompPaymentBO.BatchNos = reader.GetString(reader.GetOrdinal("CMP_BATCHNO")).Trim().Replace(" ", ", ");

            return oCompPaymentBO;
        }

        /// <summary>
        /// To Send For Approval
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public int SendforApproval(int HHID)
        {
            cnn = new SqlConnection(con);
            int returnResult;
            proc = "USP_TRN_UPD_PAYAPPRSTATUS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("HHID_", HHID);
            returnResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Upadate Status
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="FUNDREQSTATUS"></param>
        /// <returns></returns>
        public int UpdateStatus(int HHID, string FUNDREQSTATUS)
        {
            cnn = new SqlConnection(con);
            int returnResult;
            proc = "USP_TRN_UPD_PAYSTATUS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("HHID_", HHID);
            cmd.Parameters.AddWithValue("FUNDREQSTATUS_", FUNDREQSTATUS);
            returnResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return returnResult;
        }
        #endregion


        #region PAP VALUATION SUMMERY
        /// <summary>
        /// To Get Pap Valuation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PaymentBO getPapValuation(int HHID)
        {
            //Reading Payement Status from the TRN_PAP_VALUATION_SUMMERY table
            PaymentBO oPaymentBO = null;// = new PaymentBO();
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAP_VAL_SUMMARY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("hhid_", HHID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                oPaymentBO = new PaymentBO();

                if (ColumnExists(dr, "PaymentStatus") && !dr.IsDBNull(dr.GetOrdinal("PaymentStatus")))
                    oPaymentBO.PaymentStatus = dr.GetString(dr.GetOrdinal("PaymentStatus"));

                if (ColumnExists(dr, "grandtotal") && !dr.IsDBNull(dr.GetOrdinal("grandtotal")))
                    oPaymentBO.GrandTotal = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("grandtotal")));

                if (ColumnExists(dr, "negotiatedamtapproved") && !dr.IsDBNull(dr.GetOrdinal("negotiatedamtapproved")))
                    oPaymentBO.NegotiatedAmountApproved = dr.GetString(dr.GetOrdinal("negotiatedamtapproved"));

                if (ColumnExists(dr, "negotiatedamount") && !dr.IsDBNull(dr.GetOrdinal("negotiatedamount")))
                    oPaymentBO.NegotiatedAmount = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("negotiatedamount")));
            }
            dr.Close();

            return oPaymentBO;
        }

        /// <summary>
        /// To Update Pap Valutaion
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="PaymentStatus"></param>
        /// <returns></returns>
        public string UpdatePapValutaion(int HHID, string PaymentStatus)
        {
            //Updating Payement Status from the TRN_PAP_VALUATION_SUMMERY table
            string returnResult = string.Empty;
            // PaymentBO ooPaymentBO = new PaymentBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_UPD_PAP_VAL_SUMMARY", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("hhid_", HHID);
                oCmd.Parameters.AddWithValue("compensationtype_", PaymentStatus);
                //oCmd.Parameters.AddWithValue("modeofpaymentid_", oCompPayementBO.ModeOfPaymentId);

                oCmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// To Get File closing Comments
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PaymentBO GetFileclosingComments(int HHID)
        {
            string proc = "USP_TRN_GET_FILECLOCOMMENTS";
            SqlConnection cnn = new SqlConnection(con);
            PaymentBO oPaymentBO = null;

            SqlCommand cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", HHID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oPaymentBO = new PaymentBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("FILECLOSINGCOMMENTS")))
                        oPaymentBO.FILECLOSINGCOMMENTS = dr.GetString((dr.GetOrdinal("FILECLOSINGCOMMENTS")));

                    if (!dr.IsDBNull(dr.GetOrdinal("GRIEVOVERRIDE")))
                        oPaymentBO.GRIEVOVERRIDE = dr.GetString(dr.GetOrdinal("GRIEVOVERRIDE"));

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oPaymentBO;
        }

        /// <summary>
        /// To Save File Closing Comments
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="comments"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string SaveFileclosingComments(int HHID, string comments, Boolean Status)
        {
            //Updating Payement Status from the TRN_PAP_VALUATION_SUMMERY table
            string returnResult = string.Empty;
            // PaymentBO ooPaymentBO = new PaymentBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_UPD_FILECLOCOMMENTS", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("hhid_", HHID);
                oCmd.Parameters.AddWithValue("FILECLOSINGCOMMENTS_", comments);
                oCmd.Parameters.AddWithValue("GRIEVOVERRIDE_", Status.ToString());
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }
        #endregion
    }
}