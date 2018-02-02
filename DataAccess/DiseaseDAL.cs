using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLDISEASE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_DISEASES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("DiseaseNameIN", DiseaseName);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_INSERTDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DiseaseNameIN", objDisease.DiseaseName);
                myCommand.Parameters.AddWithValue("@ISDELETEDIN",  "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objDisease.CreatedBy);
                myConnection.Open();
                //result = myCommand.ExecuteNonQuery();
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("DiseaseIDIN", DiseaseID);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_UPDATEDISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DISEASEIDIN",  objDisease.DiseaseID);
                myCommand.Parameters.AddWithValue("@DISEASENAMEIN", objDisease.DiseaseName);
                myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objDisease.UpdatedBy);                
                myConnection.Open();
            //result = myCommand.ExecuteNonQuery();
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/
            SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar);
            outputValue.Size=200;
            outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;            
            string proc = "USP_MST_GETDISEASEBYDISEASEID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DiseaseIdIN", diseaseID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DiseaseBO obDisease = null;
            while (dr.Read())
            {
                obDisease = new DiseaseBO();
                obDisease.DiseaseID = (int)dr.GetDecimal(dr.GetOrdinal("DiseaseId"));
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_DISEASE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("DISEASEID_", DiSEASEID);
                myCommand.Parameters.AddWithValue("@isdeleted_", IsDeleted);
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

    }
}

