using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class GraveFinishDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch all details
        /// </summary>
        /// <returns></returns>
        public GraveFinishList GetAllGraveFinish()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ALLGRAVEFINISH";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GraveFinishBO objGF = null;
            GraveFinishList objGFList = new GraveFinishList();

            while (dr.Read())
            {
                objGF = new GraveFinishBO();
                objGF.GraveFinishID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRV_FINISHID"))));
                objGF.GraveFinishType = dr.GetValue(dr.GetOrdinal("GRV_FINISHTYPE")).ToString();
                objGF.isDeleted = dr.GetValue(dr.GetOrdinal("isDeleted")).ToString();
                objGFList.Add(objGF);
            }

            dr.Close();
            return objGFList;
        }
        /// <summary>
        /// To fetch all details
        /// </summary>
        /// <returns></returns>
        public GraveFinishList GetGraveFinish()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_GRAVEFINISH";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GraveFinishBO objGF = null; 
            GraveFinishList  objGFList = new GraveFinishList();

            while (dr.Read())
            {
                objGF = new GraveFinishBO();
                objGF.GraveFinishID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRV_FINISHID"))));
                objGF.GraveFinishType  = dr.GetValue(dr.GetOrdinal("GRV_FINISHTYPE")).ToString();
                objGFList.Add(objGF);
            }

            dr.Close();
            return objGFList;
        }
        /// <summary>
        /// To fetch all details by ID
        /// </summary>
        /// <param name="graveID"></param>
        /// <returns></returns>
        public GraveFinishBO GetGraveByID(int graveID)
        {

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_GRAVEFINISHBYID";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("GRV_FINISHID", graveID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            GraveFinishBO objGF = null;
            GraveFinishList objGFList = new GraveFinishList();

            while (dr.Read())
            {
                objGF = new GraveFinishBO();
                objGF.GraveFinishID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GRV_FINISHID"))));
                objGF.GraveFinishType = dr.GetValue(dr.GetOrdinal("GRV_FINISHTYPE")).ToString();
                objGF.isDeleted = dr.GetValue(dr.GetOrdinal("isDeleted")).ToString();
                objGFList.Add(objGF);
            }

            dr.Close();
            return objGF;
        }
        /// <summary>
        /// to save data
        /// </summary>
        /// <param name="objGF"></param>
        /// <returns></returns>
        public string AddGrave(GraveFinishBO objGF)
        {
            string returnResult;

            cnn = new OracleConnection(con);

            proc = "USP_MST_INS_GRAVEFINISH";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("GRV_FINISHTYPE", objGF.GraveFinishType);

            cmd.Parameters.Add("ISDELETEDIN", objGF.IsDeleted);
            cmd.Parameters.Add("CREATEDBY", objGF.CreatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
            //OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            //int result = 0;
            //{
            //    OracleCommand myCommand;
            //    myCommand = new OracleCommand("USP_MST_INS_GRAVEFINISH", con);
            //    myCommand.Connection = con;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("@GRV_FINISHTYPE", OracleDbType.Varchar2, 25).Value = objGF.GraveFinishType;
            //    myCommand.Parameters.Add("@ISDELETEDIN", OracleDbType.Varchar2, 5).Value = "False";
            //    myCommand.Parameters.Add("@CREATEDBY", OracleDbType.Int64, 5).Value = 1;
            //    con.Open();
            //    result = myCommand.ExecuteNonQuery();
            //    con.Close();
            //}
            ////return objRelr;

        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="graveID"></param>
        /// <returns></returns>
        public string DeleteGrave(int graveID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string retrunResult = string.Empty;
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_MST_DEL_GRAVEFINISH", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("GRV_FINISHID", graveID);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                con.Open();
                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    retrunResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                
                con.Close();
            }
            return retrunResult;
        }
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="objGF"></param>
        /// <returns></returns>
        public string UpdateGrave(GraveFinishBO objGF)
        {
            string returnResult;
            cnn = new OracleConnection(con);

            proc = "USP_MST_UPD_GRAVEFINISH";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("GRV_FINISHID", objGF.GraveFinishID);
            cmd.Parameters.Add("GRV_FINISHTYPE", objGF.GraveFinishType);

            cmd.Parameters.Add("UPDATEDBY", objGF.CreatedBy);

            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
            //OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            //int result = 0;
            //{
            //    OracleCommand myCommand;
            //    myCommand = new OracleCommand("USP_MST_UPD_GRAVEFINISH", con);
            //    myCommand.Connection = con;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.Add("GRV_FINISHID", objGF.GraveFinishID);
            //    if (string.IsNullOrEmpty(objGF.GraveFinishType) == true)
            //    {
            //        myCommand.Parameters.Add("GRV_FINISHTYPE", OracleDbType.Varchar2, 250).Value = " ";
            //    }
            //    else
            //    {
            //        myCommand.Parameters.Add("GRV_FINISHTYPE", OracleDbType.Varchar2, 250).Value = objGF.GraveFinishType;
                    
            //    }
            //    myCommand.Parameters.Add("UPDATEDBY", OracleDbType.Int64, 5).Value = 1;              
            //    con.Open();
            //    result = myCommand.ExecuteNonQuery();
            //    con.Close();
              
            //    }
            //return result;
            }
        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGraveFinish(int FloorTypeID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_GRAVEFINISH", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.Add("grv_finishid_", FloorTypeID);
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


    
