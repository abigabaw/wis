using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LandInfoDAL
    {
        /// <summary>
        /// To Get Land Info
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PublicLandInfoBO GetLandInfo(int householdID)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_PUBLIC";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PublicLandInfoBO objLF = null;

            while (dr.Read())
            {
                objLF = new PublicLandInfoBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objLF.HID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                if (!dr.IsDBNull(dr.GetOrdinal("LND_TENUREID"))) objLF.LND_TENUREID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TENUREID")));
                if (!dr.IsDBNull(dr.GetOrdinal("HASTITLEDETAILS"))) objLF.HASTITLEDETAILS = dr.GetValue(dr.GetOrdinal("HASTITLEDETAILS")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("YEAROFACQUISITION"))) objLF.YEAROFACQUISITION = dr.GetValue(dr.GetOrdinal("YEAROFACQUISITION")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("FROMWHOM"))) objLF.FROMWHOM = dr.GetValue(dr.GetOrdinal("FROMWHOM")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) objLF.COMMENTS = dr.GetValue(dr.GetOrdinal("COMMENTS")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("WHOCLAIMSLAND"))) objLF.WHOCLAIMSLAND = dr.GetValue(dr.GetOrdinal("WHOCLAIMSLAND")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("LIVEDSINCEBIRTH"))) objLF.LIVEDSINCEBIRTH = dr.GetValue(dr.GetOrdinal("LIVEDSINCEBIRTH")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("MOVEDYEAR"))) objLF.MOVEDYEAR = dr.GetValue(dr.GetOrdinal("MOVEDYEAR")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("WHERELIVEDBEFORE"))) objLF.WHERELIVEDBEFORE = dr.GetValue(dr.GetOrdinal("WHERELIVEDBEFORE")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("ISMORTGAGED"))) objLF.ISMORTGAGED = dr.GetValue(dr.GetOrdinal("ISMORTGAGED")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("MORTGAGEDETAILS"))) objLF.MORTGAGEDETAILS = dr.GetValue(dr.GetOrdinal("MORTGAGEDETAILS")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("LANDRECDFROMID"))) objLF.LandRecivedfromid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LANDRECDFROMID")).ToString());
            }

            dr.Close();
            return objLF;
        }

        /// <summary>
        /// To Add Land Info
        /// </summary>
        /// <param name="objLF"></param>
        public void AddLandInfo(PublicLandInfoBO objLF)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_UPD_PAP_LND_PUBLIC", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("HOUSEHOLDID_", objLF.HID);
                myCommand.Parameters.AddWithValue("LND_TENUREID_", objLF.LND_TENUREID);
                myCommand.Parameters.AddWithValue("HASTITLEDETAILS_", objLF.HASTITLEDETAILS);
                myCommand.Parameters.AddWithValue("YEAROFACQUISITION_", objLF.YEAROFACQUISITION);
                myCommand.Parameters.AddWithValue("FROMWHOM_", objLF.FROMWHOM); 
                myCommand.Parameters.AddWithValue("COMMENTS_", objLF.COMMENTS);
                myCommand.Parameters.AddWithValue("WHOCLAIMSLAND_", objLF.WHOCLAIMSLAND);
                myCommand.Parameters.AddWithValue("LIVEDSINCEBIRTH_", objLF.LIVEDSINCEBIRTH);
                myCommand.Parameters.AddWithValue("MOVEDYEAR_", objLF.MOVEDYEAR);
                myCommand.Parameters.AddWithValue("WHERELIVEDBEFORE_", objLF.WHERELIVEDBEFORE);
                myCommand.Parameters.AddWithValue("ISMORTGAGED_", objLF.ISMORTGAGED);
                myCommand.Parameters.AddWithValue("MORTGAGEDETAILS_", objLF.MORTGAGEDETAILS);
                myCommand.Parameters.AddWithValue("ISDELETEDIN", objLF.IsDeleted);

                if (objLF.LandRecivedfromid > 0)
                    myCommand.Parameters.AddWithValue("LANDRECDFROMID_", objLF.LandRecivedfromid);
                else
                    myCommand.Parameters.AddWithValue("LANDRECDFROMID_", DBNull.Value);

                myCommand.Parameters.AddWithValue("CREATEDBY_", objLF.CreatedBy);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// To Update Land Info
        /// </summary>
        /// <param name="objLF"></param>
        /// <returns></returns>
        public int UpdateLandInfo(PublicLandInfoBO objLF)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_UPD_PAP_LND_PUBLIC", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("HOUSEHOLDID_", objLF.HID);
                myCommand.Parameters.AddWithValue("LND_TENUREID_", objLF.LND_TENUREID);
                myCommand.Parameters.AddWithValue("HASTITLEDETAILS_", objLF.HASTITLEDETAILS);
                myCommand.Parameters.AddWithValue("YEAROFACQUISITION_", objLF.YEAROFACQUISITION);                    
                myCommand.Parameters.AddWithValue("FROMWHOM_", objLF.FROMWHOM); 
                myCommand.Parameters.AddWithValue("COMMENTS_", objLF.COMMENTS);
                myCommand.Parameters.AddWithValue("WHOCLAIMSLAND_", objLF.WHOCLAIMSLAND);
                myCommand.Parameters.AddWithValue("LIVEDSINCEBIRTH_", objLF.LIVEDSINCEBIRTH);
                myCommand.Parameters.AddWithValue("MOVEDYEAR_", objLF.MOVEDYEAR);
                myCommand.Parameters.AddWithValue("WHERELIVEDBEFORE_", objLF.WHERELIVEDBEFORE);
                myCommand.Parameters.AddWithValue("ISMORTGAGED_", objLF.ISMORTGAGED);
                myCommand.Parameters.AddWithValue("MORTGAGEDETAILS_", objLF.MORTGAGEDETAILS);

                if (objLF.LandRecivedfromid > 0)
                    myCommand.Parameters.AddWithValue("LANDRECDFROMID_", objLF.LandRecivedfromid);
                else
                    myCommand.Parameters.AddWithValue("LANDRECDFROMID_", DBNull.Value);

                myCommand.Parameters.AddWithValue("CREATEDBY_", objLF.Userid);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    }
}