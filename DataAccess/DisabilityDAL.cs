using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class DisabilityDAL
    {
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public DisabilityList GetDisabilities()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_DISABILITY";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DisabilityBO objDisability = null;
            DisabilityList Disabilities = new DisabilityList();

            while (dr.Read())
            {
                objDisability = new DisabilityBO();
                objDisability.DisabilityID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DisabilityID"))));
                objDisability.DisabilityName = dr.GetValue(dr.GetOrdinal("DisabilityName")).ToString();
                Disabilities.Add(objDisability);
            }

            dr.Close();
            return Disabilities;
        }
    }
}
