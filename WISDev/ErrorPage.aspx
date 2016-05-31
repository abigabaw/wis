<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WIS.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:Panel id="pnlError" runat="server" visible="false">
    <asp:Label id="lblError" runat="server" Text="An error occurred while performing your request. Sorry for any inconvenience."></asp:Label>
    <br />
    <asp:Label id="lblGoBack" runat="server" Text="You may want to get back to the previous page and try again or perform other activities."></asp:Label>
    <br /><br />
    <asp:HyperLink id="hlinkPreviousPage" runat="server">Go back</asp:HyperLink>
    <br /><br />
    <asp:HyperLink id="lnkHomePage" runat="server">Go to Home Page</asp:HyperLink>
    <asp:LinkButton id="lnkClose" OnClientClick="CloseWindow();" runat="server">Close</asp:LinkButton>
</asp:Panel> 
<script type="text/javascript">
    window.onload = function () {
        if (window.opener != null && !window.opener.closed) {
            document.getElementById('<%=hlinkPreviousPage.ClientID %>').style.display = "none";
            document.getElementById('<%=lnkHomePage.ClientID %>').style.display = "none";            
            document.getElementById('<%=lnkClose.ClientID %>').style.display = "";
        }
        else {
            document.getElementById('<%=hlinkPreviousPage.ClientID %>').style.display = "";
            document.getElementById('<%=lnkHomePage.ClientID %>').style.display = "";
            document.getElementById('<%=lnkClose.ClientID %>').style.display = "none"; 
        }
    };
    function CloseWindow() {
        window.close();
    }    
</script>
</asp:Content>
