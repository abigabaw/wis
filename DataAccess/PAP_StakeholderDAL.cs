using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_StakeholderDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To Get Stake holder By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_StakeholderBO GetStakeholderByID(int householdID)
        {
            proc = "USP_TRN_GET_STAKEHOLDERBYID";
            cnn = new OracleConnection(con);
            PAP_StakeholderBO objStakeholder = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HOUSEHOLDID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStakeholder = new PAP_StakeholderBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("STAKEHOLDERID"))) objStakeholder.StakeHolderID = dr.GetInt32(dr.GetOrdinal("STAKEHOLDERID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objStakeholder.HouseHoldID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("STAKEHOLDERNAME"))) objStakeholder.StakeholderName = dr.GetString(dr.GetOrdinal("STAKEHOLDERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REPRESENTATION"))) objStakeholder.Representation = dr.GetString(dr.GetOrdinal("REPRESENTATION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RESIDENTIALADDRESS"))) objStakeholder.ResidentialAddress = dr.GetString(dr.GetOrdinal("RESIDENTIALADDRESS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("POSTALADDRESS"))) objStakeholder.PostalAddress = dr.GetString(dr.GetOrdinal("POSTALADDRESS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TELEPHONENO"))) objStakeholder.TelephoneNo = dr.GetString(dr.GetOrdinal("TELEPHONENO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DATEOFSURVEY"))) objStakeholder.DateOfSurvey = dr.GetDateTime(dr.GetOrdinal("DATEOFSURVEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objStakeholder.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objStakeholder.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objStakeholder.Subcounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objStakeholder.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objStakeholder.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SEGMENTID"))) objStakeholder.SegmentID = dr.GetInt32(dr.GetOrdinal("SEGMENTID"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStakeholder;
        }

        /// <summary>
        /// To Update Stake holder
        /// </summary>
        /// <param name="objStakeholder"></param>
        public void UpdateStakeholder(PAP_StakeholderBO objStakeholder)
        {
            cnn = new OracleConnection(con);

            proc = "USP_TRN_UPD_STAKEHOLDER";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("STAKEHOLDERID_", objStakeholder.StakeHolderID);
            cmd.Parameters.Add("HOUSEHOLDID_", objStakeholder.HouseHoldID);
            cmd.Parameters.Add("STAKEHOLDERNAME_", objStakeholder.StakeholderName);
            if (objStakeholder.Representation != "0")
                cmd.Parameters.Add("REPRESENTATION_", objStakeholder.Representation);
            else
                cmd.Parameters.Add("REPRESENTATION_",DBNull.Value);

            if (objStakeholder.ResidentialAddress.Trim().Length > 1000)
                cmd.Parameters.Add("RESIDENTIALADDRESS_", objStakeholder.ResidentialAddress.Substring(0, 1000));
            else
                cmd.Parameters.Add("RESIDENTIALADDRESS_", objStakeholder.ResidentialAddress);

            if (objStakeholder.PostalAddress.Trim().Length > 1000)
                cmd.Parameters.Add("POSTALADDRESS_", objStakeholder.PostalAddress.Substring(0, 1000));
            else
                cmd.Parameters.Add("POSTALADDRESS_", objStakeholder.PostalAddress);

            cmd.Parameters.Add("TELEPHONENO_", objStakeholder.TelephoneNo);

            if (objStakeholder.DateOfSurvey != null && objStakeholder.DateOfSurvey != DateTime.MinValue)
                cmd.Parameters.Add("DATEOFSURVEY_", objStakeholder.DateOfSurvey.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.Add("DATEOFSURVEY_", DBNull.Value);
            
            cmd.Parameters.Add("DISTRICT_", objStakeholder.District);
            cmd.Parameters.Add("COUNTY_", objStakeholder.County);
            cmd.Parameters.Add("SUBCOUNTY_", objStakeholder.Subcounty);
            cmd.Parameters.Add("PARISH_", objStakeholder.Parish);
            cmd.Parameters.Add("VILLAGE_", objStakeholder.Village);

            if (objStakeholder.SegmentID > 0)
                cmd.Parameters.Add("SEGMENTID_", objStakeholder.SegmentID);
            else
                cmd.Parameters.Add("SEGMENTID_", DBNull.Value);

            cmd.Parameters.Add("CREATEDBY_", objStakeholder.CreatedBy);
            cmd.Parameters.Add("UPDATEDBY_", objStakeholder.UpdatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
