using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class AddPAPDAL
    {
        /// <summary>
        /// To import excel data to grid
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileextension"></param>
        /// <param name="projectID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string filePath, string fileextension, int projectID, int createdBy)
        {
            //string result = "";

            //AddPAPBO objAddPAP = null;
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0";
            conStr = String.Format(conStr, filePath, 1);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
           
            connExcel.Close();
            //DataTable dtdata = new DataTable();
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    if(dc.ColumnName=="RIGHT_OF_WAY")
            //        dtdata.Columns.Add("RightOfWay");

            //    else
            //        dtdata.Columns.Add(dc);
            //}
            //dtdata.Columns.Add("Totalha", typeof(double));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drRow = dtdata.NewRow();
            //    drRow["Surname"] = dr["Surname"];
            //    dtdata.Rows.Add(drRow);
            //}
          //  dt.Columns.Add("PapType", typeof(string));//New Blank Column Added
            return dt;
        }
        /// <summary>
        /// To save excel data to database
        /// </summary>
        /// <param name="dtPap"></param>
        /// <param name="ProjectID"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(DataTable dt, int ProjectID, string uID)
        {
            AddPAPBO objAddPAP;
            string result = string.Empty;
            string sPaps = string.Empty;
            string fPaps = string.Empty;
            string PapName = string.Empty, InstituteName = string.Empty;

            SqlConnection myConnection;
            SqlCommand myCommand;

         

            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_PAPHOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.AddWithValue("PROJECTIDIN", "");
            myCommand.Parameters.AddWithValue("SurnameIN", "");
            myCommand.Parameters.AddWithValue("FirstnameIN", "");
            myCommand.Parameters.AddWithValue("OthernameIN", "");
            myCommand.Parameters.AddWithValue("PAPNAMEIN", "");
            myCommand.Parameters.AddWithValue("INSTITUTION_", "");
            myCommand.Parameters.AddWithValue("PAPTYPE_", "");
            myCommand.Parameters.AddWithValue("PLOTREFERENCEIN", "");
            myCommand.Parameters.AddWithValue("DESIGNATIONIN", "");
            myCommand.Parameters.AddWithValue("DISTRICTIN", "");
            myCommand.Parameters.AddWithValue("COUNTYIN", "");
            myCommand.Parameters.AddWithValue("SUBCOUNTYIN", "");
            myCommand.Parameters.AddWithValue("PARISHIN", "");
            myCommand.Parameters.AddWithValue("VILLAGEIN", "");
            myCommand.Parameters.AddWithValue("RIGHTWAYIN", "");
            myCommand.Parameters.AddWithValue("WAYLEAVESIN", "");
            myCommand.Parameters.AddWithValue("ISDELETEDIN", "");
            myCommand.Parameters.AddWithValue("USERIDIN", "");
            myCommand.Parameters.AddWithValue("PLOTLATITUDEIN", "");
            myCommand.Parameters.AddWithValue("PLOTLONGITUDEIN", "");
            myCommand.Parameters.AddWithValue("PAP_UIDIN", "");
            myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            myConnection.Open();
            int CountSuccess = 0, CountFail = 0;
            foreach (DataRow dr in dt.Rows)
            {
                objAddPAP = new AddPAPBO();
                objAddPAP.ProjectID = ProjectID;
                objAddPAP.Surname = Convert.ToString(dr["SURNAME"]);
                objAddPAP.Firstname = Convert.ToString(dr["FIRSTNAME"]);
                objAddPAP.Othername = Convert.ToString(dr["OTHERNAME"]);
                objAddPAP.Pap = Convert.ToString(dr["PAPNAME"]);//Renamed
                objAddPAP.Institution = Convert.ToString(dr["INSTITUTION"]);
                objAddPAP.PapType = Convert.ToString(dr["PapType"]);
                objAddPAP.Plot_ref = Convert.ToString(dr["PLOTREFERENCE"]);
                objAddPAP.Designation = Convert.ToString(dr["DESIGNATION"]);
                objAddPAP.District = Convert.ToString(dr["DISTRICT"]);
                objAddPAP.County = Convert.ToString(dr["COUNTY"]);
                objAddPAP.SubCounty = Convert.ToString(dr["SUBCOUNTY"]);
                objAddPAP.Parish = Convert.ToString(dr["PARISH"]);
                objAddPAP.Village = Convert.ToString(dr["VILLAGE"]);
                objAddPAP.Right_of_way = Convert.ToString(dr["RIGHTOFWAY"]);
                objAddPAP.Wayleaves = Convert.ToString(dr["WAYLEAVES"]);
                objAddPAP.Total = Convert.ToString(dr["TOTAL"]);
                objAddPAP.Plotlatitude = Convert.ToString(dr["PLOTLATITUDE"]);
                objAddPAP.Plotlongitude = Convert.ToString(dr["PLOTLONGITUDE"]);
                objAddPAP.Papuid = Convert.ToString(dr["Papuid"]);

                myCommand.Parameters["PROJECTIDIN"].Value = objAddPAP.ProjectID;
                myCommand.Parameters["SurnameIN"].Value = objAddPAP.Surname;
                myCommand.Parameters["FirstnameIN"].Value = objAddPAP.Firstname;
                myCommand.Parameters["OthernameIN"].Value = objAddPAP.Othername;
                myCommand.Parameters["PAPNAMEIN"].Value =  objAddPAP.Surname.ToString()+ " " +objAddPAP.Firstname;
                
                if (objAddPAP.Institution.Trim() != "")
                {
                    myCommand.Parameters["INSTITUTION_"].Value = objAddPAP.Institution;//INSTITUTION
                }                
                else
                {                    
                    myCommand.Parameters["INSTITUTION_"].Value = DBNull.Value;//INSTITUTION
                }
                if (objAddPAP.PapType.ToUpper() == "GROUPOWNER")// || objAddPAP.Group_Owner != null)
                {
                    myCommand.Parameters["PAPTYPE_"].Value = "GRP";
                }
                else if (objAddPAP.PapType.ToUpper() == "INSTITUTION")// || !string.IsNullOrEmpty(objAddPAP.Institution))
                {
                    myCommand.Parameters["PAPTYPE_"].Value = "INS";
                }
                else
                {
                    myCommand.Parameters["PAPTYPE_"].Value = "IND";
                }

                myCommand.Parameters["PLOTREFERENCEIN"].Value = objAddPAP.Plot_ref;
                myCommand.Parameters["DESIGNATIONIN"].Value = objAddPAP.Designation;
                myCommand.Parameters["DISTRICTIN"].Value = objAddPAP.District;
                myCommand.Parameters["COUNTYIN"].Value = objAddPAP.County;
                myCommand.Parameters["SUBCOUNTYIN"].Value = objAddPAP.SubCounty;
                myCommand.Parameters["PARISHIN"].Value = objAddPAP.Parish;
                myCommand.Parameters["VILLAGEIN"].Value = objAddPAP.Village;
                myCommand.Parameters["RIGHTWAYIN"].Value = objAddPAP.Right_of_way;
                myCommand.Parameters["WAYLEAVESIN"].Value = objAddPAP.Wayleaves;
                myCommand.Parameters["ISDELETEDIN"].Value = "False";
                myCommand.Parameters["USERIDIN"].Value = uID;
                myCommand.Parameters["PLOTLATITUDEIN"].Value = objAddPAP.Plotlatitude;
                myCommand.Parameters["PLOTLONGITUDEIN"].Value = objAddPAP.Plotlongitude;
                myCommand.Parameters["PAP_UIDIN"].Value = objAddPAP.Papuid;

                myCommand.ExecuteNonQuery();

                string resultMessage = string.Empty;
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    resultMessage = myCommand.Parameters["errorMessage_"].Value.ToString();

                if (string.IsNullOrEmpty(resultMessage) || resultMessage == "null" || resultMessage == "")
                {
                    result = string.Empty;
                    sPaps += "," + Convert.ToString(dr["Papuid"]);
                    CountSuccess++;
                }
                else
                {
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                    fPaps += ", " + Convert.ToString(dr["Papuid"]);
                    CountFail++;

                }
            }

            myConnection.Close();
            if (sPaps.Length > 0)
                sPaps = sPaps.Remove(0, 1);
            if (fPaps.Length > 0)
                fPaps = fPaps.Remove(0, 1);
            return CountSuccess.ToString() + "|" + fPaps;
            //return CountSuccess.ToString();
        }
        /// <summary>
        /// To add pap data into database
        /// </summary>
        /// <param name="objAddPAP"></param>
        /// <returns></returns>
        public string AddPAP(AddPAPBO objAddPAP)
        {
            string result = "";

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_INS_PAPHOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("PROJECTIDIN", objAddPAP.ProjectID);
            myCommand.Parameters.AddWithValue("SurnameIN", objAddPAP.Surname);
            myCommand.Parameters.AddWithValue("FirstnameIN", objAddPAP.Firstname);
            myCommand.Parameters.AddWithValue("OthernameIN", objAddPAP.Othername);
            myCommand.Parameters.AddWithValue("PAPNAMEIN", objAddPAP.Pap);
            myCommand.Parameters.AddWithValue("INSTITUTION_", objAddPAP.Institution);
            myCommand.Parameters.AddWithValue("PAPTYPE_", objAddPAP.PapType);
            myCommand.Parameters.AddWithValue("PLOTREFERENCEIN", objAddPAP.Plot_ref);
            myCommand.Parameters.AddWithValue("DESIGNATIONIN", objAddPAP.Designation);
            myCommand.Parameters.AddWithValue("DISTRICTIN", objAddPAP.District);
            myCommand.Parameters.AddWithValue("COUNTYIN", objAddPAP.County);
            myCommand.Parameters.AddWithValue("SUBCOUNTYIN", objAddPAP.SubCounty);
            myCommand.Parameters.AddWithValue("PARISHIN", objAddPAP.Parish);
            myCommand.Parameters.AddWithValue("VILLAGEIN", objAddPAP.Village);
            myCommand.Parameters.AddWithValue("RIGHTWAYIN", objAddPAP.Right_of_way);
            myCommand.Parameters.AddWithValue("WAYLEAVESIN", objAddPAP.Wayleaves);
            myCommand.Parameters.AddWithValue("ISDELETEDIN", "False");
            myCommand.Parameters.AddWithValue("USERIDIN", objAddPAP.CreatedBy);
            myCommand.Parameters.AddWithValue("PLOTLATITUDEIN", objAddPAP.Plotlatitude);
            myCommand.Parameters.AddWithValue("PLOTLONGITUDEIN", objAddPAP.Plotlongitude);
            myCommand.Parameters.AddWithValue("PAP_UIDIN", objAddPAP.Papuid);
            //myCommand.Parameters.AddWithValue("RELIGIONIDIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("optiongroupidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("literacylevelidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("occupationidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("papstatusidIN", objAddPAP.CreatedBy);
            myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        /// TO update PAP data to database
        /// </summary>
        /// <param name="objHouseholdPAP"></param>
        /// <returns></returns>
        public string UpdatePAP(PAP_HouseholdBO objHouseholdPAP)
        {
            string result = "";

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_UPD_PAPHOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("HHIDIN", objHouseholdPAP.HhId);
            myCommand.Parameters.AddWithValue("PROJECTIDIN", objHouseholdPAP.ProjectedId);
            myCommand.Parameters.AddWithValue("SurnameIN", objHouseholdPAP.Surname);
            myCommand.Parameters.AddWithValue("FirstnameIN", objHouseholdPAP.Firstname);
            myCommand.Parameters.AddWithValue("OthernameIN", objHouseholdPAP.Othername);
            myCommand.Parameters.AddWithValue("PAPNAMEIN", objHouseholdPAP.PapName);
            myCommand.Parameters.AddWithValue("PLOTREFERENCEIN", objHouseholdPAP.PlotReference);
            myCommand.Parameters.AddWithValue("DESIGNATIONIN", objHouseholdPAP.Designation);
            myCommand.Parameters.AddWithValue("DISTRICTIN", objHouseholdPAP.District);
            myCommand.Parameters.AddWithValue("COUNTYIN", objHouseholdPAP.County);
            myCommand.Parameters.AddWithValue("SUBCOUNTYIN", objHouseholdPAP.SubCounty);
            myCommand.Parameters.AddWithValue("PARISHIN", objHouseholdPAP.Parish);
            myCommand.Parameters.AddWithValue("VILLAGEIN", objHouseholdPAP.Village);
            myCommand.Parameters.AddWithValue("RIGHTWAYIN", objHouseholdPAP.Rightofway);
            myCommand.Parameters.AddWithValue("WAYLEAVESIN", objHouseholdPAP.Wayleaves);
            myCommand.Parameters.AddWithValue("USERIDIN", objHouseholdPAP.UpdatedBy);
            myCommand.Parameters.AddWithValue("PLOTLATITUDEIN", objHouseholdPAP.Plotlatitude);
            myCommand.Parameters.AddWithValue("PLOTLONGITUDEIN", objHouseholdPAP.Plotlongitude);
            myCommand.Parameters.AddWithValue("PAP_UIDIN", objHouseholdPAP.Papuid);
            //myCommand.Parameters.AddWithValue("RELIGIONIDIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("optiongroupidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("literacylevelidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("occupationidIN", objAddPAP.CreatedBy);
            //myCommand.Parameters.AddWithValue("papstatusidIN", objAddPAP.CreatedBy);
            myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
        /// To Obsolete PAP data in database
        /// </summary>
        /// <param name="PAPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoletePAP(int PAPID, string IsDeleted)
        {
            string result = "";

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_OBS_PAPHOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                myCommand.Parameters.AddWithValue("HHIDIN", PAPID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;
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
        /// To Delete PAP data from database
        /// </summary>
        /// <param name="PAPID"></param>
        /// <returns></returns>
        public string DeletePAP(int PAPID)
        {
            string result = "";

            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_DEL_PAPHOUSEHOLD", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                myCommand.Parameters.AddWithValue("HHIDIN", PAPID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }
    }
}
