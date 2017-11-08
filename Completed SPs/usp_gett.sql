USE [UETCLWIS]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_LOAD_MNEGOALELEMENTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_LAND_INKIND_PENDING'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERTNVRATTDSCHL'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERTINNOTATTENTEDSCHOOL'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERT_OPTIONGRP'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_TRN_MNE_EVAL'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_TRN_CONCERN'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MST_CULTUREPROPERTY'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MST_COMMENTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MNEEVALELEMENTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_CLARIFY_REQUEST'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_APPROVALTEMPAUTHORISER'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_IN_KIND_DELIVERED'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_HOUSE_IN_PENDING'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GETNASCOOLDETAILS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GETCOMMENTS_DATABY_HHID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_WIS_CONFIGURATION'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_VILLAGEID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_PAP_LND_VALUATION'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_PAP_HOUSEHOLD'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_MNE_EVALBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_MNE_EVAL'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_PHASESBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_PHASES'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_BUDGBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_BUDG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_SMSDETAILSFORDUE'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_RESPONSE_REQUESTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PHASEACTIVITY'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PARAMETERS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAPSBYVILLAGE'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAP_PROJECT_USERS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAP_PAYT_STATUS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTIONGRPDETAILS_BYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTIONAVAIL'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTION_PARAM_BYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPT_PRM_MAPPING_BYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPT_HOUSEHOLD'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LND_TYPE'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LIVBUDCATGBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LIVBUDCATG_ITEM'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_SUBCATEGBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_SUBCATEG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_CATEGBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_CATEG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_ACTIVITY'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MNEEVALELEMENTSBYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MNEEVALELEMENTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_EMAILDETAILSFORDUE'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_COUNTYNAME'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_STATUS_PEND'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_REQUESTS'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_REQUEST'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_PENDING'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAPPLANDISPLAY'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAP_PHASEID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAP_PHASE_ACTIVITYID'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_BUILD_CONFIG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_OPTIONGRP'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_OPT_PRM_MAPPING'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_LIVCATG_ITEM'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_LIVBUDCATG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_CDAP_SUBCATEG'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_SSMA_SOURCE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_CDAP_CATEG'
GO
/****** Object:  StoredProcedure [dbo].[USP_LOAD_MNEGOALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_LOAD_MNEGOALELEMENTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_LAND_INKIND_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_LAND_INKIND_PENDING]
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERTNVRATTDSCHL]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INSERTNVRATTDSCHL]
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERTINNOTATTENTEDSCHOOL]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INSERTINNOTATTENTEDSCHOOL]
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_OPTIONGRP]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INSERT_OPTIONGRP]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_TRN_MNE_EVAL]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_TRN_MNE_EVAL]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_TRN_CONCERN]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_TRN_CONCERN]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MST_CULTUREPROPERTY]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_MST_CULTUREPROPERTY]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MST_COMMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_MST_COMMENTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MNEEVALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_MNEEVALELEMENTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_CLARIFY_REQUEST]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_CLARIFY_REQUEST]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_APPROVALTEMPAUTHORISER]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_INS_APPROVALTEMPAUTHORISER]
GO
/****** Object:  StoredProcedure [dbo].[USP_IN_KIND_DELIVERED]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_IN_KIND_DELIVERED]
GO
/****** Object:  StoredProcedure [dbo].[USP_HOUSE_IN_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_HOUSE_IN_PENDING]
GO
/****** Object:  StoredProcedure [dbo].[USP_GETNASCOOLDETAILS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GETNASCOOLDETAILS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GETCOMMENTS_DATABY_HHID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GETCOMMENTS_DATABY_HHID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_WIS_CONFIGURATION]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_WIS_CONFIGURATION]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_VILLAGEID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_VILLAGEID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_PAP_LND_VALUATION]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_PAP_LND_VALUATION]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_PAP_HOUSEHOLD]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_PAP_HOUSEHOLD]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_MNE_EVALBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_MNE_EVALBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_MNE_EVAL]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_MNE_EVAL]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_PHASESBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_CDAP_PHASESBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_PHASES]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_CDAP_PHASES]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_BUDGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_CDAP_BUDGBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_BUDG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_TRN_CDAP_BUDG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_SMSDETAILSFORDUE]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_SMSDETAILSFORDUE]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_RESPONSE_REQUESTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_RESPONSE_REQUESTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PHASEACTIVITY]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_PHASEACTIVITY]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PARAMETERS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_PARAMETERS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PAPSBYVILLAGE]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_PAPSBYVILLAGE]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PAP_PROJECT_USERS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_PAP_PROJECT_USERS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PAP_PAYT_STATUS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_PAP_PAYT_STATUS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTIONGRPDETAILS_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_OPTIONGRPDETAILS_BYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTIONAVAIL]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_OPTIONAVAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTION_PARAM_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_OPTION_PARAM_BYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPT_PRM_MAPPING_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_OPT_PRM_MAPPING_BYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPT_HOUSEHOLD]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_OPT_HOUSEHOLD]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LND_TYPE]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_LND_TYPE]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LIVBUDCATGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_LIVBUDCATGBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LIVBUDCATG_ITEM]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_LIVBUDCATG_ITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_SUBCATEGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_CDAP_SUBCATEGBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_SUBCATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_CDAP_SUBCATEG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_CATEGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_CDAP_CATEGBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_CATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_CDAP_CATEG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_ACTIVITY]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MST_CDAP_ACTIVITY]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MNEEVALELEMENTSBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MNEEVALELEMENTSBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MNEEVALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_MNEEVALELEMENTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_EMAILDETAILSFORDUE]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_EMAILDETAILSFORDUE]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_COUNTYNAME]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_COUNTYNAME]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_STATUS_PEND]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CLARIFY_STATUS_PEND]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_REQUESTS]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CLARIFY_REQUESTS]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_REQUEST]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CLARIFY_REQUEST]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CLARIFY_PENDING]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAPPLANDISPLAY]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CDAPPLANDISPLAY]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAP_PHASEID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CDAP_PHASEID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAP_PHASE_ACTIVITYID]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_CDAP_PHASE_ACTIVITYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_BUILD_CONFIG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_BUILD_CONFIG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_OPTIONGRP]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_OPTIONGRP]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_OPT_PRM_MAPPING]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_OPT_PRM_MAPPING]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_LIVCATG_ITEM]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_MST_LIVCATG_ITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_LIVBUDCATG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_MST_LIVBUDCATG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_CDAP_SUBCATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_MST_CDAP_SUBCATEG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_CDAP_CATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
DROP PROCEDURE [dbo].[USP_GET_ALL_MST_CDAP_CATEG]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_CDAP_CATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_MST_CDAP_CATEG]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_CATEG.CDAP_CATEGORYID, MST_CDAP_CATEG.CDAP_CATEGORYNAME, MST_CDAP_CATEG.ISDELETED
      FROM dbo.MST_CDAP_CATEG
      ORDER BY upper(MST_CDAP_CATEG.CDAP_CATEGORYNAME)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_CDAP_SUBCATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_MST_CDAP_SUBCATEG]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_CATEGORYID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYID, MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYNAME, MST_CDAP_SUBCATEG.ISDELETED
      FROM dbo.MST_CDAP_SUBCATEG
      WHERE MST_CDAP_SUBCATEG.CDAP_CATEGORYID = @CDAP_CATEGORYID_
      ORDER BY upper(MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYNAME)

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_LIVBUDCATG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_MST_LIVBUDCATG]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_LIV_BDG_CATEG.LIV_BUD_CATEGID, MST_LIV_BDG_CATEG.LIV_BUD_CATEGORYNAME, MST_LIV_BDG_CATEG.ISDELETED
      FROM dbo.MST_LIV_BDG_CATEG
      ORDER BY upper(MST_LIV_BDG_CATEG.LIV_BUD_CATEGORYNAME)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_MST_LIVCATG_ITEM]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_MST_LIVCATG_ITEM]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @LIV_BUD_CATEGID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_LIV_BDG_ITEM.LIV_BUD_ITEMID, MST_LIV_BDG_ITEM.LIV_BUD_ITEMNAME, MST_LIV_BDG_ITEM.LIV_BUD_ITEMDESC, MST_LIV_BDG_ITEM.ISDELETED
      FROM dbo.MST_LIV_BDG_ITEM
      WHERE MST_LIV_BDG_ITEM.LIV_BUD_CATEGID = @LIV_BUD_CATEGID_
      ORDER BY upper(MST_LIV_BDG_ITEM.LIV_BUD_ITEMNAME)

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_OPT_PRM_MAPPING]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_OPT_PRM_MAPPING]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         MOP.OPTPARID, 
         MOG.OPTIONGROUP, 
         MOA.OPTIONAVAILABLE, 
         MPD.DESCRIPTION, 
         MP.PARAMETERNAME, 
         MOP.ISDELETED
      FROM 
         dbo.MST_OPT_PARAMETERS  AS MOP 
            INNER JOIN dbo.MST_OPTION_GROUPS  AS MOG 
            ON MOG.OPTIONGROUPID = MOP.OPTIONGROUPID 
            INNER JOIN dbo.MST_OPTIONAVAILABLE  AS MOA 
            ON MOA.ID = MOP.OPTIONAVAILABLEID 
            INNER JOIN dbo.MST_PARAMETERDES  AS MPD 
            ON MPD.DESCRIPTIONID = MOP.DESCRIPTIONID 
            INNER JOIN dbo.MST_PARAMETER  AS MP 
            ON MP.PARAMETERID = MPD.PARAMETERID
      ORDER BY 
         MOG.OPTIONGROUP, 
         MOA.OPTIONAVAILABLE, 
         MP.PARAMETERNAME, 
         MPD.DESCRIPTION
      /*INNER JOIN MST_OPT*/

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_OPTIONGRP]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_ALL_OPTIONGRP]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         OPG.PARAMETERID, 
         OG.OPTIONGROUP, 
         MSP.PAPDESIGNATION, 
         OPG.ISRESIDENT, 
         OPG.LANDCOMPENSATION, 
         OPG.HOUSECOMPENSATION
      FROM 
         dbo.MST_OPTIONGROUPPARAMETER  AS OPG 
            INNER JOIN dbo.MST_OPTION_GROUPS  AS OG 
            ON OG.OPTIONGROUPID = OPG.OPTIONGROUPID 
            INNER JOIN dbo.MST_PAPDESIGNATION  AS MSP 
            ON MSP.PAPDESIGNATIONID = OPG.LANDSTATUSID
      WHERE lower(OPG.ISDELETED) = 'false'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_BUILD_CONFIG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_BUILD_CONFIG]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT DISTINCT 
         (
            SELECT WISCONFIGURATION.CONFIGDATA AS BUILDVERSION
            FROM dbo.WISCONFIGURATION
            WHERE WISCONFIGURATION.CONFIGITEM = 'BUILDVERSION'
         ) AS BUILDVERSION, 
         (
            SELECT WISCONFIGURATION.CONFIGDATA AS BUILDDATE
            FROM dbo.WISCONFIGURATION
            WHERE WISCONFIGURATION.CONFIGITEM = 'BUILDDATE'
         ) AS BUILDDATE, 
         (
            SELECT WISCONFIGURATION.CONFIGDATA AS BUILDCOPY
            FROM dbo.WISCONFIGURATION
            WHERE WISCONFIGURATION.CONFIGITEM = 'BUILDCOPY'
         ) AS BUILDCOPY
      FROM dbo.WISCONFIGURATION

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAP_PHASE_ACTIVITYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CDAP_PHASE_ACTIVITYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   select CDAP_PHASEACTIVITYID from TRN_CDAP_PHASE_ACTIVITY where isdeleted = 'False';
      *   select max(CDAP_PHASEACTIVITYID) from TRN_CDAP_PHASE_ACTIVITY where isdeleted = 'False';
      */
      SELECT o.CDAP_PHASEACTIVITYID
      FROM dbo.TRN_CDAP_PHASE_ACTIVITY  AS o
      WHERE NOT EXISTS 
         (
            SELECT DISTINCT r.CDAP_PHASEACTIVITYID
            FROM dbo.TRN_CDAP_ACTIVITY_PAPS  AS r
            WHERE o.CDAP_PHASEACTIVITYID = r.CDAP_PHASEACTIVITYID
         )
      ORDER BY o.CDAP_PHASEACTIVITYID

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAP_PHASEID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CDAP_PHASEID]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      /*select CDAP_PHASEID from TRN_CDAP_PHASE where isdeleted = 'False';*/
      SELECT max(TRN_CDAP_PHASE.CDAP_PHASEID) AS CDAP_PHASEID
      FROM dbo.TRN_CDAP_PHASE
      WHERE TRN_CDAP_PHASE.ISDELETED = 'False'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CDAPPLANDISPLAY]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CDAPPLANDISPLAY]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_PHASEACTIVITYID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *    SELECT c.cdap_phaseid,c.cdap_phaseno,c.cdap_periodfrom,c.cdap_periodto,a.cdap_activityid,a.district,a.county,a.subcounty,a.villages,a.activitydetails,
      *   a.modeofimplementation,a.challenges,a.activitydatefrom,a.activitydateto,d.hhid from TRN_CDAP_PHASE_ACTIVITY a
      *   --inner join mst_cdap_activity b on a.CDAP_ACTIVITYID = b.CDAP_ACTIVITYID
      *   inner join trn_cdap_phase c on a.cdap_phaseid = c.cdap_phaseid
      *   inner join trn_cdap_activity_paps d on a.cdap_phaseactivityid = d.cdap_phaseactivityid
      *   where a.cdap_phaseactivityid = CDAP_PHASEACTIVITYID_;
      */
      SELECT 
         a.CDAP_ACTIVITYID, 
         a.DISTRICT, 
         a.COUNTY, 
         a.SUBCOUNTY, 
         a.VILLAGES, 
         a.ACTIVITYDETAILS, 
         a.MODEOFIMPLEMENTATION, 
         a.CHALLENGES, 
         a.COMMENTS, 
         a.ACTIVITYDATEFROM, 
         a.ACTIVITYDATETO, 
         d.HHID
      FROM 
         dbo.TRN_CDAP_PHASE_ACTIVITY  AS a 
            LEFT JOIN dbo.TRN_CDAP_ACTIVITY_PAPS  AS d 
            ON a.CDAP_PHASEACTIVITYID = d.CDAP_PHASEACTIVITYID
      WHERE a.CDAP_PHASEACTIVITYID = @CDAP_PHASEACTIVITYID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLARIFY_PENDING]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   TRACKHDR_ IN TRN_APPROVAL_CLARIFY.TRACKERHEADERID%TYPE,
      *   REQUEST_ IN TRN_APPROVAL_CLARIFY.CLARIFYREQUEST%TYPE,
      *    STATUS_ IN TRN_APPROVAL_CLARIFY.CLARIFYSTATUS%TYPE,
      *    ISDELETED_ IN TRN_APPROVAL_CLARIFY.ISDELETED%TYPE,
      *   CREATEDBY_ IN TRN_APPROVAL_CLARIFY.CREATEDBY%TYPE,
      *   UPDATEDBY_ IN TRN_APPROVAL_CLARIFY.UPDATEDBY%TYPE,
      *   ERRORMSG_ OUT VARCHAR2
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_SEL_CLARIFY_PENDING
      *   Author            : Edwin Baguma               
      *   Created Date      : 25-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      SELECT count_big(TRN_APPROVAL_CLARIFY.ID) AS EXISTING_REQUESTS
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            INNER JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID
      WHERE 
         TRN_APPROVAL_CLARIFY.TRACKERHEADERID IN 
         (
            SELECT TRN_APPROVALTRACKERHEADER.TRACKERHEADERID
            FROM dbo.TRN_APPROVALTRACKERHEADER
            WHERE TRN_APPROVALTRACKERHEADER.HHID = @HHID_ AND TRN_APPROVALTRACKERHEADER.PAGECODE = 'CRFND'
         ) AND 
         TRN_APPROVAL_CLARIFY.CLARIFYSTATUS = 'Pending' AND 
         upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_REQUEST]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLARIFY_REQUEST]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *    ERROMSG_ OUT VARCHAR2
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_GET_CLARIFY_REQUEST
      *   Author            : Edwin Baguma               
      *   Created Date      : 16-AUG-2016                 
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      SELECT 
         TRN_APPROVAL_CLARIFY.ID, 
         TRN_APPROVAL_CLARIFY.CLARIFYREQUEST, 
         TRN_APPROVAL_CLARIFY.CLARIFYRESPONSE, 
         TRN_APPROVAL_CLARIFY.CLARIFYSTATUS, 
         TRN_APPROVAL_CLARIFY.CREATEDBY, 
         TRN_APPROVAL_CLARIFY.CREATEDDATE, 
         TRN_APPROVAL_CLARIFY.UPDATEDBY, 
         isnull(TRN_APPROVAL_CLARIFY.UPDATEDDATE, NULL) AS UPDATEDDATE, 
         TRN_APPROVAL_CLARIFY.ISDELETED, 
         TRN_APPROVALTRACKERHEADER.TRACKERHEADERID, 
         TRN_APPROVALTRACKERHEADER.HHID, 
         TRN_PAP_HOUSEHOLD.PAPNAME, 
         REQ.DISPLAYNAME AS REQUESTER, 
         RES.DISPLAYNAME AS RESPONDENT
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            INNER JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD 
            ON TRN_PAP_HOUSEHOLD.HHID = TRN_APPROVALTRACKERHEADER.HHID 
            INNER JOIN dbo.MST_USER  AS REQ 
            ON TRN_APPROVAL_CLARIFY.CREATEDBY = REQ.USERID 
            INNER JOIN dbo.MST_USER  AS RES 
            ON TRN_APPROVAL_CLARIFY.UPDATEDBY = RES.USERID
      WHERE TRN_APPROVAL_CLARIFY.ID = @ID_ AND upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE'

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_REQUESTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLARIFY_REQUESTS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @USERID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *    errorMessage_ OUT VARCHAR2
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_GET_CLARIFY_REQUEST
      *   Author            : Edwin Baguma               
      *   Created Date      : 16-AUG-2016                 
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      SELECT DISTINCT 
         TRN_APPROVAL_CLARIFY.ID, 
         TRN_APPROVALTRACKERHEADER.TRACKERHEADERID, 
         TRN_APPROVALTRACKERHEADER.HHID, 
         TRN_PAP_HOUSEHOLD.PAPNAME, 
         REQ.DISPLAYNAME AS REQUESTER, 
         TRN_APPROVAL_CLARIFY.CREATEDDATE, 
         TRN_APPROVAL_CLARIFY.CLARIFYREQUEST, 
         RES.DISPLAYNAME AS RESPONDENT, 
         TRN_APPROVAL_CLARIFY.UPDATEDDATE, 
         TRN_APPROVAL_CLARIFY.CLARIFYRESPONSE, 
         TRN_APPROVAL_CLARIFY.CLARIFYSTATUS, 
         TRN_APPROVAL_CLARIFY.CREATEDBY, 
         TRN_APPROVAL_CLARIFY.UPDATEDBY, 
         TRN_APPROVAL_CLARIFY.ISDELETED
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            LEFT JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD 
            ON TRN_PAP_HOUSEHOLD.HHID = TRN_APPROVALTRACKERHEADER.HHID 
            LEFT JOIN dbo.MST_USER  AS REQ 
            ON TRN_APPROVAL_CLARIFY.CREATEDBY = REQ.USERID 
            LEFT JOIN dbo.MST_USER  AS RES 
            ON TRN_APPROVAL_CLARIFY.UPDATEDBY = RES.USERID
      WHERE upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE' AND /* (TRN_APPROVAL_CLARIFY.UPDATEDBY = USERID_ OR TRN_APPROVAL_CLARIFY.CREATEDBY = USERID_)*/TRN_APPROVALTRACKERHEADER.HHID = @HHID_
      ORDER BY TRN_APPROVAL_CLARIFY.CREATEDDATE DESC

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_CLARIFY_STATUS_PEND]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_CLARIFY_STATUS_PEND]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @USERID_ float(53)
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */


AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *    errorMessage_ OUT VARCHAR2
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_GET_CLARIFY_STATUS_PEND
      *   Author            : Edwin Baguma               
      *   Created Date      : 16-AUG-2016                 
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      SELECT count_big(1) AS COUNTS
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            LEFT JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD 
            ON TRN_PAP_HOUSEHOLD.HHID = TRN_APPROVALTRACKERHEADER.HHID 
            LEFT JOIN dbo.MST_USER  AS REQ 
            ON TRN_APPROVAL_CLARIFY.CREATEDBY = REQ.USERID 
            LEFT JOIN dbo.MST_USER  AS RES 
            ON TRN_APPROVAL_CLARIFY.UPDATEDBY = RES.USERID
      WHERE 
         upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE' AND 
         (TRN_APPROVAL_CLARIFY.UPDATEDBY = @USERID_) AND 
         upper(TRN_APPROVAL_CLARIFY.CLARIFYSTATUS) = 'PENDING'

      /* AND TRN_APPROVALTRACKERHEADER.HHID = HHID_;*/
      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_COUNTYNAME]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_COUNTYNAME]  
   @County_ varchar(max)
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_DISTRICT.DISTRICTNAME, MST_DISTRICT.DISTRICTID
      FROM dbo.MST_DISTRICT
      WHERE lower(MST_DISTRICT.DISTRICTNAME) LIKE (lower(@County_) + '%')
      ORDER BY MST_DISTRICT.DISTRICTNAME

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_EMAILDETAILSFORDUE]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_EMAILDETAILSFORDUE]  
   @EMAILTEMPLATECODE_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_EMAILDETAILSFORDUE
      *   Author            : Anjankumar               
      *   Created Date      : 26-jun-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   This SP is used to Get the Email details to send mails   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   -                                                                                                                         
      *   *********************************************************************************************************
      */
      SELECT MST_EMAIL_TEMPLATE.EMAILSUBJECT, MST_EMAIL_TEMPLATE.EMAILBODY
      FROM dbo.MST_EMAIL_TEMPLATE
      WHERE MST_EMAIL_TEMPLATE.EMAILTEMPLATECODE = @EMAILTEMPLATECODE_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MNEEVALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MNEEVALELEMENTS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @EVALUATIONID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_MNEEVALELEMENTS
      *   Author            : Victor Maity               
      *   Created Date      : 26-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   Input Parameters 
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. MST_MNE_GOAL_ELEMENTS
      *   2. TRN_MNE_EVALELEMENTS
      *   
      *   Input Parameters 
      *       EVALUATIONID_
      *   _________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT eval.EVALUATIONID, eval.EVALELEMENTID, goal.GOAL_ELEMENTNAME, eval.EVALELEMENTDESCRIPTIONN
      FROM 
         dbo.TRN_MNE_EVALELEMENTS  AS eval 
            LEFT JOIN dbo.MST_MNE_GOAL_ELEMENTS  AS goal 
            ON eval.GOAL_ELEMENTID = goal.GOAL_ELEMENTID
      WHERE eval.EVALUATIONID = @EVALUATIONID_
      ORDER BY lower(goal.GOAL_ELEMENTNAME)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MNEEVALELEMENTSBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MNEEVALELEMENTSBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @EVALELEMENTID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_MNEEVALELEMENTSBYID
      *   Author            : Victor Maity               
      *   Created Date      : 26-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   Input Parameters 
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. TRN_MNE_EVALELEMENTS
      *   
      *   Input Parameters 
      *       EVALELEMENTID_
      *   _________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT TRN_MNE_EVALELEMENTS.GOAL_ELEMENTID, TRN_MNE_EVALELEMENTS.EVALELEMENTDESCRIPTIONN, TRN_MNE_EVALELEMENTS.EVALUATIONID, TRN_MNE_EVALELEMENTS.EVALELEMENTID
      FROM dbo.TRN_MNE_EVALELEMENTS
      WHERE TRN_MNE_EVALELEMENTS.EVALELEMENTID = @EVALELEMENTID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_ACTIVITY]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_CDAP_ACTIVITY]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_ACTIVITY.CDAP_ACTIVITYID AS Id, MST_CDAP_ACTIVITY.CDAP_ACTIVITYNAME AS NAME
      FROM dbo.MST_CDAP_ACTIVITY
      WHERE MST_CDAP_ACTIVITY.ISDELETED = 'False'
      ORDER BY MST_CDAP_ACTIVITY.CDAP_ACTIVITYNAME

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_CATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_CDAP_CATEG]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_CATEG.CDAP_CATEGORYID AS Id, MST_CDAP_CATEG.CDAP_CATEGORYNAME AS NAME
      FROM dbo.MST_CDAP_CATEG
      WHERE MST_CDAP_CATEG.ISDELETED = 'False'
      ORDER BY MST_CDAP_CATEG.CDAP_CATEGORYID

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_CATEGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_CDAP_CATEGBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_CATEGORYID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_CATEG.CDAP_CATEGORYID, MST_CDAP_CATEG.CDAP_CATEGORYNAME, MST_CDAP_CATEG.ISDELETED
      FROM dbo.MST_CDAP_CATEG
      WHERE MST_CDAP_CATEG.CDAP_CATEGORYID = @CDAP_CATEGORYID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_SUBCATEG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_CDAP_SUBCATEG]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_CATEGORYID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYID AS Id, MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYNAME AS NAME
      FROM dbo.MST_CDAP_SUBCATEG
      WHERE MST_CDAP_SUBCATEG.ISDELETED = 'False' AND MST_CDAP_SUBCATEG.CDAP_CATEGORYID = @CDAP_CATEGORYID_
      ORDER BY MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYID

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_CDAP_SUBCATEGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_CDAP_SUBCATEGBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_SUBCATEGORYID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYID, MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYNAME, MST_CDAP_SUBCATEG.ISDELETED
      FROM dbo.MST_CDAP_SUBCATEG
      WHERE MST_CDAP_SUBCATEG.CDAP_SUBCATEGORYID = @CDAP_SUBCATEGORYID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LIVBUDCATG_ITEM]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_LIVBUDCATG_ITEM]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @LIV_BUD_ITEMID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_LIV_BDG_ITEM.LIV_BUD_ITEMID, MST_LIV_BDG_ITEM.LIV_BUD_ITEMNAME, MST_LIV_BDG_ITEM.LIV_BUD_ITEMDESC, MST_LIV_BDG_ITEM.ISDELETED
      FROM dbo.MST_LIV_BDG_ITEM
      WHERE MST_LIV_BDG_ITEM.LIV_BUD_ITEMID = @LIV_BUD_ITEMID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LIVBUDCATGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_LIVBUDCATGBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @LIV_BUD_CATEGID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_LIV_BDG_CATEG.LIV_BUD_CATEGID, MST_LIV_BDG_CATEG.LIV_BUD_CATEGORYNAME, MST_LIV_BDG_CATEG.ISDELETED
      FROM dbo.MST_LIV_BDG_CATEG
      WHERE MST_LIV_BDG_CATEG.LIV_BUD_CATEGID = @LIV_BUD_CATEGID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_MST_LND_TYPE]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_MST_LND_TYPE]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT MST_LND_TYPE.LND_TYPEID AS ID, MST_LND_TYPE.LANDTYPE AS NAME
      FROM dbo.MST_LND_TYPE
      WHERE MST_LND_TYPE.ISDELETED = 'False'
      ORDER BY MST_LND_TYPE.LANDTYPE

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPT_HOUSEHOLD]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_OPT_HOUSEHOLD]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
   @LNDCOMP_ varchar(max),
   @HOUSECOMP_ varchar(max),
   @LNDSTATUS_ varchar(max),
   @RESIDENT_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      DECLARE
         @LANDSUM numeric(10, 2), 
         @LANDAVL_ varchar(50)

      SELECT @LANDSUM = CAST(isnull(TRN_PAP_HOUSEHOLD.RIGHTOFWAY, 0) AS float(53)) + CAST(isnull(TRN_PAP_HOUSEHOLD.RIGHTOFWAY, 0) AS float(53))
      FROM dbo.TRN_PAP_HOUSEHOLD
      WHERE TRN_PAP_HOUSEHOLD.HHID = @HHID_

      IF @LANDSUM = 0
         SET @LANDAVL_ = 'No land'
      ELSE 
         IF @LANDSUM < 0.25
            SET @LANDAVL_ = '< 0.25'
         ELSE 
            IF @LANDSUM < 0.5
               SET @LANDAVL_ = '>0.25 and <=0.5'
            ELSE 
               IF @LANDSUM < 0.75
                  SET @LANDAVL_ = '>0.5 and<=0.75'
               ELSE 
                  SET @LANDAVL_ = '>0.75 and<=1'

      /* 
      *   SSMA error messages:
      *   O2SS0004: Unparsed SQL (Parse error at line 29, column 3.  Encountered: WITH) [OPEN SP_RECORDSET FOR  
      *     WITH CTE AS (SELECT   
      *            MOG.OPTIONGROUP,
      *            MOP.OPTIONGROUPID, Count(1) as Total
      *     FROM MST_OPT_PARAMETERS MOP
      *     INNER JOIN MST_OPTION_GROUPS MOG ON MOG.OPTIONGROUPID = MOP.OPTIONGROUPID
      *     INNER JOIN MST_OPTIONAVAILABLE MOA ON MOA.ID = MOP.OPTIONAVAILABLEID
      *     INNER JOIN MST_PARAMETERDES MPD ON MPD.DESCRIPTIONID = MOP.DESCRIPTIONID
      *     INNER JOIN MST_PARAMETER MP ON MP.PARAMETERID = MPD.PARAMETERID
      *     Where (((Description = LNDCOMP_ and MOA.OPTIONAVAILABLE='House Compensation') or Description like LANDAVL_ or 
      *     lower(Description) like concat('%',concat(lower(LNDSTATUS_),'%'))  or 
      *     (Description like HOUSECOMP_ and  MOA.OPTIONAVAILABLE='Land Compensation'))
      *     and MOP.OPTIONGROUPID in (select A.OPTIONGROUPID From MST_OPT_PARAMETERS A
      *     INNER JOIN MST_PARAMETERDES B ON A.DESCRIPTIONID = B.DESCRIPTIONID
      *     where lower(B.Description) = lower(RESIDENT_)))
      *     group by MOG.OPTIONGROUP,
      *            MOP.OPTIONGROUPID order by Total desc)         
      *            SELECT OPTIONGROUP,OPTIONGROUPID FROM CTE WHERE ROWNUM=1;] cannot be converted.

      /-*INNER JOIN MST_OPT*-/      */



   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPT_PRM_MAPPING_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_OPT_PRM_MAPPING_BYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @OptParID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         MOP.OPTPARID, 
         MOG.OPTIONGROUPID, 
         MOA.ID, 
         MPD.DESCRIPTIONID, 
         MP.PARAMETERID, 
         MOG.OPTIONGROUP, 
         MOA.OPTIONAVAILABLE, 
         MPD.DESCRIPTION, 
         MP.PARAMETERNAME
      FROM 
         dbo.MST_OPT_PARAMETERS  AS MOP 
            INNER JOIN dbo.MST_OPTION_GROUPS  AS MOG 
            ON MOG.OPTIONGROUPID = MOP.OPTIONGROUPID 
            INNER JOIN dbo.MST_OPTIONAVAILABLE  AS MOA 
            ON MOA.ID = MOP.OPTIONAVAILABLEID 
            INNER JOIN dbo.MST_PARAMETERDES  AS MPD 
            ON MPD.DESCRIPTIONID = MOP.DESCRIPTIONID 
            INNER JOIN dbo.MST_PARAMETER  AS MP 
            ON MP.PARAMETERID = MPD.PARAMETERID
      WHERE MOP.OPTPARID = @OptParID_
      /*INNER JOIN MST_OPT*/

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTION_PARAM_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_OPTION_PARAM_BYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @DescriptionID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   *******************************************************************************************************                  
      *   Procedure Name    : USP_GET_OPTION_PARAM_BYID
      *   Author            : Sunil M V               
      *   Created Date      : 10-May-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   For Fetching MST_GET_TYPE Table Data
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   1.DescriptionID_
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. USP_GET_OPTION_PARAM_BYID
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   1. MASTER
      *   __________________________________________________________________________________________________________
      *   
      *   *********************************************************************************************************
      */
      SELECT MS.PARAMETERID, MS.OPTIONAVAILABLEID, MS.DESCRIPTION, MS.ISDELETED
      FROM dbo.MST_PARAMETERDES  AS MS
      WHERE MS.DESCRIPTIONID = @DescriptionID_/*and lower(MS.ISDELETED)='false'*/
      ORDER BY lower(MS.DESCRIPTION)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTIONAVAIL]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_OPTIONAVAIL]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   *******************************************************************************************************                  
      *   Procedure Name    : MST_SUBCOUNTY
      *   Author            : EShwar B V               
      *   Created Date      : 02-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   For Fetching MST_GET_TYPE Table Data
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   1.SUBCOUNTYNAME
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. MST_SUBCOUNTY
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   1. MASTER
      *   __________________________________________________________________________________________________________
      *   
      *   *********************************************************************************************************
      */
      SELECT MST_OPTIONAVAILABLE.ID, MST_OPTIONAVAILABLE.OPTIONAVAILABLE
      FROM dbo.MST_OPTIONAVAILABLE
      WHERE MST_OPTIONAVAILABLE.ISDELETED = 'False'
      ORDER BY lower(MST_OPTIONAVAILABLE.OPTIONAVAILABLE)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_OPTIONGRPDETAILS_BYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_OPTIONGRPDETAILS_BYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @paraID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         MST_OPTIONGROUPPARAMETER.PARAMETERID, 
         MST_OPTIONGROUPPARAMETER.OPTIONGROUPID, 
         MST_OPTIONGROUPPARAMETER.LANDSTATUSID, 
         MST_OPTIONGROUPPARAMETER.ISRESIDENT, 
         MST_OPTIONGROUPPARAMETER.LANDCOMPENSATION, 
         MST_OPTIONGROUPPARAMETER.HOUSECOMPENSATION
      FROM dbo.MST_OPTIONGROUPPARAMETER
      WHERE MST_OPTIONGROUPPARAMETER.PARAMETERID = @paraID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PAP_PAYT_STATUS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PAP_PAYT_STATUS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_PAP_PAYT_STATUS
      *   Author            : EDWIN BAGUMA          
      *   Created Date      : 20-JAN-2016                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *          
      *           PROJECTID_, HHID_
      *         
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. TRN_APPROVALTRACKERHEADER
      *   2. TRN_PAP_HOUSEHOLD
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT 
         TRN_APPROVALTRACKERHEADER.STATUSID, 
         TRN_APPROVALTRACKERHEADER.PAGECODE, 
         TRN_APPROVALTRACKERHEADER.HHID, 
         /* 1 APPROVED, 2 DECLINED, 3 PENDING*/TRN_APPROVALTRACKERHEADER.STATUSID AS PAYT_STATUS, 
         TRN_PAP_HOUSEHOLD.PROJECTID, 
         TRN_PAP_HOUSEHOLD.PAPNAME, 
         TRN_PAP_HOUSEHOLD.PAP_UID, 
         TRN_PAP_HOUSEHOLD.PLOTREFERENCE
      FROM 
         dbo.TRN_APPROVALTRACKERHEADER 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD 
            ON TRN_PAP_HOUSEHOLD.HHID = TRN_APPROVALTRACKERHEADER.HHID
      WHERE 
         TRN_APPROVALTRACKERHEADER.PAGECODE = 'CREND' AND 
         TRN_APPROVALTRACKERHEADER.HHID = @HHID_ AND 
         TRN_PAP_HOUSEHOLD.PROJECTID = @PROJECTID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PAP_PROJECT_USERS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PAP_PROJECT_USERS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_PAP_PROJECT_USERS
      *   Author            : EDWIN BAGUMA          
      *   Created Date      : 20-JAN-2016                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *          
      *           PROJECTID_
      *         
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. TRN_PROJECT_PERSONNEL
      *   2. MST_USER
      *   
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT TRN_PROJECT_PERSONNEL.USERID, MST_USER.USERNAME, MST_USER.DISPLAYNAME, MST_USER.EMAILID
      FROM 
         dbo.TRN_PROJECT_PERSONNEL 
            INNER JOIN dbo.MST_USER 
            ON TRN_PROJECT_PERSONNEL.USERID = MST_USER.USERID
      WHERE TRN_PROJECT_PERSONNEL.PROJECTID = @PROJECTID_

   END

GO
/****** Object:  StoredProcedure [dbo].[USP_GET_PARAMETERS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_PARAMETERS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @OPTIONAVAILABLEID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   *******************************************************************************************************                  
      *   Procedure Name    : USP_GET_PARAMETERS
      *   Author            : EShwar B V               
      *   Created Date      : 02-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   For Fetching MST_PARAMETER Table Data
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   
      *   1.OPTIONAVAILABLEID_
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. MST_PARAMETER
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   1. MASTER
      *   __________________________________________________________________________________________________________
      *   
      *   *********************************************************************************************************
      */
      SELECT MST_PARAMETER.PARAMETERID, MST_PARAMETER.PARAMETERNAME
      FROM dbo.MST_PARAMETER
      WHERE MST_PARAMETER.ISDELETED = 'False' AND (MST_PARAMETER.OPTIONAVAILABLEID = @OPTIONAVAILABLEID_ OR @OPTIONAVAILABLEID_ = 0)
      ORDER BY lower(MST_PARAMETER.PARAMETERNAME)

   END

GO
/****** Object:  StoredProcedure [dbo].[USP_GET_RESPONSE_REQUESTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_RESPONSE_REQUESTS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @USERID_ /* HHID_ IN TRN_APPROVALTRACKERHEADER.HHID%TYPE,*/float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *    errorMessage_ OUT VARCHAR2
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_GET_CLARIFY_REQUEST
      *   Author            : Edwin Baguma               
      *   Created Date      : 16-AUG-2016                 
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      SELECT DISTINCT 
         TRN_APPROVAL_CLARIFY.ID, 
         TRN_APPROVALTRACKERHEADER.TRACKERHEADERID, 
         TRN_APPROVALTRACKERHEADER.HHID, 
         TRN_PAP_HOUSEHOLD.PAPNAME, 
         REQ.DISPLAYNAME AS REQUESTER, 
         TRN_APPROVAL_CLARIFY.CREATEDDATE, 
         TRN_APPROVAL_CLARIFY.CLARIFYREQUEST, 
         RES.DISPLAYNAME AS RESPONDENT, 
         TRN_APPROVAL_CLARIFY.UPDATEDDATE, 
         TRN_APPROVAL_CLARIFY.CLARIFYRESPONSE, 
         TRN_APPROVAL_CLARIFY.CLARIFYSTATUS, 
         TRN_APPROVAL_CLARIFY.CREATEDBY, 
         TRN_APPROVAL_CLARIFY.UPDATEDBY, 
         TRN_APPROVAL_CLARIFY.ISDELETED
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            LEFT JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD 
            ON TRN_PAP_HOUSEHOLD.HHID = TRN_APPROVALTRACKERHEADER.HHID 
            LEFT JOIN dbo.MST_USER  AS REQ 
            ON TRN_APPROVAL_CLARIFY.CREATEDBY = REQ.USERID 
            LEFT JOIN dbo.MST_USER  AS RES 
            ON TRN_APPROVAL_CLARIFY.UPDATEDBY = RES.USERID
      WHERE upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE' AND (TRN_APPROVAL_CLARIFY.UPDATEDBY = @USERID_ OR TRN_APPROVAL_CLARIFY.CREATEDBY = @USERID_)
      /* AND TRN_APPROVALTRACKERHEADER.HHID = HHID_*/
      ORDER BY TRN_APPROVAL_CLARIFY.CREATEDDATE DESC

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_SMSDETAILSFORDUE]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_SMSDETAILSFORDUE]  
   @SMSTEMPLATECODE_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_SMSDETAILSFORDUE
      *   Author            : Anjankumar               
      *   Created Date      : 26-jun-2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose
      *   
      *   This SP is used to Get the SMS details to send SMS Over Dues   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *   -                                                                                                                         
      *   *********************************************************************************************************
      */
      SELECT MST_SMS_TEMPLATE.SMSTEXT
      FROM dbo.MST_SMS_TEMPLATE
      WHERE MST_SMS_TEMPLATE.SMSTEMPLATECODE = @SMSTEMPLATECODE_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_BUDG]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_CDAP_BUDG]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ float(53),
   @Status_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      IF (@Status_ = 'ALL')
         SELECT 
            CDAPBUDG.CDAP_BUDGID, 
            CDAPCAT.CDAP_CATEGORYNAME, 
            CDAPSUBCAT.CDAP_SUBCATEGORYNAME, 
            UNIT.UNITNAME, 
            CDAPBUDG.QUANTITY, 
            CDAPBUDG.RATEPERUNIT, 
            CASE 
               WHEN CDAPBUDG.FUNDREQSTATUS = 'P' THEN 'Pending Approval'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'S' THEN 'Sent for Approval'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'A' THEN 'Approved'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'D' THEN 'Declined'
            END AS FUNDREQSTATUS
         FROM 
            dbo.TRN_CDAP_BUDG  AS CDAPBUDG 
               INNER JOIN dbo.MST_CDAP_CATEG  AS CDAPCAT 
               ON CDAPBUDG.CDAP_CATEGORYID = CDAPCAT.CDAP_CATEGORYID 
               INNER JOIN dbo.MST_CDAP_SUBCATEG  AS CDAPSUBCAT 
               ON CDAPBUDG.CDAP_SUBCATEGORYID = CDAPSUBCAT.CDAP_SUBCATEGORYID 
               INNER JOIN dbo.MST_UNIT  AS UNIT 
               ON CDAPBUDG.UNIT = UNIT.UNITID
         WHERE CDAPBUDG.PROJECTID = @PROJECTID_ AND CDAPBUDG.ISDELETED = 'False'
         ORDER BY CDAPBUDG.CDAP_BUDGID

      /* Used in the My_Approval Page*/
      IF (@Status_ = 'S')
         SELECT 
            CDAPBUDG.CDAP_BUDGID, 
            CDAPCAT.CDAP_CATEGORYNAME, 
            CDAPSUBCAT.CDAP_SUBCATEGORYNAME, 
            UNIT.UNITNAME, 
            CDAPBUDG.QUANTITY, 
            CDAPBUDG.RATEPERUNIT, 
            CASE 
               WHEN CDAPBUDG.FUNDREQSTATUS = 'P' THEN 'Pending'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'S' THEN 'Send'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'A' THEN 'Approved'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'D' THEN 'Declined'
            END AS FUNDREQSTATUS
         FROM 
            dbo.TRN_CDAP_BUDG  AS CDAPBUDG 
               INNER JOIN dbo.MST_CDAP_CATEG  AS CDAPCAT 
               ON CDAPBUDG.CDAP_CATEGORYID = CDAPCAT.CDAP_CATEGORYID 
               INNER JOIN dbo.MST_CDAP_SUBCATEG  AS CDAPSUBCAT 
               ON CDAPBUDG.CDAP_SUBCATEGORYID = CDAPSUBCAT.CDAP_SUBCATEGORYID 
               INNER JOIN dbo.MST_UNIT  AS UNIT 
               ON CDAPBUDG.UNIT = UNIT.UNITID
         WHERE 
            CDAPBUDG.PROJECTID = @PROJECTID_ AND 
            CDAPBUDG.FUNDREQSTATUS = 'S' AND 
            CDAPBUDG.ISDELETED = 'False'
         ORDER BY CDAPBUDG.CDAP_BUDGID

      IF (@Status_ = 'A')
         SELECT 
            CDAPBUDG.CDAP_BUDGID, 
            CDAPCAT.CDAP_CATEGORYNAME, 
            CDAPSUBCAT.CDAP_SUBCATEGORYNAME, 
            UNIT.UNITNAME, 
            CDAPBUDG.QUANTITY, 
            CDAPBUDG.RATEPERUNIT, 
            CASE 
               WHEN CDAPBUDG.FUNDREQSTATUS = 'P' THEN 'Pending'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'S' THEN 'Send'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'A' THEN 'Approved'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'D' THEN 'Declined'
            END AS FUNDREQSTATUS
         FROM 
            dbo.TRN_CDAP_BUDG  AS CDAPBUDG 
               INNER JOIN dbo.MST_CDAP_CATEG  AS CDAPCAT 
               ON CDAPBUDG.CDAP_CATEGORYID = CDAPCAT.CDAP_CATEGORYID 
               INNER JOIN dbo.MST_CDAP_SUBCATEG  AS CDAPSUBCAT 
               ON CDAPBUDG.CDAP_SUBCATEGORYID = CDAPSUBCAT.CDAP_SUBCATEGORYID 
               INNER JOIN dbo.MST_UNIT  AS UNIT 
               ON CDAPBUDG.UNIT = UNIT.UNITID
         WHERE 
            CDAPBUDG.PROJECTID = @PROJECTID_ AND 
            CDAPBUDG.FUNDREQSTATUS = 'A' AND 
            CDAPBUDG.ISDELETED = 'False'
         ORDER BY CDAPBUDG.CDAP_BUDGID

      IF (@Status_ = 'D')
         SELECT 
            CDAPBUDG.CDAP_BUDGID, 
            CDAPCAT.CDAP_CATEGORYNAME, 
            CDAPSUBCAT.CDAP_SUBCATEGORYNAME, 
            UNIT.UNITNAME, 
            CDAPBUDG.QUANTITY, 
            CDAPBUDG.RATEPERUNIT, 
            CASE 
               WHEN CDAPBUDG.FUNDREQSTATUS = 'P' THEN 'Pending'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'S' THEN 'Send'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'A' THEN 'Approved'
               WHEN CDAPBUDG.FUNDREQSTATUS = 'D' THEN 'Declined'
            END AS FUNDREQSTATUS
         FROM 
            dbo.TRN_CDAP_BUDG  AS CDAPBUDG 
               INNER JOIN dbo.MST_CDAP_CATEG  AS CDAPCAT 
               ON CDAPBUDG.CDAP_CATEGORYID = CDAPCAT.CDAP_CATEGORYID 
               INNER JOIN dbo.MST_CDAP_SUBCATEG  AS CDAPSUBCAT 
               ON CDAPBUDG.CDAP_SUBCATEGORYID = CDAPSUBCAT.CDAP_SUBCATEGORYID 
               INNER JOIN dbo.MST_UNIT  AS UNIT 
               ON CDAPBUDG.UNIT = UNIT.UNITID
         WHERE 
            CDAPBUDG.PROJECTID = @PROJECTID_ AND 
            CDAPBUDG.FUNDREQSTATUS = 'D' AND 
            CDAPBUDG.ISDELETED = 'False'
         ORDER BY CDAPBUDG.CDAP_BUDGID

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_BUDGBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_CDAP_BUDGBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_BUDGID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         TRN_CDAP_BUDG.CDAP_CATEGORYID, 
         TRN_CDAP_BUDG.CDAP_SUBCATEGORYID, 
         TRN_CDAP_BUDG.UNIT, 
         TRN_CDAP_BUDG.QUANTITY, 
         TRN_CDAP_BUDG.RATEPERUNIT
      FROM dbo.TRN_CDAP_BUDG
      WHERE TRN_CDAP_BUDG.CDAP_BUDGID = @CDAP_BUDGID_ AND TRN_CDAP_BUDG.ISDELETED = 'False'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_PHASES]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_CDAP_PHASES]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         TRN_CDAP_PHASE.CDAP_PHASENO, 
         TRN_CDAP_PHASE.CDAP_PHASEID, 
         TRN_CDAP_PHASE.CDAP_PERIODFROM, 
         TRN_CDAP_PHASE.CDAP_PERIODTO, 
         TRN_CDAP_PHASE.PROJECTID, 
         TRN_CDAP_PHASE.EXPENDITURE
      FROM dbo.TRN_CDAP_PHASE
      WHERE TRN_CDAP_PHASE.PROJECTID = @projectid_ AND lower(TRN_CDAP_PHASE.ISDELETED) = 'false'
      ORDER BY TRN_CDAP_PHASE.CDAP_PHASENO

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_CDAP_PHASESBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_CDAP_PHASESBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CDAP_PHASEID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         TRN_CDAP_PHASE.CDAP_PHASENO, 
         TRN_CDAP_PHASE.CDAP_PHASEID, 
         TRN_CDAP_PHASE.CDAP_PERIODFROM, 
         TRN_CDAP_PHASE.CDAP_PERIODTO, 
         TRN_CDAP_PHASE.PROJECTID, 
         TRN_CDAP_PHASE.EXPENDITURE
      FROM dbo.TRN_CDAP_PHASE
      WHERE TRN_CDAP_PHASE.CDAP_PHASEID = @CDAP_PHASEID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_MNE_EVAL]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_MNE_EVAL]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ProjectID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_DEL_TRN_MNE_EVAL
      *   Author            : Victor Maity               
      *   Created Date      : 25-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *          
      *           PROJECTID_,
      *         
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. trn_mne_eval
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT eval.EVALUATIONID, mstgoal.GOALNAME, eval.GOALDESCRIPTION
      FROM 
         dbo.TRN_MNE_EVAL  AS eval 
            LEFT JOIN dbo.MST_MNE_GOAL  AS mstgoal 
            ON eval.GOALID = mstgoal.GOALID
      WHERE eval.PROJECTID = @ProjectID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_MNE_EVALBYID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_MNE_EVALBYID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @EVALUATIONID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_DEL_TRN_MNE_EVAL
      *   Author            : Victor Maity               
      *   Created Date      : 25-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *           EVALUATIONID_
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. trn_mne_eval
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT 
         TRN_MNE_EVAL.GOALID, 
         TRN_MNE_EVAL.GOALDESCRIPTION, 
         TRN_MNE_EVAL.GOALNARRATIVE, 
         TRN_MNE_EVAL.PROJECTID, 
         TRN_MNE_EVAL.EVALUATIONID, 
         TRN_MNE_EVAL.ISDELETED
      FROM dbo.TRN_MNE_EVAL
      WHERE TRN_MNE_EVAL.EVALUATIONID = @EVALUATIONID_

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_PAP_HOUSEHOLD]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_PAP_HOUSEHOLD]  
   @VILLAGE_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      /*village_ in array[max]*/
      SELECT TRN_PAP_HOUSEHOLD.PAP_UID, TRN_PAP_HOUSEHOLD.HHID, TRN_PAP_HOUSEHOLD.PAPNAME
      FROM dbo.TRN_PAP_HOUSEHOLD
      WHERE TRN_PAP_HOUSEHOLD.ISDELETED = 'False' AND TRN_PAP_HOUSEHOLD.VILLAGE IN (  @VILLAGE_ )
      ORDER BY TRN_PAP_HOUSEHOLD.PAPNAME

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_TRN_PAP_LND_VALUATION]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_TRN_PAP_LND_VALUATION]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ /*LND_VALUATIONID_ IN TRN_PAP_LND_VALUATION.LND_VALUATIONID%TYPE,*/float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         /*LND_VALUATIONID,*/TRN_PAP_LND_VALUATION.LANDOWNER, 
         TRN_PAP_LND_VALUATION.LANDBLOCK, 
         TRN_PAP_LND_VALUATION.LANDPLOT, 
         TRN_PAP_LND_VALUATION.PROPRIETORID, 
         TRN_PAP_LND_VALUATION.WHOLEACREAGEACRES, 
         TRN_PAP_LND_VALUATION.ROWACRES, 
         TRN_PAP_LND_VALUATION.ROWLANDVALUESHARE, 
         TRN_PAP_LND_VALUATION.ROWRATEPERACRE, 
         TRN_PAP_LND_VALUATION.ROWLANDVALUE, 
         TRN_PAP_LND_VALUATION.WLACRES, 
         TRN_PAP_LND_VALUATION.DIMUNITIONLEVEL, 
         TRN_PAP_LND_VALUATION.WLRATEPERACRE, 
         TRN_PAP_LND_VALUATION.WLLANDVALUESHARE, 
         TRN_PAP_LND_VALUATION.WLLANDVALUE, 
         TRN_PAP_LND_VALUATION.UPDATEDBY, 
         TRN_PAP_LND_VALUATION.UPDATEDDATE, 
         TRN_PAP_LND_VALUATION.STAKEHOLDERDESIGNATION
      FROM dbo.TRN_PAP_LND_VALUATION
      WHERE TRN_PAP_LND_VALUATION.HHID = @HHID_
      /* and (LND_VALUATIONID = LND_VALUATIONID_ or LND_VALUATIONID_ is null);*/

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_VILLAGEID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_VILLAGEID]  
   @VILLAGES_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      
      
      /* 
      *   SSMA error messages:
      *   O2SS0050: Conversion of identifier 'regexp_substr(VARCHAR2, CHAR, BINARY_INTEGER, NUMBER)' is not supported.

      WITH 
         h$cte AS 
         (
            SELECT 1 AS LEVEL, CAST(row_number() OVER(
               ORDER BY @@spid) AS varchar(max)) AS path
             UNION ALL
            SELECT h$cte.LEVEL + 1 AS LEVEL, path + ',' + CAST(row_number() OVER(
               ORDER BY @@spid) AS varchar(max)) AS path
            FROM h$cte
            WHERE (NULL) IS NOT NULL
         )
      
         SELECT MST_VILLAGE.VILLAGEID
         FROM dbo.MST_VILLAGE
         WHERE MST_VILLAGE.VILLAGENAME IN 
            (
               /* 
               *   SSMA error messages:
               *   O2SS0050: Conversion of identifier 'regexp_substr(VARCHAR2, CHAR, BINARY_INTEGER, NUMBER)' is not supported.

               SELECT TOP 9223372036854775807 (NULL)
               FROM h$cte
               ORDER BY h$cte.path               */


            )      */



   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_WIS_CONFIGURATION]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GET_WIS_CONFIGURATION]  
   @configitem_ varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_GET_WIS_CONFIGURATION
      *   Author            : Iranna Shirol               
      *   Created Date      : 18 JUNE 2013                  
      *   Copyright©        : Copyright © UETCL
      *   __________________________________________________________________________________________________________
      *   Purpose : This SP is used to RETRIEVE records from the data into Table WISCONFIGURATION 
      *   __________________________________________________________________________________________________________
      *   Input Parameters : configitem_  
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   TableName:  1. WISCONFIGURATION
      *   __________________________________________________________________________________________________________
      *   Modules Affected
      *   1. Open Batch for Payment Request
      *   __________________________________________________________________________________________________________
      *   
      *   *********************************************************************************************************
      */
      SELECT WISCONFIGURATION.CONFIGITEM, WISCONFIGURATION.CONFIGDATA
      FROM dbo.WISCONFIGURATION
      WHERE upper(WISCONFIGURATION.CONFIGITEM) LIKE '%' + ISNULL(upper(@configitem_), '') + '%'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GETCOMMENTS_DATABY_HHID]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GETCOMMENTS_DATABY_HHID]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         HH.COMMENTID, 
         HH.HHID AS HHID, 
         HH.COMMENTS AS COMMENTS, 
         HH.OPTIONGROUPID, 
         HH.CREATEDDATE
      FROM 
         dbo.TRN_OPT_COMMENTS  AS HH 
            LEFT OUTER JOIN dbo.MST_USER  AS USC 
            ON USC.USERID = HH.CREATEDBY 
            LEFT OUTER JOIN dbo.MST_USER  AS USU 
            ON USU.USERID = HH.UPDATEDBY 
            LEFT OUTER JOIN dbo.MST_OPTION_GROUPS  AS mog 
            ON mog.OPTIONGROUPID = HH.OPTIONGROUPID
      WHERE HH.HHID = @HHID_ AND HH.COMMENTID = 
         (
            SELECT isnull(max(TRN_OPT_COMMENTS.COMMENTID), 0) AS expr
            FROM dbo.TRN_OPT_COMMENTS
            WHERE TRN_OPT_COMMENTS.HHID = @HHID_
         )
      ORDER BY HH.COMMENTID DESC

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_GETNASCOOLDETAILS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GETNASCOOLDETAILS]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         MST_NEVERATTENDEDSCHOOLREASON.NVR_ATT_SCH_REASONID, 
         MST_NEVERATTENDEDSCHOOLREASON.NVR_ATT_SCH_REASON, 
         MST_NEVERATTENDEDSCHOOLREASON.DESCRIPTION, 
         MST_NEVERATTENDEDSCHOOLREASON.ISDELETED, 
         MST_NEVERATTENDEDSCHOOLREASON.CREATEDBY, 
         MST_NEVERATTENDEDSCHOOLREASON.CREATEDDATE, 
         MST_NEVERATTENDEDSCHOOLREASON.UPDATEDBY, 
         MST_NEVERATTENDEDSCHOOLREASON.UPDATEDDATE
      FROM dbo.MST_NEVERATTENDEDSCHOOLREASON

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_HOUSE_IN_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_HOUSE_IN_PENDING]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ProjectID_ float(53),
   @ProjectFromtDate_ datetime2(0),
   @ProjectToDate_ datetime2(0)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         GRP.OPTIONGROUP, 
         
            (
               SELECT count_big(*) AS expr
               FROM dbo.TRN_PAP_STAKEHOLDER  AS A
               WHERE A.HHID = PAP.HHID
            ) AS NoofStakeholderswaiting, 
         FIN.RESINKINDCOMPENSATION, 
         PAP.PAPNAME, 
         PAP.VILLAGE
      FROM 
         dbo.TRN_PROJECT_DETAILS  AS PRJ 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD  AS PAP 
            ON PAP.PROJECTID = PRJ.PROJECTID 
            INNER JOIN dbo.MST_OPTION_GROUPS  AS GRP 
            ON GRP.OPTIONGROUPID = PAP.OPTIONGROUPID 
            INNER JOIN dbo.TRN_CMP_FINANCIALS  AS FIN 
            ON FIN.HHID = PAP.HHID 
            INNER JOIN dbo.TRN_PAP_VALUATION_SUMMARY  AS VAL 
            ON VAL.HHID = PAP.HHID
      WHERE 
         FIN.RESINKINDCOMPENSATION IS NOT NULL AND 
         FIN.RESINKINDCOMPENSATION <> 'NA' AND 
         VAL.PAYMENTSTATUS <> 'CP' AND 
         PRJ.PROJECTID = @ProjectID_ AND 
         (isnull(PAP.UPDATEDDATE, PAP.CREATEDDATE) >= @PROJECTFROMTDATE_ OR @PROJECTFROMTDATE_ IS NULL) AND 
         (isnull(PAP.UPDATEDDATE, PAP.CREATEDDATE) <= DATEADD(D, 1, @ProjectToDate_) OR @ProjectToDate_ IS NULL)

   END

GO
/****** Object:  StoredProcedure [dbo].[USP_INS_APPROVALTEMPAUTHORISER]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_APPROVALTEMPAUTHORISER]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @APPROVALTEMPAUTHORISERID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @AUTHORISERID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ASSIGNTOID_ float(53),
   @FROMDATE_ datetime2(0),
   @TODATE_ datetime2(0),
   @REMARKS_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY_ float(53),
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
      *   DELETE FROM TRN_APPROVALTEMPAUTHORISER
      *   WHERE fromdate = FROMDATE_
      *   and authoriserid = AUTHORISERID_ and ASSIGNTOID = ASSIGNTOID_;
      */
      SELECT @row_count = count_big(1)
      FROM dbo.TRN_APPROVALTEMPAUTHORISER
      WHERE /*LOWER(ASSIGNTOID) = LOWER(ASSIGNTOID_) and*/
         lower(TRN_APPROVALTEMPAUTHORISER.ISDELETED) = 'false' AND 
         TRN_APPROVALTEMPAUTHORISER.FROMDATE <= ssma_oracle.trunc_date(@FROMDATE_) AND 
         TRN_APPROVALTEMPAUTHORISER.TODATE >= ssma_oracle.trunc_date(@TODATE_) AND 
         TRN_APPROVALTEMPAUTHORISER.PROJECTID = @PROJECTID_ AND 
         (TRN_APPROVALTEMPAUTHORISER.AUTHORISERID = @AUTHORISERID_) AND 
         (TRN_APPROVALTEMPAUTHORISER.APPROVALTEMPAUTHORISERID <> @APPROVALTEMPAUTHORISERID_ OR @APPROVALTEMPAUTHORISERID_ = 0)

      IF @row_count = 0
         IF @APPROVALTEMPAUTHORISERID_ = 0
            BEGIN

               INSERT dbo.TRN_APPROVALTEMPAUTHORISER(
                  APPROVALTEMPAUTHORISERID, 
                  AUTHORISERID, 
                  ASSIGNTOID, 
                  FROMDATE, 
                  TODATE, 
                  REMARKS, 
                  ISDELETED, 
                  CREATEDBY, 
                  CREATEDDATE, 
                  PROJECTID)
                  VALUES (
                     NEXT VALUE FOR dbo.SEQ_TRN_APPROVALTEMPAUTHORISER, 
                     @AUTHORISERID_, 
                     @ASSIGNTOID_, 
                     @FROMDATE_, 
                     @TODATE_, 
                     @REMARKS_, 
                     'False', 
                     @CREATEDBY_, 
                     sysdatetime(), 
                     @PROJECTID_)

               SET @errormessage_ = NULL

            END
         ELSE 
            BEGIN

               UPDATE dbo.TRN_APPROVALTEMPAUTHORISER
                  SET 
                     AUTHORISERID = @AUTHORISERID_, 
                     ASSIGNTOID = @ASSIGNTOID_, 
                     FROMDATE = @FROMDATE_, 
                     TODATE = @TODATE_, 
                     REMARKS = @REMARKS_, 
                     UPDATEDBY = @CREATEDBY_, 
                     UPDATEDDATE = sysdatetime()
               WHERE TRN_APPROVALTEMPAUTHORISER.APPROVALTEMPAUTHORISERID = @APPROVALTEMPAUTHORISERID_

               SET @errormessage_ = NULL

            END
      ELSE 
         SET @errormessage_ = 'Temp Authorization already exists.'

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_CLARIFY_REQUEST]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_CLARIFY_REQUEST]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @TRACKHDR_ float(53),
   @REQUEST_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY_ /* STATUS_ IN TRN_APPROVAL_CLARIFY.CLARIFYSTATUS%TYPE,   ISDELETED_ IN TRN_APPROVAL_CLARIFY.ISDELETED%TYPE,*/float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @UPDATEDBY_ float(53),
   @ERRORMSG_ varchar(max)  OUTPUT
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      SET @ERRORMSG_ = NULL

      
      /*
      *   ********************************************************************************************************  
      *   
      *   Procedure Name    : USP_INS_CLARIFY_REQUEST
      *   Author            : Edwin Baguma               
      *   Created Date      : 25-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   ***********************************************************************************************************
      *   
      *   TRN_APPROVAL_CLARIFY
      *                                                                                                                                  
      *   *********************************************************************************************************
      */
      DECLARE
         /*
         *   SSMA warning messages:
         *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
         */

         @EXISTING_REQUESTS float(53)

      SELECT @EXISTING_REQUESTS = count_big(1)
      FROM 
         dbo.TRN_APPROVAL_CLARIFY 
            INNER JOIN dbo.TRN_APPROVALTRACKERHEADER 
            ON TRN_APPROVAL_CLARIFY.TRACKERHEADERID = TRN_APPROVALTRACKERHEADER.TRACKERHEADERID
      WHERE 
         TRN_APPROVAL_CLARIFY.TRACKERHEADERID IN 
         (
            SELECT TRN_APPROVALTRACKERHEADER.TRACKERHEADERID
            FROM dbo.TRN_APPROVALTRACKERHEADER
            WHERE TRN_APPROVALTRACKERHEADER.HHID = @HHID_ AND TRN_APPROVALTRACKERHEADER.PAGECODE = 'CRFND'
         ) AND 
         TRN_APPROVAL_CLARIFY.CLARIFYSTATUS = 'Pending' AND 
         upper(TRN_APPROVAL_CLARIFY.ISDELETED) = 'FALSE'

      IF @EXISTING_REQUESTS = 0
         BEGIN

            INSERT dbo.TRN_APPROVAL_CLARIFY(
               ID, 
               TRACKERHEADERID, 
               CLARIFYREQUEST, 
               CLARIFYSTATUS, 
               CREATEDBY, 
               CREATEDDATE, 
               UPDATEDBY, 
               ISDELETED)
               VALUES (
                  NEXT VALUE FOR dbo.TRN_APPROVAL_CLARIFY_SEQ, 
                  @TRACKHDR_, 
                  @REQUEST_, 
                  'Pending', 
                  @CREATEDBY_, 
                  sysdatetime(), 
                  @UPDATEDBY_, 
                  'False')

            SET @ERRORMSG_ = 'Success'

         END
      ELSE 
         SET @ERRORMSG_ = 'Failed'

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MNEEVALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_MNEEVALELEMENTS]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @EVALUATIONID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @GOAL_ELEMENTID_ float(53),
   @EVALELEMENTDESCRIPTIONN_ nvarchar(max),
   @ISDELETED_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY_ float(53),
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

         @ROWCOUNT float(53)

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_INS_MNEEVALELEMENTS
      *   Author            : Victor Maity               
      *   Created Date      : 26-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *       EVALUATIONID_,
      *       GOAL_ELEMENTID_,
      *       EVALELEMENTDESCRIPTIONN_,
      *       ISDELETED_,
      *       CREATEDBY_
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. trn_mne_evalelements
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT @ROWCOUNT = count_big(*)
      FROM dbo.TRN_MNE_EVALELEMENTS
      WHERE TRN_MNE_EVALELEMENTS.EVALUATIONID = @EVALUATIONID_ AND TRN_MNE_EVALELEMENTS.GOAL_ELEMENTID = @GOAL_ELEMENTID_

      IF (@ROWCOUNT = 0)
         BEGIN

            INSERT dbo.TRN_MNE_EVALELEMENTS(
               EVALELEMENTID, 
               EVALUATIONID, 
               GOAL_ELEMENTID, 
               EVALELEMENTDESCRIPTIONN, 
               ISDELETED, 
               CREATEDBY, 
               CREATEDDATE)
               VALUES (
                  NEXT VALUE FOR dbo.SEQ_TRN_MNE_EVALELEMENTS, 
                  @EVALUATIONID_, 
                  @GOAL_ELEMENTID_, 
                  @EVALELEMENTDESCRIPTIONN_, 
                  @ISDELETED_, 
                  @CREATEDBY_, 
                  sysdatetime())

            SET @errorMessage_ = NULL

         END
      ELSE 
         SET @errorMessage_ = 'The particular Goal Element already exists in this Evaluation'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MST_COMMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_MST_COMMENTS]  
   @Comments_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @OPTIONGROUPID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY_ float(53),
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

      SELECT @row_count = count_big(*)
      FROM dbo.TRN_OPT_COMMENTS
      WHERE 
         TRN_OPT_COMMENTS.OPTIONGROUPID = @OPTIONGROUPID_ AND 
         TRN_OPT_COMMENTS.HHID = @HHID_ AND 
         TRN_OPT_COMMENTS.COMMENTS = @Comments_

      IF @row_count = 0
         BEGIN

            INSERT dbo.TRN_OPT_COMMENTS(
               COMMENTID, 
               COMMENTS, 
               OPTIONGROUPID, 
               HHID, 
               CREATEDBY, 
               CREATEDDATE)
               VALUES (
                  NEXT VALUE FOR dbo.SEQ_COMMENTS, 
                  @Comments_, 
                  @OPTIONGROUPID_, 
                  @HHID_, 
                  @createdBy_, 
                  sysdatetime())

            IF @@TRANCOUNT > 0
               COMMIT WORK 

         END

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_MST_CULTUREPROPERTY]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_MST_CULTUREPROPERTY]  
   @CultureProptype_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CreatedBy_ float(53),
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

      SELECT @row_count = count_big(MST_CULTURE_PROP_TYPE.CULTUREPROPTYPE)
      FROM dbo.MST_CULTURE_PROP_TYPE
      WHERE lower(MST_CULTURE_PROP_TYPE.CULTUREPROPTYPE) = lower(@CultureProptype_)

      IF @row_count = 0
         BEGIN

            INSERT dbo.MST_CULTURE_PROP_TYPE(
               CULTUREPROPTYPEID, 
               CULTUREPROPTYPE, 
               HASDIMENSIONS, 
               ISDELETED, 
               CREATEDBY, 
               CREATEDDATE)
               VALUES (
                  NEXT VALUE FOR dbo.SEQ_MST_CTYPE, 
                  @CultureProptype_, 
                  'False', 
                  'False', 
                  @CreatedBy_, 
                  sysdatetime())

            IF @@TRANCOUNT > 0
               COMMIT WORK 

            SET @errormessage_ = NULL

         END
      ELSE 
         SET @errormessage_ = 'Culture Property Type' + ISNULL(@CultureProptype_, '') + 'already exists in the system for this PAP.'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_TRN_CONCERN]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_TRN_CONCERN]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ConcernID_ float(53),
   @OtherConcern_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @HHID_ float(53),
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

      SELECT @row_count = count_big(1)
      FROM dbo.TRN_PAP_CONCERNS
      WHERE 
         TRN_PAP_CONCERNS.CONCERNID = @CONCERNID_ AND 
         lower(TRN_PAP_CONCERNS.OTHERCONCERN) = lower(@OtherConcern_) AND 
         TRN_PAP_CONCERNS.HHID = @HHID_

      IF @row_count = 0
         BEGIN

            INSERT dbo.TRN_PAP_CONCERNS(
               PAP_CONCERNID, 
               HHID, 
               CONCERNID, 
               OTHERCONCERN, 
               CREATEDBY, 
               CREATEDDATE)
               VALUES (
                  NEXT VALUE FOR dbo.SEQ_TRN_CONCERN, 
                  @HHID_, 
                  @ConcernID_, 
                  @OtherConcern_, 
                  @CREATEDBY, 
                  sysdatetime())

            IF @@TRANCOUNT > 0
               COMMIT WORK 

            SET @errormessage_ = NULL

         END
      ELSE 
         SET @errormessage_ = 'Selected Concern already exists in the system for this PAP.'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_TRN_MNE_EVAL]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INS_TRN_MNE_EVAL]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @PROJECTID_ /*EVALUATIONID_ IN trn_mne_eval.evaluationid%type,*/float(53),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @GOALID_ float(53),
   @GOALDESCRIPTION_ varchar(max),
   @GOALNARRATIVE_ nvarchar(max),
   @ISDELETED_ varchar(max),
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @CREATEDBY_ float(53),
   @errorMessage_ varchar(max)  OUTPUT
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      SET @errorMessage_ = NULL

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_DEL_TRN_MNE_EVAL
      *   Author            : Victor Maity               
      *   Created Date      : 25-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Input Parameters 
      *           PROJECTID_,
      *           GOALID_,
      *           GOALDESCRIPTION_,
      *           GOALNARRATIVE_,
      *           ISDELETED_,
      *           CREATEDBY_
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. trn_mne_eval
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      DECLARE
         /*
         *   SSMA warning messages:
         *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
         */

         @ROWCOUNT float(53)

      /*SEQ_TRN_MNE_EVAL*/
      SELECT @ROWCOUNT = count_big(*)
      FROM dbo.TRN_MNE_EVAL
      WHERE TRN_MNE_EVAL.GOALID = @GOALID_ AND TRN_MNE_EVAL.PROJECTID = @PROJECTID_

      IF (@ROWCOUNT = 0)
         BEGIN

            INSERT dbo.TRN_MNE_EVAL(
               EVALUATIONID, 
               PROJECTID, 
               GOALID, 
               GOALDESCRIPTION, 
               GOALNARRATIVE, 
               ISDELETED, 
               CREATEDBY, 
               CREATEDDATE)
               VALUES (
                  NEXT VALUE FOR dbo.SEQ_TRN_MNE_EVAL, 
                  @PROJECTID_, 
                  @GOALID_, 
                  @GOALDESCRIPTION_, 
                  @GOALNARRATIVE_, 
                  @ISDELETED_, 
                  @CREATEDBY_, 
                  sysdatetime())

            SET @errorMessage_ = NULL

            IF @@TRANCOUNT > 0
               COMMIT WORK 

         END
      ELSE 
         SET @errorMessage_ = 'The particular Goal already exists in this project'

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_OPTIONGRP]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INSERT_OPTIONGRP]  
   /* 
   *   SSMA error messages:
   *   O2SS0005: The source datatype 'OPTIONGROUPPARAMETER.OPTIONGROUP%TYPE' was not recognized.
   */

   @OptionGrp_ varchar(8000),
   /* 
   *   SSMA error messages:
   *   O2SS0005: The source datatype 'OPTIONGROUPPARAMETERS.PARAMETERS%TYPE' was not recognized.
   */

   @optionStatus_ varchar(8000),
   /* 
   *   SSMA error messages:
   *   O2SS0005: The source datatype 'OPTIONGROUPPARAMETERS.OPTIONNAME%TYPE' was not recognized.
   */

   @OptionGrpName_ varchar(8000),
   /* 
   *   SSMA error messages:
   *   O2SS0005: The source datatype 'OPTIONGROUPPARAMETERS.STATUSNAME%TYPE' was not recognized.
   */

   @optionStatusName_ varchar(8000)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      /* 
      *   SSMA error messages:
      *   O2SS0083: Identifier OPTIONGROUPPARAMETERS cannot be converted because it was not resolved.
      *   O2SS0083: Identifier PARAMETERID cannot be converted because it was not resolved.
      *   O2SS0083: Identifier OPTIONGROUP cannot be converted because it was not resolved.
      *   O2SS0083: Identifier PARAMETERS cannot be converted because it was not resolved.
      *   O2SS0083: Identifier OPTIONNAME cannot be converted because it was not resolved.
      *   O2SS0083: Identifier STATUSNAME cannot be converted because it was not resolved.

      
      /-*
      *   errorMessage_ OUT VARCHAR2
      *   row_count NUMBER;
      *    SELECT count(BANKNAME) INTO row_count FROM MST_BANK  WHERE LOWER(BANKNAME) = LOWER(bankName_);
      *   IF row_count = 0 THEN
      *-/
      INSERT OPTIONGROUPPARAMETERS(
         PARAMETERID, 
         OPTIONGROUP, 
         PARAMETERS, 
         OPTIONNAME, 
         STATUSNAME)
         VALUES (
            NEXT VALUE FOR dbo.SEQ_MST_OPTION_GROUPS, 
            @OptionGrp_, 
            @optionStatus_, 
            @OptionGrpName_, 
            @optionStatusName_)      */



      IF @@TRANCOUNT > 0
         COMMIT WORK 
      
      /*
      *    ELSE
      *   errormessage_:='Bank '||bankName_ ||' already exists in the system.';
      *    END IF;
      */

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERTINNOTATTENTEDSCHOOL]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INSERTINNOTATTENTEDSCHOOL]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @NVR_ATT_SCH_REASON float(53),
   @DESCRIPTION varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      INSERT dbo.MST_NEVERATTENDEDSCHOOLREASON(
         NVR_ATT_SCH_REASON, 
         DESCRIPTION, 
         CREATEDBY, 
         CREATEDDATE, 
         ISDELETED)
         VALUES (
            @NVR_ATT_SCH_REASON, 
            @DESCRIPTION, 
            '1', 
            ssma_oracle.to_date2(sysdatetime(), 'DD MM YYYY'), 
            'false')

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERTNVRATTDSCHL]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_INSERTNVRATTDSCHL]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @p_NVR_ATT_SCH_REASONID float(53),
   @p_NVRATTSCHREASON varchar(max),
   @p_DESCRIPTION varchar(max)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      IF @p_NVR_ATT_SCH_REASONID IS NOT NULL
         INSERT dbo.MST_NEVERATTENDEDSCHOOLREASON(
            NVR_ATT_SCH_REASONID, 
            NVR_ATT_SCH_REASON, 
            DESCRIPTION, 
            ISDELETED, 
            CREATEDBY, 
            CREATEDDATE)
            VALUES (
               @p_NVR_ATT_SCH_REASONID, 
               @p_NVRATTSCHREASON, 
               @p_DESCRIPTION, 
               '1', 
               '1', 
               sysdatetime())

      IF @@TRANCOUNT > 0
         COMMIT WORK 

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_LAND_INKIND_PENDING]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_LAND_INKIND_PENDING]  
   /*
   *   SSMA warning messages:
   *   O2SS0356: Conversion from NUMBER datatype can cause data loss.
   */

   @ProjectID_ float(53),
   @ProjectFromtDate_ datetime2(0),
   @ProjectToDate_ datetime2(0)
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      SELECT 
         GRP.OPTIONGROUP, 
         
            (
               SELECT count_big(*) AS expr
               FROM dbo.TRN_PAP_STAKEHOLDER  AS A
               WHERE A.HHID = PAP.HHID
            ) AS NoofStakeholderswaiting, 
         FIN.LANDINKINDCOMPENSATION, 
         PAP.PAPNAME, 
         PAP.VILLAGE, 
         PAP.HHID_DISP AS HHID
      FROM 
         dbo.TRN_PROJECT_DETAILS  AS PRJ 
            INNER JOIN dbo.TRN_PAP_HOUSEHOLD  AS PAP 
            ON PAP.PROJECTID = PRJ.PROJECTID 
            INNER JOIN dbo.MST_OPTION_GROUPS  AS GRP 
            ON GRP.OPTIONGROUPID = PAP.OPTIONGROUPID 
            INNER JOIN dbo.TRN_CMP_FINANCIALS  AS FIN 
            ON FIN.HHID = PAP.HHID 
            INNER JOIN dbo.TRN_PAP_VALUATION_SUMMARY  AS VAL 
            ON VAL.HHID = PAP.HHID
      WHERE 
         FIN.LANDINKINDCOMPENSATION IS NOT NULL AND 
         VAL.PAYMENTSTATUS <> 'CP' AND 
         PRJ.PROJECTID = @ProjectID_ AND 
         (isnull(PAP.UPDATEDDATE, PAP.CREATEDDATE) >= @PROJECTFROMTDATE_ OR @PROJECTFROMTDATE_ IS NULL) AND 
         (isnull(PAP.UPDATEDDATE, PAP.CREATEDDATE) <= DATEADD(D, 1, @ProjectToDate_) OR @ProjectToDate_ IS NULL)

   END
GO
/****** Object:  StoredProcedure [dbo].[USP_LOAD_MNEGOALELEMENTS]    Script Date: 11/8/2017 12:02:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_LOAD_MNEGOALELEMENTS]  
   /*
   *   SSMA warning messages:
   *   O2SS0259: CURSOR type was converted to varchar(8000).
   */

   
AS 
   /*Generated by SQL Server Migration Assistant for Oracle version 6.0.0.*/
   BEGIN

      

      
      /*
      *   ********************************************************************************************************                  
      *   Procedure Name    : USP_LOAD_MNEGOALELEMENTS
      *   Author            : Victor Maity               
      *   Created Date      : 26-June-2013                  
      *   Copyright©        : Copyright © UETCL
      *   
      *   __________________________________________________________________________________________________________
      *   Tables Used
      *   
      *   1. MST_MNE_GOAL_ELEMENTS
      *   __________________________________________________________________________________________________________
      *   
      *                                                                                                                                   
      *   *********************************************************************************************************
      */
      SELECT MST_MNE_GOAL_ELEMENTS.GOAL_ELEMENTID AS ID, MST_MNE_GOAL_ELEMENTS.GOAL_ELEMENTNAME AS NAME
      FROM dbo.MST_MNE_GOAL_ELEMENTS
      WHERE MST_MNE_GOAL_ELEMENTS.ISDELETED = 'False'

   END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_MST_CDAP_CATEG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_CDAP_CATEG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_MST_CDAP_SUBCATEG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_CDAP_SUBCATEG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_MST_LIVBUDCATG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_LIVBUDCATG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_MST_LIVCATG_ITEM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_MST_LIVCATG_ITEM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_OPT_PRM_MAPPING' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_OPT_PRM_MAPPING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_ALL_OPTIONGRP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_ALL_OPTIONGRP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_BUILD_CONFIG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_BUILD_CONFIG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CDAP_PHASE_ACTIVITYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAP_PHASE_ACTIVITYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CDAP_PHASEID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAP_PHASEID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CDAPPLANDISPLAY' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CDAPPLANDISPLAY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CLARIFY_PENDING' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_PENDING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CLARIFY_REQUEST' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_REQUEST'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CLARIFY_REQUESTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_REQUESTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_CLARIFY_STATUS_PEND' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_CLARIFY_STATUS_PEND'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_COUNTYNAME' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_COUNTYNAME'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_EMAILDETAILSFORDUE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_EMAILDETAILSFORDUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MNEEVALELEMENTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MNEEVALELEMENTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MNEEVALELEMENTSBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MNEEVALELEMENTSBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_CDAP_ACTIVITY' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_ACTIVITY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_CDAP_CATEG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_CATEG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_CDAP_CATEGBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_CATEGBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_CDAP_SUBCATEG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_SUBCATEG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_CDAP_SUBCATEGBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_CDAP_SUBCATEGBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_LIVBUDCATG_ITEM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LIVBUDCATG_ITEM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_LIVBUDCATGBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LIVBUDCATGBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_MST_LND_TYPE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_MST_LND_TYPE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_OPT_HOUSEHOLD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPT_HOUSEHOLD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_OPT_PRM_MAPPING_BYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPT_PRM_MAPPING_BYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_OPTION_PARAM_BYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTION_PARAM_BYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_OPTIONAVAIL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTIONAVAIL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_OPTIONGRPDETAILS_BYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_OPTIONGRPDETAILS_BYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_PAP_PAYT_STATUS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAP_PAYT_STATUS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_PAP_PROJECT_USERS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAP_PROJECT_USERS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_PAPSBYVILLAGE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PAPSBYVILLAGE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_PARAMETERS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PARAMETERS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_PHASEACTIVITY' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_PHASEACTIVITY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_RESPONSE_REQUESTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_RESPONSE_REQUESTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_SMSDETAILSFORDUE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_SMSDETAILSFORDUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_CDAP_BUDG' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_BUDG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_CDAP_BUDGBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_BUDGBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_CDAP_PHASES' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_PHASES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_CDAP_PHASESBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_CDAP_PHASESBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_MNE_EVAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_MNE_EVAL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_MNE_EVALBYID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_MNE_EVALBYID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_PAP_HOUSEHOLD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_PAP_HOUSEHOLD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_TRN_PAP_LND_VALUATION' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_TRN_PAP_LND_VALUATION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_VILLAGEID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_VILLAGEID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GET_WIS_CONFIGURATION' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GET_WIS_CONFIGURATION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GETCOMMENTS_DATABY_HHID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GETCOMMENTS_DATABY_HHID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_GETNASCOOLDETAILS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_GETNASCOOLDETAILS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_HOUSE_IN_PENDING' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_HOUSE_IN_PENDING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_IN_KIND_DELIVERED' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_IN_KIND_DELIVERED'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_APPROVALTEMPAUTHORISER' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_APPROVALTEMPAUTHORISER'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_CLARIFY_REQUEST' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_CLARIFY_REQUEST'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_MNEEVALELEMENTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MNEEVALELEMENTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_MST_COMMENTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MST_COMMENTS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_MST_CULTUREPROPERTY' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_MST_CULTUREPROPERTY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_TRN_CONCERN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_TRN_CONCERN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INS_TRN_MNE_EVAL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INS_TRN_MNE_EVAL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INSERT_OPTIONGRP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERT_OPTIONGRP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INSERTINNOTATTENTEDSCHOOL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERTINNOTATTENTEDSCHOOL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_INSERTNVRATTDSCHL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_INSERTNVRATTDSCHL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_LAND_INKIND_PENDING' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_LAND_INKIND_PENDING'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'UETCLWIS.USP_LOAD_MNEGOALELEMENTS' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'USP_LOAD_MNEGOALELEMENTS'
GO
