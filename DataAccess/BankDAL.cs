using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class BankDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
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

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                objBank = new BankBO();

                                if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = dr.GetInt32(dr.GetOrdinal("BankID"));
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

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (bankName != "")
                        cmd.Parameters.Add("bankName_", bankName);
                    else
                        cmd.Parameters.Add("bankName_", DBNull.Value);

                    
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            objBank = new BankBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = dr.GetInt32(dr.GetOrdinal("BankID"));
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
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_BANK";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("bankName", objBank.BankName);
           
            cmd.Parameters.Add("createdby", objBank.CreatedBy);
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
        /// To Get Bank details  By BankID
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public BankBO GetBankByBankID(int bankID)
        {
            proc = "USP_MST_GET_BANKBYID";
            cnn = new OracleConnection(con);
            BankBO objBank = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("bankID_", bankID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objBank = new BankBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) objBank.BankID = dr.GetInt32(dr.GetOrdinal("BankID"));
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
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

            proc = "USP_MST_UPD_BANK";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("bankID", objBank.BankID);
            cmd.Parameters.Add("bankName", objBank.BankName);
           
            cmd.Parameters.Add("updatedby", objBank.UpdatedBy);
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
        /// To delete bank details from database
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public string DeleteBank(int bankID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_BANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("bankid_", bankID);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETEBANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("bankid_", bankID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
