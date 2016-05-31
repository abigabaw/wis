<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherFixturesMenu.ascx.cs" Inherits="WIS.OtherFixturesMenu" %>
<asp:Menu ID="NavigationFixturesSubMenu" runat="server" CssClass="menuSub" EnableViewState="false"
    StaticSelectedStyle-BackColor="Aqua" StaticSelectedStyle-ForeColor="Black" IncludeStyleBlock="true" Orientation="Horizontal" Width="90%">
    <Items>
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Grave.aspx" Text="&nbsp;Grave&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Fence.aspx" Text="&nbsp;Fence&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/OtherFixtures.aspx" Text="&nbsp;Others&nbsp;" />
    </Items>
</asp:Menu>