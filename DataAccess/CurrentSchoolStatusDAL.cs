using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CurrentSchoolStatusDAL
    {   
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="CurrentSchoolStatusBOObj"></param>
        /// <returns></returns>
        public string InsertSchoolStatusDetails(CurrentSchoolStatusBO CurrentSchoolStatusBOObj)
        {
            string returnResult = string.Empty;
            OracleConnection Con = new OracleConnection(AppConfiguration.ConnectionString);
            Con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_INSERTSCHOOLSTATUS", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {              
                cmd.Parameters.Add("S_CUR_SCH_STATUS", CurrentSchoolStatusBOObj.CurrentSchoolStatus);
                cmd.Parameters.Add("S_DESCRIPTION", CurrentSchoolStatusBOObj.Description);
                cmd.Parameters.Add("S_CREATEDBY", CurrentSchoolStatusBOObj.Createdby);

                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;       
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// To fetch all details from database
        /// </summary>
        /// <returns></returns>
        public SchoolStatusList GetAllSchoolDetails()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSCHOOLSTATUSALL";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            CurrentSchoolStatusBO CurSchStatusObj = null;
            SchoolStatusList CurSchStatusListObj = new SchoolStatusList();
            CurSchStatusObj = new CurrentSchoolStatusBO();

            while (dr.Read())
            {
                CurSchStatusObj = new CurrentSchoolStatusBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUSID")))
                    CurSchStatusObj.CurrentSchoolStatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CUR_SCH_STATUSID")));
                if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUS")))
                    CurSchStatusObj.CurrentSchoolStatus = dr.GetString(dr.GetOrdinal("CUR_SCH_STATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    CurSchStatusObj.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CurSchStatusObj.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                CurSchStatusListObj.Add(CurSchStatusObj);
            }
            dr.Close();
            return CurSchStatusListObj;
        }
        /// <summary>
        /// To fetch all details from database
        /// </summary>
        public SchoolStatusList GetSchoolDetails()
        {  
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSCHOOLSTATUS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            CurrentSchoolStatusBO CurSchStatusObj = null;
            SchoolStatusList CurSchStatusListObj = new SchoolStatusList();
            CurSchStatusObj = new CurrentSchoolStatusBO();

            while (dr.Read())
            {
                CurSchStatusObj = new CurrentSchoolStatusBO();

                if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUSID")))
                CurSchStatusObj.CurrentSchoolStatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CUR_SCH_STATUSID")));
                if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUS")))
                CurSchStatusObj.CurrentSchoolStatus = dr.GetString(dr.GetOrdinal("CUR_SCH_STATUS"));

                CurSchStatusListObj.Add(CurSchStatusObj);
            }
            dr.Close();
            return CurSchStatusListObj;            
        }              

        string proc = string.Empty;
       
        /// <summary>
        /// To fetch details based on ID
        /// </summary>
        /// <param name="CurrentSchoolStatusID"></param>
        /// <returns></returns>
        public CurrentSchoolStatusBO GetCurSchlStatusById(int CurrentSchoolStatusID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_SELECTCURSCHSTATUS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("S_CUR_SCH_STATUSID", CurrentSchoolStatusID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CurrentSchoolStatusBO CurSchStatusObj = null;
            SchoolStatusList CurSchStatusListObj = new SchoolStatusList();
            CurSchStatusObj = new CurrentSchoolStatusBO();

            while (dr.Read())
            {
                if (ColumnExists(dr, "CUR_SCH_STATUSID") && !dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUSID")))
                    CurSchStatusObj.CurrentSchoolStatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CUR_SCH_STATUSID")));

                if (ColumnExists(dr, "CUR_SCH_STATUS") && !dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUS")))
                    CurSchStatusObj.CurrentSchoolStatus = dr.GetString(dr.GetOrdinal("CUR_SCH_STATUS"));

                if (ColumnExists(dr, "DESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    CurSchStatusObj.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
            }
            dr.Close();

            return CurSchStatusObj;
        }
       /// <summary>
       /// To check whether the column exists
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


        //public int DeleteCurSchlStatus(int CurrentSchoolStatusID)
        //{
        //    OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
        //    OracleCommand cmd;

        //    string proc = "USP_MST_DELETESCHOOLSTATUS";

        //    cmd = new OracleCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("S_CUR_SCH_STATUSID", CurrentSchoolStatusID);
        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();
        //    return result;
        //}
        /// <summary>
        /// To delete data from database
        /// </summary>
        /// <param name="CurrentSchoolStatusID"></param>
        /// <returns></returns>
        public string DeleteCurSchlStatus(int CurrentSchoolStatusID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETESCHOOLSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("S_CUR_SCH_STATUSID", CurrentSchoolStatusID);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected item is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="CurrentSchoolStatusBOObj"></param>
        /// <param name="EditCurSchStatusID"></param>
        /// <returns></returns>
        public string EditCurSchStatus(CurrentSchoolStatusBO CurrentSchoolStatusBOObj, int EditCurSchStatusID)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATESCHOOLSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("S_CUR_SCH_STATUSID", EditCurSchStatusID);
                dcmd.Parameters.Add("S_CUR_SCH_STATUS", CurrentSchoolStatusBOObj.CurrentSchoolStatus);
                dcmd.Parameters.Add("S_DESCRIPTION", CurrentSchoolStatusBOObj.Description);
                dcmd.Parameters.Add("S_UPDATEDBY", CurrentSchoolStatusBOObj.Createdby);

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
        /// <summary>
        /// To search details
        /// </summary>
        /// <param name="CurSchoolStatus"></param>
        /// <returns></returns>
        public object SearchSchoolStatus(string CurSchoolStatus)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATESCHOOLSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("CUR_SCH_STATUS", CurSchoolStatus);

                return dcmd.ExecuteNonQuery();
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
       /// To make data obsolete
       /// </summary>
       /// <param name="CurrentSchoolStatusID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteSchoolStatus(int CurrentSchoolStatusID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETECHOOLSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("S_CUR_SCH_STATUSID", CurrentSchoolStatusID);
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
    }
}