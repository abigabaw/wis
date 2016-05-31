using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CropRateDAL
    {
        /// <summary>
        /// to fetch details from database
        /// </summary>
        /// <param name="cropid"></param>
        /// <returns></returns>
        public CropRateList GetCropRate(int cropid)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_CROPRATE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cropid_", cropid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropRateBO objCRBO = null;
            CropRateList objCRList = new CropRateList();

            while (dr.Read())
            {
                objCRBO = new CropRateBO();
                objCRBO.CropRateID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPRATEID"))));
                objCRBO.CropID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID"))));
                objCRBO.DistrictName = (Convert.ToString(dr.GetValue(dr.GetOrdinal("DISTRICTNAME"))));
                objCRBO.CropDescription = (Convert.ToString(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTION"))));              
                objCRBO.CropRate = dr.GetValue(dr.GetOrdinal("CROPRATE")).ToString();
                objCRBO.IsDeleted = dr.GetValue(dr.GetOrdinal("ISDELETED")).ToString();
                objCRList.Add(objCRBO);
            }

            dr.Close();
            return objCRList;
        }
        /// <summary>
        /// To get details from database
        /// </summary>
        /// <param name="cropID"></param>
        /// <param name="CropDesID"></param>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CropRateBO GetCropRateByDistrict(int cropID,int CropDesID, int householdID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_CROPRATEBYDISTRICT";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CROPID", cropID);
            cmd.Parameters.Add("CROPDESCRIPTIONID_", CropDesID);
            cmd.Parameters.Add("HHID", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropRateBO objCRBO = null;

            while (dr.Read())
            {
                objCRBO = new CropRateBO();
                objCRBO.CropRateID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPRATEID"))));
                objCRBO.CropRate = dr.GetValue(dr.GetOrdinal("CROPRATE")).ToString();
            }

            dr.Close();
            return objCRBO;
        }
        /// <summary>
        /// To insert data to database
        /// </summary>
        /// <param name="objCRBO"></param>
        /// <returns></returns>
        public string AddCropRate(CropRateBO objCRBO)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string result = "";

            OracleCommand myCommand;
            myCommand = new OracleCommand("USP_MST_INS_CROPRATE", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("CROPID_", objCRBO.CropID);
            myCommand.Parameters.Add("DISTRICTID_", objCRBO.DistrictID);
            myCommand.Parameters.Add("CROPDESCRIPTIONID_", objCRBO.CropDescriptionID);
            myCommand.Parameters.Add("CROPRATE_", objCRBO.CropRate);
            myCommand.Parameters.Add("CREATEDBY_", objCRBO.CreatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            con.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            con.Close();

            return result;
        }
        /// <summary>
        /// to delete data from database
        /// </summary>
        /// <param name="cropRateID"></param>
        /// <returns></returns>
        public string DeleteCropRate(int cropRateID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string message = string.Empty;            
            OracleCommand myCommand=null;

            try
            {
                myCommand = new OracleCommand("USP_MST_DEL_CROPRATE", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CROPRATEID", cropRateID);                              
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                con.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    message = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    message = "Selected CROP RATE is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                con.Close();
                con.Dispose();
            }               

                return message;
        }
        /// <summary>
        /// to ake data obsolete
        /// </summary>
        /// <param name="cropRateID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropRate(int cropRateID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETECROPRATE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CROPRATEID_", cropRateID);
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
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="objCRBO"></param>
        /// <returns></returns>
        public string UpdateCropRate(CropRateBO objCRBO)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string result = "";
            OracleCommand myCommand;
            myCommand = new OracleCommand("USP_MST_UPD_CROPRATE", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("CROPRATEID", objCRBO.CropRateID);
            myCommand.Parameters.Add("CROPID", objCRBO.CropID);
            myCommand.Parameters.Add("DISTRICTID", objCRBO.DistrictID);
            myCommand.Parameters.Add("CROPDESCRIPTIONID_", objCRBO.CropDescriptionID);
            myCommand.Parameters.Add("CROPRATE", objCRBO.CropRate);
            myCommand.Parameters.Add("UPDATEDBY", objCRBO.UpdatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            con.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            con.Close();

            return result;
        }
        /// <summary>
        /// To get data by ID
        /// </summary>
        /// <param name="cropID"></param>
        /// <returns></returns>
        public CropRateBO GetCropRateByID(int cropID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_CROPRATEBYID";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CROPRATEID", cropID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropRateBO objCRBO = null;
            CropRateList objCRList = new CropRateList();

            while (dr.Read())
            {
                objCRBO = new CropRateBO();
                objCRBO.CropRateID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPRATEID"))));
                objCRBO.CropID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID"))));
                objCRBO.DistrictID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DISTRICTID"))));
                objCRBO.CropDescriptionID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID"))));
                objCRBO.CropRate = dr.GetValue(dr.GetOrdinal("CROPRATE")).ToString();
                objCRBO.IsDeleted = dr.GetValue(dr.GetOrdinal("ISDELETED")).ToString();
                objCRList.Add(objCRBO);
            }

            dr.Close();
            return objCRBO;
        }

    }
}


