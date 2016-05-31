<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FenceDescription.aspx.cs" Inherits="WIS.FenceDescription" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Fence Description Master UI screen   
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
    <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">       
                <legend>Fence Description</legend>
            <table border="0" width="50%" align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblFenceDescription" runat="server" Text="Fence Description" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFenceDescription" runat="server" CssClass="iceTextBoxLarge" MaxLength="250"
                            Width="200px"></asp:TextBox>
                        <asp:TextBox ID="ConcernIDTextBox" runat="server" CssClass="iceTextBox" Visible="false"
                            MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteFence" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=",- " TargetControlID="txtFenceDescription" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rqeFenceDescription" ControlToValidate="txtFenceDescription"
                            ErrorMessage="Enter Fence Description" Display="None" ValidationGroup="VsFence"
                            runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                            ValidationGroup="VsFence" />
                        <asp:ValidationSummary ID="VsFence" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsFence" runat="server" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
        <asp:GridView ID="dv_Details" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4" CellSpacing="1" GridLines="None"
            Height="50px" PageSize="10" Width="100%" OnRowCommand="dv_Details_RowCommand"
            OnPageIndexChanging="dv_Details_PageIndexChanging">
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
                <asp:BoundField DataField="FenceID" HeaderText="FenceID" HeaderStyle-HorizontalAlign="Left"
                    Visible="false" />
                <asp:BoundField DataField="FenceDescription" HeaderText="Fence Description" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                            CommandArgument='<%#Eval("FenceID") %>' />
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
                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/delete.gif" runat="server" ID="ImgDelete" CommandName="DeleteRow"
                            CommandArgument='<%#Eval("FenceID") %>' OnClientClick="return DeleteRecord(); " />
                        <asp:Literal ID="litFenceID" Text='<%#Eval("FenceID") %>' Visible="false" runat="server"></asp:Literal>
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
            var tat1 = document.getElementById("<%= txtFenceDescription.ClientID  %>");
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
