<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectCompare.aspx.cs" Inherits="WIS.ProjectCompare" UICulture="en" Culture="en-US" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Compare Projects</legend>
            <table border="0" width="100%">
                <tr>
                    <td style="width: 40%">
                        <div style="text-align: right; padding-right: 230px">
                            <asp:Label ID="allprojectsLabel" runat="server" CssClass="iceLable" Text="All Projects"></asp:Label></div>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 40%">
                        <asp:Label ID="projectstocompLabel" runat="server" CssClass="iceLable" Text="Projects To Compare"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:ListBox ID="LstProjects" runat="server" Height="192px" Width="300px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                    <td align="center" valign="top" class="style3">
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="BtnAdd" runat="server" CssClass="icebutton" Text="&gt;&gt;" Width="90px"
                            OnClick="BtnAdd_Click" />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="Btn_remove" runat="server" CssClass="icebutton" Text="&lt;&lt;" Width="91px"
                            OnClick="Btn_remove_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="LstProjectcomp" runat="server" Height="195px" Width="300px" ></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="padding-top: 12px">
                        <asp:Button ID="btnCompare" runat="server" CssClass="icebutton" Text="Compare" Width="84px"
                            OnClick="btnCompare_Click" OnClientClick="return CheckCount();"/>
                        &nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="icebutton" Text="Reset" Width="84px"
                            OnClick="btnReset_Click" />
                        <asp:Label ID="msgsaveLabel" runat="server" CssClass="iceLable">
                        </asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <%--<table id="tblProjectHeader" class="gridStyle" cellpadding="4" cellspacing="1" width="100%" visible="false" runat="server">
            <tr class="gridHeaderStyle">
                <td align="center" rowspan="2" style="vertical-align: top">
                    Sl. No.
                </td>
                <td align="center" rowspan="2" style="vertical-align: top">
                    Projects
                </td>
                <td align="center" rowspan="2" style="vertical-align: top">
                    Distance
                </td>
                <td align="center" rowspan="2" style="vertical-align: top">
                    Budget
                </td>
                <td align="center" rowspan="2" style="vertical-align: top">
                    Actual Expense
                </td>
                <td align="center" colspan="5" style="vertical-align: top">
                    Total No. of PAPs
                </td>
                <td align="center" colspan="5">
                    Option Groups
                </td>
            </tr>
            <tr class="gridHeaderStyle">
                <td align="center">
                    Paid
                </td>
                <td align="center">
                    Not Paid
                </td>
                <td align="center">
                    Queried
                </td>
                <td align="center">
                    Disclosed
                </td>
                <td align="center">
                    Rejected Payment
                </td>
                <td align="center">
                    1
                </td>
                <td align="center">
                    1A
                </td>
                <td align="center">
                    2
                </td>
                <td align="center">
                    3A
                </td>
                <td align="center">
                    3B
                </td>
            </tr>
        </table>--%>
        <asp:GridView ID="grdcompareprjt" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%">
            <RowStyle CssClass="gridRowStyle" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectName" HeaderText="Projects" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TotalestBudget" DataFormatString="{0:N0}" HeaderText="Total Budget " HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Option1" HeaderText="1" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Option2" HeaderText="1A" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Option3" HeaderText="2" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Option4" HeaderText="3A" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Option5" HeaderText="3B" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">

        function CheckCount() {
            var fldLstProjectcomp = document.getElementById('<%=LstProjectcomp.ClientID%>');
            if (fldLstProjectcomp.length < 2) {
                alert('Select at least two projects to Compare.');
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
