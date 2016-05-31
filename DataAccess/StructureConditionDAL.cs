using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class StructureConditionDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Condition
        /// </summary>
        /// <returns></returns>
        public StructureConditionList GetAllStructureCondition()
        {
            proc = "USP_MST_GET_ALLSTRUCTCONDITION";
            cnn = new OracleConnection(con);
            StructureConditionBO objStructureCondition = null;

            StructureConditionList lstStructureConditionList = new StructureConditionList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureCondition = new StructureConditionBO();

                    if (ColumnExists(dr, "str_conditionid") && !dr.IsDBNull(dr.GetOrdinal("str_conditionid")))
                        objStructureCondition.StructureConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_conditionid")));
                    if (ColumnExists(dr, "str_condition") && !dr.IsDBNull(dr.GetOrdinal("str_condition")))
                        objStructureCondition.StructureConditionName = dr.GetString(dr.GetOrdinal("str_condition"));
                    if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                        objStructureCondition.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    lstStructureConditionList.Add(objStructureCondition);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureConditionList;
        }

        /// <summary>
        /// To Get Structure Condition
        /// </summary>
        /// <returns></returns>
        public StructureConditionList GetStructureCondition()
        {
            proc = "USP_MST_GET_STRUCTURECONDITION";
            cnn = new OracleConnection(con);
            StructureConditionBO objStructureCondition = null;
            
            StructureConditionList lstStructureConditionList = new StructureConditionList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objStructureCondition = new StructureConditionBO();

                    if (ColumnExists(dr, "str_conditionid") && !dr.IsDBNull(dr.GetOrdinal("str_conditionid")))
                        objStructureCondition.StructureConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_conditionid")));
                    if (ColumnExists(dr, "str_condition") && !dr.IsDBNull(dr.GetOrdinal("str_condition")))
                        objStructureCondition.StructureConditionName = dr.GetString(dr.GetOrdinal("str_condition"));
                   
                    lstStructureConditionList.Add(objStructureCondition);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStructureConditionList;
        }

        /// <summary>
        /// To Get Structure Condition By Id
        /// </summary>
        /// <param name="StructureConditionID"></param>
        /// <returns></returns>
        public StructureConditionBO GetStructureConditionById(int StructureConditionID)
        {
            cnn = new OracleConnection(con);

            proc = "USP_MST_GET_STRUCTCONDI_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("str_conditionid_", StructureConditionID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            StructureConditionBO objStructureCondition = null;


            while (dr.Read())
            {
                objStructureCondition = new StructureConditionBO();

                if (ColumnExists(dr, "str_conditionid") && !dr.IsDBNull(dr.GetOrdinal("str_conditionid")))
                    objStructureCondition.StructureConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("str_conditionid")));
                if (ColumnExists(dr, "str_condition") && !dr.IsDBNull(dr.GetOrdinal("str_condition")))
                    objStructureCondition.StructureConditionName = dr.GetString(dr.GetOrdinal("str_condition"));
                if (ColumnExists(dr, "IsDeleted") && !dr.IsDBNull(dr.GetOrdinal("IsDeleted")))
                    objStructureCondition.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
            }
            dr.Close();

            return objStructureCondition;
        }

        /// <summary>
        /// To Check Weather Column Exists or not
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
        /// To Save Structure Condition
        /// </summary>
        /// <param name="oStructureCondition"></param>
        /// <returns></returns>
        public string SaveStructureCondition(StructureConditionBO oStructureCondition)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_STRUCTURECONDITION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("str_condition_", oStructureCondition.StructureConditionName);

            cmd.Parameters.Add("isdeleted_", oStructureCondition.IsDeleted);
            cmd.Parameters.Add("createdby_", oStructureCondition.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            return returnResult;
        }

        /// <summary>
        /// To Update Structure Condition
        /// </summary>
        /// <param name="oStructureCondition"></param>
        /// <returns></returns>
        public string UpdateStructureCondition(StructureConditionBO oStructureCondition)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_STRUCTURECONDITION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("str_conditionid_", oStructureCondition.StructureConditionID);
            cmd.Parameters.Add("str_condition_", oStructureCondition.StructureConditionName);

            cmd.Parameters.Add("updatedby_", oStructureCondition.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
    
        /// <summary>
        /// To Delete Structure Condition 
        /// </summary>
        /// <param name="StructureConditionID_"></param>
        /// <returns></returns>
        public string DeleteStructureCondition(int StructureConditionID_)
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_DEL_STRUCTURECONDITION";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("str_conditionid_", StructureConditionID_);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();
            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;
            return returnResult;
        }

        /// <summary>
        /// To Obsolete Structure Condition
        /// </summary>
        /// <param name="StructureConditionID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureCondition(int StructureConditionID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_STRUCTCONDIT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("str_conditionid_", StructureConditionID);
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
        #endregion
    }
}