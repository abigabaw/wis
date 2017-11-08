using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LivelihoodRestLocationDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LivelihoodRestLocationBO GetNewLocation(int HHID)
        {
            proc = "USP_TRN_GET_PAP_LIV_REST_LOC";
            cnn = new SqlConnection(con);
            //LivelihoodRestLocationBO oLivelihoodRestLocationBO = null;

            NewLocationList lstNewLocation = new NewLocationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("hhid_", HHID);
           
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            LivelihoodRestLocationBO oLivelihoodRestLocationBO;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oLivelihoodRestLocationBO = new LivelihoodRestLocationBO();
                while (dr.Read())
                {
                    oLivelihoodRestLocationBO = MapData(dr);
                    //lstNewLocation.Add(oLivelihoodRestLocationBO);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oLivelihoodRestLocationBO;
        }
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="oLivelihoodRestLocationBO"></param>
        /// <returns></returns>
        public string AddNewLocation(LivelihoodRestLocationBO oLivelihoodRestLocationBO)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_TRN_INS_PAP_LIV_REST_LOC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("hhid_", oLivelihoodRestLocationBO.HHID);
            cmd.Parameters.AddWithValue("NewDistrict_", oLivelihoodRestLocationBO.NewDistrict);
            cmd.Parameters.AddWithValue("NewCounty_", oLivelihoodRestLocationBO.NewCounty);
            cmd.Parameters.AddWithValue("NewSubCounty_", oLivelihoodRestLocationBO.NewSubCounty);
            cmd.Parameters.AddWithValue("NewParish_", oLivelihoodRestLocationBO.NewParish);
            cmd.Parameters.AddWithValue("NewVillage_", oLivelihoodRestLocationBO.NewVillage);
            cmd.Parameters.AddWithValue("DistFrmOldLoc_", oLivelihoodRestLocationBO.DistFrmOldLoc);
            if(oLivelihoodRestLocationBO.DateOfSettlement!=DateTime.MinValue)
            cmd.Parameters.AddWithValue("DistFrmOldLoc_", oLivelihoodRestLocationBO.DateOfSettlement);
            else
                cmd.Parameters.AddWithValue("DistFrmOldLoc_", DBNull.Value);

            //cmd.Parameters.AddWithValue("distFromOldPlot_", oLivelihoodRestLocationBO.DistanceFromOldPlot);
            //cmd.Parameters.AddWithValue("plotparish_", oLivelihoodRestLocationBO.Parish);

            cmd.Parameters.AddWithValue("isdeleted_", oLivelihoodRestLocationBO.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oLivelihoodRestLocationBO.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// to map data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private LivelihoodRestLocationBO MapData(IDataReader reader)
        {
            LivelihoodRestLocationBO oLivelihoodRestLocationBO = new LivelihoodRestLocationBO();
            try
            {
                if (ColumnExists(reader, "Liv_Rest_LocationID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_LocationID")))
                    oLivelihoodRestLocationBO.Liv_Rest_LocationID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Rest_LocationID")));

                if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                    oLivelihoodRestLocationBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

                if (ColumnExists(reader, "NewDistrict") && !reader.IsDBNull(reader.GetOrdinal("NewDistrict")))
                    oLivelihoodRestLocationBO.NewDistrict = reader.GetString(reader.GetOrdinal("NewDistrict"));

                if (ColumnExists(reader, "NewCounty") && !reader.IsDBNull(reader.GetOrdinal("NewCounty")))
                    oLivelihoodRestLocationBO.NewCounty = reader.GetString(reader.GetOrdinal("NewCounty"));

                if (ColumnExists(reader, "NewSubCounty") && !reader.IsDBNull(reader.GetOrdinal("NewSubCounty")))
                    oLivelihoodRestLocationBO.NewSubCounty = reader.GetString(reader.GetOrdinal("NewSubCounty"));

                if (ColumnExists(reader, "NewParish") && !reader.IsDBNull(reader.GetOrdinal("NewParish")))
                    oLivelihoodRestLocationBO.NewParish = reader.GetString(reader.GetOrdinal("NewParish"));

                if (ColumnExists(reader, "NewVillage") && !reader.IsDBNull(reader.GetOrdinal("NewVillage")))
                    oLivelihoodRestLocationBO.NewVillage = reader.GetString(reader.GetOrdinal("NewVillage"));

                if (ColumnExists(reader, "DistFrmOldLoc") && !reader.IsDBNull(reader.GetOrdinal("DistFrmOldLoc")))
                    oLivelihoodRestLocationBO.DistFrmOldLoc = reader.GetString(reader.GetOrdinal("DistFrmOldLoc"));

                if (ColumnExists(reader, "DateOfSettlement") && !reader.IsDBNull(reader.GetOrdinal("DateOfSettlement")))
                    oLivelihoodRestLocationBO.DateOfSettlement = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DateOfSettlement")));
            }
            catch (Exception ex)
            { throw ex; }

            return oLivelihoodRestLocationBO;
        }
        /// <summary>
        /// to check whether column exists
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
