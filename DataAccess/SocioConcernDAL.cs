using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConcernDAL
    {
        /// <summary>
        /// To Insert Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string InsertSocioConcern(SocioConcernBO objSocioConcern)
        {
            string result = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_INS_TRN_CONCERN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("ConcernID_", objSocioConcern.ConcernID);
                dcmd.Parameters.Add("OtherConcern_", objSocioConcern.OtherConcern);
                dcmd.Parameters.Add("CREATEDBY", objSocioConcern.UserID);
                dcmd.Parameters.Add("HHID_", objSocioConcern.HHID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();
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

            return result;
        }

        /// <summary>
        /// To get Socio Concern
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernList getSocioConcern(int HHID)
        {
            // used in Master page
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_TRN_GETSOCIALCONCERNS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SocioConcernBO objSocioConcern = null;
            SocioConcernList SocioConcernList = new SocioConcernList();

            while (dr.Read())
            {
                objSocioConcern = new SocioConcernBO();

                if (!dr.IsDBNull(dr.GetOrdinal("PAP_CONCERNID"))) objSocioConcern.PapConcernID = dr.GetInt32(dr.GetOrdinal("PAP_CONCERNID"));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objSocioConcern.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONCERNID"))) objSocioConcern.ConcernID = dr.GetInt32(dr.GetOrdinal("CONCERNID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONCERN"))) objSocioConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                if (!dr.IsDBNull(dr.GetOrdinal("OTHERCONCERN"))) objSocioConcern.OtherConcern = dr.GetString(dr.GetOrdinal("OTHERCONCERN"));

                SocioConcernList.Add(objSocioConcern);
            }

            dr.Close();

            return SocioConcernList;
        }

        /// <summary>
        /// To Get Socio Concern By Id
        /// </summary>
        /// <param name="PapConcernID"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernBO GetSocioConcernById(int PapConcernID, int HHID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_SOC_CONCERN_ID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PapConcernID_", PapConcernID);
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SocioConcernBO SocioConcernObj = null;
            SocioConcernList SocioConcernList = new SocioConcernList();

            SocioConcernObj = new SocioConcernBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "OTHERCONCERN") && !dr.IsDBNull(dr.GetOrdinal("OTHERCONCERN")))
                    SocioConcernObj.OtherConcern = dr.GetString(dr.GetOrdinal("OTHERCONCERN"));
                if (ColumnExists(dr, "PAP_CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("PAP_CONCERNID")))
                    SocioConcernObj.PapConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_CONCERNID")));
                if (ColumnExists(dr, "CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("CONCERNID")))
                    SocioConcernObj.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));

            }
            dr.Close();
            return SocioConcernObj;
        }

        // To check the Column are Exists or not
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
        /// To Edit Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string EditSocioConcern(SocioConcernBO objSocioConcern)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPDATE_SOCIAL_CONCER", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("PapConcernID_", objSocioConcern.PapConcernID);
                dcmd.Parameters.Add("HHID_", objSocioConcern.HHID);
                dcmd.Parameters.Add("ConcernID_", objSocioConcern.ConcernID);
                dcmd.Parameters.Add("OtherConcern_", objSocioConcern.OtherConcern);
                dcmd.Parameters.Add("UpdatedBY", objSocioConcern.UserID);
               
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

        //Delete the data in mst_Concern table using USP_MST_DELETECONCERN-SP signal Data based on ID
        public string DeleteSocialConcern(int PapConcernID, int HHID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_DELETESOCIALCONCERN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("PapConcernID_", PapConcernID);
                myCommand.Parameters.Add("HHID_", HHID);
                //myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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