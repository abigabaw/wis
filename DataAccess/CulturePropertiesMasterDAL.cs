using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;
 
namespace WIS_DataAccess
{
    public class CulturePropertiesMasterDAL
    {
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="CulturePropertiesMasterBObj"></param>
        /// <returns></returns>
        public string InsertIntoCultureProp(CulturePropertiesMasterBO CulturePropertiesMasterBObj)
        {
            string ErrorMessage=string.Empty;
            int CreatedBy = 0;
            SqlConnection Conn=new SqlConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            SqlCommand Cmd=new SqlCommand("USP_INS_MST_CULTUREPROPERTY",Conn);
            Cmd.CommandType= CommandType.StoredProcedure;
            try
            {
                CreatedBy=Convert.ToInt32(CulturePropertiesMasterBObj.CreatedBy);
                Cmd.Parameters.AddWithValue("CultureProptype_",CulturePropertiesMasterBObj.CulturePropTypeName);
                Cmd.Parameters.AddWithValue("CreatedBy_", CreatedBy);
                Cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction=ParameterDirection.Output;
                Cmd.ExecuteNonQuery();
                if(Cmd.Parameters["errorMessage_"].Value !=null)
                {
                    ErrorMessage=Cmd.Parameters["errorMessage_"].Value.ToString();
                }
                else
                {
                    ErrorMessage="";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Cmd.Dispose();
                Conn.Close();
                Conn.Dispose();
            }

            return ErrorMessage;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public CulturePropTypeList GetAllCulturePropertyType()
        {
            CulturePropTypeList CulturePropTypeListObj = new CulturePropTypeList();
            CulturePropertiesMasterBO CulturePropertiesMasterBObj = null;
            SqlConnection Conn = new SqlConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            SqlCommand Cmd = null;
            try
            {
                Cmd = new SqlCommand("USP_SEL_MST_CULTUREPROPERTY", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                // Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CulturePropertiesMasterBObj = new CulturePropertiesMasterBO();
                    CulturePropertiesMasterBObj.CulturePropTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTUREPROPTYPEID")));
                    CulturePropertiesMasterBObj.CulturePropTypeName = dr.GetString(dr.GetOrdinal("CULTUREPROPTYPE"));
                    CulturePropertiesMasterBObj.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                    CulturePropTypeListObj.Add(CulturePropertiesMasterBObj);
                }

            }
            catch
            {
                throw;
            } 
            finally
            {
                Cmd.Dispose();
                Conn.Close();
                Conn.Dispose();
            }
            return CulturePropTypeListObj;
        }
        /// <summary>
        ///  To fetch details from database
        /// </summary>
        /// <param name="CulturePropID"></param>
        /// <returns></returns>
        public CulturePropertiesMasterBO GetCulturePropByID(int CulturePropID)
        {
            CulturePropTypeList CulturePropTypeListObj = new CulturePropTypeList();
            CulturePropertiesMasterBO CulturePropertiesMasterBObj = null;
            SqlConnection Conn = new SqlConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            SqlCommand Cmd = null;
            try
            {
                Cmd = new SqlCommand("USP_MST_GET_CULTUREPROPBYID", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("CULTUREPROPTYPEID_", CulturePropID);
               // Cmd.Parameters.AddWithValue("Sp_RecordSet", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
                SqlDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    CulturePropertiesMasterBObj = new CulturePropertiesMasterBO();
                    CulturePropertiesMasterBObj.CulturePropTypeID = Convert.ToInt32(dr["CULTUREPROPTYPEID"]);
                    CulturePropertiesMasterBObj.CulturePropTypeName = dr["CULTUREPROPTYPE"].ToString();
                }
            
            }
            catch
            {
                throw;
            }
            finally
            {
                Cmd.Dispose();
                Conn.Close();
                Conn.Dispose();
            }
            return CulturePropertiesMasterBObj;
        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="CulturePropertiesMasterBObj"></param>
        /// <returns></returns>
        public string EDITCulturePropByID(CulturePropertiesMasterBO CulturePropertiesMasterBObj)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_CULTUREPROPTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CULTUREPROPTYPEID_", CulturePropertiesMasterBObj.CulturePropTypeID);
                dcmd.Parameters.AddWithValue("CULTUREPROPTYPE_", CulturePropertiesMasterBObj.CulturePropTypeName);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", CulturePropertiesMasterBObj.UpdatedBy);
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
        /// To delete data from database
        /// </summary>
        /// <param name="CulturePropID"></param>
        /// <returns></returns>
        public string DeleteCulturePropByID(int CulturePropID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_CULTUREPROPTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CULTUREPROPTYPEID_", CulturePropID);
                ///* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myCommand.Parameters.AddWithValue("ErrorMessage", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["ErrorMessage"].Value != null)
                    result = myCommand.Parameters["ErrorMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Concern is already in use. Cannot delete";
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
        /// To make data obsolete
        /// </summary>
        /// <param name="CulturePropTypeID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
        public string ObsoleteCulturePropType(int CulturePropTypeID, string Isdeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_CULTUREPROPTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CULTUREPROPTYPEID_", CulturePropTypeID);
                myCommand.Parameters.AddWithValue("ISDELETED_", Isdeleted);
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
