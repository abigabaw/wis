USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_GETALLLITERACYSTATUS]    Script Date: 11/8/2017 10:01:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_GETALLLITERACYSTATUS]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN



      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_MST_GETALLLITERACYSTATUS
      *   Author            : Rekha.M               
      *   Created Date      : 17-04-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   This SP is used to Fetch all records
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   MST_LITERACYSTATUS
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   
      *   Master->Education->Literacy Status
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT MST_LITERACYSTATUS.LTR_STATUSID, MST_LITERACYSTATUS.LTR_STATUS, MST_LITERACYSTATUS.DESCRIPTION, MST_LITERACYSTATUS.ISDELETED
      FROM dbo.MST_LITERACYSTATUS
      ORDER BY lower(MST_LITERACYSTATUS.LTR_STATUS)

   END