using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class FixtureTypeDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
       /// <summary>
       /// to insert data
       /// </summary>
       /// <param name="ObjFixture"></param>
       /// <returns></returns>
        public string AddFixtureType(FixtureTypeBO ObjFixture)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
           // proc = "USP_MST_INS_BANK";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("fixtureType", ObjFixture.FixtureType);

            cmd.Parameters.Add("createdby", ObjFixture.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

           // proc = "USP_MST_UPD_BANK";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("fixturetypeID", ObjFixture.FixtureID);
            cmd.Parameters.Add("fixturetype", ObjFixture.FixtureType);

            cmd.Parameters.Add("updatedby", ObjFixture.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
               // myCommand = new OracleCommand("USP_MST_DEL_BANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("fixtureID_", fixtureID);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                //myCommand = new OracleCommand("USP_MST_OBSOLETEBANK", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("fixtureID_", fixtureID);
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
       /// to fetch details
       /// </summary>
       /// <param name="fixturetype"></param>
       /// <returns></returns>
        public FixtureTypeList GetAllFixtureType(string fixturetype)
        {
            proc = "USP_MST_GET_ALLBANKS";
            FixtureTypeBO ObjFixture = null;
            FixtureTypeList fixtureList = new FixtureTypeList();

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (fixturetype != "")
                        cmd.Parameters.Add("fixturetype_", fixturetype);
                    else
                        cmd.Parameters.Add("fixturetype_", DBNull.Value);


                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
