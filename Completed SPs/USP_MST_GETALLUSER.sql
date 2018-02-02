USE [UETCLWIS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [DBO].[USP_MST_GETALLUSER]  

   @ROLEID_ FLOAT(53),
   @USERNAME_ VARCHAR(MAX)

AS 
   BEGIN

 

      
      /*
      *   ********************************************************************************************************                  
      *   PROCEDURE NAME    : USP_MST_GETALLUSER
      *   AUTHOR            : WILSON ABIGABA               
      *   CREATED DATE      : 29-JUNE-2017                 
      *   COPYRIGHT©        : COPYRIGHT © UETCL
      *   __________________________________________________________________________________________________________
      *   PURPOSE
      *   
      *   THIS SP IS USED TO FEATCH ALL USER RECORDS
      *   __________________________________________________________________________________________________________
      *   INPUT PARAMETERS 
      *   -
      *   __________________________________________________________________________________________________________
      *   TABLES USED
      *   
      *   1. MST_USER
      *   __________________________________________________________________________________________________________
      *   MODULES AFFECTED
      *   
      *   1. MASTER - USER - USER CREATION
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      *   
      *      OPEN SP_RECORDSET FOR  
      *              SELECT USERID,USERNAME  FROM MST_USER 
      *              WHERE ISDELETED='FALSE' 
      *              ORDER BY USERID;
      *
      */
      SELECT 
         MU.USERID, 
         MU.USERNAME, 
         MU.CELLNUMBER, 
         MU.ISDELETED, 
         MU.EMAILID, 
         MU.ROLEID, 
         MR.ROLENAME, 
         MR.ROLENAME AS ROLENAME$2, 
         MR.ISDELETED AS ISDELETED$2, 
         MU.DISPLAYNAME
      FROM 
         DBO.MST_USER  AS MU 
            INNER JOIN DBO.MST_ROLE  AS MR 
            ON MU.ROLEID = MR.ROLEID
      WHERE (LOWER(MU.USERNAME) LIKE LOWER(@USERNAME_) OR @USERNAME_ IS NULL) AND (MU.ROLEID = @ROLEID_ OR @ROLEID_ = 0)
      ORDER BY LOWER(MU.USERNAME)

   END