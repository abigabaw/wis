using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Data;

namespace WIS_BusinessLogic
{
    public class RouteCoordinatesBLL
    {
        /// <summary>
        /// To Get Route Coordinates
        /// </summary>
        /// <param name="RouteID"></param>
        /// <returns></returns>
        public RouteCoordinatesList GetRouteCoordinates(string RouteID)
        {
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
            return objRouteCoordinatesDAL.GetRouteCoordinates(RouteID);
        }

        /// <summary>
        /// To Add Route Coordinates
        /// </summary>
        /// <param name="objRouteCoordinates"></param>
        public void AddRouteCoordinates(RouteCoordinatesBO objRouteCoordinates)
        {
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
            objRouteCoordinatesDAL.AddRouteCoordinates(objRouteCoordinates);
        }

        /// <summary>
        /// To Update Route Coordinates
        /// </summary>
        /// <param name="objRouteCoordinates"></param>
        public void UpdateRouteCoordinates(RouteCoordinatesBO objRouteCoordinates)
        {
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
            objRouteCoordinatesDAL.UpdateRouteCoordinates(objRouteCoordinates);
        }

        /// <summary>
        /// To Get Route Coordinates By ID
        /// </summary>
        /// <param name="RouteCoordinateID"></param>
        /// <returns></returns>
        public RouteCoordinatesBO GetRouteCoordinatesByID(int RouteCoordinateID)
        {
            RouteCoordinatesDAL objCoordinatesDAL = new RouteCoordinatesDAL();
            return objCoordinatesDAL.GetRouteCoordinatesByID(RouteCoordinateID);
        }

        /// <summary>
        /// To Delete Route Coordinates
        /// </summary>
        /// <param name="RouteCoordinateID"></param>
        /// <returns></returns>
        public int DeleteRouteCoordinates(int RouteCoordinateID)
        {
           
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
           return objRouteCoordinatesDAL.DeleteRouteCoordinates(RouteCoordinateID);            
        }

        /// <summary>
        /// To Excel Data Import into Grid
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="routeID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string FilePath, string Extension, int routeID, int createdBy)
        {
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
            return objRouteCoordinatesDAL.ExcelDataImportintoGrid(FilePath, Extension, routeID, createdBy);
        }

        /// <summary>
        /// To Save Excel Data
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="ProjectID"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(RouteCoordinatesList list1, int ProjectID, string uID)
        {
            RouteCoordinatesDAL objRouteCoordinatesDAL = new RouteCoordinatesDAL();
            return objRouteCoordinatesDAL.SaveExcelData(list1, ProjectID, uID);
        }

    }
}