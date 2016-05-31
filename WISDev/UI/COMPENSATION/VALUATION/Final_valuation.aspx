<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    UICulture="en" Culture="en-US" CodeBehind="Final_valuation.aspx.cs" Inherits="WIS.Final_valuation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy"
    TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ Register Src="ValuationMenu.ascx" TagName="ValuationMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../../Scripts/CommonFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:ValuationMenu ID="ValuationMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right">
        <table width="100%">
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td align="right" style="width: 180px">
                    <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Final Valuation</legend>
            <div style="float: right">
                <a id="lnkUPloadDoc" href="#" runat="server"><b>Upload Document</b></a> &nbsp;&nbsp;|&nbsp;&nbsp;
                <a id="lnkUPloadDoclist" href="#" runat="server"><b>View Upload Document</b></a>
            </div>
            <script type="text/javascript" language="javascript">
                function OpenUploadDocumnet(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                    var left = (screen.width - 800) / 2;
                    var top = (screen.height - 600) / 4;
                    open('../../UploadDocPop.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPop', 'width=800px,height=600px,top=' + top + ', left=' + left);
                }

                function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
                    var left = (screen.width - 800) / 2;
                    var top = (screen.height - 600) / 4;
                    open('../../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=600px,top=' + top + ', left=' + left);
                }
            </script>
            <table id="table1" align="center">
                <tr>
                    <td style="width: 180px;">
                        &nbsp;
                    </td>
                    <td class="iceLable" align="center" style="width: 140px;">
                        Valuation Amount
                    </td>
                    <td align="center" style="width: 180px;">
                        <label class="iceLable">
                            Max Cap Case</label>
                    </td>
                    <td align="center" style="width: 140px;">
                        <label class="iceLable">
                            Negotiated Amount</label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Crops Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="cropTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            ReadOnly="True" onKeyDown="doCheck()" Style="text-align: right;">
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkMaxCapCase" runat="server" Enabled="false" />
                        <asp:TextBox ID="txtMaxcapCrops" runat="server" Text="" class="iceTextBox" MaxLength="20"
                            ReadOnly="True" Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCropsNegAmount" runat="server" Text="" class="iceTextBox" MaxLength="20"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="CropsrequestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblCropsStatus" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Land Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="landTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" onKeyDown="doCheck()" ReadOnly="True">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtLandNegAmount" runat="server" Text="" class="iceTextBox" MaxLength="20"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="LandrequestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblLandStatus" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Fixtures Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="fixturesTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" onKeyDown="doCheck()" ReadOnly="True">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtFixturesNegAmount" runat="server" Text="" MaxLength="20" class="iceTextBox"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="FixturesrequestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblFixturesStatus" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <%--  <tr>
                    <td>
                        <label class="iceLable">
                            House Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="houseTextBox" runat="server" CssClass="iceTextBox" Width="200px"
                            Style="text-align: right;" ReadOnly="True" onKeyDown="doCheck()">
                        </asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <label class="iceLable">
                            Replacement Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="replacementTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" ReadOnly="True" onKeyDown="doCheck()">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtReplacementNegAmount" runat="server" Text="" MaxLength="20" class="iceTextBox"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="ReplacementrequestButton" runat="server" Text="Request For Change"
                            CssClass="icebutton" Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblReplacementStatus" runat="server" Style="text-decoration: blink;
                            color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Damaged crop Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="damagedTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" ReadOnly="True" onKeyDown="doCheck()">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDamagedNegAmount" runat="server" Text="" MaxLength="20" class="iceTextBox"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="DamagedrequestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblDamagedStatus" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Cultural Property Value</label>
                    </td>
                    <td>
                        <asp:TextBox ID="culturalTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" ReadOnly="True" onKeyDown="doCheck()">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCulturalNegAmount" runat="server" Text="" MaxLength="20" class="iceTextBox"
                            Style="width: 140px; text-align: right;" onkeypress="javascript:return isNumber (event);" />
                    </td>
                    <td valign="top">
                        <asp:Button ID="CulturalrequestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButtonForInd_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="lblCulturalStatus" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="iceLable">
                            Grand Total</label>
                    </td>
                    <td>
                        <asp:TextBox ID="grandTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                            Style="text-align: right;" ReadOnly="True" onKeyDown="doCheck()">
                        </asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:UpdatePanel ID="updNEGAmount" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="negotiatedTextBox" runat="server" CssClass="iceTextBox" Width="140px"
                                                Style="text-align: right;" MaxLength="20" onblur="CheckAmount(this);" />
                                            <asp:RequiredFieldValidator ID="rfvnegotiatedTextBox" runat="server" ControlToValidate="negotiatedTextBox"
                                                ValidationGroup="vgFinalValuation" Text="Mandatory" ErrorMessage="Negotiated Amount cannot be Zero or empty."
                                                Display="None">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="cmpnegotiatedTextBox1" runat="server" ControlToValidate="negotiatedTextBox"
                                                ClientValidationFunction="CheckValue" ErrorMessage="Negotiated Amount cannot be Zero or empty."
                                                ValidationGroup="vgFinalValuation" Display="None">
                                            </asp:CustomValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" FilterType="Numbers,Custom"
                                                TargetControlID="negotiatedTextBox" ValidChars="," runat="server">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnLoadNEGAmount" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td valign="middle">
                        <asp:Button ID="btnLoadNEGAmount" Style="display: none" runat="server" OnClick="btnLoadExpense_Click" />
                        <asp:Button ID="requestButton" runat="server" Text="Request For Change" CssClass="icebutton"
                            Width="150px" OnClick="requestButton_Click" ValidationGroup="vgFinalValuation" />
                    </td>
                    <td>
                        <asp:Label ID="StatusLabel" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <center>
                            <asp:ValidationSummary ID="valFinalValuation" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgFinalValuation"
                                runat="server" />
                            <asp:Label ID="lblStatusValuationPCI" runat="server" Style="text-decoration: blink;
                                font-family: Arial; font-size: 16px; font-weight: bold" />
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <fieldset class="icePnlinner">
                            <legend>Valuation Comments</legend>
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" width="20%">
                                        <label class="iceLable">
                                            Comments
                                        </label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="commentsTextBox" runat="server" CssClass="iceTextBox" Width="500px"
                                            Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <a id="lnkFinalValuation" runat="server" href="#" class="iceLinkButton" style="text-decoration: none;
                                            color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px;
                                            height: 17px; margin-top: -0.5px; vertical-align: middle;">Change Request</a>&nbsp;
                                        <asp:Button ID="btn_Save" runat="server" ValidationGroup="ValSummary" value="Save"
                                            class="icebutton" Text="Save" OnClick="btn_Save_Click" />&nbsp;
                                        <asp:Button ID="clearButton" runat="server" ValidationGroup="ValSummary" value="Clear"
                                            class="icebutton" Text="Clear" OnClick="clearButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Label ID="StatusFinalValuation" runat="server" Style="text-decoration: blink;
                            color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <script language="javascript" type="text/javascript">

            spnpnldiv = document.getElementById('table1');
            if (spnpnldiv != null) {
                scrWidth = screen.availWidth;
                spnpnldiv.style.width = parseInt(scrWidth - 120).toString() + "px";
            }

            function isNumber(evt) {
                var iKeyCode = (evt.which) ? evt.which : evt.keyCode

                if (iKeyCode != 44 && iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                    return false;

                return true;
            }

            function CheckText(src) {
                if (RemoveComma(src.value.toString()) == '') {
                    alert('Negotiated Amount cannot be Zero or empty.');
                    return false;
                }
                else if (parseFloat(RemoveComma(src.value.toString())) <= 0) {
                    alert('Negotiated Amount cannot be Zero or empty.');
                    return false;
                }
            }

            function CalculateAmount() {

                var Crop;
                var Land;
                var Fixtures;
                //               
                var Replacement;
                var Damage;
                var Cultural;
                var Grand;

                Crop = document.getElementById('<%=cropTextBox.ClientID %>').value;
                if (isNaN(Crop)) Crop = 0;
                Crop = parseFloat(Crop);

                Land = document.getElementById('<%=landTextBox.ClientID %>').value;
                if (isNaN(Land)) Land = 0;
                Land = parseFloat(Land);

                Fixtures = document.getElementById('<%=fixturesTextBox.ClientID %>').value;
                if (isNaN(Fixtures)) Fixtures = 0;
                Fixtures = parseFloat(Fixtures);

                Replacement = document.getElementById('<%=replacementTextBox.ClientID %>').value;
                if (isNaN(Replacement)) Replacement = 0;
                Replacement = parseFloat(Replacement);

                Damage = document.getElementById('<%=damagedTextBox.ClientID %>').value;
                if (isNaN(Damage)) Damage = 0;
                Damage = parseFloat(Damage);

                Cultural = document.getElementById('<%=culturalTextBox.ClientID %>').value;
                if (isNaN(Cultural)) Cultural = 0;
                Cultural = parseFloat(Cultural);

                Grand = Crop + Land + Fixtures + Replacement + Damage + Cultural;

                if (!isNaN(Grand)) {
                    document.getElementById('<%=grandTextBox.ClientID %>').value = Grand;
                }
            }

            function CheckValue(oSrc, args) {

                var val = document.getElementById('<%=negotiatedTextBox.ClientID %>').value;
                if (val == undefined) {
                    args.IsValid = false;
                    return;
                }
                else if (parseFloat(RemoveComma(val)) <= 0) {
                    args.IsValid = false;
                    return;
                }

                args.IsValid = true;
            }

            function doCheck() {


                var keyCode = (event.which) ? event.which : event.keyCode;
                if ((keyCode == 8) || (keyCode == 46))
                    event.returnValue = false;
            }

            function CheckAmount(src) {
                var amount;
                var val = RemoveComma(src.value);

                if (!isNaN(val))
                    amount = val;
                else
                    amount = '';
                src.value = AddComma(amount);
            }

            function AddComma(iValue) {
                return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }

            function RemoveComma(iValue) {
                return iValue.toString().replace(/,?/g, "");
            }

            function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, NegotiatedAmount, pageCode) {
                //                var element1 = document.getElementById('<%=requestButton.ClientID%>');
                //                element1.style.display = "block";
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&NegotiatedAmount=' + NegotiatedAmount + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
            }

            function refreshValue() {

                $get('<%=btnLoadNEGAmount.ClientID%>').click();
                //                var element1 = document.getElementById('<%=requestButton.ClientID%>');
                //                element1.style.display = "none";

            }

            function OpenChangeRequestfroze(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
                var left = (screen.width - 600) / 2;
                var top = (screen.height - 500) / 4;
                open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
            }                  
        </script>
    </div>
</asp:Content>
