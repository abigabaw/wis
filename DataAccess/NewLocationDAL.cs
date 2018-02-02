using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class NewLocationDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get New Location
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public NewLocationBO GetNewLocation(int HHID)
        {
            proc = "USP_TRN_GET_CMP_NEW_PLOT";
            cnn = new SqlConnection(con);
            //NewLocationBO oNewLocationBO = null;

            NewLocationList lstNewLocation = new NewLocationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("hhid_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            NewLocationBO oNewLocationBO = null;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oNewLocationBO = new NewLocationBO();

                    //if (!dr.IsDBNull(dr.GetOrdinal("BankID"))) oNewLocationBO.BankID = dr.GetInt32(dr.GetOrdinal("BankID"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("BankName"))) oNewLocationBO.BankName = dr.GetString(dr.GetOrdinal("BankName"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("City"))) oNewLocationBO.City = dr.GetString(dr.GetOrdinal("City"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("BranchName"))) oNewLocationBO.BranchName = dr.GetString(dr.GetOrdinal("BranchName"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("SwiftCode"))) oNewLocationBO.SwiftCode = dr.GetString(dr.GetOrdinal("SwiftCode"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) oNewLocationBO.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));


                    oNewLocationBO = MapData(dr);

                    //lstNewLocation.Add(oNewLocationBO);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oNewLocationBO;
        }

        /// <summary>
        /// To Add New Location
        /// </summary>
        /// <param name="oNewLocationBO"></param>
        /// <returns></returns>
        public string AddNewLocation(NewLocationBO oNewLocationBO)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_TRN_INS_CMP_NEW_PLOT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("hhid_", oNewLocationBO.HHID);
            cmd.Parameters.AddWithValue("newplotno_", oNewLocationBO.NewPlotNo);
            cmd.Parameters.AddWithValue("newplotstatusid_", oNewLocationBO.NewPlotStatusId);
            cmd.Parameters.AddWithValue("plotdistrict_", oNewLocationBO.District);
            cmd.Parameters.AddWithValue("plotcounty_", oNewLocationBO.County);
            cmd.Parameters.AddWithValue("plotsubcounty_", oNewLocationBO.SubCounty);
            cmd.Parameters.AddWithValue("plotvillage_", oNewLocationBO.Village);
            cmd.Parameters.AddWithValue("distFromOldPlot_", oNewLocationBO.DistanceFromOldPlot);

            if (oNewLocationBO.DateOfSettlement != DateTime.MinValue)
                cmd.Parameters.AddWithValue("dateofsettlement_", oNewLocationBO.DateOfSettlement);
            else
                cmd.Parameters.AddWithValue("dateofsettlement_", DBNull.Value);

            cmd.Parameters.AddWithValue("plotparish_", oNewLocationBO.Parish);

            cmd.Parameters.AddWithValue("isdeleted_", oNewLocationBO.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oNewLocationBO.CreatedBy);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private NewLocationBO MapData(IDataReader reader)
        {
            NewLocationBO oNewLocationBO = new NewLocationBO();
            try
            {
                if (ColumnExists(reader, "cmp_newplotid") && !reader.IsDBNull(reader.GetOrdinal("cmp_newplotid")))
                    oNewLocationBO.NewPlotID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("cmp_newplotid")));

                if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
                    oNewLocationBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

                if (ColumnExists(reader, "newplotstatusid") && !reader.IsDBNull(reader.GetOrdinal("newplotstatusid")))
                    oNewLocationBO.NewPlotStatusId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("newplotstatusid")));

                if (ColumnExists(reader, "newplotno") && !reader.IsDBNull(reader.GetOrdinal("newplotno")))
                    oNewLocationBO.NewPlotNo = reader.GetString(reader.GetOrdinal("newplotno"));

                if (ColumnExists(reader, "plotdistrict") && !reader.IsDBNull(reader.GetOrdinal("plotdistrict")))
                    oNewLocationBO.District = reader.GetString(reader.GetOrdinal("plotdistrict"));

                if (ColumnExists(reader, "plotcounty") && !reader.IsDBNull(reader.GetOrdinal("plotcounty")))
                    oNewLocationBO.County = reader.GetString(reader.GetOrdinal("plotcounty"));

                if (ColumnExists(reader, "plotsubcounty") && !reader.IsDBNull(reader.GetOrdinal("plotsubcounty")))
                    oNewLocationBO.SubCounty = reader.GetString(reader.GetOrdinal("plotsubcounty"));

                if (ColumnExists(reader, "plotvillage") && !reader.IsDBNull(reader.GetOrdinal("plotvillage")))
                    oNewLocationBO.Village = reader.GetString(reader.GetOrdinal("plotvillage"));

                if (ColumnExists(reader, "plotparish") && !reader.IsDBNull(reader.GetOrdinal("plotparish")))
                    oNewLocationBO.Parish = reader.GetString(reader.GetOrdinal("plotparish"));

                if (ColumnExists(reader, "distancefromoldplot"))
                    if (!reader.IsDBNull(reader.GetOrdinal("distancefromoldplot")))
                    {
                        oNewLocationBO.DistanceFromOldPlot = Convert.ToUInt32(reader.GetValue(reader.GetOrdinal("distancefromoldplot"))).ToString();

                    }
                if (ColumnExists(reader, "DateOfSettlement") && !reader.IsDBNull(reader.GetOrdinal("DateOfSettlement")))
                    oNewLocationBO.DateOfSettlement = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DateOfSettlement")));
            }
            catch (Exception ex)
            { throw ex; }

            return oNewLocationBO;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
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
