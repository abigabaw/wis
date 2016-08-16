<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="BatchPayment.aspx.cs" Inherits="WIS.BatchPayment" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Styles/page_specific.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function OpenBatchStatus() {

            var BatchNo = "<%=ViewState["CMP_BATCHNO"]%>";
            var ProjectID = "<%=Session["PROJECT_ID"]%>"

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            // open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYRQ&ProjectID=' + ProjectID + '&BatchNo=' + BatchNo, 'Package Status', 'width=960px,height=650px,resizable=0,scrollbars=no,top=' + top + ', left=' + left);
            // function modalWin() {
                if (window.showModalDialog) {
                    window.showModalDialog('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYRQ&ProjectID=' + ProjectID + '&BatchNo=' + BatchNo, 'Package Status', "name", "dialogWidth:255px;dialogHeight:250px");
                } else {
                    window.open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYRQ&ProjectID=' + ProjectID + '&BatchNo=' + BatchNo, 'Package Status', 'name', 'height=255,width=250,toolbar=no,directories=no,status=no,continued from previous linemenubar=no,scrollbars=no,resizable=no ,modal=yes');
                }
            //} 
        }

        function OpenBatchPrint() {

            var BatchNo = "<%=ViewState["CMP_BATCHNO"]%>";
            var ProjectID = "<%=Session["PROJECT_ID"]%>"

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=BATCHPRINT&ProjectID=' + ProjectID + '&BatchNo=' + BatchNo, 'Package Status', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }

        function OpenBatchComments() {

            var BatchNo = "<%=ViewState["CMP_BATCHNO"]%>";
            var ProjectID = "<%=Session["PROJECT_ID"]%>"

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYRQCMTS&ProjectID=' + ProjectID + '&BatchNo=' + BatchNo, 'Package Status', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend id="lgnd" runat="server">Payment Request Batch</legend>
        <table width="100%">
            <tr>
                <td>
                    <fieldset class="icePnlinner">
                        <legend>Search Batch</legend>
                        <table width="95%" align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelBatch" Text="Batch #" runat="server" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBachNo" runat="server" CssClass="iceTextBox" MaxLength="10" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtBachNo" FilterType="Numbers"
                                        TargetControlID="txtBachNo" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:Label ID="LabelFromDate" Text="From Date" runat="server" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="dpBatchFromDate" runat="server" CssClass="iceTextBox" />
                                    <ajaxToolkit:CalendarExtender ID="caldpBatchFromDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpBatchFromDate" />
                                </td>
                                <td>
                                    <asp:Label ID="LabelTODate" Text="To Date" runat="server" CssClass="iceLable" />
                                </td>
                                <td>
                                    <asp:TextBox ID="dpBatchToDate" runat="server" CssClass="iceTextBox" />
                                    <ajaxToolkit:CalendarExtender ID="CaldpBatchToDate" runat="server" CssClass="WISCalendarStyle" TargetControlID="dpBatchToDate" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSearchBatch" runat="server" Text="Search Batch"
                                                    CssClass="icebutton" OnClick="btnSearchBatch_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="icebutton"
                                                    OnClick="ButtonClear_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblBatchNo" runat="server"></asp:Label>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td style="width: 30%" valign="top">
                    <fieldset class="icePnlinner">
                        <legend>Batch List</legend>
                        <div align="center">
                            <label class="iceLable">T = Total, A = Approved,  D = Declined,  P = Pending</label></div>
                        <asp:Label ID="lblMessage" Text="" Visible="false" runat="server" CssClass="iceLable"></asp:Label>
                        <asp:Repeater ID="rptrBatchPayment" runat="server"
                            OnItemCommand="rptrBatchPayment_ItemCommand"
                            OnItemDataBound="rptrBatchPayment_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" width="100%" class="table table-bordered table-striped">

                                    <tr>
                                        <th style="width: 20%;" class="gridHeaderStyle">Batch NO
                                        </th>
                                        <th style="width: 45%;" class="gridHeaderStyle">Date
                                        </th>
                                        <th style="width: 08%;" class="gridHeaderStyle">T
                                        </th>
                                        <th style="width: 08%;" class="gridHeaderStyle">A
                                        </th>
                                        <th style="width: 08%;" class="gridHeaderStyle">D
                                        </th>
                                        <th style="width: 08%;" class="gridHeaderStyle">P
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                </center>
                                <tr class="gridRowStyle">
                                    <td align="center" style="color: Black;">
                                        <asp:LinkButton ID="lnkBatchDate" CommandName="GetPAPS" CommandArgument='<%#Eval("CMP_BatchNo")%>'
                                            runat="server">
                                        <%#Eval("CMP_BatchNo")%> 
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnBatchNo" runat="server" Value='<%#Eval("CMP_BatchNo")%>' />
                                        <asp:HiddenField ID="hdnBatchDate" runat="server" Value='<%#Eval("BATCHCREATEDDATE")%>' />
                                    </td>
                                    <td>
                                        <%#Eval("BATCHCREATEDDATE")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("TOTALCount")%>
                                    </td>
                                    <td align="center">
                                        <asp:Literal ID="ltrtotalApproval" runat="server" Text="" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal ID="ltrtotalDeclined" runat="server" Text="" />
                                    </td>
                                    <td align="center">
                                        <%#Eval("TOTALPending")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </fieldset>
                </td>
                <td style="width: 75%" valign="top">
                    <asp:Panel ID="pnlBatchPAPS" Visible="false" runat="server">
                        <fieldset class="icePnlinner">
                            <legend id="lgndBatch" runat="server">Batch</legend>
                            <br />
                            <asp:Panel ID="pnlBatchScroll" runat="server" ScrollBars="Both" Width="600px" Height="350px">
                                <asp:GridView ID="grdPaymentRequestBatch" AllowPaging="false" runat="server" AllowSorting="True" CellPadding="4"
                                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdPaymentRequestBatch_RowCommand"
                                    OnRowDataBound="grdPaymentRequestBatch_RowDataBound" OnPageIndexChanging="grdPaymentRequestBatch_PageIndexChanging">
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
                                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HHID" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblHHID_DISP" runat="server" Text='<%#Eval("HHID_DISP")%>'></asp:Label>
                                                <asp:Label ID="lblHHID" runat="server" Visible="false" Text='<%#Eval("HHID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField DataField="HHID" HeaderText="HHID" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="12"/>--%>
                                        <asp:BoundField DataField="PAPName" HeaderText="PAP Name" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                                        <%-- <asp:BoundField DataField="BATCHSTATUS" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />--%>
                                        <asp:BoundField DataField="Amt_Requested" DataFormatString="{0:N2}" HeaderText="Requested Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="TotalAmount" DataFormatString="{0:N2}" HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="InKindValue" DataFormatString="{0:N2}" HeaderText="In-Kind Compensation (In Acres)" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentReqStatus" runat="server"></asp:Label>
                                                <%-- <asp:Label ID="lblRequestStatus2" Text='<%#Eval("RequestStatus")%>' Visible="false"
                                runat="server">--%>
                                                <asp:Label ID="lblRequestStatus" Text='<%#Eval("RequestStatus")%>' Visible="true"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                    CommandName="DeleteRow" CommandArgument='<%#Eval("PAYT_REQUESTID") %>' runat="server"
                                                    OnClientClick="return DeleteRecord();" />
                                                <asp:Label ID="lblPaymentRequestId" Text='<%#Eval("PAYT_REQUESTID")%>' Visible="false"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <table width="600px">
                                <tr>
                                    <td align="center">
                                        <span id="BatchStatusLinks" runat="server">
                                            <a id="lnkPrintBatchDetail" class="iceStatusLinks" href="#" onclick="OpenBatchPrint();" runat="server" style="border-right: 1px solid; float:left; width:15%;"><b>Print Details</b></a>
                                            <a href="#" id="lnkPackageStatus" class="iceStatusLinks" onclick="OpenBatchStatus();" runat="server" style="border-right: 1px solid; float:left; width:15%;"><b>View Status</b></a>
                                                                                <a href="#" id="lnkViewBatchComments" style="float:left; width:20%;" class="iceStatusLinks" onclick="OpenBatchComments();" runat="server"><b>View Comments</b></a>
                                        </span>
                                            </td>
                                </tr>

                            </table>
                            <table width="600px">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSubmitForPayment" runat="server" Text="Submit For Payment" CssClass="icebutton"
                                            Style="width: 150px" OnClick="btnSubmitForPayment_Click" />
                                        <asp:HiddenField ID="hdnPendingRequestCount" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummary" HeaderText="Please correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgCompResettle" runat="server" />
    </fieldset>
    <script type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode, BatchNo) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode + '&BatchNo=' + BatchNo, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }

    </script>
</asp:Content>
