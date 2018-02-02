﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class HIVContractedDAL
    {
       /// <summary>
        /// To Get ALL HIV Contracted
       /// </summary>
       /// <returns></returns>
       public HIVContractedList GetALLHIVContracted()
       {
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_GETALL_HIVC";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           HIVContractedBO objHIVContracted = null;
           HIVContractedList objHIV = new HIVContractedList();

           while (dr.Read())
           {
               objHIVContracted = new HIVContractedBO();
               objHIVContracted.ContractedID = dr.GetInt32(dr.GetOrdinal("CONTRACTED_ID"));
               objHIVContracted.ContractedThrough = dr.GetString(dr.GetOrdinal("CONTRACTED_THROUGH"));
               objHIVContracted.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
               objHIV.Add(objHIVContracted);
           }

           dr.Close();
           return objHIV;
       }

       /// <summary>
       /// To Get HIV Contracted
       /// </summary>
       /// <returns></returns>
       public HIVContractedList GetHIVContracted()
       {
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_GET_HIVC";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           HIVContractedBO objHIVContracted = null;
           HIVContractedList objHIV = new HIVContractedList();

           while (dr.Read())
           {
               objHIVContracted = new HIVContractedBO();
               objHIVContracted.ContractedID = dr.GetInt32(dr.GetOrdinal("CONTRACTED_ID"));
               objHIVContracted.ContractedThrough = dr.GetString(dr.GetOrdinal("CONTRACTED_THROUGH"));
              // objHIVContracted.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
               objHIV.Add(objHIVContracted);
           }

           dr.Close();
           return objHIV;
       }

       /// <summary>
       /// To Get Contracted ID
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <returns></returns>
       public HIVContractedBO GetContractedID(int ContractedID)
       {
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_MST_GET_HIVC";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("CONTRACTEDID_", ContractedID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           HIVContractedBO objHIVContracted = null;
           HIVContractedList Users = new HIVContractedList();

           objHIVContracted = new HIVContractedBO();
           while (dr.Read())
           {
               if (ColumnExists(dr, "CONTRACTED_THROUGH") && !dr.IsDBNull(dr.GetOrdinal("CONTRACTED_THROUGH")))
                   objHIVContracted.ContractedThrough = dr.GetString(dr.GetOrdinal("CONTRACTED_THROUGH"));
               if (ColumnExists(dr, "CONTRACTED_ID") && !dr.IsDBNull(dr.GetOrdinal("CONTRACTED_ID")))
                   objHIVContracted.ContractedID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONTRACTED_ID")));

           }
           dr.Close();


           return objHIVContracted;
       }

       /// <summary>
       /// To Check Weather Column Exists or Not
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
       /// To insert HIVC
       /// </summary>
       /// <param name="objHIVContracted"></param>
       /// <returns></returns>
       public string insertHIVC(HIVContractedBO objHIVContracted)
        {
            string returnResult = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_HIVC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CONTRACTEDTHROUGH_", objHIVContracted.ContractedThrough);
                dcmd.Parameters.AddWithValue("CREATEDBY", objHIVContracted.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
       /// To EDIT HIVC
       /// </summary>
       /// <param name="objHIVContracted"></param>
       /// <returns></returns>
       public string EDITHIVC(HIVContractedBO objHIVContracted)
        {
            string returnResult = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_HIVC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CONTRACTEDTHROUGH_", objHIVContracted.ContractedThrough);
                dcmd.Parameters.AddWithValue("CONTRACTEDID_", objHIVContracted.ContractedID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objHIVContracted.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
       /// To Delete HIVC
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <returns></returns>
       public string DeleteHIVC(int ContractedID)
       {
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;

           string result = string.Empty;
           try
           {
               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_DEL_HIVC", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("CONTRACTEDID_", ContractedID);
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
                   result = "Selected HIV Contract is already in use. Cannot Delete";
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
       /// To Obsolete HIVC
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <param name="IsDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteHIVC(int ContractedID, string IsDeleted, int updatedBy)
       {
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;
           string result = "";
           try
           {
               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_OBSOLETE_HIVC", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("CONTRACTEDID_", ContractedID);
               myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
               myCommand.Parameters.AddWithValue("updatedBy_", updatedBy);
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
