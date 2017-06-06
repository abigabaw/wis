<%@ Page Language="C#" debug="true" AutoEventWireup="true" CodeBehind="RptViewer.aspx.cs" Inherits="WIS.RptViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Roylecss.css" rel="stylesheet" type="text/css" />
</head>
<body style="padding: 0; margin: 0">
     <form id="form1" runat="server">
    <table align="center" border="0" width="100%" bgcolor="#e8e8e8">
        <tr>
            <td colspan="4" align="right" style="padding-top: 2px">
                &nbsp;<asp:Button ID="btnClose" CssClass="icebutton" runat="server" Text="Close" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
   
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            ToolPanelView="None" HasToggleGroupTreeButton="False" HasDrilldownTabs="False"
            HasDrillUpButton="False" EnableParameterPrompt="false" ReuseParameterValuesOnRefresh="true"
            GroupTreeImagesFolderUrl="" Height="610px" Width="960px" ToolbarImagesFolderUrl=""
            BestFitPage="False" />
    </div>
    </form>
</body>
</html>
