<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  EnableEventValidation="true"
    CodeBehind="CompensationPackages.aspx.cs" Inherits="WIS.CompensationPackages" %>

<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/page_specific.css" />
    <script type="text/javascript">
    function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
        var left = (screen.width - 600) / 2;
        var top = (screen.height - 500) / 4;
        open('../../UI/EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
    }

    function OpenPackageStatus() {

        var HHID = "<%=Session["HH_ID"]%>";
        var ProjectID = "<%=Session["PROJECT_ID"]%>"

        var left = (screen.width - 960) / 2;
        var top = (screen.height - 650) / 4;

        var modal = 'yes';
        var ReportWindow = "";
        ReportWindow = window.open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PKREV&ProjectID=' + ProjectID + '&HHID=' + HHID, 'Package Status', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left + ',modal=' + modal);
        ReportWindow.focus();
    }
    
    function OpenReviewCom() {
        var left = (screen.width - 960) / 2;
        var top = (screen.height - 650) / 4;
        showModalDialog('PrintComments.aspx?', 'ReviewComments', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
  
        <a id="lnkPackageStatus" href="" class="iceStatusLinks" style="border-left:1px solid; float:right;" onclick="OpenPackageStatus();">View Approval Status</a>
<a id="lnkAppReviewCom" href="" class="iceStatusLinks" onclick="OpenReviewCom();" style="float:right;">View Print Comments</a>
    <table cellspacing="0" width="100%"  class="tftable">
        <!--class="tftable"-->
        <tr>
            <td class="gridHeaderStyle" style="height: 24px">
                Description
            </td>
            <td class="gridHeaderStyle" style="width: 10%; text-align:center;">
                View
            </td>
            <td class="gridHeaderStyle" style="width: 10%; text-align:center;" id="thPrint" runat="server">
                Print
            </td>
            <td class="gridHeaderStyle" style="width: 13%" id="thApprover" visible="false" runat="server" >
                Approver
            </td>
            <td class="gridHeaderStyle" style="width: 13%" id="thStatus" visible="false" runat="server" >
                Review Status
            </td>
        </tr>
    </table>
    <%--<table border="0" cellspacing="0" cellpadding="4" width="100%" class="tftable">
        <tr>
            <td>--%>
    <table border="0" cellpadding="4" cellspacing="0" width="100%">
        <asp:Repeater ID="rptPKGDoccatName" runat="server" OnItemDataBound="rptPKGDoccatName_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td style="background-color: #7195A3" colspan="5">
                        <asp:Label ID="lblPKGDoccatName" runat="server" Text='<%#Eval("PKGDoccatName") %>'></asp:Label>
                        <asp:HiddenField ID="hdnCATpkgdoccatID" runat="server" Value='<%#Eval("CATpkgdoccatID") %>' />
                    </td>
                </tr>
                <asp:Repeater ID="rptDOCitem" runat="server" OnItemDataBound="rptDOCitem_ItemDataBound">
                    <ItemTemplate>
                        <tr class="gridRowStyle">
                            <td>
                                <asp:Label ID="lblDOCitem" runat="server" Text='<%#Eval("PKGDOCitemName") %>'></asp:Label>
                                <asp:HiddenField ID="hdnDOCitemID" runat="server" Value='<%#Eval("ITEMpkgdocitemID") %>' />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:HiddenField ID="hdnPKGdocItemID" runat="server" Value='<%#Eval("PKGdocItemID") %>' />
                                <a id="lnkViewPkgItem" href="#" runat="server">
                                    <img alt="View" src="../../IMAGE/edit.gif" border="0" width="20px" height="20px" /></a>
                            </td>
                            <td align="center" style="width: 10%" id="tdPrintButton" runat="server">
                                <a id="LnkprintApproved" href="#" runat="server" onclick="cmdUpdateField();">
                                    <img alt="View" src="../../IMAGE/printer.png" border="0" width="20px" height="20px" /></a>
                            </td>
                            <td align="center" style="width: 12%" id="tdApproverButton" visible="false" runat="server">
                                <a id="lnkApprovalTAsk" runat="server" href="#" class="iceLinkButton" style="text-decoration: none;
                                    color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 0px;
                                    height: 15px; margin-top: -0.5px; vertical-align: middle;">For Approval </a>
                                    <%--<input type="button" id ="lnkApprovalTAsk" runat="server" /> --%>
                            </td>
                            <td align="center" style="width: 12%" id="tdStatusButton" runat="server" visible="false" valign="middle">
                                <asp:CheckBox ID="IsRead" runat="server" Checked='<%#bool.Parse(Eval("Status").ToString())%>'
                                    OnCheckedChanged="IsRead_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lblDocID" runat="server" Visible="false" Text='<%#Eval("PKGdocItemID") %>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="gridAlternateRow">
                            <td>
                                <asp:Label ID="lblDOCitem" runat="server" Text='<%#Eval("PKGDOCitemName") %>'></asp:Label>
                                <asp:HiddenField ID="hdnDOCitemID" runat="server" Value='<%#Eval("ITEMpkgdocitemID") %>' />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:HiddenField ID="hdnPKGdocItemID" runat="server" Value='<%#Eval("PKGdocItemID") %>' />
                                <a id="lnkViewPkgItem" href="#" runat="server">
                                    <img alt="View" src="../../IMAGE/edit.gif" border="0" width="20px" height="20px" /></a>
                            </td>
                            <td align="center" style="width: 10%" id="tdPrintButton" runat="server">
                                <a id="LnkprintApproved" href="#" runat="server" onclick="cmdUpdateField();">
                                    <img alt="View" src="../../IMAGE/printer.png" border="0" width="20px" height="20px" /></a>
                            </td>
                            <td align="center" style="width: 12%" id="tdApproverButton" visible="false" runat="server">
                                <a id="lnkApprovalTAsk" runat="server" href="#" visible="false" class="iceLinkButton" style="text-decoration: none;
                                    color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 0px;
                                    height: 15px; margin-top: -0.5px; vertical-align: middle;">For Approval </a>
                                    <%--<input type="button" id ="lnkApprovalTAsk" runat="server" /> --%>
                            </td>
                            <td align="center" style="width: 12%" id="tdStatusButton" visible="false" runat="server">
                                <asp:CheckBox ID="IsRead" runat="server" Checked='<%#bool.Parse(Eval("Status").ToString())%>'
                                    OnCheckedChanged="IsRead_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lblDocID" runat="server" Visible="false" Text='<%#Eval("PKGdocItemID") %>'></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td>
                <table border="0" align="center" style="width: auto;">
                    <tr>
                        <td align="center">
                            <a id="lnkValuationPCI" runat="server" href="#" class="iceLinkButton" style="text-decoration: none;
                                color: White; font-family: Arial; font-size: 12px; width: 200px; font-weight: normal;
                                vertical-align: middle; border: 1px solid #dedede; padding-top: 3px; padding-bottom: 2px;
                                height: 20px; margin-top: -0.5px; vertical-align: middle;">For Package Review Approval</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
        <asp:Label ID="lblCompensationPackageStatus" runat="server" Style="text-decoration: blink;font-family: Arial; font-size: 18px; font-weight: bold" /></center>
    <script type="text/javascript" language="javascript">
        function OpenReport(documentCode, HHID) {
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('../COMPENSATION/SOCIOECONOMIC/CompensationPkgItem.aspx?documentCode=' + documentCode + '&HHID=' + HHID, 'winPkgItem', 'width=1040px,height=650px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenApprovalReport(documentCode, HHID, Mode, ApprovalLevel, PageCode) {
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('../COMPENSATION/SOCIOECONOMIC/CompensationPkgItem.aspx?documentCode=' + documentCode + '&HHID=' + HHID + '&Mode=' + Mode + '&ApprovalLevel=' + ApprovalLevel + '&PageCode=' + PageCode, 'winPkgItem', 'width=1040px,height=650px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenPrintReport(documentCode, HHID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            if (confirm('Are you sure you want to take a print of this report.')) {
                open('../COMPENSATION/PrintApproveReports.aspx?documentCode=' + documentCode + '&HHID=' + HHID, 'winPkgItem', 'width=800px,height=650px,scrollbars=1,top=' + top + ', left=' + left);
            }

        }
        //Newly Added on 21 June 2013
        function OpenApproverRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            //   alert('ChangeRequestCode:' + ChangeRequestCode + ';  ProjectID:' + ProjectID + ';  userID:' + userID + ';  HHID:' + HHID + ';  pageCode:' + pageCode);
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
