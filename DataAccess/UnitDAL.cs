using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class UnitDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Unit
        /// </summary>
        /// <returns></returns>
        public UnitList GetAllUnit()//(Unit oUnit)
        {
            proc = "USP_MST_GET_ALL_UNIT";
            cnn = new SqlConnection(con);
            UnitBO objUnit = null;

            UnitList lstUnitList = new UnitList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUnit = new UnitBO();

                    if (ColumnExists(dr, "unitid") && !dr.IsDBNull(dr.GetOrdinal("unitid")))
                        objUnit.UnitID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("unitid")));
                    if (ColumnExists(dr, "unitname") && !dr.IsDBNull(dr.GetOrdinal("unitname")))
                        objUnit.UnitName = dr.GetString(dr.GetOrdinal("unitname"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objUnit.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    lstUnitList.Add(objUnit);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUnitList;
        }

        /// <summary>
        /// To Get Unit
        /// </summary>
        /// <returns></returns>
        public UnitList GetUnit()//(Unit oUnit)
        {
            proc = "USP_MST_GET_UNIT";
            cnn = new SqlConnection(con);
            UnitBO objUnit = null;
            
            UnitList lstUnitList = new UnitList();

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objUnit = new UnitBO();

                    if (ColumnExists(dr, "unitid") && !dr.IsDBNull(dr.GetOrdinal("unitid")))
                        objUnit.UnitID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("unitid")));
                    if (ColumnExists(dr, "unitname") && !dr.IsDBNull(dr.GetOrdinal("unitname")))
                        objUnit.UnitName = dr.GetString(dr.GetOrdinal("unitname"));
                   
                    lstUnitList.Add(objUnit);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUnitList;
        }

        /// <summary>
        /// To Get Unit By Id
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public UnitBO GetUnitById(int UnitID)
        {
            cnn = new SqlConnection(con);

            proc = "USP_MST_GET_UNIT_BYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("unitid_", UnitID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            UnitBO objUnit = null;


            while (dr.Read())
            {
                objUnit = new UnitBO();

                if (ColumnExists(dr, "unitid") && !dr.IsDBNull(dr.GetOrdinal("unitid")))
                    objUnit.UnitID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("unitid")));
                if (ColumnExists(dr, "unitname") && !dr.IsDBNull(dr.GetOrdinal("unitname")))
                    objUnit.UnitName = dr.GetString(dr.GetOrdinal("unitname"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objUnit.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objUnit;
        }

        /// <summary>
        /// To check weather Column Exists or Not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
        {
            //string[] ColumnNames = new string[20];
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //ColumnNames[i] = reader.GetName(i);

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
        /// To Save Unit
        /// </summary>
        /// <param name="oUnit"></param>
        /// <returns></returns>
        public string SaveUnit(UnitBO oUnit)
        {
            string returnResult;
            cnn = new SqlConnection(con);

            proc = "USP_MST_INS_UNIT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("unitname_", oUnit.UnitName);

            cmd.Parameters.AddWithValue("isdeleted_", oUnit.IsDeleted);
            cmd.Parameters.AddWithValue("createdby_", oUnit.CreatedBy);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }

        /// <summary>
        /// To Update Unit
        /// </summary>
        /// <param name="oUnit"></param>
        /// <returns></returns>
        public string UpdateUnit(UnitBO oUnit)
        {
            string returnResult;
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPD_UNIT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("unitid_", oUnit.UnitID);
            cmd.Parameters.AddWithValue("unitname_", oUnit.UnitName);

            cmd.Parameters.AddWithValue("updatedby_", oUnit.CreatedBy);

            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
    
        /// <summary>
        /// To Delete Unit
        /// </summary>
        /// <param name="UnitID_"></param>
        /// <returns></returns>
        public string DeleteUnit(int UnitID_)
        {
            cnn = new SqlConnection(con);
            string returnResult=string.Empty;
            proc = "USP_MST_DEL_UNIT";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("unitid_", UnitID_);
            /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = cmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            return returnResult;
        }

        /// <summary>
        /// To Obsolete Unit
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteUnit(int UnitID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;

            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_UNIT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("UnitID_", UnitID);
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