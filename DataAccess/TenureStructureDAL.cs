using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TenureStructureDAL
    {
        /// <summary>
        /// To Get Tenure Structures
        /// </summary>
        /// <param name="TenureStructureName"></param>
        /// <returns></returns>
        public TenureStructureList GetTenureStructures(string TenureStructureName)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_TENURESTRUCTURES";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureStructureName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@TenureStructureNameIN",  DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TenureStructureNameIN", TenureStructureName.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureStructureBO objTenureStructre = null;
            TenureStructureList TenureStructure = new TenureStructureList();
            while (dr.Read())
            {
                objTenureStructre = new TenureStructureBO();
                objTenureStructre.Str_TenureId =  Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STR_TENUREID")));
                objTenureStructre.Str_Tenure = dr.GetString(dr.GetOrdinal("STR_TENURE"));
                objTenureStructre.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                TenureStructure.Add(objTenureStructre);
            }
            dr.Close();
            return TenureStructure;
        }

        /// <summary>
        /// To Get All Tenure Structures
        /// </summary>
        /// <param name="TenureStructureName"></param>
        /// <returns></returns>
        public TenureStructureList GetAllTenureStructures(string TenureStructureName)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ALTENURESTRUCTURES";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureStructureName.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@TenureStructureNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TenureStructureNameIN", TenureStructureName.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureStructureBO objTenureStructre = null;
            TenureStructureList TenureStructure = new TenureStructureList();
            while (dr.Read())
            {
                objTenureStructre = new TenureStructureBO();
                objTenureStructre.Str_TenureId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STR_TENUREID")));
                objTenureStructre.Str_Tenure = dr.GetString(dr.GetOrdinal("STR_TENURE"));
                objTenureStructre.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                TenureStructure.Add(objTenureStructre);
            }
            dr.Close();
            return TenureStructure;
        }

        /// <summary>
        /// To Add Tenure Structure
        /// </summary>
        /// <param name="objTenureStructure"></param>
        /// <returns></returns>
        public string AddTenureStructure(TenureStructureBO objTenureStructure)
        {
            string result = "";
            
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_INS_TENURESTRUCTURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@TenureStructureNameIN", objTenureStructure.Str_Tenure);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objTenureStructure.CreatedBy);
            myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            myConnection.Close();
            
            return result;
        }


        //public int DeleteTenureStructure(int TenureStructureId)
        //{
        //    int result = 0;
        //    {
        //        SqlConnection myConnection;
        //        SqlCommand myCommand;
        //        myConnection = new SqlConnection(AppConfiguration.ConnectionString);
        //        myCommand = new SqlCommand("USP_MST_DEL_TENURESTRUCTURE", myConnection);
        //        myCommand.Connection = myConnection;
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        myCommand.Parameters.AddWithValue("@TenureStructureIDIN",TenureStructureId);
        //        myConnection.Open();
        //        result = myCommand.ExecuteNonQuery();
        //        myConnection.Close();
        //    }
        //    return result;
        //}

        /// <summary>
        /// To Delete Tenure Structure
        /// </summary>
        /// <param name="TenureStructureId"></param>
        /// <returns></returns>
        public string DeleteTenureStructure(int TenureStructureId)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_TENURESTRUCTURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TenureStructureIDIN", TenureStructureId);
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

        //To Obsolete Tenure Structure
        public string ObsoleteTenureStructure(int TenureStructureId, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBS_TENURESTRUCTURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("TenureStructureIDIN", TenureStructureId);
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
        /// To Update Tenure Structure
        /// </summary>
        /// <param name="objTenureStructure"></param>
        /// <returns></returns>
        public string UpdateTenureStructure(TenureStructureBO objTenureStructure)
        {
            string result = "";
            
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_MST_UPD_TENURESTRUCTURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@TenureStructureIDIN", objTenureStructure.Str_TenureId);
            myCommand.Parameters.AddWithValue("@TenureStructureNameIN", objTenureStructure.Str_Tenure);
            myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("@USERIDIN", objTenureStructure.UpdatedBy);
            myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            myConnection.Close();
            
            return result;
        }

        /// <summary>
        /// To Get Tenure Structure Item
        /// </summary>
        /// <param name="TenureStructureID"></param>
        /// <returns></returns>
        public TenureStructureBO GetTenureStructureItem(int TenureStructureID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_STRTNRBYSTRTNRID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TenureStructureIDIN", TenureStructureID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureStructureBO obTenureStructure = null;
            while (dr.Read())
            {
                obTenureStructure = new TenureStructureBO();
                obTenureStructure.Str_TenureId = dr.GetInt32(dr.GetOrdinal("STR_TENUREID"));
                obTenureStructure.Str_Tenure = dr.GetString(dr.GetOrdinal("STR_TENURE"));
            }
            dr.Close();
            return obTenureStructure;
        }  
    }
}

