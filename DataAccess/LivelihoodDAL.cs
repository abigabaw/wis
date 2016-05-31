using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LivelihoodDAL
    {
        public string AddLivelihood(LivelihoodBO objLivelihood)
        {
            string returnResult = string.Empty;

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            con.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_INSERTLIVELIHOOD", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("L_itemname", objLivelihood.ITEMNAME);
                dcmd.Parameters.Add("L_createdby", objLivelihood.Createdby);

                //return dcmd.ExecuteNonQuery();

                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;      
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                dcmd.Dispose();
                con.Close();
                con.Dispose();

            }
            return returnResult;
        }

        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public LivelihoodList GetALLLivelihood()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_LIVELIHOOD";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodBO objLivelihood = null;
            LivelihoodList Livelihoods = new LivelihoodList();

            while (dr.Read())
            {
                objLivelihood = new LivelihoodBO();
                objLivelihood.Itemid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ITEMID")));
                objLivelihood.ITEMNAME = dr.GetString(dr.GetOrdinal("ITEMNAME"));
                objLivelihood.Isdeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                Livelihoods.Add(objLivelihood);
            }

            dr.Close();

            return Livelihoods;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <returns></returns>
        public LivelihoodList GetLivelihood()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETLIVELIHOOD";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            LivelihoodBO objLivelihood = null;
            LivelihoodList Livelihoods = new LivelihoodList();

            while (dr.Read())
            {
                objLivelihood = new LivelihoodBO();
                objLivelihood.Itemid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ITEMID")));
                objLivelihood.ITEMNAME = dr.GetString(dr.GetOrdinal("ITEMNAME"));

                Livelihoods.Add(objLivelihood);
            }

            dr.Close();

            return Livelihoods;
        }
        /// <summary>
        /// to update data to database
        /// </summary>
        /// <param name="objLivelihood"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public string UpdateLivelihood(LivelihoodBO objLivelihood, int itemid)
        {
            string returnResult = string.Empty;

            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_MST_UPDATELIVELIHOOD", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("L_ITEMID", itemid);
                dCmd.Parameters.Add("L_itemname", objLivelihood.ITEMNAME);
                dCmd.Parameters.Add("L_createdby", objLivelihood.Createdby);
                //return dCmd.ExecuteNonQuery();

                dCmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                dCmd.ExecuteNonQuery();

                if (dCmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dCmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;      
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return returnResult;
        }
        /// <summary>
        /// to delete data from database
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public string DeleteLivelihood(int itemid)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETELIVELIHOOD", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@L_ITEMID", itemid);
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


            //OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            //conn.Open();
            //OracleCommand dCmd = new OracleCommand("USP_MST_DELETELIVELIHOOD", conn);
            //dCmd.CommandType = CommandType.StoredProcedure;
            //try
            //{
            //    dCmd.Parameters.Add("L_ITEMID",itemid );
            //    return dCmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    dCmd.Dispose();
            //    conn.Close();
            //    conn.Dispose();

            //}
        }

        /// <summary>
        /// to make data obsolete
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLivelihood(int itemid, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_LIVELIHOOD", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ItemId_", itemid);
                myCommand.Parameters.Add("@isdeleted_", IsDeleted);
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