﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_PRIVATE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("HHID_", householdID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_SPOSE";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PRIVATELANDIDIN", PRIVATELANDID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_TRN_GET_PAP_LND_CHILD";
            cmd = new OracleCommand(proc, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PRIVATELANDIDIN", PRIVATELANDID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_PAP_LND_PRIVATE", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("HOUSEHOLDID_", objLF.HIDPriv);
                myCommand.Parameters.Add("LND_TENUREID_", objLF.Lnd_TENUREIDPriv);
                myCommand.Parameters.Add("LANDLORDNAME_", objLF.LANDLORDNAME);
                myCommand.Parameters.Add("CLAIMANTNAME_", objLF.CLAIMANTNAME);
                myCommand.Parameters.Add("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN);
                myCommand.Parameters.Add("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE);
                myCommand.Parameters.Add("DOSPOUSESFARM_", objLF.DOSPOUSESFARM);
                myCommand.Parameters.Add("DOCHILDRENFARM_", objLF.DOCHILDRENFARM);
                myCommand.Parameters.Add("AGREEMENTTYPE_", objLF.AGREEMENTTYPE);
                myCommand.Parameters.Add("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES);
                myCommand.Parameters.Add("CREATEDBY", objLF.Createby);
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();
            }

            OracleCommand myCommand1;
            myCommand1 = new OracleCommand("USP_TRN_DEL_LND_CHILD", con);
            myCommand1.Connection = con;
            myCommand1.CommandType = CommandType.StoredProcedure;
            myCommand1.Parameters.Add("HHID_", objLF.HIDPriv);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                for (int i = 0; i < RelationsSpose.Count; i++)
                {
                    OracleCommand myCommand;
                    myCommand = new OracleCommand("USP_TRN_UPD_LND_SPOSE", con);
                    myCommand.Connection = con;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add("RelationID_", RelationsSpose[i].RelationID);
                    myCommand.Parameters.Add("HHID_", RelationsSpose[i].HouseholdID);
                    myCommand.Parameters.Add("CREATEDBY", RelationsSpose[i].CreatedBy);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                try
                {
                    for (int i = 0; i < RelationsChild.Count; i++)
                    {
                        OracleCommand myCommand;
                        myCommand = new OracleCommand("USP_TRN_UPD_LND_CHILD", con);
                        myCommand.Connection = con;
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.Add("RelationID_", RelationsChild[i].RelationID);
                        myCommand.Parameters.Add("HHID_", RelationsChild[i].HouseholdID);
                        myCommand.Parameters.Add("CREATEDBY", RelationsChild[i].CreatedBy);
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
            OracleConnection con = new OracleConnection(AppConfiguration.ConnectionString);
            int result = 0;
            {
                OracleCommand myCommand;
                myCommand = new OracleCommand("USP_TRN_UPD_PAP_LND_PRIVATE", con);
                myCommand.Connection = con;
                myCommand.CommandType = CommandType.StoredProcedure;

                //if (string.IsNullOrEmpty(objLF.LANDLORDNAME) == true)
                //{
                //    myCommand.Parameters.Add("LND_TENUREID_", objLF.Lnd_TENUREIDPriv).Value = "";
                //    myCommand.Parameters.Add("LANDLORDNAME_", objLF.LANDLORDNAME).Value="";
                //    myCommand.Parameters.Add("CLAIMANTNAME_", objLF.CLAIMANTNAME).Value="";
                //    myCommand.Parameters.Add("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN).Value="";
                //    myCommand.Parameters.Add("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE).Value="";
                //    myCommand.Parameters.Add("DOSPOUSESFARM_", objLF.DOSPOUSESFARM).Value="";
                //    myCommand.Parameters.Add("DOCHILDRENFARM_", objLF.DOCHILDRENFARM).Value="";
                //    myCommand.Parameters.Add("AGREEMENTTYPE_", objLF.AGREEMENTTYPE).Value="";
                //    myCommand.Parameters.Add("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES).Value="";

                //}
                //else
                //{
                myCommand.Parameters.Add("HHID", objLF.HIDPriv);
                myCommand.Parameters.Add("LND_TENUREID_", objLF.Lnd_TENUREIDPriv);
                myCommand.Parameters.Add("LANDLORDNAME_", objLF.LANDLORDNAME);
                myCommand.Parameters.Add("CLAIMANTNAME_", objLF.CLAIMANTNAME);
                myCommand.Parameters.Add("WHENFARMINGBEGAN_", objLF.WHENFARMINGBEGAN);
                myCommand.Parameters.Add("WHEREFARMEDBEFORE_", objLF.WHEREFARMEDBEFORE);
                myCommand.Parameters.Add("DOSPOUSESFARM_", objLF.DOSPOUSESFARM);
                myCommand.Parameters.Add("DOCHILDRENFARM_", objLF.DOCHILDRENFARM);
                myCommand.Parameters.Add("AGREEMENTTYPE_", objLF.AGREEMENTTYPE);
                myCommand.Parameters.Add("PRODASSETOPPORTUNITIES_", objLF.PRODASSETOPPORTUNITIES);
                myCommand.Parameters.Add("CREATEDBY", objLF.Useridpriv);

                //}

                //myCommand.Parameters.Add("CREATEDBY", OracleDbType.Int64, 5).Value = 1;
                con.Open();
                result = myCommand.ExecuteNonQuery();
                con.Close();

            }

            OracleCommand myCommand1;
            myCommand1 = new OracleCommand("USP_TRN_DEL_LND_CHILD", con);
            myCommand1.Connection = con;
            myCommand1.CommandType = CommandType.StoredProcedure;
            myCommand1.Parameters.Add("HHID_", objLF.HIDPriv);
            con.Open();
            result = myCommand1.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
    
    
}