using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_NEIGHBOUR",cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = string.Empty;

            try 
            {
                dcmd.Parameters.AddWithValue("NEIGHBOURNAME_", Neighbourobj.TRN_PAP_NEIGHBOURNAme1);
                 dcmd.Parameters.AddWithValue("DIRECTION", Neighbourobj.DIRECTION1);
                 dcmd.Parameters.AddWithValue("BOUNDARIESCONFIRMED", Neighbourobj.BOUNDARIESCONFIRMED1);
                 dcmd.Parameters.AddWithValue("BOUNDARY_DISPUTE", Neighbourobj.BOUNDARY_DISPUTE);
                 dcmd.Parameters.AddWithValue("DISPUTE_DETAILS", Neighbourobj.DISPUTE_DETAILS);
                dcmd.Parameters.AddWithValue("CREATEDBY", Neighbourobj.CREATEDBY1);
                dcmd.Parameters.AddWithValue("N_HHID", Neighbourobj.HHID1);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_TRN_SEL_NEIGHBOUR";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("HHID_", householdID);
           //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_TRN_GET_NEIGHBR";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("P_PAP_NEIGHBOURID", Pap_NeighbrID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_NEIGHBOUR",cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           string result = string.Empty;

           try
           {
               dcmd.Parameters.AddWithValue("N_PAP_NEIGHBOURID", Neighbourobj.PAP_NEIGHBOURID1);
               dcmd.Parameters.AddWithValue("N_NEIGHBOURNAME", Neighbourobj.TRN_PAP_NEIGHBOURNAme1);
               dcmd.Parameters.AddWithValue("N_DIRECTION", Neighbourobj.DIRECTION1);
               dcmd.Parameters.AddWithValue("N_BOUNDARIESCONFIRMED", Neighbourobj.BOUNDARIESCONFIRMED1);
               dcmd.Parameters.AddWithValue("N_BOUNDARY_DISPUTE", Neighbourobj.BOUNDARY_DISPUTE);
               dcmd.Parameters.AddWithValue("N_DISPUTE_DETAILS", Neighbourobj.DISPUTE_DETAILS);
               dcmd.Parameters.AddWithValue("N_UPDATEDBY", Neighbourobj.CREATEDBY1);
               /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
           SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
           conn.Open();
           SqlCommand dCmd = new SqlCommand("USP_TRN_DEL_NEIGHBOUR", conn);
           dCmd.CommandType = CommandType.StoredProcedure;
           try
           {
               dCmd.Parameters.AddWithValue("PAP_NEIGHBOURID_", Pap_NeighbrID);
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