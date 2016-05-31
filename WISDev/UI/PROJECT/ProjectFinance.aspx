<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectFinance.aspx.cs" Inherits="WIS.ProjectFinance" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="ProjectMenu.ascx" TagName="ProjectMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProjectMenu ID="ProjectMenu1" runat="server" />
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" EnablePageMethods="true" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <fieldset class="icePnlinner">
        <legend>Financier Details</legend>
        <table border="0" width="100%">
            <tr>
                <td align="left" style="width: 15%">
                    <label class="iceLable">
                        Financier Name</label>
                    <span class="mandatory">*</span>
                </td>
                <td align="left" style="width: 35%">
                    <asp:TextBox ID="txtFinancierName" CssClass="iceTextBox" MaxLength="300" Width="250px" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFinancierName" ControlToValidate="txtFinancierName"
                        ErrorMessage="Enter Financier Name" Display="None" ValidationGroup="Financier"
                        runat="server"></asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="ftetxtFinancierName" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                        ValidChars=" " TargetControlID="txtFinancierName" runat="server">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="left" style="width: 20%">
                    <label class="iceLable">
                        Reason for Financing
                    </label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReason" runat="server" CssClass="iceDropDown" AppendDataBoundItems="True" Width="255px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlReason" InitialValue="0"
                        ErrorMessage="Select Reason for Financing" Display="None" ValidationGroup="Financier" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label class="iceLable">
                        Nature of Financing</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNature" runat="server" CssClass="iceDropDown" AppendDataBoundItems="True"
                        Width="255px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlNature" InitialValue="0"
                        ErrorMessage="Select Nature of Financing" Display="None" ValidationGroup="Financier" runat="server"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <label class="iceLable">
                        Conditions for Financing</label> <span class="mandatory">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCondition" runat="server" CssClass="iceDropDown" AppendDataBoundItems="True"
                        Width="255px">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlCondition" InitialValue="0"
                        ErrorMessage="Select Conditions for Financing" Display="None" ValidationGroup="Financier" runat="server"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" OnClientClick="DisableOnSaveWithVal(this);" UseSubmitBehavior="false"
                        OnClick="btnSave_Click" />&nbsp;
                    <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                    <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Financier" runat="server" />
                </td>
            </tr>
        </table>
    </fieldset>
        <asp:Panel ID="p1Grid" runat="server" ScrollBars="Horizontal" Height="100%">
    <asp:GridView ID="grdFinances" runat="server" CssClass="gridStyle" CellPadding="4"
        CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdFinances_RowCommand"
        AllowPaging="True" OnPageIndexChanging="grdFinances_PageIndexChanging">
        <RowStyle CssClass="gridRowStyle" />
        <AlternatingRowStyle CssClass="gridAlternateRow" />
        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
        <HeaderStyle CssClass="gridHeaderStyle" />
        <Columns>
            <asp:TemplateField HeaderText="Sl. No.">
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FinancierName" HeaderText="Financier Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Financereason" HeaderText="Reason for Financing" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Financenature" HeaderText="Nature of Financing" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Finacecondition" HeaderText="Conditions for Financing"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("FinancierID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("FINANCIERID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litProjectFinanceID" Text='<%#Eval("FINANCIERID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete?');
        }

        spnpnl = document.getElementById('<%=p1Grid.ClientID%>');
        if (spnpnl != null) {
            scrWidth = screen.availWidth;
            spnpnl.style.width = parseInt(scrWidth - 80).toString() + "px";
        }

        function DisableOnSaveWithVal(src) {
            if (Page_ClientValidate()) {
                src.disabled = true;
                src.value = 'Please Wait...';
            }
        }
        var isDirty = 0;
        function setDirty() {
            isDirty = 1;
        }

        function setDirtyText() {
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtFinancierName.ClientID  %>");
            
            if (btn == 'undefined' || btn == null) {
                isDirty = 0;
            }
            else if (tat1.value.toString().replace(/^\s+/, '') == ''  && btn.value.toString() == 'Save') {
                isDirty = 0;
            }
            else {
                isDirty = 1;

            }
        }

        window.onbeforeunload = function DoSome() {
            if (isDirty == 1) {
                return '';
            }
        }                

    </script>
</asp:Content>
