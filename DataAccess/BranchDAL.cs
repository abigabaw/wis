using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public  class BranchDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
       /// <summary>
        /// To Get Active Branches from database
       /// </summary>
       /// <param name="bankID"></param>
       /// <returns></returns>
        public BankBranchList GetActiveBranches(int bankID)
        {
             OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
                OracleCommand cmd;

                string proc = "USP_MST_GET_BRANCHES";

                cmd = new OracleCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("bankID_", Convert.ToInt32(bankID));
                
                cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                BranchBO objBranchBO = null;
                BankBranchList objBranchList = new BankBranchList();
                while (dr.Read())
                {
                     objBranchBO = new BranchBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objBranchBO.BankBranchId = dr.GetInt32(dr.GetOrdinal("BRANCHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHNAME"))) objBranchBO.BranchName = dr.GetString(dr.GetOrdinal("BRANCHNAME"));

                     objBranchList.Add(objBranchBO);
                }

                dr.Close();
                return objBranchList;
            }
       /// <summary>
       /// To get all branches from database
       /// </summary>
       /// <param name="bankID"></param>
       /// <returns></returns>
        public BankBranchList GetAllBranches(int bankID)
        {
            proc = "USP_MST_GET_ALLBRANCHES";
            BranchBO objBranchBO = null;
            BankBranchList objBranchList = new BankBranchList();

            using (cnn = new OracleConnection(con))
            {
                using (cmd = new OracleCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("bankID_", bankID);
                    cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.Connection.Open();
                        OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            objBranchBO = new BranchBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objBranchBO.BankID = dr.GetInt32(dr.GetOrdinal("bankID"));                           
                            if (!dr.IsDBNull(dr.GetOrdinal("CITY"))) objBranchBO.City = dr.GetString(dr.GetOrdinal("City"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BRANCHNAME"))) objBranchBO.BranchName = dr.GetString(dr.GetOrdinal("branchName"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SWIFTCODE"))) objBranchBO.SwiftCode = dr.GetString(dr.GetOrdinal("swiftCode"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BANKCODE"))) objBranchBO.BANKCODE = dr.GetString(dr.GetOrdinal("BANKCODE"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objBranchBO.IsDeleted = dr.GetString(dr.GetOrdinal("isDeleted"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BranchID"))) objBranchBO.BankBranchId = dr.GetInt32(dr.GetOrdinal("BranchID"));  
                            objBranchList.Add(objBranchBO);
                        }

                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return objBranchList;
        }
       /// <summary>
       /// To addd branch to database
       /// </summary>
       /// <param name="objBranchBO"></param>
       /// <returns></returns>

        public string AddBranch(BranchBO objBranchBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_BANKBRANCHES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("bankID_", objBranchBO.BankID);
            cmd.Parameters.Add("branchName_", objBranchBO.BranchName);
            cmd.Parameters.Add("city_", objBranchBO.City);
            cmd.Parameters.Add("swiftCode_", objBranchBO.SwiftCode);
            cmd.Parameters.Add("BANKCODE_", objBranchBO.BANKCODE);
            cmd.Parameters.Add("createdby_", objBranchBO.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
       /// <summary>
       /// To get branch by ID
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <returns></returns>
        public BranchBO GetBranchByID(int BankBranchId)
        {
            proc = "USP_MST_GET_BRANCHESBYID";
            cnn = new OracleConnection(con);
            BranchBO objBranchBO = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("BankBranchId_", BankBranchId);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objBranchBO = new BranchBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objBranchBO.BankBranchId = dr.GetInt32(dr.GetOrdinal("BRANCHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objBranchBO.BankID = dr.GetInt32(dr.GetOrdinal("BANKID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CITY"))) objBranchBO.City = dr.GetString(dr.GetOrdinal("CITY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHNAME"))) objBranchBO.BranchName = dr.GetString(dr.GetOrdinal("BRANCHNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SWIFTCODE"))) objBranchBO.SwiftCode = dr.GetString(dr.GetOrdinal("SWIFTCODE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKCODE"))) objBranchBO.BANKCODE = dr.GetString(dr.GetOrdinal("BANKCODE"));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objBranchBO;
        }
       /// <summary>
       /// To update branch details to database
       /// </summary>
       /// <param name="objBranchBO"></param>
       /// <returns></returns>
        public string UpdateBranch(BranchBO objBranchBO)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;

            proc = "USP_MST_UPD_BANKBRANCHES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("bankBranchID_", objBranchBO.BankBranchId);
            cmd.Parameters.Add("bankID_", objBranchBO.BankID);
            cmd.Parameters.Add("branchName_", objBranchBO.BranchName);
            cmd.Parameters.Add("city_", objBranchBO.City);

            cmd.Parameters.Add("swiftCode_", objBranchBO.SwiftCode);
            cmd.Parameters.Add("BANKCODE_", objBranchBO.BANKCODE);
            cmd.Parameters.Add("updatedby_", objBranchBO.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
       /// <summary>
       /// To delete branch
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <returns></returns>
        public string DeleteBranch(int BankBranchId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_BANKBRANCHES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BankBranchId_", BankBranchId);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-02292"))
                {
                    result = "Selected Bank is already in use. Cannot delete";
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                myCommand.Dispose();
                myConnection.Close();
                myConnection.Dispose();
            }

            return result;
        }
       /// <summary>
       /// To make branch details obsolete
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteBranch(int BankBranchId, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETEBANKBRANCHES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BankBranchId_", BankBranchId);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
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
