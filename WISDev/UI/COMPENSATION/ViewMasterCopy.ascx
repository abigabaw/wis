<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewMasterCopy.ascx.cs" Inherits="WIS.ViewMasterCopy" %>
<table align="center" border="0" cellpadding="2">
    <tr>
        <td>
            <asp:HiddenField ID="hfReportType" runat="server" />
            <asp:LinkButton ID="lnkMasterCopy" runat="server" BackColor="LightSkyBlue"
                Width="160px" Height="20px" style="text-align: center;">View Master Copy</asp:LinkButton>
        </td>
    </tr>
</table>
    <script type="text/javascript">
        function OpenReport(ReportType) {
//            var ddlReportType = document.getElementById('<%=hfReportType.ClientID%>');

//            var ReportType;

//            ReportType = ddlReportType.value;

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            //            var param = 'rptCode=' + ReportType;
            var param = 'rptCode=CMPDT' +
                        '&ReportType=' + ReportType +
                        '&ViewMaster=Y';

            open('../../REPORTUI/RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
