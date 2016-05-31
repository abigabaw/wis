<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="PAPList.aspx.cs" Inherits="WIS.PAPList" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/PAPListUserControl.ascx" TagName="PAPListUC" TagPrefix="UCPap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <UCPap:PAPListUC ID="PAPListUC1" runat="server" />
    <asp:HiddenField ID="HiddenFieldPAP" runat="server" Value="PAPList" />
</asp:Content>
