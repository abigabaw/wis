using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTCROPDIAMETER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CROPDIAMETER_", objCropDiameter.CROPDIAMETER);
                dcmd.Parameters.AddWithValue("CreatedBY", objCropDiameter.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSECTCROPDIAMETRID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CROPDIAMETERID_", CROPDIAMETERID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPDIAMTR";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPDIAMETERID_", CROPDIAMETERID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPDIAMTR";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPDIAMETERID_", CROPDIAMETERID);
                cmd.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCROPDIAMTR";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATECROPDIAMTR", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CROPDIAMETER_", objCropDiameter.CROPDIAMETER);
                dcmd.Parameters.AddWithValue("CROPDIAMETERID_", objCropDiameter.CROPDIAMETERID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objCropDiameter.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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