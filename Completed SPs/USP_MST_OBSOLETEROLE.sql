USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_OBSOLETEROLE]    Script Date: 10/24/2017 7:51:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_OBSOLETEROLE]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @RoleId_ float(53),
   @isdeleted_ varchar(max),
   @errorMessage_ varchar(max)  OUTPUT
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      SET @errorMessage_ = NULL

      DECLARE
         /*
         *   SSMA warning messages:
         *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
         */

         @row_count float(53)

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_MST_OBSOLETEROLE
      *   Author            : Wilson Abigaba              
      *   Created Date      : 24/10/2017                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   For Obsolete MST_ROLE Master Records based on RoleId
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   1.RoleId_
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
      SELECT @row_count = count_big(MST_USER.ROLEID)
      FROM dbo.MST_USER
      WHERE MST_USER.ROLEID = @RoleId_

      IF @row_count = 0
         BEGIN

            UPDATE dbo.MST_ROLE
               SET 
                  ISDELETED = @isdeleted_
            WHERE MST_ROLE.ROLEID = @RoleId_

            IF @@TRANCOUNT > 0
               COMMIT WORK 

         END
      ELSE 
         SET @errormessage_ = 'Selected Role is already in use. Cannot Obsolete'

   END