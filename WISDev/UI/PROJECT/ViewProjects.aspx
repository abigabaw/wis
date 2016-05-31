<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewProjects.aspx.cs" Inherits="WIS.ViewProjects" %>

<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="toolkitManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Search Projects</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Project Name</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="iceTextBox" />
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Start Date</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="dpProjStartDate" runat="server" Width="90px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caldpProjStartDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="dpProjStartDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td>
                        <label class="iceLable">
                            End Date</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="dpProjEndDate" runat="server" Width="90px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caldpProjEndDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="dpProjEndDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Status</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProjectStatus" runat="server" CssClass="iceDropDown">
                            <asp:ListItem Value="">--All--</asp:ListItem>
                            <asp:ListItem Value="New">New</asp:ListItem>
                            <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <div style="margin-top: 12px;">
                            <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearSearch_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdProjects" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdProjects_RowCommand"
            OnRowDataBound="grdProjects_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Code" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Left" Width="12%" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkProjectCode" CommandName="EditProject" CommandArgument='<%#Eval("ProjectID") %>'
                            Text='<%#Eval("ProjectCode") %>' runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <ItemTemplate>
                        <asp:Literal ID="litProjectStartDate" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <ItemTemplate>
                        <asp:Literal ID="litProjectEndDate" Text="" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                <asp:TemplateField HeaderText="Data Verification" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDataVerification" CommandName="DataVerificationProject" CommandArgument='<%#Eval("ProjectID") %>'
                            Text='Data Verification' runat="server"></asp:LinkButton>
                        <asp:Literal ID="litDataVerification" Text="DataVerification" Visible="false" EnableViewState="false"
                            runat="server"></asp:Literal>
                        <asp:Literal ID="litProjectID" Text='<%#Eval("ProjectID") %>' Visible="false" EnableViewState="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Freeze" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkFreeze" CommandName="FreezeProject" CommandArgument='<%#Eval("ProjectID")  %>'
                            Text='Freeze' OnClientClick="return ConfirmFreezeProject();" runat="server"></asp:LinkButton>
                        <asp:Literal ID="litFrozen" Text="Frozen" Visible="false" EnableViewState="false"
                            runat="server"></asp:Literal>
                        <%--Start--%>
                        <asp:LinkButton ID="lnkFrozen" Text="Frozen" OnClientClick="return false;" Visible="false" EnableViewState="false"
                            runat="server"></asp:LinkButton>
                        <ajaxToolkit:HoverMenuExtender ID="ajxHoverMenuExtender" HoverCssClass="hovermenu"
                            PopupControlID="pnlUnfreeze" PopupPosition="Right" TargetControlID="lnkFrozen"
                            runat="server">
                        </ajaxToolkit:HoverMenuExtender>
                        <asp:Panel ID="pnlUnfreeze" CssClass="hovermenu" runat="server" Visible="false" BorderWidth="1px">
                            <div style="padding: 0px">
                                <asp:LinkButton ID="lnkUnfreeze" CommandName="Unfreeze" BackColor="Transparent" CommandArgument='<%#Eval("ProjectID")+ "," + Eval("ProjectCode") %>'
                                    Text="Unfreeze" runat="server" CssClass="iceLable" OnClientClick="return ConfirmUnfreeze();"></asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <%--End--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Route Map" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    <ItemTemplate>
                        <a id="lnkRouteMap" href="#" runat="server">View</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <EmptyDataTemplate>
                There are no records for the selected criteria.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <script language="javascript">
        PreventDateFieldEntry(document.getElementById('<%=dpProjStartDate.ClientID%>'));
        PreventDateFieldEntry(document.getElementById('<%=dpProjEndDate.ClientID%>'));

        function OpenBatchPayment(projID) {
            open('../Compensation/BatchPayment.aspx?projID=' + projID, 'batchPaymentWin', 'width=800px,height=700px,scrollbars=1');
        }

        function OpenRouteMap(projID) {
            open('RouteMap.aspx?projID=' + projID, 'routeMapWin', 'width=800px,height=700px,scrollbars=1,resizable=1');
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

        function ConfirmUnfreeze() {
            return confirm('Are you sure you want to Unfreeze this project?');
        }
        function UnfreezeWindow(ProjectID,ProjectCode, UserID) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../Project/ProjectUnfreeze.aspx?&ProjectID=' + ProjectID + '&ProjectCode=' + ProjectCode + '&UserID=' + UserID, 'OpenWindow', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
