<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompSocioEconomyMenu.ascx.cs" Inherits="WIS.CompSocioEconomyMenu" %>
<div id="dvSubMenuContainer" style="visibility:hidden">
<asp:Menu ID="NavigationSubMenu" runat="server" CssClass="menuSub" EnableViewState="false"
    StaticSelectedStyle-BackColor="Aqua" StaticSelectedStyle-ForeColor="Black" IncludeStyleBlock="true" Orientation="Horizontal" Width="98%">
    <Items>
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/Household.aspx" Text="&nbsp;Household&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/HouseholdRelation.aspx" Text="&nbsp;HH Relations&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/PAPInfo.aspx" Text="&nbsp;Stakeholder Details&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/PAPLivelihood.aspx" Text="&nbsp;Livelihood&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/PAPHealth.aspx" Text="&nbsp;Health&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/Neighbours.aspx" Text="&nbsp;Neighbours&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/Welfare.aspx" Text="&nbsp;Welfare&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/Major_shocks.aspx" Text="&nbsp;Major Shocks&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/SocioConcerns.aspx" Text="&nbsp;Concerns&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/LandInfoRespondents.aspx" Text="&nbsp;Other Land Holdings&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/LandInfoRespondentsOn.aspx" Text="&nbsp;Living On Affected Land&nbsp;" />
        <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/LandInfoRespondentsOff.aspx" Text="&nbsp;Living Off Affected Land&nbsp;" />
        <%-- <asp:MenuItem NavigateUrl="~/UI/Compensation/SocioEconomic/AcreageValuation.aspx" Text="&nbsp;Acreage Valuation&nbsp;" /> --%>
    </Items>
</asp:Menu>
</div>