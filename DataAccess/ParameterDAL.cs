using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;
using System.Configuration;

namespace WIS_DataAccess
{
  public  class ParameterDAL
    {
        string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ConnectionString; //AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public ParameterList GetOptionAvailable()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UETCL_WIS"].ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_OPTIONS";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ParameterBO objcountBO = null;
            ParameterList objcountlist = new ParameterList();

            while (dr.Read())
            {
                objcountBO = new ParameterBO();
                objcountBO.AvailableOptionsID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID"))));
                objcountBO.AvailableOptions = dr.GetValue(dr.GetOrdinal("OPTIONAVAILABLE")).ToString();
                objcountlist.Add(objcountBO);
            }

            dr.Close();
            return objcountlist;
        }

        /// <summary>
        /// To fetch details
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public ParameterList GetAllParameters(int AvailableOptionID)
        {
            ParameterList objParameterList = null;

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_GET_PARAMETERS", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("OPTIONAVAILABLEID_", AvailableOptionID);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    ParameterBO objParameterBO = null;
                    objParameterList = new ParameterList();

                    while (dr.Read())
                    {
                        objParameterBO = new ParameterBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("PARAMETERID"))) objParameterBO.ParameterID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PARAMETERID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("PARAMETERNAME"))) objParameterBO.ParameterName = dr.GetValue(dr.GetOrdinal("PARAMETERNAME")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("optionavailable"))) objParameterBO.AvailableOptions = dr.GetString(dr.GetOrdinal("optionavailable"));
                        if (!dr.IsDBNull(dr.GetOrdinal("isdeleted"))) objParameterBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
                        // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objParameterBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                        objParameterList.Add(objParameterBO);
                    }

                    dr.Close();
                }
            }

            return objParameterList;
        }

        /// <summary>
        /// To insert data to database
        /// </summary>
        /// <param name="objParameterBO"></param>
        /// <returns></returns>
        public string AddParameter(ParameterBO objParameterBO)
        {
            string result = "";

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_INS_PARAMETER", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("AvailableOptionsID_", objParameterBO.AvailableOptionsID);
                    cmd.Parameters.AddWithValue("ParameterName_", objParameterBO.ParameterName);
                    cmd.Parameters.AddWithValue("CREATEDBY_", objParameterBO.CreatedBy);
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
        /// To update details to database
        /// </summary>
        /// <param name="objParameterBO"></param>
        /// <returns></returns>
        public string UpdateParameter(ParameterBO objParameterBO)
        {
            string result = "";

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_UPD_PARAMETER", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("ParameterID_", objParameterBO.ParameterID);
                    cmd.Parameters.AddWithValue("AvailableOptionsID_", objParameterBO.AvailableOptionsID);
                    cmd.Parameters.AddWithValue("ParameterName_", objParameterBO.ParameterName);
                    cmd.Parameters.AddWithValue("UPDATEDBY_", objParameterBO.UpdatedBy);
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
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public string DeleteParameter(int parameterID)
        {
            string result = "";

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_DEL_PARAMETER", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("parameterID_", parameterID);
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
        /// <param name="parameterID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteParameter(int parameterID, string isDeleted, int updatedBy)
        {
            string result = "";

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_OBS_PARAMETER", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("parameterID_", parameterID);
                    cmd.Parameters.AddWithValue("ISDELETED_", isDeleted);
                    cmd.Parameters.AddWithValue("UPDATEDBY_", updatedBy);

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
        /// To get details by ID
        /// </summary>
        /// <param name="parameterID"></param>
        /// <returns></returns>
        public ParameterBO GetParameterById(int parameterID)
        {
            proc = "USP_MST_GET_PARAMETERBYID";

            cnn = new SqlConnection(connStr);
            ParameterBO ParameterBOobj = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("parameterID_", parameterID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ParameterBOobj = new ParameterBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("parameterid")))
                        ParameterBOobj.ParameterID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("parameterid"))); // dr.GetInt32(dr.GetOrdinal("parameterid"));

                    if (!dr.IsDBNull(dr.GetOrdinal("parametername")))
                        ParameterBOobj.ParameterName = dr.GetString(dr.GetOrdinal("parametername"));
                    ////if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) ParameterBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("optionavailable"))) ParameterBOobj.AvailableOptions = dr.GetString(dr.GetOrdinal("optionavailable"));
                    if (!dr.IsDBNull(dr.GetOrdinal("optionavailableid")))
                        ParameterBOobj.AvailableOptionsID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("optionavailableid"))); // dr.GetInt32(dr.GetOrdinal("optionavailableid"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ParameterBOobj;
        }

    }
}
