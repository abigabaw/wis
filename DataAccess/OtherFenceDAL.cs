using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class OtherFenceDAL
    {
        /// <summary>
        /// To Edit Fence
        /// </summary>
        /// <param name="FenceBOobj"></param>
        /// <returns></returns>
        public int EditFence(OtherFenceBO FenceBOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_OTHERFENCE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("PAP_FIXTUREID_", FenceBOobj.Pap_otherfenceid);
                dcmd.Parameters.Add("HHID", FenceBOobj.HouseholdID);
                dcmd.Parameters.Add("FIXTURETYPE_", FenceBOobj.Otherfencedescription);
                dcmd.Parameters.Add("DIMEN_LENGTH_", FenceBOobj.DIMEN_LENGTH);
                dcmd.Parameters.Add("DIMEN_WIDTH_", FenceBOobj.DIMEN_WIDTH);
                dcmd.Parameters.Add("DEPRECIATEDVALUE_", FenceBOobj.Depreciatedvalue);
                dcmd.Parameters.Add("UPDATEDBY_", FenceBOobj.CreatedBy);
                //if (FenceBOobj.Photo != null)
                //    dcmd.Parameters.Add("FENCEPHOTO_", OracleDbType.Blob).Value = FenceBOobj.Photo;
                //else
                //    dcmd.Parameters.Add("FENCEPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
                return dcmd.ExecuteNonQuery();
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
        }

        /// <summary>
        /// To Insert into DATABASE
        /// </summary>
        /// <param name="FenceBOobj"></param>
        /// <returns></returns>
        public int Insert(OtherFenceBO FenceBOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_OTHERFENCE", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", FenceBOobj.HouseholdID);
                dcmd.Parameters.Add("FIXTURETYPE_", FenceBOobj.Otherfencedescription);
                dcmd.Parameters.Add("DIMEN_LENGTH_", FenceBOobj.DIMEN_LENGTH);
                dcmd.Parameters.Add("DIMEN_WIDTH_", FenceBOobj.DIMEN_WIDTH);
                dcmd.Parameters.Add("DEPRECIATEDVALUE_", FenceBOobj.Depreciatedvalue);
                dcmd.Parameters.Add("CREATEDBY_", FenceBOobj.CreatedBy);
              

                return dcmd.ExecuteNonQuery();

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
        }

        /// <summary>
        /// To Get Fence data
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public OtherFenceList GetFencedata(int householdID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_PAP_OTHERFIXTURES";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            OtherFenceBO FenceBOobj = null;
            OtherFenceList FenceListobj = new OtherFenceList();

            while (dr.Read())
            {
                FenceBOobj = new OtherFenceBO();
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_FIXTUREID"))) FenceBOobj.Pap_otherfenceid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_FIXTUREID")));
                if (!dr.IsDBNull(dr.GetOrdinal("FIXTURETYPE"))) FenceBOobj.Otherfencedescription = dr.GetString(dr.GetOrdinal("FIXTURETYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_LENGTH"))) FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_LENGTH")));
                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_WIDTH"))) FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_WIDTH")));
                if (!dr.IsDBNull(dr.GetOrdinal("DEPRECIATEDVALUE"))) FenceBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DEPRECIATEDVALUE")));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) FenceBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));

                FenceListobj.Add(FenceBOobj);
            }

            dr.Close();

            return FenceListobj;
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="Pap_fenceid"></param>
        /// <returns></returns>
        public int Delete(int Pap_fenceid)
        {
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_OTHERFENCE", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("PAP_FIXTUREID_", Pap_fenceid);
                return dCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dCmd.Dispose();
                conn.Close();
                conn.Dispose();

            }
        }

        /// <summary>
        /// To Get fence data row
        /// </summary>
        /// <param name="Pap_fenceid"></param>
        /// <returns></returns>
        public OtherFenceBO Getfencedatarow(int Pap_fenceid)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_OTHERFENCE";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PAP_FIXTUREID_", Pap_fenceid);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OtherFenceBO FenceBOobj = null;

            FenceBOobj = new OtherFenceBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("PAP_FIXTUREID"))) FenceBOobj.Pap_otherfenceid = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PAP_FIXTUREID")));
                if (!dr.IsDBNull(dr.GetOrdinal("FIXTURETYPE"))) FenceBOobj.Otherfencedescription = dr.GetString(dr.GetOrdinal("FIXTURETYPE"));
                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_LENGTH"))) FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_LENGTH")));
                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_WIDTH"))) FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_WIDTH")));
                if (!dr.IsDBNull(dr.GetOrdinal("DEPRECIATEDVALUE"))) FenceBOobj.Depreciatedvalue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DEPRECIATEDVALUE")));
                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) FenceBOobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
            }
            dr.Close();


            return FenceBOobj;
        }

        // to get Image File for DataBase
        public OtherFenceBO ShowPAPOHFIXImage(int householdID, int PermanentStructureID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GETOTHERFENCEPHOTO", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("HHID_", householdID);
            myCommand.Parameters.Add("PermanentStructureID_", PermanentStructureID);
            myCommand.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            myCommand.Connection.Open();
            object img = myCommand.ExecuteScalar();

            try
            {
                byte[] papPhotoBytes = (byte[])img;

                OtherFenceBO objPAPOTHFIX = new OtherFenceBO();
                objPAPOTHFIX.Photo = papPhotoBytes;
                return objPAPOTHFIX;
                //return new System.IO.MemoryStream(papPhotoBytes);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
