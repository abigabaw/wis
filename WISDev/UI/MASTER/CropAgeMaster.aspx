<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CropAgeMaster.aspx.cs" Inherits="WIS.CropAgeMaster" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Crop Age UI screen   
 * @package		 Crop Age
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
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
        <fieldset class="icePnlinner">
            <legend>Crop Age</legend>
            <table border="0" align="center" width="40%">
                <tr>
                    <td align="left" width="25%">
                        <label class="iceLable">
                            Crop Age</label>
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="CropAgeTextBox" runat="server" CssClass="iceTextBox" MaxLength="50" Width="250px" />
                        <asp:RequiredFieldValidator ID="reqCropAge" runat="server" ErrorMessage="Enter Crop Age"
                            ControlToValidate="CropAgeTextBox" Display="None" ValidationGroup="CropAge"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="CropAgeIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                            ValidChars=" " TargetControlID="CropAgeTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="CropAge"
                            OnClick="SaveButton_Click" />
                        <asp:ValidationSummary ID="valsumCropAge" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="CropAge" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdCropAge" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCropAge_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
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
                <asp:BoundField DataField="CropAge" HeaderText="Crop Age" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CropAgeID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" AutoPostBack="true" OnCheckedChanged="IsObsolete_CheckedChanged"
                            Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CropAgeID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="ltlCropAgeId" runat="server" Text='<%#Eval("CropAgeID") %>' Visible="false"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <script language="javascript" type="text/javascript">
            function DeleteRecord() {
                return confirm('Are you sure you want to delete this record?');
            }
            function ObsoleteRecord() {
                return confirm('Are you sure you want to updagte this record ?');
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
                var btn = document.getElementById("<%= SaveButton.ClientID  %>");
                var tat1 = document.getElementById("<%= CropAgeTextBox.ClientID  %>");
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
