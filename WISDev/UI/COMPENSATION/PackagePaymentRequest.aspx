<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="PackagePaymentRequest.aspx.cs" Inherits="WIS.PackagePaymentRequest" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Src="HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/page_specific.css" />

    <script type="text/javascript">
        function OpenBatchingHistory() {

            var HHID = "<%=Session["HH_ID"]%>";
            var ProjectID = "<%=Session["PROJECT_ID"]%>"

            var left = (screen.width - 960) / 2;
            var top = (screen.height - 650) / 4;

            var modal = 'yes';
            var ReportWindow = "";
            ReportWindow = window.open('/UI/REPORTUI/RptViewer.aspx?WorkflowCode=PAYRQHIST&ProjectID=' + ProjectID + '&HHID=' + HHID, 'Batch History', 'width=960px,height=650px,resizable=1,scrollbars=1,top=' + top + ', left=' + left + ',modal=' + modal);
            ReportWindow.focus();
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tkManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <fieldset class="icePnlinner">
        <legend>Package Fund Request Info</legend>
        <table border="0" cellpadding="2" cellspacing="5" style="width: 100%">
            <tr>
                <td style="vertical-align: top;">
                    <fieldset class="icePnlinner">
                        <legend>Summary</legend>
                        <table width="100%" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td style="width: 5%">
                                    <asp:CheckBox ID="ChkAll" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'All',1);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Select All</label>
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
                                <td>
                                    <label class="iceLable">
                                        Item Status
                                    </label>
                                </td>
                                <td>
                                    <a id="lnkBatchingHistory" href="" class="iceStatusLinks" style="border-left: 1px solid;" onclick="OpenBatchingHistory();">View Batch History</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    <asp:CheckBox ID="chkLandValuation" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'LV',1);" />
                                </td>
                                <td align="left" style="width: 20%">
                                    <label class="iceLable">
                                        Land Valuation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLandValuation" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td nowrap>
                                    <asp:TextBox ID="txtInKindLand" runat="server" class="iceTextBox" Enabled="false" Style="width: 140px; text-align: right;" MaxLength="12" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        TargetControlID="txtInKindLand" ValidChars="." FilterType="Numbers,Custom">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    Acres
                                </td>
                                <td>
                                    <asp:Label ID="lblLandValuationMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkFixtureValuation" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'FV',2);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Fixtures Valuation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFixturesValuation" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblFixturesValuationMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td align="left">
                                    <label class="iceLable">
                                        House In-kind Option</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHouseInKindOption" runat="server" Enabled="true" class="iceTextBox"
                                        Style="width: 200px;" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                        ValidChars="., " TargetControlID="txtHouseInKindOption" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkCropsValuation" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'CV',3);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Crops Valuation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCropsValuation" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblCropsValuationMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkReplacementHouseValue" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'RV',4);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Replacement House Value</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtReplacementHouseValue" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtResidentialStructure" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 140px;" />
                                    <asp:DropDownList ID="ddlResidentialStructure" Visible="false" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="UETCL Resettlement House">UETCL Resettlement House</asp:ListItem>
                                        <asp:ListItem Value="UETCL Building Materials+Labour">UETCL Building Materials+Labour</asp:ListItem>
                                        <asp:ListItem Value="NA">NA</asp:ListItem>
                                    </asp:DropDownList>
                                    <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlResidentialStructure"
                                        PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                        IsSorted="true" />
                                </td>
                                <td>
                                    <asp:Label ID="lblReplacementHouseValueMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkDamagedCropValue" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'DV',5);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Damaged Crop Value</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDamagedCropValue" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblDamagedCropValueMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkCulturePropertyValue" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'PV',6);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Culture Property Valuation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCultureProperty" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblCulturePropertyMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkFacilitationValue" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'OV',7);" />
                                </td>
                                <td align="left">
                                    <label class="iceLable">
                                        Facilitation/GOU allowance/Others</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFacilitation" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblFacilitationMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <%--<td>
                                    <asp:CheckBox ID="chkFinalCompensation" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'Final Compensation',7);" />
                                </td>--%>
                                <td align="left">
                                    <label class="iceLable">
                                        Total Compensation</label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFinalCompensation" runat="server" onkeypress="return CheckDecimal(event, this)"
                                        Enabled="false" class="iceTextBox" Style="width: 180px; text-align: right;" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers,Custom"
                                        ValidChars=".," TargetControlID="txtFinalCompensation" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblFinalCompensationMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                    <asp:HiddenField ID="hdnNegotiatedAmount" runat="server" Value="" />
                                </td>
                            </tr>
                            <tr id="trNegotiatedAmount" runat="server" visible="false">
                                <td>
                                    <asp:CheckBox ID="chkNegotiatedAmount" Text="" runat="server" ValidationGroup="vgChk"
                                        onclick="FieldChecked(this,'NV',8);" />
                                </td>
                                <td>
                                    <label class="iceLable">
                                        Negotiated Amount</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNegotiatedAmount" runat="server" Enabled="false" class="iceTextBox"
                                        Style="width: 180px; text-align: right;" />
                                    <%-- <b><asp:Label ID="lblNegotiatedAmount" runat="server" Text="0" BorderWidth="0px" Style="width: 120px;
                                            text-align: right; border: 1px"></asp:Label></b>--%>
                                </td>
                                <td>&nbsp</td>
                                <td>
                                    <asp:Label ID="lblNegotiatedAmountMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top">
                    <fieldset class="icePnlinner">
                        <legend>Payment Request Information</legend>
                        <asp:Panel ID="pnlPaymentReqInfo" runat="server">
                            <asp:Panel ID="pnlPaymentRequest" runat="server">
                                <table border="0" cellpadding="3" cellspacing="0" style="margin-bottom: -14px; margin-right: -13px;"
                                    width="100%">
                                    <tr>
                                        <td colspan="4" align="left">
                                            <label class="iceLable">
                                                Note: Click on a valuation summary Check Box to continue</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="18%">
                                            <div>
                                                <label class="iceLable">
                                                    Request Date</label><span class="mandatory">*</span>
                                        </td>
                                        <td width="32%">
                                            <asp:TextBox ID="txtPaymentFor" runat="server" Text="" Width="220px" Style="display: none"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPaymentFor"
                                                ValidationGroup="vgBatch" Text="Mandatory" ErrorMessage="Select Payment For"
                                                Display="None"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="dpRequestDate" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calRequestDate" runat="server" CssClass="WISCalendarStyle"
                                                TargetControlID="dpRequestDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvRequstedDate" runat="server" Display="None" ControlToValidate="dpRequestDate"
                                                ValidationGroup="vgBatch" ErrorMessage="Select Request Date"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" width="12%"></td>
                                        <td align="left"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="iceLable">
                                                Amount Requested</label>
                                            <span class="mandatory">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmountRequested" runat="server" Text="" Style="text-align: right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmountRequested"
                                                ValidationGroup="vgBatch" Text="Mandatory" ErrorMessage="Amount Requested" Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                        <td width="12%">
                                            <label class="iceLable">
                                                Total Amount</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalAmount" runat="server" Text="" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label class="iceLable">
                                                Comments</label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtComments" CssClass="iceTextBox" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="iceLable">Select Batch</label>
                                        </td>
                                        <td colspan="3" style="width: 30%">
                                            <asp:UpdatePanel ID="uplBatch" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div style="float: left">
                                                        <asp:RadioButtonList ID="rbBatch" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                            OnSelectedIndexChanged="rbBatch_SelectedIndexChanged" CellPadding="3" CellSpacing="2">
                                                            <asp:ListItem Value="2">New Batch</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">Existing Batch</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div style="float: left; padding-left: 5px">
                                                        <asp:DropDownList ID="ddlBatchList" runat="server" CssClass="iceDropDown" Width="120">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatchList"
                                                        ValidationGroup="vgBatch" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Batch"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="btnAddToBatch" runat="server" Text="Add to Batch" class="icebutton"
                                                ValidationGroup="vgBatch" Style="width: 120px" OnClick="btnAddToBatch_Click" />
                                            <asp:ValidationSummary ID="vsPaymentMode" runat="server" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="vgBatch" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <asp:Label ID="lblRequestStatus" CssClass="iceLable" Text="" Visible="false" runat="server"></asp:Label>
                        </asp:Panel>
                        <ul id="itemStatuses" runat="server" style="color:red;">
                            <li id="itemCompPackageStatus" runat="server" style="display:none;"><asp:Label ID="lblCompPackageStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                            <li id="itemDisclosureStatus" runat="server" style="display:none;"><asp:Label ID="lblDisclosureStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                            <li id="itemGrievanceStatus" runat="server" style="display:none;"><asp:Label ID="lblGrievanceStatus" runat="server" Style="text-decoration: blink; color: Red; font-family: Arial; font-size: 18px; font-weight: bold" /></li>
                        </ul>
                        
                        
                        
                    </fieldset>
                </td>
            </tr>
        </table>
    </fieldset>
    <script type="text/javascript">
        PreventDateFieldEntry(document.getElementById('<%=dpRequestDate.ClientID%>'));

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

        function FieldChecked(iControl, PaymentFor, ctrlIdNo) {
            var control = iControl;
            if (PaymentFor == 'All') {
                var sattus1 = document.getElementById('<%=lblLandValuationMsg.ClientID %>').innerHTML.toString();
                var sattus2 = document.getElementById('<%=lblFixturesValuationMsg.ClientID %>').innerHTML.toString();
                var sattus3 = document.getElementById('<%=lblCropsValuationMsg.ClientID %>').innerHTML.toString();
                var sattus4 = document.getElementById('<%=lblReplacementHouseValueMsg.ClientID %>').innerHTML.toString();
                var sattus5 = document.getElementById('<%=lblDamagedCropValueMsg.ClientID %>').innerHTML.toString();
                var sattus6 = document.getElementById('<%=lblCulturePropertyMsg.ClientID %>').innerHTML.toString();
                var sattus7 = document.getElementById('<%=lblFacilitationMsg.ClientID %>').innerHTML.toString();
                //alert(iControl + ' Clicked True');
                //alert(sattus1.toLowerCase() +'<FONT class=StatusPending></FONT>'.toLowerCase());
                if (sattus1 == '' || sattus1.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus1.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus1.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus1.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtLandValuation.ClientID %>').value)) > 0
                    || parseFloat(RemoveComma(document.getElementById('<%=txtInKindLand.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkLandValuation.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus2 == '' || sattus2.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus2.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus2.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus2.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtFixturesValuation.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkFixtureValuation.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus3 == '' || sattus3.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus3.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus3.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus3.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtCropsValuation.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkCropsValuation.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus4 == '' || sattus4.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus4.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus4.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus4.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtReplacementHouseValue.ClientID %>').value)) > 0
                    || document.getElementById('<%=txtResidentialStructure.ClientID %>').value.toString().length > 0) {
                        document.getElementById('<%=chkReplacementHouseValue.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus5 == '' || sattus5.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus5.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus5.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus5.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtDamagedCropValue.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkDamagedCropValue.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus6 == '' || sattus6.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus6.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus6.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus6.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtCultureProperty.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkCulturePropertyValue.ClientID %>').checked = iControl.checked;
                    }
                }
                if (sattus7 == '' || sattus7.toLowerCase() == '<FONT class=StatusPending></FONT>'.toLowerCase() ||
                        sattus7.toLowerCase() == '<FONT class="StatusPending"></FONT>'.toLowerCase() ||
                        sattus7.toLowerCase() == '<FONT class="StatusPending">Declined</FONT>'.toLowerCase() ||
                        sattus7.toLowerCase() == '<FONT class=StatusPending>Declined</FONT>'.toLowerCase()) {
                    if (parseFloat(RemoveComma(document.getElementById('<%=txtFacilitation.ClientID %>').value)) > 0) {
                        document.getElementById('<%=chkFacilitationValue.ClientID %>').checked = iControl.checked;
                    }
                }

                //                if (document.getElementById('<%=hdnNegotiatedAmount.ClientID %>').value == "1") {
                //                    document.getElementById('<%=chkNegotiatedAmount.ClientID %>').checked = false;
                //                }
                //alert(document.getElementById(iControl) + ' Clicked True');
                if (iControl.checked) {
                    AncherLabelClicked(PaymentFor, 0);
                }
                else {
                    AncherLabelClicked('', 0);
                }
            }
            else if (PaymentFor == 'NV') {
                //                if (iControl.checked) {
                //                    document.getElementById('<%=ChkAll.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkLandValuation.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkFixtureValuation.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkCropsValuation.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkReplacementHouseValue.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkDamagedCropValue.ClientID %>').style.display = 'none';
                //                    document.getElementById('<%=chkCulturePropertyValue.ClientID %>').style.display = 'none';
                //                }
                //                else {
                //                    document.getElementById('<%=ChkAll.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkLandValuation.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkFixtureValuation.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkCropsValuation.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkReplacementHouseValue.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkDamagedCropValue.ClientID %>').style.display = '';
                //                    document.getElementById('<%=chkCulturePropertyValue.ClientID %>').style.display = '';
                //                }
                CalcAmount(PaymentFor);
            }
            else {
                AncherLabelClicked(PaymentFor, ctrlIdNo);
            }
    }

    function CalcAmount(PaymentFor) {
        var txtValue1 = 0;
        var pfor = "";
        if (document.getElementById('<%=chkNegotiatedAmount.ClientID %>').checked) {
            txtValue1 = document.getElementById('<%=txtNegotiatedAmount.ClientID %>').value;
            pfor += 'NV';
        }
        document.getElementById('<%=txtAmountRequested.ClientID %>').value = AddComma(parseFloat(RemoveComma(txtValue1)));
        document.getElementById('<%=txtPaymentFor.ClientID %>').value = pfor;
    }

    function AncherLabelClicked(PaymentFor, iValue2) {
        var pfor = "";
        var temp = parseInt(iValue2);
        var txtValue1 = 0; var txtValue2 = 0; var txtValue3 = 0; var txtValue4 = 0;
        var txtValue5 = 0; var txtValue6 = 0; var txtValue7 = 0;
        if (document.getElementById('<%=chkLandValuation.ClientID %>').checked) {
                txtValue1 = document.getElementById('<%=txtLandValuation.ClientID %>').value;
                pfor += ',LV';
            }
            if (document.getElementById('<%=chkFixtureValuation.ClientID %>').checked) {
                txtValue2 = document.getElementById('<%=txtFixturesValuation.ClientID %>').value;
                pfor += ',FV';
            }
            if (document.getElementById('<%=chkCropsValuation.ClientID %>').checked) {
                txtValue3 = document.getElementById('<%=txtCropsValuation.ClientID %>').value;
                pfor += ',CV';
            }
            if (document.getElementById('<%=chkReplacementHouseValue.ClientID %>').checked) {
                txtValue4 = document.getElementById('<%=txtReplacementHouseValue.ClientID %>').value;
                pfor += ',RV';
            }
            if (document.getElementById('<%=chkDamagedCropValue.ClientID %>').checked) {
                txtValue5 = document.getElementById('<%=txtDamagedCropValue.ClientID %>').value;
                pfor += ',DV';
            }
            if (document.getElementById('<%=chkCulturePropertyValue.ClientID %>').checked) {
                txtValue6 = document.getElementById('<%=txtCultureProperty.ClientID %>').value;
                pfor += ',PV';
            }
            if (document.getElementById('<%=chkFacilitationValue.ClientID %>').checked) {
                txtValue7 = document.getElementById('<%=txtFacilitation.ClientID %>').value;
                pfor += ',OV';
            }

            if (pfor.toString().length > 0) {
                pfor = pfor.substring(1, pfor.toString().length);
                document.getElementById('<%=txtPaymentFor.ClientID %>').value = pfor;
            }
            else {
                document.getElementById('<%=txtPaymentFor.ClientID %>').value = pfor;
            }
            //            else if (temp == 7)
            //                txtValue = document.getElementById('<%=txtFinalCompensation.ClientID %>').value;
            //            else if (temp == 8)
            //                txtValue = document.getElementById('<%=txtNegotiatedAmount.ClientID %>').value;
            //Amount Assignment
            document.getElementById('<%=txtAmountRequested.ClientID %>').value = AddComma(parseFloat(RemoveComma(txtValue1)) + parseFloat(RemoveComma(txtValue2)) +
            parseFloat(RemoveComma(txtValue3)) + parseFloat(RemoveComma(txtValue4)) + parseFloat(RemoveComma(txtValue5)) + parseFloat(RemoveComma(txtValue6)) + parseFloat(RemoveComma(txtValue7)));

            var NegoAmount = document.getElementById('<%=txtNegotiatedAmount.ClientID %>').value;
            if (NegoAmount.toString() != "NaN" && parseFloat(NegoAmount) > 0) {
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = document.getElementById('<%=txtNegotiatedAmount.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = document.getElementById('<%=txtFinalCompensation.ClientID %>').value;
            }
        }

        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }
        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    </script>
</asp:Content>
