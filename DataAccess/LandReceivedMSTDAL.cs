using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LandReceivedMSTDAL
    {
        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        public string InsertLandReceived(LandReceivedMSTBO LandReceivedMSTBOObj)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTLANDRECDFROM", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("LandReceived_", LandReceivedMSTBOObj.LandReceived);
                dcmd.Parameters.AddWithValue("CREATEDBY", LandReceivedMSTBOObj.UserID);
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

        //get active data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        public LandReceivedMSTList GetLandReceived()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTLANDRECD";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandReceivedMSTBO objLandReceived = null;
            LandReceivedMSTList LandReceivedMSTList = new LandReceivedMSTList();

            while (dr.Read())
            {
                objLandReceived = new LandReceivedMSTBO();
                objLandReceived.LandReceivedID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LANDRECDFROMID")));
                objLandReceived.LandReceived = dr.GetString(dr.GetOrdinal("LANDRECDFROM"));
                objLandReceived.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                LandReceivedMSTList.Add(objLandReceived);
            }

            dr.Close();

            return LandReceivedMSTList;
        }

        /// <summary>
        /// To Get All Land Received
        /// </summary>
        /// <returns></returns>
        public LandReceivedMSTList GetAllLandReceived()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTLANDRECDALL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandReceivedMSTBO objLandReceived = null;
            LandReceivedMSTList LandReceivedMSTList = new LandReceivedMSTList();

            while (dr.Read())
            {
                objLandReceived = new LandReceivedMSTBO();
                objLandReceived.LandReceivedID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LANDRECDFROMID")));
                objLandReceived.LandReceived = dr.GetString(dr.GetOrdinal("LANDRECDFROM"));
                objLandReceived.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                LandReceivedMSTList.Add(objLandReceived);
            }

            dr.Close();

            return LandReceivedMSTList;
        }

        /// <summary>
        /// To Obsolete Land Received
        /// </summary>
        /// <param name="LandReceivedID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLandReceived(int LandReceivedID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_LANDRECD", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("LandReceivedID_", LandReceivedID);
                myCommand.Parameters.AddWithValue("ISDELETED_", IsDeleted);
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteLandReceived(int LandReceivedID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETELANDRECDFROM", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("LandReceivedID_", LandReceivedID);

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

        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID
        public LandReceivedMSTBO GetLandReceivedByID(int LandReceivedID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTLANDRECD";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("LandReceivedID_", LandReceivedID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandReceivedMSTBO LandReceivedMSTBOObj = null;
            LandReceivedMSTList LandReceivedMSTList = new LandReceivedMSTList();

            LandReceivedMSTBOObj = new LandReceivedMSTBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "LANDRECDFROM") && !dr.IsDBNull(dr.GetOrdinal("LANDRECDFROM")))
                    LandReceivedMSTBOObj.LandReceived = dr.GetString(dr.GetOrdinal("LANDRECDFROM"));
                if (ColumnExists(dr, "LANDRECDFROMID") && !dr.IsDBNull(dr.GetOrdinal("LANDRECDFROMID")))
                    LandReceivedMSTBOObj.LandReceivedID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LANDRECDFROMID")));
                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    LandReceivedMSTBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();
            return LandReceivedMSTBOObj;
        }
        // to check the Column are Exists or not
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

        public string EDITLANDRECEIVED(LandReceivedMSTBO LandReceivedMSTBOObj)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATELANDRECDFROM", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("LandReceived_", LandReceivedMSTBOObj.LandReceived);
                dcmd.Parameters.AddWithValue("LandReceivedID_", LandReceivedMSTBOObj.LandReceivedID);
                dcmd.Parameters.AddWithValue("UpdatedBY", LandReceivedMSTBOObj.UserID);
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
