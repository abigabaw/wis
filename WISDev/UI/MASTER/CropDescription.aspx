<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CropDescription.aspx.cs" Inherits="WIS.CropDescription" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Crop Description UI screen   
 * @package		 Crop Description
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
            <legend>Crop Description</legend>
            <table border="0" align="center" width="45%">
                <tr>
                    <td align="left" width="30%">
                        <asp:Label ID="CropDescriptionLabel" runat="server" Text="Crop Description" CssClass="iceLable" />
                        <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="CropDescriptionTextBox" runat="server" CssClass="iceTextBox" MaxLength="250"
                            Width="250px" />
                        <asp:RequiredFieldValidator ID="reqCropDescr" runat="server" ErrorMessage="Enter Crop Description"
                            ControlToValidate="CropDescriptionTextBox" Display="None" ValidationGroup="CropDescr"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="CropDescriptionIDTextBox" runat="server" CssClass="iceTextBoxLarge"
                            Visible="false" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="CropDescriptionTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="CropDescr"
                            OnClick="SaveButton_Click" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                        <asp:ValidationSummary ID="valsumCropDescr" runat="server" ShowSummary="false" ShowMessageBox="true"
                            HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="CropDescr" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdCropDes" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdCropDes_RowCommand"
            AllowPaging="True" OnPageIndexChanging="ChangePage">
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
                <asp:BoundField DataField="CROPDESNAME" HeaderText="Crop Description" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CROPDESID") %>' runat="server" />
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
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CROPDESID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litCropDescriptionID" Text='<%#Eval("CROPDESID") %>' Visible="false" runat="server"></asp:Literal>
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
                return confirm('Are you sure you want to update this record?');
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
                var tat1 = document.getElementById("<%= CropDescriptionTextBox.ClientID  %>");
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
