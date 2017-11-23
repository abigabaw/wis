using System;
using System.Data;
using System.Data.SqlClient;
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

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTOCCUPATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("OccupationName_", objOccupation.OCCUPATIONNAME);
                dcmd.Parameters.AddWithValue("CreatedBY", objOccupation.UserID);
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
        /// To Get ALL Occupation
        /// </summary>
        /// <returns></returns>
        public OccupationList GetALLOccupation()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLOCCUPATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTOCCUPATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEOCCUPATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("OCCUPATIONID_", OCCUPATIONID);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_OCCUPATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("OCCUPATIONID_", OCCUPATIONID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
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

        /// <summary>
        /// To Get Occupation Id
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <returns></returns>
        public OccupationBO GetOccupationId(int OCCUPATIONID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETOCCUPATIONBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("OCCUPATIONID_", OCCUPATIONID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATEOCCUPATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("OCCUPATIONNAME_", objOccupation.OCCUPATIONNAME);
                dcmd.Parameters.AddWithValue("OCCUPATIONID_", objOccupation.OCCUPATIONID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objOccupation.UserID);
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
    }
}