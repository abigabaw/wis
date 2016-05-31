<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="User_Creation.aspx.cs" Inherits="WIS.User_Creation" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
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
<%--/**
 * 
 * @version		 0.1 User Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Amalesh.t
 * @Created Date 17-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
      <div id="divAll">
    <div style="width: 100%">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnShowAdd" Text="Add New User" runat="server" OnClick="btnShowAdd_Click"
                            CssClass="icebutton" />
                    </td>
                    <td>
                        <asp:Button ID="btnShowSearch" Text="Search Users" runat="server" OnClick="btnShowSearch_Click"
                            CssClass="icebutton" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="pnlSearch" Visible="false" runat="server" DefaultButton="btnUserSearch">
            <fieldset class="icePnlinner" style="margin-top: -06px;">
                <legend>Search Users</legend>
                <table align="center" border="0" width="70%">
                    <tr>
                        <td align="left" style="width: 15%">
                            <asp:Label ID="Label1" runat="server" Text="Username" CssClass="iceLable" />
                        </td>
                        <td align="left" style="width: 40%">
                            <asp:TextBox ID="txtUserNameSearch" runat="server" MaxLength="100" CssClass="iceTextBox"
                                Width="210px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars="." TargetControlID="txtUserNameSearch" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:Label ID="Label3" runat="server" Text="Role" CssClass="iceLable" />
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRoleSearch" runat="server" CssClass="iceDropDownlarge" Width="218px">
                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>                      
                            </asp:DropDownList>
                        <ajaxToolkit:ListSearchExtender id="ListSearchExtender2" runat="server"
                        TargetControlID="ddlRoleSearch"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnUserSearch" CssClass="icebutton" Text="Search" runat="server"
                                OnClick="btnUserSearch_Click" />
                            <asp:Button ID="btnClearSearch" CssClass="icebutton" Text="Clear" runat="server"
                                OnClick="btnClearSearch_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlUserDetails" runat="server" DefaultButton="btnSave">
            <fieldset class="icePnlinner" style="margin-top: -06px;">
                <legend>User Details</legend>
                <table align="center" border="0" width="90%">
                    <tr>
                        <td align="left" style="width: 14%">
                            <asp:Label ID="lblUsername" runat="server" Text="Username" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left" style="width: 36%">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="iceTextBox" MaxLength="50"
                                Width="210px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars="." TargetControlID="txtUsername" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtUsername" runat="server" ControlToValidate="txtUsername"
                                ValidationGroup="vgUser" Text="Mandatory" ErrorMessage="Enter Username" Display="None"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 14%">
                            <asp:Label ID="lblDisplayName" runat="server" Text="Display Name" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDisplayName" runat="server" CssClass="iceTextBox" MaxLength="100"
                                Width="210px" />
                            <asp:RequiredFieldValidator ID="rfvtxtDisplayName" runat="server" ControlToValidate="txtDisplayName"
                                ValidationGroup="vgUser" Text="Mandatory" ErrorMessage="Enter Display Name" Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="LowercaseLetters,UppercaseLetters,Custom"
                                ValidChars="' " TargetControlID="txtDisplayName" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="iceDropDown" Width="218px">
                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender1" runat="server"
                        TargetControlID="ddlRole"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>

                            <asp:RequiredFieldValidator ID="rfvddlRole" runat="server" ControlToValidate="ddlRole"
                                ValidationGroup="vgUser" Text="Mandatory" InitialValue="0" ErrorMessage="Select a Role"
                                Display="None"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblEmailID" runat="server" Text="Email ID" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="iceTextBox" MaxLength="100"
                                Width="210px" />
                            <asp:RequiredFieldValidator ID="rfvtxtEmailID" runat="server" ControlToValidate="txtEmailID"
                                ValidationGroup="vgUser" Text="Mandatory" ErrorMessage="Enter Email ID" Display="None"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REVtxtEmailID" runat="server" ControlToValidate="txtEmailID"
                                ErrorMessage="Invalid Email" Text="Invalid Email" Display="None" ValidationGroup="vgUser"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblCellNumber" runat="server" Text="Mobile Number" CssClass="iceLable" />
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCellNumber" runat="server" MaxLength="11" CssClass="iceTextBox"
                                Width="210px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteMobile" FilterType="Numbers" TargetControlID="txtCellNumber"
                                runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="vgUser"
                                            OnClick="btnSave_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                        <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgUser" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    <asp:HiddenField ID="hfVisible" runat="server" Value="0" />
        <asp:GridView ID="grdUsers" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1"
            GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdUsers_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="grdUsers_PageIndexChanging">
            <%-- <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" />--%>
            <RowStyle CssClass="gridRowStyle" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Username" HeaderText="Username" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DisplayName" HeaderText="Display Name" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="RoleName" HeaderText="Role" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EmailID" HeaderText="Email" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CellNumber" HeaderText="Mobile Number" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="RoleID" HeaderText="Role" HeaderStyle-HorizontalAlign="Left"
                    Visible="false">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("UserID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Obsolete Column Added Here--%>
                <%-- <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" OnClick="return ObsoleteRecord();"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("isdeleted").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("UserID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("UserID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
       </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to delete this Record?');
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
            var btn = document.getElementById("<%= btnSave.ClientID  %>");
            var tat1 = document.getElementById("<%= txtUsername.ClientID  %>");
            var tat2 = document.getElementById("<%= txtDisplayName.ClientID  %>");
            var tat3 = document.getElementById("<%= txtEmailID.ClientID  %>");
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
