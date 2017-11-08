using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class SocioEconomyDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        #region "Welfare Indicator"
        /// <summary>
        /// To Get General Welfare Masters
        /// </summary>
        /// <returns></returns>
        public GeneralWelfareMasterList GetGeneralWelfareMasters()
        {
            proc = "USP_MST_GET_WELFAREINDICATORS";
            cnn = new SqlConnection(con);
            GeneralWelfareMasterBO objWelfareMaster = null;

            GeneralWelfareMasterList WelfareMasterList = new GeneralWelfareMasterList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);
            GeneralWelfareBO objWelfare = null;

            GeneralWelfareList WelfareList = new GeneralWelfareList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            //try
            //{
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);
            proc = "USP_TRN_UPD_GENERALWELFARE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", "");
            cmd.Parameters.AddWithValue("WLF_INDICATORID_", "");
            cmd.Parameters.AddWithValue("WLF_INDICATORVALUE_", "");
            cmd.Parameters.AddWithValue("UPDATEDBY_", "");

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
            cnn = new SqlConnection(con);
            WelfareVoluntaryBO objVoluntary = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);
            proc = "USP_TRN_UPD_WELFAREVOLUNTARY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objVoluntary.HouseholdID);
            cmd.Parameters.AddWithValue("WHEREGETDRINKINGWATER_", objVoluntary.WhereGetDrinkingWater);
            cmd.Parameters.AddWithValue("WATERSOURCEDISTANCE_", objVoluntary.WaterSourceDistance);
            cmd.Parameters.AddWithValue("DOYOUFISH_", objVoluntary.DoYouFish);
            cmd.Parameters.AddWithValue("WHEREDOYOUFISH_", objVoluntary.WhereDoYouFish);
            cmd.Parameters.AddWithValue("HOWOFTEN_", objVoluntary.HowOftenFish);
            cmd.Parameters.AddWithValue("DOYOUHUNT_", objVoluntary.DoYouHunt);
            cmd.Parameters.AddWithValue("WHEREHUNT_", objVoluntary.WhereHunt);
            cmd.Parameters.AddWithValue("FIREWOOD_", objVoluntary.Firewood);
            cmd.Parameters.AddWithValue("CHARCOAL_", objVoluntary.Charcoal);
            cmd.Parameters.AddWithValue("PARAFFIN_", objVoluntary.Paraffin);
            cmd.Parameters.AddWithValue("ELECTRICITY_", objVoluntary.Electricity);
            cmd.Parameters.AddWithValue("GAS_", objVoluntary.Gas);
            cmd.Parameters.AddWithValue("SOLAR_", objVoluntary.Solar);
            cmd.Parameters.AddWithValue("BIOGAS_", objVoluntary.Biogas);
            cmd.Parameters.AddWithValue("OTHERFUEL_", objVoluntary.OtherFuel);
            cmd.Parameters.AddWithValue("COMMENTS_", objVoluntary.Comments);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objVoluntary.UpdatedBy);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        #endregion
    }
}