using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection Conn=new OracleConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            OracleCommand Cmd=new OracleCommand("USP_INS_MST_CULTUREPROPERTY",Conn);
            Cmd.CommandType= CommandType.StoredProcedure;
            try
            {
                CreatedBy=Convert.ToInt32(CulturePropertiesMasterBObj.CreatedBy);
                Cmd.Parameters.Add("CultureProptype_",CulturePropertiesMasterBObj.CulturePropTypeName);
                Cmd.Parameters.Add("CreatedBy_", CreatedBy);
                Cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2,500).Direction=ParameterDirection.Output;
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
            OracleConnection Conn = new OracleConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            OracleCommand Cmd = null;
            try
            {
                Cmd = new OracleCommand("USP_SEL_MST_CULTUREPROPERTY", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection Conn = new OracleConnection(AppConfiguration.ConnectionString);
            Conn.Open();
            OracleCommand Cmd = null;
            try
            {
                Cmd = new OracleCommand("USP_MST_GET_CULTUREPROPBYID", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add("CULTUREPROPTYPEID_", CulturePropID);
                Cmd.Parameters.Add("Sp_RecordSet", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader dr = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_CULTUREPROPTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CULTUREPROPTYPEID_", CulturePropertiesMasterBObj.CulturePropTypeID);
                dcmd.Parameters.Add("CULTUREPROPTYPE_", CulturePropertiesMasterBObj.CulturePropTypeName);
                dcmd.Parameters.Add("UPDATEDBY_", CulturePropertiesMasterBObj.UpdatedBy);
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
        /// <summary>
        /// To delete data from database
        /// </summary>
        /// <param name="CulturePropID"></param>
        /// <returns></returns>
        public string DeleteCulturePropByID(int CulturePropID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_CULTUREPROPTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CULTUREPROPTYPEID_", CulturePropID);
                //myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myCommand.Parameters.Add("ErrorMessage", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_CULTUREPROPTYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CULTUREPROPTYPEID_", CulturePropTypeID);
                myCommand.Parameters.Add("ISDELETED_", Isdeleted);
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
