using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class GraveFinishDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        /// <summary>
        /// To fetch all details
        /// </summary>
        /// <returns></returns>
        public GraveFinishList GetAllGraveFinish()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ALLGRAVEFINISH";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_GRAVEFINISH";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_GRAVEFINISHBYID";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("GRV_FINISHID", graveID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            cnn = new SqlConnection(con);

            proc = "USP_MST_INS_GRAVEFINISH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("GRV_FINISHTYPE", objGF.GraveFinishType);

            cmd.Parameters.AddWithValue("ISDELETEDIN", objGF.IsDeleted);
            cmd.Parameters.AddWithValue("CREATEDBY", objGF.CreatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
            //SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            //int result = 0;
            //{
            //    SqlCommand myCommand;
            //    myCommand = new SqlCommand("USP_MST_INS_GRAVEFINISH", con);
            //    myCommand.Connection = con;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.AddWithValue("@GRV_FINISHTYPE", SqlDbType.NVarChar).Value = objGF.GraveFinishType;
            //    myCommand.Parameters.AddWithValue("@ISDELETEDIN", SqlDbType.NVarChar).Value = "False";
            //    myCommand.Parameters.AddWithValue("@CREATEDBY", SqlDbType.BigInt, 5).Value = 1;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            string retrunResult = string.Empty;
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_MST_DEL_GRAVEFINISH", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("GRV_FINISHID", graveID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            cnn = new SqlConnection(con);

            proc = "USP_MST_UPD_GRAVEFINISH";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("GRV_FINISHID", objGF.GraveFinishID);
            cmd.Parameters.AddWithValue("GRV_FINISHTYPE", objGF.GraveFinishType);

            cmd.Parameters.AddWithValue("UPDATEDBY", objGF.CreatedBy);

            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            return returnResult;
            //SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            //int result = 0;
            //{
            //    SqlCommand myCommand;
            //    myCommand = new SqlCommand("USP_MST_UPD_GRAVEFINISH", con);
            //    myCommand.Connection = con;
            //    myCommand.CommandType = CommandType.StoredProcedure;
            //    myCommand.Parameters.AddWithValue("GRV_FINISHID", objGF.GraveFinishID);
            //    if (string.IsNullOrEmpty(objGF.GraveFinishType) == true)
            //    {
            //        myCommand.Parameters.AddWithValue("GRV_FINISHTYPE", SqlDbType.NVarChar).Value = " ";
            //    }
            //    else
            //    {
            //        myCommand.Parameters.AddWithValue("GRV_FINISHTYPE", SqlDbType.NVarChar).Value = objGF.GraveFinishType;
                    
            //    }
            //    myCommand.Parameters.AddWithValue("UPDATEDBY", SqlDbType.BigInt, 5).Value = 1;              
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_GRAVEFINISH", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("grv_finishid_", FloorTypeID);
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


    
