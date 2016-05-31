<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PAPLivelihood.aspx.cs" Inherits="WIS.PAPLivelihood" %>

<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
    <fieldset class="icePnlinner">
        <legend>Livelihood Details</legend>
        <asp:GridView ID="grdLivelihoodItems" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" ShowFooter="true"
            Width="100%" OnRowDataBound="grdLivelihoodItems_RowDataBound">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                        <asp:Literal ID="litItemID" Text='<%#Eval("Itemid") %>' runat="server" Visible="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Description">
                    <ItemStyle HorizontalAlign="Left" Width="43%" />
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ITEMNAME") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        Total Cash :
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cash (USH)">
                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                    <FooterStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:TextBox ID="txtCash" runat="server" MaxLength="14" style="text-align:right" Text="0"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCash" FilterType="Numbers,Custom" ValidChars=","
                            TargetControlID="txtCash" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTotalCash" Text="" ReadOnly="true" runat="server" ForeColor="Black" Font-Bold="true" style="text-align:right"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Kind">
                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                    <ItemTemplate>
                        <asp:TextBox ID="txtInKind" runat="server" MaxLength="300" Text="NONE"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteInKind" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                            ValidChars=",; - '" TargetControlID="txtInKind" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <center>
            <table width="100%">
                <tr>
                  <td align="center">
                        <asp:Label ID="StatusPAPLivehood" runat="server" Style="text-decoration: blink; color: Red;
                    font-family: Arial; font-size: 18px; font-weight: bold" />
                    &nbsp; 
                      <asp:Button ID="lnkPAPLiveHood" runat="server" Text="Change Request" Width="120px"
                    CssClass="icebutton" Visible="false" />
                     &nbsp;
                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClick="btnSave_Click" />
                     &nbsp; 
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </fieldset>
    <fieldset class="icePnlinner">         
        <legend>Bank Details</legend>
        <asp:UpdatePanel ID="UplBank" runat="server">
            <ContentTemplate>
           
                <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
                    <tr>
                        <td align="left" colspan="4">
                                <label class="iceLable">
                                    Do you have a Bank Account</label>
                                <asp:CheckBox ID="chkHasBankAccount" runat="server" AutoPostBack="true" OnCheckedChanged="chkHasBankAccount_CheckedChanged" />
                                &nbsp;<label class="labelSuffix">(Check if YES)</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="15%">
                            <label class="iceLable">Name of Bank</label> <span id="spnMandatoryBank" class="mandatory" visible="false" runat="server">*</span>
                        </td>
                        <td align="left" width="35%">
                            <asp:DropDownList ID="ddlBank" CssClass="iceTextBox" 
                                AppendDataBoundItems="true" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlBank_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
                                ErrorMessage="Select Name of Bank" ValidationGroup="Bank" InitialValue="0" ControlToValidate="ddlBank"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" width="12%">
                            <label class="iceLable">Branch</label> <span id="spnMandatoryBranch" class="mandatory" visible="false" runat="server">*</span>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="uplBranch" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlBranch" CssClass="iceTextBox" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlBranch"
                                        ErrorMessage="Select Branch" Display="None" InitialValue="0" ValidationGroup="Bank" runat="server"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlBank" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label class="iceLable">Account No</label> <span id="spnMandatoryAccNum" class="mandatory" visible="false" runat="server">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAccountNo" CssClass="iceTextBox" MaxLength="30" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAccountNo"
                                ErrorMessage="Enter Account No" Display="None" ValidationGroup="Bank" runat="server"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <label class="iceLable">Account Name</label> <span id="spnMandatoryAccName" class="mandatory" visible="false" runat="server">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAccountName" CssClass="iceTextBox" MaxLength="300" Width="250px" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountName"
                                ErrorMessage="Enter Account Name" Display="None" ValidationGroup="Bank" runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSaveBank" CssClass="icebutton" Text="Save" ValidationGroup="Bank"
                                runat="server" OnClick="btnSaveBank_Click" />
                            &nbsp;<asp:Button ID="btnClearBank" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearBank_Click" />
                            <asp:ValidationSummary ID="Vsbank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Bank" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
         

    </fieldset>
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
        <tr>
            <td align="center">
                <asp:Button ID="lnkPAPLivehoodBNKDet" runat="server" Text="Change Request" Width="120px"
                    CssClass="icebutton" Visible="false" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="StatusPAPLivehood1" runat="server" Style="text-decoration: blink; color: Red;
                    font-family: Arial; font-size: 18px; font-weight: bold" />
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function CalculateTotalCash(src) {
            fldCurrItemID = src.id;
            var val = src.value.replace(/,?/g, "");
            if (val == "") {
                val = 0;
            }
            var amount = 0;
            if (!isNaN(val))
                amount = val;
            else
                amount = 0;
            document.getElementById(src.id).value = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

            elems = document.getElementsByTagName('input');
            totalCash = 0;

            for (i = 0; i < elems.length; i++) {
                elem = elems[i];
                if (elem.type == 'text' && elem.id.indexOf('txtCash') >= 0) {
                    if (elem.value != "") totalCash += parseFloat(elem.value.toString().replace(/,?/g, ""));
                }
            }

            document.getElementById('MainContent_grdLivelihoodItems_txtTotalCash').value = totalCash.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
//        document.getElementById('divAll').onclick = function () {
//            isDirty = 0;
//            setTimeout(function () { setDirtyText(); }, 100);
//        };
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSaveBank.ClientID  %>");
            var tat1 = document.getElementById("<%= txtAccountNo.ClientID  %>");
            var tat2 = document.getElementById("<%=txtAccountName.ClientID %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;
                //txtyes = 1;
            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                //isDirty = 2;
                return '';
            }
        }                
    </script>
</asp:Content>
