using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace WIS_DataAccess
{
    public class ActivityDAL
    {
        /// <summary>
        /// To Get Activity Details
        /// </summary>
        /// <returns></returns>
        public ActivityList GetActivity()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_GET_MST_CDAP_ACTIVITY";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ActivityBO objActivityBO = null;
            ActivityList Activity = new ActivityList();
            while (dr.Read())
            {
                objActivityBO = new ActivityBO();
                objActivityBO.Cdap_activityid = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id"))));
                objActivityBO.Cdap_activityname = dr.GetValue(dr.GetOrdinal("Name")).ToString();
                Activity.Add(objActivityBO);
            }
            dr.Close();
            return Activity;
        }
    }
}
