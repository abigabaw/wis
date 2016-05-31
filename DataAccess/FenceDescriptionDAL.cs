using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class FenceDescriptionDAL
    {
        /// <summary>
        /// to save data to database
        /// </summary>
        /// <param name="FenceDescriptionBOobj"></param>
        /// <returns></returns>
        public string insert(FenceDescriptionBO FenceDescriptionBOobj)
        {
            string returnResult = string.Empty;

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_FENCE", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("p_FENCEDESCRIPTION", FenceDescriptionBOobj.FenceDescription);
                dcmd.Parameters.Add("p_CREATEDBY", FenceDescriptionBOobj.Createdby);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();
                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public FenceDescriptionList GetAllFence()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GET_ALLFENCE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FenceDescriptionBO objUser = null;
            FenceDescriptionList Users = new FenceDescriptionList();

            while (dr.Read())
            {
                objUser = new FenceDescriptionBO();
                objUser.FenceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FENCEID")));
                objUser.FenceDescription = dr.GetString(dr.GetOrdinal("FENCEDESCRIPTION"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objUser.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                Users.Add(objUser);
            }

            dr.Close();

            return Users;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public FenceDescriptionList GetFence()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GET_FENCE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FenceDescriptionBO objUser = null;
            FenceDescriptionList Users = new FenceDescriptionList();

            while (dr.Read())
            {
                objUser = new FenceDescriptionBO();
                objUser.FenceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FENCEID")));
                objUser.FenceDescription = dr.GetString(dr.GetOrdinal("FENCEDESCRIPTION"));

                Users.Add(objUser);
            }

            dr.Close();

            return Users;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFenceDescriptionDAL(int FloorTypeID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_FENCE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("fenceid_", FloorTypeID);
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
        /// To delete data from database
        /// </summary>
        /// <param name="FenceID"></param>
        /// <returns></returns>
        public string Delete(int FenceID)
        {
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            string result = string.Empty;
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_DEL_FENCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_fenceid", FenceID);
                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    result = dCmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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
        }
        /// <summary>
        /// to get details by ID
        /// </summary>
        /// <param name="FenceID"></param>
        /// <returns></returns>
        public FenceDescriptionBO GetFencebyID(int FenceID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_FENCEBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_fenceid", FenceID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FenceDescriptionBO FenceDescriptionBOobj = null;
            FenceDescriptionList Users = new FenceDescriptionList();

            FenceDescriptionBOobj = new FenceDescriptionBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "FENCEDESCRIPTION") && !dr.IsDBNull(dr.GetOrdinal("FENCEDESCRIPTION")))
                    FenceDescriptionBOobj.FenceDescription = dr.GetString(dr.GetOrdinal("FENCEDESCRIPTION"));
                if (ColumnExists(dr, "FENCEID") && !dr.IsDBNull(dr.GetOrdinal("FENCEID")))
                    FenceDescriptionBOobj.FenceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FENCEID")));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    FenceDescriptionBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();


            return FenceDescriptionBOobj;
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
        /// To update details to database
        /// </summary>
        /// <param name="FenceDescriptionBOobj"></param>
        /// <param name="FenceID"></param>
        /// <returns></returns>
        public string Update(FenceDescriptionBO FenceDescriptionBOobj, int FenceID)
        {
            string returnResult = string.Empty;

            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPD_FENCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("p_FENCEID", FenceID);
                dCmd.Parameters.Add("p_FENCEDESCRIPTION", FenceDescriptionBOobj.FenceDescription);
                dCmd.Parameters.Add("p_UPDATEDBY", FenceDescriptionBOobj.Createdby);
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