using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public class MonthlyProjectExpenditureDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
      /// <summary>
        /// To Load Project Code
      /// </summary>
      /// <returns></returns>
        public List<MonthlyProjectExpenditureBO> LoadProjectCode()
        {
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            string proc = "USP_MST_GETPROJECTCODE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
