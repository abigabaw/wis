<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShocksExperienced.aspx.cs" Inherits="WIS.ShocksExperienced" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 Shocks Experienced Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Victory
 * @Created Date 21-April-203
 * @Updated By
 * @Updated Date
 *  
 */
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="tsManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
       <div id="divAll">
    <div style="width: 100%">
        <asp:Panel ID="pnlShocksExperience" runat="server">
            <fieldset class="icePnlinner">
                <legend>Shocks Experienced</legend>
                <table align="center" border="0" width="55%">
                    <tr>
                       <td align="left" class="iceLable">
                            <asp:Label ID="lblShocksExperienced" runat="server" Text="Shock Experienced" CssClass="iceLable" /> <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShocksExperienced" runat="server" CssClass="iceTextBox" Width="300px" MaxLength = "100" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="shock" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars = " ," TargetControlID="txtShocksExperienced" runat="server"></ajaxToolkit:FilteredTextBoxExtender>
                           
                            <asp:RequiredFieldValidator ID="rfvtxtShocksExperienced" runat="server" ControlToValidate="txtShocksExperienced"
                                ValidationGroup="vgShocksExperienced" Text="Mandatory!!" ErrorMessage="Enter Shock Experienced"
                                Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="vgShocksExperienced"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                    ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="vgShocksExperienced"
                                    runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdShocksExperience" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdShocksExperience_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="grdShocksExperience_PageIndexChanging" >
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
                <asp:BoundField DataField="ShocksExperiencedID" HeaderText="ShocksExperiencedID"
                    Visible="false" />
                <asp:BoundField DataField="ShocksExperience" HeaderText="Shock Experienced" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("ShocksExperiencedID") %>' runat="server" />
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
                        CommandName="DeleteRow" CommandArgument='<%#Eval("ShocksExperiencedID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litShocksExperiencedID" Text='<%#Eval("ShocksExperiencedID") %>' Visible="false" runat="server"></asp:Literal>
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
              var tat1 = document.getElementById("<%= txtShocksExperienced.ClientID  %>");
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
