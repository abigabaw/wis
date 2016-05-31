using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class FloorTypeDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <returns></returns>
        public FloorTypeList GetAllFloorType()//(FloorType oFloorType)
        {
            proc = "USP_MST_GET_ALLFLOOR";
            cnn = new OracleConnection(con);
            FloorTypeBO objFloorType = null;

            FloorTypeList lstFloorTypeList = new FloorTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFloorType = new FloorTypeBO();

                    if (ColumnExists(dr, "floorid") && !dr.IsDBNull(dr.GetOrdinal("floorid")))
                        objFloorType.FloorTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("floorid")));
                    if (ColumnExists(dr, "floortype") && !dr.IsDBNull(dr.GetOrdinal("floortype")))
                        objFloorType.FloorTypeName = dr.GetString(dr.GetOrdinal("floortype"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objFloorType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    lstFloorTypeList.Add(objFloorType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstFloorTypeList;
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <returns></returns>
        public FloorTypeList GetFloorType()//(FloorType oFloorType)
        {
            proc = "USP_MST_GET_FLOOR";
            cnn = new OracleConnection(con);
            FloorTypeBO objFloorType = null;

            FloorTypeList lstFloorTypeList = new FloorTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFloorType = new FloorTypeBO();

                    if (ColumnExists(dr, "floorid") && !dr.IsDBNull(dr.GetOrdinal("floorid")))
                        objFloorType.FloorTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("floorid")));
                    if (ColumnExists(dr, "floortype") && !dr.IsDBNull(dr.GetOrdinal("floortype")))
                        objFloorType.FloorTypeName = dr.GetString(dr.GetOrdinal("floortype"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objFloorType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstFloorTypeList.Add(objFloorType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstFloorTypeList;
        }
        /// <summary>
        /// To fetch details by ID
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <returns></returns>
        public FloorTypeBO GetFloorTypeById(int FloorTypeID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_FLOOR_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("floorid_", FloorTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FloorTypeBO objFloorType = null;


            while (dr.Read())
            {
                objFloorType = new FloorTypeBO();

                if (ColumnExists(dr, "floorid") && !dr.IsDBNull(dr.GetOrdinal("floorid")))
                    objFloorType.FloorTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("floorid")));
                if (ColumnExists(dr, "floortype") && !dr.IsDBNull(dr.GetOrdinal("floortype")))
                    objFloorType.FloorTypeName = dr.GetString(dr.GetOrdinal("floortype"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objFloorType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objFloorType;
        }
        /// <summary>
        /// To check whether column exists
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
        #endregion

        #region Save, Update & Delete Record
        /// <summary>
        /// to save data 
        /// </summary>
        /// <param name="oFloorType"></param>
        /// <returns></returns>
        public string SaveFloorType(FloorTypeBO oFloorType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_FLOOR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("floortype_", oFloorType.FloorTypeName);

            cmd.Parameters.Add("isdeleted_", oFloorType.IsDeleted);
            cmd.Parameters.Add("createdby_", oFloorType.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="oFloorType"></param>
        /// <returns></returns>
        public string UpdateFloorType(FloorTypeBO oFloorType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_FLOOR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("floorid_", oFloorType.FloorTypeID);
            cmd.Parameters.Add("floortype_", oFloorType.FloorTypeName);

            cmd.Parameters.Add("updatedby_", oFloorType.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// to delete data 
        /// </summary>
        /// <param name="FloorTypeID_"></param>
        /// <param name="UserID_"></param>
        /// <returns></returns>
        public string DeleteFloorType(int FloorTypeID_, int UserID_)
        {
            cnn = new OracleConnection(con);
            string retrunResult = string.Empty;
            proc = "USP_MST_DEL_FLOOR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("floorid_", FloorTypeID_);
           // cmd.Parameters.Add("updatedby_", UserID_);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                retrunResult = cmd.Parameters["errorMessage_"].Value.ToString();
            return retrunResult;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFloorTypeDAL(int FloorTypeID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_FLOOR", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("str_typeid_", FloorTypeID);
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