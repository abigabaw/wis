using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class DistrictDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public DistrictList GetDistrict()
        {
            cnn = new OracleConnection(connStr);

            string proc = "USP_MST_GET_DISTRICT";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

           
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DistrictBO  objDisBO = null;
            DistrictList objDislist = new DistrictList();

            while (dr.Read())
            {
                objDisBO = new DistrictBO();
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) objDisBO.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
              //  objDisBO.DistrictID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DISTRICTID"))));
                if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objDisBO.DistrictName = dr.GetValue(dr.GetOrdinal("DISTRICTNAME")).ToString();
                objDislist.Add(objDisBO);
            }

            dr.Close();
            return objDislist;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public DistrictList GetAllDistricts()
        {
            DistrictList objDislist = null;

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_GET_DISTRICT_ALL", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    

                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DistrictBO objDisBO = null;
                    objDislist = new DistrictList();

                    while (dr.Read())
                    {
                        objDisBO = new DistrictBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) objDisBO.DistrictID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DISTRICTID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objDisBO.DistrictName = dr.GetValue(dr.GetOrdinal("DISTRICTNAME")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objDisBO.IsDeleted = dr.GetValue(dr.GetOrdinal("ISDELETED")).ToString();
                       
                        objDislist.Add(objDisBO);
                    }

                    dr.Close();
                }                
            }

            return objDislist;
        }
        /// <summary>
        /// To insert data to database
        /// </summary>
        /// <param name="objDistrictBO"></param>
        /// <returns></returns>
        public string AddDistrict(DistrictBO objDistrictBO)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_INS_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DISTRICTNAME_", objDistrictBO.DistrictName);
                    cmd.Parameters.Add("CREATEDBY_", objDistrictBO.CreatedBy);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="objDistrictBO"></param>
        /// <returns></returns>
        public string UpdateDistrict(DistrictBO objDistrictBO)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_UPD_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DISTRICTID_", objDistrictBO.DistrictID);
                    cmd.Parameters.Add("DISTRICTNAME_", objDistrictBO.DistrictName);
                    cmd.Parameters.Add("UPDATEDBY_", objDistrictBO.UpdatedBy);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// To delete data from database
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public string DeleteDistrict(int districtID)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_DEL_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DISTRICTID_", districtID);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="districtID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteDistrict(int districtID, string isDeleted, int updatedBy)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_OBS_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DISTRICTID_", districtID);
                    cmd.Parameters.Add("UPDATEDBY_", updatedBy);
                    cmd.Parameters.Add("ISDELETED_", isDeleted);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        /// <summary>
        /// to get data based on ID
        /// </summary>
        /// <param name="DistrictId"></param>
        /// <returns></returns>
        public DistrictBO GetDistrictById(int DistrictId)
        {
            proc = "USP_MST_GET_DISTRICTBYID";

            cnn = new OracleConnection(connStr);
            DistrictBO DistrictBOobj = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("DISTRICTID_", DistrictId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    DistrictBOobj = new DistrictBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) DistrictBOobj.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) DistrictBOobj.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DistrictBOobj;
        }
        /// <summary>
        /// To get details
        /// </summary>
        /// <param name="districtname"></param>
        /// <returns></returns>
        public DistrictList SearchDistrict(string districtname)
        {
            proc = "USP_MST_SEARCH_ALLDISTRICTS";
            DistrictBO objDisBO = null;
            DistrictList objDislist = new DistrictList();

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (districtname != "")
                        cmd.Parameters.Add("DistrictName_", districtname);
                    else
                        cmd.Parameters.Add("DistrictName_", DBNull.Value);


                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            objDisBO = new DistrictBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTID"))) objDisBO.DistrictID = dr.GetInt32(dr.GetOrdinal("DISTRICTID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) objDisBO.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objDisBO.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                            objDislist.Add(objDisBO);
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return objDislist;
        }

    }
}
