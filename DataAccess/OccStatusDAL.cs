using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class OccStatusDAL
    {
        /// <summary>
        /// To Load Status Data
        /// </summary>
        /// <returns></returns>
        public OccStatusList LoadStatusData()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_OCCUPATION";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OccStatusBO obMaster = null;
            OccStatusList Master = new OccStatusList();
            while (dr.Read())
            {
                obMaster = new OccStatusBO();
                obMaster.MasterID = dr.GetInt32(dr.GetOrdinal("ID"));
                obMaster.MasterName = dr.GetString(dr.GetOrdinal("Name"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }
    }
}