using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LoanDAL
    {
        /// <summary>
        /// To Get Loan
        /// </summary>
        /// <param name="EncumbrancepurposeName"></param>
        /// <returns></returns>
        public LoanList GetLoan(string EncumbrancepurposeName)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ENCUMBRANCEPURPOSE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (EncumbrancepurposeName.ToString() == "")
            {
                cmd.Parameters.Add("@EncumbrancepurposeNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@EncumbrancepurposeNameIN", EncumbrancepurposeName.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LoanBO objLoan = null;
            LoanList Loan = new LoanList();
            while (dr.Read())
            {
                objLoan = new LoanBO();
                objLoan.EncumbranceId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ENCUMBRANCEID")));
                objLoan.Encumbrancepurpose = dr.GetString(dr.GetOrdinal("ENCUMBRANCEPURPOSE"));
                objLoan.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                Loan.Add(objLoan);
            }
            dr.Close();
            return Loan;
        }

        /// <summary>
        /// To Add Loan
        /// </summary>
        /// <param name="objLoan"></param>
        /// <returns></returns>
        public string AddLoan(LoanBO objLoan)
        {
            string result = "";

            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_INS_ENCUMBRANCEPURPOSE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@ENCUMBRANCEPURPOSENameIN", objLoan.Encumbrancepurpose);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objLoan.CreatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            myConnection.Close();

            return result;
        }

        //public int DeleteLoan(int EncumbrancepurposeId)
        //{
        //    int result = 0;

        //    OracleConnection myConnection;
        //    OracleCommand myCommand;
        //    myConnection = new OracleConnection(AppConfiguration.ConnectionString);
        //    myCommand = new OracleCommand("USP_MST_DEL_ENCUMBRANCEPURPOSE", myConnection);
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.Parameters.Add("@ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);

        //    myConnection.Open();
        //    result = myCommand.ExecuteNonQuery();
        //    myConnection.Close();

        //    return result;
        //}

        /// <summary>
        /// To Delete Loan
        /// </summary>
        /// <param name="EncumbrancepurposeId"></param>
        /// <returns></returns>
        public string DeleteLoan(int EncumbrancepurposeId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_ENCUMBRANCEPURPOSE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);
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
                    result = "Selected Role is already in use. Connot delete";
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
        /// To Update Loan
        /// </summary>
        /// <param name="objLoan"></param>
        /// <returns></returns>
        public string UpdateLoan(LoanBO objLoan)
        {
            string result = "";
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_UPD_ENCUMBRANCEPURPOSE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@ENCUMBRANCEPURPOSEIDIN", objLoan.EncumbranceId);
            myCommand.Parameters.Add("@ENCUMBRANCEPURPOSENameIN", objLoan.Encumbrancepurpose);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objLoan.UpdatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            myConnection.Close();

            return result;
        }

        /// <summary>
        /// To Get Loan By Loan ID
        /// </summary>
        /// <param name="EncumbranceId"></param>
        /// <returns></returns>
        public LoanBO GetLoanByLoanID(int EncumbranceId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ENCPURPOSEBYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EncumbranceIDIN", EncumbranceId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LoanBO obLoan = null;
            while (dr.Read())
            {
                obLoan = new LoanBO();
                obLoan.EncumbranceId = dr.GetInt32(dr.GetOrdinal("ENCUMBRANCEID"));
                obLoan.Encumbrancepurpose = dr.GetString(dr.GetOrdinal("ENCUMBRANCEPURPOSE"));
            }
            dr.Close();
            return obLoan;
        }

        //newly added
        public string ObsoleteLoan(int EncumbrancepurposeId, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_ENCUMBRANCEPURPOSE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);
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

