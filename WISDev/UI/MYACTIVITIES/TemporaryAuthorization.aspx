<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TemporaryAuthorization.aspx.cs" Inherits="WIS.UI.MYACTIVITIES.TemporaryAuthorization" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Panel ID="pnlMyAuthorizationApprovel" runat="server">
        <fieldset class="icePnlinner">
            <legend>Temporary Authorization</legend>
            <table border="0" width="60%" align="center">
                <tr>
                    <td align="left">
                        <label class="iceLable">Assign To </label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="iceDropDown" Style="width: 190px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqddlAssignTo" runat="server" ErrorMessage="Select Assign To User"
                            InitialValue="0" ControlToValidate="ddlAssignTo" Display="None" ValidationGroup="VsTempAuth"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">From </label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                                        <asp:TextBox ID="dptxtFrom" runat="server" Width="90px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqfromdate" runat="server" ErrorMessage="Select From Date"
                         ControlToValidate="dptxtFrom" Display="None" ValidationGroup="VsTempAuth"></asp:RequiredFieldValidator>
<ajaxToolkit:CalendarExtender ID="CaldptxtFrom" CssClass="WISCalendarStyle" runat="server" TargetControlID="dptxtFrom"></ajaxToolkit:CalendarExtender>

                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">To </label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                    <asp:TextBox ID="dptxtTo" runat="server" Width="90px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqTodate" runat="server" ErrorMessage="Select To Date"
                            ControlToValidate="dptxtTo" Display="None" ValidationGroup="VsTempAuth"></asp:RequiredFieldValidator>
<ajaxToolkit:CalendarExtender ID="caldptxtTo" CssClass="WISCalendarStyle" runat="server" TargetControlID="dptxtTo"></ajaxToolkit:CalendarExtender>
                  
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dptxtTo"
                            ClientValidationFunction="CheckDate" ErrorMessage="To Date cannot be lesser than From Date"
                            ValidationGroup="VsTempAuth" Display="None"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">Remarks</label>
                    </td>
                    <td align="left" style="vertical-align: top" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="iceTextBox" TextMode="MultiLine"
                            MaxLength="500" Style="width: 400px">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td colspan="4">
                        <div style="margin-top: 12px;">
                            <asp:ValidationSummary ID="valsumTempAuth" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsTempAuth" runat="server" />
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                ValidationGroup="VsTempAuth" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            <asp:Label ID="msgsaveLabel" runat="server" CssClass="iceLable"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset class="icePnlinner">
            <legend>Temporary Authorizations</legend>
            <asp:GridView ID="grdTempAuth" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdTempAuth_RowCommand"
                PageSize="10" AllowPaging="True" OnPageIndexChanging="grdTempAuth_PageIndexChanging"
                OnRowDataBound="grdTempAuth_RowDataBound">
                <RowStyle CssClass="gridRowStyle" />
                <AlternatingRowStyle CssClass="gridAlternateRow" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AuthoriserName" HeaderText="Authoriser" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Assigned To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="15%" DataField="AssignedTo" />
                    <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <ItemStyle HorizontalAlign="Left" Width="80px"/>
                        <ItemTemplate>
                            <asp:Label ID="lblFromDate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <ItemStyle HorizontalAlign="Left" Width="80px"/>
                        <ItemTemplate>
                            <asp:Label ID="lblToDate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                         DataField="Remarks" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                                CommandArgument='<%#Eval("APPROVALTEMPAUTHORISERID") %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Obsolete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>                        
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("Isdeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                            <%--<asp:ImageButton ImageUrl="~/Image/delete.gif" runat="server" ID="ImgDelete" CommandName="DeleteRow"
                                CommandArgument='<%#Eval("APPROVALTEMPAUTHORISERID") %>' OnClientClick="return DeleteRecord();" />--%>
                            <asp:Literal ID="litLineTypeID" Text='<%#Eval("APPROVALTEMPAUTHORISERID") %>' Visible="false"
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                        <HeaderStyle Width="5%"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=dptxtFrom.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dptxtTo.ClientID%>'));

        function CheckDate(oSrc, args) {
            dtProjectStart = GetCalDate('<%=dptxtFrom.ClientID%>');
            dtProjectEnd = GetCalDate('<%=dptxtTo.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

            if (ProjStartYear > ProjEndYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth > ProjEndMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ProjEndYear) && (ProjStartMonth == ProjEndMonth) && (ProjStartDate > ProjEndDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    </script>
</asp:Content>
