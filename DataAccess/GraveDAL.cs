using System;
using System.Data;
using System.Data.SqlClient;
using WIS_DataAccess;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class GraveDAL
    {
       public GraveList GetGraveFinish()
           {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_GRAVEDATA";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_TRN_INS_GRAVE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.AddWithValue("HHID", GraveBOobj.HouseholdID);
             //  dcmd.Parameters.AddWithValue("GRV_FINISHID", GraveBOobj.Grv_finishid);

               if (GraveBOobj.Grv_finishid > 0)
                   dcmd.Parameters.AddWithValue("GRV_FINISHID", GraveBOobj.Grv_finishid);
               else
                   dcmd.Parameters.AddWithValue("GRV_FINISHID", DBNull.Value);
                
               dcmd.Parameters.AddWithValue("OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
               dcmd.Parameters.AddWithValue("GRV_DIMEN_LENGTH", GraveBOobj.Grv_dimen_length);
               dcmd.Parameters.AddWithValue("GRV_DIMEN_WIDTH", GraveBOobj.Grv_dimen_width);
               dcmd.Parameters.AddWithValue("DEPRECIATEDVALUE", GraveBOobj.Depreciatedvalue);
               dcmd.Parameters.AddWithValue("CREATEDBY", GraveBOobj.CreatedBy);
               if (GraveBOobj.Photo != null)
                   dcmd.Parameters.AddWithValue("GRAVEPHOTO_", SqlDbType.Image).Value = GraveBOobj.Photo;
               else
                   dcmd.Parameters.AddWithValue("GRAVEPHOTO_", DBNull.Value);
              
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_TRN_SEL_GRAVE";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("HHID_", householdID);
           //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;

           string proc = "USP_TRN_GET_GRAVE";

           cmd = new SqlCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("G_PAP_GRAVEID", Pap_graveid);
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

           cmd.Connection.Open();

           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
           SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
           cnn.Open();
           SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_GRAVE", cnn);
           dcmd.CommandType = CommandType.StoredProcedure;
           int count = Convert.ToInt32(dcmd.CommandType);

           try
           {
               dcmd.Parameters.AddWithValue("HHID", GraveBOobj.HouseholdID);
               dcmd.Parameters.AddWithValue("G_PAP_GRAVEID", GraveBOobj.Pap_graveid);
               dcmd.Parameters.AddWithValue("G_GRV_FINISHID", GraveBOobj.Grv_finishid);
               dcmd.Parameters.AddWithValue("G_OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
               dcmd.Parameters.AddWithValue("G_GRV_DIMEN_LENGTH", GraveBOobj.Grv_dimen_length);
               dcmd.Parameters.AddWithValue("G_GRV_DIMEN_WIDTH", GraveBOobj.Grv_dimen_width);
               dcmd.Parameters.AddWithValue("G_DEPRECIATEDVALUE", GraveBOobj.Depreciatedvalue);
               dcmd.Parameters.AddWithValue("G_UPDATEDBY", GraveBOobj.CreatedBy);
               if (GraveBOobj.Photo != null)
                   dcmd.Parameters.AddWithValue("GRAVEPHOTO_", SqlDbType.Image).Value = GraveBOobj.Photo;
               else
                   dcmd.Parameters.AddWithValue("GRAVEPHOTO_", DBNull.Value);
                                        
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
           SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
           conn.Open();
           SqlCommand dCmd = new SqlCommand("USP_TRN_DEL_GRAVE", conn);
           dCmd.CommandType = CommandType.StoredProcedure;
           try
           {
               dCmd.Parameters.AddWithValue("PAP_GRAVEID_", Pap_graveid);
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
           SqlConnection myConnection;
           SqlCommand myCommand;
           myConnection = new SqlConnection(AppConfiguration.ConnectionString);
           myCommand = new SqlCommand("USP_TRN_GET_GRAVE_PHOTO", myConnection);
           myCommand.Connection = myConnection;
           myCommand.CommandType = CommandType.StoredProcedure;
           myCommand.Parameters.AddWithValue("HHID_", householdID);
           myCommand.Parameters.AddWithValue("PermanentStructureID_", PermanentStructureID);
           // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

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
