﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;

namespace WIS_DataAccess
{
    public class UploadPAPCoordinatesDAL
    {
        /// <summary>
        /// To Excel Data Import into Grid
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="HHID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string FilePath, string Extension, int HHID, int createdBy)
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
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(UploadPAPCoordinatesList list1, string uID)
        {
            string result = string.Empty;
            OracleConnection myConnection;
            OracleCommand myCommand;
            //myConnection = new OracleConnection(AppConfiguration.ConnectionString);

            ////del
            //if (list1.Count > 0)
            //{
            //    myCommand = new OracleCommand("USP_TRN_DEL_PAP_COORDINATES", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("HHID_", list1[0].HHID);
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}
            //end
            //insert
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_INS_PAP_COORDINATES", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", "");
            myCommand.Parameters.Add("ROW_X_", "");
            myCommand.Parameters.Add("ROW_Y_", "");
            myCommand.Parameters.Add("ROW_LATITUDE_", "");
            myCommand.Parameters.Add("ROW_LONGITUDE_", "");
            myCommand.Parameters.Add("WL_X_", "");
            myCommand.Parameters.Add("WL_Y_", "");
            myCommand.Parameters.Add("WL_LATITUDE_", "");
            myCommand.Parameters.Add("WL_LONGITUDE_", "");
            myCommand.Parameters.Add("USERID_", "");
            myCommand.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            myConnection.Open();
            for (int i = 0; i < list1.Count; i++)
            {
                myCommand.Parameters["HHID_"].Value = list1[i].HHID;
                myCommand.Parameters["ROW_X_"].Value = list1[i].ROW_X;
                myCommand.Parameters["ROW_Y_"].Value = list1[i].ROW_Y;
                myCommand.Parameters["ROW_LATITUDE_"].Value = list1[i].ROW_LATITUDE;
                myCommand.Parameters["ROW_LONGITUDE_"].Value = list1[i].ROW_LONGITUDE;
                myCommand.Parameters["WL_X_"].Value = list1[i].WL_X;
                myCommand.Parameters["WL_Y_"].Value = list1[i].WL_Y;
                myCommand.Parameters["WL_LATITUDE_"].Value = list1[i].WL_LATITUDE;
                myCommand.Parameters["WL_LONGITUDE_"].Value = list1[i].WL_LONGITUDE;
                myCommand.Parameters["USERID_"].Value = uID;
                myCommand.ExecuteNonQuery();
            }

            myConnection.Close();
            return list1.Count.ToString();
        }

        /// <summary>
        /// To Get All Pap Coordinates Data
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public UploadPAPCoordinatesList GetAllPapCoordinatesData(int HHID, int PID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAP_COORDINATES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("PROJECTID_", PID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            UploadPAPCoordinatesBO UploadPAPCoordinatesBOobj = null;
            UploadPAPCoordinatesList UploadPAPCoordinatesListobj = new UploadPAPCoordinatesList();
            UploadPAPCoordinatesBOobj = new UploadPAPCoordinatesBO();

            while (dr.Read())
            {
                UploadPAPCoordinatesBOobj = new UploadPAPCoordinatesBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) UploadPAPCoordinatesBOobj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) UploadPAPCoordinatesBOobj.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_X"))) UploadPAPCoordinatesBOobj.ROW_X = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_X")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_Y"))) UploadPAPCoordinatesBOobj.ROW_Y = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_Y")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_LATITUDE"))) UploadPAPCoordinatesBOobj.ROW_LATITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_LATITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_LONGITUDE"))) UploadPAPCoordinatesBOobj.ROW_LONGITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_LONGITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_X"))) UploadPAPCoordinatesBOobj.WL_X = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_X")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_Y"))) UploadPAPCoordinatesBOobj.WL_Y = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_Y")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_LATITUDE"))) UploadPAPCoordinatesBOobj.WL_LATITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_LATITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_LONGITUDE"))) UploadPAPCoordinatesBOobj.WL_LONGITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_LONGITUDE")));
                
                UploadPAPCoordinatesListobj.Add(UploadPAPCoordinatesBOobj);
            }
            dr.Close();
            return UploadPAPCoordinatesListobj;
        }

        /// <summary>
        /// To Save Excel Data For All Paps
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelDataForAllPaps(UploadPAPCoordinatesList list1, string uID)
        {
            string result = string.Empty;
            OracleConnection myConnection;
            OracleCommand myCommand;
            //myConnection = new OracleConnection(AppConfiguration.ConnectionString);

            ////del
            //if (list1.Count > 0)
            //{
            //    myCommand = new OracleCommand("USP_TRN_DEL_PAP_COORDINATES", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("HHID_", list1[0].HHID);
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}
            //end
            //insert
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_INS_PAP_COORDINATES", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", "");
            myCommand.Parameters.Add("ROW_X_", "");
            myCommand.Parameters.Add("ROW_Y_", "");
            myCommand.Parameters.Add("ROW_LATITUDE_", "");
            myCommand.Parameters.Add("ROW_LONGITUDE_", "");
            myCommand.Parameters.Add("WL_X_", "");
            myCommand.Parameters.Add("WL_Y_", "");
            myCommand.Parameters.Add("WL_LATITUDE_", "");
            myCommand.Parameters.Add("WL_LONGITUDE_", "");
            myCommand.Parameters.Add("USERID_", "");
            myCommand.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            myConnection.Open();
            for (int i = 0; i < list1.Count; i++)
            {
                myCommand.Parameters["HHID_"].Value = list1[i].HHID;
                myCommand.Parameters["ROW_X_"].Value = list1[i].ROW_X;
                myCommand.Parameters["ROW_Y_"].Value = list1[i].ROW_Y;
                myCommand.Parameters["ROW_LATITUDE_"].Value = list1[i].ROW_LATITUDE;
                myCommand.Parameters["ROW_LONGITUDE_"].Value = list1[i].ROW_LONGITUDE;
                myCommand.Parameters["WL_X_"].Value = list1[i].WL_X;
                myCommand.Parameters["WL_Y_"].Value = list1[i].WL_Y;
                myCommand.Parameters["WL_LATITUDE_"].Value = list1[i].WL_LATITUDE;
                myCommand.Parameters["WL_LONGITUDE_"].Value = list1[i].WL_LONGITUDE;
                myCommand.Parameters["USERID_"].Value = uID;
                myCommand.ExecuteNonQuery();
            }

            myConnection.Close();
            return list1.Count.ToString();
        }

        public string SavePAPCoordinates(UploadPAPCoordinatesBO oUploadPAPCoordinatesBO)
        {
            string result = "";

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_INS_PAP_COORDINATES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("HHID_", oUploadPAPCoordinatesBO.HHID);
            cmd.Parameters.Add("ROW_X_", oUploadPAPCoordinatesBO.ROW_X);
            cmd.Parameters.Add("ROW_Y_", oUploadPAPCoordinatesBO.ROW_Y);
            cmd.Parameters.Add("ROW_Lat_", oUploadPAPCoordinatesBO.ROW_LATITUDE);
            cmd.Parameters.Add("ROW_Long_", oUploadPAPCoordinatesBO.ROW_LONGITUDE);
            cmd.Parameters.Add("WL_X_", oUploadPAPCoordinatesBO.WL_X);
            cmd.Parameters.Add("WL_Y_", oUploadPAPCoordinatesBO.WL_Y);
            cmd.Parameters.Add("WL_Lat_", oUploadPAPCoordinatesBO.WL_LATITUDE);
            cmd.Parameters.Add("WL_Long_", oUploadPAPCoordinatesBO.WL_LONGITUDE);

            cmd.Parameters.Add("USERID_", oUploadPAPCoordinatesBO.CreatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return result;

            // return "";
        }

        public string UpdatePAPCoordinates(UploadPAPCoordinatesBO oUploadPAPCoordinatesBO)
        {
            string result = "";

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_UPD_PAP_COORDINATES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("ID_", oUploadPAPCoordinatesBO.Id);
            cmd.Parameters.Add("HHID_", oUploadPAPCoordinatesBO.HHID);
            cmd.Parameters.Add("ROW_X_", oUploadPAPCoordinatesBO.ROW_X);
            cmd.Parameters.Add("ROW_Y_", oUploadPAPCoordinatesBO.ROW_Y);
            cmd.Parameters.Add("ROW_Lat_", oUploadPAPCoordinatesBO.ROW_LATITUDE);
            cmd.Parameters.Add("ROW_Long_", oUploadPAPCoordinatesBO.ROW_LONGITUDE);
            cmd.Parameters.Add("WL_X_", oUploadPAPCoordinatesBO.WL_X);
            cmd.Parameters.Add("WL_Y_", oUploadPAPCoordinatesBO.WL_Y);
            cmd.Parameters.Add("WL_Lat_", oUploadPAPCoordinatesBO.WL_LATITUDE);
            cmd.Parameters.Add("WL_Long_", oUploadPAPCoordinatesBO.WL_LONGITUDE);

            cmd.Parameters.Add("USERID_", oUploadPAPCoordinatesBO.UpdatedBy);
            cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

            cmd.Connection.Close();

            return result;

            // return "";
        }

        public void DeletePapCoordinates(int ID)
        {
            string result = string.Empty;
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);

            myCommand = new OracleCommand("USP_TRN_DEL_PAP_COORDINATES", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("ID_", ID);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
            myConnection.Close();
            myConnection.Dispose();
        }

        public UploadPAPCoordinatesBO GetPapCoordinatesDataByID(int ID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAP_COORDINATESID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ID_", ID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            UploadPAPCoordinatesBO UploadPAPCoordinatesBOobj = null;
            UploadPAPCoordinatesList UploadPAPCoordinatesListobj = new UploadPAPCoordinatesList();
            UploadPAPCoordinatesBOobj = new UploadPAPCoordinatesBO();

            while (dr.Read())
            {
                UploadPAPCoordinatesBOobj = new UploadPAPCoordinatesBO();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) UploadPAPCoordinatesBOobj.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) UploadPAPCoordinatesBOobj.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_X"))) UploadPAPCoordinatesBOobj.ROW_X = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_X")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_Y"))) UploadPAPCoordinatesBOobj.ROW_Y = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_Y")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_LATITUDE"))) UploadPAPCoordinatesBOobj.ROW_LATITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_LATITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("ROW_LONGITUDE"))) UploadPAPCoordinatesBOobj.ROW_LONGITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("ROW_LONGITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_X"))) UploadPAPCoordinatesBOobj.WL_X = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_X")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_Y"))) UploadPAPCoordinatesBOobj.WL_Y = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_Y")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_LATITUDE"))) UploadPAPCoordinatesBOobj.WL_LATITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_LATITUDE")));
                if (!dr.IsDBNull(dr.GetOrdinal("WL_LONGITUDE"))) UploadPAPCoordinatesBOobj.WL_LONGITUDE = Convert.ToString(dr.GetValue(dr.GetOrdinal("WL_LONGITUDE")));

            }
            dr.Close();
            return UploadPAPCoordinatesBOobj;
        }
    }
}
