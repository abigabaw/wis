<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Project.aspx.cs" Inherits="WIS.UI.PROJECT.Project" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Search Projects</legend>
        <table border="0" width="100%">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Project Name</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProjectName" runat="server" CssClass="iceTextBox" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Start Date</label>
                </td>
                <td align="left">
                <asp:TextBox ID="dpProjStartDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpProjStartDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjStartDate"></ajaxToolkit:CalendarExtender>
                 
                </td>
                <td>
                    <label class="iceLable">
                        End Date</label>
                </td>
                <td align="left">
                <asp:TextBox ID="dpProjEndDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpProjEndDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjEndDate"></ajaxToolkit:CalendarExtender>
               
                </td>
                <td align="left">
                    <label class="iceLable">
                        Status</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProjectStatus" runat="server" CssClass="iceDropDown">
                        <asp:ListItem Value="">--All--</asp:ListItem>
                        <asp:ListItem Value="New">New</asp:ListItem>
                        <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8" align="center">
                    <div style="margin-top: 12px;">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="View" runat="server" />&nbsp;
                        <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <script type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_dpProjStartDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_dpProjEndDate'));
    </script>
</asp:Content>
