using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class DistrictDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public DistrictList GetDistrict()
        {
            cnn = new SqlConnection(connStr);

            string proc = "USP_MST_GET_DISTRICT";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

           
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_GET_DISTRICT_ALL", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    

                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_INS_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DISTRICTNAME_", objDistrictBO.DistrictName);
                    cmd.Parameters.AddWithValue("CREATEDBY_", objDistrictBO.CreatedBy);
                    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_UPD_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DISTRICTID_", objDistrictBO.DistrictID);
                    cmd.Parameters.AddWithValue("DISTRICTNAME_", objDistrictBO.DistrictName);
                    cmd.Parameters.AddWithValue("UPDATEDBY_", objDistrictBO.UpdatedBy);
                    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_DEL_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DISTRICTID_", districtID);
                    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_OBS_DISTRICT", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DISTRICTID_", districtID);
                    cmd.Parameters.AddWithValue("UPDATEDBY_", updatedBy);
                    cmd.Parameters.AddWithValue("ISDELETED_", isDeleted);
                    /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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

            cnn = new SqlConnection(connStr);
            DistrictBO DistrictBOobj = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("DISTRICTID_", DistrictId);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (districtname != "")
                        cmd.Parameters.AddWithValue("DistrictName_", districtname);
                    else
                        cmd.Parameters.AddWithValue("DistrictName_", DBNull.Value);


                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
