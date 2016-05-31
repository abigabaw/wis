<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Loan.aspx.cs" Inherits="WIS.Loan" %>
     <%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Lone Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Sunil Kumar
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div id="divAll">
  
    <div>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <asp:Button ID="btnShowAdd" Text="Add New Purpose of Encumbrance" CssClass="icebutton"
            Width="215px" runat="server" OnClick="btnShowAdd_Click" />
        <asp:Button ID="btnShowSearch" Text="Search Purpose of Encumbrance" CssClass="icebutton"
            Width="220px" runat="server" OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch">
        <fieldset class="icePnlinner">
            <legend>Search Purpose of Encumbrance</legend>
            <table border="0" cellpadding="3" width="100%">
                <tr>
                    <td align="right" style="width: 40%">
                        <asp:Label ID="lblPurposeofencumbrance" runat="server" Text="Purpose of Encumbrance"
                            CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="iceTextBox" Width="300px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="margin-top: 12px;">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlPurposeofencumbranceDetails" runat="server" DefaultButton="btn_Save">
        <fieldset class="icePnlinner">
            <legend>Purpose of Encumbrance Details</legend>
            <table border="0" width="100%">
                <tr>
                    <td align="right" style="width: 40%">
                        <label class="iceLable">
                            Purpose of Encumbrance</label>
                        <span class="mandatory">*</span> &nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="width: 60%">
                        <asp:TextBox ID="txtPurposeofencumbrance" runat="server" class="iceTextBox" MaxLength="250"
                            Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPurposeofencumbrance" ControlToValidate="txtPurposeofencumbrance"
                            ErrorMessage="Enter Purpose of Encumbrance" Display="None" runat="server" ValidationGroup="PurposeofencumbranceGroup"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteEncumbrance" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtPurposeofencumbrance" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="margin-top: 12px;">
                            <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" ValidationGroup="PurposeofencumbranceGroup"
                                OnClick="btn_Save_Click" />&nbsp;<asp:Button ID="btn_Clear" runat="server" Text="Clear"
                                    class="icebutton" OnClick="btn_Clear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummaryPurposeofencumbrance" DisplayMode="BulletList"
        HeaderText="Please enter/correct the following:" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="PurposeofencumbranceGroup" runat="server" />
    <asp:GridView ID="GrdPurposeofencumbrance" runat="server" CssClass="gridStyle" CellPadding="4"
        PageSize="10" CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%"
        AllowPaging="True" OnPageIndexChanging="GrdPurposeofencumbrance_PageIndexChanging"
        OnRowCommand="GrdPurposeofencumbrance_RowCommand">
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
            <asp:BoundField DataField="ENCUMBRANCEPURPOSE" HeaderText="Purpose of Encumbrance"
                HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("ENCUMBRANCEID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("ENCUMBRANCEID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litENCUMBRANCEID" Text='<%#Eval("ENCUMBRANCEID") %>' Visible="false"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" />
        <EmptyDataTemplate>
            There are no records for the selected criteria.
        </EmptyDataTemplate>
    </asp:GridView>
      </div>
    <div class="footer">
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }

            function SetVisible(val) {
                var hf = document.getElementById("<%= hfVisible.ClientID  %>");
                hf.value = val;
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
                var btn = document.getElementById("<%= btn_Save.ClientID  %>");
                var tat1 = document.getElementById("<%= txtPurposeofencumbrance.ClientID  %>");
                if (btn == 'undefined' || btn == null) {
                    isDirty = 0;
                }
                else if (tat1.value.toString().replace(/^\s+/, '') == '' && btn.value.toString() == 'Save') {
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
    </div>
</asp:Content>
