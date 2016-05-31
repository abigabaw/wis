using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ProjectList : List<ProjectBO>
    {
        public ProjectList() { }
    }

    // Edwin Baguma: Start
    public class ReportList : List<ProjectBO>
    {
        public ReportList() { }
    }
    // Edwin Baguma: end

    //public class ProjectRouteList : List<Project.Route>
    //{
    //    public ProjectRouteList() {}
    //}

    public class ProjectFinancierList : List<FinancierBO>
    {
        public ProjectFinancierList() { }
    }

    public class ProjectSegmentList : List<SegmentBO>
    {
        public ProjectSegmentList() { }
    }

    public class RouteSelectionFactorsList : List<RouteSelectionFactorsBO>
    {
        public RouteSelectionFactorsList() { }
    }

    public class RouteSelectionCriteriaList : List<RouteSelectionCriteriaBO>
    {
        public RouteSelectionCriteriaList() { }
    }

    public class RouteCriteriaScoreList : List<RouteCriteriaScoreBO>
    {
        public RouteCriteriaScoreList() { }
    }

    //public class ProjectRouteList : List<Project.Route.ProjectRoute>
    //{
    //    public ProjectRouteList() { }
    //}

    public class RouteScoreList : List<RouteScoreBO>
    {
        public RouteScoreList() { }
    }
    public class ProjectGeoList : List<GeographyBO>
    {
        public ProjectGeoList() { }
    }
}