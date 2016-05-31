using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class WallTypeDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Wall Type
        /// </summary>
        /// <returns></returns>
        public WallTypeList GetAllWallType()
        {
            proc = "USP_MST_GET_ALLWALL";
            cnn = new OracleConnection(con);
            WallTypeBO objWallType = null;

            WallTypeList lstWallTypeList = new WallTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWallType = new WallTypeBO();

                    if (ColumnExists(dr, "wallid") && !dr.IsDBNull(dr.GetOrdinal("wallid")))
                        objWallType.WallTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("wallid")));
                    if (ColumnExists(dr, "WallType") && !dr.IsDBNull(dr.GetOrdinal("WallType")))
                        objWallType.WallTypeName = dr.GetString(dr.GetOrdinal("WallType"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objWallType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstWallTypeList.Add(objWallType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstWallTypeList;
        }

        /// <summary>
        /// To Get Wall Type
        /// </summary>
        /// <returns></returns>
        public WallTypeList GetWallType()
        {
            proc = "USP_MST_GET_WALL";
            cnn = new OracleConnection(con);
            WallTypeBO objWallType = null;
            
            WallTypeList lstWallTypeList = new WallTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWallType = new WallTypeBO();

                    if (ColumnExists(dr, "wallid") && !dr.IsDBNull(dr.GetOrdinal("wallid")))
                        objWallType.WallTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("wallid")));
                    if (ColumnExists(dr, "WallType") && !dr.IsDBNull(dr.GetOrdinal("WallType")))
                        objWallType.WallTypeName = dr.GetString(dr.GetOrdinal("WallType"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objWallType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstWallTypeList.Add(objWallType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstWallTypeList;
        }

        /// <summary>
        /// To Get Wall Type By Id
        /// </summary>
        /// <param name="WallTypeID"></param>
        /// <returns></returns>
        public WallTypeBO GetWallTypeById(int WallTypeID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_WALL_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("wallid_", WallTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WallTypeBO objWallType = null;


            while (dr.Read())
            {
                objWallType = new WallTypeBO();

                if (ColumnExists(dr, "wallid") && !dr.IsDBNull(dr.GetOrdinal("wallid")))
                    objWallType.WallTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("wallid")));
                if (ColumnExists(dr, "WallType") && !dr.IsDBNull(dr.GetOrdinal("WallType")))
                    objWallType.WallTypeName = dr.GetString(dr.GetOrdinal("WallType"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objWallType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objWallType;
        }

        /// <summary>
        /// To check weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
          //  string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
              //  ColumnNames[i] = reader.GetName(i);

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
        /// To Add Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string SaveWallType(WallTypeBO oWallType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_WALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("walltype_", oWallType.WallTypeName);

            cmd.Parameters.Add("isdeleted_", oWallType.IsDeleted);
            cmd.Parameters.Add("createdby_", oWallType.UserID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            return returnResult;
        }

        /// <summary>
        /// To Update Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string UpdateWallType(WallTypeBO oWallType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_WALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("wallid_", oWallType.WallTypeID);
            cmd.Parameters.Add("WallType_", oWallType.WallTypeName);

            cmd.Parameters.Add("updatedby_", oWallType.UserID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            return returnResult;
        }
    
        /// <summary>
        /// To Delete Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string DeleteWallType(WallTypeBO oWallType)
        {
            cnn = new OracleConnection(con);
            string retrunResult = string.Empty;
            proc = "USP_MST_DEL_WALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("wallid_", oWallType.WallTypeID);
            //cmd.Parameters.Add("updatedby_", oWallType.UserID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                retrunResult = cmd.Parameters["errorMessage_"].Value.ToString();
            return retrunResult;
        }

        /// <summary>
        /// To Obsolete Wall Type
        /// </summary>
        /// <param name="WallTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFloorTypeDAL(int WallTypeID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_WALL", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("wallid_", WallTypeID);
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