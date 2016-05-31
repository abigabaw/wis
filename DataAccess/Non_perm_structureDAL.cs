using System;
using System.Data;
using WIS_DataAccess;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class Non_perm_structureDAL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
        public int Insert(NonPermanentStructureBO BOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_INS_NPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("HHID", BOobj.HouseholdID);
                if (BOobj.StructureTypeID > 0)
                    dcmd.Parameters.Add("STR_TYPEID", BOobj.StructureTypeID);
                else
                    dcmd.Parameters.Add("STR_TYPEID", DBNull.Value);

              dcmd.Parameters.Add("OTHERSTRUCTURETYPE", BOobj.OtherStructureType);

                if (BOobj.CategoryID > 0)
                    dcmd.Parameters.Add("CATEGORYID", BOobj.CategoryID);
                else
                    dcmd.Parameters.Add("CATEGORYID", DBNull.Value);

                if (BOobj.StructureConditionID > 0)
                    dcmd.Parameters.Add("STR_CONDITIONID", BOobj.StructureConditionID);
                else
                    dcmd.Parameters.Add("STR_CONDITIONID", DBNull.Value);
                
               // dcmd.Parameters.Add("CATEGORYID", BOobj.CategoryID);
               // dcmd.Parameters.Add("STR_CONDITIONID", BOobj.StructureConditionID);
                dcmd.Parameters.Add("OWNER", BOobj.Owner);
                dcmd.Parameters.Add("OWNERNAME", BOobj.OwnerName);
                dcmd.Parameters.Add("OCCUPANT", BOobj.Occupant);
                dcmd.Parameters.Add("OTHEROCCUPANTNAME", BOobj.OtherOccupantName);
                if (BOobj.OccupantStatusID > 0)
                    dcmd.Parameters.Add("OCCUPANTSTATUSID", BOobj.OccupantStatusID);
                else
                    dcmd.Parameters.Add("OCCUPANTSTATUSID", DBNull.Value);

                //dcmd.Parameters.Add("OCCUPANTSTATUSID", BOobj.OccupantStatusID);
                dcmd.Parameters.Add("OTHEROCCUPANTSTATUS", BOobj.OtherOccupantStatus);
                dcmd.Parameters.Add("DIMEN_LENGTH", BOobj.DimensionLength);
                dcmd.Parameters.Add("DIMEN_WIDTH", BOobj.DimensionWidth);
                dcmd.Parameters.Add("NOOFROOMS", BOobj.NoOfRooms);
                dcmd.Parameters.Add("SURFACEAREA", BOobj.SurfaceArea);
                dcmd.Parameters.Add("CREATEDBY", BOobj.CreatedBy);


                if (BOobj.Photo != null)
                    dcmd.Parameters.Add(new OracleParameter("PAPNPPHOTO_", OracleDbType.Blob)).Value = BOobj.Photo;
                else
                    dcmd.Parameters.Add("PAPNPPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);

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
        /// To Get NPS
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public Non_perm_structureList GetNPS(int householdID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_NPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            NonPermanentStructureBO BOobj1 = null;
            Non_perm_structureList Non_perm_structureListobj = new Non_perm_structureList();

            while (dr.Read())
            {
                BOobj1 = new NonPermanentStructureBO();
                BOobj1.NonPermanentStructureID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NONPERM_STRUCTUREID")));
                BOobj1.STR_TYPE = dr.GetString(dr.GetOrdinal("str_type"));
                BOobj1.STR_CATEGORYNAME = dr.GetString(dr.GetOrdinal("str_categoryname"));
                BOobj1.STR_CONDITION = dr.GetString(dr.GetOrdinal("str_condition"));
                BOobj1.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));
                Non_perm_structureListobj.Add(BOobj1);
            }

            dr.Close();

            return Non_perm_structureListobj;
        }

        /// <summary>
        /// To Edit NPS
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
        public int EditNPS(NonPermanentStructureBO BOobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_NPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);

            try
            {
                dcmd.Parameters.Add("N_NONPERM_STRUCTUREID", BOobj.NonPermanentStructureID);
                dcmd.Parameters.Add("N_STR_TYPEID", BOobj.StructureTypeID);
                dcmd.Parameters.Add("N_CATEGORYID", BOobj.CategoryID);
                dcmd.Parameters.Add("N_STR_CONDITIONID", BOobj.StructureConditionID);
                dcmd.Parameters.Add("N_OTHERSTRUCTURETYPE", BOobj.OtherStructureType);
                dcmd.Parameters.Add("N_OWNER", BOobj.Owner);
                dcmd.Parameters.Add("N_OWNERNAME", BOobj.OwnerName);
                dcmd.Parameters.Add("N_OCCUPANT", BOobj.Occupant);
                dcmd.Parameters.Add("N_OTHEROCCUPANTNAME", BOobj.OtherOccupantName);
                dcmd.Parameters.Add("N_OCCUPANTSTATUSID", BOobj.OccupantStatusID);
                dcmd.Parameters.Add("N_OTHEROCCUPANTSTATUS", BOobj.OtherOccupantStatus);
                dcmd.Parameters.Add("N_DIMEN_LENGTH", BOobj.DimensionLength);
                dcmd.Parameters.Add("N_DIMEN_WIDTH", BOobj.DimensionWidth);
                dcmd.Parameters.Add("N_NOOFROOMS", BOobj.NoOfRooms);
                dcmd.Parameters.Add("N_SURFACEAREA", BOobj.SurfaceArea);
                dcmd.Parameters.Add("N_UPDATEDBY", BOobj.CreatedBy);
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
        /// To Get Non Permanent Structure Id
        /// </summary>
        /// <param name="NonPermanentStructureID"></param>
        /// <returns></returns>
        public NonPermanentStructureBO GetNPSId(int NonPermanentStructureID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_NPS";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("S_NONPERM_STRUCTUREID", NonPermanentStructureID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            NonPermanentStructureBO BOobj1 = null;
            Non_perm_structureList Non_perm_structureListobj = new Non_perm_structureList();

            BOobj1 = new NonPermanentStructureBO();
            while (dr.Read())
            {

                if (ColumnExists(dr, "nonperm_structureid") && !dr.IsDBNull(dr.GetOrdinal("nonperm_structureid")))
                    BOobj1.NonPermanentStructureID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("nonperm_structureid")));
                if (ColumnExists(dr, "str_typeid") && !dr.IsDBNull(dr.GetOrdinal("str_typeid")))
                    BOobj1.StructureTypeID = dr.GetInt32(dr.GetOrdinal("str_typeid"));
                if (ColumnExists(dr, "categoryid") && !dr.IsDBNull(dr.GetOrdinal("categoryid")))
                    BOobj1.CategoryID = dr.GetInt32(dr.GetOrdinal("categoryid"));
                if (ColumnExists(dr, "str_conditionid") && !dr.IsDBNull(dr.GetOrdinal("str_conditionid")))
                    BOobj1.StructureConditionID = dr.GetInt32(dr.GetOrdinal("str_conditionid"));
                if (ColumnExists(dr, "otherstructuretype") && !dr.IsDBNull(dr.GetOrdinal("otherstructuretype")))
                    BOobj1.OtherStructureType = dr.GetString(dr.GetOrdinal("otherstructuretype"));
                if (ColumnExists(dr, "owner") && !dr.IsDBNull(dr.GetOrdinal("owner")))
                    BOobj1.Owner = dr.GetString(dr.GetOrdinal("owner"));
                if (ColumnExists(dr, "ownername") && !dr.IsDBNull(dr.GetOrdinal("ownername")))
                    BOobj1.OwnerName = dr.GetString(dr.GetOrdinal("ownername"));
                if (ColumnExists(dr, "occupant") && !dr.IsDBNull(dr.GetOrdinal("occupant")))
                    BOobj1.Occupant = dr.GetString(dr.GetOrdinal("occupant"));
                if (ColumnExists(dr, "otheroccupantname") && !dr.IsDBNull(dr.GetOrdinal("otheroccupantname")))
                    BOobj1.OtherOccupantName = dr.GetString(dr.GetOrdinal("otheroccupantname"));

                if (ColumnExists(dr, "occupantstatusid") && !dr.IsDBNull(dr.GetOrdinal("occupantstatusid")))
                    BOobj1.OccupantStatusID = dr.GetInt32(dr.GetOrdinal("occupantstatusid"));

                if (ColumnExists(dr, "otheroccupantstatus") && !dr.IsDBNull(dr.GetOrdinal("otheroccupantstatus")))
                    BOobj1.OtherOccupantStatus = dr.GetString(dr.GetOrdinal("otheroccupantstatus"));

                if (!dr.IsDBNull(dr.GetOrdinal("dimen_length")))
                    BOobj1.DimensionLength = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("dimen_length")));

                if (!dr.IsDBNull(dr.GetOrdinal("dimen_width")))
                    BOobj1.DimensionWidth = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("dimen_width")));

                if (!dr.IsDBNull(dr.GetOrdinal("noofrooms")))
                    BOobj1.NoOfRooms = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("noofrooms")));

                if (!dr.IsDBNull(dr.GetOrdinal("surfacearea")))
                    BOobj1.SurfaceArea = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("surfacearea")));

                           

            }
            dr.Close();


            return BOobj1;

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

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="NonPermanentStructureID"></param>
        /// <returns></returns>
        public int Delete(string NonPermanentStructureID)
        {
            OracleConnection conn = new OracleConnection(AppConfiguration.ConnectionString);
            conn.Open();
            OracleCommand dCmd = new OracleCommand("USP_TRN_DEL_NPS", conn);
            dCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dCmd.Parameters.Add("NONPERM_STRUCTUREID_", NonPermanentStructureID);
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
        /// To Update photo
        /// </summary>
        /// <param name="BOobj1"></param>
        /// <returns></returns>
        public int Updatephoto(NonPermanentStructureBO BOobj1)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_NPS", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("N_NONPERM_STRUCTUREID", BOobj1.NonPermanentStructureID);
                dcmd.Parameters.Add("N_HHID", BOobj1.HouseholdID);
                dcmd.Parameters.Add(new OracleParameter("PAPPSPHOTO_", OracleDbType.Blob)).Value = BOobj1.Photo;   
                dcmd.Parameters.Add("N_UPDATEDBY", BOobj1.UpdatedBy);

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

        // to get Image File for DataBase
        public NonPermanentStructureBO ShowPAPNPBImage(int householdID, int PermanentStructureID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GET_PAPNPS_PHOTO", myConnection);
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

                NonPermanentStructureBO objPAPNPS = new NonPermanentStructureBO();
                objPAPNPS.Photo = papPhotoBytes;
                return objPAPNPS;
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
