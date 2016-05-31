<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="RouteIdentification.aspx.cs" Inherits="WIS.RouteIdentification" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function AfterTotalScore(refreshParent) {
            if (opener && refreshParent) {
                window.opener.location.replace(window.opener.location.pathname);
            }

            window.close();
        }

        function AfterSave(refreshParent) {
            window.opener.location.replace(window.opener.location.pathname);
        }

        function AfterClick(refreshParent) {
            window.close();
        }

        function DisableOnSave(src) {
            //                if ( Page_ClientValidate() ){
            //                    document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            src.disabled = true;
            src.value = 'Please Wait...';
            //                    document.getElementById('<%=btnSave.ClientID%>').value = 'Please Wait...';
            //          };
        }
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <asp:Panel ID="pnlFactors" Visible="true" runat="server">
            <fieldset class="icePnlinner">
                <legend>Route Selection Criteria</legend>
                  <asp:Panel ID="pnlFactorsData" Visible="true" runat="server">
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Repeater ID="rptRoute" runat="server" OnItemDataBound="rptRout_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Panel ID="pnlRouteHeader" runat="server">
                                        <table width="100%" border="0" style="color: #ffffff; background-color: #447294;
                                            border-bottom: 1px solid #ffffff;">
                                            <tr>
                                                <td align="center" style="width: 5%; background-color: white;">
                                                    <asp:Image ID="ImageExpCol" runat="server" ImageUrl="~/IMAGE/plus.gif" />
                                                </td>
                                                <td style="width: 85%">
                                                    <div style="padding: 2px 0px 2px 0px;">
                                                        &nbsp;&nbsp;<asp:Label ID="lblRouteName" runat="server" Text='<%#Eval("Route_Name") %>'></asp:Label><%--Text='<%#Eval("FactorName") %>'--%>
                                                        <asp:HiddenField ID="hdnRouteId" runat="server" Value='<%#Eval("Route_ID") %>' />
                                                        <%--Value='<%#Eval("FactorId") %>'--%>
                                                    </div>
                                                </td>
                                                <td style="width: 10%" align="center">
                                                    <asp:TextBox ID="txtRouteScore" runat="server" Text="" Style="text-align: center"
                                                        MaxLength="3" Width="50" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRouteFactors" Height="0px" runat="server">
                                        <asp:Repeater ID="rptFactors" runat="server" OnItemDataBound="rptFactors_ItemDataBound">
                                            <ItemTemplate>
                                                <table width="100%" border="0" style="background-color: #8fbcdb;">
                                                    <tr>
                                                        <td style="width: 90%">
                                                            <div style="padding: 2px 0px 2px 0px;">
                                                                &nbsp;&nbsp;<asp:Label ID="lblFactorName" CssClass="iceLable" runat="server" Text='<%#Eval("FactorName") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnFactorId" runat="server" Value='<%#Eval("FactorId") %>' />
                                                            </div>
                                                        </td>
                                                        <td style="width: 10%" align="center">
                                                            <asp:TextBox ID="txtFactorScore" runat="server" Text="" MaxLength="3" Width="50" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Repeater ID="rptCriteria" runat="server" OnItemDataBound="rptCriteria_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table border="0" cellspacing="0" cellpadding="3" width="100%" style="background-color: #eeeeee;
                                                            border-bottom: 1px solid #e0e0e0;">
                                                            <tr class="gridRowStyle">
                                                                <td align="center" style="vertical-align: top; width: 4%">
                                                                    <%#Container.ItemIndex + 1%>
                                                                </td>
                                                                <td>
                                                                    <b>Criteria-<%#Eval("CriteriaId") %>:</b>
                                                                    <asp:Label ID="lblCriteriaDescription" runat="server" CssClass="labelSuffix" Text='<%#Eval("CriteriaDescription") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnCriteriaId" runat="server" Value='<%#Eval("CriteriaId") %>' />
                                                                </td>
                                                                <td style="padding-top: 3px; vertical-align: top; width: 15%" align="center">
                                                                    <asp:DropDownList ID="ddlCriteriaScore" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCriteriaScore_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>   
                                        <asp:Panel ID="pnlspace" Visible="false" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        &nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>              
                                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeRoute1" ExpandControlID="pnlRouteHeader"
                                        CollapseControlID="pnlRouteHeader" TargetControlID="pnlRouteFactors" ImageControlID="ImageExpCol" ExpandedImage="~/IMAGE/minus.gif" CollapsedImage="~/IMAGE/plus.GIF" Collapsed="true"
                                        runat="server">
                                    </ajaxToolkit:CollapsiblePanelExtender>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <table border="0" width="100%">
                  <tr>
                        <td align="center">
                            <div style="margin-top: 12px;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlSave" runat="server">
                                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server"
                                                    OnClick="btnSave_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />&nbsp;
                                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                            </asp:Panel>
                                        </td>
                                        <td>
                                             &nbsp;<asp:Button ID="btnClose" CssClass="icebutton" Text="Close" runat="server" OnClick="btnClose_Click" />
                                        </td>
                                    </tr>
                                </table>
                               
                                <%-- <input type="button" id="btnClose" value="Close" class="icebutton" onclick="btnClose_onclick" />--%>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
