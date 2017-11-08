using System;
using System.Data;
using System.Data.SqlClient;
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_MST_INS_NATUR_FINANCE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.AddWithValue("F_FINANCENATURE", BOobj.FINANCENATURE);
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
       /// To Get All Nature Finance
       /// </summary>
       /// <returns></returns>
       public NatureofFinancingList GetAllNatureFinance()
       {
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_TRN_GET_ALLNATURFIN";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_MST_GETALL_FIN_NATURE";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           if (financeNature.ToString() == "")
           {
               cmd.Parameters.AddWithValue("@FINANCENATURE_", DBNull.Value);
           }
           else
           {
               cmd.Parameters.AddWithValue("@FINANCENATURE_", financeNature.ToString());
           }
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_MST_GET_NATURE_FIN";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;
           string result = string.Empty;
           try
           {

               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_OBSOLETE_NATUR_FIN", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("@F_FINANCENATUREID", FNatureId);
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
       /// To Update
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Update(NatureofFinancingBO BOobj)
       {
           string returnResult = string.Empty;
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_MST_UPD_NATURE_FIN", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.AddWithValue("F_FINANCENATUREID", BOobj.FINANCENATUREID);
               dcmd.Parameters.AddWithValue("F_FINANCENATURE", BOobj.FINANCENATURE);
               dcmd.Parameters.AddWithValue("F_UPDATEDBY", BOobj.CREATEDBY);
               //return dcmd.ExecuteNonQuery();
               dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_MST_GET_NATUR_FIN";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("F_FINANCENATUREID", NatureFinanceID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;

           string result = string.Empty;
           
           try
           {

               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_DEL_NATUR_FIN", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("@F_FINANCENATUREID", NatureFinanceID);
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

    }
}
