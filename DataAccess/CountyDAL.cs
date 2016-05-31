using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class CountyDAL
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
       public CountyList GetCounty(string districtID)
       {
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
           string proc = "USP_MST_GET_COUNTY";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("DISTRICTID_", districtID);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           CountyBO objcountBO = null;
           CountyList objcountlist = new CountyList();

           while (dr.Read())
           {
               objcountBO = new CountyBO();
               objcountBO.CountyID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COUNTYID"))));
               objcountBO.CountyName = dr.GetValue(dr.GetOrdinal("COUNTYNAME")).ToString();
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
       public CountyList GetAllCounties(int districtID)
       {
           CountyList objCountyList = null;

           using (cnn = new OracleConnection(connStr))
           {
               using (cmd = new OracleCommand("USP_MST_GET_COUNTY_ALL", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;

                   cmd.Parameters.Add("DistrictIDIN", districtID);
                   cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                   cmd.Connection.Open();

                   OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                   CountyBO objCountyBO = null;
                   objCountyList = new CountyList();

                   while (dr.Read())
                   {
                       objCountyBO = new CountyBO();
                       if (!dr.IsDBNull(dr.GetOrdinal("COUNTYID"))) objCountyBO.CountyID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("COUNTYID")));
                       if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objCountyBO.CountyName = dr.GetValue(dr.GetOrdinal("COUNTYNAME")).ToString();
                       if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objCountyBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                       if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objCountyBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
                       // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objCountyBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                       objCountyList.Add(objCountyBO);
                   }

                   dr.Close();
               }
           }

           return objCountyList;
       }
       /// <summary>
       /// To insert data to database
       /// </summary>
       /// <param name="objCountyBO"></param>
       /// <returns></returns>
       public string AddCounty(CountyBO objCountyBO)
       {
           string result = "";

           using (cnn = new OracleConnection(connStr))
           {
               using (cmd = new OracleCommand("USP_MST_INS_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.Add("DISTRICTID_", objCountyBO.DistrictID);
                   cmd.Parameters.Add("COUNTYNAME_", objCountyBO.CountyName);
                   cmd.Parameters.Add("CREATEDBY_", objCountyBO.CreatedBy);
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
       /// <param name="objCountyBO"></param>
       /// <returns></returns>
       public string UpdateCounty(CountyBO objCountyBO)
       {
           string result = "";

           using (cnn = new OracleConnection(connStr))
           {
               using (cmd = new OracleCommand("USP_MST_UPD_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.Add("COUNTYID_", objCountyBO.CountyID);
                   cmd.Parameters.Add("COUNTYNAME_", objCountyBO.CountyName);
                   cmd.Parameters.Add("UPDATEDBY_", objCountyBO.UpdatedBy);
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
       /// <param name="countyID"></param>
       /// <returns></returns>
       public string DeleteCounty(int countyID)
       {
           string result = "";

           using (cnn = new OracleConnection(connStr))
           {
               using (cmd = new OracleCommand("USP_MST_DEL_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.Add("COUNTYID_", countyID);
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
       /// <param name="countyID"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteCounty(int countyID, string isDeleted, int updatedBy)
       {
           string result = "";

           using (cnn = new OracleConnection(connStr))
           {
               using (cmd = new OracleCommand("USP_MST_OBS_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.Add("COUNTYID_", countyID);
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
       /// <param name="countyID"></param>
       /// <returns></returns>
       public CountyBO GetCountyById(int countyID)
       {
           proc = "USP_MST_GET_COUNTYBYID";

           cnn = new OracleConnection(connStr);
           CountyBO CountyBOobj = null;

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("COUNTYID_", countyID);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           try
           {
               cmd.Connection.Open();
               OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               while (dr.Read())
               {
                   CountyBOobj = new CountyBO();

                   if (!dr.IsDBNull(dr.GetOrdinal("COUNTYID"))) CountyBOobj.CountyID = dr.GetInt32(dr.GetOrdinal("COUNTYID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) CountyBOobj.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
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
       /// <param name="County"></param>
       /// <returns></returns>

       public CountyList GetCounties(string County)
       {
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
           string proc = "USP_MST_SER_COUNTY";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;           
           cmd.Parameters.Add("COUNTYNAME_", County);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           CountyBO objcountBO = null;
           CountyList objcountlist = new CountyList();

           while (dr.Read())
           {
               objcountBO = new CountyBO();
               if (!dr.IsDBNull(dr.GetOrdinal("COUNTYID"))) objcountBO.CountyID = dr.GetInt32(dr.GetOrdinal("COUNTYID"));
               if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) objcountBO.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
               if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) objcountBO.CountyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
               if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objcountBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
               if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) objcountBO.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
               objcountlist.Add(objcountBO);
           }

           dr.Close();
           return objcountlist;
       }

    }
}
