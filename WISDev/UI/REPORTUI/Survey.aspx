<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Survey.aspx.cs" Inherits="WIS.Survey" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
                        Width="250px" runat="server" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlProject" runat="server" ControlToValidate="ddlProject"
                        ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Project to search Paps"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        District</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" AppendDataBoundItems="true"
                        AutoPostBack="true" Width="250px" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <label class="iceLable">
                        County</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
                                runat="server" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
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
            </tr>
            <tr>
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
                <td align="left">
                    <label class="iceLable">
                        PAP Name</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPAPName" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="240px" />&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ValidationGroup="vgSearch" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                    ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                    <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSearch" runat="server" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtPAPName" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Plot Reference</label> 
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPlotreference" runat="server" MaxLength="100" CssClass="iceTextBoxPlotRef" 
                        onchange="SetUpperCase(this);"  Width="240px" />
                    <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true" TargetControlID="txtPlotReference" clearmaskonlostfocus="false"></ajaxToolkit:MaskedEditExtender>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlotreference" 
                 ErrorMessage="Enter Plot Reference" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
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
    <script language="javascript">
        function OpenReport() {
            var fldDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
            var fldCounty = document.getElementById('<%=ddlCounty.ClientID%>');
            var fldSubCounty = document.getElementById('<%=ddlSubCounty.ClientID%>');
            var fldParish = document.getElementById('<%=ddlParish.ClientID%>');
            var fldVillage = document.getElementById('<%=ddlVillage.ClientID%>');
            var fldPAPName = document.getElementById('<%=txtPAPName.ClientID%>');
            var fldPlotReference = document.getElementById('<%=txtPlotreference.ClientID%>');
            var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');

            var district;
            var county;
            var subCounty;
            var parish;
            var village;
            var papName;
            var plotReference = ''; 
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

            if (fldPlotReference.value.toString() == '____/_____/___') {
                plotReference = '';
            }
            else {
                plotReference = fldPlotReference.value.toString();
            }

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=SUR' +
                        '&district=' + district +
                        '&county=' + county +
                        '&subCounty=' + subCounty +
                        '&parish=' + parish +
                        '&village=' + village +
                        '&PAPName=' + fldPAPName.value +
                        '&PlotReference=' + plotReference +
                         '&ProjectID=' + projectID;

            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
