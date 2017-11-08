using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTREPRESENTATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("REPRESENTATIONNAME_", objRepresentationBO.RepresentationName);
                dcmd.Parameters.AddWithValue("CREATEDBY_", objRepresentationBO.UserID);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_REPRESENTATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATEREPRESENTATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("REPRESENTATIONID_", objRepresentationBO.RepresentationID);
                dcmd.Parameters.AddWithValue("REPRESENTATIONNAME_", objRepresentationBO.RepresentationName);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", objRepresentationBO.UserID);
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
        /// To Get Representation By Id
        /// </summary>
        /// <param name="RepresentationID"></param>
        /// <returns></returns>
        public RepresentationBO GetRepresentationById(int RepresentationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_REPRESENTATIONBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("REPRESENTATIONID_", RepresentationID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEREPRESENTATION";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("REPRESENTATIONID_", RepresentationID);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETEREPRESENTATION";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("REPRESENTATIONID_", RepresentationID);
                cmd.Parameters.AddWithValue("ISDELETED_", IsDeleted);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
