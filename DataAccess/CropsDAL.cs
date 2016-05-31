using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class CropsDAL    
    {    
        /// <summary>
        /// TO save details to database
        /// </summary>
        /// <param name="Cropsobj"></param>
        /// <returns></returns>
        public int Insert(CropsBO Cropsobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_CROP", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", Cropsobj.HHID);
                dcmd.Parameters.Add("CROPID", Cropsobj.CROPID);
                dcmd.Parameters.Add("CROPTYPEID", Cropsobj.CROPTYPEID);
                dcmd.Parameters.Add("CROPDESCRIPTIONID", Cropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.Add("UNITOFMEASURE", DBNull.Value);             
                dcmd.Parameters.Add("QUANTITY", Cropsobj.QUANTITY);
                dcmd.Parameters.Add("CROPRATE", Cropsobj.CROPRATE);

                if (Cropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.Add("COMMENTS", Cropsobj.COMMENTS.Substring(0,1500));
                else
                    dcmd.Parameters.Add("COMMENTS", Cropsobj.COMMENTS);

                dcmd.Parameters.Add("ISDELETED", Cropsobj.ISDELETED);
                dcmd.Parameters.Add("CREATEDBY", Cropsobj.CREATEDBY);
                if (Cropsobj.Photo != null)
                    dcmd.Parameters.Add("CROPPHOTO_", OracleDbType.Blob).Value = Cropsobj.Photo;
                else
                    dcmd.Parameters.Add("CROPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);   

                return dcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CropsList GetCrops(int householdID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_CROPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropsBO Cropsobj = null;
            CropsList Listobj = new CropsList();

            while (dr.Read())
            {
                Cropsobj = new CropsBO();

                Cropsobj.PAP_CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_cropid")));

                if (!dr.IsDBNull(dr.GetOrdinal("cropname"))) Cropsobj.Cropname = dr.GetString(dr.GetOrdinal("cropname"));
                if (!dr.IsDBNull(dr.GetOrdinal("croptype"))) Cropsobj.Croptype = dr.GetString(dr.GetOrdinal("croptype"));
                if (!dr.IsDBNull(dr.GetOrdinal("cropdescription"))) Cropsobj.Cropdescription = dr.GetString(dr.GetOrdinal("cropdescription"));
                if (!dr.IsDBNull(dr.GetOrdinal("unitname"))) Cropsobj.UnitName = dr.GetString(dr.GetOrdinal("unitname"));
                if (!dr.IsDBNull(dr.GetOrdinal("quantity"))) Cropsobj.QUANTITY = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("quantity")));
                if (!dr.IsDBNull(dr.GetOrdinal("croprate"))) Cropsobj.CROPRATE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("croprate")));

                Listobj.Add(Cropsobj);
            }

            dr.Close();

            return Listobj;
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="CropId"></param>
        /// <returns></returns>
        public CropsBO GetData(int CropId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_CROPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("c_pap_cropid", CropId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CropsBO Cropsobj = null;
            CropsList Listobj = new CropsList();

            Cropsobj = new CropsBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("pap_cropid")))
                    Cropsobj.PAP_CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("pap_cropid")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPID")))
                    Cropsobj.CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPTYPEID")))
                    Cropsobj.CROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPTYPEID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPDESCRIPTIONID")))
                    Cropsobj.CROPDESCRIPTIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("UNITOFMEASURE")))
                    Cropsobj.UNITOFMEASURE = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("UNITOFMEASURE")));               

                if (!dr.IsDBNull(dr.GetOrdinal("QUANTITY")))
                    Cropsobj.QUANTITY = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("QUANTITY")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPRATE")))
                    Cropsobj.CROPRATE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CROPRATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS")))
                    Cropsobj.COMMENTS = dr.GetString(dr.GetOrdinal("COMMENTS"));

                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    Cropsobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();

            return Cropsobj;
        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="Cropsobj"></param>
        /// <returns></returns>
        public int Update(CropsBO Cropsobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_CROPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", Cropsobj.HHID);
                dcmd.Parameters.Add("c_PAP_CROPID", Cropsobj.PAP_CROPID);
                dcmd.Parameters.Add("c_CROPID", Cropsobj.CROPID);
                dcmd.Parameters.Add("c_CROPTYPEID", Cropsobj.CROPTYPEID);
                dcmd.Parameters.Add("c_CROPDESCRIPTIONID", Cropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.Add("c_UNITOFMEASURE", DBNull.Value);
                dcmd.Parameters.Add("c_QUANTITY", Cropsobj.QUANTITY);
                dcmd.Parameters.Add("c_CROPRATE", Cropsobj.CROPRATE);

                if (Cropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.Add("c_COMMENTS", Cropsobj.COMMENTS.Substring(0, 1500));
                else
                    dcmd.Parameters.Add("c_COMMENTS", Cropsobj.COMMENTS);
                
                dcmd.Parameters.Add("c_UPDATEDBY", Cropsobj.CREATEDBY);

                if (Cropsobj.Photo != null)
                    dcmd.Parameters.Add("CROPPHOTO_", OracleDbType.Blob).Value = Cropsobj.Photo;
                else
                    dcmd.Parameters.Add("CROPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);

                return dcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }
        /// <summary>
        /// Todelete data from database
        /// </summary>
        /// <param name="cropid"></param>
        /// <returns></returns>
        public int DeleteData(string cropid)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_DEL_CROPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("c_PAP_CROPID", cropid);
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }


        /// <summary>
        ///  to get Image File for DataBase
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public CropsBO ShowPAPCROPImage(int householdID, int PermanentStructureID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GET_PAPCROP_PHOTO", myConnection);
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

                CropsBO objDAMAGEDCROPSImage = new CropsBO();
                objDAMAGEDCROPSImage.Photo = papPhotoBytes;

                return objDAMAGEDCROPSImage;

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