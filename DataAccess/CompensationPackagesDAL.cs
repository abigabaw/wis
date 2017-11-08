using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;



namespace WIS_DataAccess
{
    public class CompensationPackagesDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
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
            cnn = new SqlConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("HHID_", HHID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PackageCat_", PackageCat);
            cmd.Parameters.AddWithValue("HHID_", householdID);
            cmd.Parameters.AddWithValue("USERID_", USERID);


            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_GRIEAPPROVAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", objCompensationPackagesBO.HHID);
                dcmd.Parameters.AddWithValue("DocumentCode_", objCompensationPackagesBO.DocumentCode);

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
            cnn = new SqlConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.AddWithValue("PackageCat_", PackageCat);
            cmd.Parameters.AddWithValue("HHID_", householdID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_APP_PKGDOCUMENT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", objCOMPPACKBO.HHID);
                dcmd.Parameters.AddWithValue("APPROVERCOMMENTS_", objCOMPPACKBO.ApprovalComents);
                dcmd.Parameters.AddWithValue("REVIEWEDBY_", objCOMPPACKBO.UserID);
                dcmd.Parameters.AddWithValue("APPROVAL_LEVEL", objCOMPPACKBO.ApprovalLevel);
                dcmd.Parameters.AddWithValue("DOCUMENTCODE", objCOMPPACKBO.DocumentCode);
                dcmd.Parameters.AddWithValue("PROJECTID", objCOMPPACKBO.ProjectID);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            cnn = new SqlConnection(con);
            CompensationPackagesBO oCompensationPackagesBO = null;

            CompensationPackagesList oCompensationPackagesList = new CompensationPackagesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("DocumentCode_", pCompensationPackagesBO.DocumentCode);
            cmd.Parameters.AddWithValue("HHID_", pCompensationPackagesBO.HHID);
            cmd.Parameters.AddWithValue("ApprovalLevel_", pCompensationPackagesBO.ApprovalLevel);
            cmd.Parameters.AddWithValue("UserID_", pCompensationPackagesBO.UserID);
            cmd.Parameters.AddWithValue("ProjectID_", pCompensationPackagesBO.ProjectID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new SqlConnection(con);
            CompensationPackagesBO objCOMPACK = null;

            CompensationPackagesList COMPACKList = new CompensationPackagesList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("DocumentCode_", objCOMPPACKBO.DocumentCode);
            cmd.Parameters.AddWithValue("HHID_", objCOMPPACKBO.HHID);
            cmd.Parameters.AddWithValue("ApprovalLevel_", objCOMPPACKBO.ApprovalLevel);
            cmd.Parameters.AddWithValue("UserID_", objCOMPPACKBO.UserID);
            cmd.Parameters.AddWithValue("ProjectID_", objCOMPPACKBO.ProjectID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GETPRINTCOUNT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("DocumentCode_", cmppkgBo.DocumentCode);
            cmd.Parameters.AddWithValue("HHID_", cmppkgBo.HHID);
            cmd.Parameters.AddWithValue("UserID_", cmppkgBo.UserID);
            cmd.Parameters.AddWithValue("ProjectID_", cmppkgBo.ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PKGPRINTDOC", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID_", cmppkgBo.HHID);
                dcmd.Parameters.AddWithValue("DOCUMENTCODE", cmppkgBo.DocumentCode);
                dcmd.Parameters.AddWithValue("REVIEWEDBY_", cmppkgBo.UserID);
                dcmd.Parameters.AddWithValue("APPROVERCOMMENTS_", cmppkgBo.ApprovalComents);
                dcmd.Parameters.AddWithValue("PROJECTID", cmppkgBo.ProjectID);
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PKGREADSTATUS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("DocItemId_", DocItemId);
                dcmd.Parameters.AddWithValue("HHID_", HHID);
                dcmd.Parameters.AddWithValue("USERID_", UID);
                dcmd.Parameters.AddWithValue("Status_", Status);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            
            string proc = "USP_TRN_GET_PRINTPKGCOMMENTS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("HHID_", Hhid);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
             try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
