<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CopingMechanism.aspx.cs" Inherits="WIS.CopingMechanism" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		 0.1 coping Mechanisum Master UI screen   
 * @package		 WIS
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Irran
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
        <asp:Panel ID="pnlCopMechanism" runat="server">
            <fieldset class="icePnlinner">
                <legend>Coping Mechanism Details</legend>
                <table align="center" border="0" width="60%">
                    <tr>
                        <td style="width: 30%" align="left">
                            <asp:Label ID="lblCopingMechanism" runat="server" Text="Coping Mechanism" CssClass="iceLable" />
                            <span class="mandatory">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCopingMechanism" runat="server" MaxLength="50" CssClass="iceTextBox" Width="300px" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="fteConcern" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                ValidChars=" ," TargetControlID="txtCopingMechanism" runat="server">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtCopingMechanism" runat="server" ControlToValidate="txtCopingMechanism"
                                ValidationGroup="vgCM" ErrorMessage="Enter Coping Mechanism" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <div style="margin-top: 12px;">
                                <asp:Button ID="btnSave" CssClass="icebutton" Text="Save" runat="server" ValidationGroup="vgCM"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnClear" CssClass="icebutton" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                    ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="vgCM"
                                    runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:GridView ID="grdCopMechansim" runat="server" CssClass="gridStyle" CellPadding="4"
            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdCopMechansim_RowCommand"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="grdCopMechansim_PageIndexChanging">
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
                <asp:BoundField DataField="CopMechanismID" HeaderText="CopMechanismID" Visible="false" />
                <asp:BoundField DataField="CopMechanismName" HeaderText="Coping Mechanism" HeaderStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                            CommandName="EditRow" CommandArgument='<%#Eval("CopMechanismID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obsolete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsObsolete" runat="server" Checked='<%#bool.Parse(Eval("isdeleted ").ToString())%>'
                            OnCheckedChanged="IsObsolete_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                            CommandName="DeleteRow" CommandArgument='<%#Eval("CopMechanismID") %>' OnClientClick="return DeleteRecord();"
                            runat="server" />
                        <asp:Literal ID="litCopMechanismID" Text='<%#Eval("CopMechanismID") %>' Visible="false"
                            runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
     </div>
      <script type="text/javascript">
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
              var tat1 = document.getElementById("<%= txtCopingMechanism.ClientID  %>");
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
        <script language="javascript" type="text/javascript" src="../../Scripts/CommonFunctions.js"/>
</asp:Content>
