USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_MST_CULTUREPROPERTY]    Script Date: 11/8/2017 11:52:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_SEL_MST_CULTUREPROPERTY]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN


      SELECT MST_CULTURE_PROP_TYPE.CULTUREPROPTYPEID, MST_CULTURE_PROP_TYPE.CULTUREPROPTYPE, MST_CULTURE_PROP_TYPE.ISDELETED
      FROM dbo.MST_CULTURE_PROP_TYPE
      ORDER BY MST_CULTURE_PROP_TYPE.CULTUREPROPTYPE

   END