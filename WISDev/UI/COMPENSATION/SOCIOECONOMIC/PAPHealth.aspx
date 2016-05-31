<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PAPHealth.aspx.cs" Inherits="WIS.PAPHealth" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/UI/COMPENSATION/ViewMasterCopy.ascx" TagName="ViewMasterCopy" TagPrefix="uc2" %>
<%@ Register Src="CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu" TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListSearchExtenderPrompt
        {
            display:none;
            background-color: transparent;
            visibility: hidden;
        }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc1:CompSocioEconomyMenu ID="CompSocioEconomyMenu1" runat="server" />
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />

   
     <fieldset class="icePnlinner">
        <legend>Health Care</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy1" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="100%">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Nearest Health Centre</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:DropDownList ID="ddlNearestHealthCentre" runat="server" CssClass="iceTextBox"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlNearestHealthCentre"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top" IsSorted="true" />
                </td>
                <td align="left">
                    <label class="iceLable">
                        Distance to Health Care Centre</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDistanceToHealthCentre" CssClass="iceTextBox" MaxLength="5" Width="50px"
                        runat="server">
                    </asp:TextBox>&nbsp;<span class="labelSuffix"><b>(KM)</b></span>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers" TargetControlID="txtDistanceToHealthCentre"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Is it actually used by the family?</label>
                </td>
                <td align="left" style="vertical-align: top">
                    <div>
                        <asp:CheckBox ID="chkUsedByFamily" Checked="false" runat="server" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                </td>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        If not, why?</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtNonUseReason" Enabled="true" ClientIDMode="Static" CssClass="iceTextBox"
                        TextMode="MultiLine" Rows="3" MaxLength="1000" Width="220px" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                       Number of births (in the last one year) </label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtNoOfBirth" CssClass="iceTextBox" MaxLength="3" Width="50px" runat="server">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteBirth" FilterType="Numbers" TargetControlID="txtNoOfBirth"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Number of deaths (in the last one year) </label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtNoOfDeath" CssClass="iceTextBox" MaxLength="3" Width="50px" runat="server">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDeath" FilterType="Numbers" TargetControlID="txtNoOfDeath"
                        runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Reason for Death</label>
                </td>
                <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:TextBox ID="txtReasonForDeath" CssClass="iceTextBox" TextMode="MultiLine" Rows="3"
                        MaxLength="1000" Width="220px" runat="server">
                    </asp:TextBox>
                </td>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        Common Diseases</label>
                </td>
                <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:Panel ID="Panel1" runat="server" CssClass="icePnlinner" BackColor="White" Height="100px"
                        ScrollBars="Auto">
                        <asp:CheckBoxList ID="chklstCommonDiseases" Width="100%" BackColor="White" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Practice family planning?</label>
                </td>
                <td align="left" class="iceNormalText">
                    <div>
                        <asp:CheckBox ID="chkFamilyPlanning" runat="server" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                </td>
                <td align="left">
                    <label class="iceLable">
                        Heard of HIV/AIDS?</label>
                </td>
                <td align="left" class="iceNormalText">
                    <div>
                        <asp:CheckBox ID="chkHeardOfAIDS" runat="server" />
                        &nbsp;<label class="labelSuffix">(Check if YES)</label></div>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        How is HIV/AIDS contracted</label>
                </td>
                  <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:Panel ID="Panel2" runat="server" CssClass="icePnlinner" BackColor="White" Height="100px"
                        ScrollBars="Auto">
                        <asp:CheckBoxList ID="chklsthivcontracted" Width="100%" BackColor="White" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
              <%--  <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtHowContracted" CssClass="iceTextBox" TextMode="MultiLine" Rows="3"
                        MaxLength="1000" Width="220px" runat="server">
                    </asp:TextBox>
                </td>--%>
                <td align="left" style="vertical-align: top">
                    <label class="iceLable">
                        How can HIV/AIDS be avoided?</label>
                </td>
                <td align="left" class="iceNormalText" style="vertical-align: top">
                    <asp:TextBox ID="txtHowAvoided" CssClass="iceTextBox" TextMode="MultiLine" Rows="5"
                        MaxLength="1000" Width="220px" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="padding-top: 10px">
                    <asp:Button ID="lnkPAPHealthInfo" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSaveHealthInfo" Text="Save" runat="server" class="icebutton" OnClick="btnSaveHealthInfo_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearHealthInfo" runat="server" Text="Clear" class="icebutton"
                        OnClick="btnClearHealthInfo_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="StatusPAPHealthInfo" runat="server" Style="text-decoration: blink;
                        color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
    </fieldset>
   
      <fieldset class="icePnlinner">
        <legend>Disability Details</legend>
    <div style="width: 100%; height: 25px; float: right" >
        <table width="100%"><tr><td>&nbsp;&nbsp;</td><td align="right" style="width:180px">
        <uc2:ViewMasterCopy ID="ViewMasterCopy2" runat="server" /></td></tr></table>
    </div>
        <table align="center" border="0" cellpadding="3" cellspacing="1" width="96%">
            <tr>
                <td align="left" style="width: 10%">
                    <label class="iceLable">
                        Disability</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" style="width: 30%">
                    <asp:DropDownList ID="ddlDisability" CssClass="iceDropDown" AppendDataBoundItems="true"
                        runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlDisability"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                    <asp:RequiredFieldValidator ID="reqDisability" ControlToValidate="ddlDisability"
                        InitialValue="0" ErrorMessage="Select Disability" Display="None" ValidationGroup="Disab"
                        runat="server">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="left" style="width: 15%">
                    <label class="iceLable">
                        Health Care Needed</label>
                </td>
                <td align="left" class="iceNormalText">
                    <asp:TextBox ID="txtHealthCareNeeded" CssClass="iceTextBox" MaxLength="200" runat="server"
                        Width="194px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" style="padding-top:12px">
                    <asp:Button ID="lnkPAPHealthDisability" runat="server" Text="Change Request" CssClass="icebutton"
                        Width="120px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSaveDisability" CssClass="icebutton" Text="Save" runat="server"
                        ValidationGroup="Disab" OnClick="btnSaveDisability_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearDisability" CssClass="icebutton" Text="Clear" runat="server"
                        OnClick="btnClearDisability_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="StatusPAPHealthDisability" runat="server" Style="text-decoration: blink;
                        color: Red; font-family: Arial; font-size: 18px; font-weight: bold" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSummaryDisab" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Disab" runat="server" />
            <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
        <asp:GridView ID="grdDisabilities" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" PageSize="10"
            AllowPaging="True" OnPageIndexChanging="grdDisabilities_PageIndexChanging" OnRowCommand="grdDisabilities_RowCommand">
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
                <asp:BoundField DataField="DisabilityName" HeaderText="Disability" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="HealthCareNeeded" HeaderText="Health Care Needed" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("PAPDisabilityID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center" Visible="false">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("PAPDisabilityID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litDisabilityID" Text='<%#Eval("PAPDisabilityID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </asp:Panel>
    </fieldset>

   
   
  
  
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 140).toString() + "px";
        }

        function EnableBuried(src, field1) {
            field1 = document.getElementById(field1);

            if (src.checked) {
                field1.disabled = true;
                field1.value = '';
            }
            else {
                field1.disabled = false;
                field1.focus();
            }
        }

        function OpenChangeRequest(ChangeRequestCode, ProjectID, userID, HHID, pageCode) {
            var left = (screen.width - 600) / 2;
            var top = (screen.height - 500) / 4;
            open('../../EmailPopUpwindow.aspx?ChangeRequestCode=' + ChangeRequestCode + '&ProjectID=' + ProjectID + '&userID=' + userID + '&HHID=' + HHID + '&pageCode=' + pageCode, 'ChangeRequest', 'width=600px,height=500px,top=' + top + ', left=' + left);
        }
    </script>
</asp:Content>
