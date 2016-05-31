<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="UploadPhotoDocument.aspx.cs" Inherits="WIS.UI.UploadPhotoDocument" %>
<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <script language="javascript">
     function AfterUPLOADIMAGE() {
         if (opener)
         opener.ReloadParentWindow();
             //opener.PapPhoto();
         window.close();
         
     }
     function AfterUPLOADIMAGEPAPINST() {
         //window.opener.location.replace(window.opener.location.pathname);
         opener.PapPhoto();
         window.close();
     }
     function AfterUPLOADIMAGE() {
         window.close();
     }
 </script>
 <div width="100%">
     <table>
         <tr>
             <td>
                 <asp:Image ID="photoImage" runat="server" BorderColor="Silver" BorderStyle="Solid"
                     BorderWidth="1px" />
             </td>
         </tr>
         <tr>
             <td>
                 <asp:FileUpload ID="photoFileUpload" runat="server" CssClass="iceTextBoxLarge" />&nbsp;<asp:Button
                     ID="btnUpload" runat="server" OnClick="btnUpload_Click" CssClass="icebutton"
                     Text="Upload" />&nbsp;
                 <asp:Button ID="btnClose" runat="server" CssClass="icebutton" Text="Close" OnClick="btnClose_Click" />
             </td>
         </tr>
         <tr>
            <td><asp:Label ID="Info" Text="(Max Photo Upload Size is 2MB)" runat="server" CssClass="iceLable" /></td>
         </tr>
     </table>
 </div>
</asp:Content>
