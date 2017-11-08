USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_GETSCHOOLDROPREASONALL]    Script Date: 11/8/2017 9:53:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_GETSCHOOLDROPREASONALL]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN


      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_MST_GETSCHOOLDROPREASONALL
      *   Author            : Nikitha               
      *   Created Date      : 16-Apr-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   This SP is used to fetch values of table
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. mst_schooldropreason
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   
      *   1. schooldropreason
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT MST_SCHOOLDROPREASON.SCH_DRP_REASONID, MST_SCHOOLDROPREASON.SCH_DRP_REASON, MST_SCHOOLDROPREASON.DESCRIPTION, MST_SCHOOLDROPREASON.ISDELETED
      FROM dbo.MST_SCHOOLDROPREASON
      ORDER BY lower(MST_SCHOOLDROPREASON.SCH_DRP_REASON)

   END