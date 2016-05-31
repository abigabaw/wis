<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="GeneralPapCategory.aspx.cs" Inherits="WIS.GeneralPapCategory" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<fieldset class="icePnlinner">
     <legend>Report Criteria</legend>
<table width="80%" align="center" border="0">
<tr>
                <td align="left">
                    <label class="iceLable">
                        Project Name
                    </label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlProject" CssClass="iceTextBox" Width="250px" AppendDataBoundItems="true" runat="server">
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
                    <asp:DropDownList ID="ddlDistrict" ClientIDMode="Static" CssClass="iceTextBox" AppendDataBoundItems="true" AutoPostBack="true" Width="250px"
                        runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
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
                            <asp:DropDownList ID="ddlCounty" ClientIDMode="Static" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
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
                            <asp:DropDownList ID="ddlSubCounty" ClientIDMode="Static" CssClass="iceTextBox" AutoPostBack="true" Width="250px"
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
                            <asp:DropDownList ID="ddlParish" ClientIDMode="Static" CssClass="iceTextBox" Width="250px" runat="server">
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
                    <label class="iceLable">Village</label>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlVillage" ClientIDMode="Static" CssClass="iceTextBox" Width="250px" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
               
                <td align="left">
                    <label class="iceLable">PAP Name</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPAPName" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="250px" />&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ValidationGroup="vgSearch" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                    ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                    <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSearch" runat="server" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtPAPName" runat="server" />
                </td>
            </tr>
              <tr>
             <td align="left" class="style1">
                    <label class="iceLable">Status</label>
                </td>
                <td>
                <asp:RadioButton ID="RadioButtonPAPID" runat="server" GroupName = "Radiobtn" ClientIDMode="Static" Text = "By PAPID" />
                 </td>
                
                 <td>
                <asp:RadioButton ID="RadioButtonPAPOption" runat="server" GroupName = "Radiobtn" ClientIDMode="Static" Text = "by PAP Option"/>
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
             <script language="javascript">
                 function OpenReport() {
                     var fldDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
                     var fldCounty = document.getElementById('<%=ddlCounty.ClientID%>');
                     var fldSubCounty = document.getElementById('<%=ddlSubCounty.ClientID%>');
                     var fldParish = document.getElementById('<%=ddlParish.ClientID%>');
                     var fldVillage = document.getElementById('<%=ddlVillage.ClientID%>');
                     var fldPAPName = document.getElementById('<%=txtPAPName.ClientID%>');
                     var fldprojectID = document.getElementById('<%=ddlProject.ClientID%>');

                     var district;
                     var county;
                     var subCounty;
                     var parish;
                     var village;
                     var papName;
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


                     var rptCode;

                     if (RadioButtonPAPID.checked)
                         rptCode = 'GPC';
                     else
                         rptCode = 'GPCOP';


                     var left = (screen.width - 960) / 2;
                     var top = (screen.height - 650) / 4;

                     var param = 'rptCode=' + rptCode +     
                        '&district=' + district +
                        '&county=' + county +
                        '&subCounty=' + subCounty +
                        '&parish=' + parish +
                        '&village=' + village +
                        '&PAPName=' + fldPAPName.value +
                         '&ProjectID=' + projectID;


                     open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                 }
             </script>
             &nbsp;
             <%--  <div>
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
                    ToolPanelWidth="200px" Visible="false" />
            </div>--%>
</asp:Content>
