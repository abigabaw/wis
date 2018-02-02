USE [UETCLWIS]
GO
/****** Object:  StoredProcedure [dbo].[USP_MST_GET_REPRESENTATION]    Script Date: 11/8/2017 10:34:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[USP_MST_GET_REPRESENTATION]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

  
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

   

      SELECT MST_REPRESENTATION.REPRESENTATIONID, MST_REPRESENTATION.REPRESENTATIONNAME, MST_REPRESENTATION.ISDELETED
      FROM dbo.MST_REPRESENTATION
      ORDER BY lower(MST_REPRESENTATION.REPRESENTATIONNAME)

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END