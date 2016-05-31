using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class LandUseDAL
    {
      /// <summary>
        /// To Get Land Use
      /// </summary>
      /// <returns></returns>
      public LandUseList GetLandUse()
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_LND_GET_USE";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          LandUseBO objLUBO = null;
          LandUseList objLUlist = new LandUseList();

          while (dr.Read())
          {
              objLUBO = new LandUseBO();
              objLUBO.LND_USEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_USEID"))));
              objLUBO.LANDUSE = dr.GetValue(dr.GetOrdinal("LANDUSE")).ToString();
              objLUlist.Add(objLUBO);
          }

          dr.Close();
          return objLUlist;
      }

    }
}
