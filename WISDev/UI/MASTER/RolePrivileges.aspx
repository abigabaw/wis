<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RolePrivileges.aspx.cs" Inherits="WIS.RolePrivileges" %>

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
 * @version		 0.1 RoleP rivilegesUI screen   
 * @package		 Role Privileges
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
 * @Created Date 24-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div style="width: 100%">
        <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">
        <legend>Role Privileges</legend>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSaveTop" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="vgUserP"
                            OnClick="SaveButton_Click" />
                        <asp:Button ID="btnClearTop" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>

            <table align="center" border="0" width="60%">
                <tr>
                    <td align="left" width="20%">
                        <label class="iceLable">User Name</label> <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="UserIDDropDownList" runat="server" CssClass="iceDropDownlarge"
                            AutoPostBack="true" OnSelectedIndexChanged="ROLEPRIVDropDownList_SelectedIndexChanged" />
                            <ajaxToolkit:ListSearchExtender id="ListSearchExtender5" runat="server"
                        TargetControlID="UserIDDropDownList"
                        PromptText="Type to search"
                        PromptCssClass="ListSearchExtenderPrompt"
                        PromptPosition="Top"
                        IsSorted="true"/>
                        <asp:RequiredFieldValidator ID="rfvddlRole" runat="server" ControlToValidate="UserIDDropDownList"
                            ValidationGroup="vgUserP" Text="Mandatory" InitialValue="0" ErrorMessage="Select a User Name"
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="valSummary" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="vgUserP" runat="server" />
                    </td>
                </tr>
            </table>

            <asp:Repeater ID="rptRolePrivileges" runat="server" 
                onitemdatabound="rptRolePrivileges_ItemDataBound">
                <HeaderTemplate>
                    <table id="tblRolePrivileges" border="0" align="center" class="gridStyle" width="60%">
                        <tr class="gridHeaderStyle">
                            <td align="center">Item</td>
                            <td align="center" width="15%">
                                View<br />
                                <input type="checkbox" onclick="CheckAllView(this);" />
                            </td>
                            <td align="center" width="15%">
                                Update<br />
                                <input type="checkbox" onclick="CheckAllUpdate(this);" />
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="tr_<%#Eval("MENUID") %>_P<%#Eval("PARENTMENUID") %>" class="gridRowStyle">
                        <td align="left">
                            <asp:Literal id="litMenuID" Text='<%#Eval("MenuID")%>' Visible="false" runat="server"></asp:Literal>
                            <asp:Literal ID="litSpacer" Text="" runat="server"></asp:Literal>                            
                            <asp:Label ID="lblMenuDescription" Text='<%#Eval("MenuDescription") %>' runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:CheckBox ID="chkView" runat="server" />
                        </td>
                        <td align="center">
                           <asp:CheckBox ID="chkUpdate" runat="server" /> 
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="vgUserP"
                            OnClick="SaveButton_Click" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
    </div>
    <script language="javascript" type="text/javascript">
        function CheckAllView(src) {
            tbl = document.getElementById('tblRolePrivileges');
            chkViewArr = tbl.getElementsByTagName('input');
            for (ct = 0; ct < chkViewArr.length; ct++) {
                elem = chkViewArr[ct];

                if (elem.id.indexOf('chkView') >= 0) {
                    elem.checked = src.checked;
                }
            }
        }

        function CheckAllUpdate(src) {
            tbl = document.getElementById('tblRolePrivileges');
            chkViewArr = tbl.getElementsByTagName('input');
            for (ct = 0; ct < chkViewArr.length; ct++) {
                elem = chkViewArr[ct];

                if (elem.id.indexOf('chkUpdate') >= 0) {
                    elem.checked = src.checked;
                }
            }
        }

        function ExpCollapse(parentMnuID) {
            parentMnuID = parentMnuID;
            tbl = document.getElementById('tblRolePrivileges');
            for (rw = 0; rw < tbl.rows.length; rw++) {
                row = tbl.rows[rw];

                if (row.id.indexOf(parentMnuID) >= 0) {
                    row.style.display = 'none';
                }
            }
        }
    </script>
</asp:Content>
