using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class LivelihoodRestorationPlanDAL
    {

        //string connStr = ConfigurationManager.ConnectionStrings["UETCL_WIS"].ToString();  // database connection string 
        string con = WIS_DataAccess.AppConfiguration.ConnectionString;
        string proc = string.Empty;

        #region GetData
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public LivelihoodRestorationList GetLivelihoodRestorationPlan(int LocationId)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAP_LIV_REST_PLAN";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_rest_locationid_", LocationId);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodRestorationBO objLivelihoodRestorationBO = null;
            LivelihoodRestorationList oLivelihoodRestorationList = new LivelihoodRestorationList();

            while (dr.Read())
            {
                objLivelihoodRestorationBO = new LivelihoodRestorationBO();
                objLivelihoodRestorationBO = MapData(dr);

                oLivelihoodRestorationList.Add(objLivelihoodRestorationBO);
            }
            dr.Close();

            return oLivelihoodRestorationList;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="LivRestPlanId"></param>
        /// <returns></returns>
        public LivelihoodRestorationBO GetLivelihoodRestorationPlanById(int LivRestPlanId)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAPLIVRESTPLANBYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Liv_Rest_PlanID_", LivRestPlanId);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodRestorationBO objLivelihoodRestorationBO = null;
            LivelihoodRestorationList oLivelihoodRestorationList = new LivelihoodRestorationList();
            objLivelihoodRestorationBO = new LivelihoodRestorationBO();
            while (dr.Read())
            {

                objLivelihoodRestorationBO = MapData(dr);

                //oLivelihoodRestorationList.Add(objLivelihoodRestorationBO);
            }
            dr.Close();

            return objLivelihoodRestorationBO;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="LivRestPlanId"></param>
        /// <returns></returns>
        public LivelihoodRestorationList GetLivelihoodRestReceivedByPlanId(int LivRestPlanId)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAP_LIV_REST_RECEI";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Liv_Rest_PlanID_", LivRestPlanId);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodRestorationBO objLivelihoodRestorationBO = null;
            LivelihoodRestorationList oLivelihoodRestorationList = new LivelihoodRestorationList();
            objLivelihoodRestorationBO = new LivelihoodRestorationBO();
            while (dr.Read())
            {
                objLivelihoodRestorationBO = MapData(dr);

                oLivelihoodRestorationList.Add(objLivelihoodRestorationBO);
            }
            dr.Close();

            return oLivelihoodRestorationList;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="LivReceivedId"></param>
        /// <returns></returns>
        public LivelihoodRestorationBO GetItemReceivedByPlanId(int LivReceivedId)
        {
            OracleConnection cnn = new OracleConnection(con);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PAP_RECEIVED_BYID";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("liv_rest_recdid_", LivReceivedId);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodRestorationBO objLivelihoodRestorationBO = null;
            LivelihoodRestorationList oLivelihoodRestorationList = new LivelihoodRestorationList();
            objLivelihoodRestorationBO = new LivelihoodRestorationBO();
            while (dr.Read())
            {
                objLivelihoodRestorationBO = MapData(dr);

                // oLivelihoodRestorationList.Add(objLivelihoodRestorationBO);
            }
            dr.Close();

            return objLivelihoodRestorationBO;
        }
        /// <summary>
        /// to check whetehr column exists
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

        private LivelihoodRestorationBO MapData(IDataReader reader)
        {
            LivelihoodRestorationBO oLiveRestPlanBO = new LivelihoodRestorationBO();

            if (ColumnExists(reader, "Liv_Rest_PlanID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_PlanID")))
                oLiveRestPlanBO.Liv_Rest_PlanID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Rest_PlanID")));

            //if (ColumnExists(reader, "HHID") && !reader.IsDBNull(reader.GetOrdinal("HHID")))
            //    oLiveRestPlanBO.HHID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("HHID")));

            if (ColumnExists(reader, "Liv_Rest_LocationID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_LocationID")))
                oLiveRestPlanBO.Liv_Rest_LocationID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Rest_LocationID")));

            if (ColumnExists(reader, "Liv_Rest_ItemID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_ItemID")))
                oLiveRestPlanBO.Liv_Rest_ItemID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Rest_ItemID")));

            if (ColumnExists(reader, "UnitID") && !reader.IsDBNull(reader.GetOrdinal("UnitID")))
                oLiveRestPlanBO.UnitID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("UnitID")));

            if (ColumnExists(reader, "Planned") && !reader.IsDBNull(reader.GetOrdinal("Planned")))
                oLiveRestPlanBO.Planned = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Planned")));

            if (ColumnExists(reader, "PlannedDate") && !reader.IsDBNull(reader.GetOrdinal("PlannedDate")))
                oLiveRestPlanBO.PlannedDate = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("PlannedDate")).ToString(UtilBO.DateFormat));

            if (ColumnExists(reader, "Received") && !reader.IsDBNull(reader.GetOrdinal("Received")))
                oLiveRestPlanBO.Received = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("Received")));

            if (ColumnExists(reader, "DATERECEIVED") && !reader.IsDBNull(reader.GetOrdinal("DATERECEIVED")))
                oLiveRestPlanBO.ReceivedDate = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DATERECEIVED")).ToString(UtilBO.DateFormat));
                    //Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DATERECEIVED")));

            if (ColumnExists(reader, "UnitName") && !reader.IsDBNull(reader.GetOrdinal("UnitName")))
                oLiveRestPlanBO.UnitName = reader.GetString(reader.GetOrdinal("UnitName"));

            if (ColumnExists(reader, "ItemName") && !reader.IsDBNull(reader.GetOrdinal("ItemName")))
                oLiveRestPlanBO.ItemName = reader.GetString(reader.GetOrdinal("ItemName"));

            if (ColumnExists(reader, "Liv_Rest_RecdID") && !reader.IsDBNull(reader.GetOrdinal("Liv_Rest_RecdID")))
                oLiveRestPlanBO.Liv_Rest_RecdID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Liv_Rest_RecdID")));

            if (ColumnExists(reader, "UNITPRICE") && !reader.IsDBNull(reader.GetOrdinal("UNITPRICE")))
                oLiveRestPlanBO.UnitPrice = Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("UNITPRICE")));

            return oLiveRestPlanBO;
        }
        #endregion
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
   
        public string AddLiveRestPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            string returnResult = string.Empty;
            LivelihoodRestorationBO ooLiveRestPlanBO = new LivelihoodRestorationBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_INS_PAP_LIV_REST_PLAN", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("Liv_Rest_PlanID_", oLiveRestPlanBO.Liv_Rest_PlanID);
                oCmd.Parameters.Add("Liv_Rest_LocationID_", oLiveRestPlanBO.Liv_Rest_LocationID);
                oCmd.Parameters.Add("Liv_Rest_ItemID_", oLiveRestPlanBO.Liv_Rest_ItemID);
                oCmd.Parameters.Add("UnitID_", oLiveRestPlanBO.UnitID);
                oCmd.Parameters.Add("Planned_", oLiveRestPlanBO.Planned);
                if (oLiveRestPlanBO.PlannedDate != DateTime.MinValue)
                    oCmd.Parameters.Add("PlannedDate_", oLiveRestPlanBO.PlannedDate);
                else
                    oCmd.Parameters.Add("PlannedDate_", DBNull.Value);

                oCmd.Parameters.Add("Received_", oLiveRestPlanBO.Received);
                
                if (oLiveRestPlanBO.UnitPrice != 0)
                    oCmd.Parameters.Add("UnitPrice_", oLiveRestPlanBO.UnitPrice);
                else
                    oCmd.Parameters.Add("UnitPrice_", DBNull.Value);

                oCmd.Parameters.Add("isdeleted_", oLiveRestPlanBO.IsDeleted);
                oCmd.Parameters.Add("createdby_", oLiveRestPlanBO.CreatedBy);
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                //oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            string returnResult = string.Empty;
            LivelihoodRestorationBO ooLiveRestPlanBO = new LivelihoodRestorationBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_CMP_ADDLivelihoodRestorationPlan", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("Liv_Rest_PlanID_", oLiveRestPlanBO.Liv_Rest_PlanID);
                oCmd.Parameters.Add("Liv_Rest_LocationID_", oLiveRestPlanBO.Liv_Rest_LocationID);

                //oCmd.Parameters.Add("hhid_", oLiveRestPlanBO.HHID);
                oCmd.Parameters.Add("Liv_Rest_ItemID_", oLiveRestPlanBO.Liv_Rest_ItemID);
                oCmd.Parameters.Add("UnitID_", oLiveRestPlanBO.UnitID);
                oCmd.Parameters.Add("Planned_", oLiveRestPlanBO.Planned);
                if (oLiveRestPlanBO.PlannedDate!=DateTime.MinValue)
                    oCmd.Parameters.Add("PlannedDate_", oLiveRestPlanBO.PlannedDate);
                else
                    oCmd.Parameters.Add("PlannedDate_", DBNull.Value);
                oCmd.Parameters.Add("Received_", oLiveRestPlanBO.Received);
                //oCmd.Parameters.Add("UnitPrice_", oLiveRestPlanBO.UnitPrice);

                oCmd.Parameters.Add("isdeleted_", oLiveRestPlanBO.IsDeleted);
                oCmd.Parameters.Add("createdby_", oLiveRestPlanBO.CreatedBy);
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                //oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="LiveRestPlanId"></param>
        /// <returns></returns>
        public string DeleteLivRestPlan(int LiveRestPlanId)
        {
            string returnResult = string.Empty;
            LivelihoodRestorationBO ooLiveRestPlanBO = new LivelihoodRestorationBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_DEL_PAP_LIV_REST_PLAN", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("Liv_Rest_PlanID_", LiveRestPlanId);

                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                //oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to add data
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
        public string AddLiveReceivedPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            string returnResult = string.Empty;
            LivelihoodRestorationBO ooLiveRestPlanBO = new LivelihoodRestorationBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_INS_PAP_LIV_REST_RECEI", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("liv_rest_recdid_", oLiveRestPlanBO.Liv_Rest_RecdID);
                oCmd.Parameters.Add("liv_rest_planid_", oLiveRestPlanBO.Liv_Rest_PlanID);
                //oCmd.Parameters.Add("Liv_Rest_ItemID_", oLiveRestPlanBO.Liv_Rest_ItemID);
                //oCmd.Parameters.Add("UnitID_", oLiveRestPlanBO.UnitID);
                //oCmd.Parameters.Add("Planned_", oLiveRestPlanBO.Planned);
                oCmd.Parameters.Add("datereceived_", oLiveRestPlanBO.ReceivedDate);
                oCmd.Parameters.Add("Received_", oLiveRestPlanBO.Received);

                oCmd.Parameters.Add("isdeleted_", oLiveRestPlanBO.IsDeleted);
                oCmd.Parameters.Add("createdby_", oLiveRestPlanBO.CreatedBy);
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                //oCmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="ReceivedId"></param>
        /// <returns></returns>
        public string DeleteItemReceived(int ReceivedId)
        {
            string returnResult = string.Empty;
            LivelihoodRestorationBO ooLiveRestPlanBO = new LivelihoodRestorationBO();//For Storing & Returning Result as Object

            OracleConnection OCon = new OracleConnection(con);
            OCon.Open();
            OracleCommand oCmd = new OracleCommand("USP_TRN_DEL_PAP_LIV_REST_RECEI", OCon);
            oCmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(oCmd.CommandType);

            try
            {
                oCmd.Parameters.Add("liv_rest_recdid_", ReceivedId);
                oCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                oCmd.ExecuteNonQuery();

                if (oCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = oCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                oCmd.Dispose();
                OCon.Close();
                OCon.Dispose();
            }
            return returnResult;
        }


        //public int DeletePaymentRequest(int PaymentRequestId)
        //{
        //    cnn = new OracleConnection(con);

        //    proc = "USP_TRN_CMP_DEL_SUBMIT_PAYMENT";

        //    cmd = new OracleCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("payt_requestid_", PaymentRequestId);
        //    //cmd.Parameters.Add("Sp_recordset", OracleDbType.Int32).Direction = ParameterDirection.Output;

        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();

        //    return result;
        //}

        //public string UpdatePaymentRequest(LivelihoodRestorationBO oLiveRestPlanBO)
        //{
        //    string returnResult = string.Empty;
        //    cnn = new OracleConnection(con);

        //    proc = "USP_TRN_CMP_UPD_SUBMIT_PAYMENT";

        //    cmd = new OracleCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Connection.Open();
        //    cmd.Parameters.Add("payt_requestid_", oLiveRestPlanBO.Payt_RequestID);
        //    cmd.Parameters.Add("requeststatus_", oLiveRestPlanBO.RequestStatus);
        //    cmd.Parameters.Add("updatedby_", oLiveRestPlanBO.UpdatedBy);           

        //    cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();

        //        if (cmd.Parameters["errorMessage_"].Value != null)
        //            returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
        //        else
        //            returnResult = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        returnResult = string.Empty;
        //        throw ex;
        //    }

        //    return returnResult;
        //}
        //get the data in mst_Concern table using USP_MST_GETSELECTCONCERN-SP signal Data based on ID

        /*  public Concern GetConcernById(int ConcernID)
          {
              OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
              OracleCommand cmd;

              string proc = "USP_MST_GETSELECTCONCERN";

              cmd = new OracleCommand(proc, cnn);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add("ConcernID_", ConcernID);
              cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

              cmd.Connection.Open();

              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              BO.Concern ConcernObj = null;
              BO.ConcernList Users = new BO.ConcernList();

              ConcernObj = new BO.Concern();
              while (dr.Read())
              {
                  if (ColumnExists(dr, "CONCERN") && !dr.IsDBNull(dr.GetOrdinal("CONCERN")))
                      ConcernObj.ConcernName = dr.GetString(dr.GetOrdinal("CONCERN"));
                  if (ColumnExists(dr, "CONCERNID") && !dr.IsDBNull(dr.GetOrdinal("CONCERNID")))
                      ConcernObj.ConcernID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONCERNID")));

              }
              dr.Close();


              return ConcernObj;
          }*/
    }
}