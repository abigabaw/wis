<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WIS.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Roylecss.css" rel="stylesheet" />

    <!-- link href="/Styles/Roylecss.css" rel="stylesheet" type="text/css" / -->
    <script type="text/javascript" src="Scripts/ImageSliderJs/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/ImageSliderJs/chili-1.7.pack.js"></script>
    <script type="text/javascript" src="Scripts/ImageSliderJs/jquery.cycle.all.js"></script>
    <%--<script type="text/javascript" src="Scripts/ImageSliderJs/jquery.easing.1.3.js"></script>--%>
    <script type="text/javascript" language="javascript">
        //  window.onload = function () {
        // document.getElementById('zoom').style.display = '';
        //  }

        //   function CheckEnterKey(e) {
        /*var keycode = null;
        if (e.keyCode) {
        keycode = e.keyCode;
        }
        else if (e.which) {
        keycode = e.which;
        }

        if (keycode == 13) {
        document.getElementById('<%=btnSave.ClientID%>').click();
        }*/
        //  }

        $('#zoom').cycle({
            fx: 'fade',
            sync: false,
            delay: -2000
        });

        $('#shuffle').cycle({
            fx: 'shuffle',
            easing: 'easeOutBack',
            delay: -4000
        });
    </script>
</head>
<body>
<!-- Joel Jombwe: 01/03/16 -->
    <form id="form1" runat="server" defaultbutton="btnSave" >
	
    <div class="page">
        <div class="header">
            <div class="title">
                <table>
                    <tr>
                        <td>
                            <input name="image" type="image" src="Image/UTCL_Logo.png" />
                        </td>
                        <td class="appHeader">
                            Wayleaves Information System
                        </td>
                    </tr>
                </table>
            </div>
          <%--  <div class="loginDisplay">
                &nbsp;
            </div>
            <div class="clear hideSkiplink">
                &nbsp;
            </div>--%>
        </div>
        <div style="clear: both">
        </div>
        <div style="width: 100%; margin-top:15px;">
            <table width="100%">
                <tr>
                    <td width="99%">
                        <table width="99%">
                            <tr style="">
                                <td>
                                    <div id="zoom" class="pics" width="100%" style="display:block">
                                        <img src="Image/ImageSlider/buja1.jpg" width="100%" height="500" />
                                        <img src="Image/ImageSlider/buj1.jpg" width="100%" height="500" />
                                        <img src="Image/ImageSlider/buj2.jpg" width="100%" height="500" />
                                        <img src="Image/ImageSlider/buj3.jpg" width="100%" height="500" />
                                        <img src="Image/ImageSlider/buj4.jpg" width="100%" height="500" />
                                          <img src="Image/ImageSlider/buj5.jpg" width="100%" height="500" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="margin-right: -30px;">
                        <fieldset class="icePnlogin">
                            <table align="right" style="margin-left: -10px; margin-right: -10px;">
                                <tr>
                                    <td>
                                        <fieldset>
                                            <table>
                                                <tr>
                                                    <td class="appHeader">
                                                        Welcome
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p align="justify">
                                                            Wayleaves Information System is a web based application for the various users of UETCL
                                                            who are involved in the Wayleaves management. The various activities of the entire
                                                            process are being captured in the application from route identification process
                                                            till awarding the compensation and tracking the expenses.</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 25px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <fieldset>
                                            <table border="0" align="right">
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <h1>
                                                            Login</h1>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUsername" runat="server" Text="Username" CssClass="iceLablelogin" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="loginuser" Text="" AutoCompleteType="Disabled" />
                                                    </td>
                                                </tr>
                                                <!-- Edwin Baguma: 26/02/2016
                                                    <tr>
                                                    <td>
                                                        &nbsp;

                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkStaySignedIn" runat="server" CssClass="Logincheckbox" /><asp:Label
                                                            ID="lblStaySignedIn" runat="server" Text="Remember Me" 
                                                            CssClass="Loginfont" />
                                                    </td>
                                                </tr> -->
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="iceLablelogin" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="PasswordTextBox" TextMode="Password" runat="server" CssClass="loginpassword" Text="" AutoCompleteType="Disabled" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="iceLoginbutton" Text="Sign In"
                                                            OnClick="btnSave_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" class="mandatory">
                                                        <asp:Label ID="lblMsgSave" runat="server" CssClass="iceLablelogin" ForeColor="Red" Width="280px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 1px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
        <font color="#FFFFFF">Copyright &copy; 2013. <a href="http://www.uetcl.com" style="text-decoration: none" target="_blank">
            <font color="#FFFFFF">Uganda Electricity Transmission Company Ltd.<sup>&reg;</sup></font></a></font>
    </div>
    </form>
</body>
</html>
