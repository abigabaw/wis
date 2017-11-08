using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class DamagedCropsDAL
    {
        /// <summary>
        /// to fetch details from database
        /// </summary>
        /// <returns></returns>
        public DamagedCropsList GetDamagedBy()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_DAMAGE_DCROP";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DamagedCropsBO BOobj = null;
            DamagedCropsList Listobj = new DamagedCropsList();

            while (dr.Read())
            {
                BOobj = new DamagedCropsBO();
                BOobj.CROPDAMAGEDBYID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDAMAGEDBYID"))));
                BOobj.CROPDAMAGEDBYOTHER = dr.GetValue(dr.GetOrdinal("CROPDAMAGEDBY")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;
        }
        /// <summary>
        /// to save data to database
        /// </summary>
        /// <param name="DamagedCropsobj"></param>
        /// <returns></returns>
        public int Insert(DamagedCropsBO DamagedCropsobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand(" USP_TRN_INS_DAMAGE_CROPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID", DamagedCropsobj.HHID);
                dcmd.Parameters.AddWithValue("DMGCRPFORMREFNO", DamagedCropsobj.DMGCRPFORMREFNO);
                dcmd.Parameters.AddWithValue("CROPID", DamagedCropsobj.CROPID);
                dcmd.Parameters.AddWithValue("CROPTYPEID", DamagedCropsobj.CROPTYPEID);
                dcmd.Parameters.AddWithValue("CROPDESCRIPTIONID", DamagedCropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.AddWithValue("DATEDAMAGED", DamagedCropsobj.DATEDAMAGED);

                dcmd.Parameters.AddWithValue("CROPDAMAGEDBYID", DamagedCropsobj.CROPDAMAGEDBYID);

                dcmd.Parameters.AddWithValue("CROPDAMAGEDBYOTHER", DamagedCropsobj.CROPDAMAGEDBYOTHER);

                dcmd.Parameters.AddWithValue("QUANTITY", DamagedCropsobj.QUANTITY);
                dcmd.Parameters.AddWithValue("CROPRATE", DamagedCropsobj.CROPRATE);
                dcmd.Parameters.AddWithValue("AMOUNTPAID", DamagedCropsobj.AMOUNTPAID);

                if (DamagedCropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.AddWithValue("COMMENTS", DamagedCropsobj.COMMENTS.Substring(0, 1500));
                else
                    dcmd.Parameters.AddWithValue("COMMENTS", DamagedCropsobj.COMMENTS);

                dcmd.Parameters.AddWithValue("ISDELETED", DamagedCropsobj.ISDELETED);
                dcmd.Parameters.AddWithValue("CREATEDBY", DamagedCropsobj.CREATEDBY);
                if (DamagedCropsobj.Photo != null)
                    dcmd.Parameters.AddWithValue("DAMGEDCROPPHOTO_", SqlDbType.Image).Value = DamagedCropsobj.Photo;
                else
                    dcmd.Parameters.AddWithValue("DAMGEDCROPPHOTO_", DBNull.Value);

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
        /// to fetch details from database
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public DamagedCropsList GetDamagedCrops(string hhid)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_DAMAG_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("D_HHID", hhid);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DamagedCropsBO DamagedCropsobj = null;
            DamagedCropsList Listobj = new DamagedCropsList();

            while (dr.Read())
            {
                DamagedCropsobj = new DamagedCropsBO();

                DamagedCropsobj.DAMAGED_CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("damaged_cropid")));
                if (!dr.IsDBNull(dr.GetOrdinal("dmgcrpformrefno")))
                {
                    DamagedCropsobj.DMGCRPFORMREFNO = dr.GetString(dr.GetOrdinal("dmgcrpformrefno"));
                }
                else
                {
                    DamagedCropsobj.DMGCRPFORMREFNO = "";
                }
                if (!dr.IsDBNull(dr.GetOrdinal("cropname")))
                {
                    DamagedCropsobj.Cropname = dr.GetString(dr.GetOrdinal("cropname"));
                }
                else
                {
                    DamagedCropsobj.Cropname = "";
                }
                if (!dr.IsDBNull(dr.GetOrdinal("croptype")))
                {
                    DamagedCropsobj.Croptype = dr.GetString(dr.GetOrdinal("croptype"));
                }
                else
                {
                    DamagedCropsobj.Croptype = "";
                }
                if (!dr.IsDBNull(dr.GetOrdinal("cropdescription")))
                {
                    DamagedCropsobj.Description = dr.GetString(dr.GetOrdinal("cropdescription"));
                }
                else
                {
                    DamagedCropsobj.Description = "";
                }
                if (!dr.IsDBNull(dr.GetOrdinal("datedamaged")))
                {
                    DamagedCropsobj.DATEDAMAGED = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("datedamaged")));
                }
                else
                {
                    DamagedCropsobj.DATEDAMAGED = Convert.ToDateTime("");
                }
                if (!dr.IsDBNull(dr.GetOrdinal("quantity")))
                {
                    DamagedCropsobj.QUANTITY = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("quantity")));
                }
                else
                {
                    DamagedCropsobj.QUANTITY = 0;
                }
                if (!dr.IsDBNull(dr.GetOrdinal("croprate")))
                {
                    DamagedCropsobj.CROPRATE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("croprate")));
                }
                else
                {
                    DamagedCropsobj.CROPRATE = 0;
                }
                if (!dr.IsDBNull(dr.GetOrdinal("amountpaid")))
                {
                    DamagedCropsobj.AMOUNTPAID = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("amountpaid")));
                }
                else
                {
                    DamagedCropsobj.AMOUNTPAID = 0;
                }
                // PermanentStructureobj.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                Listobj.Add(DamagedCropsobj);
            }

            dr.Close();

            return Listobj;
        }
        /// <summary>
        /// to fetch details from database
        /// </summary>
        /// <param name="damageCropId"></param>
        /// <returns></returns>
        public DamagedCropsBO GetData(int damageCropId)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_DAMAGE_CROPS";//"USP_TRN_GET_DAMAGE_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("D_DAMAGED_CROPID", damageCropId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DamagedCropsBO DamagedCropsobj = null;
            DamagedCropsList Listobj = new DamagedCropsList();

            DamagedCropsobj = new DamagedCropsBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("DAMAGED_CROPID")))
                    DamagedCropsobj.DAMAGED_CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DAMAGED_CROPID")));

                if (!dr.IsDBNull(dr.GetOrdinal("DMGCRPFORMREFNO")))
                    DamagedCropsobj.DMGCRPFORMREFNO = dr.GetString(dr.GetOrdinal("DMGCRPFORMREFNO"));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPID")))
                    DamagedCropsobj.CROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPTYPEID")))
                    DamagedCropsobj.CROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPTYPEID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPDESCRIPTIONID")))
                    DamagedCropsobj.CROPDESCRIPTIONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDESCRIPTIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("DATEDAMAGED")))
                    DamagedCropsobj.DATEDAMAGED = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("DATEDAMAGED")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPDAMAGEDBYID")))
                    DamagedCropsobj.CROPDAMAGEDBYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CROPDAMAGEDBYID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPDAMAGEDBYOTHER")))
                    DamagedCropsobj.CROPDAMAGEDBYOTHER = dr.GetString(dr.GetOrdinal("CROPDAMAGEDBYOTHER"));

                if (!dr.IsDBNull(dr.GetOrdinal("QUANTITY")))
                    DamagedCropsobj.QUANTITY = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("QUANTITY")));

                if (!dr.IsDBNull(dr.GetOrdinal("CROPRATE")))
                    DamagedCropsobj.CROPRATE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CROPRATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("AMOUNTPAID")))
                    DamagedCropsobj.AMOUNTPAID = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("AMOUNTPAID")));

                if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS")))
                    DamagedCropsobj.COMMENTS = dr.GetString(dr.GetOrdinal("COMMENTS"));


                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    DamagedCropsobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();


            return DamagedCropsobj;
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="DamagedCropsobj"></param>
        /// <returns></returns>
        public int Update(DamagedCropsBO DamagedCropsobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_DAMAGE_CROPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", DamagedCropsobj.HHID);

                dcmd.Parameters.AddWithValue("D_DAMAGED_CROPID", DamagedCropsobj.DAMAGED_CROPID);
                dcmd.Parameters.AddWithValue("D_DMGCRPFORMREFNO", DamagedCropsobj.DMGCRPFORMREFNO);
                dcmd.Parameters.AddWithValue("D_CROPID", DamagedCropsobj.CROPID);
                dcmd.Parameters.AddWithValue("D_CROPTYPEID", DamagedCropsobj.CROPTYPEID);
                dcmd.Parameters.AddWithValue("D_CROPDESCRIPTIONID", DamagedCropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.AddWithValue("D_DATEDAMAGED", DamagedCropsobj.DATEDAMAGED);
                dcmd.Parameters.AddWithValue("D_CROPDAMAGEDBYID", DamagedCropsobj.CROPDAMAGEDBYID);
                dcmd.Parameters.AddWithValue("D_CROPDAMAGEDBYOTHER", DamagedCropsobj.CROPDAMAGEDBYOTHER);
                dcmd.Parameters.AddWithValue("D_QUANTITY", DamagedCropsobj.QUANTITY);
                dcmd.Parameters.AddWithValue("D_CROPRATE", DamagedCropsobj.CROPRATE);
                dcmd.Parameters.AddWithValue("D_AMOUNTPAID", DamagedCropsobj.AMOUNTPAID);

                if (DamagedCropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.AddWithValue("D_COMMENTS", DamagedCropsobj.COMMENTS.Substring(0, 1500));
                else
                    dcmd.Parameters.AddWithValue("D_COMMENTS", DamagedCropsobj.COMMENTS);

                dcmd.Parameters.AddWithValue("D_UPDATEDBY", DamagedCropsobj.CREATEDBY);
                if (DamagedCropsobj.Photo != null)
                    dcmd.Parameters.AddWithValue("DAMGEDCROPPHOTO_", SqlDbType.Image).Value = DamagedCropsobj.Photo;
                else
                    dcmd.Parameters.AddWithValue("DAMGEDCROPPHOTO_", DBNull.Value);

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
        /// to delete data from database
        /// </summary>
        /// <param name="damageCropId"></param>
        /// <returns></returns>
        public int DeleteData(int damageCropId)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_DEL_DAMGE_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("DAMAGED_CROPID_", damageCropId);
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }

       
        /// <summary>
        /// to get Image File for DataBase
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public DamagedCropsBO ShowDAMAGEDCROPSImage(int householdID, int PermanentStructureID)
        {
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_GET_DAMAGED_CROP_PHOTO", myConnection);
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

                DamagedCropsBO objDAMAGEDCROPSImage = new DamagedCropsBO();
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