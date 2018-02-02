using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
          SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;
          string proc = "USP_MST_LND_GET_USE";
          cmd = new SqlCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
