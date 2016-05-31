using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class PAPliveliwoodAfterDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get Livelihood Items By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAPliveliwoodAfterList GetLivelihoodItemsByID(int householdID)
        {
            proc = "USP_TRN_GET_LIVELIHOODAFTER";
            cnn = new OracleConnection(con);
            PAPliveliwoodAfterList LivelihoodItems = new PAPliveliwoodAfterList();
            PAPLiveliwoodAfter objLivelihood = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HOUSEHOLDID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objLivelihood = new PAPLiveliwoodAfter();

                    //if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objLivelihood.LivelihoodItemID = dr.GetInt32(dr.GetOrdinal("LIVELIHOODITEMID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objLivelihood.HouseHoldID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Cash"))) objLivelihood.Cash = dr.GetDecimal(dr.GetOrdinal("Cash"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CAPTUREDDATE"))) objLivelihood.CAPTUREDDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("CAPTUREDDATE")));

                    LivelihoodItems.Add(objLivelihood);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return LivelihoodItems;
        }

        /// <summary>
        /// To Update Livelihood
        /// </summary>
        /// <param name="LivelihoodItems"></param>
        public string UpdateLivelihood(PAPliveliwoodAfterList LivelihoodItems)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

            proc = "USP_TRN_UPD_LIVELIHOODAFTER";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("ID_", "");
            cmd.Parameters.Add("LIVELIHOOD_ITEMID_", "");
            cmd.Parameters.Add("HOUSEHOLDID_", "");
            cmd.Parameters.Add("CASH_", "");
            cmd.Parameters.Add("INKIND_", "");
            cmd.Parameters.Add("CREATEDBY_", "");
            cmd.Parameters.Add("UPDATEDBY_", "");
            cmd.Parameters.Add("CAPTUREDDATE_", "");
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            foreach (PAPLiveliwoodAfter objLivelihood in LivelihoodItems)
            {
                cmd.Parameters["ID_"].Value = objLivelihood.LID;
                cmd.Parameters["LIVELIHOOD_ITEMID_"].Value = objLivelihood.LivelihoodItemID;
                cmd.Parameters["HOUSEHOLDID_"].Value = objLivelihood.HouseHoldID;
                cmd.Parameters["CASH_"].Value = objLivelihood.Cash;
                cmd.Parameters["INKIND_"].Value = objLivelihood.InKind;
                cmd.Parameters["CREATEDBY_"].Value = objLivelihood.CreatedBy;
                cmd.Parameters["UPDATEDBY_"].Value = objLivelihood.UpdatedBy;
                cmd.Parameters["CAPTUREDDATE_"].Value = Convert.ToDateTime(objLivelihood.CAPTUREDDATE).ToString(UtilBO.DateFormatDB);
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null && cmd.Parameters["errorMessage_"].Value.ToString().ToLower() != "null" && cmd.Parameters["errorMessage_"].Value.ToString().Trim() != "")
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }

            cmd.Connection.Close();
            return returnResult;
        }


        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public PAPliveliwoodAfterList GetLivelihood()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETLIVELIHOOD";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PAPLiveliwoodAfter objLivelihood = null;
            PAPliveliwoodAfterList Livelihoods = new PAPliveliwoodAfterList();

            while (dr.Read())
            {
                objLivelihood = new PAPLiveliwoodAfter();
                objLivelihood.LivelihoodItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ITEMID")));
                objLivelihood.ITEMNAME = dr.GetString(dr.GetOrdinal("ITEMNAME"));

                Livelihoods.Add(objLivelihood);
            }

            dr.Close();

            return Livelihoods;
        }
        public PAPliveliwoodAfterList GetLivelihoodItemsByIDCD(int householdID,string CaptDate)
        {
            proc = "USP_TRN_GET_LIVHOODABYID";
            cnn = new OracleConnection(con);
            PAPliveliwoodAfterList LivelihoodItems = new PAPliveliwoodAfterList();
            PAPLiveliwoodAfter objLivelihood = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HOUSEHOLDID_", householdID);
            cmd.Parameters.Add("CAPTUREDDATE_", Convert.ToDateTime(CaptDate).ToString(UtilBO.DateFormatDB));
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objLivelihood = new PAPLiveliwoodAfter();
                    if (!dr.IsDBNull(dr.GetOrdinal("ID"))) objLivelihood.LID = dr.GetInt32(dr.GetOrdinal("ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIVELIHOOD_ITEMID"))) objLivelihood.LivelihoodItemID = dr.GetInt32(dr.GetOrdinal("LIVELIHOOD_ITEMID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objLivelihood.HouseHoldID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CASH"))) objLivelihood.Cash = dr.GetDecimal(dr.GetOrdinal("CASH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INKIND"))) objLivelihood.InKind = dr.GetString(dr.GetOrdinal("INKIND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CAPTUREDDATE"))) objLivelihood.CAPTUREDDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("CAPTUREDDATE")));

                    LivelihoodItems.Add(objLivelihood);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return LivelihoodItems;
        }
        public string DeleteLiveliHood(int HHID, string CaptDate)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_DEL_LIVHOODABYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("HOUSEHOLDID_", HHID);
                myCommand.Parameters.Add("CAPTUREDDATE_", CaptDate);
                //myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //if (myCommand.Parameters["errorMessage_"].Value != null)
                //    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Item is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

    }
}
