<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="PackagePaymentAcknowledgement.aspx.cs" Inherits="WIS.PackagePaymentAcknowledgement" %>
<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" 
        Width="861px"
        ToolPanelView="None"
        HasToggleGroupTreeButton="False"
        HasDrilldownTabs="False"
        HasDrillUpButton="False"
        EnableParameterPrompt="false"
        ReuseParameterValuesOnRefresh="true" 
        GroupTreeImagesFolderUrl="" Height="1158px" 
        ToolbarImagesFolderUrl="" 
        ToolPanelWidth="200px" />
    </div>
</asp:Content>
