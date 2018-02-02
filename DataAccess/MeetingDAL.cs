using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class MeetingDAL
    {
        /// <summary>
        /// To get MEETING PURPOSE
        /// </summary>
        /// <returns></returns>
        public MeetingList getMEETINGPURPOSE()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_NAME_MEETING";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Meeting BOobj = null;
            MeetingList Listobj = new MeetingList();

            while (dr.Read())
            {
                BOobj = new Meeting();
                BOobj.MEETINGPURPOSEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MEETINGPURPOSEID"))));
                BOobj.Meetingpurpose = dr.GetValue(dr.GetOrdinal("MEETINGPURPOSE")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Meetingobj"></param>
        /// <returns></returns>
        public int Insert(Meeting Meetingobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INS_MEETING", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID", Meetingobj.HHID);
                dcmd.Parameters.AddWithValue("CULTURALPROPID", Meetingobj.CULTURALPROPID);
                dcmd.Parameters.AddWithValue("MEETINGDATE", Meetingobj.MEETINGDATE);
                dcmd.Parameters.AddWithValue("MEETINGLOCATION", Meetingobj.MEETINGLOCATION);
                dcmd.Parameters.AddWithValue("MEETINGPURPOSEID", Meetingobj.MEETINGPURPOSEID);
                dcmd.Parameters.AddWithValue("WITNESSNGO", Meetingobj.WITNESSNGO);
                dcmd.Parameters.AddWithValue("OPINIONLEADER", Meetingobj.OPINIONLEADER);
                dcmd.Parameters.AddWithValue("MINISTRYOFGLSD", Meetingobj.MINISTRYOFGLSD);
                dcmd.Parameters.AddWithValue("AESREP", Meetingobj.AESREP);
                dcmd.Parameters.AddWithValue("MOUSIGNED", Meetingobj.MOUSIGNED);
                dcmd.Parameters.AddWithValue("MEETINGCOMMENTS", Meetingobj.MEETINGCOMMENTS);

                dcmd.Parameters.AddWithValue("ISDELETED", Meetingobj.ISDELETED);
                dcmd.Parameters.AddWithValue("CREATEDBY", Meetingobj.CREATEDBY);

                return dcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();

            }
        }

        /// <summary>
        /// To Get Culture Property Meetings
        /// </summary>
        /// <param name="culturalPropertyID"></param>
        /// <returns></returns>
        public MeetingList GetCulturePropertyMeetings(int culturalPropertyID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_MEETING";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CULTURALPROPERTYID_", culturalPropertyID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Meeting Meetingobj = null;
            MeetingList Listobj = new MeetingList();

            while (dr.Read())
            {
                Meetingobj = new Meeting();
                if (!dr.IsDBNull(dr.GetOrdinal("culturalmeetid")))
                Meetingobj.CULTURALMEETID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("culturalmeetid")));
                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGDATE")))
               
                    Meetingobj.MEETINGDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("meetingdate")));

                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGLOCATION")))
                {
                    Meetingobj.MEETINGLOCATION = dr.GetString(dr.GetOrdinal("meetinglocation"));
                }

                    Meetingobj.Meetingpurpose = dr.GetString(dr.GetOrdinal("meetingpurpose"));
              
               
                if (!dr.IsDBNull(dr.GetOrdinal("WITNESSNGO")))
                {
                    Meetingobj.WITNESSNGO = dr.GetString(dr.GetOrdinal("witnessngo"));
                }

                if (!dr.IsDBNull(dr.GetOrdinal("OPINIONLEADER")))
                {
                    Meetingobj.OPINIONLEADER = dr.GetString(dr.GetOrdinal("opinionleader"));
                }

                if (!dr.IsDBNull(dr.GetOrdinal("MINISTRYOFGLSD")))
                {
                    Meetingobj.MINISTRYOFGLSD = dr.GetString(dr.GetOrdinal("ministryofglsd"));
                }

                if (!dr.IsDBNull(dr.GetOrdinal("AESREP")))
                {
                    Meetingobj.AESREP = dr.GetString(dr.GetOrdinal("aesrep"));
                }

                if (!dr.IsDBNull(dr.GetOrdinal("MOUSIGNED")))
                {
                    Meetingobj.MOUSIGNED = dr.GetString(dr.GetOrdinal("mousigned"));
                }
                else
                {
                    Meetingobj.MOUSIGNED = "No";
                }

                Listobj.Add(Meetingobj);
            }

            dr.Close();

            return Listobj;
        }

        /// <summary>
        /// To Get Meeting Data
        /// </summary>
        /// <param name="CULTURALMEETID"></param>
        /// <returns></returns>
        public Meeting GetMeetingData(int CULTURALMEETID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_MEETING";//"USP_TRN_GET_DAMAGE_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("M_CULTURALMEETID", CULTURALMEETID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Meeting Meetingobj = null;
            MeetingList Listobj = new MeetingList();

            Meetingobj = new Meeting();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("culturalpropid")))
                    Meetingobj.CULTURALPROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("culturalpropid")));


                if (!dr.IsDBNull(dr.GetOrdinal("culturalmeetid")))
                    Meetingobj.CULTURALMEETID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("culturalmeetid")));

                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGDATE")))
                    Meetingobj.MEETINGDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("MEETINGDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGLOCATION")))
                    Meetingobj.MEETINGLOCATION = dr.GetString(dr.GetOrdinal("MEETINGLOCATION"));

                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGPURPOSEID")))
                    Meetingobj.MEETINGPURPOSEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MEETINGPURPOSEID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WITNESSNGO")))
                    Meetingobj.WITNESSNGO = dr.GetString(dr.GetOrdinal("WITNESSNGO"));

                if (!dr.IsDBNull(dr.GetOrdinal("OPINIONLEADER")))
                    Meetingobj.OPINIONLEADER = dr.GetString(dr.GetOrdinal("OPINIONLEADER"));

                if (!dr.IsDBNull(dr.GetOrdinal("MINISTRYOFGLSD")))
                    Meetingobj.MINISTRYOFGLSD = dr.GetString(dr.GetOrdinal("MINISTRYOFGLSD"));

                if (!dr.IsDBNull(dr.GetOrdinal("AESREP")))
                    Meetingobj.AESREP = dr.GetString(dr.GetOrdinal("AESREP"));

                if (!dr.IsDBNull(dr.GetOrdinal("MOUSIGNED")))
                    Meetingobj.MOUSIGNED = dr.GetString(dr.GetOrdinal("MOUSIGNED"));

                if (!dr.IsDBNull(dr.GetOrdinal("MEETINGCOMMENTS")))
                    Meetingobj.MEETINGCOMMENTS = dr.GetString(dr.GetOrdinal("MEETINGCOMMENTS"));
            }
            dr.Close();

            return Meetingobj;
        }

        /// <summary>
        /// To Update Meeting
        /// </summary>
        /// <param name="Meetingobj"></param>
        /// <returns></returns>
        public int UpdateMeeting(Meeting Meetingobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_MEETING", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("M_CULTURALMEETID", Meetingobj.CULTURALMEETID);
                dcmd.Parameters.AddWithValue("M_CULTURALPROPID", Meetingobj.CULTURALPROPID);
                dcmd.Parameters.AddWithValue("M_MEETINGDATE", Meetingobj.MEETINGDATE);
                dcmd.Parameters.AddWithValue("M_MEETINGLOCATION", Meetingobj.MEETINGLOCATION);
                dcmd.Parameters.AddWithValue("M_MEETINGPURPOSEID", Meetingobj.MEETINGPURPOSEID);
                dcmd.Parameters.AddWithValue("M_WITNESSNGO", Meetingobj.WITNESSNGO);
                dcmd.Parameters.AddWithValue("M_OPINIONLEADER", Meetingobj.OPINIONLEADER);
                dcmd.Parameters.AddWithValue("M_MINISTRYOFGLSD", Meetingobj.MINISTRYOFGLSD);
                dcmd.Parameters.AddWithValue("M_AESREP", Meetingobj.AESREP);
                dcmd.Parameters.AddWithValue("M_MOUSIGNED", Meetingobj.MOUSIGNED);
                dcmd.Parameters.AddWithValue("M_MEETINGCOMMENTS", Meetingobj.MEETINGCOMMENTS);
                dcmd.Parameters.AddWithValue("M_UPDATEDBY", Meetingobj.CREATEDBY);

                return dcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dcmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }
    }
}
