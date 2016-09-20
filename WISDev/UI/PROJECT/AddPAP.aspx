<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="AddPAP.aspx.cs" Inherits="WIS.AddPAP" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>    
    <div style="width:100%">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummaryCache" runat="server" Visible="false" />
    <div style="width: 100%">
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnADDPAP" Text="ADD PAP" runat="server" CssClass="icebutton" OnClick="btnADDPAP_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSearchPAP" Text="Search PAP" runat="server" CssClass="icebutton"
                        OnClick="btnSearchPAP_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlSearchPAP" Visible="false" runat="server" DefaultButton="btnSearch">
        <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="SearchPAP" runat="server" />
        <fieldset class="icePnlinner">
            <legend>Search PAP</legend>
            <table width="100%" align="center" border="0">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            HHID</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtHHID" runat="server" MaxLength="100" CssClass="iceTextBox" Width="200px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="Numbers"
                            TargetControlID="txtHHID" runat="server" />
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            PAP UID</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPAPUIDSearch" runat="server" MaxLength="100" CssClass="iceTextBox"
                            Width="200px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" FilterType="Numbers,LowercaseLetters,UppercaseLetters"
                            TargetControlID="txtPAPUIDSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            PAP Name</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPAPName" runat="server" MaxLength="100" CssClass="iceTextBox"
                            Width="200px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtPAPName" runat="server" />
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Plot Reference</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPlotReferenceSearch" runat="server" Width="194px" CssClass="iceTextBoxPlotRef"
                                                onchange="SetUpperCase(this);"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MessageValidatorTip="true"
                            ClearMaskOnLostFocus="false" TargetControlID="txtPlotReferenceSearch">
                        </ajaxToolkit:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            District</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDistrictSearch" CssClass="iceTextBox" AppendDataBoundItems="true"
                            AutoPostBack="true" Width="205px" runat="server" OnSelectedIndexChanged="ddlDistrictSearch_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            County</label>
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="uplCountySearch" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCountySearch" CssClass="iceTextBox" AutoPostBack="true"
                                    Width="205px" runat="server" OnSelectedIndexChanged="ddlCountySearch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDistrictSearch" EventName="SelectedIndexChanged" />
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
                        <asp:UpdatePanel ID="uplSubCountySearch" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSubCountySearch" CssClass="iceTextBox" AutoPostBack="true"
                                    Width="205px" runat="server" OnSelectedIndexChanged="ddlSubCountySearch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCountySearch" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Parish</label>
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="uplParishSearch" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlParishSearch" CssClass="iceTextBox" Width="205px" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCountySearch" EventName="SelectedIndexChanged" />
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
                        <asp:UpdatePanel ID="uplVillageSearch" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlVillageSearch" CssClass="iceTextBox" Width="205px" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCountySearch" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />
                        &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                            OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlAddPAP" runat="server" DefaultButton="btn_Save">
        <asp:Panel ID="pnlPAPInformation" runat="server">
            <table border="0" width="100%" align="center">
                <tr>
                    <td>
                        <asp:Panel ID="pnlPapDetails" runat="server">
                            <fieldset class="icePnlinner">
                                <legend>PAP Information</legend>
                                <table border="0" cellpadding="4" style="background-color: #dcdcdc;">
                                    <tr>
                                        <td>
                                            <label class="iceLable">
                                                NOTE: This is for adding All type of PAPs.&nbsp;&nbsp;For Changing a pap as Institution or Group Ownership
                                                PAPs, go to Compensation -> Socio Economic page.<br />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                                <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                                    <tr>
                                        <td class="iceLable" style="width: 15%">
                                            Name of PAP <span class="mandatory">*</span>
                                        </td>
                                        <td align="left" colspan="3" style="vertical-align: top;">
                                            <div style="float: left;">
                                                Surname <span class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtSurname" runat="server" Width="100px" TabIndex="1" onblur="SetUpperCase(this);UpdateFullName();"
                                                    CssClass="iceTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtSurname"
                                                    ErrorMessage="Enter Surname" Display="None" ValidationGroup="AddPAP" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    ValidChars=" " TargetControlID="txtSurname" runat="server">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                            <div style="float: left; padding-left: 10px;">
                                                First Name <span class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtfirstname" runat="server" Width="100px" TabIndex="2" onblur="SetUpperCase(this);UpdateFullName();"
                                                    CssClass="iceTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtfirstname"
                                                    ErrorMessage="Enter First Name" Display="None" ValidationGroup="AddPAP" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    ValidChars=" " TargetControlID="txtfirstname" runat="server">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                            <div style="float: left; padding-left: 10px;">
                                                Other Name
                                                <br />
                                                <asp:TextBox ID="txtOthername" runat="server" Width="100px" TabIndex="3" onblur="SetUpperCase(this);UpdateFullName();"
                                                    CssClass="iceTextBox">
                                                </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                                    ValidChars=" " TargetControlID="txtOthername" runat="server">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                            <div style="float: left; padding-left: 25px;">
                                                Full Name
                                                <br />
                                                <asp:TextBox ID="txtNameofPAP" class="iceTextBox" ReadOnly="true" runat="server"
                                                    Width="194px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="req1" ControlToValidate="txtNameofPAP" ErrorMessage="Enter PAP Name"
                                                    Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                                    ValidChars=" " TargetControlID="txtNameofPAP" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable" style="width: 15%">
                                            PAP UID <span class="mandatory">*</span>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtPapUid" runat="server" Width="200px" TabIndex="4" CssClass="iceTextBox" MaxLength="100"
                                                onblur="SetUpperCase(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPapUid"
                                                ErrorMessage="Enter PAP UID" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="LowercaseLetters,UppercaseLetters,Numbers"
                                                TargetControlID="txtPapUid" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable" style="width: 15%">
                                            Plot Reference <span class="mandatory">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlotReference" runat="server" Width="194px" TabIndex="5" CssClass="iceTextBoxPlotRef"
                                                ></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true"
                                                ClearMaskOnLostFocus="false" TargetControlID="txtPlotReference">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlotReference" InitialValue="____/_____/___"
                                                ErrorMessage="Enter Plot Reference" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="iceLable">
                                            Designation <span class="mandatory">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDesignation" runat="server" Width="100px" TabIndex="6" MaxLength="7" CssClass="iceTextBox"
                                                onblur="SetUpperCase(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtDesignation"
                                                ErrorMessage="Enter Designation" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                                ValidChars="-" TargetControlID="txtDesignation" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable">
                                            District <span class="mandatory">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="201px" TabIndex="7" class="iceDropDown"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlDistrict"
                                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                                IsSorted="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlDistrict"
                                                InitialValue="0" ErrorMessage="Select a District" Display="None" ValidationGroup="AddPAP"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="iceLable">
                                            County <span class="mandatory">*</span>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCounty" runat="server" Width="201px" TabIndex="8" class="iceDropDown"
                                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCounty"
                                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                                        IsSorted="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCounty"
                                                        InitialValue="0" ErrorMessage="Select a County" Display="None" ValidationGroup="AddPAP"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable">
                                            Sub County <span class="mandatory">*</span>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlSubCounty" runat="server" Width="201px" TabIndex="9" class="iceDropDown"
                                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlSubCounty"
                                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                                        IsSorted="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlSubCounty"
                                                        InitialValue="0" ErrorMessage="Select a Sub County" Display="None" ValidationGroup="AddPAP"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="iceLable">
                                            Village <span class="mandatory">*</span>
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlVillage" runat="server" Width="201px" TabIndex="10" class="iceDropDown"
                                                        AppendDataBoundItems="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlVillage"
                                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                                        IsSorted="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlVillage"
                                                        InitialValue="0" ErrorMessage="Select a Village" Display="None" ValidationGroup="AddPAP"
                                                        runat="server"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable">
                                            Parish
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlParish" runat="server" Width="201px" TabIndex="11" class="iceDropDown"
                                                        AppendDataBoundItems="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlParish"
                                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                                        IsSorted="true" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="iceLable">
                                            Right of Way <span class="mandatory">*</span>
                                        </td>
                                        <td align="left" class="iceNormalText">
                                            <asp:TextBox ID="txtrightofWay" class="iceTextBox" onkeypress="return CheckDecimal(event, this)"
                                                runat="server" Width="194px" TabIndex="12" MaxLength="8"></asp:TextBox>
                                            Acres
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtrightofWay"
                                                ErrorMessage="Enter Right of Way" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers, Custom"
                                                ValidChars="." TargetControlID="txtrightofWay" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="iceLable">
                                            Wayleaves <span class="mandatory">*</span>
                                        </td>
                                        <td align="left" class="iceNormalText">
                                            <asp:TextBox ID="txtWayleaves" class="iceTextBox" onkeypress="return CheckDecimal(event, this)"
                                                runat="server" Width="194px" TabIndex="13" MaxLength="8"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtWayleaves"
                                                ErrorMessage="Enter Wayleaves" Display="None" ValidationGroup="AddPAP" runat="server"></asp:RequiredFieldValidator>
                                            Acres
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers, Custom"
                                                ValidChars="." TargetControlID="txtWayleaves" runat="server" />
                                        </td>
                                        <td class="iceLable">
                                            Total
                                        </td>
                                        <td align="left" class="iceNormalText">
                                            <asp:TextBox ID="txttotal" class="iceTextBox" ReadOnly="true" TabIndex="14" runat="server" Width="194px"></asp:TextBox>
                                            Acres
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="iceLable">
                                            Latitude:
                                        </td>
                                        <td align="left" class="iceNormalText">
                                            <asp:TextBox ID="txtLatitude" class="iceTextBox" onkeypress="return CheckDecimal(event, this)"
                                                runat="server" Width="194px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers,Custom"
                                                ValidChars=".-" TargetControlID="txtLatitude" runat="server" />
                                        </td>
                                        <td class="iceLable">
                                            Longitude:
                                        </td>
                                        <td align="left" class="iceNormalText">
                                            <asp:TextBox ID="txtLongitude" class="iceTextBox" onkeypress="return CheckDecimal(event, this)"
                                                runat="server" Width="194px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Numbers,Custom"
                                                ValidChars=".-" TargetControlID="txtLongitude" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <br />
                                            <asp:Label ID="lblMessage" runat="server" Text="Selected Project is Frozen." Visible="false"
                                                ForeColor="Red"></asp:Label>&nbsp;
                                            <asp:Button ID="btn_ImportExcel" Text="Import from Excel" runat="server" CssClass="icebutton"
                                                Style="width: 140px" OnClick="btn_ImportExcel_Click" />
                                            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="icebutton" OnClick="btn_Save_Click"
                                                OnClientClick="DisableOnSaveWithVal(this);" UseSubmitBehavior="false" />
                                            <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btn_Clear_Click" />
                                            <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddPAP" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlFileUpload" Visible="false" runat="server">
            <table border="0" width="100%" align="center">
                <tr>
                    <td>
                        <fieldset class="icePnl1">
                            <legend>PAP Upload Information</legend>
                            <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                                <tr>
                                    <td class="iceLable" style="width: 15%">
                                        Select File
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:FileUpload ID="FileUpload" class="iceTextBox" runat="server" Width="250px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnUpload" Text="Upload" runat="server" class="icebutton" Style="width: 140px"
                                            OnClick="BtnUpload_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnCancelUpload" Text="Cancel" runat="server" class="icebutton" Style="width: 140px"
                                            OnClick="btnCancelUpload_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <fieldset class="icePnlinner">
        <legend id="lgndTitle" runat="server">PAP List</legend>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
            <table border="0" width="100%" align="center">
                <tr>
                    <td>
                        <asp:GridView ID="GrdPapInformation" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="True"
                            PageSize="10" OnRowCommand="GrdPapInformation_RowCommand" OnPageIndexChanging="GrdPapInformation_PageIndexChanging"
                            OnRowDataBound="GrdPapInformation_RowDataBound" >
                            <RowStyle CssClass="gridRowStyle" />
                            <AlternatingRowStyle CssClass="gridAlternateRow" />
                            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                            <HeaderStyle CssClass="gridHeaderStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="PAP UID" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif" CommandName="EditRow"
                                            CommandArgument='<%#Eval("HHID") %>' runat="server">
                                            <%#Eval("Papuid") %></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Papuid" HeaderText="PAP UID" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Surname" HeaderText="Surname" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Firstname" HeaderText="First Name" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Othername" HeaderText="Other Name" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PapName" HeaderText="Name of PAP" Visible="true" HeaderStyle-HorizontalAlign="Left" />
                                <%--New Columns Added Here--%>
                                <asp:BoundField DataField="Institution" HeaderText="Institution/Organization Name"
                                    Visible="true" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="PapType" HeaderText="Pap Type" Visible="true" HeaderStyle-HorizontalAlign="Left" />
                                <%--New Columns Ended Here--%>
                                <asp:BoundField DataField="PlotReference" HeaderText="Plot Ref" Visible="false" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Plot Ref">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <a href="#" id='<%# Eval("PlotReference") %>' onclick="ViewMap(<%# Eval("HHID") %>);">
                                            <%# Eval("PlotReference")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="District" HeaderText="District" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="County" HeaderText="County" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="SubCounty" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Parish" HeaderText="Parish" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Village" HeaderText="Village" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Landtenure" HeaderText="Land Tenure" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="RightOfWay" HeaderText="Right Of Way (acres)" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Wayleaves" HeaderText="Wayleaves (acres)" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="TotalSQM" HeaderText="Total Take(SQM)" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="TotalHa" HeaderText="Total Take(Ha)" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="TotalAcres" HeaderText="Total Take(Acres)" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Cropsvalue" HeaderText="Valuation for Crops/Plants (UGX)"
                                    HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Housevalue" HeaderText="Buildings / Improvements (UGX)"
                                    HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="SubTotal" HeaderText="Sub Total (UGX)" HeaderStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Disturbance" HeaderText="Disturbance Allowance" HeaderStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Total" HeaderText="Total (UGX)" HeaderStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:N0}" />
                                <asp:TemplateField HeaderText="" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAddCo" CommandName="AddCoordinates" CommandArgument='<%#Eval("HHID")+ "|" + Eval("PapName") %>'
                                            runat="server">Add Coordinates</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                                  <ItemStyle HorizontalAlign="Center" Width="7%" />
                                   <ItemTemplate>
                               <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                                OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />                   
                               </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                            CommandName="DeleteRow" CommandArgument='<%#Eval("HHID") %>' OnClientClick="return DeleteRecord();"
                                            runat="server" />
                                        <asp:Literal ID="litHHID" Text='<%#Eval("HHID") %>' Visible="false" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <center>
            <br />
            <asp:Button ID="btnGridDataSave" runat="server" Text="Save" CssClass="icebutton"
                OnClick="btnGridDataSave_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
            <asp:Button ID="btnGridDataCancel" runat="server" Text="Cancel" CssClass="icebutton"
                OnClick="btnGridDataCancel_Click" /></center>
    </fieldset>
    </div>
    <!-- Popup window-->
    <div style="width: 100%; height: 120%;">
        <div id="light1" class="white_content">
            <div style="clear: both">
            </div>
            <div style="margin-top: 60px; margin-left: 200px;">
                <div style="width: 450px; height: 200px; overflow: scroll">
                    <div style="clear: both;">
                    </div>
                    <fieldset class="icePnl" style="background-color: #eee;">
                        <table class="frmpopTable">
                            <tr class="frmMenu">
                                <td class="textboldform">
                                    &nbsp; Paps Information
                                </td>
                            </tr>
                        </table>
                        <hr class="icepophrtag" />
                        <table width="400px;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblSpaps" runat="server" Text="" CssClass="iceLable" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="iceLable">

                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblFpaps" runat="server" Text="" CssClass="iceLable" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="button" id="Button1" class="icebutton" value="Close" onclick="document.getElementById('light1').style.display='none';document.getElementById('fade1').style.display='none';" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
        </div>
        <div id="fade1" class="black_overlay">
            <%--background, used to hide the master page--%>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <!-- Popup window-->

    <script language="javascript" type="text/javascript">
        function CalculateTotalAcres() {
            rightOfWayAcres = document.getElementById('<%=txtrightofWay.ClientID%>').value;
            wayleaveAcres = document.getElementById('<%=txtWayleaves.ClientID%>').value;
            totalAcres = document.getElementById('<%=txttotal.ClientID%>');
//            if (rightOfWayAcres == '' && wayleaveAcres == '') {
//                totalAcres.value = '0';
//            }
            if (rightOfWayAcres != '' && !isNaN(rightOfWayAcres) && wayleaveAcres != '' && !isNaN(wayleaveAcres)) {
                totalAcres.value = parseFloat(rightOfWayAcres) + parseFloat(wayleaveAcres);
            }
            else if (rightOfWayAcres != '' && !isNaN(rightOfWayAcres)) {
                totalAcres.value = parseFloat(rightOfWayAcres);
            }
            else if (wayleaveAcres != '' && !isNaN(wayleaveAcres)) {
                totalAcres.value = parseFloat(wayleaveAcres);
            }
            else {
                totalAcres.value = '0';
            }
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
        }

        function DisableOnSaveWithVal(src) {
            if (Page_ClientValidate()) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        }

        function HideDiv() {
            document.getElementById('light1').style.display = 'block';
            //document.getElementById('light').style.display = 'block'; 
            document.getElementById('fade1').style.display = 'block';
        }

        function DisableOnSave(src) {
            src.disabled = true;
            src.value = 'Please Wait...';
        }

        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

        function ViewMap(HHID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            open('ViewPAPMap.aspx?HHID=' + HHID, 'routeMapWin', 'width=800px,height=500px,top=' + top + ', left=' + left + ',scrollbars=1');
        }


        function ViewMapadasda(Plotlatitude, Plotlongitude) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            open('ViewPAPMap.aspx?Plotlatitude=' + Plotlatitude + '&Plotlongitude=' + Plotlongitude, 'routeMapWin', 'width=800px,height=500px,top=' + top + ', left=' + left + ',scrollbars=1');
        }

        function UpdateFullName() {
            var surname = document.getElementById('<%=txtSurname.ClientID %>').value;
            var firstname = document.getElementById('<%=txtfirstname.ClientID %>').value;
            var othername = document.getElementById('<%=txtOthername.ClientID %>').value;

            document.getElementById('<%=txtNameofPAP.ClientID %>').value = surname + ' ' + firstname;
        }

        function CheckDecimal(e, src) {
            if (e.keyCode == 46) { // Invoke when press Enter Key
                var char = src.value;
                if (char.indexOf(".") == -1) {
                    return true;
                }
                else if (char.indexOf(".") > -1) {
                    return false;
                }
                return true;
            }
            return true;
        }

        function SetVisible(val) {
            var hf = document.getElementById("<%= hfVisible.ClientID  %>");
            hf.value = val;
        }

        function CheckPlotReference(src) {
            var msg;
            msg = src.value.toString().replace("_","");
            if (msg.length == 14) {
            }
            else {
                alert('Please enter Valid Plot Referance');
                src.value = '____/_____/___';
            }
        }
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btn_Save.ClientID  %>");
            var tat1 = document.getElementById("<%= txtSurname.ClientID  %>");
            var tat2 = document.getElementById("<%= txtfirstname.ClientID  %>");
            var tat3 = document.getElementById("<%= txtPapUid.ClientID  %>");
            var tat4 = document.getElementById("<%= txtPlotReference.ClientID  %>");
            var tat5 = document.getElementById("<%= txtDesignation.ClientID  %>");
            var tat6 = document.getElementById("<%= txtWayleaves.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && tat4.value.toString().replace(/^\s+/, '') == '' && tat5.value.toString().replace(/^\s+/, '') == '' && tat6.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;

            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                return '';
            }
        }                
 
    </script>

    <script type="text/javascript">

        (function () { try { var n = Sys.Extended.UI.MaskedEditBehavior.prototype, t = n._ExecuteNav; n._ExecuteNav = function (n) { var i = n.type; i == "keydown" && (n.type = "keypress"), t.apply(this, arguments), n.type = i } } catch (i) { return } })()

        // Fixes issue with delete key not working on Ipad browsers.

    </script>
</asp:Content>
