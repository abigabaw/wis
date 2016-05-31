<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NatureofFinancing.aspx.cs" Inherits="WIS.NatureofFinancing" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		Nature of Financing UI  screen   
 * @package		 Nature of Financing
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  14-05-2013
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
         <legend>Nature Of Financing</legend>
            <table align="center" border="0" width="55%">
                <tr>
                    <td width="28%">
                        <label class="iceLable">
                            Nature of Financing</label><span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="NatureTextBox" runat="server" MaxLength="50" CssClass="iceTextBoxLarge" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="NatureFT" FilterType="LowercaseLetters, UppercaseLetters, Custom"
                            ValidChars=" ," TargetControlID="NatureTextBox" runat="server">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="reqReason" runat="server" ErrorMessage="Nature of Financing "
                            Display="None" ControlToValidate="NatureTextBox" ValidationGroup="Nature"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="NatureIDTextBox" runat="server" CssClass="iceTextBoxLarge" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding-top:12px">
                        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                            ValidationGroup="Nature" />
                        <asp:ValidationSummary ID="VsNature" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Nature" runat="server" />
                        <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                    </td>
                </tr>
            </table>
            </fieldset>
            </asp:Panel>
            <asp:GridView ID="grdNature" runat="server" CssClass="gridStyle" CellPadding="4"
                CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdNature_RowCommand"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="ChangePage">
                <RowStyle CssClass="gridRowStyle" />
                <AlternatingRowStyle CssClass="gridAlternateRow" />
                <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White" />
                <HeaderStyle CssClass="gridHeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl. No.">
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FINANCENATURE" HeaderText="Nature of Financing" HeaderStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("FINANCENATUREID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("IsDeleted").ToString())%>'
                                OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                                CommandName="DeleteRow" CommandArgument='<%#Eval("FINANCENATUREID") %>' OnClientClick="return DeleteRecord();"
                                runat="server" />
                            <asp:Literal ID="litNatureID" Text='<%#Eval("FINANCENATUREID") %>' Visible="false"
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        
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
             var tat1 = document.getElementById("<%= NatureTextBox.ClientID  %>");
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
