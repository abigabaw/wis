using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class CulturePropertiesDAL
    {
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <returns></returns>
        public CulturePropertiesList GetCulturalPropertyType()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_NAME_CULTURPROP";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CulturPropertiesBO BOobj = null;
            CulturePropertiesList Listobj = new CulturePropertiesList();

            while (dr.Read())
            {
                BOobj = new CulturPropertiesBO();
                BOobj.CULTUREPROPTYPEID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTUREPROPTYPEID"))));
                BOobj.CULTUREPROPTYP = dr.GetValue(dr.GetOrdinal("CULTUREPROPTYPE")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;
        }
        /// <summary>
        /// To insert details to database
        /// </summary>
        /// <param name="CulturPropertiesobj"></param>
        /// <returns></returns>
        public int Insert(CulturPropertiesBO CulturPropertiesobj)
        {
            //string returnResult = string.Empty;

            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_CULTURPROP", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", CulturPropertiesobj.HHID);
                dcmd.Parameters.Add("CULTUREPROPTYPEID", CulturPropertiesobj.CULTUREPROPTYPEID);
                dcmd.Parameters.Add("CULTUREPROPDESCRIPTION", CulturPropertiesobj.CULTUREPROPDESCRIPTION);
                dcmd.Parameters.Add("CULT_DIMEN_LENGTH", CulturPropertiesobj.CULT_DIMEN_LENGTH);
                dcmd.Parameters.Add("CULT_DIMEN_WIDTH", CulturPropertiesobj.CULT_DIMEN_WIDTH);
                dcmd.Parameters.Add("CULT_DEPRECIATEDVALUE", CulturPropertiesobj.CULT_DEPRECIATEDVALUE);
                dcmd.Parameters.Add("CULT_VALUATIONAMOUNT", CulturPropertiesobj.CULT_VALUATIONAMOUNT);
                dcmd.Parameters.Add("ISDELETED", CulturPropertiesobj.ISDELETED);
                dcmd.Parameters.Add("CREATEDBY", CulturPropertiesobj.CREATEDBY);
                if (CulturPropertiesobj.Photo != null)
                    dcmd.Parameters.Add("PAPCPPHOTO_", OracleDbType.Blob).Value = CulturPropertiesobj.Photo;
                else
                    dcmd.Parameters.Add("PAPCPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);
             

                //dcmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


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
        /// To fetch details from database
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CulturePropertiesList GetCultureProp(int householdID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_CULTURPROP";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            CulturPropertiesBO CulturPropertiesobj = null;
            CulturePropertiesList Listobj = new CulturePropertiesList();

            while (dr.Read())
            {
                CulturPropertiesobj = new CulturPropertiesBO();


                CulturPropertiesobj.CULTURALPROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("culturalpropid")));
                if (!dr.IsDBNull(dr.GetOrdinal("cultureproptype")))
                    CulturPropertiesobj.CULTUREPROPTYP = dr.GetString(dr.GetOrdinal("cultureproptype"));

                if (!dr.IsDBNull(dr.GetOrdinal("culturepropdescription")))
                {
                    CulturPropertiesobj.CULTUREPROPDESCRIPTION = dr.GetString(dr.GetOrdinal("culturepropdescription"));
                }
                else
                {
                    CulturPropertiesobj.CULTUREPROPDESCRIPTION = " ";
                }
                CulturPropertiesobj.CULT_VALUATIONAMOUNT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cult_valuationamount")));
                Listobj.Add(CulturPropertiesobj);

            }

            dr.Close();

            return Listobj;

        }
        /// <summary>
        /// To update data to database
        /// </summary>
        /// <param name="CulturPropertiesobj"></param>
        /// <returns></returns>
        public int Update(CulturPropertiesBO CulturPropertiesobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_CULTURPROP", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID_", CulturPropertiesobj.HHID);

                dcmd.Parameters.Add("c_CULTURALPROPID", CulturPropertiesobj.CULTURALPROPID);

                dcmd.Parameters.Add("c_CULTUREPROPTYPEID", CulturPropertiesobj.CULTUREPROPTYPEID);
                dcmd.Parameters.Add("c_CULTUREPROPDESCRIPTION", CulturPropertiesobj.CULTUREPROPDESCRIPTION);
                dcmd.Parameters.Add("c_CULT_DIMEN_LENGTH", CulturPropertiesobj.CULT_DIMEN_LENGTH);
                dcmd.Parameters.Add("c_CULT_DIMEN_WIDTH", CulturPropertiesobj.CULT_DIMEN_WIDTH);

                dcmd.Parameters.Add("c_CULT_DEPRECIATEDVALUE", CulturPropertiesobj.CULT_DEPRECIATEDVALUE);
                dcmd.Parameters.Add("c_CULT_VALUATIONAMOUNT", CulturPropertiesobj.CULT_VALUATIONAMOUNT);


                dcmd.Parameters.Add("c_UPDATEDBY", CulturPropertiesobj.CREATEDBY);

                if (CulturPropertiesobj.Photo != null)
                    dcmd.Parameters.Add("PAPCPPHOTO_", OracleDbType.Blob).Value = CulturPropertiesobj.Photo;
                else
                    dcmd.Parameters.Add("PAPCPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);

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
        ///  To fetch details from database
        /// </summary>
        /// <param name="culTURALPROPID"></param>
        /// <returns></returns>
        public CulturPropertiesBO GetData(int culTURALPROPID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_CULTURPROP";//"USP_TRN_GET_DAMAGE_CROPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("C_CULTURALPROPID", culTURALPROPID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            CulturPropertiesBO CulturPropertiesobj = null;
            CulturePropertiesList Listobj = new CulturePropertiesList();

            CulturPropertiesobj = new CulturPropertiesBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("CULTURALPROPID")))
                    CulturPropertiesobj.CULTURALPROPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTURALPROPID")));


                if (!dr.IsDBNull(dr.GetOrdinal("CULTUREPROPTYPEID")))
                    CulturPropertiesobj.CULTUREPROPTYPEID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULTUREPROPTYPEID")));

                if (!dr.IsDBNull(dr.GetOrdinal("CULTUREPROPDESCRIPTION")))
                    CulturPropertiesobj.CULTUREPROPDESCRIPTION = dr.GetString(dr.GetOrdinal("CULTUREPROPDESCRIPTION"));

                if (!dr.IsDBNull(dr.GetOrdinal("CULT_DIMEN_LENGTH")))
                    CulturPropertiesobj.CULT_DIMEN_LENGTH = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULT_DIMEN_LENGTH")));

                if (!dr.IsDBNull(dr.GetOrdinal("CULT_DIMEN_WIDTH")))
                    CulturPropertiesobj.CULT_DIMEN_WIDTH = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CULT_DIMEN_WIDTH")));


                if (!dr.IsDBNull(dr.GetOrdinal("CULT_DEPRECIATEDVALUE")))
                    CulturPropertiesobj.CULT_DEPRECIATEDVALUE = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CULT_DEPRECIATEDVALUE")));


                if (!dr.IsDBNull(dr.GetOrdinal("CULT_VALUATIONAMOUNT")))
                    CulturPropertiesobj.CULT_VALUATIONAMOUNT = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("CULT_VALUATIONAMOUNT")));

                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    CulturPropertiesobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

            }
            dr.Close();


            return CulturPropertiesobj;
        }


        /// <summary>
        /// to get Image File for DataBase
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public CulturPropertiesBO ShowPAPCPImage(int householdID, int PermanentStructureID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GET_PAPCP_PHOTO", myConnection);
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

                CulturPropertiesBO objPAPCP = new CulturPropertiesBO();
                objPAPCP.Photo = papPhotoBytes;
                return objPAPCP;
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
