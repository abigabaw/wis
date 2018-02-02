using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_HealthDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
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
            cnn = new SqlConnection(con);

            PAP_DisabilityBO objDisability = null;
            PAP_DisabilityList Disabilities = new PAP_DisabilityList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_PAPDISABILITY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HHID_", objDisability.HouseholdID);
            cmd.Parameters.AddWithValue("DISABILITYID_", objDisability.DisabilityID);
            cmd.Parameters.AddWithValue("HEALTHCARENEEDED_", objDisability.HealthCareNeeded);
            cmd.Parameters.AddWithValue("createdBy_", objDisability.CreatedBy);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            cnn = new SqlConnection(con);

            PAP_DisabilityBO objDisability = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PAP_DISABILITYID_", PAPDisabilityID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PAPDISABILITY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("PAP_DISABILITYID_", objDisability.PAPDisabilityID);
            cmd.Parameters.AddWithValue("HHID_", objDisability.HouseholdID);
            cmd.Parameters.AddWithValue("DISABILITYID_", objDisability.DisabilityID);
            cmd.Parameters.AddWithValue("HEALTHCARENEEDED_", objDisability.HealthCareNeeded);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objDisability.UpdatedBy);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_DISABILITY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("DISABILITYID_", disabilityID);
                myCommand.Parameters.AddWithValue("ISDELETED_", IsDeleted);
                myCommand.Parameters.AddWithValue("UPDATEDBY_", updatedBy);
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
        /// To Delete Disability
        /// </summary>
        /// <param name="disabilityID"></param>
        public void DeleteDisability(int disabilityID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_PAPDISABILITY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("PAP_DISABILITYID_", disabilityID);
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
            cnn = new SqlConnection(con);
            PAP_HealthBO objPAPHealth = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            SqlDataReader dr = null;
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
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PAPHEALTH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HHID_", objPAPHealth.HouseholdID);
            cmd.Parameters.AddWithValue("NEARESETHEALTHCENTRE_", objPAPHealth.NearestHealthCentre);
            cmd.Parameters.AddWithValue("DISTANCETOHEALTHCENTRE_", objPAPHealth.DistanceToHealthCentre);
            cmd.Parameters.AddWithValue("USEDBYFAMILY_", objPAPHealth.UsedByFamily);
            cmd.Parameters.AddWithValue("NONUSEREASON_", objPAPHealth.NonUseReason);
            cmd.Parameters.AddWithValue("NOOFBIRTH_", objPAPHealth.NoOfBirth);
            cmd.Parameters.AddWithValue("NOOFDEATH_", objPAPHealth.NoOfDeath);
            cmd.Parameters.AddWithValue("REASONFORDEATH_", objPAPHealth.ReasonForDeath);
            cmd.Parameters.AddWithValue("COMMONDISEASES_", objPAPHealth.CommonDiseases);
            cmd.Parameters.AddWithValue("PRACTICEFAMILYPLANNING_", objPAPHealth.PracticeFamilyPlanning);
            cmd.Parameters.AddWithValue("HEARDOFAIDS_", objPAPHealth.HeardOfAIDS);
            cmd.Parameters.AddWithValue("HOWCONTRACTED_", objPAPHealth.HowContracted);
            cmd.Parameters.AddWithValue("HOWAVOIDED_", objPAPHealth.HowAvoided);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objPAPHealth.UpdatedBy);
            cmd.ExecuteNonQuery();
           
            cmd.Connection.Close();
        }
    }
}
