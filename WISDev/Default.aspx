<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WISDev._Default" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <table width="95%" border="0" style="border-collapse: collapse">
        <tr>
            <td align="left" width="380px" valign="bottom">
                <asp:PlaceHolder ID="ProjectsSubmenu" runat="server">
                    <div id="tablewidth" style="margin-left: 0px; vertical-align: middle;width: 420px;height: 220px; margin-bottom: 3px;">
                        <table width="100%" border="0" style="background-color: #2A75A9;" >
                            <tr>
                                <td style="height: 30px;" valign="middle">
                                    <label class="iceLable">
                                        &nbsp;<font color="white"><b>Quick Links</b></font>
                                    </label>
                                    <%-- </marque>--%>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="margin-top: -5px; background-color: #BBBBE1;">
                            <tr>
                                <td >
                                    <div style="padding-top: 0px; padding-bottom: 2px; margin-top: -90px; margin-left: 5px">
                                        <b>
                                            <asp:LinkButton ID="lnkRecentPAPS" Text="Recent PAPs" runat="server" CssClass="iceLable">
                                            </asp:LinkButton></b>
                                        <ajaxToolkit:HoverMenuExtender ID="hmeRecentPAPS" HoverCssClass="hovermenu" PopupControlID="pnlRecentPAPS"
                                            PopupPosition="Right" TargetControlID="lnkRecentPAPS" runat="server">
                                        </ajaxToolkit:HoverMenuExtender>
                                        <asp:Panel ID="pnlRecentPAPS" CssClass="hovermenu" runat="server">
                                            <asp:Repeater ID="rptRecentPAPS" runat="server" OnItemCommand="rptRecentPAPS_ItemCommand">
                                                <ItemTemplate>
                                                    <div style="padding: 2px">
                                                        <asp:LinkButton ID="lnkPAP" CommandName="LoadPAP" CommandArgument='<%#Eval("ProjectID") + "," + Eval("HouseholdID") + "," + Eval("ProjectCode")%>'
                                                            Text='<%#Eval("PAPName")%>' runat="server" CssClass="iceLable" Font-Underline="true"></asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                    </div>
                                    <div style="padding-top: 3px; padding-bottom: 2px; margin-left: 5px">
                                        <b>
                                            <asp:HyperLink ID="hypUploadDoc" NavigateUrl="~/UI/DOCUMENT/UploadDocument.aspx"
                                                Text="Upload Document" EnableViewState="false" runat="server">
                                            </asp:HyperLink></b>
                                    </div>
                                    <div style="padding-top: 3px; padding-bottom: 2px; margin-left: 5px">
                                        <b>
                                            <asp:HyperLink ID="hypApprovals" NavigateUrl="~/UI/MYACTIVITIES/MyTasks_Approval.aspx"
                                                Text="Approvals" EnableViewState="false" runat="server">
                                            </asp:HyperLink></b>
                                    </div>
                                </td>
                                <td style="height: 183px" id="tdempty">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:PlaceHolder>
            </td>
            <td align="left">
                <asp:PlaceHolder ID="phProjects" runat="server">
                    <table style="vertical-align: top; margin-top: -6px">
                        <tr>
                            <td>
                                <label class="iceLable">
                                    Change Project:
                                </label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpProject" runat="server" Width="320px" AutoPostBack="true"
                                    AppendDataBoundItems="true" OnSelectedIndexChanged="drpProject_SelectedIndexChanged"
                                    CssClass="iceTextBox">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
                <asp:Chart ID="PAPStatusChart" BackColor="#BBBBE1" runat="server" Width="770px" Height="220px">
                    <Series>
                        <asp:Series Name="Series1" LegendText="PAPs" Color="Blue" Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="Series2" LegendText="Paid" Color="Green" Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="Series3" LegendText="Pending" Color="Red" Legend="Legend1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="Status Count" IsLabelAutoFit="true">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" BackColor="#BBBBE1">
                            <CellColumns>
                            </CellColumns>
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Text="Projectwise PAP Status" Font="Helvetica, 10pt, style=Bold" TextOrientation="Horizontal"
                            Alignment="TopCenter">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td align="left" style="vertical-align: top">
                <asp:Chart ID="ProjectStatusPieChart" BackColor="#BBBBE1" Height="220px" Width="420px"
                    runat="server" ViewStateMode="Disabled">
                    <Series>
                        <asp:Series ChartType="Pie" Name="Series1" IsValueShownAsLabel="true" LabelForeColor="White">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BackColor="#BBBBE1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="ProjectStatus" Docking="Right" Alignment="Center" ForeColor="DarkBlue"
                            LegendItemOrder="SameAsSeriesOrder" BackColor="#BBBBE1">
                            <CellColumns>
                            </CellColumns>
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Text="Project Status (in %)" Font="Microsoft Sans Serif, 10pt, style=Bold"
                            TextOrientation="Horizontal" Alignment="TopCenter">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
            </td>
            <td align="left">
                <asp:Chart ID="ProjectStatusSplineChart" BackColor="#BBBBE1" Height="220px" runat="server"
                     Width="770px">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Spline" LegendText="Budgeted" Color="Green"
                            IsValueShownAsLabel="false" Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="Series2" ChartType="Spline" LegendText="Actual" Color="Red" IsValueShownAsLabel="false"
                            Legend="Legend1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX Title="Period" IsLabelAutoFit="True">
                                <LabelStyle Angle="0" Interval="1" />
                            </AxisX>
                            <AxisY Title="Expense" IsLabelAutoFit="true">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Legend1" BackColor="#BBBBE1">
                            <CellColumns>
                            </CellColumns>
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Text="Budgeted v/s Actual Expense" Font="Helvetica, 10pt, style=Bold"
                            TextOrientation="Horizontal" Alignment="TopCenter">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        _graph1 = document.getElementById('<%=ProjectStatusSplineChart.ClientID%>');
        _graph2 = document.getElementById('<%=PAPStatusChart.ClientID%>');
        if (_graph1 != null) {
            scrWidth = screen.availWidth;
            _graph1.style.width = parseInt(scrWidth - 520).toString() + "px";
            _graph2.style.width = parseInt(scrWidth - 520).toString() + "px";
            var tds = document.getElementById('tdempty');
            if (isIE()) {
                tds.style.height = 190;
            } else {
                tds.style.height = '183px';
            }
        }


        function isIE() {
            var myNav = navigator.userAgent.toLowerCase();
            return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
        }

    </script>
</asp:Content>
