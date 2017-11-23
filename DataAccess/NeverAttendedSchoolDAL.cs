using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class NeverAttendedSchoolDAL
    {
        /// <summary>
        /// To Insert Into Never Attended School
        /// </summary>
        /// <param name="NeverAttendedSchoolBOObj"></param>
        /// <returns></returns>
        public string InsertIntoNeverAttendedSchool(NeverAttendedSchoolBO NeverAttendedSchoolBOObj)
        {
            string returnResult = string.Empty;
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            con.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_INSERTNSSCHOOL", con);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                //dcmd.Parameters.AddWithValue("NVR_ATT_SCH_REASONID", NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASONID);
                dcmd.Parameters.AddWithValue("NVR_ATT_SCH_REASON_", NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON);
                dcmd.Parameters.AddWithValue("DESCRIPTION_", NeverAttendedSchoolBOObj.DESCRIPTION);
                //dcmd.Parameters.AddWithValue("ISDELETED", NeverAttendedSchoolBOObj.IsDeleted);
                dcmd.Parameters.AddWithValue("CREATEDBY_", NeverAttendedSchoolBOObj.CreatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;

                //return dcmd.ExecuteNonQuery();
                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
            }
            catch
            {
                throw;
            }
            finally
            {
                dcmd.Dispose();
                con.Close();
                con.Dispose();

            }
            return returnResult;
        }


        //public List<NeverAttendedSchoolBO> FetchGridViewDetails()
        //{
        //    try
        //    {
        //        NeverAttendedSchoolBO NeverAttendedSchoolBOObj = new NeverAttendedSchoolBO();
        //        List<NeverAttendedSchoolBO> ListNASchoolList = new List<NeverAttendedSchoolBO>();

        //        //SqlConnection con = new SqlConnection(connStr);
        //        //con.Open();
        //        //SqlCommand cmd = new SqlCommand("USP_MST_GETNASCHOOLDETAILS", con);
        //        //cmd.CommandType = CommandType.StoredProcedure;
        //        //int count = Convert.ToInt32(cmd.CommandType);

        //        SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
        //        SqlCommand cmd;

        //        string proc = "USP_MST_GETNASCHOOLDETAILS";

        //        cmd = new SqlCommand(proc, cnn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

        //        cmd.Connection.Open();

        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
                

        //        while (dr.Read())
        //        {
        //            NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NVR_ATT_SCH_REASONID")));
        //            NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON = dr.GetString(dr.GetOrdinal("NVR_ATT_SCH_REASON"));
        //            NeverAttendedSchoolBOObj.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
        //            ListNASchoolList.Add(NeverAttendedSchoolBOObj);
        //        }

        //        dr.Close();

        //        return ListNASchoolList;           
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        /// <summary>
        /// To Fetch Never Attended School
        /// </summary>
        /// <returns></returns>
        public NeverAttendedSchoolList FetchNeverAttendedSchool()
        {

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETNASCHOOLDETAILS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            NeverAttendedSchoolBO objNeverAttendedSchool = null;
            NeverAttendedSchoolList NeverAttendedSchoollist = new NeverAttendedSchoolList();

            while (dr.Read())
            {
                objNeverAttendedSchool = new NeverAttendedSchoolBO();
                if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASONID")))
                objNeverAttendedSchool.NVR_ATT_SCH_REASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NVR_ATT_SCH_REASONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASON")))
                objNeverAttendedSchool.NVR_ATT_SCH_REASON = dr.GetString(dr.GetOrdinal("NVR_ATT_SCH_REASON"));
                
                NeverAttendedSchoollist.Add(objNeverAttendedSchool);
            }

            dr.Close();

            return NeverAttendedSchoollist;
        }

        /// <summary>
        /// To Get All Never Attended School
        /// </summary>
        /// <returns></returns>
        public NeverAttendedSchoolList GetAllNeverAttendedSchool()
        {

            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETNASCHOOLDETAILSALL";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            NeverAttendedSchoolBO objNeverAttendedSchool = null;
            NeverAttendedSchoolList NeverAttendedSchoollist = new NeverAttendedSchoolList();

            while (dr.Read())
            {
                objNeverAttendedSchool = new NeverAttendedSchoolBO();
                if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASONID")))
                    objNeverAttendedSchool.NVR_ATT_SCH_REASONID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NVR_ATT_SCH_REASONID")));
                if (!dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASON")))
                    objNeverAttendedSchool.NVR_ATT_SCH_REASON = dr.GetString(dr.GetOrdinal("NVR_ATT_SCH_REASON"));
                if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                    objNeverAttendedSchool.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    objNeverAttendedSchool.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                NeverAttendedSchoollist.Add(objNeverAttendedSchool);
            }

            dr.Close();

            return NeverAttendedSchoollist;
        }

        /// <summary>
        /// To Get Never Attended School By Id
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <returns></returns>
        public NeverAttendedSchoolBO GetNASchoolById(int NASchoolID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETNASCHOOLDETAILSBYID";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("NVRATTSCHREASONID", NASchoolID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            NeverAttendedSchoolBO NeverAttendedSchoolBOObj = null;
            NeverAttendedSchoolList NASchoolList = new NeverAttendedSchoolList();

            NeverAttendedSchoolBOObj = new NeverAttendedSchoolBO();
            while (dr.Read())
            {
                if (ColumnExists(dr, "NVR_ATT_SCH_REASON") && !dr.IsDBNull(dr.GetOrdinal("NVR_ATT_SCH_REASON")))
                    NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON = dr.GetString(dr.GetOrdinal("NVR_ATT_SCH_REASON"));
                if (ColumnExists(dr, "description") && !dr.IsDBNull(dr.GetOrdinal("description")))
                    NeverAttendedSchoolBOObj.DESCRIPTION = Convert.ToString(dr.GetValue(dr.GetOrdinal("description")));

            }
            dr.Close();


            return NeverAttendedSchoolBOObj;
        }

        /// <summary>
        /// To Check Weather Column Exists or Not
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


        //public int DeleteNASchoolById(int NASchoolID)
        //{
        //    SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
        //    SqlCommand cmd;


        //    string proc = "USP_DELETE_NASCHOOLID";

        //    cmd = new SqlCommand(proc, cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("NVRATTSCHREASONID", NASchoolID);
        //    cmd.Connection.Open();

        //    int result = cmd.ExecuteNonQuery();

        //    return result;
        //}

        /// <summary>
        /// To Delete Never Attended School By Id
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <returns></returns>
        public string DeleteNASchoolById(int NASchoolID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_DELETE_NASCHOOLID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("NVRATTSCHREASONID", NASchoolID);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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
        }

        /// <summary>
        /// To EDIT Never Attended SCHOOL
        /// </summary>
        /// <param name="NeverAttendedSchoolBOObj"></param>
        /// <returns></returns>
        public string EDITNASCHOOL(NeverAttendedSchoolBO NeverAttendedSchoolBOObj)
        {
            string returnResult = string.Empty;
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_MST_UPDATENASCHOOL", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("NVRATTSCHREASONID", NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASONID);
                dcmd.Parameters.AddWithValue("NVRATTSCHREASON", NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON);
                dcmd.Parameters.AddWithValue("DESCPT", NeverAttendedSchoolBOObj.DESCRIPTION);
                dcmd.Parameters.AddWithValue("UPDTBY", NeverAttendedSchoolBOObj.UpdatedBy);
                /* cmdd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = dcmd.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
                //return dcmd.ExecuteNonQuery();

                dcmd.ExecuteNonQuery();

                if (dcmd.Parameters["errorMessage_"].Value != null)
                    returnResult = dcmd.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;
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
            return returnResult;
        }

        //newly added
        /// <summary>
        /// To Obsolete Never Attended School
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteNASchool(int NASchoolID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_OBSOLETE_NASCHOOLID", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("NVRATTSCHREASONID", NASchoolID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                /* myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;*/ SqlParameter outputValue = myCommand.Parameters.Add("errorMessage_", SqlDbType.VarChar); outputValue.Size=200; outputValue.Direction = ParameterDirection.Output;
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