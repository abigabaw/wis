using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class AcreagePercentDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Add Acreage Percentage into Database
        /// </summary>
        /// <param name="objPercent"></param>
        /// <returns></returns>
        public string AddAcreagePercent(AcreagePercentBO objPercent)
        {
            string returnResult = string.Empty;

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_MST_INS_ACREAGEPERC";
                    cmd.Connection.Open();

                    cmd.Parameters.Add("percent_", objPercent.Percent);
                    cmd.Parameters.Add("createdBy_", objPercent.CreatedBy);
                    cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["errorMessage_"].Value != null)
                        returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                    else
                        returnResult = string.Empty;

                    cmd.Connection.Close();
                }
            }
            
            return returnResult;
        }

        public string UpdateAcreagePercent(AcreagePercentBO objPercent)
        {
            string returnResult = string.Empty;

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_MST_UPD_ACREAGEPERC";
                    cmd.Connection.Open();

                    cmd.Parameters.Add("percentID_", objPercent.PercentID);
                    cmd.Parameters.Add("percent_", objPercent.Percent);
                    cmd.Parameters.Add("updatedBy_", objPercent.UpdatedBy);

                    cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["errorMessage_"].Value != null)
                        returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                    else
                        returnResult = string.Empty;

                    cmd.Connection.Close();
                }
            }
            
            return returnResult;
        }

        public void DeleteAcreagePercent(int percentID)
        {
            string result = string.Empty;
            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    try
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_MST_DEL_ACREAGEPERC";
                        
                        cmd.Parameters.Add("percentid_", percentID);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }

        
       public AcreagePercentBO GetAcreagePercentID(int percentID)
       {
           AcreagePercentBO objPercent = null;
           using (cnn = new OracleConnection(con))
           {
               using (cmd = new OracleCommand(proc, cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandText = "USP_MST_GET_ACREAGEPERCBYID";

                   cmd.Parameters.Add("percentID_", percentID);
                   cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                   try
                   {
                       cmd.Connection.Open();
                       OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                       while (dr.Read())
                       {
                           objPercent = new AcreagePercentBO();

                           if (!dr.IsDBNull(dr.GetOrdinal("PercentID"))) objPercent.PercentID = dr.GetInt32(dr.GetOrdinal("PercentID"));
                           if (!dr.IsDBNull(dr.GetOrdinal("Percent"))) objPercent.Percent = dr.GetString(dr.GetOrdinal("Percent"));

                       }

                       dr.Close();
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
               }
           }

           return objPercent;
       }
    }
}
