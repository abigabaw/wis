<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="CompensationFinancial.aspx.cs" Inherits="WIS.CompensationFinancial" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Src="HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display: none;
            background-color: transparent;
            visibility: hidden;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server" EnablePageMethods="true">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <fieldset class="icePnlinner">
        <asp:HiddenField ID="hdnRoundOffValue" runat="server" />
        <table border="0" width="100%" cellpadding="1" cellspacing="0">
            <tr>
                <%-- <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="true" />--%>
                <td style="vertical-align: top">
                    <fieldset class="icePnlinner">
                        <legend>Land</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <label id="lblCashCompensation" class="iceLable">
                                        Cash Compensation (USH)</label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblLandValuation" class="iceLable">
                                        Land Valuation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLandValuation" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" onblur="CalcLandTotal();" />
                                    <%--<asp:RegularExpressionValidator ID="revLandValuation" ControlToValidate="txtLandValuation"
 runat="server" ValidationExpression="^\d{1,6}(\.\d{1,2})?$" ErrorMessage="Land Valuation Must Be Numeric" Display="None"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblLandDAinPercentage" class="iceLable">
                                        Land Disturbance Allowance (%)</label>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:TextBox ID="txtLandDAinPercentage" runat="server" Text="" class="iceTextBox"
                                            MaxLength="2" onkeypress="javascript:return isNumber (event);" Style="width: 50px;
                                            text-align: right;" onblur="CalcLandTotal();" />
                                        %</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblLandDAinAmount" class="iceLable">
                                        Land Disturbance Allowance (amount)</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLandDAinAmount" runat="server" Text="" class="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblLandTotal" class="iceLable">
                                        Land Total</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLandTotal" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblAcreageDifferencePayment" class="iceLable">
                                        Acreage Difference Payment</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAcreageDifferencePayment" runat="server" Text="" class="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <label id="lblLandComments" class="iceLable">
                                        Comments</label>
                                    <br />
                                    <asp:TextBox ID="txtLandComments" runat="server" Text="" TextMode="MultiLine" class="iceTextBox"
                                        Style="width: 100%;" Rows="3" onkeydown="return checkMaxLength(this)" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </fieldset>
                </td>
                <%-- <td style="width: 5px">
                </td>--%>
                <td style="vertical-align: top">
                    <fieldset class="icePnlinner">
                        <legend>Residential Structure</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="left">
                                    <label id="lblRSDepreciatedValue" class="iceLable">
                                        Depreciated value</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRSDepreciatedValue" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);"
                                        onblur='CalcResidenitalTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblRSReplacementValue" class="iceLable">
                                        Replacement Value</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRSReplacementValue" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);"
                                        onblur='CalcResidenitalTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblReplacementUplift" class="iceLable">
                                        Replacement Uplift</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtReplacementUplift" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;"
                                        onblur='CalcResidenitalTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblRSDAinPercentage" class="iceLable">
                                        Residential Disturbance Allowance (%)</label>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:TextBox ID="txtRSDAinPercentage" runat="server" Text="" MaxLength="2" CssClass="iceTextBox"
                                            onkeypress="javascript:return isNumber (event);" Style="width: 50px; text-align: right;"
                                            onblur='CalcResidenitalTotal();' />
                                        %</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblResidentialDAinAmount" class="iceLable">
                                        Residential Disturbance Allowance (amount)</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtResidentialDAinAmount" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblRSMovingAllowance" class="iceLable">
                                        Moving Allowance</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRSMovingAllowance" runat="server" Text="" CssClass="iceTextBox"
                                        onblur='CalcResidenitalTotal();' Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblRSLabourCost" class="iceLable">
                                        Labour Cost</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRSLabourCost" runat="server" Text="" onblur='CalcResidenitalTotal();'
                                        CssClass="iceTextBox" Style="width: 100px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: top">
                                    <label id="lblRSPaymentHighHouseValue" class="iceLable">
                                        Payment (High House Value)</label>
                                </td>
                                <td align="left" style="vertical-align: top">
                                    <asp:TextBox ID="txtRSPaymentHighHouseValue" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="vertical-align: top">
                                    <label id="lblResidentialStructureComments" class="iceLable">
                                        Comments</label>
                                    <br />
                                    <asp:TextBox ID="txtResidentialStructureComments" runat="server" Text="" TextMode="MultiLine"
                                        CssClass="iceTextBox" Style="width: 100%;" Rows="3" onkeydown="return checkMaxLength(this)"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <%--</table>
        <table border="1" width="100%">--%>
            <tr>
                <td style="vertical-align: top; width: 50%">
                    <fieldset class="icePnlinner">
                        <legend>Fixtures</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 65%">
                                    <label id="lblFixturesValuation" class="iceLable">
                                        Fixtures Valuation</label>
                                </td>
                                <td align="left" style="width: 35%">
                                    <asp:TextBox ID="txtFixturesValuation" runat="server" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onblur='CalcFixtureTotal();'></asp:TextBox><%--onkeypress="javascript:return isNumber (event);"--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblFixturesDAinPercentage" class="iceLable">
                                        Fixtures Disturbance Allowance (%)</label>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:TextBox ID="txtFixturesDAinPercentage" runat="server" Text="" CssClass="iceTextBox"
                                            MaxLength="2" onkeypress="javascript:return isNumber (event);" Style="width: 50px;
                                            text-align: right;" onblur='CalcFixtureTotal();' />
                                        %</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblFixturesDAinAmount" class="iceLable">
                                        Fixtures Disturbance Allowance (amount)</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFixturesDAinAmount" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="vertical-align: top">
                                    <label id="lblFixturesComments" class="iceLable">
                                        Comments</label>
                                    <br />
                                    <asp:TextBox ID="txtFixturesComments" runat="server" Text="" TextMode="MultiLine"
                                        CssClass="iceTextBox" Style="width: 100%;" Rows="3" onkeydown="return checkMaxLength(this)" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </fieldset>
                </td>
                <%-- <td style="width: 5px">
                </td>--%>
                <td style="vertical-align: top">
                    <fieldset class="icePnlinner">
                        <legend>Crops</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 65%">
                                    <label id="lblCropsValuation" class="iceLable">
                                        Crops Valuation</label>
                                </td>
                                <td align="left" style="width: 35%">
                                    <asp:TextBox ID="txtCropsValuation" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);"
                                        onblur='CalcCropTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblMaxCapCase" class="iceLable">
                                        Max Cap Case</label>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:CheckBox ID="chkMaxCapCase" runat="server" AutoPostBack="false" />
                                        <%--onclick="FieldChecked(this);"--%>
                                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                                    <%-- <input type="checkbox" class="iceTextBox" checked="checked" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblValuationAfterMaxCap" class="iceLable">
                                        Valuation after Max Cap</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtValuationAfterMaxCap" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;"
                                        onblur='CalcCropTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblCropsDAinPercentage" class="iceLable">
                                        Crops Disturbance Allowance (%)</label>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:TextBox ID="txtCropsDAinPercentage" runat="server" Text="" CssClass="iceTextBox"
                                            MaxLength="2" onkeypress="javascript:return isNumber (event);" Style="width: 50px;
                                            text-align: right;" onblur='CalcCropTotal();' />
                                        %</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblCropsDAinAmount" class="iceLable">
                                        Crops Disturbance Allowance (amount)</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCropsDAinAmount" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="vertical-align: top">
                                    <label id="lblCropsComments" class="iceLable">
                                        Comments</label>
                                    <br />
                                    <asp:TextBox ID="txtCropsComments" runat="server" Text="" TextMode="MultiLine" CssClass="iceTextBox"
                                        Style="width: 100%" Rows="3" onkeydown="return checkMaxLength(this)" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <%--  </table>
        <table border="1" width="100%">--%>
            <tr>
                <td style="vertical-align: top; width: 50%">
                    <fieldset class="icePnlinner">
                        <legend>Others</legend>
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%">
                                    <label id="lblCompensationForCultureProperty" class="iceLable">
                                        Compensation for Culture property</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCompensationForCultureProperty" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;"
                                        onblur='CalcOtherTotal();' />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblOtherDamagedCrops" class="iceLable">
                                        Damaged Crops</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOtherDamagedCrops" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);"
                                        onblur='CalcOtherTotal();' />
                                </td>
                            </tr>
                            <tr style="background-color: #eeeeee">
                                <td align="left" width="10%">
                                    <label id="lblOthersTotal" class="iceLable">
                                        Total</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOthersTotal" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <%--  <div align="left">
                        <i>DA - Disturbance Allowance</i></div>--%>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <%--<td style="width: 5px">
                </td>--%>
                <td style="vertical-align: top;" colspan="2">
                    <fieldset class="icePnlinner">
                        <legend>Summary</legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    &nbsp;
                                </td>
                                <td style="width: 25%">
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
                                    <label id="lblSummaryLandValues" class="iceLable">
                                        Land Values</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSummaryLandValues" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td nowrap>
                                    <asp:TextBox ID="txtInKindLand" runat="server" onkeypress="javascript:return CheckDecimal (event, this);"
                                        class="iceTextBox" Style="width: 180px; text-align: right;" MaxLength="12" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txtInKindLand" ValidChars="." FilterType="Numbers,Custom">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    Acres
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblHouseValues" class="iceLable">
                                        House Values</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHouseValues" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblReplacementHouseValue" class="iceLable">
                                        Replacement House Value</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReplacementHouseValue" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlResidentialStructure" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="UETCL Resettlement House">UETCL Resettlement House</asp:ListItem>
                                        <asp:ListItem Value="UETCL Building Materials+Labour">UETCL Building Materials+Labour</asp:ListItem>
                                        <asp:ListItem Value="NA">NA</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlResidentialStructure"
                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                        IsSorted="true" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblSummeryFixturesValue" class="iceLable">
                                        Fixtures Value</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSummeryFixturesValue" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblCropsValue" class="iceLable">
                                        Crops Value</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCropsValue" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblDamagedCropsValue" class="iceLable">
                                        Damaged Crops Value</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDamagedCropsValue" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblCulturePropertyValue" class="iceLable">
                                        Culture Property Value</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCulturePropertyValue" runat="server" Text="" CssClass="iceTextBox"
                                        onkeypress="javascript:return isNumber (event);" Style="width: 180px; text-align: right;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="Label1" class="iceLable">
                                        Facilitation allowances/Others</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFacilitationallowances" runat="server" Text="" CssClass="iceTextBox" onblur='FindTotalAmount();'
                                        MaxLength="20" Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="Label2" class="iceLable">
                                        Total Amount</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Text="" CssClass="iceTextBox" Style="width: 180px;
                                        text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                    <%--<asp:Label ID="lblTotalAmount" runat="server" Text="0" class="iceLable"></asp:Label>--%>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <label id="lblNegotiatedAmount" class="iceLable">
                                        Negotiated Amount</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNegotiatedAmount" runat="server" Text="" CssClass="iceTextBox"
                                        Style="width: 180px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <fieldset class="icePnlinner">
            <legend>Package Disclosed Info</legend>
            <table border="0" cellpadding="2" width="100%" style="margin-top: 5px">
                <tr>
                    <td align="left" width="10%">
                        <label id="lblDeliveryDate" class="iceLable">
                            Disclosed Date</label>
                    </td>
                    <td align="left" width="20%">
                        <asp:TextBox ID="dpDeliveryDate" runat="server" Width="130px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calDeliveryDate" runat="server" CssClass="WISCalendarStyle"
                            TargetControlID="dpDeliveryDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="left" width="10%">
                        <label id="lblDeliveredBy" class="iceLable">
                            Disclosed By</label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlDeliveredBy" runat="server" CssClass="iceDropDown">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlDeliveredBy"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top">
                        <label class="iceLable">
                            PAP Action</label>
                    </td>
                    <td align="left" style="vertical-align: top">
                        <asp:RadioButton ID="rdoActionAccepted" GroupName="PAPAction" Text="Accepted" CssClass="labelSuffix"
                            runat="server" />
                        <asp:RadioButton ID="rdoActionRejected" GroupName="PAPAction" Text="Rejected" CssClass="labelSuffix"
                            runat="server" />
                    </td>
                    <td align="left" style="vertical-align: top">
                        <label id="lblPackageDeliveryInfoComments" class="iceLable">
                            Comments</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPackageDeliveryInfoComments" runat="server" Text="" TextMode="MultiLine"
                            class="iceTextBox" Rows="3" Style="width: 80%" onkeydown="return checkMaxLength(this)" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <table width="100%" align="center">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:HiddenField ID="hdnDoCalc" Value="0" runat="server" />
    <script type="text/javascript">
        document.body.onload = function () {
            SiteMasterBody_OnLoad();    // Master Page function

            PreventDateFieldEntry(document.getElementById('<%=dpDeliveryDate.ClientID%>'));

            if (document.getElementById('<%=hdnDoCalc.ClientID %>').value == 1) {
                CalcLandTotal();
                CalcResidenitalTotal();
                CalcFixtureTotal();
                CalcCropTotal();
                CalcOtherTotal();
            }
            FindTotalAmount();
        }

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode

            if (iKeyCode != 44 && iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
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

        function CalcLandTotal() {
            // get id of land valuation and land total
            CalculateMehtod('LND');
        }

        function CalcResidenitalTotal() {
            // get id of land valuation and land total
            CalculateMehtod('RES');
        }
        function CalcFixtureTotal() {
            // get id of land valuation and land total
            CalculateMehtod('FIX');
        }
        function CalcCropTotal() {
            // get id of land valuation and land total
            CalculateMehtod('CRP');
        }

        function CalcOtherTotal() {
            // get id of land valuation and land total
            CalculateMehtod('OTHR');
        }

        function CalculateMehtod(fldCode) {

            switch (fldCode) {
                case 'LND': //LAND
                    var LandValue = RemoveComma(document.getElementById('<%=txtLandValuation.ClientID %>').value);
                    //  alert('Land Value = ' + LandValue)
                    //  alert('After Remove Comma from Land Value = ' + RemoveComma(LandValue))
                    var Percent = RemoveComma(document.getElementById('<%=txtLandDAinPercentage.ClientID %>').value);
                    if (LandValue.toString() == "NaN" || LandValue == "")
                        LandValue = 0;
                    if (Percent.toString() == "NaN" || Percent == "")
                        Percent = 0;

                    var DAAmount = (LandValue * Percent) / 100;
                    var LandTotal = parseFloat(DAAmount) + parseFloat(LandValue);

                    document.getElementById('<%=txtLandDAinAmount.ClientID %>').value = AddComma(Math.round(DAAmount));
                    document.getElementById('<%=txtLandTotal.ClientID %>').value = AddComma(Math.round(LandTotal));
                    document.getElementById('<%=txtSummaryLandValues.ClientID %>').value = AddComma(Math.round(LandTotal)); //For Summery
                    break;

                case 'RES': //RESIDENTIAL STRUCTURE
                    var DeprecitedValue = RemoveComma(document.getElementById('<%=txtRSDepreciatedValue.ClientID %>').value);
                    var ReplacementValue = RemoveComma(document.getElementById('<%=txtRSReplacementValue.ClientID %>').value);
                    var Percent = RemoveComma(document.getElementById('<%=txtRSDAinPercentage.ClientID %>').value);
                    var PmntHighHouseVal = RemoveComma(document.getElementById('<%=txtRSPaymentHighHouseValue.ClientID %>').value);
                    var ResidentialDAinAmountVal = RemoveComma(document.getElementById('<%=txtResidentialDAinAmount.ClientID %>').value);

                    var MovingAllowanceVal = RemoveComma(document.getElementById('<%=txtRSMovingAllowance.ClientID %>').value);
                    var LabourCostVal = RemoveComma(document.getElementById('<%=txtRSLabourCost.ClientID %>').value);

                    if (DeprecitedValue.toString() == "NaN" || DeprecitedValue == "")
                        DeprecitedValue = 0;
                    if (ReplacementValue.toString() == "NaN" || ReplacementValue == "")
                        ReplacementValue = 0;
                    if (Percent.toString() == "NaN")
                        Percent = 0;
                    if (PmntHighHouseVal.toString() == "NaN" || PmntHighHouseVal == "")
                        PmntHighHouseVal = 0;
                    if (ResidentialDAinAmountVal.toString() == "NaN" || ResidentialDAinAmountVal == "")
                        ResidentialDAinAmountVal = 0;
                    if (MovingAllowanceVal.toString() == "NaN" || MovingAllowanceVal == "")
                        MovingAllowanceVal = 0;
                    if (LabourCostVal.toString() == "NaN" || LabourCostVal == "")
                        LabourCostVal = 0;

                    var ReplacementUpLift = parseFloat(ReplacementValue) - parseFloat(DeprecitedValue);
                    var RDAllowance = (ReplacementValue * Percent) / 100;
                    var ReplacementHouseValue = parseFloat(RDAllowance) + parseFloat(ReplacementValue);

                    if (PmntHighHouseVal == 0) {
                        PmntHighHouseVal = ReplacementHouseValue;
                        document.getElementById('<%=txtRSPaymentHighHouseValue.ClientID %>').value = AddComma(PmntHighHouseVal);
                    }
                    ReplacementHouseValue = parseFloat(ReplacementHouseValue) + parseFloat(MovingAllowanceVal) + parseFloat(LabourCostVal);

                    document.getElementById('<%=txtReplacementUplift.ClientID %>').value = AddComma(Math.round(ReplacementUpLift));
                    document.getElementById('<%=txtResidentialDAinAmount.ClientID %>').value = AddComma(Math.round(RDAllowance));
                    document.getElementById('<%=txtHouseValues.ClientID %>').value = AddComma(Math.round(DeprecitedValue)); //PmntHighHouseVal; //For Summery
                    document.getElementById('<%=txtReplacementHouseValue.ClientID %>').value = AddComma(Math.round(ReplacementHouseValue)); //For Summery Replacement House Value

                    document.getElementById('<%=txtRSPaymentHighHouseValue.ClientID %>').value = AddComma(Math.round(ReplacementHouseValue));

                    if (ReplacementValue == 0) {
                    /*When Replacment Value is 0 then it should not to add other Values to it*/
                        document.getElementById('<%=txtReplacementHouseValue.ClientID %>').value = 0; //For Summery Replacement House Value

                        document.getElementById('<%=txtRSPaymentHighHouseValue.ClientID %>').value = 0;
                    }

                    break;

                case 'FIX': //FIXTURES
                    var FixturesValuation = RemoveComma(document.getElementById('<%=txtFixturesValuation.ClientID %>').value);
                    var Percent = RemoveComma(document.getElementById('<%=txtFixturesDAinPercentage.ClientID %>').value);
                    //alert(RemoveComma(document.getElementById('<%=txtFixturesDAinPercentage.ClientID %>').value));
                    if (FixturesValuation.toString() == "NaN" || FixturesValuation == "")
                        FixturesValuation = 0;
                    if (Percent.toString() == "NaN" || Percent == "") {
                        Percent = 0;
                    }

                    var FixtureTotal = (FixturesValuation * Percent) / 100;
                    var SummeryFixtureValue = parseFloat(FixturesValuation) + parseFloat(FixtureTotal);

                    document.getElementById('<%=txtFixturesDAinAmount.ClientID %>').value = AddComma(Math.round(FixtureTotal));
                    document.getElementById('<%=txtSummeryFixturesValue.ClientID %>').value = AddComma(Math.round(SummeryFixtureValue));
                    break;

                case 'CRP': //CROPS
                    var CropValue = RemoveComma(document.getElementById('<%=txtCropsValuation.ClientID %>').value);
                    var Percent = RemoveComma(document.getElementById('<%=txtCropsDAinPercentage.ClientID %>').value);
                    var MaxCapValue = RemoveComma(document.getElementById('<%=txtValuationAfterMaxCap.ClientID %>').value);

                    if (CropValue.toString() == "NaN" || CropValue == "")
                        CropValue = 0;
                    if (Percent.toString() == "NaN" || Percent == "")
                        Percent = 0;

                    if (document.getElementById('<%=chkMaxCapCase.ClientID %>').checked) {

                        var CropDAAmount = (MaxCapValue * Percent) / 100;
                        var CropTotalValuation = parseFloat(CropDAAmount) + parseFloat(MaxCapValue);
                    }
                    else {
                        var CropDAAmount = (CropValue * Percent) / 100;
                        var CropTotalValuation = parseFloat(CropDAAmount) + parseFloat(CropValue);
                    }

                    if (CropDAAmount.toString() == "NaN" || CropDAAmount == "")
                        CropDAAmount = 0;
                    if (CropTotalValuation.toString() == "NaN" || CropTotalValuation == "")
                        CropTotalValuation = 0;
                    document.getElementById('<%=txtCropsDAinAmount.ClientID %>').value = AddComma(Math.round(CropDAAmount));
                    document.getElementById('<%=txtCropsValue.ClientID %>').value = AddComma(Math.round(CropTotalValuation));
                    break;

                case 'OTHR': //OTHERS
                    var CultProperty = RemoveComma(document.getElementById('<%=txtCompensationForCultureProperty.ClientID %>').value);
                    var DamagedCrops = RemoveComma(document.getElementById('<%=txtOtherDamagedCrops.ClientID %>').value);

                    if (CultProperty.toString() == "NaN" || CultProperty == "")
                        CultProperty = 0;
                    if (DamagedCrops.toString() == "NaN" || DamagedCrops == "")
                        DamagedCrops = 0;

                    var otherTotal = parseFloat(CultProperty) + parseFloat(DamagedCrops);

                    document.getElementById('<%=txtOthersTotal.ClientID %>').value = AddComma(Math.round(otherTotal));
                    document.getElementById('<%=txtCulturePropertyValue.ClientID %>').value = AddComma(Math.round(CultProperty));
                    document.getElementById('<%=txtDamagedCropsValue.ClientID %>').value = AddComma(Math.round(DamagedCrops));
                    break;
            }

            FindTotalAmount();
        }
        //Find Total Amount
        function FindTotalAmount() {
            //Total Amount = Land Values+House Values+Replacement Values+Fixtures Values+CropsValues+Damaged Crop Values+Cultural Property Value+Facilitation Allownace
            var LandValue = RemoveComma(document.getElementById('<%=txtSummaryLandValues.ClientID %>').value);
            var HouseValue = 0; //RemoveComma(document.getElementById('<%=txtHouseValues.ClientID %>').value);
            var RepHouseValue = RemoveComma(document.getElementById('<%=txtReplacementHouseValue.ClientID %>').value);
            var FixtureValue = RemoveComma(document.getElementById('<%=txtSummeryFixturesValue.ClientID %>').value);
            var CropValue = RemoveComma(document.getElementById('<%=txtCropsValue.ClientID %>').value);
            var DamagedCropValue = RemoveComma(document.getElementById('<%=txtDamagedCropsValue.ClientID %>').value);
            var CulPropertyValue = RemoveComma(document.getElementById('<%=txtCulturePropertyValue.ClientID %>').value);
            var FacilitationValue = RemoveComma(document.getElementById('<%=txtFacilitationallowances.ClientID %>').value);
           // alert(FacilitationValue);
            if (LandValue.toString() == "NaN" || LandValue == "") {
                LandValue = 0;
            }
            if (HouseValue.toString() == "NaN" || HouseValue == "") {
                HouseValue = 0;
            }
            if (RepHouseValue.toString() == "NaN" || RepHouseValue == "") {
                RepHouseValue = 0;
            }
            if (FixtureValue.toString() == "NaN" || FixtureValue == "") {
                FixtureValue = 0;
            }
            if (CropValue.toString() == "NaN" || CropValue == "") {
                CropValue = 0;
            }

            if (DamagedCropValue.toString() == "NaN" || DamagedCropValue == "") {
                DamagedCropValue = 0;
            }
            if (CulPropertyValue.toString() == "NaN" || CulPropertyValue == "") {
                CulPropertyValue = 0;
            }
            if (FacilitationValue.toString() == "NaN" || FacilitationValue == "") {
                FacilitationValue = 0;
            }
            var TotalValue = parseFloat(LandValue) + parseFloat(HouseValue) + parseFloat(RepHouseValue) + parseFloat(FixtureValue) + parseFloat(CropValue) + parseFloat(DamagedCropValue) + parseFloat(CulPropertyValue) + parseFloat(FacilitationValue);
            //alert(AddComma(Math.round(TotalValue)));
            document.getElementById('<%=txtTotalAmount.ClientID%>').value = AddComma(Math.round(TotalValue));
            document.getElementById('<%=txtTotalAmount.ClientID%>').disabled = 'disabled';
        }
        //Round OFF Function for Last Two Digits based on DB round Value
        function RoundOFF(iValue) {
            var LimitValue = document.getElementById('<%=hdnRoundOffValue.ClientID %>').value;
            var lastTwoDigits = parseInt(iValue) % 100;

            if (parseInt(iValue.toString().length) < 3) {
                // alert(iValue.toString().length);
                return iValue;
            }
            var roundedValue;
            if (lastTwoDigits <= LimitValue)
                roundedValue = parseInt(iValue) - parseInt(lastTwoDigits);
            else {
                var remainingValue = 100 - parseInt(lastTwoDigits);
                roundedValue = parseInt(iValue) + remainingValue;
            }
            return roundedValue;
        }
        function checkMaxLength(e) {

            if (e.value.length > 999) {
                // alert('no no no');
                // Set value back to the first 6 characters 
                e.value = e.value.substring(0, 999);
            }
            return true;
        }
        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }
        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        function FieldChecked(iControl) {
            if (iControl.checked.toString() == 'true') {
                document.getElementById('<%=txtValuationAfterMaxCap.ClientID%>').disabled = '';
            }
            else {
                document.getElementById('<%=txtValuationAfterMaxCap.ClientID%>').disabled = 'disabled';
            }
        }
    </script>
</asp:Content>
