using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class DiseaseDAL
    {
        /// <summary>
        /// To fethc all details from database
        /// </summary>
        /// <param name="DiseaseName"></param>
        /// <returns></returns>
        public DiseaseList GetALLDisease(string DiseaseName)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALLDISEASE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DiseaseBO objDisease = null;
            DiseaseList Diseases = new DiseaseList();

            while (dr.Read())
            {
                objDisease = new DiseaseBO();
                objDisease.DiseaseID =  Convert.ToInt32(dr.GetValue(dr.GetOrdinal("diseaseid")));
                objDisease.DiseaseName = dr.GetString(dr.GetOrdinal("diseasename"));
                objDisease.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                Diseases.Add(objDisease);
            }

            dr.Close();

            return Diseases;

        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="DiseaseName"></param>
        /// <returns></returns>

        public DiseaseList SearchDisease(string DiseaseName)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_DISEASES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DiseaseNameIN", DiseaseName);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DiseaseBO objDisease = null;
           DiseaseList Diseases = new DiseaseList();

            while (dr.Read())
            {
                objDisease = new DiseaseBO();
                objDisease.DiseaseID =Convert.ToInt32(dr.GetValue(dr.GetOrdinal("diseaseid")));
                objDisease.DiseaseName = dr.GetString(dr.GetOrdinal("diseasename"));
                objDisease.Isdeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                Diseases.Add(objDisease);
            }

            dr.Close();

            return Diseases;
   
        }
        /// <summary>
        /// to insert details to datbase
        /// </summary>
        /// <param name="objDisease"></param>
        /// <returns></returns>
        public string AddDisease(DiseaseBO objDisease)
        {
            string returnResult = string.Empty;
            int result = 0;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_INSERTDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@DiseaseNameIN", objDisease.DiseaseName);
                myCommand.Parameters.Add("@ISDELETEDIN",  "False");
                myCommand.Parameters.Add("@USERIDIN", objDisease.CreatedBy);
                myConnection.Open();
                //result = myCommand.ExecuteNonQuery();
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    returnResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

                myConnection.Close();
            }
            return returnResult;
        }

        /// <summary>
        /// to delete data from databse
        /// </summary>
        /// <param name="DiseaseID"></param>
        /// <returns></returns>
        public string DeleteDisease(int DiseaseID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("DiseaseIDIN", DiseaseID);
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
        /// to update details to database
        /// </summary>
        /// <param name="objDisease"></param>
        /// <returns></returns>
        public string UpdateDisease(DiseaseBO objDisease)
        {
            string returnResult = string.Empty;

                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_UPDATEDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@DISEASEIDIN",  objDisease.DiseaseID);
                myCommand.Parameters.Add("@DISEASENAMEIN", objDisease.DiseaseName);
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objDisease.UpdatedBy);                
                myConnection.Open();
                //result = myCommand.ExecuteNonQuery();
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    returnResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
                myConnection.Close();

            return returnResult;
        }
        /// <summary>
        /// to fetch details based on ID
        /// </summary>
        /// <param name="diseaseID"></param>
        /// <returns></returns>
        public DiseaseBO GetDiseaseByDiseaseID(int diseaseID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;            
            string proc = "USP_MST_GETDISEASEBYDISEASEID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DiseaseIdIN", diseaseID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DiseaseBO obDisease = null;
            while (dr.Read())
            {
                obDisease = new DiseaseBO();
                obDisease.DiseaseID = dr.GetInt32(dr.GetOrdinal("DiseaseId"));
                obDisease.DiseaseName = dr.GetString(dr.GetOrdinal("DiseaseName"));                
            }
            dr.Close();
            return obDisease;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="DiSEASEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteDisease(int DiSEASEID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_DISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("DISEASEID_", DiSEASEID);
                myCommand.Parameters.Add("@isdeleted_", IsDeleted);
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

