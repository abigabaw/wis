using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GETALL_GRIVCATEGORY";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_GRIEVANCECATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("GRIEVANCECATEGORY_", objconsultantType.GrievancesCategory);
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
        /// to update data
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string EDITGrievancesCategory(GrievancesMasterBO objconsultantType)
        {
            string returnResult = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_GRIEVANCECATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("GRIEVANCECATEGID_", objconsultantType.GRIEVANCECATEGID);
                dcmd.Parameters.AddWithValue("GRIEVANCECATEGORY_", objconsultantType.GrievancesCategory);
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

        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="GRIEVANCECATEGID"></param>
        /// <returns></returns>
        public GrievancesMasterBO GetGrievancesCategoryId(int GRIEVANCECATEGID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_GRIVCATEGORYBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GRIEVANCECATEGID_", GRIEVANCECATEGID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_GRIEVANCECATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("GRIEVANCECATEGID_", GRIEVANCECATEGID);
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
        /// to make data obsolete
        /// </summary>
        /// <param name="GRIEVANCECATEGID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteconsultantType(int GRIEVANCECATEGID, string IsDeleted, int updatedBy)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_GRIEVANCECATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("GRIEVANCECATEGID_", GRIEVANCECATEGID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
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
