<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SharedApprovals.aspx.cs"
    Inherits="WIS.SharedApprovals" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server" EnablePageMethods="true">
    </ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-impromptu.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/page_specific.css" />
    <div id="divAll">
        <asp:Panel ID="pnlMytaskApprovel" runat="server">
            <fieldset class="icePnlinner">
                <legend>Approval </legend>
                <asp:HiddenField ID="HfTHeaderID" runat="server" Value="0" />
                <asp:Panel ID="pnlSelection" runat="server" Width="100%">
                    <table style="width: 100%">
                        <tr>
                            <td class="iceLable" align="right">View Approvals Of : 
                            </td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlAssignFrom" runat="server" CssClass="iceDropDown" Style="width: 190px"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlAssignTo" runat="server" ErrorMessage="Select User to view Approvals"
                                    InitialValue="0" ControlToValidate="ddlAssignFrom" Display="None" ValidationGroup="vgShared"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:ValidationSummary ID="valsumTempAuth" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgShared" runat="server" />
                                <asp:Button ID="btnView" CssClass="icebutton" Text="View" runat="server" OnClick="btnView_Click"
                                    ValidationGroup="vgShared" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlMyTasksSele" runat="server" Visible="false" Width="100%">
                    <div style="float: right; width: 100%;">
                        <asp:LinkButton ID="lnkSelectionCh" Text="Change Selection" runat="server" OnClick="lnkSelectionCh_Click"></asp:LinkButton>
                    </div>
                    <br />
                    <asp:GridView ID="GrdMyTaskApproval" runat="server" CssClass="gridStyle" CellPadding="4"
                        CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                        OnRowDataBound="GrdMyTaskApproval_RowDataBound" OnPageIndexChanging="GrdMyTaskApproval_PageIndexChanging"
                        OnRowCommand="GrdMyTaskApproval_RowCommand" EmptyDataRowStyle-BackColor="Silver"
                        EmptyDataText="No Records Found" EmptyDataRowStyle-HorizontalAlign="Center">
                        <RowStyle CssClass="gridRowStyle" />
                        <AlternatingRowStyle CssClass="gridAlternateRow" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField HeaderText="Project Name" HeaderStyle-HorizontalAlign="Center" DataField="ProjectName" HeaderStyle-Width="30%">
                    </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Project Name" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrProjectName" runat="server" Text='<%#Eval("ProjectName") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Module" HeaderStyle-HorizontalAlign="Center" DataField="ModuleName"></asp:BoundField>
                            <asp:TemplateField HeaderText="Pending" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkPendingCount" runat="server" Text='<%#Eval("PendingCount") %>'
                                        CommandArgument='<%#Eval("PendingCount") %>' CommandName="ClickPendingCount">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkapprovedCount" runat="server" Text='<%#Eval("ApprovedCount") %>'
                                        CommandArgument='<%#Eval("ApprovedCount") %>' CommandName="ClickApprovedCount">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Declined" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDeclinedCount" runat="server" Text='<%#Eval("DeclinedCount") %>'
                                        CommandArgument='<%#Eval("DeclinedCount") %>' CommandName="ClickDeclinedCount">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProjectID" Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectID" runat="server" Text='<%#Eval("PROJECTID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ModuleId" Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblModuleId" runat="server" Text='<%#Eval("MODULEID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </fieldset>
        </asp:Panel>

        <asp:Panel ID="PnlProjectDtl" runat="server" Visible="false">
            <fieldset class="icePnlinner">
                <legend>
                    <asp:Label ID="lblTrackerDetail" runat="server" Visible="true"></asp:Label></legend>
                <asp:GridView ID="GrdProjectDtl" runat="server" CssClass="gridStyle" CellPadding="4"
                    CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                    OnRowCommand="GrdProjectDtl_RowCommand" OnPageIndexChanging="GrdProjectDtl_PageIndexChanging"
                    EmptyDataRowStyle-BackColor="Silver" EmptyDataText="No Records Found" EmptyDataRowStyle-HorizontalAlign="Center">
                    <RowStyle CssClass="gridRowStyle" />
                    <AlternatingRowStyle CssClass="gridAlternateRow" />
                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                    <HeaderStyle CssClass="gridHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No.">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Workflow Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="18%" />
                            <ItemTemplate>
                                <%--'<%#Eval("WORKFLOWCODE") %>'--%>
                                <asp:LinkButton ID="linkWorkflowcode" runat="server" Text='<%#Eval("WCODE") %>' CommandArgument='<%#Eval("WORKFLOWCODE") %>'
                                    CommandName="ClickWorkflow">                               
                                </asp:LinkButton>
                                <asp:Literal ID="litWORKFLOWCODE" Text='<%#Eval("WORKFLOWCODE") %>' Visible="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" DataField="DESCRIPTION"></asp:BoundField>
                        <asp:BoundField HeaderText="TRACKERHEADERID" HeaderStyle-HorizontalAlign="Center"
                            DataField="TRACKERHEADERID" Visible="false"></asp:BoundField>
                        <%--   <asp:BoundField HeaderText="Date Sent" HeaderStyle-HorizontalAlign="Center" DataField="UpdatedDate"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Request Date">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <ItemTemplate>
                                <asp:Label ID="lblUpdateDate" runat="server" Text='<%#Eval("UpdatedDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action Taken Date">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <ItemTemplate>
                                <asp:Label ID="lblActionTakenDate" runat="server" Text='<%#Eval("ActionTakenDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WORKFLOWAPPROVERID" Visible="False" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblWORKFLOWAPPROVERID" runat="server" Text='<%#Eval("WORKFLOWAPPROVERID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WORKFLOWDEFINITIONID" Visible="False" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblWORKFLOWDEFINITIONID" runat="server" Text='<%#Eval("WorkflowdefinationID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WorkFlowId" Visible="false" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblWorkFlowId" runat="server" Text='<%#Eval("WorkFlowId") %>'></asp:Label>
                                <asp:Label ID="lblHHId" runat="server" Text='<%#Eval("HHID") %>'></asp:Label>
                                <asp:Label ID="lblApproverLevel" runat="server" Text='<%#Eval("ApproverLevel") %>'></asp:Label>
                                <asp:Label ID="lblElementID" runat="server" Text='<%#Eval("ElementID") %>'></asp:Label>
                                <asp:Literal ID="litTrackerDetailID" runat="server" Text='<%#Eval("TrackerDetailID") %>'></asp:Literal>
                                <asp:Literal ID="litPageCode" runat="server" Text='<%#Eval("PageCode") %>'></asp:Literal>
                                <asp:Literal ID="litWorkflowDes" runat="server" Text='<%#Eval("WCODE") %>'></asp:Literal>
                                <asp:Literal ID="litTrackHdrId" runat="server" Text='<%#Eval("TrackHdrId") %>'></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </asp:Panel>

        <!-- Interim grid to show the list of Payment Batches -->
        <!--  <asp:Panel ID="pnlPaymentBatches" runat="server" Visible="false">
        <fieldset class="icePnlinner">
            <legend>Payment Batches</legend>
            <asp:GridView ID="grdPaymentBatches" runat="server" AllowSorting="True" CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%">
                <RowStyle CssClass="gridRowStyle" />
                <AlternatingRowStyle CssClass="gridAlternateRow" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                <FooterStyle CssClass="gridFooterStyle" />
                <EmptyDataTemplate>
                    <center>No Records Found</center>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="SI No.">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Batch No." HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lbl_CMP_BatchNo" Text='<%#Eval("CMP_BatchNo")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HHID" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lbl_PR_HHID" Text='<%#Eval("HHID")%>' Visible="true" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PAPName" HeaderText="Pap Name" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Payt_Description" HeaderText="Request For" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Amt_Requested" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lblRequestStatus" Text='<%#Eval("RequestStatus")%>' Visible="true"
                                runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("PAYT_REQUESTID") %>' runat="server" />
                            <asp:Label ID="lblPaymentRequestId" Text='<%#Eval("PAYT_REQUESTID")%>' Visible="false"
                                runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </asp:Panel> -->
        <!-- End Interim grid to show the list of Payment Batches -->

        <asp:MultiView ID="ApprovalMultiView" ActiveViewIndex="0" runat="server">
            <asp:View ID="ViewRTA" runat="server">
                <asp:Panel ID="pnlFinalPojectdEtail" runat="server" Visible="false">
                    <fieldset class="icePnlinner">
                        <legend>
                            <asp:Label ID="lblFinalProjectDetl" runat="server" Visible="True"></asp:Label></legend>

                        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
                            <asp:GridView ID="grdFinalProjectDtl" runat="server" CssClass="gridStyle" CellPadding="4"
                                CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                                EmptyDataRowStyle-BackColor="Silver" EmptyDataText="No Records Found" EmptyDataRowStyle-HorizontalAlign="Center"
                                OnRowDataBound="grdFinalProjectDtl_RowDataBound">
                                <RowStyle CssClass="gridRowStyle" />
                                <AlternatingRowStyle CssClass="gridAlternateRow" />
                                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                                <HeaderStyle CssClass="gridHeaderStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectRoute" runat="server" CommandName="ClickWorkflow" onclick="valCheckBoxes(this);" />
                                            <asp:Literal ID="ltlIsFinal" runat="server" Text='<%#Eval("ISFINAL") %>' Visible="false"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Route No" HeaderStyle-HorizontalAlign="Center" DataField="ROUTENAME">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" DataField="ROUTEDETAILS">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Score" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblScore" runat="server" Text='<%#Eval("TOTALROUTESCORE") %>'
                                                OnClick="lblScore_Click1">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Map" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblMap" runat="server" Text="View Map"
                                                OnClick="lblViewMap_Click1">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RouteID" Visible="False" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRouteID" runat="server" Text='<%#Eval("ROUTEID") %>'></asp:Label>
                                            <asp:Label ID="lblHHId" runat="server" Text='<%#Eval("HHID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <%-- </fieldset>
                <fieldset class="icePnl1">--%>
                    </fieldset>
                </asp:Panel>
            </asp:View>
            <asp:View ID="ViewCON" runat="server">
                <fieldset class="icePnlinner">
                    <legend id="ViewCR" runat="server"></legend>
                    <div style="float: right" class="iceLable">
                        <asp:Label ID="LblHhidBatch" CssClass="iceStatusLinks" Style="float: left; width: 100px; border-right: 1px solid;" runat="server" Text=""></asp:Label>&nbsp;
                    <a id="lnkPageSource" href="#" class="iceStatusLinks" style="float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Details</b></a>&nbsp;&nbsp;
                    <span style="display: none;" runat="server" id="spanPackage">
                        <a id="lnkPackageDocument" href="#" class="iceStatusLinks" style="float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Package</b></a>&nbsp;&nbsp; 
                        <a id="lnkPapPhoto" href="#" class="iceStatusLinks" style="float: left; width: 100px; border-right: 1px solid;" runat="server"><b>View Photo</b></a>&nbsp;&nbsp;
                        <a id="lnkUPloadDoclistSup" href="#" class="iceStatusLinks" style="float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Attachments</b></a>&nbsp;&nbsp;</span>
                        <span style="display: none;" runat="server" id="spanReviewCom">
                            <a id="lnkAppReviewCom" href="#" class="iceStatusLinks" style="float: left; width: 150px; border-right: 1px solid;" runat="server"><b>Review Comments</b></a>&nbsp;&nbsp;
                        </span>
                        <a id="lnkUPloadDoclist" href="#" class="iceStatusLinks" style="float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Attachments</b></a>
                        <a id="lnkSendClarify" href="#" class="iceStatusLinks" style="display: none; float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Clarification</b></a>
                        <a id="lnkClarifyResponse" href="#" class="iceStatusLinks" style="display: none; float: left; width: 100px; border-right: 1px solid;" runat="server"><b>Responses</b></a>
                    </div>
                    <br />
                    <table border="0" cellpadding="2" cellspacing="5" width="100%">
                        <tr>
                            <td style="width: 15%">
                                <asp:Label ID="ProjectCodeLabel" runat="server" CssClass="iceLable" Text="Project Code" />
                            </td>
                            <td style="width: 85%">
                                <asp:Label ID="GetProjCodeLabel" runat="server" CssClass="iceTextBox" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="ProjectNameLabel" runat="server" CssClass="iceLable" Text="Project Name" />
                            </td>
                            <td>
                                <asp:Label ID="GetProjNameLabel" runat="server" CssClass="iceTextBox" Width="300" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="EmailSubLabel" runat="server" CssClass="iceLable" Text="Email Subject" />
                            </td>
                            <td>
                                <asp:Label ID="getEmailSubLabel" runat="server" CssClass="iceTextBox" Width="300" />
                                <asp:Label ID="trackHeaderIDLabel" runat="server" CssClass="iceTextBox" />
                                <asp:Label ID="lblHHId0" runat="server" Text='<%#Eval("HHID") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="EmailBodyLabel" runat="server" CssClass="iceTextAeralarge" Height="150px"
                                    ReadOnly="true" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlPaymentRequestBatch" runat="server" Width="100%">
                                    <asp:GridView ID="grdPaymentRequestBatch" runat="server" AllowSorting="True" CellPadding="4"
                                        CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdPaymentRequestBatch_RowDataBound"
                                        OnRowCommand="grdPaymentRequestBatch_RowCommand" OnSelectedIndexChanged="grdPaymentRequestBatch_SelectedIndexChanged">
                                        <%-- OnRowDataBound="grdPaymentRequestBatch_RowDataBound">--%>
                                        <HeaderStyle CssClass="gridHeaderStyle" />
                                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                                        <FooterStyle CssClass="gridFooterStyle" />
                                        <RowStyle CssClass="gridRowStyle" />
                                        <EmptyDataTemplate>
                                            <center>
                                            No Records Found</center>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="SI No.">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch No." HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_CMP_BatchNo" Text='<%#Eval("CMP_BatchNo")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HHID" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif" CommandName="EditRow"
                                                        CommandArgument='<%#Eval("HHID") %>' runat="server">
                                                        <%#Eval("HHID_DISP")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HHID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_PR_HHID" Text='<%#Eval("HHID")%>' Visible="true" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12" />--%>
                                            <asp:BoundField DataField="PAPName" HeaderText="Pap Name" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Payt_Description" HeaderText="Request For" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Amt_Requested" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                                DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemTemplate>
                                                    <%-- <asp:Label ID="lblPaymentReqStatus" runat="server"></asp:Label>--%>
                                                    <asp:Label ID="lblRequestStatus" Text='<%#Eval("RequestStatus")%>' Visible="false"
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="lblRequestStatusShow" Text='<%#Eval("RequestStatusShow")%>' Visible="true"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                        CommandName="DeleteRow" CommandArgument='<%#Eval("PAYT_REQUESTID") %>' runat="server" />
                                                    <asp:Label ID="lblPaymentRequestId" Text='<%#Eval("PAYT_REQUESTID")%>' Visible="false"
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="LblStausLevel" Text='<%#Eval("StausLevel")%>' Visible="false"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlCDPABudget" runat="server" Width="100%">
                                    <asp:GridView ID="grdCDAPBudget" runat="server" CssClass="gridStyle" CellPadding="4"
                                        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%">
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
                                            <asp:BoundField DataField="CDAP_CATEGORYNAME" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="CDAP_SUBCATEGORYNAME" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="UNITNAME" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="QUANTITY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="RATEPERUNIT" HeaderText="Rate per Unit" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="CDAPBUDID" HeaderStyle-HorizontalAlign="Center" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Literal ID="litCDAPBUDID" Text='<%#Eval("Cdap_budgid") %>' runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
        </asp:MultiView>
        <asp:Panel ID="pnlAprovalFooter" runat="server" Visible="true" Width="100%">
            <div align="right">
                <a id="lnkAppComments" href="#" runat="server" visible="True"><b>View Comments</b></a>
            </div>
            <table width="100%">
                <tr>
                    <td colspan="2" align="center">Approver Comments
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:TextBox ID="txtapprovercomments" CssClass="iceTextBox" TextMode="MultiLine"
                            runat="server" Style="width: 750px; height: 65px;">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="BtnApprove" runat="server" class="icebutton" Style="width: 130px"
                            Text="Approve" OnClick="BtnApprove_Click" OnClientClick="DisableOnSave(this);"
                            UseSubmitBehavior="false" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnDecline" runat="server" class="icebutton" Style="width: 130px"
                            Text="Decline" OnClick="btnDecline_Click" OnClientClick="DisableOnSave(this);"
                            UseSubmitBehavior="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div style="display: none;">
            <asp:Button ID="btnUnlock" runat="server" OnClick="btnUnlock_Click" Text="UnLock" />
        </div>
    </div>
    <br />
    <script type="text/javascript" language="javascript">

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
        }

        function validateCheckBoxes() {
            var isValid = false;
            var count = 0;
            var gridView = document.getElementById('<%= grdFinalProjectDtl.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked == true) {
                            count = parseInt(count, 10) + parseInt(1, 10);
                            if (count > 1) {
                                inputs[0].checked = false;
                            }
                        }
                    }
                }
            }
            return false;
        }


        function valCheckBoxes(objRef) {
            var GridView = document.getElementById('<%= grdFinalProjectDtl.ClientID %>');

            var inputs = GridView.getElementsByTagName('input');
            var n = inputs.length;

            for (var i = 0; i < n; i++) {
                if (inputs[i].type == "checkbox" && inputs[i] != objRef) {

                    if (objRef.checked == true) {
                        inputs[i].checked = false;
                    }
                }
            }
        }

        function valPayCheckBoxes(objRef) {
            //Validate Checkboxes For Payment Request   onclick="valPayCheckBoxes(this);"
            if (objRef.checked) {
                //alert("hi");
                var isValid = false;
                var count = 0;
                var gridView = document.getElementById('<%= grdPaymentRequestBatch.ClientID %>');
                for (var i = 1; i < gridView.rows.length; i++) {
                    var inputs = gridView.rows[i].getElementsByTagName('input');
                    if (inputs != null) {
                        if (inputs[0] != undefined) {
                            if (inputs[0].type == "checkbox") {
                                if (inputs[0].checked == true) {
                                    inputs[0].checked = false;
                                }
                            }
                        }
                    }
                }
                objRef.checked = true;
            }
        }

        function OpenSourcePage(ProjectID, HHID, userID, ProjectCode, Mode, PageCode, ApprovalLevel) {
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            if (PageCode == 'HH') {
                open('../COMPENSATION/SOCIOECONOMIC/Household.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHINS') {
                open('../COMPENSATION/SOCIOECONOMIC/Institution.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHGMS' || PageCode == 'HHGOS') {
                open('../COMPENSATION/SOCIOECONOMIC/GroupOwnership.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-SE') {
                open('../COMPENSATION/SOCIOECONOMIC/PAPInfo.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHSEL') {
                open('../COMPENSATION/SOCIOECONOMIC/PAPLivelihood.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-LU') {
                open('../COMPENSATION/SOCIOECONOMIC/HouseholdRelation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-SA') {
                open('../COMPENSATION/SOCIOECONOMIC/HouseholdRelation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'Griev') {
                open('../COMPENSATION/SOCIOECONOMIC/AcreageValuation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHTRD') {
                open('../COMPENSATION/SOCIOECONOMIC/HolderTypeDetails.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-HD') {
                open('../COMPENSATION/SOCIOECONOMIC/PAPHealth.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHHIF') {
                open('../COMPENSATION/SOCIOECONOMIC/PAPHealth.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHNEH') {
                open('../COMPENSATION/SOCIOECONOMIC/Neighbours.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-WE') {
                open('../COMPENSATION/SOCIOECONOMIC/Welfare.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-MS') {
                open('../COMPENSATION/SOCIOECONOMIC/Major_shocks.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-SC') {
                open('../COMPENSATION/SOCIOECONOMIC/SocioConcerns.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHLHH') {
                open('../COMPENSATION/SOCIOECONOMIC/LandInfoRespondents.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HHMCE') {
                open('../COMPENSATION/SOCIOECONOMIC/LandInfoRespondents.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HLION') {
                open('../COMPENSATION/SOCIOECONOMIC/LandInfoRespondentsOn.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HLIOF') {
                open('../COMPENSATION/SOCIOECONOMIC/LandInfoRespondentsOff.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HH-AV') {
                open('../COMPENSATION/SOCIOECONOMIC/AcreageValuation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'IT-OP') {
                open('../COMPENSATION/SURVEY/LandInfo.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'IT-ML') {
                open('../COMPENSATION/SURVEY/LandInfo.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HVPBU') {
                open('../COMPENSATION/VALUATION/PermanenetBuilding.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HVNPS') {
                open('../COMPENSATION/VALUATION/Non-perm_structure.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HVDAC') {
                open('../COMPENSATION/VALUATION/DamagedCrops.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HV-CO') {
                open('../COMPENSATION/VALUATION/Crops.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HV-GR') {
                open('../COMPENSATION/VALUATION/Grave.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HVFEN') {
                open('../COMPENSATION/VALUATION/Fence.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HVCUP') {
                open('../COMPENSATION/VALUATION/CulturProperties.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'HFVAL') {
                open('../COMPENSATION/VALUATION/Final_valuation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'CRGRA') {
                open('../COMPENSATION/GRIEVANCES/Grievances.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'RFPRI' || PageCode == 'PAYVR') {
                open('../COMPENSATION/PackagePaymentRequest.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'PAYRQ') {
                open('../COMPENSATION/PackagePaymentRequest.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'CPREV' || PageCode == 'LND' || PageCode == 'PERID' || PageCode == 'DEL1' || PageCode == 'DEL2' || PageCode == 'DEL3A' || PageCode == 'CNS35' || PageCode == 'RCASH' || PageCode == 'LND' || PageCode == 'DEFN' || PageCode == 'CNS39' ||
                        PageCode == 'DEL3B' || PageCode == 'PERID' || PageCode == 'MNDOC' || PageCode == 'DCRP' || PageCode == 'OPTDS' || PageCode == 'CRP' || PageCode == 'FIXT' || PageCode == 'ATTR' || PageCode == 'RES' || PageCode == 'BLDG') {
                open('../COMPENSATION/CompensationPackages.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode + '&ApprovalLevel=' + ApprovalLevel, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'PKREV') {
                open('../COMPENSATION/PackagePaymentRequest.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'CRFND') {
                open('../COMPENSATION/PaymentProcessing.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'CREND') {
                open('../COMPENSATION/PackageClosingInfo.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'NEG' || PageCode == 'NEGC' || PageCode == 'NEGL' || PageCode == 'NEGF' || PageCode == 'NEGR'
            || PageCode == 'NEGD' || PageCode == 'NEGCU') {
                open('../COMPENSATION/VALUATION/Final_valuation.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
            else if (PageCode == 'CDAPB') {
                open('../COMPENSATION/CDAP-Budget.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
            }
        }

        function OpenReviewCom(ProjectID, HHID) {
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('../COMPENSATION/ReviewLog.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID, 'ReviewComments', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }


        function OpenDocumnetlist(ProjectID, HHID, userID, ProjectCode, Mode) {
            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('../DOCUMENT/UploadDocument.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&Mode=' + Mode, 'ViewPagedetailsRead', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=650px,top=' + top + ', left=' + left);
        }

        function DisableOnSave(src) {
            src.disabled = true;
            src.value = 'Please Wait...';
        }

        function OpenViewPhoto(ProjectID, HHID, userID, ProjectCode, PhotoModule) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../ViewPhotoDocumnet.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&PhotoModule=' + PhotoModule, 'Uploadphoto', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

        function OpenApproverDocumnet(ProjectID, WorkFlowCode, pageCode, TrackHdrId, BatchNo) {
            var left = (screen.width - 850) / 2;
            var top = (screen.height - 350) / 4;
            open('../COMPENSATION/ConversationLog.aspx?ProjectID=' + ProjectID + '&WorkFlowCode=' + WorkFlowCode + '&pageCode=' + pageCode + '&TrackHdrId=' + TrackHdrId + '&BatchNo=' + BatchNo, 'ChangeRequest', 'width=850px,height=350px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenClarify(UserID, HHID) {
            var left = (screen.width - 850) / 2;
            var top = (screen.height - 350) / 4;
            open('../ClarificationPop.aspx?UserID=' + UserID + '&HHID=' + HHID, 'BatchComments', 'width=850px,height=350px,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenBatchComments(BatchNo, HHID) {
            var left = (screen.width - 850) / 2;
            var top = (screen.height - 350) / 4;
            open('../COMPENSATION/BatchComments.aspx?BatchNo=' + BatchNo + '&HHID=' + HHID, 'BatchComments', 'width=850px,height=350px,scrollbars=1,top=' + top + ', left=' + left);
        }

        document.getElementById('divAll').onclick = function () {
            isDirty = 0;
            setTimeout(function () { setDirtyText(); }, 100);
        };

        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var tat1 = document.getElementById("<%= HfTHeaderID.ClientID  %>");
            if (tat1.value.toString() == '0') {
                isDirty = 0;
            }
            else {
                isDirty = 1;
                //txtyes = 1;
            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                var tat1 = document.getElementById("<%= HfTHeaderID.ClientID  %>").value.toString();
                PageMethods.UnlockData(tat1);
                var left = screen.width;
                var top = screen.height;
                open('UnlockNew.aspx?HeaderID=' + tat1, 'Unlock', 'width=1px,height=1px,resizable=0,scrollbars=0,top=' + top + ', left=' + left);
            }
        }

    </script>
</asp:Content>

