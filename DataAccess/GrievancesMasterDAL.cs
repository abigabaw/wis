using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class GrievancesMasterDAL
    {
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <returns></returns>
        public GrievancesMasterList GetALLGrievancesCategory()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GETALL_GRIVCATEGORY";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesMasterBO objTypeBO = null;
            GrievancesMasterList objGrievancesCategorys = new GrievancesMasterList();

            while (dr.Read())
            {
                objTypeBO = new GrievancesMasterBO();
                objTypeBO.GRIEVANCECATEGID = dr.GetInt32(dr.GetOrdinal("GRIEVANCECATEGID"));
                objTypeBO.GrievancesCategory = dr.GetString(dr.GetOrdinal("GRIEVANCECATEGORY"));
                objTypeBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                objGrievancesCategorys.Add(objTypeBO);
            }

            dr.Close();
            return objGrievancesCategorys;
        }
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string insert(GrievancesMasterBO objconsultantType)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_GRIEVANCECATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("GRIEVANCECATEGORY_", objconsultantType.GrievancesCategory);
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
        /// to update data
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string EDITGrievancesCategory(GrievancesMasterBO objconsultantType)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_GRIEVANCECATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("GRIEVANCECATEGID_", objconsultantType.GRIEVANCECATEGID);
                dcmd.Parameters.Add("GRIEVANCECATEGORY_", objconsultantType.GrievancesCategory);
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

        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="GRIEVANCECATEGID"></param>
        /// <returns></returns>
        public GrievancesMasterBO GetGrievancesCategoryId(int GRIEVANCECATEGID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_GRIVCATEGORYBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("GRIEVANCECATEGID_", GRIEVANCECATEGID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesMasterBO objconsultantType = null;
            GrievancesMasterList Users = new GrievancesMasterList();

            objconsultantType = new GrievancesMasterBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "GRIEVANCECATEGORY") && !dr.IsDBNull(dr.GetOrdinal("GRIEVANCECATEGORY")))
                    objconsultantType.GrievancesCategory = dr.GetString(dr.GetOrdinal("GRIEVANCECATEGORY"));
                if (ColumnExists(dr, "GRIEVANCECATEGID") && !dr.IsDBNull(dr.GetOrdinal("GRIEVANCECATEGID")))
                    objconsultantType.GRIEVANCECATEGID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRIEVANCECATEGID")));

            }
            dr.Close();


            return objconsultantType;
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
 /// To delete data
 /// </summary>
 /// <param name="GRIEVANCECATEGID"></param>
 /// <returns></returns>

        public string DeleteGrievancesCategory(int GRIEVANCECATEGID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_GRIEVANCECATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("GRIEVANCECATEGID_", GRIEVANCECATEGID);
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
        /// to make data obsolete
        /// </summary>
        /// <param name="GRIEVANCECATEGID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteconsultantType(int GRIEVANCECATEGID, string IsDeleted, int updatedBy)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_GRIEVANCECATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("GRIEVANCECATEGID_", GRIEVANCECATEGID);
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
