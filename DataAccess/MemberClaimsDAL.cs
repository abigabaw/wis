using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class MemberClaimsDAL
    {
        #region GetData
        //USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To get Member Claim
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public MemberClaimsBO getMemberClaim(int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PAP_LND_RESPONDENT";
            MemberClaimsBO objMemberClaimsBO = null;
            try
            {
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("HHID_", HHID);

                //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              
                BatchList oBatchList = new BatchList();

                while (dr.Read())
                {
                    objMemberClaimsBO = new MemberClaimsBO();
                    objMemberClaimsBO = MapData(dr);

                    //oBatchList.Add(objMemberClaimsBO);
                }
                dr.Close();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
               
            }
            return objMemberClaimsBO;
        }

        // To check the Column are Exists or not
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private MemberClaimsBO MapData(IDataReader reader)
        {
            MemberClaimsBO oMemberClaimsBO = new MemberClaimsBO();

            if (ColumnExists(reader, "LND_RESPONDENTID") && !reader.IsDBNull(reader.GetOrdinal("LND_RESPONDENTID")))
                oMemberClaimsBO.LND_RESPONDENTID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LND_RESPONDENTID")));

            if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                oMemberClaimsBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            if (ColumnExists(reader, "CLAIMDETAILS") && !reader.IsDBNull(reader.GetOrdinal("CLAIMDETAILS")))
                oMemberClaimsBO.CLAIMDETAILS = reader.GetString(reader.GetOrdinal("CLAIMDETAILS"));

            if (ColumnExists(reader, "HASCLAIM") && !reader.IsDBNull(reader.GetOrdinal("HASCLAIM")))
                oMemberClaimsBO.HASCLAIM = reader.GetString(reader.GetOrdinal("HASCLAIM"));

            if (ColumnExists(reader, "OTHEREASEMENT") && !reader.IsDBNull(reader.GetOrdinal("OTHEREASEMENT")))
                oMemberClaimsBO.OTHEREASEMENT = reader.GetString(reader.GetOrdinal("OTHEREASEMENT"));

            if (ColumnExists(reader, "OTHEREASEMENTDETAILS") && !reader.IsDBNull(reader.GetOrdinal("OTHEREASEMENTDETAILS")))
                oMemberClaimsBO.OTHEREASEMENTDETAILS = reader.GetString(reader.GetOrdinal("OTHEREASEMENTDETAILS"));

            if (ColumnExists(reader, "OTHPEOPLEACCESSWATER") && !reader.IsDBNull(reader.GetOrdinal("OTHPEOPLEACCESSWATER")))
                oMemberClaimsBO.OTHPEOPLEACCESSWATER = reader.GetString(reader.GetOrdinal("OTHPEOPLEACCESSWATER"));

            if (ColumnExists(reader, "OTHPEOPLEPICK") && !reader.IsDBNull(reader.GetOrdinal("OTHPEOPLEPICK")))
                oMemberClaimsBO.OTHPEOPLEPICK = reader.GetString(reader.GetOrdinal("OTHPEOPLEPICK"));

            if (ColumnExists(reader, "PICKFROMOTHPEOPLELAND") && !reader.IsDBNull(reader.GetOrdinal("PICKFROMOTHPEOPLELAND")))
                oMemberClaimsBO.PICKFROMOTHPEOPLELAND = reader.GetString(reader.GetOrdinal("PICKFROMOTHPEOPLELAND"));

            if (ColumnExists(reader, "ACCESSWATERFRMOTHPEOPLE") && !reader.IsDBNull(reader.GetOrdinal("ACCESSWATERFRMOTHPEOPLE")))
                oMemberClaimsBO.ACCESSWATERFRMOTHPEOPLE = reader.GetString(reader.GetOrdinal("ACCESSWATERFRMOTHPEOPLE"));



            return oMemberClaimsBO;
        }
        #endregion

        /// <summary>
        /// To Add Member
        /// </summary>
        /// <param name="MCBO"></param>
        /// <returns></returns>
        public string AddMember(MemberClaimsBO MCBO)
        {
            string returnResult = string.Empty;
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                try
                {
                    SqlCommand myCommand;
                    myCommand = new SqlCommand("USP_TRN_INS_PAP_LND_RESPONDENT", con);
                    myCommand.Connection = con;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("LND_RESPONDENTID_", MCBO.LND_RESPONDENTID);
                    myCommand.Parameters.AddWithValue("HHID_", MCBO.HHID);
                    myCommand.Parameters.AddWithValue("HASCLAIM_", MCBO.HASCLAIM);
                    myCommand.Parameters.AddWithValue("CLAIMDETAILS_", MCBO.CLAIMDETAILS);
                    myCommand.Parameters.AddWithValue("OTHPEOPLEPICK_", MCBO.OTHPEOPLEPICK);
                    myCommand.Parameters.AddWithValue("PICKFROMOTHPEOPLELAND_", MCBO.PICKFROMOTHPEOPLELAND);
                    myCommand.Parameters.AddWithValue("OTHPEOPLEACCESSWATER_", MCBO.OTHPEOPLEACCESSWATER);
                    myCommand.Parameters.AddWithValue("ACCESSWATERFRMOTHPEOPLE_", MCBO.ACCESSWATERFRMOTHPEOPLE);
                    myCommand.Parameters.AddWithValue("OTHEREASEMENT_", MCBO.OTHEREASEMENT);
                    myCommand.Parameters.AddWithValue("OTHEREASEMENTDETAILS_", MCBO.OTHEREASEMENTDETAILS);
                    myCommand.Parameters.AddWithValue("CREATEDBY_", MCBO.CreatedBy);
                    myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                    con.Open();
                    result = myCommand.ExecuteNonQuery();
                    if (myCommand.Parameters["errorMessage_"].Value != null)
                        returnResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                    else
                        returnResult = string.Empty;
                }
                catch (Exception ex)
                { throw ex; }
                finally
                {
                    con.Close();
                }
                return returnResult;
            }

        }
    }
           
}