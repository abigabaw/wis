<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PublicConsultationandDisclosure .aspx.cs" Inherits="WIS.PublicConsultationandDisclosure" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
</asp:Content>
<%--/**
 * 
 * @version		 Public Consultation and Disclosure  UI screen   
 * @package		  Public Consultation and Disclosure 
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  09-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
        <div style="width: 100%">
            <fieldset class="icePnlinner">
                <legend>Public Consultation and Disclosure Details</legend>
                <table border="0" style="width: 100%" cellpadding="3" cellspacing="0">
                    <tr>
                        <td width="15%">
                            <label class="iceLable">
                                Date</label>
                            <span class="mandatory">*</span>
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="txtConsultationDate" runat="server" CssClass="iceTextBox" Width="90px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calConsultationDate" runat="server" CssClass="WISCalendarStyle"
                                TargetControlID="txtConsultationDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtConsultationDate"
                                ErrorMessage="Select Consultation Date" Display="None" ValidationGroup="PCDD"
                                runat="server"></asp:RequiredFieldValidator>
                            <%-- <asp:CustomValidator ID="cvConsultationDate1" runat="server" ControlToValidate="txtConsultationDate"
                                ClientValidationFunction="CheckDOB" ErrorMessage="Date Damaged should not be greater than Today's Date"
                                ValidationGroup="PCDD" Display="None">
                            </asp:CustomValidator>--%>
                            <%--  <asp:CustomValidator ID="cvConsultationDate2" runat="server" ControlToValidate="txtConsultationDate"
                                ClientValidationFunction="CheckConstrSatrtDate" ErrorMessage="Date Damaged cannot be before Project Start Date."
                                ValidationGroup="PCDD" Display="None"></asp:CustomValidator>--%>
                            <asp:HiddenField ID="hfProjStartDate" runat="server" Value="0" />
                            <asp:HiddenField ID="hfProjEndDate" runat="server" Value="0" />
                        </td>
                        <td width="15%">
                            <label class="iceLable">
                                District</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="200px" AutoPostBack="True"
                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlDistrict"
                                PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                IsSorted="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlDistrict"
                                InitialValue="0" ErrorMessage="Select a District" Display="None" ValidationGroup="PCDD"
                                runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="iceLable">
                                County</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="uplCounty" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlCounty" runat="server" Width="200px" AutoPostBack="True"
                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlCounty"
                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                        IsSorted="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCounty"
                                        InitialValue="0" ErrorMessage="Select a County" Display="None" ValidationGroup="PCDD"
                                        runat="server"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <label class="iceLable">
                                Sub County</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSubCounty" runat="server" Width="200px" AutoPostBack="True"
                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender8" runat="server" TargetControlID="ddlSubCounty"
                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                        IsSorted="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSubCounty"
                                        InitialValue="0" ErrorMessage="Select a Sub County" Display="None" ValidationGroup="PCDD"
                                        runat="server"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="iceLable">
                                Parish</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlParish" runat="server" Width="200px" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender9" runat="server" TargetControlID="ddlParish"
                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                        IsSorted="true" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlParish"
                                        InitialValue="0" ErrorMessage="Select a Parish" Display="None" ValidationGroup="PCDD"
                                        runat="server"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <label class="iceLable">
                                Village</label><span class="mandatory">*</span>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlVillage"
                                        InitialValue="0" ErrorMessage="Select a Village" Display="None" ValidationGroup="PCDD"
                                        runat="server"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <label class="iceLable">
                                Name of the Person/Group<span class="mandatory">*</span></label>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtbxNameofthePersonGroup" runat="server" CssClass="iceTextBox"
                                MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtbxNameofthePersonGroup"
                                ErrorMessage="Enter Name of the Person" Display="None" ValidationGroup="PCDD"
                                runat="server"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ,-/" TargetControlID="txtbxNameofthePersonGroup" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td valign="top">
                            <label class="iceLable">
                                Address
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxAddress" TextMode="MultiLine" runat="server" CssClass="iceTextBox"
                                MaxLength="500" Rows="3" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="iceLable">
                                Telephone Contact</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxTelephoneContact" runat="server" CssClass="iceTextBox" MaxLength="30"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="Numbers,Custom"
                                ValidChars="+() " TargetControlID="txtbxTelephoneContact" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <label class="iceLable">
                                Category of Stakeholding</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxCategoryofStakeholding" runat="server" CssClass="iceTextBox"
                                MaxLength="10"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                ValidChars=",-/" TargetControlID="txtbxCategoryofStakeholding" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="iceLable">
                                Officer in Charge</label><span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOfficerInCharge" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlOfficerInCharge"
                                InitialValue="0" ErrorMessage="Select a Officer in charge" Display="None" ValidationGroup="PCDD"
                                runat="server"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label class="iceLable">
                                Purpose of Meeting
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxPurposeofMeeting" runat="server" MaxLength="200"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ,/-" TargetControlID="txtbxPurposeofMeeting" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <label class="iceLable">
                                Issues Arising</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxIssuesArising" CssClass="iceTextBox" runat="server" TextMode="MultiLine"
                                MaxLength="2000" Rows="4" Width="96%"> </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ,/-" TargetControlID="txtbxIssuesArising" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td valign="top">
                            <label class="iceLable">
                                Proposed Remedies/ Action Items
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtbxProposedRem" CssClass="iceTextBox" runat="server" TextMode="MultiLine"
                                MaxLength="2000" Rows="4" Width="96%"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ,/-" TargetControlID="txtbxProposedRem" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td colspan="4">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" ValidationGroup="PCDD"
                                    runat="server" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="PCDD" runat="server" />
                                <%--/**
 * 
 * @version		 Public Consultation and Disclosure  UI screen   
 * @package		  Public Consultation and Disclosure 
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  09-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
                                --%>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="p1Grid" runat="server" Height="100%">
                    <table border="0" width="100%" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="grdPublicConsultation" runat="server" CssClass="gridStyle" CellPadding="4"
                                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" AllowPaging="True"
                                    PageSize="10" OnRowCommand="PublicConsultation_RowCommand" OnRowDataBound="grdPublicConsultation_RowDataBound"
                                    OnPageIndexChanging="ChangePage">
                                    <RowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="gridHeaderStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemStyle HorizontalAlign="Center" Width="35px" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lnkConsultationDate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="District" HeaderText="District" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="County" HeaderText="County" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="SubCounty" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="Parish" HeaderText="Parish" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="Village" HeaderText="Village" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="NameOfPerson" HeaderText="Name of the Person/Group " HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="250px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="Telephone" HeaderText="Telephone Contact" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="StakeholdingCategory" HeaderText="Category of Stakeholding"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="PURPOSEOFMEETING" HeaderText="Purpose of Meeting " HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="ISSUES" HeaderText="Issues Arising " HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="250px" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="REMEDIES" HeaderText="Proposed Remedies/Action Items"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                                        <asp:BoundField DataField="OfficerInchargeName" HeaderText="Officer in Charge" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                                    CommandName="EditRow" CommandArgument='<%#Eval("CONSULTATIONID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Left" />
                                    <EmptyDataTemplate>
                                        There are no records.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=txtConsultationDate.ClientID%>'));

        function CheckDOB(oSrc, args) {
            var now = new Date();
            // alert('CheckDOB()');
            dtMeeting = GetCalDate('<%=txtConsultationDate.ClientID%>');

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

        function CheckConstrSatrtDate(oSrc, args) {
            //            alert('CheckConstrSatrtDate()');
            dtProjectStart = document.getElementById('<%=hfProjStartDate.ClientID %>').value;
            dtProjectEnd = document.getElementById('<%=hfProjEndDate.ClientID %>').value;
            dtConstrStart = GetCalDate('<%=txtConsultationDate.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

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

        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
        }
        document.getElementById('divAll').onclick = function () {
            isDirty = 0;
            setTimeout(function () { setDirtyText(); }, 100);
        };

        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtbxNameofthePersonGroup.ClientID  %>");
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
