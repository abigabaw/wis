using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class OptionAvailableDAL
    {
        public OptionAvailableList Getoptionavailable()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELOPTIONAVAIL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionAvailableBO objOptionAvailable = null;
            OptionAvailableList optionavailable = new OptionAvailableList();

            while (dr.Read())
            {
                objOptionAvailable = new OptionAvailableBO();
                objOptionAvailable.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                objOptionAvailable.OptionAvailable = dr.GetString(dr.GetOrdinal("OPTIONAVAILABLE"));
                objOptionAvailable.Isdeleted = dr.GetString(dr.GetOrdinal("Isdeleted"));

                optionavailable.Add(objOptionAvailable);
            }

            dr.Close();

            return optionavailable;
        }


        /// <summary>
        /// save the data
        /// </summary>
        /// <param name="objConcern"></param>
        /// <returns></returns>
        public string Insert(OptionAvailableBO objoptionavail)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSOPTIONAVAIL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("OPTIONAVAILABLE_", objoptionavail.OptionAvailable);
                dcmd.Parameters.AddWithValue("CREATEDBY", objoptionavail.UserID);

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

        /// <summary>
        /// To GET ALL CONCERN
        /// </summary>
        /// <returns></returns>
        public OptionAvailableList GetAllOptionAvail()
        {
            // used in Master page
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLOPTIONAVAIL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionAvailableBO objOptionAvailableBO = null;
            OptionAvailableList lstooptionavail = new OptionAvailableList();

            while (dr.Read())
            {
                objOptionAvailableBO = new OptionAvailableBO();
                objOptionAvailableBO.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                objOptionAvailableBO.OptionAvailable = dr.GetString(dr.GetOrdinal("OPTIONAVAILABLE"));
                objOptionAvailableBO.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                lstooptionavail.Add(objOptionAvailableBO);
            }

            dr.Close();

            return lstooptionavail;
        }


        /// <summary>
        /// get the data based on ID
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <returns></returns>
        public OptionAvailableBO GetAllOptionById(int optionID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETOPTNBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID_", optionID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionAvailableBO objoptionavail = null;
            OptionAvailableList Users = new OptionAvailableList();

            objoptionavail = new OptionAvailableBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "OPTIONAVAILABLE") && !dr.IsDBNull(dr.GetOrdinal("OPTIONAVAILABLE")))
                    objoptionavail.OptionAvailable = dr.GetString(dr.GetOrdinal("OPTIONAVAILABLE"));
                if (ColumnExists(dr, "ID") && !dr.IsDBNull(dr.GetOrdinal("ID")))
                    objoptionavail.ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

            }
            dr.Close();


            return objoptionavail;
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
        /// <param name="objConcern"></param>
        /// <returns></returns>
        public string editoptionavail(OptionAvailableBO objoptionavail)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATEALLOPTIONAVAIL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("OPTIONAVAILABLE_", objoptionavail.OptionAvailable);
                dcmd.Parameters.AddWithValue("ID_", objoptionavail.ID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objoptionavail.UserID);
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
        /// <summary>
        /// TO delete data
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <returns></returns>
        public string Deleteoptionavail(int optionID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELOPTIONAVAIL", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ID_", optionID);

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
                    result = "Selected option is already in use. Cannot delete";
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
        /// To Obsolete Concern
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
        public string Obsoleteoptionavail(int ID, string Isdeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOPTIONAVAIL", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("Id_", ID);
                myCommand.Parameters.AddWithValue("isdeleted_", Isdeleted);
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

