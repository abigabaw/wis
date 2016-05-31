using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConsultantTypeDAL
    {
        /// <summary>
        /// To Get ALL Consultant Type
        /// </summary>
        /// <returns></returns>
        public ConsultantTypeList GetALLConsultantType()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GETALL_CTYPE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConsultantTypeBO objTypeBO = null;
            ConsultantTypeList objConsultantTypes = new ConsultantTypeList();

            while (dr.Read())
            {
                objTypeBO = new ConsultantTypeBO();
                objTypeBO.ConsultantTypeID = dr.GetInt32(dr.GetOrdinal("CONSULTANTTYPEID"));
                objTypeBO.ConsultantType = dr.GetString(dr.GetOrdinal("CONSULTANTTYPE"));
                objTypeBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                objConsultantTypes.Add(objTypeBO);
            }

            dr.Close();
            return objConsultantTypes;
        }

        /// <summary>
        /// To Get Consultant Type
        /// </summary>
        /// <returns></returns>
        public ConsultantTypeList GetConsultantType()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_CONSULTANT";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConsultantTypeBO objTypeBO = null;
            ConsultantTypeList objConsultantTypes = new ConsultantTypeList();

            while (dr.Read())
            {
                objTypeBO = new ConsultantTypeBO();
                objTypeBO.ConsultantTypeID = dr.GetInt32(dr.GetOrdinal("CONSULTANTTYPEID"));
                objTypeBO.ConsultantType = dr.GetString(dr.GetOrdinal("CONSULTANTTYPE"));
                objConsultantTypes.Add(objTypeBO);
            }

            dr.Close();
            return objConsultantTypes;
        }

        /// <summary>
        /// To insert into database
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string insert(ConsultantTypeBO objconsultantType)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_CONSULTANTTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CONSULTANTTYPE_", objconsultantType.ConsultantType);
                dcmd.Parameters.Add("CREATEDBY", objconsultantType.CreatedBy);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

                return returnResult;

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
        }

        /// <summary>
        /// To EDIT Consultant Type
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string EDITConsultantType(ConsultantTypeBO objconsultantType)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_CONSULTANTTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("c_consultanttype", objconsultantType.ConsultantType);
                dcmd.Parameters.Add("c_consultanttypeid", objconsultantType.ConsultantTypeID);
                dcmd.Parameters.Add("UpdatedBY", objconsultantType.CreatedBy);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

                return returnResult;


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
        }

        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID
        public ConsultantTypeBO GetConsultantTypeId(int consultantTypeID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_CONSULTANTTYPE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("c_consultanttype", consultantTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConsultantTypeBO objconsultantType = null;
            ConsultantTypeList Users = new ConsultantTypeList();

            objconsultantType = new ConsultantTypeBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CONSULTANTTYPE") && !dr.IsDBNull(dr.GetOrdinal("CONSULTANTTYPE")))
                    objconsultantType.ConsultantType = dr.GetString(dr.GetOrdinal("CONSULTANTTYPE"));
                if (ColumnExists(dr, "CONSULTANTTYPEID") && !dr.IsDBNull(dr.GetOrdinal("CONSULTANTTYPEID")))
                    objconsultantType.ConsultantTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONSULTANTTYPEID")));

            }
            dr.Close();

       
            return objconsultantType;
        }

        /// <summary>
        /// To check the Column are Exists or not
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
        // to check the Column are Exists or not

        /// <summary>
        /// To Delete Consultant Type
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <returns></returns>
        public string DeleteConsultantType(int consultantTypeID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_CONSULTANTTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("c_consultanttypeid", consultantTypeID);
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
                    result = "Selected Consultant Type is already in use. Connot delete";
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
        /// To Obsolete consultant Type
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteconsultantType(int consultantTypeID, string IsDeleted, int updatedBy)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_CTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("c_consultanttypeid", consultantTypeID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("updatedBy_", updatedBy);
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
