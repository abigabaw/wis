using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class PAP_RelationDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Add Relation
        /// </summary>
        /// <param name="objRelation"></param>
        /// <returns></returns>
        public string AddRelation(PAP_RelationBO objRelation)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_PAPRELATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objRelation.HouseholdID);
            cmd.Parameters.AddWithValue("HOLDERTYPEID_", objRelation.HolderTypeID);
            cmd.Parameters.AddWithValue("HOLDERNAME_", objRelation.HolderName);
            cmd.Parameters.AddWithValue("RESIDEONAFFECTEDLAND_", objRelation.ResideOnAffectedLand);

            if (objRelation.DateOfBirth != null && objRelation.DateOfBirth != DateTime.MinValue)
                cmd.Parameters.AddWithValue("DATEOFBIRTH_", objRelation.DateOfBirth);
            else
                cmd.Parameters.AddWithValue("DATEOFBIRTH_", DBNull.Value);

            cmd.Parameters.AddWithValue("GENDER_", objRelation.Sex);

            if (objRelation.LiteracyLevelID > 0)
                cmd.Parameters.AddWithValue("LITERACYLEVELID_", objRelation.LiteracyLevelID);
            else
                cmd.Parameters.AddWithValue("LITERACYLEVELID_", DBNull.Value);

            if (objRelation.CurrentSchoolStatusID > 0)
                cmd.Parameters.AddWithValue("CUR_SCH_STATUSID_", objRelation.CurrentSchoolStatusID);
            else
                cmd.Parameters.AddWithValue("CUR_SCH_STATUSID_", DBNull.Value);

            if (objRelation.NeverAttendedSchoolID > 0)
                cmd.Parameters.AddWithValue("NVR_ATT_SCH_REASONID_", objRelation.NeverAttendedSchoolID);
            else
                cmd.Parameters.AddWithValue("NVR_ATT_SCH_REASONID_", DBNull.Value);

            if (objRelation.SchoolDropReasonID > 0)
                cmd.Parameters.AddWithValue("SCH_DRP_REASONID_", objRelation.SchoolDropReasonID);
            else
                cmd.Parameters.AddWithValue("SCH_DRP_REASONID_", DBNull.Value);

            cmd.Parameters.AddWithValue("CREATEDBY_", objRelation.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Update Relation
        /// </summary>
        /// <param name="objRelation"></param>
        /// <returns></returns>
        public string UpdateRelation(PAP_RelationBO objRelation)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PAPRELATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("PAP_HH_RELATIONID_", objRelation.RelationID);
            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objRelation.HouseholdID);
            cmd.Parameters.AddWithValue("HOLDERTYPEID_", objRelation.HolderTypeID);
            cmd.Parameters.AddWithValue("HOLDERNAME_", objRelation.HolderName);
            cmd.Parameters.AddWithValue("RESIDEONAFFECTEDLAND_", objRelation.ResideOnAffectedLand);

            if (objRelation.DateOfBirth != null && objRelation.DateOfBirth != DateTime.MinValue)
                cmd.Parameters.AddWithValue("DATEOFBIRTH_", objRelation.DateOfBirth);
            else
                cmd.Parameters.AddWithValue("DATEOFBIRTH_", DBNull.Value);

            cmd.Parameters.AddWithValue("GENDER_", objRelation.Sex);

            if (objRelation.LiteracyLevelID > 0)
                cmd.Parameters.AddWithValue("LITERACYLEVELID_", objRelation.LiteracyLevelID);
            else
                cmd.Parameters.AddWithValue("LITERACYLEVELID_", DBNull.Value);

            if (objRelation.CurrentSchoolStatusID > 0)
                cmd.Parameters.AddWithValue("CUR_SCH_STATUSID_", objRelation.CurrentSchoolStatusID);
            else
                cmd.Parameters.AddWithValue("CUR_SCH_STATUSID_", DBNull.Value);

            if (objRelation.NeverAttendedSchoolID > 0)
                cmd.Parameters.AddWithValue("NVR_ATT_SCH_REASONID_", objRelation.NeverAttendedSchoolID);
            else
                cmd.Parameters.AddWithValue("NVR_ATT_SCH_REASONID_", DBNull.Value);

            if (objRelation.SchoolDropReasonID > 0)
                cmd.Parameters.AddWithValue("SCH_DRP_REASONID_", objRelation.SchoolDropReasonID);
            else
                cmd.Parameters.AddWithValue("SCH_DRP_REASONID_", DBNull.Value);

            cmd.Parameters.AddWithValue("UPDATEDBY_", objRelation.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Delete Relation
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        public string DeleteRelation(int relationID)
        {
            string result = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_PAPRELATION";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("PAP_HH_RELATIONID_", relationID);
            //cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            //if (cmd.Parameters["errorMessage_"].Value != null)
             //   result = cmd.Parameters["errorMessage_"].Value.ToString();
            cmd.Connection.Close();
            return result;  
        }

        /// <summary>
        /// To Get Relations
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="holderTypeID"></param>
        /// <returns></returns>
        public PAPRelationList GetRelations(int householdID, int holderTypeID)
        {
            proc = "USP_TRN_GET_PAPRELATIONS";
            cnn = new SqlConnection(con);

            PAP_RelationBO objRelation = null;
            PAPRelationList Relations = new PAPRelationList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", householdID);
            cmd.Parameters.AddWithValue("HOLDERTYPEID_", holderTypeID);            
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objRelation = new PAP_RelationBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PAP_HH_RELATIONID"))) objRelation.RelationID = dr.GetInt32(dr.GetOrdinal("PAP_HH_RELATIONID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objRelation.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOLDERNAME"))) objRelation.HolderName = dr.GetString(dr.GetOrdinal("HOLDERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RESIDEONAFFECTEDLAND"))) objRelation.ResideOnAffectedLand = dr.GetString(dr.GetOrdinal("RESIDEONAFFECTEDLAND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DATEOFBIRTH"))) objRelation.DateOfBirth = dr.GetDateTime(dr.GetOrdinal("DATEOFBIRTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LTR_STATUS"))) objRelation.LiteracyStatus = dr.GetString(dr.GetOrdinal("LTR_STATUS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUS"))) objRelation.CurrentSchoolStatus = dr.GetString(dr.GetOrdinal("CUR_SCH_STATUS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASON"))) objRelation.NeverAttendedSchool = dr.GetString(dr.GetOrdinal("NVR_ATT_SCH_REASON"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASON"))) objRelation.SchoolDropReason = dr.GetString(dr.GetOrdinal("SCH_DRP_REASON"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objRelation.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GENDER"))) objRelation.Sex = dr.GetString(dr.GetOrdinal("GENDER"));

                    Relations.Add(objRelation);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Relations;
        }

        /// <summary>
        /// To Get Relation By ID
        /// </summary>
        /// <param name="relationID"></param>
        /// <returns></returns>
        public PAP_RelationBO GetRelationByID(int relationID)
        {
            proc = "USP_TRN_GET_PAPRELATIONBYID";
            cnn = new SqlConnection(con);

            PAP_RelationBO objRelation = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PAP_HH_RELATIONID_", relationID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objRelation = new PAP_RelationBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PAP_HH_RELATIONID"))) objRelation.RelationID = dr.GetInt32(dr.GetOrdinal("PAP_HH_RELATIONID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objRelation.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOLDERNAME"))) objRelation.HolderName = dr.GetString(dr.GetOrdinal("HOLDERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RESIDEONAFFECTEDLAND"))) objRelation.ResideOnAffectedLand = dr.GetString(dr.GetOrdinal("RESIDEONAFFECTEDLAND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DATEOFBIRTH"))) objRelation.DateOfBirth = dr.GetDateTime(dr.GetOrdinal("DATEOFBIRTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LITERACYLEVELID"))) objRelation.LiteracyLevelID = dr.GetInt32(dr.GetOrdinal("LITERACYLEVELID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_SCH_STATUSID"))) objRelation.CurrentSchoolStatusID = dr.GetInt32(dr.GetOrdinal("CUR_SCH_STATUSID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASONID"))) objRelation.NeverAttendedSchoolID = dr.GetInt32(dr.GetOrdinal("NVR_ATT_SCH_REASONID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SCH_DRP_REASONID"))) objRelation.SchoolDropReasonID = dr.GetInt32(dr.GetOrdinal("SCH_DRP_REASONID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GENDER"))) objRelation.Sex = dr.GetString(dr.GetOrdinal("GENDER"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objRelation;
        }

        /// <summary>
        /// To Get Holder Types
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="holderTypeID"></param>
        /// <returns></returns>
        public HolderTypeList GetHolderTypes(int householdID, int holderTypeID)
        {
            proc = "USP_MST_GET_HOLDERTYPES";
            cnn = new SqlConnection(con);
            HolderTypeBO objHolderType = null;

            HolderTypeList HolderTypes = new HolderTypeList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);

            if (holderTypeID > 0)
                cmd.Parameters.AddWithValue("HOLDERTYPEID_", holderTypeID);
            else
                cmd.Parameters.AddWithValue("HOLDERTYPEID_", DBNull.Value);
            
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objHolderType = new HolderTypeBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("HOLDERTYPEID"))) objHolderType.HolderTypeID = dr.GetInt32(dr.GetOrdinal("HOLDERTYPEID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOLDERTYPE"))) objHolderType.HolderTypeName = dr.GetString(dr.GetOrdinal("HOLDERTYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOLDERCOUNT"))) objHolderType.HolderCount = dr.GetInt32(dr.GetOrdinal("HOLDERCOUNT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AFFECTEDHOLDERCOUNT"))) objHolderType.AffectedHolderCount = dr.GetInt32(dr.GetOrdinal("AFFECTEDHOLDERCOUNT"));

                    HolderTypes.Add(objHolderType);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return HolderTypes;
        }

        /// <summary>
        /// To Add Affected Land User
        /// </summary>
        /// <param name="objAffLandUser"></param>
        /// <returns></returns>
        public string AddAffectedLandUser(AffectedLandUserBO objAffLandUser)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_AFFECTEDLNDUSERS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objAffLandUser.HouseholdID);
            cmd.Parameters.AddWithValue("LANDUSERNAME_", objAffLandUser.LandUserName);
            cmd.Parameters.AddWithValue("STATUSID_", objAffLandUser.StatusID);
            cmd.Parameters.AddWithValue("RELATEDTO_", objAffLandUser.RelatedTo);
            cmd.Parameters.AddWithValue("TIMEONLAND_", objAffLandUser.TimeOnLand);
            cmd.Parameters.AddWithValue("CREATEDBY_", objAffLandUser.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Update Affected Land User
        /// </summary>
        /// <param name="objAffLandUser"></param>
        /// <returns></returns>
        public string UpdateAffectedLandUser(AffectedLandUserBO objAffLandUser)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_AFFECTEDLNDUSERS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("LANDUSERID_", objAffLandUser.LandUserID);
            cmd.Parameters.AddWithValue("HOUSEHOLDID_", objAffLandUser.HouseholdID);
            cmd.Parameters.AddWithValue("LANDUSERNAME_", objAffLandUser.LandUserName);
            cmd.Parameters.AddWithValue("STATUSID_", objAffLandUser.StatusID);
            cmd.Parameters.AddWithValue("RELATEDTO_", objAffLandUser.RelatedTo);
            cmd.Parameters.AddWithValue("TIMEONLAND_", objAffLandUser.TimeOnLand);
            cmd.Parameters.AddWithValue("UPDATEDBY_", objAffLandUser.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }

        /// <summary>
        /// To Delete Affected Land User
        /// </summary>
        /// <param name="landUserID"></param>
        /// <param name="updatedBy"></param>
        public void DeleteAffectedLandUser(int landUserID, int updatedBy)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_DEL_AFFECTEDLNDUSERS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("LANDUSERID_", landUserID);
           // cmd.Parameters.AddWithValue("UPDATEDBY_", updatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// To Get Affected Land Users
        /// </summary>
        /// <param name="landUserID"></param>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_AffectedLandUserList GetAffectedLandUsers(int landUserID, int householdID)
        {
            proc = "USP_TRN_GET_AFFECTEDLNDUSERS";
            cnn = new SqlConnection(con);
            AffectedLandUserBO objAffecLandUser = null;

            PAP_AffectedLandUserList AffecLandUserList = new PAP_AffectedLandUserList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (landUserID > 0)
                cmd.Parameters.AddWithValue("LANDUSERID_", landUserID);
            else
                cmd.Parameters.AddWithValue("LANDUSERID_", DBNull.Value);

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objAffecLandUser = new AffectedLandUserBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("LANDUSERID"))) objAffecLandUser.LandUserID = dr.GetInt32(dr.GetOrdinal("LANDUSERID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objAffecLandUser.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LANDUSERNAME"))) objAffecLandUser.LandUserName = dr.GetString(dr.GetOrdinal("LANDUSERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("STATUSID"))) objAffecLandUser.StatusID = dr.GetInt32(dr.GetOrdinal("STATUSID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("STATUSNAME"))) objAffecLandUser.StatusName = dr.GetString(dr.GetOrdinal("STATUSNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RELATEDTO"))) objAffecLandUser.RelatedTo = dr.GetString(dr.GetOrdinal("RELATEDTO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TIMEONLAND"))) objAffecLandUser.TimeOnLand = dr.GetString(dr.GetOrdinal("TIMEONLAND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objAffecLandUser.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                    AffecLandUserList.Add(objAffecLandUser);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AffecLandUserList;
        }

        #region "Service"
        /// <summary>
        /// To Get PAP Service Masters
        /// </summary>
        /// <returns></returns>
        public PAPServiceMasterList GetPAPServiceMasters()
        {
            proc = "USP_MST_GET_PAPSERVICES";
            cnn = new SqlConnection(con);
            PAPServiceMasterBO objServiceMaster = null;

            PAPServiceMasterList ServiceMasterList = new PAPServiceMasterList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objServiceMaster = new PAPServiceMasterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("SERVICEID"))) objServiceMaster.ServiceID = dr.GetInt32(dr.GetOrdinal("SERVICEID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SERVICENAME"))) objServiceMaster.ServiceName = dr.GetString(dr.GetOrdinal("SERVICENAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FIELDTYPE"))) objServiceMaster.FieldType = dr.GetString(dr.GetOrdinal("FIELDTYPE"));

                    ServiceMasterList.Add(objServiceMaster);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ServiceMasterList;
        }

        #endregion

        /// <summary>
        /// To Get PAP Services
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAPServicesList GetPAPServices(int householdID)
        {
            proc = "USP_TRN_GET_PAPSERVICES";
            cnn = new SqlConnection(con);
            PAPServiceBO objPAPService = null;

            PAPServicesList Services = new PAPServicesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPService = new PAPServiceBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPAPService.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SERVICEID"))) objPAPService.ServiceID = dr.GetInt32(dr.GetOrdinal("SERVICEID"));                    
                    if (!dr.IsDBNull(dr.GetOrdinal("FIELDVALUE"))) objPAPService.FieldValue = dr.GetString(dr.GetOrdinal("FIELDVALUE"));

                    Services.Add(objPAPService);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Services;
        }

        /// <summary>
        /// To Add PAP Service
        /// </summary>
        /// <param name="PAPServices"></param>
        public void AddPAPService(PAPServicesList PAPServices)
        {
            cnn = new SqlConnection(con);
            proc = "USP_TRN_UPD_PAPSERVICE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("HOUSEHOLDID_", "");
            cmd.Parameters.AddWithValue("SERVICEID_", "");
            cmd.Parameters.AddWithValue("FIELDVALUE_", "");
            cmd.Parameters.AddWithValue("UPDATEDBY_", "");

            foreach (PAPServiceBO objPAPService in PAPServices)
            {
                cmd.Parameters["HOUSEHOLDID_"].Value = objPAPService.HouseholdID;
                cmd.Parameters["SERVICEID_"].Value = objPAPService.ServiceID;
                cmd.Parameters["FIELDVALUE_"].Value = objPAPService.FieldValue;
                cmd.Parameters["UPDATEDBY_"].Value = objPAPService.UpdatedBy;
                cmd.ExecuteNonQuery();
            }
         
            cmd.Connection.Close();
        }
    }
}
