using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GETALL_CTYPE";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_CONSULTANT";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_CONSULTANTTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CONSULTANTTYPE_", objconsultantType.ConsultantType);
                dcmd.Parameters.AddWithValue("CREATEDBY", objconsultantType.CreatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_CONSULTANTTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("c_consultanttype", objconsultantType.ConsultantType);
                dcmd.Parameters.AddWithValue("c_consultanttypeid", objconsultantType.ConsultantTypeID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objconsultantType.CreatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_CONSULTANTTYPE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("c_consultanttype", consultantTypeID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_CONSULTANTTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("c_consultanttypeid", consultantTypeID);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_CTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("c_consultanttypeid", consultantTypeID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("updatedBy_", updatedBy);
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
