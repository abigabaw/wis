using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class CropDescriptionDAL
    {
        /// <summary>
        /// TO save details to database
        /// </summary>
        /// <param name="objCropDescription"></param>
        /// <returns></returns>
        public string InsertCropDescription(CropDescriptionBO objCropDescription)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTCROPDESC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CropDesc", objCropDescription.CROPDESNAME);
                dcmd.Parameters.AddWithValue("CreatedBY", objCropDescription.UserID);
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
        /// GetAllCropDescription
        /// </summary>
        /// <returns></returns>
        public CropDescriptionList GetAllCropDescription()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_ALLCROPDESCRIPTIONS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropDescriptionBO objCropDescription = null;
            CropDescriptionList CropDescriptionList = new CropDescriptionList();

            while (dr.Read())
            {
                objCropDescription = new CropDescriptionBO();
                objCropDescription.CROPDESID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID")));
                objCropDescription.CROPDESNAME = dr.GetString(dr.GetOrdinal("CROPDESCRIPTION"));
                objCropDescription.CROPDESISDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                objCropDescription.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropDescriptionList.Add(objCropDescription);
            }

            dr.Close();

            return CropDescriptionList;
        }
        /// <summary>
        /// GetCropDescription
        /// </summary>
        /// <returns></returns>
        public CropDescriptionList GetCropDescription()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCROPDESCRIPTION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropDescriptionBO objCropDescription = null;
            CropDescriptionList CropDescriptionList = new CropDescriptionList();

            while (dr.Read())
            {
                objCropDescription = new CropDescriptionBO();
                objCropDescription.CROPDESID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID")));
                objCropDescription.CROPDESNAME = dr.GetString(dr.GetOrdinal("CROPDESCRIPTION"));
                objCropDescription.CROPDESISDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                objCropDescription.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropDescriptionList.Add(objCropDescription);
            }

            dr.Close();

            return CropDescriptionList;
        }
        /// <summary>
        /// To delete details from database
        /// </summary>
        /// <param name="CROPDESCRIPTIONID"></param>
        /// <returns></returns>
        public string DeleteCropDESC(int CROPDESCRIPTIONID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd=null;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPDESC";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
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
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return result;
        }
        /// <summary>
        /// To make data obsolete
        /// </summary>
        /// <param name="CROPDESCRIPTIONID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropDESC(int CROPDESCRIPTIONID,string IsDeleted)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd = null;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPDESC";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
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
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return result;
        }
        /// <summary>
        /// to get details by ID
        /// </summary>
        /// <param name="CROPDESCRIPTIONID"></param>
        /// <returns></returns>
        public CropDescriptionBO GetCropDescriptionId(int CROPDESCRIPTIONID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTCROPDESC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropDescriptionBO CropDescriptionObj = null;
            CropDescriptionList CropDescriptionList = new CropDescriptionList();

            CropDescriptionObj = new CropDescriptionBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CROPDESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("CROPDESCRIPTION")))
                    CropDescriptionObj.CROPDESNAME = dr.GetString(dr.GetOrdinal("CROPDESCRIPTION"));
                if (ColumnExists(dr, "CROPDESCRIPTIONID") && !dr.IsDBNull(dr.GetOrdinal("CROPDESCRIPTIONID")))
                    CropDescriptionObj.CROPDESID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID")));
                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CropDescriptionObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();
            return CropDescriptionObj;
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
        /// To update details to database
        /// </summary>
        /// <param name="objCropDesc"></param>
        /// <returns></returns>
        public string EDITCropDescr(CropDescriptionBO objCropDesc)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATECROPDESC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CROPNAME_", objCropDesc.CROPDESNAME);
                dcmd.Parameters.AddWithValue("CROPID_", objCropDesc.CROPDESID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objCropDesc.UserID);
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