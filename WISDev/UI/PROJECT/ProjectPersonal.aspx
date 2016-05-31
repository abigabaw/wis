<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectPersonal.aspx.cs" Inherits="WIS.ProjectPersonal" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
<ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server"></ajaxToolkit:ToolkitScriptManager>
<fieldset class="icePnlinner">
    <legend>Project Personnel</legend>
<table border="0" width="100%">
    <tr>
        <td style="width:40%">
            <div style="text-align:right;padding-right:130px"><asp:Label ID="Label9" runat="server" CssClass="iceLable" Text="All Users"></asp:Label></div>
        </td>
        <td>
            &nbsp;</td>
        <td style="width:40%">
            <asp:Label ID="Label8" runat="server" CssClass="iceLable" Text="Project Users"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:ListBox ID="LstUsers" runat="server" Height="192px" Width="186px" 
                AutoPostBack="False" SelectionMode="Multiple">               
            </asp:ListBox>
        </td>
        <td align="center" valign="top" class="style3">
            <br />
            <br />
            <br />
            <asp:Button ID="BtnAdd" runat="server" CssClass="icebutton" Text="&gt;&gt;" 
                Width="90px" onclick="BtnAdd_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="Btn_remove" runat="server" CssClass="icebutton" 
                 Text="&lt;&lt;" Width="91px" onclick="Btn_remove_Click" />
        </td>
        <td>
            <asp:ListBox ID="LstProject" runat="server" Height="195px" Width="198px">
            </asp:ListBox>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" CssClass="icebutton" 
                onclick="btn_Save_Click" Text="Save" Width="84px" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false"/>
            <asp:Button ID="btnReset" runat="server" CssClass="icebutton" 
                onclick="btnReset_Click" Text="Reset" Width="84px" OnClientClick="return ResetPersonnel()" />
        </td>
    </tr>
</table>
</fieldset>
<script language="javascript" type="text/javascript">
            function ResetPersonnel() {
                return confirm('Are you sure you want to Reset the Project Personnel?');
            }

            function DisableOnSave(src) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }  
            </script>
</asp:Content>
