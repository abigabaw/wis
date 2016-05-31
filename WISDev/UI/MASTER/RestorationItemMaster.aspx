<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RestorationItemMaster.aspx.cs" Inherits="WIS.RestorationItemMaster" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Restoration Item Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Victory
 * @Created Date 21-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <asp:Panel ID="pnlShocksExperience" runat="server">
            <fieldset class="icePnlinner">
                <legend>Item</legend>
                <table align="center" border="0" width="40%">
                    <tr>
                        <asp:ValidationSummary ID="valsumItem" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="vgItem" />
                        <td style="width: 25%" align="left">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="iceTextBox" MaxLength="100" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteItem" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=",- " TargetControlID="txtItemName" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtItem" runat="server" ControlToValidate="txtItemName"
                                ValidationGroup="vgItem" ErrorMessage="Enter Item Name" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="vgItem"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdItem" runat="server" CellPadding="4" CellSpacing="1" AllowPaging="True"
            AllowSorting="True" GridLines="None" PageSize="10" AutoGenerateColumns="false"
            Width="100%" OnRowCommand="grdItem_RowCommand" OnPageIndexChanging="grdItem_PageIndexChanging">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Liv_Rest_ItemID" HeaderText="ItemID" Visible="false" />
                <asp:BoundField DataField="Liv_Rest_ItemName" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("Liv_Rest_ItemID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Obsolete Column Added Here--%>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("Liv_Rest_ItemID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litItemID" Text='<%#Eval("Liv_Rest_ItemID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
        }
    </script>
</asp:Content>
