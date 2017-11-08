using System;
using System.Data.SqlClient;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class RelationshipDAL
    {
        /// <summary>
        /// To Get ALL Relationship
        /// </summary>
        /// <returns></returns>
        public RELATIONSHIPLIST GetALLRelationship()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GETALL_RELATIONSHIP";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RelationshipBO objRelr = null; ;
            RELATIONSHIPLIST Rels = new RELATIONSHIPLIST();

            while (dr.Read())
            {
                objRelr = new RelationshipBO();
                objRelr.RELATIONSHIPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RELATIONSHIPID")));
                objRelr.RELATIONSHIP = dr.GetValue(dr.GetOrdinal("RELATION")).ToString();
                objRelr.IsDeleted = dr.GetValue(dr.GetOrdinal("IsDeleted")).ToString();
                Rels.Add(objRelr);
            }

            dr.Close();
            return Rels;
        }

        /// <summary>
        /// To Get Relationship
        /// </summary>
        /// <returns></returns>
        public RELATIONSHIPLIST GetRelationship()
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_RELATIONSHIP";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RelationshipBO objRelr = null; ;
            RELATIONSHIPLIST Rels = new RELATIONSHIPLIST();

            while (dr.Read())
            {
                objRelr = new RelationshipBO();
                objRelr.RELATIONSHIPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RELATIONSHIPID")));
                objRelr.RELATIONSHIP = dr.GetValue(dr.GetOrdinal("RELATION")).ToString();
                objRelr.IsDeleted = dr.GetValue(dr.GetOrdinal("IsDeleted")).ToString();
                //objRelr.RELATIONSHIP(objRelr);
                Rels.Add(objRelr);
            }

            dr.Close();
            return Rels;
        }

        /// <summary>
        /// To Get Relationship By ID
        /// </summary>
        /// <param name="relationshipID"></param>
        /// <returns></returns>
        public RelationshipBO GetRelationshipByID(int relationshipID)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;

            string proc = "USP_MST_GET_RELATIONSHIPBYID";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("RELATIONSHIPID", relationshipID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            RelationshipBO objRelr = null;

            while (dr.Read())
            {
                objRelr = new RelationshipBO();
                objRelr.RELATIONSHIPID = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("RELATIONSHIPID")));
                objRelr.RELATIONSHIP = dr.GetValue(dr.GetOrdinal("RELATION")).ToString();
            }

            dr.Close();
            return objRelr;
        }

        /// <summary>
        /// To Add Relation
        /// </summary>
        /// <param name="objRelr"></param>
        /// <returns></returns>
        public string AddRelation(RelationshipBO objRelr)
        {

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            string returnResult = string.Empty;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_MST_INS_RELATIONSHIP", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                //Relationship objrel = new Relationship();
                myCommand.Parameters.AddWithValue("@RELATION_", objRelr.RELATIONSHIP);
                myCommand.Parameters.AddWithValue("@ISDELETEDIN",  "False");
                myCommand.Parameters.AddWithValue("@CREATEDBY",objRelr.UserID);
                con.Open();
                //result = myCommand.ExecuteNonQuery();

                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    returnResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

                
                con.Close();
            }
            //return objRelr;
            return returnResult;
        }

        /// <summary>
        /// To Delete Relation
        /// </summary>
        /// <param name="RELATIONSHIPID"></param>
        /// <returns></returns>
        public string DeleteRelation(int RELATIONSHIPID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_RELATIONSHIP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("RELATIONSHIPID_", RELATIONSHIPID);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
        /// To Update Relation
        /// </summary>
        /// <param name="objRel"></param>
        /// <returns></returns>
        public string UpdateRelation(RelationshipBO objRel)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            string returnResult = string.Empty;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_MST_UPD_RELATIONSHIP", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("RELATIONSHIPID", objRel.RELATIONSHIPID);
                if (string.IsNullOrEmpty(objRel.RELATIONSHIP) == true)
                {
                    myCommand.Parameters.AddWithValue("RELATION", objRel.RELATIONSHIP);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("RELATION", objRel.RELATIONSHIP);
                    //myCommand.Parameters.AddWithValue("RELATION", objRel.RELATIONSHIP);
                }
                myCommand.Parameters.AddWithValue("UPDATEDBY", objRel.UserID);
                //myCommand.Parameters.AddWithValue("UPDATEDBY", UPDATEDBY);
                con.Open();
                //result = myCommand.ExecuteNonQuery();

                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;

                myCommand.ExecuteNonQuery();

                if (myCommand.Parameters["errorMessage_"].Value != null)
                    returnResult = myCommand.Parameters["errorMessage_"].Value.ToString();
                else
                    returnResult = string.Empty;

              //  return returnResult;
                con.Close();
              
                }
            return returnResult;
            }

        /// <summary>
        /// To Obsolete Relationship
        /// </summary>
        /// <param name="RELATIONSHIPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRelationship (int RELATIONSHIPID, string IsDeleted)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_OBSOLETE_RELATIONSHIP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("RELATIONSHIPID_",RELATIONSHIPID);
                myCommand.Parameters.AddWithValue("isdeleted_", IsDeleted);
                myCommand.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
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
