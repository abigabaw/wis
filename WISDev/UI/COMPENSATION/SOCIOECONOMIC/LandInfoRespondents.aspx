<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LandInfoRespondents.aspx.cs" Inherits="WIS.LandInfoRespondents" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
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
    <fieldset class="icePnlinner">
        <legend>Land Holdings</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
            <tr>
                <td align="left">
                    <asp:Label ID="Label9" runat="server" CssClass="iceLable" Text="Type"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DrpType" runat="server" CssClass="iceDropDown" Height="21px"
                        Width="145px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="DrpType"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="DrpType"
                        InitialValue="0" ErrorMessage="Select a Type" Display="None" ValidationGroup="Landinfo"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <asp:Label ID="Label1" runat="server" CssClass="iceLable" Text="Use"></asp:Label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DrpUse" runat="server" CssClass="iceDropDown" Height="21px"
                        Width="125px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="DrpUse"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DrpUse"
                        InitialValue="0" ErrorMessage="Select Use" Display="None" ValidationGroup="Landinfo"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label10" runat="server" CssClass="iceLable" Text="District"></asp:Label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="DrpDistrict" runat="server" CssClass="iceDropDown" Height="21px"
                        Width="147px" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DrpDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="DrpDistrict"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                </td>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" CssClass="iceLable" Text="County"></asp:Label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpCounty" runat="server" CssClass="iceDropDown" Height="21px"
                                Width="125px" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DrpCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="DrpCounty"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DrpDistrict" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label3" runat="server" CssClass="iceLable" Text="Sub County"></asp:Label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpSubCounty" runat="server" CssClass="iceDropDown" Height="21px"
                                Width="148px" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DrpSubCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="DrpSubCounty"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DrpCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left">
                    <asp:Label ID="Label4" runat="server" CssClass="iceLable" Text="Village"></asp:Label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DrpVillage" runat="server" CssClass="iceDropDown" Height="21px"
                                Width="128px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="DrpVillage"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DrpSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 30%">
                    <label class="iceLable">
                        Type of Tenure</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlTenureType" runat="server" CssClass="iceDropDown" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlTenureType"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                </td>
                <td align="left">
                    <asp:Label ID="Label5" runat="server" CssClass="iceLable" Text="Tenure"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txttenure" runat="server" MaxLength="04" AutoCompleteType="Disabled"
                        CssClass="iceTextBox" Width="123px">
                    </asp:TextBox>
                    Years
                    <ajaxToolkit:FilteredTextBoxExtender ID="TenureId" FilterType="Numbers" TargetControlID="txttenure"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label6" runat="server" CssClass="iceLable" Text="Total Size"></asp:Label>
                      <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txttotal" runat="server" onkeypress="javascript:return CheckDecimal (event, this);" AutoCompleteType="Disabled" CssClass="iceTextBox"
                        MaxLength="8" Width="123px">
                    </asp:TextBox>(sq mts)
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers,custom"
                       validchars="." TargetControlID="txttotal" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txttotal"
                    ErrorMessage="Enter Total Size" Display="None" ValidationGroup="Landinfo" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label7" runat="server" CssClass="iceLable" Text="Primary Residence"></asp:Label>
                </td>
                <td align="left">
                    <div>
                        <asp:CheckBox runat="server" ID="ChkPrimary" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                </td>
                <td align="left">
                    <asp:Label ID="Label8" runat="server" CssClass="iceLable" Text="Affected"></asp:Label>
                </td>
                <td align="left">
                    <div>
                        <asp:CheckBox runat="server" ID="ChkAffected" />&nbsp;<label class="labelSuffix">(Check
                            if YES)</label></div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="lnkLandHoldings" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_SaveRes" runat="server" ValidationGroup="Landinfo" value="Save"
                        CssClass="icebutton" OnClick="btn_SaveRes_Click" Text="Save" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_ClearRes" value="Clear" runat="server" CssClass="icebutton" Text="Clear"
                        OnClick="btn_ClearRes_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="StatusLandHoldings" runat="server" Style="text-decoration: blink;
                        color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Landinfo" runat="server" />
        <br />
        <asp:GridView ID="grdLandInfoRespondents" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" PageSize="10" AutoGenerateColumns="false" Width="100%"
            OnRowCommand="grdLandInfoRespondents_RowCommand" AllowPaging="true" OnPageIndexChanging="grdLandInfoRespondents_PageIndexChanging">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LND_HOLDINGID" HeaderText="HOld ID" HeaderStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="Land_Type" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Land_Use" HeaderText="Use" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DISTRICT" HeaderText="District" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="COUNTY" HeaderText="County" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SUBCOUNTY" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="VILLAGE" HeaderText="Village" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TENURE" HeaderText="Tenure yrs" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Total Size<br/>sq mts">
                    <ItemStyle HorizontalAlign="Right" Width="7%" />
                    <ItemTemplate>
                        <asp:Label ID="lblTotalSize" runat="server" Text='<%# (Eval("TOTALSIZE").ToString()=="-1")?"": Eval("TOTALSIZE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="TOTALSIZE" HeaderText="Total Size" HeaderStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="ISPRIMARYRESIDENCE" HeaderText="Primary Residence" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ISAFFECTED" HeaderText="Affected" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("holdingID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("holdingID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("holdingID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="footer">
            <script language="javascript" type="text/javascript">
                function DeleteRecord() {
                    return confirm('Are you sure you want to Delete this Record?');
                }                  
                   
            </script>
        </div>
    </fieldset>
    <fieldset class="icePnlinner">
        <legend>Member Claims and Easements</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
            <tr>
                <td class="iceSectionHeader" colspan="2">
                    <asp:Label ID="Label11" runat="server" CssClass="iceLable" Text="Family Members"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 35%">
                    <div>
                        <asp:Label ID="Label12" runat="server" CssClass="iceLable" Text="Does any member have claims to these holdings">
                        </asp:Label>
                     <%--   &nbsp;<label class="labelSuffix">(Check if YES)</label>--%>
                        </div>
                </td>
                <td align="left" class="iceNormalText">
                    <div>
                        <%-- <asp:CheckBox runat="server" ID="ChkHoldings" OnCheckedChanged="ChkHoldings_CheckedChanged"
                            AutoPostBack="True" />--%>
                        <asp:CheckBox runat="server" ID="ChkHoldings" />
                        <asp:TextBox ID="txtenterholdings" runat="server" ClientIDMode="Static" CssClass="iceTextBox"
                            MaxLength="100" Width="400px" Text="" Enabled="False">
                        </asp:TextBox></div>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="ftbeEnterHolding" WatermarkText="Enter Details"
                        TargetControlID="txtenterholdings" runat="server">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
            <tr>
                <td class="iceSectionHeader" colspan="4" valign="top">
                    <asp:Label ID="Label13" runat="server" CssClass="iceLable" Text="Easements"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" width="30%" valign="top">
                    <asp:Label ID="Label14" runat="server" CssClass="iceLable" Text=" 1. Do other people have a right to pick from your land?">
                    </asp:Label>
                </td>
                <td align="left" width="20%" valign="top">
                    <div>
                        <asp:CheckBox runat="server" ID="Chkrighttopick" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                </td>
                <td align="left" width="30%" valign="top">
                    <asp:Label ID="Label15" runat="server" CssClass="iceLable" Text=" 2. Do you have a right to pick from other people's land?">
                    </asp:Label>
                </td>
                <td align="left" width="20%" valign="top">
                    <div>
                        <asp:CheckBox runat="server" ID="Chkotherland" />&nbsp;<label class="labelSuffix">(Check
                            if YES)</label></div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <asp:Label ID="Label16" runat="server" CssClass="iceLable" Text=" 3. Do other people have the right to access water via your land?">
                    </asp:Label>
                </td>
                <td align="left" valign="top">
                    <div>
                        <asp:CheckBox runat="server" ID="Chkrighttoacess" />&nbsp;<label class="labelSuffix">(Check
                            if YES)</label></div>
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="Label17" runat="server" CssClass="iceLable" Text="4. Do you have the right to access water via other people's land?">
                    </asp:Label>
                </td>
                <td align="left" valign="top">
                    <div>
                        <asp:CheckBox runat="server" ID="Chkwaterother" />&nbsp;<label class="labelSuffix">(Check
                            if YES)</label></div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <asp:Label ID="Label18" runat="server" CssClass="iceLable" Text="5. Any other Easements?">
                    </asp:Label>
                </td>
                <td align="left" valign="top" colspan="3">
                    <div>
                        <%--<asp:CheckBox runat="server" ID="ChkEasements" AutoPostBack="True" OnCheckedChanged="ChkEasements_CheckedChanged" />--%>
                        <asp:CheckBox runat="server" ID="ChkEasements" />
                        <asp:TextBox ID="txtentereaseemnts" runat="server" ClientIDMode="Static" CssClass="iceTextBox"
                            MaxLength="100" Width="500px" Text="" Enabled="False">
                        </asp:TextBox></div>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Details"
                        TargetControlID="txtentereaseemnts" runat="server">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="lnkMCE" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btn_Savemambers" runat="server" class="icebutton" OnClick="btn_Savemambers_Click"
                        Text="Save" value="Save" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearMembers" runat="server" class="icebutton" Text="Clear" value="Clear"
                        OnClick="btnClearMembers_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="StatusMCE" runat="server" Style="text-decoration: blink; color: Red;
                        font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script language="javascript" type="text/javascript">
        function CheckDecimal(e, src) {
            if (e.keyCode == 46) { // Invoke when press Enter Key
                var char = src.value;
                if (char.indexOf(".") == -1) {
                    return true;
                }
                else if (char.indexOf(".") > -1) {
                    return false;
                }
                //               
                return true;
            }
            return true;
        }
        function EnableHolding(src, field1) {
            field1 = document.getElementById(field1);

            if (src.checked) {
                field1.disabled = false;
                field1.focus();
                field1.value = '';

            }
            else {
                field1.disabled = true;
                field1.value = 'Enter Details';
            }
        }

        function EnableEasement(src, field1) {
            field1 = document.getElementById(field1);

            if (src.checked) {
                field1.disabled = false;
                field1.focus();
                field1.value = '';
            }
            else {
                field1.disabled = true;
                field1.value = 'Enter Details';
            }
        }


        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btn_SaveRes.ClientID  %>");
            var tat1 = document.getElementById("<%= txttotal.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;
                //txtyes = 1;
            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                //isDirty = 2;
                return '';
            }
        }                
    </script>
</asp:Content>
