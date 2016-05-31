using System;
using System.Data;
using Oracle.DataAccess.Client;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
 public  class OptionGroupParametersDAL
    {
        string con = AppConfiguration.ConnectionString;
        OracleConnection cnn;
        OracleCommand cmd;
        string proc = string.Empty;
        public string SaveOptionGroup(OptionGroupParametersBO objOptionGroupParametersBO) 
        {
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_INS_OPTIONGRPPARAMETER"; 

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            cmd.Parameters.Add("OPTIONGROUPID_", objOptionGroupParametersBO.OptionGrpID);
            cmd.Parameters.Add("LANDSTATUSID_", objOptionGroupParametersBO.OptionstatusID);
            cmd.Parameters.Add("ISRESIDENT_", objOptionGroupParametersBO.IsResident);
            cmd.Parameters.Add("LANDCOMPENSATION_", objOptionGroupParametersBO.LandCompensation);
            cmd.Parameters.Add("HOUSECOMPENSATION_", objOptionGroupParametersBO.HouseCompensation);
             cmd.Parameters.Add("CREATEDBY_", objOptionGroupParametersBO.Createdby);
           cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);
            OracleCommand cmd;
            string proc = "USP_GET_ALL_OPTIONGRP";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("Sp_recordset", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            cnn = new OracleConnection(con);
            OptionGroupParametersBO objGrpParam = null;

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("paraID_", paramID);

            cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            cnn = new OracleConnection(con);
            string returnResult = string.Empty;
            proc = "USP_MST_UPD_OPTIONGRP";

            cmd = new OracleCommand(proc, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.Parameters.Add("paramID_", objOptionGroupParametersBO.ParamID);
            cmd.Parameters.Add("OptionGrpID_", objOptionGroupParametersBO.OptionGrpID);
            cmd.Parameters.Add("OptionStatusID_", objOptionGroupParametersBO.OptionstatusID);
            cmd.Parameters.Add("IsResident_", objOptionGroupParametersBO.IsResident);
            cmd.Parameters.Add("landCompen_", objOptionGroupParametersBO.LandCompensation);
            cmd.Parameters.Add("HouseCompen_", objOptionGroupParametersBO.HouseCompensation);
            cmd.Parameters.Add("updatedBy_", objOptionGroupParametersBO.UpdatedBy);
            cmd.Parameters.Add("errorMessage_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
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
            OracleConnection myConnection = null;
            OracleCommand myCommand = null;

            string result = string.Empty;
            try
            {
                myConnection = new OracleConnection(AppConfiguration.ConnectionString);
                myCommand = new OracleCommand("USP_MST_DEL_OPTIONGROUP", myConnection);
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("ParamID_", paramID);
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
