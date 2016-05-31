<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FloorType.aspx.cs" Inherits="WIS.FloorType" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Floor Type Master UI screen   
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
    <div style="width: 100%">
        <asp:Panel ID="pnlFloorType" runat="server">
            <fieldset class="icePnlinner">
                <legend>Floor Types</legend>
                <table align="center" border="0" width="40%">
                    <tr>
                        <td style="width: 30%" align="left">
                            <asp:Label ID="lblFloorType" runat="server" Text="Floor Type" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFloorType" runat="server" CssClass="iceTextBox" MaxLength="100"
                                Width="250px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteFloorType" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=",- " TargetControlID="txtFloorType" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rqeVulnerability" ControlToValidate="txtFloorType"
                                ErrorMessage="Enter Floor Type" Display="None" ValidationGroup="VsFloorType"
                                runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="VsFloorType"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:ValidationSummary ID="VsFloor" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsFloorType" runat="server" />
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdFloorType" runat="server" CellPadding="4" AllowPaging="True"
            AllowSorting="True" PageSize="10" CellSpacing="1" GridLines="None" AutoGenerateColumns="false"
            Width="100%" OnRowCommand="grdFloorType_RowCommand" OnPageIndexChanging="grdFloorType_PageIndexChanging">
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
                <asp:BoundField DataField="FloorTypeID" HeaderText="FloorTypeID" Visible="false" />
                <asp:BoundField DataField="FloorTypeName" HeaderText="Floor Type" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("FloorTypeID") %>' runat="server" />
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("FloorTypeID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litFloorTypeID" Text='<%#Eval("FloorTypeID") %>' Visible="false"
                            runat="server"></asp:Literal>
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
            var tat1 = document.getElementById("<%= txtFloorType.ClientID  %>");
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
