USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_GETALLOCCUPATION]    Script Date: 11/8/2017 10:32:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_GETALLOCCUPATION]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

   

      SELECT MST_MAIN_OCCUPATION.OCCUPATIONID, MST_MAIN_OCCUPATION.OCCUPATIONNAME, MST_MAIN_OCCUPATION.ISDELETED
      FROM dbo.MST_MAIN_OCCUPATION
      ORDER BY lower(MST_MAIN_OCCUPATION.OCCUPATIONNAME)

   END