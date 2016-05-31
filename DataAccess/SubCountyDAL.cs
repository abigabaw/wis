using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class SubCountyDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

      /// <summary>
        /// To Get Sub County
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public SubCountyList GetSubCounty(string countyID)
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_GET_SUBCOUNTY";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@CountyIDIN", countyID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          SubCountyBO objsubcountBO = null;
          SubCountyList objsubcountlist = new SubCountyList();

          while (dr.Read())
          {
              objsubcountBO = new SubCountyBO();
              objsubcountBO.SubCountyID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUBCOUNTYID"))));
              objsubcountBO.SubCountyName = dr.GetValue(dr.GetOrdinal("SUBCOUNTYNAME")).ToString();
              objsubcountlist.Add(objsubcountBO);
          }

          dr.Close();
          return objsubcountlist;
      }

      /// <summary>
      /// To Get All Sub Counties
      /// </summary>
      /// <param name="COUNTYID"></param>
      /// <returns></returns>
      public SubCountyList GetAllSubCounties(int COUNTYID)
      {
          SubCountyList objSubCountyList = null;

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_GET_SUBCOUNTY_ALL", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.Add("CountyIDIN", COUNTYID);
                  cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                  SubCountyBO objSubCountyBO = null;
                  objSubCountyList = new SubCountyList();

                  while (dr.Read())
                  {
                      objSubCountyBO = new SubCountyBO();
                      if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objSubCountyBO.CountyName = dr.GetValue(dr.GetOrdinal("COUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objSubCountyBO.DistrictName = dr.GetValue(dr.GetOrdinal("DISTRICTNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYID"))) objSubCountyBO.SubCountyID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("SUBCOUNTYID")));
                      if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) objSubCountyBO.SubCountyName = dr.GetValue(dr.GetOrdinal("SUBCOUNTYNAME")).ToString();
                      if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objSubCountyBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                     // if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objSubCountyBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
                      // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objCountyBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                      objSubCountyList.Add(objSubCountyBO);
                  }

                  dr.Close();
              }
          }

          return objSubCountyList;
      }

      /// <summary>
      /// To Add Sub County
      /// </summary>
      /// <param name="objSubCountyBO"></param>
      /// <returns></returns>
      public string AddSubCounty(SubCountyBO objSubCountyBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_INS_SUBCOUNTY", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  //cmd.Parameters.Add("DISTRICTID_", objSubCountyBO.DistrictID);
                  cmd.Parameters.Add("COUNTYID_", objSubCountyBO.CountyID);
                  cmd.Parameters.Add("SUBCOUNTYNAME_", objSubCountyBO.SubCountyName);
                 
                  cmd.Parameters.Add("CREATEDBY_", objSubCountyBO.CreatedBy);
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
      /// To Update Sub County
      /// </summary>
      /// <param name="objSubCountyBO"></param>
      /// <returns></returns>
      public string UpdateSubCounty(SubCountyBO objSubCountyBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_UPD_SUBCOUNTY", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("SUBCOUNTYID_", objSubCountyBO.SubCountyID);
                  cmd.Parameters.Add("COUNTYID_", objSubCountyBO.CountyID);
                  cmd.Parameters.Add("SUBCOUNTYNAME_", objSubCountyBO.SubCountyName);
                  cmd.Parameters.Add("UPDATEDBY_", objSubCountyBO.CreatedBy);
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
      /// To Delete Sub County
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public string DeleteSubCounty(int SUBCOUNTYID)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_DEL_SUBCOUNTY", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("SUBCOUNTYID_", SUBCOUNTYID);
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
      /// To Get Sub County By Id
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public SubCountyBO GetSubCountyById(int SUBCOUNTYID)
      {
          proc = "USP_MST_GET_SUBCOUNTYBYID";

          cnn = new OracleConnection(connStr);
          SubCountyBO SubCountyBOobj = null;

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("SUBCOUNTYID_", SUBCOUNTYID);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          try
          {
              cmd.Connection.Open();
              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              while (dr.Read())
              {
                  SubCountyBOobj = new SubCountyBO();

                  if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) SubCountyBOobj.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
                  if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) SubCountyBOobj.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
                  if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYID"))) SubCountyBOobj.SubCountyID = dr.GetInt32(dr.GetOrdinal("SUBCOUNTYID"));
                  if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) SubCountyBOobj.SubCountyName = dr.GetString(dr.GetOrdinal("SUBCOUNTYNAME"));
                  //if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) CountyBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                 // if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) SubCountyBOobj.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
              }
              dr.Close();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return SubCountyBOobj;
      }

      /// <summary>
      /// To Obsolete Sub County
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <param name="ISDELETED"></param>
      /// <param name="UPDATEDBY"></param>
      /// <returns></returns>
      public string ObsoleteSubCounty(int SUBCOUNTYID, string ISDELETED, int UPDATEDBY)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_OBS_SUBCOUNTY", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("SUBCOUNTYID_", SUBCOUNTYID);
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

      /// <summary>
      /// To Get Sub Counties
      /// </summary>
      /// <param name="subCounty"></param>
      /// <returns></returns>
      public SubCountyList GetSubCounties(string subCounty)
      {
          OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;
          string proc = "USP_MST_SER_SUBCOUNTYSEARCH";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("SubCountyName_", subCounty);
          //cmd.Parameters.Add("COUNTYID_", CountyID);
          //cmd.Parameters.Add("SUBCOUNTYNAME_", subCounty);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          SubCountyBO objsubcountBO = null;
          SubCountyList objsubcountlist = new SubCountyList();

          while (dr.Read())
          {
              objsubcountBO = new SubCountyBO();
              if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objsubcountBO.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
              if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objsubcountBO.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
              if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYID"))) objsubcountBO.SubCountyID = dr.GetInt32(dr.GetOrdinal("SUBCOUNTYID"));
              if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) objsubcountBO.SubCountyName = dr.GetString(dr.GetOrdinal("SUBCOUNTYNAME"));
              if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objsubcountBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
              //if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objsubcountBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
              objsubcountlist.Add(objsubcountBO);
          }

          dr.Close();
          return objsubcountlist;
      }
    }
}
