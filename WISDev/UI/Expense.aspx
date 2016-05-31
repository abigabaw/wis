<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="Expense.aspx.cs" Inherits="WIS.Expense" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Consolidated Expense</legend>
            <table border="0" width="100%">
                <tr>
                    <td class="iceLable" style="width: 16%">
                        Expense Type <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtExpensetype" runat="server" Width="200px" MaxLength="50" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqBankNameSearch" runat="server" ErrorMessage=" Enter Expense Type"
                                ControlToValidate="txtExpensetype" Display="None" ValidationGroup="Expense"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fte1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                        ValidChars="()& " TargetControlID="txtExpensetype" runat="server" />
                    </td>
                    <td class="iceLable" style="width: 16%">
                        Account code <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAccountcode" runat="server" Width="200px" MaxLength="10"  CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" Enter Account code"
                                ControlToValidate="txtAccountcode" Display="None" ValidationGroup="Expense"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Numbers"
                        ValidChars=" " TargetControlID="txtAccountcode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="iceLable" style="width: 16%">
                        Expense Amount <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtExpenseAmt" runat="server" Width="200px" MaxLength="22" CssClass="iceTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" Enter Expense Amount"
                                ControlToValidate="txtExpenseAmt" Display="None" ValidationGroup="Expense"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers"
                        ValidChars=" " TargetControlID="txtExpenseAmt" runat="server" />
                    </td>
                    <td class="iceLable" style="width: 16%">
                        Date of Expense<span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="dpDateofexpense" runat="server" CssClass="iceTextBox" Width="90px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" Enter Date of Expense"
                                ControlToValidate="dpDateofexpense" Display="None" ValidationGroup="Expense"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="dpDateofexpense">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="padding-top: 12px">
                        <input type="button" id="Btn_ImportFromExcel" value="Import From Excel" class="icebutton"
                            onclick="UploadExpense()" style="width: 180px" runat="server"/>
                        <asp:Button ID="btnSaveExpence" Text="Save" runat="server" class="icebutton" OnClick="btnSaveExpence_Click"
                            ValidationGroup="Expense" />&nbsp;
                        <asp:Button ID="btnClearExpence" runat="server" Text="Clear" class="icebutton" OnClick="btnClearExpence_Click" />
                        <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Expense" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
         <%--   <asp:UpdatePanel ID="updExpense" UpdateMode="Conditional" runat="server">
                <ContentTemplate>--%>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grdExpense" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
            PageSize="10" OnPageIndexChanging="ChangePage" OnRowCommand="grdExpense_RowCommand" OnRowDataBound="grdExpense_RowDataBound">
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
                <asp:BoundField DataField="EXPENSETYPE" HeaderText="Expense Type" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ACCOUNTCODE" HeaderText="Account Code" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="EXPENSEAMOUNT" HeaderText="Expense Amount (USH)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Date Of Expense" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                    <ItemTemplate>
                        <asp:Literal ID="litExpenseDate" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("PROJECTEXPENSEID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PROJECTEXPENSEID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litBankID" Text='<%#Eval("PROJECTEXPENSEID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnLoadExpense" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel--%>
            <asp:Button ID="btnLoadExpense" Style="display: none" runat="server" OnClick="btnLoadExpense_Click" />
            <script language="javascript" type="text/javascript">
                function DeleteRecord() {
                    return confirm('Are you sure you want to Delete this Record?');
                }
            </script>
            <asp:Panel ID="pnlSave" runat="server" Visible="false">
                <table id="tblSave" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click"
                                ValidationGroup="Permanent" />
                            <asp:Button ID="btnCancel" CssClass="icebutton" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        <asp:HiddenField ID="hdnFilePath" ClientIDMode="Static" runat="server" />
    </div>
    <script language="javascript">
        function UploadExpense() {
            open('ExpensePopUp.aspx', 'expWin', 'width=800px,height=700px');
        }

        function SendFilePath(filePath) {
            document.getElementById('hdnFilePath').value = filePath;
            $get('<%=btnLoadExpense.ClientID%>').click();
        }

        function btn_clear_onclick() {

        }

    </script>
</asp:Content>
