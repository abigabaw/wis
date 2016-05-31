using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_FINCONDITION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("F_financecondition", FinanceConditionBOobj.FINANCECONDITION);
                dcmd.Parameters.Add("CREATEDBY", FinanceConditionBOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_FIN_CONDITION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@F_FINANCECONDITIONID", FcondId);
                myCommand.Parameters.Add("@isdeleted_", ISDELETED);
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
        /// <summary>
        /// to fetch details 
        /// </summary>
        /// <returns></returns>
        public FinanceConditionList GetAllFinanceConditions()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_FINAN_COND_ALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GET_FINAN_COND";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_DETAIL_FINANCE_COND";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("F_FINANCECONDITIONID", financeConditionID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_FINANCE_COND", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@F_FINANCECONDITIONID", financeConditionId);
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
        /// to update data
        /// </summary>
        /// <param name="FinanceConditionBOobj"></param>
        /// <returns></returns>
        public string Update(FinanceConditionBO FinanceConditionBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_FINANCE_COND", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("F_FINANCECONDITIONID", FinanceConditionBOobj.FINANCECONDITIONID);
                dcmd.Parameters.Add("F_FINANCECONDITION", FinanceConditionBOobj.FINANCECONDITION);
                dcmd.Parameters.Add("F_UPDATEDBY", FinanceConditionBOobj.CREATEDBY);
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
