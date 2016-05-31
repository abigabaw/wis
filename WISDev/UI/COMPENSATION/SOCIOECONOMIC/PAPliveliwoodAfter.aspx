<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PAPliveliwoodAfter.aspx.cs" Inherits="WIS.UI.COMPENSATION.SOCIOECONOMIC.PAPliveliwoodAfter" %>

<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <fieldset class="icePnlinner">
        <legend>Livelihood Details</legend>
        <center>
            <table>
                <tr>
                    <td align="left" class="iceLable">
                        Captured Date <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="dpCapturedDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="cpCapturedDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="dpCapturedDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpCapturedDate"
                            ClientValidationFunction="CheckCapturedDate" ErrorMessage="Captured Date should not be greater than Today's Date"
                            ValidationGroup="HHDetails" Display="None">
                        </asp:CustomValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="dpCapturedDate"
                            ErrorMessage="Enter Captured Date" Display="None" ValidationGroup="HHDetails"
                            runat="server"></asp:RequiredFieldValidator>
                     <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="HHDetails" runat="server" />  
                    </td>                   
                </tr>
            </table>
        </center>
        <asp:GridView ID="grdLivelihoodItems" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" ShowFooter="true"
            Width="100%" OnRowDataBound="grdLivelihoodItems_RowDataBound">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                        <asp:Literal ID="litItemID" Text='<%#Eval("LivelihoodItemID") %>' runat="server" Visible="false"></asp:Literal>
                        <asp:Literal ID="litID" Text='<%#Eval("LID") %>' runat="server" Visible="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemStyle HorizontalAlign="Left" Width="43%" />
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ITEMNAME") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        Total Cash :
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cash (USH)">
                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                    <FooterStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:TextBox ID="txtCash" runat="server" MaxLength="14" style="text-align:right" Text="0"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCash" FilterType="Numbers,Custom" ValidChars=","
                            TargetControlID="txtCash" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTotalCash" Text="" ReadOnly="true" runat="server" ForeColor="Black" Font-Bold="true" style="text-align:right"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Kind">
                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                    <ItemTemplate>
                        <asp:TextBox ID="txtInKind" runat="server" MaxLength="300" Text="NONE"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteInKind" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                            ValidChars=",; - '" TargetControlID="txtInKind" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <center>
            <table width="100%">
                <tr>
                  <td align="center">
                        <asp:Label ID="StatusPAPLivehood" runat="server" Style="text-decoration: blink; color: Red;
                    font-family: Arial; font-size: 18px; font-weight: bold" />
                    &nbsp; 
                      <asp:Button ID="lnkPAPLiveHood" runat="server" Text="Change Request" Width="120px"
                    CssClass="icebutton" Visible="false" />
                     &nbsp;
                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" ValidationGroup="HHDetails" runat="server" OnClick="btnSave_Click" />
                     &nbsp; 
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </fieldset>
    
    <div>
        <asp:GridView ID="grdPAPLiveTotal" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdLivelihoodItems_RowCommand"
             AllowPaging="True" OnRowDataBound="grdPAPLiveTotal_RowDataBound">
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
                <asp:BoundField DataField="HouseHoldID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Cash" HeaderText="Total Cash" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Captured Date" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal ID="litCAPTUREDDATEDate" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CAPTUREDDATE") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CAPTUREDDATE") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="PapLDate" Text='<%#Eval("CAPTUREDDATE") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
      </div>
    <script language="javascript" type="text/javascript">
        function CalculateTotalCash(src) {
            fldCurrItemID = src.id;
            var val = src.value.replace(/,?/g, "");
            if (val == "") {
                val = 0;
            }
            var amount = 0;
            if (!isNaN(val))
                amount = val;
            else
                amount = 0;
            document.getElementById(src.id).value = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

            elems = document.getElementsByTagName('input');
            totalCash = 0;

            for (i = 0; i < elems.length; i++) {
                elem = elems[i];
                if (elem.type == 'text' && elem.id.indexOf('txtCash') >= 0) {
                    if (elem.value != "") totalCash += parseFloat(elem.value.toString().replace(/,?/g, ""));
                }
            }

            document.getElementById('MainContent_grdLivelihoodItems_txtTotalCash').value = totalCash.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
        //        document.getElementById('divAll').onclick = function () {
        //            isDirty = 0;
        //            setTimeout(function () { setDirtyText(); }, 100);
        //        };
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }   

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                //isDirty = 2;
                return '';
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
    </script>
</asp:Content>

