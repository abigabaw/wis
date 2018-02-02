using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class CountyDAL
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
       public CountyList GetCounty(string districtID)
       {
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_GET_COUNTY";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("DISTRICTID_", districtID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

           using (cnn = new SqlConnection(connStr))
           {
               using (cmd = new SqlCommand("USP_MST_GET_COUNTY_ALL", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;

                   cmd.Parameters.AddWithValue("DistrictIDIN", districtID);
                   // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                   cmd.Connection.Open();

                   SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

           using (cnn = new SqlConnection(connStr))
           {
               using (cmd = new SqlCommand("USP_MST_INS_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.AddWithValue("DISTRICTID_", objCountyBO.DistrictID);
                   cmd.Parameters.AddWithValue("COUNTYNAME_", objCountyBO.CountyName);
                   cmd.Parameters.AddWithValue("CREATEDBY_", objCountyBO.CreatedBy);
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
       /// <param name="objCountyBO"></param>
       /// <returns></returns>
       public string UpdateCounty(CountyBO objCountyBO)
       {
           string result = "";

           using (cnn = new SqlConnection(connStr))
           {
               using (cmd = new SqlCommand("USP_MST_UPD_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.AddWithValue("COUNTYID_", objCountyBO.CountyID);
                   cmd.Parameters.AddWithValue("COUNTYNAME_", objCountyBO.CountyName);
                   cmd.Parameters.AddWithValue("UPDATEDBY_", objCountyBO.UpdatedBy);
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
       /// <param name="countyID"></param>
       /// <returns></returns>
       public string DeleteCounty(int countyID)
       {
           string result = "";

           using (cnn = new SqlConnection(connStr))
           {
               using (cmd = new SqlCommand("USP_MST_DEL_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.AddWithValue("COUNTYID_", countyID);
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
       /// <param name="countyID"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteCounty(int countyID, string isDeleted, int updatedBy)
       {
           string result = "";

           using (cnn = new SqlConnection(connStr))
           {
               using (cmd = new SqlCommand("USP_MST_OBS_COUNTY", cnn))
               {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Connection.Open();

                   cmd.Parameters.AddWithValue("COUNTYID_", countyID);
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
       /// <param name="countyID"></param>
       /// <returns></returns>
       public CountyBO GetCountyById(int countyID)
       {
           proc = "USP_MST_GET_COUNTYBYID";

           cnn = new SqlConnection(connStr);
           CountyBO CountyBOobj = null;

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("COUNTYID_", countyID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           try
           {
               cmd.Connection.Open();
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_SER_COUNTY";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;           
           cmd.Parameters.AddWithValue("COUNTYNAME_", County);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
