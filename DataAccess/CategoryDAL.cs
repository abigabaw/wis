using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("BGT_CATEGORYNAME", CategoryBOobj.BGT_CATEGORYNAME);
                dcmd.Parameters.AddWithValue("CREATEDBY", CategoryBOobj.CREATEDBY);
                

                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_CATEGORY", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("BGT_CATEGORYNAME_", CategoryBOobj.BGT_CATEGORYNAME);
                dcmd.Parameters.AddWithValue("BGT_CATEGORYID_", CategoryBOobj.BGT_CATEGORYID);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", CategoryBOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_CATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_CATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_CATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("BGT_CATEGORYID_", CATEGORYID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_CATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BGT_CATEGORYID_", CATEGORYID);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_CATEGORY", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BGT_CATEGORYID_", CATEGORYID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
