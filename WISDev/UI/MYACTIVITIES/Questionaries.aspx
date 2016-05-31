<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="Questionaries.aspx.cs" Inherits="WIS.Questionaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Questionnaire Report</legend>
        <table width="40%" align="center" border="0" style="height: 52px">
            <tr>
                <td align="left" width="40%">
                    <label class="iceLable">
                        Questionnaire Type
                    </label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReportType" CssClass="iceTextBox" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="QESTSOCIO">Socio Economic</asp:ListItem>
                        <asp:ListItem Value="QESTSUR">Survey</asp:ListItem>
                        <asp:ListItem Value="QESTVALN">Valuation</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" style="padding-top:12px">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script language="javascript" type="text/javascript">
        function OpenReport() {
            var fldReportType = document.getElementById('<%=ddlReportType.ClientID%>');
            var reportType;

            if (fldReportType.selectedIndex > 0) {
                reportType = fldReportType.options[fldReportType.selectedIndex].value;
            }
            else {
                alert('Please select a  Questionnaire Type');
                return;
            }

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=' + reportType;

            open('../REPORTUI/RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
