using System;
using System.Data;
using System.Data.SqlClient;
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

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTMODEOFPAYMENT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("TYPEOFPAYMENT_", ModeofPaymentBOObj.PaymentType);
                dcmd.Parameters.AddWithValue("MODEOFPAYMENT_", ModeofPaymentBOObj.ModeofPayment);
                dcmd.Parameters.AddWithValue("CREATEDBY", ModeofPaymentBOObj.UserID);
                //return dcmd.ExecuteNonQuery();

                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTMODEOFPAYMENT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_MODEOFPAY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("MODEOFPAYMENT_", ModeofPaymentID);
                myCommand.Parameters.AddWithValue("ISDELETED_", IsDeleted);
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteModeofPayment(int ModeofPaymentID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEMODEOFPAY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("MODEOFPAYMENT_", ModeofPaymentID);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTMODEOFPAY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PositionID_", ModeofPaymentID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATEMODEOFPAY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("TYPEOFPAYMENT_", ModeofPaymentBOObj.PaymentType);
                dcmd.Parameters.AddWithValue("MODEOFPAYMENT_", ModeofPaymentBOObj.ModeofPayment);
                dcmd.Parameters.AddWithValue("MODEOFPAYMENTID_", ModeofPaymentBOObj.ModeofPaymentID);
                dcmd.Parameters.AddWithValue("UpdatedBY", ModeofPaymentBOObj.UserID);
                //return dcmd.ExecuteNonQuery();

                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
