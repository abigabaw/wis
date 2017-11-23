using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class LiveRestBudVillagesDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        #region GetData
        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To Get Live Rest Bud Villages
        /// </summary>
        /// <returns></returns>
        public LiveRestBudVillagesList GetLiveRestBudVillages()
        {
            SqlConnection cnn = new SqlConnection(con);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_LIVRESBUD_VILLAGE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LiveRestBudVillagesBO objLiveRestBudVillagesBO = null;
            LiveRestBudVillagesList oLiveRestBudVillagesList = new LiveRestBudVillagesList();

            while (dr.Read())
            {
                objLiveRestBudVillagesBO = new LiveRestBudVillagesBO();
               
                objLiveRestBudVillagesBO = MapData(dr);

                oLiveRestBudVillagesList.Add(objLiveRestBudVillagesBO);
            }

            dr.Close();

            return oLiveRestBudVillagesList;
        }

        /// <summary>
        /// To Get Live Rest Bud Villages By Id
        /// </summary>
        /// <param name="LivRestBudgID"></param>
        /// <returns></returns>
        public LiveRestBudVillagesList GetLiveRestBudVillagesById(int LivRestBudgID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_LIV_BUD_VILLG_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("liv_res_budgid_", LivRestBudgID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            LiveRestBudVillagesBO oLiveRestBudVillagesBO = null;
            LiveRestBudVillagesList oLiveRestBudVillagesList = new LiveRestBudVillagesList();

            while (dr.Read())
            {
                oLiveRestBudVillagesBO = new LiveRestBudVillagesBO();
                oLiveRestBudVillagesBO = MapData(dr);
                oLiveRestBudVillagesList.Add(oLiveRestBudVillagesBO);
            }
            dr.Close();

            return oLiveRestBudVillagesList;
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
        private LiveRestBudVillagesBO MapData(IDataReader reader)
        {
            LiveRestBudVillagesBO oLiveRestBudVillagesBO = new LiveRestBudVillagesBO();

            if (ColumnExists(reader, "Liv_Res_BudgId") && !reader.IsDBNull(reader.GetOrdinal("Liv_Res_BudgId")))
                oLiveRestBudVillagesBO.Liv_Res_BudgId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Res_BudgId")));

            if (ColumnExists(reader, "Village") && !reader.IsDBNull(reader.GetOrdinal("Village")))
                oLiveRestBudVillagesBO.Village = reader.GetString(reader.GetOrdinal("Village"));

            return oLiveRestBudVillagesBO;
        }
        #endregion

        //save the data to mst_Concern table using USP_MST_INSERTCONCERN-SP
        public string AddLiveRestBudVillages(LiveRestBudVillagesBO oLiveRestBudVillagesBO)
        {
            string returnResult = string.Empty;
            LiveRestBudVillagesBO ooLiveRestBudVillagesBO = new LiveRestBudVillagesBO();//For Storing & Returning Result as Object

            SqlConnection OCon = new SqlConnection(con);
            OCon.Open();
            SqlCommand oCmd = new SqlCommand("USP_TRN_INS_LIVRESBUD_VILLAGE", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.AddWithValue("Liv_Res_BudgId_", oLiveRestBudVillagesBO.Liv_Res_BudgId);
                oCmd.Parameters.AddWithValue("Village_", oLiveRestBudVillagesBO.Village);
                oCmd.Parameters.AddWithValue("CreatedBy_", oLiveRestBudVillagesBO.CreatedBy);
                oCmd.Parameters.AddWithValue("IsDeleted_", oLiveRestBudVillagesBO.IsDeleted);

                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = oCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// To Delete Live Rest Bud Villages
        /// </summary>
        /// <param name="LivRestBudgID"></param>
        /// <returns></returns>
        public int DeleteLiveRestBudVillages(int LivRestBudgID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_LIVRESBUD_VILLAGE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("liv_res_budgid_", LivRestBudgID);
    
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// To Update Live Rest Bud Villages
        /// </summary>
        /// <param name="oLiveRestBudVillagesBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestBudVillages(LiveRestBudVillagesBO oLiveRestBudVillagesBO)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_LIVRESBUD_VILLAGE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("Liv_Res_BudgId_", oLiveRestBudVillagesBO.Liv_Res_BudgId);
            cmd.Parameters.AddWithValue("Village_", oLiveRestBudVillagesBO.Village);
            cmd.Parameters.AddWithValue("updatedby_", oLiveRestBudVillagesBO.CreatedBy);
            cmd.Parameters.AddWithValue("IsDeleted_", oLiveRestBudVillagesBO.IsDeleted);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                returnResult = string.Empty;
                throw ex;
            }

            return returnResult;
        }
    }
}