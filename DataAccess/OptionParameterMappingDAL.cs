using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_DataAccess
{
  public  class OptionParameterMappingDAL
  {
      string connStr = AppConfiguration.ConnectionString;
      OracleConnection cnn;
      OracleCommand cmd;
      string proc = string.Empty;

      
      /// <summary>
      /// To Get All Option Parameter 
      /// </summary>
      /// <param name="subcountyid"></param>
      /// <param name="countyid"></param>
      /// <param name="districtid"></param>
      /// <returns></returns>
      public OptionParameterMappingList GetAllOptionParameterMapping()
      {
          OptionParameterMappingList lstOptPrmMapping = null;

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_GET_ALL_OPT_PRM_MAPPING", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  //cmd.Parameters.Add("SUBCOUNTYID_", subcountyid);

                  cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  cmd.Connection.Open();

                  OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                  OptionParameterMappingBO optPrmMappingBO = null;
                  lstOptPrmMapping = new OptionParameterMappingList();

                  while (dr.Read())
                  {
                      optPrmMappingBO = new OptionParameterMappingBO();

                      optPrmMappingBO = Mapping(dr);
                      lstOptPrmMapping.Add(optPrmMappingBO);
                  }

                  dr.Close();
              }
          }

          return lstOptPrmMapping;
      }

      private OptionParameterMappingBO Mapping(IDataReader reader)
      {
          OptionParameterMappingBO optPrmMappingBO = new OptionParameterMappingBO();
          if (ColExists(reader, "OPTPARID") && !reader.IsDBNull(reader.GetOrdinal("OPTPARID")))
              optPrmMappingBO.OptParID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("OPTPARID")));

          if (ColExists(reader, "OPTIONGROUPID") && !reader.IsDBNull(reader.GetOrdinal("OPTIONGROUPID")))
              optPrmMappingBO.OptionGroupID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("OPTIONGROUPID")));

          if (ColExists(reader, "ID") && !reader.IsDBNull(reader.GetOrdinal("ID")))
              optPrmMappingBO.OptionAvailableID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ID")));

          if (ColExists(reader, "DESCRIPTIONID") && !reader.IsDBNull(reader.GetOrdinal("DESCRIPTIONID")))
              optPrmMappingBO.DescriptionID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("DESCRIPTIONID")));

          if (ColExists(reader, "PARAMETERID") && !reader.IsDBNull(reader.GetOrdinal("PARAMETERID")))
              optPrmMappingBO.ParameterID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("PARAMETERID")));
          //----------------------------------------------------------------------------------------------------

          if (ColExists(reader, "OPTIONGROUP") && !reader.IsDBNull(reader.GetOrdinal("OPTIONGROUP")))
              optPrmMappingBO.OptionGroup = reader.GetString(reader.GetOrdinal("OPTIONGROUP"));

          if (ColExists(reader, "OPTIONAVAILABLE") && !reader.IsDBNull(reader.GetOrdinal("OPTIONAVAILABLE")))
              optPrmMappingBO.OptionAvailable = reader.GetString(reader.GetOrdinal("OPTIONAVAILABLE"));

          if (ColExists(reader, "DESCRIPTION") && !reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")))
              optPrmMappingBO.Description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));

          if (ColExists(reader, "PARAMETERNAME") && !reader.IsDBNull(reader.GetOrdinal("PARAMETERNAME")))
              optPrmMappingBO.ParameterName = reader.GetString(reader.GetOrdinal("PARAMETERNAME"));

          if (ColExists(reader, "IsDeleted") && !reader.IsDBNull(reader.GetOrdinal("IsDeleted")))
              optPrmMappingBO.IsDeleted = reader.GetString(reader.GetOrdinal("IsDeleted"));
          return optPrmMappingBO;
      }

      /// <summary>
      /// To check weather Column Exists or Not
      /// </summary>
      /// <param name="reader"></param>
      /// <param name="columnName"></param>
      /// <returns></returns>
      public Boolean ColExists(IDataReader reader, string columnName)
      {
          //string[] ColumnNames = new string[20];
          for (int i = 0; i < reader.FieldCount; i++)
          {
              //ColumnNames[i] = reader.GetName(i);

              if (reader.GetName(i).ToLower() == columnName.ToLower())
              {
                  return true;
              }
          }

          return false;
      }
      /// <summary>
      /// To Add Parish
      /// </summary>
      /// <param name="ParishBOobj"></param>
      /// <returns></returns>
      public String AddOptionParameterMapping(OptionParameterMappingBO pOptParmMappingBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_INS_OPT_PRM_MAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();
                  cmd.Parameters.Add("OptionGroupID_", pOptParmMappingBO.OptionGroupID);
                  cmd.Parameters.Add("OptionAvailableID_", pOptParmMappingBO.OptionAvailableID);
                  cmd.Parameters.Add("DescriptionID_", pOptParmMappingBO.DescriptionID);
                  cmd.Parameters.Add("ParameterID_", pOptParmMappingBO.ParameterID);
                  cmd.Parameters.Add("CREATEDBY_", pOptParmMappingBO.CreatedBy);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Update Option Parameter
      /// </summary>
      /// <param name="ParishBOobj"></param>
      /// <returns></returns>
      public String UpdateOptionParameterMapping(OptionParameterMappingBO pOptParmMappingBO)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_UPD_OPT_PRM_MAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("OptParID_", pOptParmMappingBO.OptParID);
                  cmd.Parameters.Add("DescriptionID_", pOptParmMappingBO.DescriptionID);
                  cmd.Parameters.Add("OptionAvailableID_", pOptParmMappingBO.OptionAvailableID);
                  cmd.Parameters.Add("OptionGroupID_", pOptParmMappingBO.OptionGroupID);
                  cmd.Parameters.Add("ParameterID_", pOptParmMappingBO.ParameterID);
                  cmd.Parameters.Add("UPDATEDBY_", pOptParmMappingBO.UpdatedBy);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Delete Option Parameter
      /// </summary>
      /// <param name="ParishId"></param>
      /// <returns></returns>
      public string DeleteOptionParameterMapping(int pOptParameterId)
      {
          string result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_DEL_OPT_PRM_MAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("OptParID_", pOptParameterId);
                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// Obsolete Option Parameter
      /// </summary>
      /// <param name="ParishId"></param>
      /// <param name="isDeleted"></param>
      /// <param name="updatedBy"></param>
      /// <returns></returns>
      public String ObsoleteOptionalParameterMapping(Int32 pOptParameterId, String isDeleted, Int32 updatedBy)
      {
          String result = "";

          using (cnn = new OracleConnection(connStr))
          {
              using (cmd = new OracleCommand("USP_MST_OBS_OPT_PRM_MAP", cnn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Connection.Open();

                  cmd.Parameters.Add("OptParID_", pOptParameterId);
                  cmd.Parameters.Add("ISDELETED_", isDeleted);
                  cmd.Parameters.Add("UPDATEDBY_", updatedBy);

                  cmd.Parameters.Add("ERRORMESSAGE_", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;
                  cmd.ExecuteNonQuery();

                  if (cmd.Parameters["ERRORMESSAGE_"].Value != null)
                      result = cmd.Parameters["ERRORMESSAGE_"].Value.ToString();

                  cmd.Connection.Close();
              }
          }

          return result;
      }

      /// <summary>
      /// To Get Option Parameter By Id
      /// </summary>
      /// <param name="ParishId"></param>
      /// <returns></returns>
      public OptionParameterMappingBO GetOptionalParameterMappingById(Int32 pOptParameterId)
      {
          proc = "USP_GET_OPT_PRM_MAPPING_BYID";

          cnn = new OracleConnection(connStr);
          OptionParameterMappingBO oOptPrmMappingBO = null;

          cmd = new OracleCommand(proc, cnn);
          cmd.CommandType = CommandType.StoredProcedure;

          cmd.Parameters.Add("OptParID_", pOptParameterId);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          try
          {
              cmd.Connection.Open();
              OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
              while (dr.Read())
              {
                  oOptPrmMappingBO = new OptionParameterMappingBO();

                  oOptPrmMappingBO = Mapping(dr);
              }
              dr.Close();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return oOptPrmMappingBO;
      }

      /// <summary>
      /// To Get Option Available
      /// </summary>
      /// <param name="SearchParish"></param>
      /// <returns></returns>


      public OptionParameterList GetOptionAvailable()
      {
          OracleConnection con = new OracleConnection(connStr);
          OracleCommand cmd;
          string proc = "USP_MST_GET_OPTIONS";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          OptionParameterBO oOptionParameterBO = null;
          OptionParameterList objcountlist = new OptionParameterList();

          while (dr.Read())
          {
              oOptionParameterBO = new OptionParameterBO();
              oOptionParameterBO.AvailableOptionsID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID"))));
              oOptionParameterBO.AvailableOptions = dr.GetValue(dr.GetOrdinal("OPTIONAVAILABLE")).ToString();
              objcountlist.Add(oOptionParameterBO);
          }

          dr.Close();
          return objcountlist;
      }

      public OptionGroupList GetOptionGroup()
      {
          OracleConnection con = new OracleConnection(connStr);
          OracleCommand cmd;
          string proc = "USP_MST_GET_OPTIONGROUPS";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          OptionGroupBO oOptionGroupBO = null;
          OptionGroupList lstOptionGroup = new OptionGroupList();

          while (dr.Read())
          {
              oOptionGroupBO = new OptionGroupBO();
              oOptionGroupBO.OptionGroupID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID"))));
              oOptionGroupBO.OptionGroupName = dr.GetValue(dr.GetOrdinal("Name")).ToString();
              lstOptionGroup.Add(oOptionGroupBO);
          }

          dr.Close();
          return lstOptionGroup;
      }

      public OptionGroupList GetOptionDescription(int Pid)
      {
          OracleConnection con = new OracleConnection(connStr);
          OracleCommand cmd;
          string proc = "USP_DSH_GET_OPT_DESCRIPTION";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("PID_", Pid);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          OptionGroupBO oOptionGroupBO = null;
          OptionGroupList lstOptionGroup = new OptionGroupList();

          while (dr.Read())
          {
              oOptionGroupBO = new OptionGroupBO();
              oOptionGroupBO.OptionGroupID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DESCRIPTIONID"))));
              oOptionGroupBO.OptionGroupName = dr.GetValue(dr.GetOrdinal("DESCRIPTION")).ToString();
              lstOptionGroup.Add(oOptionGroupBO);
          }

          dr.Close();
          return lstOptionGroup;
      }

      public OptionParameterMappingList GetOptionParameters(int Pid)
      {
          OracleConnection con = new OracleConnection(connStr);
          OracleCommand cmd;
          string proc = "USP_GET_PARAMETERS";
          cmd = new OracleCommand(proc, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("OPTIONAVAILABLEID_", Pid);
          cmd.Parameters.Add("Sp_recordset", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
          cmd.Connection.Open();
          OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
          OptionParameterMappingBO oOptionParameterBO = null;
          OptionParameterMappingList lstOptionGroup = new OptionParameterMappingList();

          while (dr.Read())
          {
              oOptionParameterBO = new OptionParameterMappingBO();
              oOptionParameterBO.ID = (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PARAMETERID"))));
              oOptionParameterBO.Name = dr.GetValue(dr.GetOrdinal("PARAMETERNAME")).ToString();
              lstOptionGroup.Add(oOptionParameterBO);
          }

          dr.Close();
          return lstOptionGroup;
      }
  }

}
