using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_DISABILITY";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
