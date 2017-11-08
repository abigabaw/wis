using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   
    public class DescriptionDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        public string AddDescription(DescriptionBO objDescription)
        {
            string result = "";

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_MST_INS_DESCRIPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    //cmd.Parameters.AddWithValue("DISTRICTID_", objSubCountyBO.DistrictID);
                    cmd.Parameters.AddWithValue("PARAMETERID_", objDescription.ParameterID);
                    cmd.Parameters.AddWithValue("OPTIONAVAILABLEID_", objDescription.OptionAvailID);
                    cmd.Parameters.AddWithValue("DESCRIPTION_", objDescription.Description);
                    cmd.Parameters.AddWithValue("CREATEDBY_", objDescription.CreatedBy);
                    cmd.Parameters.AddWithValue("ERRORMESSAGE_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_GET_PARAMETERS";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("OPTIONAVAILABLEID_", Pid);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_GET_OPTIONAVAIL";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_OPTION_PARAM_DATA_GRID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_GET_OPTION_PARAM_BYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("DescriptionID_", id);
                    // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                   
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_UPD_PARAM_OPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DESCRIPTIONID_", objDescription.DescriptionID);
                    cmd.Parameters.AddWithValue("PARAMETERID_", objDescription.ParameterID);
                    cmd.Parameters.AddWithValue("OPTIONAVAILABLEID_", objDescription.OptionAvailID);
                    cmd.Parameters.AddWithValue("DESCRIPTION_", objDescription.Description);
                    cmd.Parameters.AddWithValue("CREATEDBY_", objDescription.CreatedBy);
                    cmd.Parameters.AddWithValue("ERRORMESSAGE_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_DEL_PARAM_OPTIONBYID", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DescriptionID_", id);
                    cmd.Parameters.AddWithValue("ERRORMESSAGE_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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

            using (cnn = new SqlConnection(connStr))
            {
                using (cmd = new SqlCommand("USP_OBSLTE_DESCRIPTION", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("DESCRIPTIONID_", DesID);
                    cmd.Parameters.AddWithValue("ISDELETED_", ISDELETED);
                    cmd.Parameters.AddWithValue("UPDATEDBY_", UPDATEDBY);

                    cmd.Parameters.AddWithValue("ERRORMESSAGE_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
