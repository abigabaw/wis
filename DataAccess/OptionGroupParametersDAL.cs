using System;
using System.Data;
using System.Data.SqlClient;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
 public  class OptionGroupParametersDAL
    {
        string con = AppConfiguration.ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        string proc = string.Empty;
        public string SaveOptionGroup(OptionGroupParametersBO objOptionGroupParametersBO) 
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_OPTIONGRPPARAMETER"; 

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("OPTIONGROUPID_", objOptionGroupParametersBO.OptionGrpID);
            cmd.Parameters.AddWithValue("LANDSTATUSID_", objOptionGroupParametersBO.OptionstatusID);
            cmd.Parameters.AddWithValue("ISRESIDENT_", objOptionGroupParametersBO.IsResident);
            cmd.Parameters.AddWithValue("LANDCOMPENSATION_", objOptionGroupParametersBO.LandCompensation);
            cmd.Parameters.AddWithValue("HOUSECOMPENSATION_", objOptionGroupParametersBO.HouseCompensation);
             cmd.Parameters.AddWithValue("CREATEDBY_", objOptionGroupParametersBO.Createdby);
           cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }

        public OptionGrpParamList getdatatogrid()
        {
            SqlConnection cnn = new SqlConnection(AppConfiguration.ConnectionString);
            SqlCommand cmd;
            string proc = "USP_GET_ALL_OPTIONGRP";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //// Cmd.Parameters.AddWithValue"Sp_recordset", Sql.DataAccess.Client.SqlDbType.RefCursor.Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            OptionGroupParametersBO objOptionGroupParamBO = null;
            OptionGrpParamList OptionGroupLst = new OptionGrpParamList();

            while (dr.Read())
            {
                objOptionGroupParamBO = new OptionGroupParametersBO();
                objOptionGroupParamBO.ParamID = dr.GetInt32(dr.GetOrdinal("PARAMETERID"));
                objOptionGroupParamBO.Optiongrpname = dr.GetString(dr.GetOrdinal("OPTIONGROUP"));
                objOptionGroupParamBO.Optiongrpstatusname = dr.GetString(dr.GetOrdinal("papdesignation"));
                objOptionGroupParamBO.IsResident = dr.GetString(dr.GetOrdinal("ISRESIDENT"));
                objOptionGroupParamBO.LandCompensation = dr.GetString(dr.GetOrdinal("LANDCOMPENSATION"));
                objOptionGroupParamBO.HouseCompensation = dr.GetString(dr.GetOrdinal("HOUSECOMPENSATION"));
                OptionGroupLst.Add(objOptionGroupParamBO);
             
            }

            dr.Close();

            return OptionGroupLst;
          
           
        }
        public OptionGroupParametersBO GetOptionalDetailsByID(int paramID)
        {
            proc = "USP_GET_OPTIONGRPDETAILS_BYID";
            cnn = new SqlConnection(con);
            OptionGroupParametersBO objGrpParam = null;

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("paraID_", paramID);

            // // cmd.Parameters.AddWithValue"SP_RECORDSET", SqlDbType.RefCursor.Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    objGrpParam = new OptionGroupParametersBO();
                    if (!dr.IsDBNull(dr.GetOrdinal("PARAMETERID"))) objGrpParam.ParamID = dr.GetInt32(dr.GetOrdinal("PARAMETERID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OPTIONGROUPID"))) objGrpParam.OptionGrpID = dr.GetInt32(dr.GetOrdinal("OPTIONGROUPID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LANDSTATUSID"))) objGrpParam.OptionstatusID = dr.GetInt32(dr.GetOrdinal("LANDSTATUSID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ISRESIDENT"))) objGrpParam.IsResident = dr.GetString(dr.GetOrdinal("ISRESIDENT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LANDCOMPENSATION"))) objGrpParam.LandCompensation = dr.GetString(dr.GetOrdinal("LANDCOMPENSATION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOUSECOMPENSATION"))) objGrpParam.HouseCompensation = dr.GetString(dr.GetOrdinal("HOUSECOMPENSATION"));

                }

                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objGrpParam;
        }
        public string UpdateOptionGroup(OptionGroupParametersBO objOptionGroupParametersBO)
        {
            cnn = new SqlConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_UPD_OPTIONGRP";

            cmd = new SqlCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("paramID_", objOptionGroupParametersBO.ParamID);
            cmd.Parameters.AddWithValue("OptionGrpID_", objOptionGroupParametersBO.OptionGrpID);
            cmd.Parameters.AddWithValue("OptionStatusID_", objOptionGroupParametersBO.OptionstatusID);
            cmd.Parameters.AddWithValue("IsResident_", objOptionGroupParametersBO.IsResident);
            cmd.Parameters.AddWithValue("landCompen_", objOptionGroupParametersBO.LandCompensation);
            cmd.Parameters.AddWithValue("HouseCompen_", objOptionGroupParametersBO.HouseCompensation);
            cmd.Parameters.AddWithValue("updatedBy_", objOptionGroupParametersBO.UpdatedBy);
            cmd.Parameters.AddWithValue("errorMessage_", SqlDbType.NVarChar).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            if (cmd.Parameters["errorMessage_"].Value != null)
                returnResult = cmd.Parameters["errorMessage_"].Value.ToString();
            else
                returnResult = string.Empty;

            cmd.Connection.Close();
            return returnResult;
        }
        public string DeleteOptionGrp(int paramID)
        {
            SqlConnection myConnection = null;
            SqlCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new SqlConnection(AppConfiguration.ConnectionString);
                myCommand = new SqlCommand("USP_MST_DEL_OPTIONGROUP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("ParamID_", paramID);
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
                    result = "Can't Delete";
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


    }
}
