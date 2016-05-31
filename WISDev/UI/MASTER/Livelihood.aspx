<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Livelihood.aspx.cs" Inherits="WIS.Livelihood" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Livelihood Master UI screen   
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
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <div id="divAll">
    <div style="width: 100%">
        <fieldset class="icePnlinner">
        <legend>Livelihood Item</legend>
            <table align="center" border="0" width="55%">
                <tr>
                    <td align="left" style="width: 30%">
                        <asp:Label ID="livelihoodLabel" runat="server" Text="Livelihood Item" CssClass="iceLable" /> <span class="mandatory">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="livelihoodTextBox" runat="server" CssClass="iceTextBox" MaxLength="100" Width="350px" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" ," TargetControlID="livelihoodTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rqelivelihood" ControlToValidate="livelihoodTextBox"
                            ErrorMessage="Enter Livelihood Item" Display="None" ValidationGroup="ValSummary"
                            runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <div style="margin-top: 12px;">
                            <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="ValSummary"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                            <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                                runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        <asp:GridView ID="grdlivelihood" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="False" Width="100%" AllowPaging="true"
            PageSize="10" OnPageIndexChanging="grdlivelihood_PageIndexChanging" OnRowCommand="grdlivelihood_RowCommand"
            OnSelectedIndexChanged="grdlivelihood_SelectedIndexChanged" OnRowDeleting="grdlivelihood_RowDeleting">
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
                <asp:BoundField DataField="ITEMNAME" HeaderText="Livelihood Item" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("ITEMID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="7%" />
            <ItemTemplate>
            <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("ISDELETED").ToString())%>'
             OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
            </ItemTemplate>
            </asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("ITEMID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litUserID" Text='<%#Eval("ITEMID") %>' Visible="false" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("ITEMID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litITEMID" Text='<%#Eval("ITEMID") %>' Visible="false" runat="server"></asp:Literal>
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
         var btn = document.getElementById("<%= btnSave.ClientID  %>");
         var tat1 = document.getElementById("<%= livelihoodTextBox.ClientID  %>");
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
