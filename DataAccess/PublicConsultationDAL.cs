using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PublicConsultationDAL
    {

        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="oBO"></param>
        /// <returns></returns>
        public String Insert(PublicConsultationBO oBO)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PCDD", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("PROJECTID_", oBO.ProjectID);
                dcmd.Parameters.AddWithValue("DISTRICT_", oBO.District);
                dcmd.Parameters.AddWithValue("COUNTY_", oBO.County);
                dcmd.Parameters.AddWithValue("SUBCOUNTY_", oBO.SubCounty);
                dcmd.Parameters.AddWithValue("PARISH_", oBO.Parish);
                dcmd.Parameters.AddWithValue("VILLAGE_", oBO.Village);
                dcmd.Parameters.AddWithValue("NAMEOFPERSON_", oBO.NameOfPerson);
                dcmd.Parameters.AddWithValue("ADDRESS_", oBO.Address);
                dcmd.Parameters.AddWithValue("TELEPHONE_", oBO.Telephone);
                dcmd.Parameters.AddWithValue("STAKEHOLDINGCATEG_", oBO.StakeholdingCategory);

                if (oBO.ConsultationDate != DateTime.MinValue)
                    dcmd.Parameters.AddWithValue("CONSULTATIONDATE_", oBO.ConsultationDate.ToString(UtilBO.DateFormatDB));
                else
                    dcmd.Parameters.AddWithValue("CONSULTATIONDATE_", DBNull.Value);

                dcmd.Parameters.AddWithValue("PURPOSEOFMEETING_", oBO.PurposeOfMeeting);
                dcmd.Parameters.AddWithValue("ISSUES_", oBO.Issues);
                dcmd.Parameters.AddWithValue("REMEDIES_", oBO.Remedies);
                dcmd.Parameters.AddWithValue("OFFICERINCHARGE_", oBO.OfficerIncharge);
                dcmd.Parameters.AddWithValue("CREATEDBY_", oBO.CreatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                 dcmd.ExecuteNonQuery();

                 String result = dcmd.Parameters["errorMessage_"].Value.ToString();
                 return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }

        /// <summary>
        /// To Get Public Consultation
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public PublicConsultationList GetPublucConsultation(int projectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_PCDD";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("projectID_", projectID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            PublicConsultationBO oBO = null;
            PublicConsultationList listobj = new PublicConsultationList();
            while (dr.Read())
            {
                oBO = new PublicConsultationBO();

                oBO.CONSULTATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONSULTATIONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) oBO.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) oBO.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) oBO.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) oBO.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) oBO.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("NAMEOFPERSON"))) oBO.NameOfPerson = dr.GetString(dr.GetOrdinal("NAMEOFPERSON"));
                if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) oBO.Address = dr.GetString(dr.GetOrdinal("ADDRESS"));
                if (!dr.IsDBNull(dr.GetOrdinal("TELEPHONE"))) oBO.Telephone = dr.GetString(dr.GetOrdinal("TELEPHONE"));
                if (!dr.IsDBNull(dr.GetOrdinal("STAKEHOLDINGCATEG"))) oBO.StakeholdingCategory = dr.GetString(dr.GetOrdinal("STAKEHOLDINGCATEG"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONSULTATIONDATE"))) oBO.ConsultationDate = dr.GetDateTime(dr.GetOrdinal("CONSULTATIONDATE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PURPOSEOFMEETING"))) oBO.PurposeOfMeeting = dr.GetString(dr.GetOrdinal("PURPOSEOFMEETING"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISSUES"))) oBO.Issues = dr.GetString(dr.GetOrdinal("ISSUES"));
                if (!dr.IsDBNull(dr.GetOrdinal("REMEDIES"))) oBO.Remedies = dr.GetString(dr.GetOrdinal("REMEDIES"));
                if (!dr.IsDBNull(dr.GetOrdinal("OFFICERINCHARGENAME"))) oBO.OfficerInchargeName = dr.GetString(dr.GetOrdinal("OFFICERINCHARGENAME"));

                listobj.Add(oBO);
            }

            dr.Close();
            return listobj;
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="CONSULTATIONID"></param>
        /// <returns></returns>
        public PublicConsultationBO GetData(int CONSULTATIONID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PCDD";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("C_CONSULTATIONID", CONSULTATIONID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            PublicConsultationBO oBO = null;
            PublicConsultationList listobj = new PublicConsultationList();

            oBO = new PublicConsultationBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("CONSULTATIONID")))
                    oBO.CONSULTATIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONSULTATIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) oBO.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) oBO.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) oBO.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) oBO.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) oBO.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("NAMEOFPERSON"))) oBO.NameOfPerson = dr.GetString(dr.GetOrdinal("NAMEOFPERSON"));
                if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) oBO.Address = dr.GetString(dr.GetOrdinal("ADDRESS"));
                if (!dr.IsDBNull(dr.GetOrdinal("TELEPHONE"))) oBO.Telephone = dr.GetString(dr.GetOrdinal("TELEPHONE"));
                if (!dr.IsDBNull(dr.GetOrdinal("STAKEHOLDINGCATEG"))) oBO.StakeholdingCategory = dr.GetString(dr.GetOrdinal("STAKEHOLDINGCATEG"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONSULTATIONDATE"))) oBO.ConsultationDate = dr.GetDateTime(dr.GetOrdinal("CONSULTATIONDATE"));

                if (!dr.IsDBNull(dr.GetOrdinal("PURPOSEOFMEETING")))
                    oBO.PurposeOfMeeting = dr.GetString(dr.GetOrdinal("PURPOSEOFMEETING"));

                if (!dr.IsDBNull(dr.GetOrdinal("ISSUES")))
                    oBO.Issues = dr.GetString(dr.GetOrdinal("ISSUES"));

                if (!dr.IsDBNull(dr.GetOrdinal("REMEDIES")))
                    oBO.Remedies = dr.GetString(dr.GetOrdinal("REMEDIES"));

                if (!dr.IsDBNull(dr.GetOrdinal("OFFICERINCHARGE"))) oBO.OfficerIncharge = dr.GetInt32(dr.GetOrdinal("OFFICERINCHARGE"));

                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    oBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();


            return oBO;
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="oBO"></param>
        /// <returns></returns>
        public String Update(PublicConsultationBO oBO)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PCDD", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("consultationid_", oBO.CONSULTATIONID);

                dcmd.Parameters.AddWithValue("DISTRICT_", oBO.District);
                dcmd.Parameters.AddWithValue("COUNTY_", oBO.County);
                dcmd.Parameters.AddWithValue("SUBCOUNTY_", oBO.SubCounty);
                dcmd.Parameters.AddWithValue("PARISH_", oBO.Parish);
                dcmd.Parameters.AddWithValue("VILLAGE_", oBO.Village);
                dcmd.Parameters.AddWithValue("NAMEOFPERSON_", oBO.NameOfPerson);
                dcmd.Parameters.AddWithValue("ADDRESS_", oBO.Address);
                dcmd.Parameters.AddWithValue("TELEPHONE_", oBO.Telephone);
                dcmd.Parameters.AddWithValue("STAKEHOLDINGCATEG_", oBO.StakeholdingCategory);

                if (oBO.ConsultationDate != DateTime.MinValue)
                    dcmd.Parameters.AddWithValue("CONSULTATIONDATE_", oBO.ConsultationDate.ToString(UtilBO.DateFormatDB));
                else
                    dcmd.Parameters.AddWithValue("CONSULTATIONDATE_", DBNull.Value);

                dcmd.Parameters.AddWithValue("PURPOSEOFMEETING_", oBO.PurposeOfMeeting);
                dcmd.Parameters.AddWithValue("ISSUES_", oBO.Issues);
                dcmd.Parameters.AddWithValue("REMEDIES_", oBO.Remedies);
                dcmd.Parameters.AddWithValue("OFFICERINCHARGE_", oBO.OfficerIncharge);

                dcmd.Parameters.AddWithValue("updatedby_", oBO.UpdatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();
                String result = dcmd.Parameters["errorMessage_"].Value.ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
        }
    }
}
