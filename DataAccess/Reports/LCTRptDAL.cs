using System.Data.SqlClient;
using WIS_BusinessObjects;
using System.Data;

namespace WIS_DataAccess
{
   public class LCTRptDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        public DistrictList LoadDistrictData()
        {
            cnn = new SqlConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_DISTRICT";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            cnn = new SqlConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_COUNTY";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DistrictIDIN", pDstrictId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            cnn = new SqlConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_SUBCOUNTY";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CountyIDIN", pCountyId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_PARISH";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("SUBCOUNTYID_", pSubcountyId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_VILLAGE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SubcountyIDIN", pSubcountyId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
