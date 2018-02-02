using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class NegoDAL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Negoobj"></param>
        /// <returns></returns>
        public int Insert(Nego Negoobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_INSERT_NGO", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.AddWithValue("HHID", Negoobj.HHID);
                dcmd.Parameters.AddWithValue("CULTURALPROPID", Negoobj.CULTURALPROPID);
                dcmd.Parameters.AddWithValue("NEGO_APPOINTMENTDATE", Negoobj.NEGO_APPOINTMENTDATE);
                dcmd.Parameters.AddWithValue("NEGO_VENUE", Negoobj.NEGO_VENUE);
                dcmd.Parameters.AddWithValue("NEGO_DATE", Negoobj.NEGO_DATE);

                if (Negoobj.NEGO_PROBLEMDESC.Trim().Length > 1000)
                    dcmd.Parameters.AddWithValue("NEGO_PROBLEMDESC", Negoobj.NEGO_PROBLEMDESC.Substring(0, 1000));
                else
                    dcmd.Parameters.AddWithValue("NEGO_PROBLEMDESC", Negoobj.NEGO_PROBLEMDESC);

                dcmd.Parameters.AddWithValue("ISDELETED", Negoobj.ISDELETED);
                dcmd.Parameters.AddWithValue("CREATEDBY", Negoobj.CREATEDBY);

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
        /// To Get NGO DATA
        /// </summary>
        /// <param name="culturalPropertyID"></param>
        /// <returns></returns>
        public NegoList GetNGODATA(int culturalPropertyID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_NGO";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CULTURALPROPERTYID_", culturalPropertyID);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Nego Ngoobj = null;
            NegoList Listobj = new NegoList();

            while (dr.Read())
            {
                Ngoobj = new Nego();

                if (!dr.IsDBNull(dr.GetOrdinal("culturalnegoid")))
                Ngoobj.CULTURALNEGOID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("culturalnegoid")));

                if (!dr.IsDBNull(dr.GetOrdinal("nego_appointmentdate")))
                Ngoobj.NEGO_APPOINTMENTDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("nego_appointmentdate")));

                if (!dr.IsDBNull(dr.GetOrdinal("nego_venue")))
                Ngoobj.NEGO_VENUE = dr.GetString(dr.GetOrdinal("nego_venue"));

                if (!dr.IsDBNull(dr.GetOrdinal("nego_date")))
                Ngoobj.NEGO_DATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("nego_date")));

                if (!dr.IsDBNull(dr.GetOrdinal("nego_problemdesc")))
                {
                    Ngoobj.NEGO_PROBLEMDESC = dr.GetString(dr.GetOrdinal("nego_problemdesc"));
                }               

                Listobj.Add(Ngoobj);
            }

            dr.Close();

            return Listobj;
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="CULTURALNEGOID"></param>
        /// <returns></returns>
        public Nego GetData(int CULTURALNEGOID)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_NGO";//"USP_TRN_GET_DAMAGE_CROPS";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("N_CULTURALNEGOID", CULTURALNEGOID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Nego Ngoobj = null;
            NegoList Listobj = new NegoList();

            Ngoobj = new Nego();
            while (dr.Read())
            {

                if (!dr.IsDBNull(dr.GetOrdinal("CULTURALNEGOID")))
                    Ngoobj.CULTURALNEGOID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTURALNEGOID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CULTURALPROPID")))
                    Ngoobj.CULTURALPROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTURALPROPID")));



                if (!dr.IsDBNull(dr.GetOrdinal("NEGO_APPOINTMENTDATE")))
                    Ngoobj.NEGO_APPOINTMENTDATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("NEGO_APPOINTMENTDATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("NEGO_VENUE")))
                    Ngoobj.NEGO_VENUE = dr.GetString(dr.GetOrdinal("NEGO_VENUE"));

                if (!dr.IsDBNull(dr.GetOrdinal("NEGO_DATE")))
                    Ngoobj.NEGO_DATE = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("NEGO_DATE")));

                if (!dr.IsDBNull(dr.GetOrdinal("NEGO_PROBLEMDESC")))
                    Ngoobj.NEGO_PROBLEMDESC = dr.GetString(dr.GetOrdinal("NEGO_PROBLEMDESC"));




            }
            dr.Close();


            return Ngoobj;
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="Negoobj"></param>
        /// <returns></returns>
        public int Update(Nego Negoobj)
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_NGO", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                // dcmd.Parameters.AddWithValue("HHID", PermanentStructureobj.HouseholdID);
                dcmd.Parameters.AddWithValue("N_CULTURALNEGOID", Negoobj.CULTURALNEGOID);
                dcmd.Parameters.AddWithValue("N_CULTURALPROPID", Negoobj.CULTURALPROPID);

                dcmd.Parameters.AddWithValue("N_NEGO_APPOINTMENTDATE", Negoobj.NEGO_APPOINTMENTDATE);
                dcmd.Parameters.AddWithValue("N_NEGO_VENUE", Negoobj.NEGO_VENUE);
                dcmd.Parameters.AddWithValue("N_NEGO_DATE", Negoobj.NEGO_DATE);
                if (Negoobj.NEGO_PROBLEMDESC.Length > 1000)
                    dcmd.Parameters.AddWithValue("N_NEGO_PROBLEMDESC", Negoobj.NEGO_PROBLEMDESC.Substring(0, 1000));
                else
                    dcmd.Parameters.AddWithValue("N_NEGO_PROBLEMDESC", Negoobj.NEGO_PROBLEMDESC);
                dcmd.Parameters.AddWithValue("N_UPDATEDBY", Negoobj.CREATEDBY);
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
