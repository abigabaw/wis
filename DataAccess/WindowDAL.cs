using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class WindowTypeDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Window Type()
        /// </summary>
        /// <returns></returns>
        public WindowTypeList GetAllWindowType()
        {
            proc = "USP_MST_GET_ALLWINDOW";
            cnn = new OracleConnection(con);
            WindowTypeBO objWindowType = null;

            WindowTypeList lstWindowTypeList = new WindowTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWindowType = new WindowTypeBO();

                    if (ColumnExists(dr, "windowid") && !dr.IsDBNull(dr.GetOrdinal("windowid")))
                        objWindowType.WindowTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("windowid")));
                    if (ColumnExists(dr, "windowtype") && !dr.IsDBNull(dr.GetOrdinal("windowtype")))
                        objWindowType.WindowTypeName = dr.GetString(dr.GetOrdinal("windowtype"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objWindowType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstWindowTypeList.Add(objWindowType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstWindowTypeList;
        }

        /// <summary>
        /// To Get Window Type
        /// </summary>
        /// <returns></returns>
        public WindowTypeList GetWindowType()
        {
            proc = "USP_MST_GET_WINDOW";
            cnn = new OracleConnection(con);
            WindowTypeBO objWindowType = null;
            
            WindowTypeList lstWindowTypeList = new WindowTypeList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWindowType = new WindowTypeBO();

                    if (ColumnExists(dr, "windowid") && !dr.IsDBNull(dr.GetOrdinal("windowid")))
                        objWindowType.WindowTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("windowid")));
                    if (ColumnExists(dr, "windowtype") && !dr.IsDBNull(dr.GetOrdinal("windowtype")))
                        objWindowType.WindowTypeName = dr.GetString(dr.GetOrdinal("windowtype"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objWindowType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstWindowTypeList.Add(objWindowType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstWindowTypeList;
        }

        /// <summary>
        /// To Get Window Type By Id
        /// </summary>
        /// <param name="WindowTypeID"></param>
        /// <returns></returns>
        public WindowTypeBO GetWindowTypeById(int WindowTypeID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_WINDOW_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Windowid_", WindowTypeID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WindowTypeBO objWindowType = null;


            while (dr.Read())
            {
                objWindowType = new WindowTypeBO();

                if (ColumnExists(dr, "Windowid") && !dr.IsDBNull(dr.GetOrdinal("Windowid")))
                    objWindowType.WindowTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Windowid")));
                if (ColumnExists(dr, "WindowType") && !dr.IsDBNull(dr.GetOrdinal("WindowType")))
                    objWindowType.WindowTypeName = dr.GetString(dr.GetOrdinal("WindowType"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objWindowType.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objWindowType;
        }

        //To Check Weather Column Exists or Not
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
        /// To Save Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string SaveWindowType(WindowTypeBO oWindowType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_Window";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("Windowtype_", oWindowType.WindowTypeName);

            cmd.Parameters.Add("isdeleted_", oWindowType.IsDeleted);
            cmd.Parameters.Add("createdby_", oWindowType.UserID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string UpdateWindowType(WindowTypeBO oWindowType)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_Window";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("Windowid_", oWindowType.WindowTypeID);
            cmd.Parameters.Add("WindowType_", oWindowType.WindowTypeName);

            cmd.Parameters.Add("updatedby_", oWindowType.UserID);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
    
        //public int DeleteWindowType(int WindowTypeID_,int UserID_)
        //{
        //    cnn = new OracleConnection(con);

        //    proc = "USP_MST_DEL_Window";

        //    cmd = new OracleCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("Windowid_", WindowTypeID_);
        //    cmd.Parameters.Add("updatedby_", UserID_);

        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();

        //    return result;
        //}

        /// <summary>
        /// To Delete Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string DeleteWindowType(WindowTypeBO oWindowType)
        {
            cnn = new OracleConnection(con);
            string retrunResult = string.Empty;
            proc = "USP_MST_DEL_Window";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Windowid_", oWindowType.WindowTypeID);
           // cmd.Parameters.Add("updatedby_", oWindowType.UserID);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                retrunResult = cmd.Parameters["errorMessage_"].Value.ToString();

            return retrunResult;
        }

        /// <summary>
        /// To Get Obsolete Floor Type DAL
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
                myCommand = new OracleCommand("USP_MST_OBSOLETE_WINDOW", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("Windowid_", FloorTypeID);
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