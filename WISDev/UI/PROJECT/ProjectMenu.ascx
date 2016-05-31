<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectMenu.ascx.cs" Inherits="WIS.ProjectMenu" %>
<asp:Menu ID="NavigationSubMenu" runat="server" CssClass="menuSub" EnableViewState="false"
    StaticSelectedStyle-BackColor="Aqua" StaticSelectedStyle-ForeColor="Black" IncludeStyleBlock="true" Orientation="Horizontal" Width="90%">
    <Items>
        <asp:MenuItem NavigateUrl="~/UI/Project/ProjectDetails.aspx" Text="&nbsp;Project Details&nbsp;" Value="PROJECT_DETAILS" />
        <asp:MenuItem NavigateUrl="~/UI/Project/ProjectRoute.aspx" Text="&nbsp;Route Info&nbsp;" Value="ROUTE_INFO" />
        <asp:MenuItem NavigateUrl="~/UI/Project/ProjectGeography.aspx" Text="&nbsp;Geographical Info&nbsp;" Value="GEOGRAPHICAL_INFO" />
        <asp:MenuItem NavigateUrl="~/UI/Project/ProjectFinance.aspx" Text="&nbsp;Financier Info&nbsp;" Value="FINANCIER_INFO" />
        <asp:MenuItem NavigateUrl="~/UI/PROJECT/BudgetEstimation.aspx" Text="&nbsp;Budget Estimation&nbsp;" Value="BUDGET" />
        <asp:MenuItem NavigateUrl="~/UI/Project/Consultant.aspx" Text="&nbsp;Consultant&nbsp;" Value="CONSULTANT" />
        <asp:MenuItem NavigateUrl="~/UI/Project/ProjectPersonal.aspx" Text="&nbsp;Personnel&nbsp;" Value="PERSONNEL" />
        <asp:MenuItem NavigateUrl="~/UI/Project/AddPAP.aspx" Text="&nbsp;PAP&nbsp;" Value="PAP" />
        <asp:MenuItem NavigateUrl="~/UI/PROJECT/DefineWorkflow.aspx" Text="&nbsp;Workflow Definition&nbsp;" Value="WORKFLOW" />
        <asp:MenuItem NavigateUrl="~/UI/PROJECT/MaxCapDistrict.aspx" Text="&nbsp;MaxCap Districts&nbsp;" Value="MAXCAP_DIS" />
    </Items>
</asp:Menu>