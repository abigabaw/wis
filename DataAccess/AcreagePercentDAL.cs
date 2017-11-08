using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class AcreagePercentDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Add Acreage Percentage into Database
        /// </summary>
        /// <param name="objPercent"></param>
        /// <returns></returns>
        public string AddAcreagePercent(AcreagePercentBO objPercent)
        {
            string returnResult = string.Empty;

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_MST_INS_ACREAGEPERC";
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("percent_", objPercent.Percent);
                    cmd.Parameters.AddWithValue("createdBy_", objPercent.CreatedBy);
                    cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_MST_UPD_ACREAGEPERC";
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("percentID_", objPercent.PercentID);
                    cmd.Parameters.AddWithValue("percent_", objPercent.Percent);
                    cmd.Parameters.AddWithValue("updatedBy_", objPercent.UpdatedBy);

                    cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    try
                    {                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_MST_DEL_ACREAGEPERC";
                        
                        cmd.Parameters.AddWithValue("percentid_", percentID);

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
           using (cnn = new SqlConnection(con))
           {
               using (cmd = new SqlCommand(proc, cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandText = "USP_MST_GET_ACREAGEPERCBYID";

                   cmd.Parameters.AddWithValue("percentID_", percentID);
                   // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                   try
                   {
                       cmd.Connection.Open();
                       SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
