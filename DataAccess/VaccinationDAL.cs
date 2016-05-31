using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_VACCINATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("VACCINATIONNAME", Vaccinationobj.VACCINATIONNAME);
                dcmd.Parameters.Add("CREATEDBY", Vaccinationobj.Createdby);
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
        /// To Edit Vaccination
        /// </summary>
        /// <param name="Vaccinationobj"></param>
        /// <returns></returns>
        public string EditVaccination(VaccinationBO Vaccinationobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_VACCINATION", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("V_VACCINATIONNAME", Vaccinationobj.VACCINATIONNAME);
                dcmd.Parameters.Add("V_VACCINATIONID", Vaccinationobj.VACCINATIONID);
                dcmd.Parameters.Add("V_UPDATEDBY", Vaccinationobj.Createdby);
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
        /// To Get ALL Vaccination
        /// </summary>
        /// <returns></returns>
        public VaccinationList GetALLVaccination()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_VACCINATION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_VACCINATION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_VACCIN";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("V_VACCINATIONID",VaccinationID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_VACCIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("V_VACCINATIONID", vaccinationID);
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
        /// To Obsolete Vaccination
        /// </summary>
        /// <param name="VaccinationID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoletevaccination(int VaccinationID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_VACCINATION", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("VACCINATIONID_", VaccinationID);
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

    }
}