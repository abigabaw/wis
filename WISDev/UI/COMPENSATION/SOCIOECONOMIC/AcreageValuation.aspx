<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="AcreageValuation.aspx.cs" Inherits="WIS.AcreageValuation" %>

<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy"
    TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
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
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div align="right" style="width: 100%">
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right" style="width: 410px">
                                <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                                <div style="margin-top:-25px;">|&nbsp;<a id="lnkViewDependents" href="#" runat="server"><b>View Stakeholders</b></a></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <fieldset class="icePnlinner">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table align="center" border="0" cellpadding="3" cellspacing="1" style="margin-top: 10px;
                        width: 100%">
                        <tr>
                            <td align="left" style="width: 18%">
                                <label class="iceLable">
                                    Stakeholder Designation</label>
                            </td>
                            <td align="left" style="width: 18%">
                                <asp:Label ID="lblStakeholdDesignation" runat="server" CssClass="iceLable"></asp:Label>
                            </td>
                            <td align="left" style="width: 10%">
                                <label class="iceLable">
                                    Land Type</label>
                            </td>
                            <td align="left" style="width: 20%">
                                <asp:Label ID="lblLandType" runat="server" CssClass="iceLable"></asp:Label>
                            </td>
                            <td align="left" style="vertical-align: top; width: 34%" rowspan="3" colspan="2">
                                <fieldset class="icePnlinner">
                                    <legend>Information Available Only For Residents</legend>
                                    <table border="0" cellspacing="0" cellpadding="2" width="100%" style="margin-left: 0px">
                                        <tr>
                                            <td align="left" style="background-color: #eeeeee;">
                                                <label class="iceLable">
                                                    Whole Acreage Acres</label>
                                            </td>
                                            <td align="left" style="background-color: #eeeeee;">
                                                <asp:TextBox ID="txtAcreageAcres" runat="server" CssClass="iceTextBox" MaxLength="10"
                                                    AutoPostBack="true" onkeypress="return CheckDecimal(event, this)" OnTextChanged="txtAcreageAcres_TextChanged">
                                                </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers, Custom"
                                                    ValidChars="." TargetControlID="txtAcreageAcres" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="background-color: #eeeeee;">
                                                <label class="iceLable">
                                                    Whole Acreage HA</label>
                                            </td>
                                            <td align="left" style="background-color: #eeeeee;">
                                                <asp:UpdatePanel ID="upnAcreageHA" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtAcreageHA" runat="server" CssClass="iceTextBox" ReadOnly="true"></asp:TextBox>    
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtAcreageAcres" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>                                                
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Status</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblStatus" runat="server" CssClass="iceLable"></asp:Label>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Land Owner</label><span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLandOwner" runat="server" CssClass="iceTextBox">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqLandOwner" runat="server" ErrorMessage="Enter Land Owner"
                                    ControlToValidate="txtLandOwner" Display="None" ValidationGroup="AcreageValuation">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                    ValidChars=" " TargetControlID="txtLandOwner" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Land Block</label><span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLandBlock" runat="server" CssClass="iceTextBox">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqlandblock" runat="server" ErrorMessage="Enter Land Block"
                                    ControlToValidate="txtLandBlock" Display="None" ValidationGroup="AcreageValuation">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Land Plot</label><span class="mandatory">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLandPlot" runat="server" CssClass="iceTextBox">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqlandplot" runat="server" ErrorMessage="Enter Land Plot"
                                    ControlToValidate="txtLandPlot" Display="None" ValidationGroup="AcreageValuation">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Current Proprietor</label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCurrentOperation" runat="server" CssClass="iceTextBox" Width="150px"
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlCurrentOperation"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    District</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDistrict" CssClass="iceLable" runat="server"></asp:Label>
                            </td>
                            <td align="left" style="width: 20px">
                                <label class="iceLable">
                                    County</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCounty" CssClass="iceLable" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <label class="iceLable">
                                    Sub County</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSubCounty" CssClass="iceLable" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Parish</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblParish" CssClass="iceLable" runat="server"></asp:Label>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Village</label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblVillage" CssClass="iceLable" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <div>
    <table width="60%"><tr><td>
        <label class="iceLable">
            Unit of Measure</label></td><td>
        <asp:DropDownList ID="ddlMeasure" runat="server" Width="100px" AutoPostBack="true"
            OnSelectedIndexChanged="ddlMeasure_SelectedIndexChanged">
            <asp:ListItem Value="Acre">Acre</asp:ListItem>
            <asp:ListItem Value="HA">HA</asp:ListItem>
            <asp:ListItem Value="Sq Metre">Sq Metre</asp:ListItem>
        </asp:DropDownList></td>
            <td class="iceLable">
                Location Classification <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlLocClassification" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender16" runat="server" TargetControlID="ddlLocClassification"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ddlLocClassification"
                    InitialValue="0" ErrorMessage="Select Location Classification" Display="None" ValidationGroup="AcreageValuation"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr></table>
    </div>
    <div>
        <br />
    </div>

    <asp:UpdatePanel ID="upnAcreageVal" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div align="center" class="CSSTableGenerator" style="width: 100%;">
                <table border="1" cellspacing="0" cellpadding="0" style="border-color: #3A5F7A; height: 50px"
                    width="100%">
                    <tbody>
                        <tr>
                            <td width="12%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Right of Way <span class="mandatory">*</span>
                            </td>
                            <td width="10%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Acres
                            </td>
                            <td width="10%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Ha
                            </td>
                            <td width="18%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Sq Metres
                            </td>
                            <td width="15%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Share of
                                <br />
                                Land Value <span class="mandatory">*</span>
                            </td>
                            <td width="13%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Rate per Acre <span class="mandatory">*</span>
                            </td>
                            <td bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                ROW land Value
                            </td>
                        </tr>
                        <tr>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:TextBox ID="txtRightWay" runat="server" Width="70px" MaxLength="13" AutoPostBack="true"
                                        OnTextChanged="txtRightWay_TextChanged" onkeypress="return CheckDecimal(event, this)">
                                    </asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtRightWay" FilterType="Numbers,Custom"
                                        TargetControlID="txtRightWay" runat="server" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="reqroghtway" runat="server" ErrorMessage="Enter Right of Way"
                                        ControlToValidate="txtRightWay" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblAcres" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblHA" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblSqmtrs" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:DropDownList ID="ddlRowLandvalshare" runat="server" Width="80px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRowLandvalshare_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="10">10%</asp:ListItem>
                                        <asp:ListItem Value="20">20%</asp:ListItem>
                                        <asp:ListItem Value="30">30%</asp:ListItem>
                                        <asp:ListItem Value="40">40%</asp:ListItem>
                                        <asp:ListItem Value="50">50%</asp:ListItem>
                                        <asp:ListItem Value="60">60%</asp:ListItem>
                                        <asp:ListItem Value="66">66%</asp:ListItem>
                                        <asp:ListItem Value="70">70%</asp:ListItem>
                                        <asp:ListItem Value="80">80%</asp:ListItem>
                                        <asp:ListItem Value="90">90%</asp:ListItem>
                                        <asp:ListItem Value="100">100%</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqrowlandshare" runat="server" ErrorMessage="Select Share of Land Value"
                                        ControlToValidate="ddlRowLandvalshare" InitialValue="0" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:TextBox ID="txtRatePerAcres" runat="server" AutoPostBack="true" Width="100px" MaxLength="20"
                                        Style="text-align: right" onchange="CheckAmount(this);" OnTextChanged="txtRatePerAcres_TextChanged">
                                    </asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtRatePerAcres" FilterType="Numbers,Custom"
                                        TargetControlID="txtRatePerAcres" ValidChars="," runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="reqrateperacresrw" runat="server" ErrorMessage="Enter Rate per Acres"
                                        ControlToValidate="txtRatePerAcres" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblRowLandVal" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <div align="center" class="CSSTableGenerator">
                <table border="1" cellspacing="0" cellpadding="0" style="border-color: #3A5F7A; height: 50px"
                    width="100%">
                    <tbody>
                        <tr>
                            <td width="12%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Wayleave <span class="mandatory">*</span>
                            </td>
                            <td width="10%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Acres
                            </td>
                            <td width="10%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Ha
                            </td>
                            <td width="18%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                In Sq Metres
                            </td>
                            <td width="12%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Level of
                                <br />
                                Dimunition <span class="mandatory">*</span>
                            </td>
                            <td width="12%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Share of
                                <br />
                                Land Value <span class="mandatory">*</span>
                            </td>
                            <td width="13%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Rate per Acre <span class="mandatory">*</span>
                            </td>
                            <td bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                Wayleave
                                <br />
                                land Value
                            </td>
                        </tr>
                        <tr>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:TextBox ID="txtWayleave" runat="server" Width="100px" MaxLength="13" AutoPostBack="true"
                                        OnTextChanged="txtWayleave_TextChanged" onkeypress="return CheckDecimal(event, this)">
                                    </asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtWayleave" FilterType="Numbers,Custom"
                                        TargetControlID="txtWayleave" ValidChars="." runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="reqwayleave" runat="server" ErrorMessage="Enter Wayleave"
                                        ControlToValidate="txtWayleave" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblWayleavesAcres" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblWayleavesHA" runat="server" Width="50px" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblWayleaveSqmtrs" runat="server" Width="80px" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:DropDownList ID="ddlDimunition" runat="server" Width="80px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlDimunition_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="10">10%</asp:ListItem>
                                        <asp:ListItem Value="20">20%</asp:ListItem>
                                        <asp:ListItem Value="30">30%</asp:ListItem>
                                        <asp:ListItem Value="40">40%</asp:ListItem>
                                        <asp:ListItem Value="50">50%</asp:ListItem>
                                        <asp:ListItem Value="60">60%</asp:ListItem>
                                        <asp:ListItem Value="66">66%</asp:ListItem>
                                        <asp:ListItem Value="70">70%</asp:ListItem>
                                        <asp:ListItem Value="80">80%</asp:ListItem>
                                        <asp:ListItem Value="90">90%</asp:ListItem>
                                        <asp:ListItem Value="100">100%</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqdimunition" runat="server" ErrorMessage="Select Level of Dimunition"
                                        ControlToValidate="ddlDimunition" InitialValue="0" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:DropDownList ID="ddlWayleaveShareLandVal" runat="server" Width="80px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlWayleaveShareLandVal_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="10">10%</asp:ListItem>
                                        <asp:ListItem Value="20">20%</asp:ListItem>
                                        <asp:ListItem Value="30">30%</asp:ListItem>
                                        <asp:ListItem Value="40">40%</asp:ListItem>
                                        <asp:ListItem Value="50">50%</asp:ListItem>
                                        <asp:ListItem Value="60">60%</asp:ListItem>
                                        <asp:ListItem Value="66">66%</asp:ListItem>
                                        <asp:ListItem Value="70">70%</asp:ListItem>
                                        <asp:ListItem Value="80">80%</asp:ListItem>
                                        <asp:ListItem Value="90">90%</asp:ListItem>
                                        <asp:ListItem Value="100">100%</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqwayleavesshare" runat="server" ErrorMessage="Select Share of Land value"
                                        ControlToValidate="ddlWayleaveShareLandVal" InitialValue="0" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:TextBox ID="txtWayleaveRateperAcres" runat="server" Width="100px" AutoPostBack="true" MaxLength="20"
                                        Style="text-align: right" onchange="CheckAmount(this);" OnTextChanged="txtWayleaveRateperAcres_TextChanged">
                                    </asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtWayleaveRateperAcres" FilterType="Numbers,Custom"
                                        TargetControlID="txtWayleaveRateperAcres" ValidChars="," runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="reqwlrateacres" runat="server" ErrorMessage="Enter Rate per Acres"
                                        ControlToValidate="txtWayleaveRateperAcres" Display="None" ValidationGroup="AcreageValuation">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td style="border-color: #3A5F7A">
                                <div align="center">
                                    <asp:Label ID="lblWayleavelandVal" runat="server" CssClass="iceLable"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <div align="center">
                <div align="center" class="CSSTableGenerator" style="width: 80%;">
                    <table border="1" cellspacing="0" cellpadding="0" align="center" style="border-color: #3A5F7A;
                        height: 50px;" width="100%">
                        <tbody>
                            <tr>
                                <td width="15%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                    Total
                                </td>
                                <td width="15%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                    In Acres
                                </td>
                                <td width="15%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                    In Ha
                                </td>
                                <td width="20%" bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;">
                                    In Sq Metres
                                </td>
                                <td bgcolor="#3A5F7A" style="border: 0px; color: #FFFFFF; text-align: center;" width="30%">
                                    Total land Value
                                </td>
                            </tr>
                            <tr>
                                <td style="border-color: #3A5F7A">
                                    <div align="center">
                                        <asp:Label ID="lblTotal" runat="server" Text="&nbsp;" CssClass="iceLable"></asp:Label>
                                    </div>
                                </td>
                                <td style="border-color: #3A5F7A">
                                    <div align="center">
                                        <asp:Label ID="lblTotAcres" runat="server" CssClass="iceLable"></asp:Label>
                                    </div>
                                </td>
                                <td style="border-color: #3A5F7A">
                                    <div align="center">
                                        <asp:Label ID="lblTotHA" runat="server" CssClass="iceLable"></asp:Label>
                                    </div>
                                </td>
                                <td style="border-color: #3A5F7A">
                                    <div align="center">
                                        <asp:Label ID="lblTotSqmtrs" runat="server" CssClass="iceLable"></asp:Label>
                                    </div>
                                </td>
                                <td style="border-color: #3A5F7A">
                                    <div align="center">
                                        <asp:Label ID="lblTotlandVal" runat="server" CssClass="iceLable"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <br />
            <table align="center" width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="lnkAcrValuation" runat="server" Text="Change Request" CssClass="icebutton"
                            Width="120px" Visible="false" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="AcreageValuation" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="icebutton" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="StatusAcrValuation" runat="server" Style="text-decoration: blink;
                            color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="valsumAcreageValuation" runat="server" ShowSummary="false"
                ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                ValidationGroup="AcreageValuation" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlMeasure" 
                EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }

        function OpenPAPDependents(HHID_) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('../SOCIOECONOMIC/PlotDependents.aspx?HHID=' + HHID_, 'ViewPopupPApDepend', 'width=800px,height=650px,resizable=0,scrollbars=0,top=' + top + ', left=' + left);
        }

        function CheckAmount(src) {
            var amount;
            var val = RemoveComma(src.value);
            if (val == "") {
                val = 0;
            }
            if (!isNaN(val))
                amount = val;
            else
                amount = '0';
            src.value = AddComma(amount);
        }

        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        function RemoveComma(iValue) {
            return iValue.toString().replace(/,?/g, "");
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
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtLandOwner.ClientID  %>");
            var tat2 = document.getElementById("<%= txtLandBlock.ClientID  %>");
            var tat3 = document.getElementById("<%= txtLandPlot.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
