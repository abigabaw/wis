using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class MaxCapDAL
  {
      string connStr = AppConfiguration.ConnectionString;
      SqlConnection cnn;
      SqlCommand cmd;
      string proc = string.Empty;
      /// <summary>
      /// To fetch details
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public MaxCapList GetMaxCapByDist(string districtID)
      {
          SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;
          string proc = "USP_MST_GET_MAXCAP";
          cmd = new SqlCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("DistrictIDIN", districtID);
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

          using (cnn = new SqlConnection(connStr))
          {
              using (cmd = new SqlCommand("USP_MST_GET_MAXCAP_ALL", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.AddWithValue("PROJECTID_", ProjectId);
                  // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

          using (cnn = new SqlConnection(connStr))
          {
              using (cmd = new SqlCommand("USP_MST_INS_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.AddWithValue("DISTRICTID_", objMaxCapBO.DistrictID);
                  cmd.Parameters.AddWithValue("MAXCAPVAL_", objMaxCapBO.MaxCapVal);
                  cmd.Parameters.AddWithValue("PROJECTID_", objMaxCapBO.ProjectID);                   
                  //cmd.Parameters.AddWithValue("MAXCAPID_", objMaxCapBO.MaxCapID);
                  cmd.Parameters.AddWithValue("CREATEDBY_", objMaxCapBO.CreatedBy);
                  /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

          using (cnn = new SqlConnection(connStr))
          {
              using (cmd = new SqlCommand("USP_MST_UPD_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.AddWithValue("MAXCAPID_", objMaxCapBO.MaxCapID);
                  cmd.Parameters.AddWithValue("DISTRICTID_", objMaxCapBO.DistrictID);
                  cmd.Parameters.AddWithValue("MAXCAPVAL_", objMaxCapBO.MaxCapVal);
                  cmd.Parameters.AddWithValue("PROJECTID_", objMaxCapBO.ProjectID);
                  cmd.Parameters.AddWithValue("UPDATEDBY_", objMaxCapBO.UpdatedBy);
                  /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

          using (cnn = new SqlConnection(connStr))
          {
              using (cmd = new SqlCommand("USP_MST_DEL_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.AddWithValue("MAXCAPID_", maxCapID);
                  /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

          using (cnn = new SqlConnection(connStr))
          {
              using (cmd = new SqlCommand("USP_MST_OBS_MAXCAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.AddWithValue("MAXCAPID_", maxCapID);
                  cmd.Parameters.AddWithValue("ISDELETED_", isDeleted);
                  cmd.Parameters.AddWithValue("UPDATEDBY_", updatedBy);

                  /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

          cnn = new SqlConnection(connStr);
          MaxCapBO CountyBOobj = null;

          cmd = new SqlCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("MAXCAPID_", maxCapID);
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
          try
          {
              cmd.Connection.Open();
              SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;
          string proc = "USP_MST_SER_MAXCAP";
          cmd = new SqlCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("MAXCAPVAL_", MaxCap);
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
