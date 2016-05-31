using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class ConsultantDAL
    {
        /// <summary>
        /// To Get Consultant
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ConsultantList GetConsultant(int projectID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_CONSULTANT";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (projectID > 0)
                cmd.Parameters.Add("PROJECTID_", projectID);
            else
                cmd.Parameters.Add("PROJECTID_", DBNull.Value);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConsultantBO objCon = null;
            ConsultantList objConList = new ConsultantList();

            while (dr.Read())
            {
                objCon = new ConsultantBO();
                 objCon.ConsultID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONSULTANTID"))));
                 objCon.ConsultName = dr.GetValue(dr.GetOrdinal("CONSULTANTNAME")).ToString();
                 objCon.ConsultType = dr.GetValue(dr.GetOrdinal("CONSULTATIONTYPE")).ToString();
                 objCon.ConPerson = dr.GetValue(dr.GetOrdinal("CONTACTPERSON")).ToString();
                 objCon.ConNumber = dr.GetValue(dr.GetOrdinal("CONTACTNUMBER")).ToString();
                 objCon.Address = dr.GetValue(dr.GetOrdinal("ADDRESS")).ToString();
                 objCon.EmailAddress = dr.GetValue(dr.GetOrdinal("EMAILADDRESS")).ToString();
                 objCon.IsDeleted = dr.GetString(dr.GetOrdinal("IsDeleted"));
                objConList.Add(objCon);
            }

            dr.Close();
            return objConList;
        }

        /// <summary>
        /// To Get Consultant By ID
        /// </summary>
        public ConsultantBO GetConsultantByID(int ConID)
        {

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_CONSULTANTBYID";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CONSULTANTID", ConID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ConsultantBO objCon = null;
            ConsultantList objConList = new ConsultantList();

            while (dr.Read())
            {
                objCon = new ConsultantBO();
                objCon.ConsultID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CONSULTANTID"))));
                objCon.ConsultName = dr.GetValue(dr.GetOrdinal("CONSULTANTNAME")).ToString();
                objCon.ConsultType = dr.GetValue(dr.GetOrdinal("CONSULTATIONTYPE")).ToString();
                objCon.ConPerson = dr.GetValue(dr.GetOrdinal("CONTACTPERSON")).ToString();
                objCon.ConNumber = dr.GetValue(dr.GetOrdinal("CONTACTNUMBER")).ToString();
                objCon.Address = dr.GetValue(dr.GetOrdinal("ADDRESS")).ToString();
                objCon.EmailAddress = dr.GetValue(dr.GetOrdinal("EMAILADDRESS")).ToString();
                objConList.Add(objCon);
            }

            dr.Close();
            return objCon;
        }

        /// <summary>
        /// To Add Consultant
        /// </summary>
        /// <param name="objCon"></param>
        /// <returns></returns>
        public string AddConsultant(ConsultantBO objCon)
        {
            string result = "";

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);

            OracleCommand myCommand;
            myCommand = new OracleCommand("USP_TRN_INS_CONSULTANT", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("CONSULTANTNAME_", objCon.ConsultName);
            myCommand.Parameters.Add("PROJECTID_", objCon.ProjectID);
            myCommand.Parameters.Add("CONSULTANTTYPE_", objCon.ConsultType);
            myCommand.Parameters.Add("ADDRESS_", objCon.Address);
            myCommand.Parameters.Add("CONTACTNUMBER_", objCon.ConNumber);
            myCommand.Parameters.Add("CONTACTPERSON_", objCon.ConPerson);
            myCommand.Parameters.Add("EMAILADDRESS_", objCon.EmailAddress);
            myCommand.Parameters.Add("ISDELETEDIN_", "False");
            myCommand.Parameters.Add("CREATEDBY_", objCon.CreatedBy);
            myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            con.Open();
            myCommand.ExecuteNonQuery();
            con.Close();

            if (myCommand.Parameters["errorMessage_"].Value != null)
                result = myCommand.Parameters["errorMessage_"].Value.ToString();

            return result;
        }

        /// <summary>
        /// To Delete Consultant
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>
        public int DeleteConsultant(int ConID)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_DEL_CONSULTANT", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CONSULTANTID_", ConID);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// To Obsolete Consultant
        /// </summary>
        /// <param name="consultantID"></param>
        /// <param name="isObsolete"></param>
        public void ObsoleteConsultant(int consultantID, string isObsolete)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);

            OracleCommand myCommand;
            myCommand = new OracleCommand("USP_TRN_OBSOLETE_CONSULTANT", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("CONSULTANTID_", consultantID);
            myCommand.Parameters.Add("ISOBSOLETE_", isObsolete);
            con.Open();
            myCommand.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// To Update Consultant
        /// </summary>
        /// <param name="objCon"></param>
        /// <returns></returns>
        public int UpdateConsultant(ConsultantBO objCon)
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_CONSULTANT", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("CONSULTANTID", objCon.ConsultID);
              
                myCommand.Parameters.Add("@CONSULTANTNAME", objCon.ConsultName);
                myCommand.Parameters.Add("@CONSULTATIONTYPE", objCon.ConsultType);
                myCommand.Parameters.Add("@ADDRESS", objCon.Address);
                myCommand.Parameters.Add("@CONTACTPERSON", objCon.ConPerson);
                myCommand.Parameters.Add("@CONTACTNUMBER", objCon.ConNumber);
                myCommand.Parameters.Add("@EMAILADDRESS", objCon.EmailAddress);
                myCommand.Parameters.Add("UPDATEDBY", objCon.UpdatedBy);

                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    }
}