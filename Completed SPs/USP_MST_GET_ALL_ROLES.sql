USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_GET_ALL_ROLES]    Script Date: 10/24/2017 7:21:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_GET_ALL_ROLES]  
   @RoleNameIN varchar(max)
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_MST_GETROLES
      *   Author            : Wilson Abigaba             
      *   Created Date      : 24/10/2017                 
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   For Fetching MST_ROLE Table Data
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   1.RoleNameIN
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. MST_ROLE
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   1. MASTER
      *   __________________________________________________________________________________________________________
      *   
      *   *********************************************************************************************************
      */
      IF @RoleNameIN IS NULL
         SELECT MST_ROLE.ROLEID, MST_ROLE.ROLENAME, MST_ROLE.ROLEDESCRIPTION, MST_ROLE.ISDELETED
         FROM dbo.MST_ROLE
         /* WHERE lower(isdeleted) ='false'*/
         ORDER BY lower(MST_ROLE.ROLENAME)
      ELSE 
         SELECT MST_ROLE.ROLEID, MST_ROLE.ROLENAME, MST_ROLE.ROLEDESCRIPTION, MST_ROLE.ISDELETED
         FROM dbo.MST_ROLE
         WHERE (lower(MST_ROLE.ROLENAME) LIKE lower((@RoleNameIN + '%')))
         /*or (isdeleted) = 'False'*/
         ORDER BY lower(MST_ROLE.ROLENAME)
      /*commit;*/

   END