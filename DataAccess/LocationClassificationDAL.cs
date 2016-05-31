using System;
using Oracle.DataAccess.Client;
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
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_MST_INS_LOCATION", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("LOCTNCLASFCTNNAME_", BOobj.LOCTNCLASFCTNNAME);
               dcmd.Parameters.Add("LOCTNCODE_", BOobj.LOCTNCODE);
               dcmd.Parameters.Add("CREATEDBY_", BOobj.CREATEDBY);
               dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_MST_UPD_LOCATION", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);
           try
           {
               dcmd.Parameters.Add("LOCTNCLASFCTNID_", BOobj.LOCTNCLASFCTNID);
               dcmd.Parameters.Add("LOCTNCLASFCTNNAME_", BOobj.LOCTNCLASFCTNNAME);
               dcmd.Parameters.Add("LOCTNCODE_", BOobj.LOCTNCODE);
               dcmd.Parameters.Add("CREATEDBY_", BOobj.CREATEDBY);
               dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
           string proc = "USP_MST_GETALL_LOCATION";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
           string proc = "USP_MST_GET_LOCATIONCLS";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_MST_GET_LOCATION";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           OracleConnection myConnection = null;
           OracleCommand myCommand = null;

           string result = string.Empty;
           try
           {
               myConnection = new OracleConnection(AppConfiguration.ConnectionString);
               myCommand = new OracleCommand("USP_MST_DEL_Location", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.Add("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
               myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
           OracleConnection myConnection = null;
           OracleCommand myCommand = null;
           string result = "";
           try
           {
               myConnection = new OracleConnection(AppConfiguration.ConnectionString);
               myCommand = new OracleCommand("USP_MST_OBS_LOCATION", myConnection);
               myCommand.Connection = myConnection;
               myCommand.CommandType = CommandType.StoredProcedure;
               myCommand.Parameters.Add("LOCTNCLASFCTNID_", LOCTNCLASFCTNID);
               myCommand.Parameters.Add("isdeleted_", IsDeleted);
            //   myCommand.Parameters.Add("updatedBy_", updatedBy);
               myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
