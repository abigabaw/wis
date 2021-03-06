﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
   public class LivBudgCategoryDAL
    {
        #region Declaration Section
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        #endregion

       /// <summary>
        /// To Insert Budget Category
       /// </summary>
       /// <param name="LivBudgCategoryBOobj"></param>
       /// <returns></returns>
       public string InsertBudCategory(LivBudgCategoryBO LivBudgCategoryBOobj)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_INS_LIV_BUDGCATG";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();
           cmd.Parameters.Add("LIV_BUD_CATEGORYNAME_", LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME);
           cmd.Parameters.Add("CREATEDBY_", LivBudgCategoryBOobj.CREATEDBY);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Update Living Budget Category
       /// </summary>
       /// <param name="LivBudgCategoryBOobj"></param>
       /// <returns></returns>
       public string UpdateLivBudgCategory(LivBudgCategoryBO LivBudgCategoryBOobj)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_UPD_LIVBUDGCATG";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_CATEGID_", LivBudgCategoryBOobj.lIV_BUD_CATEGID);
           cmd.Parameters.Add("LIV_BUD_CATEGORYNAME_", LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME);
           cmd.Parameters.Add("UPDATEDBY_", LivBudgCategoryBOobj.UPDATEDBY);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Get Living Budget Category By ID
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <returns></returns>
       public LivBudgCategoryBO GetLivBudCategoryByID(int livBudgCatgId)
       {
           proc = "USP_GET_MST_LIVBUDCATGBYID";

           cnn = new OracleConnection(con);
           LivBudgCategoryBO LivBudgCategoryBOobj = null;

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("LIV_BUD_CATEGID_", livBudgCatgId);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           try
           {
               cmd.Connection.Open();
               OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               while (dr.Read())
               {
                   LivBudgCategoryBOobj = new LivBudgCategoryBO();

                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_CATEGID"))) LivBudgCategoryBOobj.lIV_BUD_CATEGID = dr.GetInt32(dr.GetOrdinal("LIV_BUD_CATEGID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_CATEGORYNAME"))) LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME = dr.GetString(dr.GetOrdinal("LIV_BUD_CATEGORYNAME"));
               }
               dr.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return LivBudgCategoryBOobj;
       }

       /// <summary>
       /// To Delete CDAP Budget Master
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <returns></returns>
       public string DeleteCDAPBudgetMaster(int livBudgCatgId)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_DEL_LIVBUDCATG";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_CATEGID_", livBudgCatgId);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Obsolete CDAP Budget Master
       /// </summary>
       /// <param name="livBudgCatgId"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteCDAPBudgetMaster(int livBudgCatgId, string isDeleted, int updatedBy)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_OBS_LIVBUDCATG";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_CATEGID_", livBudgCatgId);
           cmd.Parameters.Add("UPDATEDBY_", updatedBy);
           cmd.Parameters.Add("ISDELETED_", isDeleted);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Get All Living Budget Category
       /// </summary>
       /// <returns></returns>
       public List<LivBudgCategoryBO> GetAllLivBudCategory()
       {
           proc = "USP_GET_ALL_MST_LIVBUDCATG";
           cnn = new OracleConnection(con);
           LivBudgCategoryBO LivBudgCategoryBOobj = null;
           List<LivBudgCategoryBO> LivBudgCategoryBOlist = new List<LivBudgCategoryBO>();
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           try
           {
               cmd.Connection.Open();
               OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               while (dr.Read())
               {
                   LivBudgCategoryBOobj = new LivBudgCategoryBO();

                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_CATEGID"))) LivBudgCategoryBOobj.lIV_BUD_CATEGID = dr.GetInt32(dr.GetOrdinal("LIV_BUD_CATEGID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_CATEGORYNAME"))) LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME = dr.GetString(dr.GetOrdinal("LIV_BUD_CATEGORYNAME"));
                   if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) LivBudgCategoryBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                   LivBudgCategoryBOlist.Add(LivBudgCategoryBOobj);
               }
               dr.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return LivBudgCategoryBOlist;
       }

       /// <summary>
       /// To Insert Budget Sub Item
       /// </summary>
       /// <param name="LivBudgItemBOobj"></param>
       /// <returns></returns>
       public string InsertBudgetSubItem(LivBudgItemBO LivBudgItemBOobj)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_INS_BUDGCATG_ITEM";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_CATEGID_", LivBudgItemBOobj.LIV_BUD_CATEGID);
           cmd.Parameters.Add("LIV_BUD_ITEMNAME_", LivBudgItemBOobj.LIV_BUD_ITEMNAME);
           cmd.Parameters.Add("LIV_BUD_ITEMDESC_", LivBudgItemBOobj.LIV_BUD_ITEMDESC);
           cmd.Parameters.Add("CREATEDBY_", LivBudgItemBOobj.CREATEDBY);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Update Budget Sub Item
       /// </summary>
       /// <param name="LivBudgItemBOobj"></param>
       /// <returns></returns>
       public string UpdateBudgetSubItem(LivBudgItemBO LivBudgItemBOobj)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_UPD_LIVBUDCATG_ITEM";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_ITEMID_", LivBudgItemBOobj.LIV_BUD_ITEMID);
           cmd.Parameters.Add("LIV_BUD_CATEGID_", LivBudgItemBOobj.LIV_BUD_CATEGID);
           cmd.Parameters.Add("LIV_BUD_ITEMNAME_", LivBudgItemBOobj.LIV_BUD_ITEMNAME);
           cmd.Parameters.Add("LIV_BUD_ITEMDESC_", LivBudgItemBOobj.LIV_BUD_ITEMDESC);
           cmd.Parameters.Add("UPDATEDBY_", LivBudgItemBOobj.UPDATEDBY);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Delete Budget Sub Item
       /// </summary>
       /// <param name="ItemId"></param>
       /// <returns></returns>
       public string DeleteBudgetSubItem(int ItemId)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_DEL_LIVBUDCATG_ITEM";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_ITEMID_", ItemId);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Obsolete Budget Sub Item
       /// </summary>
       /// <param name="ItemId"></param>
       /// <param name="isDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteBudgetSubItem(int ItemId, string isDeleted, int updatedBy)
       {
           cnn = new OracleConnection(con);
           string returnResult = "";

           proc = "USP_MST_OBS_LIVBUDCAG_ITEM";
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Connection.Open();

           cmd.Parameters.Add("LIV_BUD_ITEMID_", ItemId);
           cmd.Parameters.Add("UPDATEDBY_", updatedBy);
           cmd.Parameters.Add("ISDELETED_", isDeleted);
           cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
           cmd.ExecuteNonQuery();

           if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
               returnResult = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

           cmd.Connection.Close();

           return returnResult;
       }

       /// <summary>
       /// To Get Budget Sub Item By ID
       /// </summary>
       /// <param name="ItemId"></param>
       /// <returns></returns>
       public LivBudgItemBO GetBudgetSubItemByID(int ItemId)
       {
           proc = "USP_GET_MST_LIVBUDCATG_ITEM";

           cnn = new OracleConnection(con);
           LivBudgItemBO LivBudgItemBOobj = null;

           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("LIV_BUD_ITEMID_", ItemId);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
           try
           {
               cmd.Connection.Open();
               OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               while (dr.Read())
               {
                   LivBudgItemBOobj = new LivBudgItemBO();

                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMID"))) LivBudgItemBOobj.LIV_BUD_ITEMID = dr.GetInt32(dr.GetOrdinal("LIV_BUD_ITEMID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMNAME"))) LivBudgItemBOobj.LIV_BUD_ITEMNAME = dr.GetString(dr.GetOrdinal("LIV_BUD_ITEMNAME"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMDESC"))) LivBudgItemBOobj.LIV_BUD_ITEMDESC = dr.GetString(dr.GetOrdinal("LIV_BUD_ITEMDESC"));
               }
               dr.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return LivBudgItemBOobj;
       }

       /// <summary>
       /// To Get All Budget Sub Items
       /// </summary>
       /// <param name="ItemId"></param>
       /// <returns></returns>
       public List<LivBudgItemBO> GetAllBudgetSubItems(int ItemId)
       {
           proc = "USP_GET_ALL_MST_LIVCATG_ITEM";
           cnn = new OracleConnection(con);
           LivBudgItemBO LivBudgItemBOobj = null;
           List<LivBudgItemBO> LivBudgItemlist = new List<LivBudgItemBO>();
           cmd = new OracleCommand(proc, cnn);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.Add("LIV_BUD_CATEGID_", ItemId);
           cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

           try
           {
               cmd.Connection.Open();
               OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               while (dr.Read())
               {
                   LivBudgItemBOobj = new LivBudgItemBO();

                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMID"))) LivBudgItemBOobj.LIV_BUD_ITEMID = dr.GetInt32(dr.GetOrdinal("LIV_BUD_ITEMID"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMNAME"))) LivBudgItemBOobj.LIV_BUD_ITEMNAME = dr.GetString(dr.GetOrdinal("LIV_BUD_ITEMNAME"));
                   if (!dr.IsDBNull(dr.GetOrdinal("LIV_BUD_ITEMDESC"))) LivBudgItemBOobj.LIV_BUD_ITEMDESC = dr.GetString(dr.GetOrdinal("LIV_BUD_ITEMDESC"));
                   if (!dr.IsDBNull(dr.GetOrdinal("ISDELETED"))) LivBudgItemBOobj.ISDELETED = dr.GetString(dr.GetOrdinal("ISDELETED"));

                   LivBudgItemlist.Add(LivBudgItemBOobj);
               }
               dr.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return LivBudgItemlist;
       }
    }
}
