using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CropTypeDAL
    {
        /// <summary>
        /// To get details from database
        /// </summary>
        /// <returns></returns>
        public object GetCropDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_CROPTYPE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);         

            CropTypeBO CropTypeBOObj = new CropTypeBO();
            CropTypeList CropTypeListObj = new CropTypeList();
            CropTypeBOObj = new CropTypeBO(); 

            while (dr.Read())
            {
                CropTypeBOObj = new CropTypeBO();
                CropTypeBOObj.CROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPTYPEID")));
                CropTypeBOObj.CropType = dr.GetString(dr.GetOrdinal("CROPTYPE"));
                CropTypeBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropTypeListObj.Add(CropTypeBOObj);
            }
            dr.Close();
            return CropTypeListObj;
        }
        /// <summary>
        /// To get details from database
        /// </summary>
        /// <returns></returns>
        public object GetAllCropDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALLCROPTYPES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            CropTypeBO CropTypeBOObj = new CropTypeBO();
            CropTypeList CropTypeListObj = new CropTypeList();
            CropTypeBOObj = new CropTypeBO();

            while (dr.Read())
            {
                CropTypeBOObj = new CropTypeBO();
                CropTypeBOObj.CROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPTYPEID")));
                CropTypeBOObj.CropType = dr.GetString(dr.GetOrdinal("CROPTYPE"));
                //CropTypeBOObj.UnitName = dr.GetString(dr.GetOrdinal("UNITNAME"));
                CropTypeBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropTypeListObj.Add(CropTypeBOObj);
            }
            dr.Close();
            return CropTypeListObj;
        }
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="CropTypeBOObj"></param>
        /// <returns></returns>
        public string InsertCropTypeDetails(CropTypeBO CropTypeBOObj)
        {
            string returnResult = "";
            SqlConnection Con = new SqlConnection(AppConfiguration.ConnectionString);
            Con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_INSERT_CROPTYPE", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.AddWithValue("C_CROPTYPE", CropTypeBOObj.CropType);
                //cmd.Parameters.AddWithValue("C_UnitId", CropTypeBOObj.UnitMeasure);
                cmd.Parameters.AddWithValue("C_CREATEDBY", CropTypeBOObj.Createdby);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();

                return returnResult;
            }
            catch(Exception ee)
            {
                throw ee;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="CropTypeBOObj"></param>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public string EditCropTypeDetails(CropTypeBO CropTypeBOObj, int CROPTYPEID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_CROPTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.AddWithValue("C_CROPTYPEID", CROPTYPEID);
                dcmd.Parameters.AddWithValue("C_CROPTYPE", CropTypeBOObj.CropType);
                //dcmd.Parameters.AddWithValue("C_UnitID", CropTypeBOObj.UnitMeasure);
                dcmd.Parameters.AddWithValue("C_UPDATEDBY", CropTypeBOObj.Createdby);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();
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

            return result;
        }
        /// <summary>
        /// To delete data from database
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public string DeleteCropTypeRow(int CROPTYPEID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd=null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DEL_CROPTYPE";
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPTYPEID_", CROPTYPEID);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    message = "Selected CROP Type is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }


            return message;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropTypeRow(int CROPTYPEID,string IsDeleted)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPTYPE";
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPTYPEID_", CROPTYPEID);
                cmd.Parameters.AddWithValue("isdeleted_", IsDeleted);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return message;
        }
        /// <summary>
        /// To get details by ID
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public CropTypeBO GetCropTypeById(int CROPTYPEID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_CROPTYPE ";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("C_CROPTYPEID", CROPTYPEID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                       
            CropTypeBO CropTypeBOObj = null;
            CropTypeList CropTypeListObj = new CropTypeList();
            CropTypeBOObj = new CropTypeBO();

            while (dr.Read())
            {
                if (ColumnExists(dr, "CROPTYPEID") && !dr.IsDBNull(dr.GetOrdinal("CROPTYPEID")))
                    CropTypeBOObj.CROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPTYPEID")));

                if (ColumnExists(dr, "CROPTYPE") && !dr.IsDBNull(dr.GetOrdinal("CROPTYPE")))
                    CropTypeBOObj.CropType = dr.GetString(dr.GetOrdinal("CROPTYPE"));

                //if (ColumnExists(dr, "UNITNAME") && !dr.IsDBNull(dr.GetOrdinal("UNITNAME")))
                //    CropTypeBOObj.UnitName = dr.GetString(dr.GetOrdinal("UNITNAME"));

                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CropTypeBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();

            return CropTypeBOObj;
        }
        /// <summary>
        /// to check whether column exists
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
    }
}
