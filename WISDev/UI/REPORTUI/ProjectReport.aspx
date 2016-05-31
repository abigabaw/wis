<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
CodeBehind="ProjectReport.aspx.cs" Inherits="WIS.ProjectReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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
            <legend>Search Projects</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="left">
                        <label class="iceLable">Project Name</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtProjectName" ClientIDMode="Static" runat="server" CssClass="iceTextBox" />
                    </td>
                    <td align="left">
                        <label class="iceLable">From Date</label>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="dpProjStartDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpProjStartDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjStartDate"></ajaxToolkit:CalendarExtender>

                    </td>
                    <td>
                        <label class="iceLable">To Date</label>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="dpProjEndDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="CaldpProjEndDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjEndDate"></ajaxToolkit:CalendarExtender>

                    
                    </td>
                  
                </tr>
                <tr>
                <td align="left">
                        <label class="iceLable">Project Code</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="projectcodeTextBox" ClientIDMode="Static" runat="server" CssClass="iceTextBox" />
                    </td>
                      <td align="left">
                        <label class="iceLable">Consultant Name</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlconsultantname" CssClass="iceTextBox" AppendDataBoundItems="true"
                            Width="205px" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                      <td align="left">
                        <label class="iceLable">Status</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProjectStatus" ClientIDMode="Static" runat="server" CssClass="iceDropDown">
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
                          <input type="button" class="icebutton" value="View" onclick="OpenReport()" /> 
                           <%-- <asp:Button ID="btnView" CssClass="icebutton" Text="View" runat="server" onclick="btnView_Click"
                                 />--%>&nbsp;
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" 
                                runat="server" onclick="btnClearSearch_Click"
                                 />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
         <script language="javascript">
             PreventDateFieldEntry(document.getElementById('MainContent_dpProjStartDate'));
             PreventDateFieldEntry(document.getElementById('MainContent_dpProjEndDate'));
             function OpenReport() {
                  
               var fldProjectstatus = document.getElementById('<%=ddlProjectStatus.ClientID%>');
               var fldconsultname = document.getElementById('<%=ddlconsultantname.ClientID%>');
   
               var ProjectID;
                 var ProjectName;
                 var Projectstatus;
                 var Projectcode;
                 var consultname;

                 if (fldProjectstatus.selectedIndex > 0)
                     ProjectID = fldProjectstatus.options[fldProjectstatus.selectedIndex].value;
                 else
                     ProjectID = 0;

                 if (fldProjectstatus.selectedIndex > 0)
                     Projectstatus = fldProjectstatus.options[fldProjectstatus.selectedIndex].text;
                 else
                     Projectstatus = '';

                 if (fldconsultname.selectedIndex > 0)
                     consultname = fldconsultname.options[fldconsultname.selectedIndex].text;
                 else
                     consultname = '';

                 ProjectName = document.getElementById('<%=txtProjectName.ClientID%>').value;
                 Projectstartdate = GetCalDate('<%=dpProjStartDate.ClientID%>');
                 ProjectEnddate = GetCalDate('<%=dpProjEndDate.ClientID%>');
                 Projectcode = document.getElementById('<%=projectcodeTextBox.ClientID%>').value;
               

                 var left = (screen.width - 960) / 2;
                 var top = (screen.height - 650) / 4;
           

                 var param = 'rptCode=PR' +
                        '&ProjectName=' + ProjectName +
                        '&Projectstartdate=' + Projectstartdate +
                        '&ProjectEnddate=' + ProjectEnddate +
                        '&Projectstatus=' + Projectstatus +
                        '&Projectcode=' + Projectcode +
                        '&consultname=' + consultname;

                 open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
             }
             </script>
</div>
  &nbsp;
            <%--<div>
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
                    ToolPanelWidth="200px" />
            </div>--%>
</asp:Content>
