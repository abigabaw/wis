using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TypeDAL
    {
        /// <summary>
        /// To Get Land Type
        /// </summary>
        /// <returns></returns>
        public TypeList GetLandType()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_LND_GET_TYPE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TypeBO objtypeBO = null;
            TypeList objtypelist = new TypeList();

            while (dr.Read())
            {
                objtypeBO = new TypeBO();
                objtypeBO.LND_TYPEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TYPEID"))));
                objtypeBO.LandType = dr.GetValue(dr.GetOrdinal("LANDTYPE")).ToString();
                objtypelist.Add(objtypeBO);
            }

            dr.Close();
            return objtypelist;
        }

    }
}
