using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_BankDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get PAP Bank By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_BankBO GetPAPBankByID(int householdID)
        {
            proc = "USP_TRN_GET_PAPBANKBYID";
            cnn = new SqlConnection(con);

            PAP_BankBO objPAPBank = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPBank = new PAP_BankBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BANKDETAILID"))) objPAPBank.BankDetailID = dr.GetInt32(dr.GetOrdinal("BANKDETAILID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAPBank.HouseHoldID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objPAPBank.BankID = dr.GetInt32(dr.GetOrdinal("BANKID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objPAPBank.BranchID = dr.GetInt32(dr.GetOrdinal("BRANCHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ACCOUNTNO"))) objPAPBank.AccountNo = dr.GetString(dr.GetOrdinal("ACCOUNTNO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ACCOUNTHOLDERNAME"))) objPAPBank.AccountHolderName = dr.GetString(dr.GetOrdinal("ACCOUNTHOLDERNAME"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objPAPBank;
        }

        /// <summary>
        /// To Update PAP Bank
        /// </summary>
        /// <param name="objPAPBank"></param>
        public void UpdatePAPBank(PAP_BankBO objPAPBank)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PAPBANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("BANKDETAILID_", objPAPBank.BankDetailID);
            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objPAPBank.HouseHoldID);
            cmd.Parameters.AddWithValue("BANKID_", objPAPBank.BankID);
            cmd.Parameters.AddWithValue("BRANCH_", objPAPBank.BranchID);
            cmd.Parameters.AddWithValue("ACCOUNTNO_", objPAPBank.AccountNo);
            cmd.Parameters.AddWithValue("ACCOUNTHOLDERNAME_", objPAPBank.AccountHolderName);
            cmd.Parameters.AddWithValue("CREATEDBY_", objPAPBank.CreatedBy);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objPAPBank.UpdatedBy);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// To Delete PAP Bank
        /// </summary>
        /// <param name="HHID"></param>
        public void DeletePAPBank(int HHID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_PAPBANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("HOUSEHOLDID_", HHID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

    }
}
