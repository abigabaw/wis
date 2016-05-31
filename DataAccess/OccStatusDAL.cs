using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_OCCUPATION";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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