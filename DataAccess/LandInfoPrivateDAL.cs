using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class LandInfoPrivateDAL
    {
        /// <summary>
        /// To Get Land Info Priv
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PrivateLandInfoBO GetLandInfoPriv(int householdID)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_PRIVATE";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("HHID_", householdID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            PrivateLandInfoBO objPrivateLF = null;

            while (dr.Read())
            {
                objPrivateLF = new PrivateLandInfoBO();
                if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objPrivateLF.HIDPriv = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HHID"))));
                if (!dr.IsDBNull(dr.GetOrdinal("PRIVATELANDID"))) objPrivateLF.PRIVATELANDID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PRIVATELANDID"))));
                if (!dr.IsDBNull(dr.GetOrdinal("LND_TENUREID"))) objPrivateLF.Lnd_TENUREIDPriv = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("LND_TENUREID"))));
                if (!dr.IsDBNull(dr.GetOrdinal("LANDLORDNAME"))) objPrivateLF.LANDLORDNAME = dr.GetValue(dr.GetOrdinal("LANDLORDNAME")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("CLAIMANTNAME"))) objPrivateLF.CLAIMANTNAME = dr.GetValue(dr.GetOrdinal("CLAIMANTNAME")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("WHENFARMINGBEGAN"))) objPrivateLF.WHENFARMINGBEGAN = dr.GetValue(dr.GetOrdinal("WHENFARMINGBEGAN")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("WHEREFARMEDBEFORE"))) objPrivateLF.WHEREFARMEDBEFORE = dr.GetValue(dr.GetOrdinal("WHEREFARMEDBEFORE")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("DOSPOUSESFARM"))) objPrivateLF.DOSPOUSESFARM = dr.GetValue(dr.GetOrdinal("DOSPOUSESFARM")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("DOCHILDRENFARM"))) objPrivateLF.DOCHILDRENFARM = dr.GetValue(dr.GetOrdinal("DOCHILDRENFARM")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("AGREEMENTTYPE"))) objPrivateLF.AGREEMENTTYPE = dr.GetValue(dr.GetOrdinal("AGREEMENTTYPE")).ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("PRODASSETOPPORTUNITIES"))) objPrivateLF.PRODASSETOPPORTUNITIES = dr.GetValue(dr.GetOrdinal("PRODASSETOPPORTUNITIES")).ToString();
            }

            dr.Close();
            return objPrivateLF;
        }

        /// <summary>
        /// To Get Land Info Priv Spose
        /// </summary>
        /// <param name="PRIVATELANDID"></param>
        /// <returns></returns>
        public PAPRelationList GetLandInfoPrivSpose(int PRIVATELANDID)
        {

            PAP_RelationBO objRelation = null;
            PAPRelationList Relations = new PAPRelationList();

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_SPOSE";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PRIVATELANDIDIN", PRIVATELANDID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                objRelation = new PAP_RelationBO();

                if (!dr.IsDBNull(dr.GetOrdinal("pap_hh_relationid"))) objRelation.RelationID = dr.GetInt32(dr.GetOrdinal("pap_hh_relationid"));
                if (!dr.IsDBNull(dr.GetOrdinal("privatelandid"))) objRelation.HolderTypeID = dr.GetInt32(dr.GetOrdinal("privatelandid"));

                Relations.Add(objRelation);
            }
            dr.Close();
            return Relations;
        }

        /// <summary>
        /// To Get Land Info Priv Child
        /// </summary>
        /// <param name="PRIVATELANDID"></param>
        /// <returns></returns>
        public PAPRelationList GetLandInfoPrivChild(int PRIVATELANDID)
        {

            PAP_RelationBO objRelation = null;
            PAPRelationList Relations = new PAPRelationList();

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_CHILD";
            cmd = new SqlCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("PRIVATELANDIDIN", PRIVATELANDID);
            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                objRelation = new PAP_RelationBO();

                if (!dr.IsDBNull(dr.GetOrdinal("pap_hh_relationid"))) objRelation.RelationID = dr.GetInt32(dr.GetOrdinal("pap_hh_relationid"));
                if (!dr.IsDBNull(dr.GetOrdinal("privatelandid"))) objRelation.HolderTypeID = dr.GetInt32(dr.GetOrdinal("privatelandid"));

                Relations.Add(objRelation);
            }
            dr.Close();
            return Relations;
        }

        /// <summary>
        /// To Add Land Info Priv
        /// </summary>
        /// <param name="objLF"></param>
        public void AddLandInfoPriv(PrivateLandInfoBO objLF)
        {

            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_UPD_PAP_LND_PRIVATE", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("HOUSEHOLDID_", objLF.HIDPriv);
                myCommand.Parameters.AddWithValue("LND_TENUREID_", objLF.Lnd_TENUREIDPriv);
                myCommand.Parameters.AddWithValue("LANDLORDNAME_", objLF.LANDLORDNAME);
                myCommand.Parameters.AddWithValue("CLAIMANTNAME_", objLF.CLAIMANTNAME);
                myCommand.Parameters.AddWithValue("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN);
                myCommand.Parameters.AddWithValue("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE);
                myCommand.Parameters.AddWithValue("DOSPOUSESFARM_", objLF.DOSPOUSESFARM);
                myCommand.Parameters.AddWithValue("DOCHILDRENFARM_", objLF.DOCHILDRENFARM);
                myCommand.Parameters.AddWithValue("AGREEMENTTYPE_", objLF.AGREEMENTTYPE);
                myCommand.Parameters.AddWithValue("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES);
                myCommand.Parameters.AddWithValue("CREATEDBY", objLF.Createby);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }

            SqlCommand myCommand1;
            myCommand1 = new SqlCommand("USP_TRN_DEL_LND_CHILD", con);
            myCommand1.Connection = con;
            myCommand1.CommandType = CommandType.StoredProcedure;
            myCommand1.Parameters.AddWithValue("HHID_", objLF.HIDPriv);
            con.Open();
            result = myCommand1.ExecuteNonQuery();
            con.Close();

        }

        /// <summary>
        /// To Insert Update Relations Spose
        /// </summary>
        /// <param name="RelationsSpose"></param>
        public void InsertUpdateRelationsSpose(PAPRelationList RelationsSpose)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                for (int i = 0; i < RelationsSpose.Count; i++)
                {
                    SqlCommand myCommand;
                    myCommand = new SqlCommand("USP_TRN_UPD_LND_SPOSE", con);
                    myCommand.Connection = con;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("RelationID_", RelationsSpose[i].RelationID);
                    myCommand.Parameters.AddWithValue("HHID_", RelationsSpose[i].HouseholdID);
                    myCommand.Parameters.AddWithValue("CREATEDBY", RelationsSpose[i].CreatedBy);
                    con.Open();
                    result = myCommand.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        /// <summary>
        /// To Insert Update Relations Child
        /// </summary>
        /// <param name="RelationsChild"></param>
        public void InsertUpdateRelationsChild(PAPRelationList RelationsChild)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                try
                {
                    for (int i = 0; i < RelationsChild.Count; i++)
                    {
                        SqlCommand myCommand;
                        myCommand = new SqlCommand("USP_TRN_UPD_LND_CHILD", con);
                        myCommand.Connection = con;
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.AddWithValue("RelationID_", RelationsChild[i].RelationID);
                        myCommand.Parameters.AddWithValue("HHID_", RelationsChild[i].HouseholdID);
                        myCommand.Parameters.AddWithValue("CREATEDBY", RelationsChild[i].CreatedBy);
                        con.Open();
                        result = myCommand.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// To Update Land Info Priv
        /// </summary>
        /// <param name="objLF"></param>
        /// <returns></returns>
        public int UpdateLandInfoPriv(PrivateLandInfoBO objLF)
        {
            SqlConnection con = new SqlConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                SqlCommand myCommand;
                myCommand = new SqlCommand("USP_TRN_UPD_PAP_LND_PRIVATE", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;

                //if (string.IsNullOrEmpty(objLF.LANDLORDNAME) == true)
                //{
                //    myCommand.Parameters.AddWithValue("LND_TENUREID_", objLF.Lnd_TENUREIDPriv).Value = "";
                //    myCommand.Parameters.AddWithValue("LANDLORDNAME_", objLF.LANDLORDNAME).Value="";
                //    myCommand.Parameters.AddWithValue("CLAIMANTNAME_", objLF.CLAIMANTNAME).Value="";
                //    myCommand.Parameters.AddWithValue("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN).Value="";
                //    myCommand.Parameters.AddWithValue("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE).Value="";
                //    myCommand.Parameters.AddWithValue("DOSPOUSESFARM_", objLF.DOSPOUSESFARM).Value="";
                //    myCommand.Parameters.AddWithValue("DOCHILDRENFARM_", objLF.DOCHILDRENFARM).Value="";
                //    myCommand.Parameters.AddWithValue("AGREEMENTTYPE_", objLF.AGREEMENTTYPE).Value="";
                //    myCommand.Parameters.AddWithValue("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES).Value="";

                //}
                //else
                //{
                myCommand.Parameters.AddWithValue("HHID", objLF.HIDPriv);
                myCommand.Parameters.AddWithValue("LND_TENUREID_", objLF.Lnd_TENUREIDPriv);
                myCommand.Parameters.AddWithValue("LANDLORDNAME_", objLF.LANDLORDNAME);
                myCommand.Parameters.AddWithValue("CLAIMANTNAME_", objLF.CLAIMANTNAME);
                myCommand.Parameters.AddWithValue("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN);
                myCommand.Parameters.AddWithValue("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE);
                myCommand.Parameters.AddWithValue("DOSPOUSESFARM_", objLF.DOSPOUSESFARM);
                myCommand.Parameters.AddWithValue("DOCHILDRENFARM_", objLF.DOCHILDRENFARM);
                myCommand.Parameters.AddWithValue("AGREEMENTTYPE_", objLF.AGREEMENTTYPE);
                myCommand.Parameters.AddWithValue("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES);
                myCommand.Parameters.AddWithValue("CREATEDBY", objLF.Useridpriv);

                //}

                //myCommand.Parameters.AddWithValue("CREATEDBY", SqlDbType.BigInt, 5).Value = 1;
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();

            }

            SqlCommand myCommand1;
            myCommand1 = new SqlCommand("USP_TRN_DEL_LND_CHILD", con);
            myCommand1.Connection = con;
            myCommand1.CommandType = CommandType.StoredProcedure;
            myCommand1.Parameters.AddWithValue("HHID_", objLF.HIDPriv);
            con.Open();
            result = myCommand1.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
    
    
}