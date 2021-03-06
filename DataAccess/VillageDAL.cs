﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class VillageDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

      /// <summary>
        /// To Get Village
      /// </summary>
      /// <param name="subcountyID"></param>
      /// <returns></returns>
      public VillageList GetVillage(string subcountyID)
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_GET_VILLAGE";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@SubcountyIDIN", subcountyID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          VillageBO objvBO = null;
          VillageList objvlist = new VillageList();

          while (dr.Read())
          {
              objvBO = new VillageBO();
              objvBO.VillageID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("VILLAGEID"))));
              objvBO.VillageName = dr.GetValue(dr.GetOrdinal("VILLAGENAME")).ToString();
              objvlist.Add(objvBO);
          }

          dr.Close();
          return objvlist;
      }

      /// <summary>
      /// To Search Village
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public VillageList SearchVillage(string val)
      {
           VillageList objVillageList = null;

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_SEARCH_VILLAGE", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.Add("VillageName_", val);
                  cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                  VillageBO objVillageBO = null;
                  objVillageList = new VillageList();

                  while (dr.Read())
                  {
                      objVillageBO = new VillageBO();
                      if (!dr.IsDBNull(dr.GetOrdinal("VILLAGEID"))) objVillageBO.VillageID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("VILLAGEID")));
                      if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYID"))) objVillageBO.SubCountyID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUBCOUNTYID")));
                      if (!dr.IsDBNull(dr.GetOrdinal("countyid"))) objVillageBO.CountyID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("countyid")));
                      if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objVillageBO .DistrictID= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("districtid")));
                      if (!dr.IsDBNull(dr.GetOrdinal("VILLAGENAME"))) objVillageBO.VillageName = dr.GetValue(dr.GetOrdinal("VILLAGENAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objVillageBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                      if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) objVillageBO.SubCountyName = dr.GetValue(dr.GetOrdinal("SUBCOUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objVillageBO.CountyName = dr.GetValue(dr.GetOrdinal("COUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objVillageBO.DistrictName = dr.GetValue(dr.GetOrdinal("DISTRICTNAME")).ToString();
                     objVillageList.Add(objVillageBO);
                  }

                  dr.Close();
              }
          }

          return objVillageList;
      }
 
   
      /// <summary>
      /// To Get All Village
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public VillageList GetAllVillage(int SUBCOUNTYID)
      {
          VillageList objVillageList = null;

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_GET_VILLAGE_ALL", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.Add("SubcountyIDIN", SUBCOUNTYID);
                  cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                  VillageBO objVillageBO = null;
                  objVillageList = new VillageList();

                  while (dr.Read())
                  {
                      objVillageBO = new VillageBO();
                      if (!dr.IsDBNull(dr.GetOrdinal("VILLAGEID"))) objVillageBO.VillageID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("VILLAGEID")));
                      if (!dr.IsDBNull(dr.GetOrdinal("VILLAGENAME"))) objVillageBO.VillageName = dr.GetValue(dr.GetOrdinal("VILLAGENAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objVillageBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                      if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) objVillageBO.SubCountyName = dr.GetValue(dr.GetOrdinal("SUBCOUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objVillageBO.CountyName = dr.GetValue(dr.GetOrdinal("COUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objVillageBO.DistrictName = dr.GetValue(dr.GetOrdinal("DISTRICTNAME")).ToString();
                       objVillageList.Add(objVillageBO);
                  }

                  dr.Close();
              }
          }

          return objVillageList;
      }

      /// <summary>
      /// To Add Village
      /// </summary>
      /// <param name="objVillageBO"></param>
      /// <returns></returns>
      public string AddVillage(VillageBO objVillageBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_INS_VILLAGE", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("SUBCOUNTYID_", objVillageBO.SubCountyID);
                  cmd.Parameters.Add("VILLAGENAME_", objVillageBO.VillageName);
                  cmd.Parameters.Add("CREATEDBY_", objVillageBO.CreatedBy);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Update Village
      /// </summary>
      /// <param name="objVillageBO"></param>
      /// <returns></returns>
      public string UpdateVillage(VillageBO objVillageBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_UPD_VILLAGE", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("VILLAGEID_", objVillageBO.VillageID);
                  cmd.Parameters.Add("SUBCOUNTYID_", objVillageBO.SubCountyID);
                  cmd.Parameters.Add("VILLAGENAME_", objVillageBO.VillageName);
                  cmd.Parameters.Add("UPDATEDBY_", objVillageBO.CreatedBy);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Delete Village
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <returns></returns>
      public string DeleteVillage(int VILLAGEID)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_DEL_VILLAGE", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("VILLAGEID_", VILLAGEID);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Get Village By Id
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <returns></returns>
      public VillageBO GetVillageById(int VILLAGEID)
      {
          proc = "USP_MST_GET_VILLAGEBYID";

          cnn = new OracleConnection(connStr);
          VillageBO VillageBOobj = null;

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("VILLAGEID_", VILLAGEID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          try
          {
              cmd.Connection.Open();
              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              while (dr.Read())
              {
                  VillageBOobj = new VillageBO();
                  if (!dr.IsDBNull(dr.GetOrdinal("VILLAGEID"))) VillageBOobj.VillageID = dr.GetInt32(dr.GetOrdinal("VILLAGEID"));
                  if (!dr.IsDBNull(dr.GetOrdinal("VILLAGENAME"))) VillageBOobj.VillageName = dr.GetString(dr.GetOrdinal("VILLAGENAME"));
                
                  if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) VillageBOobj.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
                  if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) VillageBOobj.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
               //   if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYID"))) VillageBOobj.SubCountyID = dr.GetInt32(dr.GetOrdinal("SUBCOUNTYID"));
                  if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) VillageBOobj.SubCountyName = dr.GetString(dr.GetOrdinal("SUBCOUNTYNAME"));
                  //if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) VillageBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                  // if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) VillageBOobj.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
              }
              dr.Close();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return VillageBOobj;
      }

      /// <summary>
      /// To Obsolete Village
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <param name="ISDELETED"></param>
      /// <param name="UPDATEDBY"></param>
      /// <returns></returns>
      public string ObsoleteVillage(int VILLAGEID, string ISDELETED, int UPDATEDBY)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_OBS_VILLAGE", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("VILLAGEID_", VILLAGEID);
                  cmd.Parameters.Add("ISDELETED_", ISDELETED);
                  cmd.Parameters.Add("UPDATEDBY_", UPDATEDBY);

                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

    }
}
