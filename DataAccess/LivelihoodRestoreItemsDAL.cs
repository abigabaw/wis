using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LivelihoodRestoreItemsDAL
    {
        #region Global Declaration
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion Global Declaration

        #region GET Live Rest Items
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public LivelihoodRestoreItemsList GetLiveRestItems()
        {
            proc = "USP_MST_GET_LIV_REST_ITEMS";
            cnn = new OracleConnection(con);
            LivelihoodRestoreItemsBO oLiveRestItemsBO;

            LivelihoodRestoreItemsList lstLiveRestItems = new LivelihoodRestoreItemsList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oLiveRestItemsBO = new LivelihoodRestoreItemsBO();

                    //if (!dr.IsDBNull(dr.GetOrdinal("Liv_Rest_ItemID"))) oLiveRestItemsBO.Liv_Rest_ItemID = dr.GetInt32(dr.GetOrdinal("Liv_Rest_ItemID"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("Liv_Rest_ItemName"))) oLiveRestItemsBO.Liv_Rest_ItemName = dr.GetString(dr.GetOrdinal("Liv_Rest_ItemName"));

                    ////if (!dr.IsDBNull(dr.GetOrdinal("IsDeleted"))) objBank.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                    //lstLiveRestItems.Add(oLiveRestItemsBO);
                    lstLiveRestItems.Add(MapData(dr));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstLiveRestItems;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public LivelihoodRestoreItemsList GetLiveRestItems_All()
        {
            proc = "USP_MST_GET_LIV_REST_ITEMS_ALL";
            cnn = new OracleConnection(con);
            LivelihoodRestoreItemsBO oLiveRestItemsBO;

            LivelihoodRestoreItemsList lstLiveRestItems = new LivelihoodRestoreItemsList();

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oLiveRestItemsBO = new LivelihoodRestoreItemsBO();
                  
                    lstLiveRestItems.Add(MapData(dr));
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstLiveRestItems;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="RestorationItemID"></param>
        /// <returns></returns>
        public LivelihoodRestoreItemsBO GetLiveRestItemsById(int RestorationItemID)
        {
            proc = "USP_MST_GET_LIV_REST_ITEMSBYID";
            cnn = new OracleConnection(con);
            LivelihoodRestoreItemsBO oLiveRestItemsBO = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Liv_Rest_ItemID_", RestorationItemID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    oLiveRestItemsBO = new LivelihoodRestoreItemsBO();

                    oLiveRestItemsBO = MapData(dr);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oLiveRestItemsBO;
        }
        /// <summary>
        /// to map data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private LivelihoodRestoreItemsBO MapData(IDataReader reader)
        {
            LivelihoodRestoreItemsBO oLiveRestItemsBO = new LivelihoodRestoreItemsBO();

            if (ColumnExists(reader, "Liv_Rest_ItemID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_ItemID")))
                oLiveRestItemsBO.Liv_Rest_ItemID = reader.GetInt32(reader.GetOrdinal("Liv_Rest_ItemID"));
            if (ColumnExists(reader, "Liv_Rest_ItemName") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_ItemName")))
                oLiveRestItemsBO.Liv_Rest_ItemName = reader.GetString(reader.GetOrdinal("Liv_Rest_ItemName"));
            if (ColumnExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
                oLiveRestItemsBO.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));
            return oLiveRestItemsBO;
        }
        /// <summary>
        /// to check whether column exists
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
        #endregion GET Live Rest Items

        #region MODIFY Live Rest Items
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="oLiveRestItemsBO"></param>
        /// <returns></returns>
        public string AddLiveRestItem(LivelihoodRestoreItemsBO oLiveRestItemsBO)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_LIV_REST_ITEMS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("Liv_Rest_ItemName_", oLiveRestItemsBO.Liv_Rest_ItemName);
            //Common Parameters
            cmd.Parameters.Add("isdeleted_", oLiveRestItemsBO.IsDeleted);
            cmd.Parameters.Add("createdby_", oLiveRestItemsBO.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="oLiveRestItemsBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestItem(LivelihoodRestoreItemsBO oLiveRestItemsBO)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_LIV_REST_ITEMS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("Liv_Rest_ItemID_", oLiveRestItemsBO.Liv_Rest_ItemID);
            cmd.Parameters.Add("Liv_Rest_ItemName_", oLiveRestItemsBO.Liv_Rest_ItemName);
            //Common Parameters
            cmd.Parameters.Add("updatedby_", oLiveRestItemsBO.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="LiveRestItemID"></param>
        /// <returns></returns>
        public string DeleteLiveRestItem(int LiveRestItemID)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_DEL_LIV_REST_ITEMS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("Liv_Rest_ItemID_", LiveRestItemID);
            //Common Parameters
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="LiveRestItemID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLiveRestItem(int LiveRestItemID, string IsDeleted) 
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_OBS_LIV_REST_ITEMS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("Liv_Rest_ItemID_", LiveRestItemID);
            cmd.Parameters.Add("IsDeleted_", IsDeleted);
            //Common Parameters
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
        }
      
        #endregion MODIFY Live Rest Items
    }
}
