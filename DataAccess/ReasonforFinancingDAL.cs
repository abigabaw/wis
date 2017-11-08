using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_REASON_FIN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("F_FINANCEREASON", BOobj.FINANCEREASON);
                dcmd.Parameters.AddWithValue("CREATEDBY", BOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_OBSOLETE_REASON_FIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCEREASONID", ReasonForFinanceId);
                myCommand.Parameters.AddWithValue("@isdeleted_", ISDELETED);
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
      /// To Get All Reason For Finance
      /// </summary>
      /// <param name="financereason"></param>
      /// <returns></returns>
      public ReasonforFinancingList GetAllReasonForFinance(string financereason)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_ALL_FIN_REASON ";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (financereason.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FINANCEREASON_", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FINANCEREASON_", financereason.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;

          string proc = "USP_MST_GET_REASON_FIN";

          cmd = new SqlCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

          cmd.Connection.Open();
          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETDETAIL_REASN_FIN";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("F_FINANCEREASONID", ReasonForFinanceID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_REASON_FIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCEREASONID", ReasonForFinanceID);
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
      /// To Update
      /// </summary>
      /// <param name="BOobj"></param>
      /// <returns></returns>
      public string Update(ReasonforFinancingBO BOobj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_REASON_FIN", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("F_FINANCEREASONID", BOobj.FINANCEREASONID);
                dcmd.Parameters.AddWithValue("F_FINANCEREASON", BOobj.FINANCEREASON);
                dcmd.Parameters.AddWithValue("F_UPDATEDBY", BOobj.CREATEDBY);
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
    }
}
