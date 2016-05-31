using System;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;


namespace WIS_DataAccess
{
    public class PermanentStructureDAL
    {

        /// <summary>
        /// To Get Occupant status
        /// </summary>
        /// <returns></returns>
        public PermanentStructureList GetOccupantstatus()
        {
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_OCCUPANTSTATUSDATA";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PermanentStructureBO BOobj = null;
            PermanentStructureList Listobj = new PermanentStructureList();

            while (dr.Read())
            {
                BOobj = new PermanentStructureBO();

                BOobj.OccupantStatusID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OCCUPANTSTATUSID"))));
                BOobj.OccupantStatus = dr.GetValue(dr.GetOrdinal("OCCUPANTSTATUS")).ToString();

                Listobj.Add(BOobj);
            }

            dr.Close();
            return Listobj;
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="PermanentStructureobj"></param>
        /// <returns></returns>
        public int Insert(PermanentStructureBO PermanentStructureobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();            

            try
            {               
                    OracleCommand dcmd = new OracleCommand("USP_TRN_INS_PERMANENTSTRUCTURE", cnn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    int count = Convert.ToInt32(dcmd.CommandType);

                    dcmd.Parameters.Add("HHID", PermanentStructureobj.HouseholdID);

                    dcmd.Parameters.Add("PERMSTR_TYPEID", PermanentStructureobj.StructureType);

                    if (PermanentStructureobj.StructureTypeID > 0)
                        dcmd.Parameters.Add("STR_TYPEID", PermanentStructureobj.StructureTypeID);
                    else
                        dcmd.Parameters.Add("STR_TYPEID", DBNull.Value);

                    dcmd.Parameters.Add("OTHERSTRUCTURETYPE", PermanentStructureobj.OtherStructureType);

                    if (PermanentStructureobj.RoofID > 0)
                        dcmd.Parameters.Add("ROOFID", PermanentStructureobj.RoofID);
                    else
                        dcmd.Parameters.Add("ROOFID", DBNull.Value);

                    if (PermanentStructureobj.WallID > 0)
                        dcmd.Parameters.Add("WALLID", PermanentStructureobj.WallID);
                    else
                        dcmd.Parameters.Add("WALLID", DBNull.Value);

                    if (PermanentStructureobj.FloorID > 0)
                        dcmd.Parameters.Add("FLOORID", PermanentStructureobj.FloorID);
                    else
                        dcmd.Parameters.Add("FLOORID", DBNull.Value);

                    if (PermanentStructureobj.WindowID > 0)
                        dcmd.Parameters.Add("WINDOWID", PermanentStructureobj.WindowID);
                    else
                        dcmd.Parameters.Add("WINDOWID", DBNull.Value);

                    if (PermanentStructureobj.RoofConditionID > 0)
                        dcmd.Parameters.Add("ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                    else
                        dcmd.Parameters.Add("ROOFCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.WallConditionID > 0)
                        dcmd.Parameters.Add("WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                    else
                        dcmd.Parameters.Add("WALLCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.FloorConditionID > 0)
                        dcmd.Parameters.Add("FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                    else
                        dcmd.Parameters.Add("FLOORCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.WindowConditionID > 0)
                        dcmd.Parameters.Add("WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                    else
                        dcmd.Parameters.Add("WINDOWCONDITIONID", DBNull.Value);

                    dcmd.Parameters.Add("OWNER", PermanentStructureobj.Owner);
                    dcmd.Parameters.Add("OWNERNAME", PermanentStructureobj.OwnerName);
                    dcmd.Parameters.Add("OCCUPANT", PermanentStructureobj.Occupant);
                    dcmd.Parameters.Add("OTHEROCCUPANTNAME", PermanentStructureobj.OtherOccupantName);

                    if (PermanentStructureobj.OccupantStatusID > 0)
                        dcmd.Parameters.Add("OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);
                    else
                        dcmd.Parameters.Add("OCCUPANTSTATUSID", DBNull.Value);

                    dcmd.Parameters.Add("OTHEROCCUPANTSTATUS", PermanentStructureobj.OtherOccupantStatus);
                    dcmd.Parameters.Add("DIMEN_LENGTH", PermanentStructureobj.DimensionLength);
                    dcmd.Parameters.Add("DIMEN_WIDTH", PermanentStructureobj.DimensionWidth);
                    dcmd.Parameters.Add("NOOFROOMS", PermanentStructureobj.NoOfRooms);
                    dcmd.Parameters.Add("SURFACEAREA", PermanentStructureobj.SurfaceArea);
                    dcmd.Parameters.Add("DEPRECIATEDVALUE", PermanentStructureobj.DepreciatedValue);
                    dcmd.Parameters.Add("REPLACEMENTVALUE", PermanentStructureobj.ReplacementValue);
                    dcmd.Parameters.Add("ADDITIONALCOMMENTS", PermanentStructureobj.AdditionalComments);

                    if (PermanentStructureobj.Photo != null)
                        dcmd.Parameters.Add(new OracleParameter("PAPPSPHOTO_", OracleDbType.Blob)).Value = PermanentStructureobj.Photo;
                    else
                        dcmd.Parameters.Add("PAPPSPHOTO_", Oracle.DataAccess.Types.OracleBlob.Null);

                    dcmd.Parameters.Add("ISDELETED", PermanentStructureobj.IsDeleted);
                    dcmd.Parameters.Add("CREATEDBY", PermanentStructureobj.CreatedBy);
                    return dcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        /// <summary>
        /// To Get Permanent Structure
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public PermanentStructureList GetPermanentStructure(string hhid)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_SEL_PERMANST";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_HHID", hhid);
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
         
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PermanentStructureBO PermanentStructureobj = null;
            PermanentStructureList Listobj = new PermanentStructureList();

            while (dr.Read())
            {
                PermanentStructureobj = new PermanentStructureBO();
                PermanentStructureobj.PermanentStructureID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("perm_structureid")));
                if (!dr.IsDBNull(dr.GetOrdinal("str_type")))
                {
                    PermanentStructureobj.StructTypid = dr.GetString(dr.GetOrdinal("str_type"));
                }
                else
                {
                    PermanentStructureobj.StructTypid = " ";
                }
                if (!dr.IsDBNull(dr.GetOrdinal("depreciatedvalue")))
                {
                    PermanentStructureobj.DepreciatedValue = dr.GetDecimal(dr.GetOrdinal("depreciatedvalue"));
                }
                else
                {
                    PermanentStructureobj.DepreciatedValue = 0;
                }
                if (!dr.IsDBNull(dr.GetOrdinal("depreciatedvalue")))
                {
                    PermanentStructureobj.ReplacementValue = dr.GetDecimal(dr.GetOrdinal("replacementvalue"));
                }
                else
                {
                    PermanentStructureobj.ReplacementValue = 0;
                }
                if (!dr.IsDBNull(dr.GetOrdinal("StructureType")))
                {
                    PermanentStructureobj.StructureType = dr.GetString(dr.GetOrdinal("StructureType"));
                }
                else
                {
                    PermanentStructureobj.StructureType = "";
                }
               // PermanentStructureobj.IsDeleted = dr.GetString(dr.GetOrdinal("isdeleted"));
             
                Listobj.Add(PermanentStructureobj);
            }

            dr.Close();

            return Listobj;
        }

        /// <summary>
        /// To GET STRUCTURE ID
        /// </summary>
        /// <param name="STRUCTUREID"></param>
        /// <returns></returns>
        public PermanentStructureBO GetSTRUCTUREID(int STRUCTUREID)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_TRN_GET_PERSTRC";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_PERM_STRUCTUREID", STRUCTUREID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PermanentStructureBO PermanentStructureobj = null;
            PermanentStructureList Listobj = new PermanentStructureList();

            PermanentStructureobj = new PermanentStructureBO();
            while (dr.Read())
            {
                if (!dr.IsDBNull(dr.GetOrdinal("perm_structureid")))
                    PermanentStructureobj.PermanentStructureID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("perm_structureid")));

                if (!dr.IsDBNull(dr.GetOrdinal("STR_TYPEID")))
                    PermanentStructureobj.StructureTypeID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("STR_TYPEID")));

                if (!dr.IsDBNull(dr.GetOrdinal("OTHERSTRUCTURETYPE")))
                    PermanentStructureobj.OtherStructureType = dr.GetString(dr.GetOrdinal("OTHERSTRUCTURETYPE"));

                if (!dr.IsDBNull(dr.GetOrdinal("ROOFID")))
                    PermanentStructureobj.RoofID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROOFID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WALLID")))
                    PermanentStructureobj.WallID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WALLID")));

                if (!dr.IsDBNull(dr.GetOrdinal("FLOORID")))
                    PermanentStructureobj.FloorID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FLOORID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WINDOWID")))
                    PermanentStructureobj.WindowID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WINDOWID")));

                if (!dr.IsDBNull(dr.GetOrdinal("ROOFCONDITIONID")))
                    PermanentStructureobj.RoofConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ROOFCONDITIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WALLCONDITIONID")))
                    PermanentStructureobj.WallConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WALLCONDITIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("FLOORCONDITIONID")))
                    PermanentStructureobj.FloorConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FLOORCONDITIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("WINDOWCONDITIONID")))
                    PermanentStructureobj.WindowConditionID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("WINDOWCONDITIONID")));

                if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                    PermanentStructureobj.Owner = dr.GetString(dr.GetOrdinal("OWNER"));


                if (!dr.IsDBNull(dr.GetOrdinal("OWNERNAME")))
                    PermanentStructureobj.OwnerName = dr.GetString(dr.GetOrdinal("OWNERNAME"));


                if (!dr.IsDBNull(dr.GetOrdinal("OCCUPANT")))
                    PermanentStructureobj.Occupant =dr.GetString(dr.GetOrdinal("OCCUPANT"));


                if (!dr.IsDBNull(dr.GetOrdinal("OTHEROCCUPANTNAME")))
                    PermanentStructureobj.OtherOccupantName = dr.GetString(dr.GetOrdinal("OTHEROCCUPANTNAME"));

                if (!dr.IsDBNull(dr.GetOrdinal("OCCUPANTSTATUSID")))
                    PermanentStructureobj.OccupantStatusID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OCCUPANTSTATUSID")));

                if (!dr.IsDBNull(dr.GetOrdinal("OTHEROCCUPANTSTATUS")))
                    PermanentStructureobj.OtherOccupantStatus = dr.GetString(dr.GetOrdinal("OTHEROCCUPANTSTATUS"));

                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_LENGTH")))
                    PermanentStructureobj.DimensionLength = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_LENGTH")));

                if (!dr.IsDBNull(dr.GetOrdinal("DIMEN_WIDTH")))
                    PermanentStructureobj.DimensionWidth = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DIMEN_WIDTH")));

                if (!dr.IsDBNull(dr.GetOrdinal("NOOFROOMS")))
                    PermanentStructureobj.NoOfRooms = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("NOOFROOMS")));

                if (!dr.IsDBNull(dr.GetOrdinal("SURFACEAREA")))
                    PermanentStructureobj.SurfaceArea = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("SURFACEAREA")));

              

                if (!dr.IsDBNull(dr.GetOrdinal("DEPRECIATEDVALUE")))
                    PermanentStructureobj.DepreciatedValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("DEPRECIATEDVALUE")));

                if (!dr.IsDBNull(dr.GetOrdinal("REPLACEMENTVALUE")))
                    PermanentStructureobj.ReplacementValue = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("REPLACEMENTVALUE")));

                if (!dr.IsDBNull(dr.GetOrdinal("ADDITIONALCOMMENTS")))
                    PermanentStructureobj.AdditionalComments = dr.GetString(dr.GetOrdinal("ADDITIONALCOMMENTS"));

                if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED")))
                    PermanentStructureobj.IsDeleted = dr.GetString(dr.GetOrdinal("ISDELETED"));


                if (!dr.IsDBNull(dr.GetOrdinal("permstructuretype")))
                {
                    PermanentStructureobj.StructureType = dr.GetString(dr.GetOrdinal("permstructuretype"));
                }

            }
            dr.Close();


            return PermanentStructureobj;
        }

        /// <summary>
        /// To Delete Permanent Structure
        /// </summary>
        /// <param name="structurId"></param>
        /// <returns></returns>
        public int DeletePermanentStruct(string structurId)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;


            string proc = "USP_TRN_DEL_PERMSTR";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_PERM_STRUCTUREID", structurId);
            cmd.Connection.Open();

            int result = cmd.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// To Update in Database
        /// </summary>
        /// <param name="PermanentStructureobj"></param>
        /// <returns></returns>
        public int Update(PermanentStructureBO PermanentStructureobj)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PERMANTSTRUCT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("HHID_", PermanentStructureobj.HouseholdID);
                dcmd.Parameters.Add("PERMSTR_TYPEID", PermanentStructureobj.StructureType);
                dcmd.Parameters.Add("P_PERM_STRUCTUREID", PermanentStructureobj.PermanentStructureID);
                dcmd.Parameters.Add("P_STR_TYPEID", PermanentStructureobj.StructureTypeID);
                dcmd.Parameters.Add("P_OTHERSTRUCTURETYPE", PermanentStructureobj.OtherStructureType);

                if (PermanentStructureobj.RoofID > 0)
                    dcmd.Parameters.Add("P_ROOFID", PermanentStructureobj.RoofID);
                else
                    dcmd.Parameters.Add("P_ROOFID", DBNull.Value);

                if (PermanentStructureobj.WallID > 0)
                    dcmd.Parameters.Add("P_WALLID", PermanentStructureobj.WallID);
                else
                    dcmd.Parameters.Add("P_WALLID", DBNull.Value);

                if (PermanentStructureobj.FloorID > 0)
                    dcmd.Parameters.Add("P_FLOORID", PermanentStructureobj.FloorID);
                else
                    dcmd.Parameters.Add("P_FLOORID", DBNull.Value);

                if (PermanentStructureobj.WindowID > 0)
                    dcmd.Parameters.Add("P_WINDOWID", PermanentStructureobj.WindowID);
                else
                    dcmd.Parameters.Add("P_WINDOWID", DBNull.Value);

                if (PermanentStructureobj.RoofConditionID > 0)
                    dcmd.Parameters.Add("P_ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                else
                    dcmd.Parameters.Add("P_ROOFCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.WallConditionID > 0)
                    dcmd.Parameters.Add("P_WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                else
                    dcmd.Parameters.Add("P_WALLCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.FloorConditionID > 0)
                    dcmd.Parameters.Add("P_FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                else
                    dcmd.Parameters.Add("P_FLOORCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.WindowConditionID > 0)
                    dcmd.Parameters.Add("P_WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                else
                    dcmd.Parameters.Add("P_WINDOWCONDITIONID", DBNull.Value);

                //dcmd.Parameters.Add("P_ROOFID", PermanentStructureobj.RoofID);
                //dcmd.Parameters.Add("P_WALLID", PermanentStructureobj.WallID);
                //dcmd.Parameters.Add("P_FLOORID", PermanentStructureobj.FloorID);
                //dcmd.Parameters.Add("P_WINDOWID", PermanentStructureobj.WindowID);
                //dcmd.Parameters.Add("P_ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                //dcmd.Parameters.Add("P_WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                //dcmd.Parameters.Add("P_FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                //dcmd.Parameters.Add("P_WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                dcmd.Parameters.Add("P_OWNER", PermanentStructureobj.Owner);
                dcmd.Parameters.Add("P_OWNERNAME", PermanentStructureobj.OwnerName);
                dcmd.Parameters.Add("P_OCCUPANT", PermanentStructureobj.Occupant);
                dcmd.Parameters.Add("P_OTHEROCCUPANTNAME", PermanentStructureobj.OtherOccupantName);

                if (PermanentStructureobj.OccupantStatusID > 0)
                    dcmd.Parameters.Add("P_OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);
                else
                    dcmd.Parameters.Add("P_OCCUPANTSTATUSID", DBNull.Value);

                //dcmd.Parameters.Add("P_OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);

                dcmd.Parameters.Add("P_OTHEROCCUPANTSTATUS", PermanentStructureobj.OtherOccupantStatus);
                dcmd.Parameters.Add("P_DIMEN_LENGTH", PermanentStructureobj.DimensionLength);
                dcmd.Parameters.Add("P_DIMEN_WIDTH", PermanentStructureobj.DimensionWidth);
                dcmd.Parameters.Add("P_NOOFROOMS", PermanentStructureobj.NoOfRooms);
                dcmd.Parameters.Add("P_SURFACEAREA", PermanentStructureobj.SurfaceArea);
                dcmd.Parameters.Add("P_DEPRECIATEDVALUE", PermanentStructureobj.DepreciatedValue);
                dcmd.Parameters.Add("P_REPLACEMENTVALUE", PermanentStructureobj.ReplacementValue);
                dcmd.Parameters.Add("P_ADDITIONALCOMMENTS", PermanentStructureobj.AdditionalComments);
                // dcmd.Parameters.Add("PAPPSPHOTO_", PermanentStructureobj.Photo); //save the photo in blob
                //dcmd.Parameters.Add("ISDELETED", PermanentStructureobj.IsDeleted);
                dcmd.Parameters.Add("P_UPDATEDBY", PermanentStructureobj.CreatedBy);
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
        /// To Update Photo
        /// </summary>
        /// <param name="PermanentStructureobj1"></param>
        /// <returns></returns>
        public int Updatephoto(PermanentStructureBO PermanentStructureobj1)
        {
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            OracleCommand dcmd = new OracleCommand("USP_TRN_UPD_PHOTO_PERMSTRUCT", cnn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.Add("P_PERM_STRUCTUREID", PermanentStructureobj1.PermanentStructureID);
                dcmd.Parameters.Add("P_HHID", PermanentStructureobj1.HouseholdID);
                dcmd.Parameters.Add(new OracleParameter("PAPPSPHOTO_", OracleDbType.Blob)).Value = PermanentStructureobj1.Photo;
                dcmd.Parameters.Add("P_UPDATEDBY", PermanentStructureobj1.UpdatedBy);
                 
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
        public PermanentStructureBO GetPAPPSPhoto(int householdID, int PermanentStructureID)
        {
            OracleConnection myConnection;
            OracleCommand myCommand;
            myConnection = new OracleConnection(AppConfiguration.ConnectionString);
            myCommand = new OracleCommand("USP_TRN_GET_PAPPS_PHOTO", myConnection);
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

                PermanentStructureBO objPAPPS = new PermanentStructureBO();
                objPAPPS.Photo = papPhotoBytes;
                return objPAPPS;
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