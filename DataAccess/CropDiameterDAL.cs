using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CropDiameterDAL
    {
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="objCropDiameter"></param>
        /// <returns></returns>
        public string InsertCropDiameter(CropDiameterBO objCropDiameter)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTCROPDIAMETER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CROPDIAMETER_", objCropDiameter.CROPDIAMETER);
                dcmd.Parameters.Add("CreatedBY", objCropDiameter.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
        }
        /// <summary>
        /// get details by ID
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <returns></returns>
        public CropDiameterBO GetCropDiameterById(int CROPDIAMETERID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSECTCROPDIAMETRID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CROPDIAMETERID_", CROPDIAMETERID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropDiameterBO CropDiameterObj = null;
            CropDiameterList CropDiameterList = new CropDiameterList();

            CropDiameterObj = new CropDiameterBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CROPDIAMETER") && !dr.IsDBNull(dr.GetOrdinal("CROPDIAMETER")))
                    CropDiameterObj.CROPDIAMETER = dr.GetString(dr.GetOrdinal("CROPDIAMETER"));
                if (ColumnExists(dr, "CROPDIAMETERID") && !dr.IsDBNull(dr.GetOrdinal("CROPDIAMETERID")))
                    CropDiameterObj.CROPDIAMETERID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDIAMETERID")));
                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CropDiameterObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();
            return CropDiameterObj;
        }

     
        /// <summary>
        /// to check the Column are Exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// To delete details from database
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <returns></returns>
        public string DeleteCropDiameter(int CROPDIAMETERID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPDIAMTR";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPDIAMETERID_", CROPDIAMETERID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
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

            return result;
        }
        /// <summary>
        /// To make data obsolete
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropDiameter(int CROPDIAMETERID,string IsDeleted)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPDIAMTR";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPDIAMETERID_", CROPDIAMETERID);
                cmd.Parameters.Add("isdeleted_", IsDeleted);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// To fetch edtails
        /// </summary>
        /// <returns></returns>
        public CropDiameterList GetCropDiameter()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTCROPDIAMTR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropDiameterBO objCropDiameter = null;
            CropDiameterList CropDiameterList = new CropDiameterList();

            while (dr.Read())
            {
                objCropDiameter = new CropDiameterBO();
                objCropDiameter.CROPDIAMETERID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDIAMETERID")));
                objCropDiameter.CROPDIAMETER = dr.GetString(dr.GetOrdinal("CROPDIAMETER"));
                objCropDiameter.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                
                CropDiameterList.Add(objCropDiameter);
            }

            dr.Close();

            return CropDiameterList;
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="objCropDiameter"></param>
        /// <returns></returns>
        public string EDITCropDiameter(CropDiameterBO objCropDiameter)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATECROPDIAMTR", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CROPDIAMETER_", objCropDiameter.CROPDIAMETER);
                dcmd.Parameters.Add("CROPDIAMETERID_", objCropDiameter.CROPDIAMETERID);
                dcmd.Parameters.Add("UpdatedBY", objCropDiameter.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }
    }
}