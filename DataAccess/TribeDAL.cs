﻿using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TribeDAL
    {
        /// <summary>
        /// To Insert Into Tribe Master
        /// </summary>
        /// <param name="TribeBOObj"></param>
        /// <returns></returns>
        public string InsertIntoTribeMaster(TribeBO TribeBOObj)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_INSERTINTOTRIBEMASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(cmd.CommandType);
            string result = string.Empty;
            try
            {
                cmd.Parameters.AddWithValue("TrbName", TribeBOObj.TribeName);
                cmd.Parameters.AddWithValue("CrtBy", TribeBOObj.CreatedBy);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        /// <summary>
        /// To Fetch ALL Tribe List
        /// </summary>
        /// <returns></returns>
        public TribeList FetchALLTribeList()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_TRIBES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TribeBO objTribeBO = null;
            TribeList TribeListObj = new TribeList();

            while (dr.Read())
            {
                objTribeBO = new TribeBO();
                objTribeBO.TribeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIBEID")));
                objTribeBO.TribeName = dr.GetString(dr.GetOrdinal("TRIBENAME"));
                objTribeBO.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                TribeListObj.Add(objTribeBO);
            }

            dr.Close();

            return TribeListObj;
        }

        /// <summary>
        /// To Fetch Tribe List
        /// </summary>
        /// <returns></returns>
        public TribeList FetchTribeList()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETTRIBEDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TribeBO objTribeBO = null;
            TribeList TribeListObj = new TribeList();

            while (dr.Read())
            {
                objTribeBO = new TribeBO();
                objTribeBO.TribeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIBEID")));
                objTribeBO.TribeName = dr.GetString(dr.GetOrdinal("TRIBENAME"));

                TribeListObj.Add(objTribeBO);
            }

            dr.Close();

            return TribeListObj;
        }

        /// <summary>
        /// To Get Tribe By Id
        /// </summary>
        /// <param name="TribeID"></param>
        /// <returns></returns>
        public TribeBO GetTribeById(int TribeID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETTRIBEDETAILSBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TrbID", TribeID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TribeBO TribeBOObj = null;
            TribeList TribeListObj = new TribeList();

            TribeBOObj = new TribeBO();
            while (dr.Read())
            {
                //if (ColumnExists(dr, "TRIBEID") && !dr.IsDBNull(dr.GetOrdinal("TRIBEID")))
                //    TribeBOObj.TribeID = Convert.ToInt32(dr.GetString(dr.GetOrdinal("TRIBEID")));
                if (ColumnExists(dr, "TRIBENAME") && !dr.IsDBNull(dr.GetOrdinal("TRIBENAME")))
                    TribeBOObj.TribeName = Convert.ToString(dr.GetValue(dr.GetOrdinal("TRIBENAME")));

            }
            dr.Close();


            return TribeBOObj;
        }

        /// <summary>
        /// To Check that the Column Exists or not.
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
        /// To Delete Tribe By Id
        /// </summary>
        /// <param name="TribeID"></param>
        /// <returns></returns>
        public string DeleteTribeById(int TribeID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETETRIBEBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TrbID_", TribeID);
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
                    result = "Selected item is already in use. Connot delete";
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
        /// To EDIT Tribe
        /// </summary>
        /// <param name="TribeBOObj"></param>
        /// <returns></returns>
        public string EDITTribe(TribeBO TribeBOObj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_EDITTRIBE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            string result = string.Empty;
            try
            {
                dcmd.Parameters.AddWithValue("TrbID", TribeBOObj.TribeID);
                dcmd.Parameters.AddWithValue("TrbName", TribeBOObj.TribeName);
                dcmd.Parameters.AddWithValue("TrbUpdate", TribeBOObj.UpdatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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
        /// To Obsolete tribe
        /// </summary>
        /// <param name="TRIBEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoletetribe(int TRIBEID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_TRIBE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TRIBEID_", TRIBEID);
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