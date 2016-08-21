<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="ClarificationPop.aspx.cs" Inherits="WIS.RequestClarification" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <asp:Panel ID="pnlSearchDocument" Visible="false" runat="server">
        &nbsp;
    </asp:Panel>
    <asp:Panel ID="pnlUploadDocuments" runat="server">
        <fieldset class="icePnlinner">
            <table align="center" width="100%" cellpadding="2" cellspacing="5">


                <%-- table align="center" width="100%" --%>
                <tr id="PapDetails" runat="server">
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtHHID" runat="server" Enabled="False" ReadOnly="True" Width="50px" Font-Names="Calibri"></asp:TextBox>&nbsp;&nbsp;
                        <asp:TextBox ID="txtPapName" runat="server" Enabled="False" ReadOnly="True" Width="250px" Font-Names="Calibri"></asp:TextBox>
                    </td>

                </tr>
                <tr id="ProjectDetails" runat="server">
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtProjectName" runat="server" Enabled="False" ReadOnly="True" Width="500px" Font-Names="Calibri"></asp:TextBox>
                    </td>
                </tr>
                <tr id="RequesterDetails" runat="server">
                    <td align="right"><b>Requester Name:</b></td>
                    <td>
                        <asp:TextBox ID="txtRequester" runat="server" Enabled="False" ReadOnly="True" Width="500px" Font-Names="Calibri"></asp:TextBox>
                    </td>
                </tr>
                <tr id="RespondentSelect" runat="server">
                    <td align="right"><b>Request Clarification:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlProjectPersonnel" runat="server" Width="279px" Font-Names="Calibri">
                            <asp:ListItem>-- Select --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="ClarificationDetail" runat="server">
                    <td align="right" valign="top"><b>Clarification Details</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClarifyDetails" runat="server" Height="87px" TextMode="MultiLine" Width="551px" Font-Names="Calibri"></asp:TextBox>
                    </td>
                </tr>
                <tr id="ResponseFields" runat="server">
                    <td align="right" valign="top"><b>Response Details</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResponseDetails" runat="server" Height="86px" TextMode="MultiLine" Width="551px" Font-Names="Calibri"></asp:TextBox>
                    </td>
                </tr>
                <%-- tr>
                                <td align="right" valign="top"><b>Due Date</b></td>
                                <td>
                                    <asp:TextBox ID="dpDateOfBirth" runat="server" Width="278px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" CssClass="WISCalendarStyle"
                                        TargetControlID="dpDateOfBirth">
                                    </ajaxToolkit:CalendarExtender>
                                    <br />
                                </td>
                            </ !--%>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="SaveButton" runat="server" CssClass="icebutton" Style="width: 150px;" Text="Send Clarification" ValidationGroup="vgfilMyFile" Width="159px" OnClick="SaveButton_Click" />
                            <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" Style="width: 150px;" Text="Clear" OnClick="ClearButton_Click" />
                            <input type="button" id="btnClose" class="icebutton" style="width: 150px;" value="Close" onclick="window.close();" />
                        </td>
                    </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <!-- table width="100%">
        <tr>
            <td>
                
            </td>
        </tr>
    </!-->

    <div>
        <asp:GridView ID="grdClarifications" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" AllowPaging="True" OnRowDataBound="grdClarifications_RowDataBound"  OnRowCommand="grdClarifications_RowCommand">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <%--<ItemStyle Width="7%" />--%>
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PapName" HeaderText="PAP Name" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Requester" HeaderText="Requested By" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <%--<asp:TemplateField HeaderText="RequestDate" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="RequestDate" Text='<%# Eval("RequestDate").Equals(DateTime.MinValue) ? "" : Eval("RequestDate")%>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:BoundField DataField="RequestDate" HeaderText="Requested Date" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="RequestDetails" HeaderText="Request Details" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Respondent" HeaderText="Respondent" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ResponseDetails" HeaderText="Response Details" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <%--<asp:TemplateField HeaderText="ResponseDate" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="ResponseDate" Text='<%#IIF(Eval("ResponseDate") = '01/01/0001 00:00', "", ResponseDate) %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:BoundField DataField="ResponseDate" NullDisplayText="" HeaderText="Response Date" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkRespond" runat="server" CommandName="RespondToClarify" CommandArgument='<%#Eval("ID") %>' runat="server">Respond</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript" language="javascript">
        function ViewUploadDocument(PAPDOCUMENTID, ProjectCode) {
            var left = (screen.width - 650) / 2;
            var top = (screen.height - 640) / 4;
            open('../UI/ViewUploadDoc.aspx?papDocumentID=' + PAPDOCUMENTID + '&ProjectCode=' + ProjectCode, 'ChangeRequest', 'width=650px,height=640px,resizable=1,top=' + top + ', left=' + left);
        }

        /* function SetVisible(val) {
           var hf = document.getElementById("<!-- %= hfVisible.ClientID  % -->");
           hf.value = val;
        } */

        function ViewFile(path) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 500) / 4;
            window.open(path);
        }

        function SeesionExperpopup() {
            alert('Session Expired. Please relogin.');

            if (opener) {
                window.opener.location.reload();
            }

            window.close();
        }
    </script>
</asp:Content>
