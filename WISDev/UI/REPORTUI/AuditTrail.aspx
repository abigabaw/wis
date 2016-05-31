<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="AuditTrail.aspx.cs" Inherits="WIS.AuditTrail" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
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
        <td align="left">
                  <%--  <label class="iceLable">
                        Project Name
                    </label>--%>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" AppendDataBoundItems="true" Visible = "false" AutoPostBack="true"
                     runat="server" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Style="width: 250px" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
        
          <%--       <td>
                    <label class="iceLable">
                        Action By
                    </label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActionBy" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true" AutoPostBack="true"
                        Style="width: 250px" >
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
               
                </tr>
                <tr>
                 <td>
                    <label class="iceLable">
                        Action By
                    </label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActionBy" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true" AutoPostBack="true"
                        Style="width: 250px" >
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>
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

//            var Action = document.getElementById('<%=ddlActionBy.ClientID%>');
//            if (Action.selectedIndex > 0)
//                ActionBy = Action.options[Action.selectedIndex].value;
//            else
            //                ActionBy = '';
            var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
            ProjectFromdate = GetCalDate('<%=DPFromDate.ClientID%>');
            ProjectTodate = GetCalDate('<%=DPToDate.ClientID%>');
           
            var ActionBy = document.getElementById('<%=ddlActionBy.ClientID%>');
           
            var projectID;
            var ActionByID;
            var ActionName;

//            if (fldprojectID.selectedIndex > 0) {
//                projectID = fldprojectID.options[fldprojectID.selectedIndex].value;
//            }
//            else {
//                alert('Please select a Project Name');
//                return;
//            }
            projectID = 0;
            if (ActionBy.selectedIndex > 0) {
                ActionByID = ActionBy.options[ActionBy.selectedIndex].value;

            }
            else {
                ActionByID = 0;
            }

            if (ActionBy.selectedIndex > 0) {
                ActionName = ActionBy.options[ActionBy.selectedIndex].text;

            }
            else {
                ActionName = '';
            }
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=AUDT' +                    
                         '&ProjectFromdate=' + ProjectFromdate +
                         '&ProjectTodate=' + ProjectTodate +
                         '&ProjectID=' + projectID +
                         '&ActionByID=' + ActionByID +
                         '&ActionName=' + ActionName ;

            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,scrollbars=1,resizable=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
