using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class WIS_ConfigDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get Configuration
        /// </summary>
        /// <param name="SearchValue"></param>
        /// <returns></returns>
        public WIS_ConfigBO getConfiguration(string SearchValue)
        {
            proc = "USP_GET_WIS_CONFIGURATION";
            cnn = new OracleConnection(con);
            WIS_ConfigBO oWIS_ConfigBO = null;

            WIS_ConfigList lstWIS_Config = new WIS_ConfigList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ConfigItem_", SearchValue);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oWIS_ConfigBO = new WIS_ConfigBO();

                    oWIS_ConfigBO = MapData(dr);

                    // lstWIS_Config.Add(oWIS_ConfigBO);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oWIS_ConfigBO;
        }

        /// <summary>
        /// To Get Serial Number
        /// </summary>
        /// <param name="CONFIGITEM"></param>
        /// <returns></returns>
        public WIS_ConfigBO GetSerialNumber(string CONFIGITEM)
        {
            proc = "USP_RPT_UPD_SERIALNO";
            cnn = new OracleConnection(con);
            WIS_ConfigBO oWIS_ConfigBO = null;

            WIS_ConfigList lstWIS_Config = new WIS_ConfigList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CONFIGITEM_", CONFIGITEM);
           // cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oWIS_ConfigBO = new WIS_ConfigBO();

                    oWIS_ConfigBO = MapData(dr);

                    // lstWIS_Config.Add(oWIS_ConfigBO);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oWIS_ConfigBO;
        }

        /// <summary>
        /// Get Round Off Limit
        /// </summary>
        /// <param name="SearchValue"></param>
        /// <returns></returns>
        public string getRoundOffLimit(string SearchValue)
        {
            WIS_ConfigBO oWIS_ConfigBO;
           
            try
            {
                oWIS_ConfigBO = new WIS_ConfigBO();
                oWIS_ConfigBO = getConfiguration(SearchValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oWIS_ConfigBO.ConfigData; 
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private WIS_ConfigBO MapData(IDataReader reader)
        {
            WIS_ConfigBO oWIS_ConfigBO = new WIS_ConfigBO();

            if (ColumnExists(reader, "ConfigItem") && !reader.IsDBNull(reader.GetOrdinal("ConfigItem")))
                oWIS_ConfigBO.ConfigItem = reader.GetString(reader.GetOrdinal("ConfigItem"));

            if (ColumnExists(reader, "ConfigData") && !reader.IsDBNull(reader.GetOrdinal("ConfigData")))
                oWIS_ConfigBO.ConfigData = reader.GetString(reader.GetOrdinal("ConfigData"));

            return oWIS_ConfigBO;
        }

        /// <summary>
        /// To check that Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Get Config SMS sending status
        /// </summary>
        /// <returns></returns>
        public WIS_ConfigBO GetConfigSMSsending()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_SEL_SMS_CONFIG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WIS_ConfigBO WIS_ConfigSMSBO = null;
           // EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

            while (dr.Read())
            {
                WIS_ConfigSMSBO = new WIS_ConfigBO();

                if (!dr.IsDBNull(dr.GetOrdinal("RegMobileNumber")))
                    WIS_ConfigSMSBO.MobileNumber = (dr.GetString(dr.GetOrdinal("RegMobileNumber")));

                if (!dr.IsDBNull(dr.GetOrdinal("RegMobilePassword")))
                    WIS_ConfigSMSBO.MobilePassword = (dr.GetString(dr.GetOrdinal("RegMobilePassword")));

                if (!dr.IsDBNull(dr.GetOrdinal("RegSiteUrl")))
                    WIS_ConfigSMSBO.SiteUrl = (dr.GetString(dr.GetOrdinal("RegSiteUrl")));

                if (!dr.IsDBNull(dr.GetOrdinal("RegMobileStatus")))
                    WIS_ConfigSMSBO.MobileStatus = (dr.GetString(dr.GetOrdinal("RegMobileStatus")));

                //EmailTemplateListobj.Add(WIS_ConfigSMSBO);
            }
            dr.Close();
            return WIS_ConfigSMSBO;
        }

        /// <summary>
        /// To Get Build Version
        /// </summary>
        /// <returns></returns>
        public WIS_ConfigBO getBuildVersion()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_GET_BUILD_CONFIG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WIS_ConfigBO WIS_ConfigBuildBO = null;
            // EmailTemplateList EmailTemplateListobj = new EmailTemplateList();

            while (dr.Read())
            {
                WIS_ConfigBuildBO = new WIS_ConfigBO();

                if (!dr.IsDBNull(dr.GetOrdinal("BUILDVERSION")))
                    WIS_ConfigBuildBO.BUILDVERSION = (dr.GetString(dr.GetOrdinal("BUILDVERSION")));

                if (!dr.IsDBNull(dr.GetOrdinal("BUILDDATE")))
                    WIS_ConfigBuildBO.BUILDDATE = (dr.GetString(dr.GetOrdinal("BUILDDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("BUILDCOPY")))
                    WIS_ConfigBuildBO.BUILDCOPY = (dr.GetString(dr.GetOrdinal("BUILDCOPY")));

            }
            dr.Close();
            return WIS_ConfigBuildBO;
        }
        //getBuildVersion
    }
}
