using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LandReceivedMSTDAL
    {
        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        public string InsertLandReceived(LandReceivedMSTBO LandReceivedMSTBOObj)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTLANDRECDFROM", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("LandReceived_", LandReceivedMSTBOObj.LandReceived);
                dcmd.Parameters.Add("CREATEDBY", LandReceivedMSTBOObj.UserID);
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

        //get active data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        public LandReceivedMSTList GetLandReceived()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTLANDRECD";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTLANDRECDALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_LANDRECD", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LandReceivedID_", LandReceivedID);
                myCommand.Parameters.Add("ISDELETED_", IsDeleted);
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteLandReceived(int LandReceivedID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETELANDRECDFROM", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LandReceivedID_", LandReceivedID);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSELECTLANDRECD";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("LandReceivedID_", LandReceivedID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATELANDRECDFROM", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("LandReceived_", LandReceivedMSTBOObj.LandReceived);
                dcmd.Parameters.Add("LandReceivedID_", LandReceivedMSTBOObj.LandReceivedID);
                dcmd.Parameters.Add("UpdatedBY", LandReceivedMSTBOObj.UserID);
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
