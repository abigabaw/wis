using System;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_CROP", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID", Cropsobj.HHID);
                dcmd.Parameters.AddWithValue("CROPID", Cropsobj.CROPID);
                dcmd.Parameters.AddWithValue("CROPTYPEID", Cropsobj.CROPTYPEID);
                dcmd.Parameters.AddWithValue("CROPDESCRIPTIONID", Cropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.AddWithValue("UNITOFMEASURE", DBNull.Value);             
                dcmd.Parameters.AddWithValue("QUANTITY", Cropsobj.QUANTITY);
                dcmd.Parameters.AddWithValue("CROPRATE", Cropsobj.CROPRATE);

                if (Cropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.AddWithValue("COMMENTS", Cropsobj.COMMENTS.Substring(0,1500));
                else
                    dcmd.Parameters.AddWithValue("COMMENTS", Cropsobj.COMMENTS);

                dcmd.Parameters.AddWithValue("ISDELETED", Cropsobj.ISDELETED);
                dcmd.Parameters.AddWithValue("CREATEDBY", Cropsobj.CREATEDBY);
                if (Cropsobj.Photo != null)
                    dcmd.Parameters.AddWithValue("CROPPHOTO_", SqlDbType.Image).Value = Cropsobj.Photo;
                else
                    dcmd.Parameters.AddWithValue("CROPPHOTO_", DBNull.Value);   

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("c_pap_cropid", CropId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_CROPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", Cropsobj.HHID);
                dcmd.Parameters.AddWithValue("c_PAP_CROPID", Cropsobj.PAP_CROPID);
                dcmd.Parameters.AddWithValue("c_CROPID", Cropsobj.CROPID);
                dcmd.Parameters.AddWithValue("c_CROPTYPEID", Cropsobj.CROPTYPEID);
                dcmd.Parameters.AddWithValue("c_CROPDESCRIPTIONID", Cropsobj.CROPDESCRIPTIONID);
                dcmd.Parameters.AddWithValue("c_UNITOFMEASURE", DBNull.Value);
                dcmd.Parameters.AddWithValue("c_QUANTITY", Cropsobj.QUANTITY);
                dcmd.Parameters.AddWithValue("c_CROPRATE", Cropsobj.CROPRATE);

                if (Cropsobj.COMMENTS.Length > 1500)
                    dcmd.Parameters.AddWithValue("c_COMMENTS", Cropsobj.COMMENTS.Substring(0, 1500));
                else
                    dcmd.Parameters.AddWithValue("c_COMMENTS", Cropsobj.COMMENTS);
                
                dcmd.Parameters.AddWithValue("c_UPDATEDBY", Cropsobj.CREATEDBY);

                if (Cropsobj.Photo != null)
                    dcmd.Parameters.AddWithValue("CROPPHOTO_", SqlDbType.Image).Value = Cropsobj.Photo;
                else
                    dcmd.Parameters.AddWithValue("CROPPHOTO_", DBNull.Value);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_DEL_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("c_PAP_CROPID", cropid);
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
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_GET_PAPCROP_PHOTO", myConnection);
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