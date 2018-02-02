using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WIS_BusinessObjects;
using System.Data;
using WIS_BusinessObjects.Collections;

namespace WIS_DataAccess
{
    public class PAP_InstitutionDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = "";
        /// <summary>
        /// To Update Institution Details
        /// </summary>
        /// <param name="objInstitution"></param>
        /// <returns></returns>
        public string UpdateInstitutionDetails(PAP_InstitutionBO objInstitution)
        {
            string result = "";
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_UPD_HH_INSTITUTION", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Hh_IDIN", objInstitution.HHID);

                cmd.Parameters.AddWithValue("PaptypeIN", objInstitution.Paptype);
                cmd.Parameters.AddWithValue("DistrictIN", objInstitution.DistrictIN);
                cmd.Parameters.AddWithValue("CountyIN", objInstitution.CountyIN);
                cmd.Parameters.AddWithValue("SubCountyIN", objInstitution.SubCountyIN);
                cmd.Parameters.AddWithValue("ParishIN", objInstitution.ParishIN);
                cmd.Parameters.AddWithValue("VillageIN", objInstitution.VillageIN);
                cmd.Parameters.AddWithValue("OptionGroupIdIN", objInstitution.OptionGroupIdIN);
                cmd.Parameters.AddWithValue("NoofplotsIN", objInstitution.NoofplotsIN);
                cmd.Parameters.AddWithValue("InstitutionNameIN", objInstitution.InstitutionNameIN);
                cmd.Parameters.AddWithValue("PlotReferenceIN", objInstitution.PlotReferenceIN);
                cmd.Parameters.AddWithValue("DateofBirthIN", objInstitution.DateofBirthIN);
                cmd.Parameters.AddWithValue("IsResidentIN", objInstitution.IsResidentIN);
                cmd.Parameters.AddWithValue("SexIN", objInstitution.SexIN);
                cmd.Parameters.AddWithValue("SurnameIN", objInstitution.SurnameIN);
                cmd.Parameters.AddWithValue("FirstnameIN", objInstitution.FirstnameIN);
                cmd.Parameters.AddWithValue("OthernameIN", objInstitution.OthernameIN);
                cmd.Parameters.AddWithValue("UpdatedbyIN", objInstitution.UpdatedbyIN);
                cmd.Parameters.AddWithValue("@GOUSTATUS_", objInstitution.Gouallowance);
                cmd.Parameters.AddWithValue("@UNDERTAKINGPERIOD_", objInstitution.Undertakingperiod);

                cmd.Parameters.AddWithValue("POSITIONIDIN", objInstitution.POSITIONID);
                cmd.Parameters.AddWithValue("CONT_DISTRICTIN", objInstitution.CONT_DISTRICT);
                cmd.Parameters.AddWithValue("CONT_COUNTYIN", objInstitution.CONT_COUNTY);
                cmd.Parameters.AddWithValue("CONT_SUBCOUNTYIN", objInstitution.CONT_SUBCOUNTY);
                cmd.Parameters.AddWithValue("CONT_PARISHIN", objInstitution.CONT_PARISH);
                cmd.Parameters.AddWithValue("CONT_VILLAGEIN", objInstitution.CONT_VILLAGE);
                cmd.Parameters.AddWithValue("CONTACTPHONE1IN", objInstitution.CONTACTPHONE1);
                cmd.Parameters.AddWithValue("CONTACTPHONE2IN", objInstitution.CONTACTPHONE2);
                //cmd.Parameters.AddWithValue("PLOTPHOTOIN", objInstitution.PLOTPHOTO);
                cmd.Parameters.AddWithValue("CREATEDBYIN", objInstitution.CreatedBy);
                cmd.Parameters.AddWithValue("CONT_UPDATEBYIN", objInstitution.UpdatedbyIN);
                cmd.Parameters.AddWithValue("PAP_UIDIN", objInstitution.Papuid);
                cmd.Parameters.AddWithValue("@DETAILSCAPTUREDBYIN", objInstitution.CapturedBy);
                if (objInstitution.CapturedDate.Trim() != "")
                    cmd.Parameters.AddWithValue("@DETAILSCAPTUREDDATEIN", Convert.ToDateTime(objInstitution.CapturedDate).ToString(UtilBO.DateFormatDB));
                else
                    cmd.Parameters.AddWithValue("@DETAILSCAPTUREDDATEIN", DBNull.Value);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                cnn.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// To Get Inst Contact By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PAP_InstitutionList GetInstContactByHHID(int HHID)
        {
            PAP_InstitutionBO objInstitution = null;
            PAP_InstitutionList PAP_Institutionlist1;
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_GET_PAPINAT_CONTACT", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("HHIDIN", HHID);
                // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                PAP_Institutionlist1 = new PAP_InstitutionList();
                while (dr.Read())
                {
                    objInstitution = new PAP_InstitutionBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objInstitution.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("POSITIONID"))) objInstitution.POSITIONID = dr.GetInt32(dr.GetOrdinal("POSITIONID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONT_DISTRICT"))) objInstitution.CONT_DISTRICT = dr.GetString(dr.GetOrdinal("CONT_DISTRICT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONT_COUNTY"))) objInstitution.CONT_COUNTY = dr.GetString(dr.GetOrdinal("CONT_COUNTY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONT_SUBCOUNTY"))) objInstitution.CONT_SUBCOUNTY = dr.GetString(dr.GetOrdinal("CONT_SUBCOUNTY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONT_PARISH"))) objInstitution.CONT_PARISH = dr.GetString(dr.GetOrdinal("CONT_PARISH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONT_VILLAGE"))) objInstitution.CONT_VILLAGE = dr.GetString(dr.GetOrdinal("CONT_VILLAGE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONTACTPHONE1"))) objInstitution.CONTACTPHONE1 = dr.GetString(dr.GetOrdinal("CONTACTPHONE1"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONTACTPHONE2"))) objInstitution.CONTACTPHONE2 = dr.GetString(dr.GetOrdinal("CONTACTPHONE2"));
                    PAP_Institutionlist1.Add(objInstitution);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PAP_Institutionlist1;
        }
    }
}
