using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public  class BranchDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
       /// <summary>
        /// To Get Active Branches from database
       /// </summary>
       /// <param name="bankID"></param>
       /// <returns></returns>
        public BankBranchList GetActiveBranches(int bankID)
        {
             SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
                SqlCommand cmd;

                string proc = "USP_MST_GET_BRANCHES";

                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("bankID_", Convert.ToInt32(bankID));
                

                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                BranchBO objBranchBO = null;
                BankBranchList objBranchList = new BankBranchList();
                while (dr.Read())
                {
                     objBranchBO = new BranchBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objBranchBO.BankBranchId = (int)dr.GetDecimal(dr.GetOrdinal("BRANCHID"));
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

            using (cnn = new SqlConnection(con))
            {
                using (cmd = new SqlCommand(proc, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("bankID_", bankID);

                    try
                    {
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dr.Read())
                        {
                            objBranchBO = new BranchBO();

                            if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objBranchBO.BankID = (int)dr.GetDecimal(dr.GetOrdinal("bankID"));                           
                            if (!dr.IsDBNull(dr.GetOrdinal("CITY"))) objBranchBO.City = dr.GetString(dr.GetOrdinal("City"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BRANCHNAME"))) objBranchBO.BranchName = dr.GetString(dr.GetOrdinal("branchName"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SWIFTCODE"))) objBranchBO.SwiftCode = dr.GetString(dr.GetOrdinal("swiftCode"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BANKCODE"))) objBranchBO.BANKCODE = dr.GetString(dr.GetOrdinal("BANKCODE"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) objBranchBO.IsDeleted = dr.GetString(dr.GetOrdinal("isDeleted"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BranchID"))) objBranchBO.BankBranchId = (int)dr.GetDecimal(dr.GetOrdinal("BranchID"));  
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
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_BANKBRANCHES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("bankID_", objBranchBO.BankID);
            cmd.Parameters.AddWithValue("branchName_", objBranchBO.BranchName);
            cmd.Parameters.AddWithValue("city_", objBranchBO.City);
            cmd.Parameters.AddWithValue("swiftCode_", objBranchBO.SwiftCode);
            cmd.Parameters.AddWithValue("BANKCODE_", objBranchBO.BANKCODE);
            cmd.Parameters.AddWithValue("createdby_", objBranchBO.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            cnn = new SqlConnection(con);
            BranchBO objBranchBO = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("BankBranchId_", BankBranchId);


            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objBranchBO = new BranchBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("BRANCHID"))) objBranchBO.BankBranchId = (int)dr.GetDecimal(dr.GetOrdinal("BRANCHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BANKID"))) objBranchBO.BankID = (int)dr.GetDecimal(dr.GetOrdinal("BANKID"));
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
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;

            proc = "USP_MST_UPD_BANKBRANCHES";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("bankBranchID_", objBranchBO.BankBranchId);
            cmd.Parameters.AddWithValue("bankID_", objBranchBO.BankID);
            cmd.Parameters.AddWithValue("branchName_", objBranchBO.BranchName);
            cmd.Parameters.AddWithValue("city_", objBranchBO.City);

            cmd.Parameters.AddWithValue("swiftCode_", objBranchBO.SwiftCode);
            cmd.Parameters.AddWithValue("BANKCODE_", objBranchBO.BANKCODE);
            cmd.Parameters.AddWithValue("updatedby_", objBranchBO.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_BANKBRANCHES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BankBranchId_", BankBranchId);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETEBANKBRANCHES", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BankBranchId_", BankBranchId);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
