using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ModeofPaymentDAL
    {
        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        /// <summary>
        /// To Insert Mode of Payment
        /// </summary>
        /// <param name="ModeofPaymentBOObj"></param>
        /// <returns></returns>
        public string InsertModeofPayment(ModeofPaymentBO ModeofPaymentBOObj)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTMODEOFPAYMENT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("TYPEOFPAYMENT_", ModeofPaymentBOObj.PaymentType);
                dcmd.Parameters.Add("MODEOFPAYMENT_", ModeofPaymentBOObj.ModeofPayment);
                dcmd.Parameters.Add("CREATEDBY", ModeofPaymentBOObj.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
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
            return returnResult;
        }

        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To Get Mode of Payment
        /// </summary>
        /// <returns></returns>
        public ModeofPaymentList GetModeofPayment()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTMODEOFPAYMENT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ModeofPaymentBO objModeofPayment = null;
            ModeofPaymentList ModeofPaymentList = new ModeofPaymentList();

            while (dr.Read())
            {
                objModeofPayment = new ModeofPaymentBO();
                objModeofPayment.ModeofPaymentID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODEOFPAYMENTID")));
                objModeofPayment.ModeofPayment = dr.GetString(dr.GetOrdinal("MODEOFPAYMENT"));
                objModeofPayment.PaymentType = dr.GetString(dr.GetOrdinal("TYPEOFPAYMENT"));
                objModeofPayment.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                ModeofPaymentList.Add(objModeofPayment);
            }

            dr.Close();

            return ModeofPaymentList;
        }

        /// <summary>
        /// To Obsolete Mode of Payment
        /// </summary>
        /// <param name="ModeofPaymentID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteModeofPayment(int ModeofPaymentID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_MODEOFPAY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("MODEOFPAYMENT_", ModeofPaymentID);
                myCommand.Parameters.Add("ISDELETED_", IsDeleted);
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteModeofPayment(int ModeofPaymentID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEMODEOFPAY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("MODEOFPAYMENT_", ModeofPaymentID);

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
                    result = "Selected item is already in use. Connot delete";
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

        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID
        public ModeofPaymentBO GetModeofPaymentID(int ModeofPaymentID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSELECTMODEOFPAY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PositionID_", ModeofPaymentID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ModeofPaymentBO ModeofPaymentBOObj = null;
            ModeofPaymentList ModeofPaymentList = new ModeofPaymentList();

            ModeofPaymentBOObj = new ModeofPaymentBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("TYPEOFPAYMENT"))) ModeofPaymentBOObj.PaymentType = dr.GetString(dr.GetOrdinal("TYPEOFPAYMENT"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODEOFPAYMENT"))) ModeofPaymentBOObj.ModeofPayment = dr.GetString(dr.GetOrdinal("MODEOFPAYMENT"));
                if (!dr.IsDBNull(dr.GetOrdinal("MODEOFPAYMENTID"))) ModeofPaymentBOObj.ModeofPaymentID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MODEOFPAYMENTID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) ModeofPaymentBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
            }
            dr.Close();
            return ModeofPaymentBOObj;
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

        public string EDITMODEOFPAYMENT(ModeofPaymentBO ModeofPaymentBOObj)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATEMODEOFPAY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("TYPEOFPAYMENT_", ModeofPaymentBOObj.PaymentType);
                dcmd.Parameters.Add("MODEOFPAYMENT_", ModeofPaymentBOObj.ModeofPayment);
                dcmd.Parameters.Add("MODEOFPAYMENTID_", ModeofPaymentBOObj.ModeofPaymentID);
                dcmd.Parameters.Add("UpdatedBY", ModeofPaymentBOObj.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
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

            return returnResult;
        }
    }
}
