using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTCROPAGE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CROPAGE_", objCropAge.CROPAGE);
                dcmd.Parameters.Add("CreatedBY", objCropAge.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSELECTCROPAGEBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CROPAGEID_", CROPAGEID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd=null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPAGE";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPAGEID_", CROPAGEID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPAGE";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPAGEID_", CROPAGEID);
                cmd.Parameters.Add("isdeleted_", IsDeleted);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTCROPAGE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATECROPAGE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CROPAGE_", objCropAge.CROPAGE);
                dcmd.Parameters.Add("CROPID_", objCropAge.CROPAGEID);
                dcmd.Parameters.Add("UpdatedBY", objCropAge.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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