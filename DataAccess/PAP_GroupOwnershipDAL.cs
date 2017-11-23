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
    public class PAP_GroupOwnershipDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = "";

        /// <summary>
        /// To Update Group Ownership Details
        /// </summary>
        /// <param name="objGroupOwnership"></param>
        /// <returns></returns>
        public string UpdateGroupOwnershipDetails(PAP_GroupOwnershipBO objGroupOwnership)
        {
            string result = "";
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_UPD_HH_GROUPOWNER", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Hh_IDIN", objGroupOwnership.HHID);
                cmd.Parameters.AddWithValue("PaptypeIN", objGroupOwnership.Paptype);
                cmd.Parameters.AddWithValue("DistrictIN", objGroupOwnership.DistrictIN);
                cmd.Parameters.AddWithValue("CountyIN", objGroupOwnership.CountyIN);
                cmd.Parameters.AddWithValue("SubCountyIN", objGroupOwnership.SubCountyIN);
                cmd.Parameters.AddWithValue("ParishIN", objGroupOwnership.ParishIN);
                cmd.Parameters.AddWithValue("VillageIN", objGroupOwnership.VillageIN);
                cmd.Parameters.AddWithValue("OptionGroupIdIN", objGroupOwnership.OptionGroupIdIN);
                cmd.Parameters.AddWithValue("PlotReferenceIN", objGroupOwnership.PlotReferenceIN);
                cmd.Parameters.AddWithValue("DateofBirthIN", objGroupOwnership.DateofBirthIN);
                cmd.Parameters.AddWithValue("IsResidentIN", objGroupOwnership.IsResidentIN);
                cmd.Parameters.AddWithValue("SexIN", objGroupOwnership.SexIN);
                cmd.Parameters.AddWithValue("SurnameIN", objGroupOwnership.SurnameIN);
                cmd.Parameters.AddWithValue("FirstnameIN", objGroupOwnership.FirstnameIN);
                cmd.Parameters.AddWithValue("OthernameIN", objGroupOwnership.OthernameIN);
                cmd.Parameters.AddWithValue("PositionidIN", objGroupOwnership.PositionidIN);
                cmd.Parameters.AddWithValue("Contactphone1IN", objGroupOwnership.Contactphone1IN);
                cmd.Parameters.AddWithValue("Contactphone2IN", objGroupOwnership.Contactphone2IN);
                cmd.Parameters.AddWithValue("UpdatedbyIN", objGroupOwnership.Createdby);
                cmd.Parameters.AddWithValue("PAP_UIDIN", objGroupOwnership.Papuid);
                cmd.Parameters.AddWithValue("@DETAILSCAPTUREDBYIN", objGroupOwnership.CapturedBy);
                if (objGroupOwnership.CapturedDate.Trim() != "")
                    cmd.Parameters.AddWithValue("@DETAILSCAPTUREDDATEIN", Convert.ToDateTime(objGroupOwnership.CapturedDate).ToString(UtilBO.DateFormatDB));
                else
                    cmd.Parameters.AddWithValue("@DETAILSCAPTUREDDATEIN", DBNull.Value);

                cmd.Parameters.AddWithValue("@GOUSTATUS_", objGroupOwnership.Gouallowance);
                cmd.Parameters.AddWithValue("@UNDERTAKINGPERIOD_", objGroupOwnership.Undertakingperiod);
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
        /// To Insert and Update Group Ownership
        /// </summary>
        /// <param name="objGroupOwnership"></param>
        public void InsertandUpdateGroupOwnership(PAP_GroupOwnershipBO objGroupOwnership)
        {
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_UPD_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GroupmemberidIN", objGroupOwnership.Groupmemberid);
                cmd.Parameters.AddWithValue("HhidIN", objGroupOwnership.HHID);
                cmd.Parameters.AddWithValue("SurnameIN", objGroupOwnership.SurnameIN);
                cmd.Parameters.AddWithValue("FirstnameIN", objGroupOwnership.FirstnameIN);
                cmd.Parameters.AddWithValue("OthernameIN", objGroupOwnership.OthernameIN);
                cmd.Parameters.AddWithValue("CreatedbyIN", objGroupOwnership.Createdby);
                cnn.Open();
                cmd.ExecuteNonQuery();
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
        }

        /// <summary>
        /// To Get Group Ownership By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PAP_GroupOwnershipList GetGroupOwnershipByHHID(int HHID)
        {
            PAP_GroupOwnershipBO objGroupOwnership = null;
            PAP_GroupOwnershipList PAP_GroupOwnershiplist1;
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_GET_PAP_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("HHIDIN", HHID);
                // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                PAP_GroupOwnershiplist1 = new PAP_GroupOwnershipList();
                while (dr.Read())
                {
                    objGroupOwnership = new PAP_GroupOwnershipBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objGroupOwnership.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GROUPMEMBERID"))) objGroupOwnership.Groupmemberid = dr.GetInt32(dr.GetOrdinal("GROUPMEMBERID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SURNAME"))) objGroupOwnership.SurnameIN = dr.GetString(dr.GetOrdinal("SURNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FIRSTNAME"))) objGroupOwnership.FirstnameIN = dr.GetString(dr.GetOrdinal("FIRSTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OTHERNAME"))) objGroupOwnership.OthernameIN = dr.GetString(dr.GetOrdinal("OTHERNAME"));
                    PAP_GroupOwnershiplist1.Add(objGroupOwnership);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PAP_GroupOwnershiplist1;
        }
        
        /// <summary>
        /// To Delete Group Ownership By GMID
        /// </summary>
        /// <param name="GMID"></param>
        public void DeleteGroupOwnershipByGMID(int GMID)
        {
            try
            {
                cnn = new SqlConnection(con);
                cmd = new SqlCommand("USP_TRN_DEL_PAP_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("GroupmemberidIN", GMID);
                cnn.Open();
                cmd.ExecuteNonQuery();
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
        }

    }
}
