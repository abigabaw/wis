using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConcerDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 

        //get all data in mst_Concern table using USP_MST_SELECTCONCERN-SP
        public ConcernList GetConcern()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SELECTCONCERN";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConcernBO objConcern = null;
            ConcernList Concern = new ConcernList();

            while (dr.Read())
            {
                objConcern = new ConcernBO();
                objConcern.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));
                objConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                objConcern.Isdeleted = dr.GetString(dr.GetOrdinal("Isdeleted"));

                Concern.Add(objConcern);
            }

            dr.Close();

            return Concern;
        }
        
       
        /// <summary>
        /// save the data
        /// </summary>
        /// <param name="objConcern"></param>
        /// <returns></returns>
        public string Insert(ConcernBO objConcern)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTCONCERN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("CONCERN", objConcern.ConcernName);
                dcmd.Parameters.AddWithValue("CREATEDBY", objConcern.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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

        /// <summary>
        /// To GET ALL CONCERN
        /// </summary>
        /// <returns></returns>
        public ConcernList GETALLCONCERN()
        {
            // used in Master page
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALLCONCERN";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConcernBO objConcern = null;
            ConcernList Concern = new ConcernList();

            while (dr.Read())
            {
                objConcern = new ConcernBO();
                objConcern.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));
                objConcern.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                objConcern.Isdeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                Concern.Add(objConcern);
            }

            dr.Close();

            return Concern;
        }


        /// <summary>
        /// get the data based on ID
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <returns></returns>
        public ConcernBO GetConcernById(int ConcernID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETSELECTCONCERN";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ConcernID_", ConcernID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConcernBO ConcernObj = null;
            ConcernList Users = new ConcernList();

            ConcernObj = new ConcernBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "CONCERN") && !dr.IsDBNull(dr.GetOrdinal("CONCERN")))
                    ConcernObj.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                if (ColumnExists(dr, "CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("CONCERNID")))
                    ConcernObj.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));

            }
            dr.Close();


            return ConcernObj;
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
        /// To update data to database
        /// </summary>
        /// <param name="objConcern"></param>
        /// <returns></returns>
        public string EDITCONCERN(ConcernBO objConcern)
        {
            string returnResult = string.Empty;

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATECCONCERN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("Concern_", objConcern.ConcernName);
                dcmd.Parameters.AddWithValue("ConcernID_", objConcern.ConcernID);
                dcmd.Parameters.AddWithValue("UpdatedBY", objConcern.UserID);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        /// <summary>
        /// TO delete data
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <returns></returns>
        public string DeleteConcern(int ConcernID)
        {

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETECONCERN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ConcernID_", ConcernID);
                //myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
                    result = "Selected Concern is already in use. Cannot delete";
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

            //SqlConnection myConnection = null;
            //SqlCommand myCommand = null;

            //string result = string.Empty;
            //try
            //{

            //    myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            //    myCommand = new SqlCommand("USP_MST_DELETECONCERN", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.AddWithValue("@ConcernID_", ConcernID);
            //    myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    if (myCommand.Parameters["errorMessage_"].Value != null)
            //        result = myCommand.Parameters["errorMessage_"].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("ORA-02292"))
            //    {
            //        result = "Selected item is already in use. Connot delete";
            //    }
            //    else
            //    {
            //        throw ex;
            //    }
            //}
            //finally
            //{
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}

            //return result;  
            //SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            //SqlCommand cmd;


            //string proc = "USP_MST_DELETECONCERN";

            //cmd = new SqlCommand(proc, cnn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("ConcernID_", ConcernID);
            //cmd.Connection.Open();

            //int result = cmd.ExecuteNonQuery();

            //return result;
        }

        /// <summary>
        /// To Obsolete Concern
        /// </summary>
        /// <param name="ConcernID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
        public string ObsoleteConcern(int ConcernID, string Isdeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_CONCERN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ConcernId_", ConcernID);
                myCommand.Parameters.AddWithValue("isdeleted_", Isdeleted);
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