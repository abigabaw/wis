<%@ Page Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="PrintApproveReports.aspx.cs" Inherits="WIS.PrintApproveReports" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function AfterstatusChange() {
            if (opener) {
                window.opener.location.reload();

            }
        }

        function AfterPrint() {
            alert('Document printed successfully.');
            window.close();
        }
    </script>
    <div>
        <td>
            <%--input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" runat=server/ --%>
            <asp:Button ID="btnClose" CssClass="icebutton" runat="server" Text="Close" OnClick="btnClose_Click" />
        </td>
    </div>
    <br />
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
            AutoDataBind="True"
            Width="861px"
            ToolPanelView="None"
            HasToggleGroupTreeButton="False"
            HasDrilldownTabs="False"
            HasDrillUpButton="False"
            EnableParameterPrompt="false"
            ReuseParameterValuesOnRefresh="true"
            GroupTreeImagesFolderUrl="" Height="1158px"
            ToolbarImagesFolderUrl=""
            ToolPanelWidth="200px"
            OnDrillDownSubreport="CrystalReportViewer1_DrillDownSubreport" />

    </div>

    <asp:Panel ID="PnlPrintResion" Visible="false" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPrintcomments" runat="server" Text="Print Comments" CssClass="iceLable" />&nbsp; <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:TextBox ID="TxtPrintcomments" runat="server" CssClass="iceTextAeralarge" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="rfvUnfreezeComments" ControlToValidate="TxtPrintcomments"
                        ErrorMessage="Enter Comments" Display="None" ValidationGroup="Unfreeze" runat="server"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnprintresion" runat="server" ValidationGroup="Unfreeze" Text="Print Reason" CssClass="icebutton"
                        OnClick="btnprintresion_Click" />
                    <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Unfreeze" runat="server" />
                </td>
                <td>
                    <input type="button" id="btnSaveClose" class="icebutton" value="Close" onclick="window.close();" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
