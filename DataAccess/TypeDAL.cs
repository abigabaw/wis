using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_LND_GET_TYPE";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
