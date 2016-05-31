<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="RouteCoordinatesPopup.aspx.cs" Inherits="WIS.UI.RouteCoordinatesPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script language="javascript">
    function RouteCoordinateUploadCompleted() {
        filePath = document.getElementById('hdnFilePath').value;
        if (opener && filePath != '') {
            opener.SendFilePath(filePath);
            window.close();
        }
    }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlFileUpload" runat="server">
        <table border="0" width="100%" align="center">
            <tr>
                <td>
                    <fieldset class="icePnl1">
                        <legend>Route Coordinates Information</legend>
                        <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                            <tr>
                                <td class="iceLable" style="width: 15%">
                                    Select File
                                </td>
                                <td align="left" style="width: 40%">
                                    <asp:FileUpload ID="FileUpload" class="iceTextBox" runat="server" Width="250px" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnUpload" Text="Upload" runat="server" class="icebutton" 
                                        Style="width: 140px" onclick="BtnUpload_Click" />
                                    <asp:Button ID="btnCancelUpload" Text="Cancel" runat="server" class="icebutton" Style="width: 140px" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
         <asp:HiddenField ID="hdnFilePath" ClientIDMode="Static" runat="server" />
    </asp:Panel>
</asp:Content>
