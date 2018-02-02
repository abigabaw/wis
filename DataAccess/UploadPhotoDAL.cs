using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class UploadPhotoDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        string proc = string.Empty;
        #endregion

        /// <summary>
        /// To Insert Upload Photo
        /// </summary>
        /// <param name="objUploadPhotoBO"></param>
        /// <returns></returns>
        public string InsertUploadPhoto(UploadPhotoBO objUploadPhotoBO)
        {
            string returnResult;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            if (objUploadPhotoBO.PhotoModule == "PAPPB")
            {

                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PERMSTRUCT", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);
                try
                {
                    dcmd.Parameters.AddWithValue("P_PERM_STRUCTUREID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("P_HHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("PAPPSPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("P_UPDATEDBY", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPNPB")
            {   
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_NPS", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("N_NONPERM_STRUCTUREID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("N_HHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("PAPPSPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("N_UPDATEDBY", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "DAMAGEDCROPS")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_DAMCROP", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("DAMAGECROPID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("DAMAGEHHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("DAMAGEPHOTO", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("DAMAGEUPDATEDBY", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPCROP")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PAPCROP", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("PAP_CROPID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("CROPPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("UPDATEDBY_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPGRAVE")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PAPGRAVE", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("PAP_GRAVEID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("GRAVEPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("UPDATEDBY_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPFENCE")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PAPFENCE", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("PAP_FENCEID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("FENCEPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("UPDATEDBY_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPCP")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PAPCULTURAL", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("CULTURALPROPID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("CULTURALPROPPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("UPDATEDBY_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            else if (objUploadPhotoBO.PhotoModule == "PAPOHFIX")
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPDPHOTOPAPOHRFIX", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("PAP_FIXTUREID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new SqlParameter("FIXTUREPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("UPDATEDBY_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            //PAPOHFIX
            else
            {
                SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PAP_PHOTO", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.AddWithValue("HHID_", objUploadPhotoBO.Hhid);
                    if (objUploadPhotoBO.Photo != null)
                        dcmd.Parameters.Add(new SqlParameter("PAPPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    else
                        dcmd.Parameters.AddWithValue("PAPPHOTO_", DBNull.Value);
                    //dcmd.Parameters.Add(new SqlParameter("PAPPHOTO_", SqlDbType.Image)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.AddWithValue("Updatedby_", objUploadPhotoBO.UserID);

                    returnResult = Convert.ToString(dcmd.ExecuteNonQuery());
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
            return returnResult;
        }
    }
}