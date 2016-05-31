using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Data;
using System;

namespace WIS_BusinessLogic
{
    public class MeetingBLL
    {
        /// <summary>
        /// To get MEETING PURPOSE
        /// </summary>
        /// <returns></returns>
        public MeetingList getMEETINGPURPOSE()
        {
            MeetingDAL DALobj = new MeetingDAL();

            try
            {
                return DALobj.getMEETINGPURPOSE();
            }
            catch (Exception erromsg)
            {
                throw (erromsg);
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Meetingobj"></param>
        /// <returns></returns>
        public int Insert(Meeting Meetingobj)
        {
            MeetingDAL DALobj = new MeetingDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(Meetingobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Get Culture Property Meetings
        /// </summary>
        /// <param name="culturalPropertyID"></param>
        /// <returns></returns>
        public MeetingList GetCulturePropertyMeetings(int culturalPropertyID)
        {
            MeetingDAL DALobj = new MeetingDAL();
            return DALobj.GetCulturePropertyMeetings(culturalPropertyID);
        }

        /// <summary>
        /// To Get Meeting Data
        /// </summary>
        /// <param name="CULTURALMEETID"></param>
        /// <returns></returns>
        public Meeting GetMeetingData(int CULTURALMEETID)
        {
            MeetingDAL DALobj = new MeetingDAL();
            return DALobj.GetMeetingData(CULTURALMEETID);
        }

        /// <summary>
        /// To Update Meeting
        /// </summary>
        /// <param name="Meetingobj"></param>
        /// <returns></returns>
        public int UpdateMeeting(Meeting Meetingobj)
        {
            MeetingDAL DALobj = new MeetingDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.UpdateMeeting(Meetingobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }
    }
}
