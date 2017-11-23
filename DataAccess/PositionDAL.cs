using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PositionDAL
    {

        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        /// <summary>
        /// To Insert Position
        /// </summary>
        /// <param name="PositionBOObj"></param>
        /// <returns></returns>
        public string InsertPosition(PositionBO PositionBOObj)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTPOSITION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("POSITION_", PositionBOObj.PositionName);
                dcmd.Parameters.AddWithValue("CREATEDBY", PositionBOObj.UserID);
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

        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To Get Position
        /// </summary>
        /// <returns></returns>
        public PositionList GetPosition()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTPOSITION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PositionBO objPosition = null;
            PositionList PositionList = new PositionList();

            while (dr.Read())
            {
                objPosition = new PositionBO();
                objPosition.PositionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("POSITIONID")));
                objPosition.PositionName = dr.GetString(dr.GetOrdinal("POSITION"));
                objPosition.PositionIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                PositionList.Add(objPosition);
            }

            dr.Close();

            return PositionList;
        }

        /// <summary>
        /// To Get All Positions
        /// </summary>
        /// <returns></returns>
        public PositionList GetAllPositions()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALLPOSITIONS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PositionBO objPosition = null;
            PositionList PositionList = new PositionList();

            while (dr.Read())
            {
                objPosition = new PositionBO();
                objPosition.PositionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("POSITIONID")));
                objPosition.PositionName = dr.GetString(dr.GetOrdinal("POSITION"));
                objPosition.PositionIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                PositionList.Add(objPosition);
            }

            dr.Close();

            return PositionList;
        }

        /// <summary>
        /// To Obsolete Position
        /// </summary>
        /// <param name="PositionID"></param>
        /// <param name="PositionIsDeleted"></param>
        /// <returns></returns>
        public string ObsoletePosition(int PositionID, string PositionIsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_POSITION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("POSITIONID_", PositionID);
                myCommand.Parameters.AddWithValue("ISDELETED_", PositionIsDeleted);
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        /// <summary>
        /// To Delete Position
        /// </summary>
        /// <param name="PositionID"></param>
        /// <returns></returns>
        public string DeletePosition(int PositionID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEPOSITION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("POSITION_", PositionID);
               
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

        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID
        /// <summary>
        /// To Get Position By Id
        /// </summary>
        /// <param name="PositionID"></param>
        /// <returns></returns>
        public PositionBO GetPositionById(int PositionID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTPOSITION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PositionID_", PositionID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PositionBO PositionBOObj = null;
            PositionList PositionList = new PositionList();

            PositionBOObj = new PositionBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "POSITION") && !dr.IsDBNull(dr.GetOrdinal("POSITION")))
                    PositionBOObj.PositionName = dr.GetString(dr.GetOrdinal("POSITION"));
                if (ColumnExists(dr, "POSITIONID") && !dr.IsDBNull(dr.GetOrdinal("POSITIONID")))
                    PositionBOObj.PositionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("POSITIONID")));
                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    PositionBOObj.PositionIsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();
            return PositionBOObj;
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
        /// TO EDIT POSITION
        /// </summary>
        /// <param name="PositionBOObj"></param>
        /// <returns></returns>
        public string EDITPOSITION(PositionBO PositionBOObj)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATEPOSITION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("PositionName_", PositionBOObj.PositionName);
                dcmd.Parameters.AddWithValue("PositionID_", PositionBOObj.PositionID);
                dcmd.Parameters.AddWithValue("UpdatedBY", PositionBOObj.UserID);
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
