using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ExpenseDAL
    {

        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// Excel Data Import into Grid
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="projectID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string FilePath, string Extension, int projectID, int createdBy)
        {

            //string result = "";

            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0";
            conStr = String.Format(conStr, FilePath, 1);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            return dt;

        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public object GetExpenseDataForACC(int ProjectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PROJ_EXPENSEACC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PROJECTID_", ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            ExpenseBO ExpenseBOobj = null;
            ExpenseList ExpenseListobj = new ExpenseList();
            ExpenseBOobj = new ExpenseBO();

            while (dr.Read())
            {
                ExpenseBOobj = new ExpenseBO();
                //if (!dr.IsDBNull(dr.GetOrdinal("PROJECTEXPENSEID"))) ExpenseBOobj.PROJECTEXPENSEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTEXPENSEID")));
                //if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) ExpenseBOobj.PROJECTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

                if (!dr.IsDBNull(dr.GetOrdinal("EXPENSETYPE"))) ExpenseBOobj.EXPENSETYPE = dr.GetString(dr.GetOrdinal("EXPENSETYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ACCOUNTCODE"))) ExpenseBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("ACCOUNTCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("EXPENSEAMOUNT"))) ExpenseBOobj.EXPENSEAMOUNT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("EXPENSEAMOUNT")));

                if (!dr.IsDBNull(dr.GetOrdinal("DATEOFEXPENSE"))) ExpenseBOobj.DATEOFEXPENSE = dr.GetDateTime(dr.GetOrdinal("DATEOFEXPENSE"));
                //ExpenseBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                ExpenseListobj.Add(ExpenseBOobj);
            }
            dr.Close();
            return ExpenseListobj;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public object GetAllExpenseData(int ProjectID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PROJ_EXPENSE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PROJECTID_", ProjectID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            ExpenseBO ExpenseBOobj = null;
            ExpenseList ExpenseListobj = new ExpenseList();
            ExpenseBOobj = new ExpenseBO();

            while (dr.Read())
            {
                ExpenseBOobj = new ExpenseBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTEXPENSEID"))) ExpenseBOobj.PROJECTEXPENSEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTEXPENSEID")));
                if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) ExpenseBOobj.PROJECTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

                if (!dr.IsDBNull(dr.GetOrdinal("EXPENSETYPE"))) ExpenseBOobj.EXPENSETYPE = dr.GetString(dr.GetOrdinal("EXPENSETYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("ACCOUNTCODE"))) ExpenseBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("ACCOUNTCODE"));
                if (!dr.IsDBNull(dr.GetOrdinal("EXPENSEAMOUNT"))) ExpenseBOobj.EXPENSEAMOUNT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("EXPENSEAMOUNT")));

                if (!dr.IsDBNull(dr.GetOrdinal("DATEOFEXPENSE"))) ExpenseBOobj.DATEOFEXPENSE = dr.GetDateTime(dr.GetOrdinal("DATEOFEXPENSE"));
                ExpenseBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                ExpenseListobj.Add(ExpenseBOobj);
            }
            dr.Close();
            return ExpenseListobj;
        }
        /// <summary>
        /// to insert details to datbase
        /// </summary>
        /// <param name="dtExpen"></param>
        /// <param name="ProjectID"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public DataTable savedata(DataTable dtExpen, int ProjectID, string uID)
        {
            ExpenseBO objExpenseBO = null;
            SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myConnection.Open();

            SqlCommand myCommand;
            SqlCommand cmd;

            myConnection = new SqlConnection(AppConfiguration.ConnectionString);


            //cmd = new SqlCommand("USP_TRN_DEL_PROJ_EXPENSE", myConnection);
            //cmd.Connection = myConnection;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("PROJECTID_", ProjectID);
            //myConnection.Open();
            //cmd.ExecuteNonQuery();
            //myConnection.Close();

            myCommand = new SqlCommand("USP_TRN_INS_PROJ_EXPENSE", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.AddWithValue("PROJECTID_", "");
            myCommand.Parameters.AddWithValue("EXPENSETYPE_", "");
            myCommand.Parameters.AddWithValue("ACCOUNTCODE_", "");
            myCommand.Parameters.AddWithValue("EXPENSEAMOUNT_", "");
            myCommand.Parameters.AddWithValue("DATEOFEXPENSE_", "");
            myCommand.Parameters.AddWithValue("CREATEDBY_", "");

            myConnection.Open();

            foreach (DataRow dr in dtExpen.Rows)
            {
                objExpenseBO = new ExpenseBO();

                objExpenseBO.PROJECTID = ProjectID;
                objExpenseBO.EXPENSETYPE = Convert.ToString(dr["EXPENSETYPE"]);
                objExpenseBO.ACCOUNTCODE = (dr["ACCOUNTCODE"].ToString());

                if (dr["EXPENSEAMOUNT"].ToString().Trim() != "")
                    objExpenseBO.EXPENSEAMOUNT = Convert.ToDecimal((dr["EXPENSEAMOUNT"]));
                else
                    objExpenseBO.EXPENSEAMOUNT = 0;

                objExpenseBO.DATEOFEXPENSE = Convert.ToDateTime((dr["DATEOFEXPENSE"]));
                objExpenseBO.CREATEDBY = Convert.ToInt32(uID);

                myCommand.Parameters["PROJECTID_"].Value = objExpenseBO.PROJECTID;
                myCommand.Parameters["EXPENSETYPE_"].Value = objExpenseBO.EXPENSETYPE;
                myCommand.Parameters["ACCOUNTCODE_"].Value = objExpenseBO.ACCOUNTCODE;
                myCommand.Parameters["EXPENSEAMOUNT_"].Value = objExpenseBO.EXPENSEAMOUNT;
                myCommand.Parameters["DATEOFEXPENSE_"].Value = objExpenseBO.DATEOFEXPENSE.ToString(UtilBO.DateFormatDBFull);
                myCommand.Parameters["CREATEDBY_"].Value = objExpenseBO.CREATEDBY;

                myCommand.ExecuteNonQuery();
            }

            myConnection.Close();

            return dtExpen;
        }

        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns></returns>

        public string AddExpense(ExpenseBO objExpense)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_TRN_INS_PROJ_EXPENSE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("PROJECTID_", objExpense.PROJECTID);
            cmd.Parameters.AddWithValue("EXPENSETYPE_", objExpense.EXPENSETYPE);
            cmd.Parameters.AddWithValue("ACCOUNTCODE_", objExpense.ACCOUNTCODE);
            cmd.Parameters.AddWithValue("EXPENSEAMOUNT_", objExpense.EXPENSEAMOUNT);
            cmd.Parameters.AddWithValue("DATEOFEXPENSE_", objExpense.DATEOFEXPENSE.ToString(UtilBO.DateFormatDBFull));
            cmd.Parameters.AddWithValue("CREATEDBY_", objExpense.UPDATEDBY);
            ///* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            //if (cmd.Parameters["errorMessage_"].Value != null)
            //    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            //else
            //    returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To fetch details by ID
        /// </summary>
        /// <param name="EXPENSEID"></param>
        /// <returns></returns>
        public ExpenseBO GetExpenseByID(int EXPENSEID)
        {
            proc = "USP_TRN_GET_PROJ_EXPENSEBYID";
            cnn = new SqlConnection(con);
            ExpenseBO ExpenseBOobj = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("EXPENSEID_", EXPENSEID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ExpenseBOobj = new ExpenseBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTEXPENSEID"))) ExpenseBOobj.PROJECTEXPENSEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTEXPENSEID")));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTID"))) ExpenseBOobj.PROJECTID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PROJECTID")));

                    if (!dr.IsDBNull(dr.GetOrdinal("EXPENSETYPE"))) ExpenseBOobj.EXPENSETYPE = dr.GetString(dr.GetOrdinal("EXPENSETYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ACCOUNTCODE"))) ExpenseBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("ACCOUNTCODE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EXPENSEAMOUNT"))) ExpenseBOobj.EXPENSEAMOUNT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("EXPENSEAMOUNT")));

                    if (!dr.IsDBNull(dr.GetOrdinal("DATEOFEXPENSE"))) ExpenseBOobj.DATEOFEXPENSE = dr.GetDateTime(dr.GetOrdinal("DATEOFEXPENSE"));
                    ExpenseBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ExpenseBOobj;
        }
        /// <summary>
        ///  To update details to database
        /// </summary>
        /// <param name="objExpense"></param>
        /// <returns></returns>
        public string UpdateBank(ExpenseBO objExpense)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;

            proc = "USP_TRN_UPD_PROJ_EXPENSE";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("EXPENSEID_", objExpense.PROJECTEXPENSEID);
            cmd.Parameters.AddWithValue("PROJECTID_", objExpense.PROJECTID);
            cmd.Parameters.AddWithValue("EXPENSETYPE_", objExpense.EXPENSETYPE);
            cmd.Parameters.AddWithValue("ACCOUNTCODE_", objExpense.ACCOUNTCODE);
            cmd.Parameters.AddWithValue("EXPENSEAMOUNT_", objExpense.EXPENSEAMOUNT);
            cmd.Parameters.AddWithValue("DATEOFEXPENSE_", objExpense.DATEOFEXPENSE.ToString(UtilBO.DateFormatDBFull));
            cmd.Parameters.AddWithValue("UPDATEDBY_", objExpense.UPDATEDBY);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        /// <summary>
        /// To delete details to database
        /// </summary>
        /// <param name="EXPENSEID"></param>
        /// <returns></returns>
        public string DeleteExpense(int EXPENSEID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_TRN_DEL_PROJ_EXPENSE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("EXPENSEID_", EXPENSEID);
                ///* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //if (myCommand.Parameters["errorMessage_"].Value != null)
                //    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }

    }
}
