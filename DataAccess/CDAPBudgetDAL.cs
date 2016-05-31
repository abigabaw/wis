using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class CDAPBudgetDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To Add CDAP Budget into database
        /// </summary>
        /// <param name="objCDAPBudgetBO"></param>
        /// <returns></returns>
        public string AddCDAPBudget(CDAPBudgetBO objCDAPBudgetBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = "";
            proc = "USP_TRN_CDAP_BUDG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("@CDAP_BUDGID_", objCDAPBudgetBO.Cdap_budgid);
            cmd.Parameters.Add("@CDAP_CATEGORYID_", objCDAPBudgetBO.Cdap_categoryid);
            cmd.Parameters.Add("@CDAP_SUBCATEGORYID_", objCDAPBudgetBO.Cdap_subcategoryid);
            cmd.Parameters.Add("@UNIT_", objCDAPBudgetBO.Unit);
            cmd.Parameters.Add("@QUANTITY_", objCDAPBudgetBO.Quantity);
            cmd.Parameters.Add("@RATEPERUNIT_", objCDAPBudgetBO.Rateperunit);
            cmd.Parameters.Add("@UPDATEDBY_", objCDAPBudgetBO.UpdatedBy);
            cmd.Parameters.Add("ProjectID_", objCDAPBudgetBO.ProjectID);
            cmd.Parameters.Add("@errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            if (cmd.Parameters["@errorMessage_"].Value != null)
                returnResult = cmd.Parameters["@errorMessage_"].Value.ToString();
            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To Get CDAP Budget from database
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public CDAPBudgetList GetCDAPBudget(int ProjectID, string Status)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_TRN_CDAP_BUDG";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PROJECTID_", ProjectID);
            cmd.Parameters.Add("Status_", Status);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPBudgetBO objCDAPBudgetBO = null;
            CDAPBudgetList CDAPBudget = new CDAPBudgetList();
            while (dr.Read())
            {
                objCDAPBudgetBO = new CDAPBudgetBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_BUDGID"))) objCDAPBudgetBO.Cdap_budgid = dr.GetInt32(dr.GetOrdinal("CDAP_BUDGID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYNAME"))) objCDAPBudgetBO.Cdap_categoryname = dr.GetString(dr.GetOrdinal("CDAP_CATEGORYNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"))) objCDAPBudgetBO.Cdap_subcategoryname = dr.GetString(dr.GetOrdinal("CDAP_SUBCATEGORYNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("UNITNAME"))) objCDAPBudgetBO.UnitName = dr.GetString(dr.GetOrdinal("UNITNAME"));
                if (!dr.IsDBNull(dr.GetOrdinal("QUANTITY"))) objCDAPBudgetBO.Quantity = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("QUANTITY")));
                if (!dr.IsDBNull(dr.GetOrdinal("RATEPERUNIT"))) objCDAPBudgetBO.Rateperunit = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("RATEPERUNIT")));
                if (!dr.IsDBNull(dr.GetOrdinal("FUNDREQSTATUS"))) objCDAPBudgetBO.FundReqStatus = dr.GetString(dr.GetOrdinal("FUNDREQSTATUS"));
                CDAPBudget.Add(objCDAPBudgetBO);
            }
            dr.Close();
            return CDAPBudget;
        }
        /// <summary>
        /// To Get CDAP Budget Item from database
        /// </summary>
        /// <param name="cdap_budgid"></param>
        /// <returns></returns>
        public CDAPBudgetBO GetCDAPBudgetItem(int cdap_budgid)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_TRN_CDAP_BUDGBYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CDAP_BUDGID_", cdap_budgid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CDAPBudgetBO obCDAPBudgetBO = null;
            while (dr.Read())
            {
                obCDAPBudgetBO = new CDAPBudgetBO();
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_CATEGORYID"))) obCDAPBudgetBO.Cdap_categoryid = dr.GetInt32(dr.GetOrdinal("CDAP_CATEGORYID"));
                if (!dr.IsDBNull(dr.GetOrdinal("CDAP_SUBCATEGORYID"))) obCDAPBudgetBO.Cdap_subcategoryid = dr.GetInt32(dr.GetOrdinal("CDAP_SUBCATEGORYID"));
                if (!dr.IsDBNull(dr.GetOrdinal("UNIT"))) obCDAPBudgetBO.Unit = dr.GetInt32(dr.GetOrdinal("UNIT"));
                if (!dr.IsDBNull(dr.GetOrdinal("QUANTITY"))) obCDAPBudgetBO.Quantity = dr.GetInt32(dr.GetOrdinal("QUANTITY"));
                if (!dr.IsDBNull(dr.GetOrdinal("RATEPERUNIT"))) obCDAPBudgetBO.Rateperunit = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("RATEPERUNIT")));
            }
            dr.Close();
            return obCDAPBudgetBO;
        }
        /// <summary>
        /// To Delete CDAP Budget details
        /// </summary>
        /// <param name="cdap_budgid"></param>
        /// <returns></returns>
        public int DeleteCDAPBudget(int cdap_budgid)
        {
            int result = 0;
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_DEL_TRN_CDAP_BUDG", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@cdap_budgid_", cdap_budgid);
            myConnection.Open();
            result = myCommand.ExecuteNonQuery();
            myConnection.Close();
            return result;
        }
        /// <summary>
        /// To Send for Approval
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int SendforApproval(int projectID)
        {
            cnn = new OracleConnection(con);
            int returnResult;
            proc = "USP_TRN_UPD_CDAPBUDGETSTATUS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("PROJECTID_", projectID);
            returnResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return returnResult;
        }
    }
}
