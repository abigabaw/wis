<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="ProjectUnfreeze.aspx.cs" Inherits="WIS.ProjectUnfreeze" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="toolkitManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Unfreeze Details</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="right">
                        <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Enter the reason for Unfreeze and upload supporting autorization</label><span class="mandatory">*</span></div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:TextBox ID="txtUnfreezeComments" runat="server" CssClass="iceTextBox" Width="98%"
                            Rows="5" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="rfvUnfreezeComments" ControlToValidate="txtUnfreezeComments"
                            ErrorMessage="Enter Comments" Display="None" ValidationGroup="Unfreeze" runat="server"> </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="color: Red;">
                            (Max 500 characters)</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" align="center">
                        <div style="margin-top: 12px;">
                            <asp:Button ID="btnUnfreeze" CssClass="icebutton" Text="Unfreeze" runat="server"
                                ValidationGroup="Unfreeze" OnClick="btnUnfreeze_Click" />&nbsp;
                            <asp:Button ID="btnCancel" CssClass="icebutton" Text="Cancel" OnClientClick="Aftersend();"
                                runat="server" />
                            <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Unfreeze" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <script type="text/javascript" language="javascript">
        function Aftersend() {
            window.opener.location.replace(window.opener.location.pathname)
            //          window.opener.location.reload();
            window.close();
        }

        function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPop', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }

        function commentsValidation() {

            var txtComents = document.getElementById('<%=txtUnfreezeComments.ClientID%>').value;
            var rfvComents = document.getElementById('<%=rfvUnfreezeComments.ClientID%>');
            if (txtComents.length == 0) {
                rfvComents.enabled = true;
                alert('Enter Comments');
                return false;
            }
            else
                return true;
        }
        function DisableOnSave(src, Vgroup) {
            if (Page_ClientValidate(Vgroup)) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        }
    </script>
</asp:Content>
