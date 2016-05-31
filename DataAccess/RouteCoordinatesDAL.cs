using System;
using System.Data;
using Oracle.DataAccess.Client;
using System.Data.OleDb;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class RouteCoordinatesDAL
    {
        /// <summary>
        /// To Get Route Coordinates
        /// </summary>
        /// <param name="RouteId"></param>
        /// <returns></returns>
        public RouteCoordinatesList GetRouteCoordinates(string RouteId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GETCOORDINATES";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (RouteId.ToString() == "")
            {
                cmd.Parameters.Add("@Route_IdIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@Route_IdIN", RouteId.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RouteCoordinatesBO objRouteCoordinates = null;
            RouteCoordinatesList RouteCoordinates = new RouteCoordinatesList();
            while (dr.Read())
            {
                objRouteCoordinates = new RouteCoordinatesBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTE_COORDINATEID"))) objRouteCoordinates.Route_CoordinateID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROUTE_COORDINATEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTEID"))) objRouteCoordinates.Route_ID =Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROUTEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROUTENAME"))) objRouteCoordinates.Routename = dr.GetString(dr.GetOrdinal("ROUTENAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("X_LONGITUDE"))) objRouteCoordinates.X_axis = dr.GetString(dr.GetOrdinal("X_LONGITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("Y_LATITUDE"))) objRouteCoordinates.Y_axis = dr.GetString(dr.GetOrdinal("Y_LATITUDE"));
                if (!dr.IsDBNull(dr.GetOrdinal("Z_HEIGHT"))) objRouteCoordinates.Z_axis = dr.GetString(dr.GetOrdinal("Z_HEIGHT"));
                if (!dr.IsDBNull(dr.GetOrdinal("latitude"))) objRouteCoordinates.Latitude = dr.GetString(dr.GetOrdinal("latitude"));
                if (!dr.IsDBNull(dr.GetOrdinal("LONGITUDE"))) objRouteCoordinates.Longitude = dr.GetString(dr.GetOrdinal("LONGITUDE"));
                RouteCoordinates.Add(objRouteCoordinates);
            }
            dr.Close();
            return RouteCoordinates;
        }

        /// <summary>
        /// To Add Route Coordinates
        /// </summary>
        /// <param name="objRoutecoordinates"></param>
        /// <returns></returns>
        public int AddRouteCoordinates(RouteCoordinatesBO objRoutecoordinates)
        {
            int result = 0;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_INS_COORDINATES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RouteIdIN", objRoutecoordinates.Route_ID);
                myCommand.Parameters.Add("@X_AxiesIN", objRoutecoordinates.X_axis);
                myCommand.Parameters.Add("@Y_AxiesIN", objRoutecoordinates.Y_axis);
                myCommand.Parameters.Add("@Z_AxiesIN", objRoutecoordinates.Z_axis);
                myCommand.Parameters.Add("@LATITUDEIN", objRoutecoordinates.Latitude);
                myCommand.Parameters.Add("@LONGITUDEIN", objRoutecoordinates.Longitude);
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objRoutecoordinates.CreatedBy);
                myCommand.Parameters.Add("rows_Affected", OracleDbType.Int32).Direction = ParameterDirection.Output;

                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// To Delete Route Coordinates
        /// </summary>
        /// <param name="Route_CoordinateId"></param>
        /// <returns></returns>
        public int DeleteRouteCoordinates(int Route_CoordinateId)
        {
            int result = 0;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_DEL_COORDINATES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RoutecoordinateIdIN", Route_CoordinateId);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// To Update Route Coordinates
        /// </summary>
        /// <param name="objRouteCoordinates"></param>
        /// <returns></returns>
        public int UpdateRouteCoordinates(RouteCoordinatesBO objRouteCoordinates)
        {
            int result = 0;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_TRN_UPD_Coordinates", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@RouteCoordinateIdIN", objRouteCoordinates.Route_CoordinateID);
                myCommand.Parameters.Add("@RouteIdIN", objRouteCoordinates.Route_ID);
                myCommand.Parameters.Add("@X_AxiesIN", objRouteCoordinates.X_axis);
                myCommand.Parameters.Add("@Y_AxiesIN", objRouteCoordinates.Y_axis);
                myCommand.Parameters.Add("@Z_AxiesIN", objRouteCoordinates.Z_axis);
                myCommand.Parameters.Add("@LATITUDEIN", objRouteCoordinates.Latitude);
                myCommand.Parameters.Add("@LONGITUDEIN", objRouteCoordinates.Longitude);
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objRouteCoordinates.UpdatedBy);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// To Get Route Coordinates By ID
        /// </summary>
        /// <param name="RouteCoordinateId"></param>
        /// <returns></returns>
        public RouteCoordinatesBO GetRouteCoordinatesByID(int RouteCoordinateId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_CORDINATEBYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@RouteCoordinateIdIN", RouteCoordinateId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RouteCoordinatesBO obRoutecoordinates = null;
            while (dr.Read())
            {
                obRoutecoordinates = new RouteCoordinatesBO();
                obRoutecoordinates.Route_CoordinateID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROUTE_COORDINATEID")));
                obRoutecoordinates.Route_ID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROUTEID")));
                obRoutecoordinates.X_axis = dr.GetString(dr.GetOrdinal("X_LONGITUDE"));
                obRoutecoordinates.Y_axis = dr.GetString(dr.GetOrdinal("Y_LATITUDE"));
                obRoutecoordinates.Z_axis = dr.GetString(dr.GetOrdinal("Z_HEIGHT"));
            }
            dr.Close();
            return obRoutecoordinates;
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
            //string result = "";

            //AddPAPBO objAddPAP = null;
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0";
            conStr = String.Format(conStr, FilePath, 1);

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
            return dt;
        }

        /// <summary>
        /// To Save Excel Data
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="RouteID"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(RouteCoordinatesList list1, int RouteID, string uID)
        {
            Int32 result = 0;
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_INS_COORDINATES", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@RouteIdIN", "");
            myCommand.Parameters.Add("@X_AxiesIN", "");
            myCommand.Parameters.Add("@Y_AxiesIN", "");
            myCommand.Parameters.Add("@Z_AxiesIN", "");
            myCommand.Parameters.Add("@LATITUDEIN", "");
            myCommand.Parameters.Add("@LONGITUDEIN", "");
            myCommand.Parameters.Add("@ISDELETEDIN", "");
            myCommand.Parameters.Add("@USERIDIN", "");
            myCommand.Parameters.Add("rows_Affected", OracleDbType.Int32).Direction = ParameterDirection.Output;

            myConnection.Open();
            for (int i = 0; i < list1.Count; i++)
            {
                myCommand.Parameters["@RouteIdIN"].Value = RouteID;
                myCommand.Parameters["@X_AxiesIN"].Value = list1[i].X_axis;
                myCommand.Parameters["@Y_AxiesIN"].Value = list1[i].Y_axis;
                myCommand.Parameters["@Z_AxiesIN"].Value = list1[i].Z_axis;
                myCommand.Parameters["@LATITUDEIN"].Value = list1[i].Latitude;
                myCommand.Parameters["@LONGITUDEIN"].Value = list1[i].Longitude;
                myCommand.Parameters["@ISDELETEDIN"].Value = "false";
                myCommand.Parameters["@USERIDIN"].Value = uID;
                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["rows_Affected"].Value != null)
                    result += Convert.ToInt32(myCommand.Parameters["rows_Affected"].Value.ToString());
            }

            myConnection.Close();
            return result.ToString();
        }

        /// <summary>
        /// To Excel Data Import into Grid Old
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="routeID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGridOld(string FilePath, string Extension, int routeID, int createdBy)
        {
            RouteCoordinatesBO objRouteCoordinates = null;
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0";
            conStr = String.Format(conStr, FilePath, 1);

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

            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_INS_COORDINATES", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@RouteIdIN", "");
            myCommand.Parameters.Add("@X_AxiesIN", "");
            myCommand.Parameters.Add("@Y_AxiesIN", "");
            myCommand.Parameters.Add("@Z_AxiesIN", "");
            myCommand.Parameters.Add("@ISDELETEDIN", "False");
            myCommand.Parameters.Add("@USERIDIN", "");
            myConnection.Open();
            //result = myCommand.ExecuteNonQuery();
            // myConnection.Close();
            //myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


            // myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;



            foreach (DataRow dr in dt.Rows)
            {

                //  objExpenseBO = new ExpenseBO();
                objRouteCoordinates = new RouteCoordinatesBO();

                objRouteCoordinates.Route_ID = routeID;
                objRouteCoordinates.X_axis = Convert.ToString(dr["X_axis"]);
                objRouteCoordinates.Y_axis = (dr["Y_axis"].ToString());
                objRouteCoordinates.Z_axis = (dr["Z_axis"].ToString());
                objRouteCoordinates.CreatedBy = createdBy;

                myCommand.Parameters["@RouteIdIN"].Value = objRouteCoordinates.Route_ID;
                myCommand.Parameters["@X_AxiesIN"].Value = objRouteCoordinates.X_axis;
                myCommand.Parameters["@Y_AxiesIN"].Value = objRouteCoordinates.Y_axis;
                myCommand.Parameters["@Z_AxiesIN"].Value = objRouteCoordinates.Z_axis;
                myCommand.Parameters["@USERIDIN"].Value = objRouteCoordinates.CreatedBy;


                // myCommand.Parameters["ISDELETEDIN"].Value = "False";


                myCommand.ExecuteNonQuery();

                //if (myCommand.Parameters["errorMessage_"].Value != null)
                //    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                //else
                //    result = string.Empty;
            }

            myConnection.Close();
            return dt;
            // return dt;
        }
    }
}