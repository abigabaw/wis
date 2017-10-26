using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ProjectDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get Projects
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectStartDate"></param>
        /// <param name="projectEndDate"></param>
        /// <param name="projectStatus"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ProjectList GetProjects(string projectName, string projectStartDate, string projectEndDate, string projectStatus, int userID)
        {
            proc = "USP_TRN_GET_PROJECTS";
            cnn = new SqlConnection(con);
            ProjectBO oProject = null;

            ProjectList Projects = new ProjectList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("projectName_", projectName);

            if (projectStartDate != "")
                cmd.Parameters.AddWithValue("projectStartDate_", Convert.ToDateTime(projectStartDate).ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectStartDate_", DBNull.Value);

            if (projectEndDate != "")
                cmd.Parameters.AddWithValue("projectEndDate_", Convert.ToDateTime(projectEndDate).ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectEndDate_", DBNull.Value);

            cmd.Parameters.AddWithValue("projectStatus_", projectStatus);
            cmd.Parameters.AddWithValue("userID_", userID);

           // cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oProject = new ProjectBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) oProject.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) oProject.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) oProject.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectStartDate"))) oProject.ProjectStartDate = dr.GetDateTime(dr.GetOrdinal("ProjectStartDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectEndDate"))) oProject.ProjectEndDate = dr.GetDateTime(dr.GetOrdinal("ProjectEndDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectStatus"))) oProject.ProjectStatus = dr.GetString(dr.GetOrdinal("ProjectStatus"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FROZEN"))) oProject.Frozen = dr.GetString(dr.GetOrdinal("FROZEN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RouteCount"))) oProject.RouteCount = dr.GetInt32(dr.GetOrdinal("RouteCount"));

                    Projects.Add(oProject);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Projects;
        }

        /// <summary>
        /// To Get Project Names
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ProjectList GetProjectNames(int userID)
        {
            proc = "USP_TRN_GET_PROJECTNAMES";

            ProjectBO oProject = null;

            ProjectList Projects = new ProjectList();

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userID_", userID);
                 //   cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            oProject = new ProjectBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) oProject.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) oProject.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) oProject.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));

                            Projects.Add(oProject);
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return Projects;
        }

        /// <summary>
        /// To Add Project
        /// </summary>
        /// <param name="oProject"></param>
        /// <returns></returns>
        public string[] AddProject(ProjectBO oProject)
        {
            string[] result = { "0", "" };

            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_PROJECTDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectCode_", oProject.ProjectCode);
            cmd.Parameters.AddWithValue("projectName_", oProject.ProjectName);
            cmd.Parameters.AddWithValue("objective_", oProject.Objective);

            if (oProject.ProjectStartDate != DateTime.MinValue)
                cmd.Parameters.AddWithValue("projectStartDate_", oProject.ProjectStartDate.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectStartDate_", DBNull.Value);

            if (oProject.ProjectEndDate != DateTime.MinValue)
                cmd.Parameters.AddWithValue("projectEndDate_", oProject.ProjectEndDate.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectEndDate_", DBNull.Value);

            cmd.Parameters.AddWithValue("totalEstBudget_", oProject.TotalEstBudget);
            cmd.Parameters.AddWithValue("projectStatus_", oProject.ProjectStatus);
            cmd.Parameters.AddWithValue("createdBy_", oProject.CreatedBy);
            cmd.Parameters.AddWithValue("budgetCurrency_", oProject.BudgetCurrency);
            cmd.Parameters.AddWithValue("Labourcost_", oProject.Labourcost);
            cmd.Parameters.AddWithValue("BUILDINGMATCOST_", oProject.BUILDINGMATCOST);

            cmd.Parameters.AddWithValue("USHVALUE_", oProject.Dollervalue);
            cmd.Parameters.AddWithValue("percentagePAP_", oProject.PercentageofPAP);


            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.VarChar);//, 500).Direction = ParameterDirection.Output;

            SqlParameter param = new SqlParameter("newProjectID", SqlDbType.Int);//, ParameterDirection.Output);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                result[1] = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                result[1] = "";

            cmd.Connection.Close();

            result[0] = cmd.Parameters["newProjectID"].Value.ToString();

            return result;
        }

        /// <summary>
        /// To Get Project By ProjectID
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectBO GetProjectByProjectID(int projectID)
        {
            proc = "USP_TRN_GET_PROJECTBYPROJECTID";
            cnn = new SqlConnection(con);
            ProjectBO oProject = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("projectID_", projectID);

          //  cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oProject = new ProjectBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) oProject.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) oProject.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) oProject.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Objective"))) oProject.Objective = dr.GetString(dr.GetOrdinal("Objective"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectStartDate"))) oProject.ProjectStartDate = dr.GetDateTime(dr.GetOrdinal("ProjectStartDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectEndDate"))) oProject.ProjectEndDate = dr.GetDateTime(dr.GetOrdinal("ProjectEndDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TotalEstBudget"))) oProject.TotalEstBudget = dr.GetDecimal(dr.GetOrdinal("TotalEstBudget"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectStatus"))) oProject.ProjectStatus = dr.GetString(dr.GetOrdinal("ProjectStatus"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    {
                        if (dr.GetString(dr.GetOrdinal("IsDeleted")).ToUpper() == "FALSE")
                            oProject.IsDeleted = false;
                        else
                            oProject.IsDeleted = true;
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("CreatedBy"))) oProject.CreatedBy = dr.GetInt32(dr.GetOrdinal("CreatedBy"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UpdatedBy"))) oProject.UpdatedBy = dr.GetInt32(dr.GetOrdinal("UpdatedBy"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CreatedDate"))) oProject.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CreatedDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UpdatedDate"))) oProject.UpdatedDate = dr.GetDateTime(dr.GetOrdinal("UpdatedDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("budgetcurrency"))) oProject.BudgetCurrency = dr.GetInt32(dr.GetOrdinal("budgetcurrency"));
                    if (!dr.IsDBNull(dr.GetOrdinal("labourcost"))) oProject.Labourcost = dr.GetDecimal(dr.GetOrdinal("labourcost"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BUILDINGMATCOST"))) oProject.BUILDINGMATCOST = dr.GetDecimal(dr.GetOrdinal("BUILDINGMATCOST"));
                    if (!dr.IsDBNull(dr.GetOrdinal("USHVALUE"))) oProject.Dollervalue = dr.GetDecimal(dr.GetOrdinal("USHVALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("minperforpdp"))) oProject.PercentageofPAP = dr.GetDecimal(dr.GetOrdinal("minperforpdp"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oProject;
        }

        /// <summary>
        /// To Update Project
        /// </summary>
        /// <param name="oProject"></param>
        /// <returns></returns>
        public string UpdateProject(ProjectBO oProject)
        {
            string result = "";
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PROJECTDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectID_", oProject.ProjectID);
            cmd.Parameters.AddWithValue("projectCode_", oProject.ProjectCode);
            cmd.Parameters.AddWithValue("projectName_", oProject.ProjectName);
            cmd.Parameters.AddWithValue("objective_", oProject.Objective);

            if (oProject.ProjectStartDate != DateTime.MinValue)
                cmd.Parameters.AddWithValue("projectStartDate_", oProject.ProjectStartDate.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectStartDate_", DBNull.Value);

            if (oProject.ProjectEndDate != DateTime.MinValue)
                cmd.Parameters.AddWithValue("projectEndDate_", oProject.ProjectEndDate.ToString(UtilBO.DateFormatDB));
            else
                cmd.Parameters.AddWithValue("projectEndDate_", DBNull.Value);

            cmd.Parameters.AddWithValue("totalEstBudget_", oProject.TotalEstBudget);
            cmd.Parameters.AddWithValue("projectStatus_", oProject.ProjectStatus);
            cmd.Parameters.AddWithValue("updatedBy_", oProject.UpdatedBy);
            cmd.Parameters.AddWithValue("budgetCurrency_", oProject.BudgetCurrency);
            cmd.Parameters.AddWithValue("LABOURCOST_", oProject.Labourcost);
            cmd.Parameters.AddWithValue("BUILDINGMATCOST_", oProject.BUILDINGMATCOST);
            cmd.Parameters.AddWithValue("USHVALUE_", oProject.Dollervalue);
            cmd.Parameters.AddWithValue("percentagePAP_", oProject.PercentageofPAP);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.VarChar);//.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null && cmd.Parameters["errorMessage_"].Value.ToString().ToLower().Trim() != "null" && cmd.Parameters["errorMessage_"].Value.ToString().Trim() != "")
                result = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                result = "";

            cmd.Connection.Close();
            return result;
        }

        /// <summary>
        /// To Freeze Project
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="updatedBy"></param>
        public void FreezeProject(int projectID, int updatedBy)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_FREEZEPROJECT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectID_", projectID);
            cmd.Parameters.AddWithValue("updatedBy_", updatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// To Unfreeze Project
        /// </summary>
        /// <param name="oProjectBO"></param>
        /// <returns></returns>
        public string UnfreezeProject(ProjectBO oProjectBO)//(int projectID, int updatedBy)
        {
            string result = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_UNFREEZEPROJECT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectID_", oProjectBO.ProjectID);
            cmd.Parameters.AddWithValue("updatedBy_", oProjectBO.UnfreezeBy);

            if (oProjectBO.UnfreezeDate == DateTime.MinValue)
                cmd.Parameters.AddWithValue("UnfreezeDate_", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("UnfreezeDate_", oProjectBO.UnfreezeDate);

            cmd.Parameters.AddWithValue("UnfreezeComments_", oProjectBO.UnfreezeComments);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.VarChar);//, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                result = "";
            return result;
        }

        #region "Geography"
        /// <summary>
        /// To Add Project Geography
        /// </summary>
        /// <param name="oGeo"></param>
        public void AddProjectGeography(GeographyBO oGeo)
        {
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PROJECTGEOGRAPHY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("GEOGRAPHICALID_", oGeo.GeographicalID);
            cmd.Parameters.AddWithValue("projectID_", oGeo.ProjectID);
            cmd.Parameters.AddWithValue("generalDirection", oGeo.GeneralDirection);
            cmd.Parameters.AddWithValue("keyFeatures", oGeo.KeyFeatures);
            cmd.Parameters.AddWithValue("updatedBy_", oGeo.UpdatedBy);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// To Get Project Geography By Project ID
        /// </summary>
        /// <param name="GEOGRAPHICALID"></param>
        /// <returns></returns>
        public GeographyBO GetProjectGeographyByProjectID(int GEOGRAPHICALID)
        {
            proc = "USP_TRN_GET_PROJGEOGBYPROJID";
            cnn = new SqlConnection(con);
            GeographyBO oGeo = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("GEOGRAPHICALID_", GEOGRAPHICALID);

            //cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oGeo = new GeographyBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("GeographicalID"))) oGeo.GeographicalID = dr.GetInt32(dr.GetOrdinal("GeographicalID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) oGeo.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GeneralDirection"))) oGeo.GeneralDirection = dr.GetString(dr.GetOrdinal("GeneralDirection"));
                    if (!dr.IsDBNull(dr.GetOrdinal("KeyFeatures"))) oGeo.KeyFeatures = dr.GetString(dr.GetOrdinal("KeyFeatures"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    {
                        if (dr.GetString(dr.GetOrdinal("IsDeleted")).ToUpper() == "FALSE")
                            oGeo.IsDeleted = false;
                        else
                            oGeo.IsDeleted = true;
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("CreatedBy"))) oGeo.CreatedBy = dr.GetInt32(dr.GetOrdinal("CreatedBy"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UpdatedBy"))) oGeo.UpdatedBy = dr.GetInt32(dr.GetOrdinal("UpdatedBy"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CreatedDate"))) oGeo.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CreatedDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UpdatedDate"))) oGeo.UpdatedDate = dr.GetDateTime(dr.GetOrdinal("UpdatedDate"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oGeo;
        }

        /// <summary>
        /// To Get All Project Geography Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectGeoList GetAllProjectGeoDetails(int projectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_PROGEO_GETALL";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("projectID_", projectID);
          //  cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            GeographyBO GeographyBOObj = null;
            ProjectGeoList ProjectGeoListObj = new ProjectGeoList();
            GeographyBOObj = new GeographyBO();

            while (dr.Read())
            {
                GeographyBOObj = new GeographyBO();

                if (!dr.IsDBNull(dr.GetOrdinal("GEOGRAPHICALID")))
                    GeographyBOObj.GeographicalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GEOGRAPHICALID")));
                if (!dr.IsDBNull(dr.GetOrdinal("GENERALDIRECTION")))
                    GeographyBOObj.GeneralDirection = dr.GetString(dr.GetOrdinal("GENERALDIRECTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("KEYFEATURES")))
                    GeographyBOObj.KeyFeatures = dr.GetString(dr.GetOrdinal("KEYFEATURES"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    GeographyBOObj.IsDeleted = Convert.ToBoolean(dr.GetString(dr.GetOrdinal("ISDELETED")));

                ProjectGeoListObj.Add(GeographyBOObj);
            }
            dr.Close();
            return ProjectGeoListObj;
        }

        /// <summary>
        /// To Delete Project Geography Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string DeleteProjGeo(int projectID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DEL_PROJGEO", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("geographicalid_", projectID);
               // myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Geography details is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

        #endregion

        #region "Financier"

        /// <summary>
        /// To Add Project Financier
        /// </summary>
        /// <param name="objFin"></param>
        /// <returns></returns>
        public string AddProjectFinancier(FinancierBO objFin)
        {
            string result = "";

            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_PROJECTFINANCIER";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectID_", objFin.ProjectID);
            cmd.Parameters.AddWithValue("financierName_", objFin.FinancierName);
            cmd.Parameters.AddWithValue("createdBy_", objFin.CreatedBy);
           // cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("FINANCECONDITIONID", objFin.FINANCECONDITIONID);
            cmd.Parameters.AddWithValue("FINANCENATUREID", objFin.FINANCENATUREID);
            cmd.Parameters.AddWithValue("FINANCEREASONID", objFin.FINANCEREASONID);

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();

            cmd.Connection.Close();

            return result;
        }

        /// <summary>
        /// To Project Financier List
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectFinancierList GetProjectFinanciers(int projectID)
        {
            proc = "USP_TRN_GET_PROJFINANCIERS";
            cnn = new SqlConnection(con);
            FinancierBO objFinancier = null;

            ProjectFinancierList ProjectFinanciers = new ProjectFinancierList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("projectID_", projectID);
           // cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFinancier = new FinancierBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("FinancierID"))) objFinancier.FinancierID = dr.GetInt32(dr.GetOrdinal("FinancierID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) objFinancier.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FinancierName"))) objFinancier.FinancierName = dr.GetString(dr.GetOrdinal("FinancierName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) objFinancier.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financecondition"))) objFinancier.Finacecondition = dr.GetString(dr.GetOrdinal("financecondition"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financenature"))) objFinancier.Financenature = dr.GetString(dr.GetOrdinal("financenature"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financereason"))) objFinancier.Financereason = dr.GetString(dr.GetOrdinal("financereason"));

                    ProjectFinanciers.Add(objFinancier);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ProjectFinanciers;
        }

        /// <summary>
        /// To Get Project Financier By ID
        /// </summary>
        /// <param name="financierID"></param>
        /// <returns></returns>
        public FinancierBO GetProjectFinancierByID(int financierID)
        {
            proc = "USP_TRN_GET_PROJFINANCIERBYID";
            cnn = new SqlConnection(con);
            FinancierBO objFinancier = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("financierID_", financierID);
           // cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objFinancier = new FinancierBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("FinancierID"))) objFinancier.FinancierID = dr.GetInt32(dr.GetOrdinal("FinancierID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FinancierName"))) objFinancier.FinancierName = dr.GetString(dr.GetOrdinal("FinancierName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financeconditionid"))) objFinancier.FINANCECONDITIONID = dr.GetInt32(dr.GetOrdinal("financeconditionid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financenatureid"))) objFinancier.FINANCENATUREID = dr.GetInt32(dr.GetOrdinal("financenatureid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("financereasonid"))) objFinancier.FINANCEREASONID = dr.GetInt32(dr.GetOrdinal("financereasonid"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objFinancier;
        }

        /// <summary>
        /// To Delete Project For Finance
        /// </summary>
        /// <param name="ProjectFinanceID"></param>
        /// <returns></returns>
        public string DeleteProjectForFinance(int ProjectFinanceID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DEL_FINANCIER", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCIERID", ProjectFinanceID);
            //    myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Role is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Obsolete Project Finance
        /// </summary>
        /// <param name="ProjectFinanceID"></param>
        /// <param name="ISDELETED"></param>
        /// <returns></returns>
        public string ObsoleteProjectFinance(int ProjectFinanceID, string ISDELETED)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_OBSOLETE_PROJECT_FIN", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@F_FINANCIERID", ProjectFinanceID);
                myCommand.Parameters.AddWithValue("@isdeleted_", ISDELETED);
             //   myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Update Project Financier
        /// </summary>
        /// <param name="objFin"></param>
        /// <returns></returns>
        public string UpdateProjectFinancier(FinancierBO objFin)
        {
            string result = "";

            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PROJECTFINANCIER";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectID_", objFin.ProjectID);
            cmd.Parameters.AddWithValue("financierID_", objFin.FinancierID);
            cmd.Parameters.AddWithValue("financierName_", objFin.FinancierName);
            cmd.Parameters.AddWithValue("updatedBy", objFin.UpdatedBy);
          //  cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("F_FINANCECONDITIONID", objFin.FINANCECONDITIONID);
            cmd.Parameters.AddWithValue("F_FINANCENATUREID", objFin.FINANCENATUREID);
            cmd.Parameters.AddWithValue("F_FINANCEREASONID", objFin.FINANCEREASONID);

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                result = cmd.Parameters["errorMessage_"].Value.ToString();

            cmd.Connection.Close();

            return result;
        }

        #endregion

        #region "Segments"
        #region Get Record(s)
        /// <summary>
        /// To Get Project Segments
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ProjectSegmentList GetProjectSegments(int ProjectId)
        {
            proc = "USP_TRN_GET_PROJECTSEGMENTS";
            cnn = new SqlConnection(con);

            SegmentBO objProjectSegment = null;

            ProjectSegmentList ProjectSegments = new ProjectSegmentList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("projectid_", ProjectId);
           // cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objProjectSegment = new SegmentBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectSegmentID")))
                        objProjectSegment.ProjectSegmentID = dr.GetInt32(dr.GetOrdinal("ProjectSegmentID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SegmentName")))
                        objProjectSegment.SegmentName = dr.GetString(dr.GetOrdinal("SegmentName"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ROUTELENGTH")))
                        objProjectSegment.RouteLength = dr.GetString(dr.GetOrdinal("ROUTELENGTH"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LINETYPEID")))
                        objProjectSegment.LineTypeID = dr.GetInt32(dr.GetOrdinal("LINETYPEID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TYPEOFLINE")))
                        objProjectSegment.TypeofLine = dr.GetString(dr.GetOrdinal("TYPEOFLINE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ESTBUDGET")))
                        objProjectSegment.EstBudget = dr.GetDecimal(dr.GetOrdinal("ESTBUDGET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IMPLEMENTATIONPERIOD")))
                        objProjectSegment.ImplementationPeriod = dr.GetString(dr.GetOrdinal("IMPLEMENTATIONPERIOD"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CONSTRSTARTDATE")))
                    {
                        objProjectSegment.ConstrStartDate = dr.GetDateTime(dr.GetOrdinal("CONSTRSTARTDATE"));
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("CONSTRENDDATE")))
                    {
                        objProjectSegment.ConstrEndDate = dr.GetDateTime(dr.GetOrdinal("CONSTRENDDATE"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FUNDER")))
                    {
                        objProjectSegment.Funder = dr.GetString(dr.GetOrdinal("FUNDER"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKID")))
                    {
                        objProjectSegment.Bankid = dr.GetInt32(dr.GetOrdinal("BANKID"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("BankName")))
                    {
                        objProjectSegment.Bankname = dr.GetString(dr.GetOrdinal("BankName"));
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("rightofwaymeasurement")))
                        objProjectSegment.RightOfWay = dr.GetString(dr.GetOrdinal("rightofwaymeasurement"));

                    if (!dr.IsDBNull(dr.GetOrdinal("wayleavemeasurement")))
                        objProjectSegment.WayLeave = dr.GetString(dr.GetOrdinal("wayleavemeasurement"));

                    if (!dr.IsDBNull(dr.GetOrdinal("HOUSEVALUE")))
                        objProjectSegment.Valueofhouse = dr.GetDecimal(dr.GetOrdinal("HOUSEVALUE"));

                    ProjectSegments.Add(objProjectSegment);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ProjectSegments;
        }

        /// <summary>
        /// To Get Project Segment By ID
        /// </summary>
        /// <param name="ProjectSegmentId"></param>
        /// <returns></returns>
        public SegmentBO GetProjectSegmentByID(int ProjectSegmentId)
        {
            proc = "USP_TRN_GET_PROJSEGMENTS_BYID";
            cnn = new SqlConnection(con);

            SegmentBO objProjectSegment = null;

            ProjectSegmentList oProjectSegmentList = new ProjectSegmentList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("projectsegmentid_", ProjectSegmentId);
           //    cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                objProjectSegment = new SegmentBO();

                while (dr.Read())
                {

                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectSegmentID")))
                        objProjectSegment.ProjectSegmentID = dr.GetInt32(dr.GetOrdinal("ProjectSegmentID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SegmentName")))
                        objProjectSegment.SegmentName = dr.GetString(dr.GetOrdinal("SegmentName"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ROUTELENGTH")))
                        objProjectSegment.RouteLength = dr.GetString(dr.GetOrdinal("ROUTELENGTH"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LINETYPEID")))
                        objProjectSegment.LineTypeID = dr.GetInt32(dr.GetOrdinal("LINETYPEID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ESTBUDGET")))
                        objProjectSegment.EstBudget = dr.GetDecimal(dr.GetOrdinal("ESTBUDGET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IMPLEMENTATIONPERIOD")))
                        objProjectSegment.ImplementationPeriod = dr.GetString(dr.GetOrdinal("IMPLEMENTATIONPERIOD"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CONSTRSTARTDATE")))
                    {
                        objProjectSegment.ConstrStartDate = dr.GetDateTime(dr.GetOrdinal("CONSTRSTARTDATE"));
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("CONSTRENDDATE")))
                    {
                        objProjectSegment.ConstrEndDate = dr.GetDateTime(dr.GetOrdinal("CONSTRENDDATE"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FUNDER")))
                    {
                        objProjectSegment.Funder = dr.GetString(dr.GetOrdinal("FUNDER"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKID")))
                    {
                        objProjectSegment.Bankid = dr.GetInt32(dr.GetOrdinal("BANKID"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("BankName")))
                    {
                        objProjectSegment.Bankname = dr.GetString(dr.GetOrdinal("BankName"));
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("rightofwaymeasurement")))
                        objProjectSegment.RightOfWay = dr.GetString(dr.GetOrdinal("rightofwaymeasurement"));

                    if (!dr.IsDBNull(dr.GetOrdinal("wayleavemeasurement")))
                        objProjectSegment.WayLeave = dr.GetString(dr.GetOrdinal("wayleavemeasurement"));

                    if (!dr.IsDBNull(dr.GetOrdinal("HOUSEVALUE")))
                        objProjectSegment.Valueofhouse = dr.GetDecimal(dr.GetOrdinal("HOUSEVALUE"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objProjectSegment;
        }

        #endregion

        #region Save
        /// <summary>
        /// To Save Project Segment
        /// </summary>
        /// <param name="oProjectSegment"></param>
        /// <returns></returns>
        public string SaveProjectSegment(SegmentBO oProjectSegment)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_INS_PROJECTSEGMENTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("segmentname_", oProjectSegment.SegmentName);
            cmd.Parameters.AddWithValue("projectid_", oProjectSegment.ProjectID);
            cmd.Parameters.AddWithValue("routelength_", oProjectSegment.RouteLength);
            cmd.Parameters.AddWithValue("linetypeid_", oProjectSegment.LineTypeID);
            cmd.Parameters.AddWithValue("estbudget_", oProjectSegment.EstBudget);
            cmd.Parameters.AddWithValue("implementationperiod_", oProjectSegment.ImplementationPeriod);
            cmd.Parameters.AddWithValue("constrstartdate_", oProjectSegment.ConstrStartDate.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.AddWithValue("constrenddate_", oProjectSegment.ConstrEndDate.ToString(UtilBO.DateFormatDB));
            cmd.Parameters.AddWithValue("funder_", oProjectSegment.Funder);
            cmd.Parameters.AddWithValue("bankid_", oProjectSegment.Bankid);

            cmd.Parameters.AddWithValue("isdeleted_", oProjectSegment.IsDeleted);
            cmd.Parameters.AddWithValue("USERID_", oProjectSegment.CreatedBy);
            cmd.Parameters.AddWithValue("ValueofHouse_", oProjectSegment.Valueofhouse);
          //  cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Project Segment
        /// </summary>
        /// <param name="oProjectSegment"></param>
        /// <returns></returns>
        public string UpdateProjectSegment(SegmentBO oProjectSegment)
        {
            string returnResult = string.Empty;
            cnn = new SqlConnection(con);

            proc = "USP_TRN_UPD_PROJECTSEGMENTS";


            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("projectsegmentid_", oProjectSegment.ProjectSegmentID);
            cmd.Parameters.AddWithValue("segmentname_", oProjectSegment.SegmentName);
            cmd.Parameters.AddWithValue("projectid_", oProjectSegment.ProjectID);
            cmd.Parameters.AddWithValue("routelength_", oProjectSegment.RouteLength);
            cmd.Parameters.AddWithValue("linetypeid_", oProjectSegment.LineTypeID);
            cmd.Parameters.AddWithValue("estbudget_", oProjectSegment.EstBudget);
            cmd.Parameters.AddWithValue("implementationperiod_", oProjectSegment.ImplementationPeriod);
            cmd.Parameters.AddWithValue("constrstartdate_", oProjectSegment.ConstrStartDate);
            cmd.Parameters.AddWithValue("constrenddate_", oProjectSegment.ConstrEndDate);
            cmd.Parameters.AddWithValue("funder_", oProjectSegment.Funder);
            cmd.Parameters.AddWithValue("bankid_", oProjectSegment.Bankid);

            cmd.Parameters.AddWithValue("isdeleted_", oProjectSegment.IsDeleted);
            cmd.Parameters.AddWithValue("USERID_", oProjectSegment.CreatedBy);
            cmd.Parameters.AddWithValue("ValueofHouse_", oProjectSegment.Valueofhouse);
          //  cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        #endregion
        #endregion

        #region Route
        /// <summary>
        /// To Get Route Selection Factors
        /// </summary>
        /// <returns></returns>
        public RouteSelectionFactorsList GetRouteSelectionFactors()
        {
            proc = "USP_MST_GET_ROUTE_FACTOR";
            cnn = new SqlConnection(con);

            RouteSelectionFactorsBO oRouteSelectionFactors = null;

            RouteSelectionFactorsList lstRouteSelectionFactorsList = new RouteSelectionFactorsList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteSelectionFactors = new RouteSelectionFactorsBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("FactorId")))
                        oRouteSelectionFactors.FactorId = dr.GetInt32(dr.GetOrdinal("FactorId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FactorName")))
                        oRouteSelectionFactors.FactorName = dr.GetString(dr.GetOrdinal("FactorName"));

                    lstRouteSelectionFactorsList.Add(oRouteSelectionFactors);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRouteSelectionFactorsList;
        }

        /// <summary>
        /// To Get Route Selection Criteria
        /// </summary>
        /// <returns></returns>
        public RouteSelectionCriteriaList GetRouteSelectionCriteria()
        {
            proc = "USP_MST_GET_ROUTE_CRITERIA";
            cnn = new SqlConnection(con);

            RouteSelectionCriteriaBO oRouteSelectionCriteria = null;

            RouteSelectionCriteriaList lstRouteSelectionCriteriaList = new RouteSelectionCriteriaList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

      //      cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteSelectionCriteria = new RouteSelectionCriteriaBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CriteriaId")))
                        oRouteSelectionCriteria.CriteriaId = dr.GetInt32(dr.GetOrdinal("CriteriaId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FactorId")))
                        oRouteSelectionCriteria.FactorId = dr.GetInt32(dr.GetOrdinal("FactorId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CRITERIADESCRIPTION")))
                        oRouteSelectionCriteria.CriteriaDescription = dr.GetString(dr.GetOrdinal("CRITERIADESCRIPTION"));

                    lstRouteSelectionCriteriaList.Add(oRouteSelectionCriteria);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRouteSelectionCriteriaList;
        }

        /// <summary>
        /// To Get Route Selection Criteria By Factor Id
        /// </summary>
        /// <param name="FactorId"></param>
        /// <returns></returns>
        public RouteSelectionCriteriaList GetRouteSelectionCriteria_ByFactorId(int FactorId)
        {
            proc = "USP_MST_GET_ROUTE_CRITBYFACTID";
            cnn = new SqlConnection(con);

            RouteSelectionCriteriaBO oRouteSelectionCriteria = null;

            RouteSelectionCriteriaList lstRouteSelectionCriteriaList = new RouteSelectionCriteriaList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("factorid_", FactorId);
    //        cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteSelectionCriteria = new RouteSelectionCriteriaBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("CriteriaId")))
                        oRouteSelectionCriteria.CriteriaId = dr.GetInt32(dr.GetOrdinal("CriteriaId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FactorId")))
                        oRouteSelectionCriteria.FactorId = dr.GetInt32(dr.GetOrdinal("FactorId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CRITERIADESCRIPTION")))
                        oRouteSelectionCriteria.CriteriaDescription = dr.GetString(dr.GetOrdinal("CRITERIADESCRIPTION"));

                    lstRouteSelectionCriteriaList.Add(oRouteSelectionCriteria);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRouteSelectionCriteriaList;
        }

        /// <summary>
        /// To Get Route Criteria Score
        /// </summary>
        /// <returns></returns>
        public RouteCriteriaScoreList GetRouteCriteriaScore()
        {
            proc = "USP_MST_GET_CRITERIA_SCORE";
            cnn = new SqlConnection(con);

            RouteCriteriaScoreBO oRouteCriteriaScore = null;

            RouteCriteriaScoreList lstRouteCriteriaScoreList = new RouteCriteriaScoreList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

   //         cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteCriteriaScore = new RouteCriteriaScoreBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("ScoreId")))
                        oRouteCriteriaScore.ScoreId = dr.GetInt32(dr.GetOrdinal("ScoreId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SCOREDESCRIPTION")))
                        oRouteCriteriaScore.ScoreDescription = dr.GetString(dr.GetOrdinal("SCOREDESCRIPTION"));

                    lstRouteCriteriaScoreList.Add(oRouteCriteriaScore);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRouteCriteriaScoreList;
        }


        //SAVE PROJECT ROUTE IDENTIFICATION
        /// <summary>
        /// To Save Route Score
        /// </summary>
        /// <param name="oRouteScore"></param>
        /// <returns></returns>
        public int SaveRouteScore(RouteScoreBO oRouteScore)
        {
            int returnResult;

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand("USP_TRN_INS_ROUTE_SCORE", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("routeid_", oRouteScore.RouteId);
                    cmd.Parameters.AddWithValue("criteriaid_", oRouteScore.CriteriaId);
                    cmd.Parameters.AddWithValue("scoreid_", oRouteScore.ScoreId);

                    cmd.Parameters.AddWithValue("isdeleted_", oRouteScore.IsDeleted);
                    cmd.Parameters.AddWithValue("USERID_", oRouteScore.UserId);

                    returnResult = cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                }
            }

            return returnResult;
        }

        /// <summary>
        /// To Get Route Score
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="criteriaID"></param>
        /// <returns></returns>
        public RouteScoreBO GetRouteScore(int routeID, int criteriaID)
        {
            proc = "USP_TRN_GET_ROUTE_SCORE";
            cnn = new SqlConnection(con);

            RouteScoreBO oRouteScore = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ROUTEID_", routeID);
            cmd.Parameters.AddWithValue("CRITERIAID_", criteriaID);
        //    cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteScore = new RouteScoreBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("RouteScoreId")))
                        oRouteScore.RouteScoreId = dr.GetInt32(dr.GetOrdinal("RouteScoreId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RouteId")))
                        oRouteScore.RouteId = dr.GetInt32(dr.GetOrdinal("RouteId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CriteriaId")))
                        oRouteScore.CriteriaId = dr.GetInt32(dr.GetOrdinal("CriteriaId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FactorId")))
                        oRouteScore.FactorId = dr.GetInt32(dr.GetOrdinal("FactorId"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ScoreId")))
                        oRouteScore.ScoreId = dr.GetInt32(dr.GetOrdinal("ScoreId"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRouteScore;
        }

        #region Total Route Score
        public RouteBO getTotalRouteScore(int ProjectID)
        {
            proc = "USP_TRN_GET_PROJROUTE_TOTSCORE";
            cnn = new SqlConnection(con);

            RouteBO oRouteBO = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ProjectID_", ProjectID);
       //     cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oRouteBO = new RouteBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("TotalRouteScore")))
                        oRouteBO.TotalRouteScore = dr.GetInt32(dr.GetOrdinal("TotalRouteScore"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRouteBO;
        }

        public string SaveToalRouteScore(RouteBO oRouteBO)
        {
            cnn = new SqlConnection(con);
            string returnMessage = string.Empty;

            proc = "USP_TRN_UPD_PROJROUTE_TOTSCORE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("RouteID_", oRouteBO.RouteID);
            cmd.Parameters.AddWithValue("TotalRouteScore_", oRouteBO.TotalRouteScore);

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

            return returnMessage;
        }
        #endregion
        #endregion

        #region Frozen
        public ProjectBO getFrozen(ProjectBO ObjProjectBO)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_CHECKFROZEN";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("ProjectIdIN_", ObjProjectBO.ProjectID);
          //  cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ProjectBO objProjectBO = null;
            ProjectList ProjectList = new ProjectList();
            while (dr.Read())
            {
                objProjectBO = new ProjectBO();
                objProjectBO.Frozen = dr.GetString(dr.GetOrdinal("FROZEN"));
            }
            dr.Close();
            return objProjectBO;
        }
        #endregion Frozen


        // Edwin Baguma: Start
        public ReportList GetLegacyReports()
        {


            proc = "USP_TRN_GET_LEGACYRPTS";

            ProjectBO oProject = null;

            ReportList Projects = new ReportList();

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                 //   cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            oProject = new ProjectBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("RPT_ID"))) oProject.ReportID = dr.GetInt32(dr.GetOrdinal("RPT_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("RPT_CODE"))) oProject.ReportCode = dr.GetString(dr.GetOrdinal("RPT_CODE"));
                            if (!dr.IsDBNull(dr.GetOrdinal("RPT_NAME"))) oProject.ReportName = dr.GetString(dr.GetOrdinal("RPT_NAME"));
                            if (!dr.IsDBNull(dr.GetOrdinal("RPT_FILE"))) oProject.ReportFile = dr.GetString(dr.GetOrdinal("RPT_FILE"));


                            Projects.Add(oProject);
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return Projects;
        }
        // Edwin Baguma: End

        public string GetLegacyReportByID(int reportID)
        {
            proc = "USP_TRN_GET_LEGACYRPTS_BYID";
            cnn = new SqlConnection(con);
            string ReportName = "";
            //ProjectBO oReport = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("RPT_ID_", reportID);

         //   cmd.Parameters.AddWithValue("Sp_recordset", SqlDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //oReport = new ProjectBO();



                while (dr.Read())
                {
                    //oReport = new ProjectBO();
                    //if (!dr.IsDBNull(dr.GetOrdinal("RPT_FILE"))) oReport.ReportFile = dr.GetString(dr.GetOrdinal("RPT_FILE"));
                    ReportName = dr.GetString(dr.GetOrdinal("RPT_FILE"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ReportName;
        }
    }
}

