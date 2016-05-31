<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="LivelihoodRestorationView.aspx.cs" Inherits="WIS.LivelihoodRestorationView" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <asp:GridView ID="grdLivelihoodItems" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" ShowFooter="true"
        Width="100%" OnRowDataBound="grdLivelihoodItems_RowDataBound">
        <RowStyle CssClass="gridRowStyle" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <EmptyDataTemplate>
            No Records Found
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                    <asp:Literal ID="litItemID" Text='<%#Eval("Itemid") %>' runat="server" Visible="false"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <%--   <asp:BoundField DataField="ITEMNAME" HeaderText="Item Description" HeaderStyle-HorizontalAlign="Left" />--%>
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
                    <asp:Literal ID="txtCash" Text="" runat="server"></asp:Literal>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="txtTotalCash" Text="" runat="server"></asp:Literal>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="In Kind">
                <ItemStyle HorizontalAlign="Center" Width="25%" />
                <ItemTemplate>
                    <asp:Literal ID="txtInKind" Text="" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
