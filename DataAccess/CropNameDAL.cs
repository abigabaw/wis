using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CropNameDAL
    {
        /// <summary>
        /// To get details
        /// </summary>
        /// <returns></returns>
        public object GetAllCropNameDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALLCROPNAMES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                       
            CropNameBO CropNameBOObj = null;
            CropNameList CropNameListObj = new CropNameList();
            CropNameBOObj = new CropNameBO();

            while (dr.Read())
            {
                CropNameBOObj = new CropNameBO();
                CropNameBOObj.CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID")));
                CropNameBOObj.CropName = dr.GetString(dr.GetOrdinal("CROPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("UNITNAME"))) CropNameBOObj.UnitName = dr.GetString(dr.GetOrdinal("UNITNAME"));
                CropNameBOObj.IsDeletedBy = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropNameListObj.Add(CropNameBOObj);
            }
            dr.Close();
            return CropNameListObj;
        }
        /// <summary>
        /// To get details
        /// </summary>
        /// <returns></returns>
        public object GetCropNameDetails()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_CROPNAME";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            CropNameBO CropNameBOObj = null;
            CropNameList CropNameListObj = new CropNameList();
            CropNameBOObj = new CropNameBO();

            while (dr.Read())
            {
                CropNameBOObj = new CropNameBO();
                CropNameBOObj.CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID")));
                CropNameBOObj.CropName = dr.GetString(dr.GetOrdinal("CROPNAME"));
                CropNameBOObj.IsDeletedBy = dr.GetString(dr.GetOrdinal("ISDELETED"));
                CropNameListObj.Add(CropNameBOObj);
            }
            dr.Close();
            return CropNameListObj;
        }
        /// <summary>
        /// To save data to database
        /// </summary>
        /// <param name="CropNameBOObj"></param>
        /// <returns></returns>
        public string InsertCropNameDetails(CropNameBO CropNameBOObj)
        {
            string result = "";
            SqlConnection Con = new SqlConnection(AppConfiguration.ConnectionString);
            Con.Open();
            SqlCommand cmd = new SqlCommand("USP_MST_INSERT_CROPNAME", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);

            try
            {
                cmd.Parameters.AddWithValue("C_CROPNAME", CropNameBOObj.CropName);
                cmd.Parameters.AddWithValue("C_UnitId", CropNameBOObj.UnitMeasure);
                cmd.Parameters.AddWithValue("C_CREATEDBY", CropNameBOObj.CreatedBy);
                cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
                
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }

            return result;
        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="CropNameBOObj"></param>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public string UpdateCropNameDetails(CropNameBO CropNameBOObj, int CROPID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_CROPNAME", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.AddWithValue("C_CROPID", CROPID);
                dcmd.Parameters.AddWithValue("C_CROPNAME", CropNameBOObj.CropName);
                dcmd.Parameters.AddWithValue("C_UnitId", CropNameBOObj.UnitMeasure);
                dcmd.Parameters.AddWithValue("C_UPDATEDBY", CropNameBOObj.CreatedBy);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
        /// To get details by ID
        /// </summary>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public CropNameBO GetCropNameById(int CROPID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_CROPNAME ";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("C_CROPID", CROPID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);                      
           
            CropNameBO CropNameBOObj = null;
            CropNameList CropNameListObj = new CropNameList();
            CropNameBOObj = new CropNameBO();

            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("CROPID")))
                    CropNameBOObj.CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPNAME")))
                    CropNameBOObj.CropName = dr.GetString(dr.GetOrdinal("CROPNAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("UNITNAME")))
                    CropNameBOObj.UnitName = dr.GetString(dr.GetOrdinal("UNITNAME"));
            }
            dr.Close();

            return CropNameBOObj;
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

        //public int DeleteCropTypeRow(int CROPID)
        //{
        //    SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
        //    SqlCommand cmd;

        //    string proc = "USP_MST_DEL_CROPNAME";

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("C_CROPID", CROPID);
        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();
        //    return result;
        //}
        /// <summary>
        /// to delete details from database
        /// </summary>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public string DeleteCropTypeRow(int CROPID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_CROPNAME", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("C_CROPID", CROPID);
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
                    result = "Selected Role is already in use. Connot delete";
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
        /// <param name="CROPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropName(int CROPID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETECROPNAME", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("C_CROPID", CROPID);
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