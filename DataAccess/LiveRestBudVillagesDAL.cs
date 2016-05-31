using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class LiveRestBudVillagesDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        #region GetData
        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        /// <summary>
        /// To Get Live Rest Bud Villages
        /// </summary>
        /// <returns></returns>
        public LiveRestBudVillagesList GetLiveRestBudVillages()
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_LIVRESBUD_VILLAGE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_LIV_BUD_VILLG_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_res_budgid_", LivRestBudgID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_INS_LIVRESBUD_VILLAGE", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("Liv_Res_BudgId_", oLiveRestBudVillagesBO.Liv_Res_BudgId);
                oCmd.Parameters.Add("Village_", oLiveRestBudVillagesBO.Village);
                oCmd.Parameters.Add("CreatedBy_", oLiveRestBudVillagesBO.CreatedBy);
                oCmd.Parameters.Add("IsDeleted_", oLiveRestBudVillagesBO.IsDeleted);

                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            cnn = new OracleConnection(con);

            proc = "USP_TRN_DEL_LIVRESBUD_VILLAGE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_res_budgid_", LivRestBudgID);
    
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
            cnn = new OracleConnection(con);

            proc = "USP_TRN_UPD_LIVRESBUD_VILLAGE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("Liv_Res_BudgId_", oLiveRestBudVillagesBO.Liv_Res_BudgId);
            cmd.Parameters.Add("Village_", oLiveRestBudVillagesBO.Village);
            cmd.Parameters.Add("updatedby_", oLiveRestBudVillagesBO.CreatedBy);
            cmd.Parameters.Add("IsDeleted_", oLiveRestBudVillagesBO.IsDeleted);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
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