<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CDAPBudgetItem.aspx.cs" Inherits="WIS.CDAPBudgetItem" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 CDAP Budget Item Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 amalesh.t
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="divAll">
 <asp:Panel ID="pnlSave" Visible="true" runat="server">
 <fieldset class="icePnlinner">
 <legend>CDAP Budget Item</legend>
    <table align="center" border="0" width="40%">
        <tr>
            <td width="30%">
                <label class="iceLable">Budget Item</label><span class="mandatory">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtBudgetItem" runat="server" MaxLength="100" Width="300px" CssClass="iceTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqTribe" runat="server" ErrorMessage="Enter Budget Item"
                    Display="None" ControlToValidate="txtBudgetItem" ValidationGroup="BudgetItem"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="padding-top:12px">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                    ValidationGroup="BudgetItem" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="BudgetItem" runat="server" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
    </fieldset>
    </asp:Panel>
    <asp:GridView ID="grdBudgetItem" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBudgetItem_RowCommand"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage" OnRowDataBound="grdBudgetItem_RowDataBound">
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
            <asp:BoundField DataField="CategoryName" HeaderText="Budget Item" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Description">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <a id="lnkSubCategory" href="#" runat="server">View</a>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Clans" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemTemplate>
                                        <asp:Button ID="ViewPage" ImageAlign="AbsMiddle" Text="View" CommandName="ViewROW" CommandArgument='<%#Eval("TRIBEID")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="8%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("CategoryID")%>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("CategoryID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litCategoryID" Text='<%#Eval("CategoryID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
    <script language="javascript" type="text/javascript">
        function OpenSubCategories(categoryID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('CDAPBudgetItemDescr.aspx?id=' + categoryID, 'Clans', 'width=800px,height=650px,top=' + top + ', left=' + left);
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
            var tat1 = document.getElementById("<%= txtBudgetItem.ClientID  %>");
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
