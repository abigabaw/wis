﻿using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class FinalValuationDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        #region GET DATA
        public FinalValuationBO getFinalValuation(int HHID)
        {
            proc = "USP_TRN_GET_FINALVALUATION";
            cnn = new SqlConnection(con);
          
            FinalValuationBO objFinalValuation = null;

            FinalValuationList lstFinalValuation = new FinalValuationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", HHID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFinalValuation = new FinalValuationBO();
                    objFinalValuation = MapData(dr);

                   // lstFinalValuation.Add(objFinalValuation);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objFinalValuation;
        }

        public FinalValuationList getFinalValuationById(int HHID)
        {
            proc = "USP_TRN_GET_FINALVALUATION";
            cnn = new SqlConnection(con);

            FinalValuationBO objFinalValuation = null;

            FinalValuationList lstFinalValuation = new FinalValuationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("hhid_", HHID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFinalValuation = new FinalValuationBO();
                    objFinalValuation = MapData(dr);

                    lstFinalValuation.Add(objFinalValuation);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private FinalValuationBO MapData(IDataReader reader)
        {
            FinalValuationBO oFinalValuation = new FinalValuationBO();
            try
            {
                if (ColumnExists(reader, "Val_SummaryID") && !reader.IsDBNull(reader.GetOrdinal("Val_SummaryID")))
                    oFinalValuation.Val_SummaryID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Val_SummaryID")));

                if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                    oFinalValuation.HouseholdID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

                if (ColumnExists(reader, "cropvalue") && !reader.IsDBNull(reader.GetOrdinal("cropvalue")))
                    oFinalValuation.CropValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("cropvalue")));

                if (ColumnExists(reader, "culturalpropvalue") && !reader.IsDBNull(reader.GetOrdinal("culturalpropvalue")))
                    oFinalValuation.CulturalpropertyValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("culturalpropvalue")));

                if (ColumnExists(reader, "damagedcropvalue") && !reader.IsDBNull(reader.GetOrdinal("damagedcropvalue")))
                    oFinalValuation.DamagedcropValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("damagedcropvalue")));

                if (ColumnExists(reader, "fixturevalue") && !reader.IsDBNull(reader.GetOrdinal("fixturevalue")))
                    oFinalValuation.FixtureValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("fixturevalue")));

                //if (ColumnExists(reader, "grandtotal") && !reader.IsDBNull(reader.GetOrdinal("grandtotal")))
                //    oFinalValuation.GrandtotalValue = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("grandtotal"))); 

                if (ColumnExists(reader, "housevalue") && !reader.IsDBNull(reader.GetOrdinal("housevalue")))
                    oFinalValuation.HouseValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("housevalue")));
               
                if (ColumnExists(reader, "landvalue") && !reader.IsDBNull(reader.GetOrdinal("landvalue")))
                    oFinalValuation.LandValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("landvalue")));

                if (ColumnExists(reader, "negotiatedamount") && !reader.IsDBNull(reader.GetOrdinal("negotiatedamount")))
                    oFinalValuation.NegotiatedAmount = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("negotiatedamount")));

                if (ColumnExists(reader, "ReplacementValue") && !reader.IsDBNull(reader.GetOrdinal("ReplacementValue")))
                    oFinalValuation.ReplacementValue = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("ReplacementValue")));

                if (ColumnExists(reader, "ValsummaryComments") && !reader.IsDBNull(reader.GetOrdinal("ValsummaryComments")))
                {
                    oFinalValuation.ValsummaryComments = reader.GetString(reader.GetOrdinal("ValsummaryComments"));
                }

                if (ColumnExists(reader, "LABOURCOST") && !reader.IsDBNull(reader.GetOrdinal("LABOURCOST")))
                {
                    oFinalValuation.ResLabourCost = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("LABOURCOST")));
                }

                if (ColumnExists(reader, "GOUAllowance") && !reader.IsDBNull(reader.GetOrdinal("GOUAllowance")))
                {
                    oFinalValuation.GOUAllowance = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("GOUAllowance")));
                }

                if (ColumnExists(reader, "CROPMAXCAPCASE") && !reader.IsDBNull(reader.GetOrdinal("CROPMAXCAPCASE")))
                {
                    oFinalValuation.Crop_Max_Cap_Case = reader.GetString(reader.GetOrdinal("CROPMAXCAPCASE"));
                }

                if (ColumnExists(reader, "CROPVALAFTMAXCAP") && !reader.IsDBNull(reader.GetOrdinal("CROPVALAFTMAXCAP")))
                {
                    oFinalValuation.Crop_Val_Aft_Max_Cap = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("CROPVALAFTMAXCAP")));
                }

                decimal total = ((oFinalValuation.CropValue) + (oFinalValuation.CulturalpropertyValue) + (oFinalValuation.DamagedcropValue) + (oFinalValuation.FixtureValue) + (oFinalValuation.ReplacementValue) +  (oFinalValuation.LandValue));
                if (total != 0)
                {
                    oFinalValuation.GrandtotalValue = total;
                }
                else
                {
                    oFinalValuation.GrandtotalValue = 0;
                }
                //  grandTextBox.Text = (cropTextBox.Text + landTextBox.Text + fixturesTextBox.Text + houseTextBox.Text + replacementTextBox.Text + damagedTextBox.Text + culturalTextBox.Text);
                   
            }
            catch (Exception ex)
            { throw ex; }

            return oFinalValuation;
        }

        private bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="Finalvaluationobj"></param>
        /// <returns></returns>
        public int Insert(FinalValuationBO Finalvaluationobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_FINALVALUATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("VAL_SUMMARYID", Finalvaluationobj.Val_SummaryID);
                dcmd.Parameters.AddWithValue("HHID", Finalvaluationobj.HouseholdID);
                dcmd.Parameters.AddWithValue("CROPVALUE", Finalvaluationobj.CropValue);
                //Max Cap Value
                dcmd.Parameters.AddWithValue("CROPMAXCAPCASE_", Finalvaluationobj.Crop_Max_Cap_Case);
                dcmd.Parameters.AddWithValue("CROPVALAFTMAXCAP_", Finalvaluationobj.Crop_Val_Aft_Max_Cap);
                //End Max Cap Value
                dcmd.Parameters.AddWithValue("LANDVALUE", Finalvaluationobj.LandValue);
                dcmd.Parameters.AddWithValue("FIXTUREVALUE", Finalvaluationobj.FixtureValue);
              //  dcmd.Parameters.AddWithValue("HOUSEVALUE", Finalvaluationobj.HouseValue);
                dcmd.Parameters.AddWithValue("REPLACEMENTVALUE", Finalvaluationobj.ReplacementValue);
                dcmd.Parameters.AddWithValue("DAMAGEDCROPVALUE", Finalvaluationobj.DamagedcropValue);
                dcmd.Parameters.AddWithValue("CULTURALPROPVALUE", Finalvaluationobj.CulturalpropertyValue);
                dcmd.Parameters.AddWithValue("GRANDTOTAL", Finalvaluationobj.GrandtotalValue);
                dcmd.Parameters.AddWithValue("NEGOTIATEDAMOUNT", Finalvaluationobj.NegotiatedAmount);
                dcmd.Parameters.AddWithValue("VALSUMMARYCOMMENTS", Finalvaluationobj.ValsummaryComments);
                dcmd.Parameters.AddWithValue("CREATEDBY", Finalvaluationobj.CreatedBy);
              dcmd.Parameters.AddWithValue("UPDATEDBY", Finalvaluationobj.CreatedBy);

                return dcmd.ExecuteNonQuery();

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
        }
        /// <summary>
        /// Approval Change request Status
        /// </summary>
        /// <param name="objFinalValuationBO"></param>
        /// <returns></returns>
        public FinalValuationBO ApprovalChangerequestStatus(FinalValuationBO objFinalValuationBO)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_APPROVALVALUEPENDING"; //USP_TRN_APPROVALSTATUSPENDING
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectedId_", objFinalValuationBO.ProjectedId);
            cmd.Parameters.AddWithValue("Workflowcode_", objFinalValuationBO.Workflowcode);
            cmd.Parameters.AddWithValue("HHID_", objFinalValuationBO.HhId);
            cmd.Parameters.AddWithValue("PageCode_", objFinalValuationBO.PageCode);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FinalValuationBO objTrn_Pap_FinalValuation = null;
            while (dr.Read())
            {
                objTrn_Pap_FinalValuation = new FinalValuationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("STATUSID"))) objTrn_Pap_FinalValuation.ApproverStatus = dr.GetInt32(dr.GetOrdinal("STATUSID"));
                if (!dr.IsDBNull(dr.GetOrdinal("NEGOTIATEDAMTAPPROVED"))) objTrn_Pap_FinalValuation.IsFinal = dr.GetString(dr.GetOrdinal("NEGOTIATEDAMTAPPROVED"));
                if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) objTrn_Pap_FinalValuation.Comments = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
            }
            dr.Close();
            return objTrn_Pap_FinalValuation;
        }
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="objFinalValuationBO"></param>
        /// <returns></returns>
        public int SaveNogotiatedAmount(FinalValuationBO objFinalValuationBO)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_FINVALNOGAMOUNT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HOUSEHOLDID_", objFinalValuationBO.HhId);
                dcmd.Parameters.AddWithValue("NEGOTIATEDAMOUNT_", objFinalValuationBO.NegotiatedAmount);
                return dcmd.ExecuteNonQuery();
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
        }
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="NegotiatedAmount"></param>
        /// <param name="NegType"></param>
        /// <returns></returns>
        public int SaveNogotiatedAmountIndividual(int HHID, decimal NegotiatedAmount, string NegType)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_FINVALNOGAMTIND", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HOUSEHOLDID_", HHID);
                dcmd.Parameters.AddWithValue("NEGOTIATEDAMOUNT_", NegotiatedAmount);
                dcmd.Parameters.AddWithValue("NEGAMTTYPE_", NegType);
                return dcmd.ExecuteNonQuery();
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
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public FinalValuationBO getNegIndValuation(int HHID)
        {
            proc = "USP_TRN_GET_FINVALIND";
            cnn = new SqlConnection(con);

            FinalValuationBO objFinalValuation = null;

            FinalValuationList lstFinalValuation = new FinalValuationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", HHID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFinalValuation = new FinalValuationBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("CROPVALUENEGAMT"))) objFinalValuation.CropNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CROPVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("LANDVALUENEGAMT"))) objFinalValuation.LandNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("LANDVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("FIXTUREVALUENEGAMT"))) objFinalValuation.FixtureNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FIXTUREVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("REPLACEMENTVALUENEGAMT"))) objFinalValuation.ReplacementNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("REPLACEMENTVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DAMAGEDCROPVALUENEGAMT"))) objFinalValuation.DamagedcropNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DAMAGEDCROPVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("CULTURALPROPVALUENEGAMT"))) objFinalValuation.CulturalpropertyNegValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CULTURALPROPVALUENEGAMT")));
                    if (!dr.IsDBNull(dr.GetOrdinal("CROPVALUENEGAMTAPPRVD"))) objFinalValuation.CropNegValueAppStatus = dr.GetString(dr.GetOrdinal("CROPVALUENEGAMTAPPRVD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LANDVALUENEGAMTAPPRVD"))) objFinalValuation.LandNegValueAppStatus = dr.GetString(dr.GetOrdinal("LANDVALUENEGAMTAPPRVD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FIXTUREVALUENEGAMTAPPRVD"))) objFinalValuation.FixtureNegValueAppStatus = dr.GetString(dr.GetOrdinal("FIXTUREVALUENEGAMTAPPRVD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REPLACEMENTVALUENEGAMTAPPRVD"))) objFinalValuation.ReplacementNegValueAppStatus = dr.GetString(dr.GetOrdinal("REPLACEMENTVALUENEGAMTAPPRVD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DAMAGEDCROPVALUENEGAMTAPPRVD"))) objFinalValuation.DamagedcropNegValueAppStatus = dr.GetString(dr.GetOrdinal("DAMAGEDCROPVALUENEGAMTAPPRVD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CULTURALPROPVALUENEGAMTAPPRVD"))) objFinalValuation.CulturalpropertyNegValueAppStatus = dr.GetString(dr.GetOrdinal("CULTURALPROPVALUENEGAMTAPPRVD"));

                    // lstFinalValuation.Add(objFinalValuation);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objFinalValuation;
        }

    }
}
