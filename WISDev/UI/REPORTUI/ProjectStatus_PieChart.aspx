<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectStatus_PieChart.aspx.cs" Inherits="WIS.ProjectStatus_PieChart" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <fieldset class="icePnlinner">
            <legend>Project Status </legend>
            <table border="0" width="100%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Project Status
                        </label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlProjectStatus" Width="250px" ClientIDMode="Static" runat="server"
                            CssClass="iceDropDown">
                            <asp:ListItem Value="">--All--</asp:ListItem>
                            <asp:ListItem Value="New">New</asp:ListItem>
                            <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px;">
                        <label class="iceLable">
                            From Date</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="opsStartDate" runat="server" Width="90px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calopsStartDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="opsStartDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td style="width: 100px;">
                        <label class="iceLable">
                            To Date</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="opsEndDate" runat="server" Width="90px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalopsEndDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="opsEndDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                            <%-- <asp:Button ID="btnView" CssClass="icebutton" Text="View" runat="server" onclick="btnView_Click"
                                 />--%>&nbsp;
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearSearch_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_opsStartDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_opsEndDate'));
        function OpenReport() {
            var fldProjectstatus = document.getElementById('<%=ddlProjectStatus.ClientID%>');

            var Projectstatus;

            if (fldProjectstatus.selectedIndex > 0)
                Projectstatus = fldProjectstatus.options[fldProjectstatus.selectedIndex].text;
            else
                Projectstatus = '';

            startdate = GetCalDate('<%=opsStartDate.ClientID%>');
            Enddate = GetCalDate('<%=opsEndDate.ClientID%>');
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=PIECHART' +
              '&Projectstatus=' + Projectstatus +
                  '&opsStartDate=' + startdate +
            '&opsEndDate=' + Enddate;
            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
     
    </script>
</asp:Content>
