using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_TENURESTRUCTURES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureStructureName.ToString() == "")
            {
                cmd.Parameters.Add("@TenureStructureNameIN",  DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@TenureStructureNameIN", TenureStructureName.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ALTENURESTRUCTURES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (TenureStructureName.ToString() == "")
            {
                cmd.Parameters.Add("@TenureStructureNameIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@TenureStructureNameIN", TenureStructureName.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_INS_TENURESTRUCTURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@TenureStructureNameIN", objTenureStructure.Str_Tenure);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objTenureStructure.CreatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
        //        OracleConnection myConnection;
        //        OracleCommand myCommand;
        //        myConnection = new OracleConnection(AppConfiguration.ConnectionString);
        //        myCommand = new OracleCommand("USP_MST_DEL_TENURESTRUCTURE", myConnection);
        //        myCommand.Connection = myConnection;
        //        myCommand.CommandType = CommandType.StoredProcedure;
        //        myCommand.Parameters.Add("@TenureStructureIDIN",TenureStructureId);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_TENURESTRUCTURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("TenureStructureIDIN", TenureStructureId);
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBS_TENURESTRUCTURE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("TenureStructureIDIN", TenureStructureId);
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
        /// To Update Tenure Structure
        /// </summary>
        /// <param name="objTenureStructure"></param>
        /// <returns></returns>
        public string UpdateTenureStructure(TenureStructureBO objTenureStructure)
        {
            string result = "";
            
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_MST_UPD_TENURESTRUCTURE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@TenureStructureIDIN", objTenureStructure.Str_TenureId);
            myCommand.Parameters.Add("@TenureStructureNameIN", objTenureStructure.Str_Tenure);
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", objTenureStructure.UpdatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_STRTNRBYSTRTNRID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenureStructureIDIN", TenureStructureID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

