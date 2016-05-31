<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SocialSupport.aspx.cs"
    Inherits="WIS.SocialSupport" MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 SocialSupport Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Amalesh.T
 * @Created Date 24-April-203
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
                <legend>Social Support</legend>
                <table align="center" border="0" width="50%">
                    <tr>
                        <asp:ValidationSummary ID="ValSumSupportedBy" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="SupportedBy" runat="server" />
                        <td style="width: 30%" align="left">
                            <asp:Label ID="lblSupportedBy" runat="server" Text="Supported By " CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSupportedBy" runat="server" CssClass="iceTextBox" Width="300"
                                AutoCompleteType="Disabled" MaxLength="100" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="support" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                                ValidChars=" ," TargetControlID="txtSupportedBy" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="reqSupportedBy" runat="server" ErrorMessage="Enter Supported By"
                                ValidationGroup="SupportedBy" Display="None" ControlToValidate="txtSupportedBy"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtSupportedByID" runat="server" CssClass="iceTextBoxLarge" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <div style="margin-top: 15px;">
                                <asp:Button ID="SaveButton" CssClass="icebutton" Text="Save" runat="server" OnClick="SaveButton_Click"
                                    ValidationGroup="SupportedBy" />&nbsp;
                                <asp:Button ID="ClearButton" CssClass="icebutton" Text="Clear" runat="server" OnClick="ClearButton_Click" />
                                <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                    ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                                    runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSaveMsg" runat="server" Text="" CssClass="iceLable"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="gvSupportedBy" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" AllowPaging="True"
            AllowSorting="True" Width="100%" OnPageIndexChanging="gvSupportedBy_PageIndexChanging"
            OnRowCommand="gvSupportedBy_RowCommand">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerSettings FirstPageText="<<" LastPageText=">>" NextPageText=">" PreviousPageText="<"
                Mode="NumericFirstLast" Position="Bottom" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
            <HeaderStyle CssClass="gridHeaderStyle" />
            <FooterStyle CssClass="gridFooterStyle" />
            <Columns>
                <asp:TemplateField HeaderText="SI No">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Supported By" HeaderStyle-HorizontalAlign="Center" DataField="SupportedBy"
                    HeaderStyle-Width="50%">
                    <HeaderStyle HorizontalAlign="Center" Width="50%"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("SUPPORTEDBYID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted ").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("SUPPORTEDBYID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litSUPPORTEDBYID" Text='<%#Eval("SUPPORTEDBYID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
       </div>
    <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"></script>
     <script language="javascript" type="text/javascript">
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
             var tat1 = document.getElementById("<%= txtSupportedBy.ClientID  %>");
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
