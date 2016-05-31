<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConditionsforFinancing.aspx.cs" Inherits="WIS.ConditionsforFinancing" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--/**
 * 
 * @version		  Conditions for Financing UI screen   
 * @package		  Conditions for Financing
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  16-05-2013
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
         <legend>Conditions For Financing</legend>
            <table width="100%">
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td >
                                   <label class="iceLable" > Conditions for Financing</label><span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConditionsTextBox" runat="server" MaxLength="50" CssClass="iceTextBoxLarge" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ConditionsFT" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars = " ," TargetControlID="ConditionsTextBox" runat="server"></ajaxToolkit:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="reqConditions" runat="server" ErrorMessage="Enter Conditions for Financing"
                                        Display="None" ControlToValidate="ConditionsTextBox" ValidationGroup="Conditions"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="ConditionsIDTextBox" runat="server" CssClass="iceTextBoxLarge"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="icebutton" OnClick="SaveButton_Click"
                                                    ValidationGroup="Conditions" />
                                            </td>
                                            <asp:ValidationSummary ID="VsNature" DisplayMode="BulletList" HeaderText="Please enter/correct the following:"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Conditions" runat="server" />
                                            <td>
                                                <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="icebutton" OnClick="ClearButton_Click" />
                                            <%-- <asp:ValidationSummary ID="valSummary" DisplayMode="BulletList" ShowMessageBox="true"
                                        ShowSummary="false" HeaderText="Please enter/correct the following:" ValidationGroup="ValSummary"
                                        runat="server" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                  </table>
        </fieldset>
        </asp:Panel>
                        <asp:GridView ID="grdConditions" runat="server" CssClass="gridStyle" CellPadding="4"
                            CellSpacing="1" GridLines="None" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdConditions_RowCommand"
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
                           
                                <asp:BoundField DataField="FINANCECONDITION" HeaderText="Conditions for Financing" HeaderStyle-HorizontalAlign="Left" />
                          
                              
                               <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageAlign="AbsMiddle" ImageUrl="~/Image/edit.gif"
                                CommandName="EditRow" CommandArgument='<%#Eval("FINANCECONDITIONID") %>' runat="server" />
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

            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="7%" />
                <ItemTemplate>
                    <asp:ImageButton ID="imgDelete" ImageAlign="AbsMiddle" ImageUrl="~/Image/delete.gif"
                        CommandName="DeleteRow" CommandArgument='<%#Eval("FINANCECONDITIONID") %>' OnClientClick="return DeleteRecord();"
                        runat="server" />
                    <asp:Literal ID="litFcondID" Text='<%#Eval("FINANCECONDITIONID") %>' Visible="false" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
         
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
              var tat1 = document.getElementById("<%= ConditionsTextBox.ClientID  %>");
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
