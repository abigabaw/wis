<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GroupOwnership.aspx.cs" Inherits="WIS.GroupOwnership" UICulture="en" Culture="en-US" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc3" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
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
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc3:HouseholdSummary ID="HouseholdSummaryCache" runat="server" Visible="false" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="1" cellspacing="1" style="margin-top: 10px;
        margin-bottom: 10px; width: 100%">
        <tr>
            <td class="iceNormalText">
                <div style="float: left">
                    <label class="iceLable">
                        Household ID</label>&nbsp;
                    <asp:TextBox ID="txtHouseHoldID" runat="server" Width="60px" Enabled="false" CssClass="iceTextBox">
                    </asp:TextBox>&nbsp;&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                    ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div style="float: left">
                    Change To&nbsp;
                    <asp:DropDownList ID="ddlPaptype" Width="130px" AppendDataBoundItems="true" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPaptype_SelectedIndexChanged" runat="server">
                        <asp:ListItem Value="IND">Individual</asp:ListItem>
                        <asp:ListItem Value="INS">Institution</asp:ListItem>
                        <asp:ListItem Value="GRP" Selected="True">Group Ownership</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="float: right">
                    <a id="lnkUPloadPhoto" href="#" runat="server"><b>Upload Photo</b></a>&nbsp;|&nbsp;<a
                        id="lnkViewPhoto" href="#" runat="server"><b>View Photo</b></a>&nbsp;|&nbsp;
                    <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a> &nbsp;|&nbsp;
                    <a id="lnkUPloadDoclist" href="#" runat="server"><b>View Document</b></a>
                </div>
                <script type="text/javascript" language="javascript">
                    function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                        var left = (screen.width - 800) / 2;
                        var top = (screen.height - 650) / 4;
                        open('../../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPop', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                    }

                    function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                        var left = (screen.width - 800) / 2;
                        var top = (screen.height - 650) / 4;
                        open('../../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                    }

                    function OpenUploadPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule) {
                        var left = (screen.width - 600) / 2;
                        var top = (screen.height - 500) / 4;
                        open('../../UploadPhotoDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule, 'Uploadphoto', 'width=600px,height=500px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                    }

                    function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule) {
                        var left = (screen.width - 600) / 2;
                        var top = (screen.height - 500) / 4;
                        open('../../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule, 'Uploadphoto', 'width=600px,height=500px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                    }
                </script>
            </td>
        </tr>
    </table>
    <fieldset class="icePnlinner">
        <legend>Group Owner</legend>        
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
            <tr>
                <td class="iceLable" style="width: 16%">
                    PAP UID <span class="mandatory">*</span>
                </td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtPapUid" runat="server" Width="200px" CssClass="iceTextBox" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPapUid"
                        ErrorMessage="Enter PAP UID" Display="None" ValidationGroup="HHDetails" runat="server"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="UppercaseLetters,Numbers"
                        TargetControlID="txtPapUid" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="iceLable" style="vertical-align: middle; width: 12%">
                    Head of Group
                </td>
                <td align="left" colspan="3" style="vertical-align: top;">
                    <div style="float: left;">
                        Surname <span class="mandatory">*</span><br />
                        <asp:TextBox ID="txtSurname" runat="server" Width="100px"  onblur="SetUpperCase(this);UpdateFullName();" CssClass="iceTextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSurname"
                            ErrorMessage="Enter Surname" Display="None" ValidationGroup="GroupOwnership"
                            runat="server">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtSurname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        First Name <span class="mandatory">*</span><br />
                        <asp:TextBox ID="txtfirstname" runat="server" onblur="SetUpperCase(this);UpdateFullName();" Width="100px" CssClass="iceTextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtfirstname"
                            ErrorMessage="Enter First Name" Display="None" ValidationGroup="GroupOwnership"
                            runat="server">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtfirstname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        Other Name
                        <br />
                        <asp:TextBox ID="txtOthername" runat="server" Width="100px" CssClass="iceTextBox" onblur="SetUpperCase(this);">
                        </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtOthername" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 25px;">
                        Full Name
                        <br />
                        <asp:TextBox ID="txtFullname" runat="server" Width="200px" CssClass="iceTextBox">
                        </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtFullname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </td>
                <td align="left">
                </td>
               <%-- <td rowspan="4">
                    <asp:Image ID="imgPAPPhoto" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="180px" Width="150px" />
                </td>--%>
                <td rowspan="4">
              <asp:UpdatePanel ID="updimgPAPPhoto" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <asp:Image ID="imgPAPPhoto" runat="server" BorderColor="Silver" BorderStyle="Solid"
                    BorderWidth="1px" Height="150px" Width="180px" />
                     </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnimgPAPPhoto" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                 <asp:Button ID="btnimgPAPPhoto" Style="display: none" runat="server" OnClick="btnimgPAPPhoto_Click" />
            </td>
            </tr>
            <tr>
                <td class="iceLable">
                    Plot Reference <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPlotReference" runat="server" Width="200px" CssClass="iceTextBoxPlotRef" onchange="SetUpperCase(this);">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPlotReference"
                        ErrorMessage="Enter Plot Reference" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                                            <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true" ClearMaskOnLostFocus="false"
                                                TargetControlID="txtPlotReference">
                                            </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" ControlExtender="mskPlotReference"
                    ControlToValidate="txtPlotReference" IsValidEmpty="False" EmptyValueBlurredText="*"
                    InvalidValueBlurredMessage="*" MaximumValueBlurredMessage="*" MinimumValueBlurredText="*"
                    Display="Dynamic" runat="server" />
                </td>
                <td align="left" class="iceLable">
                    Option Groups
                        <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddloptionGroup" runat="server" Width="260px">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender6" runat="server"
                        TargetControlID="ddloptionGroup"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="ddloptionGroup" InitialValue="0" ErrorMessage="Select a Option Group"
                                        Display="None" ValidationGroup="GroupOwnership" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="iceLable">
                    Sex <span class="mandatory">*</span>
                    <br />
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlGender" runat="server">
                        <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                        <asp:ListItem Value="Male">Male</asp:ListItem>
                        <asp:ListItem Value="Female">Female</asp:ListItem>
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="ddlGender"
                        InitialValue="0" ErrorMessage="Select Sex" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="iceLable">
                    Date of Birth <span class="mandatory">*</span><br />
                </td>
                <td align="left">
                <asp:TextBox ID="dpDateofBirth" runat="server" Width="90px"></asp:TextBox>
<ajaxToolkit:CalendarExtender ID="caldpDateofBirth" runat="server" CssClass="WISCalendarStyle"  TargetControlID="dpDateofBirth"></ajaxToolkit:CalendarExtender>
                 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="dpDateofBirth"
                        ErrorMessage="Enter Date of Birth" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpDateofBirth"
                    ClientValidationFunction="CheckDOB" ErrorMessage="Date of Birth should not be greater than Today's Date"
                    ValidationGroup="GroupOwnership" Display="None"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="iceLable">
                    Position <span class="mandatory">*</span><br />
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlPosition" runat="server">
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="lsePAPConcernsDropDownList" runat="server"
                        TargetControlID="ddlPosition"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="ddlPosition"
                        InitialValue="0" ErrorMessage="Select a Position" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="iceLable">
                    Resident
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="rdlResident" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem Text="Yes" Value="0">
                        </asp:ListItem>
                        <asp:ListItem Text="No" Selected="True" Value="1">
                        </asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="rdlResident"
                        ErrorMessage="Select Resident" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="iceLable">
                    District <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="200px" AutoPostBack="True"
                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlDistrict"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlDistrict"
                        InitialValue="0" ErrorMessage="Select a District" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left" class="iceLable">
                    County <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <contenttemplate>
                        <asp:DropDownList ID="ddlCounty" runat="server" Width="200px" AutoPostBack="True"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    </contenttemplate>
                        <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                    </triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlCounty"
                        InitialValue="0" ErrorMessage="Select a County" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="iceLable">
                    Sub County <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <contenttemplate>
                        <asp:DropDownList ID="ddlSubCounty" runat="server" Width="200px" AutoPostBack="True"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="ddlSubCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    </contenttemplate>
                        <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                    </triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlSubCounty"
                        InitialValue="0" ErrorMessage="Select a Sub County" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left" class="iceLable">
                    Parish <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText" colspan="2">
                    <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                        <contenttemplate>
                        <asp:DropDownList ID="ddlParish" runat="server" Width="200px" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender4" runat="server"
                        TargetControlID="ddlParish"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    </contenttemplate>
                        <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                    </triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlParish"
                        InitialValue="0" ErrorMessage="Select a Parish" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" class="iceLable">
                    Village <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <contenttemplate>
                        <asp:DropDownList ID="ddlVillage" runat="server" Width="200px" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlVillage"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    </contenttemplate>
                        <triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                    </triggers>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ddlVillage"
                        InitialValue="0" ErrorMessage="Select a Village" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left" class="iceLable">
                    &nbsp;
                </td>
                <td align="left" class="iceNormalText" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="iceLable">
                    Contact Phone1 <span class="mandatory">*</span><br />
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtTelephoneNo1" CssClass="iceTextBox" Text="" MaxLength="15" runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtTelephoneNo1"
                        ErrorMessage="Enter Contact Phone1" Display="None" ValidationGroup="GroupOwnership"
                        runat="server">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteTelephoneNo1" FilterType="Numbers" TargetControlID="txtTelephoneNo1"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left" class="iceLable">
                    Contact Phone2
                </td>
                <td align="left" class="iceNormalText" colspan="2">
                    <asp:TextBox ID="txtTelephoneNo2" CssClass="iceTextBox" Text="" MaxLength="15" runat="server">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteTelephoneNo2" FilterType="Numbers" TargetControlID="txtTelephoneNo2"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr> 
            <tr>
            <td class="iceLable">
                GOU Allowance Category <span class="mandatory">*</span> 
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGouAllowance" runat="server" AppendDataBoundItems="true"
                    >
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender14" runat="server" TargetControlID="ddlGouAllowance"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="ddlGouAllowance"
                    InitialValue="0" ErrorMessage="Select GOU Allowance Category" Display="None" ValidationGroup="GroupOwnership"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
            <td class="iceLable">
                Under Taking Period <span class="mandatory">*</span>
            </td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlUnderTakingPeriod" runat="server" AppendDataBoundItems="true"
                    Width="200px">
                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                    <asp:ListItem Value="One">1 Month</asp:ListItem>
                    <asp:ListItem Value="Two">2 Months</asp:ListItem>
                    <asp:ListItem Value="Three">3 Months</asp:ListItem>
                    <asp:ListItem Value="Four">4 Months</asp:ListItem>
                    <asp:ListItem Value="Five">5 Months</asp:ListItem>
                    <asp:ListItem Value="Six">6 Months</asp:ListItem>
                    <asp:ListItem Value="Seven">7 Months</asp:ListItem>
                    <asp:ListItem Value="Eight">8 Months</asp:ListItem>
                    <asp:ListItem Value="Nine">9 Months</asp:ListItem>
                    <asp:ListItem Value="Ten">10 Months</asp:ListItem>
                    <asp:ListItem Value="Eleven">11 Months</asp:ListItem>
                    <asp:ListItem Value="Twelve">12 Months</asp:ListItem>
                     <asp:ListItem Value="Thirteen">13 Months</asp:ListItem>
                    <asp:ListItem Value="Fourteen">14 Months</asp:ListItem>
                    <asp:ListItem Value="Fifteen">15 Months</asp:ListItem>
                    <asp:ListItem Value="Sixteen">16 Months</asp:ListItem>
                    <asp:ListItem Value="Seventeen">17 Months</asp:ListItem>
                    <asp:ListItem Value="Eighteen">18 Months</asp:ListItem>
                    <asp:ListItem Value="Nineteen">19 Months</asp:ListItem>
                    <asp:ListItem Value="Twenty">20 Months</asp:ListItem>
                    <asp:ListItem Value="TwentyOne">21 Months</asp:ListItem>
                    <asp:ListItem Value="TwentyTwo">22 Months</asp:ListItem>
                    <asp:ListItem Value="TwentyThree">23 Months</asp:ListItem>
                    <asp:ListItem Value="TwentyFour">24 Months</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender15" runat="server" TargetControlID="ddlUnderTakingPeriod"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="ddlUnderTakingPeriod"
                    InitialValue="0" ErrorMessage="Select Under Taking Period" Display="None" ValidationGroup="GroupOwnership"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td align="left" class="iceLable" nowrap>
                HH Details Captured By <span class="mandatory">*</span>
            </td>
            <td align="left" class="labelSuffix">
                <asp:TextBox ID="txtCapturedBy" MaxLength="100" runat="server"
                    CssClass="iceTextBox">                                    
                </asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                    ValidChars=" " TargetControlID="txtCapturedBy" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtCapturedBy"
                    ErrorMessage="Enter HH Details Captured By" Display="None" ValidationGroup="GroupOwnership"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
            <td align="left" class="iceLable" >
                HH Details Captured Date <span class="mandatory">*</span>
            </td>                       
                <td align="left">
                    <asp:TextBox ID="dpCapturedDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="cpCapturedDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpCapturedDate"></ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpCapturedDate"
                        ClientValidationFunction="CheckCapturedDate" ErrorMessage="HH Details Captured Date should not be greater than Today's Date"
                        ValidationGroup="HHDetails" Display="None">
                    </asp:CustomValidator>                    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="dpCapturedDate"
                    ErrorMessage="Enter HH Details Captured Date" Display="None" ValidationGroup="GroupOwnership"
                    runat="server"></asp:RequiredFieldValidator>
                </td>
        </tr>
            <tr>
                <td align="center" colspan="5">
                <center>
                    <table>
                        <tr>
                            <td>
                                <a id="lnkGroupOwner" runat="server" href="#" runat="server" class="iceLinkButton" style="text-decoration: none; color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px; height: 17px; margin-top:-0.5px; vertical-align:middle;">
                                    Change Request</a>
                        &nbsp;
                                <asp:Button ID="btnSave" Text="Save" runat="server" ValidationGroup="GroupOwnership"
                                    CssClass="icebutton" OnClick="btnSave_Click" />
                            &nbsp;
                                <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="icebutton" OnClick="btnClear_Click" />
                           &nbsp;
                                <asp:Label ID="StatusGroupOwner" runat="server" Style="text-decoration: blink; color: Red;
                                    font-family: Arial; font-size: 18px; font-weight: bold" />
                            </td>
                        </tr>
                    </table>
                    </center>
                    <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="GroupOwnership" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="icePnlinner">
        <legend>Group Members</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="90%">
            <tr>
                <td class="iceLable" style="vertical-align: middle; width: 12%">
                    Name
                </td>
                <td align="left" style="vertical-align: top;">
                    <div style="float: left;">
                        Surname <span class="mandatory">*</span><br />
                        <asp:TextBox ID="txtMeberSurname" runat="server" onblur="SetUpperCase(this);UpdateMembersFullName();" MaxLength="100" Width="100px" CssClass="iceTextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMeberSurname"
                            ErrorMessage="Enter Member Surname" Display="None" ValidationGroup="Member" runat="server">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtMeberSurname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        First Name <span class="mandatory">*</span><br />
                        <asp:TextBox ID="txtMeberFirstname" runat="server" onblur="SetUpperCase(this);UpdateMembersFullName();" MaxLength="100" Width="100px"
                            CssClass="iceTextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtMeberFirstname"
                            ErrorMessage="Enter Member First Name" Display="None" ValidationGroup="Member"
                            runat="server">
                        </asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtMeberFirstname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        Other Name<br />
                        <asp:TextBox ID="txtMeberOthername" runat="server" Width="100px" MaxLength="100"
                            CssClass="iceTextBox" onblur="SetUpperCase(this);">
                        </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtMeberOthername" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                    <div style="float: left; padding-left: 25px;">
                        Full Name
                        <br />
                        <asp:TextBox ID="txtMeberFullname" runat="server" Width="200px" MaxLength="300" CssClass="iceTextBox">
                        </asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtMeberFullname" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </div>
                </td>
            </tr>
            <tr>
              <center>
                <td align="center" colspan="2">
              
                   <table><tr><td>
                     <a id="lnkGroupMenber" runat="server" href="#" runat="server" class="iceLinkButton" style="text-decoration: none; color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px; height: 17px; margin-top:-0.5px; vertical-align:middle;">Change Request</a>
                   &nbsp;
                    <asp:Button ID="btnSavemember" Text="Save" runat="server" ValidationGroup="Member"
                        CssClass="icebutton" OnClick="btnSavemember_Click" />
                     &nbsp;
                   <asp:Button ID="btnClearMember" Text="Clear" runat="server" CssClass="icebutton"
                        OnClick="btnClearMember_Click" />
                    &nbsp;
                                <asp:Label ID="StatusGroupMenber" runat="server" Style="text-decoration: blink; color: Red;
                                    font-family: Arial; font-size: 18px; font-weight: bold" />
                            </td>
                        </tr>
                   </table>
                   </center>
                    
                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Member" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <div align="center" class="CSSTableGenerator">
            <asp:GridView ID="grdGroupMembers" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdGroupMembers_RowCommand"
                AllowPaging="True" OnPageIndexChanging="grdGroupMembers_PageIndexChanging">
                <rowstyle cssclass="gridRowStyle" />
                <alternatingrowstyle cssclass="gridAlternateRow" />
                <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" font-bold="true" forecolor="White" />
                <headerstyle cssclass="gridHeaderStyle" />
                <columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SurnameIN" HeaderText="Surname" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FirstnameIN" HeaderText="First Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OthernameIN" HeaderText="Other Name" HeaderStyle-HorizontalAlign="Left">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("Groupmemberid") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("Groupmemberid") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </columns>
            </asp:GridView>
            <script language="javascript" type="text/javascript">
                PreventDateFieldEntry(document.getElementById('<%=dpDateofBirth.ClientID%>'));
                PreventDateFieldEntry(document.getElementById('<%=dpCapturedDate.ClientID%>'));

                function DeleteRecord() {
                    return confirm('Are you sure you want to Delete this Group Member?');
                }
                function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                    var left = (screen.width - 600) / 2;
                    var top = (screen.height - 500) / 4;
                    open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
                }

                function UpdateMembersFullName() {
                    var surname = document.getElementById('<%=txtMeberSurname.ClientID %>').value;
                    var firstname = document.getElementById('<%=txtMeberFirstname.ClientID %>').value;
                    var othername = document.getElementById('<%=txtMeberOthername.ClientID %>').value;

                    document.getElementById('<%=txtMeberFullname.ClientID %>').value = surname + ' ' + firstname ;
                }

                function UpdateFullName() {
                    var surname = document.getElementById('<%=txtSurname.ClientID %>').value;
                    var firstname = document.getElementById('<%=txtfirstname.ClientID %>').value;
                    var othername = document.getElementById('<%=txtOthername.ClientID %>').value;

                    document.getElementById('<%=txtFullname.ClientID %>').value = surname + ' ' + firstname;
                }


                function CheckCapturedDate(oSrc, args) {
                    var now = new Date();
                    dtMeeting = GetCalDate('<%=dpCapturedDate.ClientID%>');

                    var CurrentMonth = (now.getMonth() + 1);
                    var CurrentDate = now.getDate();
                    var CurrentYear = now.getFullYear();

                    if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
                    if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

                    var ArrMeetingDt = dtMeeting.split("-");
                    var MeetingDt = ArrMeetingDt[0];
                    var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
                    var MeetingYear = ArrMeetingDt[2];

                    if (CurrentYear < MeetingYear) {
                        args.IsValid = false;
                        return;
                    }
                    else if ((CurrentYear == MeetingYear) && (CurrentMonth < MeetingMonth)) {
                        args.IsValid = false;
                        return;
                    }
                    else if ((CurrentYear == MeetingYear) && (CurrentMonth == MeetingMonth) && (CurrentDate < MeetingDt)) {
                        args.IsValid = false;
                        return;
                    }

                    args.IsValid = true;
                }

                function CheckDOB(oSrc, args) {
                    var now = new Date();
                    dtMeeting = GetCalDate('<%=dpDateofBirth.ClientID%>');
                    var CurrentMonth = (now.getMonth() + 1);
                    var CurrentDate = now.getDate();
                    var CurrentYear = now.getFullYear();

                    if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
                    if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

                    var ArrMeetingDt = dtMeeting.split("-");
                    var MeetingDt = ArrMeetingDt[0];
                    var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
                    var MeetingYear = ArrMeetingDt[2];

                    if (CurrentYear < MeetingYear) {
                        args.IsValid = false;
                        return;
                    }
                    else if ((CurrentYear == MeetingYear) && (CurrentMonth < MeetingMonth)) {
                        args.IsValid = false;
                        return;
                    }
                    else if ((CurrentYear == MeetingYear) && (CurrentMonth == MeetingMonth) && (CurrentDate < MeetingDt)) {
                        args.IsValid = false;
                        return;
                    }

                    args.IsValid = true;
                }

                function PapPhoto() {
                    $get('<%=btnimgPAPPhoto.ClientID%>').click();
                }
                var isDirty = 0;
                function setDirty() {
                    isDirty = 1;
                }

                function setDirtyText() {
                    var btn = document.getElementById("<%= btnSave.ClientID  %>");
                    var tat1 = document.getElementById("<%= txtPapUid.ClientID  %>");
                    var tat2 = document.getElementById("<%= txtSurname.ClientID  %>");
                    var tat3 = document.getElementById("<%= txtCapturedBy.ClientID  %>");
                    var tat4 = document.getElementById("<%= txtfirstname.ClientID  %>");
                    var tat5 = document.getElementById("<%= txtPlotReference.ClientID  %>");
                    var tat6 = document.getElementById("<%= txtTelephoneNo1.ClientID  %>");
                    var tat7 = document.getElementById("<%= txtMeberSurname.ClientID  %>");
                    var tat8 = document.getElementById("<%= txtMeberFirstname.ClientID  %>");
                    if (btn == 'undefined' || btn == null) {
                        isDirty = 0;
                    }
                    else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && tat4.value.toString().replace(/^\s+/, '') == '' && tat5.value.toString().replace(/^\s+/, '') == '' && tat6.value.toString().replace(/^\s+/, '') == '' && tat7.value.toString().replace(/^\s+/, '') == '' && tat8.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
        </div>
    </fieldset>
</asp:Content>
