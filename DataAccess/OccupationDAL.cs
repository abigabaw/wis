using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class OccupationDAL
    {
        /// <summary>
        /// To Insert Occupation
        /// </summary>
        /// <param name="objOccupation"></param>
        /// <returns></returns>
        public string InsertOccupation(OccupationBO objOccupation)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTOCCUPATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("OccupationName_", objOccupation.OCCUPATIONNAME);
                dcmd.Parameters.Add("CreatedBY", objOccupation.UserID);
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
        /// To Get ALL Occupation
        /// </summary>
        /// <returns></returns>
        public OccupationList GetALLOccupation()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALLOCCUPATION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OccupationBO objOccupation = null;
            OccupationList OccupationList = new OccupationList();

            while (dr.Read())
            {
                objOccupation = new OccupationBO();
                objOccupation.OCCUPATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OCCUPATIONID")));
                objOccupation.OCCUPATIONNAME = dr.GetString(dr.GetOrdinal("OCCUPATIONNAME"));
                objOccupation.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                OccupationList.Add(objOccupation);
            }

            dr.Close();

            return OccupationList;
        }

        /// <summary>
        /// To Get Occupation
        /// </summary>
        /// <returns></returns>
        public OccupationList GetOccupation()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTOCCUPATION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OccupationBO objOccupation = null;
            OccupationList OccupationList = new OccupationList();

            while (dr.Read())
            {
                objOccupation = new OccupationBO();
                objOccupation.OCCUPATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OCCUPATIONID")));
                objOccupation.OCCUPATIONNAME = dr.GetString(dr.GetOrdinal("OCCUPATIONNAME"));
               // objOccupation.OCCUPATIONIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                OccupationList.Add(objOccupation);
            }

            dr.Close();

            return OccupationList;
        }

        /// <summary>
        /// To Delete Occupation
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <returns></returns>
        public string DeleteOccupation(int OCCUPATIONID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEOCCUPATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("OCCUPATIONID_", OCCUPATIONID);
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

        /// <summary>
        /// To Obsolete Occupation
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteOccupation(int OCCUPATIONID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_OCCUPATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("OCCUPATIONID_", OCCUPATIONID);
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

        /// <summary>
        /// To Get Occupation Id
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <returns></returns>
        public OccupationBO GetOccupationId(int OCCUPATIONID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETOCCUPATIONBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("OCCUPATIONID_", OCCUPATIONID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OccupationBO OccupationObj = null;
            OccupationList OccupationList = new OccupationList();

            OccupationObj = new OccupationBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "OCCUPATIONNAME") && !dr.IsDBNull(dr.GetOrdinal("OCCUPATIONNAME")))
                    OccupationObj.OCCUPATIONNAME = dr.GetString(dr.GetOrdinal("OCCUPATIONNAME"));
                if (ColumnExists(dr, "OCCUPATIONID") && !dr.IsDBNull(dr.GetOrdinal("OCCUPATIONID")))
                    OccupationObj.OCCUPATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OCCUPATIONID")));

            }
            dr.Close();
           return OccupationObj;
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

        /// <summary>
        /// To EDIT Occupation
        /// </summary>
        /// <param name="objOccupation"></param>
        /// <returns></returns>
        public string EDITOccupation(OccupationBO objOccupation)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATEOCCUPATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("OCCUPATIONNAME_", objOccupation.OCCUPATIONNAME);
                dcmd.Parameters.Add("OCCUPATIONID_", objOccupation.OCCUPATIONID);
                dcmd.Parameters.Add("UpdatedBY", objOccupation.UserID);
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