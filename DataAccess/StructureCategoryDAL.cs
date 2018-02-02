using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class StructureCategoryDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Category
        /// </summary>
        /// <returns></returns>
        public StructureCategoryList GetAllStructureCategory()
        {
            proc = "USP_MST_GET_ALL_STRUCTCATEGORY";
            cnn = new SqlConnection(con);
            StructureCategoryBO objStructureCategory = null;

            StructureCategoryList lstStructureCategoryList = new StructureCategoryList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureCategory = new StructureCategoryBO();

                    if (ColumnExists(dr, "str_categoryid") && !dr.IsDBNull(dr.GetOrdinal("str_categoryid")))
                        objStructureCategory.StructureCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_categoryid")));
                    if (ColumnExists(dr, "str_categoryname") && !dr.IsDBNull(dr.GetOrdinal("str_categoryname")))
                        objStructureCategory.StructureCategoryName = dr.GetString(dr.GetOrdinal("str_categoryname"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objStructureCategory.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    lstStructureCategoryList.Add(objStructureCategory);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureCategoryList;
        }

        /// <summary>
        /// To Get Structure Category
        /// </summary>
        /// <returns></returns>
        public StructureCategoryList GetStructureCategory()
        {
            proc = "USP_MST_GET_STRUCTCATEGORY";
            cnn = new SqlConnection(con);
            StructureCategoryBO objStructureCategory = null;

            StructureCategoryList lstStructureCategoryList = new StructureCategoryList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureCategory = new StructureCategoryBO();

                    if (ColumnExists(dr, "str_categoryid") && !dr.IsDBNull(dr.GetOrdinal("str_categoryid")))
                        objStructureCategory.StructureCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_categoryid")));
                    if (ColumnExists(dr, "str_categoryname") && !dr.IsDBNull(dr.GetOrdinal("str_categoryname")))
                        objStructureCategory.StructureCategoryName = dr.GetString(dr.GetOrdinal("str_categoryname"));
                    //if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    //    objStructureCategory.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                    lstStructureCategoryList.Add(objStructureCategory);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureCategoryList;
        }

        /// <summary>
        /// To Get Structure Category By Id
        /// </summary>
        /// <param name="StructureCategoryID"></param>
        /// <returns></returns>
        public StructureCategoryBO GetStructureCategoryById(int StructureCategoryID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_MST_GET_STRUCTCATEGORYBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("str_categoryid_", StructureCategoryID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            StructureCategoryBO objStructureCategory = null;


            while (dr.Read())
            {
                objStructureCategory = new StructureCategoryBO();

                if (ColumnExists(dr, "str_categoryid") && !dr.IsDBNull(dr.GetOrdinal("str_categoryid")))
                    objStructureCategory.StructureCategoryID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_categoryid")));
                if (ColumnExists(dr, "str_categoryname") && !dr.IsDBNull(dr.GetOrdinal("str_categoryname")))
                    objStructureCategory.StructureCategoryName = dr.GetString(dr.GetOrdinal("str_categoryname"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objStructureCategory.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objStructureCategory;
        }

        /// <summary>
        /// To Check weather column exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                ColumnNames[i] = reader.GetName(i);

                if (reader.GetName(i).ToLower() == columnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Save, Update & Delete Record
        /// <summary>
        /// To Save Structure Category
        /// </summary>
        /// <param name="oStructureCategory"></param>
        /// <returns></returns>
        public string SaveStructureCategory(StructureCategoryBO oStructureCategory)
        {
            string returnResult;
            cnn = new SqlConnection(con);

            proc = "USP_MST_INS_STRUCTCATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("str_categoryname_", oStructureCategory.StructureCategoryName);

            cmd.Parameters.AddWithValue("isdeleted_", oStructureCategory.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oStructureCategory.UserID);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Structure Category
        /// </summary>
        /// <param name="oStructureCategory"></param>
        /// <returns></returns>
        public string UpdateStructureCategory(StructureCategoryBO oStructureCategory)
        {
            string returnResult;

            cnn = new SqlConnection(con);

            proc = "USP_MST_UPD_STRUCTCATEGORY";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("str_categoryid_", oStructureCategory.StructureCategoryID);
            cmd.Parameters.AddWithValue("str_categoryname_", oStructureCategory.StructureCategoryName);

            cmd.Parameters.AddWithValue("updatedby_", oStructureCategory.UserID);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Delete Structure Category ID
        /// </summary>
        /// <param name="structureCategoryID"></param>
        /// <returns></returns>
        public string DeleteStructureCategory(int structureCategoryID)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;

            proc = "USP_MST_DEL_STRUCTCATEGORY";

            try
            {
                cmd = new SqlCommand(proc, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("str_categoryid_", structureCategoryID);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["errorMessage_"].Value != null)
                    returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch (Exception ex)
            { throw ex; }
            return returnResult;
        }

        /// <summary>
        /// To Obsolete Structure Category
        /// </summary>
        /// <param name="StructureCategoryID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureCategory(int StructureCategoryID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;

            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_STRUCTURECAT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("str_categoryid_", StructureCategoryID);
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
        #endregion
    }
}