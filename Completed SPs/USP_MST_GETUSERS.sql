USE [UETCLWIS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [DBO].[USP_MST_GETUSERS]  
  

   @ROLEID_ FLOAT(53),
   @USERNAME_ VARCHAR(MAX)
  
   
AS 
   BEGIN


      
      /*
      *   ********************************************************************************************************                  
      *   PROCEDURE NAME    : USP_MST_GETUSERS
      *   AUTHOR            : WILSON ABIGABA            
      *   CREATED DATE      : 29-JUNE-2017                  
      *   COPYRIGHT©        : COPYRIGHT © UETCL
      *   __________________________________________________________________________________________________________
      *   PURPOSE: FOR UPDATING MST_USER MASTER RECORDS BASED ON ROLEID
      *   __________________________________________________________________________________________________________
      *   INPUT PARAMETERS  1.USERNAME_ 
      *                     2.ROLEID_
      *   __________________________________________________________________________________________________________
      *   TABLES USED : 1. MST_USER
      *   __________________________________________________________________________________________________________
      *   MODULES AFFECTED : 1. MASTER
      *   __________________________________________________________________________________________________________
      *   *********************************************************************************************************
      */
      SELECT MST_USER.USERID, MST_USER.DISPLAYNAME AS USERNAME
      
      /*
      *   ,CELLNUMBER
      *   ISDELETED
      *   ,EMAILID
      *   ,MU.ROLEID
      *   ,MR.ROLENAME
      *   ,MR.ROLENAME
      *   ,MR.ISDELETED
      *    , MU.DISPLAYNAME
      */
      FROM DBO.MST_USER
      WHERE /*INNER JOIN MST_ROLE MR          ON MU.ROLEID = MR.ROLEID  (LOWER(USERNAME) LIKE LOWER(USERNAME_) OR USERNAME_ IS NULL)  AND (MU.ROLEID = ROLEID_ OR ROLEID_ = 0)*/UPPER(MST_USER.ISDELETED) = 'FALSE'
      ORDER BY LOWER(MST_USER.DISPLAYNAME)

   END