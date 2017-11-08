using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public class MonthlyProjectExpenditureDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
      /// <summary>
        /// To Load Project Code
      /// </summary>
      /// <returns></returns>
        public List<MonthlyProjectExpenditureBO> LoadProjectCode()
        {
            cnn = new SqlConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GETPROJECTCODE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<MonthlyProjectExpenditureBO> ProjectCodeList = new List<MonthlyProjectExpenditureBO>();
            MonthlyProjectExpenditureBO MonthlyProjectExpenditureBOobj=null;
            while(dr.Read())
            {
                MonthlyProjectExpenditureBOobj =new MonthlyProjectExpenditureBO();
                MonthlyProjectExpenditureBOobj.ProjectCodeID = dr.GetInt32(dr.GetOrdinal("PROJECTID"));
                MonthlyProjectExpenditureBOobj.ProjectCode = dr.GetString(dr.GetOrdinal("PROJECTCODE"));
                ProjectCodeList.Add(MonthlyProjectExpenditureBOobj);
            }
            dr.Close();
            return ProjectCodeList;
        }
        
    }
}
