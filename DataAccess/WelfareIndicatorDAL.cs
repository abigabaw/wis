using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;


namespace WIS_DataAccess
{
    public class WelfareIndicatorDAL
    {
        /// <summary>
        /// To Insert Welfare Indicator
        /// </summary>
        /// <param name="objWelfareIndicatorBO"></param>
        /// <returns></returns>
        public string InsertWelfareIndicator(WelfareIndicatorBO objWelfareIndicatorBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSWELFARE_INDICATOR", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("WLF_INDICATORNAME_", objWelfareIndicatorBO.Wlf_indicatorname);
                dcmd.Parameters.Add("FIELDTYPE_", objWelfareIndicatorBO.Fieldtype);

                if (objWelfareIndicatorBO.AssociatedWith > 0)
                    dcmd.Parameters.Add("ASSOCIATEDWITH_", objWelfareIndicatorBO.AssociatedWith);
                else
                    dcmd.Parameters.Add("ASSOCIATEDWITH_", DBNull.Value);

                dcmd.Parameters.Add("CREATEDBY_", objWelfareIndicatorBO.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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
        /// To Get Welfare Indicator
        /// </summary>
        /// <returns></returns>
        public WelfareIndicatorList GetWelfareIndicator()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETWELFARE_INDICATOR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WelfareIndicatorBO objWelfareIndicatorBO = null;
            WelfareIndicatorList OBJWelfareIndicatorList = new WelfareIndicatorList();

            while (dr.Read())
            {
                objWelfareIndicatorBO = new WelfareIndicatorBO();
                objWelfareIndicatorBO.Wlf_indicatorID = dr.GetInt32(dr.GetOrdinal("wlf_indicatorid"));
                objWelfareIndicatorBO.Wlf_indicatorname = dr.GetString(dr.GetOrdinal("wlf_indicatorname"));
                objWelfareIndicatorBO.Fieldtype = dr.GetString(dr.GetOrdinal("fieldtype"));
                if (!dr.IsDBNull(dr.GetOrdinal("ASSOCIATEDWITH"))) objWelfareIndicatorBO.AssociatedWith = dr.GetInt32(dr.GetOrdinal("ASSOCIATEDWITH"));
                objWelfareIndicatorBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                OBJWelfareIndicatorList.Add(objWelfareIndicatorBO);
            }

            dr.Close();

            return OBJWelfareIndicatorList;
        }

        /// <summary>
        /// To Get Welfare Indicator By Id
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <returns></returns>
        public WelfareIndicatorBO GetWelfareIndicatorById(int Wlf_indicatorID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETWELFAREINDICATORID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("WLF_INDICATORID_", Wlf_indicatorID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            WelfareIndicatorBO objWelfareIndicatorBO = null;
            WelfareIndicatorList OBJWelfareIndicatorList = new WelfareIndicatorList();

            objWelfareIndicatorBO = new WelfareIndicatorBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("wlf_indicatorid")))
                    objWelfareIndicatorBO.Wlf_indicatorID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("wlf_indicatorid")));
                if (!dr.IsDBNull(dr.GetOrdinal("wlf_indicatorname")))
                    objWelfareIndicatorBO.Wlf_indicatorname = Convert.ToString(dr.GetValue(dr.GetOrdinal("wlf_indicatorname")));
                if (!dr.IsDBNull(dr.GetOrdinal("fieldtype")))
                    objWelfareIndicatorBO.Fieldtype = Convert.ToString(dr.GetValue(dr.GetOrdinal("fieldtype")));
                if (!dr.IsDBNull(dr.GetOrdinal("ASSOCIATEDWITH"))) objWelfareIndicatorBO.AssociatedWith = dr.GetInt32(dr.GetOrdinal("ASSOCIATEDWITH"));
                if (!dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                    objWelfareIndicatorBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

            }
            dr.Close();
            return objWelfareIndicatorBO;
        }

        /// <summary>
        /// To Update Welfare Indicator
        /// </summary>
        /// <param name="objWelfareIndicatorBO"></param>
        /// <returns></returns>
        public string UpdateWelfareIndicator(WelfareIndicatorBO objWelfareIndicatorBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDWELFARE_INDICATOR", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("WLF_INDICATORID_", objWelfareIndicatorBO.Wlf_indicatorID);
                dcmd.Parameters.Add("WLF_INDICATORNAME_", objWelfareIndicatorBO.Wlf_indicatorname);
                dcmd.Parameters.Add("FIELDTYPE_", objWelfareIndicatorBO.Fieldtype);

                if (objWelfareIndicatorBO.AssociatedWith > 0)
                    dcmd.Parameters.Add("ASSOCIATEDWITH_", objWelfareIndicatorBO.AssociatedWith);
                else
                    dcmd.Parameters.Add("ASSOCIATEDWITH_", DBNull.Value);

                dcmd.Parameters.Add("UPDATEDBY_", objWelfareIndicatorBO.UserID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

                return result;
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
        /// To Delete Welfare Indicator
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <returns></returns>
        public string DeleteWelfareIndicator(int Wlf_indicatorID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELWELFARE_INDICATOR";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("WLF_INDICATORID_", Wlf_indicatorID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Welfare Indicator is already in use. Cannot delete.";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
              
                cnn.Close();
                cnn.Dispose();

            }

            return result;
        }

        /// <summary>
        /// To Obsolete Welfare Indicator
        /// </summary>
        /// <param name="Wlf_indicatorID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteWelfareIndicator(int Wlf_indicatorID, string IsDeleted)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSWELFARE_INDICATOR";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("WLF_INDICATORID_", Wlf_indicatorID);
                cmd.Parameters.Add("ISDELETED_", IsDeleted);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {              
                cnn.Close();
                cnn.Dispose();
            }

            return result;
        }
    }
}
