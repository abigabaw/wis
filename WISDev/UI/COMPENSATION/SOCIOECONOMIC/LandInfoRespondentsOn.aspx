<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LandInfoRespondentsOn.aspx.cs" Inherits="WIS.LandInfoRespondentsOn" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div><br />
    <div style="text-align:center">
        <asp:Label id="lblMessage" runat="server" CssClass="iceLable" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 14px;font-weight: bold"></asp:Label>
    </div>
    <asp:Panel ID="pnlLivingOnEffectedLand" runat="server">
        <fieldset class="icePnlinner" id="fieldsetpage">
            <legend>Living On Affected Land</legend>
            <table align="center" border="0" cellpadding="3" cellspacing="1" style="margin-top: 10px;" id="table1">
                <tr>
                    <td align="left" style="width: 40%">
                        <label class="iceLable">
                            Where were you living before you came here?</label>
                    </td>
                    <td align="left" class="iceNormalText" colspan="3">
                        <asp:TextBox ID="txtLivingbefore" runat="server" MaxLength="100" CssClass="iceTextBox">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="iceLable" colspan="4">
                        To which location do you hope to move?
                    </td>
                </tr>
                <tr>
                    <td align="left" class="iceLable">
                        District
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:DropDownList ID="ddlDistrict" runat="server" Width="200px" AutoPostBack="True"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlDistrict"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    </td>
                    <td align="left" class="iceLable">
                        County
                    </td>
                    <td align="left" class="iceNormalText" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCounty" runat="server" Width="200px" AutoPostBack="True"
                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCounty"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="iceLable">
                        Sub County
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSubCounty" runat="server" Width="200px" AutoPostBack="True"
                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlSubCounty"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" class="iceLable">
                        Village
                    </td>
                    <td align="left" class="iceNormalText" colspan="2">
                        <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlVillage" runat="server" Width="200px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlVillage"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
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
                            Is it one of your other land holdings?</label>
                    </td>
                    <td align="left">
                        <div>
                            <asp:CheckBox ID="chkLandHold" runat="server" AutoPostBack="True" OnCheckedChanged="chkLandHold_CheckedChanged" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            If yes, which?</label>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:TextBox ID="txtYesLandHold" runat="server" MaxLength="100" CssClass="iceTextBox">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Do you require transport assistance for moving?</label>
                    </td>
                    <td align="left" colspan="3">
                        <div>
                            <asp:CheckBox ID="chkTransport" runat="server" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Would you move to a village where you have relatives?</label>
                    </td>
                    <td align="left" colspan="3">
                        <div>
                            <asp:CheckBox ID="chkRelative" runat="server" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Have you buried family members on this land?</label>
                    </td>
                    <td align="left">
                        <div>
                            <asp:CheckBox ID="chkFamilyMember" runat="server" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            How many buried?</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBuried" runat="server" Enabled="false" CssClass="iceTextBox"
                            MaxLength="3" Width="50px">
                        </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteBuried" FilterType="Numbers" TargetControlID="txtBuried"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Do you wish to relocate the ancestors?</label>
                    </td>
                    <td align="left" colspan="3">
                        <div>
                            <asp:CheckBox ID="chkAncestors" runat="server" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Panel ID="pnlButtons" runat="server">
                            <asp:Button ID="lnkLandInfoResOn" runat="server" Text="Change Request" CssClass="icebutton"
                                Width="120px" Visible="false" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="icebutton" OnClientClick="return CheckDataEntered();" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="icebutton" OnClick="btnClear_Click" />
                        </asp:Panel>                        
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="StatusLandInfoResOn" runat="server" Style="text-decoration: blink;
                            color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <script type="text/javascript">
        function EnableBuried(src, field1) {
            field1 = document.getElementById(field1);

            if (src.checked) {
                field1.disabled = false;
                field1.focus();
            }
            else {
                field1.disabled = true;
                field1.value = '';
            }
        }

        spnpnldiv = document.getElementById('table1');
        if (spnpnldiv != null) {
            scrWidth = screen.availWidth;
            spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
        }

        function CheckDataEntered() {
            var table = document.getElementById('fieldsetpage');
            inputFieldtable = table.getElementsByTagName('input');
            for (i = 0; i < inputFieldtable.length; i++) {
                elem = inputFieldtable[i];
                if (elem.type == "checkbox") {
                    if (elem.checked == true)
                        return true;
                }
                else if (elem.type == 'text') {
                    if (trim(elem.value.toString()).length > 0) {
                        return true;
                    }
                }
            }
            var District = document.getElementById('<%=ddlDistrict.ClientID%>');
            if (District.selectedIndex > 0) {
                return true;
            }

            alert('Please Enter data to save.');
            return false;
        }

        function trim(myString) {
            return myString.replace(/^\s+|\s+$/g, '');
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
