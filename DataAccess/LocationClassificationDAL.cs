using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class LocationClassificationDAL
    {
       /// <summary>
        /// To INSERT location
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string INSERTlocation(LocationClassificationBO BOobj)
       {
           string returnResult = "";
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_MST_INS_LOCATION", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.AddWithValue("LOCTNCLASFCTNNAME_", BOobj.LOCTNCLASFCTNNAME);
               dcmd.Parameters.AddWithValue("LOCTNCODE_", BOobj.LOCTNCODE);
               dcmd.Parameters.AddWithValue("CREATEDBY_", BOobj.CREATEDBY);
               dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

               return returnResult;
           }
           catch
           {

               throw;
           }
           finally
           {
               dcmd.Dispose();
               cnn.Close();
               cnn.Dispose();
           }
       }

       /// <summary>
       /// To UPDATE location
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string UPDATElocation(LocationClassificationBO BOobj)
       {
           string returnResult = "";
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_MST_UPD_LOCATION", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);
           try
           {
               dcmd.Parameters.AddWithValue("LOCTNCLASFCTNID_", BOobj.LOCTNCLASFCTNID);
               dcmd.Parameters.AddWithValue("LOCTNCLASFCTNNAME_", BOobj.LOCTNCLASFCTNNAME);
               dcmd.Parameters.AddWithValue("LOCTNCODE_", BOobj.LOCTNCODE);
               dcmd.Parameters.AddWithValue("CREATEDBY_", BOobj.CREATEDBY);
               dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
               dcmd.ExecuteNonQuery();

               if (dcmd.Parameters["errorMessage_"].Value != null)
                   returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();

               return returnResult;
               
           }
           catch
           {
               throw;
           }
           finally
           {
               dcmd.Dispose();
               cnn.Close();
               cnn.Dispose();

           }
       }

       /// <summary>
       /// To Get All LOCATION
       /// </summary>
       /// <returns></returns>
       public LocationClassificationList GetallLOCATION()
       {
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_GETALL_LOCATION";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           LocationClassificationBO BOobj = null;
           LocationClassificationList objlocation = new LocationClassificationList();

           while (dr.Read())
           {
               BOobj = new LocationClassificationBO();
               BOobj.LOCTNCLASFCTNID = dr.GetInt32(dr.GetOrdinal("LOCTNCLASFCTNID"));
               BOobj.LOCTNCLASFCTNNAME = dr.GetString(dr.GetOrdinal("LOCTNCLASFCTNNAME"));
               BOobj.LOCTNCODE = dr.GetString(dr.GetOrdinal("LOCTNCODE"));
               BOobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));
               objlocation.Add(BOobj);
           }

           dr.Close();
           return objlocation;
       }

       /// <summary>
       /// To Get LOCATION Classification
       /// </summary>
       /// <returns></returns>
       public LocationClassificationList GetLOCATIONClassification()
       {
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_MST_GET_LOCATIONCLS";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           LocationClassificationBO BOobj = null;
           LocationClassificationList objlocation = new LocationClassificationList();

           while (dr.Read())
           {
               BOobj = new LocationClassificationBO();
               BOobj.LOCTNCLASFCTNID = dr.GetInt32(dr.GetOrdinal("LOCTNCLASFCTNID"));
               BOobj.LOCTNCLASFCTNNAME = dr.GetString(dr.GetOrdinal("LOCTNCLASFCTNNAME"));
               BOobj.LOCTNCODE = dr.GetString(dr.GetOrdinal("LOCTNCODE"));
               BOobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));
               objlocation.Add(BOobj);
           }

           dr.Close();
           return objlocation;
       }

       /// <summary>
       /// To Get Location Classification ID
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <returns></returns>
       public LocationClassificationBO GetLOCTNCLASFCTNID(int LOCTNCLASFCTNID)
       {
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_MST_GET_LOCATION";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           LocationClassificationBO BOobj = null;
           LocationClassificationList listobj = new LocationClassificationList();


           BOobj = new LocationClassificationBO();
          
           while (dr.Read())
           {
               if (ColumnExists(dr, "LOCTNCLASFCTNNAME") && !dr.IsDBNull(dr.GetOrdinal("LOCTNCLASFCTNNAME")))
                   BOobj.LOCTNCLASFCTNNAME = dr.GetString(dr.GetOrdinal("LOCTNCLASFCTNNAME"));
               if (ColumnExists(dr, "LOCTNCODE") && !dr.IsDBNull(dr.GetOrdinal("LOCTNCODE")))
                   BOobj.LOCTNCODE = dr.GetString(dr.GetOrdinal("LOCTNCODE"));
                if (ColumnExists(dr, "LOCTNCLASFCTNID") && !dr.IsDBNull(dr.GetOrdinal("LOCTNCLASFCTNID")))
                   BOobj.LOCTNCLASFCTNID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LOCTNCLASFCTNID")));

           }
           dr.Close();


           return BOobj;
       }

       /// <summary>
       /// To Check Weather Column Exists or Not
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

       /// <summary>
       /// To Delete Location
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <returns></returns>
       public string DeleteLocation(int LOCTNCLASFCTNID)
       {
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;

           string result = string.Empty;
           try
           {
               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_DEL_Location", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
               myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
               myConnection.Open();
               myCommand.ExecuteNonQuery();
               if (myCommand.Parameters["errorMessage_"].Value != null)
                   result = myCommand.Parameters["errorMessage_"].Value.ToString();
           }
           catch (Exception ex)
           {
               if (ex.Message.Contains("ORA-02292"))
               {
                   result = "Selected Location Classification  is already in use. Cannot Delete";
               }
               else
               {
                   throw ex;
               }
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
       /// To Obsolete Location
       /// </summary>
       /// <param name="LOCTNCLASFCTNID"></param>
       /// <param name="IsDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteLocation(int LOCTNCLASFCTNID, string IsDeleted, int updatedBy)
       {
           SqlConnection myConnection = null;
           SqlCommand myCommand = null;
           string result = "";
           try
           {
               myConnection = new SqlConnection(AppConfiguration.ConnectionString);
               myCommand = new SqlCommand("USP_MST_OBS_LOCATION", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.AddWithValue("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
               myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
            //   myCommand.Parameters.AddWithValue("updatedBy_", updatedBy);
               myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
               myConnection.Open();
               myCommand.ExecuteNonQuery();
               if (myCommand.Parameters["errorMessage_"].Value != null)
                   result = myCommand.Parameters["errorMessage_"].Value.ToString();
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
    }
}
