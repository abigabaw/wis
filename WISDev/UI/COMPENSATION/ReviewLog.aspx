<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="ReviewLog.aspx.cs" Inherits="WIS.ReviewLog" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="icePnlinner">
        <legend id="lgnd" runat="server">Review Log Details</legend>
        <%-- <asp:Label ID="lblBatchNo" runat="server"></asp:Label>--%>
        <asp:Repeater ID="rptrSenderDetails" runat="server" OnItemDataBound="rptrSenderDetails_RowDataBound">
            <HeaderTemplate>
                <table border="0" cellspacing="1" width="100%" class="tftable">
                    <tr>
                        <th style="width: 15%;" class="gridHeaderStyle">
                            Reviewed By
                        </th>
                        <th style="width: 15%;" class="gridHeaderStyle">
                            Reviewed on
                        </th>
                        <th style="width: 15%;" class="gridHeaderStyle">
                            Document
                        </th>
                        <th class="gridHeaderStyle">
                            Comments
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <%--  <tr>
                    <td colspan = "4">
                       Conversation Log For :  <asp:Label ID="lblWorkFlowDescription" runat="server" Text= '<%#Eval("WorkFlowDescription")%>'></asp:Label>
                    </td>
                    </tr>--%>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Label ID="lblRequesterName" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                    </td>
                    <td align="center" style="width: 15; background-color: White; vertical-align: top;
                        text-align: left;">
                        <asp:Label ID="lblRequestDateTime" runat="server" Text=' <%#Eval("ApprovedDate")%>'></asp:Label>
                    </td>
                    <td align="center" style="width: 70; background-color: White; vertical-align: top;">
                        <asp:Label ID="lblStatus" runat="server" Text=' <%#Eval("PKGDoccatName")%>'></asp:Label>
                    </td>
                    <td style="background-color: White">
                        <asp:Label ID="lbleMailBody" runat="server" Text=' <%#Eval("ApprovalComents")%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <center>
            <input type="button" id="btnClose" value="Close" class="icebutton" onclick="window.close();" />
            <asp:Label ID="lblMessage" Text="" Visible="false" runat="server"></asp:Label>
        </center>
        <asp:Panel ID="pnlBatchPAPS" Visible="false" runat="server">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:HiddenField ID="hdnPendingRequestCount" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ValidationSummary ID="valSummary" HeaderText="Please correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgCompResettle" runat="server" />
    </fieldset>
    <%--  <script type="text/javascript">
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>--%>
</asp:Content>
