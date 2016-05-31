<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="ViewPhotoDocumnet.aspx.cs" Inherits="WIS.ViewPhotoDocumnet" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
   <tr>
      <td>
          <asp:Image ID="photoImage" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />         
      </td>      
   </tr>
  <tr>
    <td>
       <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
    </td>
  </tr>
</table>
</asp:Content>
