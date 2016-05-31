using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class RepresentationDAL
    {
        /// <summary>
        /// To Insert Representation
        /// </summary>
        /// <param name="objRepresentationBO"></param>
        /// <returns></returns>
        public string InsertRepresentation(RepresentationBO objRepresentationBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTREPRESENTATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("REPRESENTATIONNAME_", objRepresentationBO.RepresentationName);
                dcmd.Parameters.Add("CREATEDBY_", objRepresentationBO.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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

        /// <summary>
        /// To Get Representation
        /// </summary>
        /// <returns></returns>
        public RepresentationList GetRepresentation()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_REPRESENTATION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RepresentationBO objRepresentationBO = null;
            RepresentationList Representation = new RepresentationList();

            while (dr.Read())
            {
                objRepresentationBO = new RepresentationBO();
                objRepresentationBO.RepresentationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("representationid")));
                objRepresentationBO.RepresentationName = dr.GetString(dr.GetOrdinal("representationname"));
                objRepresentationBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                Representation.Add(objRepresentationBO);
            }

            dr.Close();

            return Representation;
        }

        /// <summary>
        /// To Update Representation
        /// </summary>
        /// <param name="objRepresentationBO"></param>
        /// <returns></returns>
        public string UpdateRepresentation(RepresentationBO objRepresentationBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATEREPRESENTATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("REPRESENTATIONID_", objRepresentationBO.RepresentationID);
                dcmd.Parameters.Add("REPRESENTATIONNAME_", objRepresentationBO.RepresentationName);
                dcmd.Parameters.Add("UPDATEDBY_", objRepresentationBO.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
        /// To Get Representation By Id
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <returns></returns>
        public RepresentationBO GetRepresentationById(int RepresentationID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_REPRESENTATIONBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("REPRESENTATIONID_", RepresentationID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RepresentationBO objRepresentationBO = null;
            //RepresentationList Representation = new RepresentationList();

            objRepresentationBO = new RepresentationBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "representationid") && !dr.IsDBNull(dr.GetOrdinal("representationid")))
                    objRepresentationBO.RepresentationID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("representationid")));
                if (ColumnExists(dr, "representationname") && !dr.IsDBNull(dr.GetOrdinal("representationname")))
                    objRepresentationBO.RepresentationName = Convert.ToString(dr.GetValue(dr.GetOrdinal("representationname")));
                if (ColumnExists(dr, "isdeleted") && !dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                    objRepresentationBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

            }
            dr.Close();
            return objRepresentationBO;
        }

        /// <summary>
        /// To Delete Representation
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <returns></returns>
        public string DeleteRepresentation(int RepresentationID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEREPRESENTATION";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("REPRESENTATIONID_", RepresentationID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Representation is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// To Obsolete Representation
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRepresentation(int RepresentationID, string IsDeleted)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETEREPRESENTATION";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("REPRESENTATIONID_", RepresentationID);
                cmd.Parameters.Add("ISDELETED_", IsDeleted);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
