<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" UICulture="en" Culture="en-US"
    CodeBehind="LRP_Budget.aspx.cs" Inherits="WIS.LRP_Budget" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%--<%@ Register Src="~/UI/COMPENSATION/HouseholdSummary.ascx" TagName="HouseholdSummary" TagPrefix="uc2" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
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
    <%--<uc2:HouseholdSummary ID="HouseholdSummary1" runat="server" />--%>
    <ajaxToolkit:ToolkitScriptManager ID="tkManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
      <div>
    <fieldset class="icePnlinner">
        <legend>Item Details</legend>
        <table border="0" cellpadding="4" width="100%">
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Category</label><span class="mandatory"> *</span>
                </td>
                <td align="left" style="width: 90%">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="201px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                      <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="ddlCategory"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                        ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Category"
                        Display="None"></asp:RequiredFieldValidator>
                </td>
                <%-- <td>
                    
                </td>--%>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <div>
                        <label class="labelSuffix">
                            (Check if YES)</label>
                        <asp:CheckBoxList ID="chklst" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Implementation Cost"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Operational Cost & Internal Monitory"></asp:ListItem>
                            <asp:ListItem Value="3" Text="External Monitory"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Item
                    </label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlItem" runat="server" Width="201px" AutoPostBack="true" CssClass="iceDropDown"
                        OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                        <%-- <asp:ListItem Value="1">Energy Saving Stoves</asp:ListItem>
                        <asp:ListItem Value="2">Work tools From equipment</asp:ListItem>--%>
                    </asp:DropDownList>
                      <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlItem"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="ddlItem"
                        ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Item"
                        Display="None"></asp:RequiredFieldValidator>
                </td>
                <%-- <td>
                </td>--%>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Item Description</label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtItemDescription" runat="server" Rows="2" CssClass="iceTextBox"
                        TextMode="MultiLine" Enabled="false" Style="width: 500px;" MaxLength="1000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        No of beneficial</label><span class="mandatory"> *</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtNoOfBeneficial" runat="server" CssClass="iceTextBox" MaxLength="14"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeNoOfBeneficial" FilterType="Numbers"
                        ValidChars="" TargetControlID="txtNoOfBeneficial" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="vgBudget"
                        Display="None" ControlToValidate="txtNoOfBeneficial" ErrorMessage="Enter No of beneficial"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Item Quantity</label><span class="mandatory"> *</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtItemQuantity" runat="server" CssClass="iceTextBox" MaxLength="14"
                        onblur="CalcTotal();"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeItemQuantity" FilterType="Numbers" ValidChars=""
                        TargetControlID="txtItemQuantity" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvItemQuantity" runat="server" ValidationGroup="vgBudget"
                        Display="None" ControlToValidate="txtItemQuantity" ErrorMessage="Enter Quantity"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Cost Per Unit</label><span class="mandatory"> *</span>
                </td>
                <td>
                    <asp:TextBox ID="txtCostPerUnit" runat="server" CssClass="iceTextBox" MaxLength="14"
                        onblur="CalcTotal();"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCostPerUnit" FilterType="Numbers,Custom" ValidChars=","
                        TargetControlID="txtCostPerUnit" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvCostPerUnit" runat="server" ValidationGroup="vgBudget"
                        Display="None" ControlToValidate="txtCostPerUnit" ErrorMessage="Enter Cost Per Unit"></asp:RequiredFieldValidator>
                    <label class="iceLable">
                        (USH)</label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Total</label><%--<span class="mandatory"> *</span>--%>
                </td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" CssClass="iceTextBox" MaxLength="24"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeTotal" FilterType="Numbers,Custom" ValidChars=","
                        TargetControlID="txtTotal" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <%-- <asp:RequiredFieldValidator ID="rfvTotal" runat="server" ValidationGroup="vgBudget"
                        Display="None" ControlToValidate="txtTotal" ErrorMessage="Enter Item Total"></asp:RequiredFieldValidator>--%>
                    <label class="iceLable">
                        (USH)</label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <label class="iceLable">
                        Comments</label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtComments" runat="server" Rows="2" CssClass="iceTextBox" TextMode="MultiLine"
                        Style="width: 500px;" MaxLength="1000" onkeydown="return checkMaxLength(this,1000)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <fieldset class="icePnlinner">
                        <legend>Areas of Benefit</legend>
                        <table border="0" cellpadding="3" width="100%">
                            <tr>
                                <td>
                                    <label class="iceLable">
                                        District</label><span class="mandatory"> *</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="201px" AutoPostBack="true"
                                        CssClass="iceDropDown" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                      <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlDistrict"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                                    <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict"
                                        ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a District"
                                        Display="None"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label class="iceLable">
                                        County</label><span class="mandatory"> *</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlCounty" runat="server" Width="201px" AutoPostBack="true"
                                                CssClass="iceDropDown" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                              <ajaxToolkit:ListSearchExtender id="ListSearchExtender3" runat="server"
                        TargetControlID="ddlCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                                            <asp:RequiredFieldValidator ID="rfvCounty" runat="server" ControlToValidate="ddlCounty"
                                                ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a County"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <label class="iceLable">
                                        Sub County</label><span class="mandatory"> *</span>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="uplSubCounty" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlSubCounty" runat="server" Width="201px" AutoPostBack="true"
                                                CssClass="iceDropDown" OnSelectedIndexChanged="ddlSubCounty_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                              <ajaxToolkit:ListSearchExtender id="ListSearchExtender4" runat="server"
                        TargetControlID="ddlSubCounty"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                                            <asp:RequiredFieldValidator ID="rfvSubCounty" runat="server" ControlToValidate="ddlSubCounty"
                                                ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Sub County"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlCounty" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <label class="iceLable">
                                        Parish</label><span class="mandatory"> *</span>
                                </td>
                                <td style="vertical-align: top">
                                    <asp:UpdatePanel ID="uplParish" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlParish" runat="server" CssClass="iceDropDown" Width="201px">
                                                <asp:ListItem Value="0" Selected="False">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                              <ajaxToolkit:ListSearchExtender id="ListSearchExtender6" runat="server"
                        TargetControlID="ddlParish"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                                            <asp:RequiredFieldValidator ID="rfvParish" runat="server" ControlToValidate="ddlParish"
                                                ValidationGroup="vgBudget" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Parish"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="vertical-align: top">
                                    <label class="iceLable">
                                        Villages</label><span class="mandatory"> *</span>
                                </td>
                                <td colspan="3" style="vertical-align: top">
                                    <asp:UpdatePanel ID="uplVillage" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            &nbsp;&nbsp; <asp:CheckBox ID="chkAllVillages" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkAllVillages_CheckedChanged" /><br />
                                            <asp:ListBox ID="lstbVillages" runat="server" SelectionMode="Multiple" Width="201px">
                                            </asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvVillages" runat="server" ControlToValidate="lstbVillages"
                                                ValidationGroup="vgBudget" Text="Mandatory" InitialValue="" ErrorMessage="Select a Villages"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlSubCounty" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="vgBudget"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                    <asp:ValidationSummary ID="valRestPlan" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgBudget" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdRestBudget" runat="server" AllowPaging="True" AllowSorting="True"
                        CellPadding="4" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
                        Width="100%" OnRowCommand="grdRestBudget_RowCommand" OnPageIndexChanging="grdRestBudget_PageIndexChanging"
                        OnRowDataBound="grdRestBudget_RowDataBound" OnRowCreated="grdRestBudget_RowCreated"
                        OnRowEditing="grdRestBudget_RowEditing" OnRowCancelingEdit="grdRestBudget_RowCancelingEdit"
                        OnRowUpdating="grdRestBudget_RowUpdating">
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
                        <FooterStyle CssClass="gridFooterStyle" />
                        <RowStyle CssClass="gridRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="SI No.">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Liv_Res_BudgID" HeaderText="Liv_Res_BudgID" HeaderStyle-HorizontalAlign="Left"
                                Visible="false" />
                            <%--<asp:TemplateField HeaderText="Liv_Res_BudgID" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLiv_Res_BudgID" runat="server" Text='<%#Eval("Liv_Res_BudgID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="Liv_Bud_CategoryName" HeaderText="Category" HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="Category" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLiv_Bud_CategoryName" runat="server" CssClass="iceLable" Text='<%#Eval("Liv_Bud_CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="Liv_Bud_ItemName" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="Item" Visible="true">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLiv_Bud_ItemName" runat="server" CssClass="iceLable" Text='<%#Eval("Liv_Bud_ItemName") %>'
                                       ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="NoOfBeneficial" HeaderText="No Of Beneficial" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="No Of Beneficial" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNoOfBeneficial" runat="server" CssClass="iceLable" Text='<%#Eval("NoOfBeneficial") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItemQuantity" runat="server" CssClass="iceLable" Text='<%#Eval("ItemQuantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="CostPerUnit" HeaderText="Cost Per Unit" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:N0}"/>
                            <%--<asp:TemplateField HeaderText="Cost Per Unit" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCostPerUnit" runat="server" CssClass="iceLable" Text='<%#Eval("CostPerUnit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" DataFormatString="{0:N0}"/>
                            <%--<asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>' CssClass="iceLable"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="District" HeaderText="District" HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDistrict" runat="server" CssClass="iceLable" Text='<%#Eval("District") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="County" HeaderText="County" HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="County" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCounty" runat="server" CssClass="iceLable" Text='<%#Eval("County") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="SubCounty" HeaderText="Sub County" HeaderStyle-HorizontalAlign="Left" />
                            <%--<asp:TemplateField HeaderText="Sub County" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSubCounty" runat="server" CssClass="iceLable" Text='<%#Eval("SubCounty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                        CommandName="EditRow" CommandArgument='<%#Eval("Liv_Res_BudgID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                        CommandName="DeleteRow" CommandArgument='<%#Eval("Liv_Res_BudgID") %>' OnClientClick="return DeleteRecord();"
                                        runat="server" />
                                    <asp:Literal ID="litLiv_Res_BudgID" Text='<%#Eval("Liv_Res_BudgID") %>' Visible="false"
                                        runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
      </div>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete?');
        }

        function CalcTotal() {
            //Get GridView Control
            //alert('Entry');
            var Quantity = document.getElementById('<%=txtItemQuantity.ClientID%>').value;
            var CostPerUnit = document.getElementById('<%=txtCostPerUnit.ClientID%>').value.replace(/,/g, '');
            //alert('Exit');
            if (Quantity == '') {
                Quantity = 0;
                //alert('txtReceived is NaN');
            }
            if (CostPerUnit == '') {
                CostPerUnit = 0;
                //alert('txtReceived is NaN');
            }
            var TotalAmount = parseFloat(Quantity) * parseFloat(RemoveComma(CostPerUnit));
            document.getElementById('<%=txtCostPerUnit.ClientID%>').value = document.getElementById('<%=txtCostPerUnit.ClientID%>').value.replace(/,/g, '');
            document.getElementById('<%=txtTotal.ClientID%>').value = AddComma(TotalAmount);
        }

        function RemoveComma(iValue) {
            return parseFloat(iValue.toString().replace(/,?/g, ""));
        }
        function AddComma(iValue) {
            return iValue.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        function checkMaxLength(e, maxLength) {
            //Calling will be lyk this
            //onkeydown="return checkMaxLength(this,1000)"
            if (e.value.length > parseInt(maxLength)) {
                //               alert('no no no');
                // Set value back to the first 6 characters 
                e.value = e.value.substring(0, parseInt(maxLength));
            }
            return true;
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
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtNoOfBeneficial.ClientID  %>");
            var tat2 = document.getElementById("<%= txtItemQuantity.ClientID  %>");
            var tat3 = document.getElementById("<%= txtCostPerUnit.ClientID  %>");
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == '' && tat2.value.toString().replace(/^\s+/, '') == '' && tat3.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
