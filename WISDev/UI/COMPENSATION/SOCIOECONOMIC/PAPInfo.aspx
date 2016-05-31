<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PAPInfo.aspx.cs" Inherits="WIS.PAPInfo" %>
    
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
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
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true"
        runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <asp:HiddenField ID="hdnProjectStartDate" runat="server" Value="0" />
    <asp:HiddenField ID="hdnProjectEndDate" runat="server" Value="0" />
    <fieldset class="icePnlinner">
        <legend>Stakeholder Details</legend>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
            <tr>
                <td align="left" style="width: 15%">
                    <label class="iceLable">
                        Stakeholder</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" style="width: 36%">
                    <asp:TextBox ID="txtStakeholder" CssClass="iceTextBox" Width="60%" MaxLength="300" runat="server" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteStakeholder" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars=" '" TargetControlID="txtStakeholder" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStakeholder"
                        ErrorMessage="Enter Stakeholder Name" Display="None" ValidationGroup="PAPInfo"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
                <td align="left" style="width: 14%">
                    <label class="iceLable">
                        Representation</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlRepresentation" CssClass="iceTextBox" Width="201px" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender8" runat="server" TargetControlID="ddlRepresentation"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Residential Address</label>
                </td>
                <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:TextBox ID="txtResidentialAddress" CssClass="iceTextBox" TextMode="MultiLine"
                        Rows="3" MaxLength="1000" Width="201px" runat="server"></asp:TextBox>
                </td>
                <td align="left" style="vertical-align: top" style="vertical-align: top">
                    <label class="iceLable">
                        Postal Address</label>
                </td>
                <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:TextBox ID="txtPostalAddress" CssClass="iceTextBox" TextMode="MultiLine" Rows="3"
                        MaxLength="1000" Width="201px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Telephone No</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtTelephoneNo" CssClass="iceTextBox" Text="" MaxLength="15" runat="server"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteTelephoneNo" FilterType="Numbers" TargetControlID="txtTelephoneNo"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Date of Survey</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="dpSurveyDate" runat="server" Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDateOfBirth" CssClass="WISCalendarStyle" runat="server"
                        TargetControlID="dpSurveyDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpSurveyDate"
                        ClientValidationFunction="ProjectDateCheck" ErrorMessage="Date of Survey should be in between the Project Start date and End date."
                        ValidationGroup="PAPInfo" Display="None"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpSurveyDate"
                        ClientValidationFunction="ProjectDateCheckToday" ErrorMessage="Date of Survey should not be greater than Today's Date."
                        ValidationGroup="PAPInfo" Display="None"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        District</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlDistrict" CssClass="iceTextBox" Width="201px" runat="server"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AppendDataBoundItems="true"
                        AutoPostBack="true">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlDistrict"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlDistrict"
                        InitialValue="0" ErrorMessage="Select a District" Display="None" ValidationGroup="PAPInfo"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <label class="iceLable">
                        County</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCounty" CssClass="iceTextBox" Width="201px" runat="server"
                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlCounty"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCounty"
                                InitialValue="0" ErrorMessage="Select a County" Display="None" ValidationGroup="PAPInfo"
                                runat="server"></asp:RequiredFieldValidator>
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
                    <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSubCounty" runat="server" Width="201px" class="iceDropDown"
                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlSubCounty"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlSubCounty"
                                InitialValue="0" ErrorMessage="Select a Sub County" Display="None" ValidationGroup="PAPInfo"
                                runat="server"></asp:RequiredFieldValidator>
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
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlParish" runat="server" Width="201px" class="iceDropDown"
                                AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlParish"
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
                        Village</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlVillage" runat="server" Width="201px" class="iceDropDown"
                                AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlVillage"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlVillage"
                                InitialValue="0" ErrorMessage="Select a Village" Display="None" ValidationGroup="PAPInfo"
                                runat="server"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left" class="iceNormalText">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Segment</label> <span class="mandatory">*</span>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlSegment" CssClass="iceTextBox" Width="150px" AppendDataBoundItems="true"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlSegment"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlSegment" InitialValue="0" 
                ErrorMessage="Select Segment" Display="None" ValidationGroup="PAPInfo" runat="server"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Type of Line</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:Label ID="lblTypeOfLine" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Wayleave</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:Label ID="lblWayleave" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Right of Way</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:Label ID="lblRightOfWay" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                </td>
            </tr>
           
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="lnkPAPINFO" runat="server" Text="Change Request" CssClass="icebutton" Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="PAPInfo" UseSubmitBehavior="false"  OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />                   
                    &nbsp;&nbsp;
                    <asp:Label ID="StatusPAPINFO" runat="server" Style="text-decoration: blink; color: Red;
                        font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
         
           
                 
        </table>
        <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="PAPInfo" runat="server" />
    </fieldset>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=dpSurveyDate.ClientID%>'));

        function Segment_IndexChanged(src) {
            segmentID = src.options[src.selectedIndex].value;
            PageMethods.GetTypeOfLineDetails(segmentID, OnWSGetTypeOfLineDetailsComplete);
        }

        function OnWSGetTypeOfLineDetailsComplete(result) {
            document.getElementById('lblTypeOfLine').innerText = '';
            document.getElementById('lblRightOfWay').innerText = '';
            document.getElementById('lblWayleave').innerText = '';

            if (result != 'null') {
                if (result.TypeOfLine != null)
                    document.getElementById('lblTypeOfLine').innerText = result.TypeOfLine + ' kV';

                if (result.Rightofwaymeasurement != null)
                    document.getElementById('lblRightOfWay').innerText = result.Rightofwaymeasurement + ' Metres';

                if (result.Wayleavemeasurement != null)
                    document.getElementById('lblWayleave').innerText = result.Wayleavemeasurement + ' Metres';
            }
        }
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

        function ProjectDateCheck(oSrc, args) {
            var projStartDate = document.getElementById('<%=hdnProjectStartDate.ClientID%>').value;
            var projeEndDate = document.getElementById('<%=hdnProjectEndDate.ClientID%>').value;
            dtConstrStart = GetCalDate('<%=dpSurveyDate.ClientID%>');
            var ProjEndDate;
            var ProjEndMonth ;
            var ProjEndYear;

            var ArrProjSt = projStartDate.split("/");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = ArrProjSt[1];
            var ProjStartYear = ArrProjSt[2];

            if (projeEndDate.toString() == '01/01/0001') {
                projeEndDate = new Date();
                ProjEndMonth = (projeEndDate.getMonth() + 1);
                ProjEndDate = projeEndDate.getDate();
                ProjEndYear = projeEndDate.getFullYear();
            }
            else {
                var ArrProjEnd = projeEndDate.split("/");
                ProjEndDate = ArrProjEnd[0];
                ProjEndMonth = ArrProjEnd[1];
                ProjEndYear = ArrProjEnd[2];
            }

            var ArrConstrSt = dtConstrStart.split("-");
            var ConstrStartDate = ArrConstrSt[0];
            var ConstrStartMonth = GetMonthNumber(ArrConstrSt[1]);
            var ConstrStartYear = ArrConstrSt[2];

            if (ProjStartYear > ConstrStartYear) {
                args.IsValid = false;
                return;
            }
            else if (ProjEndYear < ConstrStartYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth > ConstrStartMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth == ConstrStartMonth) && (ProjStartDate > ConstrStartDate)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth < ConstrStartMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth == ConstrStartMonth) && (ProjEndDate < ConstrStartDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        function ProjectDateCheckToday(oSrc, args) {
            var now = new Date();
            dtMeeting = GetCalDate('<%=dpSurveyDate.ClientID%>');

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
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtStakeholder.ClientID  %>");
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
