using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class DwellingDAL
    {
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="DwellingBOobj"></param>
        /// <returns></returns>
        public string Insert(DwellingBO DwellingBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTDWELLING", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("p_DWELLINGTYPE", DwellingBOobj.DwellingType);
                dcmd.Parameters.Add("p_CREATEDBY", DwellingBOobj.Createdby);

                //return dcmd.ExecuteNonQuery();
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

               
            }
            catch(Exception ex)
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
        /// To fetch details
        /// </summary>
        /// <returns></returns>
        public DwellingList GetAllDwelling()
        {
            // used in Master page
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GETALLDWELLING";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DwellingBO objDwelling = null;
            DwellingList Dwellings = new DwellingList();

            while (dr.Read())
            {
                objDwelling = new DwellingBO();
                objDwelling.DwellingID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("dwellingid")));
                objDwelling.DwellingType = dr.GetString(dr.GetOrdinal("dwellingtype"));
                objDwelling.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                Dwellings.Add(objDwelling);
            }

            dr.Close();

            return Dwellings;
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <returns></returns>
        public DwellingList GetDwelling()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETDWELLING";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DwellingBO objUser = null;
            DwellingList Users = new DwellingList();

            while (dr.Read())
            {
                objUser = new DwellingBO();
                objUser.DwellingID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("dwellingid")));
                objUser.DwellingType = dr.GetString(dr.GetOrdinal("dwellingtype"));
              
                Users.Add(objUser);
            }

            dr.Close();

            return Users;
        }
        /// <summary>
        /// To fetch details by ID
        /// </summary>
        /// <param name="DwellingID"></param>
        /// <returns></returns>
        public DwellingBO GetDwellingById(int DwellingID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETDWELLINGBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_DWELLINGID", DwellingID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DwellingBO DwellingBOobj = null;
            DwellingList Users = new DwellingList();

            DwellingBOobj = new DwellingBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "dwellingtype") && !dr.IsDBNull(dr.GetOrdinal("dwellingtype")))
                    DwellingBOobj.DwellingType = dr.GetString(dr.GetOrdinal("dwellingtype"));
                if (ColumnExists(dr, "dwellingid") && !dr.IsDBNull(dr.GetOrdinal("dwellingid")))
                    DwellingBOobj.DwellingID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("dwellingid")));

            }
            dr.Close();


            return DwellingBOobj;
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
        /// To delete data from database
        /// </summary>
        /// <param name="DwellingID"></param>
        /// <returns></returns>
        public string DeleteDwelling(int DwellingID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEDWELLING", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_DWELLINGID", DwellingID);
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
        /// To make data obsolete
        /// </summary>
        /// <param name="DwellingID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteDwelling(int DwellingID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_DWELLING", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("DWELLINGID_", DwellingID);
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

        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="DwellingBOobj"></param>
        /// <param name="DwellingID"></param>
        /// <returns></returns>

        public string Update(DwellingBO DwellingBOobj, int DwellingID)
        {
            string returnResult = string.Empty;

            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPDATEDWELLING", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_SCH_DRP_REASONID", DwellingID);
                dCmd.Parameters.Add("p_SCH_DRP_REASON", DwellingBOobj.DwellingType);
                dCmd.Parameters.Add("p_CREATEDBY", DwellingBOobj.Createdby);
                //return dCmd.ExecuteNonQuery();

                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
    }
}