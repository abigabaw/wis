<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HouseholdSummary.ascx.cs" Inherits="WIS.HouseholdSummary" %>
<table align="center" border="0" cellpadding="2" cellspacing="2" style="background-color: #eeeeee; margin-top: 10px; margin-bottom: 5px" width="100%">
    <tr>
        <td style="width: 80px;" nowrap align="right">
            <label class="iceLable">Household ID:</label></td>
        <td class="labelPlotRef" style="width: 80px" nowrap align="left">
            <asp:Literal ID="litSmrHouseholdID" runat="server"></asp:Literal>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgSearch" runat="server" ImageAlign="Bottom" ToolTip="Click here to change PAP"
                ImageUrl="~/IMAGE/search.png" OnClick="imgSearch_Click" />
        </td>
        <td style="width: 75px" align="right">
            <label class="iceLable">Pap UID:</label></td>
        <td class="labelPlotRef" style="width: 75px" align="left">
            <asp:Literal ID="litPAPUID" runat="server"></asp:Literal>&nbsp;&nbsp;
        </td>
        <td style="width: 50px" nowrap align="right">
            <label class="iceLable">Plot Ref:</label></td>
        <td class="labelPlotRef" nowrap style="width: 130px" align="left">
            <a href="#" id="aSmrPlotReference" onclick="ViewMap();">
            <asp:Literal ID="litSmrPlotReference" runat="server"></asp:Literal></a>
            <asp:HiddenField ID="hfHHID" runat="server" Value="0" />
            <asp:HiddenField ID="hfCheckPath" runat="server" Value="" />
            &nbsp;&nbsp;
        </td>
        <td style="width: 70px" nowrap align="right">
            <label class="iceLable">Designation:</label></td>
        <td class="labelPlotRef" nowrap align="left" style="width:50px">
            <asp:Literal ID="litSmrDesignation" runat="server" ></asp:Literal>
        </td>
        <td style="width: 35px" nowrap>
            <label class="iceLable">Pap Name:</label></td>
        <td class="labelPlotRef" style="width: 800px">
            <asp:Literal ID="litSmrPAPName" runat="server"></asp:Literal>&nbsp;&nbsp;
        </td>

    </tr>
</table>
<script type="text/javascript">

    function ViewMap() {
        var HHID = document.getElementById('<%=hfHHID.ClientID %>').value;
            var CheckPath = document.getElementById('<%=hfCheckPath.ClientID %>').value;
            if (CheckPath == 'Yes') {
                open('../PROJECT/ViewPAPMap.aspx?HHID=' + HHID, 'routeMapWin', 'width=800px,height=700px,scrollbars=1');
            }
            else {
                open('../../PROJECT/ViewPAPMap.aspx?HHID=' + HHID, 'routeMapWin', 'width=800px,height=700px,scrollbars=1');
            }

        }
</script>
