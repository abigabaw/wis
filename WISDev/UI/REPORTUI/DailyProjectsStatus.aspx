<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="DailyProjectsStatus.aspx.cs" Inherits="WIS.DailyProjectsStatus" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
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
                        Project Name
                    </label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" AppendDataBoundItems="true"
                        AutoPostBack="True" Width="250px" runat="server" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td align="left">
        <label class="iceLable">
            District</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="250px"
                        runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
<tr>

                <td align="left">
                    <label class="iceLable">
                        County</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
                                runat="server"  OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                  <td align="left">
                    <label class="iceLable">
                        Sub County</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSubCounty" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
                                runat="server" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
              
                <td align="left">
                    <label class="iceLable">
                        Parish</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlParish" CssClass="iceTextBox" Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                 <td align="left">
                    <label class="iceLable">
                        Village</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlVillage" CssClass="iceTextBox" Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
               
                  <td align="left">
                    <label class="iceLable">
                        From Date</label>
                </td>
                <td align="left">
                <asp:TextBox ID="dpProjStartDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpProjStartDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjStartDate"></ajaxToolkit:CalendarExtender>
                 
                </td>
                  <td>
                    <label class="iceLable">
                       To Date</label>
                </td>
                <td align="left">
                <asp:TextBox ID="dpProjEndDate" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpProjEndDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpProjEndDate"></ajaxToolkit:CalendarExtender>
               
                </td>
               
                <td align="left">
                   
                </td>
                <td align="left">
                   
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
                    <asp:Button ID="btnSearch" CssClass="icebutton" Text="View" runat="server" onclick="btnSearch_Click"  
                        />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" 
                        runat="server" onclick="btnClear_Click"/>
                </td>
            </tr>--%>
            <%-- <asp:HiddenField ID="hdnProjectID" ClientIDMode="Static" runat="server" />--%>
            </table>
             </fieldset>
             <script language="javascript" type="text/javascript">
                 PreventDateFieldEntry(document.getElementById('MainContent_dpProjStartDate'));
                 PreventDateFieldEntry(document.getElementById('MainContent_dpProjEndDate'));

                 function OpenReport() {
                     var fldDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
                     var fldCounty = document.getElementById('<%=ddlCounty.ClientID%>');
                     var fldSubCounty = document.getElementById('<%=ddlSubCounty.ClientID%>');
                     var fldParish = document.getElementById('<%=ddlParish.ClientID%>');
                     var fldVillage = document.getElementById('<%=ddlVillage.ClientID%>');
                     var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
                     Projectstartdate = GetCalDate('<%=dpProjStartDate.ClientID%>');
                     ProjectEnddate = GetCalDate('<%=dpProjEndDate.ClientID%>');

                     var district;
                     var county;
                     var subCounty;
                     var parish;
                     var village;
                     var projectID;


                     if (fldprojectID.selectedIndex > 0) {
                         projectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                     }
                     else {
                         alert('Please select a Project Name');
                         return;
                     }

                     if (fldDistrict.selectedIndex > 0)
                         district = fldDistrict.options[fldDistrict.selectedIndex].text;
                     else
                         district = '';

                     if (fldCounty.selectedIndex > 0)
                         county = fldCounty.options[fldCounty.selectedIndex].text;
                     else
                         county = '';

                     if (fldSubCounty.selectedIndex > 0)
                         subCounty = fldSubCounty.options[fldSubCounty.selectedIndex].text
                     else
                         subCounty = '';

                     if (fldParish.selectedIndex > 0)
                         parish = fldParish.options[fldParish.selectedIndex].text;
                     else
                         parish = '';

                     if (fldVillage.selectedIndex > 0)
                         village = fldVillage.options[fldVillage.selectedIndex].text;
                     else
                         village = '';

                     var left = (screen.width - 960) / 2;
                     var top = (screen.height - 650) / 4;

                     var param = 'rptCode=DPS' +
                        '&district=' + district +
                        '&county=' + county +
                        '&subCounty=' + subCounty +
                        '&parish=' + parish +
                        '&village=' + village +
                         '&ProjectID=' + projectID +
                         '&Projectstartdate=' + Projectstartdate +
                        '&ProjectEnddate=' + ProjectEnddate ;

                     open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                 }
             </script>
             &nbsp;
               <div>
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
            </div>
</asp:Content>

