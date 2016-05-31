using System;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalNameDAL
    {
        /// <summary>
        /// To Insert MNE Goal Details
        /// </summary>
        /// <param name="GoalNameBOObj"></param>
        /// <returns></returns>
        public string InsertMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            string result = "";
            OracleConnection Con = new OracleConnection(AppConfiguration.ConnectionString);
            Con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_MNE_INSERT_GOAL", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);
            try
            {
                cmd.Parameters.Add("GOALNAME_", GoalNameBOObj.GoalName);
                cmd.Parameters.Add("CREATEDBY_", GoalNameBOObj.CreatedBy);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                Con.Close();
                Con.Dispose();
            }

            return result;
        }

        /// <summary>
        /// To Update MNE Goal Details
        /// </summary>
        /// <param name="GoalNameBOObj"></param>
        /// <returns></returns>
        public string UpdateMNEGoalDetails(MNEGoalBO GoalNameBOObj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_MNE_UPDATE_GOAL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.Add("GOALNAME_", GoalNameBOObj.GoalName);
                dcmd.Parameters.Add("UPDATEDBY_", GoalNameBOObj.CreatedBy);
                dcmd.Parameters.Add("GOALID_", GoalNameBOObj.GoalID);
                dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    result = dcmd.Parameters["errorMessage_"].Value.ToString();

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

            return result;
        }

        /// <summary>
        /// To Get All MNE Goal Name Details
        /// </summary>
        /// <returns></returns>
        public MNEGoalList GetAllMNEGoalNameDetails()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_MNE_GET_ALLGOALNAMES";


            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = null;
            MNEGoalList MNEGoalListObj = new MNEGoalList();
            MNEGoalBOObj = new MNEGoalBO();
            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));
                MNEGoalBOObj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                MNEGoalListObj.Add(MNEGoalBOObj);

            }
            dr.Close();
            return MNEGoalListObj;
        }

        /// <summary>
        /// To Get Active MNE Goal Names
        /// </summary>
        /// <returns></returns>
        public MNEGoalList GetActiveMNEGoalNames()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_MNE_GET_GOALNAMES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = null;
            MNEGoalList MNEGoalListObj = new MNEGoalList();
            MNEGoalBOObj = new MNEGoalBO();
            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));
                MNEGoalListObj.Add(MNEGoalBOObj);

            }
            dr.Close();
            return MNEGoalListObj;
        }

        /// <summary>
        /// To Get MNE Goal Name Details by ID
        /// </summary>
        /// <param name="GOALID"></param>
        /// <returns></returns>
        public MNEGoalBO GetMNEGoalNameDetailsbyID(int GOALID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_MNE_GET_GOALBYID";


            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("GoalID_", GOALID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalBO MNEGoalBOObj = new MNEGoalBO();

            while (dr.Read())
            {
                MNEGoalBOObj = new MNEGoalBO();
                MNEGoalBOObj.GoalID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOALID")));
                MNEGoalBOObj.GoalName = dr.GetString(dr.GetOrdinal("GOALNAME"));

            }
            dr.Close();
            return MNEGoalBOObj;

        }

        /// <summary>
        /// To Delete Goal Name
        /// </summary>
        /// <param name="GoalID"></param>
        /// <returns></returns>
        public string DeleteGoalName(int GoalID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = "";
            try
            {

                string proc = "USP_MST_MNE_DEL_GOALNAME";
                cmd = new OracleCommand(proc, cnn);
                int Count = Convert.ToInt32(cmd.CommandType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("c_GoalID ", GoalID);
                cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["errorMessage_"].Value != null)
                    result = cmd.Parameters["errorMessage_"].Value.ToString();

            }
            catch
            {
                throw;
            }
            finally
            {

                cnn.Close();
                cnn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// To Obsolete Goal Name
        /// </summary>
        /// <param name="goalID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGoalName(int goalID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_MNE_OBSOLETEGOALNAME", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("goalID_", goalID);
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
