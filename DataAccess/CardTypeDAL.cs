using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CardTypeDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 

        //get all data in mst_CardType table using USP_MST_SELECTCardType-SP
        public CardTypeList GetCardType()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCardType";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CardTypeBO objCardType = null;
            CardTypeList CardType = new CardTypeList();

            while (dr.Read())
            {
                objCardType = new CardTypeBO();
                objCardType.CardTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CardTypeID")));
                objCardType.CardTypeName = dr.GetString(dr.GetOrdinal("CARDNAME"));
                objCardType.Isdeleted = dr.GetString(dr.GetOrdinal("Isdeleted"));

                CardType.Add(objCardType);
            }

            dr.Close();

            return CardType;
        }


        /// <summary>
        /// save the data
        /// </summary>
        /// <param name="objCardType"></param>
        /// <returns></returns>
        public string Insert(CardTypeBO objCardType)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTCardType", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CardType_", objCardType.CardTypeName);
                dcmd.Parameters.AddWithValue("CREATEDBY_", objCardType.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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

        /// <summary>
        /// To GET ALL CardType
        /// </summary>
        /// <returns></returns>
        public CardTypeList GETALLCardType()
        {
            // used in Master page
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLCardType";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CardTypeBO objCardType = null;
            CardTypeList CardType = new CardTypeList();

            while (dr.Read())
            {
                objCardType = new CardTypeBO();
                objCardType.CardTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CardTypeID")));
                objCardType.CardTypeName = dr.GetString(dr.GetOrdinal("CARDNAME"));
                objCardType.Isdeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                CardType.Add(objCardType);
            }

            dr.Close();

            return CardType;
        }


        /// <summary>
        /// get the data based on ID
        /// </summary>
        /// <param name="CardTypeID"></param>
        /// <returns></returns>
        public CardTypeBO GetCardTypeById(int CardTypeID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTCardType";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CardTypeID_", CardTypeID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CardTypeBO CardTypeObj = null;
            CardTypeList Users = new CardTypeList();

            CardTypeObj = new CardTypeBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CARDNAME") && !dr.IsDBNull(dr.GetOrdinal("CARDNAME")))
                    CardTypeObj.CardTypeName = dr.GetString(dr.GetOrdinal("CARDNAME"));
                if (ColumnExists(dr, "CardTypeID") && !dr.IsDBNull(dr.GetOrdinal("CardTypeID")))
                    CardTypeObj.CardTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CardTypeID")));

            }
            dr.Close();


            return CardTypeObj;
        }

        /// <summary>
        /// to check the Column are Exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="objCardType"></param>
        /// <returns></returns>
        public string EDITCardType(CardTypeBO objCardType)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATECCardType", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CardType_", objCardType.CardTypeName);
                dcmd.Parameters.AddWithValue("CardTypeID_", objCardType.CardTypeID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objCardType.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        /// <summary>
        /// TO delete data
        /// </summary>
        /// <param name="CardTypeID"></param>
        /// <returns></returns>
        public string DeleteCardType(int CardTypeID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETECardType", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CardTypeID_", CardTypeID);
                //myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
                    result = "Selected CARDNAME is already in use. Cannot delete";
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

            //SqlConnection myConnection = null;
            //SqlCommand myCommand = null;

            //string result = string.Empty;
            //try
            //{

            //    myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            //    myCommand = new SqlCommand("USP_MST_DELETECardType", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.AddWithValue("@CardTypeID_", CardTypeID);
            //    myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    if (myCommand.Parameters["errorMessage_"].Value != null)
            //        result = myCommand.Parameters["errorMessage_"].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("ORA-02292"))
            //    {
            //        result = "Selected item is already in use. Connot delete";
            //    }
            //    else
            //    {
            //        throw ex;
            //    }
            //}
            //finally
            //{
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}

            //return result;  
            //SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            //SqlCommand cmd;


            //string proc = "USP_MST_DELETECardType";

            //cmd = new SqlCommand(proc, cnn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("CardTypeID_", CardTypeID);
            //cmd.Connection.Open();

            //int result = cmd.ExecuteNonQuery();

            //return result;
        }

        /// <summary>
        /// To Obsolete CARDNAME
        /// </summary>
        /// <param name="CardTypeID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
        public string ObsoleteCardType(int CardTypeID, string Isdeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_CardType", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CardTypeId_", CardTypeID);
                myCommand.Parameters.AddWithValue("isdeleted_", Isdeleted);
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