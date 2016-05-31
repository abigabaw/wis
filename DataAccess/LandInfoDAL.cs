using System;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_PUBLIC";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_PAP_LND_PUBLIC", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("HOUSEHOLDID_", objLF.HID);
                myCommand.Parameters.Add("LND_TENUREID_", objLF.LND_TENUREID);
                myCommand.Parameters.Add("HASTITLEDETAILS_", objLF.HASTITLEDETAILS);
                myCommand.Parameters.Add("YEAROFACQUISITION_", objLF.YEAROFACQUISITION);
                myCommand.Parameters.Add("FROMWHOM_", objLF.FROMWHOM); 
                myCommand.Parameters.Add("COMMENTS_", objLF.COMMENTS);
                myCommand.Parameters.Add("WHOCLAIMSLAND_", objLF.WHOCLAIMSLAND);
                myCommand.Parameters.Add("LIVEDSINCEBIRTH_", objLF.LIVEDSINCEBIRTH);
                myCommand.Parameters.Add("MOVEDYEAR_", objLF.MOVEDYEAR);
                myCommand.Parameters.Add("WHERELIVEDBEFORE_", objLF.WHERELIVEDBEFORE);
                myCommand.Parameters.Add("ISMORTGAGED_", objLF.ISMORTGAGED);
                myCommand.Parameters.Add("MORTGAGEDETAILS_", objLF.MORTGAGEDETAILS);
                myCommand.Parameters.Add("ISDELETEDIN", objLF.IsDeleted);

                if (objLF.LandRecivedfromid > 0)
                    myCommand.Parameters.Add("LANDRECDFROMID_", objLF.LandRecivedfromid);
                else
                    myCommand.Parameters.Add("LANDRECDFROMID_", DBNull.Value);

                myCommand.Parameters.Add("CREATEDBY_", objLF.CreatedBy);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_PAP_LND_PUBLIC", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("HOUSEHOLDID_", objLF.HID);
                myCommand.Parameters.Add("LND_TENUREID_", objLF.LND_TENUREID);
                myCommand.Parameters.Add("HASTITLEDETAILS_", objLF.HASTITLEDETAILS);
                myCommand.Parameters.Add("YEAROFACQUISITION_", objLF.YEAROFACQUISITION);                    
                myCommand.Parameters.Add("FROMWHOM_", objLF.FROMWHOM); 
                myCommand.Parameters.Add("COMMENTS_", objLF.COMMENTS);
                myCommand.Parameters.Add("WHOCLAIMSLAND_", objLF.WHOCLAIMSLAND);
                myCommand.Parameters.Add("LIVEDSINCEBIRTH_", objLF.LIVEDSINCEBIRTH);
                myCommand.Parameters.Add("MOVEDYEAR_", objLF.MOVEDYEAR);
                myCommand.Parameters.Add("WHERELIVEDBEFORE_", objLF.WHERELIVEDBEFORE);
                myCommand.Parameters.Add("ISMORTGAGED_", objLF.ISMORTGAGED);
                myCommand.Parameters.Add("MORTGAGEDETAILS_", objLF.MORTGAGEDETAILS);

                if (objLF.LandRecivedfromid > 0)
                    myCommand.Parameters.Add("LANDRECDFROMID_", objLF.LandRecivedfromid);
                else
                    myCommand.Parameters.Add("LANDRECDFROMID_", DBNull.Value);

                myCommand.Parameters.Add("CREATEDBY_", objLF.Userid);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    }
}