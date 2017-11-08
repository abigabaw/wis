using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class FinanceConditionDAL
    {
       /// <summary>
       /// to save data to database
       /// </summary>
       /// <param name="FinanceConditionBOobj"></param>
       /// <returns></returns>
        public string Insert(FinanceConditionBO FinanceConditionBOobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_FINCONDITION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("F_financecondition", FinanceConditionBOobj.FINANCECONDITION);
                dcmd.Parameters.AddWithValue("CREATEDBY", FinanceConditionBOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch(Exception errormsg)
            {
                throw errormsg;
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
        /// To make data obsolete
        /// </summary>
        /// <param name="FcondId"></param>
        /// <param name="ISDELETED"></param>
        /// <returns></returns>
        public string ObsoleteFcond(int FcondId, string ISDELETED)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_FIN_CONDITION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCECONDITIONID", FcondId);
                myCommand.Parameters.AddWithValue("@isdeleted_", ISDELETED);
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
        /// <summary>
        /// to fetch details 
        /// </summary>
        /// <returns></returns>
        public FinanceConditionList GetAllFinanceConditions()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_FINAN_COND_ALL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FinanceConditionBO Boobj = null;
            FinanceConditionList Listobj = new FinanceConditionList();

            while (dr.Read())
            {
                Boobj = new FinanceConditionBO();
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCECONDITIONID")))
                    Boobj.FINANCECONDITIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCECONDITIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCECONDITION")))
                    Boobj.FINANCECONDITION = dr.GetString(dr.GetOrdinal("FINANCECONDITION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
               
                Listobj.Add(Boobj);
            }

            dr.Close();
            return Listobj;
        }
        /// <summary>
        /// to fetch details 
        /// </summary>
        /// <returns></returns>
        public FinanceConditionList GetFinanceCondition()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_FINAN_COND";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FinanceConditionBO Boobj = null;
            FinanceConditionList Listobj = new FinanceConditionList();

            while (dr.Read())
            {
                Boobj = new FinanceConditionBO();
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCECONDITIONID")))
                    Boobj.FINANCECONDITIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCECONDITIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCECONDITION")))
                    Boobj.FINANCECONDITION = dr.GetString(dr.GetOrdinal("FINANCECONDITION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                Listobj.Add(Boobj);
            }

            dr.Close();

            return Listobj;
        }
        /// <summary>
        /// to fetch details 
        /// </summary>
        /// <returns></returns>
        public FinanceConditionBO GetfinanceConditionID(int financeConditionID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_DETAIL_FINANCE_COND";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("F_FINANCECONDITIONID", financeConditionID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FinanceConditionBO Boobj = null;
            FinanceConditionList Listobj = new FinanceConditionList();

            Boobj = new FinanceConditionBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "FINANCECONDITION") && !dr.IsDBNull(dr.GetOrdinal("FINANCECONDITION")))
                    Boobj.FINANCECONDITION = dr.GetString(dr.GetOrdinal("FINANCECONDITION"));
                if (ColumnExists(dr, "FINANCECONDITIONID") && !dr.IsDBNull(dr.GetOrdinal("FINANCECONDITIONID")))
                    Boobj.FINANCECONDITIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCECONDITIONID")));

            }
            dr.Close();


            return Boobj;
        }
        /// <summary>
        /// to check whether data exists
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool ColumnExists(IDataReader reader, string columnName)
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
        /// to delete data
        /// </summary>
        /// <param name="financeConditionId"></param>
        /// <returns></returns>
        public string DeleteFinanceCondition(int financeConditionId)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_FINANCE_COND", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCECONDITIONID", financeConditionId);
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
        /// to update data
        /// </summary>
        /// <param name="FinanceConditionBOobj"></param>
        /// <returns></returns>
        public string Update(FinanceConditionBO FinanceConditionBOobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_FINANCE_COND", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("F_FINANCECONDITIONID", FinanceConditionBOobj.FINANCECONDITIONID);
                dcmd.Parameters.AddWithValue("F_FINANCECONDITION", FinanceConditionBOobj.FINANCECONDITION);
                dcmd.Parameters.AddWithValue("F_UPDATEDBY", FinanceConditionBOobj.CREATEDBY);
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

    }
}
