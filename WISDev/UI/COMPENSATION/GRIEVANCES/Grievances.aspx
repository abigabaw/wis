<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="Grievances.aspx.cs" Inherits="WIS.Grievances1" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%">
        <table border="0" style="width: 100%">
            <tr>
                <td align="left" width="350px">
                    <label class="iceLable">
                        District:
                        <asp:Label ID="districtTextBox" runat="server" CssClass="labelSuffix" />
                    </label>
                </td>
                <td align="left" width="350px">
                    <label class="iceLable">
                        County:
                    </label>
                    <asp:Label ID="countyTextBox" runat="server" CssClass="labelSuffix" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Sub County:
                    </label>
                    <asp:Label ID="subcountyTextBox" runat="server" CssClass="labelSuffix" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Parish:
                    </label>
                    <asp:Label ID="parishTextBox" runat="server" CssClass="labelSuffix" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Village:
                    </label>
                    <asp:Label ID="villageTextBox" runat="server" CssClass="labelSuffix" />
                </td>
            </tr>
        </table>
    </div>
    <fieldset class="icePnlinner">
        <legend>Grievance Details</legend>
        <div style="text-align: right;">
            <a id="lnkUploadDoc" href="#" visible="false" runat="server"><b>Upload Supporting Document</b></a>&nbsp;|&nbsp;
            <a id="lnkViewUploadDoc" href="#" visible="false" runat="server"><b>View Documents</b></a>
        </div>
        <table border="0" cellpadding="3" width="100%">
            <tr>
                <td align="left" style="width: 80px">
                    <label class="iceLable">
                        Category</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="categoryDropDownList" runat="server" CssClass="iceDropDown"
                        AppendDataBoundItems="true" Width="200px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                        ErrorMessage="Select a Category" ValidationGroup="Grievance" InitialValue="0"
                        ControlToValidate="categoryDropDownList"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Description</label>
                </td>
                <td align="left" style="vertical-align: top">
                    <asp:TextBox ID="descriptionTextBox" CssClass="iceTextBox" runat="server" TextMode="MultiLine" MaxLength="999"
                        Width="750px" Rows="7" onkeydown="return checkMaxLength(this,999)"></asp:TextBox>
                    <br /><label style="color:Red;">(Max 1000 characters)</label>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="icePnlinner">
        <legend>Action Taken </legend>
        <table border="0" width="100%">
            <tr>
                <td align="left" valign="top">
                    <label class="iceLable">Action Taken</label>
                </td>
                <td align="left" valign="top" colspan="3">
                    <asp:TextBox ID="actionTextBox" runat="server" CssClass="iceTextBox" TextMode="MultiLine" MaxLength="499" Width="80%" Rows="5" />
                    <br /><label style="color:Red;">(Max 500 characters)</label>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                        ValidChars="- ,.()/" TargetControlID="actionTextBox" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="10%">
                    <label class="iceLable">Date</label>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="actionDatePicker" Width="90px" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calactionDatePicker" CssClass="WISCalendarStyle" runat="server" TargetControlID="actionDatePicker">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="actionDatePicker"
                        ClientValidationFunction="CheckDOB" ErrorMessage="Action Date should not be greater than Today's Date"
                        ValidationGroup="Grievance" Display="None">
                    </asp:CustomValidator>
                </td>
                <td align="left" width="10%">
                    <label class="iceLable">Action Taken By</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="actionDropDownList" runat="server">
                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>            
        </table>
    </fieldset>
    <div style="width: 100%" id="ResolutionStatusChange" runat="server">
        <fieldset class="icePnlinner">
            <legend>Resolution </legend>
            <table border="0" width="100%">
            <tr>
                <td align="left" valign="top">
                    <label class="iceLable">Basic Facts</label>
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="basicTextBox" runat="server" CssClass="iceTextBox" TextMode="MultiLine" Width="90%" Rows="5" />
                    <br /><label style="color:Red;">(Max 500 characters)</label>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                        ValidChars="- ,.()/" TargetControlID="basicTextBox" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left" valign="top">
                    <label class="iceLable">Resolution / Response</label>
                </td>
                <td align="left" valign="top" colspan="3">
                    <asp:TextBox ID="resolutionTextBox" runat="server" CssClass="iceTextBox" TextMode="MultiLine" Width="90%" Rows="5" />
                    <br /><label style="color:Red;">(Max 500 characters)</label>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                        ValidChars="- ,.()/" TargetControlID="resolutionTextBox" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" width="10%">
                    <label class="iceLable">Date</label>
                </td>
                <td align="left" width="40%">
                    <asp:TextBox ID="resolutionDatePicker" Width="90px" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calresolutionDatePicker" runat="server" CssClass="WISCalendarStyle"
                        TargetControlID="resolutionDatePicker">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidatorProjDate" runat="server" ControlToValidate="resolutionDatePicker"
                        ClientValidationFunction="CheckProjectDate" ErrorMessage="Resolution Date cannot be lesser than or equal to Action Taken Date"
                        ValidationGroup="Grievance" Display="None"></asp:CustomValidator>
                </td>
                <td align="left" width="12%">
                    <label class="iceLable">Resolved By</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="resolDropDownList" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>            
        </table>
        </fieldset>
    </div>
    <table align="center">
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td>
                            <a id="lnkGrievances" runat="server" href="#" runat="server" class="iceLinkButton"
                                style="text-decoration: none; color: White; font-family: Arial; font-size: 12px;
                                font-weight: normal; padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">
                                Change Request</a>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="Grievance"
                                OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="StatusGrievances" runat="server" Style="text-decoration: blink; color: Red;
                                font-family: Arial; font-size: 18px; font-weight: bold" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="msgsaveLabel" runat="server" CssClass="iceLable"></asp:Label>
                <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Grievance" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div>
        <fieldset class="icePnlinner">
            <legend>Grievances List </legend>
            <asp:GridView ID="grdgrvlist" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grievances_RowCommand"
                AllowPaging="True" OnPageIndexChanging="grdgrvlist_PageIndexChanging" OnRowDataBound="grdgrvlist_RowDataBound">
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
                    <asp:BoundField DataField="GrievCategory" HeaderText="Grievance Category" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Created Date" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                        <ItemTemplate>
                            <asp:Literal ID="litCreatedDate" Text="" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Complaint Description" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Resolution Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="14%">
                        <ItemTemplate>
                            <a id="lnkGrievanceClosure" href="#" visible="false" runat="server"><%#Eval("ResolutionStatus") %></a>
                            <asp:Literal ID="litResolutionStatus" Text='<%#Eval("ResolutionStatus") %>' runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grievance Closure">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <a id="lnkGravience" href="#" runat="server">Close</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("GrievanceID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("GrievanceID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                            <asp:Literal ID="PermanentStrucID" Text='<%#Eval("GrievanceID") %>' Visible="false"
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=actionDatePicker.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=resolutionDatePicker.ClientID%>'));

        function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode, DocumentID) {
            open('../../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode + '&DOCSERVICEID=' + DocumentID, 'UploadDocPop', 'width=800px,height=700px');
        }

        function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode, DocumentID) {
            open('../../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode + '&DOCSERVICEID=' + DocumentID, 'UploadDocPoplist', 'width=800px,height=700px');
        }

        function CheckProjectDate(oSrc, args) {
            dtProjectStart = GetCalDate('<%=actionDatePicker.ClientID%>');
            dtProjectEnd = GetCalDate('<%=resolutionDatePicker.ClientID%>');

            var ArrProjSt = dtProjectStart.split("/");
            var ProjStartMonth = ArrProjSt[0];
            var ProjStartDate = ArrProjSt[1];
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("/");
            var ProjEndMonth = ArrProjEnd[0];
            var ProjEndDate = ArrProjEnd[1];
            var ProjEndYear = ArrProjEnd[2];

            if (ProjStartYear > ProjEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth > ProjEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth == ProjEndMonth) && (ProjStartDate >= ProjEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
        function checkMaxLength(e, maxLength) {
            //onkeydown="return checkMaxLength(this,1000)"
            if (e.value.length > parseInt(maxLength)) {
                // Set value back to the first 6 characters 
                e.value = e.value.substring(0, parseInt(maxLength));
            }
            return true;
        }
        function OpenGravienceClosure(mode,GravienceID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('GrievanceClosure.aspx?mode=' + mode + '&id=' + GravienceID, 'Gravience', 'width=800px,height=650px,top=' + top + ', left=' + left);
        }
        function CheckDOB(oSrc, args) {
            var now = new Date();
            dtMeeting = GetCalDate('<%=actionDatePicker.ClientID%>');

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

        function SendApprovalEmail(workflowCode, ProjectID, userID, HHID, pageCode, grievanceID) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + workflowCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode + '&GrievanceID=' + grievanceID, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
