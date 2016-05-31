using System;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INST_LINETYPE", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("p_TYPEOFLINE", TypeOfLineBOobj.TypeOfLine);
                dcmd.Parameters.Add("p_WAYLEAVEMEASUREMENT", TypeOfLineBOobj.Wayleavemeasurement);
                dcmd.Parameters.Add("p_RIGHTOFWAYMEASUREMENT", TypeOfLineBOobj.Rightofwaymeasurement);
                dcmd.Parameters.Add("p_CREATEDBY", TypeOfLineBOobj.Createdby);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection conn = new OracleConnection(connStr);
            OracleCommand cmd;

            string proc = " USP_MST_GET_LINETYPE";

            cmd = new OracleCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_DEL_LINETYPE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_linetypeid", LineTypeID);
                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection conn = new OracleConnection(connStr);
            OracleCommand cmd;

            string proc = "USP_MST_GET_LINETYPEID";

            cmd = new OracleCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_LINETYPEID", LineTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection conn = new OracleConnection(connStr);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPD_LINE_TYPE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            string result = "";
            try
            {
                dCmd.Parameters.Add("p_LINETYPEID", LineTypeID);
                dCmd.Parameters.Add("p_TYPEOFLINE", TypeOfLineBOobj.TypeOfLine);
                dCmd.Parameters.Add("p_WAYLEAVEMEASUREMENT", TypeOfLineBOobj.Wayleavemeasurement);
                dCmd.Parameters.Add("p_RIGHTOFWAYMEASUREMENT", TypeOfLineBOobj.Rightofwaymeasurement);
                dCmd.Parameters.Add("p_UPDATEDBY", TypeOfLineBOobj.Createdby);
                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_LINETYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_LINETYPEID", lineTypeID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("updatedBy_", updatedBy);
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
        /// To Get All Line Types
        /// </summary>
        /// <returns></returns>
        public TypeOfLineLists GetAllLineTypes()
        {
            OracleConnection conn = new OracleConnection(connStr);
            OracleCommand cmd;

            string proc = "USP_MST_GET_ALLLINETYPE";

            cmd = new OracleCommand(proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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