<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HouseholdSummary.ascx.cs" Inherits="WIS.HouseholdSummary" %>
<table align="center" border="0" cellpadding="2" style="background-color:#eeeeee;margin-top:10px;margin-bottom:5px" width="100%">
<tr>
<td style="width:80px;" nowrap><label class="iceLable">Household ID:</label></td>
<td class="iceLable" style="width:80px" nowrap>
    <asp:Literal ID="litSmrHouseholdID" runat="server"></asp:Literal>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgSearch"  runat="server" ImageAlign="Bottom" ToolTip="Click here to change PAP"
        ImageUrl="~/IMAGE/search.png" onclick="imgSearch_Click" />
</td>
<td style="width:25px"><label class="iceLable">UID:</label></td>
<td class="iceLable" style="width:120px">
    <asp:Literal ID="litPAPUID" runat="server"></asp:Literal>&nbsp;&nbsp;
</td>
<td style="width:35px" nowrap><label class="iceLable">Name:</label></td>
<td class="iceLable" style="width:250px">
    <asp:Literal ID="litSmrPAPName" runat="server"></asp:Literal>&nbsp;&nbsp;
</td>
<td style="width:90px" nowrap><label class="iceLable">Plot Reference:</label></td>
<td class="labelPlotRef" nowrap style="width:130px">
    <a href="#" id="aSmrPlotReference" onclick="ViewMap();">
    <asp:Literal ID="litSmrPlotReference" runat="server"></asp:Literal></a>
    <asp:HiddenField ID="hfHHID" runat="server" Value="0"/>
    <asp:HiddenField ID="hfCheckPath" runat="server" Value=""/>&nbsp;&nbsp;
</td>
<td style="width:70px" nowrap><label class="iceLable">Designation:</label></td>
<td class="iceLable" nowrap>
    <asp:Literal ID="litSmrDesignation" runat="server"></asp:Literal>
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