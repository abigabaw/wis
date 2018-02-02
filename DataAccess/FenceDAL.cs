using System;
using System.Data;
using System.Data.SqlClient;
using WIS_DataAccess;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
  public class FenceDAL
    {
      /// <summary>
      /// To fetch details from database
      /// </summary>
      /// <returns></returns>
      public FenceList GetFencedescription( )
        
            {
                SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
                SqlCommand cmd;
                string proc = "USP_TRN_GET_FENCEDATA";
                cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                FenceBO BOobj = null;
                FenceList Listobj = new FenceList();

                while (dr.Read())
                {
                    BOobj = new FenceBO();
                    BOobj.Fenceid = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FENCEID"))));
                    BOobj.Fencedescription = dr.GetValue(dr.GetOrdinal("FENCEDESCRIPTION")).ToString();

                    Listobj.Add(BOobj);
                }

                dr.Close();
                return Listobj;

            }
    /// <summary>
    /// to update details to database
    /// </summary>
    /// <param name="FenceBOobj"></param>
    /// <returns></returns>

      public int EditFence(FenceBO FenceBOobj)
      {
          SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
          cnn.Open();
          SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_FENCE", cnn);
          dcmd.CommandType = CommandType.StoredProcedure;
          int count = Convert.ToInt32(dcmd.CommandType);

          try
          {
              dcmd.Parameters.AddWithValue("HHID", FenceBOobj.HouseholdID);
              dcmd.Parameters.AddWithValue("F_PAP_FENCEID", FenceBOobj.Pap_fenceid);
              dcmd.Parameters.AddWithValue("F_FENCEID", FenceBOobj.Fenceid);
              //dcmd.Parameters.AddWithValue("G_OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
              dcmd.Parameters.AddWithValue("F_FEN_DIMEN_LENGTH", FenceBOobj.Fen_dimen_length);
              dcmd.Parameters.AddWithValue("F_FEN_DIMEN_HEIGHT", FenceBOobj.Fen_dimen_height);
              dcmd.Parameters.AddWithValue("F_DEPRECIATEDVALUE", FenceBOobj.Depreciatedvalue);
              dcmd.Parameters.AddWithValue("F_UPDATEDBY", FenceBOobj.CreatedBy);
              if (FenceBOobj.Photo != null)
                  dcmd.Parameters.AddWithValue("FENCEPHOTO_", SqlDbType.Image).Value = FenceBOobj.Photo;
              else
                  dcmd.Parameters.AddWithValue("FENCEPHOTO_", DBNull.Value);
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
      /// to insert details to database
      /// </summary>
      /// <param name="FenceBOobj"></param>
      /// <returns></returns>
      public int Insert(FenceBO FenceBOobj)
      {
          SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
          cnn.Open();
          SqlCommand dcmd = new SqlCommand("USP_TRN_INS_FENCE", cnn);
          dcmd.CommandType = CommandType.StoredProcedure;
          int count = Convert.ToInt32(dcmd.CommandType);

          try
          {
              dcmd.Parameters.AddWithValue("HHID", FenceBOobj.HouseholdID);
             // dcmd.Parameters.AddWithValue("FENCEID", FenceBOobj.Fenceid);
              if (FenceBOobj.Fenceid > 0)
                  dcmd.Parameters.AddWithValue("FENCEID", FenceBOobj.Fenceid);
              else
                  dcmd.Parameters.AddWithValue("FENCEID", DBNull.Value);
                
             
              dcmd.Parameters.AddWithValue("FEN_DIMEN_LENGTH", FenceBOobj.Fen_dimen_length);
              dcmd.Parameters.AddWithValue("FEN_DIMEN_HEIGHT", FenceBOobj.Fen_dimen_height);
              dcmd.Parameters.AddWithValue("DEPRECIATEDVALUE", FenceBOobj.Depreciatedvalue);
              dcmd.Parameters.AddWithValue("CREATEDBY", FenceBOobj.CreatedBy);
              if (FenceBOobj.Photo != null)
                  dcmd.Parameters.AddWithValue("FENCEPHOTO_", SqlDbType.Image).Value = FenceBOobj.Photo;
              else
                  dcmd.Parameters.AddWithValue("FENCEPHOTO_", DBNull.Value);
             
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
      /// To fetch details from database
      /// </summary>
      /// <param name="householdID"></param>
      /// <returns></returns>
      public FenceList GetFencedata(int householdID)
      {
          SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;

          string proc = "USP_TRN_SEL_FENCE";

          cmd = new SqlCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.AddWithValue("HHID_", householdID);
          //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

          cmd.Connection.Open();
          
          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

          FenceBO FenceBOobj = null;
          FenceList FenceListobj = new FenceList();
         
          while (dr.Read())
          {
              FenceBOobj = new FenceBO();
              FenceBOobj.Pap_fenceid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_FENCEID")));
              FenceBOobj.Fencedescription = dr.GetString(dr.GetOrdinal("FENCEDESCRIPTION"));
              FenceBOobj.Fen_dimen_length = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FEN_DIMEN_LENGTH")));
              FenceBOobj.Fen_dimen_height = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("FEN_DIMEN_HEIGHT")));
              FenceBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DEPRECIATEDVALUE")));
              FenceBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

              FenceListobj.Add(FenceBOobj);
          }

          dr.Close();

          return FenceListobj;
      }
      /// <summary>
      /// To delete data from database
      /// </summary>
      /// <param name="Pap_fenceid"></param>
      /// <returns></returns>
      public int Delete(int Pap_fenceid)
      {
          SqlConnection conn = new SqlConnection(AppConfiguration.ConnectionString);
          conn.Open();
          SqlCommand dCmd = new SqlCommand("USP_TRN_DEL_FENCE", conn);
          dCmd.CommandType = CommandType.StoredProcedure;
          try
          {
              dCmd.Parameters.AddWithValue("PAP_FENCEID_", Pap_fenceid);
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
      /// Getfencedatarow
      /// </summary>
      /// <param name="Pap_fenceid"></param>
      /// <returns></returns>
      public FenceBO Getfencedatarow(int Pap_fenceid)
      {
          SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
          SqlCommand cmd;

          string proc = "USP_TRN_GET_FENCE";

          cmd = new SqlCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("F_PAP_FENCEID", Pap_fenceid);
          // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

          cmd.Connection.Open();

          SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          FenceBO FenceBOobj = null;
          FenceList FenceListobj = new FenceList();

          FenceBOobj = new FenceBO();
          while (dr.Read())
          {

              if (!dr.IsDBNull(dr.GetOrdinal("pap_fenceid")))
                  FenceBOobj.Pap_fenceid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_fenceid")));

              if (!dr.IsDBNull(dr.GetOrdinal("fenceid")))
                  FenceBOobj.Fenceid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("fenceid")));

              if (!dr.IsDBNull(dr.GetOrdinal("fen_dimen_length")))
                  FenceBOobj.Fen_dimen_length = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fen_dimen_length")));

              if (!dr.IsDBNull(dr.GetOrdinal("fen_dimen_height")))
                  FenceBOobj.Fen_dimen_height = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fen_dimen_height")));

              if (!dr.IsDBNull(dr.GetOrdinal("depreciatedvalue")))
                  FenceBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("depreciatedvalue")));

          }
          dr.Close();


          return FenceBOobj;
      }

     
      /// <summary>
      /// to get Image File for DataBase
      /// </summary>
      /// <param name="householdID"></param>
      /// <param name="PermanentStructureID"></param>
      /// <returns></returns>
      public FenceBO ShowPAPGRAVE(int householdID, int PermanentStructureID)
      {
          SqlConnection myConnection;
          SqlCommand myCommand;
          myConnection = new SqlConnection(AppConfiguration.ConnectionString);
          myCommand = new SqlCommand("USP_TRN_GET_FENCE_PHOTO", myConnection);
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

              FenceBO objPAPGRAVE = new FenceBO();
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
