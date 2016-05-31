using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTCROPDESC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CropDesc", objCropDescription.CROPDESNAME);
                dcmd.Parameters.Add("CreatedBY", objCropDescription.UserID);
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
        /// GetAllCropDescription
        /// </summary>
        /// <returns></returns>
        public CropDescriptionList GetAllCropDescription()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_ALLCROPDESCRIPTIONS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTCROPDESCRIPTION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd=null;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETECROPDESC";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETECROPDESC";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSELECTCROPDESC";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CROPDESCRIPTIONID_", CROPDESCRIPTIONID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATECROPDESC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CROPNAME_", objCropDesc.CROPDESNAME);
                dcmd.Parameters.Add("CROPID_", objCropDesc.CROPDESID);
                dcmd.Parameters.Add("UpdatedBY", objCropDesc.UserID);
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