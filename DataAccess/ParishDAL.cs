using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ParishDAL
    {
        string connStr = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;

        /// <summary>
        /// To Get Parish
        /// </summary>
        /// <param name="subcountyid"></param>
        /// <returns></returns>
        public Parish_List GetParish(string subcountyid)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_PARISH";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("SUBCOUNTYID_", subcountyid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ParishBO ParishBOobj = null;
            Parish_List ParishListobj = new Parish_List();

            while (dr.Read())
            {
                ParishBOobj = new ParishBO();
                ParishBOobj.ParishId = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PARISHID"))));
                ParishBOobj.ParishName = dr.GetValue(dr.GetOrdinal("PARISHNAME")).ToString();
                ParishListobj.Add(ParishBOobj);
            }

            dr.Close();
            return ParishListobj;
        }

        /// <summary>
        /// To Get All Parish 
        /// </summary>
        /// <param name="subcountyid"></param>
        /// <param name="countyid"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public Parish_List GetAllParish(int subcountyid,int countyid,int districtid)
        {
            Parish_List Parish_Listobj = null;

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_GET_PARISH_ALL", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("SUBCOUNTYID_", subcountyid);
                    cmd.Parameters.Add("countyid_", countyid);
                    cmd.Parameters.Add("districtid_", districtid);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    ParishBO ParishBOobj = null;
                    Parish_Listobj = new Parish_List();

                    while (dr.Read())
                    {
                        ParishBOobj = new ParishBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("PARISHID"))) ParishBOobj.ParishId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PARISHID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("PARISHNAME"))) ParishBOobj.ParishName = dr.GetValue(dr.GetOrdinal("PARISHNAME")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) ParishBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                        if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) ParishBOobj.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
                        if (!dr.IsDBNull(dr.GetOrdinal("countyname"))) ParishBOobj.countyName = dr.GetString(dr.GetOrdinal("countyname"));
                        if (!dr.IsDBNull(dr.GetOrdinal("subcountyname"))) ParishBOobj.subcountyName = dr.GetString(dr.GetOrdinal("subcountyname"));
                        // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objCountyBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                        Parish_Listobj.Add(ParishBOobj);
                    }

                    dr.Close();
                }
            }

            return Parish_Listobj;
        }

        /// <summary>
        /// To Add Parish
        /// </summary>
        /// <param name="ParishBOobj"></param>
        /// <returns></returns>
        public string AddParish(ParishBO ParishBOobj)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_INS_PARISH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("SUBCOUNTYID_", ParishBOobj.SubcountyID);
                    cmd.Parameters.Add("PARISHNAME_", ParishBOobj.ParishName);
                    cmd.Parameters.Add("CREATEDBY_", ParishBOobj.CreatedBy);
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
        /// To Update Parish
        /// </summary>
        /// <param name="ParishBOobj"></param>
        /// <returns></returns>
        public string UpdateParish(ParishBO ParishBOobj)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_UPD_PARISH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("PARISHID_", ParishBOobj.ParishId);
                    cmd.Parameters.Add("SUBCOUNTYID_", ParishBOobj.SubcountyID);
                    cmd.Parameters.Add("PARISHNAME_", ParishBOobj.ParishName);
                    cmd.Parameters.Add("UPDATEDBY_", ParishBOobj.UpdatedBy);
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
        /// To Delete Parish
        /// </summary>
        /// <param name="ParishId"></param>
        /// <returns></returns>
        public string DeleteParish(int ParishId)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_DEL_PARISH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("PARISHID_", ParishId);
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
        /// Obsolete Parish
        /// </summary>
        /// <param name="ParishId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteParish(int ParishId, string isDeleted, int updatedBy)
        {
            string result = "";

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_OBS_PARISH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.Add("PARISHID_", ParishId);
                    cmd.Parameters.Add("ISDELETED_", isDeleted);
                    cmd.Parameters.Add("UPDATEDBY_", updatedBy);

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
        /// To Get Parish By Id
        /// </summary>
        /// <param name="ParishId"></param>
        /// <returns></returns>
        public ParishBO GetParishById(int ParishId)
        {
            proc = "USP_MST_GET_PARISHBYID";

            cnn = new OracleConnection(connStr);
            ParishBO ParishBOobj = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PARISHID_", ParishId);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    ParishBOobj = new ParishBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PARISHID"))) ParishBOobj.ParishId = dr.GetInt32(dr.GetOrdinal("PARISHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("subcountyid"))) ParishBOobj.SubcountyID = dr.GetInt32(dr.GetOrdinal("subcountyid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("countyid"))) ParishBOobj.CountyID = dr.GetInt32(dr.GetOrdinal("countyid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) ParishBOobj.DistrictID = dr.GetInt32(dr.GetOrdinal("districtid"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PARISHNAME"))) ParishBOobj.ParishName = dr.GetString(dr.GetOrdinal("PARISHNAME"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) CountyBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SUBCOUNTYNAME"))) ParishBOobj.subcountyName = dr.GetString(dr.GetOrdinal("SUBCOUNTYNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COUNTYNAME"))) ParishBOobj.countyName = dr.GetString(dr.GetOrdinal("COUNTYNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISTRICTNAME"))) ParishBOobj.DistrictName = dr.GetString(dr.GetOrdinal("DISTRICTNAME"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ParishBOobj;
        }

        /// <summary>
        /// To Search Parish
        /// </summary>
        /// <param name="SearchParish"></param>
        /// <returns></returns>
        public Parish_List SearchParish(string SearchParish)
        {
            Parish_List Parish_Listobj = null;

            using (cnn = new OracleConnection(connStr))
            {
                using (cmd = new OracleCommand("USP_MST_SER_COMBPARISH", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("SearchParish_", SearchParish);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Connection.Open();

                    OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    ParishBO ParishBOobj = null;
                    Parish_Listobj = new Parish_List();

                    while (dr.Read())
                    {
                        ParishBOobj = new ParishBO();
                        if (!dr.IsDBNull(dr.GetOrdinal("PARISHID"))) ParishBOobj.ParishId = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PARISHID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("PARISHNAME"))) ParishBOobj.ParishName = dr.GetValue(dr.GetOrdinal("PARISHNAME")).ToString();
                        if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) ParishBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                        if (!dr.IsDBNull(dr.GetOrdinal("districtname"))) ParishBOobj.DistrictName = dr.GetString(dr.GetOrdinal("districtname"));
                        if (!dr.IsDBNull(dr.GetOrdinal("countyname"))) ParishBOobj.countyName = dr.GetString(dr.GetOrdinal("countyname"));
                        if (!dr.IsDBNull(dr.GetOrdinal("subcountyname"))) ParishBOobj.subcountyName = dr.GetString(dr.GetOrdinal("subcountyname"));
                        // if (!dr.IsDBNull(dr.GetOrdinal("districtid"))) objCountyBO.DistrictID =Convert.ToInt32( dr.GetString(dr.GetOrdinal("districtid")));
                        Parish_Listobj.Add(ParishBOobj);
                    }

                    dr.Close();
                }
            }

            return Parish_Listobj;
        }
    }
}
