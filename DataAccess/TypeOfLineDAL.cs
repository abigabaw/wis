using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TypeOfLineDAL
    {
        string connStr = AppConfiguration.ConnectionString;

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="TypeOfLineBOobj"></param>
        /// <returns></returns>
        public string Insert(TypeOfLineBO TypeOfLineBOobj)
        {
            string returnResult = "";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INST_LINETYPE", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("p_TYPEOFLINE", TypeOfLineBOobj.TypeOfLine);
                dcmd.Parameters.AddWithValue("p_WAYLEAVEMEASUREMENT", TypeOfLineBOobj.Wayleavemeasurement);
                dcmd.Parameters.AddWithValue("p_RIGHTOFWAYMEASUREMENT", TypeOfLineBOobj.Rightofwaymeasurement);
                dcmd.Parameters.AddWithValue("p_CREATEDBY", TypeOfLineBOobj.Createdby);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

                return returnResult;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                dcmd.Dispose();
                con.Close();
                con.Dispose();

            }

        }

        /// <summary>
        /// To Get Line Type
        /// </summary>
        /// <returns></returns>
        public TypeOfLineLists GetLineType()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd;

            string proc = "USP_MST_GET_LINETYPE";

            cmd = new SqlCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TypeOfLineLists TypeOfLines = new TypeOfLineLists();
            TypeOfLineBO TypeOfLineBOobj = null;

            while (dr.Read())
            {
                TypeOfLineBOobj = new TypeOfLineBO();
                TypeOfLineBOobj.LineTypeId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LINETYPEID")));
                TypeOfLineBOobj.TypeOfLine = dr.GetString(dr.GetOrdinal("TYPEOFLINE"));
                TypeOfLineBOobj.Rightofwaymeasurement = dr.GetString(dr.GetOrdinal("RIGHTOFWAYMEASUREMENT"));
                TypeOfLineBOobj.Wayleavemeasurement = dr.GetString(dr.GetOrdinal("WAYLEAVEMEASUREMENT"));
                TypeOfLineBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                TypeOfLines.Add(TypeOfLineBOobj);
            }

            dr.Close();

            return TypeOfLines;
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public string Delete(int LineTypeID)
        {
            string result = "";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_DEL_LINETYPE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.AddWithValue("p_linetypeid", LineTypeID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                dCmd.ExecuteNonQuery();
                if (dCmd.Parameters["errorMessage_"].Value != null)
                    result = dCmd.Parameters["errorMessage_"].Value.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Get Line Type by ID
        /// </summary>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public TypeOfLineBO GetLineTypebyID(int LineTypeID)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd;

            string proc = "USP_MST_GET_LINETYPEID";

            cmd = new SqlCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_LINETYPEID", LineTypeID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TypeOfLineBO TypeOfLineBOobj = null;
            TypeOfLineLists Users = new TypeOfLineLists();

            TypeOfLineBOobj = new TypeOfLineBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "TYPEOFLINE") && !dr.IsDBNull(dr.GetOrdinal("TYPEOFLINE")))
                    TypeOfLineBOobj.TypeOfLine = dr.GetString(dr.GetOrdinal("TYPEOFLINE"));
                if (ColumnExists(dr, "WAYLEAVEMEASUREMENT") && !dr.IsDBNull(dr.GetOrdinal("WAYLEAVEMEASUREMENT")))
                    TypeOfLineBOobj.Wayleavemeasurement = dr.GetString(dr.GetOrdinal("WAYLEAVEMEASUREMENT"));
                if (ColumnExists(dr, "RIGHTOFWAYMEASUREMENT") && !dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAYMEASUREMENT")))
                    TypeOfLineBOobj.Rightofwaymeasurement = dr.GetString(dr.GetOrdinal("RIGHTOFWAYMEASUREMENT"));
                if (ColumnExists(dr, "LINETYPEID") && !dr.IsDBNull(dr.GetOrdinal("LINETYPEID")))
                    TypeOfLineBOobj.LineTypeId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LINETYPEID")));

            }
            dr.Close();


            return TypeOfLineBOobj;
        }

        /// <summary>
        /// To check the Column are Exists or not
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
        /// To Update
        /// </summary>
        /// <param name="TypeOfLineBOobj"></param>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public string Update(TypeOfLineBO TypeOfLineBOobj, int LineTypeID)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_UPD_LINE_TYPE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            string result = "";
            try
            {
                dCmd.Parameters.AddWithValue("p_LINETYPEID", LineTypeID);
                dCmd.Parameters.AddWithValue("p_TYPEOFLINE", TypeOfLineBOobj.TypeOfLine);
                dCmd.Parameters.AddWithValue("p_WAYLEAVEMEASUREMENT", TypeOfLineBOobj.Wayleavemeasurement);
                dCmd.Parameters.AddWithValue("p_RIGHTOFWAYMEASUREMENT", TypeOfLineBOobj.Rightofwaymeasurement);
                dCmd.Parameters.AddWithValue("p_UPDATEDBY", TypeOfLineBOobj.Createdby);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    result = dCmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// To Obsolete Line Type
        /// </summary>
        /// <param name="lineTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteLineType(int lineTypeID, string IsDeleted, int updatedBy)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_LINETYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("p_LINETYPEID", lineTypeID);
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

        /// <summary>
        /// To Get All Line Types
        /// </summary>
        /// <returns></returns>
        public TypeOfLineLists GetAllLineTypes()
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALLLINETYPE";

            cmd = new SqlCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TypeOfLineLists TypeOfLines = new TypeOfLineLists();
            TypeOfLineBO TypeOfLineBOobj = null;

            while (dr.Read())
            {
                TypeOfLineBOobj = new TypeOfLineBO();
                TypeOfLineBOobj.LineTypeId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LINETYPEID")));
                TypeOfLineBOobj.TypeOfLine = dr.GetString(dr.GetOrdinal("TYPEOFLINE"));
                TypeOfLineBOobj.Rightofwaymeasurement = dr.GetString(dr.GetOrdinal("RIGHTOFWAYMEASUREMENT"));
                TypeOfLineBOobj.Wayleavemeasurement = dr.GetString(dr.GetOrdinal("WAYLEAVEMEASUREMENT"));
                TypeOfLineBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                TypeOfLines.Add(TypeOfLineBOobj);
            }

            dr.Close();

            return TypeOfLines;
        }
    }
}