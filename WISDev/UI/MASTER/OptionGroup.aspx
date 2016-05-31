<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OptionGroup.aspx.cs" Inherits="WIS.OptionGroup" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Option Group Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 mahalakshmi
 * @Created Date 25-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Option Groups</legend>
            <table border="0" align="center" width="45%">
                <tr>
                    <td width="30%" align="left">
                        <asp:Label ID="OptionGroupLabel" runat="server" Text="Option Group" CssClass="iceLable" />
                        <span class="mandatory">*</span>&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtOptionGroup" runat="server" CssClass="iceTextBox" Width="250px" />
                        <%--<asp:TextBox ID="txtOptionGroupID" runat="server" CssClass="iceTextBoxLarge"
                            Visible="false" />--%>
                        <asp:RequiredFieldValidator ID="reqCropDiam" runat="server" ErrorMessage="Enter Option Group"
                            ControlToValidate="txtOptionGroup" Display="None" ValidationGroup="OptionGroup"></asp:RequiredFieldValidator>
                        <%--    <ajaxToolkit:FilteredTextBoxExtender ID="fteCropDiameter" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                            ValidChars=",- " TargetControlID="CropDiameterTextBox" runat="server">
                     </ajaxToolkit:FilteredTextBoxExtender> --%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" ValidationGroup="OptionGroup"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="icebutton" OnClick="btnClear_Click" />
                        <asp:ValidationSummary ID="valsumOptionGroup" runat="server" ShowSummary="false"
                            ShowMessageBox="true" HeaderText="Please enter/correct the following:" DisplayMode="BulletList"
                            ValidationGroup="OptionGroup" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdOptionGroup" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdOptionGroup_RowCommand"
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
                <%--<asp:BoundField DataField="CropDiaMeterID" HeaderText="CropDiaMeterID" HeaderStyle-HorizontalAlign="Left" />--%>
                <asp:BoundField DataField="OptionGroupName" HeaderText="Option Group" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("OptionGroupID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
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
                            CommandName="DeleteRow" CommandArgument='<%#Eval("OptionGroupID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("OptionGroupID") %>' Visible="false" runat="server"></asp:Literal>
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
                var btn = document.getElementById("<%= btnSave.ClientID  %>");
                var tat1 = document.getElementById("<%= txtOptionGroup.ClientID  %>");
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
