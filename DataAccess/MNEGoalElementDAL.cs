using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using Oracle.DataAccess.Client;
using System.Data;

namespace WIS_DataAccess
{
    public class MNEGoalElementDAL
    {
        /// <summary>
        /// To Insert MNE Goal Element Details
        /// </summary>
        /// <param name="GoalElementBOObj"></param>
        /// <returns></returns>
        public string InsertMNEGoalElementDetails(MNEGoalElementBOL GoalElementBOObj)
        {
            string result = "";
            OracleConnection Con = new OracleConnection(AppConfiguration.ConnectionString);
            Con.Open();
            OracleCommand cmd = new OracleCommand("USP_MST_MNE_INSERT_GOALELEMENT", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            int Count = Convert.ToInt32(cmd.CommandType);
            try
            {
                cmd.Parameters.Add(" GOAL_ELEMENTNAME", GoalElementBOObj.GoalElement);
                cmd.Parameters.Add("CREATEDBY", GoalElementBOObj.CreatedBy);
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
        /// To Update MNE Goal Element Details
        /// </summary>
        /// <param name="GoalElementBOObj"></param>
        /// <returns></returns>
        public string UpdateMNEGoalElementDetails(MNEGoalElementBOL GoalElementBOObj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_MST_MNE_UPDATE_GOALELEMENT ", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            string result = "";

            try
            {
                dcmd.Parameters.Add("GOAL_ELEMENTNAME_", GoalElementBOObj.GoalElement);
                dcmd.Parameters.Add("UPDATEDBY_", GoalElementBOObj.CreatedBy);
                dcmd.Parameters.Add("GOAL_ELEMENTID_", GoalElementBOObj.GoalElementID);
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
        /// To Get All MNE Goal Element Details
        /// </summary>
        /// <returns></returns>
        public MNEGoalElementList GetAllMNEGoalElementDetails()
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = " USP_MST_MNE_GET_ALLELEMENTS ";


            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalElementBOL MNEGoalElementBOLObj = null;
            MNEGoalElementList MNEGoalElementListObj = new MNEGoalElementList();
            MNEGoalElementBOLObj = new MNEGoalElementBOL();
            while (dr.Read())
            {
                MNEGoalElementBOLObj = new MNEGoalElementBOL();
                MNEGoalElementBOLObj.GoalElementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOAL_ELEMENTID")));
                MNEGoalElementBOLObj.GoalElement = dr.GetString(dr.GetOrdinal("GOAL_ELEMENTNAME"));
                MNEGoalElementBOLObj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));
                MNEGoalElementListObj.Add(MNEGoalElementBOLObj);

            }
            dr.Close();
            return MNEGoalElementListObj;
        }

        /// <summary>
        /// To Get MNE Goal Element Details by ID
        /// </summary>
        /// <param name="GOALElementID"></param>
        /// <returns></returns>
        public MNEGoalElementBOL GetMNEGoalElementDetailsbyID(int GOALElementID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_MST_MNE_GETGOALELEMENT";


            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("GoalElementID_", GOALElementID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            MNEGoalElementBOL MNEGoalElementBOLObj = new MNEGoalElementBOL();

            while (dr.Read())
            {
                MNEGoalElementBOLObj = new MNEGoalElementBOL();

                MNEGoalElementBOLObj.GoalElementID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("GOAL_ELEMENTID")));
                MNEGoalElementBOLObj.GoalElement = dr.GetString(dr.GetOrdinal("GOAL_ELEMENTNAME"));

            }
            dr.Close();
            return MNEGoalElementBOLObj;

        }

        /// <summary>
        /// To Delete Goal Element
        /// </summary>
        /// <param name="GoalElementID"></param>
        /// <returns></returns>
        public string DeleteGoalElement(int GoalElementID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string result = "";
            try
            {

                string proc = "USP_MST_MNE_DEL_GOALELEMENT";
                cmd = new OracleCommand(proc, cnn);
                int Count = Convert.ToInt32(cmd.CommandType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("C_GoalElementID ", GoalElementID);
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
        /// To Obsolete Goal Element
        /// </summary>
        /// <param name="goalID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteGoalElement(int goalID, string IsDeleted)
        {
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_MNE_OBSOLUTEELEMENT", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("GoalElementID_", goalID);
                myCommand.Parameters.Add("Isdeleted_", IsDeleted);
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
        /// To LoadMNEGoalElement
        /// </summary>
        /// <returns></returns>
        public MNEGoalElementList LoadMNEGoalElement()
        {
            string con = AppConfiguration.ConnectionString;
            OracleConnection cnn;
            OracleCommand cmd;
            string proc = string.Empty;
            cnn = new OracleConnection(AppConfiguration.ConnectionString);
            proc = "USP_LOAD_MNEGOALELEMENTS";
            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            MNEGoalElementBOL objMNEGoalElementBOL = null;
            MNEGoalElementList MNEGoalEvalElements = new MNEGoalElementList();
            while (dr.Read())
            {
                objMNEGoalElementBOL = new MNEGoalElementBOL();
                if (!dr.IsDBNull(dr.GetOrdinal("ID"))) objMNEGoalElementBOL.GoalElementID = dr.GetInt32(dr.GetOrdinal("ID"));
                if (!dr.IsDBNull(dr.GetOrdinal("Name"))) objMNEGoalElementBOL.GoalElement = dr.GetString(dr.GetOrdinal("Name"));

                MNEGoalEvalElements.Add(objMNEGoalElementBOL);
            }
            dr.Close();
            return MNEGoalEvalElements;
        }
    }
}
