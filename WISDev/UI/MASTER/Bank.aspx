<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Bank.aspx.cs" Inherits="WIS.Bank" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Bank Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Eshwar
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
    <div>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <asp:Button ID="btnShowAdd" Text="Add New Bank" runat="server" CssClass="icebutton"
            OnClick="btnShowAdd_Click" />&nbsp;
        <asp:Button ID="btnShowSearch" Text="Search Bank" runat="server" CssClass="icebutton"
            OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch">
        <fieldset class="icePnlinner">
            <legend>Search Bank</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <asp:Label ID="Label1" runat="server" Text="Bank Name" CssClass="iceLable" />
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearchBankName" runat="server" CssClass="iceTextBox" 
                            MaxLength="150" Width="356px"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="reqBankNameSearch" runat="server" ErrorMessage=" Enter Bankname to search"
                                ControlToValidate="txtSearchBankName" Display="None" ValidationGroup="Bank"></asp:RequiredFieldValidator>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtSearchBankName" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                        <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                            OnClick="btnClearSearch_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlBankDetails" runat="server" DefaultButton="btnSave">
        <fieldset class="icePnlinner">
            <legend>Bank Details</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <label class="iceLable">
                            Bank Name</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="width: 30%">
                        <asp:TextBox ID="txtBankName" runat="server" class="iceTextBox" MaxLength="150" 
                            Width="354px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqBankName" runat="server" ErrorMessage=" Enter Bank Name"
                                ControlToValidate="txtBankName" Display="None" ValidationGroup="Bank"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteBankName" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtBankName" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                    <td align="left" style="width: 30%">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSave" Text="Save" runat="server" class="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="Bank" />&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="icebutton" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Bank" runat="server" />
  <asp:GridView ID="grdBanks" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBanks_RowCommand"
        PageSize="10" AllowPaging="True" OnPageIndexChanging="grdBanks_PageIndexChanging" OnRowDataBound="grdBanks_RowDataBound">
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
         
           
            <asp:BoundField DataField="BankName" HeaderText="Bank Name" HeaderStyle-HorizontalAlign="Left" />
                     <asp:TemplateField HeaderText="Branches">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <a id="lnkBank" href="#" runat="server">View</a>
                    </ItemTemplate>
</asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("BankID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                     
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("BankID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litBankID" Text='<%#Eval("BankID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div class="footer">
        <script language="javascript" type="text/javascript">

            function OpenBranchBank(BankID) {
                var left = (screen.width - 800) / 2;
                var top = (screen.height - 650) / 4;
                open('BankBranches.aspx?id=' + BankID, 'Bank', 'width=800px,height=650px,top=' + top + ', left=' + left);
            }

            function SetVisible(val) {
                var hf = document.getElementById("<%= hfVisible.ClientID  %>");
                hf.value = val;
            }

            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
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
                var tat1 = document.getElementById("<%= txtBankName.ClientID  %>");
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
    </div>
</asp:Content>
