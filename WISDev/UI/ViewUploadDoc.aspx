<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopUPwithoutLogo.Master" AutoEventWireup="true" CodeBehind="ViewUploadDoc.aspx.cs" Inherits="WIS.UI.ViewUploadDoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    <!--
    function sizeFrame() {
        var viewportheight;
        var razlika = 2;
        var visina = +viewportheight - razlika + 'px'

        // the more standards compliant browsers (mozilla/netscape/opera/IE7) use window.innerWidth and window.innerHeight

        if (typeof window.innerWidth != 'undefined') {
            viewportheight = window.innerHeight
        }

        // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)

        else if (typeof document.documentElement != 'undefined'
    && typeof document.documentElement.clientWidth !=
    'undefined' && document.documentElement.clientWidth != 0) {
            viewportheight = document.documentElement.clientHeight
            //alert(document.documentElement.clientHeight);
        }

        // older versions of IE

        else {
            viewportheight = document.getElementsByTagName('body')[0].clientHeight
        }
        
        // NEW STUFF BELOW
            var frame_h = viewportheight - razlika + 'px'
        document.getElementById('contentPanel1').style.height = frame_h;
    }


    window.onload = sizeFrame;
    window.resizeTo = sizeFrame;

    //-->

    </script>
    <table height="100%" width="100%">
        <tr>
            <td><span id="spncontent" runat="server" style="display: none">
                <iframe width="100%" id="contentPanel1" sandbox="allow-scripts" clientidmode="Static"
                    runat="server" height="550px"></iframe></span>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" Text="File doe not exist in the file system." Font-Bold="true"
                    ForeColor="White" runat="server" Visible="false"></asp:Label>
            </td>
             <td>
       <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
        </td>
        </tr>
    </table>

</asp:Content>
