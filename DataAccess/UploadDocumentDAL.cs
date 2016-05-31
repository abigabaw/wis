using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Configuration;

//Edwin: 27MAY2016
using System.IO;

namespace WIS_DataAccess
{
    public class UploadDocumentDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        /// <summary>
        /// To Get Upload Document
        /// </summary>
        /// <returns></returns>
        public UploadDocumentList GetUploadDocument()
        {
            proc = "USP_MST_SEL_DOC_TYPE";
            cnn = new OracleConnection(con);
            UploadDocumentBO objUploadDocument = null;

            UploadDocumentList UploadDocumentList = new UploadDocumentList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUploadDocument = new UploadDocumentBO();

                    if (ColumnExists(dr, "DOCUMENTTYPEID") && !dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPEID")))
                        objUploadDocument.DocumentTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOCUMENTTYPEID")));
                    if (ColumnExists(dr, "DOCUMENTTYPE") && !dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPE")))
                        objUploadDocument.DocumentType = dr.GetString(dr.GetOrdinal("DOCUMENTTYPE"));
                    if (ColumnExists(dr, "DOCUMENTCODE") && !dr.IsDBNull(dr.GetOrdinal("DOCUMENTCODE")))
                        objUploadDocument.DocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTCODE"));

                    UploadDocumentList.Add(objUploadDocument);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return UploadDocumentList;
        }

        // To check that the Column Exists or not
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Insert Upload Document
        /// </summary>
        /// <param name="objUploadDocument"></param>
        /// <returns></returns>
        public string InsertUploadDocument(UploadDocumentBO objUploadDocument)
        {
            string returnResult;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_PAP_DOCUMENT_TYPE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", objUploadDocument.HHID);
                dcmd.Parameters.Add("DocumentType_", objUploadDocument.DocumentTypeID);
                dcmd.Parameters.Add("DocumentPath_", objUploadDocument.DocumentPath);
                dcmd.Parameters.Add("UserID_", objUploadDocument.UserID);
                if (objUploadDocument.DOCSERVICEID > -1) // add By Reddy
                {
                    dcmd.Parameters.Add("DOCSERVICEIDIN", objUploadDocument.DOCSERVICEID);
                }
                else
                {
                    dcmd.Parameters.Add("DOCSERVICEIDIN", 0);
                }
                if (objUploadDocument.KeyWord != null)
                {
                    dcmd.Parameters.Add("KeyWord_", objUploadDocument.KeyWord);
                }
                else
                {
                    dcmd.Parameters.Add("KeyWord_", Oracle.DataAccess.Types.OracleString.Null);
                }
                if (objUploadDocument.Description != null)
                {
                    dcmd.Parameters.Add("Description_", objUploadDocument.Description);
                }
                else
                {
                    dcmd.Parameters.Add("Description_", Oracle.DataAccess.Types.OracleString.Null);
                }
                if (objUploadDocument.ProjectID > 0)
                    dcmd.Parameters.Add("PROJECTID_", objUploadDocument.ProjectID);

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

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
            return returnResult;
        }

        /// <summary>
        /// To Get ALL Upload Document
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="DocumentCode"></param>
        /// <param name="Projectid"></param>
        /// <param name="DOCSERVICEID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UploadDocumentList GetALLUploadDocument(int HHID, string DocumentCode, string Projectid, int DOCSERVICEID, int userID)
        {
            UploadDocumentList UploadDocumentList = new UploadDocumentList();
            string projectCode = "";
            if (DocumentCode == "All")
            {
                OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;

                string proc = "USP_TRN_SEL_ALLUPLOADDOC";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PROJECTID_", Convert.ToInt32(Projectid));
                cmd.Parameters.Add("HHID_", HHID);
                cmd.Parameters.Add("userID_", userID);
                cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UploadDocumentBO objUploadDocument = null;

                while (dr.Read())
                {
                    string DocumentPath_ = string.Empty;
                    string getDate = string.Empty;
                    objUploadDocument = new UploadDocumentBO(); //PAPDOCUMENTID
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPDOCUMENTID"))) objUploadDocument.PAPDOCUMENTID = dr.GetInt32(dr.GetOrdinal("PAPDOCUMENTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objUploadDocument.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPEID"))) objUploadDocument.DocumentTypeID = dr.GetInt32(dr.GetOrdinal("DOCUMENTTYPEID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPE"))) objUploadDocument.DocumentType = dr.GetString(dr.GetOrdinal("DOCUMENTTYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) objUploadDocument.DocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH")))  DocumentPath_ = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objUploadDocument.Projectcode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                    if (objUploadDocument.HHID <= 0)
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;

                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }
                    }
                    else
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;

                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objUploadDocument.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("UPLOADEDDATE"))) objUploadDocument.Date = dr.GetDateTime(dr.GetOrdinal("UPLOADEDDATE")).ToString(UtilBO.DateFormat);
                    if (!dr.IsDBNull(dr.GetOrdinal("KEYWORD"))) objUploadDocument.KeyWord = dr.GetString(dr.GetOrdinal("KEYWORD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objUploadDocument.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    UploadDocumentList.Add(objUploadDocument);
                }

                dr.Close();
            }
            else
            {
                OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;

                string proc = "USP_TRN_SEL_UPLOADDOCBYCODE";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PROJECTID_", Convert.ToInt32(Projectid));
                cmd.Parameters.Add("HHID_", HHID);
                cmd.Parameters.Add("DocumentCode_", DocumentCode);
                cmd.Parameters.Add("DOCSERVICEID_", DOCSERVICEID);
                cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UploadDocumentBO objUploadDocument = null;

                while (dr.Read())
                {
                    string DocumentPath_ = string.Empty;
                    objUploadDocument = new UploadDocumentBO(); //PAPDOCUMENTID
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPDOCUMENTID"))) objUploadDocument.PAPDOCUMENTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDOCUMENTID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objUploadDocument.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPEID"))) objUploadDocument.DocumentTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOCUMENTTYPEID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPE"))) objUploadDocument.DocumentType = dr.GetString(dr.GetOrdinal("DOCUMENTTYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) objUploadDocument.DocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) DocumentPath_ = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objUploadDocument.Projectcode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                    if (objUploadDocument.HHID <= 0)
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }


                    }
                    else
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objUploadDocument.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UPLOADEDDATE"))) objUploadDocument.Date = dr.GetDateTime(dr.GetOrdinal("UPLOADEDDATE")).ToString(UtilBO.DateFormat); ;
                    if (!dr.IsDBNull(dr.GetOrdinal("KEYWORD"))) objUploadDocument.KeyWord = dr.GetString(dr.GetOrdinal("KEYWORD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objUploadDocument.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    UploadDocumentList.Add(objUploadDocument);
                }

                dr.Close();
            }
            return UploadDocumentList;
        }

        /// <summary>
        /// To Delete Document
        /// </summary>
        /// <param name="PAPDOCUMENTID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string DeleteDocument(int PAPDOCUMENTID, int userID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd = null;
            string message = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEPAPDOCUMENT";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PAPDOCUMENTID_", PAPDOCUMENTID);
                cmd.Parameters.Add("userID_", userID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    message = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    message = "Selected Document Not Found delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return message;
        }

        /// <summary>
        /// To Get File Path
        /// </summary>
        /// <param name="papDocumentID"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        public UploadDocumentBO getFilePath(string papDocumentID, string ProjectCode)
        {
            UploadDocumentList UploadDocumentList = new UploadDocumentList();
            string projectCode = ProjectCode;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_DOCUMENTPATH";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("ProjectID_", pID);
            cmd.Parameters.Add("papDocumentID_", papDocumentID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            UploadDocumentBO objUploadDocument = null;

            while (dr.Read())
            {
                objUploadDocument = new UploadDocumentBO(); //PAPDOCUMENTID

                string DocumnetPath_ = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));

                string projectName = projectCode;
                string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + projectName + "\\" + DocumnetPath_;// +DocPath;
                objUploadDocument.DocumentPath = main_strPath_user;

            }
            return objUploadDocument;
            
        }

        /// <summary>
        /// To Get Search Document 
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="HHID"></param>
        /// <param name="DocumentCode"></param>
        /// <param name="ProjectID"></param>
        /// <param name="DOCSERVICEID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UploadDocumentList GetSearchDocument(string KeyWord, int HHID, string DocumentCode, string ProjectID, int DOCSERVICEID, int userID)
        {
            UploadDocumentList UploadDocumentList = new UploadDocumentList();
            string projectCode = "";
            if (DocumentCode == "All")
            {
                OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;

                string proc = "USP_TRN_SEL_SEARCHDOC";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PROJECTID_", Convert.ToInt32(ProjectID));
                cmd.Parameters.Add("HHID_", HHID);
                cmd.Parameters.Add("userID_", userID);
                cmd.Parameters.Add("KeyWord_", KeyWord);
                cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UploadDocumentBO objUploadDocument = null;

                while (dr.Read())
                {
                    objUploadDocument = new UploadDocumentBO(); //PAPDOCUMENTID
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objUploadDocument.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (HHID == 0 || objUploadDocument.HHID == HHID)
                    {
                        string DocumentPath_ = string.Empty;
                        string getDate = string.Empty;
                        if (!dr.IsDBNull(dr.GetOrdinal("PAPDOCUMENTID"))) objUploadDocument.PAPDOCUMENTID = dr.GetInt32(dr.GetOrdinal("PAPDOCUMENTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objUploadDocument.HHID = dr.GetInt32(dr.GetOrdinal("HHID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPEID"))) objUploadDocument.DocumentTypeID = dr.GetInt32(dr.GetOrdinal("DOCUMENTTYPEID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPE"))) objUploadDocument.DocumentType = dr.GetString(dr.GetOrdinal("DOCUMENTTYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) objUploadDocument.DocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) DocumentPath_ = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objUploadDocument.Projectcode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                        if (HHID == 0 || objUploadDocument.HHID <= 0)
                        {
                            string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                            //Edwin: 27MAY2016 To facilitate reading from alternative location
                            string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                            string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;

                            if (File.Exists(main_strPath_user))
                            {
                                objUploadDocument.DocumentPath = main_strPath_user;
                            }
                            else if (File.Exists(backup_strPath_user))
                            {
                                objUploadDocument.DocumentPath = backup_strPath_user;
                            }
                            else
                            {
                                objUploadDocument.DocumentPath = offsite_strPath_user;
                            }
                        }
                        else
                        {
                            string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                            //Edwin: 27MAY2016 To facilitate reading from alternative location
                            string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                            string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;

                            if (File.Exists(main_strPath_user))
                            {
                                objUploadDocument.DocumentPath = main_strPath_user;
                            }
                            else if (File.Exists(backup_strPath_user))
                            {
                                objUploadDocument.DocumentPath = backup_strPath_user;
                            }
                            else
                            {
                                objUploadDocument.DocumentPath = offsite_strPath_user;
                            }
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objUploadDocument.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("UPLOADEDDATE"))) objUploadDocument.Date = dr.GetDateTime(dr.GetOrdinal("UPLOADEDDATE")).ToString(UtilBO.DateFormat);
                        if (!dr.IsDBNull(dr.GetOrdinal("KEYWORD"))) objUploadDocument.KeyWord = dr.GetString(dr.GetOrdinal("KEYWORD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objUploadDocument.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        UploadDocumentList.Add(objUploadDocument);
                    }
                }

                dr.Close();
            }
            else
            {
                OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;

                string proc = "USP_TRN_SELUPDDOCSEARCH";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PROJECTID_", Convert.ToInt32(ProjectID));
                cmd.Parameters.Add("KeyWord_", KeyWord);
                cmd.Parameters.Add("HHID_", HHID);
                cmd.Parameters.Add("userID_", userID);
                cmd.Parameters.Add("DocumentCode_", DocumentCode);
                cmd.Parameters.Add("DOCSERVICEID_", DOCSERVICEID);
                cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UploadDocumentBO objUploadDocument = null;

                while (dr.Read())
                {
                    string DocumentPath_ = string.Empty;
                    objUploadDocument = new UploadDocumentBO(); //PAPDOCUMENTID
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPDOCUMENTID"))) objUploadDocument.PAPDOCUMENTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAPDOCUMENTID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objUploadDocument.HHID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPEID"))) objUploadDocument.DocumentTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOCUMENTTYPEID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTTYPE"))) objUploadDocument.DocumentType = dr.GetString(dr.GetOrdinal("DOCUMENTTYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) objUploadDocument.DocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTPATH"))) DocumentPath_ = dr.GetString(dr.GetOrdinal("DOCUMENTPATH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTCODE"))) objUploadDocument.Projectcode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                    if (HHID == 0 || objUploadDocument.HHID <= 0)
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + DocumentPath_;// +DocPath;

                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }
                    }
                    else
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        //Edwin: 27MAY2016 To facilitate reading from alternative location
                        string backup_strPath_user = ConfigurationManager.AppSettings["backup_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;
                        string offsite_strPath_user = ConfigurationManager.AppSettings["offsite_strPath_user"].ToString() + objUploadDocument.Projectcode.ToString() + "\\" + objUploadDocument.HHID.ToString() + "\\" + DocumentPath_;// +DocPath;

                        if (File.Exists(main_strPath_user))
                        {
                            objUploadDocument.DocumentPath = main_strPath_user;
                        }
                        else if (File.Exists(backup_strPath_user))
                        {
                            objUploadDocument.DocumentPath = backup_strPath_user;
                        }
                        else
                        {
                            objUploadDocument.DocumentPath = offsite_strPath_user;
                        }
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("USERNAME"))) objUploadDocument.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("UPLOADEDDATE"))) objUploadDocument.Date = dr.GetDateTime(dr.GetOrdinal("UPLOADEDDATE")).ToString(UtilBO.DateFormat); ;
                    if (!dr.IsDBNull(dr.GetOrdinal("KEYWORD"))) objUploadDocument.KeyWord = dr.GetString(dr.GetOrdinal("KEYWORD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objUploadDocument.Description = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    UploadDocumentList.Add(objUploadDocument);
                }

                dr.Close();
            }
            return UploadDocumentList;
        }

    }
}