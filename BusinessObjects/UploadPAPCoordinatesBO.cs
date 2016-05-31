using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class UploadPAPCoordinatesBO
    {
        private int hhid = -1;
        private string row_X = String.Empty;
        private string row_Y = String.Empty;
        private string row_latitude = String.Empty;
        private string row_longitude = String.Empty;
        private string wl_X = String.Empty;
        private string wl_Y = String.Empty;
        private string wl_latitude = String.Empty;
        private string wl_longitude = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string papname = String.Empty;
        private string papUID = String.Empty;

        public int Id { set; get; }

        public string PapUID
        {
            get
            {
                return papUID;
            }
            set
            {
                papUID = value;
            }
        }

        public string Papname
        {
            get
            {
                return papname;
            }
            set
            {
                papname = value;
            }
        }

        public int HHID
        {
            get
            {
                return hhid;
            }
            set
            {
                hhid = value;
            }
        }

        public string ROW_X
        {
            get
            {
                return row_X;
            }
            set
            {
                row_X = value;
            }
        }

        public string ROW_Y
        {
            get
            {
                return row_Y;
            }
            set
            {
                row_Y = value;
            }
        }

        public string ROW_LATITUDE
        {
            get
            {
                return row_latitude;
            }
            set
            {
                row_latitude = value;
            }
        }

        public string ROW_LONGITUDE
        {
            get
            {
                return row_longitude;
            }
            set
            {
                row_longitude = value;
            }
        }


        public string WL_X
        {
            get
            {
                return wl_X;
            }
            set
            {
                wl_X = value;
            }
        }

        public string WL_Y
        {
            get
            {
                return wl_Y;
            }
            set
            {
                wl_Y = value;
            }
        }

        public string WL_LATITUDE
        {
            get
            {
                return wl_latitude;
            }
            set
            {
                wl_latitude = value;
            }
        }

        public string WL_LONGITUDE
        {
            get
            {
                return wl_longitude;
            }
            set
            {
                wl_longitude = value;
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
