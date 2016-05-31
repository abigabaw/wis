using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class Compare_ProjectDAL
    {
       /// <summary>
       /// To fetch Project name from database
       /// </summary>
       /// <param name="PROJECTID"></param>
       /// <returns></returns>
       public Compare_projectList Getprojectname(string PROJECTID)
       {
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
           string proc = "USP_TRN_GET_PROJECTNAME";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           Compare_projectBO ObjPc = null;
           Compare_projectList ObjPcList = new Compare_projectList();

           while (dr.Read())
           {
               ObjPc = new Compare_projectBO();
               ObjPc.ProjectID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID"))));
               ObjPc.ProjectName = dr.GetValue(dr.GetOrdinal("PROJECTNAME")).ToString();


               ObjPcList.Add(ObjPc);
           }

           dr.Close();
           return ObjPcList;
       }

       /// <summary>
       /// To Get data from database
       /// </summary>
       /// <param name="Compare_projectBOObj"></param>
       /// <returns></returns>s

       public Compare_projectList Getdata(Compare_projectBO Compare_projectBOObj)
       {
           OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
           OracleCommand cmd;
          
           string proc = "USP_TRN_COMPARE_PROJECTS";
           cmd = new OracleCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("PROJECTIDS_", OracleDbType.Varchar2, 100).Value = Compare_projectBOObj.CompairID;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           Compare_projectBO objPRJBO = null;
           Compare_projectList objPRJList = new Compare_projectList();

           while (dr.Read())
           {
               objPRJBO = new Compare_projectBO();
               objPRJBO.ProjectID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID"))));
               objPRJBO.ProjectName = dr.GetValue(dr.GetOrdinal("projectcode")).ToString();
               objPRJBO.TotalestBudget = (Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TOTALESTBUDGET"))));
               objPRJBO.Option1 = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Option1"))));
               objPRJBO.Option2 = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Option2"))));
               objPRJBO.Option3 = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Option3"))));
               objPRJBO.Option4 = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Option4"))));
               objPRJBO.Option5 = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Option5"))));
               

               objPRJList.Add(objPRJBO);
           }

           dr.Close();

           return objPRJList;
       }

    }
}
