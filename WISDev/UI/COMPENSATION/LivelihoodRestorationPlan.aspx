<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="LivelihoodRestorationPlan.aspx.cs" Inherits="WIS.LivelihoodRestorationPlan" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DatePickerControl" Namespace="DatePickerControl" TagPrefix="cc1" %>
<%@ Register Src="~/UI/COMPENSATION/SOCIOECONOMIC/CompSocioEconomyMenu.ascx" TagName="CompSocioEconomyMenu"
    TagPrefix="uc1" %>
<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
    <script language="javascript">
        function OpenUploadDocumnetlist(ProjectID, HHID, userID, ProjectCode, DocumentCode) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 700) / 4;
            open('../UploadDocumentList.aspx?ProjectID=' + ProjectID + '&HHID=' + HHID + '&UserID=' + userID + '&ProjectCode=' + ProjectCode + '&DOCUMENT_CODE=' + DocumentCode, 'UploadDocPoplist', 'width=800px,height=700px,top=' + top + ', left=' + left);
        }

        function OpenLivelihoodRestoration(HHID) {
            var left = (screen.width - 800) / 2;
            var top = (screen.height - 700) / 4;
            open('LivelihoodRestorationView.aspx?hhID=' + HHID, 'ViewList', 'maximize=1,width=800px,height=700px,top=' + top + ', left=' + left);
        }      
    </script>
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
    <uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />
    <fieldset class="icePnlinner">
        <legend>Livelihood Restoration - Plan</legend>
        <table border="0" cellpadding="4" width="100%">
            <tr>
                <td align="left" style="width: 150px">
                    <label class="iceLable">
                        Compensation Status:
                    </label>
                </td>
                <td align="left">
                    <asp:TextBox ID="dvCompensationStatus" runat="server" Text="" CssClass="iceTextBox"
                        Width="150px" Style="text-align: left; color: Black; background-color: #DBDBDB;
                        font-weight: bolder" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="left" style="width: 186px">
                    <a href="#" onclick="OpenLivelihoodRestoration(<%=Session["HH_ID"]%>)">Track Previous
                        Income Activity</a>&nbsp;|&nbsp;
                </td>
                <td align="left" style="width: 140px">
                    <a id="lnkUploadDoc" href="#" runat="server">View Conversation Log</a>
                </td>
            </tr>
        </table>
        <hr class="icehrtagformMiddle" />
        <table border="0" cellpadding="3" cellspacing="2" width="100%">
            <tr>
                <td align="left" style="width: 30%;">
                    <div style="float: left;">
                        <label class="iceLable">
                            District</label></div>
                    &nbsp;
                    <asp:TextBox ID="dvDistrict" runat="server" Text="" CssClass="iceTextBox" Style="text-align: left;
                        color: Black; background-color: #DBDBDB; width: auto;" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 30%;">
                    <div style="float: left;">
                        <label class="iceLable">
                            County</label></div>
                    &nbsp;
                    <asp:TextBox ID="dvCounty" runat="server" Text="" CssClass="iceTextBox" Style="text-align: left;
                        color: Black; background-color: #DBDBDB; width: auto;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div style="float: left;">
                        <label class="iceLable">
                            Sub County</label></div>
                    &nbsp;
                    <asp:TextBox ID="dvSubCounty" runat="server" Text="" CssClass="iceTextBox" Style="text-align: left;
                        color: Black; background-color: #DBDBDB; width: auto;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left;">
                        <label class="iceLable">
                            Parish</label></div>
                    &nbsp;
                    <asp:TextBox ID="dvParish" runat="server" Text="" CssClass="iceTextBox" Style="text-align: left;
                        color: Black; background-color: #DBDBDB; width: auto;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <div style="float: left;">
                        <label class="iceLable">
                            Village</label></div>
                    &nbsp;
                    <asp:TextBox ID="dvVillage" runat="server" Text="" CssClass="iceTextBox" Style="text-align: left;
                        color: Black; background-color: #DBDBDB; width: auto;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        <fieldset class="icePnlinner">
            <legend>New Location</legend>
            <table cellpadding="3" width="100%" border="0">
                <tr>
                    <td align="left" style="width: 15%">
                        <div>
                            <label class="iceLable">
                                District</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left" class="iceNormalText" style="width: 30%">
                        <asp:DropDownList ID="ddlDistrict" runat="server" Width="201px" AutoPostBack="true"
                            CssClass="iceDropDown" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender5" runat="server" TargetControlID="ddlDistrict"
                            PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                            IsSorted="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDistrict"
                            ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a District"
                            Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left" style="width: 23%">
                        <div>
                            <label class="iceLable">
                                County</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCounty" runat="server" Width="201px" AutoPostBack="true"
                                    CssClass="iceDropDown" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="ddlCounty"
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
                                Sub County</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSubCounty" runat="server" Width="201px" AutoPostBack="true"
                                    CssClass="iceDropDown" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlSubCounty"
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
                                Parish</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlParish" runat="server" Width="201px" AutoPostBack="true"
                                    CssClass="iceDropDown" OnSelectedIndexChanged="ddlParish_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server" TargetControlID="ddlParish"
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
                                Village</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left" class="iceNormalText">
                        <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlVillage" runat="server" Width="201px" AutoPostBack="true"
                                    CssClass="iceDropDown" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4" runat="server" TargetControlID="ddlVillage"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvVillage" runat="server" ControlToValidate="ddlVillage"
                                    ValidationGroup="vgNewLocation" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Sub Village"
                                    Display="None"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left">
                        <div>
                            <label class="iceLable">
                                Distance from Old Location</label><span class="mandatory"> *</span></div>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDistanceFromOldLocation" runat="server" CssClass="iceTextBox"
                            MaxLength="8"></asp:TextBox>&nbsp;<label class="labelSuffix"><b>KM</b></label>
                        <asp:RequiredFieldValidator ID="rfvDistanceFromOldLocation" runat="server" ErrorMessage="Enter Distance From Old Location "
                            ValidationGroup="vgNewLocation" ControlToValidate="txtDistanceFromOldLocation"
                            Display="None"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCostPerUnit" FilterType="Numbers" ValidChars=""
                            TargetControlID="txtDistanceFromOldLocation" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <label class="iceLable">
                                Settlement Date</label><%--<span class="mandatory"> *</span>--%></div>
                    </td>
                    <td>
                        <asp:TextBox ID="dpcSettlementDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="caldpcSettlementDate" CssClass="WISCalendarStyle"
                            runat="server" TargetControlID="dpcSettlementDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="dpcSettlementDate"
                            ClientValidationFunction="CheckDOB" ErrorMessage="Settlement Date should not be greater than Today's Date"
                            ValidationGroup="vgNewLocation" Display="None">
                        </asp:CustomValidator>
                         <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="dpcSettlementDate"
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
                        <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="vgNewLocation"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" OnClick="btnClear_Click" />
                        <asp:ValidationSummary ID="valNewLocation" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgNewLocation" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <%-- <asp:UpdatePanel ID="uplRestorationPlan" runat="server">
            <ContentTemplate>--%>
        <asp:Panel ID="pnlRestorationPlan" runat="server">
            <asp:Panel ID="pnlPlanned" runat="server">
                <fieldset class="icePnlinner">
                    <legend>Planned</legend>

                    <table border="0" style="width: 100%" cellpadding="3" cellspacing="0">
                        <tr>
                            <td width="15%">
                                <label class="iceLable">Item</label>
                            </td>
                            <td width="35%">
                                <asp:DropDownList ID="ddlRestoreItems" runat="server" CssClass="iceDropDown" AutoPostBack="false"
                                    Width="90px">
                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender6" runat="server" TargetControlID="ddlRestoreItems"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvRestoreItems" runat="server" ErrorMessage="Select Restore Item"
                                    ValidationGroup="vgRestorePlan" ControlToValidate="ddlRestoreItems" InitialValue="0"
                                    Display="None"></asp:RequiredFieldValidator>
                            </td>
                            <td width="15%">
                                <label class="iceLable">Unit</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnits" runat="server" CssClass="iceDropDown" AutoPostBack="false"
                                    Width="90px">
                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender7" runat="server" TargetControlID="ddlUnits"
                                    PromptText="Type to search" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Top"
                                    IsSorted="true" />
                                <asp:RequiredFieldValidator ID="rfvEUnits" runat="server" ErrorMessage="Select Unit"
                                    ValidationGroup="vgRestorePlan" ControlToValidate="ddlUnits" InitialValue="0"
                                    Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><label class="iceLable">Planned</label></td>
                            <td>
                                <asp:TextBox ID="txtPlanned" runat="server" CssClass="iceTextBox" Width="80px" ValidationGroup="vgRestorePlan"
                                    MaxLength="14" onblur="CalcBalance();"></asp:TextBox><%--onblur="CalcBalance();"--%>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbePlanned" FilterType="Numbers,Custom"
                                    ValidChars="," TargetControlID="txtPlanned" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvPlanned" runat="server" ValidationGroup="vgRestorePlan"
                                    Display="None" ControlToValidate="txtPlanned" ErrorMessage="Enter Planned Value"></asp:RequiredFieldValidator>
                            </td>
                            <td><label class="iceLable">Cost per Unit</label></td>
                            <td>
                                <asp:TextBox ID="txtCostPerUnit" runat="server" Width="60px" MaxLength="14" onblur="calcCostPerUnit_TotalAmount();"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCostUnit" FilterType="Numbers,Custom"
                                    ValidChars="," TargetControlID="txtCostPerUnit" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td><label class="iceLable">Total Amount</label></td>
                            <td>
                                <asp:TextBox ID="lblTotalAmount_Plan" runat="server" CssClass="iceTextBox" Width="80px"
                                    Style="text-align: left; color: Black; background-color: #DBDBDB;" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td><label class="iceLable">Received</label></td>
                            <td>
                                <asp:TextBox ID="txtRecieved" runat="server" CssClass="iceTextBox" Width="80px" ReadOnly="true"
                                    onKeyDown="doCheck()" MaxLength="100" Style="text-align: left; color: Black;
                                    background-color: #DBDBDB;"></asp:TextBox><%--onblur="CalcBalance();"--%>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeReceived" FilterType="Numbers,Custom"
                                    ValidChars="," TargetControlID="txtRecieved" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td><label class="iceLable">Balance</label></td>
                            <td>
                                <asp:TextBox ID="txtBalance" runat="server" CssClass="iceTextBox" Width="80px" Style="text-align: left;
                                    color: Black; background-color: #DBDBDB;" ReadOnly="true" onKeyDown="doCheck()"></asp:TextBox>
                            </td>
                            <td><label class="iceLable">Planned Date</label></td>
                            <td>
                                <asp:TextBox ID="dpcDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caldpcDate" CssClass="WISCalendarStyle" runat="server"
                                    TargetControlID="dpcDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="dpcDate"
                                    ClientValidationFunction="CheckDOBdpcRecDate" ErrorMessage="Planned Date should not be greater than Today's Date"
                                    ValidationGroup="vgRestorePlan" Display="None">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnSavePlanned" runat="server" CssClass="icebutton" Text="Save" ValidationGroup="vgRestorePlan" OnClick="btnSavePlanned_Click" />
                                <asp:Button ID="btnClearPlanned" runat="server" CssClass="icebutton" 
                                    Text="Clear" onclick="btnClearPlanned_Click" />
                                <asp:ValidationSummary ID="valRestPlan" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgRestorePlan" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnTotalPlanned" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnTotalPlannedAmt" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnBalanceAmount" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnEdit" runat="server" Value="0" />
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" class="gridHeaderStyle"
                                    style="height: 25px; font-weight: bolder; display: none;">
                                    <tr>
                                        <td style="width: 5%" align="center">
                                            Sl No.
                                        </td>
                                        <td style="width: 13%" align="center">
                                            Item
                                        </td>
                                        <td style="width: 15%" align="center">
                                            Unit
                                        </td>
                                        <td style="width: 14%" align="center">
                                            Planned
                                        </td>
                                        <td style="width: 10%" align="center">
                                            Cost per
                                            <br />
                                            Unit
                                        </td>
                                        <td style="width: 10%" align="center">
                                            Total<br />
                                            Amount
                                        </td>
                                        <td style="width: 14%" align="center">
                                            Received
                                        </td>
                                        <td style="width: 14%" align="center">
                                            Balance
                                        </td>
                                        <td style="width: 15%" align="center">
                                            Planned Date
                                        </td>
                                        <td style="width: 5%" align="center">
                                            Edit
                                        </td>
                                        <td style="width: 5%" align="center">
                                            Delete
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="grdRestorationPlan" runat="server" AllowSorting="True" CellPadding="4"
                                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdRestorationPlan_RowCommand"
                                    OnRowDataBound="grdRestorationPlan_RowDataBound" OnRowCreated="grdRestorationPlan_RowCreated"
                                    ShowFooter="false" ShowHeader="true">
                                    <HeaderStyle CssClass="gridHeaderStyle" />
                                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                                    <FooterStyle CssClass="gridFooterStyle" />
                                    <RowStyle CssClass="gridRowStyle" />
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Liv Rest PlanID" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLiv_Rest_PlanID" runat="server" Text='<%#Eval("Liv_Rest_PlanID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" Visible="true">
                                            <ItemStyle HorizontalAlign="Left" Width="13%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRestItemName" runat="server" Text='<%#Eval("ItemName") %>'
                                                    CommandArgument='<%#Eval("Liv_Rest_PlanID") %>' CommandName="ClickItemName"></asp:LinkButton>
                                                <asp:Label ID="lblRestItemName" runat="server" Text='<%#Eval("ItemName") %>' Visible="false"></asp:Label>
                                                <%-- <asp:Literal ID="ltrItemId" runat="server" Visible="false" Text=""></asp:Literal>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitName" runat="server" Text='<%#Eval("UnitName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Planned" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="14%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanned" runat="server" Text='<%#Eval("Planned") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Per Unit" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCostPerUnit" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label><%--Text='<%#Eval("Planned") %>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalAmount_Grid" runat="server" Text=""></asp:Label><%--Text='<%#Eval("Planned") %>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="14%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecieved" runat="server" Text='<%#Eval("Received", "{0:N0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="14%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Planned Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text=''></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                                    CommandName="EditRow" CommandArgument='<%#Eval("Liv_Rest_PlanID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                    CommandName="DeleteRow" CommandArgument='<%#Eval("Liv_Rest_PlanID") %>' OnClientClick="return DeleteRecord();" />
                                                <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("Liv_Rest_PlanID") %>'
                                                    Visible="false"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlReceived" runat="server" Visible="false">
                <fieldset class="icePnlinner">
                    <legend>
                        <asp:Label ID="lblTReceived" runat="server" Visible="true" Text="Received"></asp:Label></legend>
                    <table cellpadding="0" cellspacing="0" border="0" width="70%">
                        <tr>
                            <td>
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" class="gridHeaderStyle"
                                    style="height: 25px; font-weight: bolder;">
                                    <tr>
                                        <td style="width: 8%" align="center">
                                            Sl No.
                                        </td>
                                        <td style="width: 20%" align="center">
                                            Recieved
                                        </td>
                                        <td style="width: 20%; display: none;" align="center">
                                            Balance
                                        </td>
                                        <td style="width: 20%" align="center">
                                            Received Date
                                        </td>
                                        <td style="width: 18%" align="center">
                                            Edit
                                        </td>
                                        <td style="width: 15%" align="center">
                                            Delete
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnReceivedAmount" ClientIDMode="Static" runat="server" Value="0" />
                                <asp:GridView ID="grdItemsReceived" runat="server" AllowSorting="True" CellPadding="4"
                                    CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdItemsReceived_RowCommand"
                                    OnRowDataBound="grdItemsReceived_RowDataBound" OnRowCreated="grdItemsReceived_RowCreated"
                                    ShowFooter="false" ShowHeader="false">
                                    <HeaderStyle CssClass="gridHeaderStyle" />
                                    <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                                    <FooterStyle CssClass="gridFooterStyle" />
                                    <RowStyle CssClass="gridRowStyle" />
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LIV_REST_RECDID" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLIV_REST_RECDID" runat="server" Text='<%#Eval("LIV_REST_RECDID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recieved" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecieved" runat="server" Text='<%#Eval("Received") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("ReceivedDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="18%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                                    CommandName="EditRow" CommandArgument='<%#Eval("LIV_REST_RECDID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgObsolete" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                                    CommandName="DeleteRow" CommandArgument='<%#Eval("LIV_REST_RECDID") %>' OnClientClick="return DeleteRecord();" />
                                                <asp:Literal ID="ltlObsolete" runat="server" Text='<%#Eval("LIV_REST_RECDID") %>'
                                                    Visible="false"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" style="height: 25px;
                                    font-weight: bolder; background: white;" id="tabReceived" runat="server">
                                    <tr>
                                        <td style="width: 8%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtRecReceived" runat="server" CssClass="iceTextBox" Width="80px"
                                                MaxLength="100" onblur="return ValidateAmount1();"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers,Custom"
                                                ValidChars="," TargetControlID="txtRecReceived" runat="server">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvRecieved" runat="server" ValidationGroup="vgrReceivedPlan"
                                                Display="None" ControlToValidate="txtRecReceived" ErrorMessage="Enter Recieved Value"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 20%; display: none;">
                                            <asp:TextBox ID="txtRecBalance" runat="server" CssClass="iceTextBox" Width="80px"
                                                Style="text-align: left; color: Black; background-color: #DBDBDB;" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="dpcRecDate" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="caldpcRecDate" runat="server" CssClass="WISCalendarStyle"
                                                TargetControlID="dpcRecDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vgrReceivedPlan"
                                                Display="None" ControlToValidate="dpcRecDate" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="dpcRecDate"
                                                ClientValidationFunction="CheckDate" ErrorMessage="Received Date should not be less than Planned Date."
                                                ValidationGroup="vgrReceivedPlan" Display="None">
                                            </asp:CustomValidator>
                                            <asp:HiddenField ID="hfPlanedDate" runat="server" Value="0" />
                                        </td>
                                        <td colspan="2" style="width: 18%">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddItemDetail" runat="server" Text="Add" ValidationGroup="vgrReceivedPlan"
                                                            Width="80px" CssClass="icebutton" OnClick="btnAddItemDetail_Click" Style="width: 100%"
                                                            OnClientClick="return ValidateAmount();" /><%--OnClientClick="return ValidateAmount();"--%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCancelItemDetail" runat="server" Text="Cancel" Style="width: 100%"
                                                            CssClass="icebutton" Visible="false" OnClick="btnCancelItemDetail_Click" CommandName="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:ValidationSummary ID="valReceived" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgrReceivedPlan" runat="server" />
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <center class="iceLable">
                        Balance Amount:
                        <asp:Label ID="lblBalanceAmountBtm" runat="server"></asp:Label>
                    </center>
                </fieldset>
            </asp:Panel>
        </asp:Panel>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </fieldset>
    <script language="javascript" type="text/javascript">
        //  PreventDateFieldEntry(document.getElementById('MainContent_grdRestorationPlan_dpcDate'));
        //        PreventDateFieldEntry(document.getElementById('<%=dpcDate.ClientID%>'));
        function DeleteRecord() {
            return confirm('Are you sure you want to delete this record?');
        }


        function CheckDate(oSrc, args) {
            dtPlaned = document.getElementById('<%=hfPlanedDate.ClientID %>').value;
            dtMeeting = GetCalDate('<%=dpcRecDate.ClientID%>');

            var ArrProjSt = dtPlaned.split("-");
            var ProjStartDate = ArrProjSt[0];
            var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
            var ProjStartYear = ArrProjSt[2];

            var ArrMeetingDt = dtMeeting.split("-");
            var MeetingDt = ArrMeetingDt[0];
            var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
            var MeetingYear = ArrMeetingDt[2];

            if (ProjStartYear > MeetingYear) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == MeetingYear) && (ProjStartMonth > MeetingMonth)) {
                args.IsValid = false;
                return;
            }
            else if ((ProjStartYear == MeetingYear) && (ProjStartMonth == MeetingMonth) && (ProjStartDate > MeetingDt)) {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }


        function CalcBalance() {

            //  gridView = document.getElementById('<%=grdRestorationPlan.ClientID %>');

            var Planned = RemoveComma(document.getElementById('<%=txtPlanned.ClientID %>').value);
            var Received = RemoveComma(document.getElementById('<%=txtRecieved.ClientID%>').value);

            if (Planned == '') {
                Planned = 0;
            }
            if (!isNaN(Planned))
                Planned = Planned;
            else
                Planned = 0;
            if (Received == '') {
                Received = 0;
            }
            if (!isNaN(Received))
                Received = Received;
            else
                Received = 0;
            var Balance = parseFloat(Planned) - parseFloat(Received);

            document.getElementById('<%=txtPlanned.ClientID %>').value = document.getElementById('<%=txtPlanned.ClientID %>').value.replace(/,/g, '');
            document.getElementById('<%=txtBalance.ClientID%>').value = Balance;
        }

        function ValidateAmount1() {
            var TotalPlanned = document.getElementById('<%=hdnTotalPlannedAmt.ClientID%>').value;

            var NewReceived = document.getElementById('<%=txtRecReceived.ClientID%>').value;
            var TotalReceived = document.getElementById('<%=hdnReceivedAmount.ClientID%>').value;

            // Val For Amount
            if (NewReceived.toString().length == 0) {
                alert('Enter Recieved Value');
                return false;
            }
            //    var BalanceAmount = document.getElementById('<%=hdnBalanceAmount.ClientID%>').value;

            // alert('New Received Amount =' + NewReceived + '+ Total Received Amount  =' + TotalReceived + ';== Planned Amount =' + TotalPlanned);
            if (parseFloat(RemoveComma(NewReceived)) + parseFloat(RemoveComma(TotalReceived)) > parseFloat(RemoveComma(TotalPlanned))) {
                alert('Received Amount is more than the Planned Amount');
                return false;
            }
            return true;
        }

        function ValidateAmount() {

            var TotalPlanned = document.getElementById('<%=hdnTotalPlannedAmt.ClientID%>').value;

            var NewReceived = document.getElementById('<%=txtRecReceived.ClientID%>').value;
            var TotalReceived = document.getElementById('<%=hdnReceivedAmount.ClientID%>').value;

            // Val For Amount
            if (NewReceived.toString().length == 0) {
                alert('Enter Recieved Value');
                return false;
            }
            dtProjectStart = GetCalDate('<%=dpcRecDate.ClientID%>');
            if (dtProjectStart.toString().length == 0) {
                alert('Select Date');
                return false;
            }
            else {
                dtPlaned = document.getElementById('<%=hfPlanedDate.ClientID %>').value;
                dtMeeting = GetCalDate('<%=dpcRecDate.ClientID%>');

                var ArrProjSt = dtPlaned.split("-");
                var ProjStartDate = ArrProjSt[0];
                var ProjStartMonth = GetMonthNumber(ArrProjSt[1]);
                var ProjStartYear = ArrProjSt[2];

                var ArrMeetingDt = dtMeeting.split("-");
                var MeetingDt = ArrMeetingDt[0];
                var MeetingMonth = GetMonthNumber(ArrMeetingDt[1]);
                var MeetingYear = ArrMeetingDt[2];

                if (ProjStartYear > MeetingYear) {
                    alert('Received Date cannot be less than Planned Date.');
                    return false;
                }
                else if ((ProjStartYear == MeetingYear) && (ProjStartMonth > MeetingMonth)) {
                    alert('Received Date cannot be less than Planned Date.');
                    return false;
                }
                else if ((ProjStartYear == MeetingYear) && (ProjStartMonth == MeetingMonth) && (ProjStartDate > MeetingDt)) {
                    alert('Received Date cannot be less than Planned Date.');
                    return false;
                }
            }
            //    var BalanceAmount = document.getElementById('<%=hdnBalanceAmount.ClientID%>').value;

            // alert('New Received Amount =' + NewReceived + '+ Total Received Amount  =' + TotalReceived + ';== Planned Amount =' + TotalPlanned);
            if (parseFloat(RemoveComma(NewReceived)) + parseFloat(RemoveComma(TotalReceived)) > parseFloat(RemoveComma(TotalPlanned))) {
                alert('Received Amount is more than the Planned Amount');
                return false;
            }
            return true;
        }
        function calcCostPerUnit_TotalAmount() {

            var Planned =  RemoveComma(document.getElementById('<%=txtPlanned.ClientID %>').value);
            var CostPerUnit = document.getElementById('<%=txtCostPerUnit.ClientID%>').value.replace(/,/g, '');


            if (Planned == '') {
                Planned = 0;
            }
            if (!isNaN(Planned))
                Planned = Planned;
            else
                Planned = 0;

            if (CostPerUnit == '') {
                CostPerUnit = 0;
                document.getElementById('<%=lblTotalAmount_Plan.ClientID%>').value = '';
            }
            else {
                //            alert('hi' + Planned + '---' + CostPerUnit);
                var TotalAmount = parseFloat(RemoveComma(Planned)) * parseFloat(RemoveComma(CostPerUnit));
                //            alert(TotalAmount);
                document.getElementById('<%=lblTotalAmount_Plan.ClientID%>').value = AddComma(TotalAmount);
            }
            document.getElementById('<%=txtCostPerUnit.ClientID%>').value = document.getElementById('<%=txtCostPerUnit.ClientID%>').value.replace(/,/g, '');
        }

        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }
        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
    </script>
    <script type="text/javascript">
        function doCheck() {
            var keyCode = (event.which) ? event.which : event.keyCode;
            if ((keyCode == 8) || (keyCode == 46))
                event.returnValue = false;
        }
        function CheckDOB(oSrc, args) {
            var now = new Date();
            dtMeeting = GetCalDate('<%=dpcSettlementDate.ClientID%>');

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
            dtConstrStart = GetCalDate('<%=dpcSettlementDate.ClientID%>');

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

        function CheckDOBdpcRecDate(oSrc, args) {
            var now = new Date();
            dtMeeting = GetCalDate('<%=dpcRecDate.ClientID%>');

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
    </script>
</asp:Content>
