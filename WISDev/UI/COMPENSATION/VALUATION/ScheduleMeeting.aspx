<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="ScheduleMeeting.aspx.cs" Inherits="WIS.ScheduleMeeting" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
    <script language="javascript">
        PreventDateFieldEntry(document.getElementById('<%=AppntDate.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=NegotDate.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=MeetingDatePicker.ClientID%>'));

        function CheckProjectDate(oSrc, args) {
            dtAppointment = GetCalDate('<%=AppntDate.ClientID%>');
            dtNegotiation = GetCalDate('<%=NegotDate.ClientID%>');

            var ArrAppointmentDt = dtAppointment.split("-");
            var AppointmentDt = ArrAppointmentDt[0];
            var AppointmentMonth = GetMonthNumber(ArrAppointmentDt[1]);            
            var AppointmentYear = ArrAppointmentDt[2];
            
            var ArrNegotiationDt = dtNegotiation.split("-");
            var NegotiationDt = ArrNegotiationDt[0];
            var NegotiationMonth = GetMonthNumber(ArrNegotiationDt[1]);            
            var NegotiationYear = ArrNegotiationDt[2];

            if (AppointmentYear > NegotiationYear) {
                args.IsValid = false;
                return;
            }
            else if ((AppointmentYear == NegotiationYear) && (AppointmentMonth > NegotiationMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((AppointmentYear == NegotiationYear) && (AppointmentMonth == NegotiationMonth) && (AppointmentDt > NegotiationDt)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
        function CloseWindow() {
            this.window.close();
        }

        function CheckMeetingDate(oSrc, args) {
            var now = new Date();

            dtMeeting = GetCalDate('<%=MeetingDatePicker.ClientID%>');

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

    </script>
</asp:Content>
<%--/**
 * 
 * @version		 POPUP for cultural Properties UI  screen   
 * @package		  POPUP for cultural Properties 
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  07-05-2013
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="float:right">
        <table width="100%">
            <tr>
                <td align="left">
                    <input type="button" class="icebutton" value="Close" onclick="CloseWindow()" />
                </td>
            </tr>
        </table>
    </div><br /><br />
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Information about scheduled Negotiation Meetings</legend>
            <table border="0" width="100%">
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="CULTURALNEGOIDtxtbx" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Appointment Date</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                    <asp:TextBox ID="AppntDate" runat="server"  Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calAppntDate" CssClass="WISCalendarStyle" runat="server" TargetControlID="AppntDate">
                   </ajaxToolkit:CalendarExtender>                 
                      
                        <asp:RequiredFieldValidator ID="reqAppntDate" runat="server" ErrorMessage=" Select Appointment Date "
                            Display="None" ControlToValidate="AppntDate" ValidationGroup="ScheduleMeeting"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <label class="iceLable">
                            Venue for Negotiation</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxVenueforNegotiation" runat="server" CssClass="iceTextBox" MaxLength="200" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="VenueRequiredF" runat="server" ErrorMessage=" Enter Venue for negotiation "
                            Display="None" ControlToValidate="txtbxVenueforNegotiation" ValidationGroup="ScheduleMeeting"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top">
                        <label class="iceLable">
                            Negotiation Date</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td style="vertical-align:top">
                    <asp:TextBox ID="NegotDate" runat="server" Width="90px"></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="calNegotDate" CssClass="WISCalendarStyle"  runat="server" TargetControlID="NegotDate">
                 </ajaxToolkit:CalendarExtender>
                      
                        <asp:RequiredFieldValidator ID="RequiredFiel" runat="server" ErrorMessage=" Select Negotiation  Date"
                            Display="None" ControlToValidate="NegotDate" ValidationGroup="ScheduleMeeting"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidatorProjDate" runat="server" ControlToValidate="NegotDate"
                            ClientValidationFunction="CheckProjectDate" ErrorMessage="Negotiation Date cannot be lesser than Appointment Date"
                            ValidationGroup="ScheduleMeeting" Display="None"></asp:CustomValidator>
                    </td>
                    <td style="vertical-align:top">
                        <label class="iceLable">
                            Problems</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxProblems" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                            Rows="5" Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="VsTribe" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="ScheduleMeeting" runat="server" />
                            <asp:Button ID="ScheduledInformBtnSave" CssClass="icebutton" Text="Save" runat="server"
                                OnClick="ScheduledInformBtnSave_Click" ValidationGroup="ScheduleMeeting" />&nbsp;
                            <asp:Button ID="ScheduledInformBtnClear" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="ScheduledInformBtnClear_Click" />
                            <%--  <asp:Label ID="msgsavelbl1" runat="server" CssClass="iceLable" ></asp:Label>--%>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdScheduled" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdScheduled_RowCommand"
                OnSelectedIndexChanged="grdScheduled_SelectedIndexChanged" OnPageIndexChanging="ScheduleChangePage"
                AllowPaging="True" onrowdatabound="grdScheduled_RowDataBound">
                <RowStyle CssClass="gridRowStyle" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment Date" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Literal ID="litAppointmentDate" Text="" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NEGO_VENUE" HeaderText="Venue for Negotiation" HeaderStyle-HorizontalAlign="Center">
                        <%-- <HeaderStyle HorizontalAlign="Center"></HeaderStyle>--%>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Negotiation Date" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Literal ID="litNegotiationDate" Text="" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NEGO_PROBLEMDESC" HeaderText="Problems" HeaderStyle-HorizontalAlign="Center">
                        <%-- <HeaderStyle></HeaderStyle>--%>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("CULTURALNEGOID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Information about Meetings Conducted</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="CULTURALMEETIDtxtbx" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Date</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td style="width: 40%">
                    <asp:TextBox ID="MeetingDatePicker"  runat="server" Width="90px"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="calMeetingDatePicker" CssClass="WISCalendarStyle" runat="server" TargetControlID="MeetingDatePicker"></ajaxToolkit:CalendarExtender>

                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="MeetingDatePicker"
                            ClientValidationFunction="CheckMeetingDate" ErrorMessage="Meeting Date should not be greater than Today's Date"
                            ValidationGroup="Meeting" Display="None"></asp:CustomValidator>
                    </td>
                    <td>
                        <label class="iceLable">
                            Location</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxLocation" runat="server" CssClass="iceTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Meeting Purpose</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMeetingPurpose" runat="server" CssClass="iceTextBox" Width="150px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        </asp:DropDownList>
                          <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlMeetingPurpose"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" Select Meeting Purpose"
                            InitialValue="--Select--" Display="None" ControlToValidate="ddlMeetingPurpose"
                            ValidationGroup="Meeting"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <label class="iceLable">
                            Witness NGO</label><span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxWitnessNGO" runat="server" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" Enter witness NGO"
                            Display="None" ControlToValidate="txtbxWitnessNGO" ValidationGroup="Meeting"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Opinion Leader</label><span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxOpinionLeader" runat="server" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage=" Enter Opinion Leader"
                            Display="None" ControlToValidate="txtbxOpinionLeader" ValidationGroup="Meeting"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <label class="iceLable">
                            Ministry of GLSD
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxMinistryofGLSD" runat="server" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage=" Ministry of GLSD"
                            Display="None" ControlToValidate="txtbxMinistryofGLSD" ValidationGroup="Meeting"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                           UETCL Rep
                        </label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtbxAESRep" runat="server" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage=" Enter AES Rep"
                            Display="None" ControlToValidate="txtbxAESRep" ValidationGroup="Meeting"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <label class="iceLable">
                            MOU Signed
                        </label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkMOUSigned" runat="server" />
                        <%-- <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Red" ErrorMessage="Required" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Comments
                        </label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtbxComments" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                            Width="317px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Meeting" runat="server" />
                            <asp:Button ID="MeetingButton1" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="Meeting"
                                OnClick="MeetingBtnSave_Click" />&nbsp;
                            <asp:Button ID="MeetingButton2" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="MeetingInformBtnClear_Click" />
                            <%--<asp:Label ID="msgLabel" runat="server" CssClass="iceLable" ></asp:Label>--%>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdMeeting" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdMeeting_RowCommand"
                OnSelectedIndexChanged="grdMeeting_SelectedIndexChanged" OnPageIndexChanging="MeetingChangePage"
                AllowPaging="True" onrowdatabound="grdMeeting_RowDataBound">
                <RowStyle CssClass="gridRowStyle" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Literal ID="litMeetingDate" Text="" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MEETINGLOCATION" HeaderText="Location" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Meetingpurpose" HeaderText="Meeting Purpose" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="WITNESSNGO" HeaderText="Witness NGO " HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OPINIONLEADER" HeaderText="Opinion Leader " HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MINISTRYOFGLSD" HeaderText="Ministry of GLSD " HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="AESREP" HeaderText="AES Rep " HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MOUSIGNED" HeaderText="MOU Signed" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("CULTURALMEETID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
        <%--  <script>
            function ValidateCheckBox(sender, args) {
                if (document.getElementById("<%=ChkMOUSigned.ClientID %>").checked == true) {
                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }
            }

        </script>--%>
    </div>
</asp:Content>
