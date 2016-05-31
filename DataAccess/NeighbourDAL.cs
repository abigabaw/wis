using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class NeighbourDAL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Neighbourobj"></param>
        /// <returns></returns>
        public string Insert(NeighbourBO Neighbourobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_NEIGHBOUR",cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = string.Empty;

            try 
            {
                dcmd.Parameters.Add("NEIGHBOURNAME_", Neighbourobj.TRN_PAP_NEIGHBOURNAme1);
                 dcmd.Parameters.Add("DIRECTION", Neighbourobj.DIRECTION1);
                 dcmd.Parameters.Add("BOUNDARIESCONFIRMED", Neighbourobj.BOUNDARIESCONFIRMED1);
                 dcmd.Parameters.Add("BOUNDARY_DISPUTE", Neighbourobj.BOUNDARY_DISPUTE);
                 dcmd.Parameters.Add("DISPUTE_DETAILS", Neighbourobj.DISPUTE_DETAILS);
                dcmd.Parameters.Add("CREATEDBY", Neighbourobj.CREATEDBY1);
                dcmd.Parameters.Add("N_HHID", Neighbourobj.HHID1);
                dcmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["ERRORMESSAGE_"].Value.ToString();
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
        /// To Get Neighbour Details
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
       public NeighbourList GetneigbrDetails(int householdID)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_SEL_NEIGHBOUR";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("HHID_", householdID);
           cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NeighbourBO Neighbourobj = null;
           NeighbourList NeighbourListobj = new NeighbourList();

           while (dr.Read())
           {
               Neighbourobj = new NeighbourBO();
               Neighbourobj.PAP_NEIGHBOURID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_NEIGHBOURID")));
               Neighbourobj.TRN_PAP_NEIGHBOURNAme1 = dr.GetString(dr.GetOrdinal("NEIGHBOURNAME"));
               Neighbourobj.ISDELETED1 = dr.GetString(dr.GetOrdinal("ISDELETED"));
               Neighbourobj.DIRECTION1 = dr.GetString(dr.GetOrdinal("DIRECTION"));
               Neighbourobj.BOUNDARIESCONFIRMED1 = dr.GetString(dr.GetOrdinal("BOUNDARIESCONFIRMED"));
               Neighbourobj.BOUNDARY_DISPUTE = dr.GetString(dr.GetOrdinal("boundary_dispute"));
               if (ColumnExists(dr, "dispute_details") && !dr.IsDBNull(dr.GetOrdinal("dispute_details")))
                   Neighbourobj.DISPUTE_DETAILS = dr.GetString(dr.GetOrdinal("dispute_details"));
               NeighbourListobj.Add(Neighbourobj);
           }

           dr.Close();

           return NeighbourListobj;
       }

        /// <summary>
       /// To Get Neighbour By Id
        /// </summary>
        /// <param name="Pap_NeighbrID"></param>
        /// <returns></returns>
       public NeighbourBO GetNeighbrById(int Pap_NeighbrID)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_GET_NEIGHBR";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("P_PAP_NEIGHBOURID", Pap_NeighbrID);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           NeighbourBO Neighbourobj = null;
           NeighbourList NeighbourListobj = new NeighbourList();

           Neighbourobj = new NeighbourBO();
           while (dr.Read())
           {
               if (ColumnExists(dr, "neighbourname") && !dr.IsDBNull(dr.GetOrdinal("neighbourname")))
                   Neighbourobj.TRN_PAP_NEIGHBOURNAme1 = dr.GetString(dr.GetOrdinal("neighbourname"));
               if (ColumnExists(dr, "pap_neighbourid") && !dr.IsDBNull(dr.GetOrdinal("pap_neighbourid")))
                   Neighbourobj.PAP_NEIGHBOURID1 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_neighbourid")));
               if (ColumnExists(dr, "direction") && !dr.IsDBNull(dr.GetOrdinal("direction")))
                   Neighbourobj.DIRECTION1 = dr.GetString(dr.GetOrdinal("direction"));
               if (ColumnExists(dr, "boundariesconfirmed") && !dr.IsDBNull(dr.GetOrdinal("boundariesconfirmed")))
                   Neighbourobj.BOUNDARIESCONFIRMED1 = dr.GetString(dr.GetOrdinal("boundariesconfirmed"));
               if (ColumnExists(dr, "boundary_dispute") && !dr.IsDBNull(dr.GetOrdinal("boundary_dispute")))
                   Neighbourobj.BOUNDARY_DISPUTE = dr.GetString(dr.GetOrdinal("boundary_dispute"));
               if (ColumnExists(dr, "dispute_details") && !dr.IsDBNull(dr.GetOrdinal("dispute_details")))
                   Neighbourobj.DISPUTE_DETAILS = dr.GetString(dr.GetOrdinal("dispute_details"));

           }
           dr.Close();


           return Neighbourobj;
       }

        /// <summary>
        /// To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
       private bool ColumnExists(IDataReader reader, string columnName)
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
       /// To Edit Neighbour
        /// </summary>
        /// <param name="Neighbourobj"></param>
        /// <returns></returns>
       public string EditNeighbr(NeighbourBO Neighbourobj)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_NEIGHBOUR",cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           string result = string.Empty;

           try
           {
               dcmd.Parameters.Add("N_PAP_NEIGHBOURID", Neighbourobj.PAP_NEIGHBOURID1);
               dcmd.Parameters.Add("N_NEIGHBOURNAME", Neighbourobj.TRN_PAP_NEIGHBOURNAme1);
               dcmd.Parameters.Add("N_DIRECTION", Neighbourobj.DIRECTION1);
               dcmd.Parameters.Add("N_BOUNDARIESCONFIRMED", Neighbourobj.BOUNDARIESCONFIRMED1);
               dcmd.Parameters.Add("N_BOUNDARY_DISPUTE", Neighbourobj.BOUNDARY_DISPUTE);
               dcmd.Parameters.Add("N_DISPUTE_DETAILS", Neighbourobj.DISPUTE_DETAILS);
               dcmd.Parameters.Add("N_UPDATEDBY", Neighbourobj.CREATEDBY1);
               dcmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   result = dcmd.Parameters["ERRORMESSAGE_"].Value.ToString();
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
       /// To Delete
        /// </summary>
        /// <param name="Pap_NeighbrID"></param>
        /// <returns></returns>
       public int Delete(int Pap_NeighbrID)
       {
           OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
           conn.Open();
           OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_NEIGHBOUR", conn);
           dCmd.CommandType = CommandType.StoredProcedure;
           try
           {
               dCmd.Parameters.Add("PAP_NEIGHBOURID_", Pap_NeighbrID);
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