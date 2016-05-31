using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class RouteCoordinatesBO
    {
        private int route_CoordinateID = -1;
        private int route_ID = -1;
        private string x_axis = String.Empty;
        private string y_axis = String.Empty;
        private string z_axis = String.Empty;
        private string latitude = String.Empty;
        private string longitude = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string routename = String.Empty;


        public string Routename
        {
            get
            {
                return routename;
            }
            set
            {
                routename = value;
            }
        }

        public int Route_CoordinateID
        {
            get
            {
                return route_CoordinateID;
            }
            set
            {
                route_CoordinateID = value;
            }
        }

        public int Route_ID
        {
            get
            {
                return route_ID;
            }
            set
            {
                route_ID = value;
            }
        }

        public string X_axis
        {
            get
            {
                return x_axis;
            }
            set
            {
                x_axis = value;
            }
        }

        public string Y_axis
        {
            get
            {
                return y_axis;
            }
            set
            {
                y_axis = value;
            }
        }

        public string Z_axis
        {
            get
            {
                return z_axis;
            }
            set
            {
                z_axis = value;
            }
        }

        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }

        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }
    }
}