<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="GrievanceClosure.aspx.cs" Inherits="WIS.GrievanceClosure" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        function GrivanceClosure() {
            alert('Grievance closed successfully');
            window.opener.location.replace(window.opener.location.pathname);
            window.close();
        }
    </script>
    <fieldset class="icePnlinner">
        <legend>Grievance Closure</legend>
        <table align="center" border="0" cellpadding="1" cellspacing="1" style="margin-top: 10px;
            width: 100%">
            <tr>
                <td class="iceNormalText">
                    <div style="float: right">
                        <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a> &nbsp;|&nbsp;
                        <a id="lnkUPloadDoclist" href="#" runat="server"><b>View Document</b></a>
                    </div>
                    <script type="text/javascript" language="javascript">
                        function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                            var left = (screen.width - 800) / 2;
                            var top = (screen.height - 650) / 4;
                            open('../../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPop', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                        }

                        function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                            var left = (screen.width - 800) / 2;
                            var top = (screen.height - 650) / 4;
                            open('../../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                        }                  
                    </script>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="3" width="100%">
            <tr>
                <td align="left" width="12%">
                    <asp:Label ID="lblCategory" runat="server" class="iceLable">Category:</asp:Label>
                </td>
                <td align="left" width="40%">
                    <asp:Label ID="lblCategoryAssign" runat="server" class="iceLable">
                    </asp:Label>
                </td>
                <td align="left" width="14%">
                    <asp:Label ID="lblCreatedDate" runat="server" class="iceLable">
                            Created Date:</asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblCreatedDateAssign" runat="server" class="iceLable">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Description</label><span class="mandatory">*</span> 
                </td>
                <td align="left" style="vertical-align: top" colspan="3">
                    <asp:TextBox ID="txtClosureComments" CssClass="iceTextBox" runat="server" TextMode="MultiLine"
                        Width="98%" Rows="10" MaxLength="480"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtClosureComments"
                        ErrorMessage="Enter Comments" Display="None" ValidationGroup="VGGrievance" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnClose" CssClass="icebutton" Text="Close Grievance" runat="server"
                        ValidationGroup="VGGrievance" OnClick="btnClose_Click" Width="120px" />
                    <input type="button" id="btnCloseWindow" value="Close" class="icebutton" onclick="window.close();" />
                    <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="VGGrievance" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
