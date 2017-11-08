using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_INSERTINTOCLANSMASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.AddWithValue("TRbID", ClansBOObj.TRIBEID);
                cmd.Parameters.AddWithValue("ClansName", ClansBOObj.CLANNAME);
                cmd.Parameters.AddWithValue("CretBy", ClansBOObj.CreatedBy);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLCLANSDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("TRIBEID_", tribeID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETCLANSDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("TRIBEID_", tribeID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GetClansByID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ClansID", ClansID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETECLANSBYID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ClansID_", ClansID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_EDITCLANS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("ClansID", ClansBOObj.CLANID);
                dcmd.Parameters.AddWithValue("ClName", ClansBOObj.CLANNAME);
                dcmd.Parameters.AddWithValue("UpBy", ClansBOObj.UpdatedBy);

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_CLANS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ClansID_", ClansID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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