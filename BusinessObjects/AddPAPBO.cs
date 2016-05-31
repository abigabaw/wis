using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class AddPAPBO
    {
            private string pap = String.Empty;
            private string plot_ref = String.Empty;
            private string district = String.Empty;
            private string village = String.Empty;
            private string right_of_way = String.Empty;
            private string wayleaves = String.Empty;
            private string total = String.Empty;
            private string plotlatitude = String.Empty;
            private string plotlongitude = String.Empty;
            private string surname = string.Empty;
            private string firstname = string.Empty;
            private string otherName = string.Empty;

            public int ProjectID { get; set; }

            private string papuid = string.Empty;
            public string Papuid { get { return papuid; } set { papuid = value; } }

            public string Pap
            {
                get
                {
                    return pap;
                }
                set
                {
                    pap = value;
                }
            }

            public string Surname
            {
                get
                {
                    return surname;
                }
                set
                {
                    surname = value;
                }
            }

            public string Firstname
            {
                get
                {
                    return firstname;
                }
                set
                {
                    firstname = value;
                }
            }

            public string Othername
            {
                get
                {
                    return otherName;
                }
                set
                {
                    otherName = value;
                }
            }

            public string Institution { get; set; }

            public string Group_Owner { get; set; }

            public string PapType { get; set; }

            public string PapName { get; set; }

            public string Plotlatitude
            {
                get
                {
                    return plotlatitude;
                }
                set
                {
                    plotlatitude = value;
                }
            }

            public string Plotlongitude
            {
                get
                {
                    return plotlongitude;
                }
                set
                {
                    plotlongitude = value;
                }
            }

            private string designation = string.Empty;
            public string Designation { get { return designation; } set { designation = value; } }

            public string Plot_ref
            {
                get
                {
                    return plot_ref;
                }
                set
                {
                    plot_ref = value;
                }
            }

            public string District
            {
                get
                {
                    return district;
                }
                set
                {
                    district = value;
                }
            }

            public string County { get; set; }
            public string SubCounty { get; set; }
            public string Parish { get; set; }

            public string Village
            {
                get
                {
                    return village;
                }
                set
                {
                    village = value;
                }
            }

            public string Right_of_way
            {
                get
                {
                    return right_of_way;
                }
                set
                {
                    right_of_way = value;
                }
            }

            public string Wayleaves
            {
                get
                {
                    return wayleaves;
                }
                set
                {
                    wayleaves = value;
                }
            }

            public string Total
            {
                get
                {
                    return total;
                }
                set
                {
                    total = value;
                }
            }

            public int CreatedBy { get; set; }
      }
}