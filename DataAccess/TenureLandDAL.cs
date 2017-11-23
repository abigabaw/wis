using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_LANDTENURE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureLandName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@LandTenureNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LandTenureNameIN", TenureLandName.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_INS_LANDTENURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@LandTenureNameIN", objTenureLand.Lnd_Tenure);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objTenureLand.CreatedBy);
            myCommand.Parameters.AddWithValue("@errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        //        SqlConnection myConnection;
        //        SqlCommand myCommand;
        //        myConnection = new SqlConnection(AppConfiguration.ConnectionString);
        //        myCommand = new SqlCommand("USP_MST_DEL_LANDTENURE", myConnection);
        //        myCommand.Connection = myConnection;
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        myCommand.Parameters.AddWithValue("@LandTenureIDIN", TenureLandId);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_LANDTENURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("LandTenureIDIN", TenureLandId);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_UPD_LANDTENURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@LandTenureIDIN", objTenureLand.Lnd_TenureId);
            myCommand.Parameters.AddWithValue("@LandTenureNameIN", objTenureLand.Lnd_Tenure);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objTenureLand.UpdatedBy);
            myCommand.Parameters.AddWithValue("@errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_LANDTNRBYLANDTNRID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LandTenureIDIN", TenureLandID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_LANDTENURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("LandTenureIDIN", TenureLandId);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

