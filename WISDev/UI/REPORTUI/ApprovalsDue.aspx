<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="ApprovalsDue.aspx.cs" Inherits="WIS.ApprovalsDue" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <fieldset class="icePnlinner">
            <legend>Approvals Due</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Approver</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="iceDropDown" Style="width: 190px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Project Name</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="drpProject" runat="server" CssClass="iceDropDown" Style="width: 250px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            From Date</label>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="dpApprStartDate" runat="server" Width="90px"></asp:TextBox>
                   <ajaxToolkit:CalendarExtender ID="caldpApprStartDate" runat="server" CssClass="WISCalendarStyle"  TargetControlID="dpApprStartDate"></ajaxToolkit:CalendarExtender>
                     
                    </td>
                    <td>
                        <label class="iceLable">
                            To Date</label>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="dpApprEndDate" runat="server" Width="90px"></asp:TextBox>
                   <ajaxToolkit:CalendarExtender ID="CaldpApprEndDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpApprEndDate"></ajaxToolkit:CalendarExtender>
                     
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <input type="button" class="icebutton" value="View" onclick="OpenReport()" />&nbsp;
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearSearch_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <script type="text/javascript">
            PreventDateFieldEntry(document.getElementById('MainContent_dpApprStartDate'));
            PreventDateFieldEntry(document.getElementById('MainContent_dpApprEndDate'));

            function OpenReport() {

                var aproverID;
                var Projectid;
                var AproverName;
                var ProjectName;
                var fldaprover = document.getElementById('<%=ddlAssignTo.ClientID%>');
                var fldProject = document.getElementById('<%=drpProject.ClientID%>');

                if (fldaprover.selectedIndex > 0) {
                    aproverID = fldaprover.options[fldaprover.selectedIndex].value;
                    AproverName = fldaprover.options[fldaprover.selectedIndex].text;
                }
                else {
                    aproverID = '';
                    AproverName = '';
                }

                if (fldProject.selectedIndex > 0) {
                    Projectid = fldProject.options[fldProject.selectedIndex].value;
                    ProjectName = fldProject.options[fldProject.selectedIndex].text;
                }
                else {
                    Projectid = '';
                    ProjectName = '';
                }

                Apprstartdate = GetCalDate('<%=dpApprStartDate.ClientID%>');
                ApprEnddate = GetCalDate('<%=dpApprEndDate.ClientID%>');

                var left = (screen.width - 960) / 2;
                var top = (screen.height - 650) / 4;

                var param = 'rptCode=APPR' +
                        '&ReportType=DUE' +
                        '&Projectid=' + Projectid +
                        '&aproverID=' + aproverID +
                        '&Apprstartdate=' + Apprstartdate +
                        '&ApprEnddate=' + ApprEnddate +
                        '&AproverName=' + AproverName +
                        '&ProjectName=' + ProjectName;

                open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
        </script>
    </div>
</asp:Content>
