<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dwelling.aspx.cs" Inherits="WIS.Dwelling" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1Drwelling Master UI screen   
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
    <asp:Panel ID="pnlSave" Visible="true" runat="server">
        <fieldset class="icePnlinner">
          <legend>Dwelling</legend>
            <table border="0" width="50%" align="center">
                <tr>
                    <td style="width: 22%">
                        <label class="iceLable">Type Of Dwelling</label> <span class="mandatory">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDwelling" runat="server" CssClass="iceTextBoxLarge" MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" ," TargetControlID="txtDwelling" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                        <asp:TextBox ID="ConcernIDTextBox" runat="server" CssClass="iceTextBox" Visible="false"
                            MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqeDwelling" ControlToValidate="txtDwelling" ErrorMessage="Enter Dwelling type"
                            Display="None" ValidationGroup="ProjectDet" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <div align="center" style="margin-top: 12px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="icebutton" OnClick="btnSave_Click"
                                ValidationGroup="ProjectDet" />&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear"
                                    CssClass="icebutton" OnClick="btnClear_Click" />
                            <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ProjectDet"
                                runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
        <asp:GridView ID="gv_Details" runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False"
            Height="50px" Width="100%" AllowPaging="True" DataKeyNames="DwellingID" 
            AllowSorting="True" PageSize="10" OnPageIndexChanging="gv_Details_PageIndexChanging"
            CellSpacing="1" OnRowCommand="gv_Details_RowCommand">
            <RowStyle CssClass="gridRowStyle" />
            <AlternatingRowStyle CssClass="gridAlternateRow" />
            <PagerStyle CssClass="gridPagerStyle" HorizontalAlign="Center" Font-Bold="true" ForeColor="White"/>
            <HeaderStyle CssClass="gridHeaderStyle" />
            <Columns>
                <asp:TemplateField HeaderText="Sl. No.">
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DwellingType" HeaderText="Type of Dwelling" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Image/edit.gif" runat="server" ID="ImgButton" CommandName="EditRow"
                            CommandArgument='<%#Eval("DwellingID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center" Width="7%" />
            <ItemTemplate>
            <asp:CheckBox id="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("isdeleted ").ToString())%>'
             OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true"  />            
            </ItemTemplate>
            </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("DwellingID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litDwellingID" Text='<%#Eval("DwellingID") %>' Visible="false" runat="server"></asp:Literal>
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
           var tat1 = document.getElementById("<%= txtDwelling.ClientID  %>");
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
