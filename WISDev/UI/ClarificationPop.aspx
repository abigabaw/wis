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
                
                    
                        <!-- table align="center" width="100%" -->
                            <tr>
                                <td align="right"><b>Request Clarification:</b></td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="279px">
                                        <asp:ListItem>-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"><b>Clarification Details</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Height="154px" TextMode="MultiLine" Width="551px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"><b>Due Date</b></td>
                                <td>
                                    <asp:TextBox ID="dpDateOfBirth" runat="server" Width="278px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calDateOfBirth" runat="server" CssClass="WISCalendarStyle"
                                        TargetControlID="dpDateOfBirth">
                                    </ajaxToolkit:CalendarExtender>
                                    <br />
                                </td>
                            </tr>
                        </!-->
                    
                
                <tr>

                    <td></td>
                    <td>
                        <asp:Button ID="SaveButton" runat="server" CssClass="icebutton" ValidationGroup="vgfilMyFile"
                            style="width: 150px;" Text="Send Clarification" Width="159px" />

                        <asp:Button ID="ClearButton" runat="server" CssClass="icebutton" style="width: 150px;" Text="Clear" />

                        <input type="button" id="btnClose" class="icebutton" style="width: 150px;" value="Cancel" onclick="window.close();" />
                    </td>

                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
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
