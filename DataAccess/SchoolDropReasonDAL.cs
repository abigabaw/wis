using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class SchoolDropReasonDAL
    {
        string returnResult = string.Empty;
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="SchoolDropReasonBOobj"></param>
        /// <returns></returns>
        public string Insert(SchoolDropReasonBO SchoolDropReasonBOobj)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand dcmd = new OracleCommand("usp_MST_INSERTSCHOOLDROP", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("p_SCH_DRP_REASON", SchoolDropReasonBOobj.Schooldropreason);
                dcmd.Parameters.Add("p_DESCRIPTION", SchoolDropReasonBOobj.Description);
                dcmd.Parameters.Add("p_CREATEDBY", SchoolDropReasonBOobj.Createdby);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                //return dcmd.ExecuteNonQuery();

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                dcmd.Dispose();
                con.Close();
                con.Dispose();

            }
            return returnResult;
        }

        /// <summary>
        /// To Get school Drop Reason
        /// </summary>
        /// <returns></returns>
        public SchoolDropReasonList GetschoolDropReason()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GETSCHOOLDROPREASON";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SchoolDropReasonBO objUser = null;
            SchoolDropReasonList ReasonList = new SchoolDropReasonList();

            while (dr.Read())
            {
                objUser = new SchoolDropReasonBO();
                if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASONID")))
                    objUser.SchooldropreasonID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SCH_DRP_REASONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASON")))
                    objUser.Schooldropreason = dr.GetString(dr.GetOrdinal("SCH_DRP_REASON"));

                ReasonList.Add(objUser);
            }

            dr.Close();

            return ReasonList;
        }

        /// <summary>
        /// To Get All School Drop Reason
        /// </summary>
        /// <returns></returns>
        public SchoolDropReasonList GetAllSchoolDropReason()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GETSCHOOLDROPREASONALL";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SchoolDropReasonBO objUser = null;
            SchoolDropReasonList ReasonList = new SchoolDropReasonList();

            while (dr.Read())
            {
                objUser = new SchoolDropReasonBO();
                if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASONID")))
                    objUser.SchooldropreasonID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SCH_DRP_REASONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASON")))
                    objUser.Schooldropreason = dr.GetString(dr.GetOrdinal("SCH_DRP_REASON"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    objUser.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    objUser.Isdeleted = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ISDELETED")));

                ReasonList.Add(objUser);
            }

            dr.Close();

            return ReasonList;
        }

        /// <summary>
        /// To Get school Drop Reason by ID
        /// </summary>
        /// <param name="SchooldropreasonID"></param>
        /// <returns></returns>
        public SchoolDropReasonBO GetschoolDropReasonbyID(int SchooldropreasonID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_SCHOOLDROPBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_SCH_DRP_REASONID", SchooldropreasonID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SchoolDropReasonBO SchoolDropReasonBOobj = null;
            SchoolDropReasonList Users = new SchoolDropReasonList();

            SchoolDropReasonBOobj = new SchoolDropReasonBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "SCH_DRP_REASON") && !dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASON")))
                    SchoolDropReasonBOobj.Schooldropreason = dr.GetString(dr.GetOrdinal("SCH_DRP_REASON"));
                if (ColumnExists(dr, "DESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    SchoolDropReasonBOobj.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (ColumnExists(dr, "SCH_DRP_REASONID") && !dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASONID")))
                    SchoolDropReasonBOobj.SchooldropreasonID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SCH_DRP_REASONID")));

            }
            dr.Close();


            return SchoolDropReasonBOobj;
        }
        
        /// <summary>
        /// To check the Column are Exists or not
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
        //public int Delete(int reasonid)
        //{
        //    OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
        //    conn.Open();
        //    OracleCommand dCmd = new OracleCommand("USP_MST_DELETESCHOOLDROP", conn);
        //    dCmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        dCmd.Parameters.Add("p_SCH_DRP_REASONID", reasonid);
        //        return dCmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        dCmd.Dispose();
        //        conn.Close();
        //        conn.Dispose();

        //    }
        //}

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Delete(int reasonid)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETESCHOOLDROP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_SCH_DRP_REASONID", reasonid);
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
        /// To Update
        /// </summary>
        /// <param name="SchoolDropReasonBOobj"></param>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Update(SchoolDropReasonBO SchoolDropReasonBOobj, int reasonid)
        {
            string returnResult = string.Empty;
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPDATESCHOOLDROP", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_SCH_DRP_REASONID", reasonid);
                dCmd.Parameters.Add("p_SCH_DRP_REASON", SchoolDropReasonBOobj.Schooldropreason);
                dCmd.Parameters.Add("p_description", SchoolDropReasonBOobj.Description);
                dCmd.Parameters.Add("p_CREATEDBY", SchoolDropReasonBOobj.Createdby);
                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                //return dCmd.ExecuteNonQuery();

                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return returnResult;
        }

        //newly added
        /// <summary>
        /// To Obsolete School Drop
        /// </summary>
        /// <param name="reasonid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteSchoolDrop(int reasonid, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETESCHOOLDROP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_SCH_DRP_REASONID", reasonid);
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