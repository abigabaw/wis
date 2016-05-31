<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  UICulture="en" Culture="en-US"
    CodeBehind="ACCEPTANCECOUNT.aspx.cs" Inherits="WIS.ACCEPTANCECOUNT" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <fieldset class="icePnlinner">
     <legend>Report Criteria</legend>
<table width="100%" align="center" border="0">
<tr>
<td >
                <asp:RadioButton ID="RadioButtonSummary" runat="server" GroupName = "Radiobtn"  ClientIDMode="Static" Text = "Summary" />

</td>

<td >
                <asp:RadioButton ID="RadioButtonDetail" runat="server" GroupName = "Radiobtn" ClientIDMode="Static" Text = "Detail"/>

</td>
 <td align="left" >
        <label class="iceLable">
            Project Name </label>
                </td>
                <td align="left" class="style1">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" 
                        AppendDataBoundItems="true" AutoPostBack="True" Width="205px"
                        runat="server" onselectedindexchanged="ddlProject_SelectedIndexChanged">
                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
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
                 function OpenReport() {
                     var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');
                     var RadioButtonDetail = document.getElementById('<%=RadioButtonDetail.ClientID%>');
                     FromDate = GetCalDate('<%=DPFromDate.ClientID%>');
                     ToDate = GetCalDate('<%=DPToDate.ClientID%>');

                     var ProjectID;
                     //var Detail;

                     if (fldprojectID.selectedIndex > 0)
                         ProjectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                     else
                         ProjectID = 0;

                     if (RadioButtonDetail.checked) {
                         if (fldprojectID.selectedIndex == 0) {
                             alert("Select the Project Name");
                         } else if (fldprojectID.selectedIndex > 0) {
                             ProjectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                             rptCode = 'Detail';
                         }
                        
                     }
                     else {
                         rptCode = 'ACTCOUNT';
                     }
                     var left = (screen.width - 960) / 2;
                     var top = (screen.height - 650) / 4;

                     var param = 'rptCode=' + rptCode +
                        '&ProjectID=' + ProjectID +
                        '&FromDate=' + FromDate +
                        '&ToDate=' + ToDate ;

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
