<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" UICulture="en" Culture="en-US" CodeBehind="PopUpPAPList.aspx.cs" 
Inherits="WIS.UI.COMPENSATION.PopUpPAPList" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<%@ Register Src="~/UI/COMPENSATION/PAPListUserControl.ascx" TagName="PAPListUC" TagPrefix="UCPap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        html
        {
            overflow: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <UCPap:PAPListUC ID="PAPListUC1" runat="server" />
    <asp:HiddenField ID="HiddenFieldPAP" runat="server" Value="PopUpPAPList" />
    <script type="text/javascript">
        function AfterSelectPAP() {
            window.opener.location.replace(window.opener.location.pathname);

            window.close();
        }

        function AfterSelectPAPForReports(ProjectID, Distict, County, SubCounty, Parish, Village) {
            window.opener.location.replace(window.opener.location.pathname + '?ProjectID=' + ProjectID + '&Distict=' + Distict + '&County=' + County + '&SubCounty=' + SubCounty + '&Parish=' + Parish + '&Village=' + Village);

            window.close();
        }

        var width = 760;
        var height = 700;
        window.resizeTo(width, height);
        window.onresize = function () { window.resizeTo(width, height); } 
 </script>
</asp:Content>
