using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_DataAccess;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class GraveDAL
    {
       public GraveList GetGraveFinish()
           {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_GRAVEDATA";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GraveBO BOobj = null;
            GraveList Listobj = new GraveList();

            while (dr.Read())
            {
                BOobj = new GraveBO();
                BOobj.Grv_finishid = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRV_FINISHID"))));
                BOobj.Grv_finishtype = dr.GetValue(dr.GetOrdinal("GRV_FINISHTYPE")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;

        }
        
       /// <summary>
       /// to insert data
       /// </summary>
       /// <param name="GraveBOobj"></param>
       /// <returns></returns>
       public int Insert(GraveBO GraveBOobj)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_TRN_INS_GRAVE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("HHID", GraveBOobj.HouseholdID);
             //  dcmd.Parameters.Add("GRV_FINISHID", GraveBOobj.Grv_finishid);

               if (GraveBOobj.Grv_finishid > 0)
                   dcmd.Parameters.Add("GRV_FINISHID", GraveBOobj.Grv_finishid);
               else
                   dcmd.Parameters.Add("GRV_FINISHID", DBNull.Value);
                
               dcmd.Parameters.Add("OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
               dcmd.Parameters.Add("GRV_DIMEN_LENGTH", GraveBOobj.Grv_dimen_length);
               dcmd.Parameters.Add("GRV_DIMEN_WIDTH", GraveBOobj.Grv_dimen_width);
               dcmd.Parameters.Add("DEPRECIATEDVALUE", GraveBOobj.Depreciatedvalue);
               dcmd.Parameters.Add("CREATEDBY", GraveBOobj.CreatedBy);
               if (GraveBOobj.Photo != null)
                   dcmd.Parameters.Add("GRAVEPHOTO_", OracleDbType.Blob).Value = GraveBOobj.Photo;
               else
                   dcmd.Parameters.Add("GRAVEPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
              
               return dcmd.ExecuteNonQuery();

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
       /// to fetch details
       /// </summary>
       /// <param name="householdID"></param>
       /// <returns></returns>
       public GraveList GetGravedata(int householdID)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_SEL_GRAVE";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("HHID_", householdID);
           cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

           GraveBO GraveBOobj = null;
           GraveList GraveListobj = new GraveList();


           while (dr.Read())
           {
               GraveBOobj = new GraveBO();
               GraveBOobj.Pap_graveid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_GRAVEID")));
               GraveBOobj.Grv_finishtype = dr.GetString(dr.GetOrdinal("GRV_FINISHTYPE"));
               GraveBOobj.Grv_dimen_length = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("GRV_DIMEN_LENGTH")));
               GraveBOobj.Grv_dimen_width = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("GRV_DIMEN_WIDTH")));
               GraveBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DEPRECIATEDVALUE")));
               GraveBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

               GraveListobj.Add(GraveBOobj);
           }

           dr.Close();

           return GraveListobj;
       }
       /// <summary>
       /// to fetch details
       /// </summary>
       /// <param name="Pap_graveid"></param>
       /// <returns></returns>
       public GraveBO Getdatarow(int Pap_graveid)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;

           string proc = "USP_TRN_GET_GRAVE";

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("G_PAP_GRAVEID", Pap_graveid);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           GraveBO GraveBOobj = null;
           GraveList GraveListobj = new GraveList();
         
           GraveBOobj = new GraveBO();
           while (dr.Read())
           {

               if (!dr.IsDBNull(dr.GetOrdinal("pap_graveid")))
                   GraveBOobj.Pap_graveid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_graveid")));

               if (!dr.IsDBNull(dr.GetOrdinal("grv_finishid")))
                   GraveBOobj.Grv_finishid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grv_finishid")));

               if (!dr.IsDBNull(dr.GetOrdinal("grv_dimen_length")))
                    GraveBOobj.Grv_dimen_length = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("grv_dimen_length")));

                if (!dr.IsDBNull(dr.GetOrdinal("grv_dimen_width")))
                    GraveBOobj.Grv_dimen_width = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("grv_dimen_width")));

                if (!dr.IsDBNull(dr.GetOrdinal("depreciatedvalue")))
                    GraveBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("depreciatedvalue")));

                if (!dr.IsDBNull(dr.GetOrdinal("othergravefinish")))
                    GraveBOobj.Othergravefinish = dr.GetString(dr.GetOrdinal("othergravefinish"));
             }
           dr.Close();


           return GraveBOobj;
       }
       /// <summary>
       /// to update data
       /// </summary>
       /// <param name="GraveBOobj"></param>
       /// <returns></returns>
       public int EditGRAVE(GraveBO GraveBOobj)
       {
           OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_GRAVE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.Add("HHID", GraveBOobj.HouseholdID);
               dcmd.Parameters.Add("G_PAP_GRAVEID", GraveBOobj.Pap_graveid);
               dcmd.Parameters.Add("G_GRV_FINISHID", GraveBOobj.Grv_finishid);
               dcmd.Parameters.Add("G_OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
               dcmd.Parameters.Add("G_GRV_DIMEN_LENGTH", GraveBOobj.Grv_dimen_length);
               dcmd.Parameters.Add("G_GRV_DIMEN_WIDTH", GraveBOobj.Grv_dimen_width);
               dcmd.Parameters.Add("G_DEPRECIATEDVALUE", GraveBOobj.Depreciatedvalue);
               dcmd.Parameters.Add("G_UPDATEDBY", GraveBOobj.CreatedBy);
               if (GraveBOobj.Photo != null)
                   dcmd.Parameters.Add("GRAVEPHOTO_", OracleDbType.Blob).Value = GraveBOobj.Photo;
               else
                   dcmd.Parameters.Add("GRAVEPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
                                        
               return dcmd.ExecuteNonQuery();
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
       /// to delete data
       /// </summary>
       /// <param name="Pap_graveid"></param>
       /// <returns></returns>
       public int Delete(int Pap_graveid)
       {
           OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
           conn.Open();
           OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_GRAVE", conn);
           dCmd.CommandType = CommandType.StoredProcedure;
           try
           {
               dCmd.Parameters.Add("PAP_GRAVEID_", Pap_graveid);
               return dCmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally
           {
               dCmd.Dispose();
               conn.Close();
               conn.Dispose();

           }
       }

     
       /// <summary>
       /// to get Image File for DataBase
       /// </summary>
       /// <param name="householdID"></param>
       /// <param name="PermanentStructureID"></param>
       /// <returns></returns>
       public GraveBO ShowPAPGRAVE(int householdID, int PermanentStructureID)
       {
           OracleConnection myConnection;
           OracleCommand myCommand;
           myConnection = new OracleConnection(AppConfiguration.ConnectionString);
           myCommand = new OracleCommand("USP_TRN_GET_GRAVE_PHOTO", myConnection);
           myCommand.Connection = myConnection;
           myCommand.CommandType = CommandType.StoredProcedure;
           myCommand.Parameters.Add("HHID_", householdID);
           myCommand.Parameters.Add("PermanentStructureID_", PermanentStructureID);
           myCommand.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           myCommand.Connection.Open();
           object img = myCommand.ExecuteScalar();

           try
           {
               byte[] papPhotoBytes = (byte[])img;

               GraveBO objPAPGRAVE = new GraveBO();
               objPAPGRAVE.Photo = papPhotoBytes;
               return objPAPGRAVE;
               //return new System.IO.MemoryStream(papPhotoBytes);
           }
           catch (Exception ex)
           {
               return null;
           }
           finally
           {
               myConnection.Close();
           }
       }
    }
}
