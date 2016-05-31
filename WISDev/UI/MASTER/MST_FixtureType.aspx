<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MST_FixtureType.aspx.cs" Inherits="WIS.UI.MASTER.MST_FixtureType" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      
</asp:Content>
<%--/**
 * 
 * @version		 0.1 fixture Type Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Sunil
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <asp:Button ID="btnShowAdd" Text="Add New FixtureType" runat="server" CssClass="icebutton"
            OnClick="btnShowAdd_Click" />&nbsp;
        <asp:Button ID="btnShowSearch" Text="Search FixtureType" runat="server" CssClass="icebutton"
            OnClick="btnShowSearch_Click" />
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server">
        <fieldset class="icePnlinner">
            <legend>Search FixtureType</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <asp:Label ID="Label1" runat="server" Text="Fixture Type" CssClass="iceLable" />
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearchFixtureType" runat="server" CssClass="iceTextBox" 
                            MaxLength="150" Width="356px"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="reqFixtureTypeSearch" runat="server" ErrorMessage=" Enter FixtureType to search"
                                ControlToValidate="txtSearchFixtureType" Display="None" ValidationGroup="Bank"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtFixtureType" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                        <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                            OnClick="btnClearSearch_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlBankDetails" runat="server">
        <fieldset class="icePnlinner">
            <legend>FixtureType Details</legend>
            <table align="center" border="0" width="70%">
                <tr>
                    <td align="left" style="width: 15%">
                        <label class="iceLable">
                            Fixture Type</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left" style="width: 30%">
                        <asp:TextBox ID="txtFixtureType" runat="server" class="iceTextBox" MaxLength="150" 
                            Width="354px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="reqFixtureType" runat="server" ErrorMessage=" Enter Fixture Type"
                                ControlToValidate="txtFixtureType" Display="None" ValidationGroup="Bank"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteBankName" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtFixtureType" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                    <td align="left" style="width: 30%">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="padding-top: 12px">
                        <asp:Button ID="btnSave" Text="Save" runat="server" class="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="Bank" />&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="icebutton" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="valSummaryBank" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Bank" runat="server" />
  <asp:GridView ID="grdBanks" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdBanks_RowCommand"
        PageSize="10" AllowPaging="True" OnPageIndexChanging="grdBanks_PageIndexChanging" OnRowDataBound="grdBanks_RowDataBound">
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
         
           
            <asp:BoundField DataField="FixtureType" HeaderText="Fixture Type" HeaderStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("FixtureID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("FixtureID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litBankID" Text='<%#Eval("FixtureID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="footer">
        <script language="javascript" type="text/javascript">
               function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }      
        </script>
    </div>
</asp:Content>
