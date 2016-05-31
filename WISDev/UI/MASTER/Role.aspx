<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Role.aspx.cs" Inherits="WIS.Role" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Role Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Victory
 * @Created Date 24-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
      <div id="divAll">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnShowAdd" Text="Add New Role" runat="server" OnClick="btnShowAdd_Click"
                        CssClass="icebutton" />
                </td>
                <td>
                    <asp:Button ID="btnShowSearch" Text="Search Role" runat="server" OnClick="btnShowSearch_Click"
                        CssClass="icebutton" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnSearch">
        <fieldset class="icePnlinner">
            <legend>Search Role</legend>
            <table align="center" border="0" width="40%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Role Name" CssClass="iceLable" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="iceTextBox" Width="250px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <asp:Button ID="btnSearch" CssClass="icebutton" Text="Search" runat="server" OnClick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlRoleDetails" runat="server" DefaultButton="btn_Save">
        <fieldset class="icePnlinner">
            <legend>Role Details</legend>
            <table align="center" border="0" width="90%">
                <tr>
                    <td>
                        <label class="iceLable">
                            Role Name</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleName" runat="server" class="iceTextBox" MaxLength="100" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqRoleName" ControlToValidate="txtRoleName" ErrorMessage="Enter Role Name"
                            Display="None" ValidationGroup="RoleNameGroup" runat="server"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                            ValidChars=". " TargetControlID="txtRoleName" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td align="left">
                        <label class="iceLable">
                            Description</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" class="iceTextBox"
                            MaxLength="200" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Save" Text="Save" runat="server" class="icebutton" OnClick="btn_Save_Click"
                                        ValidationGroup="RoleNameGroup" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Clear" runat="server" Text="Clear" class="icebutton" OnClick="btn_Clear_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
    <asp:ValidationSummary ID="valSummaryRole" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="RoleNameGroup" runat="server" />
    <asp:GridView ID="grdRoles" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
        GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdRoles_RowCommand"
        AllowPaging="True" OnPageIndexChanging="grdRoles_PageIndexChanging">
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
            <asp:BoundField DataField="RoleName" HeaderText="Role Name" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RoleDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                        CommandName="EditRow" CommandArgument='<%#Eval("RoleId") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("RoleId") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litRoleID" Text='<%#Eval("RoleId") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="footer">
    </div>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to Delete this Record?');
            }

            function SetVisible(val) {
                var hf = document.getElementById("<%= hfVisible.ClientID  %>");
                hf.value = val;
            }
//            document.getElementById('divAll').onclick = function () {
//                isDirty = 0;
//                setTimeout(function () { isDirty = 1; }, 100);
//            };

//            var isDirty = 0;
//            function setDirty() {
//                isDirty = 1;
//            }

//            window.onbeforeunload = function DoSome() {
//                if (isDirty == 1) {
//                    //isDirty = 2;
//                    return '';
//                }
            //            } 
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
                var tat1 = document.getElementById("<%= txtRoleName.ClientID  %>");
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
