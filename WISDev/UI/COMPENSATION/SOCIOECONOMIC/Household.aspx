<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="Household.aspx.cs" Inherits="WIS.Household" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc3" %>
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
    <uc3:HouseholdSummary ID="HouseholdSummaryCache" runat="server" Visible="false" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="1" cellspacing="1" style="margin-top: 10px;
        width: 100%">
        <tr>
            <td class="iceNormalText">
                <div style="float: left">
                    <label class="iceLable">
                        Household ID</label>&nbsp;
                    <asp:TextBox ID="txtHouseHoldID" runat="server" Width="60px" Enabled="false" CssClass="iceTextBox"></asp:TextBox>&nbsp;&nbsp;
                    <asp:ImageButton ID="imgSearch" runat="server" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                        ImageUrl="~/IMAGE/search.png" OnClick="imgSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div style="float: left">
                    Change To:&nbsp;<asp:DropDownList ID="ddlPaptype" Width="130px" AppendDataBoundItems="true"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPaptype_SelectedIndexChanged"
                        runat="server">
                        <asp:ListItem Value="IND" Selected="True">Individual</asp:ListItem>
                        <asp:ListItem Value="INS">Institution</asp:ListItem>
                        <asp:ListItem Value="GRP">Group Ownership</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="float: right">
                    <a id="lnkAddPap" href="~/UI/PROJECT/AddPAP.aspx" runat="server"><b>Edit Name</b></a>&nbsp;|&nbsp;
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


                    function OpenViewComments() {
                        var left = (screen.width - 600) / 2;
                        var top = (screen.height - 500) / 4;
                        open('ViewORComments.aspx', 'ViewComments', 'width=600px,height=500px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
                    }
                  
                </script>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height: 25px; float: right">
        <table width="100%">
            <tr>
                <td align="right" style="float:right">
                   
                     <a id="lnkViewComments" href="#" runat="server"><b>View Comments</b></a>
                </td>
                <td align="right" style="width: 180px">
                    <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
        <tr>
            <td class="iceLable" style="width: 16%">
                PAP UID <span class="mandatory">*</span>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtPapUid" runat="server" Width="200px" CssClass="iceTextBox" MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPapUid"
                    ErrorMessage="Enter PAP UID" Display="None" ValidationGroup="HHDetails" runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="UppercaseLetters,Numbers"
                    TargetControlID="txtPapUid" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="iceLable" style="width: 12%">
                Name <span class="mandatory">*</span>
            </td>
            <td align="left" style="width: 32%">
                <asp:TextBox ID="txtName" runat="server" Width="200px" CssClass="iceTextBox" MaxLength="450"
                    onblur="SetUpperCase(this);" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtName"
                    ErrorMessage="Enter Name" Display="None" ValidationGroup="HHDetails" runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                    ValidChars=" ,()/" TargetControlID="txtName" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
            <td class="iceLable" style="width: 16%">
                Plot Reference <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="txtPlotReference" runat="server" Width="200px" CssClass="iceTextBoxPlotRef"
                    MaxLength="100" onchange="SetUpperCase(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlotReference"
                    ErrorMessage="Enter Plot Reference" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
                <ajaxToolkit:MaskedEditExtender ID="mskPlotReference" runat="server" MessageValidatorTip="true"
                    ClearMaskOnLostFocus="false" TargetControlID="txtPlotReference">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" ControlExtender="mskPlotReference"
                    ControlToValidate="txtPlotReference" IsValidEmpty="False" EmptyValueBlurredText="*"
                    InvalidValueBlurredMessage="*" MaximumValueBlurredMessage="*" MinimumValueBlurredText="*"
                    Display="Dynamic" runat="server" />
            </td>
            <td rowspan="5">
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
                Status <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:DropDownList ID="OccupationStatus" runat="server" AppendDataBoundItems="true"
                    Width="200px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="OccupationStatus"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="OccupationStatus"
                    InitialValue="0" ErrorMessage="Select Status" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
            <td class="iceLable">
                Designation <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="txtDesignation" runat="server" Width="100px" MaxLength="7" CssClass="iceTextBox"
                    onchange="SetUpperCase(this);"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                    ValidChars="-" TargetControlID="txtDesignation" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtDesignation"
                    ErrorMessage="Enter Designation" Display="None" ValidationGroup="HHDetails" runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <asp:Label ID="lblLandCompn" Text="Land Compensation" runat="server" CssClass="iceLable"></asp:Label>
                <span class="mandatory">*</span>
            </td>
            <td>
                <asp:RadioButton ID="RdbtnCash" Text="Cash" GroupName="Rd2" runat="server" />
                <asp:RadioButton ID="Rdbtninkind" Text="In-Kind" GroupName="Rd2" runat="server" /></br>
                <asp:RadioButton ID="RdbtnBoth" Text="Both" GroupName="Rd2" runat="server" />
                <asp:RadioButton ID="RdbtnNotApplic" Text="Not Applicable" GroupName="Rd2" runat="server" />
            </td>
            <td nowrap>
                <asp:Label ID="lblHouseCompn" Text="House Compensation" runat="server" CssClass="iceLable"></asp:Label>
                <span class="mandatory">*</span>
            </td>
            <td>
                <asp:RadioButton ID="RdbtnHcash" Text="Cash" GroupName="Rd1" runat="server" />
                <asp:RadioButton ID="RdbtnHbtninkind" Text="In-Kind" GroupName="Rd1" runat="server" /></br>
                <asp:RadioButton ID="RdbtnHBoth" Text="Both" GroupName="Rd1" runat="server" />
                <asp:RadioButton ID="RdbtnHNotApplic" Text="Not applicable" GroupName="Rd1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Option Groups
            </td>
            <td align="left" nowrap>
                <asp:DropDownList ID="ddloptionGroup" runat="server" Width="260px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                &nbsp
                <asp:ImageButton runat="server" ID="ImgRefresh" ImageUrl="~/IMAGE/Refresh.jpeg" ImageAlign="Top"
                    Height="20px" OnClick="ImgRefresh_Click" /><br />
                <asp:CheckBox ID="chkOverRide" Text="Override" runat="server" AutoPostBack="true" 
                    OnCheckedChanged="chkOverRide_CheckedChanged" />
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddloptionGroup"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddloptionGroup"
                    InitialValue="0" ErrorMessage="Select Option Groups" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="iceLable">
                Resident
            </td>
            <td align="left" class="iceNormalText">
                <asp:RadioButtonList ID="rdnResident" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">Yes</asp:ListItem>
                    <asp:ListItem Value="1">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="4" id="tdOverideComments" runat="server">
                <table width="100%" id="spnWhichBoundaryDisputes">
                    <tr>
                        <td>
                            <asp:Label ID="lblCmooment" runat="server" Text="Comments" CssClass="iceLable"></asp:Label><span class="mandatory" id="ManSymbol" runat="server">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBoundaryDisputes" Text="" TextMode="MultiLine" CssClass="iceTextBox"
                                Width="250px" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvOverideComments" ControlToValidate="txtBoundaryDisputes"
                                ErrorMessage="Enter Override Comments" Display="None" ValidationGroup="HHDetails"
                                runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblpercentageOccupied" runat="server" Text="Percentage of Land Occupied by UETCL"
                    CssClass="iceLable" nowrap></asp:Label>
                <span class="mandatory">*</span>
            </td>
            <td align="left" style="vertical-align: top" colspan="0">
                <asp:TextBox ID="txtpercentage" CssClass="iceTextBox" MaxLength="4" Width="150px"
                    runat="server"></asp:TextBox>
                <label>
                    %</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtpercentage"
                    ErrorMessage="Enter Percentage Percentage of Land Occupied by UETCL" Display="None"
                    ValidationGroup="HHDetails" runat="server"></asp:RequiredFieldValidator>
                <br />
                <asp:RangeValidator ID="RangeValidator1" Type="Integer" MinimumValue="1" MaximumValue="100"
                    ControlToValidate="txtpercentage" runat="server" ValidationGroup="HHDetails"
                    ForeColor="Red" ErrorMessage="Enter only numbers between 1 and 100"></asp:RangeValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" FilterType="Numbers,Custom"
                    TargetControlID="txtpercentage" ValidChars="." runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Sex <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlGender"
                    InitialValue="0" ErrorMessage="Select Sex" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
            <td class="iceLable">
                Place of Birth
            </td>
            <td align="left">
                <asp:TextBox ID="txtPlaceofBirth" MaxLength="100" runat="server" CssClass="iceTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Date of Birth
            </td>
            <td align="left">
                <asp:TextBox ID="dpDateOfBirth" runat="server" CssClass="iceTextBox" Width="90px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" CssClass="WISCalendarStyle"
                    TargetControlID="dpDateOfBirth">
                </ajaxToolkit:CalendarExtender>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpDateOfBirth"
                    ClientValidationFunction="CheckDOB" ErrorMessage="Date of Birth should not be greater than Today's Date"
                    ValidationGroup="HHDetails" Display="None">
                </asp:CustomValidator>
            </td>
            <td class="iceLable">
                When did you come here?
            </td>
            <td align="left">
                <asp:TextBox ID="txtwhendiducomehere" MaxLength="4" runat="server" CssClass="iceTextBox"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="flttxtwhendiducomehere" FilterType="Numbers"
                    TargetControlID="txtwhendiducomehere" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Parents Alive?
            </td>
            <td align="left">
                <asp:RadioButton ID="rdoParentAliveYes" GroupName="ParentAlive" Text="Yes" runat="server" />
                <asp:RadioButton ID="rdoParentAliveNo" GroupName="ParentAlive" Text="No" Checked="true"
                    runat="server" />
                &nbsp; <span id="spnWhichParents" style="display: none">
                    <asp:RadioButton ID="rdoParentAliveFather" GroupName="WhichParentAlive" Text="Father"
                        runat="server" />
                    <asp:RadioButton ID="rdoParentAliveMother" GroupName="WhichParentAlive" Text="Mother"
                        runat="server" />
                    <asp:RadioButton ID="rdoParentAliveBoth" GroupName="WhichParentAlive" Text="Both"
                        runat="server" />
                </span>
            </td>
            <td class="iceLable">
                Where Parents Live
            </td>
            <td align="left">
                <asp:TextBox ID="txtwhereparentslive" runat="server" MaxLength="100" ClientIDMode="Static"
                    Enabled="false" CssClass="iceTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Identification Card
            </td>
            <td align="left">
                <asp:RadioButton ID="rdoIdentificationCardYes" GroupName="IdentificationCard" Text="Yes"
                    runat="server" />
                <asp:RadioButton ID="rdoIdentificationCardNo" GroupName="IdentificationCard" Text="No"
                    runat="server" />
            </td>
            <td class="iceLable">
                Card Type
            </td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlcardType" Enabled="false" AppendDataBoundItems="true" runat="server"
                    Width="200px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Name on Card
            </td>
            <td align="left">
                <asp:TextBox ID="txtNameofCard" ClientIDMode="Static" MaxLength="300" runat="server"
                    Enabled="false" CssClass="iceTextBox">                                    
                </asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                    ValidChars=" " TargetControlID="txtNameofCard" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
            </td>
            <td class="iceLable">
                Address on Card
            </td>
            <td align="left" colspan="2">
                <asp:TextBox ID="txtaddressoncard" ClientIDMode="Static" MaxLength="300" runat="server"
                    Enabled="false" CssClass="iceTextBox">                                    
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Marital Status
            </td>
            <td align="left">
                <div style="float: left; width: 110px">
                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" onchange="checkMaritalStaus();">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="Single" Selected="True">Single</asp:ListItem>
                        <asp:ListItem Value="Married">Married</asp:ListItem>
                        <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                        <asp:ListItem Value="Widowed">Widowed</asp:ListItem>
                        <asp:ListItem Value="Separated">Separated</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlMaritalStatus"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                </div>
                <div style="float: left">
                    <label class="iceLable">
                        No of Spouses
                    </label>
                    <asp:TextBox ID="txtMaritalStatus" runat="server" CssClass="iceTextBox" Enabled="false"
                        MaxLength="2" Width="50px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers"
                        TargetControlID="txtMaritalStatus" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </div>
            </td>
            <td class="iceLable">
                Tribe
            </td>
            <td align="left" colspan="2">
                <div style="float: left">
                    <asp:DropDownList ID="ddlTribe" runat="server" Width="150px" AppendDataBoundItems="true"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlTribe_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlTribe"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                </div>
                <div style="float: left">
                    &nbsp;
                    <label class="iceLable">
                        Clan:&nbsp;</label></div>
                <div style="float: left">
                    <asp:UpdatePanel ID="updPnlClan" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlClan" CssClass="iceDropDown" runat="server" Width="150px">
                                <asp:ListItem Value="0">--None--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlClan"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTribe" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
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
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlDistrict"
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
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlCounty"
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
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender8" runat="server" TargetControlID="ddlSubCounty"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" class="iceLable">
                Parish
            </td>
            <td align="left" class="iceNormalText" colspan="2">
                <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlParish" runat="server" Width="200px" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender9" runat="server" TargetControlID="ddlParish"
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
            <td align="left" class="iceLable">
                Village
            </td>
            <td>
                <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlVillage" runat="server" Width="200px" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender10" runat="server" TargetControlID="ddlVillage"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" class="iceLable">
                Religion
            </td>
            <td align="left" class="iceNormalText" colspan="2">
                <asp:DropDownList ID="ddlReligion" runat="server" AppendDataBoundItems="true" onchange="checkReligion();"
                    Width="100px" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <%--<asp:ListItem Value="-1">Other</asp:ListItem>--%>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender11" runat="server" TargetControlID="ddlReligion"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                &nbsp;
                <asp:TextBox ID="txtotherReligion" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="wmeReligion" TargetControlID="txtotherReligion"
                    WatermarkCssClass="watermarked" WatermarkText="Other Religion" runat="server">
                </ajaxToolkit:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                Occupation
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOccupation" runat="server" AppendDataBoundItems="true" Width="300px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender12" runat="server" TargetControlID="ddlOccupation"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
            </td>
            <td class="iceLable">
                Literacy Status
            </td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlLiteracyStatus" runat="server" AppendDataBoundItems="true"
                    Width="200px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender13" runat="server" TargetControlID="ddlLiteracyStatus"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
            </td>
        </tr>
        <tr>
            <td class="iceLable">
                GOU Allowance Category
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGouAllowance" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender14" runat="server" TargetControlID="ddlGouAllowance"
                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                    IsSorted="true" />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlGouAllowance"
                    InitialValue="0" ErrorMessage="Select GOU Allowance Category" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>--%>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlUnderTakingPeriod"
                    InitialValue="0" ErrorMessage="Select Under Taking Period" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" class="iceLable">
                HH Details Captured By <span class="mandatory">*</span>
            </td>
            <td align="left" class="labelSuffix">
                <asp:TextBox ID="txtCapturedBy" MaxLength="100" runat="server" CssClass="iceTextBox">                                    
                </asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                    ValidChars=" " TargetControlID="txtCapturedBy" runat="server">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtCapturedBy"
                    ErrorMessage="Enter HH Details Captured By" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
            <td align="left" class="iceLable">
                HH Details Captured Date <span class="mandatory">*</span>
            </td>
            <td align="left">
                <asp:TextBox ID="dpCapturedDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cpCapturedDate" runat="server" CssClass="WISCalendarStyle"
                    TargetControlID="dpCapturedDate">
                </ajaxToolkit:CalendarExtender>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpCapturedDate"
                    ClientValidationFunction="CheckCapturedDate" ErrorMessage="HH Details Captured Date should not be greater than Today's Date"
                    ValidationGroup="HHDetails" Display="None">
                </asp:CustomValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="dpCapturedDate"
                    ErrorMessage="Enter HH Details Captured Date" Display="None" ValidationGroup="HHDetails"
                    runat="server"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="padding-top: 12px">
                <script type="text/javascript">
                    function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                        var left = (screen.width - 600) / 2;
                        var top = (screen.height - 500) / 4;
                        open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
                    }
                </script>
                <asp:Button ID="lnkChangeRequest" runat="server" Text="Change Request" CssClass="icebutton"
                    Width="120px" Visible="false" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" Text="Save" runat="server" class="icebutton" ValidationGroup="HHDetails"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="icebutton" OnClick="btnClear_Click" />
                <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="HHDetails" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="StatusLabel" runat="server" Style="text-decoration: blink; color: Red;
                    font-family: Arial; font-size: 18px; font-weight: bold" />
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function ShowHideBoundaryDisputes() {
//            if (document.getElementById('<%=chkOverRide.ClientID %>').checked) {
//                document.getElementById('spnWhichBoundaryDisputes').style.display = 'block';
//                document.getElementById('<%= ddloptionGroup.ClientID %>').disabled = false;
//            }
//            else {
//                document.getElementById('spnWhichBoundaryDisputes').style.display = 'none';
//                document.getElementById('<%= ddloptionGroup.ClientID %>').disabled = true;
//                document.getElementById('<%= ddloptionGroup.ClientID %>').value = 0;
//            }
        }

        PreventDateFieldEntry(document.getElementById('<%=dpDateOfBirth.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dpCapturedDate.ClientID%>'));


        document.body.onload = function () {
            SiteMasterBody_OnLoad();    // Master Page function
            checkMaritalStaus();
        }

        function ShowHideWhichParents(show) {
            if (show) {
                document.getElementById('spnWhichParents').style.display = '';
                document.getElementById('<%=txtwhereparentslive.ClientID%>').disabled = '';
            }
            else {
                document.getElementById('spnWhichParents').style.display = 'none';
                document.getElementById('<%=txtwhereparentslive.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtwhereparentslive.ClientID%>').value = '';
            }
        }

        function EnableDisableIDFields(enable) {
            if (enable) {
                document.getElementById('<%=ddlcardType.ClientID%>').disabled = '';
                document.getElementById('<%=txtNameofCard.ClientID%>').disabled = '';
                document.getElementById('<%=txtaddressoncard.ClientID%>').disabled = '';
            }
            else {
                document.getElementById('<%=ddlcardType.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtNameofCard.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtaddressoncard.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=ddlcardType.ClientID%>').selectedIndex = 0;
                document.getElementById('<%=txtNameofCard.ClientID%>').value = '';
                document.getElementById('<%=txtaddressoncard.ClientID%>').value = '';
            }
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
            dtMeeting = GetCalDate('<%=dpDateOfBirth.ClientID%>');

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


        function checkMaritalStaus() {
            var parm = document.getElementById('<%=ddlMaritalStatus.ClientID%>');
            var mStatus;
            var fldMaritalStatus;

            fldMaritalStatus = document.getElementById('<%=txtMaritalStatus.ClientID%>');
            mStatus = parm.options[parm.selectedIndex].text;

            if (mStatus.toUpperCase() == 'MARRIED') {
                fldMaritalStatus.disabled = false;
                fldMaritalStatus.focus();
            }
            else {
                fldMaritalStatus.disabled = true;
                fldMaritalStatus.value = '';
            }
        }

        function checkReligion() {
            var parm = document.getElementById('<%=ddlReligion.ClientID%>');
            var mStatus = parm.options[parm.selectedIndex].text;

            if (mStatus == 'Other') {
                document.getElementById('<%=txtotherReligion.ClientID%>').disabled = false;
                document.getElementById('<%=txtotherReligion.ClientID%>').value = '';
                document.getElementById('<%=txtotherReligion.ClientID%>').focus();
            }
            else {
                document.getElementById('<%=txtotherReligion.ClientID%>').disabled = true;
                document.getElementById('<%=txtotherReligion.ClientID%>').value = 'Other Religion';
            }
        }

        function PapPhoto() {
            $get('<%=btnimgPAPPhoto.ClientID%>').click();
        }

        function CheckYear() {
            var msg;
            msg = document.getElementById('<%=txtwhendiducomehere.ClientID %>').value;
            var stringlenght = msg.length;
            var d = new Date();
            var n = d.getFullYear();
            if (stringlenght < 4) {
                alert("Enter 4 Digits for the year.");
                document.getElementById('<%=txtwhendiducomehere.ClientID %>').value = '';
                return;
            }
            if (msg > n) {
                alert("Entered Year cannot be in the future.");
                document.getElementById('<%=txtwhendiducomehere.ClientID %>').value = '';
                return;
            }

            dtMeeting = GetCalDate('<%=dpDateOfBirth.ClientID%>');
            var ArrMeetingDt = dtMeeting.split("-");
            var MeetingYear = ArrMeetingDt[2];

            if (msg < MeetingYear) {
                alert("Entered Year cannot be less than the Year of Birth.");
                document.getElementById('<%=txtwhendiducomehere.ClientID %>').value = '';
            }
        }

        function CheckYearForMove() {
            var msg;
            msg = document.getElementById('<%=txtwhendiducomehere.ClientID %>').value;
            dtMeeting = GetCalDate('<%=dpDateOfBirth.ClientID%>');
            var ArrMeetingDt = dtMeeting.split("-");
            var MeetingYear = ArrMeetingDt[2];

            if (msg.length > 0 && msg < MeetingYear) {
                alert("'When did you come here?' cannot be less than the Year of Birth.");
                document.getElementById('<%=txtwhendiducomehere.ClientID %>').value = '';
            }
            CheckCardType();
        }

        function CheckCardType() {
            dtMeeting = GetCalDate('<%=dpDateOfBirth.ClientID%>');
            var ArrMeetingDt = dtMeeting.split("-");
            var MeetingYear = ArrMeetingDt[2];
            var d = new Date();
            var n = d.getFullYear();
            var msg = parseInt(n.toString()) - 18;
            var fld = document.getElementById('<%=ddlcardType.ClientID%>');
            if (MeetingYear > msg) {
                if (fld.options[fld.selectedIndex].text.toString().toLowerCase() == "voter's id".toLowerCase()) {
                    alert('Voting card field is not available for Children (U18)');
                    fld.selectedIndex = 0;
                    document.getElementById('<%=txtNameofCard.ClientID%>').value = '';
                    document.getElementById('<%=txtaddressoncard.ClientID%>').value = '';
                }
            }
        }
        //            document.getElementById('divAll').onclick = function () {
        //                isDirty = 0;
        //                setTimeout(function () { setDirtyText(); }, 100);
        //            };

        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtPapUid.ClientID  %>");
            var tat2 = document.getElementById("<%= txtName.ClientID  %>");
            var tat3 = document.getElementById("<%= txtCapturedBy.ClientID  %>");
            var tat4 = document.getElementById("<%= txtDesignation.ClientID  %>");
            var tat5 = document.getElementById("<%= txtPlotReference.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && tat4.value.toString().replace(/^\s+/, '') == '' && tat5.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
