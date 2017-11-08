using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class HealthCenterDAL
    {
        /// <summary>
        /// To Insert into DB
        /// </summary>
        /// <param name="HealthCenterBOobj"></param>
        /// <returns></returns>
        public string Insert(HealthCenterBO HealthCenterBOobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_HEALTHCENTER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HEALTHCENTERNAME", HealthCenterBOobj.HEALTHCENTERNAME);
                dcmd.Parameters.AddWithValue("CREATEDBY", HealthCenterBOobj.CREATEDBY);
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

        /// <summary>
        /// To Edit
        /// </summary>
        public string Edit(HealthCenterBO HealthCenterBOobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_HEALTHCENTER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("H_HEALTHCENTERNAME", HealthCenterBOobj.HEALTHCENTERNAME);
                dcmd.Parameters.AddWithValue("H_HEALTHCENTERID", HealthCenterBOobj.HEALTHCENTERID);
                dcmd.Parameters.AddWithValue("H_UPDATEDBY", HealthCenterBOobj.CREATEDBY);
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

        /// <summary>
        /// To Get ALL Health Center
        /// </summary>
        /// <returns></returns>
        public HealthCenterList GetALLHealthCenter()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_HEALTH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            HealthCenterBO HealthCenterBOobj = null;
            HealthCenterList HealthCenterListobj = new HealthCenterList();


            while (dr.Read())
            {
                HealthCenterBOobj = new HealthCenterBO();
                HealthCenterBOobj.HEALTHCENTERID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("healthcenterid")));
                HealthCenterBOobj.HEALTHCENTERNAME = dr.GetString(dr.GetOrdinal("healthcentername"));
                HealthCenterBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                HealthCenterListobj.Add(HealthCenterBOobj);
            }


            dr.Close();

            return HealthCenterListobj; ;
        }

        /// <summary>
        /// To Get Health Center
        /// </summary>
        /// <returns></returns>
        public HealthCenterList GetHealthCenter()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_HEALTH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            HealthCenterBO HealthCenterBOobj = null;
            HealthCenterList HealthCenterListobj = new HealthCenterList();

            while (dr.Read())
            {
                HealthCenterBOobj = new HealthCenterBO();
                HealthCenterBOobj.HEALTHCENTERID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("healthcenterid")));
                HealthCenterBOobj.HEALTHCENTERNAME = dr.GetString(dr.GetOrdinal("healthcentername"));
                // Vaccinationobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                HealthCenterListobj.Add(HealthCenterBOobj);
            }

            dr.Close();

            return HealthCenterListobj;
        }

        /// <summary>
        /// To Get Health Center By Id
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <returns></returns>
        public HealthCenterBO GetHealthCenterById(int HEALTHCENTERID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_HEALTH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("H_HEALTHCENTERID", HEALTHCENTERID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            HealthCenterBO HealthCenterBOobj = null;
            HealthCenterList HealthCenterListobj = new HealthCenterList();

            HealthCenterBOobj = new HealthCenterBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "HEALTHCENTERNAME") && !dr.IsDBNull(dr.GetOrdinal("HEALTHCENTERNAME")))
                    HealthCenterBOobj.HEALTHCENTERNAME = dr.GetString(dr.GetOrdinal("HEALTHCENTERNAME"));
                if (ColumnExists(dr, "HEALTHCENTERID") && !dr.IsDBNull(dr.GetOrdinal("HEALTHCENTERID")))
                    HealthCenterBOobj.HEALTHCENTERID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HEALTHCENTERID")));

            }
            dr.Close();


            return HealthCenterBOobj;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool ColumnExists(IDataReader reader, string columnName)
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
        /// To Delete Health Center
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <returns></returns>
        public string DeleteHealthCenter(int HEALTHCENTERID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_HEALTH", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("H_HEALTHCENTERID", HEALTHCENTERID);
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
        /// To Obsolete Health Center
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteHealthCenter(int HEALTHCENTERID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_HEALTH", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("HEALTHCENTERID_", HEALTHCENTERID);
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