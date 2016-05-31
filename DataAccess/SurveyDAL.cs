using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_UPD_LANDLIVINGON", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", objSurvey.HouseholdID);
            myCommand.Parameters.Add("WHERELIVEDBEFORE_", objSurvey.WhereLivedBefore);
            myCommand.Parameters.Add("PREFERREDVILLAGE_", objSurvey.PreferredVillege);
            myCommand.Parameters.Add("ISOTHERLANDHOLDING_", objSurvey.IsOtherLandHold);
            myCommand.Parameters.Add("WHICHLANDHOLDING_", objSurvey.WhichLandHold);
            myCommand.Parameters.Add("REQUIRETRANSPORT_", objSurvey.RequireTransport);
            myCommand.Parameters.Add("MOVENEARRELATIVES_", objSurvey.MovenearRelatives);
            myCommand.Parameters.Add("BURIEDFAMILYMEMONLAND_", objSurvey.BuriedFamilyMemonLand);
            myCommand.Parameters.Add("HOWMANYBURIED_", objSurvey.HowmanyBuried);
            myCommand.Parameters.Add("RELOCATEANCESTORS_", objSurvey.RelocateAncestors);
            myCommand.Parameters.Add("DISTRICT_", objSurvey.District);
            myCommand.Parameters.Add("COUNTY_", objSurvey.County);
            myCommand.Parameters.Add("SUBCOUNTY_", objSurvey.Subcounty);
            myCommand.Parameters.Add("VILLAGE_", objSurvey.Village);
            myCommand.Parameters.Add("UPDATEDBY_", objSurvey.UpdatedBy);
            myCommand.Parameters.Add("errormessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGON";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_UPD_LANDLIVINGOFF", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("LIVINGOFFID_", objSurveyLandLivingOff.LivingOffID);
            myCommand.Parameters.Add("HHID_", objSurveyLandLivingOff.HouseholdID);
            myCommand.Parameters.Add("DWELLINGID_", objSurveyLandLivingOff.DwellingID);
            myCommand.Parameters.Add("NOOFROOMS_", objSurveyLandLivingOff.NoofRooms);

            if (objSurveyLandLivingOff.Str_TenureID > 0)
                myCommand.Parameters.Add("STR_TENUREID_", objSurveyLandLivingOff.Str_TenureID);
            else
                myCommand.Parameters.Add("STR_TENUREID_", DBNull.Value);

            if (objSurveyLandLivingOff.Tenure > 0)
                myCommand.Parameters.Add("TENURE_", objSurveyLandLivingOff.Tenure);
            else
                myCommand.Parameters.Add("TENURE_", DBNull.Value);

            if (objSurveyLandLivingOff.RoofID > 0)
                myCommand.Parameters.Add("ROOFID_", objSurveyLandLivingOff.RoofID);
            else
                myCommand.Parameters.Add("ROOFID_", DBNull.Value);

            if (objSurveyLandLivingOff.WallID > 0)
                myCommand.Parameters.Add("WALLID_", objSurveyLandLivingOff.WallID);
            else
                myCommand.Parameters.Add("WALLID_", DBNull.Value);

            if (objSurveyLandLivingOff.FloorID > 0)
                myCommand.Parameters.Add("FLOORID_", objSurveyLandLivingOff.FloorID);
            else
                myCommand.Parameters.Add("FLOORID_", DBNull.Value);

            myCommand.Parameters.Add("UPDATEDBY_", objSurveyLandLivingOff.UpdatedBy);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGOFF";

            LandLivingOffBO objLivingOff = null;
            LandLivingOffList LandLivingOffLst = new LandLivingOffList();
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LANDLIVINGONBYID";

            LandLivingOffBO objLivingOff = null;

            //SurveyList.LandLivingOffList LandLivingOffLst = new SurveyList.LandLivingOffList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("LIVINGOFFID_", LivingOffID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_DEL_LANDLIVINGOFF";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("LIVINGOFFID_", LivingOffID);
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
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_PAP_LND_VALUATION", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", objAffectedAcreageValuation.HouseholdID);
            myCommand.Parameters.Add("LANDOWNER_", objAffectedAcreageValuation.Landowner);
            myCommand.Parameters.Add("LANDBLOCK_", objAffectedAcreageValuation.Landblock);
            myCommand.Parameters.Add("LANDPLOT_", objAffectedAcreageValuation.Landplot);

            if (objAffectedAcreageValuation.Proprietorid > 0)
                myCommand.Parameters.Add("PROPRIETORID_", objAffectedAcreageValuation.Proprietorid);
            else
                myCommand.Parameters.Add("PROPRIETORID_", DBNull.Value);

            myCommand.Parameters.Add("WHOLEACREAGEACRES_", objAffectedAcreageValuation.Wholeacreageacres);
            myCommand.Parameters.Add("ROWACRES_", objAffectedAcreageValuation.Rowacres);
            myCommand.Parameters.Add("ROWLANDVALUESHARE_", objAffectedAcreageValuation.Rowlandvalueshare);
            myCommand.Parameters.Add("ROWRATEPERACRE_", objAffectedAcreageValuation.Rowrateperacre);
            myCommand.Parameters.Add("ROWLANDVALUE_", objAffectedAcreageValuation.Rowlandvalue);
            myCommand.Parameters.Add("WLACRES_", objAffectedAcreageValuation.Wlacres);
            myCommand.Parameters.Add("DIMUNITIONLEVEL_", objAffectedAcreageValuation.Dimunitionlevel);
            myCommand.Parameters.Add("WLRATEPERACRE_", objAffectedAcreageValuation.Wlrateperacre);
            myCommand.Parameters.Add("WLLANDVALUESHARE_", objAffectedAcreageValuation.Wllandvalueshare);
            myCommand.Parameters.Add("WLLANDVALUE_", objAffectedAcreageValuation.Wllandvalue);
            myCommand.Parameters.Add("UPDATEDBY_", objAffectedAcreageValuation.UpdatedBy);
            myCommand.Parameters.Add("Class_", objAffectedAcreageValuation.LocClassification);
            myCommand.Parameters.Add("errormessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_TRN_PAP_LND_VALUATION";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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