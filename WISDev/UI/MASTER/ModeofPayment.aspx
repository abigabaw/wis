﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModeofPayment.aspx.cs" Inherits="WIS.ModeofPayment" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Mode of Payment Master UI screen   
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
<ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
<div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
            <legend>Mode of Payment</legend>
            <table align="center" border="0" width="40%">
                <tr>
                    <td align="left">
                        <label class="iceLable">Type</label>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoTypeCash" GroupName="CompensationType" Text="Cash" runat="server" />
                        <asp:RadioButton ID="rdoTypeInKind" GroupName="CompensationType" Text="In Kind" runat="server" />
                    </td>
                </tr>
                <tr>
                    <asp:ValidationSummary ID="valsumModeofPayment" runat="server" ShowSummary="false" ShowMessageBox="true"
                        HeaderText="Please enter/correct the following:" DisplayMode="BulletList" ValidationGroup="ModeofPayment" />
                    <td align="left" width="35%">
                        <asp:Label ID="lblModeofPayment" runat="server" Text="Mode of Payment" CssClass="iceLable"></asp:Label>
                        <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtModeofPayment" runat="server" MaxLength="25" CssClass="iceTextBox" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqModeofPayment" runat="server" ErrorMessage="Enter Mode of Payment"
                            ControlToValidate="txtModeofPayment" Display="None" ValidationGroup="ModeofPayment"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteModeofPayment" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                            ValidChars=" " TargetControlID="txtModeofPayment" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:TextBox ID="txtModeofPaymentID" runat="server" Visible="false" CssClass="iceTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top: 12px">
                        <asp:Button ID="btnSave" runat="server" CssClass="icebutton" Text="Save" 
                            ValidationGroup="ModeofPayment" onclick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" CssClass="icebutton" Text="Clear" 
                            onclick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
         <asp:GridView ID="grdModeofPayment" runat="server" CssClass="gridStyle" CellPadding="4" CellSpacing="1" GridLines="None"
            AutoGenerateColumns="false" Width="100%" onrowcommand="grdModeofPayment_RowCommand" 
            AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage"> 
            <RowStyle CssClass="gridRowStyle" />
             <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White"/>
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ModeofPayment" HeaderText="Mode of Payment" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif" CommandName="EditRow" CommandArgument='<%#Eval("ModeofPaymentID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="7%" />
            <ItemTemplate>
             <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
             OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
            </ItemTemplate>
            </asp:TemplateField>
             
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("ModeofPaymentID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litModeofPaymentID" Text='<%#Eval("ModeofPaymentID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
        <script language="javascript" type="text/javascript">

            function DeleteRecord() {
                return confirm('Are you sure you want to Delete the Record?');
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
                var tat1 = document.getElementById("<%= txtModeofPayment.ClientID  %>");
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
