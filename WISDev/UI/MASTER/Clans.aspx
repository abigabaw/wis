<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true"
    CodeBehind="Clans.aspx.cs" Inherits="WIS.Clans" %>

<%@ MasterType VirtualPath="~/SitePopup.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">



</asp:Content>
<%--/**
 * 
 * @version		 0.1 Clan Status Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 eshwar
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="100%">
        <table width="100%">
            <tr>
                <td>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblClans" runat="server" Text="Clan" CssClass="iceLable"></asp:Label>
                                <span class="mandatory">* </span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClans" runat="server" CssClass="iceTextBoxLarge"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                    ValidChars=" " TargetControlID="txtClans" runat="server">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <%-- <asp:RequiredFieldValidator ID="reqClan" runat="server" ControlToValidate="txtClans"
                                ValidationGroup="vgClan" ErrorMessage="Enter Clan"
                                Display="None"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table align="center">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="vgClan"
                                                OnClick="btnSave_Click" />
                                            <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                                ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="vgClan"
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                                            &nbsp;&nbsp;
                                            <input type="button" id="btnClose" class="icebutton" value="Close" onclick="window.close();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvClans" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
                        GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="gvClans_RowCommand"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="ChangePage">
                        <RowStyle CssClass="gridRowStyle" />
                        <AlternatingRowStyle CssClass="gridAlternateRow" />
                        <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="gridHeaderStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CLANNAME" HeaderText="Clans" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="50%">
                                <HeaderStyle HorizontalAlign="Center" Width="50%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                        CommandName="EditRow" CommandArgument='<%#Eval("CLANID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted ").ToString())%>'
                                        OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                        CommandName="DeleteRow" CommandArgument='<%#Eval("CLANID") %>' OnClientClick="return DeleteRecord();"
                                        runat="server" />
                                    <asp:Literal ID="litCLANID" Text='<%#Eval("CLANID") %>' Visible="false" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <script language="javascript" type="text/javascript">
                        function DeleteRecord() {
                            return confirm('Are you sure you want to Delete this Record?');
                        }
                    </script>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
