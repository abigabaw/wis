using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_LivelihoodDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get Livelihood Items By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_LivelihoodList GetLivelihoodItemsByID(int householdID)
        {
            proc = "USP_TRN_GET_LIVELIHOODITEMS";
            cnn = new SqlConnection(con);
            PAP_LivelihoodList LivelihoodItems = new PAP_LivelihoodList();
            PAP_LivelihoodBO objLivelihood = null;

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
                    objLivelihood = new PAP_LivelihoodBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIVELIHOOD_ITEMID"))) objLivelihood.LivelihoodItemID = dr.GetInt32(dr.GetOrdinal("LIVELIHOOD_ITEMID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objLivelihood.HouseHoldID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CASH"))) objLivelihood.Cash = dr.GetDecimal(dr.GetOrdinal("CASH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INKIND"))) objLivelihood.InKind = dr.GetString(dr.GetOrdinal("INKIND"));

                    LivelihoodItems.Add(objLivelihood);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return LivelihoodItems;
        }

        /// <summary>
        /// To Update Livelihood
        /// </summary>
        /// <param name="LivelihoodItems"></param>
        public void UpdateLivelihood(PAP_LivelihoodList LivelihoodItems)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_LIVELIHOOD";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("LIVELIHOOD_ITEMID_", "");
            cmd.Parameters.AddWithValue("HOUSEHOLDID_","");
            cmd.Parameters.AddWithValue("CASH_", "");
            cmd.Parameters.AddWithValue("INKIND_", "");
            cmd.Parameters.AddWithValue("CREATEDBY_", "");
            cmd.Parameters.AddWithValue("UPDATEDBY_", "");

            foreach (PAP_LivelihoodBO objLivelihood in LivelihoodItems)
            {
                cmd.Parameters["LIVELIHOOD_ITEMID_"].Value = objLivelihood.LivelihoodItemID;
                cmd.Parameters["HOUSEHOLDID_"].Value = objLivelihood.HouseHoldID;
                cmd.Parameters["CASH_"].Value = objLivelihood.Cash;
                cmd.Parameters["INKIND_"].Value = objLivelihood.InKind;
                cmd.Parameters["CREATEDBY_"].Value = objLivelihood.CreatedBy;
                cmd.Parameters["UPDATEDBY_"].Value = objLivelihood.UpdatedBy;
                cmd.ExecuteNonQuery();
            }
            
            cmd.Connection.Close();
        }
    }
}
