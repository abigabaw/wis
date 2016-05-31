<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="LiveliHoodAfter.aspx.cs" Inherits="WIS.LiveliHoodAfter" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Report Criteria</legend>
        <table width="100%" align="center" border="0">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        HHID
                    </label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtHHID" runat="server" MaxLength="100" CssClass="iceTextBox"
                        Width="100px" Enabled="false"/>&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ValidationGroup="vgSearch" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                    ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
                    <asp:RequiredFieldValidator ID="rfvddlProject" runat="server" ControlToValidate="txtHHID"
                        ValidationGroup="vgSearch" Text="Mandatory" InitialValue="0" ErrorMessage="Enter HHID"
                        Display="None">
                    </asp:RequiredFieldValidator>
                </td> 
                 <td align="left" style="width: 100px;">
                    <label class="iceLable">
                        From Date</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="opsStartDate" runat="server" Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calopsStartDate" runat="server" CssClass="WISCalendarStyle"
                        TargetControlID="opsStartDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td style="width: 100px;">
                    <label class="iceLable">
                        To Date</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="opsEndDate" runat="server" Width="90px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalopsEndDate" runat="server" CssClass="WISCalendarStyle"
                        TargetControlID="opsEndDate">
                    </ajaxToolkit:CalendarExtender>
                </td>         
                </tr>
            <tr>
                <td colspan="4" align="center">
                    <input type="button" class="icebutton" value="View" onclick="OpenReport()" />
                    &nbsp;<asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <script language="javascript" type="text/javascript">
        function OpenReport() {
            var FHHID = document.getElementById('<%=txtHHID.ClientID%>');

            var HHID;
            startdate = GetCalDate('<%=opsStartDate.ClientID%>');
            Enddate = GetCalDate('<%=opsEndDate.ClientID%>');

            if (FHHID.value.length > 0) {
                HHID = FHHID.value;
            }
            else {
                alert('Please select a Pap');
                return;
            }

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var param = 'rptCode=PLAFTR' +
                        '&HHID=' + HHID +
                        '&opsStartDate=' + startdate +
                        '&opsEndDate=' + Enddate;

            open('RptViewer.aspx?' + param, 'winRptViewer', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
