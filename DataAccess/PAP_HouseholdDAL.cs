using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_HouseholdDAL
    {
        /// <summary>
        /// To Get House Hold Data
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public PAP_HouseholdBO GetHouseHoldData(int ID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_HOUSEHOLDDETAILS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", ID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PAP_HouseholdBO objTrn_Pap_HouseHold = null;
            while (dr.Read())
            {
                objTrn_Pap_HouseHold = new PAP_HouseholdBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objTrn_Pap_HouseHold.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objTrn_Pap_HouseHold.Pap_UId = dr.GetString(dr.GetOrdinal("PAP_UID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) objTrn_Pap_HouseHold.ProjectedId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUPID"))) objTrn_Pap_HouseHold.OptiongroupId = dr.GetInt32(dr.GetOrdinal("OPTIONGROUPID"));
                if (!dr.IsDBNull(dr.GetOrdinal("RELIGIONID"))) objTrn_Pap_HouseHold.ReligionId = dr.GetInt32(dr.GetOrdinal("RELIGIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("OCCUPATIONID"))) objTrn_Pap_HouseHold.OccupationId = dr.GetInt32(dr.GetOrdinal("OCCUPATIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPSTATUSID"))) objTrn_Pap_HouseHold.PapstatusId = dr.GetInt32(dr.GetOrdinal("PAPSTATUSID"));
                if (!dr.IsDBNull(dr.GetOrdinal("LITERACYLEVELID"))) objTrn_Pap_HouseHold.LiteracyCycleId = dr.GetInt32(dr.GetOrdinal("LITERACYLEVELID"));
                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDBY"))) objTrn_Pap_HouseHold.UpdatedBy = dr.GetInt32(dr.GetOrdinal("UPDATEDBY"));
                if (!dr.IsDBNull(dr.GetOrdinal("INSTITUTIONNAME"))) objTrn_Pap_HouseHold.InstitutionName = dr.GetString(dr.GetOrdinal("INSTITUTIONNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("NOOFPLOTS"))) objTrn_Pap_HouseHold.Noofplots = dr.GetInt32(dr.GetOrdinal("NOOFPLOTS"));
                if (!dr.IsDBNull(dr.GetOrdinal("SURNAME"))) objTrn_Pap_HouseHold.Surname = dr.GetString(dr.GetOrdinal("SURNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("FIRSTNAME"))) objTrn_Pap_HouseHold.Firstname = dr.GetString(dr.GetOrdinal("FIRSTNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("OTHERNAME"))) objTrn_Pap_HouseHold.Othername = dr.GetString(dr.GetOrdinal("OTHERNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("POSITIONID"))) objTrn_Pap_HouseHold.PositionId = dr.GetInt32(dr.GetOrdinal("POSITIONID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONTACTPHONE1"))) objTrn_Pap_HouseHold.Contactphone1 = dr.GetString(dr.GetOrdinal("CONTACTPHONE1"));
                if (!dr.IsDBNull(dr.GetOrdinal("CONTACTPHONE2"))) objTrn_Pap_HouseHold.Contactphone2 = dr.GetString(dr.GetOrdinal("CONTACTPHONE2"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objTrn_Pap_HouseHold.Paptype = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objTrn_Pap_HouseHold.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objTrn_Pap_HouseHold.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objTrn_Pap_HouseHold.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
            //    if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUP"))) objTrn_Pap_HouseHold.OptionGroup = dr.GetString(dr.GetOrdinal("OPTIONGROUP"));
               
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objTrn_Pap_HouseHold.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objTrn_Pap_HouseHold.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objTrn_Pap_HouseHold.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objTrn_Pap_HouseHold.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objTrn_Pap_HouseHold.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objTrn_Pap_HouseHold.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISRESIDENT"))) objTrn_Pap_HouseHold.Isresident = dr.GetString(dr.GetOrdinal("ISRESIDENT"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objTrn_Pap_HouseHold.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("SEX"))) objTrn_Pap_HouseHold.Sex = dr.GetString(dr.GetOrdinal("SEX"));
                if (!dr.IsDBNull(dr.GetOrdinal("DATEOFBIRTH"))) objTrn_Pap_HouseHold.DateofBirth = dr.GetDateTime(dr.GetOrdinal("DATEOFBIRTH")).ToShortDateString();
                if (!dr.IsDBNull(dr.GetOrdinal("PLACEOFBIRTH"))) objTrn_Pap_HouseHold.PlaceofBirth = dr.GetString(dr.GetOrdinal("PLACEOFBIRTH"));
                if (!dr.IsDBNull(dr.GetOrdinal("YEAROFMOVEIN"))) objTrn_Pap_HouseHold.Yearmoveon = dr.GetString(dr.GetOrdinal("YEAROFMOVEIN"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARENTSALIVE"))) objTrn_Pap_HouseHold.Parentslive = dr.GetString(dr.GetOrdinal("PARENTSALIVE"));
                if (!dr.IsDBNull(dr.GetOrdinal("WHICHPARENTALIVE"))) objTrn_Pap_HouseHold.Whichparentalive = dr.GetString(dr.GetOrdinal("WHICHPARENTALIVE"));
                if (!dr.IsDBNull(dr.GetOrdinal("WHEREPARENTSLIVE"))) objTrn_Pap_HouseHold.Whereparentslive = dr.GetString(dr.GetOrdinal("WHEREPARENTSLIVE"));
                if (!dr.IsDBNull(dr.GetOrdinal("HASIDENTIFICATIONCARD"))) objTrn_Pap_HouseHold.Isidentificationcard = dr.GetString(dr.GetOrdinal("HASIDENTIFICATIONCARD"));
                if (!dr.IsDBNull(dr.GetOrdinal("CARDTYPE"))) objTrn_Pap_HouseHold.Cardtype = Convert.ToString(dr.GetInt32(dr.GetOrdinal("CARDTYPE")));
                if (!dr.IsDBNull(dr.GetOrdinal("CARDNUMBER"))) objTrn_Pap_HouseHold.CardNo = dr.GetString(dr.GetOrdinal("CARDNUMBER"));
                if (!dr.IsDBNull(dr.GetOrdinal("NAMEONCARD"))) objTrn_Pap_HouseHold.NameonCard = dr.GetString(dr.GetOrdinal("NAMEONCARD"));
                if (!dr.IsDBNull(dr.GetOrdinal("ADDRESSONCARD"))) objTrn_Pap_HouseHold.AddressonCard = dr.GetString(dr.GetOrdinal("ADDRESSONCARD"));
                if (!dr.IsDBNull(dr.GetOrdinal("MARITALSTATUS"))) objTrn_Pap_HouseHold.MaritalStatus = dr.GetString(dr.GetOrdinal("MARITALSTATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("NOOFSPOUSES"))) objTrn_Pap_HouseHold.NoofSpouse = dr.GetInt32(dr.GetOrdinal("NOOFSPOUSES"));
                if (!dr.IsDBNull(dr.GetOrdinal("TRIBE"))) objTrn_Pap_HouseHold.Tribe = dr.GetString(dr.GetOrdinal("TRIBE"));
                if (!dr.IsDBNull(dr.GetOrdinal("CLAN"))) objTrn_Pap_HouseHold.Clan = dr.GetString(dr.GetOrdinal("CLAN"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objTrn_Pap_HouseHold.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objTrn_Pap_HouseHold.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objTrn_Pap_HouseHold.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objTrn_Pap_HouseHold.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDBYUSER"))) objTrn_Pap_HouseHold.CreatedByUser = dr.GetString(dr.GetOrdinal("CREATEDBYUSER"));
                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE"))) objTrn_Pap_HouseHold.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE")).ToString(UtilBO.DateFormatFull);
                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDBYUSER"))) objTrn_Pap_HouseHold.UpdatedByUser = dr.GetString(dr.GetOrdinal("UPDATEDBYUSER"));
                if (!dr.IsDBNull(dr.GetOrdinal("UPDATEDDATE"))) objTrn_Pap_HouseHold.UpdatedDate = dr.GetDateTime(dr.GetOrdinal("UPDATEDDATE")).ToString(UtilBO.DateFormatFull);
                if (!dr.IsDBNull(dr.GetOrdinal("DETAILSCAPTUREDBY"))) objTrn_Pap_HouseHold.CapturedBy = dr.GetString(dr.GetOrdinal("DETAILSCAPTUREDBY"));
                if (!dr.IsDBNull(dr.GetOrdinal("DETAILSCAPTUREDDATE"))) objTrn_Pap_HouseHold.CapturedDate = dr.GetDateTime(dr.GetOrdinal("DETAILSCAPTUREDDATE")).ToShortDateString();
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objTrn_Pap_HouseHold.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CLASS"))) objTrn_Pap_HouseHold.LocClassification = dr.GetString(dr.GetOrdinal("CLASS"));
                if (!dr.IsDBNull(dr.GetOrdinal("GOUSTATUS"))) objTrn_Pap_HouseHold.GouStatus = dr.GetString(dr.GetOrdinal("GOUSTATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("UNDERTAKINGPERIOD"))) objTrn_Pap_HouseHold.UnderTakingPeriod = dr.GetString(dr.GetOrdinal("UNDERTAKINGPERIOD"));
                if (!dr.IsDBNull(dr.GetOrdinal("OVERRIDEOPT"))) objTrn_Pap_HouseHold.Overrideopt = dr.GetString(dr.GetOrdinal("OVERRIDEOPT"));
                if (!dr.IsDBNull(dr.GetOrdinal("LANDCOMPENSATION"))) objTrn_Pap_HouseHold.LandCompensation = dr.GetString(dr.GetOrdinal("LANDCOMPENSATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("HOUSECOMPENSATION"))) objTrn_Pap_HouseHold.HouseCompensation = dr.GetString(dr.GetOrdinal("HOUSECOMPENSATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("landper"))) objTrn_Pap_HouseHold.PercentageOccupied = dr.GetDecimal(dr.GetOrdinal("landper"));
            }
            dr.Close();
            //getCommentsData(ID);
            return objTrn_Pap_HouseHold;
        }
        public int GetHousaeHold(PAP_HouseholdBO objTrn_Pap_HouseHold,string landStatus)
        {
            int res=0;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_OPT_HOUSEHOLD";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", objTrn_Pap_HouseHold.HhId);
            cmd.Parameters.Add("LNDCOMP_", objTrn_Pap_HouseHold.LandCompensation);
            cmd.Parameters.Add("HOUSECOMP_", objTrn_Pap_HouseHold.HouseCompensation);
            cmd.Parameters.Add("LNDSTATUS_", landStatus);
            cmd.Parameters.Add("RESIDENT_", objTrn_Pap_HouseHold.Isresident);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           // PAP_HouseholdBO objTrn_Pap_HouseHold = null;
            while (dr.Read())
            {
                objTrn_Pap_HouseHold = new PAP_HouseholdBO();
                if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUPID"))) objTrn_Pap_HouseHold.OptiongroupId = dr.GetInt32(dr.GetOrdinal("OPTIONGROUPID"));
                res = objTrn_Pap_HouseHold.OptiongroupId;
            }
            dr.Close();
            return res;
        }
        public PAP_HouseholdBO getCommentsData(int id)
        {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GETCOMMENTS_DATABY_HHID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", id);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PAP_HouseholdBO objTrn_Pap_HouseHold = null;
            while (dr.Read())
            {
                objTrn_Pap_HouseHold = new PAP_HouseholdBO();
                //if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objTrn_Pap_HouseHold.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) objTrn_Pap_HouseHold.Comments = dr.GetString(dr.GetOrdinal("COMMENTS"));
                if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUPID"))) objTrn_Pap_HouseHold.OptiongroupId = dr.GetInt32(dr.GetOrdinal("OPTIONGROUPID"));
            }
            return objTrn_Pap_HouseHold;
        }

        /// <summary>
        /// To Update House Hold Details
        /// </summary>
        /// <param name="objHouseHold"></param>
        /// <returns></returns>
        public string UpdateHouseHoldDetails(PAP_HouseholdBO objHouseHold)
        {
            string result = "";
            string cnn = AppConfiguration.ConnectionString;
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(cnn);
            myCommand = new OracleCommand("USP_TRN_UPD_HOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@Hh_IDIN", objHouseHold.HhId);
            myCommand.Parameters.Add("@Pap_UIDIN", objHouseHold.Papuid);
            myCommand.Parameters.Add("@Project_IdIN", objHouseHold.ProjectedId);
            myCommand.Parameters.Add("@PapNameIN", objHouseHold.PapName);
            myCommand.Parameters.Add("@PlotReferenceIN", objHouseHold.PlotReference);
            myCommand.Parameters.Add("@DistrictIN", objHouseHold.District);
            myCommand.Parameters.Add("@CountyIN", objHouseHold.County);
            myCommand.Parameters.Add("@SubCountyIN", objHouseHold.SubCounty);
            if (objHouseHold.Parish == "0")
            {
                myCommand.Parameters.Add("@ParishIN", DBNull.Value);
            }
            else
            {
                myCommand.Parameters.Add("@ParishIN", objHouseHold.Parish);
            }
            myCommand.Parameters.Add("@VillageIN", objHouseHold.Village);
            myCommand.Parameters.Add("@RightofwayIN", objHouseHold.Rightofway);
            myCommand.Parameters.Add("@WayLeavesIN", objHouseHold.Wayleaves);
            myCommand.Parameters.Add("@OptionGroupIdIN", objHouseHold.OptiongroupId);
            myCommand.Parameters.Add("@IsResidentIN", objHouseHold.Isresident);
            myCommand.Parameters.Add("@SexIN", objHouseHold.Sex);

            if (objHouseHold.DateofBirth.Trim() != "")
                myCommand.Parameters.Add("@DateofBirthIN", Convert.ToDateTime(objHouseHold.DateofBirth).ToString(UtilBO.DateFormatDB));
            else
                myCommand.Parameters.Add("@DateofBirthIN", DBNull.Value);

            myCommand.Parameters.Add("@PlaceofBirthIN", objHouseHold.PlaceofBirth);
            myCommand.Parameters.Add("@YearofMoveonIN", objHouseHold.Yearmoveon);
            myCommand.Parameters.Add("@ParentsaliveIN", objHouseHold.Parentslive);
            myCommand.Parameters.Add("@whichparentaliveIN", objHouseHold.Whichparentalive);
            myCommand.Parameters.Add("@WhereParentsLiveIN", objHouseHold.Whereparentslive);
            myCommand.Parameters.Add("@HasIdentificationCardIN", objHouseHold.Isidentificationcard);

            if (!string.IsNullOrEmpty(objHouseHold.Cardtype))
                myCommand.Parameters.Add("@CardTypeIN", objHouseHold.Cardtype);
            else
                myCommand.Parameters.Add("@CardTypeIN", DBNull.Value);

            myCommand.Parameters.Add("@CardNumberIN", objHouseHold.CardNo);
            myCommand.Parameters.Add("@NameonCardIN", objHouseHold.NameonCard);
            myCommand.Parameters.Add("@AddressonCardIN", objHouseHold.AddressonCard);
            if (objHouseHold.MaritalStatus == "0" || objHouseHold.MaritalStatus == "--Select--")
            {
                myCommand.Parameters.Add("@MaritalStatusIN", DBNull.Value);
            }
            else
            {
                myCommand.Parameters.Add("@MaritalStatusIN", objHouseHold.MaritalStatus);
            }

            myCommand.Parameters.Add("@NoofSpousesIN", objHouseHold.NoofSpouse);

            myCommand.Parameters.Add("@TribeIN", objHouseHold.Tribe);
            myCommand.Parameters.Add("@ClanIN", objHouseHold.Clan);

            if (objHouseHold.ReligionId == 0)
                myCommand.Parameters.Add("@ReligionIDIN", DBNull.Value);
            else
                myCommand.Parameters.Add("@ReligionIDIN", objHouseHold.ReligionId);

            myCommand.Parameters.Add("religionname_", objHouseHold.Otherreligion);

            if (objHouseHold.OccupationId > 0)
                myCommand.Parameters.Add("@OccupationIDIN", objHouseHold.OccupationId);
            else
                myCommand.Parameters.Add("@OccupationIDIN", DBNull.Value);

            if (objHouseHold.PapstatusId > 0)
                myCommand.Parameters.Add("@PapaStatusIDIN", objHouseHold.PapstatusId);
            else
                myCommand.Parameters.Add("@PapaStatusIDIN", DBNull.Value);

            if (objHouseHold.LiteracyCycleId > 0)
                myCommand.Parameters.Add("@LiteracyCyclelevelId", objHouseHold.LiteracyCycleId);
            else
                myCommand.Parameters.Add("@LiteracyCyclelevelId", DBNull.Value);

            myCommand.Parameters.Add("@IsdeletedIN", objHouseHold.Isdeleted);
            myCommand.Parameters.Add("@UpdatedbyIN", objHouseHold.UpdatedBy);

            myCommand.Parameters.Add("@DESIGNATIONIN", objHouseHold.Designation);
            myCommand.Parameters.Add("@DETAILSCAPTUREDBYIN", objHouseHold.CapturedBy);
            if (objHouseHold.CapturedDate.Trim() != "")
                myCommand.Parameters.Add("@DETAILSCAPTUREDDATEIN", Convert.ToDateTime(objHouseHold.CapturedDate).ToString(UtilBO.DateFormatDB));
            else
                myCommand.Parameters.Add("@DETAILSCAPTUREDDATEIN", DBNull.Value);

            //myCommand.Parameters.Add("@CLASS_", objHouseHold.LocClassification);
            myCommand.Parameters.Add("@GOUSTATUS_", objHouseHold.GouStatus);
            myCommand.Parameters.Add("@UNDERTAKINGPERIOD_", objHouseHold.UnderTakingPeriod);
            myCommand.Parameters.Add("@OVERRIDEOPT_", objHouseHold.Overrideopt);
            myCommand.Parameters.Add("@LANDCOMPENSATION_", objHouseHold.LandCompensation);
            myCommand.Parameters.Add("@HOUSECOMPENSATION_", objHouseHold.HouseCompensation);
            myCommand.Parameters.Add("@LANDPER_", objHouseHold.PercentageOccupied);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();
            else
                result = string.Empty;

            myConnection.Close();
            if (objHouseHold.Overrideopt == "Y")
            {
                insertintoComments(objHouseHold);
            }

            return result;
        }

        private string insertintoComments(PAP_HouseholdBO objHouseHold)
        {
            string result = "";
            string cnn = AppConfiguration.ConnectionString;
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(cnn);
            myCommand = new OracleCommand("USP_INS_MST_COMMENTS", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@Comments_", objHouseHold.Comments);
            myCommand.Parameters.Add("@OPTIONGROUPID_", objHouseHold.OptiongroupId);
            myCommand.Parameters.Add("@HHID_", objHouseHold.HhId);
            myCommand.Parameters.Add("@CREATEDBY_", objHouseHold.CreatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            myConnection.Open();
            myCommand.ExecuteNonQuery();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();
            else
                result = string.Empty;

            myConnection.Close();
            return result;
        }

        /// <summary>
        /// To Get PAP Photo
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_HouseholdBO GetPAPPhoto(int householdID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GET_PAP_PHOTO", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", householdID);
            myCommand.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            myCommand.Connection.Open();
            object img = myCommand.ExecuteScalar();

            try
            {
                byte[] papPhotoBytes = (byte[])img;

                PAP_HouseholdBO objPAP = new PAP_HouseholdBO();
                objPAP.Photo = papPhotoBytes;
                return objPAP;
                //return new System.IO.MemoryStream(papPhotoBytes);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        /// <summary>
        /// To Global Search PAP
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GlobalSearchPAP(int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village, int UserId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GLOBAL_PAP";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", HHID);
            if (PAPUID.Trim() == string.Empty)
                cmd.Parameters.Add("pap_uid_", DBNull.Value);
            else
                cmd.Parameters.Add("pap_uid_", PAPUID);
            cmd.Parameters.Add("PAPNAME_", papName);
            cmd.Parameters.Add("PLOTREFERENCE_", plotReference);
            cmd.Parameters.Add("DISTRICT_", district);
            cmd.Parameters.Add("COUNTY_", county);
            cmd.Parameters.Add("SUBCOUNTY_", subCounty);
            cmd.Parameters.Add("PARISH_", parish);
            cmd.Parameters.Add("VILLAGE_", village);
            cmd.Parameters.Add("userID_", UserId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Trn_Pap_HouseHoldList PAPList = new Trn_Pap_HouseHoldList();
            Trn_Pap_HouseHoldBO objPAP = null;

            while (dr.Read())
            {
                objPAP = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAP.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objPAP.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));
                //id
                //code
                //name 
                if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) objPAP.ProjectedId = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) objPAP.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) objPAP.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objPAP.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objPAP.PapType = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objPAP.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objPAP.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objPAP.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objPAP.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objPAP.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objPAP.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objPAP.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objPAP.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objPAP.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objPAP.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objPAP.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objPAP.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUP"))) objPAP.Optiongroup = dr.GetString(dr.GetOrdinal("OPTIONGROUP"));
                if (!dr.IsDBNull(dr.GetOrdinal("ViewStatus"))) objPAP.Viewstatus = dr.GetValue(dr.GetOrdinal("ViewStatus")).ToString();
               
                PAPList.Add(objPAP);
            }
            dr.Close();

            return PAPList;
        }

        /// <summary>
        /// To Search PAP
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAP(bool recentRecords, int projectID, int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEARCH_PAPS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (recentRecords)
                cmd.Parameters.Add("RECENTRECORDS_", "Y");
            else
                cmd.Parameters.Add("RECENTRECORDS_", "N");

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("HHID_", HHID);
            if (PAPUID.Trim() == string.Empty)
                cmd.Parameters.Add("pap_uid_", DBNull.Value);
            else
                cmd.Parameters.Add("pap_uid_", PAPUID);
            cmd.Parameters.Add("PAPNAME_", papName);
            cmd.Parameters.Add("PLOTREFERENCE_", plotReference);
            cmd.Parameters.Add("DISTRICT_", district);
            cmd.Parameters.Add("COUNTY_", county);
            cmd.Parameters.Add("SUBCOUNTY_", subCounty);
            cmd.Parameters.Add("PARISH_", parish);
            cmd.Parameters.Add("VILLAGE_", village);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Trn_Pap_HouseHoldList PAPList = new Trn_Pap_HouseHoldList();
            Trn_Pap_HouseHoldBO objPAP = null;

            while (dr.Read())
            {
                objPAP = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAP.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objPAP.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));

                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objPAP.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objPAP.PapType = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objPAP.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objPAP.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objPAP.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objPAP.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objPAP.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objPAP.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objPAP.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objPAP.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objPAP.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objPAP.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objPAP.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objPAP.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUP"))) objPAP.Optiongroup = dr.GetString(dr.GetOrdinal("OPTIONGROUP"));
                PAPList.Add(objPAP);
            }
            dr.Close();

            return PAPList;
        }

        /// <summary>
        /// To Search PAP For Pap Export
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPForPapExport(bool recentRecords, int projectID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEARCH_PAPS_EXPORT";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (recentRecords)
                cmd.Parameters.Add("RECENTRECORDS_", "Y");
            else
                cmd.Parameters.Add("RECENTRECORDS_", "N");

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("PAPNAME_", papName);
            cmd.Parameters.Add("PLOTREFERENCE_", plotReference);
            cmd.Parameters.Add("DISTRICT_", district);
            cmd.Parameters.Add("COUNTY_", county);
            cmd.Parameters.Add("SUBCOUNTY_", subCounty);
            cmd.Parameters.Add("PARISH_", parish);
            cmd.Parameters.Add("VILLAGE_", village);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Trn_Pap_HouseHoldList PAPList = new Trn_Pap_HouseHoldList();
            Trn_Pap_HouseHoldBO objPAP = null;

            while (dr.Read())
            {
                objPAP = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAP.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));

                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objPAP.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objPAP.PapType = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objPAP.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objPAP.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objPAP.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objPAP.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objPAP.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objPAP.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objPAP.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objPAP.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objPAP.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objPAP.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objPAP.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_X"))) objPAP.ROW_X = dr.GetString(dr.GetOrdinal("ROW_X"));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_Y"))) objPAP.ROW_Y = dr.GetString(dr.GetOrdinal("ROW_Y"));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_X"))) objPAP.WL_X = dr.GetString(dr.GetOrdinal("WL_X"));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_Y"))) objPAP.WL_Y = dr.GetString(dr.GetOrdinal("WL_Y"));

                PAPList.Add(objPAP);
            }
            dr.Close();

            return PAPList;
        }


        private const double acrehaconvert = 2.47105;
        private const double acresqmetreconvert = 0.000247105;
        private const double hasqmtreconvert = 0.00010000;

        /// <summary>
        /// To Search PAP For ALL
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPForALL(bool recentRecords, int projectID, int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEARCH_PAPSALL";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (recentRecords)
                cmd.Parameters.Add("RECENTRECORDS_", "Y");
            else
                cmd.Parameters.Add("RECENTRECORDS_", "N");

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("HHID_", HHID);
            if (PAPUID.Trim() == string.Empty)
                cmd.Parameters.Add("pap_uid_", DBNull.Value);
            else
                cmd.Parameters.Add("pap_uid_", PAPUID);
            cmd.Parameters.Add("PAPNAME_", papName);
            cmd.Parameters.Add("PLOTREFERENCE_", plotReference);
            cmd.Parameters.Add("DISTRICT_", district);
            cmd.Parameters.Add("COUNTY_", county);
            cmd.Parameters.Add("SUBCOUNTY_", subCounty);
            cmd.Parameters.Add("PARISH_", parish);
            cmd.Parameters.Add("VILLAGE_", village);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Trn_Pap_HouseHoldList PAPList = new Trn_Pap_HouseHoldList();
            Trn_Pap_HouseHoldBO objPAP = null;

            while (dr.Read())
            {
                objPAP = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAP.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));

                if (!dr.IsDBNull(dr.GetOrdinal("surname"))) objPAP.Surname = dr.GetString(dr.GetOrdinal("surname"));
                if (!dr.IsDBNull(dr.GetOrdinal("firstname"))) objPAP.Firstname = dr.GetString(dr.GetOrdinal("firstname"));
                if (!dr.IsDBNull(dr.GetOrdinal("othername"))) objPAP.Othername = dr.GetString(dr.GetOrdinal("othername"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objPAP.PapType = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("INSTITUTIONNAME"))) objPAP.Institution = dr.GetString(dr.GetOrdinal("INSTITUTIONNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) 
                    objPAP.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objPAP.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objPAP.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objPAP.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objPAP.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objPAP.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objPAP.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objPAP.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objPAP.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objPAP.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objPAP.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objPAP.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objPAP.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objPAP.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("lnd_tenure"))) objPAP.Landtenure = dr.GetString(dr.GetOrdinal("lnd_tenure"));
                if (!dr.IsDBNull(dr.GetOrdinal("cropvalue"))) objPAP.Cropsvalue = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("cropvalue")));
                if (!dr.IsDBNull(dr.GetOrdinal("housevalue"))) objPAP.Housevalue = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("housevalue")));
                if (!dr.IsDBNull(dr.GetOrdinal("Disturbance"))) objPAP.Disturbance = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Disturbance")));
                if (objPAP.Rightofway.ToString().Trim() != "" && objPAP.Wayleaves.ToString().Trim() != "")
                    objPAP.TotalAcres = Convert.ToDouble(objPAP.Rightofway) + Convert.ToDouble(objPAP.Wayleaves);
                if (objPAP.TotalAcres >= 0.00)
                {
                    objPAP.TotalHa = Math.Round((objPAP.TotalAcres / acrehaconvert), 6);
                    objPAP.TotalSQM = Math.Round((objPAP.TotalAcres / acresqmetreconvert), 6);
                }
                objPAP.SubTotal = Convert.ToDouble(objPAP.Cropsvalue + objPAP.Housevalue);
                objPAP.Total = Convert.ToDouble(objPAP.Cropsvalue + objPAP.Housevalue + objPAP.Disturbance);
                PAPList.Add(objPAP);
            }
            dr.Close();

            return PAPList;
        }

        /// <summary>
        /// To Get PAP Name By Village
        /// </summary>
        /// <param name="villages"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GetPAPNameByVillage(string villages)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_PAPSBYVILLAGE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("VILLAGES_", OracleDbType.Varchar2, 50).Value = villages;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Trn_Pap_HouseHoldBO objTrn_Pap_HouseHoldBO = null;
            Trn_Pap_HouseHoldList Trn_Pap_HouseHold = new Trn_Pap_HouseHoldList();
            while (dr.Read())
            {
                objTrn_Pap_HouseHoldBO = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objTrn_Pap_HouseHoldBO.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objTrn_Pap_HouseHoldBO.HhId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")).ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objTrn_Pap_HouseHoldBO.PapName = dr.GetValue(dr.GetOrdinal("PAPNAME")).ToString();
                Trn_Pap_HouseHold.Add(objTrn_Pap_HouseHoldBO);
            }
            dr.Close();
            return Trn_Pap_HouseHold;
        }

        /// <summary>
        /// To Approval Change request Status
        /// </summary>
        /// <param name="objHouseHold"></param>
        /// <returns></returns>
        public PAP_HouseholdBO ApprovalChangerequestStatus(PAP_HouseholdBO objHouseHold)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_APPROVALSTATUSPENDING"; //USP_TRN_APPROVALSTATUSPENDING
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ProjectedId_", objHouseHold.ProjectedId);
            cmd.Parameters.Add("Workflowcode_", objHouseHold.Workflowcode);
            cmd.Parameters.Add("HHID_", objHouseHold.HhId);
            cmd.Parameters.Add("PageCode_", objHouseHold.PageCode);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PAP_HouseholdBO objTrn_Pap_HouseHold = null;
            while (dr.Read())
            {
                objTrn_Pap_HouseHold = new PAP_HouseholdBO();
                if (!dr.IsDBNull(dr.GetOrdinal("STATUSID"))) objTrn_Pap_HouseHold.ApproverStatus = dr.GetInt32(dr.GetOrdinal("STATUSID"));

            }
            dr.Close();
            return objTrn_Pap_HouseHold;
        }

        /// <summary>
        /// To Change Request Status
        /// </summary>
        /// <param name="objHouseHold"></param>
        /// <returns></returns>
        public int ChangeRequestStatus(PAP_HouseholdBO objHouseHold)
        {
            int returnResult;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_CHANGEREQUEST", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", objHouseHold.HhId);
                dcmd.Parameters.Add("PageCode_", objHouseHold.PageCode);

                returnResult = dcmd.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }

        /// <summary>
        /// To Is PDP
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public string IsPDP(int householdID)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_PAP_PDP", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", householdID);
               
                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("isPDP", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["isPDP"].Value != null)
                    returnResult = dcmd.Parameters["isPDP"].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }

        /// <summary>
        /// To Get Plot Dependents
        /// </summary>
        /// <param name="HHID_"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GetPlotDependents(int HHID_)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_PLOTDEPENDENTS";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID_);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Trn_Pap_HouseHoldBO objTrn_Pap_HouseHold = null;
            Trn_Pap_HouseHoldList Trn_Pap_HouseHold = new Trn_Pap_HouseHoldList();
            while (dr.Read())
            {
                objTrn_Pap_HouseHold = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objTrn_Pap_HouseHold.PAPIDPOP = dr.GetString(dr.GetOrdinal("PAP_UID")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objTrn_Pap_HouseHold.HhId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")).ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) objTrn_Pap_HouseHold.PapName = dr.GetValue(dr.GetOrdinal("PAPNAME")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objTrn_Pap_HouseHold.PlotReference = dr.GetValue(dr.GetOrdinal("PLOTREFERENCE")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("PAPDESIGNATION"))) objTrn_Pap_HouseHold.PAPDesignationPOP = dr.GetValue(dr.GetOrdinal("PAPDESIGNATION")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objTrn_Pap_HouseHold.Designation = dr.GetValue(dr.GetOrdinal("DESIGNATION")).ToString();

                Trn_Pap_HouseHold.Add(objTrn_Pap_HouseHold);
            }
            dr.Close();
            return Trn_Pap_HouseHold;
        }

        /// <summary>
        /// To Search PAP ON HHID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPONHHID(int projectID, string HHID, string PAPUID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEARCH_HHIDRUID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            

            cmd.Parameters.Add("PROJECTID_", projectID);
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("PAPUID_", PAPUID);
            
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Trn_Pap_HouseHoldList PAPList = new Trn_Pap_HouseHoldList();
            Trn_Pap_HouseHoldBO objPAP = null;

            while (dr.Read())
            {
                objPAP = new Trn_Pap_HouseHoldBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAP.HhId = dr.GetInt32(dr.GetOrdinal("HHID"));

                if (!dr.IsDBNull(dr.GetOrdinal("surname"))) objPAP.Surname = dr.GetString(dr.GetOrdinal("surname"));
                if (!dr.IsDBNull(dr.GetOrdinal("firstname"))) objPAP.Firstname = dr.GetString(dr.GetOrdinal("firstname"));
                if (!dr.IsDBNull(dr.GetOrdinal("othername"))) objPAP.Othername = dr.GetString(dr.GetOrdinal("othername"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPTYPE"))) objPAP.PapType = dr.GetString(dr.GetOrdinal("PAPTYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("INSTITUTIONNAME"))) objPAP.Institution = dr.GetString(dr.GetOrdinal("INSTITUTIONNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME")))
                    objPAP.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE"))) objPAP.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESIGNATION"))) objPAP.Designation = dr.GetString(dr.GetOrdinal("DESIGNATION"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_UID"))) objPAP.Papuid = dr.GetString(dr.GetOrdinal("PAP_UID"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) objPAP.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) objPAP.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) objPAP.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGE"))) objPAP.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PARISH"))) objPAP.Parish = dr.GetString(dr.GetOrdinal("PARISH"));
                if (!dr.IsDBNull(dr.GetOrdinal("RIGHTOFWAY"))) objPAP.Rightofway = dr.GetString(dr.GetOrdinal("RIGHTOFWAY"));
                if (!dr.IsDBNull(dr.GetOrdinal("WAYLEAVES"))) objPAP.Wayleaves = dr.GetString(dr.GetOrdinal("WAYLEAVES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objPAP.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLATITUDE"))) objPAP.Plotlatitude = dr.GetString(dr.GetOrdinal("PLOTLATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("PLOTLONGITUDE"))) objPAP.Plotlongitude = dr.GetString(dr.GetOrdinal("PLOTLONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("lnd_tenure"))) objPAP.Landtenure = dr.GetString(dr.GetOrdinal("lnd_tenure"));
                if (!dr.IsDBNull(dr.GetOrdinal("cropvalue"))) objPAP.Cropsvalue = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("cropvalue")));
                if (!dr.IsDBNull(dr.GetOrdinal("housevalue"))) objPAP.Housevalue = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("housevalue")));
                if (!dr.IsDBNull(dr.GetOrdinal("Disturbance"))) objPAP.Disturbance = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Disturbance")));
                if (objPAP.Rightofway.ToString().Trim() != "" && objPAP.Wayleaves.ToString().Trim() != "")
                    objPAP.TotalAcres = Convert.ToDouble(objPAP.Rightofway) + Convert.ToDouble(objPAP.Wayleaves);
                if (objPAP.TotalAcres >= 0.00)
                {
                    objPAP.TotalHa = Math.Round((objPAP.TotalAcres / acrehaconvert), 6);
                    objPAP.TotalSQM = Math.Round((objPAP.TotalAcres / acresqmetreconvert), 6);
                }
                objPAP.SubTotal = Convert.ToDouble(objPAP.Cropsvalue + objPAP.Housevalue);
                objPAP.Total = Convert.ToDouble(objPAP.Cropsvalue + objPAP.Housevalue + objPAP.Disturbance);
                PAPList.Add(objPAP);
            }
            dr.Close();

            return PAPList;
        }


        public CompensationPackagesList getprintComments(int Hhid)
        {
            CompensationPackagesList COMPACKList = new CompensationPackagesList();
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_OPTIONGRPCOMMENTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", Hhid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                CompensationPackagesBO cmppkgBo;
                while (dr.Read())
                {
                    cmppkgBo = new CompensationPackagesBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("OverridedBy"))) cmppkgBo.UserName = dr.GetString(dr.GetOrdinal("OverridedBy"));
                    if (!dr.IsDBNull(dr.GetOrdinal("createddate"))) cmppkgBo.ApprovedDate = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("createddate"))).ToString("dd-MMM-yyyy");
                    if (!dr.IsDBNull(dr.GetOrdinal("optiongroup"))) cmppkgBo.PKGDOCitemName = (dr.GetString(dr.GetOrdinal("optiongroup")));
                    if (!dr.IsDBNull(dr.GetOrdinal("comments"))) cmppkgBo.ApprovalComents = dr.GetString(dr.GetOrdinal("comments"));

                    COMPACKList.Add(cmppkgBo);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return COMPACKList;
        }
    }
}