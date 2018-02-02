using System;
using System.Data.SqlClient;
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
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_OCCUPANTSTATUSDATA";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();            

            try
            {               
                    SqlCommand dcmd = new SqlCommand("USP_TRN_INS_PERMANENTSTRUCTURE", cnn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    int count = Convert.ToInt32(dcmd.CommandType);

                    dcmd.Parameters.AddWithValue("HHID", PermanentStructureobj.HouseholdID);

                    dcmd.Parameters.AddWithValue("PERMSTR_TYPEID", PermanentStructureobj.StructureType);

                    if (PermanentStructureobj.StructureTypeID > 0)
                        dcmd.Parameters.AddWithValue("STR_TYPEID", PermanentStructureobj.StructureTypeID);
                    else
                        dcmd.Parameters.AddWithValue("STR_TYPEID", DBNull.Value);

                    dcmd.Parameters.AddWithValue("OTHERSTRUCTURETYPE", PermanentStructureobj.OtherStructureType);

                    if (PermanentStructureobj.RoofID > 0)
                        dcmd.Parameters.AddWithValue("ROOFID", PermanentStructureobj.RoofID);
                    else
                        dcmd.Parameters.AddWithValue("ROOFID", DBNull.Value);

                    if (PermanentStructureobj.WallID > 0)
                        dcmd.Parameters.AddWithValue("WALLID", PermanentStructureobj.WallID);
                    else
                        dcmd.Parameters.AddWithValue("WALLID", DBNull.Value);

                    if (PermanentStructureobj.FloorID > 0)
                        dcmd.Parameters.AddWithValue("FLOORID", PermanentStructureobj.FloorID);
                    else
                        dcmd.Parameters.AddWithValue("FLOORID", DBNull.Value);

                    if (PermanentStructureobj.WindowID > 0)
                        dcmd.Parameters.AddWithValue("WINDOWID", PermanentStructureobj.WindowID);
                    else
                        dcmd.Parameters.AddWithValue("WINDOWID", DBNull.Value);

                    if (PermanentStructureobj.RoofConditionID > 0)
                        dcmd.Parameters.AddWithValue("ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                    else
                        dcmd.Parameters.AddWithValue("ROOFCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.WallConditionID > 0)
                        dcmd.Parameters.AddWithValue("WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                    else
                        dcmd.Parameters.AddWithValue("WALLCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.FloorConditionID > 0)
                        dcmd.Parameters.AddWithValue("FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                    else
                        dcmd.Parameters.AddWithValue("FLOORCONDITIONID", DBNull.Value);

                    if (PermanentStructureobj.WindowConditionID > 0)
                        dcmd.Parameters.AddWithValue("WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                    else
                        dcmd.Parameters.AddWithValue("WINDOWCONDITIONID", DBNull.Value);

                    dcmd.Parameters.AddWithValue("OWNER", PermanentStructureobj.Owner);
                    dcmd.Parameters.AddWithValue("OWNERNAME", PermanentStructureobj.OwnerName);
                    dcmd.Parameters.AddWithValue("OCCUPANT", PermanentStructureobj.Occupant);
                    dcmd.Parameters.AddWithValue("OTHEROCCUPANTNAME", PermanentStructureobj.OtherOccupantName);

                    if (PermanentStructureobj.OccupantStatusID > 0)
                        dcmd.Parameters.AddWithValue("OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);
                    else
                        dcmd.Parameters.AddWithValue("OCCUPANTSTATUSID", DBNull.Value);

                    dcmd.Parameters.AddWithValue("OTHEROCCUPANTSTATUS", PermanentStructureobj.OtherOccupantStatus);
                    dcmd.Parameters.AddWithValue("DIMEN_LENGTH", PermanentStructureobj.DimensionLength);
                    dcmd.Parameters.AddWithValue("DIMEN_WIDTH", PermanentStructureobj.DimensionWidth);
                    dcmd.Parameters.AddWithValue("NOOFROOMS", PermanentStructureobj.NoOfRooms);
                    dcmd.Parameters.AddWithValue("SURFACEAREA", PermanentStructureobj.SurfaceArea);
                    dcmd.Parameters.AddWithValue("DEPRECIATEDVALUE", PermanentStructureobj.DepreciatedValue);
                    dcmd.Parameters.AddWithValue("REPLACEMENTVALUE", PermanentStructureobj.ReplacementValue);
                    dcmd.Parameters.AddWithValue("ADDITIONALCOMMENTS", PermanentStructureobj.AdditionalComments);

                    if (PermanentStructureobj.Photo != null)
                        dcmd.Parameters.Add(new SqlParameter("PAPPSPHOTO_", SqlDbType.Image)).Value = PermanentStructureobj.Photo;
                    else
                        dcmd.Parameters.AddWithValue("PAPPSPHOTO_", DBNull.Value);

                    dcmd.Parameters.AddWithValue("ISDELETED", PermanentStructureobj.IsDeleted);
                    dcmd.Parameters.AddWithValue("CREATEDBY", PermanentStructureobj.CreatedBy);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_SEL_PERMANST";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("P_HHID", hhid);
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;
         
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_TRN_GET_PERSTRC";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("P_PERM_STRUCTUREID", STRUCTUREID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;


            string proc = "USP_TRN_DEL_PERMSTR";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("P_PERM_STRUCTUREID", structurId);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PERMANTSTRUCT", cnn);
            dcmd.CommandType = CommandType.StoredProcedure;
            int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("HHID_", PermanentStructureobj.HouseholdID);
                dcmd.Parameters.AddWithValue("PERMSTR_TYPEID", PermanentStructureobj.StructureType);
                dcmd.Parameters.AddWithValue("P_PERM_STRUCTUREID", PermanentStructureobj.PermanentStructureID);
                dcmd.Parameters.AddWithValue("P_STR_TYPEID", PermanentStructureobj.StructureTypeID);
                dcmd.Parameters.AddWithValue("P_OTHERSTRUCTURETYPE", PermanentStructureobj.OtherStructureType);

                if (PermanentStructureobj.RoofID > 0)
                    dcmd.Parameters.AddWithValue("P_ROOFID", PermanentStructureobj.RoofID);
                else
                    dcmd.Parameters.AddWithValue("P_ROOFID", DBNull.Value);

                if (PermanentStructureobj.WallID > 0)
                    dcmd.Parameters.AddWithValue("P_WALLID", PermanentStructureobj.WallID);
                else
                    dcmd.Parameters.AddWithValue("P_WALLID", DBNull.Value);

                if (PermanentStructureobj.FloorID > 0)
                    dcmd.Parameters.AddWithValue("P_FLOORID", PermanentStructureobj.FloorID);
                else
                    dcmd.Parameters.AddWithValue("P_FLOORID", DBNull.Value);

                if (PermanentStructureobj.WindowID > 0)
                    dcmd.Parameters.AddWithValue("P_WINDOWID", PermanentStructureobj.WindowID);
                else
                    dcmd.Parameters.AddWithValue("P_WINDOWID", DBNull.Value);

                if (PermanentStructureobj.RoofConditionID > 0)
                    dcmd.Parameters.AddWithValue("P_ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                else
                    dcmd.Parameters.AddWithValue("P_ROOFCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.WallConditionID > 0)
                    dcmd.Parameters.AddWithValue("P_WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                else
                    dcmd.Parameters.AddWithValue("P_WALLCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.FloorConditionID > 0)
                    dcmd.Parameters.AddWithValue("P_FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                else
                    dcmd.Parameters.AddWithValue("P_FLOORCONDITIONID", DBNull.Value);

                if (PermanentStructureobj.WindowConditionID > 0)
                    dcmd.Parameters.AddWithValue("P_WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                else
                    dcmd.Parameters.AddWithValue("P_WINDOWCONDITIONID", DBNull.Value);

                //dcmd.Parameters.AddWithValue("P_ROOFID", PermanentStructureobj.RoofID);
                //dcmd.Parameters.AddWithValue("P_WALLID", PermanentStructureobj.WallID);
                //dcmd.Parameters.AddWithValue("P_FLOORID", PermanentStructureobj.FloorID);
                //dcmd.Parameters.AddWithValue("P_WINDOWID", PermanentStructureobj.WindowID);
                //dcmd.Parameters.AddWithValue("P_ROOFCONDITIONID", PermanentStructureobj.RoofConditionID);
                //dcmd.Parameters.AddWithValue("P_WALLCONDITIONID", PermanentStructureobj.WallConditionID);
                //dcmd.Parameters.AddWithValue("P_FLOORCONDITIONID", PermanentStructureobj.FloorConditionID);
                //dcmd.Parameters.AddWithValue("P_WINDOWCONDITIONID", PermanentStructureobj.WindowConditionID);
                dcmd.Parameters.AddWithValue("P_OWNER", PermanentStructureobj.Owner);
                dcmd.Parameters.AddWithValue("P_OWNERNAME", PermanentStructureobj.OwnerName);
                dcmd.Parameters.AddWithValue("P_OCCUPANT", PermanentStructureobj.Occupant);
                dcmd.Parameters.AddWithValue("P_OTHEROCCUPANTNAME", PermanentStructureobj.OtherOccupantName);

                if (PermanentStructureobj.OccupantStatusID > 0)
                    dcmd.Parameters.AddWithValue("P_OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);
                else
                    dcmd.Parameters.AddWithValue("P_OCCUPANTSTATUSID", DBNull.Value);

                //dcmd.Parameters.AddWithValue("P_OCCUPANTSTATUSID", PermanentStructureobj.OccupantStatusID);

                dcmd.Parameters.AddWithValue("P_OTHEROCCUPANTSTATUS", PermanentStructureobj.OtherOccupantStatus);
                dcmd.Parameters.AddWithValue("P_DIMEN_LENGTH", PermanentStructureobj.DimensionLength);
                dcmd.Parameters.AddWithValue("P_DIMEN_WIDTH", PermanentStructureobj.DimensionWidth);
                dcmd.Parameters.AddWithValue("P_NOOFROOMS", PermanentStructureobj.NoOfRooms);
                dcmd.Parameters.AddWithValue("P_SURFACEAREA", PermanentStructureobj.SurfaceArea);
                dcmd.Parameters.AddWithValue("P_DEPRECIATEDVALUE", PermanentStructureobj.DepreciatedValue);
                dcmd.Parameters.AddWithValue("P_REPLACEMENTVALUE", PermanentStructureobj.ReplacementValue);
                dcmd.Parameters.AddWithValue("P_ADDITIONALCOMMENTS", PermanentStructureobj.AdditionalComments);
                // dcmd.Parameters.AddWithValue("PAPPSPHOTO_", PermanentStructureobj.Photo); //save the photo in blob
                //dcmd.Parameters.AddWithValue("ISDELETED", PermanentStructureobj.IsDeleted);
                dcmd.Parameters.AddWithValue("P_UPDATEDBY", PermanentStructureobj.CreatedBy);
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
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            cnn.Open();
            SqlCommand dcmd = new SqlCommand("USP_TRN_UPD_PHOTO_PERMSTRUCT", cnn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    int count = Convert.ToInt32(dcmd.CommandType);
            try
            {
                dcmd.Parameters.AddWithValue("P_PERM_STRUCTUREID", PermanentStructureobj1.PermanentStructureID);
                dcmd.Parameters.AddWithValue("P_HHID", PermanentStructureobj1.HouseholdID);
                dcmd.Parameters.Add(new SqlParameter("PAPPSPHOTO_", SqlDbType.Image)).Value = PermanentStructureobj1.Photo;
                dcmd.Parameters.AddWithValue("P_UPDATEDBY", PermanentStructureobj1.UpdatedBy);
                 
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
            SqlConnection myConnection;
            SqlCommand myCommand;
            myConnection = new SqlConnection(AppConfiguration.ConnectionString);
            myCommand = new SqlCommand("USP_TRN_GET_PAPPS_PHOTO", myConnection);
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("HHID_", householdID);
            myCommand.Parameters.AddWithValue("PermanentStructureID_", PermanentStructureID);
            // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

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