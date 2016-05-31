<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="LandInfo.aspx.cs" Inherits="WIS.LandInfo" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
     <div id="divAll">
     <div>
     
    <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
        <tr>
            <td align="left" style="width: 100px">
                <label class="iceLable">
                    Tenure of Land</label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlLandTenure" runat="server" CssClass="iceDropDown" Height="21px"
                    AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlLandTenure_SelectedIndexChanged">
                    <%--<asp:ListItem Value="0">--Select--</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <%-- <asp:UpdatePanel ID="upnlPublic" runat="server" UpdateMode="Conditional">
        <contenttemplate>--%>
    <asp:Panel ID="pnlPublic" runat="server" Visible="false">
        <fieldset class="icePnlinner">
            <legend>Information for Land Owners/Kibanja Owners/Leaseholders on Public Land</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
                <tr>
                    <td align="left" style="width: 17%">
                        <asp:Label ID="Label1" runat="server" CssClass="iceLable" Text="Details of Title (Papers)"></asp:Label>
                    </td>
                    <td align="left" class="iceNormalText" style="width: 34%">
                    <div>
                        <asp:CheckBox runat="server" ID="Chkhasitdetails" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                    <td align="left" style="width: 22%"> 
                        <asp:Label ID="Label2" runat="server" CssClass="iceLable" Text="Year of Acquisition"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtyear" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            MaxLength="4" Width="112px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtyear" FilterType="Numbers" TargetControlID="txtyear"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top">
                        <asp:Label ID="Label3" runat="server" CssClass="iceLable" Text="From Whom"></asp:Label>
                    </td>
                    <td align="left" style="vertical-align: top">
                        <asp:DropDownList ID="ddlReceivedFromWhom" runat="server" CssClass="iceDropDown"
                            AppendDataBoundItems="true" Enabled="true" Width="100px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtfromwhom" runat="server" ClientIDMode="Static" AutoCompleteType="Disabled"
                            Enabled="false" CssClass="iceTextBox" Width="112px" MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtfromwhom" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtfromwhom" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" CssClass="iceLable" Text="Comments"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtcoments" runat="server" TextMode="MultiLine" Rows="3" AutoCompleteType="Disabled"
                            CssClass="iceTextBox" Width="96%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" CssClass="iceLable" Text="Who has claim to the land?"></asp:Label>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:TextBox ID="txtland" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            MaxLength="100" Width="112px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtland" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtland" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label6" runat="server" CssClass="iceLable" Text="Lived on this plot since birth"></asp:Label>
                    </td>
                    <td align="left" class="iceNormalText">
                    <div>
                        <asp:CheckBox runat="server" ID="chkLivedSinceBirth" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label7" runat="server" CssClass="iceLable" Text="Year of move to this plot"></asp:Label>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:TextBox ID="txtyearmoved" runat="server" AutoCompleteType="Disabled" MaxLength="4"
                            CssClass="iceTextBox" Width="112px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtyearmoved" FilterType="Numbers" TargetControlID="txtyearmoved"
                            runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label8" runat="server" CssClass="iceLable" Text="Where were you living before"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtwherebefore" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            Width="112px" MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtwherebefore" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtwherebefore" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label9" runat="server" CssClass="iceLable" Text="Mortgage/Lien on your property"></asp:Label>
                    </td>
                    <td align="left">
                        <div style="float: left">
                        
                            <asp:CheckBox runat="server" ID="ChkMortagelies" />
                            &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                        <div style="float: left">
                            <asp:TextBox ID="txtmortagedetails" runat="server" AutoCompleteType="Disabled" Enabled="false"
                                CssClass="iceTextBox" Width="200px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="flttxtmortagedetails" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ," TargetControlID="txtmortagedetails" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <a id="lnkILOP" runat="server" href="#" runat="server" class="iceLinkButton" style="text-decoration: none;
                                        color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px;
                                        height: 17px; margin-top: -0.5px; vertical-align: middle;">Change Request</a>
                                </td>
                                <td>
                                    <asp:Button ID="btn_SavePub" runat="server" CssClass="icebutton" OnClick="btn_SavePublic"
                                        ValidationGroup="LandInfo" CausesValidation="true" Text="Save" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_ClearPublic" runat="server" CausesValidation="true" CssClass="icebutton"
                                        Text="Clear" OnClick="btn_ClearPublic_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="valsumLandInfo" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="LandInfo" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="StatusILOP" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <%--</contenttemplate>--%>
    <%--  <triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlLandTenure" EventName="SelectedIndexChanged" />
                        </triggers>
    </asp:UpdatePanel>--%>
    <%--  <asp:UpdatePanel ID="upnlMailo" runat="server" UpdateMode="Conditional">
        <contenttemplate>--%>
    <asp:Panel ID="pnlMailo" runat="server" Visible="false">
        <fieldset class="icePnlinner">
            <legend>Information for Tenants on Mailo Land</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
    </div>
            <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
                <tr>
                    <td align="left" width="25%">
                        <asp:Label ID="Label10" runat="server" CssClass="iceLable" Text="Who is your landlord "></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" width="25%">
                        <asp:TextBox ID="txtlandlord" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            Width="180px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtlandlord" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtlandlord" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqtxtlandlord" runat="server" ErrorMessage="Enter 'Who is your landlord?'"
                            ControlToValidate="txtlandlord" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" width="28%">
                        <asp:Label ID="Label11" runat="server" CssClass="iceLable" Text="Who has claim to this land "></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtwhoclaims" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            Width="180px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtwhoclaims" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtwhoclaims" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqtxtwhoclaims" runat="server" ErrorMessage="Enter 'Who has claim to this land?'"
                            ControlToValidate="txtwhoclaims" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label12" runat="server" CssClass="iceLable" Text="When did you begin farm on this land">
                                                             
                        </asp:Label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtwhenbeginfarm" runat="server" AutoCompleteType="Disabled" MaxLength="4" CssClass="iceTextBox"
                            Width="112px" ></asp:TextBox>
                              <asp:Label ID="LabelYear" runat="server" CssClass="iceLable" Text="Enter the Year"><span class="labelSuffix">(Enter Year)</span></asp:Label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtwhenbeginfarm" FilterType="Numbers"
                            ValidChars=" ," TargetControlID="txtwhenbeginfarm" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqtxtwhenbeginfarm" runat="server" ErrorMessage="Enter 'When did you begin farm on this land?'"
                            ControlToValidate="txtwhenbeginfarm" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label13" runat="server" CssClass="iceLable" Text="                                                               
                                                                Where did you farm before coming here "></asp:Label><span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtwheredidfarm" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                            Width="180px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="flttxtwheredidfarm" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" ," TargetControlID="txtwheredidfarm" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqtxtwheredidfarm" runat="server" ErrorMessage="Enter 'Where did you farm before coming here?'"
                            ControlToValidate="txtwheredidfarm" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label14" runat="server" CssClass="iceLable" Text="                                                              
                                                                Spouses farm on the land "></asp:Label>
                    </td>
                    <td align="left">
                    <div>
                        <asp:CheckBox runat="server" Text="" ID="Chkspouse" OnCheckedChanged="Chkspouse_CheckedChanged"
                            AutoPostBack="True" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label><asp:Label ID="chkmsg1" runat="server"
                            CssClass="mandatory" Text=""></asp:Label></div>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label15" runat="server" CssClass="iceLable" Text="                                                               
                                                                Children farm on the land "></asp:Label>
                    </td>
                    <td align="left">
                    <div>
                        <asp:CheckBox runat="server" ID="ChkChildren" AutoPostBack="True" OnCheckedChanged="ChkChildren_CheckedChanged" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                        <asp:Label ID="chkmsg" runat="server" CssClass="mandatory" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left" valign="top">
                        <asp:CheckBoxList ID="Chkspouselist" runat="server" CssClass="icePnlinner" BackColor="White"
                            Height="28px" Width="109px">
                        </asp:CheckBoxList>
                    </td>
                    <td>
                    </td>
                    <td align="left" valign="top">
                        <asp:CheckBoxList ID="Chkchildrenlist" CssClass="icePnlinner" BackColor="White" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top" colspan="2">
                        <div>
                            <asp:Label ID="Label16" runat="server" CssClass="iceLable" Text="                                                                
                                                                    Type of agreement with Land Owner"></asp:Label><span class="mandatory">*</span>
                        </div>
                        <div>
                            <asp:TextBox ID="txtagrrement" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                                Width="400px" TextMode="MultiLine"></asp:TextBox></div>
                        <asp:RequiredFieldValidator ID="reqtxtagrrement" runat="server" ErrorMessage="Enter 'Type of agreement with Land Owner'"
                            ControlToValidate="txtagrrement" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="vertical-align: top" colspan="2">
                        <div>
                            <asp:Label ID="Label17" runat="server" CssClass="iceLable" Text="                           
                                                                    Opportunities for Productive Asset "></asp:Label>
                            <span class="mandatory">*</span>
                        </div>
                        <div>
                            <asp:TextBox ID="txtprodutive" runat="server" AutoCompleteType="Disabled" CssClass="iceTextBox"
                                Width="400px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtprodutive" runat="server" ErrorMessage="Enter 'Opportunities for Productive Asset'"
                                ControlToValidate="txtprodutive" Display="None" ValidationGroup="LandInfo1"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: top" colspan="2">
                        &nbsp;
                    </td>
                    <td align="left" style="vertical-align: top" colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <a id="lnkITML" runat="server" href="#" runat="server" class="iceLinkButton" style="text-decoration: none;
                                        color: White; font-family: Arial; font-size: 12px; font-weight: normal; padding-top: 3px;
                                        height: 17px; margin-top: -0.5px; vertical-align: middle;">Change Request</a>
                                </td>
                                <td>
                                    <asp:Button ID="btn_SavePrivate" runat="server" CausesValidation="true" CssClass="icebutton"
                                        ValidationGroup="LandInfo1" OnClick="btnSave_Click" Text="Save" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Clear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btn_Clear_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="valsumprivate" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="LandInfo1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="StatuslnkITML" runat="server" Style="text-decoration: blink; color: Red;
                            font-family: Arial; font-size: 18px; font-weight: bold" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <%-- </contenttemplate>
        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlLandTenure" EventName="SelectedIndexChanged" />
                        </triggers>
    </asp:UpdatePanel>--%>
     </div>
    </div>
    <script type="text/javascript" language="javascript">
        function CheckYear() {
            var msg;
            msg = document.getElementById('<%=txtwhenbeginfarm.ClientID %>').value;
            var stringlenght = msg.length;
            var d = new Date();
            var n = d.getFullYear();
            if (stringlenght < 4) {
                alert("Enter 4 Digits for the year");
                document.getElementById('<%=txtwhenbeginfarm.ClientID %>').value = '';
                return;
            }
            if (msg > n) {
                alert("Entered Year is not in the future");
                document.getElementById('<%=txtwhenbeginfarm.ClientID %>').value = '';
                return;
            }
        }

        function CheckYearForAll(src) {
            var msg;
            msg = src.value;
            var stringlenght = msg.length;
            var d = new Date();
            var n = d.getFullYear();
            if (stringlenght < 4) {
                alert("Enter 4 Digits for the year");
                src.value = '';
                return;
            }
            if (msg > n) {
                alert("Entered Year is not in the future");
                src.value = '';
                return;
            }
        }

        function ShowHideSection(src) {
            document.getElementById('dvPublic').style.display = 'none';
            document.getElementById('dvMailo').style.display = 'none';
            if (src.selectedIndex == 3) {
                document.getElementById('dvMailo').style.display = '';
            }
            else {
                document.getElementById('dvPublic').style.display = '';
            }
        }


        function EnableDisableOtherOccupantStatus(src) {
            occupantStatus = src.options[src.selectedIndex].text;

            if (occupantStatus == 'Other' || occupantStatus == 'Individual') {
                document.getElementById('txtfromwhom').disabled = '';
                document.getElementById('txtfromwhom').focus();
            }
            else {
                document.getElementById('txtfromwhom').disabled = 'disabled';
                document.getElementById('txtfromwhom').value = '';
            }
        }

        function chkLivedSinceBirth_onclick(src) {
            if (src.checked) {
                document.getElementById('<%=txtyearmoved.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtwherebefore.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtyearmoved.ClientID%>').value = '';
                document.getElementById('<%=txtwherebefore.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=txtyearmoved.ClientID%>').disabled = '';
                document.getElementById('<%=txtwherebefore.ClientID%>').disabled = '';
            }
        }

        function chkMortgage_onclick(src) {
            if (src.checked) {
                document.getElementById('<%=txtmortagedetails.ClientID%>').disabled = '';
            }
            else {
                document.getElementById('<%=txtmortagedetails.ClientID%>').disabled = 'disabled';
                document.getElementById('<%=txtmortagedetails.ClientID%>').value = '';
            }
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
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
            var btn = document.getElementById("<%= btn_SavePrivate.ClientID  %>");
            var tat1 = document.getElementById("<%= txtlandlord.ClientID  %>");
            var tat2 = document.getElementById("<%= txtwhenbeginfarm.ClientID  %>");
            var tat3 = document.getElementById("<%= txtagrrement.ClientID  %>");
            var tat4 = document.getElementById("<%= txtwhoclaims.ClientID  %>");
            var tat5 = document.getElementById("<%= txtwheredidfarm.ClientID  %>");
            var tat6 = document.getElementById("<%= txtprodutive.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == ''
                && tat3.value.toString().replace(/^\s+/, '') == '' && tat4.value.toString().replace(/^\s+/, '') == ''
                 && tat5.value.toString().replace(/^\s+/, '') == '' && tat6.value.toString().replace(/^\s+/, '') == ''
                 && btn.value.toString() == 'Save') {
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
