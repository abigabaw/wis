﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class LandInfoRespondentsDAL
    {
        #region GET RECORDS
        /// <summary>
        /// To Get Land Info Respondents
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandInfoRespondentsList GetLandInfoRespondents(int HHID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LND_RESP_HOLDNG";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandInfoRespondentsBO objLIRBLL = null;
            LandInfoRespondentsList objLIRLIST = new LandInfoRespondentsList();

            while (dr.Read())
            {
                objLIRBLL = new LandInfoRespondentsBO();
                //objLIRBLL.HOLDINGID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_HOLDINGID"))));
                //objLIRBLL.LND_TYPEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TYPEID"))));
                //objLIRBLL.Land_Type = dr.GetValue(dr.GetOrdinal("LANDTYPE")).ToString();
                //objLIRBLL.LND_USEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_USEID"))));
                //objLIRBLL.Land_Use = dr.GetValue(dr.GetOrdinal("LANDUSE")).ToString();
                //objLIRBLL.DISTRICT = dr.GetValue(dr.GetOrdinal("DISTRICT")).ToString();
                //objLIRBLL.COUNTY = dr.GetValue(dr.GetOrdinal("COUNTY")).ToString();
                //objLIRBLL.SUBCOUNTY = dr.GetValue(dr.GetOrdinal("SUBCOUNTY")).ToString();
                //objLIRBLL.VILLAGE = dr.GetValue(dr.GetOrdinal("VILLAGE")).ToString();
                //objLIRBLL.TENURE = dr.GetValue(dr.GetOrdinal("TENURE")).ToString();
                //objLIRBLL.TOTALSIZE = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TOTALSIZE"))));
                //objLIRBLL.ISPRIMARYRESIDENCE = dr.GetValue(dr.GetOrdinal("ISPRIMARYRESIDENCE")).ToString();
                //objLIRBLL.ISAFFECTED = dr.GetValue(dr.GetOrdinal("ISAFFECTED")).ToString();

                objLIRBLL = MapData(dr);
                objLIRLIST.Add(objLIRBLL);
            }

            dr.Close();
            return objLIRLIST;
        }

        /// <summary>
        /// To Get Land Info Respondents By ID
        /// </summary>
        /// <param name="holdingID"></param>
        /// <returns></returns>
        public LandInfoRespondentsBO GetLandInfoRespondentsByID(int holdingID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_LND_RESP_HOLDGBYID";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("LND_HOLDINGID", holdingID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LandInfoRespondentsBO objLIRBLL = null;
            LandInfoRespondentsList objLIRLIST = new LandInfoRespondentsList();

            while (dr.Read())
            {
                objLIRBLL = new LandInfoRespondentsBO();
                //objLIRBLL.HOLDINGID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_HOLDINGID"))));
                //objLIRBLL.LND_TYPEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TYPEID"))));
                //objLIRBLL.LND_USEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_USEID"))));
                //objLIRBLL.DISTRICT = dr.GetValue(dr.GetOrdinal("DISTRICT")).ToString();
                //objLIRBLL.COUNTY = dr.GetValue(dr.GetOrdinal("COUNTY")).ToString();
                //objLIRBLL.SUBCOUNTY = dr.GetValue(dr.GetOrdinal("SUBCOUNTY")).ToString();
                //objLIRBLL.VILLAGE = dr.GetValue(dr.GetOrdinal("VILLAGE")).ToString();
                //objLIRBLL.TENURE = dr.GetValue(dr.GetOrdinal("TENURE")).ToString();
                //objLIRBLL.TOTALSIZE = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TOTALSIZE"))));
                //objLIRBLL.ISPRIMARYRESIDENCE = dr.GetValue(dr.GetOrdinal("ISPRIMARYRESIDENCE")).ToString();
                //objLIRBLL.ISAFFECTED = dr.GetValue(dr.GetOrdinal("ISAFFECTED")).ToString();
                objLIRBLL = MapData(dr);
                //objLIRLIST.Add(objLIRBLL);
            }

            dr.Close();
            return objLIRBLL;
        }

        /// <summary>
        /// To Map Data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private LandInfoRespondentsBO MapData(IDataReader reader)
        {
            // BatchBO oBatchBO = new BatchBO();
            LandInfoRespondentsBO oLandInfoResBO = new LandInfoRespondentsBO();

            if (ColumnExists(reader, "LND_HOLDINGID") && !reader.IsDBNull(reader.GetOrdinal("LND_HOLDINGID")))
                oLandInfoResBO.HOLDINGID = (Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LND_HOLDINGID"))));

            if (ColumnExists(reader, "LND_TYPEID") && !reader.IsDBNull(reader.GetOrdinal("LND_TYPEID")))
                oLandInfoResBO.LND_TYPEID = (Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LND_TYPEID"))));
            else if (reader.IsDBNull(reader.GetOrdinal("LND_TYPEID")))
                oLandInfoResBO.LND_TYPEID = 0;

            if (ColumnExists(reader, "LANDTYPE") && !reader.IsDBNull(reader.GetOrdinal("LANDTYPE")))
                oLandInfoResBO.Land_Type = reader.GetString(reader.GetOrdinal("LANDTYPE"));

            if (ColumnExists(reader, "LND_USEID") && !reader.IsDBNull(reader.GetOrdinal("LND_USEID")))
                oLandInfoResBO.LND_USEID = (Convert.ToInt32(reader.GetValue(reader.GetOrdinal("LND_USEID"))));
            else if (reader.IsDBNull(reader.GetOrdinal("LND_USEID")))
                oLandInfoResBO.LND_USEID = 0;

            if (ColumnExists(reader, "LANDUSE") && !reader.IsDBNull(reader.GetOrdinal("LANDUSE")))
                oLandInfoResBO.Land_Use = reader.GetString(reader.GetOrdinal("LANDUSE"));

            if (ColumnExists(reader, "DISTRICT") && !reader.IsDBNull(reader.GetOrdinal("DISTRICT")))
                oLandInfoResBO.DISTRICT = reader.GetString(reader.GetOrdinal("DISTRICT"));

            if (ColumnExists(reader, "COUNTY") && !reader.IsDBNull(reader.GetOrdinal("COUNTY")))
                oLandInfoResBO.COUNTY = reader.GetValue(reader.GetOrdinal("COUNTY")).ToString();

            if (ColumnExists(reader, "SUBCOUNTY") && !reader.IsDBNull(reader.GetOrdinal("SUBCOUNTY")))
                oLandInfoResBO.SUBCOUNTY = reader.GetValue(reader.GetOrdinal("SUBCOUNTY")).ToString();

            if (ColumnExists(reader, "VILLAGE") && !reader.IsDBNull(reader.GetOrdinal("VILLAGE")))
                oLandInfoResBO.VILLAGE = reader.GetValue(reader.GetOrdinal("VILLAGE")).ToString();

            if (ColumnExists(reader, "STR_TENUREID") && !reader.IsDBNull(reader.GetOrdinal("STR_TENUREID")))
                oLandInfoResBO.TenureId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("STR_TENUREID")).ToString());

            if (ColumnExists(reader, "TENURE") && !reader.IsDBNull(reader.GetOrdinal("TENURE")))
                oLandInfoResBO.TENURE = reader.GetValue(reader.GetOrdinal("TENURE")).ToString();

            if (ColumnExists(reader, "TOTALSIZE") && !reader.IsDBNull(reader.GetOrdinal("TOTALSIZE")))
                oLandInfoResBO.TOTALSIZE = (Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TOTALSIZE"))));

            if (ColumnExists(reader, "ISPRIMARYRESIDENCE") && !reader.IsDBNull(reader.GetOrdinal("ISPRIMARYRESIDENCE")))
                oLandInfoResBO.ISPRIMARYRESIDENCE = reader.GetValue(reader.GetOrdinal("ISPRIMARYRESIDENCE")).ToString();

            if (ColumnExists(reader, "ISAFFECTED") && !reader.IsDBNull(reader.GetOrdinal("ISAFFECTED")))
                oLandInfoResBO.ISAFFECTED = reader.GetValue(reader.GetOrdinal("ISAFFECTED")).ToString();

            //if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
            //    oBatchBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            return oLandInfoResBO;
        }

        /// <summary>
        ///         To Check Weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region ADD/UPDATE/DELETE
        /// <summary>
        /// To Add Land Info Respondents
        /// </summary>
        /// <param name="objLIRBO"></param>
        public void AddLandInfoRespondents(LandInfoRespondentsBO objLIRBO)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_INS_LND_RESP_HOLDNG", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("LND_HOLDINGID_", objLIRBO.HOLDINGID);
                myCommand.Parameters.Add("HHID_", objLIRBO.HID);

                if (objLIRBO.LND_TYPEID == 0)
                    myCommand.Parameters.Add("LND_TYPEID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("LND_TYPEID_", objLIRBO.LND_TYPEID);

                if (objLIRBO.LND_USEID == 0)
                    myCommand.Parameters.Add("LND_USEID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("LND_USEID_", objLIRBO.LND_USEID);

                myCommand.Parameters.Add("DISTRICT_", objLIRBO.DISTRICT);
                myCommand.Parameters.Add("COUNTY_", objLIRBO.COUNTY);
                myCommand.Parameters.Add("SUBCOUNTY_", objLIRBO.SUBCOUNTY);
                myCommand.Parameters.Add("VILLAGE_", objLIRBO.VILLAGE);

                if (objLIRBO.TenureId == 0)
                    myCommand.Parameters.Add("STR_TENUREID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("STR_TENUREID_", objLIRBO.TenureId);

                myCommand.Parameters.Add("TENURE_", objLIRBO.TENURE);

                if (objLIRBO.TOTALSIZE == -1)
                    myCommand.Parameters.Add("TOTALSIZE_", DBNull.Value);
                else
                    myCommand.Parameters.Add("TOTALSIZE_", objLIRBO.TOTALSIZE);

                myCommand.Parameters.Add("ISPRIMARYRESIDENCE_", objLIRBO.ISPRIMARYRESIDENCE);
                myCommand.Parameters.Add("ISAFFECTED_", objLIRBO.ISAFFECTED);
                myCommand.Parameters.Add("ISDELETEDIN", OracleDbType.Varchar2, 5).Value = "False";
                myCommand.Parameters.Add("CREATEDBY", objLIRBO.UpdatedBy);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }


        }

        /// <summary>
        /// To Delete Land Info Respondents
        /// </summary>
        /// <param name="holdingID"></param>
        /// <returns></returns>
        public int DeleteLandInfoRespondents(int holdingID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_DEL_LND_RESP_HOLDNG", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LND_HOLDINGID", OracleDbType.Int64, 5).Value = holdingID;
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// To Update Land Info Respondents
        /// </summary>
        /// <param name="objLIRBO"></param>
        /// <returns></returns>
        public int UpdateLandInfoRespondents(LandInfoRespondentsBO objLIRBO)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_LND_RESP_HOLDNG", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("LND_HOLDINGID_", objLIRBO.HOLDINGID);
                myCommand.Parameters.Add("HHID_", objLIRBO.HID);

                if (objLIRBO.LND_TYPEID == 0)
                    myCommand.Parameters.Add("LND_TYPEID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("LND_TYPEID_", objLIRBO.LND_TYPEID);

                if (objLIRBO.LND_USEID == 0)
                    myCommand.Parameters.Add("LND_USEID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("LND_USEID_", objLIRBO.LND_USEID);

                myCommand.Parameters.Add("DISTRICT_", objLIRBO.DISTRICT);
                myCommand.Parameters.Add("COUNTY_", objLIRBO.COUNTY);
                myCommand.Parameters.Add("SUBCOUNTY_", objLIRBO.SUBCOUNTY);
                myCommand.Parameters.Add("VILLAGE_", objLIRBO.VILLAGE);

                if (objLIRBO.TenureId == 0)
                    myCommand.Parameters.Add("STR_TENUREID_", DBNull.Value);
                else
                    myCommand.Parameters.Add("STR_TENUREID_", objLIRBO.TenureId);
                
                myCommand.Parameters.Add("TENURE_", objLIRBO.TENURE);

                if (objLIRBO.TOTALSIZE == -1)
                    myCommand.Parameters.Add("TOTALSIZE_", DBNull.Value);
                else
                    myCommand.Parameters.Add("TOTALSIZE_", objLIRBO.TOTALSIZE);

                myCommand.Parameters.Add("ISPRIMARYRESIDENCE_", objLIRBO.ISPRIMARYRESIDENCE);
                myCommand.Parameters.Add("ISAFFECTED_", objLIRBO.ISAFFECTED);
                //myCommand.Parameters.Add("ISAFFECTED_", OracleDbType.Varchar2, 5).Value = "False";
                myCommand.Parameters.Add("UPDATEDBYIN", objLIRBO.UpdatedBy);



                //myCommand.Parameters.Add("LND_HOLDINGID_", objLIRBLL.HOLDINGID);
                //myCommand.Parameters.Add("HHID_", objLIRBLL.HID);
                //myCommand.Parameters.Add("LND_TYPEID_", objLIRBLL.LND_TYPEID);
                //myCommand.Parameters.Add("LND_USEID_", objLIRBLL.LND_USEID);
                //myCommand.Parameters.Add("DISTRICT_", objLIRBLL.DISTRICT);
                //myCommand.Parameters.Add("COUNTY_", objLIRBLL.COUNTY);
                //myCommand.Parameters.Add("SUBCOUNTY_", objLIRBLL.SUBCOUNTY);
                //myCommand.Parameters.Add("VILLAGE_", objLIRBLL.VILLAGE);
                //myCommand.Parameters.Add("TENURE_", objLIRBLL.TENURE);
                //myCommand.Parameters.Add("TOTALSIZE_", objLIRBLL.TOTALSIZE);
                //myCommand.Parameters.Add("ISPRIMARYRESIDENCE_", objLIRBLL.ISPRIMARYRESIDENCE);
                //myCommand.Parameters.Add("ISAFFECTED_", objLIRBLL.ISAFFECTED);

                //myCommand.Parameters.Add("UPDATEDBY", objLIRBLL.Userid);
                //}

                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();

            }
            return result;
        }
        #endregion
    }
}