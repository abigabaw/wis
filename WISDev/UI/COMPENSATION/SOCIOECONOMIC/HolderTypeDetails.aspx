<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolderTypeDetails.aspx.cs"
    Inherits="WIS.HolderTypeDetails" MasterPageFile="~/SitePopup.Master" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
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
    <script type="text/javascript">
        var width = 760;
        var height = 600;
        window.resizeTo(width, height);
        window.onresize = function () { window.resizeTo(width, height); } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
    <fieldset class="icePnlinner" style="width: 98%; margin-left: -5px;">
        <legend>Householder Type Details</legend>
        <table align="left" border="0" id="table1">
            <tr>
                <td colspan="4">
                    <div style="width: 90%; height: 25px; float: right">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td align="right" style="width: 180px">
                                    <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 18%">
                    <label class="iceLable">
                        Holder Name</label>
                    <span class="mandatory">*</span>
                </td>
                <td >
                    <asp:TextBox ID="txtHolderName" MaxLength="150" runat="server" class="iceTextBox" Width="220">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqHolderName" ControlToValidate="txtHolderName"
                        ErrorMessage="Enter Holder Name" Display="None" ValidationGroup="Holder" runat="server">
                    </asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteHolderName" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtHolderName" runat="server"/>
                </td>
                <td align="left" style="width: 25%">
                    <label class="iceLable">
                        Date of Birth</label>
                </td>
                <td>
                    <asp:TextBox ID="dpDateOfBirth" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpDateOfBirth"></ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpDateOfBirth"
                        ClientValidationFunction="CheckDOB" ErrorMessage="Date of Birth should not be greater than Today's Date"
                        ValidationGroup="Holder" Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpDateOfBirth"
                        ClientValidationFunction="CheckDOBForChild" ErrorMessage="Holder age should be under 18 years."
                        ValidationGroup="Holder" Display="None">
                    </asp:CustomValidator>
                      <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="dpDateOfBirth"
                        ClientValidationFunction="CheckDOBForChildNU" ErrorMessage="Holder age should be greater than 18 years."
                        ValidationGroup="Holder" Display="None">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="dpDateOfBirth"
                        ClientValidationFunction="CompareDOBForChild" ErrorMessage="Holder age should be less than the PAP Holder age."
                        ValidationGroup="Holder" Display="None">
                    </asp:CustomValidator>
                    <asp:HiddenField ID="hfDateCheck" runat="server" ClientIDMode="Static" Value="false" />
                    <asp:HiddenField ID="hfPapDOB" runat="server" ClientIDMode="Static" Value="false" />
                </td>
            </tr>
            <tr>
                <td align="left"><label class="iceLable">Current School Status</label></td>
                <td align="left">
                    <asp:DropDownList ID="ddlCurrentSchoolStatus" CssClass="iceDropDown" AppendDataBoundItems="true" Width="220" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlCurrentSchoolStatus"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                </td>
                <td align="left"><label class="iceLable">Reason for Never Attending School</label></td>
                <td align="left">
                    <asp:DropDownList ID="ddlNeverAttendedSchool" CssClass="iceDropDown" AppendDataBoundItems="true" Width="220" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlNeverAttendedSchool"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                </td>
            </tr>
            <tr>
                <td align="left"><label class="iceLable">School Drop Reason</label></td>
                <td align="left">
                    <asp:DropDownList ID="ddlSchoolDropReason" CssClass="iceDropDown" AppendDataBoundItems="true" Width="220" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="ddlSchoolDropReason"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Literacy Level</label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlLiteracyLevel" CssClass="iceDropDown" AppendDataBoundItems="true" Width="220"
                        runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlLiteracyLevel"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Reside on Affected Land</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkResideOnAffectedLand" runat="server" />&nbsp;<label class="labelSuffix"
                        style="text-align: right">(Check if YES)</label>
                </td>
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
                        InitialValue="0" ErrorMessage="Select Sex" Display="None" ValidationGroup="Holder"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="lnkHolderType" runat="server" Text="Change Request" Width="120px"
                    CssClass="icebutton" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSaveHolder" Text="Save" runat="server" class="icebutton" ValidationGroup="Holder"
                                    OnClick="btnSaveHolder_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearHolder" runat="server" Text="Clear" class="icebutton" OnClick="btnClearHolder_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" Text="Close" class="icebutton" OnClick="btnClose_Click" />
                    <%--<input type="button" id="btnClose" value="Close" class="icebutton" onclick="RefreshRelationsList();" />--%>
                    <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Holder" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="StatusHolderType" runat="server" Style="text-decoration: blink; color: Red;
                        font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
    </fieldset>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" HorizontalAlign="Center" Height="100%">
    <asp:GridView ID="grdHolders" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="True"
        OnRowCommand="grdHolders_RowCommand" 
        onrowdatabound="grdHolders_RowDataBound" 
        onpageindexchanging="grdHolders_PageIndexChanging">
        <rowstyle cssclass="gridRowStyle" />
        <pagerstyle cssclass="gridPagerStyle" horizontalalign="Center" ForeColor="White" />
        <headerstyle cssclass="gridHeaderStyle" />
        <columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="4%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="HolderName" HeaderText="Holder Name" ItemStyle-Wrap="true" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Date of Birth" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:Literal ID="litDateOfBirth" Text="" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Sex" HeaderText="Gender" HeaderStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="CurrentSchoolStatus" HeaderText="Current School Status" HeaderStyle-HorizontalAlign="Center"
                />
            <asp:BoundField DataField="NeverAttendedSchool" HeaderText="Never Attended School" HeaderStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="SchoolDropReason" HeaderText="School Drop Reason" HeaderStyle-HorizontalAlign="Center"
                />
            <asp:BoundField DataField="LiteracyStatus" HeaderText="Literacy Level" HeaderStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="ResideOnAffectedLand" HeaderText="Reside on Affected Land"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="3%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("RelationID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("RelationID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </columns>
    </asp:GridView>
    </asp:Panel>
    </div>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('MainContent_dpDateOfBirth'));

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(670).toString() + "px";
        }

        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

        window.onbeforeunload = function doCleanup() {
            opener.RefreshHouseholdRelationsList();
        }

        function RefreshRelationsList() {
            setDirtyText();
            if (opener) {
                window.opener.location.replace(window.opener.location.pathname);
                window.close();
            }
        }


        function CompareDOBForChild(oSrc, args) {
            var now = document.getElementById('<%=hfPapDOB.ClientID %>').value;
            dtDOB = GetCalDate('<%=dpDateOfBirth.ClientID%>');

            if (now.toString() == 'false') {
                args.IsValid = true;
            }
            else {
                var ArrDOB = dtDOB.split("-");
                var DOBDt = ArrDOB[0];
                var DOBMonth = GetMonthNumber(ArrDOB[1]);
                var DOBYear = ArrDOB[2];

                var ArrPap = now.split("-");
                var DOBDtpap = ArrPap[0];
                var DOBMonthpap = GetMonthNumber(ArrPap[1]);
                var DOBYearpap = ArrPap[2];

                if (DOBYear < DOBYearpap) {
                    args.IsValid = false;
                    return;
                }
                else if ((DOBYear == DOBYearpap) && (DOBMonth < DOBMonthpap)) {
                    args.IsValid = false;
                    return;
                }
                else if ((DOBYear == DOBYearpap) && (CurrentMonth == DOBMonthpap) && (DOBDt <= DOBDtpap)) {
                    args.IsValid = false;
                    return;
                }

                args.IsValid = true;
            }
        }
        function CheckDOBForChildNU(oSrc, args) {
            var now = new Date();
            dtDOB = GetCalDate('<%=dpDateOfBirth.ClientID%>');

            var CurrentMonth = (now.getMonth() + 1);
            var CurrentDate = now.getDate();
            var CurrentYear = now.getFullYear();

            if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
            if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

            var ArrDOB = dtDOB.split("-");
            var DOBDt = ArrDOB[0];
            var DOBMonth = GetMonthNumber(ArrDOB[1]);
            var DOBYear = ArrDOB[2];
            var chkdate = document.getElementById('<%=hfDateCheck.ClientID %>').value;
            var validyear = 0;
        
        if (chkdate.toString() == 'true') {
                validyear = parseInt(CurrentYear) - parseInt(DOBYear);             
              
          }
            if (validyear < 18) {
                args.IsValid = false;
               
                return;
            }
            else {

                args.IsValid = true;
            }
        }   

        function CheckDOBForChild(oSrc, args) {
            var now = new Date();
            dtDOB = GetCalDate('<%=dpDateOfBirth.ClientID%>');

            var CurrentMonth = (now.getMonth() + 1);
            var CurrentDate = now.getDate();
            var CurrentYear = now.getFullYear();

            if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
            if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

            var ArrDOB = dtDOB.split("-");
            var DOBDt = ArrDOB[0];
            var DOBMonth = GetMonthNumber(ArrDOB[1]);
            var DOBYear = ArrDOB[2];
            var chkdate = document.getElementById('<%=hfDateCheck.ClientID %>').value;
            var validyear = 0;

            if (chkdate.toString() == 'true') {
                validyear = parseInt(CurrentYear) - 18;
            }

            if (DOBYear < validyear) {
                args.IsValid = false;
                return;
            }
            else if ((DOBYear == validyear) && (DOBMonth < CurrentMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((DOBYear == validyear) && (CurrentMonth == DOBMonth) && (DOBDt <= CurrentDate)) {
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

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
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
            var btn = document.getElementById("<%= btnSaveHolder.ClientID  %>");
            var tat1 = document.getElementById("<%= txtHolderName.ClientID  %>");
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
