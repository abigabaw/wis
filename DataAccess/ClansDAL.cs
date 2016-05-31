using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ClansDAL
    {
        /// <summary>
        /// To Insert Into Clans Master details
        /// </summary>
        /// <param name="ClansBOObj"></param>
        /// <returns></returns>
        public int InsertIntoClansMaster(ClansBO ClansBOObj)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_INSERTINTOCLANSMASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.Add("TRbID", ClansBOObj.TRIBEID);
                cmd.Parameters.Add("ClansName", ClansBOObj.CLANNAME);
                cmd.Parameters.Add("CretBy", ClansBOObj.CreatedBy);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        /// <summary>
        /// To Fetch ALL Clans List from database
        /// </summary>
        /// <param name="tribeID"></param>
        /// <returns></returns>

        public ClansList FetchALLClansList(int tribeID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALLCLANSDETAILS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("TRIBEID_", tribeID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClansBO ClansBOObj = null;
            ClansList ClansListObj = new ClansList();

            try
            {

                while (dr.Read())
                {
                    ClansBOObj = new ClansBO();
                    ClansBOObj.CLANID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CLANID")));
                    ClansBOObj.TRIBEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIBEID")));
                    ClansBOObj.CLANNAME = dr.GetString(dr.GetOrdinal("CLANNAME"));
                    ClansBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    ClansListObj.Add(ClansBOObj);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ClansListObj;
        }
        /// <summary>
        /// To Fetch Clans List from database
        /// </summary>
        /// <param name="tribeID"></param>
        /// <returns></returns>
        public ClansList FetchClansList(int tribeID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETCLANSDETAILS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("TRIBEID_", tribeID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClansBO ClansBOObj = null;
            ClansList ClansListObj = new ClansList();

            try
            {

                while (dr.Read())
                {
                    ClansBOObj = new ClansBO();
                    ClansBOObj.CLANID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CLANID")));
                    ClansBOObj.TRIBEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TRIBEID")));
                    ClansBOObj.CLANNAME = dr.GetString(dr.GetOrdinal("CLANNAME"));

                    ClansListObj.Add(ClansBOObj);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ClansListObj;
        }

        /// <summary>
        /// To Get Clans By Id from database
        /// </summary>
        /// <param name="ClansID"></param>
        /// <returns></returns>
        public ClansBO GetClansById(int ClansID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GetClansByID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ClansID", ClansID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ClansBO ClansBOObj = null;
            ClansList ClansListObj = new ClansList();

            ClansBOObj = new ClansBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CLANNAME") && !dr.IsDBNull(dr.GetOrdinal("CLANNAME")))
                    ClansBOObj.CLANNAME = Convert.ToString(dr.GetValue(dr.GetOrdinal("CLANNAME")));

            }
            dr.Close();


            return ClansBOObj;
        }
        /// <summary>
        /// To check whether column exists
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
        /// To delete clan details from database
        /// </summary>
        /// <param name="ClansID"></param>
        /// <returns></returns>

        public string DeleteClansDetails(int ClansID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETECLANSBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("ClansID_", ClansID);
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
        /// To update clans details to database
        /// </summary>
        /// <param name="ClansBOObj"></param>
        /// <returns></returns>
        public int EDITClans(ClansBO ClansBOObj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_EDITCLANS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("ClansID", ClansBOObj.CLANID);
                dcmd.Parameters.Add("ClName", ClansBOObj.CLANNAME);
                dcmd.Parameters.Add("UpBy", ClansBOObj.UpdatedBy);

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
        /// To make clans details obsolete
        /// </summary>
        /// <param name="ClansID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoleteclan(int ClansID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_CLANS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("ClansID_", ClansID);
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