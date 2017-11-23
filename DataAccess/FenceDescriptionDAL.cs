using System;
using System.Data;
using System.Data.SqlClient;
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

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_FENCE", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("p_FENCEDESCRIPTION", FenceDescriptionBOobj.FenceDescription);
                dcmd.Parameters.AddWithValue("p_CREATEDBY", FenceDescriptionBOobj.Createdby);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALLFENCE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_FENCE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_FENCE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("fenceid_", FloorTypeID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
            string result = string.Empty;
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_DEL_FENCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.AddWithValue("p_fenceid", FenceID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_FENCEBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_fenceid", FenceID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
            conn.Open();
            SqlCommand dCmd = new SqlCommand("USP_MST_UPD_FENCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.AddWithValue("p_FENCEID", FenceID);
                dCmd.Parameters.AddWithValue("p_FENCEDESCRIPTION", FenceDescriptionBOobj.FenceDescription);
                dCmd.Parameters.AddWithValue("p_UPDATEDBY", FenceDescriptionBOobj.Createdby);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dCmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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