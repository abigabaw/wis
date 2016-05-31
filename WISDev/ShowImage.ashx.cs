
using System;
using System.Web;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int householdID;
            int PermanentStructureID = 0;
            string photoModule = "";

            if (context.Request.QueryString["id"] != null)
            {
                householdID = Convert.ToInt32(context.Request.QueryString["id"]);
            }
            else
            {
                throw new ArgumentException("No parameter specified");
            }
            if (context.Request.QueryString["photoModule"] != null)
                photoModule = context.Request.QueryString["photoModule"];
            else
                throw new ArgumentException("No parameter specified");

            if (context.Request.QueryString["perStuID"] != null)
                PermanentStructureID = Convert.ToInt32(context.Request.QueryString["perStuID"]);
            else
                throw new ArgumentException("No parameter specified");


            context.Response.ContentType = "image/jpeg";
            System.IO.MemoryStream strm = null;

            switch (photoModule)
            {
                case "PAP":
                    strm = ShowPAPImage(householdID);
                    break;
                case "PAPINST":
                    strm = ShowPAPImage(householdID);
                    break;

                case "PAPGROUP":
                    strm = ShowPAPImage(householdID);
                    break;

                case "PAPPB":
                    strm = ShowPAPPBImage(householdID, PermanentStructureID);
                    break;

                case "PAPNPB":
                    strm = ShowPAPNPBImage(householdID, PermanentStructureID);
                    break;

                case "DAMAGEDCROPS":
                    strm = ShowDAMAGEDCROPSImage(householdID, PermanentStructureID);
                    break;

                case "PAPCROP":
                    strm = ShowPAPCROPImage(householdID, PermanentStructureID);
                    break;

                case "PAPGRAVE":
                    strm = ShowPAPGRAVE(householdID, PermanentStructureID);
                    break;
                case "PAPFENCE":
                    strm = ShowPAPFENCE(householdID, PermanentStructureID);
                    break;

                case "PAPCP":
                    strm = ShowPAPCPImage(householdID, PermanentStructureID);
                    break;

                case "PAPOHFIX":
                    strm = ShowPAPOHFIXImage(householdID, PermanentStructureID);
                    break;
            }

            if (strm != null)
            {
                byte[] buffer = new byte[strm.Length];
                int byteSeq = strm.Read(buffer, 0, (int)strm.Length);

                while (byteSeq > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, byteSeq);
                    byteSeq = strm.Read(buffer, 0, (int)strm.Length);
                }
            }
        }

        public System.IO.MemoryStream ShowPAPImage(int householdID)
        {
            try
            {
                PAP_HouseholdBO objPAP = (new PAP_HouseholdBLL()).GetPAPPhoto(householdID);

                if (objPAP != null)
                    return new System.IO.MemoryStream(objPAP.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowPAPPBImage(int householdID, int PermanentStructureID)
        {
            try
            {
                PermanentStructureBO objPAPPS = (new PermanentStructureBLL()).GetPAPPSPhoto(householdID, PermanentStructureID);
                if (objPAPPS.Photo != null)
                    return new System.IO.MemoryStream(objPAPPS.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        public System.IO.MemoryStream ShowPAPNPBImage(int householdID, int PermanentStructureID)
        {
            try
            {
                NonPermanentStructureBO objPAPNPS = (new Non_perm_structureBLL()).ShowPAPNPBImage(householdID, PermanentStructureID);
                if (objPAPNPS.Photo != null)
                    return new System.IO.MemoryStream(objPAPNPS.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowDAMAGEDCROPSImage(int householdID, int PermanentStructureID)
        {
            try
            {
                DamagedCropsBO objDAMAGEDCROPSImage = (new DamagedCropsBLL()).ShowDAMAGEDCROPSImage(householdID, PermanentStructureID);
                if (objDAMAGEDCROPSImage.Photo != null)
                    return new System.IO.MemoryStream(objDAMAGEDCROPSImage.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowPAPCROPImage(int householdID, int PermanentStructureID)
        {
            try
            {
                CropsBO objDAMAGEDCROPSImage = (new CropsBLL()).ShowPAPCROPImage(householdID, PermanentStructureID);
                if (objDAMAGEDCROPSImage.Photo != null)
                    return new System.IO.MemoryStream(objDAMAGEDCROPSImage.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowPAPGRAVE(int householdID, int PermanentStructureID)
        {
            try
            {
                GraveBO objPAPGRAVE = (new GraveBLL()).ShowPAPGRAVE(householdID, PermanentStructureID);
                if (objPAPGRAVE.Photo != null)
                    return new System.IO.MemoryStream(objPAPGRAVE.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
               // throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowPAPFENCE(int householdID, int PermanentStructureID)
        {
            try
            {
                FenceBO objPAPFENCE = (new FenceBLL()).ShowPAPGRAVE(householdID, PermanentStructureID);
                if (objPAPFENCE.Photo != null)
                    return new System.IO.MemoryStream(objPAPFENCE.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public System.IO.MemoryStream ShowPAPCPImage(int householdID, int PermanentStructureID)
        {
            try
            {
                CulturPropertiesBO objPAPCP = (new CulturePropertiesBLL()).ShowPAPCPImage(householdID, PermanentStructureID);
                if (objPAPCP.Photo != null)
                    return new System.IO.MemoryStream(objPAPCP.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //other Fixtures tab OtherFenceBO or OtherFixturesBLL
        public System.IO.MemoryStream ShowPAPOHFIXImage(int householdID, int PermanentStructureID)
        {
            try
            {
                OtherFenceBO objPAPFIX = (new OtherFixturesBLL()).ShowPAPOHFIXImage(householdID, PermanentStructureID);
                if (objPAPFIX.Photo != null)
                  return new System.IO.MemoryStream(objPAPFIX.Photo);
                else
                    return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}