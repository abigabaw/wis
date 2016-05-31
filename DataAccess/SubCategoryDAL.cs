using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
   public class SubCategoryDAL
    {
       /// <summary>
        /// To Insert into database
       /// </summary>
       /// <param name="SubCategoryBOobj"></param>
       /// <returns></returns>
       public string Insert(SubCategoryBO SubCategoryBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_SUB_CATG", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("BGT_SUBCATEGORYNAME_", SubCategoryBOobj.BGT_SUBCATEGORYNAME);
                dcmd.Parameters.Add("BGT_CATEGORYID_", SubCategoryBOobj.BGT_CATEGORYID);
                dcmd.Parameters.Add("ACCOUNTCODE_", SubCategoryBOobj.ACCOUNTCODE);
                dcmd.Parameters.Add("CREATEDBY", SubCategoryBOobj.CREATEDBY);


                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
            return returnResult;
        }

       /// <summary>
       /// To Edit database
       /// </summary>
       /// <param name="SubCategoryBOobj"></param>
       /// <returns></returns>
       public string Edit(SubCategoryBO SubCategoryBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_SUB_CATG", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("BGT_SUBCATEGORYNAME_", SubCategoryBOobj.BGT_SUBCATEGORYNAME);
               
                dcmd.Parameters.Add("BGT_SUBCATEGORYID_", SubCategoryBOobj.BGT_SUBCATEGORYID);
                dcmd.Parameters.Add("ACCOUNTCODE_", SubCategoryBOobj.ACCOUNTCODE);
                dcmd.Parameters.Add("UPDATEDBY_", SubCategoryBOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
            return returnResult;
        }

       /// <summary>
       /// To Get ALL Sub Category
       /// </summary>
       /// <param name="categoryID"></param>
       /// <returns></returns>
        public SubCategoryList GetALLSubCategory(int categoryID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_SUB_CATG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Bgt_categoryid_", categoryID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SubCategoryBO SubCategoryBOobj = null;
            SubCategoryList SubCategoryListobj = new SubCategoryList();


            while (dr.Read())
            {
                SubCategoryBOobj = new SubCategoryBO();
                if ( !dr.IsDBNull(dr.GetOrdinal("bgt_subcategoryid")))
                SubCategoryBOobj.BGT_SUBCATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("bgt_subcategoryid")));
                if (!dr.IsDBNull(dr.GetOrdinal("bgt_subcategoryname")))
                SubCategoryBOobj.BGT_SUBCATEGORYNAME = dr.GetString(dr.GetOrdinal("bgt_subcategoryname"));
                if (!dr.IsDBNull(dr.GetOrdinal("accountcode")))
                SubCategoryBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("accountcode"));
                if (!dr.IsDBNull(dr.GetOrdinal("isdeleted")))
                SubCategoryBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                SubCategoryListobj.Add(SubCategoryBOobj);
            }


            dr.Close();

            return SubCategoryListobj; ;
        }

       /// <summary>
        /// To Get Sub Category
       /// </summary>
       /// <returns></returns>
        public SubCategoryList GetSubCategory()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_SUB_CATG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SubCategoryBO SubCategoryBOobj = null;
            SubCategoryList SubCategoryListobj = new SubCategoryList();

            while (dr.Read())
            {
                SubCategoryBOobj = new SubCategoryBO();
                SubCategoryBOobj.BGT_SUBCATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("bgt_subcategoryid")));
                SubCategoryBOobj.BGT_SUBCATEGORYNAME = dr.GetString(dr.GetOrdinal("bgt_subcategoryname"));
                SubCategoryBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("accountcode"));
                SubCategoryBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("isdeleted"));

                SubCategoryListobj.Add(SubCategoryBOobj);
            }

            dr.Close();

            return SubCategoryListobj;
        }

       /// <summary>
        /// To Get Sub Category By Id
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <returns></returns>
        public SubCategoryBO GetSubCategoryById(int SubCATEGORYID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_SUB_CATG";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("BGT_SUBCATEGORYID_", SubCATEGORYID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            SubCategoryBO SubCategoryBOobj = null;
            SubCategoryList SubCategoryListobj = new SubCategoryList();

            SubCategoryBOobj = new SubCategoryBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "BGT_SUBCATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYNAME")))
                    SubCategoryBOobj.BGT_SUBCATEGORYNAME = dr.GetString(dr.GetOrdinal("BGT_SUBCATEGORYNAME"));
                if (ColumnExists(dr, "ACCOUNTCODE") && !dr.IsDBNull(dr.GetOrdinal("ACCOUNTCODE")))
                    SubCategoryBOobj.ACCOUNTCODE = dr.GetString(dr.GetOrdinal("ACCOUNTCODE"));
                if (ColumnExists(dr, "BGT_SUBCATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_SUBCATEGORYID")))
                    SubCategoryBOobj.BGT_SUBCATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_SUBCATEGORYID")));

            }
            dr.Close();


            return SubCategoryBOobj;
        }

       /// <summary>
       /// To check weather Column Exists or Not
       /// </summary>
       /// <param name="reader"></param>
       /// <param name="columnName"></param>
       /// <returns></returns>
        private bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

       /// <summary>
        /// To Delete Sub Category
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <returns></returns>
        public string DeleteSubCategory(int SubCATEGORYID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_SUB_CATG", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BGT_SUBCATEGORYID_", SubCATEGORYID);
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
                    result = "Selected item is already in use. Connot delete";
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
        /// To Obsolete Sub Category
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteSubCategory(int SubCATEGORYID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_SUB_CATG", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BGT_SUBCATEGORYID_", SubCATEGORYID);
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
