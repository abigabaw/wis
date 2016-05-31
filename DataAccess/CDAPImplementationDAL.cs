using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data;

namespace WIS_DataAccess
{
    public class CDAPImplementationDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To Add CDAP Phase to database
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public string AddCDAPPhase(CDAPImplementationBO objCDAPImplementationBO)
        {
            cnn = new OracleConnection(con);
            
            string result = "";
            
            proc = "USP_TRN_CDAP_PHASE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("CDAP_PHASEID_", objCDAPImplementationBO.Cdap_phaseid);
            cmd.Parameters.Add("CDAP_PHASENO_", objCDAPImplementationBO.Cdap_phaseno);
            cmd.Parameters.Add("CDAP_PERIODFROM_", objCDAPImplementationBO.PeriodFrom.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.Add("CDAP_PERIODTO_", objCDAPImplementationBO.PeriodTo.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.Add("PROJECTID_", objCDAPImplementationBO.ProjectedId);
            cmd.Parameters.Add("UPDATEDBY_", objCDAPImplementationBO.Updatedby);
            cmd.Parameters.Add("EXPENDITURE_", objCDAPImplementationBO.EXPENDITURE);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                result = string.Empty;
            //result = cmd.Parameters["NEWPHASEID_"].Value.ToString();
            cmd.Connection.Close();

            return result;
        }
        /// <summary>
        /// To Delete Phase details By ID
        /// </summary>
        /// <param name="PhaseID"></param>
        public void DeletePhasedetailsByID(int PhaseID)
        {
            try
            {
                cnn = new OracleConnection(con);
                cmd = new OracleCommand("USP_DEL_TRN_CDAP_PHASE", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CDAP_PHASEID_", PhaseID);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }
        /// <summary>
        /// To Get CDAP Phase Details By ID
        /// </summary>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseDetailsByID(int PhaseID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_TRN_CDAP_PHASESBYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CDAP_PHASEID_", PhaseID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASENO"))) obCDAPImplementationBO.Cdap_phaseno = dr.GetInt32(dr.GetOrdinal("CDAP_PHASENO"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEID"))) obCDAPImplementationBO.Cdap_phaseid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODFROM"))) obCDAPImplementationBO.PeriodFrom = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODFROM"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODTO"))) obCDAPImplementationBO.PeriodTo = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODTO"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) obCDAPImplementationBO.ProjectedId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("EXPENDITURE"))) obCDAPImplementationBO.EXPENDITURE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("EXPENDITURE")));
                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;
        }
        /// <summary>
        /// To Get CDAP Phase Details
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseDetails(int ProjectID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_TRN_CDAP_PHASES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PROJECTID_", ProjectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASENO"))) obCDAPImplementationBO.Cdap_phaseno = dr.GetInt32(dr.GetOrdinal("CDAP_PHASENO"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEID"))) obCDAPImplementationBO.Cdap_phaseid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODFROM"))) obCDAPImplementationBO.PeriodFrom = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODFROM"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODTO"))) obCDAPImplementationBO.PeriodTo = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODTO"));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) obCDAPImplementationBO.ProjectedId = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                if (!dr.IsDBNull(dr.GetOrdinal("EXPENDITURE"))) obCDAPImplementationBO.EXPENDITURE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("EXPENDITURE")));
                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;
        }
        /// <summary>
        /// To Get CDAP Phase ID
        /// </summary>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseID()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_CDAP_PHASEID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEID"))) obCDAPImplementationBO.Cdap_phaseid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEID"));
                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;

        }
        /// <summary>
        /// To Get CDAP Phases details
        /// </summary>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhases()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_CDAP_PHASES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEID"))) obCDAPImplementationBO.Cdap_phaseid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEID"));
                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;

        }
        /// <summary>
        /// To Add CDAP Phase Activity details
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public int AddCDAPPhaseActivity(CDAPImplementationBO objCDAPImplementationBO)
        {
            cnn = new OracleConnection(con);
            int returnResult = 0;
            proc = "USP_TRN_CDAP_PHASE_ACTIVITY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("CDAP_PHASEACTIVITYID_", objCDAPImplementationBO.Cdap_phaseactivityid);
            cmd.Parameters.Add("CDAP_PHASEID_", objCDAPImplementationBO.Cdap_phaseid);
            cmd.Parameters.Add("CDAP_ACTIVITYID_", objCDAPImplementationBO.Cdap_activityid);
            cmd.Parameters.Add("DISTRICT_", objCDAPImplementationBO.District);
            cmd.Parameters.Add("COUNTY_", objCDAPImplementationBO.County);
            cmd.Parameters.Add("SUBCOUNTY_", objCDAPImplementationBO.SubCounty);
            cmd.Parameters.Add("VILLAGES_", objCDAPImplementationBO.Village);
            cmd.Parameters.Add("ACTIVITYDETAILS_", objCDAPImplementationBO.Activitydetails);
            cmd.Parameters.Add("MODEOFIMPLEMENTATION_", objCDAPImplementationBO.Modeofimplementation);
            cmd.Parameters.Add("CHALLENGES_", objCDAPImplementationBO.Challenges);
            cmd.Parameters.Add("COMMENTS_", objCDAPImplementationBO.Comments);
            cmd.Parameters.Add("ACTIVITYDATEFROM_", objCDAPImplementationBO.Activitydatefrom.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.Add("ACTIVITYDATETO_", objCDAPImplementationBO.Activitydateto.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.Add("UPDATEDBY_", objCDAPImplementationBO.Updatedby);

            returnResult = cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To Get CDAP Phase Activity by ID
        /// </summary>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseActivityID()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_CDAP_PHASE_ACTIVITYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEACTIVITYID"))) obCDAPImplementationBO.Cdap_phaseactivityid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEACTIVITYID"));

                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;

        }
        /// <summary>
        /// To Add CDAP Activity PAPS details
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public int AddCDAPActivityPAPS(CDAPImplementationBO objCDAPImplementationBO)
        {
            cnn = new OracleConnection(con);
            int returnResult = 0;
            proc = "USP_CDAP_ACTIVITY_PAPS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("CDAP_PHASEACTIVITYID_", objCDAPImplementationBO.Cdap_phaseactivityid);
            cmd.Parameters.Add("HHID_", objCDAPImplementationBO.HhId);
            cmd.Parameters.Add("UPDATEDBY_", objCDAPImplementationBO.Updatedby);
            returnResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To Get CDAP Phase Activity Details
        /// </summary>
        /// <param name="prjctID"></param>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseActivityDetails(int prjctID, int PhaseID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_PHASEACTIVITY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PROJECTID_", prjctID);
            cmd.Parameters.Add("PHASEID_", PhaseID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASENO"))) obCDAPImplementationBO.Cdap_phaseno = dr.GetInt32(dr.GetOrdinal("CDAP_PHASENO"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODFROM"))) obCDAPImplementationBO.PeriodFrom = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODFROM"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PERIODTO"))) obCDAPImplementationBO.PeriodTo = dr.GetDateTime(dr.GetOrdinal("CDAP_PERIODTO"));
                //if (!dr.IsDBNull(dr.GetOrdinal("projectid"))) obCDAPImplementationBO.PeriodTo = dr.get(dr.GetOrdinal("projectid"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_PHASEACTIVITYID"))) obCDAPImplementationBO.Cdap_phaseactivityid = dr.GetInt32(dr.GetOrdinal("CDAP_PHASEACTIVITYID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_ACTIVITYNAME"))) obCDAPImplementationBO.Cdap_activityname = dr.GetString(dr.GetOrdinal("CDAP_ACTIVITYNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICT"))) obCDAPImplementationBO.District = dr.GetString(dr.GetOrdinal("DISTRICT"));
                if (!dr.IsDBNull(dr.GetOrdinal("COUNTY"))) obCDAPImplementationBO.County = dr.GetString(dr.GetOrdinal("COUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY"))) obCDAPImplementationBO.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));
                if (!dr.IsDBNull(dr.GetOrdinal("VILLAGES"))) obCDAPImplementationBO.Village = dr.GetString(dr.GetOrdinal("VILLAGES"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydatefrom"))) obCDAPImplementationBO.Activitydatefrom = dr.GetDateTime(dr.GetOrdinal("activitydatefrom"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydateto"))) obCDAPImplementationBO.Activitydateto = dr.GetDateTime(dr.GetOrdinal("activitydateto"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydetails"))) obCDAPImplementationBO.Activitydetails = dr.GetString(dr.GetOrdinal("activitydetails"));
                if (!dr.IsDBNull(dr.GetOrdinal("modeofimplementation"))) obCDAPImplementationBO.Modeofimplementation = dr.GetString(dr.GetOrdinal("modeofimplementation"));
                if (!dr.IsDBNull(dr.GetOrdinal("challenges"))) obCDAPImplementationBO.Challenges = dr.GetString(dr.GetOrdinal("challenges"));
                if (!dr.IsDBNull(dr.GetOrdinal("comments"))) obCDAPImplementationBO.Comments = dr.GetString(dr.GetOrdinal("comments"));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAMES"))) obCDAPImplementationBO.PapNames = dr.GetString(dr.GetOrdinal("PAPNAMES"));

                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;
        }
        /// <summary>
        /// To Get CDAP PAP Details
        /// </summary>
        /// <param name="PhaseactivityID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPAPDetails(int PhaseactivityID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_CDAPPLANDISPLAY";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CDAP_PHASEACTIVITYID_", PhaseactivityID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {

                obCDAPImplementationBO = new CDAPImplementationBO();
              //  if (!dr.IsDBNull(dr.GetOrdinal("cdap_phaseid"))) obCDAPImplementationBO.Cdap_phaseid = dr.GetInt32(dr.GetOrdinal("cdap_phaseid"));
              //  if (!dr.IsDBNull(dr.GetOrdinal("cdap_phaseno"))) obCDAPImplementationBO.Cdap_phaseno = dr.GetInt32(dr.GetOrdinal("cdap_phaseno"));
             //   if (!dr.IsDBNull(dr.GetOrdinal("cdap_periodfrom"))) obCDAPImplementationBO.PeriodFrom = dr.GetDateTime(dr.GetOrdinal("cdap_periodfrom"));
              //  if (!dr.IsDBNull(dr.GetOrdinal("cdap_periodto"))) obCDAPImplementationBO.PeriodTo = dr.GetDateTime(dr.GetOrdinal("cdap_periodto"));
                if (!dr.IsDBNull(dr.GetOrdinal("cdap_activityid"))) obCDAPImplementationBO.Cdap_activityid = dr.GetInt32(dr.GetOrdinal("cdap_activityid"));
                if (!dr.IsDBNull(dr.GetOrdinal("district"))) obCDAPImplementationBO.District = dr.GetString(dr.GetOrdinal("district"));
                if (!dr.IsDBNull(dr.GetOrdinal("county"))) obCDAPImplementationBO.County = dr.GetString(dr.GetOrdinal("county"));
                if (!dr.IsDBNull(dr.GetOrdinal("subcounty"))) obCDAPImplementationBO.SubCounty = dr.GetString(dr.GetOrdinal("subcounty"));
                if (!dr.IsDBNull(dr.GetOrdinal("villages"))) obCDAPImplementationBO.Village = dr.GetString(dr.GetOrdinal("villages"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydetails"))) obCDAPImplementationBO.Activitydetails = dr.GetString(dr.GetOrdinal("activitydetails"));
                if (!dr.IsDBNull(dr.GetOrdinal("modeofimplementation"))) obCDAPImplementationBO.Modeofimplementation = dr.GetString(dr.GetOrdinal("modeofimplementation"));
                if (!dr.IsDBNull(dr.GetOrdinal("challenges"))) obCDAPImplementationBO.Challenges = dr.GetString(dr.GetOrdinal("challenges"));
                if (!dr.IsDBNull(dr.GetOrdinal("comments"))) obCDAPImplementationBO.Comments = dr.GetString(dr.GetOrdinal("comments"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydatefrom"))) obCDAPImplementationBO.Activitydatefrom = dr.GetDateTime(dr.GetOrdinal("activitydatefrom"));
                if (!dr.IsDBNull(dr.GetOrdinal("activitydateto"))) obCDAPImplementationBO.Activitydateto = dr.GetDateTime(dr.GetOrdinal("activitydateto"));
                if (!dr.IsDBNull(dr.GetOrdinal("hhid"))) obCDAPImplementationBO.HhId = dr.GetInt32(dr.GetOrdinal("hhid"));
                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;
        }
        /// <summary>
        /// To Get CDAP Village by ID
        /// </summary>
        /// <param name="village"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPVillageID(string village)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_VILLAGEID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("VILLAGES_", village);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPImplementationBO obCDAPImplementationBO = null;
            CDAPImplementationList objCDAPImplementationList = new CDAPImplementationList();
            while (dr.Read())
            {
                obCDAPImplementationBO = new CDAPImplementationBO();
                if (!dr.IsDBNull(dr.GetOrdinal("VillageID"))) obCDAPImplementationBO.Villageid = dr.GetInt32(dr.GetOrdinal("VillageID"));

                objCDAPImplementationList.Add(obCDAPImplementationBO);
            }
            dr.Close();
            return objCDAPImplementationList;
        }
    }
}
