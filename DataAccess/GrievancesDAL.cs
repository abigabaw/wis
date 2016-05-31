using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class GrievancesDAL
    {
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public GrievancesBO getscreenIntialization(int hhid)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_GRIEVANCEIDDATA";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("hhid_", hhid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            GrievancesBO GrievancesBOObj = null;
            GrievanceList GrievanceListObj = new GrievanceList();
            GrievancesBOObj = new GrievancesBO();

            while (dr.Read())
            {
                if (ColumnExists(dr, "HHID") && !dr.IsDBNull(dr.GetOrdinal("HHID")))
                    GrievancesBOObj.Hhid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID")));

                if (ColumnExists(dr, "PAPNAME") && !dr.IsDBNull(dr.GetOrdinal("PAPNAME")))
                    GrievancesBOObj.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));

                if (ColumnExists(dr, "PLOTREFERENCE") && !dr.IsDBNull(dr.GetOrdinal("PLOTREFERENCE")))
                    GrievancesBOObj.PlotReference = dr.GetString(dr.GetOrdinal("PLOTREFERENCE"));

                if (ColumnExists(dr, "DISTRICT") && !dr.IsDBNull(dr.GetOrdinal("DISTRICT")))
                    GrievancesBOObj.District = dr.GetString(dr.GetOrdinal("DISTRICT"));

                if (ColumnExists(dr, "COUNTY") && !dr.IsDBNull(dr.GetOrdinal("COUNTY")))
                    GrievancesBOObj.County = dr.GetString(dr.GetOrdinal("COUNTY"));

                if (ColumnExists(dr, "SUBCOUNTY") && !dr.IsDBNull(dr.GetOrdinal("SUBCOUNTY")))
                    GrievancesBOObj.SubCounty = dr.GetString(dr.GetOrdinal("SUBCOUNTY"));

                if (ColumnExists(dr, "PARISH") && !dr.IsDBNull(dr.GetOrdinal("PARISH")))
                    GrievancesBOObj.Parish = dr.GetString(dr.GetOrdinal("PARISH"));

                if (ColumnExists(dr, "VILLAGE") && !dr.IsDBNull(dr.GetOrdinal("VILLAGE")))
                    GrievancesBOObj.Village = dr.GetString(dr.GetOrdinal("VILLAGE"));
            }
            dr.Close();

            return GrievancesBOObj;
        }
        /// <summary>
        /// to check whether column exists
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// to fetch all details
        /// </summary>
        /// <returns></returns>
        public GrievanceList Getcategory()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_GRIEV_CATEGORY";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesBO BOobj = null;
            GrievanceList Listobj = new GrievanceList();

            while (dr.Read())
            {
                BOobj = new GrievancesBO();
                BOobj.GrievCategoryID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRIEVANCECATEGID"))));
                BOobj. GrievCategory = dr.GetValue(dr.GetOrdinal("GRIEVANCECATEGORY")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;

        }



        //public DataSet Getcategory( )
        //{
        //    String SqlQuery;

        //    OracleConnection OraConnection = new OracleConnection(AppConfiguration.ConnectionString);
        //    //  OracleCommand OraCommand;



        //    SqlQuery = "select grievancecategid,grievancecategory from MST_GRIEVANCE_CATEG";
        //    //where  isdeleted = 'False'

        //    OracleCommand OracleCommand = new OracleCommand(SqlQuery, OraConnection);
        //    OracleDataAdapter dAd = new OracleDataAdapter(OracleCommand);
        //    dAd.SelectCommand.CommandType = CommandType.Text;
        //    DataSet Ds = new DataSet();

        //    try
        //    {
        //        dAd.Fill(Ds);
        //        return Ds;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Ds.Dispose();
        //        dAd.Dispose();

        //    }
        //}
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="GrievancesBOobj"></param>
        /// <returns></returns>
        public int Insert(GrievancesBO GrievancesBOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_GRIEVANCE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", GrievancesBOobj.Hhid);
                dcmd.Parameters.Add("GRIEVANCECATEGID", GrievancesBOobj.GrievCategoryID);
                dcmd.Parameters.Add("GREVDESCRIPTION", GrievancesBOobj.Description);
                dcmd.Parameters.Add("ACTIONTAKEN", GrievancesBOobj.ActionTaken);

                if (GrievancesBOobj.ActionTakenDate != DateTime.MinValue)
                    dcmd.Parameters.Add("ACTIONTAKENDATE", GrievancesBOobj.ActionTakenDate);
                else
                    dcmd.Parameters.Add("ACTIONTAKENDATE", DBNull.Value);                

                if (GrievancesBOobj.ActionTakenBy > 0)
                    dcmd.Parameters.Add("ACTIONTAKENBY", GrievancesBOobj.ActionTakenBy);
                else
                    dcmd.Parameters.Add("ACTIONTAKENBY", DBNull.Value);

                dcmd.Parameters.Add("BASICFACTS", GrievancesBOobj.BasicFacts);
                dcmd.Parameters.Add("RESOLUTION", GrievancesBOobj.Resolution);

                if (GrievancesBOobj.ResolutionDate != DateTime.MinValue)
                    dcmd.Parameters.Add("RESOLUTIONDATE", GrievancesBOobj.ResolutionDate);
                else
                    dcmd.Parameters.Add("RESOLUTIONDATE", DBNull.Value);

                if (GrievancesBOobj.ResolvedBy > 0)
                    dcmd.Parameters.Add("RESOLVEDBY", GrievancesBOobj.ResolvedBy);
                else
                    dcmd.Parameters.Add("RESOLVEDBY", DBNull.Value);

                dcmd.Parameters.Add("CREATEDBY", GrievancesBOobj.CreatedBy);
                dcmd.Parameters.Add("ResolutionStatus_", GrievancesBOobj.ResolutionStatus);

                return dcmd.ExecuteNonQuery();

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
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public GrievanceList Getgrievancedata(int householdID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_GRIEVANCE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            GrievancesBO GrievancesBOObj = null;
            GrievanceList GrievanceListObj = new GrievanceList();          
            
            while (dr.Read())
            {
                 GrievancesBOObj = new GrievancesBO();
                 if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCEID"))) GrievancesBOObj.GrievanceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRIEVANCEID")));
                 if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) GrievancesBOObj.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                 if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCECATEGORY"))) GrievancesBOObj.GrievCategory = dr.GetString(dr.GetOrdinal("GRIEVANCECATEGORY"));
                 if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE"))) GrievancesBOObj.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));
                 if (!dr.IsDBNull(dr.GetOrdinal("GREVDESCRIPTION"))) GrievancesBOObj.Description = dr.GetString(dr.GetOrdinal("GREVDESCRIPTION"));
                 if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) GrievancesBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                 if (!dr.IsDBNull(dr.GetOrdinal("RESOLUTIONSTATUS"))) GrievancesBOObj.ResolutionStatus = dr.GetString(dr.GetOrdinal("RESOLUTIONSTATUS"));
                 GrievanceListObj.Add(GrievancesBOObj);
            }

            dr.Close();

            return GrievanceListObj;
        }
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
        public GrievancesBO GetGrievancedatarow(int GrievanceID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_GRIEVANCE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("R_GRIEVANCEID", GrievanceID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesBO GrievancesBOObj = null;
            GrievanceList GrievanceListObj = new GrievanceList();

            GrievancesBOObj = new GrievancesBO();
            while (dr.Read())
            {

                if (!dr.IsDBNull(dr.GetOrdinal("grievanceid")))
                    GrievancesBOObj.GrievanceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grievanceid")));

                if (!dr.IsDBNull(dr.GetOrdinal("grievancecategid")))
                    GrievancesBOObj.GrievCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("grievancecategid")));

                if (!dr.IsDBNull(dr.GetOrdinal("grevdescription")))
                    GrievancesBOObj.Description = Convert.ToString(dr.GetValue(dr.GetOrdinal("grevdescription")));

                if (!dr.IsDBNull(dr.GetOrdinal("actiontaken")))
                    GrievancesBOObj.ActionTaken = Convert.ToString(dr.GetValue(dr.GetOrdinal("actiontaken")));

                if (!dr.IsDBNull(dr.GetOrdinal("actiontakendate")))
                    GrievancesBOObj.ActionTakenDate = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("actiontakendate")));

                if (!dr.IsDBNull(dr.GetOrdinal("actiontakenby")))
                    GrievancesBOObj.ActionTakenBy = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("actiontakenby")));

                if (!dr.IsDBNull(dr.GetOrdinal("basicfacts")))
                    GrievancesBOObj.BasicFacts = Convert.ToString(dr.GetValue(dr.GetOrdinal("basicfacts")));

                if (!dr.IsDBNull(dr.GetOrdinal("resolution")))
                    GrievancesBOObj.Resolution = Convert.ToString(dr.GetValue(dr.GetOrdinal("resolution")));

                if (!dr.IsDBNull(dr.GetOrdinal("resolutiondate")))
                    GrievancesBOObj.ResolutionDate = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("resolutiondate")));

                if (!dr.IsDBNull(dr.GetOrdinal("resolvedby")))
                    GrievancesBOObj.ResolvedBy = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("resolvedby")));

            }
            dr.Close();


            return GrievancesBOObj;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
        public int  Delete(int GrievanceID)
        {
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_GRIEVANCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("GRIEVANCEID_", GrievanceID);
                return dCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();

            }
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="GrievancesBOobj"></param>
        /// <returns></returns>
        public int EditGRIEVANCE(GrievancesBO GrievancesBOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_GRIEVANCE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("R_GRIEVANCEID", GrievancesBOobj.GrievanceID);
                dcmd.Parameters.Add("R_GRIEVANCECATEGID", GrievancesBOobj.GrievCategoryID);
                dcmd.Parameters.Add("R_GREVDESCRIPTION", GrievancesBOobj.Description);
                dcmd.Parameters.Add("R_ACTIONTAKEN", GrievancesBOobj.ActionTaken);

                if (GrievancesBOobj.ActionTakenDate != DateTime.MinValue)
                    dcmd.Parameters.Add("R_ACTIONTAKENDATE", GrievancesBOobj.ActionTakenDate);
                else
                    dcmd.Parameters.Add("R_ACTIONTAKENDATE", DBNull.Value);               

                if (GrievancesBOobj.ActionTakenBy > 0)
                    dcmd.Parameters.Add("ACTIONTAKENBY", GrievancesBOobj.ActionTakenBy);
                else
                    dcmd.Parameters.Add("ACTIONTAKENBY", DBNull.Value);

                dcmd.Parameters.Add("R_BASICFACTS", GrievancesBOobj.BasicFacts);
                dcmd.Parameters.Add("R_RESOLUTION", GrievancesBOobj.Resolution);

                if (GrievancesBOobj.ResolutionDate != DateTime.MinValue)
                    dcmd.Parameters.Add("R_RESOLUTIONDATE", GrievancesBOobj.ResolutionDate);
                else
                    dcmd.Parameters.Add("R_RESOLUTIONDATE", DBNull.Value);

                if (GrievancesBOobj.ResolvedBy > 0)
                    dcmd.Parameters.Add("R_RESOLVEDBY", GrievancesBOobj.ResolvedBy);
                else
                    dcmd.Parameters.Add("R_RESOLVEDBY", DBNull.Value);

                dcmd.Parameters.Add("R_UPDATEDBY", GrievancesBOobj.CreatedBy);
                dcmd.Parameters.Add("ResolutionStatus_", GrievancesBOobj.ResolutionStatus);

                return dcmd.ExecuteNonQuery();
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
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
        public GrievancesBO GetGrievanceClosure(int GrievanceID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_CLOSURE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("R_GRIEVANCEID", GrievanceID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesBO GrievancesBOObj = null;
            GrievanceList GrievanceListObj = new GrievanceList();

            GrievancesBOObj = new GrievancesBO();
            while (dr.Read())
            {
                //if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCEID"))) GrievancesBOObj.GrievanceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRIEVANCEID")));
                //if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) GrievancesBOObj.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCECATEGORY"))) GrievancesBOObj.GrievCategory = dr.GetString(dr.GetOrdinal("GRIEVANCECATEGORY"));
                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE"))) GrievancesBOObj.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));
                //if (!dr.IsDBNull(dr.GetOrdinal("GREVDESCRIPTION"))) GrievancesBOObj.Description = dr.GetString(dr.GetOrdinal("GREVDESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("CLOSURECOMMENTS"))) GrievancesBOObj.ClosureComments = dr.GetString(dr.GetOrdinal("CLOSURECOMMENTS"));
                //if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) GrievancesBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                //if (!dr.IsDBNull(dr.GetOrdinal("RESOLUTIONSTATUS"))) GrievancesBOObj.ResolutionStatus = dr.GetString(dr.GetOrdinal("RESOLUTIONSTATUS"));
            }
            dr.Close();
            return GrievancesBOObj;
        }
        /// <summary>
        /// to fetch all details
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public GrievancesBO getGrievanceOverAllStatus(int HHID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_HASOPENGRIEVANCES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("HHID_", HHID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GrievancesBO GrievancesBOObj = null;
            GrievanceList GrievanceListObj = new GrievanceList();

            GrievancesBOObj = new GrievancesBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCEID"))) 
                    GrievancesBOObj.GrievanceID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRIEVANCEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("PAPNAME"))) 
                    GrievancesBOObj.PapName = dr.GetString(dr.GetOrdinal("PAPNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("GRIEVANCECATEGORY"))) 
                    GrievancesBOObj.GrievCategory = dr.GetString(dr.GetOrdinal("GRIEVANCECATEGORY"));
                if (!dr.IsDBNull(dr.GetOrdinal("CREATEDDATE"))) 
                    GrievancesBOObj.CreatedDate = dr.GetDateTime(dr.GetOrdinal("CREATEDDATE"));
                if (!dr.IsDBNull(dr.GetOrdinal("GREVDESCRIPTION"))) 
                    GrievancesBOObj.Description = dr.GetString(dr.GetOrdinal("GREVDESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) 
                    GrievancesBOObj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                //if (!dr.IsDBNull(dr.GetOrdinal("RESOLUTIONSTATUS"))) 
                //    GrievancesBOObj.ResolutionStatus = dr.GetString(dr.GetOrdinal("RESOLUTIONSTATUS"));
                if (!dr.IsDBNull(dr.GetOrdinal("Status")))
                    GrievancesBOObj.Status = dr.GetString(dr.GetOrdinal("Status"));
            }
            dr.Close();
            return GrievancesBOObj;
        }
    }
}