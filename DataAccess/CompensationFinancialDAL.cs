using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CompensationFinancialDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        #region Add/Save
        /// <summary>
        /// To Add Compensation Financial
        /// </summary>
        /// <param name="oCompensationFinancial"></param>
        /// <returns></returns>
        public string AddCompensationFinancial(CompensationFinancialBO oCompensationFinancial)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_TRN_INS_CMP_FINANCIALS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HHID_", oCompensationFinancial.HHID);

            //LAND SECTION
            cmd.Parameters.Add("LandValuation_", oCompensationFinancial.LandValuation);
            cmd.Parameters.Add("LandDA_", oCompensationFinancial.LandDA);
            cmd.Parameters.Add("landtotalvaluation_", oCompensationFinancial.LandTotalValuation);
            cmd.Parameters.Add("LandDiffPayment_", oCompensationFinancial.LandDiffPayment);
            cmd.Parameters.Add("LandValComments_", oCompensationFinancial.LandValComments);

            //RESIDENTIAL STRUCTURE SECTION
            cmd.Parameters.Add("ResDepreciatedValue_", oCompensationFinancial.ResDepreciatedValue);
            cmd.Parameters.Add("ResReplacementValue_", oCompensationFinancial.ResReplacementValue);
            cmd.Parameters.Add("ResDA_", oCompensationFinancial.ResDA);
            cmd.Parameters.Add("ResMovingAllowance_", oCompensationFinancial.ResMovingAllowance);
            cmd.Parameters.Add("ResLabourCost_", oCompensationFinancial.ResLabourCost);
            cmd.Parameters.Add("ResPayment_", oCompensationFinancial.ResPayment);
            cmd.Parameters.Add("rescomments_", oCompensationFinancial.ResComments);
            cmd.Parameters.Add("restotalvaluation_", oCompensationFinancial.ResTotalValuation);

            //FIXTURES SECTION
            cmd.Parameters.Add("FixtureValuation_", oCompensationFinancial.FixtureValuation);
            cmd.Parameters.Add("FixtureDA_", oCompensationFinancial.FixtureDA);
            cmd.Parameters.Add("FixtureTotalValuation_", oCompensationFinancial.FixtureTotalValuation);
            cmd.Parameters.Add("FixtureComments_", oCompensationFinancial.FixtureComments);

            //CROP SECTION
            cmd.Parameters.Add("CropValuation_", oCompensationFinancial.CropValuation);
            cmd.Parameters.Add("CropMaxCapCase_", oCompensationFinancial.CropMaxCapCase);
            cmd.Parameters.Add("CropValAftMaxCap_", oCompensationFinancial.CropValAftMaxCap);
            cmd.Parameters.Add("CropDA_", oCompensationFinancial.CropDA);
            cmd.Parameters.Add("croptotalvaluation_", oCompensationFinancial.CropTotalValuation);
            cmd.Parameters.Add("CropComments_", oCompensationFinancial.CropComments);

            //SUMMERY SECTION
            cmd.Parameters.Add("CulturePropValuation_", oCompensationFinancial.CulturePropValuation);
            cmd.Parameters.Add("DamagedCropValuation_", oCompensationFinancial.DamagedCropValuation);
            cmd.Parameters.Add("totalothervaluation_", oCompensationFinancial.TotalOtherValuation);
            cmd.Parameters.Add("NegotiatedAmount_", oCompensationFinancial.NegotiatedAmount);
            cmd.Parameters.Add("LandInKindCompensation_", oCompensationFinancial.LandInKindCompensation);
            cmd.Parameters.Add("resinkindcompensation_", oCompensationFinancial.ResInKindCompensation);
            cmd.Parameters.Add("FacilitationAllowance_", oCompensationFinancial.FacilitationAllowance);

            //COMMON SECTION
            cmd.Parameters.Add("isdeleted_", oCompensationFinancial.IsDeleted);
            cmd.Parameters.Add("createdby_", oCompensationFinancial.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Update Compensation Financial Closing Info
        /// </summary>
        /// <param name="oCompensationFinancial"></param>
        /// <returns></returns>
        public string AddPackageDeliveryInfo(CompensationFinancialBO oCompensationFinancial)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_TRN_CMP_INS_DELIVERY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HHID_", oCompensationFinancial.HHID);
            if (oCompensationFinancial.DeliveredBy != 0)
                cmd.Parameters.Add("deliveredby_", oCompensationFinancial.DeliveredBy);
            else
                cmd.Parameters.Add("deliveredby_", DBNull.Value);

            cmd.Parameters.Add("PAPAction_", oCompensationFinancial.PAPAction);

            if (!string.IsNullOrEmpty(oCompensationFinancial.DeliveryComments))
                cmd.Parameters.Add("deliverycomments_", oCompensationFinancial.DeliveryComments);
            else
                cmd.Parameters.Add("deliverycomments_", DBNull.Value);

            string dt=oCompensationFinancial.DeliveryDate.ToString().Substring(0, 8);
            if (dt == "1/1/0001")
                cmd.Parameters.Add("deliverydate_", DBNull.Value);
            else
                cmd.Parameters.Add("deliverydate_", oCompensationFinancial.DeliveryDate);

            cmd.Parameters.Add("createdby_", oCompensationFinancial.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        #endregion

        #region Get/Fetch

        private bool ColumnExists(IDataReader reader, string columnName)
        {
            string[] ColumnNames = new string[50];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                ColumnNames[i] = reader.GetName(i);
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Get Compensation Financial List
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialList GetCompensationFinancialList(int HHID)
        {
            proc = "USP_TRN_GET_CMP_FINANCIALS";
            cnn = new OracleConnection(con);
            CompensationFinancialBO oCompensationFinancial = null;

            CompensationFinancialList lstCompensationFinancial = new CompensationFinancialList();

            CompensationFinancialBO OCompensationFinancial = new CompensationFinancialBO();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("hhid_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {

                    oCompensationFinancial = new CompensationFinancialBO();
                    oCompensationFinancial = MapData(dr);
                    lstCompensationFinancial.Add(oCompensationFinancial);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstCompensationFinancial;
        }

        /// <summary>
        /// To Get Compensation Financial
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancial(int HHID)
        {
            proc = "USP_TRN_GET_CMP_FINANCIALS";
            cnn = new OracleConnection(con);
            CompensationFinancialBO oCompensationFinancial = null;

            CompensationFinancialList lstCompensationFinancial = new CompensationFinancialList();

            CompensationFinancialBO OCompensationFinancial = new CompensationFinancialBO();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("hhid_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oCompensationFinancial = new CompensationFinancialBO();
                    oCompensationFinancial = MapData(dr);

                    lstCompensationFinancial.Add(oCompensationFinancial);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (lstCompensationFinancial.Count > 0)
                return lstCompensationFinancial[lstCompensationFinancial.Count - 1];
            else return oCompensationFinancial;
        }

        /// <summary>
        /// To Update Compensation Financial Closing Info
        /// </summary>
        /// <param name="objCompensationFinancial"></param>
        /// <returns></returns>
        public string UpdateCompFinancial_ClosingInfo(CompensationFinancialBO objCompensationFinancial)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

            proc = "USP_TRN_CMP_UPD_FINCLOSINGINFO";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("hhid_", objCompensationFinancial.HHID);
            cmd.Parameters.Add("resinkindcompensation_", objCompensationFinancial.ResInKindCompensation);

            cmd.Parameters.Add("updatedby", objCompensationFinancial.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return returnResult;
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private CompensationFinancialBO MapData(IDataReader reader)
        {
            CompensationFinancialBO oCompFinancial = new CompensationFinancialBO();
            try
            {
                if (ColumnExists(reader, "Cmp_FinancialID") && !reader.IsDBNull(reader.GetOrdinal("Cmp_FinancialID")))
                    oCompFinancial.Cmp_FinancialID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Cmp_FinancialID")));

                if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                    oCompFinancial.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

                #region Land Section
                if (ColumnExists(reader, "LandValuation") && !reader.IsDBNull(reader.GetOrdinal("LandValuation")))
                    oCompFinancial.LandValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandValuation")));

                if (ColumnExists(reader, "LANDVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("LANDVALUATIONPAID")))
                    oCompFinancial.LandPaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LANDVALUATIONPAID")));

                if (ColumnExists(reader, "LandDA") && !reader.IsDBNull(reader.GetOrdinal("LandDA")))
                    oCompFinancial.LandDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDA")));

                if (ColumnExists(reader, "LandInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("LandInKindCompensation")))
                    oCompFinancial.LandInKindCompensation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandInKindCompensation")));

                if (ColumnExists(reader, "LandDiffPayment") && !reader.IsDBNull(reader.GetOrdinal("LandDiffPayment")))
                    oCompFinancial.LandDiffPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LandDiffPayment")));

                if (ColumnExists(reader, "LandValComments") && !reader.IsDBNull(reader.GetOrdinal("LandValComments")))
                    oCompFinancial.LandValComments = reader.GetString(reader.GetOrdinal("LandValComments"));

                if (ColumnExists(reader, "LANDTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("LANDTOTALVALUATION")))
                    oCompFinancial.LandTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LANDTOTALVALUATION")));
                #endregion

                #region Residential Structure Section
                if (ColumnExists(reader, "ResDepreciatedValue") && !reader.IsDBNull(reader.GetOrdinal("ResDepreciatedValue")))
                    oCompFinancial.ResDepreciatedValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDepreciatedValue")));

                if (ColumnExists(reader, "RESVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("RESVALUATIONPAID")))
                    oCompFinancial.ResPaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("RESVALUATIONPAID")));

                if (ColumnExists(reader, "ResReplacementValue") && !reader.IsDBNull(reader.GetOrdinal("ResReplacementValue")))
                    oCompFinancial.ResReplacementValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResReplacementValue")));

                if (ColumnExists(reader, "ResDA") && !reader.IsDBNull(reader.GetOrdinal("ResDA")))
                    oCompFinancial.ResDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResDA")));

                if (ColumnExists(reader, "ResMovingAllowance") && !reader.IsDBNull(reader.GetOrdinal("ResMovingAllowance")))
                    oCompFinancial.ResMovingAllowance = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResMovingAllowance")));

                if (ColumnExists(reader, "ResLabourCost") && !reader.IsDBNull(reader.GetOrdinal("ResLabourCost")))
                    oCompFinancial.ResLabourCost = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResLabourCost")));

                if (ColumnExists(reader, "ResPayment") && !reader.IsDBNull(reader.GetOrdinal("ResPayment")))
                    oCompFinancial.ResPayment = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ResPayment")));

                if (ColumnExists(reader, "ResInKindCompensation") && !reader.IsDBNull(reader.GetOrdinal("ResInKindCompensation")))
                {
                    //  string testing = reader.GetString(reader.GetOrdinal("ResInKindCompensation"));
                    oCompFinancial.ResInKindCompensation = reader.GetString(reader.GetOrdinal("ResInKindCompensation"));
                }

                if (ColumnExists(reader, "ResComments") && !reader.IsDBNull(reader.GetOrdinal("ResComments")))
                    oCompFinancial.ResComments = reader.GetString(reader.GetOrdinal("ResComments"));

                if (ColumnExists(reader, "RESTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("RESTOTALVALUATION")))
                    oCompFinancial.ResTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("RESTOTALVALUATION")));
                #endregion

                #region Fixture Section
                if (ColumnExists(reader, "FixtureValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureValuation")))
                    oCompFinancial.FixtureValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureValuation")));

                if (ColumnExists(reader, "FIXTUREVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("FIXTUREVALUATIONPAID")))
                    oCompFinancial.FixturePaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FIXTUREVALUATIONPAID")));
                
                if (ColumnExists(reader, "FixtureDA") && !reader.IsDBNull(reader.GetOrdinal("FixtureDA")))
                    oCompFinancial.FixtureDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureDA")));

                if (ColumnExists(reader, "FixtureTotalValuation") && !reader.IsDBNull(reader.GetOrdinal("FixtureTotalValuation")))
                    oCompFinancial.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FixtureTotalValuation")));

                if (ColumnExists(reader, "FixtureComments") && !reader.IsDBNull(reader.GetOrdinal("FixtureComments")))
                    oCompFinancial.FixtureComments = reader.GetString(reader.GetOrdinal("FixtureComments"));

                if (ColumnExists(reader, "FIXTURETOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("FIXTURETOTALVALUATION")))
                    oCompFinancial.FixtureTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FIXTURETOTALVALUATION")));
                #endregion

                #region Crops Section
                if (ColumnExists(reader, "CropValuation") && !reader.IsDBNull(reader.GetOrdinal("CropValuation")))
                    oCompFinancial.CropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValuation")));

                if (ColumnExists(reader, "CROPVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("CROPVALUATIONPAID")))
                    oCompFinancial.CropPaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CROPVALUATIONPAID")));

                if (ColumnExists(reader, "CropMaxCapCase") && !reader.IsDBNull(reader.GetOrdinal("CropMaxCapCase")))
                    oCompFinancial.CropMaxCapCase = reader.GetString(reader.GetOrdinal("CropMaxCapCase"));

                if (ColumnExists(reader, "CropValAftMaxCap") && !reader.IsDBNull(reader.GetOrdinal("CropValAftMaxCap")))
                    oCompFinancial.CropValAftMaxCap = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropValAftMaxCap")));

                if (ColumnExists(reader, "CropDA") && !reader.IsDBNull(reader.GetOrdinal("CropDA")))
                    oCompFinancial.CropDA = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CropDA")));

                if (ColumnExists(reader, "CropComments") && !reader.IsDBNull(reader.GetOrdinal("CropComments")))
                    oCompFinancial.CropComments = reader.GetString(reader.GetOrdinal("CropComments"));

                if (ColumnExists(reader, "CROPTOTALVALUATION") && !reader.IsDBNull(reader.GetOrdinal("CROPTOTALVALUATION")))
                    oCompFinancial.CropTotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CROPTOTALVALUATION")));
                #endregion

                #region Summery Section
                if (ColumnExists(reader, "CulturePropValuation") && !reader.IsDBNull(reader.GetOrdinal("CulturePropValuation")))
                    oCompFinancial.CulturePropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CulturePropValuation")));

                if (ColumnExists(reader, "DamagedCropValuation") && !reader.IsDBNull(reader.GetOrdinal("DamagedCropValuation")))
                    oCompFinancial.DamagedCropValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DamagedCropValuation")));

                if (ColumnExists(reader, "CULTUREVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("CULTUREVALUATIONPAID")))
                    oCompFinancial.CulturePropPaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CULTUREVALUATIONPAID")));

                if (ColumnExists(reader, "DAMAGEDCROPVALUATIONPAID") && !reader.IsDBNull(reader.GetOrdinal("DAMAGEDCROPVALUATIONPAID")))
                    oCompFinancial.DamagedCropPaidValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("DAMAGEDCROPVALUATIONPAID")));

                if (ColumnExists(reader, "TotalValuation"))
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("TotalValuation")))
                        oCompFinancial.TotalValuation = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("TotalValuation")));
                }

                if (ColumnExists(reader, "NegotiatedAmount") && !reader.IsDBNull(reader.GetOrdinal("NegotiatedAmount")))
                    oCompFinancial.NegotiatedAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("NegotiatedAmount")));

                if (ColumnExists(reader, "NEGOTIATEDAMOUNTPAID") && !reader.IsDBNull(reader.GetOrdinal("NEGOTIATEDAMOUNTPAID")))
                    oCompFinancial.NegotiatedAmountPaid = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("NEGOTIATEDAMOUNTPAID")));

                if (ColumnExists(reader, "FACILITATIONALLOWANCE") && !reader.IsDBNull(reader.GetOrdinal("FACILITATIONALLOWANCE")))
                    oCompFinancial.FacilitationAllowance = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FACILITATIONALLOWANCE")));

                if (ColumnExists(reader, "FACILITATIONALLOWANCEPAID") && !reader.IsDBNull(reader.GetOrdinal("FACILITATIONALLOWANCEPAID")))
                    oCompFinancial.FacilitationAllowancePaid = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("FACILITATIONALLOWANCEPAID")));
                
                #endregion

                #region Common Section
                /* if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
                oCompFinancial.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

            if (ColumnExists(reader, "CreatedDate") && !reader.IsDBNull(reader.GetOrdinal("CreatedDate")))
                oCompFinancial.CreatedDate = reader.GetString(reader.GetOrdinal("CreatedDate"));

            if (ColumnExists(reader, "CreatedBy") && !reader.IsDBNull(reader.GetOrdinal("CreatedBy")))
                oCompFinancial.CreatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("CreatedBy")));

            if (ColumnExists(reader, "UpdatedDate") && !reader.IsDBNull(reader.GetOrdinal("UpdatedDate")))
                oCompFinancial.UpdatedDate = reader.GetString(reader.GetOrdinal("UpdatedDate"));

            if (ColumnExists(reader, "UpdatedBy") && !reader.IsDBNull(reader.GetOrdinal("UpdatedBy")))
                oCompFinancial.UpdatedBy = Convert.ToInt32(reader.GetString(reader.GetOrdinal("UpdatedBy")));*/
                #endregion

                if (ColumnExists(reader, "LAND_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("LAND_APPROVAL_STATUS")))
                    oCompFinancial.Land_Approval_Status = reader.GetString(reader.GetOrdinal("LAND_APPROVAL_STATUS"));

                if (ColumnExists(reader, "REPLACMENT_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("REPLACMENT_APPROVAL_STATUS")))
                    oCompFinancial.Replacment_Approval_Status = reader.GetString(reader.GetOrdinal("REPLACMENT_APPROVAL_STATUS"));

                if (ColumnExists(reader, "FIXTURE_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("FIXTURE_APPROVAL_STATUS")))
                    oCompFinancial.Fixture_Approval_Status = reader.GetString(reader.GetOrdinal("FIXTURE_APPROVAL_STATUS"));

                if (ColumnExists(reader, "CROP_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("CROP_APPROVAL_STATUS")))
                    oCompFinancial.Crop_Approval_Status = reader.GetString(reader.GetOrdinal("CROP_APPROVAL_STATUS"));

                //if (ColumnExists(reader, "LandValComments") && !reader.IsDBNull(reader.GetOrdinal("LandValComments")))
                //    oCompFinancial.LandValComments = reader.GetString(reader.GetOrdinal("LandValComments"));

                if (ColumnExists(reader, "CULTURE_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("CULTURE_APPROVAL_STATUS")))
                    oCompFinancial.Culture_Approval_Status = reader.GetString(reader.GetOrdinal("CULTURE_APPROVAL_STATUS"));

                if (ColumnExists(reader, "DAMAGED_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("DAMAGED_APPROVAL_STATUS")))
                    oCompFinancial.Damaged_Approval_Status = reader.GetString(reader.GetOrdinal("DAMAGED_APPROVAL_STATUS"));

                if (!reader.IsDBNull(reader.GetOrdinal("FACI_APPROVAL_STATUS")))
                    oCompFinancial.Facilitation_Approval_Status = reader.GetValue(reader.GetOrdinal("FACI_APPROVAL_STATUS")).ToString();

                if (ColumnExists(reader, "FINAL_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("FINAL_APPROVAL_STATUS")))
                    oCompFinancial.Final_Approval_Status = reader.GetString(reader.GetOrdinal("FINAL_APPROVAL_STATUS"));

                if (ColumnExists(reader, "NEGO_AMOUNT_APPROVAL_STATUS") && !reader.IsDBNull(reader.GetOrdinal("NEGO_AMOUNT_APPROVAL_STATUS")))
                    oCompFinancial.Nego_Amount_Approval_Status = reader.GetString(reader.GetOrdinal("NEGO_AMOUNT_APPROVAL_STATUS"));
            }
            catch (Exception ex)
            { throw ex; }

            return oCompFinancial;
        }

        /// <summary>
        /// To Get Compensation Financial By ID
        /// </summary>
        /// <param name="CompensationFinancialID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancialByID(int CompensationFinancialID)
        {
            proc = "USP_MST_GET_CompensationFinancialBYID";
            cnn = new OracleConnection(con);
            CompensationFinancialBO objCompensationFinancial = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CompensationFinancialID_", CompensationFinancialID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCompensationFinancial = new CompensationFinancialBO();
                    objCompensationFinancial = MapData(dr);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objCompensationFinancial;
        }

        /// <summary>
        /// To get Package Delivery Info
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO getPackageDeliveryInfo(int HHID)
        {
            proc = "USP_TRN_CMP_GET_DELIVERY";
            cnn = new OracleConnection(con);
            CompensationFinancialBO objCompensationFinancial = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("hhid_", HHID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCompensationFinancial = new CompensationFinancialBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("Cmp_DeliveryId")))
                        objCompensationFinancial.Cmp_DeliveryId = dr.GetInt32(dr.GetOrdinal("Cmp_DeliveryId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                        objCompensationFinancial.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DeliveryDate")))
                        objCompensationFinancial.DeliveryDate = dr.GetDateTime(dr.GetOrdinal("DeliveryDate"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DeliveredBy")))
                        objCompensationFinancial.DeliveredBy = dr.GetInt32(dr.GetOrdinal("DeliveredBy"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PAPAction")))
                        objCompensationFinancial.PAPAction = dr.GetString(dr.GetOrdinal("PAPAction"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DeliveryComments")))
                        objCompensationFinancial.DeliveryComments = dr.GetString(dr.GetOrdinal("DeliveryComments"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CreatedDate")))
                        objCompensationFinancial.DeliveryDate = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("CreatedDate")));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objCompensationFinancial;
        }

        /// <summary>
        /// To Update Compensation Financial Payment
        /// </summary>
        /// <param name="objCompensationFinancialBO"></param>
        public void UpdateCompensationFinancialPayment(CompensationFinancialBO objCompensationFinancialBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

            proc = "USP_TRN_UPD_CMP_FIN_PAYMENT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HHID_", objCompensationFinancialBO.HHID);
            cmd.Parameters.Add("CMP_PAYMENTID_", objCompensationFinancialBO.CompPaymentId);
            cmd.Parameters.Add("LANDVALUATIONPAID_", objCompensationFinancialBO.LandPaidValuation);
            cmd.Parameters.Add("RESVALUATIONPAID_", objCompensationFinancialBO.ResPaidValuation);
            cmd.Parameters.Add("FIXTUREVALUATIONPAID_", objCompensationFinancialBO.FixturePaidValuation);
            cmd.Parameters.Add("CROPVALUATIONPAID_", objCompensationFinancialBO.CropPaidValuation);
            cmd.Parameters.Add("CULTUREVALUATIONPAID_", objCompensationFinancialBO.CulturePropPaidValuation);
            cmd.Parameters.Add("DAMAGEDCROPVALUATIONPAID_", objCompensationFinancialBO.DamagedCropPaidValuation);
            cmd.Parameters.Add("FACILITATIONALLOWANCEPAID_", objCompensationFinancialBO.FacilitationAllowancePaid);
            cmd.Parameters.Add("NEGOTIATEDAMOUNTPAID_", objCompensationFinancialBO.NegotiatedAmountPaid);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// To Get Compensation Financial By Id
        /// </summary>
        /// <param name="CompPaymentId"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancialById(int CompPaymentId, int HHID)
        {
            proc = "USP_TRN_GET_CMP_FIN_PAYBYID";
            cnn = new OracleConnection(con);
            CompensationFinancialBO objCompensationFinancial = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("CMP_PAYMENTID_", CompPaymentId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCompensationFinancial = new CompensationFinancialBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("CMP_PAYMENTID")))
                        objCompensationFinancial.CompPaymentId = dr.GetInt32(dr.GetOrdinal("CMP_PAYMENTID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("HHID")))
                        objCompensationFinancial.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));

                    if (ColumnExists(dr, "LANDVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("LANDVALUATIONPAID")))
                        objCompensationFinancial.LandPaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("LANDVALUATIONPAID")));

                    if (ColumnExists(dr, "RESVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("RESVALUATIONPAID")))
                        objCompensationFinancial.ResPaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("RESVALUATIONPAID")));

                    if (ColumnExists(dr, "FIXTUREVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("FIXTUREVALUATIONPAID")))
                        objCompensationFinancial.FixturePaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FIXTUREVALUATIONPAID")));

                    if (ColumnExists(dr, "CROPVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("CROPVALUATIONPAID")))
                        objCompensationFinancial.CropPaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CROPVALUATIONPAID")));

                    if (ColumnExists(dr, "CULTUREVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("CULTUREVALUATIONPAID")))
                        objCompensationFinancial.CulturePropPaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CULTUREVALUATIONPAID")));

                    if (ColumnExists(dr, "DAMAGEDCROPVALUATIONPAID") && !dr.IsDBNull(dr.GetOrdinal("DAMAGEDCROPVALUATIONPAID")))
                        objCompensationFinancial.DamagedCropPaidValuation = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DAMAGEDCROPVALUATIONPAID")));

                    if (ColumnExists(dr, "NEGOTIATEDAMOUNTPAID") && !dr.IsDBNull(dr.GetOrdinal("NEGOTIATEDAMOUNTPAID")))
                        objCompensationFinancial.NegotiatedAmountPaid = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("NEGOTIATEDAMOUNTPAID")));

                    if (ColumnExists(dr, "FACILITATIONALLOWANCEPAID") && !dr.IsDBNull(dr.GetOrdinal("FACILITATIONALLOWANCEPAID")))
                        objCompensationFinancial.FacilitationAllowancePaid = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FACILITATIONALLOWANCEPAID")));

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objCompensationFinancial;
        }
        #endregion       
    }
}
