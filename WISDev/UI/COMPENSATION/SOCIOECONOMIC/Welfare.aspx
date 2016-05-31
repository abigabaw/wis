<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Welfare.aspx.cs" Inherits="WIS.Welfare" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <table border="0" width="98%">
        <tr>
            <td>
                <fieldset class="icePnlinner">
                    <legend>General Welfare Indicators from Government's Survey</legend>
                    <div style="width: 100%; height: 25px; float: right" >
                        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
                        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
                    </div>
                    <asp:DataList ID="lstWelfares" BackColor="#c7c7c7" BorderWidth="1" BorderColor="Black"
                        ItemStyle-BorderColor="Black" ItemStyle-BackColor="#f5f5f5" CellPadding="4" CellSpacing="1"
                        RepeatDirection="Horizontal" RepeatColumns="1" runat="server" Width="100%" OnItemDataBound="lstWelfares_ItemDataBound">
                        <ItemTemplate>
                            <asp:Literal ID="litWelfareID" Text='<%#Eval("WelfareIndicatorID")%>' Visible="false"
                                runat="server"></asp:Literal>
                            <asp:Literal ID="litAssociatedWith" Text='<%#Eval("AssociatedWith")%>' Visible="false"
                                runat="server"></asp:Literal>
                            <asp:Label ID="lblWelfareName" Text='<%#Eval("WelfareIndicatorName")%>' Width="600px"
                                runat="server"></asp:Label>
                            <asp:CheckBox ID="chkWelfareType" runat="server" /><asp:Label ID="lblCHKWelfareTypeMsg" CssClass="labelSuffix" runat="server">&nbsp;(Check if YES)</asp:Label>
                            <asp:TextBox ID="txtWelfareType" Visible="false" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <div style="width: 100%; height: 25px; float: right" >
                    <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
                    <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset class="icePnlinner" style="width: 380px">
                                <legend>Drinking Water</legend>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Where do you get Drinking Water from?</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWhereGetDrinkingWater" CssClass="iceTextBox" MaxLength="100"
                                                Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                How far is Water Source from Household?</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWaterSourceDistance" CssClass="iceTextBox" MaxLength="100" Width="120px"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </td>
                        <td style="padding-left: 5px; vertical-align: top">
                            <fieldset class="icePnlinner" style="width: 330px">
                                <legend>Fishing</legend>
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Do you fish</label>
                                        </td>
                                        <td align="left">
                                            <div>
                                                <asp:CheckBox ID="chkDoYouFish" runat="server" />
                                                &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Where do you fish</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWhereDoYouFish" CssClass="iceTextBox" Enabled="false" MaxLength="100"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                How often?</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtHowOftenFish" CssClass="iceTextBox" Enabled="false" MaxLength="100"
                                                Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset class="icePnlinner" style="width: 380px">
                                <legend>Hunting</legend>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Do you hunt in the present situation</label>
                                        </td>
                                        <td align="left">
                                            <div>
                                                <asp:CheckBox ID="chkDoYouHunt" runat="server" />
                                                &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                If yes, where?</label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtWhereHunt" CssClass="iceTextBox" Enabled="false" MaxLength="100"
                                                Width="120px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </td>
                        <td style="padding-left: 5px; vertical-align: top">
                            <fieldset class="icePnlinner" style="width: 330px">
                                <legend>Fuel</legend>
                                <table border="0" id="tabelFuel" width="100%">
                                    <tr>
                                        <td colspan="6">
                                            <label class="labelSuffix">(Check if YES)</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 25%">
                                            <label class="iceLable">
                                                Firewood</label>
                                        </td>
                                        <td align="left" style="width: 10%">
                                            <asp:CheckBox ID="chkFirewood" runat="server" />
                                        </td>
                                        <td align="left" style="width: 22%">
                                            <label class="iceLable">
                                                Charcoal</label>
                                        </td>
                                        <td align="left" style="width: 15%">
                                            <asp:CheckBox ID="chkCharcoal" runat="server" />
                                        </td>
                                        <td align="left">
                                            <label class="iceLable" style="width: 25%">
                                                Biogas</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkBiogas" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Gas</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkGas" runat="server" />
                                        </td>
                                        <td align="left">
                                            <label class="iceLable">
                                                Paraffin</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkParaffin" runat="server" />
                                        </td>
                                        <td align="left">
                                            <label class="iceLable">
                                                Other</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkOtherFuel" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label class="iceLable">
                                                Electricity</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkElectricity" runat="server" />
                                        </td>
                                        <td align="left">
                                            <label class="iceLable">
                                                Solar</label>
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkSolar" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="icePnlinner" style="width: 93%">
                    <legend>Other Observations and Comments</legend>
                    <asp:TextBox ID="txtComments" runat="server" CssClass="iceTextAera" TextMode="MultiLine"
                        Rows="3" Style="width: 100%"></asp:TextBox>
                </fieldset>
            </td>
        </tr>
    </table>
    <table border="0" width="40%" align="center">
        <tr>
            <td align="center">
                <asp:Button ID="lnkWelfare" runat="server" Text="Change Request" CssClass="icebutton"
                    Width="120px" Visible="false" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSave" Text="Save" CssClass="icebutton" runat="server" OnClientClick="return EnableDisabledInputFields(1);"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" Text="Clear" CssClass="icebutton" runat="server" OnClick="btnClear_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="StatusWelfare" runat="server" Style="text-decoration: blink; color: Red;
                    font-family: Arial; font-size: 18px; font-weight: bold" />
            </td>
        </tr>
    </table>
    <script language="javascript">
        function EnableDisabledInputFields(enable) {
            tbl = document.getElementById('<%=lstWelfares.ClientID%>');
            inputFieldArr = tbl.getElementsByTagName('input');
            for (ct = 0; ct < inputFieldArr.length; ct++) {
                elem = inputFieldArr[ct];

                if (elem.type == 'text') {
                    if (trim(elem.value.toString()).length > 0) {
                        return true;
                    }
                    //enable == 1 ? elem.disabled = false : elem.disabled = true;
                }
                else if (elem.type == "checkbox") {
                    if (elem.checked == true)
                        return true;
                }
            }

            var txt1 = document.getElementById('<%=txtWhereGetDrinkingWater.ClientID%>').value.toString();
            if (trim(txt1).length > 0) {
                return true;
            }

            txt1 = document.getElementById('<%=txtWaterSourceDistance.ClientID%>').value.toString();
            if (trim(txt1).length > 0) {
                return true;
            }

            var chk = document.getElementById('<%=chkDoYouFish.ClientID%>');
            if (chk.checked == true) {
                return true;
            }

            chk = document.getElementById('<%=chkDoYouHunt.ClientID%>');
            if (chk.checked == true) {
                return true;
            }
            var table = document.getElementById('tabelFuel');
            inputFieldtable = table.getElementsByTagName('input');
            for (i = 0; i < inputFieldArr.length; i++) {
                elem = inputFieldtable[i];
                if (elem.type == "checkbox") {
                    if (elem.checked == true)
                        return true;
                }
            }
            alert('Please Enter data to save.');
            return false;
        }

        function trim(myString) {
            return myString.replace(/^\s+|\s+$/g, '');
        }

        function Button1_onclick() {
            var inputs = document.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (inputs[i].checked == true)
                        return true;
                }
            }
            alert("No one checked");
            return false;
        }

        function EnableOtherTransEquipment(src, targetFldID) {
            targetFld = document.getElementById(targetFldID);

            if (targetFld != null) {
                if (src.checked) {
                    targetFld.disabled = false;
                    targetFld.focus();
                }
                else if (document.getElementById(src) != null) {
                    if (document.getElementById(src).checked) {
                        targetFld.disabled = false;
                    }
                }
                else {
                    targetFld.disabled = true;
                    targetFld.value = '';
                }
            }
        }

        function EnableFishing(src, field1, field2) {
            field1 = document.getElementById(field1);
            field2 = document.getElementById(field2);

            if (src.checked) {
                field1.disabled = false;
                field2.disabled = false;
                field1.focus();
            }
            else {
                field1.disabled = true;
                field2.disabled = true;
                field1.value = '';
                field2.value = '';
            }
        }

        function EnableHunting(src, field1) {
            field1 = document.getElementById(field1);

            if (src.checked) {
                field1.disabled = false;
                field1.focus();
            }
            else {
                field1.disabled = true;
                field1.value = '';
            }
        }
        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
