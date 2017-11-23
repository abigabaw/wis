using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class SurveyDAL
    {
        #region LandLivingON
        /// <summary>
        /// To Add Land Living On
        /// </summary>
        /// <param name="objSurvey"></param>
        /// <returns></returns>
        public string AddLandLivingOn(LandLivingOnBO objSurvey)
        {
            string result = string.Empty;
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_UPD_LANDLIVINGON", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("HHID_", objSurvey.HouseholdID);
            myCommand.Parameters.AddWithValue("WHERELIVEDBEFORE_", objSurvey.WhereLivedBefore);
            myCommand.Parameters.AddWithValue("PREFERREDVILLAGE_", objSurvey.PreferredVillege);
            myCommand.Parameters.AddWithValue("ISOTHERLANDHOLDING_", objSurvey.IsOtherLandHold);
            myCommand.Parameters.AddWithValue("WHICHLANDHOLDING_", objSurvey.WhichLandHold);
            myCommand.Parameters.AddWithValue("REQUIRETRANSPORT_", objSurvey.RequireTransport);
            myCommand.Parameters.AddWithValue("MOVENEARRELATIVES_", objSurvey.MovenearRelatives);
            myCommand.Parameters.AddWithValue("BURIEDFAMILYMEMONLAND_", objSurvey.BuriedFamilyMemonLand);
            myCommand.Parameters.AddWithValue("HOWMANYBURIED_", objSurvey.HowmanyBuried);
            myCommand.Parameters.AddWithValue("RELOCATEANCESTORS_", objSurvey.RelocateAncestors);
            myCommand.Parameters.AddWithValue("DISTRICT_", objSurvey.District);
            myCommand.Parameters.AddWithValue("COUNTY_", objSurvey.County);
            myCommand.Parameters.AddWithValue("SUBCOUNTY_", objSurvey.Subcounty);
            myCommand.Parameters.AddWithValue("VILLAGE_", objSurvey.Village);
            myCommand.Parameters.AddWithValue("UPDATEDBY_", objSurvey.UpdatedBy);
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            if (myCommand.Parameters["errormessage_"].Value != null)
                result = myCommand.Parameters["errormessage_"].Value.ToString();
            myConnection.Close();
            return result;
        }

        /// <summary>
        /// To Get Land Living On By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandLivingOnBO GetLandLivingOnByHHID(int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGON";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandLivingOnBO obSurvey = null;
            while (dr.Read())
            {
                obSurvey = new LandLivingOnBO();
                if (!dr.IsDBNull(dr.GetOrdinal("WHERELIVEDBEFORE"))) obSurvey.WhereLivedBefore = dr.GetString(dr.GetOrdinal("WHERELIVEDBEFORE"));

                if (!dr.IsDBNull(dr.GetOrdinal("PREFERREDVILLAGE")))
                    obSurvey.PreferredVillege = dr.GetString(dr.GetOrdinal("PREFERREDVILLAGE"));

                if (!dr.IsDBNull(dr.GetOrdinal("ISOTHERLANDHOLDING"))) obSurvey.IsOtherLandHold = dr.GetString(dr.GetOrdinal("ISOTHERLANDHOLDING"));

                if (!dr.IsDBNull(dr.GetOrdinal("WHICHLANDHOLDING")))
                    obSurvey.WhichLandHold = dr.GetString(dr.GetOrdinal("WHICHLANDHOLDING"));

                if (!dr.IsDBNull(dr.GetOrdinal("REQUIRETRANSPORT"))) obSurvey.RequireTransport = dr.GetString(dr.GetOrdinal("REQUIRETRANSPORT"));
                if (!dr.IsDBNull(dr.GetOrdinal("MOVENEARRELATIVES"))) obSurvey.MovenearRelatives = dr.GetString(dr.GetOrdinal("MOVENEARRELATIVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("BURIEDFAMILYMEMONLAND"))) obSurvey.BuriedFamilyMemonLand = dr.GetString(dr.GetOrdinal("BURIEDFAMILYMEMONLAND"));
                if (!dr.IsDBNull(dr.GetOrdinal("HOWMANYBURIED"))) obSurvey.HowmanyBuried = dr.GetString(dr.GetOrdinal("HOWMANYBURIED"));
                if (!dr.IsDBNull(dr.GetOrdinal("RELOCATEANCESTORS"))) obSurvey.RelocateAncestors = dr.GetString(dr.GetOrdinal("RELOCATEANCESTORS"));

                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT")))
                    obSurvey.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY")))
                    obSurvey.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY")))
                    obSurvey.Subcounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE")))
                    obSurvey.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
            }
            dr.Close();
            return obSurvey;
        }

        #endregion

        #region LandLivingOFF
        /// <summary>
        /// To Add Land Living OFF
        /// </summary>
        /// <param name="objSurveyLandLivingOff"></param>
        /// <returns></returns>
        public int AddLandLivingOFF(LandLivingOffBO objSurveyLandLivingOff)
        {
            int result = 0;
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_UPD_LANDLIVINGOFF", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("LIVINGOFFID_", objSurveyLandLivingOff.LivingOffID);
            myCommand.Parameters.AddWithValue("HHID_", objSurveyLandLivingOff.HouseholdID);
            myCommand.Parameters.AddWithValue("DWELLINGID_", objSurveyLandLivingOff.DwellingID);
            myCommand.Parameters.AddWithValue("NOOFROOMS_", objSurveyLandLivingOff.NoofRooms);

            if (objSurveyLandLivingOff.Str_TenureID > 0)
                myCommand.Parameters.AddWithValue("STR_TENUREID_", objSurveyLandLivingOff.Str_TenureID);
            else
                myCommand.Parameters.AddWithValue("STR_TENUREID_", DBNull.Value);

            if (objSurveyLandLivingOff.Tenure > 0)
                myCommand.Parameters.AddWithValue("TENURE_", objSurveyLandLivingOff.Tenure);
            else
                myCommand.Parameters.AddWithValue("TENURE_", DBNull.Value);

            if (objSurveyLandLivingOff.RoofID > 0)
                myCommand.Parameters.AddWithValue("ROOFID_", objSurveyLandLivingOff.RoofID);
            else
                myCommand.Parameters.AddWithValue("ROOFID_", DBNull.Value);

            if (objSurveyLandLivingOff.WallID > 0)
                myCommand.Parameters.AddWithValue("WALLID_", objSurveyLandLivingOff.WallID);
            else
                myCommand.Parameters.AddWithValue("WALLID_", DBNull.Value);

            if (objSurveyLandLivingOff.FloorID > 0)
                myCommand.Parameters.AddWithValue("FLOORID_", objSurveyLandLivingOff.FloorID);
            else
                myCommand.Parameters.AddWithValue("FLOORID_", DBNull.Value);

            myCommand.Parameters.AddWithValue("UPDATEDBY_", objSurveyLandLivingOff.UpdatedBy);
            myConnection.Open();
            result = myCommand.ExecuteNonQuery();
            myConnection.Close();
            return result;
        }

        /// <summary>
        /// To Get Living OFF
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandLivingOffList GetLivingOFF(int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGOFF";

            LandLivingOffBO objLivingOff = null;
            LandLivingOffList LandLivingOffLst = new LandLivingOffList();
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    objLivingOff = new LandLivingOffBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIVINGOFFID"))) objLivingOff.LivingOffID = dr.GetInt32(dr.GetOrdinal("LIVINGOFFID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("dwellingtype"))) objLivingOff.Dwellingtype = dr.GetString(dr.GetOrdinal("dwellingtype"));
                    if (!dr.IsDBNull(dr.GetOrdinal("noofrooms"))) objLivingOff.NoofRooms = dr.GetString(dr.GetOrdinal("noofrooms"));
                    if (!dr.IsDBNull(dr.GetOrdinal("str_tenure"))) objLivingOff.Str_Tenure = dr.GetString(dr.GetOrdinal("str_tenure"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TENURE"))) objLivingOff.Tenure = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TENURE")));
                    if (!dr.IsDBNull(dr.GetOrdinal("rooftype"))) objLivingOff.RoofType = dr.GetString(dr.GetOrdinal("rooftype"));
                    if (!dr.IsDBNull(dr.GetOrdinal("walltype"))) objLivingOff.WallType = dr.GetString(dr.GetOrdinal("walltype"));
                    if (!dr.IsDBNull(dr.GetOrdinal("floortype"))) objLivingOff.FloorType = dr.GetString(dr.GetOrdinal("floortype"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) objLivingOff.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    LandLivingOffLst.Add(objLivingOff);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LandLivingOffLst;
        }

        /// <summary>
        /// To Get Living OFF By ID
        /// </summary>
        /// <param name="LivingOffID"></param>
        /// <returns></returns>
        public LandLivingOffBO GetLivingOFFByID(int LivingOffID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGONBYID";

            LandLivingOffBO objLivingOff = null;

            //SurveyList.LandLivingOffList LandLivingOffLst = new SurveyList.LandLivingOffList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("LIVINGOFFID_", LivingOffID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objLivingOff = new LandLivingOffBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIVINGOFFID"))) objLivingOff.LivingOffID = dr.GetInt32(dr.GetOrdinal("LIVINGOFFID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("dwellingid"))) objLivingOff.DwellingID = dr.GetInt32(dr.GetOrdinal("dwellingid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("noofrooms"))) objLivingOff.NoofRooms = dr.GetString(dr.GetOrdinal("noofrooms"));
                    if (!dr.IsDBNull(dr.GetOrdinal("str_tenureid"))) objLivingOff.Str_TenureID = dr.GetInt32(dr.GetOrdinal("str_tenureid"));
                 //   if (!dr.IsDBNull(dr.GetOrdinal("str_tenure"))) objLivingOff.Str_Tenure = dr.GetString(dr.GetOrdinal("str_tenure"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TENURE"))) objLivingOff.Tenure = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TENURE")));
                    if (!dr.IsDBNull(dr.GetOrdinal("roofid"))) objLivingOff.RoofID = dr.GetInt32(dr.GetOrdinal("roofid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("wallid"))) objLivingOff.WallID = dr.GetInt32(dr.GetOrdinal("wallid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("floorid"))) objLivingOff.FloorID = dr.GetInt32(dr.GetOrdinal("floorid"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objLivingOff;
        }

        /// <summary>
        /// To Delete Land Living Off
        /// </summary>
        /// <param name="LivingOffID"></param>
        public void DeleteLandLivingOff(int LivingOffID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_DEL_LANDLIVINGOFF";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("LIVINGOFFID_", LivingOffID);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        #endregion

        #region AffectedAcreageValuation

        /// <summary>
        /// To Add Affected Acreage Valuation
        /// </summary>
        /// <param name="objAffectedAcreageValuation"></param>
        /// <returns></returns>
        public string AddAffectedAcreageValuation(AffectedAcreageValuationBO objAffectedAcreageValuation)
        {
            string result = "";
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_PAP_LND_VALUATION", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("HHID_", objAffectedAcreageValuation.HouseholdID);
            myCommand.Parameters.AddWithValue("LANDOWNER_", objAffectedAcreageValuation.Landowner);
            myCommand.Parameters.AddWithValue("LANDBLOCK_", objAffectedAcreageValuation.Landblock);
            myCommand.Parameters.AddWithValue("LANDPLOT_", objAffectedAcreageValuation.Landplot);

            if (objAffectedAcreageValuation.Proprietorid > 0)
                myCommand.Parameters.AddWithValue("PROPRIETORID_", objAffectedAcreageValuation.Proprietorid);
            else
                myCommand.Parameters.AddWithValue("PROPRIETORID_", DBNull.Value);

            myCommand.Parameters.AddWithValue("WHOLEACREAGEACRES_", objAffectedAcreageValuation.Wholeacreageacres);
            myCommand.Parameters.AddWithValue("ROWACRES_", objAffectedAcreageValuation.Rowacres);
            myCommand.Parameters.AddWithValue("ROWLANDVALUESHARE_", objAffectedAcreageValuation.Rowlandvalueshare);
            myCommand.Parameters.AddWithValue("ROWRATEPERACRE_", objAffectedAcreageValuation.Rowrateperacre);
            myCommand.Parameters.AddWithValue("ROWLANDVALUE_", objAffectedAcreageValuation.Rowlandvalue);
            myCommand.Parameters.AddWithValue("WLACRES_", objAffectedAcreageValuation.Wlacres);
            myCommand.Parameters.AddWithValue("DIMUNITIONLEVEL_", objAffectedAcreageValuation.Dimunitionlevel);
            myCommand.Parameters.AddWithValue("WLRATEPERACRE_", objAffectedAcreageValuation.Wlrateperacre);
            myCommand.Parameters.AddWithValue("WLLANDVALUESHARE_", objAffectedAcreageValuation.Wllandvalueshare);
            myCommand.Parameters.AddWithValue("WLLANDVALUE_", objAffectedAcreageValuation.Wllandvalue);
            myCommand.Parameters.AddWithValue("UPDATEDBY_", objAffectedAcreageValuation.UpdatedBy);
            myCommand.Parameters.AddWithValue("Class_", objAffectedAcreageValuation.LocClassification);
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            if (myCommand.Parameters["errormessage_"].Value != null)
                result = myCommand.Parameters["errormessage_"].Value.ToString();
            myConnection.Close();
            return result;
        }

        /// <summary>
        /// To Get Affected Acreage Valuation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public AffectedAcreageValuationBO GetAffectedAcreageValuation(int HHID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_GET_TRN_PAP_LND_VALUATION";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            AffectedAcreageValuationBO obSurvey = null;
            while (dr.Read())
            {
                obSurvey = new AffectedAcreageValuationBO();

                if (!dr.IsDBNull(dr.GetOrdinal("LANDOWNER"))) obSurvey.Landowner = dr.GetString(dr.GetOrdinal("LANDOWNER"));
                if (!dr.IsDBNull(dr.GetOrdinal("LANDBLOCK"))) obSurvey.Landblock = dr.GetString(dr.GetOrdinal("LANDBLOCK"));
                if (!dr.IsDBNull(dr.GetOrdinal("LANDPLOT"))) obSurvey.Landplot = dr.GetString(dr.GetOrdinal("LANDPLOT"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROPRIETORID"))) obSurvey.Proprietorid = dr.GetInt32(dr.GetOrdinal("PROPRIETORID"));
                if (!dr.IsDBNull(dr.GetOrdinal("WHOLEACREAGEACRES"))) obSurvey.Wholeacreageacres = dr.GetString(dr.GetOrdinal("WHOLEACREAGEACRES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROWACRES"))) obSurvey.Rowacres = dr.GetDecimal(dr.GetOrdinal("ROWACRES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROWLANDVALUESHARE"))) obSurvey.Rowlandvalueshare = dr.GetDecimal(dr.GetOrdinal("ROWLANDVALUESHARE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROWRATEPERACRE"))) obSurvey.Rowrateperacre = dr.GetDecimal(dr.GetOrdinal("ROWRATEPERACRE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROWLANDVALUE"))) obSurvey.Rowlandvalue = dr.GetDecimal(dr.GetOrdinal("ROWLANDVALUE"));
                if (!dr.IsDBNull(dr.GetOrdinal("WLACRES"))) obSurvey.Wlacres = dr.GetDecimal(dr.GetOrdinal("WLACRES"));
                if (!dr.IsDBNull(dr.GetOrdinal("DIMUNITIONLEVEL"))) obSurvey.Dimunitionlevel = dr.GetDecimal(dr.GetOrdinal("DIMUNITIONLEVEL"));
                if (!dr.IsDBNull(dr.GetOrdinal("WLRATEPERACRE"))) obSurvey.Wlrateperacre = dr.GetDecimal(dr.GetOrdinal("WLRATEPERACRE"));
                if (!dr.IsDBNull(dr.GetOrdinal("WLLANDVALUESHARE"))) obSurvey.Wllandvalueshare = dr.GetDecimal(dr.GetOrdinal("WLLANDVALUESHARE"));
                if (!dr.IsDBNull(dr.GetOrdinal("WLLANDVALUE"))) obSurvey.Wllandvalue = dr.GetDecimal(dr.GetOrdinal("WLLANDVALUE"));
            }
            dr.Close();
            return obSurvey;
        }
        #endregion
    }
}