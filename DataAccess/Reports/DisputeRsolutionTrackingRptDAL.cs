using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data;


namespace WIS_DataAccess
{
   public class DisputeRsolutionTrackingRptDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        public DistrictList LoadDistrictData()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_DISTRICT";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DistrictBO obMaster = null;
            DistrictList Master = new DistrictList();
            while (dr.Read())
            {
                obMaster = new DistrictBO();
                obMaster.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
                obMaster.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        public CountyList LoadCountyData(string pDstrictId)
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_COUNTY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DistrictIDIN", pDstrictId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CountyBO obMaster = null;
            CountyList Master = new CountyList();
            while (dr.Read())
            {
                obMaster = new CountyBO();
                obMaster.CountyID = dr.GetInt32(dr.GetOrdinal("COUNTYID"));
                obMaster.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_SUBCOUNTY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CountyIDIN", pCountyId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SubCountyBO obMaster = null;
            SubCountyList Master = new SubCountyList();
            while (dr.Read())
            {
                obMaster = new SubCountyBO();
                obMaster.SubCountyID = dr.GetInt32(dr.GetOrdinal("SUBCOUNTYID"));
                obMaster.SubCountyName = dr.GetString(dr.GetOrdinal("SUBCOUNTYNAME"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_PARISH";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("SUBCOUNTYID_", pSubcountyId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ParishBO obMaster = null;
            ParishList Master = new ParishList();
            while (dr.Read())
            {
                obMaster = new ParishBO();
                obMaster.ParishId = dr.GetInt32(dr.GetOrdinal("ID"));
                obMaster.ParishName = dr.GetString(dr.GetOrdinal("Name"));

                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_VILLAGE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SubcountyIDIN", pSubcountyId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            VillageBO obMaster = null;
            VillageList Master = new VillageList();
            while (dr.Read())
            {
                obMaster = new VillageBO();
                obMaster.VillageID = dr.GetInt32(dr.GetOrdinal("VILLAGEID"));
                obMaster.VillageName = dr.GetString(dr.GetOrdinal("VILLAGENAME"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }
    }
}
