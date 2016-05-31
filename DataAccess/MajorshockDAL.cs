using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class MajorshockDAL
    {
      /// <summary>
        /// To Get Type of shock
      /// </summary>
      /// <param name="SHOCKID"></param>
      /// <returns></returns>
       public DataSet GetTypeofshock(string SHOCKID)
        {
            String SqlQuery;

            OracleConnection OraConnection = new OracleConnection(AppConfiguration.ConnectionString);
          //  OracleCommand OraCommand;



            SqlQuery = "SELECT shockid,shockexperienced from mst_shocksexperienced";
            //where  isdeleted = 'False'

               OracleCommand OracleCommand = new OracleCommand(SqlQuery, OraConnection);
            OracleDataAdapter dAd = new OracleDataAdapter(OracleCommand);
            dAd.SelectCommand.CommandType = CommandType.Text;
            DataSet Ds = new DataSet();
        
            try
            {
                dAd.Fill(Ds);
                return Ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                Ds.Dispose();
                dAd.Dispose();
               
            }
      
        }

      /// <summary>
       /// To Get Cop Mech
      /// </summary>
      /// <param name="COP_MECHANISMID"></param>
      /// <returns></returns>
       public DataSet GetCopMech(string COP_MECHANISMID)
       {
           String SqlQuery;

           OracleConnection OraConnection = new OracleConnection(AppConfiguration.ConnectionString);
           //  OracleCommand OraCommand;



           SqlQuery = "SELECT cop_mechanismid,cop_mechanism from mst_coping_mechanism  ";
           //where  isdeleted = 'False'

           OracleCommand OracleCommand = new OracleCommand(SqlQuery, OraConnection);
           OracleDataAdapter dAd = new OracleDataAdapter(OracleCommand);
           dAd.SelectCommand.CommandType = CommandType.Text;
           DataSet Ds = new DataSet();

           try
           {
               dAd.Fill(Ds);
               return Ds;
           }
           catch
           {
               throw;
           }
           finally
           {
               Ds.Dispose();
               dAd.Dispose();

           }
       }

      /// <summary>
       /// To Get help
      /// </summary>
      /// <param name="SUPPORTID"></param>
      /// <returns></returns>
       public DataSet Gethelp(string SUPPORTID)
       {
           String SqlQuery;

           OracleConnection OraConnection = new OracleConnection(AppConfiguration.ConnectionString);
           //  OracleCommand OraCommand;



           SqlQuery = "SELECT supportid,supportedby from mst_support";
             //where  isdeleted = 'false'


           OracleCommand OracleCommand = new OracleCommand(SqlQuery, OraConnection);
           OracleDataAdapter dAd = new OracleDataAdapter(OracleCommand);
           dAd.SelectCommand.CommandType = CommandType.Text;
           DataSet Ds = new DataSet();

           try
           {
               dAd.Fill(Ds);
               return Ds;
           }
           catch
           {
               throw;
           }
           finally
           {
               Ds.Dispose();
               dAd.Dispose();

           }
       }

      /// <summary>
       /// To Insert into DB
      /// </summary>
      /// <param name="Majorshockobj"></param>
      /// <returns></returns>
       public string Insert(MajorshockBO Majorshockobj)
       {
           string resultMessage = string.Empty;
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_TRN_INS_M_SHOCK", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
              dcmd.Parameters.Add("HHID_", Majorshockobj.HHID1);
               dcmd.Parameters.Add("SHOCKID_", Majorshockobj.SHOCKID1);
               dcmd.Parameters.Add("COP_MECHANISMID_", Majorshockobj.COP_MECHANISMID1);
               dcmd.Parameters.Add("SUPPORTID_", Majorshockobj.SUPPORTID1);
               dcmd.Parameters.Add("CREATEDBY_", Majorshockobj.CREATEDBY1);
               dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   resultMessage = dcmd.Parameters["errorMessage_"].Value.ToString();

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
           return resultMessage;
       }

      /// <summary>
       /// To Get Major shock
      /// </summary>
      /// <param name="householdID"></param>
      /// <returns></returns>
       public major_shockList GetMshock(int householdID)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_SEL_SHOCK";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("HHID_", householdID);
           cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

           MajorshockBO Majorshockobj = null;
           major_shockList major_shockListobj = new major_shockList();

           while (dr.Read())
           {
               Majorshockobj = new MajorshockBO();
               Majorshockobj.PAP_SHOCKID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_SHOCKID")));
               Majorshockobj.SHOCKEXPERIENCED1 = dr.GetString(dr.GetOrdinal("shockexperienced"));
               Majorshockobj.SUPPORTEDBY1 = dr.GetString(dr.GetOrdinal("supportedby"));
               Majorshockobj.COP_MECHANISM1 = dr.GetString(dr.GetOrdinal("cop_mechanism"));
               Majorshockobj.ISDELETED1 = dr.GetString(dr.GetOrdinal("ISDELETED"));
               major_shockListobj.Add(Majorshockobj);
           }
         
           dr.Close();

           return major_shockListobj;
       }

      /// <summary>
       /// To Get Pap ShocK Id
      /// </summary>
      /// <param name="PAP_SHOCKID1"></param>
      /// <returns></returns>
       public MajorshockBO GetPapShochId(int PAP_SHOCKID1)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_GET_SHOCK";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("S_PAP_SHOCKID",PAP_SHOCKID1);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           MajorshockBO Majorshockobj = null;
           major_shockList major_shockListobj = new major_shockList();

           Majorshockobj = new MajorshockBO();
           while (dr.Read())
           {
              
               if (ColumnExists(dr, "pap_shockid") && !dr.IsDBNull(dr.GetOrdinal("pap_shockid")))
                   Majorshockobj.PAP_SHOCKID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_shockid")));
               if (ColumnExists(dr, "shockid") && !dr.IsDBNull(dr.GetOrdinal("shockid")))
                   Majorshockobj.SHOCKID1 = dr.GetInt32(dr.GetOrdinal("shockid"));
               if (ColumnExists(dr, "cop_mechanismid") && !dr.IsDBNull(dr.GetOrdinal("cop_mechanismid")))
                   Majorshockobj.COP_MECHANISMID1 = dr.GetInt32(dr.GetOrdinal("cop_mechanismid"));
               if (ColumnExists(dr, "supportid") && !dr.IsDBNull(dr.GetOrdinal("supportid")))
                   Majorshockobj.SUPPORTID1 = dr.GetInt32(dr.GetOrdinal("supportid"));

           }
           dr.Close();


           return Majorshockobj;
       }

      /// <summary>
       /// To check the Column are Exists or not
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
       /// To EDIT Major shock
      /// </summary>
      /// <param name="Majorshockobj"></param>
      /// <returns></returns>
       public string EDITMshock(MajorshockBO Majorshockobj)
       {
           string resultMessage = string.Empty;
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_M_SHOCK", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("S_PAP_SHOCKID", Majorshockobj.PAP_SHOCKID1);
               dcmd.Parameters.Add("S_SHOCKID", Majorshockobj.SHOCKID1);
               dcmd.Parameters.Add("S_COP_MECHANISMID", Majorshockobj.COP_MECHANISMID1);
               dcmd.Parameters.Add("S_SUPPORTID", Majorshockobj.SUPPORTID1);
               dcmd.Parameters.Add("S_UPDATEDBY", Majorshockobj.CREATEDBY1);
               dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   resultMessage = dcmd.Parameters["errorMessage_"].Value.ToString();
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
           return resultMessage;
       }

      /// <summary>
       /// To Delete
      /// </summary>
      /// <param name="PAP_SHOCKID1"></param>
      /// <returns></returns>
       public int Delete(int PAP_SHOCKID1)
       {
           OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
           conn.Open();
           OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_SHOCK", conn);
           dCmd.CommandType = CommandType.StoredProcedure;
           try
           {
               dCmd.Parameters.Add("PAP_SHOCKID_", PAP_SHOCKID1);
               return dCmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally
           {
               dCmd.Dispose();
               conn.Close();
               conn.Dispose();

           }
       }
    }
}
