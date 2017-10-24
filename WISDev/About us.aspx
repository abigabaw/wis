
<!-- 
	Wilson Abigaba
	24/10/2017
 --!>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="About us.aspx.cs" Inherits="WIS.About_us" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%">
        <fieldset class="icePnlinner"  style="background-color: White"><br />
            <table border="0" style="background-color: White">
                <tr>
                    <td valign="bottom">
                        <asp:Image ID="imgLogo" ImageUrl="~/IMAGE/UETCL_LogoAboutus.png" BackColor="Transparent" Height="150px" Width="200px" runat="server" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    &nbsp;<asp:Label ID="lblVersion" runat="server" Text="Version" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:Label ID="lblVersionBuild" runat="server" CssClass="iceLablelogin" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;<asp:Label ID="lblDate" runat="server" Text="Date" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:Label ID="lblDataVersion" runat="server" CssClass="iceLablelogin" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblCopyright" runat="server" CssClass="iceLablelogin" 
                                        BorderWidth="0px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <label class="iceLable">
                                        &nbsp;This Product is Designed and Developed by 
                                        <asp:HyperLink id="HLMFI" runat="server" Text="MFI" NavigateUrl="http://www.groupmfi.com" Target="_blank" />
                                        and <asp:HyperLink id="HLKtwo" runat="server" Text="KTwo Technology Solutions" NavigateUrl="http://www.Ktwo.co.in" Target="_blank" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </fieldset>
    </div>
</asp:Content>
