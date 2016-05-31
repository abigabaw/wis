<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LivBudgCategory.aspx.cs" Inherits="WIS.UI.MASTER.LivBudgCategory" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 LivBudg Category Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Sunil Kumar
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
<asp:Panel ID="pnlSave" Visible="true" runat="server">
<fieldset class="icePnlinner">
 <legend>Liv Budget Category</legend>
   <table align="center" border="0" width="50%">
        <tr>
            <td width="30%">
                <label class="iceLable">Budget Category</label><span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtBudgetCategory" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Budget Category"
                    Display="None" ControlToValidate="txtBudgetCategory" ValidationGroup="BudgetCategory"></asp:RequiredFieldValidator>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtBudgetCategory" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="BudgetCategory" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="BudgetCategory" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
    </asp:Panel>
    <asp:GridView ID="grdBudgetCategory" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBudgetCategory_RowCommand"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdBudgetCategory_RowDataBound">
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
            <asp:BoundField DataField="LIV_BUD_CATEGORYNAME" HeaderText="Budget Category" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Items">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <a id="lnkSubCategory" href="#" runat="server">View</a>
                </ItemTemplate>
            </asp:TemplateField>
        
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("lIV_BUD_CATEGID")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("lIV_BUD_CATEGID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litCategoryID" Text='<%#Eval("lIV_BUD_CATEGID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

    <script language="javascript" type="text/javascript">
        function OpenSubCategories(lIV_BUD_CATEGID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('LivBudgItem.aspx?id=' + lIV_BUD_CATEGID, 'Clans', 'width=800px,height=650px,top=' + top + ', left=' + left);
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
            var tat1 = document.getElementById("<%= txtBudgetCategory.ClientID  %>");
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
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
