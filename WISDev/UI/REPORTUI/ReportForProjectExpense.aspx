<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="ReportForProjectExpense.aspx.cs" Inherits="WIS.ReportForProjectExpense1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Select Criteria</legend>
        <table width="100%" align="center" border="0">
          <tr>
            <td>
                    <label class="iceLable">
                       Project Name
                    </label><span class="mandatory">*</span>
                   </td>
                   <td>
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" width="250px" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                   </td>
                <td>
                    <label class="iceLable">
                        Account Code
                    </label><span class="mandatory">*</span>
                </td>
                <td>
                    <asp:TextBox ID="AccountcodeTextBox" runat="server" CssClass="iceTextBox"></asp:TextBox>
                   </td>
                 
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        From Date
                    </label>
                </td>
                <td>
                <asp:TextBox ID="DPFromDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="calDPFromDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="DPFromDate"></ajaxToolkit:CalendarExtender>
                 
                </td>
                <td>
                    <label class="iceLable">
                        To Date
                    </label>
                </td>
                <td>
                                <asp:TextBox ID="DPToDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="CalDPToDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="DPToDate"></ajaxToolkit:CalendarExtender>
                  
                </td>
            </tr>
          
            <tr>
                <td colspan="4" align="center">
                    <%--<asp:Button ID="Button1" runat="server" Text="View" CssClass="icebutton" 
                        onclick="Button1_Click"/>--%>
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
 
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_DPFromDate'));
        PreventDateFieldEntry(document.getElementById('MainContent_DPToDate'));
        function OpenReport() {
          
            ProjectFromdate = GetCalDate('<%=DPFromDate.ClientID%>');
            ProjectTodate = GetCalDate('<%=DPToDate.ClientID%>');
            var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
            Accountcode = document.getElementById('<%=AccountcodeTextBox.ClientID%>');

            //var Accountcode;
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            var projectID;
            var accontcode;

            if (fldprojectID.selectedIndex > 0) {
                projectID = fldprojectID.options[fldprojectID.selectedIndex].value;
            }
            else {
                alert('Please select a Project Name');
                return;
            }
            if (Accountcode.value != '') {
                accontcode = Accountcode.value;
            }
            else {
                alert('Please enter Account Code');
                return;
            }
            var param = 'rptCode=PROJEXP' +
         
                         '&ProjectFromdate=' + ProjectFromdate +
                         '&ProjectTodate=' + ProjectTodate +
                           '&accontcode=' + accontcode +
                           '&ProjectID=' + projectID;
          
            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,scrollbars=1,resizable=1,top=' + top + ', left=' + left);
        }
    </script>
      
</asp:Content>
