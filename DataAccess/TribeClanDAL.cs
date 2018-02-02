using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_CLANS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TribeIDIN", pTribeId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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