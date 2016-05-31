using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data;
using WIS_BusinessObjects.Collections;

namespace WIS_DataAccess
{
    public class PAP_InstitutionDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
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
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_UPD_HH_INSTITUTION", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Hh_IDIN", objInstitution.HHID);

                cmd.Parameters.Add("PaptypeIN", objInstitution.Paptype);
                cmd.Parameters.Add("DistrictIN", objInstitution.DistrictIN);
                cmd.Parameters.Add("CountyIN", objInstitution.CountyIN);
                cmd.Parameters.Add("SubCountyIN", objInstitution.SubCountyIN);
                cmd.Parameters.Add("ParishIN", objInstitution.ParishIN);
                cmd.Parameters.Add("VillageIN", objInstitution.VillageIN);
                cmd.Parameters.Add("OptionGroupIdIN", objInstitution.OptionGroupIdIN);
                cmd.Parameters.Add("NoofplotsIN", objInstitution.NoofplotsIN);
                cmd.Parameters.Add("InstitutionNameIN", objInstitution.InstitutionNameIN);
                cmd.Parameters.Add("PlotReferenceIN", objInstitution.PlotReferenceIN);
                cmd.Parameters.Add("DateofBirthIN", objInstitution.DateofBirthIN);
                cmd.Parameters.Add("IsResidentIN", objInstitution.IsResidentIN);
                cmd.Parameters.Add("SexIN", objInstitution.SexIN);
                cmd.Parameters.Add("SurnameIN", objInstitution.SurnameIN);
                cmd.Parameters.Add("FirstnameIN", objInstitution.FirstnameIN);
                cmd.Parameters.Add("OthernameIN", objInstitution.OthernameIN);
                cmd.Parameters.Add("UpdatedbyIN", objInstitution.UpdatedbyIN);
                cmd.Parameters.Add("@GOUSTATUS_", objInstitution.Gouallowance);
                cmd.Parameters.Add("@UNDERTAKINGPERIOD_", objInstitution.Undertakingperiod);

                cmd.Parameters.Add("POSITIONIDIN", objInstitution.POSITIONID);
                cmd.Parameters.Add("CONT_DISTRICTIN", objInstitution.CONT_DISTRICT);
                cmd.Parameters.Add("CONT_COUNTYIN", objInstitution.CONT_COUNTY);
                cmd.Parameters.Add("CONT_SUBCOUNTYIN", objInstitution.CONT_SUBCOUNTY);
                cmd.Parameters.Add("CONT_PARISHIN", objInstitution.CONT_PARISH);
                cmd.Parameters.Add("CONT_VILLAGEIN", objInstitution.CONT_VILLAGE);
                cmd.Parameters.Add("CONTACTPHONE1IN", objInstitution.CONTACTPHONE1);
                cmd.Parameters.Add("CONTACTPHONE2IN", objInstitution.CONTACTPHONE2);
                //cmd.Parameters.Add("PLOTPHOTOIN", objInstitution.PLOTPHOTO);
                cmd.Parameters.Add("CREATEDBYIN", objInstitution.CreatedBy);
                cmd.Parameters.Add("CONT_UPDATEBYIN", objInstitution.UpdatedbyIN);
                cmd.Parameters.Add("PAP_UIDIN", objInstitution.Papuid);
                cmd.Parameters.Add("@DETAILSCAPTUREDBYIN", objInstitution.CapturedBy);
                if (objInstitution.CapturedDate.Trim() != "")
                    cmd.Parameters.Add("@DETAILSCAPTUREDDATEIN", Convert.ToDateTime(objInstitution.CapturedDate).ToString(UtilBO.DateFormatDB));
                else
                    cmd.Parameters.Add("@DETAILSCAPTUREDDATEIN", DBNull.Value);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_GET_PAPINAT_CONTACT", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("HHIDIN", HHID);
                cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cnn.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
