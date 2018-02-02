using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class CropAgeDAL
    {
        /// <summary>
        /// TO insert details to database
        /// </summary>
        /// <param name="objCropAge"></param>
        /// <returns></returns>
        public string InsertCropAge(CropAgeBO objCropAge)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTCROPAGE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CROPAGE_", objCropAge.CROPAGE);
                dcmd.Parameters.AddWithValue("CreatedBY", objCropAge.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
        /// Get CropAge By Id
        /// </summary>
        /// <param name="CROPAGEID"></param>
        /// <returns></returns>
        public CropAgeBO GetCropAgeById(int CROPAGEID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTCROPAGEBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CROPAGEID_", CROPAGEID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropAgeBO CropAgeObj = null;
            CropAgeList CropAgeList = new CropAgeList();

            CropAgeObj = new CropAgeBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CROPAGE") && !dr.IsDBNull(dr.GetOrdinal("CROPAGE")))
                    CropAgeObj.CROPAGE = dr.GetString(dr.GetOrdinal("CROPAGE"));
                if (ColumnExists(dr, "CROPAGEID") && !dr.IsDBNull(dr.GetOrdinal("CROPAGEID")))
                    CropAgeObj.CROPAGEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPAGEID")));
                if (ColumnExists(dr, "ISDELETED") && !dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CropAgeObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();
            return CropAgeObj;
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
        /// <param name="CROPAGEID"></param>
        /// <returns></returns>
        public string DeleteCropAge(int CROPAGEID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd=null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPAGE";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPAGEID_", CROPAGEID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    message = "Selected CROP AGE is already in use. Cannot delete";
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
        /// To make data obsolete
        /// </summary>
        /// <param name="CROPAGEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropAge(int CROPAGEID,string IsDeleted)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPAGE";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CROPAGEID_", CROPAGEID);
                cmd.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
        /// To fetch details
        /// </summary>
        /// <returns></returns>
        public CropAgeList GetCropAge()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCROPAGE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropAgeBO objCropAge = null;
            CropAgeList CropAgeList = new CropAgeList();

            while (dr.Read())
            {
                objCropAge = new CropAgeBO();
                objCropAge.CROPAGEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPAGEID")));
                objCropAge.CROPAGE = dr.GetString(dr.GetOrdinal("CROPAGE"));
                objCropAge.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                
                CropAgeList.Add(objCropAge);
            }

            dr.Close();

            return CropAgeList;
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="objCropAge"></param>
        /// <returns></returns>
        public string EDITCROPAGE(CropAgeBO objCropAge)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATECROPAGE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CROPAGE_", objCropAge.CROPAGE);
                dcmd.Parameters.AddWithValue("CROPID_", objCropAge.CROPAGEID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objCropAge.UserID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
    }
}