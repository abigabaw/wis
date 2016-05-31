using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class NatureofFinancingDAL
    {
       /// <summary>
       /// To Insert into Database
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Insert(NatureofFinancingBO BOobj)
       {
           string returnResult = string.Empty;
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_MST_INS_NATUR_FINANCE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("F_FINANCENATURE", BOobj.FINANCENATURE);
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
       /// To Get All Nature Finance
       /// </summary>
       /// <returns></returns>
       public NatureofFinancingList GetAllNatureFinance()
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_GET_ALLNATURFIN";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NatureofFinancingBO Boobj = null;
           NatureofFinancingList Listobj = new NatureofFinancingList();

           while (dr.Read())
           {
               Boobj = new NatureofFinancingBO();
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATUREID")))
                   Boobj.FINANCENATUREID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCENATUREID")));
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATURE")))
                   Boobj.FINANCENATURE = dr.GetString(dr.GetOrdinal("FINANCENATURE"));
               if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                   Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

               Listobj.Add(Boobj);
           }

           dr.Close();

           return Listobj;
       }

       /// <summary>
       /// To Get All Nature Finance
       /// </summary>
       /// <param name="financeNature"></param>
       /// <returns></returns>
       public NatureofFinancingList GetnatureAllfinance(string financeNature)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_MST_GETALL_FIN_NATURE";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           if (financeNature.ToString() == "")
           {
               cmd.Parameters.Add("@FINANCENATURE_", DBNull.Value);
           }
           else
           {
               cmd.Parameters.Add("@FINANCENATURE_", financeNature.ToString());
           }
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NatureofFinancingBO Boobj = null;
           NatureofFinancingList Listobj = new NatureofFinancingList();

           while (dr.Read())
           {
               Boobj = new NatureofFinancingBO();
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATUREID")))
                   Boobj.FINANCENATUREID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCENATUREID")));
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATURE")))
                   Boobj.FINANCENATURE = dr.GetString(dr.GetOrdinal("FINANCENATURE"));
               if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                   Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

               Listobj.Add(Boobj);
           }

           dr.Close();

           return Listobj;
       }

       /// <summary>
       /// To Get All Nature Finance
       /// </summary>
       /// <returns></returns>
       public NatureofFinancingList GetNatureOfFinance()
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = " USP_MST_GET_NATURE_FIN";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NatureofFinancingBO Boobj = null;
           NatureofFinancingList Listobj = new NatureofFinancingList();

           while (dr.Read())
           {
               Boobj = new NatureofFinancingBO();
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATUREID")))
                   Boobj.FINANCENATUREID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCENATUREID")));
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATURE")))
                   Boobj.FINANCENATURE = dr.GetString(dr.GetOrdinal("FINANCENATURE"));
               if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                   Boobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

               Listobj.Add(Boobj);
           }

           dr.Close();

           return Listobj;
       }

       /// <summary>
       /// To Obsolete F cond
       /// </summary>
       /// <param name="FNatureId"></param>
       /// <param name="ISDELETED"></param>
       /// <returns></returns>
       public string ObsoleteFcond(int FNatureId, string ISDELETED)
       {
           OracleConnection myConnection = null;
           OracleCommand myCommand = null;
           string result = string.Empty;
           try
           {

               myConnection = new OracleConnection(AppConfiguration.ConnectionString);
               myCommand = new OracleCommand("USP_MST_OBSOLETE_NATUR_FIN", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.Add("@F_FINANCENATUREID", FNatureId);
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
       /// To Update
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Update(NatureofFinancingBO BOobj)
       {
           string returnResult = string.Empty;
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_MST_UPD_NATURE_FIN", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("F_FINANCENATUREID", BOobj.FINANCENATUREID);
               dcmd.Parameters.Add("F_FINANCENATURE", BOobj.FINANCENATURE);
               dcmd.Parameters.Add("F_UPDATEDBY", BOobj.CREATEDBY);
               //return dcmd.ExecuteNonQuery();
               dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
               else
                   returnResult = string.Empty;
           }
           catch(Exception ex)
           {
               throw ex;
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
       /// To Get Nature Finance ID
       /// </summary>
       /// <param name="NatureFinanceID"></param>
       /// <returns></returns>
       public NatureofFinancingBO GetNatureFinanceID(int NatureFinanceID)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_MST_GET_NATUR_FIN";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("F_FINANCENATUREID", NatureFinanceID);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NatureofFinancingBO Boobj = null;
           NatureofFinancingList Listobj = new NatureofFinancingList();

           Boobj = new NatureofFinancingBO();
           while (dr.Read())
           {
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATURE")))
                   Boobj.FINANCENATURE = dr.GetString(dr.GetOrdinal("FINANCENATURE"));
               if (!dr.IsDBNull(dr.GetOrdinal("FINANCENATUREID")))
                   Boobj.FINANCENATUREID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FINANCENATUREID")));

           }
           dr.Close();


           return Boobj;
       }

       /// <summary>
       /// To Delete Nature Finance
       /// </summary>
       /// <param name="NatureFinanceID"></param>
       /// <returns></returns>
       public string DeleteNatureFinance(int NatureFinanceID)
       {
           OracleConnection myConnection = null;
           OracleCommand myCommand = null;

           string result = string.Empty;
           
           try
           {

               myConnection = new OracleConnection(AppConfiguration.ConnectionString);
               myCommand = new OracleCommand("USP_MST_DEL_NATUR_FIN", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.Add("@F_FINANCENATUREID", NatureFinanceID);
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

    }
}
