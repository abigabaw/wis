using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class TribeClanDAL
    {
        /// <summary>
        /// To Load Tribe Clan Data
        /// </summary>
        /// <param name="pTribeId"></param>
        /// <returns></returns>
        public TribeClanList LoadTribeClanData(string pTribeId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_CLANS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TribeIDIN", pTribeId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TribeClanBO obMaster = null;
            TribeClanList Master = new TribeClanList();
            while (dr.Read())
            {
                obMaster = new TribeClanBO();
                obMaster.MasterID = dr.GetInt32(dr.GetOrdinal("ID"));
                obMaster.MasterName = dr.GetString(dr.GetOrdinal("Name"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }
    }
}