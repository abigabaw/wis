using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            if (objUploadPhotoBO.PhotoModule == "PAPPB")
            {

                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PERMSTRUCT", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);
                try
                {
                    dcmd.Parameters.Add("P_PERM_STRUCTUREID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("P_HHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("PAPPSPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("P_UPDATEDBY", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_NPS", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("N_NONPERM_STRUCTUREID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("N_HHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("PAPPSPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("N_UPDATEDBY", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_DAMCROP", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("DAMAGECROPID", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("DAMAGEHHID", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("DAMAGEPHOTO", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("DAMAGEUPDATEDBY", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PAPCROP", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("PAP_CROPID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("CROPPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("UPDATEDBY_", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PAPGRAVE", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("PAP_GRAVEID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("GRAVEPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("UPDATEDBY_", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PAPFENCE", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("PAP_FENCEID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("FENCEPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("UPDATEDBY_", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PAPCULTURAL", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("CULTURALPROPID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("CULTURALPROPPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("UPDATEDBY_", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPDPHOTOPAPOHRFIX", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("PAP_FIXTUREID_", objUploadPhotoBO.PKID);
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    dcmd.Parameters.Add(new OracleParameter("FIXTUREPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("UPDATEDBY_", objUploadPhotoBO.UserID);

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
                OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PAP_PHOTO", cnn);
                dcmd.CommandType = CommandType.StoredProcedure;
                int count = Convert.ToInt32(dcmd.CommandType);

                try
                {
                    dcmd.Parameters.Add("HHID_", objUploadPhotoBO.Hhid);
                    if (objUploadPhotoBO.Photo != null)
                        dcmd.Parameters.Add(new OracleParameter("PAPPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    else
                        dcmd.Parameters.Add("PAPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
                    //dcmd.Parameters.Add(new OracleParameter("PAPPHOTO_", OracleDbType.Blob)).Value = objUploadPhotoBO.Photo;
                    dcmd.Parameters.Add("Updatedby_", objUploadPhotoBO.UserID);

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