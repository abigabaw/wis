using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class StructureTypeDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Type
        /// </summary>
        /// <returns></returns>
        public StructureTypeList GetAllStructureType()
        {
            //Get All Records from Structure Type
            proc = "USP_MST_GET_ALL_STRUCTURETYPE";
            cnn = new SqlConnection(con);
            StructureTypeBO objStructureType = null;

            StructureTypeList lstStructureTypeList = new StructureTypeList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureType = new StructureTypeBO();

                    if (ColumnExists(dr, "str_typeid") && !dr.IsDBNull(dr.GetOrdinal("str_typeid")))
                        objStructureType.StructureTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_typeid")));
                    if (ColumnExists(dr, "str_type") && !dr.IsDBNull(dr.GetOrdinal("str_type")))
                        objStructureType.StructureTypeName = dr.GetString(dr.GetOrdinal("str_type"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objStructureType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                   // objStructureType = MapData(dr);
                    lstStructureTypeList.Add(objStructureType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureTypeList;
        }
        
        /// <summary>
        /// To Get Structure Type
        /// </summary>
        /// <returns></returns>
        public StructureTypeList GetStructureType()
        {
            // Get Only Active Structure Types
            proc = "USP_MST_GET_STRUCTURETYPE";
            cnn = new SqlConnection(con);
            StructureTypeBO objStructureType = null;
            
            StructureTypeList lstStructureTypeList = new StructureTypeList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureType = new StructureTypeBO();
                 
                    objStructureType = MapData(dr);
                    lstStructureTypeList.Add(objStructureType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureTypeList;
        }

        /// <summary>
        /// To Get Structure Type By Id
        /// </summary>
        /// <param name="StructureTypeID"></param>
        /// <returns></returns>
        public StructureTypeBO GetStructureTypeById(int StructureTypeID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_MST_GET_STRUCTURETYPEBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("shockid_", StructureTypeID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            StructureTypeBO objStructureType = null;

            while (dr.Read())
            {
                objStructureType = new StructureTypeBO();

                if (ColumnExists(dr, "str_typeid") && !dr.IsDBNull(dr.GetOrdinal("str_typeid")))
                    objStructureType.StructureTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_typeid")));
                if (ColumnExists(dr, "str_type") && !dr.IsDBNull(dr.GetOrdinal("str_type")))
                    objStructureType.StructureTypeName = dr.GetString(dr.GetOrdinal("str_type"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objStructureType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                //objStructureType = objStructureType;//MapData(dr);
            }
            dr.Close();

            return objStructureType;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private StructureTypeBO MapData(IDataReader reader)
        {
            StructureTypeBO oStructureTypeBO = new StructureTypeBO();
       
            if (ColumnExists(reader, "str_typeid") && !reader.IsDBNull(reader.GetOrdinal("str_typeid")))
                oStructureTypeBO.StructureTypeID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("str_typeid")));
            if (ColumnExists(reader, "str_type") && !reader.IsDBNull(reader.GetOrdinal("str_type")))
                oStructureTypeBO.StructureTypeName = reader.GetString(reader.GetOrdinal("str_type"));
            if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
                oStructureTypeBO.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));

            return oStructureTypeBO;
        }
        #endregion

        #region Save, Update & Delete Record

        /// <summary>
        /// To Save Structure Type
        /// </summary>
        /// <param name="oStructureType"></param>
        /// <returns></returns>
        public string SaveStructureType(StructureTypeBO oStructureType)
        {
            string returnResult;
            cnn = new SqlConnection(con);

            proc = "USP_MST_INS_STRUCTURETYPE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("str_type_", oStructureType.StructureTypeName);

            cmd.Parameters.AddWithValue("isdeleted_", oStructureType.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oStructureType.CreatedBy);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Structure Type
        /// </summary>
        /// <param name="oStructureType"></param>
        /// <returns></returns>
        public string UpdateStructureType(StructureTypeBO oStructureType)
        {
            string returnResult;
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPD_STRUCTURETYPE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("str_typeid_", oStructureType.StructureTypeID);
            cmd.Parameters.AddWithValue("str_type_", oStructureType.StructureTypeName);

            cmd.Parameters.AddWithValue("updatedby_", oStructureType.CreatedBy);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
    
        /// <summary>
        /// To Delete Structure Type
        /// </summary>
        /// <param name="StructureTypeID_"></param>
        /// <returns></returns>
        public string DeleteStructureType(int StructureTypeID_)
        {
            string returnResult;
            cnn = new SqlConnection(con);
           
            proc = "USP_MST_DEL_STRUCTURETYPE";
            try
            {
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("str_typeid_", StructureTypeID_);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("ORA-02292"))
                //{
                //    returnResult = "Selected Role is already in use. Connot delete";
                //}
                //else
                //{
                    throw ex;
                //}
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
            return returnResult;
        }

        /// <summary>
        /// To Obsolete Structure Type
        /// </summary>
        /// <param name="StructureTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureType(int StructureTypeID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_STRUCTURETYPE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("str_typeid_", StructureTypeID);
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
        #endregion
    }
}