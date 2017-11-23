using System;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_CONSULTANT";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (projectID > 0)
                cmd.Parameters.AddWithValue("PROJECTID_", projectID);
            else
                cmd.Parameters.AddWithValue("PROJECTID_", DBNull.Value);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_CONSULTANTBYID";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("CONSULTANTID", ConID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);

            SqlCommand myCommand;
            myCommand = new SqlCommand("USP_TRN_INS_CONSULTANT", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("CONSULTANTNAME_", objCon.ConsultName);
            myCommand.Parameters.AddWithValue("PROJECTID_", objCon.ProjectID);
            myCommand.Parameters.AddWithValue("CONSULTANTTYPE_", objCon.ConsultType);
            myCommand.Parameters.AddWithValue("ADDRESS_", objCon.Address);
            myCommand.Parameters.AddWithValue("CONTACTNUMBER_", objCon.ConNumber);
            myCommand.Parameters.AddWithValue("CONTACTPERSON_", objCon.ConPerson);
            myCommand.Parameters.AddWithValue("EMAILADDRESS_", objCon.EmailAddress);
            myCommand.Parameters.AddWithValue("ISDELETEDIN_", "False");
            myCommand.Parameters.AddWithValue("CREATEDBY_", objCon.CreatedBy);
            /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_DEL_CONSULTANT", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CONSULTANTID_", ConID);
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);

            SqlCommand myCommand;
            myCommand = new SqlCommand("USP_TRN_OBSOLETE_CONSULTANT", con);
            myCommand.Connection = con;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("CONSULTANTID_", consultantID);
            myCommand.Parameters.AddWithValue("ISOBSOLETE_", isObsolete);
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_UPD_CONSULTANT", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("CONSULTANTID", objCon.ConsultID);
              
                myCommand.Parameters.AddWithValue("@CONSULTANTNAME", objCon.ConsultName);
                myCommand.Parameters.AddWithValue("@CONSULTATIONTYPE", objCon.ConsultType);
                myCommand.Parameters.AddWithValue("@ADDRESS", objCon.Address);
                myCommand.Parameters.AddWithValue("@CONTACTPERSON", objCon.ConPerson);
                myCommand.Parameters.AddWithValue("@CONTACTNUMBER", objCon.ConNumber);
                myCommand.Parameters.AddWithValue("@EMAILADDRESS", objCon.EmailAddress);
                myCommand.Parameters.AddWithValue("UPDATEDBY", objCon.UpdatedBy);

                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    }
}