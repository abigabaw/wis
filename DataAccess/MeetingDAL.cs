using System;
using System.Data;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_NAME_MEETING";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_MEETING", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", Meetingobj.HHID);
                dcmd.Parameters.Add("CULTURALPROPID", Meetingobj.CULTURALPROPID);
                dcmd.Parameters.Add("MEETINGDATE", Meetingobj.MEETINGDATE);
                dcmd.Parameters.Add("MEETINGLOCATION", Meetingobj.MEETINGLOCATION);
                dcmd.Parameters.Add("MEETINGPURPOSEID", Meetingobj.MEETINGPURPOSEID);
                dcmd.Parameters.Add("WITNESSNGO", Meetingobj.WITNESSNGO);
                dcmd.Parameters.Add("OPINIONLEADER", Meetingobj.OPINIONLEADER);
                dcmd.Parameters.Add("MINISTRYOFGLSD", Meetingobj.MINISTRYOFGLSD);
                dcmd.Parameters.Add("AESREP", Meetingobj.AESREP);
                dcmd.Parameters.Add("MOUSIGNED", Meetingobj.MOUSIGNED);
                dcmd.Parameters.Add("MEETINGCOMMENTS", Meetingobj.MEETINGCOMMENTS);

                dcmd.Parameters.Add("ISDELETED", Meetingobj.ISDELETED);
                dcmd.Parameters.Add("CREATEDBY", Meetingobj.CREATEDBY);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_MEETING";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CULTURALPROPERTYID_", culturalPropertyID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_MEETING";//"USP_TRN_GET_DAMAGE_CROPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("M_CULTURALMEETID", CULTURALMEETID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_MEETING", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("M_CULTURALMEETID", Meetingobj.CULTURALMEETID);
                dcmd.Parameters.Add("M_CULTURALPROPID", Meetingobj.CULTURALPROPID);
                dcmd.Parameters.Add("M_MEETINGDATE", Meetingobj.MEETINGDATE);
                dcmd.Parameters.Add("M_MEETINGLOCATION", Meetingobj.MEETINGLOCATION);
                dcmd.Parameters.Add("M_MEETINGPURPOSEID", Meetingobj.MEETINGPURPOSEID);
                dcmd.Parameters.Add("M_WITNESSNGO", Meetingobj.WITNESSNGO);
                dcmd.Parameters.Add("M_OPINIONLEADER", Meetingobj.OPINIONLEADER);
                dcmd.Parameters.Add("M_MINISTRYOFGLSD", Meetingobj.MINISTRYOFGLSD);
                dcmd.Parameters.Add("M_AESREP", Meetingobj.AESREP);
                dcmd.Parameters.Add("M_MOUSIGNED", Meetingobj.MOUSIGNED);
                dcmd.Parameters.Add("M_MEETINGCOMMENTS", Meetingobj.MEETINGCOMMENTS);
                dcmd.Parameters.Add("M_UPDATEDBY", Meetingobj.CREATEDBY);

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
