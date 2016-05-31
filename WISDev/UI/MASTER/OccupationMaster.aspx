<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OccupationMaster.aspx.cs" Inherits="WIS.OccupationMaster" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Concern UI screen   
 * @package		 Concern
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
     <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">
         <legend>Occupation</legend>
            <table width="100%">
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="MainOccupationLabel" runat="server" Text="Main Occupation" CssClass="iceLable" /><span
                                        class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="MainOccupationTextBox" MaxLength="100" runat="server" CssClass="iceTextBoxLarge" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                        ValidChars=" ," TargetControlID="MainOccupationTextBox" runat="server">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="reqMO" ControlToValidate="MainOccupationTextBox"
                                        ErrorMessage="Enter Main Occupation" Display="None" ValidationGroup="VsMainOcc"
                                        runat="server"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="MainOccupationIDTextBox" runat="server" CssClass="iceTextBoxLarge"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                                        ValidationGroup="VsMainOcc" />
                                    <asp:ValidationSummary ID="VsMainO" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="VsMainOcc" runat="server" />
                                    <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="msgSaveLabel" runat="server" Text="" CssClass="iceLable" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
               </table>
        </fieldset>
        </asp:Panel>
                
                        <asp:GridView ID="grdOccupation" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdOccupation_RowCommand"
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
                                <%--<asp:BoundField DataField="OCCUPATIONID" HeaderText="OCCUPATIONID" HeaderStyle-HorizontalAlign="Left" />--%>
                                <asp:BoundField DataField="OCCUPATIONNAME" HeaderText="Occupation Name" HeaderStyle-HorizontalAlign="Left" />
                                <%--<asp:BoundField DataField="RoleID" HeaderText="Role" HeaderStyle-HorizontalAlign="Left" />--%>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                            CommandName="EditRow" CommandArgument='<%#Eval("OCCUPATIONID") %>' runat="server" />
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
                                            CommandName="DeleteRow" CommandArgument='<%#Eval("OCCUPATIONID") %>' OnClientClick="return DeleteRecord();"
                                            runat="server" />
                                        <asp:Literal ID="litOCCUPATIONID" Text='<%#Eval("OCCUPATIONID") %>' Visible="false"
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
             var tat1 = document.getElementById("<%= MainOccupationTextBox.ClientID  %>");
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
