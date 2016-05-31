using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System;

namespace WIS_DataAccess
{
    public class MasterDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Load District Data
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// To Load County Data
        /// </summary>
        /// <param name="pDstrictId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To Load Sub County Data
        /// </summary>
        /// <param name="pCountyId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To Load Parish Data
        /// </summary>
        /// <param name="pSubcountyId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To Load Village Data
        /// </summary>
        /// <param name="pSubcountyId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To Load Religion Data
        /// </summary>
        /// <returns></returns>
        public ReligionList LoadReligionData()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_RELIGION";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReligionBO obMaster = null;
            ReligionList Master = new ReligionList();
            while (dr.Read())
            {
                obMaster = new ReligionBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) obMaster.ReligionID = dr.GetInt32(dr.GetOrdinal("ID"));
                if (!dr.IsDBNull(dr.GetOrdinal("Name"))) obMaster.ReligionName = dr.GetString(dr.GetOrdinal("Name"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        /// <summary>
        /// To Load Option Group Data
        /// </summary>
        /// <returns></returns>
        public OptionGroupList LoadOptionGroupData()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_OPTIONGROUPS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionGroupBO obMaster = null;
            OptionGroupList Master = new OptionGroupList();
            while (dr.Read())
            {
                obMaster = new OptionGroupBO();
                obMaster.OptionGroupID = dr.GetInt32(dr.GetOrdinal("ID"));
                obMaster.OptionGroupName = dr.GetString(dr.GetOrdinal("Name"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }

        /// <summary>
        /// To Load Proprietes Data
        /// </summary>
        /// <returns></returns>
        public ProprietorList LoadProprieterData()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GET_PROPRIETORGROUP";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProprietorBO obProprietor = null;
            ProprietorList Proprietor = new ProprietorList();
            while (dr.Read())
            {
                obProprietor = new ProprietorBO();
                obProprietor.ProprietorID = dr.GetInt32(dr.GetOrdinal("ID"));
                obProprietor.ProprietorName = dr.GetString(dr.GetOrdinal("Name"));
                Proprietor.Add(obProprietor);
            }
            dr.Close();
            return Proprietor;
        }

        /// <summary>
        /// To Load Tenure Land
        /// </summary>
        /// <returns></returns>
        public TenureLandList LoadTenureLand()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ALLLANDTENURE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            TenureLandBO objTenureLand = null;
            TenureLandList TenureLand = new TenureLandList();
            while (dr.Read())
            {
                objTenureLand = new TenureLandBO();
                objTenureLand.Lnd_TenureId =  Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TENUREID")));
                objTenureLand.Lnd_Tenure = dr.GetString(dr.GetOrdinal("LND_TENURE"));
                TenureLand.Add(objTenureLand);
            }
            dr.Close();
            return TenureLand;
        }

        /// <summary>
        /// To Load Currency
        /// </summary>
        /// <returns></returns>
        public CurrencyList LoadCurrency()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GETCURRENCY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CurrencyBO objCurrencyBO = null;
            CurrencyList Currency = new CurrencyList();
            while (dr.Read())
            {
                objCurrencyBO = new CurrencyBO();
                objCurrencyBO.CurrencyID = dr.GetInt16(dr.GetOrdinal("CURRENCYID"));
                objCurrencyBO.CurrencyCode = dr.GetString(dr.GetOrdinal("CURRENCYCODE"));
                Currency.Add(objCurrencyBO);
            }
            dr.Close();
            return Currency;
        }

        /// <summary>
        /// To Load Representation Data
        /// </summary>
        /// <returns></returns>
        public RepresentationList LoadRepresentationData()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_LOADREPRESENTATION";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RepresentationBO obMaster = null;
            RepresentationList Master = new RepresentationList();
            while (dr.Read())
            {
                obMaster = new RepresentationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) obMaster.RepresentationID = dr.GetInt32(dr.GetOrdinal("ID"));
                if (!dr.IsDBNull(dr.GetOrdinal("Name"))) obMaster.RepresentationName = dr.GetString(dr.GetOrdinal("Name"));
                Master.Add(obMaster);
            }
            dr.Close();
            return Master;
        }
    }
}