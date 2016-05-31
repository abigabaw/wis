using System;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GETALL_RELATIONSHIP";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_RELATIONSHIP";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;

            string proc = "USP_MST_GET_RELATIONSHIPBYID";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("RELATIONSHIPID", relationshipID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();

            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string returnResult = string.Empty;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_MST_INS_RELATIONSHIP", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                //Relationship objrel = new Relationship();
                myCommand.Parameters.Add("@RELATION_", objRelr.RELATIONSHIP);
                myCommand.Parameters.Add("@ISDELETEDIN",  "False");
                myCommand.Parameters.Add("@CREATEDBY",objRelr.UserID);
                con.Open();
                //result = myCommand.ExecuteNonQuery();

                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_RELATIONSHIP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("RELATIONSHIPID_", RELATIONSHIPID);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            string returnResult = string.Empty;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_MST_UPD_RELATIONSHIP", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("RELATIONSHIPID", objRel.RELATIONSHIPID);
                if (string.IsNullOrEmpty(objRel.RELATIONSHIP) == true)
                {
                    myCommand.Parameters.Add("RELATION", objRel.RELATIONSHIP);
                }
                else
                {
                    myCommand.Parameters.Add("RELATION", objRel.RELATIONSHIP);
                    //myCommand.Parameters.Add("RELATION", objRel.RELATIONSHIP);
                }
                myCommand.Parameters.Add("UPDATEDBY", objRel.UserID);
                //myCommand.Parameters.Add("UPDATEDBY", UPDATEDBY);
                con.Open();
                //result = myCommand.ExecuteNonQuery();

                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;
            string result = string.Empty;
            try
            {

                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_OBSOLETE_RELATIONSHIP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("RELATIONSHIPID_",RELATIONSHIPID);
                myCommand.Parameters.Add("isdeleted_", IsDeleted);
                myCommand.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
