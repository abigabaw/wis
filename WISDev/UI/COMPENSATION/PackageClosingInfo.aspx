<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="PackageClosingInfo.aspx.cs" Inherits="WIS.PackageClosingInfo" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Src="HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tkManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <%--<fieldset class="icePnlinner">--%>
    <%--   <legend>Closing Information</legend>--%>
    <asp:HiddenField ID="HfPaymentStatus" runat="server" Value="None" />
    <fieldset class="icePnlinner">
        <legend>Summary</legend>
        <table border="0" cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td style="width: 25%">
                    &nbsp;
                </td>
                <td style="width: 30%">
                    <label class="iceLable">
                        Cash Compensation (USH)</label>
                </td>
                <td align="left" style="width: 190px">
                    <label class="iceLable">
                        In-Kind Compensation
                    </label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Land</label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 120px"/>--%>
                    <asp:TextBox ID="txtCashLand" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInKindLand" runat="server" Enabled="false" onkeypress="return CheckDecimal(event, this)"
                        class="iceTextBox" Style="width: 185px" MaxLength="12" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        TargetControlID="txtInKindLand" ValidChars="." FilterType="Numbers,Custom">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    Acres
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Residential Structures</label>
                </td>
                <td>
                    <asp:TextBox ID="txtResStructure" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left">
                    <asp:TextBox ID="txtResidentialStructure" runat="server" Enabled="false" class="iceTextBox" Style="width: 185px;" />
                    <asp:DropDownList ID="ddlResidentialStructure" Visible="false" runat="server" AutoPostBack="false">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="UETCL Resettlement House">UETCL Resettlement House</asp:ListItem>
                        <asp:ListItem Value="UETCL Building Materials+Labour">UETCL Building Materials+Labour</asp:ListItem>
                        <asp:ListItem Value="NA">NA</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlResidentialStructure"
                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                        IsSorted="true" />
                    <%--<asp:RequiredFieldValidator ID="rfvResidentialStructure" runat="server" ControlToValidate="ddlResidentialStructure"
                            ValidationGroup="vgSummery" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Residential Structures (In-Kind Compensation)"
                            Display="None"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Fixtures</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixture" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left" style="width: 230px">
                    NA
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Crops</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCrops" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left">
                    NA
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Compensation for Culture property</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCulutralProperty" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left">
                    NA
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Facilitation/GOU allowance/Others</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFacilitation" runat="server" Enabled="false" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtFacilitation" runat="server" TargetControlID="txtFacilitation"
                        FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    NA
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Damaged Crops</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDamaged" runat="server" class="iceTextBox" Style="width: 180px;
                        text-align: right;" />
                </td>
                <td align="left">
                    NA
                </td>
            </tr>
            <tr>
                <td>
                    <label class="iceLable">
                        Total Amount</label>
                </td>
                <td>
                    <b>
                        <asp:Label ID="lblTotalAmount" runat="server" Text="0" BorderWidth="0px" Style="width: 180px;
                            text-align: right; border: 1px"></asp:Label></b>
                </td>
                <td>
                    <asp:Label ID="lblTestToalAmount" runat="server" Visible="true" style="visibility:hidden"></asp:Label>
                </td>
            </tr>
            <tr id="trNegotiatedAmount" runat="server" visible="false">                
                <td id="tdNegotiatedAmt1" runat="server" style="background: Silver">
                    <label class="iceLable">
                        Negotiated Amount</label>
                </td>
                <td id="tdNegotiatedAmt2" runat="server" style="background: Silver">
                    <b>
                        <asp:Label ID="lblNegotiatedAmount" runat="server" Text="0" BorderWidth="0px" Style="width: 180px;
                            text-align: right; border: 1px" ForeColor="Black"></asp:Label></b>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <br />
                    <asp:Panel ID="pnlSummery" Visible="false" runat="server">
                        <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgSummery" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlPaymentVerification" runat="server">
        <fieldset class="icePnlinner">
            <legend>Mode of Payment</legend>
            <asp:Panel ID="pnlPaymentMode" runat="server">
                <table border="0" cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="grdPaymentDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
                                PageSize="10" Width="100%" OnRowCommand="grdPaymentDetails_RowCommand" OnPageIndexChanging="grdPaymentDetails_PageIndexChanging"
                                OnRowDataBound="grdPaymentDetails_RowDataBound" OnRowCreated="grdPaymentDetails_RowCreated"
                                ShowFooter="true">
                                <HeaderStyle CssClass="gridHeaderStyle" />
                                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                                <FooterStyle CssClass="gridFooterStyle" />
                                <RowStyle CssClass="gridRowStyle" />
                                <%--<EmptyDataTemplate>
                                No Records Found
                            </EmptyDataTemplate>--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="SI No.">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PayemntRequestId" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompPaymentId" runat="server" Text='<%#Eval("CompPaymentId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compensation Type" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompensationType" runat="server" Text='<%#Eval("CompensationType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode Of <br/> Payment" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrlModeOfPayment" runat="server" Text=""></asp:Literal>
                                            <asp:Label ID="lblModeOfPayment" Text='<%#Eval("ModeOfPayment") %>' runat="server"
                                                Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Kind <br/> Type" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrlInKindType" runat="server" Text=""></asp:Literal>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalLabel" runat="server" Text="Total Amount" Style="text-align: right;
                                                float: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                        FooterStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" Width="13%" />
                                        <FooterStyle HorizontalAlign="Right" Width="13%" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnTotalAmount" runat="server" />
                                            <asp:Label ID="lblCompensationAmount" Text='<%#Eval("CompensationAmount") %>' runat="server" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BankName" HeaderText="Bank" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="12%" />
                                    <asp:BoundField DataField="BranchName" HeaderText="Branch" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="BankReference" HeaderText="Cheque#" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="7%" />
                                    <asp:BoundField DataField="BankCode" HeaderText="Bank Code" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="FixedCostCentre" HeaderText="Fixed Cost Centre" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="BatchNos" HeaderText="Batch No's" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="Received by StakeHolder" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredToStakeHolder" Text='<%#Eval("DeliveredToStakeHolder") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Date" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredDate" Text='<%#Eval("DeliveredDate") %>' runat="server" />
                                            <asp:Literal ID="litFundReqStatus" Text='<%#Eval("FundReqStatus") %>' runat="server" Visible="false"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FundReqStatus" HeaderText="Fund Status" HeaderStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblPaymentStatusMessage" runat="server" Text="" CssClass="iceLable"
                Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 14px;
                font-weight: bold"></asp:Label>
            <br />
            <div align="center" class="CSSTableGenerator">
                <table border="0" cellspacing="0" cellpadding="0">
                </table>
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlCompensationStatus" runat="server">
        <fieldset class="icePnlinner">
            <legend>Compensation Status</legend>
            <%-- <div style="background-color: #e0e0e0; text-align: left; padding: 10px">--%>
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="100px">
                        <asp:Label class="iceLable" ID="label1" runat="server">
                            Compensation Status:
                        </asp:Label>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="ddlCompensationStatus" Width="120px" runat="server" AutoPostBack="true"
                            CssClass="iceDropDown" OnSelectedIndexChanged="ddlCompensationStatus_SelectedIndexChanged">
                            <asp:ListItem Value="NP">Not Paid</asp:ListItem>
                            <asp:ListItem Value="PP">Partially Paid</asp:ListItem>
                            <asp:ListItem Value="CP">Completely Paid</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlCompensationStatus"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    </td>
                    <td align="left" width="60%" rowspan="2">
                        <%--                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="vgCompStatus" />
                        --%>
                        <asp:CheckBox ID="chkOverrideGriv" runat="server" Text="Over ride Grievance" AutoPostBack="true" Visible="false" OnCheckedChanged="chkOverrideGriv_CheckedChanged" />&nbsp;
                        <asp:Button ID="btnCompStatusSave" runat="server" Text="Save" CssClass="icebutton"
                            ValidationGroup="vgCompStatus" OnClick="btnCompStatusSave_Click" />
                        <asp:Button ID="lnkValuationPCI" runat="server" Text="For File Closure Request" CssClass="icebutton"
                            Width="160" OnClick="lnkValuationPCI_Click" />
                        <asp:ValidationSummary ID="valSummary1" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Unfreeze" runat="server" />
                        <%--  <a id="lnkValuationPCI" runat="server" href="#" class="iceLinkButton" >File
                            Closure Request</a>--%>
                        <asp:Label ID="lblStatusValuationPCI" runat="server" Style="text-decoration: blink;font-family: Arial; font-size: 16px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" nowrap>
                        <span id="Span1" runat="server" style="display: none">                        
                            <asp:Label class="iceLable" ID="label2" runat="server" Text="File Closing comments:" />&nbsp; <span class="mandatory">*</span><br />
                            <asp:TextBox ID="TxtFileClosingcomments" Width="300px" runat="server" CssClass="iceTextAeralarge"
                                TextMode="MultiLine" />                            
                            <%--<asp:RequiredFieldValidator ID="rfvUnfreezeComments" ControlToValidate="TxtFileClosingcomments"
                                ErrorMessage="Enter File Closing comments" Display="None" ValidationGroup="Unfreeze"
                                runat="server"> </asp:RequiredFieldValidator>--%>
                        </span>
                    </td>
                </tr>
                <%--<tr>
                    <td align="center" colspan="2">
                      <br />
                        
                    </td>
                </tr>--%>
            </table>
            <%-- </div>--%>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlNewLocation" runat="server">
        <fieldset class="icePnlinner">
            <legend>New Location</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="right" style="width: 100%">
                        <a id="lnkUploadDoc" href="#" runat="server"><b>View/Upload Conversation Log</b></a>
                    </td>
                </tr>
            </table>
            <table cellpadding="3" style="margin-top: 15px" width="100%">
                <tr>
                    <td align="left" width="12%">
                        <div>
                            <label class="iceLable">
                                New Plot No.</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtNewPlotNumber" runat="server" class="iceTextBox" MaxLength="50"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeNewPlotNumber" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                            ValidChars=". " TargetControlID="txtNewPlotNumber" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfvNewPlotNumber" runat="server" ControlToValidate="txtNewPlotNumber"
                            ErrorMessage="New Plot No is Required" Display="None" ValidationGroup="vgNewLocation">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Distance from Old Plot (KM)</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDistanceFromOldPlot" runat="server" class="iceTextBox" MaxLength="8"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                            ValidChars="." TargetControlID="txtDistanceFromOldPlot" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfvDistanceFromOldPlot" runat="server" ControlToValidate="txtDistanceFromOldPlot"
                            ErrorMessage="Distance From Old Plot is Required" Display="None" ValidationGroup="vgNewLocation">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                District</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:DropDownList ID="ddlDistrict" runat="server" Width="201px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select-- </asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlDistrict"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" Width="201px" ControlToValidate="ddlDistrict"
                            ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a District"
                            Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                County</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCounty" runat="server" Width="201px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select-- </asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlCounty"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvCounty" runat="server" ControlToValidate="ddlCounty"
                                    ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a County"
                                    Display="None"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Sub County</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSubCounty" runat="server" Width="201px" AutoPostBack="True"
                                    CssClass="iceDropDown" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlSubCounty"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvSubCounty" runat="server" ControlToValidate="ddlSubCounty"
                                    ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Sub County"
                                    Display="None"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Parish</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlParish" runat="server" Width="201px" AutoPostBack="false"
                                    CssClass="iceDropDown">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlParish"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvParish" runat="server" ControlToValidate="ddlParish"
                                    ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Parish"
                                    Display="None"></asp:RequiredFieldValidator>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>              
                        
                </tr>             
                <tr>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Village</label>
                            <span class="mandatory">*</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlVillage" runat="server" Width="201px" AutoPostBack="false"
                                    CssClass="iceDropDown">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender8" runat="server" TargetControlID="ddlVillage"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvVillage" runat="server" ControlToValidate="ddlVillage"
                                    ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Village"
                                    Display="None"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                         <td>
                        <div>
                            <label class="iceLable">
                                Settlement Date</label></div>
                    </td>
                         <td>
                        <asp:TextBox ID="dpcSettlementDate1" runat="server" onKeyDown="doCheck()"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caldpcSettlementDate" CssClass="WISCalendarStyle"
                            runat="server" TargetControlID="dpcSettlementDate1">
                        </ajaxToolkit:CalendarExtender>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpcSettlementDate1"
                            ClientValidationFunction="CheckDOB" ErrorMessage="Settlement Date should not be greater than Today's Date"
                            ValidationGroup="vgNewLocation" Display="None">
                        </asp:CustomValidator>
                         <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="dpcSettlementDate1"
                           ClientValidationFunction="CheckConstrSatrtDate" ErrorMessage="Date of Settlement cannot be before Project Start Date."
                           ValidationGroup="vgNewLocation" Display="None"></asp:CustomValidator>
                     <asp:HiddenField ID="hfProjStartDate" runat="server" Value="0" />
                    <asp:HiddenField ID="hfProjEndDate" runat="server" Value="0" />
                        <%--<asp:RequiredFieldValidator ID="rfvSettlementDate" runat="server" ValidationGroup="vgNewLocation"
                            Display="None" ControlToValidate="dpcSettlementDate" ErrorMessage="Select Date"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <br />
                        <asp:ValidationSummary ID="VSNewLocation" runat="server" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="vgNewLocation" />
                        <asp:Button ID="btnNewLocationSave" runat="server" Text="Save" CssClass="icebutton"
                            OnClick="btnNewLocationSave_Click" ValidationGroup="vgNewLocation" />
                        <asp:Button ID="btnNewLocationClear" runat="server" Text="Clear" CssClass="icebutton"
                            OnClick="btnNewLocationClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <%--</fieldset>--%>
    <script language="javascript" type="text/javascript">

        function DeleteRecord() {
            return confirm('Are you sure you want to make this Delete?');
        }
//        <script type="text/javascript">
       

        function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode, DocumentID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 650) / 4;
            open('../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode + '&DOCSERVICEID=' + DocumentID, 'UploadDocPop', 'width=800px,height=700px,top=' + top + ', left=' + left);
        }
        function CalculateTotalAmount() {
            var FacilitationAmount;
            var TotalAmount;
            FacilitationAmount = document.getElementById('<%=txtFacilitation.ClientID %>').value;
            if (document.getElementById('<%=txtFacilitation.ClientID %>').value == "")
                FacilitationAmount = 0;

            TotalAmount = document.getElementById('<%=lblTotalAmount.ClientID %>').value;
            if (document.getElementById('<%=lblTotalAmount.ClientID %>').value == "")
                TotalAmount = 0;
            var ASum = parseFloat(FacilitationAmount) + parseFloat(TotalAmount)
            document.getElementById('<%=lblTotalAmount.ClientID %>').value = ASum;
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            //alert('ChangeRequestCode:' + ChangeRequestCode + ';  ProjectID:' + ProjectID + ';  userID:' + userID + ';  HHID:' + HHID + ';  pageCode:' + pageCode);
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            var txtComments = document.getElementById('<%=TxtFileClosingcomments.ClientID %>').value.replace(/^\s+|\s+$/g, '');
            if (txtComments.toString() == '') {
                alert('Please enter comments.');
            }
            else {
                open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
            }
        }

        function CheckDecimal(e, src) {
            if (e.keyCode == 46) { // Invoke when press Enter Key
                var char = src.value;
                if (char.indexOf(".") == -1) {
                    return true;
                }
                else if (char.indexOf(".") > -1) {
                    return false;
                }
                return true;
            }
            return true;
        }
        function FieldChecked(iControl, PaymentFor, ctrlIdNo) { }
    </script>
    <script type = "text/javascript">
        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }
        function CheckDOB(oSrc, args) {
            var now = new Date();
            dtMeeting = GetCalDate('<%=dpcSettlementDate1.ClientID%>');

            var CurrentMonth = (now.getMonth() + 1);
            var CurrentDate = now.getDate();
            var CurrentYear = now.getFullYear();

            if (CurrentMonth.length < 2) CurrentMonth = '0' + CurrentMonth;
            if (CurrentDate.length < 2) CurrentDate = '0' + CurrentDate;

            var ArrMeetingDt = dtMeeting.split("-");
            var MeetingDt = ArrMeetingDt[0];
            var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
            var MeetingYear = ArrMeetingDt[2];

            if (CurrentYear < MeetingYear) {
                args.IsValid = false;
                return;
            }
            else if ((CurrentYear == MeetingYear) && (CurrentMonth < MeetingMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((CurrentYear == MeetingYear) && (CurrentMonth == MeetingMonth) && (CurrentDate <
MeetingDt)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
        function CheckConstrSatrtDate(oSrc, args) {
            dtProjectStart = document.getElementById('<%=hfProjStartDate.ClientID %>').value;
            dtProjectEnd = document.getElementById('<%=hfProjEndDate.ClientID %>').value;
            dtConstrStart = GetCalDate('<%=dpcSettlementDate1.ClientID%>');

            var ArrProjSt = dtProjectStart.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrProjEnd = dtProjectEnd.split("-");
            var ProjEndDate = ArrProjEnd[0];
            var ProjEndMonth = GetMonthNumber(ArrProjEnd[1]);
            var ProjEndYear = ArrProjEnd[2];

            var ArrConstrSt = dtConstrStart.split("-");
            var ConstrStartDate = ArrConstrSt[0];
            var ConstrStartMonth = GetMonthNumber(ArrConstrSt[1]);
            var ConstrStartYear = ArrConstrSt[2];

            if (ProjStartYear > ConstrStartYear) {
                args.IsValid = false;
                return;
            }
            else if (ProjEndYear < ConstrStartYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth > ConstrStartMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == ConstrStartYear) && (ProjStartMonth == ConstrStartMonth) && (ProjStartDate > ConstrStartDate)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth < ConstrStartMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjEndYear == ConstrStartYear) && (ProjEndMonth == ConstrStartMonth) && (ProjEndDate < ConstrStartDate)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    </script>
</asp:Content>
