using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LivPlanItemDAL
    {
        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 

        //get all data in mst_LivPlanItem table using USP_MST_SELECTLivPlanItem-SP
        public LivPlanItemList GetLivPlanItem()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_SELECTLPITEM";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivPlanItemBO objLivPlanItem = null;
            LivPlanItemList LivPlanItem = new LivPlanItemList();

            while (dr.Read())
            {
                objLivPlanItem = new LivPlanItemBO();
                objLivPlanItem.LivPlanItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LIV_REST_ITEMID")));
                objLivPlanItem.LivPlanItemName = dr.GetString(dr.GetOrdinal("LIV_REST_ITEMNAME"));
                objLivPlanItem.Isdeleted = dr.GetString(dr.GetOrdinal("Isdeleted"));

                LivPlanItem.Add(objLivPlanItem);
            }

            dr.Close();

            return LivPlanItem;
        }


        /// <summary>
        /// save the data
        /// </summary>
        /// <param name="objLivPlanItem"></param>
        /// <returns></returns>
        public string Insert(LivPlanItemBO objLivPlanItem)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTLPITEM", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("LivPlanItem_", objLivPlanItem.LivPlanItemName);
                dcmd.Parameters.Add("CREATEDBY", objLivPlanItem.UserID);
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
        /// To GET ALL LivPlanItem
        /// </summary>
        /// <returns></returns>
        public LivPlanItemList GETALLLivPlanItem()
        {
            // used in Master page
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = " USP_MST_GETALLLivPlanItem";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivPlanItemBO objLivPlanItem = null;
            LivPlanItemList LivPlanItem = new LivPlanItemList();

            while (dr.Read())
            {
                objLivPlanItem = new LivPlanItemBO();
                objLivPlanItem.LivPlanItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LIV_REST_ITEMID")));
                objLivPlanItem.LivPlanItemName = dr.GetString(dr.GetOrdinal("LIV_REST_ITEMNAME"));
                objLivPlanItem.Isdeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));

                LivPlanItem.Add(objLivPlanItem);
            }

            dr.Close();

            return LivPlanItem;
        }


        /// <summary>
        /// get the data based on ID
        /// </summary>
        /// <param name="LivPlanItemID"></param>
        /// <returns></returns>
        public LivPlanItemBO GetLivPlanItemById(int LivPlanItemID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETSELECTLivPlanItem";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("LivPlanItemID_", LivPlanItemID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivPlanItemBO LivPlanItemObj = null;
            LivPlanItemList Users = new LivPlanItemList();

            LivPlanItemObj = new LivPlanItemBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "LIV_REST_ITEMNAME") && !dr.IsDBNull(dr.GetOrdinal("LIV_REST_ITEMNAME")))
                    LivPlanItemObj.LivPlanItemName = dr.GetString(dr.GetOrdinal("LIV_REST_ITEMNAME"));
                if (ColumnExists(dr, "LIV_REST_ITEMID") && !dr.IsDBNull(dr.GetOrdinal("LIV_REST_ITEMID")))
                    LivPlanItemObj.LivPlanItemID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LIV_REST_ITEMID")));

            }
            dr.Close();


            return LivPlanItemObj;
        }

        /// <summary>
        /// to check the Column are Exists or not
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool ColumnExists(IDataReader reader, string columnName)
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
        /// To update data to database
        /// </summary>
        /// <param name="objLivPlanItem"></param>
        /// <returns></returns>
        public string EDITLivPlanItem(LivPlanItemBO objLivPlanItem)
        {
            string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_UPDATECLivPlanItem", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("LivPlanItem_", objLivPlanItem.LivPlanItemName);
                dcmd.Parameters.Add("LivPlanItemID_", objLivPlanItem.LivPlanItemID);
                dcmd.Parameters.Add("UpdatedBY", objLivPlanItem.UserID);
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
        /// TO delete data
        /// </summary>
        /// <param name="LivPlanItemID"></param>
        /// <returns></returns>
        public string DeleteLivPlanItem(int LivPlanItemID)
        {

            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETELivPlanItem", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LivPlanItemID_", LivPlanItemID);
                //myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
                    result = "Selected LivPlanItem is already in use. Cannot delete";
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

            //OracleConnection myConnection = null;
            //OracleCommand myCommand = null;

            //string result = string.Empty;
            //try
            //{

            //    myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            //    myCommand = new OracleCommand("USP_MST_DELETELivPlanItem", myConnection);
            //    myCommand.Connection = myConnection;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("@LivPlanItemID_", LivPlanItemID);
            //    myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
            //    myConnection.Open();
            //    myCommand.ExecuteNonQuery();
            //    if (myCommand.Parameters["errorMessage_"].Value != null)
            //        result = myCommand.Parameters["errorMessage_"].Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("ORA-02292"))
            //    {
            //        result = "Selected item is already in use. Connot delete";
            //    }
            //    else
            //    {
            //        throw ex;
            //    }
            //}
            //finally
            //{
            //    myCommand.Dispose();
            //    myConnection.Close();
            //    myConnection.Dispose();
            //}

            //return result;  
            //OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            //OracleCommand cmd;


            //string proc = "USP_MST_DELETELivPlanItem";

            //cmd = new OracleCommand(proc, cnn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("LivPlanItemID_", LivPlanItemID);
            //cmd.Connection.Open();

            //int result = cmd.ExecuteNonQuery();

            //return result;
        }

        /// <summary>
        /// To Obsolete LivPlanItem
        /// </summary>
        /// <param name="LivPlanItemID"></param>
        /// <param name="Isdeleted"></param>
        /// <returns></returns>
        public string ObsoleteLivPlanItem(int LivPlanItemID, string Isdeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_LivPlanItem", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("LivPlanItemId_", LivPlanItemID);
                myCommand.Parameters.Add("isdeleted_", Isdeleted);
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
