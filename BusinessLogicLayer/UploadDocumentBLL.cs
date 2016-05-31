using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class UploadDocumentBLL
    {
        #region Declaration Scetion
        UploadDocumentDAL objUploadDocumentDAL;
        #endregion

        /// <summary>
        /// To Get Upload Document
        /// </summary>
        /// <returns></returns>
        public UploadDocumentList GetUploadDocument()
        {
            objUploadDocumentDAL = new UploadDocumentDAL();
            return objUploadDocumentDAL.GetUploadDocument();
        }

        /// <summary>
        /// To Insert Upload Document
        /// </summary>
        /// <param name="objUploadDocument"></param>
        /// <returns></returns>
        public string InsertUploadDocument(UploadDocumentBO objUploadDocument)
        {
            UploadDocumentDAL UploadDocumentDAL = new UploadDocumentDAL(); //Data pass -to Database Layer

            try
            {
                return UploadDocumentDAL.InsertUploadDocument(objUploadDocument);
            }
            catch
            {
                throw;
            }
            finally
            {
                UploadDocumentDAL = null;
            }
        }

        //public UploadDocumentList GetALLUploadDocument(int HHID)
        //{
        //    UploadDocumentDAL UploadDocumentDALObj = new UploadDocumentDAL();
        //    return UploadDocumentDALObj.GetALLUploadDocument(HHID);
        //}

        /// <summary>
        /// To Get ALL Upload Document
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="DocumentCode"></param>
        /// <param name="ProjectCode"></param>
        /// <param name="DOCSERVICEID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UploadDocumentList GetALLUploadDocument(int HHID, string DocumentCode, string ProjectCode, int DOCSERVICEID, int userID)
        {
            UploadDocumentDAL UploadDocumentDALObj = new UploadDocumentDAL();
            return UploadDocumentDALObj.GetALLUploadDocument(HHID, DocumentCode, ProjectCode, DOCSERVICEID, userID);
        }

        /// <summary>
        /// To Delete Document
        /// </summary>
        /// <param name="PAPDOCUMENTID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string DeleteDocument(int PAPDOCUMENTID, int userID)
        {
            UploadDocumentDAL UploadDocumentDALObj = new UploadDocumentDAL();
            return UploadDocumentDALObj.DeleteDocument(PAPDOCUMENTID, userID);
        }

        /// <summary>
        /// To Get File Path
        /// </summary>
        /// <param name="papDocumentID"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        public UploadDocumentBO getFilePath(string papDocumentID, string ProjectCode)
        {
            UploadDocumentDAL UploadDocumentDALObj = new UploadDocumentDAL();
            return UploadDocumentDALObj.getFilePath(papDocumentID, ProjectCode);
        }

        /// <summary>
        /// To Get Search Document 
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="HHID"></param>
        /// <param name="DocumentCode"></param>
        /// <param name="ProjectCode"></param>
        /// <param name="DOCSERVICEID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UploadDocumentList GetSearchDocument(string KeyWord, int HHID, string DocumentCode, string ProjectCode, int DOCSERVICEID, int userID)
        {
            UploadDocumentDAL UploadDocumentDALObj = new UploadDocumentDAL();
            return UploadDocumentDALObj.GetSearchDocument(KeyWord, HHID, DocumentCode, ProjectCode, DOCSERVICEID, userID);
        }
    }
}