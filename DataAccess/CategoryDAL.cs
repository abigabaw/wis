using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class CategoryDAL
    {
        /// <summary>
        /// To insert category details to database
        /// </summary>
        /// <param name="CategoryBOobj"></param>
        /// <returns></returns>
       public string Insert(CategoryBO CategoryBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INS_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("BGT_CATEGORYNAME", CategoryBOobj.BGT_CATEGORYNAME);
                dcmd.Parameters.Add("CREATEDBY", CategoryBOobj.CREATEDBY);
                

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
       /// To Edit Category details into database
       /// </summary>
       /// <param name="CategoryBOobj"></param>
       /// <returns></returns>
       public string Edit(CategoryBO CategoryBOobj)
        {
            string returnResult = string.Empty;
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPD_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("BGT_CATEGORYNAME_", CategoryBOobj.BGT_CATEGORYNAME);
                dcmd.Parameters.Add("BGT_CATEGORYID_", CategoryBOobj.BGT_CATEGORYID);
                dcmd.Parameters.Add("UPDATEDBY_", CategoryBOobj.CREATEDBY);
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
       /// To Get ALL Category details
       /// </summary>
       /// <returns></returns>
       public CategoryList GetALLCategory()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_CATEGORY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CategoryBO CategoryBOobj = null;
            CategoryList CategoryListobj = new CategoryList();


            while (dr.Read())
            {
                CategoryBOobj = new CategoryBO();
                CategoryBOobj.BGT_CATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));
                CategoryBOobj.BGT_CATEGORYNAME = dr.GetString(dr.GetOrdinal("BGT_CATEGORYNAME"));
                CategoryBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                CategoryListobj.Add(CategoryBOobj);
            }


            dr.Close();

            return CategoryListobj; ;
        }
       /// <summary>
       /// To GetCategory details from database
       /// </summary>
       /// <returns></returns>
       public CategoryList GetCategory()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SEL_CATEGORY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CategoryBO CategoryBOobj = null;
            CategoryList CategoryListobj = new CategoryList();

            while (dr.Read())
            {
                CategoryBOobj = new CategoryBO();
                CategoryBOobj.BGT_CATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));
                CategoryBOobj.BGT_CATEGORYNAME = dr.GetString(dr.GetOrdinal("BGT_CATEGORYNAME"));
                CategoryBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                CategoryListobj.Add(CategoryBOobj);
            }

            dr.Close();

            return CategoryListobj;
        }
       /// <summary>
       /// To get category details based on ID from database
       /// </summary>
       /// <param name="CATEGORYID"></param>
       /// <returns></returns>
       public CategoryBO GetCategoryById(int CATEGORYID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_CATEGORY";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("BGT_CATEGORYID_", CATEGORYID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CategoryBO CategoryBOobj = null;
            CategoryList CategoryListobj = new CategoryList();

            CategoryBOobj = new CategoryBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "BGT_CATEGORYNAME") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYNAME")))
                    CategoryBOobj.BGT_CATEGORYNAME = dr.GetString(dr.GetOrdinal("BGT_CATEGORYNAME"));
                if (ColumnExists(dr, "BGT_CATEGORYID") && !dr.IsDBNull(dr.GetOrdinal("BGT_CATEGORYID")))
                    CategoryBOobj.BGT_CATEGORYID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("BGT_CATEGORYID")));

            }
            dr.Close();


            return CategoryBOobj;
        }
       /// <summary>
       /// To check whether column exists
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
        /// To delete Category details
        /// </summary>
        /// <param name="CATEGORYID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string DeleteCategory(int CATEGORYID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_CATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BGT_CATEGORYID_", CATEGORYID);
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
       /// To make category details obsolete
       /// </summary>
       /// <param name="CATEGORYID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteCategory(int CATEGORYID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_CATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("BGT_CATEGORYID_", CATEGORYID);
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
