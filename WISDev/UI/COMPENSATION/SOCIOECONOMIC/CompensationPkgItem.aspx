<%@ Page Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="CompensationPkgItem.aspx.cs" Inherits="WIS.CompensationPkgItem" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" border="0" width="100%" bgcolor="#e8e8e8">
        <tr>
            <td colspan="4" align="right" style="padding-top: 12px">
                &nbsp;<%-- input type="button" id="btnClose" class="icebutton" value="Close" runat="server"  / --%>
                <asp:Button ID="btnClose" CssClass="icebutton" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            Width="861px" ToolPanelView="None" HasToggleGroupTreeButton="False" HasDrilldownTabs="False"
            HasDrillUpButton="False" EnableParameterPrompt="false" ReuseParameterValuesOnRefresh="true"
            HasExportButton="False" HasPrintButton="False" GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl=""
            ToolPanelWidth="200px" 
            ondrilldownsubreport="CrystalReportViewer1_DrillDownSubreport" />
        <%--        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            ToolPanelView="None" HasToggleGroupTreeButton="False" HasDrilldownTabs="False"
            HasDrillUpButton="False" EnableParameterPrompt="false" ReuseParameterValuesOnRefresh="true"
            GroupTreeImagesFolderUrl="" Height="800px" Width="900px" ToolbarImagesFolderUrl=""
            BestFitPage="False" />--%>
    </div>
    <asp:Panel ID="pnlApprovalComments" Visible="false" runat="server">
        <fieldset class="icePnlinner">
            <legend runat="server" id="pnlAPpCom"></legend>
            <table id="tblAppComments" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="ApprovalLabel" runat="server" Text="Review Comments" CssClass="iceLable" />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="ApprovalComments" runat="server" CssClass="iceTextAeralarge" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnApproval" runat="server" Text="Review" CssClass="icebutton" OnClick="btnApproval_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center"><br />
                        Note: Choose Report and click on GO Button to change report.
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblListofotherReports" runat="server" Text="Other Reports" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:DropDownList ID="PkgDocumentList" runat="server" CssClass="iceDropDown" Width="250px"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGoTonextReport" runat="server" Text="GO" CssClass="icebutton"
                            OnClick="btnGoTonextReport_Click" />
                    </td>
                </tr>
            </table>
            <table id="tblRAppComments" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="APPComt" runat="server" Text="Review Comments" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:Label ID="ApprovalRComments" runat="server" CssClass="iceTextBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="AppBy" runat="server" Text="Review By" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:Label ID="ApprovalRBy" runat="server" CssClass="iceTextBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="AppDate" runat="server" Text="Review Date" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:Label ID="ApprovalRDate" runat="server" CssClass="iceTextBox" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
   
</asp:Content>
