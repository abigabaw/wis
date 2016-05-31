<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="MonthlyProjectExpenditure.aspx.cs" Inherits="WIS.MonthlyProjectExpenditure" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
 <fieldset class="icePnlinner">
     <legend>Report Criteria</legend>
<table width="100%" align="center" border="0">
<tr>
 <td align="left">
        <label class="iceLable">
            Project Code</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProjectCode" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="200px"
                        runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Project Name</label>
                </td>
                <td align="left">
                      <asp:UpdatePanel ID="uplProjectName" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlProjectName" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="350px"
                                runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlProjectCode" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Month</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplMonth" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlMonth" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="200px"
                                runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlProjectName" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Year</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplYear" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlYear" CssClass="iceTextBox"  AppendDataBoundItems="true" AutoPostBack="true" Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
              <tr>
                <td colspan="4" align="center">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" /> 
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" 
                        runat="server" onclick="btnClear_Click" />
                </td>
            </tr>
              <%--<tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSearch" CssClass="icebutton" Text="View" runat="server" 
                        onclick="btnSearch_Click"/>
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" 
                        runat="server" onclick="btnClear_Click" />
                </td>
            </tr>--%>
            </table>       
             </fieldset>
             <script language="javascript" type="text/javascript">

                 function ddlProjectCode_IndexChanged(src) {
                     var fddlProjectCode = document.getElementById('<%=ddlProjectCode.ClientID%>');
                     var fddlProjectName = document.getElementById('<%=ddlProjectName.ClientID%>');
                     var PID = src.options[src.selectedIndex].value;
                     fddlProjectCode.options[src.selectedIndex].selected = true;
                     fddlProjectName.options[src.selectedIndex].selected = true;
                 }


                 function OpenReport() {
                     var fldProjectCode = document.getElementById('<%=ddlProjectCode.ClientID%>');
                     var fldProjectName = document.getElementById('<%=ddlProjectName.ClientID%>');
                     var fldMonth = document.getElementById('<%=ddlMonth.ClientID%>');
                     var fldYear = document.getElementById('<%=ddlYear.ClientID%>');

                     var ProjectCode;
                     var ProjectName;
                     var Month;
                     var Year;

                     if (fldProjectCode.selectedIndex > 0)
                         ProjectCode = fldProjectCode.options[fldProjectCode.selectedIndex].text;
                     else
                         ProjectCode = '';

                     if (fldProjectName.selectedIndex > 0)
                         ProjectName = fldProjectName.options[fldProjectName.selectedIndex].text;
                     else
                         ProjectName = '';

                     if (fldMonth.selectedIndex > 0)
                         Month = fldMonth.options[fldMonth.selectedIndex].text
                     else
                         Month = '';

                     if (fldYear.selectedIndex > 0)
                         Year = fldYear.options[fldYear.selectedIndex].text;
                     else
                         Year = '';

                     var left = (screen.width - 960) / 2;
                     var top = (screen.height - 650) / 4;

                     var param = 'rptCode=MPS' +
                        '&ProjectCode=' + ProjectCode +
                        '&ProjectName=' + ProjectName +
                        '&Month=' + Month +
                        '&Year=' + Year;

                     open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                 }
             </script>
               &nbsp;
                <div>
                    <cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" 
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
              </div>
</asp:Content>


