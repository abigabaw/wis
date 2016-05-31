using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class CompensationPackagesDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion


        public bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// To Get Componestion by Id
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationPackagesList GetComponestionbyId(int HHID)
        {
            proc = "USP_MST_SEL_COM_PACK";
            cnn = new OracleConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCOMPACK = new CompensationPackagesBO();

                    //if (ColumnExists(dr, "PKGDOCITEMID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMID")))
                    //    objCOMPACK.PKGdocItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCITEMID")));

                    if (ColumnExists(dr, "PKGDOCCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCCATEGORYID")))
                        objCOMPACK.CATpkgdoccatID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCCATEGORYID")));

                    //if (ColumnExists(dr, "PKGDOCCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCCATEGORYID")))
                    //    objCOMPACK.ITEMpkgdocitemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCCATEGORYID")));

                    if (ColumnExists(dr, "PKGDOCCATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCCATEGORYNAME")))
                        objCOMPACK.PKGDoccatName = dr.GetString(dr.GetOrdinal("PKGDOCCATEGORYNAME"));

                    //if (ColumnExists(dr, "PKGDOCITEMNAME") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMNAME")))
                    //    objCOMPACK.PKGDOCitemName = dr.GetString(dr.GetOrdinal("PKGDOCITEMNAME"));

                    COMPACKList.Add(objCOMPACK);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return COMPACKList;
        }

        /// <summary>
        /// To Get Componestion
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PackageCat"></param>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public CompensationPackagesList GetComponestion(int householdID, int PackageCat, int USERID)
        {
            proc = "USP_MST_SEL_COM_PACK_ITEM";
            cnn = new OracleConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PackageCat_", PackageCat);
            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("USERID_", USERID);


            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCOMPACK = new CompensationPackagesBO();

                    if (ColumnExists(dr, "PKGDOCITEMID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMID")))
                        objCOMPACK.PKGdocItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCITEMID")));

                    if (ColumnExists(dr, "PKGDOCCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCCATEGORYID")))
                        objCOMPACK.ITEMpkgdocitemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCCATEGORYID")));

                    if (ColumnExists(dr, "PKGDOCITEMNAME") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMNAME")))
                        objCOMPACK.PKGDOCitemName = dr.GetString(dr.GetOrdinal("PKGDOCITEMNAME"));

                    if (ColumnExists(dr, "DOCUMENTCODE") && !dr.IsDBNull(dr.GetOrdinal("DOCUMENTCODE")))
                        objCOMPACK.PKGDocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTCODE"));

                    if (ColumnExists(dr, "Status") && !dr.IsDBNull(dr.GetOrdinal("Status")))
                        objCOMPACK.Status = dr.GetString(dr.GetOrdinal("Status"));

                    COMPACKList.Add(objCOMPACK);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return COMPACKList;
        }

        /// <summary>
        /// To Update Approval Status
        /// </summary>
        /// <param name="objCompensationPackagesBO"></param>
        /// <returns></returns>
        public int UpdateApprovalStatus(CompensationPackagesBO objCompensationPackagesBO)
        {
            int returnResult;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_GRIEAPPROVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", objCompensationPackagesBO.HHID);
                dcmd.Parameters.Add("DocumentCode_", objCompensationPackagesBO.DocumentCode);

                returnResult = dcmd.ExecuteNonQuery();

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
        /// To Get Componestion by HHId
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CompensationPackagesList GetComponestionbyHHId(int householdID)
        {
            proc = "USP_MST_SELCOMPKGAPP";
            cnn = new OracleConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add("PackageCat_", PackageCat);
            cmd.Parameters.Add("HHID_", householdID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCOMPACK = new CompensationPackagesBO();

                    if (ColumnExists(dr, "PKGDOCITEMID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMID")))
                        objCOMPACK.PKGdocItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCITEMID")));

                    if (ColumnExists(dr, "PKGDOCCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCCATEGORYID")))
                        objCOMPACK.ITEMpkgdocitemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PKGDOCCATEGORYID")));

                    if (ColumnExists(dr, "PKGDOCITEMNAME") && !dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMNAME")))
                        objCOMPACK.PKGDOCitemName = dr.GetString(dr.GetOrdinal("PKGDOCITEMNAME"));

                    if (ColumnExists(dr, "DOCUMENTCODE") && !dr.IsDBNull(dr.GetOrdinal("DOCUMENTCODE")))
                        objCOMPACK.PKGDocumentCode = dr.GetString(dr.GetOrdinal("DOCUMENTCODE"));

                    COMPACKList.Add(objCOMPACK);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return COMPACKList;
        }

        /// <summary>
        /// To Save Approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public int SaveApprovalComments(CompensationPackagesBO objCOMPPACKBO)
        {
            int returnResult;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_APP_PKGDOCUMENT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", objCOMPPACKBO.HHID);
                dcmd.Parameters.Add("APPROVERCOMMENTS_", objCOMPPACKBO.ApprovalComents);
                dcmd.Parameters.Add("REVIEWEDBY_", objCOMPPACKBO.UserID);
                dcmd.Parameters.Add("APPROVAL_LEVEL", objCOMPPACKBO.ApprovalLevel);
                dcmd.Parameters.Add("DOCUMENTCODE", objCOMPPACKBO.DocumentCode);
                dcmd.Parameters.Add("PROJECTID", objCOMPPACKBO.ProjectID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                returnResult = dcmd.ExecuteNonQuery();

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
        /// To get approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public CompensationPackagesList getApproverReviewComments(CompensationPackagesBO pCompensationPackagesBO)
        {
            proc = "USP_TRN_GET_REV_COMMENTS";
            cnn = new OracleConnection(con);
            CompensationPackagesBO oCompensationPackagesBO = null;

            CompensationPackagesList oCompensationPackagesList = new CompensationPackagesList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("DocumentCode_", pCompensationPackagesBO.DocumentCode);
            cmd.Parameters.Add("HHID_", pCompensationPackagesBO.HHID);
            cmd.Parameters.Add("ApprovalLevel_", pCompensationPackagesBO.ApprovalLevel);
            cmd.Parameters.Add("UserID_", pCompensationPackagesBO.UserID);
            cmd.Parameters.Add("ProjectID_", pCompensationPackagesBO.ProjectID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oCompensationPackagesBO = new CompensationPackagesBO();


                    if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS")))
                        oCompensationPackagesBO.ApprovalComents = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("username")))
                        oCompensationPackagesBO.UserName = dr.GetString(dr.GetOrdinal("username"));
                    if (!dr.IsDBNull(dr.GetOrdinal("APPROVAL_LEVEL")))
                        oCompensationPackagesBO.ApprovalLevel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVAL_LEVEL")));
                    if (!dr.IsDBNull(dr.GetOrdinal("REVIEWDATE"))) 
                        oCompensationPackagesBO.ApprovedDate = dr.GetDateTime(dr.GetOrdinal("REVIEWDATE")).ToString(UtilBO.DateFormatFull);
                    if (!dr.IsDBNull(dr.GetOrdinal("REVIEWDATE")))
                        oCompensationPackagesBO.PKGDoccatName = dr.GetString(dr.GetOrdinal("pkgdocitemname"));
                    oCompensationPackagesList.Add(oCompensationPackagesBO);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oCompensationPackagesList;
        }

        /// <summary>
        /// To get approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public CompensationPackagesBO getapprovalComments(CompensationPackagesBO objCOMPPACKBO)
        {
            proc = "USP_TRN_GET_PKGAPPCOMMENTS";
            cnn = new OracleConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("DocumentCode_", objCOMPPACKBO.DocumentCode);
            cmd.Parameters.Add("HHID_", objCOMPPACKBO.HHID);
            cmd.Parameters.Add("ApprovalLevel_", objCOMPPACKBO.ApprovalLevel);
            cmd.Parameters.Add("UserID_", objCOMPPACKBO.UserID);
            cmd.Parameters.Add("ProjectID_", objCOMPPACKBO.ProjectID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objCOMPACK = new CompensationPackagesBO();


                    if (!dr.IsDBNull(dr.GetOrdinal("APPROVERCOMMENTS"))) objCOMPACK.ApprovalComents = dr.GetString(dr.GetOrdinal("APPROVERCOMMENTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("username"))) objCOMPACK.UserName = dr.GetString(dr.GetOrdinal("username"));
                    if (!dr.IsDBNull(dr.GetOrdinal("APPROVAL_LEVEL"))) objCOMPACK.ApprovalLevel = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("APPROVAL_LEVEL")));
                    if (!dr.IsDBNull(dr.GetOrdinal("REVIEWDATE"))) objCOMPACK.ApprovedDate = dr.GetDateTime(dr.GetOrdinal("REVIEWDATE")).ToString(UtilBO.DateFormatFull);
                    //COMPACKList.Add(objCOMPACK);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objCOMPACK;
        }

        /// <summary>
        /// To get pre Comments
        /// </summary>
        /// <param name="cmppkgBo"></param>
        /// <returns></returns>
        public CompensationPackagesBO getpreComments(CompensationPackagesBO cmppkgBo)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GETPRINTCOUNT";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DocumentCode_", cmppkgBo.DocumentCode);
            cmd.Parameters.Add("HHID_", cmppkgBo.HHID);
            cmd.Parameters.Add("UserID_", cmppkgBo.UserID);
            cmd.Parameters.Add("ProjectID_", cmppkgBo.ProjectID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CompensationPackagesBO objCMPPACK = null;
            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            while (dr.Read())
            {
                objCMPPACK = new CompensationPackagesBO();

                if (!dr.IsDBNull(dr.GetOrdinal("TOTALPRINT"))) objCMPPACK.PKGdocCount = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TOTALPRINT")));

            }

            dr.Close();

            return objCMPPACK;
        }

        /// <summary>
        /// To Save reprint Comments
        /// </summary>
        /// <param name="cmppkgBo"></param>
        /// <returns></returns>
        public int SavereprintComments(CompensationPackagesBO cmppkgBo)
        {
            int returnResult;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_PKGPRINTDOC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", cmppkgBo.HHID);
                dcmd.Parameters.Add("DOCUMENTCODE", cmppkgBo.DocumentCode);
                dcmd.Parameters.Add("REVIEWEDBY_", cmppkgBo.UserID);
                dcmd.Parameters.Add("APPROVERCOMMENTS_", cmppkgBo.ApprovalComents);
                dcmd.Parameters.Add("PROJECTID", cmppkgBo.ProjectID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                returnResult = dcmd.ExecuteNonQuery();

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
        /// To Update Doc Read Status
        /// </summary>
        /// <param name="DocItemId"></param>
        /// <param name="Status"></param>
        /// <param name="UID"></param>
        /// <param name="HHID"></param>
        public void UpdateDocReadStatus(int DocItemId, string Status, int UID, int HHID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_PKGREADSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("DocItemId_", DocItemId);
                dcmd.Parameters.Add("HHID_", HHID);
                dcmd.Parameters.Add("USERID_", UID);
                dcmd.Parameters.Add("Status_", Status);
                dcmd.ExecuteNonQuery();

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
        public CompensationPackagesList getprintComments(int Hhid)
        {
            CompensationPackagesList COMPACKList = new CompensationPackagesList();
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            
            string proc = "USP_TRN_GET_PRINTPKGCOMMENTS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.Add("HHID_", Hhid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
             try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                CompensationPackagesBO cmppkgBo;
                while (dr.Read())
                {
                    cmppkgBo = new CompensationPackagesBO();


                    if (!dr.IsDBNull(dr.GetOrdinal("PRINTBY"))) cmppkgBo.UserName = dr.GetString(dr.GetOrdinal("PRINTBY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRINTUPDATE"))) cmppkgBo.ApprovedDate = dr.GetValue(dr.GetOrdinal("PRINTUPDATE")).ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("PKGDOCITEMNAME"))) cmppkgBo.PKGDOCitemName = (dr.GetString(dr.GetOrdinal("PKGDOCITEMNAME")));
                    if (!dr.IsDBNull(dr.GetOrdinal("COMMENTS"))) cmppkgBo.ApprovalComents = dr.GetString(dr.GetOrdinal("COMMENTS"));
                    COMPACKList.Add(cmppkgBo);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

             return COMPACKList;
        }
        }
    }
