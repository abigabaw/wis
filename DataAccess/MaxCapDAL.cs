using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class MaxCapDAL
  {
      string connStr = AppConfiguration.ConnectionString;
      OracleConnection cnn;
      OracleCommand cmd;
      string proc = string.Empty;
      /// <summary>
      /// To fetch details
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public MaxCapList GetMaxCapByDist(string districtID)
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_GET_MAXCAP";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("DistrictIDIN", districtID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          MaxCapBO objcountBO = null;
          MaxCapList objcountlist = new MaxCapList();

          while (dr.Read())
          {
              objcountBO = new MaxCapBO();
              objcountBO.MaxCapID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MAXCAPCROPDISTID"))));
              objcountBO.MaxCapVal = dr.GetDecimal(dr.GetOrdinal("MAXCAPSTNDRDRATE"));
              objcountlist.Add(objcountBO);
          }

          dr.Close();
          return objcountlist;
      }
      /// <summary>
      /// To fetch details
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public MaxCapList GetAllMaxCap(int ProjectId)
      {
          MaxCapList objCountyList = null;

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_GET_MAXCAP_ALL", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.Add("PROJECTID_", ProjectId);
                  cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                  MaxCapBO objMaxCapBO = null;
                  objCountyList = new MaxCapList();

                  while (dr.Read())
                  {
                      objMaxCapBO = new MaxCapBO();
                      if (!dr.IsDBNull(dr.GetOrdinal("MAXCAPCROPDISTID"))) objMaxCapBO.MaxCapID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MAXCAPCROPDISTID")));
                      if (!dr.IsDBNull(dr.GetOrdinal("MAXCAPSTNDRDRATE"))) objMaxCapBO.MaxCapVal = dr.GetDecimal(dr.GetOrdinal("MAXCAPSTNDRDRATE"));
                      if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objMaxCapBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                      if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objMaxCapBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
                      // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objMaxCapBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                      objCountyList.Add(objMaxCapBO);
                  }

                  dr.Close();
              }
          }

          return objCountyList;
      }
      /// <summary>
      /// To insert data to database
      /// </summary>
      /// <param name="objMaxCapBO"></param>
      /// <returns></returns>
      public string AddMaxCap(MaxCapBO objMaxCapBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_INS_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("DISTRICTID_", objMaxCapBO.DistrictID);
                  cmd.Parameters.Add("MAXCAPVAL_", objMaxCapBO.MaxCapVal);
                  cmd.Parameters.Add("PROJECTID_", objMaxCapBO.ProjectID);                   
                  //cmd.Parameters.Add("MAXCAPID_", objMaxCapBO.MaxCapID);
                  cmd.Parameters.Add("CREATEDBY_", objMaxCapBO.CreatedBy);
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
      /// To update details to database
      /// </summary>
      /// <param name="objMaxCapBO"></param>
      /// <returns></returns>
      public string UpdateMaxCap(MaxCapBO objMaxCapBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_UPD_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("MAXCAPID_", objMaxCapBO.MaxCapID);
                  cmd.Parameters.Add("DISTRICTID_", objMaxCapBO.DistrictID);
                  cmd.Parameters.Add("MAXCAPVAL_", objMaxCapBO.MaxCapVal);
                  cmd.Parameters.Add("PROJECTID_", objMaxCapBO.ProjectID);
                  cmd.Parameters.Add("UPDATEDBY_", objMaxCapBO.UpdatedBy);
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
      /// To delete data from database
      /// </summary>
      /// <param name="maxCapID"></param>
      /// <returns></returns>
      public string DeleteMaxCap(int maxCapID)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_DEL_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("MAXCAPID_", maxCapID);
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
      /// to make data obsolete
      /// </summary>
      /// <param name="maxCapID"></param>
      /// <param name="isDeleted"></param>
      /// <param name="updatedBy"></param>
      /// <returns></returns>
      public string ObsoleteMaxCap(int maxCapID, string isDeleted, int updatedBy)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_OBS_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("MAXCAPID_", maxCapID);
                  cmd.Parameters.Add("ISDELETED_", isDeleted);
                  cmd.Parameters.Add("UPDATEDBY_", updatedBy);

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
      /// To get details by ID
      /// </summary>
      /// <param name="maxCapID"></param>
      /// <returns></returns>
      public MaxCapBO GetMaxCapById(int maxCapID)
      {
          proc = "USP_MST_GET_MAXCAPBYID";

          cnn = new OracleConnection(connStr);
          MaxCapBO CountyBOobj = null;

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("MAXCAPID_", maxCapID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          try
          {
              cmd.Connection.Open();
              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              while (dr.Read())
              {
                  CountyBOobj = new MaxCapBO();

                  if (!dr.IsDBNull(dr.GetOrdinal("maxcapcropdistid"))) CountyBOobj.MaxCapID = dr.GetInt32(dr.GetOrdinal("maxcapcropdistid"));
                  if (!dr.IsDBNull(dr.GetOrdinal("MAXCAPSTNDRDRATE"))) CountyBOobj.MaxCapVal = dr.GetDecimal(dr.GetOrdinal("MAXCAPSTNDRDRATE"));
                  //if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) CountyBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                  if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) CountyBOobj.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
              }
              dr.Close();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return CountyBOobj;
      }
      /// <summary>
      /// To get details
      /// </summary>
      /// <param name="MaxCap"></param>
      /// <returns></returns>

      public MaxCapList GetMaxCap(string MaxCap)
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_SER_MAXCAP";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("MAXCAPVAL_", MaxCap);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          MaxCapBO objcountBO = null;
          MaxCapList objcountlist = new MaxCapList();

          while (dr.Read())
          {
              objcountBO = new MaxCapBO();
              if (!dr.IsDBNull(dr.GetOrdinal("MAXCAPCROPDISTID"))) objcountBO.MaxCapID = dr.GetInt32(dr.GetOrdinal("MAXCAPCROPDISTID"));
              if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) objcountBO.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
              if (!dr.IsDBNull(dr.GetOrdinal("MAXCAPSTNDRDRATE"))) objcountBO.MaxCapVal = dr.GetDecimal(dr.GetOrdinal("MAXCAPSTNDRDRATE"));
              if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objcountBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
              if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objcountBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
              objcountlist.Add(objcountBO);
          }

          dr.Close();
          return objcountlist;
      }

  }
}
