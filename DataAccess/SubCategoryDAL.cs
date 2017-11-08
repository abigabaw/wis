using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INS_SUB_CATG", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("BGT_SUBCATEGORYNAME_", SubCategoryBOobj.BGT_SUBCATEGORYNAME);
                dcmd.Parameters.AddWithValue("BGT_CATEGORYID_", SubCategoryBOobj.BGT_CATEGORYID);
                dcmd.Parameters.AddWithValue("ACCOUNTCODE_", SubCategoryBOobj.ACCOUNTCODE);
                dcmd.Parameters.AddWithValue("CREATEDBY", SubCategoryBOobj.CREATEDBY);


                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPD_SUB_CATG", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("BGT_SUBCATEGORYNAME_", SubCategoryBOobj.BGT_SUBCATEGORYNAME);
               
                dcmd.Parameters.AddWithValue("BGT_SUBCATEGORYID_", SubCategoryBOobj.BGT_SUBCATEGORYID);
                dcmd.Parameters.AddWithValue("ACCOUNTCODE_", SubCategoryBOobj.ACCOUNTCODE);
                dcmd.Parameters.AddWithValue("UPDATEDBY_", SubCategoryBOobj.CREATEDBY);
                //return dcmd.ExecuteNonQuery();
                dcmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_SUB_CATG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Bgt_categoryid_", categoryID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_SEL_SUB_CATG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_SUB_CATG";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("BGT_SUBCATEGORYID_", SubCATEGORYID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_SUB_CATG", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BGT_SUBCATEGORYID_", SubCATEGORYID);
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_SUB_CATG", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("BGT_SUBCATEGORYID_", SubCATEGORYID);
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
