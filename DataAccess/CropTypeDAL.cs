using System;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_CROPTYPE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);         

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_ALLCROPTYPES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection Con = new OracleConnection(AppConfiguration.ConnectionString);
            Con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_INSERT_CROPTYPE", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.Add("C_CROPTYPE", CropTypeBOObj.CropType);
                //cmd.Parameters.Add("C_UnitId", CropTypeBOObj.UnitMeasure);
                cmd.Parameters.Add("C_CREATEDBY", CropTypeBOObj.Createdby);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_CROPTYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.Add("C_CROPTYPEID", CROPTYPEID);
                dcmd.Parameters.Add("C_CROPTYPE", CropTypeBOObj.CropType);
                //dcmd.Parameters.Add("C_UnitID", CropTypeBOObj.UnitMeasure);
                dcmd.Parameters.Add("C_UPDATEDBY", CropTypeBOObj.Createdby);
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
        /// To delete data from database
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public string DeleteCropTypeRow(int CROPTYPEID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd=null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DEL_CROPTYPE";
                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPTYPEID_", CROPTYPEID);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPTYPE";
                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPTYPEID_", CROPTYPEID);
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
        /// To get details by ID
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public CropTypeBO GetCropTypeById(int CROPTYPEID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_CROPTYPE ";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("C_CROPTYPEID", CROPTYPEID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                       
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
