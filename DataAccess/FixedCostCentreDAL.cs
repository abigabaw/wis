using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;
using System.Data.Sql;

namespace WIS_DataAccess
{
    public class FixedCostCentreDAL
    {
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <param name="FixedCostCentre"></param>
        /// <returns></returns>
        public FixedCostCentreList GetFixedCostCentre(string FixedCostCentre)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_FIXEDCOSTCENTRE";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (FixedCostCentre.ToString() == "")
            {
                cmd.Parameters.Add("@FixedCostCentreIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@FixedCostCentreIN", FixedCostCentre.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FixedCostCentreBO objFixedCostCentre = null;
            FixedCostCentreList FixedCostCentres = new FixedCostCentreList();
            while (dr.Read())
            {
                objFixedCostCentre = new FixedCostCentreBO();
                objFixedCostCentre.FixedCostCentreID = dr.GetInt16(dr.GetOrdinal("FixedCostCentreId"));
                objFixedCostCentre.FixedCostCentre = dr.GetString(dr.GetOrdinal("FixedCostCentre"));
                objFixedCostCentre.FixedCostCentreDescription = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                objFixedCostCentre.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                FixedCostCentres.Add(objFixedCostCentre);
            }
            dr.Close();
            return FixedCostCentres;
        }
        /// <summary>
        /// to fetch details
        /// </summary>
        /// <param name="FixedCostCentre"></param>
        /// <returns></returns>
        public FixedCostCentreList GetAllFixedCostCentre(string FixedCostCentre)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_GET_ALL_FIXEDCOSTS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (FixedCostCentre.ToString() == "")
            {
                cmd.Parameters.Add("@FixedCostCentreIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@FixedCostCentreIN", FixedCostCentre.ToString());
            }
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FixedCostCentreBO objFixedCostCentre = null;
            FixedCostCentreList FixedCostCentres = new FixedCostCentreList();
            while (dr.Read())
            {
                objFixedCostCentre = new FixedCostCentreBO();
                objFixedCostCentre.FixedCostCentreID = dr.GetInt16(dr.GetOrdinal("FixedCostCentreId"));
                objFixedCostCentre.FixedCostCentre = dr.GetString(dr.GetOrdinal("FixedCostCentre"));
                objFixedCostCentre.FixedCostCentreDescription = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                objFixedCostCentre.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                FixedCostCentres.Add(objFixedCostCentre);
            }
            dr.Close();
            return FixedCostCentres;
        }
        /// <summary>
        /// to save data 
        /// </summary>
        /// <param name="objFixedCostCentre"></param>
        /// <returns></returns>
        public string AddFixedCostCentre(FixedCostCentreBO objFixedCostCentre)
        {
            string result = string.Empty;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_INS_FIXEDCOSTCENTRE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@FixedCostCentreIN", objFixedCostCentre.FixedCostCentre);
                if (string.IsNullOrEmpty(objFixedCostCentre.FixedCostCentreDescription) == true)
                {
                    myCommand.Parameters.Add("@FixedCostCentreDescription", " ");
                }
                else
                {
                    myCommand.Parameters.Add("@FixedCostCentreDescription", objFixedCostCentre.FixedCostCentreDescription);
                }
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objFixedCostCentre.CreatedBy);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;

                myConnection.Close();
            }
            return result;
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string DeleteFixedCostCentre(int roleId)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DELETEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@FixedCostCentreId_", roleId);
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
                    result = "Selected Fixed Cost Centre is already in use. Connot delete";
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
        /// to make data obsolete
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFixedCostCentre(int roleId, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@FixedCostCentreId_", roleId);
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
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="objFixedCostCentre"></param>
        /// <returns></returns>
        public string UpdateFixedCostCentre(FixedCostCentreBO objFixedCostCentre)
        {
            string result = string.Empty;
            {
                OracleConnection myConnection;
                OracleCommand myCommand;
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_UPDATEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@FixedCostCentreIDIN", objFixedCostCentre.FixedCostCentreID);
                myCommand.Parameters.Add("@FixedCostCentreIN", objFixedCostCentre.FixedCostCentre);
                if (string.IsNullOrEmpty(objFixedCostCentre.FixedCostCentreDescription) == true)
                {
                    myCommand.Parameters.Add("@FixedCostCentreDescription", " ");
                }
                else
                {
                    myCommand.Parameters.Add("@FixedCostCentreDescription", objFixedCostCentre.FixedCostCentreDescription);
                }
                myCommand.Parameters.Add("@ISDELETEDIN", "False");
                myCommand.Parameters.Add("@USERIDIN", objFixedCostCentre.UpdatedBy);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                myConnection.Open();

                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    result = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    result = string.Empty;

                myConnection.Close();
            }
            return result;
        }
        /// <summary>
        /// to fetch details by ID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public FixedCostCentreBO GetFixedCostCentreByFixedCostCentreID(int roleID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETFCCBYID";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FixedCostCentreIdIN", roleID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            FixedCostCentreBO obFixedCostCentre = null;
            while (dr.Read())
            {
                obFixedCostCentre = new FixedCostCentreBO();
                obFixedCostCentre.FixedCostCentreID = dr.GetInt16(dr.GetOrdinal("FixedCostCentreId"));
                obFixedCostCentre.FixedCostCentre = dr.GetString(dr.GetOrdinal("FixedCostCentre"));
                obFixedCostCentre.FixedCostCentreDescription = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
            }
            dr.Close();
            return obFixedCostCentre;
        }  
    }
}
