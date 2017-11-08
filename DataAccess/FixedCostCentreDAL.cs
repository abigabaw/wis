using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_FIXEDCOSTCENTRE";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (FixedCostCentre.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FixedCostCentreIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FixedCostCentreIN", FixedCostCentre.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_MST_GET_ALL_FIXEDCOSTS";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (FixedCostCentre.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FixedCostCentreIN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FixedCostCentreIN", FixedCostCentre.ToString());
            }
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_INS_FIXEDCOSTCENTRE", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FixedCostCentreIN", objFixedCostCentre.FixedCostCentre);
                if (string.IsNullOrEmpty(objFixedCostCentre.FixedCostCentreDescription) == true)
                {
                    myCommand.Parameters.AddWithValue("@FixedCostCentreDescription", " ");
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@FixedCostCentreDescription", objFixedCostCentre.FixedCostCentreDescription);
                }
                myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objFixedCostCentre.CreatedBy);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DELETEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FixedCostCentreId_", roleId);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FixedCostCentreId_", roleId);
                myCommand.Parameters.AddWithValue("@isdeleted_", IsDeleted);
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
        /// <summary>
        /// to update data
        /// </summary>
        /// <param name="objFixedCostCentre"></param>
        /// <returns></returns>
        public string UpdateFixedCostCentre(FixedCostCentreBO objFixedCostCentre)
        {
            string result = string.Empty;
            {
                SqlConnection myConnection;
                SqlCommand myCommand;
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_UPDATEFCC", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@FixedCostCentreIDIN", objFixedCostCentre.FixedCostCentreID);
                myCommand.Parameters.AddWithValue("@FixedCostCentreIN", objFixedCostCentre.FixedCostCentre);
                if (string.IsNullOrEmpty(objFixedCostCentre.FixedCostCentreDescription) == true)
                {
                    myCommand.Parameters.AddWithValue("@FixedCostCentreDescription", " ");
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@FixedCostCentreDescription", objFixedCostCentre.FixedCostCentreDescription);
                }
                myCommand.Parameters.AddWithValue("@ISDELETEDIN", "False");
                myCommand.Parameters.AddWithValue("@USERIDIN", objFixedCostCentre.UpdatedBy);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETFCCBYID";
            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FixedCostCentreIdIN", roleID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
