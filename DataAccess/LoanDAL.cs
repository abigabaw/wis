using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ENCUMBRANCEPURPOSE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (EncumbrancepurposeName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@EncumbrancepurposeNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EncumbrancepurposeNameIN", EncumbrancepurposeName.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_INS_ENCUMBRANCEPURPOSE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@ENCUMBRANCEPURPOSENameIN", objLoan.Encumbrancepurpose);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objLoan.CreatedBy);
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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

        //    SqlConnection myConnection;
        //    SqlCommand myCommand;
        //    myConnection = new SqlConnection(AppConfiguration.ConnectionString);
        //    myCommand = new SqlCommand("USP_MST_DEL_ENCUMBRANCEPURPOSE", myConnection);
        //    myCommand.Connection = myConnection;
        //    myCommand.CommandType = CommandType.StoredProcedure;
        //    myCommand.Parameters.AddWithValue("@ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_ENCUMBRANCEPURPOSE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_UPD_ENCUMBRANCEPURPOSE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@ENCUMBRANCEPURPOSEIDIN", objLoan.EncumbranceId);
            myCommand.Parameters.AddWithValue("@ENCUMBRANCEPURPOSENameIN", objLoan.Encumbrancepurpose);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objLoan.UpdatedBy);
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ENCPURPOSEBYID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EncumbranceIDIN", EncumbranceId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_ENCUMBRANCEPURPOSE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ENCUMBRANCEPURPOSEIDIN", EncumbrancepurposeId);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

