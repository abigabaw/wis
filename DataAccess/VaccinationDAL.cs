using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class VaccinationDAL
    {
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="Vaccinationobj"></param>
        /// <returns></returns>
        public string Insert(VaccinationBO Vaccinationobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_VACCINATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("VACCINATIONNAME", Vaccinationobj.VACCINATIONNAME);
                dcmd.Parameters.AddWithValue("CREATEDBY", Vaccinationobj.Createdby);
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
        /// To Edit Vaccination
        /// </summary>
        /// <param name="Vaccinationobj"></param>
        /// <returns></returns>
        public string EditVaccination(VaccinationBO Vaccinationobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_VACCINATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("V_VACCINATIONNAME", Vaccinationobj.VACCINATIONNAME);
                dcmd.Parameters.AddWithValue("V_VACCINATIONID", Vaccinationobj.VACCINATIONID);
                dcmd.Parameters.AddWithValue("V_UPDATEDBY", Vaccinationobj.Createdby);
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
        /// To Get ALL Vaccination
        /// </summary>
        /// <returns></returns>
        public VaccinationList GetALLVaccination()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_VACCINATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            VaccinationBO Vaccinationobj = null;
            VaccinationList VaccinationListobj = new VaccinationList();

        
            while (dr.Read())
            {
                Vaccinationobj = new VaccinationBO();
                Vaccinationobj.VACCINATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("vaccinationid")));
                Vaccinationobj.VACCINATIONNAME = dr.GetString(dr.GetOrdinal("vaccinationname"));
                Vaccinationobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                VaccinationListobj.Add(Vaccinationobj);
            }

         
            dr.Close();

            return VaccinationListobj;;
        }


        /// <summary>
        /// To Get Vaccination
        /// </summary>
        /// <returns></returns>
        public VaccinationList GetVaccination()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_VACCINATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            VaccinationBO Vaccinationobj = null;
            VaccinationList VaccinationListobj = new VaccinationList();

            while (dr.Read())
            {
                Vaccinationobj = new VaccinationBO();
                Vaccinationobj.VACCINATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("vaccinationid")));
                Vaccinationobj.VACCINATIONNAME = dr.GetString(dr.GetOrdinal("vaccinationname"));
               // Vaccinationobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                VaccinationListobj.Add(Vaccinationobj);
            }

            dr.Close();

            return VaccinationListobj;
        }

        /// <summary>
        /// To Get Vaccination By Id
        /// </summary>
        /// <param name="VaccinationID"></param>
        /// <returns></returns>
        public VaccinationBO GetVaccinationById(int VaccinationID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_VACCIN";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("V_VACCINATIONID",VaccinationID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            VaccinationBO Vaccinationobj = null;
            VaccinationList VaccinationListobj = new VaccinationList();

            Vaccinationobj = new VaccinationBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "VACCINATIONNAME") && !dr.IsDBNull(dr.GetOrdinal("VACCINATIONNAME")))
                    Vaccinationobj.VACCINATIONNAME = dr.GetString(dr.GetOrdinal("VACCINATIONNAME"));
                if (ColumnExists(dr, "VACCINATIONID") && !dr.IsDBNull(dr.GetOrdinal("VACCINATIONID")))
                    Vaccinationobj.VACCINATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("VACCINATIONID")));

            }
            dr.Close();


            return Vaccinationobj;
        }

        /// <summary>
        /// To Check weather Column Exists or Not
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
        /// To Delete Vaccination
        /// </summary>
        /// <param name="vaccinationID"></param>
        /// <returns></returns>
        public string DeleteVaccination(int vaccinationID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_VACCIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("V_VACCINATIONID", vaccinationID);
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
        /// To Obsolete Vaccination
        /// </summary>
        /// <param name="VaccinationID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoletevaccination(int VaccinationID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_VACCINATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("VACCINATIONID_", VaccinationID);
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

    }
}