using System;
using System.Data;
using Oracle.DataAccess.Client;
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
                OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;
                string proc = "USP_TRN_GET_FENCEDATA";
                cmd = new OracleCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
          cnn.Open();
          OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_FENCE", cnn);
          dcmd.CommandType = CommandType.StoredProcedure;
          int count = Convert.ToInt32(dcmd.CommandType);

          try
          {
              dcmd.Parameters.Add("HHID", FenceBOobj.HouseholdID);
              dcmd.Parameters.Add("F_PAP_FENCEID", FenceBOobj.Pap_fenceid);
              dcmd.Parameters.Add("F_FENCEID", FenceBOobj.Fenceid);
              //dcmd.Parameters.Add("G_OTHERGRAVEFINISH", GraveBOobj.Othergravefinish);
              dcmd.Parameters.Add("F_FEN_DIMEN_LENGTH", FenceBOobj.Fen_dimen_length);
              dcmd.Parameters.Add("F_FEN_DIMEN_HEIGHT", FenceBOobj.Fen_dimen_height);
              dcmd.Parameters.Add("F_DEPRECIATEDVALUE", FenceBOobj.Depreciatedvalue);
              dcmd.Parameters.Add("F_UPDATEDBY", FenceBOobj.CreatedBy);
              if (FenceBOobj.Photo != null)
                  dcmd.Parameters.Add("FENCEPHOTO_", OracleDbType.Blob).Value = FenceBOobj.Photo;
              else
                  dcmd.Parameters.Add("FENCEPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
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
          OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
          cnn.Open();
          OracleCommand dcmd = new OracleCommand("USP_TRN_INS_FENCE", cnn);
          dcmd.CommandType = CommandType.StoredProcedure;
          int count = Convert.ToInt32(dcmd.CommandType);

          try
          {
              dcmd.Parameters.Add("HHID", FenceBOobj.HouseholdID);
             // dcmd.Parameters.Add("FENCEID", FenceBOobj.Fenceid);
              if (FenceBOobj.Fenceid > 0)
                  dcmd.Parameters.Add("FENCEID", FenceBOobj.Fenceid);
              else
                  dcmd.Parameters.Add("FENCEID", DBNull.Value);
                
             
              dcmd.Parameters.Add("FEN_DIMEN_LENGTH", FenceBOobj.Fen_dimen_length);
              dcmd.Parameters.Add("FEN_DIMEN_HEIGHT", FenceBOobj.Fen_dimen_height);
              dcmd.Parameters.Add("DEPRECIATEDVALUE", FenceBOobj.Depreciatedvalue);
              dcmd.Parameters.Add("CREATEDBY", FenceBOobj.CreatedBy);
              if (FenceBOobj.Photo != null)
                  dcmd.Parameters.Add("FENCEPHOTO_", OracleDbType.Blob).Value = FenceBOobj.Photo;
              else
                  dcmd.Parameters.Add("FENCEPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
             
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
          OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;

          string proc = "USP_TRN_SEL_FENCE";

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("HHID_", householdID);
          cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

          cmd.Connection.Open();
          
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
          OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
          conn.Open();
          OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_FENCE", conn);
          dCmd.CommandType = CommandType.StoredProcedure;
          try
          {
              dCmd.Parameters.Add("PAP_FENCEID_", Pap_fenceid);
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
          OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
          OracleCommand cmd;

          string proc = "USP_TRN_GET_FENCE";

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("F_PAP_FENCEID", Pap_fenceid);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

          cmd.Connection.Open();

          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
          OracleConnection myConnection;
          OracleCommand myCommand;
          myConnection = new OracleConnection(AppConfiguration.ConnectionString);
          myCommand = new OracleCommand("USP_TRN_GET_FENCE_PHOTO", myConnection);
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
