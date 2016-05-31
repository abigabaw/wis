<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StructureCategory.aspx.cs" Inherits="WIS.StructureCategory" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Structure Category Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Amalesh.T
 * @Created Date 22-April-203
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
        <asp:Panel ID="pnlStructureCategory" runat="server">
            <fieldset class="icePnlinner">
                <legend>Structure Categories</legend>
                <table align="center" border="0" width="50%">
                    <tr>
                        <asp:ValidationSummary ID="valsumStructureCategory" runat="server" ShowSummary="false"
                            ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                            ValidationGroup="vgStructureCategory" />
                        <td style="width: 30%" align="left">
                            <asp:Label ID="lblStructureCategory" runat="server" Text="Structure Category" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtStructureCategory" runat="server" CssClass="iceTextBox" MaxLength="250"
                                Width="300px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteStructureCategory" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" '" TargetControlID="txtStructureCategory" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtStructureCategory" runat="server" ControlToValidate="txtStructureCategory"
                                ValidationGroup="vgStructureCategory" ErrorMessage="Enter Structure Category"
                                Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%-- <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>                  
                </tr>--%>
                    <tr>
                        <td colspan="2" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="vgStructureCategory"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdStructureCategory" runat="server" CellPadding="4" AllowPaging="True"
            AllowSorting="True" CellSpacing="1" GridLines="None" PageSize="10" AutoGenerateColumns="false"
            Width="100%" OnRowCommand="grdStructureCategory_RowCommand" OnPageIndexChanging="grdStructureCategory_PageIndexChanging">
            <HeaderStyle CssClass="gridHeaderStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" ForeColor="White" />
            <FooterStyle CssClass="gridFooterStyle" />
            <RowStyle CssClass="gridRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StructureCategoryID" HeaderText="StructureCategoryID"
                    Visible="false" />
                <asp:BoundField DataField="StructureCategoryName" HeaderText="Structure Category"
                    HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("StructureCategoryID") %>' runat="server" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("StructureCategoryID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litStructureCategoryID" Text='<%#Eval("StructureCategoryID") %>'
                            Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        function DeleteRecord() {
            return confirm('Are you sure you want to Delete this Record?');
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
            var tat1 = document.getElementById("<%= txtStructureCategory.ClientID  %>");
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
</asp:Content>
