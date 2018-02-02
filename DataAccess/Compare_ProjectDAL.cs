using System;
using System.Data;
using System.Data.SqlClient;
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
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
           string proc = "USP_TRN_GET_PROJECTNAME";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           Compare_projectBO ObjPc = null;
           Compare_projectList ObjPcList = new Compare_projectList();

           while (dr.Read())
           {
               ObjPc = new Compare_projectBO();
               ObjPc.ProjectID = (int)dr.GetDecimal(dr.GetOrdinal("PROJECTID"));
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
           SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
           SqlCommand cmd;
          
           string proc = "USP_TRN_COMPARE_PROJECTS";
           cmd = new SqlCommand(proc, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("PROJECTIDS_", SqlDbType.NVarChar).Value = Compare_projectBOObj.CompairID;
           // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           Compare_projectBO objPRJBO = null;
           Compare_projectList objPRJList = new Compare_projectList();

           while (dr.Read())
           {
               objPRJBO = new Compare_projectBO();
               objPRJBO.ProjectID = (int)dr.GetDecimal(dr.GetOrdinal("PROJECTID"));
               objPRJBO.ProjectName = dr.GetValue(dr.GetOrdinal("projectcode")).ToString();
               objPRJBO.TotalestBudget = dr.GetDecimal(dr.GetOrdinal("TOTALESTBUDGET"));
               objPRJBO.Option1 = dr.GetInt32(dr.GetOrdinal("Option1"));
               objPRJBO.Option2 = dr.GetInt32(dr.GetOrdinal("Option2"));
               objPRJBO.Option3 = dr.GetInt32(dr.GetOrdinal("Option3"));
               objPRJBO.Option4 = dr.GetInt32(dr.GetOrdinal("Option4"));
               objPRJBO.Option5 = dr.GetInt32(dr.GetOrdinal("Option5"));
               

               objPRJList.Add(objPRJBO);
           }

           dr.Close();

           return objPRJList;
       }

    }
}
