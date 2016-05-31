<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="ExpenseAccount.aspx.cs" Inherits="WIS.ExpenseAccount" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
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
                <asp:TemplateField HeaderText="Last Updated Date" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                    <ItemTemplate>
                        <asp:Literal ID="litExpenseDate" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("PROJECTEXPENSEID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PROJECTEXPENSEID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litBankID" Text='<%#Eval("PROJECTEXPENSEID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>
    <script language="javascript">
        function UploadExpense() {
            open('ExpensePopUp.aspx', 'expWin', 'width=800px,height=700px');
        }

        function btn_clear_onclick() {

        }

    </script>
</asp:Content>

