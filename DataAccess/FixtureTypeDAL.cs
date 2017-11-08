using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class FixtureTypeDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
       /// <summary>
       /// to insert data
       /// </summary>
       /// <param name="ObjFixture"></param>
       /// <returns></returns>
        public string AddFixtureType(FixtureTypeBO ObjFixture)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
           // proc = "USP_MST_INS_BANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("fixtureType", ObjFixture.FixtureType);

            cmd.Parameters.AddWithValue("createdby", ObjFixture.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
       /// <summary>
       /// to update data
       /// </summary>
       /// <param name="ObjFixture"></param>
       /// <returns></returns>
        public string UpdateFixtureType(FixtureTypeBO ObjFixture)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;

           // proc = "USP_MST_UPD_BANK";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("fixturetypeID", ObjFixture.FixtureID);
            cmd.Parameters.AddWithValue("fixturetype", ObjFixture.FixtureType);

            cmd.Parameters.AddWithValue("updatedby", ObjFixture.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
       /// <summary>
       /// to delete data
       /// </summary>
       /// <param name="fixtureID"></param>
       /// <returns></returns>
        public string DeleteFixtureType(int fixtureID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               // myCommand = new SqlCommand("USP_MST_DEL_BANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("fixtureID_", fixtureID);
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
                    result = "Selected Fixture Type is already in use. Cannot delete";
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
       /// to make data obsolete
       /// </summary>
       /// <param name="fixtureID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteFixtureType(int fixtureID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                //myCommand = new SqlCommand("USP_MST_OBSOLETEBANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("fixtureID_", fixtureID);
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
       /// <summary>
       /// to fetch details
       /// </summary>
       /// <param name="fixturetype"></param>
       /// <returns></returns>
        public FixtureTypeList GetAllFixtureType(string fixturetype)
        {
            proc = "USP_MST_GET_ALLBANKS";
            FixtureTypeBO ObjFixture = null;
            FixtureTypeList fixtureList = new FixtureTypeList();

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (fixturetype != "")
                        cmd.Parameters.AddWithValue("fixturetype_", fixturetype);
                    else
                        cmd.Parameters.AddWithValue("fixturetype_", DBNull.Value);


                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            ObjFixture = new FixtureTypeBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("FixtureTypeID"))) ObjFixture.FixtureID = dr.GetInt32(dr.GetOrdinal("FixtureTypeID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("FixtureType"))) ObjFixture.FixtureType = dr.GetString(dr.GetOrdinal("FixtureType"));
                            if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) ObjFixture.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                            fixtureList.Add(ObjFixture);
                            
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return fixtureList;
        }
    }
   
}
