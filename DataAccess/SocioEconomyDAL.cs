using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class SocioEconomyDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        #region "Welfare Indicator"
        /// <summary>
        /// To Get General Welfare Masters
        /// </summary>
        /// <returns></returns>
        public GeneralWelfareMasterList GetGeneralWelfareMasters()
        {
            proc = "USP_MST_GET_WELFAREINDICATORS";
            cnn = new OracleConnection(con);
            GeneralWelfareMasterBO objWelfareMaster = null;

            GeneralWelfareMasterList WelfareMasterList = new GeneralWelfareMasterList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWelfareMaster = new GeneralWelfareMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("WLF_INDICATORID"))) objWelfareMaster.WelfareIndicatorID = dr.GetInt32(dr.GetOrdinal("WLF_INDICATORID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WLF_INDICATORNAME"))) objWelfareMaster.WelfareIndicatorName = dr.GetString(dr.GetOrdinal("WLF_INDICATORNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FIELDTYPE"))) objWelfareMaster.FieldType = dr.GetString(dr.GetOrdinal("FIELDTYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ASSOCIATEDWITH"))) objWelfareMaster.AssociatedWith = dr.GetInt32(dr.GetOrdinal("ASSOCIATEDWITH"));

                    WelfareMasterList.Add(objWelfareMaster);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return WelfareMasterList;
        }

        /// <summary>
        /// To Get General Welfares
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public GeneralWelfareList GetGeneralWelfares(int householdID)
        {
            proc = "USP_MST_GET_PAPWELFARES";
            cnn = new OracleConnection(con);
            GeneralWelfareBO objWelfare = null;

            GeneralWelfareList WelfareList = new GeneralWelfareList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            //try
            //{
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objWelfare = new GeneralWelfareBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("WLF_INDICATORID"))) objWelfare.WelfareIndicatorID = dr.GetInt32(dr.GetOrdinal("WLF_INDICATORID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WLF_INDICATORVALUE"))) objWelfare.FieldValue = dr.GetString(dr.GetOrdinal("WLF_INDICATORVALUE"));

                    WelfareList.Add(objWelfare);
                }

                dr.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return WelfareList;
        }

        /// <summary>
        /// To Update PAP Welfare
        /// </summary>
        /// <param name="WelfareList"></param>
        public void UpdatePAPWelfare(GeneralWelfareList WelfareList)
        {
            cnn = new OracleConnection(con);
            proc = "USP_TRN_UPD_GENERALWELFARE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HOUSEHOLDID_", "");
            cmd.Parameters.Add("WLF_INDICATORID_", "");
            cmd.Parameters.Add("WLF_INDICATORVALUE_", "");
            cmd.Parameters.Add("UPDATEDBY_", "");

            foreach (GeneralWelfareBO objWelfare in WelfareList)
            {
                cmd.Parameters["HOUSEHOLDID_"].Value = objWelfare.HouseholdID;
                cmd.Parameters["WLF_INDICATORID_"].Value = objWelfare.WelfareIndicatorID;
                cmd.Parameters["WLF_INDICATORVALUE_"].Value = objWelfare.FieldValue;
                cmd.Parameters["UPDATEDBY_"].Value = objWelfare.UpdatedBy;
                cmd.ExecuteNonQuery();
            }

            cmd.Connection.Close();
        }

        /// <summary>
        /// To Get Welfare Voluntary
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public WelfareVoluntaryBO GetWelfareVoluntary(int householdID)
        {
            proc = "USP_MST_GET_PAPWELFAREVOL";
            cnn = new OracleConnection(con);
            WelfareVoluntaryBO objVoluntary = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                objVoluntary = new WelfareVoluntaryBO();

                if (!dr.IsDBNull(dr.GetOrdinal("WHEREGETDRINKINGWATER"))) objVoluntary.WhereGetDrinkingWater = dr.GetString(dr.GetOrdinal("WHEREGETDRINKINGWATER"));
                if (!dr.IsDBNull(dr.GetOrdinal("WATERSOURCEDISTANCE"))) objVoluntary.WaterSourceDistance = dr.GetString(dr.GetOrdinal("WATERSOURCEDISTANCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DOYOUFISH"))) objVoluntary.DoYouFish = dr.GetString(dr.GetOrdinal("DOYOUFISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("WHEREDOYOUFISH"))) objVoluntary.WhereDoYouFish = dr.GetString(dr.GetOrdinal("WHEREDOYOUFISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("HOWOFTEN"))) objVoluntary.HowOftenFish = dr.GetString(dr.GetOrdinal("HOWOFTEN"));
                if (!dr.IsDBNull(dr.GetOrdinal("DOYOUHUNT"))) objVoluntary.DoYouHunt = dr.GetString(dr.GetOrdinal("DOYOUHUNT"));
                if (!dr.IsDBNull(dr.GetOrdinal("WHEREHUNT"))) objVoluntary.WhereHunt = dr.GetString(dr.GetOrdinal("WHEREHUNT"));
                if (!dr.IsDBNull(dr.GetOrdinal("FIREWOOD"))) objVoluntary.Firewood = dr.GetString(dr.GetOrdinal("FIREWOOD"));
                if (!dr.IsDBNull(dr.GetOrdinal("CHARCOAL"))) objVoluntary.Charcoal = dr.GetString(dr.GetOrdinal("CHARCOAL"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARAFFIN"))) objVoluntary.Paraffin = dr.GetString(dr.GetOrdinal("PARAFFIN"));
                if (!dr.IsDBNull(dr.GetOrdinal("ELECTRICITY"))) objVoluntary.Electricity = dr.GetString(dr.GetOrdinal("ELECTRICITY"));
                if (!dr.IsDBNull(dr.GetOrdinal("GAS"))) objVoluntary.Gas = dr.GetString(dr.GetOrdinal("GAS"));
                if (!dr.IsDBNull(dr.GetOrdinal("SOLAR"))) objVoluntary.Solar = dr.GetString(dr.GetOrdinal("SOLAR"));
                if (!dr.IsDBNull(dr.GetOrdinal("BIOGAS"))) objVoluntary.Biogas = dr.GetString(dr.GetOrdinal("BIOGAS"));
                if (!dr.IsDBNull(dr.GetOrdinal("OTHERFUEL"))) objVoluntary.OtherFuel = dr.GetString(dr.GetOrdinal("OTHERFUEL"));
                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) objVoluntary.Comments = dr.GetString(dr.GetOrdinal("COMMENTS"));
            }

            dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objVoluntary;
        }

        /// <summary>
        /// To Update PAP Welfare Voluntary
        /// </summary>
        /// <param name="objVoluntary"></param>
        public void UpdatePAPWelfareVoluntary(WelfareVoluntaryBO objVoluntary)
        {
            cnn = new OracleConnection(con);
            proc = "USP_TRN_UPD_WELFAREVOLUNTARY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("HOUSEHOLDID_", objVoluntary.HouseholdID);
            cmd.Parameters.Add("WHEREGETDRINKINGWATER_", objVoluntary.WhereGetDrinkingWater);
            cmd.Parameters.Add("WATERSOURCEDISTANCE_", objVoluntary.WaterSourceDistance);
            cmd.Parameters.Add("DOYOUFISH_", objVoluntary.DoYouFish);
            cmd.Parameters.Add("WHEREDOYOUFISH_", objVoluntary.WhereDoYouFish);
            cmd.Parameters.Add("HOWOFTEN_", objVoluntary.HowOftenFish);
            cmd.Parameters.Add("DOYOUHUNT_", objVoluntary.DoYouHunt);
            cmd.Parameters.Add("WHEREHUNT_", objVoluntary.WhereHunt);
            cmd.Parameters.Add("FIREWOOD_", objVoluntary.Firewood);
            cmd.Parameters.Add("CHARCOAL_", objVoluntary.Charcoal);
            cmd.Parameters.Add("PARAFFIN_", objVoluntary.Paraffin);
            cmd.Parameters.Add("ELECTRICITY_", objVoluntary.Electricity);
            cmd.Parameters.Add("GAS_", objVoluntary.Gas);
            cmd.Parameters.Add("SOLAR_", objVoluntary.Solar);
            cmd.Parameters.Add("BIOGAS_", objVoluntary.Biogas);
            cmd.Parameters.Add("OTHERFUEL_", objVoluntary.OtherFuel);
            cmd.Parameters.Add("COMMENTS_", objVoluntary.Comments);
            cmd.Parameters.Add("UPDATEDBY_", objVoluntary.UpdatedBy);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        #endregion
    }
}