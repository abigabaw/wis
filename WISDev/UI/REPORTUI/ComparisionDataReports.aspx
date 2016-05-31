<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="ComparisionDataReports.aspx.cs" Inherits="WIS.ComparisionDataReports" %>
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
                <td align="right" style="width: 30%">
                    <label class="iceLable">
                        Report Type</label>
                </td>
                <td align="left" style="width: 70%">
                    <asp:DropDownList ID="ddlReportType" ClientIDMode="Static" CssClass="iceTextBox"
                        Width="350px" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">-------- Socio-Economic ------</asp:ListItem>
                        <asp:ListItem Value="CMPHOU">Household Details</asp:ListItem>
                        <asp:ListItem Value="CMPINST">Institution Details</asp:ListItem>
                        <asp:ListItem Value="CMPGRP">Group Ownership Details</asp:ListItem>
                        <asp:ListItem Value="CMPGRPM">Group Members Details</asp:ListItem>
                        <asp:ListItem Value="CMPSER">Services on Affected Plot</asp:ListItem>
                        <asp:ListItem Value="CMPHHT">Householder Details</asp:ListItem>
                        <asp:ListItem Value="CMPALU">Affected Land users on the Affected Plot of Land</asp:ListItem>
                        <asp:ListItem Value="CMPSTA">Stakeholder Details</asp:ListItem>
                        <asp:ListItem Value="CMPLHD">Livelihood Details</asp:ListItem>
                        <asp:ListItem Value="CMPDDT">Disability Details</asp:ListItem>
                        <asp:ListItem Value="CMPHCD">Health Care Details</asp:ListItem>
                        <asp:ListItem Value="CMPNEI">Neighbour Details</asp:ListItem>
                        <asp:ListItem Value="CMPSHOCK">Shock Details</asp:ListItem>
                        <asp:ListItem Value="CMPINDW">General Welfare Indicators from Government's Survey</asp:ListItem>
                        <asp:ListItem Value="CMPWELF">Welfare Details</asp:ListItem>
                        <asp:ListItem Value="CMPCON">Concern Details</asp:ListItem>
                        <asp:ListItem Value="CMPOTHLAND">Other Land Holdings</asp:ListItem>
                        <asp:ListItem Value="CMPMEMCLA">Member Claims and Easements</asp:ListItem>
                        <asp:ListItem Value="CMPLOAL">Living on Affected Land</asp:ListItem>
                        <asp:ListItem Value="CMPLOFF">Living off Affected Land</asp:ListItem>
                        <asp:ListItem Value="CMPACVAL">Affected Acreage Valuation</asp:ListItem>                        
                        <asp:ListItem Value="2">-------- Survey ------</asp:ListItem>
                        <asp:ListItem Value="CMPLNDINFO">Land Info</asp:ListItem>
                        <asp:ListItem Value="3">------ Valuation ------</asp:ListItem>
                        <asp:ListItem Value="CMPPERM">Building/Structure Details</asp:ListItem>
                        <%--<asp:ListItem Value="CMPNONPER">Non Permanent Structure Details</asp:ListItem>--%>
                        <asp:ListItem Value="CMPDMACR">Damaged Crop Details</asp:ListItem>
                        <asp:ListItem Value="CMPCROP">Crop Details</asp:ListItem>
                        <asp:ListItem Value="CMPGRAVE">Grave</asp:ListItem>
                        <asp:ListItem Value="CMPFENCE">Fence</asp:ListItem>
                        <asp:ListItem Value="CMPOTHER">Other</asp:ListItem>
                        <asp:ListItem Value="CMPCULPRO">Culture Property</asp:ListItem>
                        <asp:ListItem Value="CMPFINVAL">Final Valuation</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td></td>
                <td align="left" ><br />
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<%--<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />--%>
                </td>
            </tr>
        </table>
    </fieldset>
    <script type="text/javascript">
        function OpenReport() {
            var ddlReportType = document.getElementById('<%=ddlReportType.ClientID%>');

            var ReportType;

            if (ddlReportType.selectedIndex > 0)
                ReportType = ddlReportType.options[ddlReportType.selectedIndex].value;
            else {
                alert('Select Report Type.');
                return;
            }

            if (ReportType.toString() == "1") {
                alert('Select Report Type.');
                return;
            }
            else if (ReportType.toString() == "2") {
                alert('Select Report Type.');
                return;
            }
            else if (ReportType.toString() == "3") {
                alert('Select Report Type.');
                return;
            }

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            //            var param = 'rptCode=' + ReportType;
            var param = 'rptCode=CMPDT' +
                        '&ReportType=' + ReportType+
                        '&ViewMaster=N';

            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
