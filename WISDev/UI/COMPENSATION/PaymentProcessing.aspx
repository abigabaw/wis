<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PaymentProcessing.aspx.cs" UICulture="en" Culture="en-US" Inherits="WIS.PaymentProcessing" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Src="HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/page_specific.css" />
    <style type="text/css">
        .ListSearchExtenderPrompt {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }

        .auto-style1 {
            height: 124px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tkManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <%--<fieldset class="icePnlinner">--%>
    <%--   <legend>Closing Information</legend>--%>
    <fieldset class="icePnlinner">
        <legend>Summary</legend>
        <table border="0" cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td style="width: 5%"></td>
                <td style="width: 25%">&nbsp;
                </td>
                <td style="width: 25%">
                    <label class="iceLable">
                        Cash Compensation (USH)</label>
                </td>
                <td style="width: 20%">
                    <label class="iceLable">
                        Paid Amount (USH)</label>
                </td>
                <td style="width: 30%">
                    <label class="iceLable">
                        Payment Amount (USH)</label>
                </td>
                <td align="left" style="width: 190px">
                    <label class="iceLable">
                        In-Kind Compensation
                    </label>
                </td>
            </tr>
            <tr>
                <td style="width: 5%">
                    <asp:CheckBox ID="chkLandValuation" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkLandValuation_CheckedChanged" />
                    <%-- onclick="FieldChecked(this,'Land Valuation',1);"--%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Land</label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtCashLand" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblLandPaid" runat="server" Text="0" ClientIDMode="Static" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtLandPending" runat="server" ClientIDMode="Static" class="iceTextBox"
                        Style="width: 140px; text-align: right;" AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        TargetControlID="txtLandPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInKindLand" runat="server" onkeypress="return CheckDecimal(event, this)"
                        class="iceTextBox" Style="width: 155px" MaxLength="12" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        TargetControlID="txtInKindLand" ValidChars="." FilterType="Numbers,Custom">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    Acres
                </td>
            </tr>
            <tr class="gridAlternateRow">
                <td>
                    <asp:CheckBox ID="chkReplacementHouseValue" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkReplacementHouseValue_CheckedChanged" />
                    <%-- onclick="FieldChecked(this,'Replacement House Value',4);" --%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Residential Structures</label>
                </td>
                <td>
                    <asp:TextBox ID="txtResStructure" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblResidentialPaid" ClientIDMode="Static" runat="server" Text="0"
                        BorderWidth="0px" Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtResidentialPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        TargetControlID="txtResidentialPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlResidentialStructure" runat="server" Enabled="false" AutoPostBack="false">
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
                <td>
                    <asp:CheckBox ID="chkFixtureValuation" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkFixtureValuation_CheckedChanged" />
                    <%--onclick="FieldChecked(this,'Fixtures Valuation',2);" --%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Fixtures</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFixture" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblFixturesPaid" ClientIDMode="Static" runat="server" Text="0" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtFixturesPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        TargetControlID="txtFixturesPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left" style="width: 230px">NA
                </td>
            </tr>
            <tr class="gridAlternateRow">
                <td>
                    <asp:CheckBox ID="chkCropsValuation" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkCropsValuation_CheckedChanged" />
                    <%--onclick="FieldChecked(this,'Crops Valuation',3);" --%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Crops</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCrops" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblCropsPaid" ClientIDMode="Static" runat="server" Text="0" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtCropsPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                        TargetControlID="txtCropsPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">NA
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCulturePropertyValue" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkCulturePropertyValue_CheckedChanged" />
                    <%-- onclick="FieldChecked(this,'Compensation for Culture property',6);" --%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Compensation for Culture property</label>
                </td>
                <td>
                    <asp:TextBox ID="txtCulutralProperty" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblCulturePaid" ClientIDMode="Static" runat="server" Text="0" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtCulturePending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                        TargetControlID="txtCulturePending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">NA
                </td>
            </tr>
            <tr class="gridAlternateRow">
                <td>
                    <asp:CheckBox ID="chkFacilitation" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkFacilitation_CheckedChanged" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Facilitation/GOU allowance/Others</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFacilitation" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtFacilitation" runat="server" TargetControlID="txtFacilitation"
                        FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td>
                    <asp:Label ID="lblFacilitationPaid" ClientIDMode="Static" runat="server" Text="0"
                        BorderWidth="0px" Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtFacilitationPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                        TargetControlID="txtFacilitationPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">NA
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkDamagedCropValue" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkDamagedCropValue_CheckedChanged" />
                    <%-- onclick="FieldChecked(this,'Damaged Crop Value',5);" --%>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Damaged Crops</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDamaged" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;" />
                </td>
                <td>
                    <asp:Label ID="lblDamagedPaid" ClientIDMode="Static" runat="server" Text="0" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtDamagedPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                        TargetControlID="txtDamagedPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">NA
                </td>
            </tr>
            <tr class="gridAlternateRow">
                <td>
                    <asp:CheckBox ID="chkFinalCompensation" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkFinalCompensation_CheckedChanged" />
                    <%--onclick="FieldChecked(this,'Final Compensation',7);" --%>
                </td>
                <td>
                    <label class="iceLable">
                        Total Amount</label>
                </td>
                <td>
                    <%--colspan="3"--%>
                    <b>
                        <asp:Label ID="lblTotalAmount" runat="server" Text="0" BorderWidth="0px" Style="width: 140px; text-align: right; border: 1px"></asp:Label>
                    </b>
                </td>
                <td>
                    <asp:Label ID="lblTotalAmountPaid" ClientIDMode="Static" runat="server" Text="000"
                        BorderWidth="0px" Style="width: 100px; text-align: right; border: 1px; font-weight: bolder;"></asp:Label>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblTestToalAmount" runat="server" Visible="true" Style="visibility: hidden"></asp:Label>
                </td>
            </tr>
            <tr id="trNegotiatedAmount" runat="server" visible="false" style="background: #E4e4e4">
                <td>
                    <asp:CheckBox ID="chkNegotiatedAmount" Text="" runat="server" ValidationGroup="vgChk"
                        AutoPostBack="true" OnCheckedChanged="chkNegotiatedAmount_CheckedChanged" />
                    <%--onclick="FieldChecked(this,'Negotiated Amount',8);" --%>
                </td>
                <td id="tdNegotiatedAmt1" runat="server" style="background: #E4e4e4">
                    <label class="iceLable">
                        Negotiated Amount</label>
                </td>
                <td id="tdNegotiatedAmt2" runat="server" style="background: #E4e4e4">
                    <b>
                        <asp:Label ID="lblNegotiatedAmount" runat="server" Text="0" BorderWidth="0px" Style="width: 140px; text-align: right; border: 1px"
                            ForeColor="Black"></asp:Label></b>
                </td>
                <td>
                    <asp:Label ID="lblNegotiatedPaid" ClientIDMode="Static" runat="server" Text="0" BorderWidth="0px"
                        Style="width: 100px; text-align: right; border: 1px"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtLand" runat="server" class="iceTextBox" style="width: 140px"/>--%>
                    <asp:TextBox ID="txtNegotiatedPending" runat="server" class="iceTextBox" Style="width: 140px; text-align: right;"
                        AutoPostBack="true" OnTextChanged="TextChanged" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                        TargetControlID="txtDamagedPending" FilterType="Numbers,Custom" ValidChars=",">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td colspan="2">
                    <a id="lnkPaymentStatus" class="iceStatusLinks" href="#" onclick="OpenPaymentStatus()" runat="server" style="border-right: 1px solid; float: left; width: 18%;"><b>View Status</b></a>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr style="display: none">
                <td align="center" colspan="6">
                    <br />
                    (<b>Hint: </b>Click on Save Summery button to Save 'Facilitation/GOU allowance and
                    In-Kind Compensation Details' only. Otherwise Entire data will be loose.)
                    <br />
                    <asp:Panel ID="pnlSummery" runat="server">
                        <asp:Button ID="btnSummerySave" runat="server" Text="Save Summery" ValidationGroup="vgSummery"
                            CssClass="icebutton" OnClick="btnSummerySave_Click" Visible="false" />
                        <asp:Button ID="btnSummeryClear" runat="server" Text="Clear Summery" CssClass="icebutton"
                            OnClick="btnSummeryClear_Click" Visible="false" />
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
                <asp:Panel ID="pnlPaymentDetail" runat="server">
                    <table border="0" cellspacing="2" cellpadding="2" width="100%">
                        <tr>
                            <td align="left" style="width: 150px;">
                                <div>
                                    <label class="iceLable">
                                        Compensation Type</label><span class="mandatory">*</span>
                                </div>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCompensationType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompensationType_SelectedIndexChanged" Width="200px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                    <asp:ListItem Value="In Kind">In Kind</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCompensationType"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvCompensationType" runat="server" ControlToValidate="ddlCompensationType"
                                    ValidationGroup="vgModeOfPayment" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Compensation Type"
                                    Display="None"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" nowarp>
                                <div>
                                    <label class="iceLable">
                                        Mode of Payment</label><span class="mandatory">*</span>
                                </div>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" Width="200px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPaymentMode" runat="server" ControlToValidate="ddlPaymentMode"
                                    ValidationGroup="vgModeOfPayment" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Payment Mode"
                                    Display="None"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Amount</label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="iceTextBox" Style="text-align: right;" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                                    TargetControlID="txtPaymentAmount" ValidChars="," runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvPaymentAmount" runat="server" ControlToValidate="txtPaymentAmount"
                                    ValidationGroup="vgModeOfPayment" Text="Mandatory" ErrorMessage="Amount Requred"
                                    Display="None" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 140px;">
                                <label class="iceLable">
                                    Bank</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBank" CssClass="iceTextBox" AppendDataBoundItems="true"
                                    AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" Width="200px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <label class="iceLable">
                                    Branch</label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="uplBranch" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlBranch" CssClass="iceTextBox" AppendDataBoundItems="true"
                                            runat="server" Width="200px">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlBank" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblBankReference" CssClass="iceLable" Text="Account No"
                                    runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBankReference" runat="server" CssClass="iceTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                    ValidChars=" -_" TargetControlID="txtBankReference" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="left" style="width: 140px;">
                                <label class="iceLable">
                                    Fixed Cost Centre
                                </label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFixedCostCentre" CssClass="iceTextBox" AppendDataBoundItems="true"
                                    runat="server" Width="200px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="ChequePaymentRow" runat="server" style="display: none;">
                            <td colspan="6" class="auto-style1">
                                <table cellspacing="0" cellpadding="3">

                                    <%--<tr>
                                        
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" nowarp>
                                <label class="iceLable">
                                    Received by Stakeholder</label>
                            </td>
                            <td align="left" class="iceLable">
                                <asp:RadioButtonList ID="rblDeliveredToStakeholder" runat="server" AutoPostBack="true"
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDeliveredToStakeholder_SelectedIndexChanged" Enabled="False">
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    <asp:ListItem Value="No">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" class="iceLable" nowarp>
                                <label class="iceLable">
                                    Received Date</label>
                            </td>
                            <td align="left" class="iceLable" colspan="2">
                                <asp:TextBox ID="dpcDeliveredDate" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caldpcDeliveredDate" CssClass="WISCalendarStyle"
                                    runat="server" TargetControlID="dpcDeliveredDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <br />
                                <asp:Button ID="btnPaymentSave" runat="server" Text="Save" ValidationGroup="vgModeOfPayment"
                                    CssClass="icebutton" OnClick="btnPaymentSave_Click" />
                                <asp:Button ID="btnPaymentClear" runat="server" Text="Clear" CssClass="icebutton"
                                    OnClick="btnPaymentClear_Click" />
                                <asp:ValidationSummary ID="vsPaymentMode" runat="server" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="vgModeOfPayment" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
                                            <asp:Label ID="lblTotalLabel" runat="server" Text="Total Amount" Style="text-align: right; float: right"></asp:Label>
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
                                    <asp:BoundField DataField="BankReference" HeaderText="Account No" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="7%" />
                                    <asp:BoundField DataField="BankCode" HeaderText="Bank Code" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="FixedCostCentre" HeaderText="Cost Centre" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="BatchNos" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ControlStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="Received By Pap" HeaderStyle-HorizontalAlign="Center">
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
                                            <asp:Literal ID="litFundReqStatus" Text='<%#Eval("FundReqStatus") %>' runat="server"
                                                Visible="false"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FundReqStatus" HeaderText="Fund Status" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                                CommandName="EditRow" CommandArgument='<%#Eval("CompPaymentId") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                CommandName="DeleteRow" CommandArgument='<%#Eval("CompPaymentId") %>' OnClientClick="return DeleteRecord();"
                                                runat="server" />
                                            <asp:Literal ID="litCompPaymentId" Text='<%#Eval("CompPaymentId") %>' Visible="false"
                                                runat="server"></asp:Literal>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: auto;">
                                <tr>
                                    <td align="center">
                                        <%--<a id="lnkPaymentVerification" runat="server" href="#" class="iceLinkButton" style="text-decoration: none;
                                        color: White; font-family: Arial; font-size: 12px; width: auto; font-weight: normal;
                                        padding-top: 3px; height: 17px; margin-top: -0.5px; vertical-align: middle;">Payment
                                        Verification</a>--%>
                                        <asp:Button ID="lnkPaymentVerification" runat="server" CssClass="icebutton" Text="For Payment Verification"
                                            Width="160px" OnClick="btnApproval_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblPaytVerification" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 16px; font-weight: bold" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <br />
            <div align="center" class="CSSTableGenerator">
                <table border="0" cellspacing="0" cellpadding="0">
                </table>
            </div>
            <ul id="itemStatuses" runat="server" style="color:red;">
                <li id="itemCompPackageStatus" runat="server" style="display:none;"><asp:Label ID="lblCompPackageStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                <li id="itemDisclosureStatus" runat="server" style="display:none;"><asp:Label ID="lblDisclosureStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                <li id="itemGrievanceStatus" runat="server" style="display:none;"><asp:Label ID="lblGrievanceStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                <li id="itemPaymentRequestStatus" runat="server" style="display:none;"><asp:Label ID="lblPaymentStatusMessage" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
            </ul>
        </fieldset>
    </asp:Panel>
    <%--</fieldset>--%>
    <script language="javascript" type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=dpcDeliveredDate.ClientID%>'));

        function DeleteRecord() {
            return confirm('Are you sure you want to make this Delete?');
        }

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

        function CheckAmount(src, txt, lbl) {
            var actuval = document.getElementById(txt).value;
            if (actuval == undefined) {
                actuval = document.getElementById(txt).innerHTML;
            }
            var Paid = document.getElementById(lbl).innerHTML;
            if (Paid == undefined) {
                Paid = 0;
            }
            if (src.value == undefined) {
                alert('Enter Payment amount.');
                src.focus = true;
                return false;
            }
            var Paymaentamo = parseFloat(RemoveComma(src.value));
            var Balance = parseFloat(RemoveComma(actuval)) - parseFloat(RemoveComma(Paid));
            if (!isNaN(Paymaentamo) && !isNaN(Balance)) {
                if (Paymaentamo == 0) {
                    alert('Payment amount cannot be Zero.');
                    src.value = AddComma(Balance);
                    return false;
                }
                else if (Paymaentamo <= Balance) {
                    return true;
                }
                else {
                    alert('Payment amount cannot be greater than the Acutual amount.');
                    src.value = AddComma(Balance);
                    return false;
                }
            }
            else {
                alert('Payment amount cannot be greater than Acutual amount.');
                src.value = AddComma(Balance);
                return false;
            }
        }

        function CheckAmountaa(e, src) {
            var actuval = document.getElementById('<%=txtCashLand.ClientID %>').value;
            var Paid = document.getElementById('<%=lblLandPaid.ClientID %>').value;
            if (Paid == undefined) {
                Paid = 0;
            }
            if (src.value == undefined) {
                alert('Enter Payment amount.');
                src.focus = true;
                return false;
            }
            var Paymaentamo = parseFloat(RemoveComma(src.value));
            var Balance = parseFloat(RemoveComma(actuval)) - parseFloat(RemoveComma(Paid));
            if (!isNaN(Paymaentamo) && !isNaN(Balance)) {
                if (Paymaentamo == 0) {
                    alert('Payment amount cannot be Zero.');
                    src.value = AddComma(Balance);
                    return false;
                }
                else if (Paymaentamo <= Balance) {
                    return true;
                }
                else {
                    alert('Payment amount cannot be greater than the Acutual amount.');
                    src.value = AddComma(Balance);
                    return false;
                }
            }
            else {
                alert('Payment amount cannot be greater than Acutual amount.');
                src.value = AddComma(Balance);
                return false;
            }
        }

        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }

        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        function RemoveCommaAAA(iValue) {
            return iValue.toString().replace(/,?/g, "");
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            //alert('ChangeRequestCode:' + ChangeRequestCode + ';  ProjectID:' + ProjectID + ';  userID:' + userID + ';  HHID:' + HHID + ';  pageCode:' + pageCode);
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
        function HideCheckBox() {
            document.getElementById('<%=chkFacilitation.ClientID %>').style.display = 'none';
            var amount;
            var val = RemoveCommaAAA(document.getElementById('<%=txtFacilitation.ClientID %>').value);
            if (val == "") {
                val = 0;
            }
            if (!isNaN(val))
                amount = val;
            else
                amount = '0';
            document.getElementById('<%=txtFacilitation.ClientID %>').value = AddComma(amount);
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

        function OpenPaymentStatus() {
            var HHID = "<%=Session["HH_ID"]%>";
            var ProjectID = "<%=Session["PROJECT_ID"]%>"

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;
            open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYVR&ProjectID=' + ProjectID + '&HHID=' + HHID, 'Package Status', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
