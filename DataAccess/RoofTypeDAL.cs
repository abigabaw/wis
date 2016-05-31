using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class RoofTypeDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Roof Type
        /// </summary>
        /// <returns></returns>
        public RoofTypeList GetAllRoofType()
        {
            proc = "USP_MST_GET_ALLROOF";
            cnn = new OracleConnection(con);
            RoofTypeBO objRoofType = null;

            RoofTypeList lstRoofTypeList = new RoofTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objRoofType = new RoofTypeBO();

                    if (ColumnExists(dr, "roofid") && !dr.IsDBNull(dr.GetOrdinal("roofid")))
                        objRoofType.RoofTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("roofid")));
                    if (ColumnExists(dr, "RoofType") && !dr.IsDBNull(dr.GetOrdinal("RoofType")))
                        objRoofType.RoofTypeName = dr.GetString(dr.GetOrdinal("RoofType"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objRoofType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    lstRoofTypeList.Add(objRoofType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRoofTypeList;
        }

        /// <summary>
        /// To Get Roof Type
        /// </summary>
        /// <returns></returns>
        public RoofTypeList GetRoofType()
        {
            proc = "USP_MST_GET_ROOF";
            cnn = new OracleConnection(con);
            RoofTypeBO objRoofType = null;
            
            RoofTypeList lstRoofTypeList = new RoofTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objRoofType = new RoofTypeBO();

                    if (ColumnExists(dr, "roofid") && !dr.IsDBNull(dr.GetOrdinal("roofid")))
                        objRoofType.RoofTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("roofid")));
                    if (ColumnExists(dr, "RoofType") && !dr.IsDBNull(dr.GetOrdinal("RoofType")))
                        objRoofType.RoofTypeName = dr.GetString(dr.GetOrdinal("RoofType"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objRoofType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                   
                    lstRoofTypeList.Add(objRoofType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRoofTypeList;
        }

        /// <summary>
        /// To Get Roof Type By Id
        /// </summary>
        /// <param name="RoofTypeID"></param>
        /// <returns></returns>
        public RoofTypeBO GetRoofTypeById(int RoofTypeID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_ROOF_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("roofid_", RoofTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RoofTypeBO objRoofType = null;


            while (dr.Read())
            {
                objRoofType = new RoofTypeBO();

                if (ColumnExists(dr, "roofid") && !dr.IsDBNull(dr.GetOrdinal("roofid")))
                    objRoofType.RoofTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("roofid")));
                if (ColumnExists(dr, "rooftype") && !dr.IsDBNull(dr.GetOrdinal("rooftype")))
                    objRoofType.RoofTypeName = dr.GetString(dr.GetOrdinal("rooftype"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objRoofType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objRoofType;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Save, Update & Delete Record
        /// <summary>
        /// To Save Roof Type
        /// </summary>
        /// <param name="oRoofType"></param>
        /// <returns></returns>
        public string SaveRoofType(RoofTypeBO oRoofType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_ROOF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("rooftype_", oRoofType.RoofTypeName);

            cmd.Parameters.Add("isdeleted_", oRoofType.IsDeleted);
            cmd.Parameters.Add("createdby_", oRoofType.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Roof Type
        /// </summary>
        /// <param name="oRoofType"></param>
        /// <returns></returns>
        public string UpdateRoofType(RoofTypeBO oRoofType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_ROOF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("roofid_", oRoofType.RoofTypeID);
            cmd.Parameters.Add("RoofType_", oRoofType.RoofTypeName);

            cmd.Parameters.Add("updatedby_", oRoofType.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
    
        /// <summary>
        /// To Delete Roof Type
        /// </summary>
        /// <param name="RoofTypeID_"></param>
        /// <param name="UserID_"></param>
        /// <returns></returns>
        public string DeleteRoofType(int RoofTypeID_,int UserID_)
        {
            string retrunResult = string.Empty;
            cnn = new OracleConnection(con);

            proc = "USP_MST_DEL_ROOF";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("roofid_", RoofTypeID_);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                retrunResult = cmd.Parameters["errorMessage_"].Value.ToString();
            return retrunResult;
        }

        /// <summary>
        /// To Obsolete Floor Type DAL
        /// </summary>
        /// <param name="RoofTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFloorTypeDAL(int RoofTypeID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_ROOF", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("roofid_", RoofTypeID);
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
        #endregion
    }
}