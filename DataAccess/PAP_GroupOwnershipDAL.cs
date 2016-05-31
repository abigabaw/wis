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
    public class PAP_GroupOwnershipDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
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
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_UPD_HH_GROUPOWNER", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Hh_IDIN", objGroupOwnership.HHID);
                cmd.Parameters.Add("PaptypeIN", objGroupOwnership.Paptype);
                cmd.Parameters.Add("DistrictIN", objGroupOwnership.DistrictIN);
                cmd.Parameters.Add("CountyIN", objGroupOwnership.CountyIN);
                cmd.Parameters.Add("SubCountyIN", objGroupOwnership.SubCountyIN);
                cmd.Parameters.Add("ParishIN", objGroupOwnership.ParishIN);
                cmd.Parameters.Add("VillageIN", objGroupOwnership.VillageIN);
                cmd.Parameters.Add("OptionGroupIdIN", objGroupOwnership.OptionGroupIdIN);
                cmd.Parameters.Add("PlotReferenceIN", objGroupOwnership.PlotReferenceIN);
                cmd.Parameters.Add("DateofBirthIN", objGroupOwnership.DateofBirthIN);
                cmd.Parameters.Add("IsResidentIN", objGroupOwnership.IsResidentIN);
                cmd.Parameters.Add("SexIN", objGroupOwnership.SexIN);
                cmd.Parameters.Add("SurnameIN", objGroupOwnership.SurnameIN);
                cmd.Parameters.Add("FirstnameIN", objGroupOwnership.FirstnameIN);
                cmd.Parameters.Add("OthernameIN", objGroupOwnership.OthernameIN);
                cmd.Parameters.Add("PositionidIN", objGroupOwnership.PositionidIN);
                cmd.Parameters.Add("Contactphone1IN", objGroupOwnership.Contactphone1IN);
                cmd.Parameters.Add("Contactphone2IN", objGroupOwnership.Contactphone2IN);
                cmd.Parameters.Add("UpdatedbyIN", objGroupOwnership.Createdby);
                cmd.Parameters.Add("PAP_UIDIN", objGroupOwnership.Papuid);
                cmd.Parameters.Add("@DETAILSCAPTUREDBYIN", objGroupOwnership.CapturedBy);
                if (objGroupOwnership.CapturedDate.Trim() != "")
                    cmd.Parameters.Add("@DETAILSCAPTUREDDATEIN", Convert.ToDateTime(objGroupOwnership.CapturedDate).ToString(UtilBO.DateFormatDB));
                else
                    cmd.Parameters.Add("@DETAILSCAPTUREDDATEIN", DBNull.Value);

                cmd.Parameters.Add("@GOUSTATUS_", objGroupOwnership.Gouallowance);
                cmd.Parameters.Add("@UNDERTAKINGPERIOD_", objGroupOwnership.Undertakingperiod);
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
        /// To Insert and Update Group Ownership
        /// </summary>
        /// <param name="objGroupOwnership"></param>
        public void InsertandUpdateGroupOwnership(PAP_GroupOwnershipBO objGroupOwnership)
        {
            try
            {
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_UPD_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("GroupmemberidIN", objGroupOwnership.Groupmemberid);
                cmd.Parameters.Add("HhidIN", objGroupOwnership.HHID);
                cmd.Parameters.Add("SurnameIN", objGroupOwnership.SurnameIN);
                cmd.Parameters.Add("FirstnameIN", objGroupOwnership.FirstnameIN);
                cmd.Parameters.Add("OthernameIN", objGroupOwnership.OthernameIN);
                cmd.Parameters.Add("CreatedbyIN", objGroupOwnership.Createdby);
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
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_GET_PAP_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("HHIDIN", HHID);
                cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cnn.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_TRN_DEL_PAP_GROUPMEMBERS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("GroupmemberidIN", GMID);
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
