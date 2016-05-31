using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
  public  class ReasonforFinancingDAL
    {
      /// <summary>
      /// To Insert
      /// </summary>
      /// <param name="BOobj"></param>
      /// <returns></returns>
      public string Insert(ReasonforFinancingBO BOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_REASON_FIN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("F_FINANCEREASON", BOobj.FINANCEREASON);
                dcmd.Parameters.Add("CREATEDBY", BOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception errormsg)
            {
                throw errormsg;
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
      /// To Obsolete Reason For Fin
      /// </summary>
      /// <param name="ReasonForFinanceId"></param>
      /// <param name="ISDELETED"></param>
      /// <returns></returns>
      public string ObsoleteReasonForFin(int ReasonForFinanceId, string ISDELETED)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_OBSOLETE_REASON_FIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@F_FINANCEREASONID", ReasonForFinanceId);
                myCommand.Parameters.Add("@isdeleted_", ISDELETED);
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
      /// To Get All Reason For Finance
      /// </summary>
      /// <param name="financereason"></param>
      /// <returns></returns>
      public ReasonforFinancingList GetAllReasonForFinance(string financereason)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "  USP_MST_GET_ALL_FIN_REASON ";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (financereason.ToString() == "")
            {
                cmd.Parameters.Add("@FINANCEREASON_", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@FINANCEREASON_", financereason.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReasonforFinancingBO Boobj = null;
            ReasonforFinancingList Listobj = new ReasonforFinancingList();

            while (dr.Read())
            {
                Boobj = new ReasonforFinancingBO();
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASONID")))
                    Boobj.FINANCEREASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCEREASONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASON")))
                    Boobj.FINANCEREASON = dr.GetString(dr.GetOrdinal("FINANCEREASON"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                Listobj.Add(Boobj);
            }

            dr.Close();

            return Listobj;
        }

        /// <summary>
        /// To Get Reason For Finance
        /// </summary>
        /// <returns></returns>
      public ReasonforFinancingList GetReasonForFinance()
      {
          OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;

          string proc = " USP_MST_GET_REASON_FIN";

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          ReasonforFinancingBO Boobj = null;
          ReasonforFinancingList Listobj = new ReasonforFinancingList();

          while (dr.Read())
          {
              Boobj = new ReasonforFinancingBO();
              if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASONID")))
                  Boobj.FINANCEREASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCEREASONID")));
              if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASON")))
                  Boobj.FINANCEREASON = dr.GetString(dr.GetOrdinal("FINANCEREASON"));
              if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                  Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

              Listobj.Add(Boobj);
          }

          dr.Close();

          return Listobj;
      }

      /// <summary>
      /// To Get Reason For Finance ID
      /// </summary>
      /// <param name="ReasonForFinanceID"></param>
      /// <returns></returns>
      public ReasonforFinancingBO GetReasonForFinanceID(int ReasonForFinanceID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETDETAIL_REASN_FIN";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("F_FINANCEREASONID", ReasonForFinanceID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReasonforFinancingBO Boobj = null;
            ReasonforFinancingList Listobj = new ReasonforFinancingList();

            Boobj = new ReasonforFinancingBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASON")))
                    Boobj.FINANCEREASON = dr.GetString(dr.GetOrdinal("FINANCEREASON"));
                if (!dr.IsDBNull(dr.GetOrdinal("FINANCEREASONID")))
                    Boobj.FINANCEREASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCEREASONID")));

            }
            dr.Close();


            return Boobj;
        }

        /// <summary>
        /// To Reason For Finance ID
        /// </summary>
        /// <param name="ReasonForFinanceID"></param>
        /// <returns></returns>
      public string ReasonForFinanceID(int ReasonForFinanceID)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_REASON_FIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@F_FINANCEREASONID", ReasonForFinanceID);
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
      /// To Update
      /// </summary>
      /// <param name="BOobj"></param>
      /// <returns></returns>
      public string Update(ReasonforFinancingBO BOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_REASON_FIN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("F_FINANCEREASONID", BOobj.FINANCEREASONID);
                dcmd.Parameters.Add("F_FINANCEREASON", BOobj.FINANCEREASON);
                dcmd.Parameters.Add("F_UPDATEDBY", BOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();
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
    }
}
