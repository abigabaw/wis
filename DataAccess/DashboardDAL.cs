﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
    public class DashboardDAL
    {
        string con = AppConfiguration.ConnectionString;
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_RecentPAPSList GetRecentPAPSByUser(int userID)
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_RecentPAPSBO objRecentPAPS = null;

            DSH_RecentPAPSList RecentPAPS = new DSH_RecentPAPSList();

            OracleCommand cmd = new OracleCommand("USP_DSH_RECENT_PAPS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("USERID_", userID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objRecentPAPS = new DSH_RecentPAPSBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) objRecentPAPS.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HHID"))) objRecentPAPS.HouseholdID = dr.GetInt32(dr.GetOrdinal("HHID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPName"))) objRecentPAPS.PAPName = dr.GetString(dr.GetOrdinal("PAPName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) objRecentPAPS.ProjectID = dr.GetInt32(dr.GetOrdinal("ProjectID"));

                    RecentPAPS.Add(objRecentPAPS);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RecentPAPS;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjects(int userID)
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_PAPStatusBO objPAPStatus = null;

            DSH_PAPStatusList PAPStatusList = new DSH_PAPStatusList();

            OracleCommand cmd = new OracleCommand("USP_DSH_GET_PROJECTSFORHOME", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("USERID_", userID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPStatus = new DSH_PAPStatusBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) objPAPStatus.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) objPAPStatus.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) objPAPStatus.ProjectId = dr.GetInt32(dr.GetOrdinal("ProjectID"));

                    PAPStatusList.Add(objPAPStatus);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PAPStatusList;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetRecentProject(int userID)
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_PAPStatusBO objPAPStatus = null;

            DSH_PAPStatusList PAPStatusList = new DSH_PAPStatusList();

            OracleCommand cmd = new OracleCommand("USP_DSH_GET_RECENTPROJECT", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("USERID_", userID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPStatus = new DSH_PAPStatusBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectName"))) objPAPStatus.ProjectName = dr.GetString(dr.GetOrdinal("ProjectName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) objPAPStatus.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectID"))) objPAPStatus.ProjectId = dr.GetInt32(dr.GetOrdinal("ProjectID"));

                    PAPStatusList.Add(objPAPStatus);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PAPStatusList;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPStatus(int PROJECTID)
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_PAPStatusBO objPAPStatus = null;

            DSH_PAPStatusList PAPStatusList = new DSH_PAPStatusList();

            OracleCommand cmd = new OracleCommand("USP_DSH_PAPSTATUS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PROJECTIDIN", PROJECTID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPStatus = new DSH_PAPStatusBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTNAME"))) objPAPStatus.ProjectName = dr.GetString(dr.GetOrdinal("PROJECTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ProjectCode"))) objPAPStatus.ProjectCode = dr.GetString(dr.GetOrdinal("ProjectCode"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPCount"))) objPAPStatus.PAPCount = dr.GetInt32(dr.GetOrdinal("PAPCount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPPaidCount"))) objPAPStatus.PAPPaidCount = dr.GetInt32(dr.GetOrdinal("PAPPaidCount"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PAPPendingPayCount"))) objPAPStatus.PAPPendingPayCount = dr.GetInt32(dr.GetOrdinal("PAPPendingPayCount"));

                    PAPStatusList.Add(objPAPStatus);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PAPStatusList;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPStatusForPie()
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_PAPStatusBO objPAPStatus = null;

            DSH_PAPStatusList PAPStatusList = new DSH_PAPStatusList();

            OracleCommand cmd = new OracleCommand("USP_DSH_PAPSTATUSFORPIEHOME", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPStatus = new DSH_PAPStatusBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("PROJECTSTATUS"))) objPAPStatus.ProjectStatus = Convert.ToString(dr.GetValue(dr.GetOrdinal("PROJECTSTATUS")));
                    if (!dr.IsDBNull(dr.GetOrdinal("StatuCount"))) objPAPStatus.StatuCount = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("StatuCount")));

                    PAPStatusList.Add(objPAPStatus);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PAPStatusList;
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPBudgetForSpline(int PROJECTID)
        {
            OracleConnection cnn = new OracleConnection(con);
            DSH_PAPStatusBO objPAPStatus = null;

            DSH_PAPStatusList PAPStatusList = new DSH_PAPStatusList();

            OracleCommand cmd = new OracleCommand("USP_DSH_PAPBUDGET", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PROJECTIDIN", PROJECTID);
            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objPAPStatus = new DSH_PAPStatusBO();

                    if (!dr.IsDBNull(dr.GetOrdinal("est_value"))) objPAPStatus.est_value = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("est_value")));
                    if (!dr.IsDBNull(dr.GetOrdinal("expenseamount"))) objPAPStatus.expenseamount = Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("expenseamount")));
                    if (!dr.IsDBNull(dr.GetOrdinal("BudDate"))) objPAPStatus.BudDate = dr.GetString(dr.GetOrdinal("BudDate"));

                    PAPStatusList.Add(objPAPStatus);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PAPStatusList;
        }

    }
}
