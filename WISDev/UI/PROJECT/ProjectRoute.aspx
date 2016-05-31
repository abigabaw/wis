<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectRoute.aspx.cs" Inherits="WIS.ProjectRoute" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/ApprovalMessage.ascx" TagName="ApprovalMessage" TagPrefix="ucMsg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <asp:Panel ID="pnlRouteCoordinatesDetails" runat="server">
        <fieldset class="icePnlinner">
            <legend>Initial Routes Identified </legend>
            <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                <tr>
                    <td style="width: 60px">
                    </td>
                    <td>
                    </td>
                    <td class="iceNormalText">
                        <b>Route Details</b>
                    </td>
                    <td align="center" class="iceLable">
                        <b>Coordinates</b>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top">
                        <label class="iceLable">
                            Route 1</label>
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtRoute1" runat="server" class="iceTextBox" Style="width: 200px" MaxLength="149"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRouteDetails1" TextMode="MultiLine" runat="server" class="iceTextBox"  MaxLength="1000"
                            Style="width: 400px" Rows="3"></asp:TextBox>
                    </td>
                    <td align="center" style="vertical-align: top">
                     <div>
                        <a id="lnkbtnone1" href="#" runat="server"><b>Add/View</b></a>
                    </div>
                     <script type="text/javascript" language="javascript">
                         function openpopupOne(RouteId, Route) {
                             var left = (screen.width - 700) / 2;
                             var top = (screen.height - 600) / 4;
                             open('RouteCoordinates.aspx?RouteId=' + RouteId + '&Route=' + Route, 'Routeone', 'scrollbars=1,width=700px,height=600px,top=' + top + ', left=' + left);
                         }
                    </script>
                       <%-- <asp:LinkButton ID="lnkbtnone" Text="Add/View" runat="server" OnClick="lnkbtnone_Click"></asp:LinkButton>--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top">
                        <label class="iceLable">
                            Route 2</label>
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtRoute2" runat="server" class="iceTextBox" Style="width: 200px" MaxLength="149"></asp:TextBox>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:TextBox ID="txtRouteDetails2" TextMode="MultiLine" runat="server" class="iceTextBox"  MaxLength="1000"
                            Style="width: 400px" Rows="3"></asp:TextBox>
                    </td>
                    <td align="center" style="vertical-align: top">
                        <div>
                        <a id="lnkbtnTwo" href="#" runat="server"><b>Add/View</b></a>
                    </div>
                     <script type="text/javascript" language="javascript">
                         function openpopupTwo(RouteId, Route) {
                             var left = (screen.width - 700) / 2;
                             var top = (screen.height - 600) / 4;
                             open('RouteCoordinates.aspx?RouteId=' + RouteId + '&Route=' + Route, 'Routetwo', 'scrollbars=1,width=700px,height=600px,top=' + top + ', left=' + left);
                         }
                        </script>
                       <%-- <asp:LinkButton ID="lnkbtnTwo" Text="Add/View" runat="server" OnClick="lnkbtnTwo_Click"></asp:LinkButton>--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top">
                        <label class="iceLable">
                            Route 3</label>
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtRoute3" runat="server" class="iceTextBox" Style="width: 200px" MaxLength="149"></asp:TextBox>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:TextBox ID="txtRouteDetails3" TextMode="MultiLine" runat="server" class="iceTextBox"  MaxLength="1000"
                            Style="width: 400px" Rows="3"></asp:TextBox>
                    </td>
                    <td align="center" style="vertical-align: top">
                     <div>
                        <a id="lnkbtnthree" href="#" runat="server"><b>Add/View</b></a>
                    </div>
                     <script type="text/javascript" language="javascript">
                         function openpopupThree(RouteId, Route) {
                             var left = (screen.width - 700) / 2;
                             var top = (screen.height - 600) / 4;
                             open('RouteCoordinates.aspx?RouteId=' + RouteId + '&Route=' + Route, 'Routethreee', 'scrollbars=1,width=700px,height=600px,top=' + top + ', left=' + left);
                         }
            </script>
                      <%--  <asp:LinkButton ID="lnkbtnthree" Text="Add/View" runat="server" OnClick="lnkbtnthree_Click"></asp:LinkButton>--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Panel ID="pnlRouteSelCriteria" runat="server">
                            <a id="lnkRouteIdentification" href="#" onclick="OpenRouteIdentification('RouteIdentification.aspx');">Route Selection Criteria</a>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton"
                                        OnClick="btn_Save_Click" OnClientClick="return Chktext(this);" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Clear" runat="server" Text="Clear" class="icebutton" OnClick="btn_Clear_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="ApproverButton" runat="server" Text="Get Approval" class="icebutton"
                                        OnClick="ApproverButton_Click" OnClientClick="DisableOnSave(this);" UseSubmitBehavior="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
     <%-- <asp:Label ID="StatusLabel" runat="server" style="text-decoration:blink;color:Red;font-family:Arial;font-size:18px;font-weight:bold"/>   --%>
<ucMsg:ApprovalMessage ID="ApprovalMessage1" runat="server" />     
     <div align="right"> <a id="lnkApprovalComments" href="#" runat="server" visible="True"><b>Approver Comments</b></a></div>

    <asp:Panel ID="pnlApprovel" runat="server">
        <fieldset class="icePnlinner">
            <legend>Final Route Approved</legend>
            <table align="center" border="0" width="96%">
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Final Route</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFinalRoute" runat="server" Style="width: 200px"></asp:TextBox>
                    </td>
                    <td rowspan="3">
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text=""
                            Style="width: 400px; height: 80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Approved By</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtapprovedby" runat="server" Style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <label class="iceLable">
                            Approved Date</label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtapprovedDate" runat="server" Style="width: 100px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>

    <script type="text/javascript">
        function OpenRouteIdentification(url) {
            var left = (screen.width - 700) / 2;
            var top = (screen.height - 500) / 4;
            open(url, "WinIdent", 'width=700px,height=600px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function DisableOnSave(src) {
            src.disabled = true;
            src.value = 'Please Wait...';
        }
        function OpenApproverDocumnet(ProjectID, WorkFlowCode, pageCode, TrackHdrId) {
            var left = (screen.width - 850) / 2;
            var top = (screen.height - 350) / 4;
            open('../COMPENSATION/ConversationLog.aspx?ProjectID=' + ProjectID + '&WorkFlowCode=' + WorkFlowCode + '&pageCode=' + pageCode + '&TrackHdrId=' + TrackHdrId, 'ChangeRequest', 'width=850px,height=350px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function Chktext(src) {
            var tat1 = document.getElementById("<%= txtRoute1.ClientID  %>").value.toString().replace(/^\s+/, '');
            var tat2 = document.getElementById("<%= txtRoute2.ClientID  %>").value.toString().replace(/^\s+/, '');
            var tat3 = document.getElementById("<%= txtRoute3.ClientID  %>").value.toString().replace(/^\s+/, '');
            if (tat1 == '' && tat2 == '' && tat3 == '') {
                alert('Please enter at least one route to save data.');
                return false;
            }
            return true;
        }

    </script>
   
</asp:Content>
