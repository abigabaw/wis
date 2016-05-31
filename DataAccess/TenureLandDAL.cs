using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TenureLandDAL
    {
        /// <summary>
        /// To Get Tenure Land
        /// </summary>
        /// <param name="TenureLandName"></param>
        /// <returns></returns>
        public TenureLandList GetTenureLand(string TenureLandName)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_LANDTENURE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureLandName.ToString() == "")
            {
                cmd.Parameters.Add("@LandTenureNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@LandTenureNameIN", TenureLandName.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureLandBO objTenureLand = null;
            TenureLandList TenureLand = new TenureLandList();
            while (dr.Read())
            {
                objTenureLand = new TenureLandBO();
                objTenureLand.Lnd_TenureId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TENUREID")));
                objTenureLand.Lnd_Tenure = dr.GetString(dr.GetOrdinal("LND_TENURE"));
                objTenureLand.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                TenureLand.Add(objTenureLand);
            }
            dr.Close();
            return TenureLand;
        }

        /// <summary>
        /// To Add Tenure Land
        /// </summary>
        /// <param name="objTenureLand"></param>
        /// <returns></returns>
        public string AddTenureLand(TenureLandBO objTenureLand)
        {
            string result = "";

            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_INS_LANDTENURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@LandTenureNameIN", objTenureLand.Lnd_Tenure);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objTenureLand.CreatedBy);
            myCommand.Parameters.Add("@errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["@errorMessage_"].Value != null)
                result = myCommand.Parameters["@errorMessage_"].Value.ToString();
            myCommand.Connection.Close();

            return result;
        }


        //public int DeleteTenureLand(int TenureLandId)
        //{
        //    int result = 0;
        //    {
        //        OracleConnection myConnection;
        //        OracleCommand myCommand;
        //        myConnection = new OracleConnection(AppConfiguration.ConnectionString);
        //        myCommand = new OracleCommand("USP_MST_DEL_LANDTENURE", myConnection);
        //        myCommand.Connection = myConnection;
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        myCommand.Parameters.Add("@LandTenureIDIN", TenureLandId);
        //        myConnection.Open();
        //        result = myCommand.ExecuteNonQuery();
        //        myConnection.Close();
        //    }
        //    return result;
        //}

        /// <summary>
        /// To Delete Tenure Land
        /// </summary>
        /// <param name="TenureLandId"></param>
        /// <returns></returns>
        public string DeleteTenureLand(int TenureLandId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_LANDTENURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LandTenureIDIN", TenureLandId);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Role is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Update Tenure Land
        /// </summary>
        /// <param name="objTenureLand"></param>
        /// <returns></returns>
        public string UpdateTenureLand(TenureLandBO objTenureLand)
        {
            string result = "";

            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_UPD_LANDTENURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@LandTenureIDIN", objTenureLand.Lnd_TenureId);
            myCommand.Parameters.Add("@LandTenureNameIN", objTenureLand.Lnd_Tenure);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objTenureLand.UpdatedBy);
            myCommand.Parameters.Add("@errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["@errorMessage_"].Value != null)
                result = myCommand.Parameters["@errorMessage_"].Value.ToString();
            myCommand.Connection.Close();

            return result;
        }

        /// <summary>
        /// To Get Tenure Land By Tenure Land
        /// </summary>
        /// <param name="TenureLandID"></param>
        /// <returns></returns>
        public TenureLandBO GetTenureLandByTenureLand(int TenureLandID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_LANDTNRBYLANDTNRID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LandTenureIDIN", TenureLandID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureLandBO obTenureLand = null;
            while (dr.Read())
            {
                obTenureLand = new TenureLandBO();
                obTenureLand.Lnd_TenureId = dr.GetInt32(dr.GetOrdinal("LND_TENUREID"));
                obTenureLand.Lnd_Tenure = dr.GetString(dr.GetOrdinal("LND_TENURE"));
            }
            dr.Close();
            return obTenureLand;
        }

        //To Obsolete Tenure Land
        public string ObsoleteTenureLand(int TenureLandId, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_LANDTENURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LandTenureIDIN", TenureLandId);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }
    }
}

