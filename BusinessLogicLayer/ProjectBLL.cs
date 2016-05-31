using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ProjectBLL
    {
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
            return (new ProjectDAL()).GetProjects(projectName, projectStartDate, projectEndDate, projectStatus, userID);
        }

        /// <summary>
        /// To Get Project Names
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ProjectList GetProjectNames(int userID)
        {
            return (new ProjectDAL()).GetProjectNames(userID);
        }

        // Edwin Baguma: Start
        public ReportList GetLegacyReports()
        {
            return (new ProjectDAL()).GetLegacyReports();
        }
        // Edwin: End


        /// <summary>
        /// To Get Project By ProjectID
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectBO GetProjectByProjectID(int projectID)
        {
            return (new ProjectDAL()).GetProjectByProjectID(projectID);
        }

        /// <summary>
        /// To Add Project
        /// </summary>
        /// <param name="oProject"></param>
        /// <returns></returns>
        public string[] AddProject(ProjectBO oProject)
        {
            ProjectDAL objProjectDAL = new ProjectDAL();
            return objProjectDAL.AddProject(oProject);
        }

        /// <summary>
        /// To Update Project
        /// </summary>
        /// <param name="objProject"></param>
        /// <returns></returns>
        public string UpdateProject(ProjectBO objProject)
        {
            return (new ProjectDAL()).UpdateProject(objProject);
        }

        /// <summary>
        /// To Freeze Project
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="updatedBy"></param>
        public void FreezeProject(int projectID, int updatedBy)
        {
            (new ProjectDAL()).FreezeProject(projectID, updatedBy);
        }

        /// <summary>
        /// To Unfreeze Project
        /// </summary>
        /// <param name="OProjectBO"></param>
        /// <returns></returns>
        public string UnfreezeProject(ProjectBO OProjectBO)
        {
            return (new ProjectDAL()).UnfreezeProject(OProjectBO);
        }

        #region "Geography"
        /// <summary>
        /// To Add Project Geography
        /// </summary>
        /// <param name="oGeo"></param>
        public void AddProjectGeography(GeographyBO oGeo)
        {
            (new ProjectDAL()).AddProjectGeography(oGeo);
        }

        /// <summary>
        /// To Get Project Geography By Project ID
        /// </summary>
        /// <param name="GEOGRAPHICALID"></param>
        /// <returns></returns>
        public GeographyBO GetProjectGeographyByProjectID(int GEOGRAPHICALID)
        {
            return (new ProjectDAL()).GetProjectGeographyByProjectID(GEOGRAPHICALID);
        }

        /// <summary>
        /// To Get All Project Geography Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectGeoList GetAllProjectGeoDetails(int projectID)
        {
            ProjectDAL ProjectDALDALObj = new ProjectDAL();
            return ProjectDALDALObj.GetAllProjectGeoDetails(projectID);
        }

        /// <summary>
        /// To Delete Project Geography Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string DeleteProjGeo(int projectID)
        {
            ProjectDAL ProjectDALDALObj = new ProjectDAL();
            return ProjectDALDALObj.DeleteProjGeo(projectID);
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
            return (new ProjectDAL()).AddProjectFinancier(objFin);
        }

        /// <summary>
        /// To Get Project Financier
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectFinancierList GetProjectFinanciers(int projectID)
        {
            return (new ProjectDAL()).GetProjectFinanciers(projectID);
        }

        /// <summary>
        /// To Get Project Financier By ID
        /// </summary>
        /// <param name="financierID"></param>
        /// <returns></returns>
        public FinancierBO GetProjectFinancierByID(int financierID)
        {
            return (new ProjectDAL()).GetProjectFinancierByID(financierID);
        }

        //public void DeleteProjectFinancier(int financierID)
        //{
        //    (new ProjectDAL()).DeleteProjectFinancier(financierID);
        //}
        /// <summary>
        /// To Update Project Financier
        /// </summary>
        /// <param name="objFin"></param>
        /// <returns></returns>
        public string UpdateProjectFinancier(FinancierBO objFin)
        {
            return (new ProjectDAL()).UpdateProjectFinancier(objFin);
        }

        //public void ObsoleteFinancier(int financierID, string isObsolete)
        //{
        //    ProjectDAL objProjectDAL = new ProjectDAL();
        //    objProjectDAL.ObsoleteFinancier(financierID, isObsolete);
        //}

        #endregion

        #region Segment
        #region Get Project Segment Record(s)
        /// <summary>
        /// To Get Project Segment By ID
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ProjectSegmentList GetProjectSegment(int ProjectId)//(RoofType oRoofType)
        {
            ProjectDAL objProjectDAL = new ProjectDAL();
            return objProjectDAL.GetProjectSegments(ProjectId);// (oRoofType);
        }

        public SegmentBO GetProjectSegmentByID(int ProjectSegmentId)
        {
            ProjectDAL objProjectDAL = new ProjectDAL();
            return objProjectDAL.GetProjectSegmentByID(ProjectSegmentId);// (oRoofType);
        }
        #endregion


        #region Save/Update Segment
        /// <summary>
        /// To Save Project Segment
        /// </summary>
        /// <param name="objProject"></param>
        public void SaveProjectSegment(ProjectBO objProject)
        {
            (new ProjectDAL()).UpdateProject(objProject);
        }

        /// <summary>
        /// To Save Project Segment
        /// </summary>
        /// <param name="oProjectSegment"></param>
        /// <returns></returns>
        public string SaveProjectSegment(SegmentBO oProjectSegment)
        {
            return (new ProjectDAL()).SaveProjectSegment(oProjectSegment);
        }

        /// <summary>
        /// To Update Project Segment
        /// </summary>
        /// <param name="oProjectSegment"></param>
        /// <returns></returns>
        public string UpdateProjectSegment(SegmentBO oProjectSegment)
        {
            return (new ProjectDAL()).UpdateProjectSegment(oProjectSegment);
        }

        #endregion

        #endregion

        #region Route
        /// <summary>
        /// To Get Route Selection Factors
        /// </summary>
        /// <returns></returns>
        public RouteSelectionFactorsList getRouteSelectionFactors()
        {
            return (new ProjectDAL()).GetRouteSelectionFactors();
        }

        /// <summary>
        /// To Get Route Selection Criteria
        /// </summary>
        /// <returns></returns>
        public RouteSelectionCriteriaList getRouteSelectionCriteria()
        {
            return (new ProjectDAL()).GetRouteSelectionCriteria();
        }

        /// <summary>
        /// To Get Route Criteria ByFactorId
        /// </summary>
        /// <param name="FactorId"></param>
        /// <returns></returns>
        public RouteSelectionCriteriaList GetRouteSelectionCriteria_ByFactorId(int FactorId)
        {
            return (new ProjectDAL()).GetRouteSelectionCriteria_ByFactorId(FactorId);
        }

        /// <summary>
        /// To Get Route Criteria Score
        /// </summary>
        /// <returns></returns>
        public RouteCriteriaScoreList GetRouteCriteriaScore()
        {
            return (new ProjectDAL()).GetRouteCriteriaScore();
        }

        //public ProjectRouteList GetProjectRoute_ByProjectId(int ProjectId)
        //{
        //    return (new ProjectDAL()).GetProjectRoute_ByProjectId(ProjectId);
        //}

        /// <summary>
        /// To Save Route Score
        /// </summary>
        /// <param name="oRouteScore"></param>
        /// <returns></returns>
        public int SaveRouteScore(RouteScoreBO oRouteScore)
        {
            return (new ProjectDAL()).SaveRouteScore(oRouteScore);
        }

        /// <summary>
        /// To Get Route Score
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="criteriaID"></param>
        /// <returns></returns>
        public RouteScoreBO GetRouteScore(int routeID, int criteriaID)
        {
            return (new ProjectDAL()).GetRouteScore(routeID, criteriaID);
        }

        #region Total Route Score
        public RouteBO getTotalRouteScore(int ProjectID)
        {
            return (new ProjectDAL()).getTotalRouteScore(ProjectID);
        }

        public string SaveToalRouteScore(RouteBO oRouteBO)
        {
            return (new ProjectDAL()).SaveToalRouteScore(oRouteBO);
        }

        #endregion
        #endregion

        /// <summary>
        /// To Delete Project For Finance
        /// </summary>
        /// <param name="ProjectFinanceID"></param>
        /// <returns></returns>
        public string DeleteProjectForFinance(int ProjectFinanceID)
        {
            ProjectDAL objProjectDAL = new ProjectDAL(); //Data pass -to Database Layer
            return objProjectDAL.DeleteProjectForFinance(ProjectFinanceID);
        }

        /// <summary>
        /// To Obsolete Project Finance
        /// </summary>
        /// <param name="ProjectFinanceId"></param>
        /// <param name="ISDELETED"></param>
        /// <returns></returns>
        public string ObsoleteProjectFinance(int ProjectFinanceId, string ISDELETED)
        {
            ProjectDAL objProjectDAL = new ProjectDAL();//Data pass -to Database Layer
            return objProjectDAL.ObsoleteProjectFinance(ProjectFinanceId, ISDELETED);
        }

        /// <summary>
        /// To get Frozen
        /// </summary>
        /// <param name="ObjProjectBO"></param>
        /// <returns></returns>
        public ProjectBO getFrozen(ProjectBO ObjProjectBO)
        {
            ProjectDAL ProjectDALObj = new ProjectDAL();
            return ProjectDALObj.getFrozen(ObjProjectBO);
        }


        public string GetLegacyReportByID(int reportID)
        {
            return (new ProjectDAL()).GetLegacyReportByID(reportID);
        }
    }
}