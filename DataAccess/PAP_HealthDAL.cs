using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_HealthDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        #region "Disability"
        /// <summary>
        /// To Get Disabilities
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_DisabilityList GetDisabilities(int householdID)
        {
            proc = "USP_TRN_GET_PAPDISABILITIES";
            cnn = new OracleConnection(con);

            PAP_DisabilityBO objDisability = null;
            PAP_DisabilityList Disabilities = new PAP_DisabilityList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objDisability = new PAP_DisabilityBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PAP_DISABILITYID"))) objDisability.PAPDisabilityID = dr.GetInt32(dr.GetOrdinal("PAP_DISABILITYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objDisability.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISABILITYID"))) objDisability.DisabilityID = dr.GetInt32(dr.GetOrdinal("DISABILITYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISABILITYNAME"))) objDisability.DisabilityName = dr.GetString(dr.GetOrdinal("DISABILITYNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HEALTHCARENEEDED"))) objDisability.HealthCareNeeded = dr.GetString(dr.GetOrdinal("HEALTHCARENEEDED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objDisability.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                    Disabilities.Add(objDisability);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Disabilities;
        }

        /// <summary>
        /// To Add Disability
        /// </summary>
        /// <param name="objDisability"></param>
        /// <returns></returns>
        public string AddDisability(PAP_DisabilityBO objDisability)
        {
            string result = "";

            cnn = new OracleConnection(con);

            proc = "USP_TRN_INS_PAPDISABILITY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HHID_", objDisability.HouseholdID);
            cmd.Parameters.Add("DISABILITYID_", objDisability.DisabilityID);
            cmd.Parameters.Add("HEALTHCARENEEDED_", objDisability.HealthCareNeeded);
            cmd.Parameters.Add("createdBy_", objDisability.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                result = string.Empty;

            cmd.Connection.Close();

            return result;
        }

        /// <summary>
        /// To Get Disability By ID
        /// </summary>
        /// <param name="PAPDisabilityID"></param>
        /// <returns></returns>
        public PAP_DisabilityBO GetDisabilityByID(int PAPDisabilityID)
        {
            proc = "USP_TRN_GET_PAPDISABILITYBYID";
            cnn = new OracleConnection(con);

            PAP_DisabilityBO objDisability = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PAP_DISABILITYID_", PAPDisabilityID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objDisability = new PAP_DisabilityBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("DISABILITYID"))) objDisability.DisabilityID = dr.GetInt32(dr.GetOrdinal("DISABILITYID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HEALTHCARENEEDED"))) objDisability.HealthCareNeeded = dr.GetString(dr.GetOrdinal("HEALTHCARENEEDED"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDisability;
        }

        /// <summary>
        /// To Update Disability
        /// </summary>
        /// <param name="objDisability"></param>
        /// <returns></returns>
        public string UpdateDisability(PAP_DisabilityBO objDisability)
        {
            string result = "";

            cnn = new OracleConnection(con);

            proc = "USP_TRN_UPD_PAPDISABILITY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("PAP_DISABILITYID_", objDisability.PAPDisabilityID);
            cmd.Parameters.Add("HHID_", objDisability.HouseholdID);
            cmd.Parameters.Add("DISABILITYID_", objDisability.DisabilityID);
            cmd.Parameters.Add("HEALTHCARENEEDED_", objDisability.HealthCareNeeded);
            cmd.Parameters.Add("UPDATEDBY_", objDisability.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();

            cmd.Connection.Close();

            return result;
        }

        /// <summary>
        /// To Obsolete Disability
        /// </summary>
        /// <param name="disabilityID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteDisability(int disabilityID, string IsDeleted, int updatedBy)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_DISABILITY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("DISABILITYID_", disabilityID);
                myCommand.Parameters.Add("ISDELETED_", IsDeleted);
                myCommand.Parameters.Add("UPDATEDBY_", updatedBy);
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
        /// To Delete Disability
        /// </summary>
        /// <param name="disabilityID"></param>
        public void DeleteDisability(int disabilityID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_TRN_DEL_PAPDISABILITY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Connection.Open();
                cmd.Parameters.Add("PAP_DISABILITYID_", disabilityID);
                cmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        #endregion

        /// <summary>
        /// To Get Health Info By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_HealthBO GetHealthInfoByID(int householdID)
        {
            proc = "USP_TRN_GET_PAPHEALTH";
            cnn = new OracleConnection(con);
            PAP_HealthBO objPAPHealth = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader dr = null;
            try
            {
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPHealth = new PAP_HealthBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PAP_HEALTHID"))) objPAPHealth.HealthID = dr.GetInt32(dr.GetOrdinal("PAP_HEALTHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAPHealth.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NEARESETHEALTHCENTRE"))) objPAPHealth.NearestHealthCentre = dr.GetString(dr.GetOrdinal("NEARESETHEALTHCENTRE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTANCETOHEALTHCENTRE"))) objPAPHealth.DistanceToHealthCentre = dr.GetString(dr.GetOrdinal("DISTANCETOHEALTHCENTRE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("USEDBYFAMILY"))) objPAPHealth.UsedByFamily = dr.GetString(dr.GetOrdinal("USEDBYFAMILY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NONUSEREASON"))) objPAPHealth.NonUseReason = dr.GetString(dr.GetOrdinal("NONUSEREASON"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOOFBIRTH"))) objPAPHealth.NoOfBirth = dr.GetInt32(dr.GetOrdinal("NOOFBIRTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NOOFDEATH"))) objPAPHealth.NoOfDeath = dr.GetInt32(dr.GetOrdinal("NOOFDEATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REASONFORDEATH"))) objPAPHealth.ReasonForDeath = dr.GetString(dr.GetOrdinal("REASONFORDEATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COMMONDISEASES"))) objPAPHealth.CommonDiseases = dr.GetString(dr.GetOrdinal("COMMONDISEASES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRACTICEFAMILYPLANNING"))) objPAPHealth.PracticeFamilyPlanning = dr.GetString(dr.GetOrdinal("PRACTICEFAMILYPLANNING"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HEARDOFAIDS"))) objPAPHealth.HeardOfAIDS = dr.GetString(dr.GetOrdinal("HEARDOFAIDS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOWCONTRACTED"))) objPAPHealth.HowContracted = dr.GetString(dr.GetOrdinal("HOWCONTRACTED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOWAVOIDED"))) objPAPHealth.HowAvoided = dr.GetString(dr.GetOrdinal("HOWAVOIDED"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }

            return objPAPHealth;
        }

        /// <summary>
        /// To Update Health Info
        /// </summary>
        /// <param name="objPAPHealth"></param>
        public void UpdateHealthInfo(PAP_HealthBO objPAPHealth)
        {
            cnn = new OracleConnection(con);

            proc = "USP_TRN_UPD_PAPHEALTH";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HHID_", objPAPHealth.HouseholdID);
            cmd.Parameters.Add("NEARESETHEALTHCENTRE_", objPAPHealth.NearestHealthCentre);
            cmd.Parameters.Add("DISTANCETOHEALTHCENTRE_", objPAPHealth.DistanceToHealthCentre);
            cmd.Parameters.Add("USEDBYFAMILY_", objPAPHealth.UsedByFamily);
            cmd.Parameters.Add("NONUSEREASON_", objPAPHealth.NonUseReason);
            cmd.Parameters.Add("NOOFBIRTH_", objPAPHealth.NoOfBirth);
            cmd.Parameters.Add("NOOFDEATH_", objPAPHealth.NoOfDeath);
            cmd.Parameters.Add("REASONFORDEATH_", objPAPHealth.ReasonForDeath);
            cmd.Parameters.Add("COMMONDISEASES_", objPAPHealth.CommonDiseases);
            cmd.Parameters.Add("PRACTICEFAMILYPLANNING_", objPAPHealth.PracticeFamilyPlanning);
            cmd.Parameters.Add("HEARDOFAIDS_", objPAPHealth.HeardOfAIDS);
            cmd.Parameters.Add("HOWCONTRACTED_", objPAPHealth.HowContracted);
            cmd.Parameters.Add("HOWAVOIDED_", objPAPHealth.HowAvoided);
            cmd.Parameters.Add("UPDATEDBY_", objPAPHealth.UpdatedBy);
            cmd.ExecuteNonQuery();
           
            cmd.Connection.Close();
        }
    }
}
