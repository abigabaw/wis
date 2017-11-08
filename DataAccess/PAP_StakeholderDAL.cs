using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_StakeholderDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To Get Stake holder By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_StakeholderBO GetStakeholderByID(int householdID)
        {
            proc = "USP_TRN_GET_STAKEHOLDERBYID";
            cnn = new SqlConnection(con);
            PAP_StakeholderBO objStakeholder = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_STAKEHOLDER";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("STAKEHOLDERID_", objStakeholder.StakeHolderID);
            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objStakeholder.HouseHoldID);
            cmd.Parameters.AddWithValue("STAKEHOLDERNAME_", objStakeholder.StakeholderName);
            if (objStakeholder.Representation != "0")
                cmd.Parameters.AddWithValue("REPRESENTATION_", objStakeholder.Representation);
            else
                cmd.Parameters.AddWithValue("REPRESENTATION_",DBNull.Value);

            if (objStakeholder.ResidentialAddress.Trim().Length > 1000)
                cmd.Parameters.AddWithValue("RESIDENTIALADDRESS_", objStakeholder.ResidentialAddress.Substring(0, 1000));
            else
                cmd.Parameters.AddWithValue("RESIDENTIALADDRESS_", objStakeholder.ResidentialAddress);

            if (objStakeholder.PostalAddress.Trim().Length > 1000)
                cmd.Parameters.AddWithValue("POSTALADDRESS_", objStakeholder.PostalAddress.Substring(0, 1000));
            else
                cmd.Parameters.AddWithValue("POSTALADDRESS_", objStakeholder.PostalAddress);

            cmd.Parameters.AddWithValue("TELEPHONENO_", objStakeholder.TelephoneNo);

            if (objStakeholder.DateOfSurvey != null && objStakeholder.DateOfSurvey != DateTime.MinValue)
                cmd.Parameters.AddWithValue("DATEOFSURVEY_", objStakeholder.DateOfSurvey.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("DATEOFSURVEY_", DBNull.Value);
            
            cmd.Parameters.AddWithValue("DISTRICT_", objStakeholder.District);
            cmd.Parameters.AddWithValue("COUNTY_", objStakeholder.County);
            cmd.Parameters.AddWithValue("SUBCOUNTY_", objStakeholder.Subcounty);
            cmd.Parameters.AddWithValue("PARISH_", objStakeholder.Parish);
            cmd.Parameters.AddWithValue("VILLAGE_", objStakeholder.Village);

            if (objStakeholder.SegmentID > 0)
                cmd.Parameters.AddWithValue("SEGMENTID_", objStakeholder.SegmentID);
            else
                cmd.Parameters.AddWithValue("SEGMENTID_", DBNull.Value);

            cmd.Parameters.AddWithValue("CREATEDBY_", objStakeholder.CreatedBy);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objStakeholder.UpdatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
