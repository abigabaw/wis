<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PAPStatus_Chart.aspx.cs" Inherits="WIS.PAPStatus_Chart" %>

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
            <legend>PAP Status </legend>
            <table border="0" width="100%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Change Project :
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" Width="250px" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlProject" runat="server" ControlToValidate="ddlProject"
                            ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Project to search Paps"
                            Display="None">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Chart Type :
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlcharttype" CssClass="iceTextBox" Width="250px" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="PCHART">Projectwise PAP Status</asp:ListItem>
                            <asp:ListItem Value="LCHART">Budgeted v/s Actual Expense</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlcharttype" runat="server" ControlToValidate="ddlcharttype"
                            ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select Type of the Chart"
                            Display="None">
                        </asp:RequiredFieldValidator>
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

            var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
            var ddlcharttype = document.getElementById('<%=ddlcharttype.ClientID%>');

            var projectID;
            var rptCode;
            if (fldprojectID.selectedIndex > 0) {
                projectID = fldprojectID.options[fldprojectID.selectedIndex].value;
            }
            else {
                alert('Please select a Project Name');
                return;
            }
            startdate = GetCalDate('<%=opsStartDate.ClientID%>');
            Enddate = GetCalDate('<%=opsEndDate.ClientID%>');
            if (ddlcharttype.selectedIndex > 0) {
                rptCode = ddlcharttype.options[ddlcharttype.selectedIndex].value;
            }
            else {
                alert('Please Select Type of the Chart.');
                return;
            }


            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=' + rptCode +
                  '&ProjectID=' + projectID +
                  '&opsStartDate=' + startdate +
            '&opsEndDate=' + Enddate;
            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);

        }
    </script>
</asp:Content>
