using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class BankDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch bank details from database
        /// </summary>
        /// <returns></returns>
        public BankList GetBanks()
        {
            proc = "USP_MST_GET_BANKS";
            
            BankBO objBank = null;

            BankList Banks = new BankList();

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    

                    try
                    {
                        cmd.Connection.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                objBank = new BankBO();

                                if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = (int)dr.GetDecimal(dr.GetOrdinal("BankID"));
                                if (!dr.IsDBNull(dr.GetOrdinal("BankName"))) objBank.BankName = dr.GetString(dr.GetOrdinal("BankName"));

                                Banks.Add(objBank);
                            }

                            dr.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return Banks;
        }
        /// <summary>
        /// To fetch bank details from database based on bankname
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public BankList GetAllBanks(string bankName)
        {
            proc = "USP_MST_GET_ALLBANKS";
            BankBO objBank = null;
            BankList Banks = new BankList();

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (bankName != "")
                        cmd.Parameters.AddWithValue("bankName_", bankName);
                    else
                        cmd.Parameters.AddWithValue("bankName_", DBNull.Value);


                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            objBank = new BankBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = (int)dr.GetDecimal(dr.GetOrdinal("BankID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BankName"))) objBank.BankName = dr.GetString(dr.GetOrdinal("BankName"));
                            if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) objBank.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted")); 

                            Banks.Add(objBank);
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return Banks;
        }
        /// <summary>
        /// To save bank data to database
        /// </summary>
        /// <param name="objBank"></param>
        /// <returns></returns>
        public string AddBank(BankBO objBank)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_BANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("bankName", objBank.BankName);
           
            cmd.Parameters.AddWithValue("createdby", objBank.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To Get Bank details  By BankID
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public BankBO GetBankByBankID(int bankID)
        {
            proc = "USP_MST_GET_BANKBYID";
            cnn = new SqlConnection(con);
            BankBO objBank = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("bankID_", bankID);

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objBank = new BankBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = (int)dr.GetDecimal(dr.GetOrdinal("BankID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BankName"))) objBank.BankName = dr.GetString(dr.GetOrdinal("BankName"));
                    
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objBank;
        }
        /// <summary>
        /// To update bank details into database
        /// </summary>
        /// <param name="objBank"></param>
        /// <returns></returns>
        public string UpdateBank(BankBO objBank)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;

            proc = "USP_MST_UPD_BANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("bankID", Convert.ToSingle(objBank.BankID));
            cmd.Parameters.AddWithValue("bankName", objBank.BankName);
           
            cmd.Parameters.AddWithValue("updatedby", objBank.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To delete bank details from database
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public string DeleteBank(int bankID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_BANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("bankid_", bankID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Bank is already in use. Cannot delete";
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
        /// <summary>
        /// To obsolete bank details
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteBank(int bankID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETEBANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("bankid_", bankID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
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
