using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data;

namespace WIS_DataAccess
{
    public class OptionGroupDAL
    {
        /// <summary>
        /// To Insert Option Groups
        /// </summary>
        /// <param name="objOptionGroupBO"></param>
        /// <returns></returns>
        public string InsertOptionGroups(OptionGroupBO objOptionGroupBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTOPTION_GROUPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("OPTIONGROUP_", objOptionGroupBO.OptionGroupName);
                dcmd.Parameters.Add("CREATEDBY_", objOptionGroupBO.UserID);
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
        /// To Check Weather Column Exists or not
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
        /// To Get Option Group
        /// </summary>
        /// <returns></returns>
        public OptionGroupList GetOptionGroup()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETOPTION_GROUPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionGroupBO objOptionGroupBO = null;
            OptionGroupList OptionGroup = new OptionGroupList();

            while (dr.Read())
            {
                objOptionGroupBO = new OptionGroupBO();
                objOptionGroupBO.OptionGroupID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("optiongroupid")));
                objOptionGroupBO.OptionGroupName = dr.GetString(dr.GetOrdinal("optiongroup"));
                objOptionGroupBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

                OptionGroup.Add(objOptionGroupBO);
            }

            dr.Close();

            return OptionGroup;
        }

        /// <summary>
        /// To Update Option Groups
        /// </summary>
        /// <param name="objOptionGroupBO"></param>
        /// <returns></returns>
        public string UpdateOptionGroups(OptionGroupBO objOptionGroupBO)
        {
            string result = "";
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATEOPTION_GROUPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("OPTIONGROUPID_", objOptionGroupBO.OptionGroupID);
                dcmd.Parameters.Add("OPTIONGROUP_", objOptionGroupBO.OptionGroupName);
                dcmd.Parameters.Add("UPDATEDBY_", objOptionGroupBO.UserID);
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
        /// To Get Option Group By Id
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <returns></returns>
        public OptionGroupBO GetOptionGroupById(int OptionGroupID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETOPTION_GROUPSBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("OPTIONGROUPID_", OptionGroupID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionGroupBO objOptionGroupBO = null;
            OptionGroupList OptionGroup = new OptionGroupList();

            objOptionGroupBO = new OptionGroupBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "optiongroupid") && !dr.IsDBNull(dr.GetOrdinal("optiongroupid")))
                    objOptionGroupBO.OptionGroupID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("optiongroupid")));
                if (ColumnExists(dr, "optiongroup") && !dr.IsDBNull(dr.GetOrdinal("optiongroup")))
                    objOptionGroupBO.OptionGroupName = Convert.ToString(dr.GetValue(dr.GetOrdinal("optiongroup")));
                if (ColumnExists(dr, "isdeleted") && !dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                    objOptionGroupBO.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));

            }
            dr.Close();
            return objOptionGroupBO;
        }

        /// <summary>
        /// To Delete Option Group
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <returns></returns>
        public string DeleteOptionGroup(int OptionGroupID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_DELETEOPTION_GROUPS";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OPTIONGROUPID_", OptionGroupID);
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
                    result = "Selected Groups is already in use. Connot delete";
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// To Obsolete Option Group
        /// </summary>
        /// <param name="OptionGroupID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteOptionGroup(int OptionGroupID, string IsDeleted)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = string.Empty;

            try
            {
                string proc = "USP_MST_OBSOLETEOPTION_GROUPS";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("OPTIONGROUPID_", OptionGroupID);
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

            return result;
        }

    }
}
