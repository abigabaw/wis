using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
   public class PstatusDAL
    {
       /// <summary>
        /// To Insert
       /// </summary>
       /// <param name="objPstatus"></param>
       /// <returns></returns>
       public string Insert(PstatusBO objPstatus)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_PSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("PAPDESIGNATION_", objPstatus.PAPDESIGNATION1);
                dcmd.Parameters.Add("CREATEDBY", objPstatus.CREATEDBY1);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

                return returnResult;

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
        }

        //get all data in mst_Concern table using USP_MST_GET_PSTATUS-SP
       /// <summary>
       /// To Get Pap's status
       /// </summary>
       /// <returns></returns>
        public object GetPstatus()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_PSTATUS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PstatusBO objPstatus = null;
            PstatusList Pstatus = new PstatusList();

            while (dr.Read())
            {
                objPstatus = new PstatusBO();
                objPstatus.PAPDESIGNATIONID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDESIGNATIONID")));
                objPstatus.PAPDESIGNATION1 = dr.GetString(dr.GetOrdinal("PAPDESIGNATION"));
                objPstatus.ISDELETED1 = dr.GetString(dr.GetOrdinal("ISDELETED"));

                Pstatus.Add(objPstatus);
            }

            dr.Close();

            return Pstatus;
        }

       /// <summary>
        /// To Get All Pap's status
       /// </summary>
       /// <param name="PapStatus"></param>
       /// <returns></returns>
        public object GetAllPstatus(string PapStatus)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_ALL_PAPSTATUS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (PapStatus.ToString() == "")
            {
                cmd.Parameters.Add("@PAPstatus_", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@PAPstatus_", PapStatus.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PstatusBO objPstatus = null;
            PstatusList Pstatus = new PstatusList();

            while (dr.Read())
            {
                objPstatus = new PstatusBO();
                objPstatus.PAPDESIGNATIONID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDESIGNATIONID")));
                objPstatus.PAPDESIGNATION1 = dr.GetString(dr.GetOrdinal("PAPDESIGNATION"));
                objPstatus.ISDELETED1 = dr.GetString(dr.GetOrdinal("ISDELETED"));

                Pstatus.Add(objPstatus);
            }

            dr.Close();

            return Pstatus;
        }


        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID
       /// <summary>
        /// To Get Pap's status by ID
       /// </summary>
       /// <param name="PAPDESIGNATIONID"></param>
       /// <returns></returns>
        public PstatusBO GetPstatusById(int PAPDESIGNATIONID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_PSTATUS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_papdesignationid", PAPDESIGNATIONID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PstatusBO objPstatus = null;
            PstatusList Users = new PstatusList();

            objPstatus = new PstatusBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "PAPDESIGNATION") && !dr.IsDBNull(dr.GetOrdinal("PAPDESIGNATION")))
                    objPstatus.PAPDESIGNATION1 = dr.GetString(dr.GetOrdinal("PAPDESIGNATION"));
                if (ColumnExists(dr, "PAPDESIGNATIONID") && !dr.IsDBNull(dr.GetOrdinal("PAPDESIGNATIONID")))
                    objPstatus.PAPDESIGNATIONID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDESIGNATIONID")));

            }
            dr.Close();


            return objPstatus;
        }

       /// <summary>
       /// To Check Weather Column Exists or Not
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
        // to check the Column are Exists or not

       /// <summary>
        /// To EDIT Pap status
       /// </summary>
       /// <param name="objPstatus"></param>
       /// <returns></returns>
        public string EDITPstatus(PstatusBO objPstatus)
        {
            string returnResult = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_PSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("p_papdesignation", objPstatus.PAPDESIGNATION1);
                dcmd.Parameters.Add("p_papdesignationid", objPstatus.PAPDESIGNATIONID1);
                dcmd.Parameters.Add("UpdatedBY", objPstatus.CREATEDBY1);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

               return returnResult;
                             
             
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
        }

     /// <summary>
     /// To Delete Pap Status
     /// </summary>
     /// <param name="PAPDESIGNATIONID"></param>
     /// <returns></returns>
        public string DeletePstatus(int PAPDESIGNATIONID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_PSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("p_papdesignationid", PAPDESIGNATIONID);
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

       /// <summary>
        /// To Obsolete PAP Status
       /// </summary>
       /// <param name="papStatusID"></param>
       /// <param name="IsDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
        public string ObsoletePAPStatus(int papStatusID, string IsDeleted, int updatedBy)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = "";
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_PSTATUS", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("papdesignationid_", papStatusID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("updatedBy_", updatedBy);
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
        /// To Get All PAP Status
       /// </summary>
       /// <returns></returns>
        public object GetAllPAPStatus()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_ALLPSTATUS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PstatusBO objPstatus = null;
            PstatusList Pstatus = new PstatusList();

            while (dr.Read())
            {
                objPstatus = new PstatusBO();
                objPstatus.PAPDESIGNATIONID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDESIGNATIONID")));
                objPstatus.PAPDESIGNATION1 = dr.GetString(dr.GetOrdinal("PAPDESIGNATION"));
                objPstatus.ISDELETED1 = dr.GetString(dr.GetOrdinal("ISDELETED"));

                Pstatus.Add(objPstatus);
            }

            dr.Close();

            return Pstatus;
        }
    }
}