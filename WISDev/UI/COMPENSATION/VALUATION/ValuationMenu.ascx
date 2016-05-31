﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValuationMenu.ascx.cs" Inherits="WIS.ValuationMenu" %>
<div id="dvSubMenuContainer" style="visibility:hidden">
<asp:Menu ID="NavigationSubMenu" runat="server" CssClass="menuSub" EnableViewState="false"
    StaticSelectedStyle-BackColor="Aqua" StaticSelectedStyle-ForeColor="Black" IncludeStyleBlock="true" Orientation="Horizontal" Width="90%">
    <Items>
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/PermanenetBuilding.aspx" Text="&nbsp;Building/Structure Details&nbsp;" />
        <%--<asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Non-perm_structure.aspx" Text="&nbsp;Non-Permanent Buildings&nbsp;" />--%>
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/DamagedCrops.aspx" Text="&nbsp;Damaged Crops&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Crops.aspx" Text="&nbsp;Crops&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Grave.aspx" Text="&nbsp;Other Fixtures&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/CulturProperties.aspx" Text="&nbsp;Culture Properties&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/AcreageValuation.aspx" Text="&nbsp;Acreage Valuation&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/Valuation/Final_valuation.aspx" Text="&nbsp;Final Valuation&nbsp;" />
    </Items>
</asp:Menu>
</div>