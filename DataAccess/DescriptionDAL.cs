using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   
    public class DescriptionDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        public string AddDescription(DescriptionBO objDescription)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_INS_DESCRIPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    //cmd.Parameters.Add("DISTRICTID_", objSubCountyBO.DistrictID);
                    cmd.Parameters.Add("PARAMETERID_", objDescription.ParameterID);
                    cmd.Parameters.Add("OPTIONAVAILABLEID_", objDescription.OptionAvailID);
                    cmd.Parameters.Add("DESCRIPTION_", objDescription.Description);
                    cmd.Parameters.Add("CREATEDBY_", objDescription.CreatedBy);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        public DescriptionList GetParameters(int Pid)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_PARAMETERS";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("OPTIONAVAILABLEID_", Pid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DescriptionBO DescriptionBO = null;
            DescriptionList objDescriptionList = new DescriptionList();

            while (dr.Read())
            {
                DescriptionBO = new DescriptionBO();
                DescriptionBO.ParameterID = dr.GetInt32(dr.GetOrdinal("PARAMETERID"));
                DescriptionBO.ParameterName = dr.GetValue(dr.GetOrdinal("PARAMETERNAME")).ToString();
                objDescriptionList.Add(DescriptionBO);
            }

            dr.Close();
            return objDescriptionList;
        }
        public DescriptionList GetOptionAvail()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_OPTIONAVAIL";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DescriptionBO DescriptionBO = null;
            DescriptionList objDescriptionList = new DescriptionList();

            while (dr.Read())
            {
                DescriptionBO = new DescriptionBO();
                DescriptionBO.OptionID = dr.GetInt32(dr.GetOrdinal("ID"));
                DescriptionBO.OptionAvailablename = dr.GetValue(dr.GetOrdinal("OPTIONAVAILABLE")).ToString();
                objDescriptionList.Add(DescriptionBO);
            }

            dr.Close();
            return objDescriptionList;
        }
        public DescriptionList GetAllDescriptionDetails()
        {
            DescriptionList objDescList = null;

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_OPTION_PARAM_DATA_GRID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DescriptionBO objDescriptionBO = null;
                    objDescList = new DescriptionList();

                    while (dr.Read())
                    {
                        objDescriptionBO = new DescriptionBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("PARAMETERNAME"))) objDescriptionBO.ParameterName = dr.GetValue(dr.GetOrdinal("PARAMETERNAME")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("OPTIONAVAILABLE"))) objDescriptionBO.OptionAvailablename = dr.GetValue(dr.GetOrdinal("OPTIONAVAILABLE")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTIONID"))) objDescriptionBO.DescriptionID =(dr.GetInt32(dr.GetOrdinal("DESCRIPTIONID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objDescriptionBO.Description = dr.GetValue(dr.GetOrdinal("DESCRIPTION")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objDescriptionBO.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                        objDescList.Add(objDescriptionBO);
                    }

                    dr.Close();
                }
            }

            return objDescList;
        }
        public DescriptionBO GetAllDescriptionDetailsByID(int id)
        {
           DescriptionBO objDescriptionBO = null;

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_GET_OPTION_PARAM_BYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("DescriptionID_", id);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                   
                   // objDescList = new DescriptionList();

                    while (dr.Read())
                    {
                        objDescriptionBO = new DescriptionBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("PARAMETERID"))) objDescriptionBO.ParameterID = dr.GetInt32(dr.GetOrdinal("PARAMETERID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OPTIONAVAILABLEID"))) objDescriptionBO.OptionAvailID = dr.GetInt32(dr.GetOrdinal("OPTIONAVAILABLEID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objDescriptionBO.Description = (dr.GetString(dr.GetOrdinal("DESCRIPTION")));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION"))) objDescriptionBO.Description = dr.GetValue(dr.GetOrdinal("DESCRIPTION")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objDescriptionBO.Isdeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                       
                    }

                    dr.Close();
                }
            }

            return objDescriptionBO;
 
        }
        public string UpdateDesription(DescriptionBO objDescription)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_UPD_PARAM_OPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DESCRIPTIONID_", objDescription.DescriptionID);
                    cmd.Parameters.Add("PARAMETERID_", objDescription.ParameterID);
                    cmd.Parameters.Add("OPTIONAVAILABLEID_", objDescription.OptionAvailID);
                    cmd.Parameters.Add("DESCRIPTION_", objDescription.Description);
                    cmd.Parameters.Add("CREATEDBY_", objDescription.CreatedBy);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        public string DeleteDescription(int id)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_DEL_PARAM_OPTIONBYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DescriptionID_", id);
                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
        public string ObsoleteDescription(int DesID, string ISDELETED, int UPDATEDBY)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_OBSLTE_DESCRIPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("DESCRIPTIONID_", DesID);
                    cmd.Parameters.Add("ISDELETED_", ISDELETED);
                    cmd.Parameters.Add("UPDATEDBY_", UPDATEDBY);

                    cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                        result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                    cmd.Connection.Close();
                }
            }

            return result;
        }
    }
}
