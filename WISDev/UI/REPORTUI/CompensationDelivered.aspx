<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompensationDelivered.aspx.cs" Inherits="WIS.CompensationDelivered" %>

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
            Project Name </label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" 
                        AppendDataBoundItems="true" AutoPostBack="True" Width="205px"
                        runat="server">
                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                </tr>
            <tr>
                <td colspan="4" align="center">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" /> 
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" 
                        runat="server" onclick="btnClear_Click" />
                </td>
            </tr>
             
            </table>
             </fieldset>
             <script language="javascript" type="text/javascript">
                 function OpenReport() {
                     var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');

                     var ProjectID;

                     if (fldprojectID.selectedIndex > 0)
                         ProjectID = fldprojectID.options[fldprojectID.selectedIndex].value;
                     else
                         ProjectID = 0;

                     var left = (screen.width - 960) / 2;
                     var top = (screen.height - 650) / 4;

                     var param = 'rptCode=CCD' +
                        '&ProjectID=' + ProjectID;

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
